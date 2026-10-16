using Application.Abstractions;
using Domain.Entities;

namespace Application.Orders;

public sealed class GetLastOrdersUseCase
{
    private readonly IOrderReader _orders;

    public GetLastOrdersUseCase(IOrderReader orders)
    {
        _orders = orders;
    }

    public Task<IReadOnlyList<Order>> ExecuteAsync(int count, CancellationToken cancellationToken = default)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(count);

        return _orders.GetLastAsync(count, cancellationToken);
    }
}
