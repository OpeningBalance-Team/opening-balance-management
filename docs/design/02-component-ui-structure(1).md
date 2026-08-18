# مخطط بنية المكونات وواجهة المستخدم

> **Component & UI Structure Diagram**  
> يوضح هذا المخطط مكونات واجهة إدارة الأرصدة الافتتاحية وعلاقاتها مع الخدمة، التحقق، الحالة المؤقتة، وبيانات Mock / In-Memory.

## المخطط

```mermaid
flowchart TB

    %% =========================
    %% Actors
    %% =========================
    U["موظف المخزون / مدير النظام<br/>Authorization خارج نطاق MVP"]

    %% =========================
    %% Main page
    %% =========================
    PAGE["OpeningBalancePage.razor<br/>UC-001 — شاشة الرصيد الافتتاحي<br/>Arabic / RTL"]

    %% =========================
    %% Header
    %% =========================
    HEADER["HeaderForm.razor<br/>FR-01 / FR-02<br/>حقول رأس الوثيقة"]

    DOCNO["حقل رقم الوثيقة"]
    DATE["حقل التاريخ"]
    USER["حقل المستخدم"]
    DESC["حقل البيان"]

    %% =========================
    %% Details
    %% =========================
    GRID["DetailsGrid.razor<br/>FR-06 / UC-006<br/>عرض تفاصيل الرصيد + العمليات"]

    ADD["AddDetailRow<br/>UC-003<br/>سطر إدخال جديد"]

    EDIT["EditDetailRow<br/>UC-007<br/>تعديل السطر المحدد"]

    DELETE["حذف السطر<br/>UC-008"]

    PRODUCT["ProductDropdown<br/>UC-004 / BR-06<br/>عرض الاسم + حفظ المعرّف داخليًا"]

    WAREHOUSE["WarehouseDropdown<br/>UC-005 / BR-06<br/>عرض الاسم + حفظ المعرّف داخليًا"]

    QTY_ADD["الكمية<br/>أثناء الإضافة"]
    PRICE_ADD["السعر<br/>اختياري"]
    EXP_ADD["تاريخ الصلاحية<br/>عند الحاجة"]

    QTY_EDIT["Quantity<br/>قابل للتعديل"]
    PRICE_EDIT["Price<br/>قابل للتعديل"]
    EXP_EDIT["ExpiryDate<br/>قابل للتعديل"]

    CONFIRM["ConfirmDialog<br/>UC-008<br/>Confirm / Cancel"]

    MESSAGE["MessageBox / ValidationMessage<br/>NFR-04 / NFR-06"]

    %% =========================
    %% Service layer
    %% =========================
    SERVICE["OpeningBalanceService<br/>AddDetail / UpdateDetail / DeleteDetail / SaveDocument<br/>LoadProducts / LoadWarehouses"]

    VALIDATION["BalanceValidationService<br/>ValidateHeader / ValidateDetail / ValidateDocument"]

    RESULT["Validation / Operation Result<br/>Success / Error"]

    %% =========================
    %% Current state
    %% =========================
    SESSION["Current Opening Balance (Draft)<br/>Header + Details[]<br/>حالة مؤقتة أثناء التشغيل"]

    %% =========================
    %% Mock / In-Memory
    %% =========================
    REPO["InMemoryRepository<br/>Mock / In-Memory Data<br/>Products / Warehouses / Opening Balances"]

    %% =========================
    %% User -> Page
    %% =========================
    U --> PAGE

    %% Page composition
    PAGE --> HEADER
    PAGE --> GRID
    PAGE --> MESSAGE

    %% Header fields
    HEADER --> DOCNO
    HEADER --> DATE
    HEADER --> USER
    HEADER --> DESC

    %% Details composition
    GRID --> ADD
    GRID --> EDIT
    GRID --> DELETE

    %% Add
    ADD --> PRODUCT
    ADD --> WAREHOUSE
    ADD --> QTY_ADD
    ADD --> PRICE_ADD
    ADD --> EXP_ADD

    %% Edit
    EDIT --> PRODUCT
    EDIT --> WAREHOUSE
    EDIT --> QTY_EDIT
    EDIT --> PRICE_EDIT
    EDIT --> EXP_EDIT

    %% Delete confirmation
    DELETE --> CONFIRM
    CONFIRM -- "Confirm" --> SERVICE
    CONFIRM -. "Cancel / no change" .-> GRID

    %% UI -> Service
    HEADER --> SERVICE
    ADD --> SERVICE
    EDIT --> SERVICE
    GRID --> SERVICE

    %% Service -> Validation
    SERVICE --> VALIDATION
    VALIDATION --> RESULT
    RESULT --> MESSAGE

    %% Service <-> Draft State
    SERVICE <--> SESSION

    %% Service <-> In-Memory data
    SERVICE <--> REPO

    %% Reference data
    PRODUCT -. "Product Mock Data" .-> REPO
    WAREHOUSE -. "Warehouse Mock Data" .-> REPO

    %% =========================
    %% Styling
    %% =========================
    classDef actor fill:#E8F5E9,stroke:#2E7D32,stroke-width:2px,color:#1B5E20;
    classDef page fill:#EDE7F6,stroke:#5E35B1,stroke-width:2px,color:#311B92;
    classDef ui fill:#E8EAF6,stroke:#3949AB,stroke-width:1.5px,color:#1A237E;
    classDef service fill:#E3F2FD,stroke:#1565C0,stroke-width:2px,color:#0D47A1;
    classDef state fill:#FFF8E1,stroke:#F9A825,stroke-width:2px,color:#6D4C41;
    classDef data fill:#FCE4EC,stroke:#C2185B,stroke-width:2px,color:#880E4F;
    classDef result fill:#E0F2F1,stroke:#00897B,stroke-width:1.5px,color:#004D40;

    class U actor;
    class PAGE page;
    class HEADER,GRID,ADD,EDIT,DELETE,PRODUCT,WAREHOUSE,QTY_ADD,PRICE_ADD,EXP_ADD,QTY_EDIT,PRICE_EDIT,EXP_EDIT,CONFIRM,MESSAGE,DOCNO,DATE,USER,DESC ui;
    class SERVICE,VALIDATION service;
    class RESULT result;
    class SESSION state;
    class REPO data;
```

---

## شرح المخطط

### 1. المستخدمون

المخطط يبدأ من:

- موظف المخزون.
- مدير النظام.

أما نظام الصلاحيات الكامل فخارج نطاق الـMVP.

### 2. OpeningBalancePage

هي الصفحة الرئيسية التي تجمع:

- `HeaderForm`
- `DetailsGrid`
- رسائل التحقق والعمليات.

### 3. HeaderForm

يمثل بيانات رأس الوثيقة:

- رقم الوثيقة.
- التاريخ.
- المستخدم.
- البيان.

### 4. DetailsGrid

يمثل تفاصيل الرصيد ويتيح:

- إضافة سطر.
- تعديل سطر.
- حذف سطر.
- عرض بيانات التفاصيل.

### 5. AddDetailRow

عند إضافة تفصيل يتم التعامل مع:

- Product.
- Warehouse.
- Quantity.
- Price عند الحاجة.
- ExpiryDate عند الحاجة.

### 6. EditDetailRow

يسمح بتعديل:

- Product.
- Warehouse.
- Quantity.
- Price.
- ExpiryDate.

### 7. ConfirmDialog

يظهر قبل تنفيذ الحذف:

```text
Delete
  ↓
Confirm / Cancel
```

عند `Confirm` تنتقل العملية إلى `OpeningBalanceService`.

وعند `Cancel` تبقى البيانات كما هي.

### 8. OpeningBalanceService

ينسق العمليات الرئيسية:

- `AddDetail`
- `UpdateDetail`
- `DeleteDetail`
- `SaveDocument`
- `LoadProducts`
- `LoadWarehouses`

### 9. BalanceValidationService

مسؤول عن التحقق من:

- Header.
- Detail.
- Document.

ويُرجع نتيجة تحقق بدل التعامل مباشرة مع واجهة المستخدم.

### 10. Validation / Operation Result

يمثل نتيجة العملية:

- Success.
- Error.
- Validation details.

ثم تصل النتيجة إلى واجهة Blazor لعرض الرسالة المناسبة.

### 11. Current Opening Balance (Draft)

يمثل حالة الرصيد الحالي أثناء التشغيل:

```text
Header
Details[]
```

وهو تمثيل تصميمي للبيانات المؤقتة قبل الحفظ ضمن نطاق الـMVP.

### 12. InMemoryRepository

يوفر Mock / In-Memory Data لـ:

- Products.
- Warehouses.
- Opening Balances.

ولا توجد قاعدة بيانات حقيقية في هذه المرحلة.

---

## Traceability

| Requirement / Use Case | Component |
|---|---|
| FR-01 / UC-001 | `OpeningBalancePage.razor` |
| FR-02 / UC-002 | `HeaderForm.razor` |
| FR-03 / UC-003 | `AddDetailRow` |
| FR-04 / UC-004 | `ProductDropdown` |
| FR-05 / UC-005 | `WarehouseDropdown` |
| FR-06 / UC-006 | `DetailsGrid.razor` |
| FR-07 / UC-007 | `EditDetailRow` |
| FR-08 / UC-008 | `Delete` + `ConfirmDialog` |
| FR-09 / UC-009 | `OpeningBalanceService` + `Current Opening Balance (Draft)` + `InMemoryRepository` |

---

## ملاحظات تصميمية

- لا يُفرض تفرد لرقم الوثيقة في هذا المخطط؛ التحليل يذكر عدم التكرار كمسار بديل مشروط.
- لا يتم فرض Composite Unique Key على تفاصيل الرصيد.
- لا تتم إضافة قواعد جديدة للسعر أو تاريخ الصلاحية غير موجودة في التحليل.
- `Product` و`Warehouse` يستخدمان الأسماء في الواجهة مع الاحتفاظ بالمعرفات داخليًا.
- هذا المخطط يمثل **Component & UI Structure**، بينما تفاصيل الـDomain Model والـSequence Diagrams موثقة في مخططات مستقلة.

---

## موقع الملف المقترح

```text
docs/
└── design/
    └── 02-component-ui-structure.md
```
