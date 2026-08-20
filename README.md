# Opening Balance Management

> Feature لإدارة الأرصدة الافتتاحية للمخزون، مبنية باستخدام Blazor Web App وClean Architecture مع واجهة عربية RTL وMock Data.

---

## Technology Stack

<div align="center">

[![.NET 9](https://img.shields.io/badge/.NET-9.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)](https://dotnet.microsoft.com/)[![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-9.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)](https://learn.microsoft.com/aspnet/core/)[![Blazor](https://img.shields.io/badge/Blazor-Interactive%20Server-512BD4?style=for-the-badge&logo=blazor&logoColor=white)](https://learn.microsoft.com/aspnet/core/blazor/)[![CSharp](https://img.shields.io/badge/C%23-Programming%20Language-239120?style=for-the-badge&logo=csharp&logoColor=white)](https://learn.microsoft.com/dotnet/csharp/)

[![Razor](https://img.shields.io/badge/Razor-Components-512BD4?style=for-the-badge&logo=razor&logoColor=white)](https://learn.microsoft.com/aspnet/core/blazor/components/)[![Clean Architecture](https://img.shields.io/badge/Architecture-Clean%20Architecture-0F766E?style=for-the-badge)](https://learn.microsoft.com/dotnet/architecture/)[![Mock Data](https://img.shields.io/badge/Storage-Mock%20%2F%20In--Memory-F59E0B?style=for-the-badge)](#)[![Dependency Injection](https://img.shields.io/badge/Pattern-Dependency%20Injection-2563EB?style=for-the-badge)](https://learn.microsoft.com/aspnet/core/fundamentals/dependency-injection)

[![HTML5](https://img.shields.io/badge/HTML5-E34F26?style=for-the-badge&logo=html5&logoColor=white)](https://developer.mozilla.org/docs/Web/HTML)[![CSS3](https://img.shields.io/badge/CSS3-1572B6?style=for-the-badge&logo=css3&logoColor=white)](https://developer.mozilla.org/docs/Web/CSS)[![Arabic RTL](https://img.shields.io/badge/UI-Arabic%20RTL-166534?style=for-the-badge)](#)[![Responsive Design](https://img.shields.io/badge/UI-Responsive%20Design-7C3AED?style=for-the-badge)](#)[![Git](https://img.shields.io/badge/Version%20Control-Git-F05032?style=for-the-badge&logo=git&logoColor=white)](https://git-scm.com/)

</div>

| Technology | Usage in the Project |
| --- | --- |
| `.NET 9` | تشغيل وبناء Solution والمشاريع الأربعة. |
| `ASP.NET Core` | استضافة تطبيق الويب وإعداد Dependency Injection وRazor Components. |
| `Blazor Web App` | بناء الواجهة التفاعلية باستخدام Razor Components. |
| `Interactive Server` | تشغيل الصفحة بتفاعل Server دون إعادة تحميل كامل للصفحة. |
| `C#` | لغة البرمجة المستخدمة في Domain وApplication وInfrastructure وWeb. |
| `Clean Architecture` | فصل Domain وApplication وInfrastructure وPresentation. |
| `Mock Data / In-Memory` | توفير Products وWarehouses وحفظ الوثيقة مؤقتًا دون Database. |
| `HTML5 / CSS3` | بناء الواجهة وتصميم البطاقات والجداول والنوافذ التفاعلية. |
| `Arabic RTL` | دعم اللغة العربية واتجاه الكتابة من اليمين إلى اليسار. |
| `Responsive Design` | تكييف الواجهة مع Desktop وTablet وMobile. |
| `Git` | إدارة الفروع والتغييرات والتوثيق. |

---

## Screenshots

### Opening Balance Screen

![Opening Balance Screen](https://private-us-east-1.manuscdn.com/sessionFile/tfiWQjYFI1QGImvIYl2x3L/sandbox/sliBdgNSAQ0YF2u5swMkGu-images_1787214602800_na1fn_L2hvbWUvdWJ1bnR1L29wZW5pbmctYmFsYW5jZS1yZXBvL2RvY3Mvc2NyZWVuc2hvdHMvb3BlbmluZy1iYWxhbmNlLXNhdmUtc3VjY2Vzcw.webp?Expires=1787387599&Signature=MEUCIEd2NCGY~ymWlDYOVYfV20NKnvN-48AZp14D5w1NZ4Q8AiEAsAIKi2bAUrLM1gQECSIT9VRGNcHTzxfMj2u1uRtS7aA_&Key-Pair-Id=K1K5N5YNBUUMMN)

### Inline Editing

![Inline Editing](https://private-us-east-1.manuscdn.com/sessionFile/tfiWQjYFI1QGImvIYl2x3L/sandbox/sliBdgNSAQ0YF2u5swMkGu-images_1787214602800_na1fn_L2hvbWUvdWJ1bnR1L29wZW5pbmctYmFsYW5jZS1yZXBvL2RvY3Mvc2NyZWVuc2hvdHMvZWRpdGVkLWRldGFpbHM.webp?Expires=1787387599&Signature=MEUCIQCzaFnYkUznzJ-kgB1zAMoUlEz4nco2u6xm~hZMin9AKQIgTVuUpbsIVe5wiAuTjaiyLXWCcHKD9Akj2SUkMXgoR-M_&Key-Pair-Id=K1K5N5YNBUUMMN)

### Delete Confirmation

![Delete Confirmation](https://private-us-east-1.manuscdn.com/sessionFile/tfiWQjYFI1QGImvIYl2x3L/sandbox/sliBdgNSAQ0YF2u5swMkGu-images_1787214602800_na1fn_L2hvbWUvdWJ1bnR1L29wZW5pbmctYmFsYW5jZS1yZXBvL2RvY3Mvc2NyZWVuc2hvdHMvZGVsZXRlLWNvbmZpcm1hdGlvbg.webp?Expires=1787387599&Signature=MEUCIHV8a5g4OQ8dbNW8lbJSn~6p~hBC1SsyV~ueHek5g8~tAiEAnWGcucSw-SNlA8Kd6dw2dxykOz8nHTlD1bYpdtmeJVc_&Key-Pair-Id=K1K5N5YNBUUMMN)

### Empty State and Validation

![Empty State and Validation](https://private-us-east-1.manuscdn.com/sessionFile/tfiWQjYFI1QGImvIYl2x3L/sandbox/sliBdgNSAQ0YF2u5swMkGu-images_1787214602800_na1fn_L2hvbWUvdWJ1bnR1L29wZW5pbmctYmFsYW5jZS1yZXBvL2RvY3Mvc2NyZWVuc2hvdHMvZW1wdHktc3RhdGUtdmFsaWRhdGlvbg.webp?Expires=1787387599&Signature=MEUCIQCFXjPk~naV1Z-GbS0Zg3H-TISFX82E~TA5dQ8CHFXGKAIgIDcTJMnNdIcK5ni5ztcFKI5s6L01ftubaG4-ub~u3B0_&Key-Pair-Id=K1K5N5YNBUUMMN)

---

## Project Idea

**Opening Balance Management** هي Feature داخل نظام Inventory تهدف إلى تسجيل الكميات الأولية الموجودة في المخازن عند بداية تشغيل النظام أو عند تهيئة مخزون جديد.

تسمح الشاشة للمستخدم بإنشاء وثيقة Opening Balance تتكون من `Document Header` و`Opening Balance Details`. يحتوي الرأس على رقم الوثيقة والتاريخ والمستخدم والبيان، بينما تحتوي التفاصيل على Product وWarehouse وQuantity وPrice وExpiry Date عند الحاجة.

تستخدم النسخة الحالية `Mock Data / In-Memory Persistence` بدل قاعدة بيانات حقيقية. لذلك يستطيع المستخدم تجربة دورة العمل الأساسية كاملة، لكن البيانات لا تستمر بعد إيقاف التطبيق أو إعادة تشغيله.

```
Opening Balance Document
│
├── Document Header
│   ├── Document Number
│   ├── Document Date
│   ├── User Name
│   └── Description
│
└── Opening Balance Details
    ├── Product
    ├── Warehouse
    ├── Quantity
    ├── Price
    └── Expiry Date
```

---

## Project Architecture

المشروع مبني وفق **Clean Architecture** مع فصل واضح للمسؤوليات. تعتمد الطبقات الخارجية على Interfaces الطبقات الداخلية، بينما يبقى `Domain` مستقلًا عن تفاصيل Blazor وASP.NET Core وMock Data.

```mermaid
flowchart TB
    Web["OpeningBalance.Web\nPresentation Layer"]
    Infrastructure["OpeningBalance.Infrastructure\nMock / Persistence"]
    Application["OpeningBalance.Application\nInterfaces / Validation"]
    Domain["OpeningBalance.Domain\nEntities / Models"]

    Web --> Application
    Web --> Infrastructure
    Infrastructure --> Application
    Infrastructure --> Domain
    Application --> Domain
```

| Layer | Responsibility |
| --- | --- |
| `OpeningBalance.Domain` | تعريف Entities وModels الأساسية للوثيقة والتفاصيل وProducts وWarehouses. |
| `OpeningBalance.Application` | تعريف Service Contracts وتنفيذ Validation وقواعد التعامل مع الـ Feature. |
| `OpeningBalance.Infrastructure` | تنفيذ Service وتوفير Mock Data وIn-Memory Save. |
| `OpeningBalance.Web` | عرض الصفحة والتفاعل مع المستخدم وتسجيل الخدمات داخل Dependency Injection. |

---

## Complete Project Structure

```
OpeningBalance.sln
│
├── README.md
├── analysis.md
├── design-findings.md
├── runtime-findings.md
├── feature-integration-architecture.md
│
├── docs/
│   ├── screenshots/
│   │   ├── opening-balance-save-success.webp
│   │   ├── edited-details.webp
│   │   ├── delete-confirmation.webp
│   │   └── empty-state-validation.webp
│   └── design/
│       ├── 01-component-and-layered-architecture.md
│       ├── 02-component-ui-structure.md
│       └── design.md
│
└── src/
    ├── OpeningBalance.Domain/
    │   ├── Inventory/
    │   │   └── OpeningBalances/
    │   │       └── Entities/
    │   │           └── OpeningBalanceModels.cs
    │   ├── Class1.cs
    │   └── OpeningBalance.Domain.csproj
    │
    ├── OpeningBalance.Application/
    │   ├── Inventory/
    │   │   └── OpeningBalances/
    │   │       ├── Interfaces/
    │   │       │   └── IOpeningBalanceService.cs
    │   │       ├── Services/
    │   │       │   └── BalanceValidationService.cs
    │   │       ├── DTOs/
    │   │       └── Results/
    │   ├── Class1.cs
    │   └── OpeningBalance.Application.csproj
    │
    ├── OpeningBalance.Infrastructure/
    │   ├── Inventory/
    │   │   └── OpeningBalances/
    │   │       ├── Services/
    │   │       │   └── InMemoryOpeningBalanceService.cs
    │   │       ├── MockData/
    │   │       └── Persistence/
    │   ├── Class1.cs
    │   └── OpeningBalance.Infrastructure.csproj
    │
    └── OpeningBalance.Web/
        ├── Components/
        │   ├── App.razor
        │   ├── Routes.razor
        │   ├── _Imports.razor
        │   ├── Layout/
        │   │   ├── MainLayout.razor
        │   │   └── NavMenu.razor
        │   └── Pages/
        │       ├── Home.razor
        │       ├── Counter.razor
        │       ├── Weather.razor
        │       └── Error.razor
        ├── wwwroot/
        │   └── app.css
        ├── Program.cs
        ├── appsettings.json
        └── OpeningBalance.Web.csproj
```

### Feature-Oriented Structure

```
Inventory/
└── OpeningBalances/
    ├── Domain/
    │   └── Entities/
    ├── Application/
    │   ├── Interfaces/
    │   ├── Services/
    │   ├── DTOs/
    │   └── Results/
    ├── Infrastructure/
    │   ├── Services/
    │   ├── MockData/
    │   └── Persistence/
    └── Presentation/
        └── Home.razor
```

هذا التنظيم يجعل الـ Feature قابلة للنقل إلى نظام Inventory أكبر، ويسمح بإضافة Features أخرى مثل `PurchaseReceipt` أو `StockTransfer` دون خلط ملفاتها مع Opening Balances.

---

## Runtime Flow

```mermaid
sequenceDiagram
    participant User as User
    participant Page as Home.razor
    participant Contract as IOpeningBalanceService
    participant Service as InMemoryOpeningBalanceService
    participant Validation as BalanceValidationService

    User->>Page: Open Opening Balance Screen
    Page->>Contract: GetProductsAsync()
    Contract->>Service: Load Mock Products
    Service-->>Page: Products
    Page->>Contract: GetWarehousesAsync()
    Contract->>Service: Load Mock Warehouses
    Service-->>Page: Warehouses
    User->>Page: Add / Edit / Delete Details
    User->>Page: Save Document
    Page->>Contract: SaveAsync(Document)
    Service->>Validation: ValidateDocument(Document)
    Validation-->>Service: Success or Validation Errors
    Service-->>Page: OperationResult
    Page-->>User: Success or Error Message
```

---

## Main Feature Files

| File | Purpose |
| --- | --- |
| `OpeningBalanceModels.cs` | Domain entities and lookup records. |
| `IOpeningBalanceService.cs` | Application contract used by the Web layer. |
| `BalanceValidationService.cs` | Validation for document and detail data. |
| `InMemoryOpeningBalanceService.cs` | Mock Products وWarehouses وتنفيذ الحفظ المؤقت. |
| `Home.razor` | الصفحة الرئيسية ومنطق التفاعل مع المستخدم. |
| `Program.cs` | Composition Root وتسجيل `IOpeningBalanceService`. |
| `App.razor` | إعداد HTML root وArabic RTL وCSS المضمن الأساسي. |
| `app.css` | التنسيقات العامة للواجهة. |

---

## Implementation Boundary

هذا الملف يركز على التعريف العام للمشروع فقط: **اسم المهمة، التقنيات، الصور، فكرة المشروع، والمعمارية والهيكلية**. أما Functional Requirements وBusiness Rules وUse Cases وNon-Functional Requirements وDetailed Analysis فهي موثقة في ملفات التحليل والتصميم المخصصة لها، وليست جزءًا من هذا الملخص المختصر.

---

## References

- [ASP.NET Core](https://learn.microsoft.com/aspnet/core/)

- [Blazor](https://learn.microsoft.com/aspnet/core/blazor/)

- [ASP.NET Core Dependency Injection](https://learn.microsoft.com/aspnet/core/fundamentals/dependency-injection)

- [.NET Application Architecture](https://learn.microsoft.com/dotnet/architecture/)