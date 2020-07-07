using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Microsoft.Extensions.Logging;
using System;
using Users.API.Infrastructure;
using Users.API.Models.Context;
using Prometheus;

namespace VisualOffice
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment dev)
        {
            _environment = dev;
            _configuration = configuration;
        }

        public IConfiguration _configuration { get; }
        public IWebHostEnvironment _environment { get; }
        public string database;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            //Healthcheck
            services.AddHealthChecks()
                .AddCheck<HealthCheck>("UsersAPI");

            // ReferenceLoopHandling is currently not supported in the System.Text.Json serializer.
            services.AddControllers().AddNewtonsoftJson(options =>
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );
            // Database connection
            if (_environment.IsDevelopment())
            {
                database = _configuration.GetConnectionString("Development");
            }
            else
            {
                string server = Environment.GetEnvironmentVariable("MYSQL_SERVER_NAME");
                string userid = Environment.GetEnvironmentVariable("MYSQL_USER");
                string password = Environment.GetEnvironmentVariable("MYSQL_USER_PASSWORD");
                string db = Environment.GetEnvironmentVariable("MYSQL_DATABASE");
                database = $"server={server};userid={userid};password={password};database={db};";
            }
            services.AddDbContext<dbContext>(options => options.UseMySQL(database));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            logger.LogInformation($"ConnectionString={database}");
            // Logger Serilog
            app.UseSerilogRequestLogging();

            // Prometheus
            app.UseMetricServer();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health");
            });
        }
    }
}
