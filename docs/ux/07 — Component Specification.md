# 07 — Component Specification

| البند | القيمة |
|---|---|
| المرجع الأول | مخطط Component/UI Structure المعتمد (أسماء المكونات والتبعيات) |
| المرجع الثاني | `task.pdf` — FR-01..09، UC-001..009، قسم 6.7، BR-01..06 |
| المرجع الثالث | UI States (وثيقة 06) — الحالات البصرية لكل مكون |
| الإعداد | Manus AI |

---

## 1. المنهجية

المواصفات أدناه تحدد سلوك كل مكون من منظور UX/Implementation بحيث يُحوَّل مباشرة إلى Blazor Component دون إعادة تفكير. أسماء المكونات حرفية من مخطط Component/UI Structure المعتمد، وأحداثها تتطابق مع التسلسلات في Sequence Diagrams (UC-003 / UC-009).

## 2. OpeningBalancePage (الشاشة الأم)

| البند | المواصفة |
|---|---|
| الدور | الشاشة الوحيدة — تستضيف المناطق الخمس وتدير حالة الجلسة (OpeningBalanceSession: Editing / Validation / Saving / SavedInMemory) |
| عند التحميل (UC-001) | تستدعي `OpeningBalanceService.GetProducts()` و`GetWarehouses()`؛ عند الفشل تعرض «لا يمكن تحميل البيانات» وتبقى بلا تفاعل (UC-001 Alternate) |
| الحالة الداخلية | قائمة سطور الجلسة (List<BalanceDetail>) + رأس مؤقت (DocumentHeader) + رسالة عالمية |
| التبعيات | OpeningBalanceService (الحفظ والتحقق)، InMemoryRepository (مصدر القوائم) |
| Traceability | FR-09، UC-001، FR-06 |

## 3. HeaderForm

| البند | المواصفة |
|---|---|
| الدور | منطقة الرأس — أربعة حقول (رقم الوثيقة، التاريخ، المستخدم، البيان) |
| القواعد | الرقم والتاريخ والمستخدم مطلوبة (UC-002 A1)؛ التاريخ يجب أن يكون صالحًا (UC-002 A2)؛ البيان اختياري (قسم 6.7) |
| الأحداث | `OnHeaderChanged` إلى الصفحة — تحديث الرأس المؤقت في الجلسة |
| التحقق | عند الحفظ فقط (A1) + تحقق inline عند مغادرة الحقل إذا كان قد أُدخل فيه نص |
| Traceability | FR-01/FR-02، UC-002 A1/A2، NFR-04 |

## 4. AddDetailRow

| البند | المواصفة |
|---|---|
| الدور | سطر الإدخال الدائم أعلى الجدول — Product + Warehouse + الكمية + السعر (اختياري) + تاريخ الصلاحية (اختياري) + زر «+ إضافة» |
| القواعد | BR-01 (Product مطلوب)، BR-04 (Warehouse مطلوب)، BR-05 (Product+Warehouse معًا)، BR-02 (الكمية > 0)، BR-03 (التكرار مسموح) |
| التحقق | Sync فوري عند الضغط على «إضافة» — منع الإضافة عند أي فشل مع رسالة Inline على الحقل + Global |
| الأحداث | `OnAdd(detail)` → `BalanceValidationService.ValidateDetail` → إضافة للجلسة فقط (لا Repository) |
| سلوك المنسدلات | تُحمَّل أسماء Product/Warehouse مرة واحدة عند فتح الصفحة (Mock Data)؛ لا إعادة تحميل بعد كل إضافة (UC-004/005) |
| Traceability | FR-03/04/05، UC-003..005، BR-01..05 |

## 5. DetailsGrid

| البند | المواصفة |
|---|---|
| الدور | عرض كل السطور بأسماء Product/Warehouse (BR-06) — الجدول هو المخرج المرئي الرئيسي (UC-006) |
| الحالة الفارغة | رسالة «لا توجد تفاصيل — أضف صنفًا واحدًا على الأقل قبل الحفظ» (UC-006 A1/A3) |
| الأحداث | `OnEdit(detailId)` → تفعيل EditDetailRow للصف؛ `OnDelete(detailId)` → فتح ConfirmDialog |
| Traceability | FR-06، UC-006، BR-06 |

## 6. EditDetailRow

| البند | المواصفة |
|---|---|
| الدور | تحويل صف واحد إلى وضع تحرير بكل الحقول (Product، Warehouse، الكمية، السعر، تاريخ الصلاحية) |
| الأحداث | `OnUpdate(detail)` — بعد إعادة التحقق BR-01..05؛ `OnCancel` — إعادة البيانات الأصلية بلا أثر (UC-007 A3) |
| الفشل | رسالة «يرجى تصحيح البيانات» دون تحديث السطر (UC-007 A1/A2) |
| Traceability | FR-07، UC-007 A1/A2/A3 |

## 7. ConfirmDialog

| البند | المواصفة |
|---|---|
| الدور | تأكيد الحذف — نص حرفي: «هل تريد حذف هذا السطر؟» مع وصف السطر (الصنف، المخزن، الكمية) |
| السلوك | `OnConfirm` → حذف فوري + رسالة نجاح؛ `OnCancel` → إغلاق بلا تغيير (UC-008 A1) |
| حافة | إذا اختفى السطر بين فتح الـ Dialog والتأكيد (UC-008 A2) → رسالة مناسبة + إغلاق |
| Traceability | FR-08، UC-008 A1/A2 |

## 8. MessageBox / ValidationMessage (Global Message Area)

| البند | المواصفة |
|---|---|
| الدور | شريط الرسالة العالمية أعلى الصفحة — نجاح أخضر / خطأ أحمر |
| المحتوى الحرفي | «تم حفظ الأرصدة الافتتاحية بنجاح»، «تعذر حفظ الأرصدة الافتتاحية»، «يجب اختيار الصنف والمخزن والكمية»، «يجب إضافة صنف واحد على الأقل قبل الحفظ» |
| الربط | NFR-04 (رسائل واضحة)، NFR-06 (مؤشرات بارزة)، كل رسائل UC-002/003/006/008/009 |
| Traceability | NFR-04/NFR-06 — لا NFR-05 ولا NFR-01 هنا مباشرة |

## 9. SaveAction (زر الحفظ)

| البند | المواصفة |
|---|---|
| الدور | زر «حفظ الوثيقة» — الاستدعاء الوحيد لـ `OpeningBalanceService.SaveDocument()` |
| التحقق المسبق | تمرير الرأس + السطور لـ `BalanceValidationService.ValidateDocument` (بوابات A1..A3) قبل الاستدعاء |
| النتيجة | نجاح → رسالة J + SavedInMemory؛ فشل A4 → رسالة I + إبقاء الزر مفعّلًا لإعادة المحاولة |
| Traceability | FR-09، UC-009 A1..A4 |

## 10. خريطة الخدمات (كما في Architecture المعتمد)

| الخدمة | المسؤوليات | المستهلك |
|---|---|---|
| OpeningBalanceService | ValidateHeader، ValidateDocument، SaveDocument (InMemoryRepository) | OpeningBalancePage |
| BalanceValidationService | ValidateDetail (BR-01..05)، ValidateDocument (A1..A3) | AddDetailRow، EditDetailRow، SaveAction |
| InMemoryRepository | GetProducts، GetWarehouses، حفظ/استرجاع الجلسة | OpeningBalanceService |
