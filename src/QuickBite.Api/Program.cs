using Microsoft.EntityFrameworkCore;
using QuickBite.Api.Data;
using QuickBite.Api.Features.Orders.PlaceOrder;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<QuickBiteDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("QuickBite")));

builder.Services.AddScoped<PlaceOrderValidator>();
builder.Services.AddScoped<OrderPricing>();
builder.Services.AddScoped<PaymentProcessor>();
builder.Services.AddScoped<OrderNotification>();
builder.Services.AddScoped<PlaceOrderHandler>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<QuickBiteDbContext>();
    db.Database.EnsureDeleted();
    db.Database.EnsureCreated();
    db.Seed();
}

app.MapPlaceOrder();

app.Run();
