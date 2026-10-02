namespace QuickBite.Api.Features.Orders.PlaceOrder;

public class OrderNotification
{
    public Task Send(Guid orderId)
    {
        Console.WriteLine($"Order notification sent: {orderId}");

        return Task.CompletedTask;
    }
}
