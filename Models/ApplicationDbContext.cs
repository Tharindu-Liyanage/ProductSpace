using Microsoft.EntityFrameworkCore;
using ProductSpace.Services;
using System.Security.Cryptography;

namespace ProductSpace.Models
{
    public class ApplicationDbContext : DbContext
    {

        private readonly ICurrentTeanantService _currentTenantService;
        public string CurrentTeanantId { get; set; }
        public string CurrentConnectionString { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, ICurrentTeanantService currentTeanantService) : base(options)
        {
            _currentTenantService = currentTeanantService;
            CurrentTeanantId = _currentTenantService.TenantId;
            CurrentConnectionString = _currentTenantService.ConnectionString;

        }

        public DbSet<Product> Products { get; set; }
       // public DbSet<Tenant> Tenants { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasQueryFilter(x => x.TenantId == CurrentTeanantId);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
             string tenantConnectionString = CurrentConnectionString;
            if(!string.IsNullOrEmpty(tenantConnectionString))
            {
                optionsBuilder.UseSqlServer(tenantConnectionString);
            }
        }

        public override int SaveChanges()
        {
            foreach(var entry in ChangeTracker.Entries<IMustHaveTeanant>().ToList())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                    case EntityState.Modified:
                        entry.Entity.TenantId = CurrentTeanantId;
                        break;

                }
            }
            var result = base.SaveChanges();
            return result;
        }
    }
}
