using QuickBite.Api.Data;
using QuickBite.Api.Entities;

namespace QuickBite.Api.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly QuickBiteDbContext _db;

    public OrderRepository(QuickBiteDbContext db)
    {
        _db = db;
    }

    public async Task AddAsync(Order order)
    {
        _db.Orders.Add(order);

        await _db.SaveChangesAsync();
    }
}
