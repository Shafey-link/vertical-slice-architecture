using QuickBite.Api.Infrastructure.Notifications;

namespace QuickBite.Api.Features.Orders.PlaceOrder;

public class OrderNotification
{
    private readonly NotificationSender _sender;

    public OrderNotification(NotificationSender sender)
    {
        _sender = sender;
    }

    public Task Send(Guid orderId)
    {
        return _sender.Send(orderId);
    }
}
