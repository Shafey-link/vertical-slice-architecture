using Microsoft.EntityFrameworkCore;
using QuickBite.Api.Domain;
using QuickBite.Api.Infrastructure.Payments;
using QuickBite.Api.Infrastructure.Persistence;
using QuickBite.Api.Shared.Payments;

namespace QuickBite.Api.Features.Payments.RefundPayment;

public class RefundPaymentHandler
{
    private readonly QuickBiteDbContext _db;
    private readonly PaymentCalculator _calculator;
    private readonly PaymentGateway _gateway;

    public RefundPaymentHandler(
        QuickBiteDbContext db,
        PaymentCalculator calculator,
        PaymentGateway gateway)
    {
        _db = db;
        _calculator = calculator;
        _gateway = gateway;
    }

    public async Task<RefundPaymentResponse> Handle(Guid orderId)
    {
        var order = await _db.Orders
            .SingleOrDefaultAsync(x => x.Id == orderId);

        if (order is null)
            throw new InvalidOperationException("Order not found.");

        if (order.Status != OrderStatus.Cancelled)
            throw new InvalidOperationException("Only cancelled orders can be refunded.");

        var refundedAmount = _calculator.CalculateTotal(
            subtotal: order.TotalAmount,
            discount: 0,
            tax: 0,
            deliveryFee: 0);

        await _gateway.Refund(refundedAmount);

        return new RefundPaymentResponse(order.Id, refundedAmount);
    }
}
