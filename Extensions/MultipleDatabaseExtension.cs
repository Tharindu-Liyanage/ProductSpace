using Microsoft.EntityFrameworkCore;
using ProductSpace.Models;

namespace ProductSpace.Extensions
{
    public static class MultipleDatabaseExtension
    {
        public static IServiceCollection AddAndMigrateTenantDatabases(this IServiceCollection services, IConfiguration configuration)
        {

            using IServiceScope scopeTenant = services.BuildServiceProvider().CreateScope();
            TenantDbContext tenantDbContext = scopeTenant.ServiceProvider.GetRequiredService<TenantDbContext>();

            if (tenantDbContext.Database.GetPendingMigrations().Any())
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Migrating Tenant Database");
                Console.ResetColor();
                tenantDbContext.Database.Migrate();
            }

            List<Tenant> tenants = tenantDbContext.Tenants.ToList();
            string defaultConnectionString = configuration.GetConnectionString("DefaultConnection");

            foreach (Tenant tenant in tenants)
            {
                string newConnectionString = string.IsNullOrEmpty(tenant.ConnectionString) ? defaultConnectionString : tenant.ConnectionString;
                using IServiceScope scope = services.BuildServiceProvider().CreateScope();
                ApplicationDbContext dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                dbContext.Database.SetConnectionString(newConnectionString);

                if (dbContext.Database.GetPendingMigrations().Any())
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine($"Migrating database for tenant {tenant.Id}");
                    Console.ResetColor();
                    dbContext.Database.Migrate();
                }

            }

            return services;
        }
    }
}
