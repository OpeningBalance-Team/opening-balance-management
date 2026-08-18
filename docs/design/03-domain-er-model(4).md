# 03 — Domain / ER Model

## نموذج المجال والعلاقات

يمثل هذا المخطط البنية الأساسية لبيانات الأرصدة الافتتاحية والعلاقات بين وثيقة الرصيد والرأس والتفاصيل والأصناف والمخازن.

```mermaid
flowchart TB

    DOC["<b>OpeningBalanceDocument</b><br/>────────────────────────<br/>string &nbsp;&nbsp; DocumentId &nbsp;&nbsp; PK<br/>مفتاح داخلي للجلسة"]

    HEADER["<b>DocumentHeader</b><br/>────────────────────────────────────────<br/>string &nbsp;&nbsp; DocumentNumber &nbsp;&nbsp; &nbsp;&nbsp; رقم الوثيقة — FR-01/FR-02، قسم 6.7<br/>date &nbsp;&nbsp;&nbsp;&nbsp; DocumentDate &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; التاريخ — FR-01/FR-02<br/>string &nbsp;&nbsp; UserName &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; اسم المستخدم — FR-02<br/>string &nbsp;&nbsp; Description &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; البيان/الملاحظات — nullable (قسم 6.7)"]

    DETAIL["<b>BalanceDetail</b><br/>────────────────────────────────────────────────<br/>int &nbsp;&nbsp;&nbsp;&nbsp; Quantity &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; ملاحظة تحقق: أكبر من صفر (BR-02)<br/>decimal &nbsp; Price &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; اختياري — nullable (قسم 6.7)<br/>date &nbsp;&nbsp;&nbsp;&nbsp; ExpiryDate &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; اختياري — nullable (قسم 6.7)<br/>string &nbsp;&nbsp; ProductId &nbsp;&nbsp; FK &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; معرف الصنف — لا يظهر للمستخدم (BR-06)<br/>string &nbsp;&nbsp; WarehouseId &nbsp; FK &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; معرف المخزن — مطلوب (BR-04)"]

    PRODUCT["<b>Product</b><br/>────────────────────────────────<br/>string &nbsp;&nbsp; ProductId &nbsp;&nbsp; PK &nbsp;&nbsp;&nbsp;&nbsp; معرف داخلي — Mock Data (UC-004)<br/>string &nbsp;&nbsp; ProductName &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; يظهر للمستخدم بدل المعرف (BR-06)"]

    WAREHOUSE["<b>Warehouse</b><br/>────────────────────────────────<br/>string &nbsp;&nbsp; WarehouseId &nbsp;&nbsp; PK &nbsp;&nbsp; معرف داخلي — Mock Data (UC-005)<br/>string &nbsp;&nbsp; WarehouseName &nbsp;&nbsp;&nbsp;&nbsp; يظهر للمستخدم بدل المعرف (BR-06)"]

    DOC -->|"كل وثيقة لها رأس واحد (قسم 6.7)"| HEADER
    DOC -->|"تفاصيل الرصيد عند الحفظ — صف واحد على الأقل (UC-009 A2)"| DETAIL
    DETAIL -->|"يرتبط بصنف واحد"| PRODUCT
    DETAIL -->|"يرتبط بمخزن واحد"| WAREHOUSE

    classDef entity fill:#f4f0ff,stroke:#7c5cff,stroke-width:2px,color:#222;
    class DOC,HEADER,DETAIL,PRODUCT,WAREHOUSE entity;

    %% Visual spacing
    DOC --- HEADER
    DOC --- DETAIL
    DETAIL --- PRODUCT
    DETAIL --- WAREHOUSE

    linkStyle 4,5,6,7 stroke:transparent,fill:none;
```

---

## العلاقات

- **OpeningBalanceDocument → DocumentHeader:** كل وثيقة لها رأس واحد.
- **OpeningBalanceDocument → BalanceDetail:** عند الحفظ تحتوي الوثيقة على صف واحد على الأقل.
- **BalanceDetail → Product:** كل Detail يرتبط بصنف واحد.
- **BalanceDetail → Warehouse:** كل Detail يرتبط بمخزن واحد.

---

## Business Rules المرتبطة بالنموذج

| القاعدة | التطبيق |
|---|---|
| BR-02 | `Quantity > 0` |
| BR-03 | يسمح بتكرار الصنف وفق اختلاف السعر/تاريخ الصلاحية |
| BR-04 | `Warehouse` مطلوب |
| BR-06 | المعرفات الداخلية لا تظهر للمستخدم |

---

## الحقول الاختيارية

- `Price`
- `ExpiryDate`
- `Description`

ولا يتم فرض قيود إضافية غير محددة ضمن المتطلبات.

---

## قيود التصميم

لا يتضمن النموذج:

- Unique على `DocumentNumber`.
- Composite Unique Key على تفاصيل الرصيد.
- شرط أن يكون `ExpiryDate` في المستقبل.
- شرط `Price >= 0`.
- `DocumentStatus` كحقل مستقل.

---

## Traceability

| المتطلب / حالة الاستخدام | التمثيل |
|---|---|
| FR-02 / UC-002 | `DocumentHeader` |
| FR-03 / UC-003 | `BalanceDetail` |
| FR-04 / UC-004 | `Product` |
| FR-05 / UC-005 | `Warehouse` |
| FR-06 / UC-006 | `BalanceDetail` |
| FR-07 / UC-007 | تحديث `BalanceDetail` |
| FR-08 / UC-008 | حذف `BalanceDetail` |
| FR-09 / UC-009 | `OpeningBalanceDocument` + `BalanceDetail` |

---

## موقع الملف

```text
docs/
└── design/
    └── 03-domain-er-model.md
```
