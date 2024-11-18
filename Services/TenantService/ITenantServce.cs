using ProductSpace.Models;
using ProductSpace.Services.TenantService.DTOs;

namespace ProductSpace.Services.TenantService
{
    public interface ITenantServce
    {
        Tenant CreateTenant(CreateTenantRequsest request);
    }
}
