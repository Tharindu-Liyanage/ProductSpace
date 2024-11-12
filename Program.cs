using Microsoft.EntityFrameworkCore;
using ProductSpace.Middleware;
using ProductSpace.Models;
using ProductSpace.Services;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDbContext<TenantDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddTransient<IProductService,ProductService>();
builder.Services.AddScoped<ICurrentTeanantService, CurrentTenantService>();

var app = builder.Build();



app.UseAuthorization();
app.UseMiddleware<TenantResolver>();

app.MapControllers();

app.Run();
