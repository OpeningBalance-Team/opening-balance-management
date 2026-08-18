# 03 — Domain / ER Model

> **Domain / Entity-Relationship Model**  
> النموذج البياني للكيانات والعلاقات الخاص بميزة إدارة الأرصدة الافتتاحية، ضمن نطاق الـMVP وبالاعتماد على ملف التحليل المرجعي.

---

## المخطط البصري

<div align="center">

<img src="./03-domain-er-model-diagram.svg" alt="Domain / ER Model — Opening Balance Management" width="100%">

</div>

> **طريقة التوثيق:** الرسم محفوظ كـSVG مستقل للمحافظة على مواضع الكيانات والعلاقات والنصوص والتخطيط البصري كما في النسخة المرجعية.  
> ملف الـMarkdown يعرض الرسم مباشرة داخل صفحة GitHub ويحتوي على شرح التصميم والتحقق المرجعي أسفله.

---

## 1. الهدف من المخطط

يمثل هذا المخطط بنية البيانات الأساسية التي تحتاجها ميزة **Opening Balance Management** كما وردت في ملف التحليل.

النموذج يركز على:

- وثيقة الرصيد الافتتاحي.
- بيانات الرأس.
- تفاصيل الرصيد.
- المنتج.
- المخزن.
- العلاقات بين هذه العناصر.

ولا يضيف بنية قاعدة بيانات أو قيودًا غير مطلوبة خارج نطاق الـMVP.

---

## 2. الكيانات

### `OpeningBalanceDocument`

يمثل وثيقة الرصيد الافتتاحي.

**المفتاح الداخلي:**

```text
DocumentId
```

> وجود المعرف الداخلي هو قرار تقني مناسب للنموذج، بينما يجب أن تبقى قواعده متوافقة مع التحليل ولا تُحوّل إلى قيد أعمال غير مذكور.

---

### `DocumentHeader`

يمثل بيانات رأس الوثيقة المطلوبة في التحليل:

| الحقل | الوصف |
|---|---|
| `DocumentNumber` | رقم الوثيقة |
| `DocumentDate` | تاريخ الرصيد الافتتاحي |
| `UserName` | اسم المستخدم |
| `Description` | البيان / الملاحظات |

### ملاحظات

- `DocumentNumber` لا يتم اعتباره Unique Constraint في هذا التصميم.
- التحليل يذكر عدم تكرار رقم الوثيقة فقط كمسار بديل مشروط بتطبيق هذه القاعدة.
- طريقة تعبئة `UserName` تبقى قرارًا تصميميًا، لأن التحليل يسمح بأن يكون الإدخال يدويًا أو أن يتم تعبئته تلقائيًا/من Mock Data.
- `Description` يمكن التعامل معه كقيمة اختيارية حسب التصميم المتوافق مع التحليل.

---

### `BalanceDetail`

يمثل سطرًا واحدًا من تفاصيل الرصيد الافتتاحي.

| الحقل | الوصف |
|---|---|
| `ProductId` | المعرف الداخلي للصنف |
| `WarehouseId` | المعرف الداخلي للمخزن |
| `Quantity` | الكمية |
| `Price` | السعر — اختياري |
| `ExpiryDate` | تاريخ الصلاحية — اختياري |

### قواعد التحقق المرتبطة

- يجب اختيار Product.
- يجب اختيار Warehouse.
- `Quantity > 0`.
- يمكن تكرار الصنف وفق القاعدة المحددة في التحليل.
- لا يتم فرض Composite Unique Key غير مذكور في التحليل.

---

### `Product`

يمثل الصنف القادم من Mock Data.

| الحقل | الوصف |
|---|---|
| `ProductId` | معرف داخلي |
| `ProductName` | الاسم الظاهر للمستخدم |

الواجهة تعرض الاسم للمستخدم، بينما يحتفظ التطبيق بالمعرف داخليًا.

---

### `Warehouse`

يمثل المخزن القادم من Mock Data.

| الحقل | الوصف |
|---|---|
| `WarehouseId` | معرف داخلي |
| `WarehouseName` | الاسم الظاهر للمستخدم |

---

## 3. العلاقات

### وثيقة الرصيد → تفاصيل الرصيد

وثيقة الرصيد تحتوي على تفاصيلها.

وبحسب `UC-009` يجب أن تحتوي الوثيقة على **تفصيل واحد على الأقل عند الحفظ**.

```text
OpeningBalanceDocument
        1
        |
        | 1..*
        |
        v
BalanceDetail
```

### تفاصيل الرصيد → Product

كل `BalanceDetail` يرتبط بمنتج واحد.

ويمكن للمنتج نفسه أن يظهر في أكثر من Detail وفق القاعدة الواردة في التحليل.

### تفاصيل الرصيد → Warehouse

كل `BalanceDetail` يرتبط بمخزن واحد.

ويمكن للمخزن نفسه أن يرتبط بعدة Details.

---

## 4. Business Rules ذات الصلة

| Rule | تطبيقها في النموذج |
|---|---|
| `BR-01` | Product + Warehouse + Quantity مطلوبة عند إضافة Detail |
| `BR-02` | Quantity يجب أن تكون أكبر من صفر |
| `BR-03` | تكرار الصنف مسموح وفق الشرط المذكور في التحليل |
| `BR-04` | Warehouse مطلوب |
| `BR-05` | Product مطلوب |
| `BR-06` | المعرفات الداخلية لا تظهر للمستخدم |

> هذه القواعد تمثل **سلوكًا وتحقيقًا Validation**، وليست جميعها Database Constraints.

---

## 5. القيود التي تم استبعادها عمدًا

لا يحتوي هذا النموذج على أي من القيود التالية لأنها غير مدعومة كمتطلبات إلزامية في ملف التحليل:

- `DocumentNumber UNIQUE`
- `Composite Unique Key`
- `ExpiryDate > Today`
- `Price >= 0`
- `DocumentStatus = Draft/Saved`
- أي قيد Database غير مذكور في التحليل.

---

## 6. Traceability

| المصدر في التحليل | التمثيل |
|---|---|
| `FR-02 / UC-002` | `DocumentHeader` |
| `FR-03 / UC-003` | `BalanceDetail` |
| `FR-04 / UC-004` | `Product` |
| `FR-05 / UC-005` | `Warehouse` |
| `FR-06 / UC-006` | `BalanceDetail` |
| `FR-07 / UC-007` | تحديث `BalanceDetail` |
| `FR-08 / UC-008` | حذف `BalanceDetail` |
| `FR-09 / UC-009` | `OpeningBalanceDocument` + Details |

---

## 7. توافقه مع بقية التصميم

يجب أن تكون أسماء الكيانات في هذا المخطط متطابقة مع المخططات التالية:

```text
Component & Layered Architecture
        ↓
Component / UI Structure
        ↓
Domain / ER Model
        ↓
Activity / User Flow
        ↓
Sequence Diagrams
```

الأسماء الأساسية التي يجب الحفاظ عليها:

```text
OpeningBalanceDocument
DocumentHeader
BalanceDetail
Product
Warehouse
OpeningBalanceService
BalanceValidationService
InMemoryRepository
```

أي تغيير لاحق يجب أن يكون موثقًا كـDesign Decision قبل بدء الـCoding.

---

## 8. Design Decision Notes

### `DocumentHeader`

يُستخدم هنا لتمثيل بيانات الرأس الواردة في التحليل. لا يتم إعطاؤه Lifecycle مستقلًا عن وثيقة الرصيد إلا إذا تطلب التصميم ذلك.

### `Quantity`

ملف التحليل يفرض أن الكمية أكبر من صفر، لكنه لا يحدد Type تقنيًا بعينه؛ اختيار `int` أو `decimal` هو قرار تصميمي يجب أن يتوافق مع طبيعة بيانات المشروع.

### `Price`

اختياري وفق التحليل ولا يتم فرض شرط إضافي مثل `Price >= 0` ما لم يتم توثيقه كقرار لاحق.

### `ExpiryDate`

اختياري عند الحاجة، ولا يتم فرض أنه يجب أن يكون في المستقبل.

---

## 9. حدود الـMVP

هذا النموذج لا يتضمن:

- قاعدة بيانات حقيقية.
- API.
- Authentication.
- Authorization كامل.
- Inventory Posting.
- Purchasing.
- Sales.
- أي بنية Enterprise غير مطلوبة.

البيانات خلال هذه المرحلة تعتمد على **Mock / In-Memory Data** كما هو محدد في التحليل.

---

## 10. موقع الملف في المستودع

```text
docs/
└── design/
    ├── 01-component-and-layered-architecture.md
    ├── 02-component-ui-structure.md
    └── 03-domain-er-model.md
```

### قاعدة التوثيق للمخططات

سيتم اعتماد نفس الأسلوب لجميع مخططات المشروع:

1. ملف `.md` مستقل.
2. الرسم البصري داخل الـMarkdown عبر ملف SVG محلي.
3. شرح التصميم أسفل الرسم.
4. Traceability مع التحليل.
5. Design Decisions والقيود المهمة.
6. عدم إدخال افتراضات غير مدعومة من ملف التحليل.
