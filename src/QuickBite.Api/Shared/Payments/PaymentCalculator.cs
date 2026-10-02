namespace QuickBite.Api.Shared.Payments;

public class PaymentCalculator
{
    public decimal CalculateTotal(
        decimal subtotal,
        decimal discount,
        decimal tax,
        decimal deliveryFee)
    {
        return subtotal - discount + tax + deliveryFee;
    }
}
