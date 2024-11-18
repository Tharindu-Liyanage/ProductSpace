using Microsoft.Identity.Client;

namespace ProductSpace.Services.TenantService.DTOs
{
    public class CreateTenantRequsest
    {
        public string id { get; set; }
        public string Name { get; set; }

        public bool Isolated{get; set;}
    }
}
