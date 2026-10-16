using Domain.Entities;

namespace WebApi.Contracts;

public sealed record OrderResponse(
    Guid Id,
    string CustomerName,
    string ProductName,
    int Quantity,
    decimal UnitPrice,
    decimal Total,
    DateTime CreatedAtUtc)
{
    public static OrderResponse From(Order order) => new(
        order.Id,
        order.CustomerName,
        order.ProductName,
        order.Quantity,
        order.UnitPrice,
        order.Total,
        order.CreatedAtUtc);
}
