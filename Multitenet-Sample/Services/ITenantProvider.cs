using Multitenant_Sample.Models;
using System.Threading.Tasks;

namespace Multitenant_Sample.Services
{
    public interface ITenantProvider
    {
        //This service is used to determine the tenant based on the subdomain it originates from.
        Task<Tenant> GetTenantAsync();
    }
}