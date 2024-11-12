using Microsoft.EntityFrameworkCore;
using ProductSpace.Models;

namespace ProductSpace.Services
{
    public class CurrentTenantService : ICurrentTeanantService
    {

        private readonly TenantDbContext _dbContext;

        public CurrentTenantService(TenantDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public string? TenantId { get; set; }

        public async Task<bool> SetTenant(string tenant)
        {
            var tenantEntity = await _dbContext.Tenants.Where(x => x.Id == tenant).FirstOrDefaultAsync();
            if (tenantEntity == null)
            {
                throw new Exception("Tenant invalid");

            }
            else
            {
            TenantId = tenantEntity.Id;
            return true;

            }
        }
    }
}
