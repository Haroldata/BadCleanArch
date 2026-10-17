using System.ComponentModel.DataAnnotations;

namespace WebApi.Contracts;

public sealed record CreateOrderRequest(
    [Required, StringLength(200)] string CustomerName,
    [Required, StringLength(200)] string ProductName,
    [Required, Range(1, 10_000)] int? Quantity,
    [Required, Range(typeof(decimal), "0", "1000000")] decimal? UnitPrice);