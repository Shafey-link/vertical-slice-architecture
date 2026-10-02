namespace QuickBite.Api.Features.Orders.PlaceOrder;

public class PaymentProcessor
{
    public Task Process(decimal amount)
    {
        Console.WriteLine($"Payment processed: {amount}");

        return Task.CompletedTask;
    }
}
