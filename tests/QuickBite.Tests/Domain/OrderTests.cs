using QuickBite.Api.Domain;
using Shouldly;

namespace QuickBite.Tests.Domain;

public class OrderTests
{
    [Fact]
    public void Should_Cancel_Pending_Order()
    {
        var order = new Order(
            Guid.NewGuid(),
            Guid.NewGuid(),
            100);

        order.Cancel();

        order.Status.ShouldBe(OrderStatus.Cancelled);
    }

    [Fact]
    public void Should_Not_Cancel_Delivered_Order()
    {
        var order = new Order(
            Guid.NewGuid(),
            Guid.NewGuid(),
            100);

        order.Confirm();
        order.StartPreparing();
        order.MarkDelivered();

        Should.Throw<InvalidOperationException>(() => order.Cancel());
    }
}
