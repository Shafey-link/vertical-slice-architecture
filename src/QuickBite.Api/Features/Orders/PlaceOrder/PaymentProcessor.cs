using QuickBite.Api.Infrastructure.Payments;

namespace QuickBite.Api.Features.Orders.PlaceOrder;

public class PaymentProcessor
{
    private readonly PaymentGateway _gateway;

    public PaymentProcessor(PaymentGateway gateway)
    {
        _gateway = gateway;
    }

    public Task Process(decimal amount)
    {
        return _gateway.Charge(amount);
    }
}
