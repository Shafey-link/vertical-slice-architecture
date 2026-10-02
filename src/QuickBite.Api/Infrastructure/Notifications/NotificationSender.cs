namespace QuickBite.Api.Infrastructure.Notifications;

public class NotificationSender
{
    public Task Send(Guid orderId)
    {
        Console.WriteLine($"Order notification sent: {orderId}");

        return Task.CompletedTask;
    }
}
