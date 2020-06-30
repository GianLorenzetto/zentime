using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using ZenTime.Api.Extensions;
using ZenTime.Database;

namespace ZenTime.Api
{
    public class Program
    {
        private const int WebHostReturnCodeSuccess = 0;
        private const int WebHostReturnCodeFailure = 1;

        public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile(
                $"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json",
                optional: true)
            .AddEnvironmentVariables()
            .Build();

        public static int Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Debug)
                .ReadFrom.Configuration(Configuration)
                .CreateLogger();

            try
            {
                var host = CreateHostBuilder(args).Build();

                RunDatabaseDropAndMigrateIfRequired(host);

                Log.Information("Starting web host");
                host.Run();
                return WebHostReturnCodeSuccess;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Web host failed to start");
                return WebHostReturnCodeFailure;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });

        private static void RunDatabaseDropAndMigrateIfRequired(IHost host)
        {
            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;

            var hostingEnvironment = services.GetService<IWebHostEnvironment>();
            if (!hostingEnvironment.IsLocal()) return;

            Log.Warning(" !!! Running database drop and migrate !!!");
            var migrator = new DatabaseMigrator(Configuration.GetConnectionString("ZenTimeDatabase"));
            migrator.DropDatabase();
            migrator.EnsureDatabaseExists();

            if (!migrator.RunIncrementalMigrations().Successful
                || !migrator.SeedDatabase().Successful)
                throw new Exception("Error running database migrations, check logs for details");
        }
    }
}