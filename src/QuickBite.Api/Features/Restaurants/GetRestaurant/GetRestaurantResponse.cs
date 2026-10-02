namespace QuickBite.Api.Features.Restaurants.GetRestaurant;

public record GetRestaurantResponse(
    Guid Id,
    string Name,
    bool IsOpen,
    List<MenuItemResponse> Menu);

public record MenuItemResponse(
    Guid Id,
    string Name,
    decimal Price,
    bool IsAvailable);
