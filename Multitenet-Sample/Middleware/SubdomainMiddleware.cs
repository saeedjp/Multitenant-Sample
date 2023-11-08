using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Multitenant_Sample.Services;
using System;
using System.Threading.Tasks;

namespace Multitenant_Sample.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class SubdomainMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IServiceProvider _serviceProvider;

        public SubdomainMiddleware(RequestDelegate next, IServiceProvider serviceProvider)
        {
            _next = next;
            _serviceProvider = serviceProvider;

        }

        public async Task Invoke(HttpContext context)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var tenantProvider = scope.ServiceProvider.GetRequiredService<ITenantProvider>();
                var tenant = await tenantProvider.GetTenantAsync();
                if (tenant != null)
                {
                    context.Items["Tenant"] = tenant;
                }
            }

            await _next(context);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class SubdomainMiddlewareExtensions
    {
        public static IApplicationBuilder UseSubdomainMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<SubdomainMiddleware>();
        }
    }
}
