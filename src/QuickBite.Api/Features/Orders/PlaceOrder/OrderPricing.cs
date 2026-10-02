namespace QuickBite.Api.Features.Orders.PlaceOrder;

public class OrderPricing
{
    public OrderPrice Calculate(decimal subtotal, decimal discount, decimal deliveryFee)
    {
        var taxableAmount = subtotal - discount;

        var tax = taxableAmount * 0.14m;

        var total = taxableAmount + tax + deliveryFee;

        return new OrderPrice(subtotal, discount, tax, deliveryFee, total);
    }
}

public record OrderPrice(
    decimal Subtotal,
    decimal Discount,
    decimal Tax,
    decimal DeliveryFee,
    decimal Total);
