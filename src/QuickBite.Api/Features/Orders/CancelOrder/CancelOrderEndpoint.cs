namespace QuickBite.Api.Features.Orders.CancelOrder;

public static class CancelOrderEndpoint
{
    public static void MapCancelOrder(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("/api/orders/{orderId:guid}/cancel", async (
            Guid orderId,
            CancelOrderHandler handler) =>
        {
            var result = await handler.Handle(new CancelOrderRequest(orderId));

            return Results.Ok(result);
        });
    }
}
