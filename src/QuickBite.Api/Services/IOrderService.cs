namespace QuickBite.Api.Services;

public record PlaceOrderRequest(
    Guid CustomerId,
    Guid RestaurantId,
    List<PlaceOrderItemRequest> Items);

public record PlaceOrderItemRequest(
    Guid MenuItemId,
    int Quantity,
    decimal UnitPrice);

public record PlaceOrderResponse(Guid OrderId);

public interface IOrderService
{
    Task<PlaceOrderResponse> PlaceOrder(PlaceOrderRequest request);
}
