namespace QuickBite.Api.Infrastructure.Payments;

public class PaymentGateway
{
    public Task Charge(decimal amount)
    {
        Console.WriteLine($"Payment processed: {amount}");

        return Task.CompletedTask;
    }

    public Task Refund(decimal amount)
    {
        Console.WriteLine($"Payment refunded: {amount}");

        return Task.CompletedTask;
    }
}
