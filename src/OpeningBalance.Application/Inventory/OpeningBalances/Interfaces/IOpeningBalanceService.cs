using OpeningBalance.Domain.Inventory.OpeningBalances.Entities;

namespace OpeningBalance.Application.Inventory.OpeningBalances.Interfaces;

public sealed record OperationResult(bool Succeeded, string? Message = null, IReadOnlyList<ValidationError>? Errors = null)
{
    public static OperationResult Ok(string? message = null) => new(true, message);
    public static OperationResult Fail(string message, params ValidationError[] errors) => new(false, message, errors);
}

public interface IOpeningBalanceService
{
    Task<IReadOnlyList<Product>> GetProductsAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Warehouse>> GetWarehousesAsync(CancellationToken cancellationToken = default);
    Task<OperationResult> SaveAsync(OpeningBalanceDocument document, CancellationToken cancellationToken = default);
}
