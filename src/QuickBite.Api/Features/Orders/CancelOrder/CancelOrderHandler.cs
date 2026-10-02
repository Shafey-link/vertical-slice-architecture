using Microsoft.EntityFrameworkCore;
using QuickBite.Api.Infrastructure.Persistence;

namespace QuickBite.Api.Features.Orders.CancelOrder;

public class CancelOrderHandler
{
    private readonly QuickBiteDbContext _db;

    public CancelOrderHandler(QuickBiteDbContext db)
    {
        _db = db;
    }

    public async Task<CancelOrderResponse> Handle(CancelOrderRequest request)
    {
        var order = await _db.Orders
            .SingleOrDefaultAsync(x => x.Id == request.OrderId);

        if (order is null)
            throw new InvalidOperationException("Order not found.");

        order.Cancel();

        await _db.SaveChangesAsync();

        return new CancelOrderResponse(order.Id, order.Status.ToString());
    }
}
