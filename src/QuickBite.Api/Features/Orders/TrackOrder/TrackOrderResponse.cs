namespace QuickBite.Api.Features.Orders.TrackOrder;

public record TrackOrderResponse(Guid OrderId, string Status, decimal TotalAmount);
