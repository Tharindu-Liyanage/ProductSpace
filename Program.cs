using Microsoft.EntityFrameworkCore;
using ProductSpace.Extensions;
using ProductSpace.Middleware;
using ProductSpace.Models;
using ProductSpace.Services;
using ProductSpace.Services.TenantService;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddScoped<ICurrentTeanantService, CurrentTenantService>();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDbContext<TenantDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAndMigrateTenantDatabases(builder.Configuration);

builder.Services.AddTransient<IProductService,ProductService>();

builder.Services.AddScoped<ITenantServce, TenantService>();

var app = builder.Build();



app.UseAuthorization();
app.UseMiddleware<TenantResolver>();

app.MapControllers();

app.Run();
