using Microsoft.EntityFrameworkCore;
using QuickBite.Api.Infrastructure.Persistence;

namespace QuickBite.Api.Features.Restaurants.GetRestaurant;

public class GetRestaurantHandler
{
    private readonly QuickBiteDbContext _db;

    public GetRestaurantHandler(QuickBiteDbContext db)
    {
        _db = db;
    }

    public async Task<GetRestaurantResponse> Handle(Guid restaurantId)
    {
        var restaurant = await _db.Restaurants
            .SingleOrDefaultAsync(x => x.Id == restaurantId);

        if (restaurant is null)
            throw new InvalidOperationException("Restaurant not found.");

        var menu = await _db.MenuItems
            .Where(item => item.RestaurantId == restaurantId)
            .Select(item => new MenuItemResponse(
                item.Id,
                item.Name,
                item.Price,
                item.IsAvailable))
            .ToListAsync();

        return new GetRestaurantResponse(
            restaurant.Id,
            restaurant.Name,
            restaurant.IsOpen,
            menu);
    }
}
