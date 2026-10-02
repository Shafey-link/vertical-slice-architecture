using Microsoft.EntityFrameworkCore;
using QuickBite.Api.Features.Orders.CancelOrder;
using QuickBite.Api.Features.Orders.PlaceOrder;
using QuickBite.Api.Features.Orders.TrackOrder;
using QuickBite.Api.Features.Payments.RefundPayment;
using QuickBite.Api.Features.Restaurants.GetRestaurant;
using QuickBite.Api.Infrastructure.Notifications;
using QuickBite.Api.Infrastructure.Payments;
using QuickBite.Api.Infrastructure.Persistence;
using QuickBite.Api.Shared.Payments;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<QuickBiteDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("QuickBite")));

builder.Services.AddScoped<PaymentCalculator>();
builder.Services.AddScoped<PaymentGateway>();
builder.Services.AddScoped<NotificationSender>();

builder.Services.AddScoped<PlaceOrderValidator>();
builder.Services.AddScoped<OrderPricing>();
builder.Services.AddScoped<PaymentProcessor>();
builder.Services.AddScoped<OrderNotification>();
builder.Services.AddScoped<PlaceOrderHandler>();
builder.Services.AddScoped<CancelOrderHandler>();
builder.Services.AddScoped<TrackOrderHandler>();
builder.Services.AddScoped<RefundPaymentHandler>();
builder.Services.AddScoped<GetRestaurantHandler>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<QuickBiteDbContext>();
    db.Database.EnsureDeleted();
    db.Database.EnsureCreated();
    db.Seed();
}

app.MapPlaceOrder();
app.MapCancelOrder();
app.MapTrackOrder();
app.MapRefundPayment();
app.MapGetRestaurant();

app.Run();
