using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Multitenant_Sample.Context;
using Multitenant_Sample.Models;
using System.Threading.Tasks;

namespace Multitenant_Sample.Services
{
    public class SubdomainTenantProvider : ITenantProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly MultiTenantDbContext _context;

        public SubdomainTenantProvider(IHttpContextAccessor httpContextAccessor, MultiTenantDbContext context)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }

        public async Task<Tenant> GetTenantAsync()
        {
            var host = _httpContextAccessor.HttpContext.Request.Host.Host;
            var subdomain = host.Split('.')[0];

            return await _context.Tenants.SingleOrDefaultAsync(t => t.Subdomain == subdomain);
        }

    }
}
