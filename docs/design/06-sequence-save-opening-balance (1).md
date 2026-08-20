# 06 — Sequence Diagram: Save Opening Balance

## مخطط التسلسل — حفظ الرصيد الافتتاحي

يوضح هذا المخطط تسلسل عملية حفظ بيانات الرصيد الافتتاحي، بدءًا من ضغط المستخدم على زر الحفظ، مرورًا بالتحقق من بيانات الرأس والتفاصيل، ثم تنفيذ الحفظ في `InMemoryRepository` وإرجاع النتيجة للمستخدم.

## المخطط

```mermaid
sequenceDiagram
    autonumber

    participant User as "المستخدم<br/>(موظف المخزون/مدير النظام)"
    participant Page as "OpeningBalancePage + HeaderForm"
    participant Service as "OpeningBalanceService"
    participant Valid as "BalanceValidationService"
    participant Repo as "InMemoryRepository"

    User->>Page: "ينقر حفظ" (خطوة 7 من UC-009)
    Page->>Service: "SaveDocument(header, details[])"
    Service->>Valid: "VerifyHeader(header)"

    Note right of Valid: "التحقق المعتمد: وجود رقم الوثيقة + صحة التاريخ + اكتمال Header"

    alt "UC-009 A1: غير مكتمل Header"
        Valid-->>Service: "ValidationResult: خطأ"
        Service-->>Page: "يرجى استكمال بيانات الرصيد الافتتاحي"
        Page-->>User: "عرض رسالة الخطأ — لا يتم الحفظ"
    end

    Service->>Valid: "VerifyDocument(details[])"

    Note right of Valid: "التحقق من التفاصيل:<br/>BR-01: وجود Product + Warehouse + Quantity<br/>BR-05: Product مطلوب<br/>BR-04: Warehouse مطلوب<br/>BR-02: Quantity > 0"

    alt "UC-009 A2: لا توجد Details"
        Valid-->>Service: "ValidationResult: خطأ — لا صفوف"
        Service-->>Page: "يجب إضافة صنف واحد على الأقل قبل الحفظ"
        Page-->>User: "عرض الرسالة — لا يتم الحفظ"
    else "UC-009 A3: توجد Details غير صالحة"
        Valid-->>Service: "ValidationResult: خطأ — بيانات غير صالحة"
        Service-->>Page: "رسالة توضح المشكلة (NFR-04)"
        Page-->>User: "يرجى تصحيح البيانات — لا يتم الحفظ"
    end

    Service->>Repo: "SaveDocument(doc)"

    alt "UC-009 A4: فشل الحفظ في Mock Data"
        Repo-->>Service: "استثناء / فشل حفظ"
        Service-->>Page: "فشل الحفظ"
        Page-->>User: "تعذر حفظ الرصيد الافتتاحي — إمكانية إعادة المحاولة"
    else "حفظ ناجح"
        Repo-->>Service: "نجاح"
        Service-->>Page: "نجاح"

        Note over Page,Service: "Postconditions (UC-009):<br/>1. تم حفظ بيانات الرصيد داخل التطبيق (FR-09)<br/>2. تم الاحتفاظ بـ Header + Details معًا<br/>3. الوثيقة جاهزة للعرض أو التعديل<br/>4. عرض رسالة النجاح"

        Page-->>User: "تم حفظ الرصيد الافتتاحي بنجاح"
    end
```

---

## 1. الأطراف المشاركة

| المشارك | المسؤولية |
|---|---|
| المستخدم | يبدأ عملية الحفظ ويتلقى النتيجة |
| `OpeningBalancePage + HeaderForm` | يستقبل طلب الحفظ ويعرض نتائج العملية |
| `OpeningBalanceService` | ينسق عملية التحقق والحفظ |
| `BalanceValidationService` | يتحقق من Header وDetails |
| `InMemoryRepository` | ينفذ التخزين ضمن Mock / In-Memory Data |

---

## 2. المسار الرئيسي

```text
User
  ↓
OpeningBalancePage
  ↓
OpeningBalanceService
  ↓
VerifyHeader
  ↓
VerifyDocument
  ↓
SaveDocument
  ↓
InMemoryRepository
  ↓
Success
  ↓
Message to User
```

---

## 3. المسارات البديلة

### UC-009 A1 — Header غير مكتمل

إذا كانت بيانات الرأس غير مكتملة:

- يتم رفض عملية الحفظ.
- تعرض رسالة:
  `يرجى استكمال بيانات الرصيد الافتتاحي`
- يعود المستخدم إلى حالة التعديل.

### UC-009 A2 — لا توجد Details

إذا كانت الوثيقة تحتوي على Header فقط دون تفاصيل:

- يتم رفض الحفظ.
- تعرض رسالة:
  `يجب إضافة صنف واحد على الأقل قبل الحفظ`

### UC-009 A3 — تفاصيل غير صحيحة

إذا كانت التفاصيل موجودة ولكن تحتوي على بيانات غير صحيحة:

- يتم رفض الحفظ.
- تعرض رسالة توضح المشكلة.
- يجب تصحيح البيانات قبل إعادة الحفظ.

### UC-009 A4 — فشل الحفظ

إذا حدث فشل أثناء التخزين في Mock / In-Memory Data:

- تعاد نتيجة فشل.
- تعرض رسالة فشل الحفظ.
- يمكن إعادة المحاولة.

---

## 4. نجاح الحفظ

عند نجاح العملية:

- يتم حفظ بيانات الرصيد داخل التطبيق.
- يتم الاحتفاظ ببيانات Header وDetails معًا.
- تصبح الوثيقة جاهزة للعرض أو التعديل.
- تظهر رسالة نجاح للمستخدم.

---

## 5. Traceability

| المرجع | التمثيل في المخطط |
|---|---|
| UC-009 | التدفق الرئيسي للحفظ |
| UC-009 A1 | التحقق من Header |
| UC-009 A2 | التحقق من وجود Details |
| UC-009 A3 | التحقق من صحة Details |
| UC-009 A4 | فشل الحفظ في Mock Data |
| FR-09 | الحفظ داخل التطبيق باستخدام Mock / In-Memory |
| BR-01 | Product + Warehouse + Quantity |
| BR-02 | Quantity > 0 |
| BR-04 | Warehouse مطلوب |
| BR-05 | Product مطلوب |
| NFR-04 | رسائل النجاح والخطأ المفهومة |

---

## 6. موقع الملف

```text
docs/
└── design/
    └── 06-sequence-save-opening-balance.md
```
