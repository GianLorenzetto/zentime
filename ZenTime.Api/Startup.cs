using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using ZenTime.Api.Extensions;
using ZenTime.Application.Queries;
using ZenTime.Application.Services;
using ZenTime.Common;
using ZenTime.Database;

namespace ZenTime.Api
{
    public class Startup
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public Startup(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IDateTimeOffsetProvider, DateTimeOffsetProvider>();
            services.AddScoped<ITimeSheetService, TimeSheetService>();
            
            services.AddControllers();
            services.AddDbContext<ZenTimeDbContext>(builder =>
            {
                builder
                    .EnableSensitiveDataLogging(_webHostEnvironment.IsLocal())
                    .UseSqlServer(Configuration.GetConnectionString("ZenTimeDatabase"));
            });

            services.AddSwaggerDocument(settings =>
            {
                var envName = _webHostEnvironment.EnvironmentName.ToLower();
                settings.PostProcess = document =>
                {
                    document.Info.Version = envName;
                    document.Info.Title = "ZenTime API";
                    document.Info.Description = $"ZenTime API - {envName}";
                    document.Info.TermsOfService = "None";
                    document.Info.Contact = new NSwag.OpenApiContact
                    {
                        Name = "Gian Lorenzetto",
                        Email = "",
                        Url = "https://gianlorenzetto.net"
                    };
                };

            });

            services.AddMediatR(typeof(GetAllActivities));
            
            if (_webHostEnvironment.IsLocal())
            {
                services.AddMemoryCache();
                services
                    .AddMiniProfiler(options => options.RouteBasePath = "/profiler")
                    .AddEntityFramework();
            }
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            if (env.IsLocal())
            {
                app.UseMiniProfiler();
            }

            app.ConfigureExceptionHandler(loggerFactory.CreateLogger<Startup>());
            app.UseSerilogRequestLogging();
            app.UseOpenApi();
            app.UseSwaggerUi3();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
