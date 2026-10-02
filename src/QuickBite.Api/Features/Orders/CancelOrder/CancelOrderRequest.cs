namespace QuickBite.Api.Features.Orders.CancelOrder;

public record CancelOrderRequest(Guid OrderId);

public record CancelOrderResponse(Guid OrderId, string Status);
