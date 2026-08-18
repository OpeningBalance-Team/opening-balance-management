# 05 — Wireframes

| البند | القيمة |
|---|---|
| المرجع الأول | Information Architecture (وثيقة 04) |
| المرجع الثاني | مخطط Component/UI Structure + ER Model (أسماء الحقول والكيانات) |
| النطاق | شاشة واحدة: Opening Balance Main Screen (شاشة wireframe محايدة الألوان) |
| الإعداد | Manus AI |

---

## 1. المنهجية

Wireframe واحد دقيق يمثل الشاشة بكامل مناطقها الخمس، مرسومًا RTL بالعربية، بأسماء الحقول حرفية كما في قسم 6.7 وفي نموذج البيانات (Product / Warehouse / الكمية / السعر / تاريخ الصلاحية). الأسفل: صور States منفصلة لكل حالة مهمة — راجع وثيقة 06.

## 2. Wireframe — الشاشة الرئيسية

تُستخرج حالة Wireframe الأساسية من حالة **A — Empty / Initial** (الشاشة الأولية الفارغة قبل أي إدخال) لأنها تمثل نقطة البداية الوحيدة التي يمر بها كل مستخدم، وتُكملها حالات التصميم في وثيقة 06.

![Wireframe — الحالة الأولية A](https://private-us-east-1.manuscdn.com/sessionFile/QdUpTOsiTgU3175gRn90PJ/sandbox/Z6jZjNfTgsUoy2AShnTKKq-images_1787088265986_na1fn_L2hvbWUvdWJ1bnR1L2RvY3MvdXgvYXNzZXRzL3N0YXRlLWE.png?Policy=eyJTdGF0ZW1lbnQiOlt7IlJlc291cmNlIjoiaHR0cHM6Ly9wcml2YXRlLXVzLWVhc3QtMS5tYW51c2Nkbi5jb20vc2Vzc2lvbkZpbGUvUWRVcFRPc2lUZ1UzMTc1Z1JuOTBQSi9zYW5kYm94L1o2alpqTmZUZ3NVb3kyQVNoblRLS3EtaW1hZ2VzXzE3ODcwODgyNjU5ODZfbmExZm5fTDJodmJXVXZkV0oxYm5SMUwyUnZZM012ZFhndllYTnpaWFJ6TDNOMFlYUmxMV0UucG5nIiwiQ29uZGl0aW9uIjp7IkRhdGVMZXNzVGhhbiI6eyJBV1M6RXBvY2hUaW1lIjoxNzg5NDMwNDAwfX19XX0_&Key-Pair-Id=K2QY5QTL8JSY6C&Signature=MEYCIQDEyV8Ymm~Z9dgyhyGZ5cEXDB1oAf36oh4ytUWpsvLdCAIhAPLZVVHca1KaXYyNUkl-xX7HoRlwAbxnIoG7vPGnoJFE)

**التكوين:**

- **الشريط العلوي (Global Message):** منطقة رسالة ثابتة تظهر رسائل النجاح/الخطأ العالمية (NFR-04) — في هذا الـ wireframe تظهر فيها رسالة خطأ عامة.
- **منطقة الرأس:** أربعة حقول في صف واحد: رقم الوثيقة (مطلوب)، التاريخ (مطلوب)، المستخدم (مطلوب)، البيان (اختياري — موسوم بذلك).
- **سطر الإدخال:** المنسدلتان (Product، Warehouse) + الكمية (مطلوبة) + السعر (اختياري) + تاريخ الصلاحية (اختياري) + زر «+ إضافة» بارز.
- **الجدول:** الأعمدة بالترتيب RTL: الصنف، المخزن، الكمية، السعر، تاريخ الصلاحية، الإجراءات (تعديل / حذف). الصف الأول يمثل مثال التحليل: Laptop HP، المخزن الرئيسي، 50، 500، بدون صلاحية (قسم 6.7).
- **شريط الإجراءات:** زر «حفظ» كبير في الشريط السفلي الثابت.

## 3. ملاحظات التنفيذ المباشر إلى Blazor

| عنصر Wireframe | المقابل في Blazor |
|---|---|
| صف الرأس | `HeaderForm` بحقول string/date |
| سطر الإدخال | `AddDetailRow` بـ `ProductDropdown` و`WarehouseDropdown` |
| الجدول | `DetailsGrid` بـ `EditDetailRow` عند تفعيل صف + `ConfirmDialog` عند الحذف |
| شريط الرسائل | `MessageBox` يقرأ `ValidationMessage` من نتيجة الخدمات |
| زر الحفظ | `SaveAction` يستدعي `OpeningBalanceService.SaveDocument` |

لا يوجد في الـ Wireframe أي عنصر خارج التحليل: لا بحث، لا ترقيم صفحات، لا تصدير، لا حالة «اعتماد» مرئية (المفهوم يتيم في التحليل — معالَج في وثيقة 09).
