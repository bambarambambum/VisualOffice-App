using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Users.API.Infrastructure;
using Users.API.Models.Context;

namespace VisualOffice
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment dev)
        {
            Environment = dev;
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

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

            if (Environment.IsDevelopment())
            {
                var database = Configuration.GetConnectionString("VisualOfficeDev");
                services.AddDbContext<dbContext>(options => options.UseMySQL(database));
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

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
