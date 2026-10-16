using Domain.Entities;

namespace Application.Abstractions;

/// <summary>Puerto de salida pequeño y específico: solo guardar pedidos.</summary>
public interface IOrderWriter
{
    Task AddAsync(Order order, CancellationToken cancellationToken = default);
}
