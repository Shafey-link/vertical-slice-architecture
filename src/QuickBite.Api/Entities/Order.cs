namespace QuickBite.Api.Entities;

public class Order
{
    public Guid Id { get; set; }

    public Guid CustomerId { get; set; }

    public Guid RestaurantId { get; set; }

    public decimal TotalAmount { get; set; }

    public DateTime CreatedAt { get; set; }

    public List<OrderItem> Items { get; set; } = new();
}
