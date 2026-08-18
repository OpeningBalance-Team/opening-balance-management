# 06 — Sequence Diagram: Add Opening Balance Detail

## مخطط التسلسل — إضافة صنف إلى بيانات الرصيد الافتتاحي

يوضح هذا المخطط التسلسل التشغيلي لعملية إضافة صنف إلى تفاصيل الرصيد الافتتاحي، بدءًا من تفاعل المستخدم مع الشاشة، مرورًا بالتحقق من البيانات، وانتهاءً بإضافة السطر وتحديث جدول التفاصيل.

```mermaid
sequenceDiagram
    autonumber
    participant User as "المستخدم (موظف المخزون/المدير)"
    participant Page as "OpeningBalancePage + DetailsGrid"
    participant Valid as BalanceValidationService
    participant Service as OpeningBalanceService
    participant Repo as "InMemoryRepository (Mock Data)"

    User->>Page: «ينقر إضافة صنف» (خطوة 1 من UC-003)
    Page->>Repo: LoadItems() + LoadWarehouses()
    Note over Repo: "الأسماء فقط — BR-06 — قوائم الأصناف والمخازن"
    Repo-->>Page: قوائم الأصناف والمخازن
    Page-->>User: عرض ItemDropdown + WarehouseDropdown
    User->>Page: "اختيار الصنف (UC-004) + اختيار المخزن (UC-005)"
    Page->>Repo: "Resolve ItemId / WarehouseId بالأسماء"
    User->>Page: "إدخال الكمية والسعر وتاريخ الصلاحية (اختياريان)"
    Page->>Valid: ValidateDetail(detail)
    Note right of Valid: BR-01: صنف + مخزن + كمية مطلوبة + BR-02  
الكمية عدد صحيح > 0 + BR-03/D10  
التوليقة غير مكررة + D7  
"السعر <= 0، الصلاحية مستقبلية"

    alt "الصنف غير صالح (UC-003 A1/A3)"
        Valid-->>Page: ValidationResult خطأ
        Page-->>User: «...رسالة موحدة «الكمية غير صالحة / يجب اختيار صنف»»
    else الصنف صالح
        Valid-->>Page: ValidationResult نجاح
        Page->>Service: AddDetail(detail)
        Service->>Repo: "AddBalanceDetail(documentId, detail)"
        Repo-->>Service: "تم التسجيل في الذاكرة (FR-09)"
        Service-->>Page: Detail مضاف
        Page-->>User: "تحديث DetailsGrid بالصف الجديد (UC-003 Postcondition)"
    end
```

---

## المشاركون

| المشارك | المسؤولية |
|---|---|
| المستخدم | بدء عملية إضافة الصنف وإدخال/اختيار بيانات التفاصيل |
| `OpeningBalancePage + DetailsGrid` | عرض نموذج الإضافة، استقبال المدخلات، وعرض النتيجة |
| `BalanceValidationService` | التحقق من صحة بيانات التفاصيل |
| `OpeningBalanceService` | تنفيذ عملية إضافة التفاصيل بعد نجاح التحقق |
| `InMemoryRepository` | توفير بيانات Mock وتخزين البيانات في الذاكرة |

---

## التسلسل الرئيسي

1. يضغط المستخدم على **إضافة صنف**.
2. تقوم الصفحة بتحميل بيانات الأصناف والمخازن.
3. تعرض الصفحة قوائم اختيار الصنف والمخزن.
4. يختار المستخدم الصنف والمخزن.
5. يدخل الكمية والسعر وتاريخ الصلاحية عند الحاجة.
6. ترسل الصفحة بيانات السطر إلى `BalanceValidationService`.
7. عند فشل التحقق، يتم إرجاع نتيجة خطأ وعرض رسالة للمستخدم.
8. عند نجاح التحقق، يتم استدعاء `AddDetail`.
9. يتم تسجيل السطر داخل الذاكرة.
10. تعيد الخدمة النتيجة إلى الصفحة.
11. يتم تحديث `DetailsGrid` بالصف الجديد.

---

## Alternate Flow

### الصنف غير صالح

في حالة وجود خطأ في بيانات السطر:

```text
ValidateDetail
      ↓
ValidationResult: Error
      ↓
OpeningBalancePage
      ↓
رسالة للمستخدم
      ↓
لا تتم إضافة السطر
```

---

## Postcondition

بعد نجاح العملية:

- يتم إضافة السطر إلى تفاصيل الرصيد.
- يظهر السطر في `DetailsGrid`.
- يصبح السطر مرتبطًا بوثيقة الرصيد الحالية.
- يمكن للمستخدم متابعة إضافة أصناف أخرى.

---

## Traceability

| المرجع | التمثيل في المخطط |
|---|---|
| UC-003 | بدء الإضافة والتحقق والإضافة وتحديث الجدول |
| UC-004 | اختيار الصنف |
| UC-005 | اختيار المخزن |
| FR-03 | إضافة Detail |
| BR-01 | Product + Warehouse + Quantity |
| BR-02 | Quantity > 0 |
| BR-03 | التحقق من التكرار حسب القاعدة |
| BR-06 | عرض الأسماء والاحتفاظ بالمعرفات داخليًا |

---

## موقع الملف

```text
docs/
└── design/
    └── 06-sequence-add-detail.md
```
