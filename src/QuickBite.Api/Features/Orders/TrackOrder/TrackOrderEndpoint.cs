namespace QuickBite.Api.Features.Orders.TrackOrder;

public static class TrackOrderEndpoint
{
    public static void MapTrackOrder(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/api/orders/{orderId:guid}", async (
            Guid orderId,
            TrackOrderHandler handler) =>
        {
            var result = await handler.Handle(orderId);

            return Results.Ok(result);
        });
    }
}
