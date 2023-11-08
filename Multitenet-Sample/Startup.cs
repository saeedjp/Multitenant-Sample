using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Multitenant_Sample.Context;
using Multitenant_Sample.Middleware;
using Multitenant_Sample.Services;

namespace Multitenant_Sample
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Multitenant_Sample", Version = "v1" });
            });
            services.AddHttpContextAccessor();

            string connectionString = Configuration.GetConnectionString("MyDatabaseConnection");
            services.AddDbContext<MultiTenantDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            services.AddScoped<ITenantProvider, SubdomainTenantProvider>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSubdomainMiddleware();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Multitenant_Sample v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                // Add routes for Tenant1Controller and Tenant2Controller
                endpoints.MapControllerRoute(
                    name: "tenant1",
                    pattern: "tenant1/{controller=Tenant1}/{action=Index}"
                );

                endpoints.MapControllerRoute(
                    name: "tenant2",
                    pattern: "tenant2/{controller=Tenant2}/{action=Index}"
                );

                endpoints.MapControllers();
            });
        }
    }
}
