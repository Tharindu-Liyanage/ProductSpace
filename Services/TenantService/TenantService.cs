using Microsoft.EntityFrameworkCore;
using ProductSpace.Models;
using ProductSpace.Services.TenantService.DTOs;

namespace ProductSpace.Services.TenantService
{
    public class TenantService : ITenantServce
    {
        private readonly TenantDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly IServiceProvider _serviceProvider;

        public TenantService(TenantDbContext context, IConfiguration configuration, IServiceProvider serviceProvider)
        {
            _context = context;
            _configuration = configuration;
            _serviceProvider = serviceProvider;
        }

        public Tenant CreateTenant(CreateTenantRequsest request)
        {
            string newConnectionString = null;

            if (request.Isolated == true)
            {
                string dbName = "productSpaceDb-" + request.id;
                string defaultConnectionString = _configuration.GetConnectionString("DefaultConnection");
                newConnectionString = defaultConnectionString.Replace("productSpaceDb", dbName);

                try
                {
                    using IServiceScope scope = _serviceProvider.CreateScope();
                    ApplicationDbContext dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                    dbContext.Database.SetConnectionString(newConnectionString);

                    if (dbContext.Database.GetPendingMigrations().Any())
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine($"Migrating database for tenant {request.id}");
                        Console.ResetColor();
                        dbContext.Database.Migrate();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error creating tenant database", ex);
                }
            }

            Tenant tenant = new()
            {
                Id = request.id,
                Name = request.Name,
                ConnectionString = newConnectionString
            };

            _context.Add(tenant);
            _context.SaveChanges();
            return tenant;
        }



    }
}

