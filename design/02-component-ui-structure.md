# مخطط المكوّنات وواجهة المستخدم — الإصدار المعتمد

> **Component & UI Structure Diagram**  
> هذا المخطط يمثل البنية التفصيلية لواجهة إدارة الأرصدة الافتتاحية والمكوّنات المرتبطة بها وفق التصميم المعتمد.

## المخطط

> **ملاحظة:** تم تضمين المخطط كـSVG للحفاظ على **نفس التخطيط البصري، ترتيب العناصر، الأسهم، النصوص، والأبعاد** كما في النسخة المرجعية.

![Component & UI Structure Diagram](./02-component-ui-structure-diagram.svg)

## مكونات المخطط

### المستخدمون
- موظف المخزون
- مدير النظام
- إدارة الصلاحيات خارج نطاق الـMVP.

### صفحة الأرصدة الافتتاحية
`OpeningBalancePage.razor`

تمثل الصفحة الرئيسية للشاشة وتجمع بين:
- `HeaderForm`
- `DetailsGrid`
- رسائل التحقق والعمليات.

### رأس الوثيقة
`HeaderForm`

يشمل:
- رقم الوثيقة.
- التاريخ.
- المستخدم.
- البيان.

### تفاصيل الرصيد
`DetailsGrid`

يشمل عمليات:
- إضافة سطر.
- تعديل سطر.
- حذف سطر.
- عرض تفاصيل المنتج والمخزن والكمية والسعر وتاريخ الصلاحية.

### الإضافة والتعديل
`AddDetailRow` و`EditDetailRow`

تتعامل مع:
- Product.
- Warehouse.
- Quantity.
- Price.
- Expiry Date عند الحاجة.

### التحقق
`BalanceValidationService`

مسؤول عن التحقق من قواعد العمل قبل تنفيذ العمليات.

### الخدمة الرئيسية
`OpeningBalanceService`

تنظم عمليات:
- `AddDetail`
- `UpdateDetail`
- `DeleteDetail`
- `SaveDocument`
- `LoadProducts`
- `LoadWarehouses`

### الحالة الحالية
`Current Opening Balance (Draft)`

تمثل بيانات الرأس والتفاصيل الحالية أثناء التشغيل قبل الحفظ النهائي داخل نطاق الـMVP.

### البيانات
`InMemoryRepository`

يحتوي على Mock / In-Memory Data الخاصة بـ:
- Products
- Warehouses
- Opening Balances

### التأكيد والرسائل
- `ConfirmDialog` للحذف.
- `MessageBox / ValidationMessage` لعرض نتائج العمليات ورسائل التحقق.

## العلاقة مع المتطلبات

المخطط يغطي بصورة مباشرة:

| المتطلب / الحالة | المكوّن أو الجزء |
|---|---|
| FR-01 / UC-001 | `OpeningBalancePage.razor` |
| FR-02 / UC-002 | `HeaderForm` |
| FR-03 / UC-003 | `AddDetailRow` |
| FR-04 / UC-004 | `ProductDropdown` |
| FR-05 / UC-005 | `WarehouseDropdown` |
| FR-06 / UC-006 | `DetailsGrid` |
| FR-07 / UC-007 | `EditDetailRow` |
| FR-08 / UC-008 | `Delete Row` + `ConfirmDialog` |
| FR-09 / UC-009 | `Current Opening Balance (Draft)` + `OpeningBalanceService` + `InMemoryRepository` |

## ملاحظة التصميم

هذا الملف يمثل **Component & UI Structure Diagram**، وليس مخطط قواعد البيانات أو مخطط تسلسل التنفيذ.

ويجب أن تظل المخططات التالية متسقة معه في الأسماء والمسؤوليات:

1. Domain / ER Diagram
2. User Flow / Activity Diagram
3. Sequence Diagram — Add Detail
4. Sequence Diagram — Edit / Delete
5. Sequence Diagram — Save
