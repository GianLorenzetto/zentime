using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using ZenTime.Api.Database;
using ZenTime.Api.Extensions;

namespace ZenTime.Api
{
    public class Program
    {
        private const int WebHostReturnCodeSuccess = 0;
        private const int WebHostReturnCodeFailure = 1;
        
        public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
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
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
        
        private static void RunDatabaseDropAndMigrateIfRequired(IHost host)
        {
            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;

            var hostingEnvironment = services.GetService<IWebHostEnvironment>();
            if (!hostingEnvironment.IsLocal()) return;
            
            try
            {
                Log.Warning(" !!! Running database drop and migrate !!!");
                var context = services.GetRequiredService<ZenTimeDbContext>();
                context.Database.EnsureDeleted();
                context.Database.Migrate();
                
                Log.Warning(" +++ Seeding database with LOCAL data +++");
                ZenTimeDbLocalDev.InsertSeedData(context);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred during database drop and migrate.");
            }
        }
    }
}
