using QuickBite.Api.Data;
using QuickBite.Api.Entities;

namespace QuickBite.Api.Features.Orders.PlaceOrder;

public class PlaceOrderHandler
{
    private readonly QuickBiteDbContext _db;

    public PlaceOrderHandler(QuickBiteDbContext db)
    {
        _db = db;
    }

    public async Task<PlaceOrderResponse> Handle(PlaceOrderRequest request)
    {
        var order = new Order
        {
            Id = Guid.NewGuid(),
            CustomerId = request.CustomerId,
            RestaurantId = request.RestaurantId,
            CreatedAt = DateTime.UtcNow
        };

        foreach (var item in request.Items)
        {
            order.Items.Add(new OrderItem
            {
                Id = Guid.NewGuid(),
                OrderId = order.Id,
                MenuItemId = item.MenuItemId,
                Quantity = item.Quantity,
                UnitPrice = item.UnitPrice
            });
        }

        order.TotalAmount = order.Items.Sum(x => x.Quantity * x.UnitPrice);

        _db.Orders.Add(order);

        await _db.SaveChangesAsync();

        return new PlaceOrderResponse(order.Id);
    }
}
