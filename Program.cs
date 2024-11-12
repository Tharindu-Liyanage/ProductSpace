using Microsoft.EntityFrameworkCore;
using ProductSpace.Models;
using ProductSpace.Services;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddTransient<IProductService,ProductService>();


var app = builder.Build();



app.UseAuthorization();

app.MapControllers();

app.Run();
