# 03 — Domain / ER Model

## نموذج المجال والعلاقات

يوضح المخطط نموذج بيانات الأرصدة الافتتاحية والعلاقات بين وثيقة الرصيد، رأس الوثيقة، تفاصيل الرصيد، الأصناف، والمخازن.

> يعتمد الرسم التالي على Mermaid داخل Markdown، مع الحفاظ على مستوى التفاصيل الموجود في النسخة المتقدمة من المخطط، بما في ذلك الحقول والمفاتيح والملاحظات المرتبطة بالمتطلبات وقواعد العمل.

```mermaid
flowchart TB

    DOC["<div style='text-align:center'><b>OpeningBalanceDocument</b><hr/><table><tr><td><b>string</b></td><td><b>DocumentId</b></td><td><b>PK</b></td><td>مفتاح داخلي للجلسة</td></tr></table></div>"]

    HEADER["<div style='text-align:center'><b>DocumentHeader</b><hr/><table><tr><td>string</td><td><b>DocumentNumber</b></td><td>رقم الوثيقة — FR-01/FR-02، قسم 6.7</td></tr><tr><td>date</td><td><b>DocumentDate</b></td><td>التاريخ — FR-01/FR-02</td></tr><tr><td>string</td><td><b>UserName</b></td><td>اسم المستخدم — FR-02</td></tr><tr><td>string</td><td><b>Description</b></td><td>البيان/الملاحظات — nullable (قسم 6.7)</td></tr></table></div>"]

    DETAIL["<div style='text-align:center'><b>BalanceDetail</b><hr/><table><tr><td>int</td><td><b>Quantity</b></td><td>ملاحظة تحقق: أكبر من صفر (BR-02)</td></tr><tr><td>decimal</td><td><b>Price</b></td><td>اختياري — nullable (قسم 6.7)</td></tr><tr><td>date</td><td><b>ExpiryDate</b></td><td>اختياري — nullable (قسم 6.7)</td></tr><tr><td>string</td><td><b>ProductId</b></td><td>FK — معرف الصنف — لا يظهر للمستخدم (BR-06)</td></tr><tr><td>string</td><td><b>WarehouseId</b></td><td>FK — معرف المخزن — مطلوب (BR-04)</td></tr></table></div>"]

    PRODUCT["<div style='text-align:center'><b>Product</b><hr/><table><tr><td>string</td><td><b>ProductId</b></td><td>PK — معرف داخلي — Mock Data (UC-004)</td></tr><tr><td>string</td><td><b>ProductName</b></td><td>يظهر للمستخدم بدل المعرف (BR-06)</td></tr></table></div>"]

    WAREHOUSE["<div style='text-align:center'><b>Warehouse</b><hr/><table><tr><td>string</td><td><b>WarehouseId</b></td><td>PK — معرف داخلي — Mock Data (UC-005)</td></tr><tr><td>string</td><td><b>WarehouseName</b></td><td>يظهر للمستخدم بدل المعرف (BR-06)</td></tr></table></div>"]

    DOC ---| "كل وثيقة لها رأس واحد (قسم 6.7)" | HEADER
    DOC ---| "عند الحفظ: صف واحد على الأقل من تفاصيل الرصيد (UC-009 A2)" | DETAIL

    DETAIL ---| "كل Detail يرتبط بصنف واحد — يسمح بالتكرار عند اختلاف السعر/الصلاحية (BR-03)" | PRODUCT
    DETAIL ---| "كل Detail يرتبط بمخزن واحد" | WAREHOUSE

    classDef entity fill:#F4F0FF,stroke:#7C5CFF,stroke-width:2px,color:#222;
    class DOC,HEADER,DETAIL,PRODUCT,WAREHOUSE entity;

    linkStyle 0,1,2,3 stroke:#777,stroke-width:1.5px;
```

## 1. OpeningBalanceDocument

يمثل وثيقة الرصيد الافتتاحي، ويحتوي على:

- `DocumentId`: مفتاح داخلي للوثيقة.

## 2. DocumentHeader

يمثل رأس الوثيقة ويحتوي على:

- `DocumentNumber`
- `DocumentDate`
- `UserName`
- `Description`

## 3. BalanceDetail

يمثل سطرًا من تفاصيل الرصيد ويحتوي على:

- `Quantity`
- `Price`
- `ExpiryDate`
- `ProductId`
- `WarehouseId`

## 4. Product

يمثل الصنف القادم من Mock Data:

- `ProductId`
- `ProductName`

## 5. Warehouse

يمثل المخزن القادم من Mock Data:

- `WarehouseId`
- `WarehouseName`

## العلاقات

| العلاقة | الوصف |
|---|---|
| OpeningBalanceDocument → DocumentHeader | كل وثيقة لها رأس واحد |
| OpeningBalanceDocument → BalanceDetail | عند الحفظ يجب وجود صف واحد على الأقل |
| BalanceDetail → Product | كل Detail يرتبط بصنف واحد |
| BalanceDetail → Warehouse | كل Detail يرتبط بمخزن واحد |

## قواعد العمل المرتبطة

| القاعدة | التطبيق |
|---|---|
| BR-02 | `Quantity > 0` |
| BR-03 | يمكن تكرار الصنف وفق اختلاف السعر أو تاريخ الصلاحية |
| BR-04 | `Warehouse` مطلوب |
| BR-06 | المعرفات الداخلية لا تظهر للمستخدم |

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

## نطاق النموذج

هذا النموذج يركز على الـMVP الخاص بالأرصدة الافتتاحية، ولا يتضمن:

- قاعدة بيانات حقيقية.
- API.
- نظام صلاحيات متكامل.
- عمليات الشراء والبيع.
- الترحيل الفعلي للمخزون.
- بنية تحتية إضافية خارج نطاق المتطلبات.
