using Microsoft.EntityFrameworkCore;

namespace ProductSpace.Models
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Tenant> Tenants { get; set; }
    }
}
