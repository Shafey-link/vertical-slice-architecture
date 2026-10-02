namespace QuickBite.Api.Features.Orders.PlaceOrder;

public static class PlaceOrderEndpoint
{
    public static void MapPlaceOrder(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("/api/orders", async (
            PlaceOrderRequest request,
            PlaceOrderHandler handler) =>
        {
            var result = await handler.Handle(request);

            return Results.Ok(result);
        });
    }
}
