using QuickBite.Api.Entities;
using QuickBite.Api.Repositories;

namespace QuickBite.Api.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _repository;

    public OrderService(IOrderRepository repository)
    {
        _repository = repository;
    }

    public async Task<PlaceOrderResponse> PlaceOrder(PlaceOrderRequest request)
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

        await _repository.AddAsync(order);

        return new PlaceOrderResponse(order.Id);
    }
}
