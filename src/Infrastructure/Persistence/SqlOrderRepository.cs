using System.Data;
using Application.Abstractions;
using Domain.Entities;
using Microsoft.Data.SqlClient;

namespace Infrastructure.Persistence;

/// <summary>

/// </summary>
public sealed class SqlOrderRepository : IOrderWriter, IOrderReader
{
    private const string InsertSql =
        "INSERT INTO Orders (Id, Customer, Product, Qty, Price, CreatedAtUtc) " +
        "VALUES (@Id, @Customer, @Product, @Qty, @Price, @CreatedAtUtc)";

    private const string SelectLastSql =
        "SELECT TOP (@Count) Id, Customer, Product, Qty, Price, CreatedAtUtc " +
        "FROM Orders ORDER BY CreatedAtUtc DESC";

    private readonly string _connectionString;

    public SqlOrderRepository(string connectionString)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(connectionString);
        _connectionString = connectionString;
    }

    public async Task AddAsync(Order order, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(order);

        await using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);

        await using var command = new SqlCommand(InsertSql, connection);
        command.Parameters.Add("@Id", SqlDbType.UniqueIdentifier).Value = order.Id;
        command.Parameters.Add("@Customer", SqlDbType.NVarChar, 200).Value = order.CustomerName;
        command.Parameters.Add("@Product", SqlDbType.NVarChar, 200).Value = order.ProductName;
        command.Parameters.Add("@Qty", SqlDbType.Int).Value = order.Quantity;
        command.Parameters.Add(new SqlParameter("@Price", SqlDbType.Decimal) { Precision = 18, Scale = 2, Value = order.UnitPrice });
        command.Parameters.Add("@CreatedAtUtc", SqlDbType.DateTime2).Value = order.CreatedAtUtc;

        await command.ExecuteNonQueryAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<Order>> GetLastAsync(int count, CancellationToken cancellationToken = default)
    {
        await using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);

        await using var command = new SqlCommand(SelectLastSql, connection);
        command.Parameters.Add("@Count", SqlDbType.Int).Value = count;

        var orders = new List<Order>();

        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            orders.Add(new Order(
                reader.GetGuid(0),
                reader.GetString(1),
                reader.GetString(2),
                reader.GetInt32(3),
                reader.GetDecimal(4),
                reader.GetDateTime(5)));
        }

        return orders;
    }
}
