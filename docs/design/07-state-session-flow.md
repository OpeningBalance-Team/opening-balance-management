# 07 — Session / State Flow

## مخطط حالة وتدفق الرصيد الافتتاحي أثناء الجلسة

يوضح هذا المخطط دورة حياة الرصيد الافتتاحي داخل التطبيق أثناء العمل، بدءًا من فتح الشاشة وإنشاء/تعديل البيانات، مرورًا بعمليات الإضافة والتعديل والحذف والتحقق، ثم الانتقال إلى الحفظ داخل `Mock / In-Memory Data` وعرض نتيجة النجاح.

> يمثل المخطط **حالة العمل داخل التطبيق** وليس `DocumentStatus` أو نظام تخزين دائم.

```mermaid
flowchart TB

    START(["بدء الاستخدام<br/>UC-001 / UC-002 / UC-006"])

    subgraph EDITING["Editing — التعديل داخل الجلسة"]
        direction TB

        DRAFT["Editing / Current Opening Balance<br/>الرصيد الافتتاحي الحالي محفوظ داخل الذاكرة — FR-09"]

        ADDING["Adding — إضافة صف<br/>UC-003"]
        DELETING["Deleting — حذف صف<br/>UC-008"]
        MODIFYING["Modifying — تعديل صف<br/>UC-007"]

        VALIDATING["Validating Row — تحقق من صف واحد<br/>إعادة التحقق قبل الإضافة/التعديل"]

        MSG_ADD["رسالة الخطأ — عدم إضافة الصف"]
        MSG_EDIT["رسالة الخطأ — عدم تحديث الرصيد"]
        MSG_DELETE["رسالة مناسبة للحذف"]
        UPDATE_GRID["تحديث الرصيد / جدول التفاصيل"]

        DRAFT -->|"UC-003 — إضافة"| ADDING
        DRAFT -->|"UC-008 — حذف"| DELETING
        DRAFT -->|"UC-007 — تعديل"| MODIFYING

        ADDING --> VALIDATING
        MODIFYING --> VALIDATING

        VALIDATING -->|"صالح"| UPDATE_GRID
        VALIDATING -->|"غير صالح — NFR-04"| MSG_ADD
        VALIDATING -->|"غير صالح — NFR-04"| MSG_EDIT

        MSG_ADD --> DRAFT
        MSG_EDIT --> DRAFT
        DELETING -->|"تأكيد الحذف"| UPDATE_GRID
        DELETING -->|"إلغاء"| DRAFT

        UPDATE_GRID --> DRAFT
    end

    SAVE_VALIDATE["Validation — حفظ الرصيد<br/>التحقق الشامل قبل الحفظ — UC-009"]

    A1["A1 / A2 / A3<br/>فشل التحقق قبل الحفظ"]
    SAVING["Saving — تنفيذ الحفظ على<br/>Mock / In-Memory Data"]

    A4["A4 — فشل الحفظ<br/>إعادة المحاولة"]

    SAVED["SavedInMemory — حفظ داخل التطبيق<br/>FR-09"]

    SHOWN["Shown — رسالة النجاح<br/>UC-009"]

    SUCCESS_NOTE["شروط نجاح الرصيد:<br/>BR-01: Product + Warehouse + Quantity<br/>BR-02: Quantity > 0<br/>BR-03: تكرار الصنف يسمح به عند اختلاف السعر/الصلاحية<br/>BR-04/BR-05: Warehouse / Product مطلوبان<br/>BR-06: عرض الأسماء بدل المعرفات"]

    INMEMORY_NOTE["الحفظ داخل التطبيق (Mock / In-Memory)<br/>FR-09 — لا يوجد حفظ دائم خارج نطاق الـMVP"]

    SAVED_NOTE["SavedInMemory = «حفظ البيانات داخل التطبيق»<br/>باستخدام Mock / In-Memory Data<br/>ولا يعني DocumentStatus أو Database Persistence"]

    START --> DRAFT

    DRAFT -->|"UC-009 — حفظ"| SAVE_VALIDATE

    SAVE_VALIDATE -->|"A1/A2/A3 — غير صالح"| A1
    A1 -->|"تصحيح البيانات"| DRAFT

    SAVE_VALIDATE -->|"A4 — جاهز للحفظ"| SAVING
    SAVING -->|"فشل الحفظ"| A4
    A4 -->|"إعادة المحاولة"| SAVE_VALIDATE

    SAVING -->|"حفظ ناجح — Mock / In-Memory"| SAVED
    SAVED --> SHOWN

    SUCCESS_NOTE -.-> VALIDATING
    INMEMORY_NOTE -.-> SAVING
    SAVED_NOTE -.-> SAVED

    classDef startEnd fill:#FFFFFF,stroke:#333,stroke-width:2px,color:#111,font-weight:bold;
    classDef editing fill:#F4F0FF,stroke:#7C5CFF,stroke-width:2px,color:#222;
    classDef action fill:#F4F0FF,stroke:#7C5CFF,stroke-width:1.5px,color:#222;
    classDef validation fill:#F7F3FF,stroke:#7C5CFF,stroke-width:1.5px,color:#222;
    classDef error fill:#FFF1F1,stroke:#C62828,stroke-width:1.5px,color:#8B1E1E;
    classDef saving fill:#EEF6FF,stroke:#2563EB,stroke-width:2px,color:#153E75;
    classDef saved fill:#EEF9F1,stroke:#2E7D32,stroke-width:2px,color:#1B5E20,font-weight:bold;
    classDef note fill:#FFF7B8,stroke:#C7A600,stroke-width:1.5px,color:#3F3500;

    class START,SHOWN startEnd;
    class DRAFT editing;
    class ADDING,DELETING,MODIFYING,UPDATE_GRID action;
    class VALIDATING,SAVE_VALIDATE validation;
    class MSG_ADD,MSG_EDIT,MSG_DELETE,A1,A4 error;
    class SAVING saving;
    class SAVED saved;
    class SUCCESS_NOTE,INMEMORY_NOTE,SAVED_NOTE note;
```

---

## 1. الحالات الرئيسية

### Editing

تمثل حالة الرصيد الافتتاحي الحالي أثناء العمل داخل التطبيق.

وتشمل:

- Header.
- Details.
- التعديلات الحالية.
- البيانات الموجودة في الذاكرة أثناء الجلسة.

وترتبط مباشرة بـ:

- `UC-001`
- `UC-002`
- `UC-006`
- `FR-09`

---

### Adding

تمثل عملية إضافة صف تفاصيل جديد وفق `UC-003`.

المسار:

```text
Editing
   ↓
Adding
   ↓
Validating Row
```

---

### Modifying

تمثل تعديل صف موجود وفق `UC-007`.

بعد التعديل يتم إعادة التحقق قبل تحديث بيانات الرصيد.

---

### Deleting

تمثل حذف صف وفق `UC-008`.

يدعم:

- Confirm.
- Cancel.

عند التأكيد يتم حذف الصف وتحديث جدول التفاصيل.

---

### Validating Row

تمثل التحقق من صف التفاصيل قبل الإضافة أو التعديل.

تشمل القواعد المرتبطة في التحليل:

- Product مطلوب.
- Warehouse مطلوب.
- Quantity مطلوبة.
- `Quantity > 0`.
- قواعد التكرار حسب `BR-03`.

---

### Saving

تمثل عملية الحفظ بعد اكتمال التحقق الشامل للوثيقة.

الحفظ يتم باستخدام:

```text
Mock / In-Memory Data
```

ولا يمثل Database Persistence.

---

### SavedInMemory

تمثل نجاح الحفظ **داخل التطبيق**.

وجود هذه الحالة لا يعني وجود:

```text
DocumentStatus
Database
Permanent Persistence
```

بل تمثل فقط أن عملية `UC-009` نجحت ضمن نطاق الـMVP.

---

### Shown

تمثل عرض نتيجة النجاح للمستخدم بعد اكتمال الحفظ.

---

## 2. حالات الفشل

### A1 / A2 / A3 — فشل التحقق قبل الحفظ

إذا لم تكتمل بيانات الرصيد أو لم توجد تفاصيل أو كانت التفاصيل غير صحيحة:

```text
Save
 ↓
Validation
 ↓
Error
 ↓
Editing
```

ويعود المستخدم لتصحيح البيانات.

### A4 — فشل الحفظ

إذا حدث خطأ أثناء الحفظ داخل `Mock / In-Memory Data`:

```text
Saving
  ↓
A4 — Save Error
  ↓
Retry
  ↓
Validation
```

---

## 3. Business Rules المرتبطة

| القاعدة | أثرها في التدفق |
|---|---|
| BR-01 | Product + Warehouse + Quantity مطلوبة |
| BR-02 | Quantity > 0 |
| BR-03 | يسمح بالتكرار عند اختلاف السعر أو تاريخ الصلاحية |
| BR-04 | Warehouse مطلوب |
| BR-05 | Product مطلوب |
| BR-06 | عرض الأسماء بدل المعرفات الداخلية |

---

## 4. Traceability

| المصدر | الحالة / الانتقال |
|---|---|
| UC-001 | بداية استخدام الشاشة |
| UC-002 | إنشاء بيانات الرأس |
| UC-003 | Adding |
| UC-007 | Modifying |
| UC-008 | Deleting |
| UC-009 | Validation → Saving → SavedInMemory → Shown |
| FR-09 | الاحتفاظ بالبيانات داخل التطبيق وMock / In-Memory |

---

## 5. حدود الـMVP

هذا المخطط لا يفترض:

- قاعدة بيانات حقيقية.
- API.
- تخزين دائم خارج نطاق الـMVP.
- `DocumentStatus` كحقل Domain.
- Persistence خارجي.

التركيز هنا على **حالة العمل داخل التطبيق أثناء الجلسة** وعلى نتيجة الحفظ داخل `Mock / In-Memory Data`.

---

## 6. موقع الملف

```text
docs/
└── design/
    └── 07-state-session-flow.md
```
