namespace QuickBite.Api.Domain;

public class Order
{
    public Guid Id { get; private set; }

    public Guid CustomerId { get; private set; }

    public Guid RestaurantId { get; private set; }

    public decimal TotalAmount { get; private set; }

    public OrderStatus Status { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public List<OrderItem> Items { get; private set; } = new();

    private Order()
    {
    }

    public Order(Guid customerId, Guid restaurantId, decimal totalAmount)
    {
        Id = Guid.NewGuid();
        CustomerId = customerId;
        RestaurantId = restaurantId;
        TotalAmount = totalAmount;
        Status = OrderStatus.Pending;
        CreatedAt = DateTime.UtcNow;
    }

    public void AddItem(Guid menuItemId, int quantity, decimal unitPrice)
    {
        Items.Add(new OrderItem(Id, menuItemId, quantity, unitPrice));
    }

    public void Confirm()
    {
        if (Status != OrderStatus.Pending)
            throw new InvalidOperationException("Only pending orders can be confirmed.");

        Status = OrderStatus.Confirmed;
    }

    public void StartPreparing()
    {
        if (Status != OrderStatus.Confirmed)
            throw new InvalidOperationException("Only confirmed orders can be prepared.");

        Status = OrderStatus.Preparing;
    }

    public void MarkDelivered()
    {
        if (Status != OrderStatus.Preparing)
            throw new InvalidOperationException("Only preparing orders can be delivered.");

        Status = OrderStatus.Delivered;
    }

    public void Cancel()
    {
        if (Status == OrderStatus.Delivered)
            throw new InvalidOperationException("Delivered orders cannot be cancelled.");

        if (Status == OrderStatus.Cancelled)
            throw new InvalidOperationException("Order is already cancelled.");

        Status = OrderStatus.Cancelled;
    }
}
