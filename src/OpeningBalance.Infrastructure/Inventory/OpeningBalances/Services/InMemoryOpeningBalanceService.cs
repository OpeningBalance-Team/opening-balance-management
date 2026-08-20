using OpeningBalance.Application.Inventory.OpeningBalances.Interfaces;
using OpeningBalance.Application.Inventory.OpeningBalances.Services;
using OpeningBalance.Domain.Inventory.OpeningBalances.Entities;

namespace OpeningBalance.Infrastructure.Inventory.OpeningBalances.Services;

public sealed class InMemoryOpeningBalanceService : IOpeningBalanceService
{
    private readonly BalanceValidationService _validation = new();
    private readonly List<Product> _products =
    [
        new(1, "HP Laptop"),
        new(2, "Logitech Mouse"),
        new(3, "Keyboard"),
        new(4, "دواء X"),
        new(5, "شاشة مكتبية")
    ];
    private readonly List<Warehouse> _warehouses =
    [
        new(1, "المخزن الرئيسي"),
        new(2, "مخزن الأدوية"),
        new(3, "مخزن الأجهزة")
    ];
    private OpeningBalanceDocument? _lastSaved;

    public Task<IReadOnlyList<Product>> GetProductsAsync(CancellationToken cancellationToken = default) =>
        Task.FromResult<IReadOnlyList<Product>>(_products);

    public Task<IReadOnlyList<Warehouse>> GetWarehousesAsync(CancellationToken cancellationToken = default) =>
        Task.FromResult<IReadOnlyList<Warehouse>>(_warehouses);

    public Task<OperationResult> SaveAsync(OpeningBalanceDocument document, CancellationToken cancellationToken = default)
    {
        var errors = _validation.ValidateDocument(document);
        if (errors.Count > 0)
            return Task.FromResult(OperationResult.Fail("تعذر حفظ الأرصدة الافتتاحية", errors.ToArray()));

        _lastSaved = new OpeningBalanceDocument
        {
            DocumentNumber = document.DocumentNumber,
            DocumentDate = document.DocumentDate,
            UserName = document.UserName,
            Description = document.Description,
            Details = document.Details.Select(Clone).ToList()
        };
        return Task.FromResult(OperationResult.Ok("تم حفظ الأرصدة الافتتاحية بنجاح"));
    }

    private static OpeningBalanceDetail Clone(OpeningBalanceDetail detail) => new()
    {
        ProductId = detail.ProductId,
        ProductName = detail.ProductName,
        WarehouseId = detail.WarehouseId,
        WarehouseName = detail.WarehouseName,
        Quantity = detail.Quantity,
        Price = detail.Price,
        ExpiryDate = detail.ExpiryDate
    };
}
