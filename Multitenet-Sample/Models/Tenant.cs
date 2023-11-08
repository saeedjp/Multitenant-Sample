using System.Collections.Generic;

namespace Multitenant_Sample.Models
{
    public class Tenant
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Subdomain { get; set; }
        public List<TenantData> TenantData { get; set; }
    }
}
