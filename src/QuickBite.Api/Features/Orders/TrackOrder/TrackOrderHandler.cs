using Microsoft.EntityFrameworkCore;
using QuickBite.Api.Infrastructure.Persistence;

namespace QuickBite.Api.Features.Orders.TrackOrder;

public class TrackOrderHandler
{
    private readonly QuickBiteDbContext _db;

    public TrackOrderHandler(QuickBiteDbContext db)
    {
        _db = db;
    }

    public async Task<TrackOrderResponse> Handle(Guid orderId)
    {
        var order = await _db.Orders
            .SingleOrDefaultAsync(x => x.Id == orderId);

        if (order is null)
            throw new InvalidOperationException("Order not found.");

        return new TrackOrderResponse(
            order.Id,
            order.Status.ToString(),
            order.TotalAmount);
    }
}
