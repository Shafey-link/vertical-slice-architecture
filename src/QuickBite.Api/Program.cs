using Microsoft.EntityFrameworkCore;
using QuickBite.Api.Data;
using QuickBite.Api.Repositories;
using QuickBite.Api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<QuickBiteDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("QuickBite")));

builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<QuickBiteDbContext>();
    db.Database.EnsureDeleted();
    db.Database.EnsureCreated();
}

app.MapControllers();

app.Run();
