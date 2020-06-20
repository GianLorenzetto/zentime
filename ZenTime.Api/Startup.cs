using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using ZenTime.Api.Domain.TimeSheets;
using ZenTime.Api.Extensions;
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

            app.UseSerilogRequestLogging();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
