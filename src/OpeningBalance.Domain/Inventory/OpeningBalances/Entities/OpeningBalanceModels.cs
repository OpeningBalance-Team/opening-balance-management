namespace OpeningBalance.Domain.Inventory.OpeningBalances.Entities;

public sealed class OpeningBalanceDocument
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public string DocumentNumber { get; set; } = string.Empty;
    public DateOnly DocumentDate { get; set; } = DateOnly.FromDateTime(DateTime.Today);
    public string UserName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public List<OpeningBalanceDetail> Details { get; set; } = [];
}

public sealed class OpeningBalanceDetail
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public int ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public int WarehouseId { get; set; }
    public string WarehouseName { get; set; } = string.Empty;
    public decimal Quantity { get; set; }
    public decimal? Price { get; set; }
    public DateOnly? ExpiryDate { get; set; }
}

public sealed record Product(int Id, string Name);
public sealed record Warehouse(int Id, string Name);

public sealed record OpeningBalanceSession(
    OpeningBalanceDocument Header,
    IReadOnlyList<OpeningBalanceDetail> Details);

public sealed record ValidationError(string Field, string Message);
