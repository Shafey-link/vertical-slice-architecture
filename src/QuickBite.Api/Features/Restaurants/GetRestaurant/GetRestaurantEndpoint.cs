namespace QuickBite.Api.Features.Restaurants.GetRestaurant;

public static class GetRestaurantEndpoint
{
    public static void MapGetRestaurant(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/api/restaurants/{restaurantId:guid}", async (
            Guid restaurantId,
            GetRestaurantHandler handler) =>
        {
            var result = await handler.Handle(restaurantId);

            return Results.Ok(result);
        });
    }
}
