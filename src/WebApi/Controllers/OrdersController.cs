using System.ComponentModel.DataAnnotations;
using Application.Orders;
using Microsoft.AspNetCore.Mvc;
using WebApi.Contracts;

namespace WebApi.Controllers;

[ApiController]
[Route("orders")]
public sealed class OrdersController : ControllerBase
{
    private readonly CreateOrderUseCase _createOrder;
    private readonly GetLastOrdersUseCase _getLastOrders;

    public OrdersController(CreateOrderUseCase createOrder, GetLastOrdersUseCase getLastOrders)
    {
        _createOrder = createOrder;
        _getLastOrders = getLastOrders;
    }

    [HttpPost]
    public async Task<ActionResult<OrderResponse>> Create(
        CreateOrderRequest request,
        CancellationToken cancellationToken)
    {
        var command = new CreateOrderCommand(
            request.CustomerName,
            request.ProductName,
            request.Quantity,
            request.UnitPrice);

        var order = await _createOrder.ExecuteAsync(command, cancellationToken);

        return Ok(OrderResponse.From(order));
    }

    [HttpGet("last")]
    public async Task<ActionResult<IEnumerable<OrderResponse>>> GetLast(
        [FromQuery, Range(1, 100)] int count = 10,
        CancellationToken cancellationToken = default)
    {
        var orders = await _getLastOrders.ExecuteAsync(count, cancellationToken);

        return Ok(orders.Select(OrderResponse.From));
    }
}
