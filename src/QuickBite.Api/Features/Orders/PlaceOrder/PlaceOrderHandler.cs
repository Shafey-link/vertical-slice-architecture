using Microsoft.EntityFrameworkCore;
using QuickBite.Api.Data;
using QuickBite.Api.Entities;

namespace QuickBite.Api.Features.Orders.PlaceOrder;

public class PlaceOrderHandler
{
    private readonly QuickBiteDbContext _db;
    private readonly PlaceOrderValidator _validator;

    public PlaceOrderHandler(QuickBiteDbContext db, PlaceOrderValidator validator)
    {
        _db = db;
        _validator = validator;
    }

    public async Task<PlaceOrderResponse> Handle(PlaceOrderRequest request)
    {
        _validator.Validate(request);

        var restaurant = await _db.Restaurants
            .SingleOrDefaultAsync(x => x.Id == request.RestaurantId);

        if (restaurant is null)
            throw new InvalidOperationException("Restaurant not found.");

        if (!restaurant.IsOpen)
            throw new InvalidOperationException("Restaurant is closed.");

        var menuItemIds = request.Items
            .Select(x => x.MenuItemId)
            .ToList();

        var menuItems = await _db.MenuItems
            .Where(x => menuItemIds.Contains(x.Id))
            .ToListAsync();

        foreach (var item in request.Items)
        {
            var menuItem = menuItems.SingleOrDefault(x => x.Id == item.MenuItemId);

            if (menuItem is null)
                throw new InvalidOperationException("Menu item not found.");

            if (!menuItem.IsAvailable)
                throw new InvalidOperationException($"{menuItem.Name} is unavailable.");

            if (menuItem.RestaurantId != request.RestaurantId)
                throw new InvalidOperationException($"{menuItem.Name} is not on this restaurant's menu.");
        }

        var subtotal = request.Items.Sum(item =>
        {
            var menuItem = menuItems.Single(x => x.Id == item.MenuItemId);
            return menuItem.Price * item.Quantity;
        });

        var discount = 0m;

        if (string.Equals(request.CouponCode, "SAVE10", StringComparison.OrdinalIgnoreCase))
            discount = subtotal * 0.10m;

        var tax = (subtotal - discount) * 0.14m;

        var deliveryFee = 30m;

        var total = subtotal - discount + tax + deliveryFee;

        Console.WriteLine($"Payment processed: {total}");

        var order = new Order
        {
            Id = Guid.NewGuid(),
            CustomerId = request.CustomerId,
            RestaurantId = request.RestaurantId,
            CreatedAt = DateTime.UtcNow,
            TotalAmount = total
        };

        foreach (var item in request.Items)
        {
            var menuItem = menuItems.Single(x => x.Id == item.MenuItemId);

            order.Items.Add(new OrderItem
            {
                Id = Guid.NewGuid(),
                OrderId = order.Id,
                MenuItemId = item.MenuItemId,
                Quantity = item.Quantity,
                UnitPrice = menuItem.Price
            });
        }

        _db.Orders.Add(order);

        await _db.SaveChangesAsync();

        Console.WriteLine($"Order notification sent: {order.Id}");

        return new PlaceOrderResponse(order.Id);
    }
}
