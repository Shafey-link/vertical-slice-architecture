namespace QuickBite.Api.Features.Payments.RefundPayment;

public record RefundPaymentResponse(Guid OrderId, decimal RefundedAmount);
