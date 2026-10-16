namespace Domain.Entities;

/// <summary>

/// </summary>
public sealed class Order
{
    public Order(
        Guid id,
        string customerName,
        string productName,
        int quantity,
        decimal unitPrice,
        DateTime createdAtUtc)
    {
        if (id == Guid.Empty)
        {
            throw new ArgumentException("El identificador del pedido no es válido.", nameof(id));
        }

        ArgumentException.ThrowIfNullOrWhiteSpace(customerName);
        ArgumentException.ThrowIfNullOrWhiteSpace(productName);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(quantity);
        ArgumentOutOfRangeException.ThrowIfNegative(unitPrice);

        Id = id;
        CustomerName = customerName;
        ProductName = productName;
        Quantity = quantity;
        UnitPrice = unitPrice;
        CreatedAtUtc = createdAtUtc;
    }

    public Guid Id { get; }

    public string CustomerName { get; }

    public string ProductName { get; }

    public int Quantity { get; }

    public decimal UnitPrice { get; }

    public DateTime CreatedAtUtc { get; }

    /// <summary>Regla de negocio: el total se calcula dentro de la entidad.</summary>
    public decimal Total => Quantity * UnitPrice;

    public static Order Create(string customerName, string productName, int quantity, decimal unitPrice)
        => new(Guid.NewGuid(), customerName, productName, quantity, unitPrice, DateTime.UtcNow);
}
