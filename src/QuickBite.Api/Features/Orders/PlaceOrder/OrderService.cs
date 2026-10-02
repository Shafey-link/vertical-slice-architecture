using QuickBite.Api.Entities;
using QuickBite.Api.Repositories;

namespace QuickBite.Api.Features.Orders.PlaceOrder;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _repository;

    public OrderService(IOrderRepository repository)
    {
        _repository = repository;
    }

    public async Task<PlaceOrderResponse> PlaceOrder(PlaceOrderRequest request)
    {
        ValidateCustomer(request);
        CheckRestaurant(request.RestaurantId);

        var subtotal = CalculateSubtotal(request);
        var discount = ApplyCoupon(subtotal);
        var tax = CalculateTax(subtotal - discount);
        var deliveryFee = CalculateDeliveryFee();
        var total = subtotal - discount + tax + deliveryFee;

        await ProcessPayment(total);

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
            order.Items.Add(new OrderItem
            {
                Id = Guid.NewGuid(),
                OrderId = order.Id,
                MenuItemId = item.MenuItemId,
                Quantity = item.Quantity,
                UnitPrice = item.UnitPrice
            });
        }

        await _repository.AddAsync(order);
        await SendNotification(order.Id);

        return new PlaceOrderResponse(order.Id);
    }

    private void ValidateCustomer(PlaceOrderRequest request)
    {
        if (request.CustomerId == Guid.Empty)
            throw new ArgumentException("Customer is required.");
    }

    private void CheckRestaurant(Guid restaurantId)
    {
        if (restaurantId == Guid.Empty)
            throw new ArgumentException("Restaurant is required.");
    }

    private decimal CalculateSubtotal(PlaceOrderRequest request)
    {
        return request.Items.Sum(x => x.Quantity * x.UnitPrice);
    }

    private decimal ApplyCoupon(decimal subtotal)
    {
        return subtotal * 0.10m;
    }

    private decimal CalculateTax(decimal amount)
    {
        return amount * 0.14m;
    }

    private decimal CalculateDeliveryFee()
    {
        return 30m;
    }

    private Task ProcessPayment(decimal amount)
    {
        return Task.CompletedTask;
    }

    private Task SendNotification(Guid orderId)
    {
        return Task.CompletedTask;
    }
}
