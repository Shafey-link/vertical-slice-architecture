namespace QuickBite.Api.Features.Orders.PlaceOrder;

public record PlaceOrderRequest(
    Guid CustomerId,
    Guid RestaurantId,
    List<PlaceOrderItemRequest> Items);

public record PlaceOrderItemRequest(
    Guid MenuItemId,
    int Quantity,
    decimal UnitPrice);

public record PlaceOrderResponse(Guid OrderId);
