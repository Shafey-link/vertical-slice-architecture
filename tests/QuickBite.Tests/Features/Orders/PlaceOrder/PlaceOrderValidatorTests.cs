using QuickBite.Api.Features.Orders.PlaceOrder;
using Shouldly;

namespace QuickBite.Tests.Features.Orders.PlaceOrder;

public class PlaceOrderValidatorTests
{
    private readonly PlaceOrderValidator _validator = new();

    [Fact]
    public void Should_Reject_Empty_Customer()
    {
        var request = new PlaceOrderRequest(
            Guid.Empty,
            Guid.NewGuid(),
            new List<PlaceOrderItemRequest>
            {
                new(Guid.NewGuid(), 1, 100)
            });

        Should.Throw<ArgumentException>(() => _validator.Validate(request));
    }

    [Fact]
    public void Should_Reject_Empty_Items()
    {
        var request = new PlaceOrderRequest(
            Guid.NewGuid(),
            Guid.NewGuid(),
            new List<PlaceOrderItemRequest>());

        Should.Throw<ArgumentException>(() => _validator.Validate(request));
    }

    [Fact]
    public void Should_Reject_Invalid_Quantity()
    {
        var request = new PlaceOrderRequest(
            Guid.NewGuid(),
            Guid.NewGuid(),
            new List<PlaceOrderItemRequest>
            {
                new(Guid.NewGuid(), 0, 100)
            });

        Should.Throw<ArgumentException>(() => _validator.Validate(request));
    }
}
