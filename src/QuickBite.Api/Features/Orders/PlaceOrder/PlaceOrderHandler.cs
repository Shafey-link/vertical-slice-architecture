using Microsoft.EntityFrameworkCore;
using QuickBite.Api.Domain;
using QuickBite.Api.Infrastructure.Persistence;

namespace QuickBite.Api.Features.Orders.PlaceOrder;

public class PlaceOrderHandler
{
    private readonly QuickBiteDbContext _db;
    private readonly PlaceOrderValidator _validator;
    private readonly OrderPricing _pricing;
    private readonly PaymentProcessor _payment;
    private readonly OrderNotification _notification;

    public PlaceOrderHandler(
        QuickBiteDbContext db,
        PlaceOrderValidator validator,
        OrderPricing pricing,
        PaymentProcessor payment,
        OrderNotification notification)
    {
        _db = db;
        _validator = validator;
        _pricing = pricing;
        _payment = payment;
        _notification = notification;
    }

    public async Task<PlaceOrderResponse> Handle(PlaceOrderRequest request)
    {
        _validator.Validate(request);

        var restaurant = await GetRestaurant(request.RestaurantId);

        ValidateRestaurant(restaurant);

        var menuItems = await GetMenuItems(request);

        ValidateItems(request, menuItems);

        var subtotal = CalculateSubtotal(request, menuItems);

        var discount = 0m;

        if (string.Equals(request.CouponCode, "SAVE10", StringComparison.OrdinalIgnoreCase))
            discount = subtotal * 0.10m;

        var price = _pricing.Calculate(subtotal, discount, 30m);

        await _payment.Process(price.Total);

        var order = CreateOrder(request, menuItems, price);

        order.Confirm();

        _db.Orders.Add(order);

        await _db.SaveChangesAsync();

        await _notification.Send(order.Id);

        return new PlaceOrderResponse(order.Id);
    }

    private async Task<Restaurant> GetRestaurant(Guid restaurantId)
    {
        var restaurant = await _db.Restaurants
            .SingleOrDefaultAsync(x => x.Id == restaurantId);

        if (restaurant is null)
            throw new InvalidOperationException("Restaurant not found.");

        return restaurant;
    }

    private static void ValidateRestaurant(Restaurant restaurant)
    {
        if (!restaurant.IsOpen)
            throw new InvalidOperationException("Restaurant is closed.");
    }

    private async Task<List<MenuItem>> GetMenuItems(PlaceOrderRequest request)
    {
        var menuItemIds = request.Items
            .Select(x => x.MenuItemId)
            .ToList();

        return await _db.MenuItems
            .Where(x => menuItemIds.Contains(x.Id))
            .ToListAsync();
    }

    private static void ValidateItems(PlaceOrderRequest request, List<MenuItem> menuItems)
    {
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
    }

    private static decimal CalculateSubtotal(PlaceOrderRequest request, List<MenuItem> menuItems)
    {
        return request.Items.Sum(item =>
        {
            var menuItem = menuItems.Single(x => x.Id == item.MenuItemId);
            return menuItem.Price * item.Quantity;
        });
    }

    private static Order CreateOrder(
        PlaceOrderRequest request,
        List<MenuItem> menuItems,
        OrderPrice price)
    {
        var order = new Order(
            request.CustomerId,
            request.RestaurantId,
            price.Total);

        foreach (var item in request.Items)
        {
            var menuItem = menuItems.Single(x => x.Id == item.MenuItemId);

            order.AddItem(item.MenuItemId, item.Quantity, menuItem.Price);
        }

        return order;
    }
}
