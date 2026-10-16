using Domain.Entities;

namespace Application.Abstractions;

/// <summary>Puerto de salida pequeño y específico: solo consultar pedidos.</summary>
public interface IOrderReader
{
    Task<IReadOnlyList<Order>> GetLastAsync(int count, CancellationToken cancellationToken = default);
}
