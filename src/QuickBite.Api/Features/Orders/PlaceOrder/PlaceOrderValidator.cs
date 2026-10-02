namespace QuickBite.Api.Features.Orders.PlaceOrder;

public class PlaceOrderValidator
{
    public void Validate(PlaceOrderRequest request)
    {
        if (request.CustomerId == Guid.Empty)
        {
            throw new ArgumentException("Customer is required.");
        }

        if (request.RestaurantId == Guid.Empty)
        {
            throw new ArgumentException("Restaurant is required.");
        }

        if (request.Items.Count == 0)
        {
            throw new ArgumentException("Order must contain at least one item.");
        }

        if (request.Items.Any(x => x.Quantity <= 0))
        {
            throw new ArgumentException("Quantity must be greater than zero.");
        }
    }
}
