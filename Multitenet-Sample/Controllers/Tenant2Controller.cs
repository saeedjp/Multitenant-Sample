using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Multitenant_Sample.Context;
using System.Threading.Tasks;

namespace Multitenant_Sample.Controllers
{
    [Route("tenant2/[controller]")]
    [ApiController]
    public class Tenant2Controller : ControllerBase
    {
        private readonly MultiTenantDbContext _context;

        public Tenant2Controller(MultiTenantDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            //If a subdomain is present, no special conditions are needed. However, when testing on localhost without a subdomain, I include the following logic
            var tenant = await _context.Tenants.SingleOrDefaultAsync(x => x.Subdomain == "tenant2");
            return Ok($"Data for subdomain {tenant.Subdomain} (Tenant 2)");
        }
    }

}
