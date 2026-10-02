namespace QuickBite.Api.Features.Payments.RefundPayment;

public static class RefundPaymentEndpoint
{
    public static void MapRefundPayment(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("/api/payments/{orderId:guid}/refund", async (
            Guid orderId,
            RefundPaymentHandler handler) =>
        {
            var result = await handler.Handle(orderId);

            return Results.Ok(result);
        });
    }
}
