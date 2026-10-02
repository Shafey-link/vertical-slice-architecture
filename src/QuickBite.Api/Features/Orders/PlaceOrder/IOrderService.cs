namespace QuickBite.Api.Features.Orders.PlaceOrder;

public interface IOrderService
{
    Task<PlaceOrderResponse> PlaceOrder(PlaceOrderRequest request);
}
