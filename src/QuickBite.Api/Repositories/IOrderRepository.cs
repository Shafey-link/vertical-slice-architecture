using QuickBite.Api.Entities;

namespace QuickBite.Api.Repositories;

public interface IOrderRepository
{
    Task AddAsync(Order order);
}
