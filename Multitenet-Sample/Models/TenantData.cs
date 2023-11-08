namespace Multitenant_Sample.Models
{
    public class TenantData
    {
        public int Id { get; set; }
        public int TenantId { get; set; }
        public string Data { get; set; }
        public Tenant Tenant { get; set; }
    }
}
