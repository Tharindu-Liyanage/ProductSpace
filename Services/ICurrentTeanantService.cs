namespace ProductSpace.Services
{
    public interface ICurrentTeanantService
    {
        string? TenantId { get; set; }

        public Task<bool> SetTenant(string tenant);
    }
}
