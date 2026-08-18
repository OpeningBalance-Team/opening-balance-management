# 04 — Activity / Main User Flow

## تدفق المستخدم والعمليات الرئيسية

يمثل هذا المخطط التدفق الرئيسي لاستخدام ميزة **إدارة الأرصدة الافتتاحية**، مع تغطية حالات الاستخدام `UC-001` إلى `UC-009` والمسارات البديلة والرسائل الناتجة عن حالات الخطأ.

```mermaid
flowchart TD

    START(["بدء<br/>المستخدم مسجل الدخول"])

    %% =========================
    %% UC-001
    %% =========================
    UC001["UC-001<br/><b>فتح شاشة الأرصدة الافتتاحية</b>"]

    LOAD["تحميل بيانات Products و Warehouses<br/>من Mock / In-Memory Data"]

    DATA_OK{"هل الشاشة والبيانات<br/>متاحة؟"}

    LOAD_ERROR["رسالة خطأ مناسبة<br/>فشل تحميل الشاشة أو عدم توفر البيانات"]

    %% =========================
    %% UC-002
    %% =========================
    UC002["UC-002<br/><b>إدخال بيانات رأس الوثيقة</b><br/>رقم الوثيقة + التاريخ + المستخدم + البيان"]

    HEADER_OK{"هل بيانات الرأس<br/>صحيحة ومكتملة؟"}

    HEADER_ERROR["رسالة تحقق مناسبة<br/>تصحيح بيانات الرأس"]

    %% =========================
    %% UC-003 / 004 / 005
    %% =========================
    UC003["UC-003<br/><b>إضافة Detail</b>"]

    PRODUCT["UC-004<br/>اختيار Product<br/>من Mock Data"]

    WAREHOUSE["UC-005<br/>اختيار Warehouse<br/>من Mock Data"]

    DETAIL_INPUT["إدخال بيانات Detail<br/>Quantity + Price عند الحاجة + ExpiryDate عند الحاجة"]

    ADD["اضغط Add"]

    DETAIL_OK{"هل بيانات الـDetail<br/>صحيحة؟<br/><br/>Product ✓<br/>Warehouse ✓<br/>Quantity > 0 ✓"}

    DETAIL_ERROR["رسالة تحقق مناسبة<br/>تصحيح بيانات الـDetail"]

    %% =========================
    %% UC-006
    %% =========================
    UC006["UC-006<br/><b>عرض تفاصيل الرصيد</b><br/>جدول التفاصيل"]

    %% =========================
    %% User action
    %% =========================
    ACTION{"العملية التالية؟"}

    %% =========================
    %% Add another
    %% =========================
    ADD_ANOTHER["إضافة صنف آخر"]

    %% =========================
    %% UC-007 Edit
    %% =========================
    UC007["UC-007<br/><b>تعديل Detail</b><br/>Product / Warehouse / Quantity / Price / ExpiryDate"]

    EDIT_OK{"هل بيانات التعديل<br/>صحيحة؟"}

    EDIT_ERROR["رسالة تحقق مناسبة<br/>عدم تحديث السجل"]

    EDIT_SUCCESS["تحديث السجل داخل Details<br/>وتحديث الجدول"]

    %% =========================
    %% UC-008 Delete
    %% =========================
    UC008["UC-008<br/><b>حذف Detail</b>"]

    CONFIRM{"تأكيد الحذف؟"}

    DELETE["حذف السجل<br/>وتحديث جدول التفاصيل"]

    DELETE_SUCCESS["رسالة نجاح الحذف"]

    %% =========================
    %% UC-009 Save
    %% =========================
    UC009["UC-009<br/><b>حفظ الرصيد الافتتاحي</b>"]

    SAVE_VALIDATE{"هل الوثيقة صالحة للحفظ؟"}

    A1["A1 — Header ناقص<br/>منع الحفظ + رسالة تحقق"]

    A2["A2 — لا توجد Details<br/>يجب إضافة صنف واحد على الأقل"]

    A3["A3 — توجد بيانات Detail غير صحيحة<br/>منع الحفظ + رسالة خطأ"]

    SAVE_MOCK["محاولة الحفظ باستخدام<br/>Mock / In-Memory Data"]

    A4{"هل نجحت عملية الحفظ؟"}

    SAVE_ERROR["A4 — فشل الحفظ<br/>رسالة خطأ + إمكانية إعادة المحاولة"]

    SAVE_SUCCESS["تم حفظ الرصيد الافتتاحي داخل التطبيق<br/>Header + Details"]

    END(["نهاية التدفق"])

    %% =========================
    %% Main flow
    %% =========================
    START --> UC001
    UC001 --> LOAD
    LOAD --> DATA_OK

    DATA_OK -- "نعم" --> UC002
    DATA_OK -- "لا" --> LOAD_ERROR
    LOAD_ERROR -. "إعادة المحاولة" .-> UC001

    UC002 --> HEADER_OK
    HEADER_OK -- "نعم" --> UC003
    HEADER_OK -- "لا" --> HEADER_ERROR
    HEADER_ERROR -. "تصحيح البيانات" .-> UC002

    UC003 --> PRODUCT
    PRODUCT --> WAREHOUSE
    WAREHOUSE --> DETAIL_INPUT
    DETAIL_INPUT --> ADD
    ADD --> DETAIL_OK

    DETAIL_OK -- "نعم" --> UC006
    DETAIL_OK -- "لا" --> DETAIL_ERROR
    DETAIL_ERROR -. "تصحيح البيانات" .-> DETAIL_INPUT

    UC006 --> ACTION

    %% User actions
    ACTION -- "إضافة" --> ADD_ANOTHER
    ADD_ANOTHER --> UC003

    ACTION -- "تعديل" --> UC007
    UC007 --> EDIT_OK
    EDIT_OK -- "نعم" --> EDIT_SUCCESS
    EDIT_OK -- "لا" --> EDIT_ERROR
    EDIT_ERROR -. "تصحيح البيانات" .-> UC007
    EDIT_SUCCESS --> UC006

    ACTION -- "حذف" --> UC008
    UC008 --> CONFIRM
    CONFIRM -- "إلغاء" --> UC006
    CONFIRM -- "تأكيد" --> DELETE
    DELETE --> DELETE_SUCCESS
    DELETE_SUCCESS --> UC006

    ACTION -- "حفظ" --> UC009
    UC009 --> SAVE_VALIDATE

    %% Save alternate flows
    SAVE_VALIDATE -- "A1: Header غير مكتمل" --> A1
    SAVE_VALIDATE -- "A2: لا توجد Details" --> A2
    SAVE_VALIDATE -- "A3: Detail غير صحيح" --> A3
    SAVE_VALIDATE -- "صحيح" --> SAVE_MOCK

    A1 -. "تصحيح البيانات" .-> UC002
    A2 -. "إضافة Detail" .-> UC003
    A3 -. "تصحيح Detail" .-> UC006

    SAVE_MOCK --> A4
    A4 -- "لا" --> SAVE_ERROR
    SAVE_ERROR -. "إعادة المحاولة" .-> UC009

    A4 -- "نعم" --> SAVE_SUCCESS
    SAVE_SUCCESS --> END

    %% =========================
    %% Styling
    %% =========================
    classDef startEnd fill:#E8F5E9,stroke:#2E7D32,stroke-width:2px,color:#1B5E20;
    classDef usecase fill:#EDE7F6,stroke:#5E35B1,stroke-width:2px,color:#311B92;
    classDef process fill:#E3F2FD,stroke:#1565C0,stroke-width:1.5px,color:#0D47A1;
    classDef decision fill:#FFF8E1,stroke:#F9A825,stroke-width:2px,color:#6D4C41;
    classDef error fill:#FFEBEE,stroke:#C62828,stroke-width:1.5px,color:#B71C1C;
    classDef success fill:#E0F2F1,stroke:#00897B,stroke-width:1.5px,color:#004D40;

    class START,END startEnd;
    class UC001,UC002,UC003,UC004,UC005,UC006,UC007,UC008,UC009 usecase;
    class LOAD,PRODUCT,WAREHOUSE,DETAIL_INPUT,ADD,ADD_ANOTHER,EDIT_SUCCESS,DELETE,SAVE_MOCK process;
    class DATA_OK,HEADER_OK,DETAIL_OK,ACTION,EDIT_OK,CONFIRM,SAVE_VALIDATE,A4 decision;
    class LOAD_ERROR,HEADER_ERROR,DETAIL_ERROR,EDIT_ERROR,A1,A2,A3,SAVE_ERROR error;
    class DELETE_SUCCESS,SAVE_SUCCESS success;
```

---

## 1. قراءة التدفق

### UC-001 — فتح الشاشة

يبدأ المستخدم بفتح شاشة الأرصدة الافتتاحية، ثم يقوم النظام بتحميل بيانات الأصناف والمخازن الوهمية.

إذا فشل التحميل أو لم تتوفر البيانات، تظهر رسالة مناسبة ويمكن إعادة المحاولة.

### UC-002 — إدخال Header

يدخل المستخدم:

- رقم الوثيقة.
- التاريخ.
- المستخدم.
- البيان/الملاحظات.

يتم التحقق من صحة واكتمال بيانات الرأس قبل الانتقال إلى إضافة التفاصيل.

لا يفرض هذا التدفق تفرد رقم الوثيقة؛ عدم التكرار ورد في التحليل كمسار بديل مشروط.

### UC-003 / UC-004 / UC-005 — إضافة Detail

يمر الإدخال بالتسلسل:

```text
اختيار Product
        ↓
اختيار Warehouse
        ↓
إدخال Quantity
        ↓
Price / ExpiryDate عند الحاجة
        ↓
Add
        ↓
Validation
```

التحقق الأساسي المرتبط بالتفاصيل:

- Product مطلوب.
- Warehouse مطلوب.
- Quantity يجب أن تكون أكبر من صفر.

### UC-006 — عرض التفاصيل

بعد نجاح الإضافة يظهر السجل في جدول التفاصيل، ويستطيع المستخدم الاستمرار في العمل على الوثيقة.

### UC-007 — التعديل

يسمح للمستخدم بتعديل بيانات السطر ثم إعادة التحقق منها قبل تحديث الجدول.

### UC-008 — الحذف

يظهر تأكيد قبل الحذف:

```text
Delete
  ↓
Confirm / Cancel
```

عند التأكيد يحذف السجل ويتم تحديث الجدول وتظهر رسالة نجاح.
وعند الإلغاء تبقى البيانات كما هي.

### UC-009 — الحفظ

قبل الحفظ يتم التحقق من الوثيقة وفق المسارات البديلة:

- **A1:** بيانات Header ناقصة.
- **A2:** لا توجد Details.
- **A3:** توجد بيانات Detail غير صحيحة.
- **A4:** فشل الحفظ في Mock / In-Memory Data.

عند نجاح الحفظ يتم الاحتفاظ ببيانات Header وDetails داخل التطبيق.

---

## 2. Traceability

| المصدر | الجزء في التدفق |
|---|---|
| FR-01 / UC-001 | فتح الشاشة وتحميل Mock Data |
| FR-02 / UC-002 | إدخال والتحقق من Header |
| FR-03 / UC-003 | إضافة Detail |
| FR-04 / UC-004 | اختيار Product |
| FR-05 / UC-005 | اختيار Warehouse |
| FR-06 / UC-006 | عرض Details |
| FR-07 / UC-007 | تعديل Detail |
| FR-08 / UC-008 | حذف Detail + Confirmation |
| FR-09 / UC-009 | التحقق والحفظ |

---

## 3. ملاحظات تصميمية

- الرسم يركز على السلوك الوظيفي للمستخدم، وليس على تفاصيل طبقات النظام.
- لا يتضمن هذا المخطط قاعدة بيانات حقيقية أو API أو بنية خارج نطاق الـMVP.
- قواعد التحقق والرسائل يجب أن تبقى متوافقة مع وثيقة المتطلبات.
- تفاصيل التنفيذ الداخلية للخدمات تظهر في مخطط المعمارية ومخططات التسلسل.
- أي تغيير في التدفق يجب أن يراجع مقابل حالات الاستخدام وقواعد العمل قبل التنفيذ.

---

## 4. موقع الملف

```text
docs/
└── design/
    └── 04-activity-main-user-flow.md
```
