using Application.Abstractions;
using Domain.Entities;

namespace Application.Orders;

public sealed class CreateOrderUseCase
{
    private readonly IOrderWriter _orders;
    private readonly IAppLogger _logger;

    public CreateOrderUseCase(IOrderWriter orders, IAppLogger logger)
    {
        _orders = orders;
        _logger = logger;
    }

    public async Task<Order> ExecuteAsync(CreateOrderCommand command, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(command);

        var order = Order.Create(command.CustomerName, command.ProductName, command.Quantity, command.UnitPrice);

        await _orders.AddAsync(order, cancellationToken);

        _logger.Info($"Pedido {order.Id} creado. Total: {order.Total}");

        return order;
    }
}
