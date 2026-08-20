using OpeningBalance.Application.Inventory.OpeningBalances.Interfaces;
using OpeningBalance.Domain.Inventory.OpeningBalances.Entities;

namespace OpeningBalance.Application.Inventory.OpeningBalances.Services;

public sealed class BalanceValidationService
{
    public IReadOnlyList<ValidationError> ValidateDetail(OpeningBalanceDetail detail)
    {
        var errors = new List<ValidationError>();
        if (detail.ProductId <= 0) errors.Add(new("ProductId", "يرجى اختيار الصنف"));
        if (detail.WarehouseId <= 0) errors.Add(new("WarehouseId", "يرجى اختيار المخزن"));
        if (detail.Quantity <= 0) errors.Add(new("Quantity", "يجب أن تكون الكمية أكبر من صفر"));
        return errors;
    }

    public IReadOnlyList<ValidationError> ValidateDocument(OpeningBalanceDocument document)
    {
        var errors = new List<ValidationError>();
        if (string.IsNullOrWhiteSpace(document.DocumentNumber)) errors.Add(new("DocumentNumber", "يرجى إدخال رقم الوثيقة"));
        if (document.DocumentDate == default) errors.Add(new("DocumentDate", "يرجى اختيار التاريخ"));
        if (string.IsNullOrWhiteSpace(document.UserName)) errors.Add(new("UserName", "يرجى إدخال المستخدم"));
        if (document.Details.Count == 0) errors.Add(new("Details", "يجب إضافة صنف واحد على الأقل قبل الحفظ"));
        foreach (var detail in document.Details) errors.AddRange(ValidateDetail(detail));
        return errors;
    }
}
