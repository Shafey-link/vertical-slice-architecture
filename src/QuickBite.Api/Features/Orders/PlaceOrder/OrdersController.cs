using Microsoft.AspNetCore.Mvc;

namespace QuickBite.Api.Features.Orders.PlaceOrder;

[ApiController]
[Route("api/orders")]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _service;

    public OrdersController(IOrderService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> PlaceOrder(PlaceOrderRequest request)
    {
        var result = await _service.PlaceOrder(request);

        return Ok(result);
    }
}
