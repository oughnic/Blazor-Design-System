# Blazor GOV.UK Design System

[![NuGet Version](https://img.shields.io/nuget/v/Blazor.DesignSystem.Components)](https://www.nuget.org/packages/Blazor.DesignSystem.Components)
[![NuGet Downloads](https://img.shields.io/nuget/dt/Blazor.DesignSystem.Components)](https://www.nuget.org/packages/Blazor.DesignSystem.Components)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

> ⚠️ **ALPHA VERSION** - This project is in early development. APIs may change, and components may have bugs or incomplete features. Use at your own risk in production environments.

A Blazor component library that implements the [GOV.UK Design System](https://design-system.service.gov.uk/) for building government-style services using .NET Blazor.

## 🤖 AI-Generated Project

This project was generated with the assistance of AI tools. While the code has been reviewed and tested, please report any issues you encounter.

## 📜 Attribution and Credits

This project is a **derivative work** based on the [GOV.UK Design System](https://design-system.service.gov.uk/) and [govuk-frontend](https://github.com/alphagov/govuk-frontend) created by the [Government Digital Service (GDS)](https://www.gov.uk/government/organisations/government-digital-service).

### Original Projects

- **GOV.UK Design System**: https://design-system.service.gov.uk/
- **govuk-frontend**: https://github.com/alphagov/govuk-frontend
- **GOV.UK Frontend Docs**: https://frontend.design-system.service.gov.uk/

### Credits

This project would not be possible without the excellent work of:

- The **Government Digital Service (GDS)** team at the UK Government
- All contributors to the [govuk-frontend](https://github.com/alphagov/govuk-frontend/graphs/contributors) repository
- The designers and researchers who created the original GOV.UK Design System patterns

The original GOV.UK Design System and govuk-frontend are licensed under the [MIT License](https://github.com/alphagov/govuk-frontend/blob/main/LICENSE.txt).

## 🚀 Getting Started

### Prerequisites

- .NET 10.0 SDK or later
- A Blazor Web App project

### Installation

Install the NuGet package:

```bash
dotnet add package Blazor.DesignSystem.Components
```

Or via Package Manager Console:

```powershell
Install-Package Blazor.DesignSystem.Components
```

Or add directly to your project file:

```xml
<PackageReference Include="Blazor.DesignSystem.Components" Version="1.0.0-alpha.1" />
```

For local development, you can also add a project reference:

```xml
<ProjectReference Include="..\Blazor.DesignSystem.Components\Blazor.DesignSystem.Components.csproj" />
```

### Setup

1. Add the GOV.UK Frontend CSS to your `App.razor` or `_Host.cshtml`:

```html
<link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/govuk-frontend@5.4.0/dist/govuk/govuk-frontend.min.css">
```

2. Add the namespace to your `_Imports.razor`:

```razor
@using Blazor.DesignSystem.Components
```

### Basic Usage

```razor
@using Blazor.DesignSystem.Components

<GovUkButton Text="Save and continue" OnClick="HandleClick" />

<GovUkInput 
    Id="email" 
    Label="Email address" 
    Hint="We'll use this to contact you"
    @bind-Value="email" />

@code {
    private string email = "";
    
    private void HandleClick()
    {
        // Handle button click
    }
}
```

## 📦 Available Components

All 32 GOV.UK Design System components are implemented:

| Component | Documentation |
|-----------|---------------|
| Accordion | [docs/accordion.md](docs/accordion.md) |
| Back link | [docs/back-link.md](docs/back-link.md) |
| Breadcrumbs | [docs/breadcrumbs.md](docs/breadcrumbs.md) |
| Button | [docs/button.md](docs/button.md) |
| Character count | [docs/character-count.md](docs/character-count.md) |
| Checkboxes | [docs/checkboxes.md](docs/checkboxes.md) |
| Cookie banner | [docs/cookie-banner.md](docs/cookie-banner.md) |
| Date input | [docs/date-input.md](docs/date-input.md) |
| Details | [docs/details.md](docs/details.md) |
| Error message | [docs/error-message.md](docs/error-message.md) |
| Error summary | [docs/error-summary.md](docs/error-summary.md) |
| Fieldset | [docs/fieldset.md](docs/fieldset.md) |
| File upload | [docs/file-upload.md](docs/file-upload.md) |
| Footer | [docs/footer.md](docs/footer.md) |
| Header | [docs/header.md](docs/header.md) |
| Hint | [docs/hint.md](docs/hint.md) |
| Input | [docs/input.md](docs/input.md) |
| Inset text | [docs/inset-text.md](docs/inset-text.md) |
| Label | [docs/label.md](docs/label.md) |
| Notification banner | [docs/notification-banner.md](docs/notification-banner.md) |
| Pagination | [docs/pagination.md](docs/pagination.md) |
| Panel | [docs/panel.md](docs/panel.md) |
| Phase banner | [docs/phase-banner.md](docs/phase-banner.md) |
| Radios | [docs/radios.md](docs/radios.md) |
| Select | [docs/select.md](docs/select.md) |
| Skip link | [docs/skip-link.md](docs/skip-link.md) |
| Summary list | [docs/summary-list.md](docs/summary-list.md) |
| Table | [docs/table.md](docs/table.md) |
| Tabs | [docs/tabs.md](docs/tabs.md) |
| Tag | [docs/tag.md](docs/tag.md) |
| Textarea | [docs/textarea.md](docs/textarea.md) |
| Warning text | [docs/warning-text.md](docs/warning-text.md) |

## 🏗️ Project Structure

```
Blazor.DesignSystem/
├── Blazor.DesignSystem.Components/    # Component library
│   ├── GovUkAccordion.razor
│   ├── GovUkButton.razor
│   ├── GovUkInput.razor
│   └── ... (all components)
├── Blazor.DesignSystem.Web/           # Documentation/demo site
│   └── Components/Pages/              # Demo pages for each component
├── docs/                              # Component documentation
└── Planning/                          # Project planning documents
```

## ♿ Accessibility

All components have been designed with accessibility in mind, following the GOV.UK Design System's accessibility standards:

- Proper ARIA attributes and roles
- Keyboard navigation support
- Screen reader announcements for dynamic content
- Error states with proper labelling
- Focus management

See the [Progress.md](Progress.md) file for a detailed breakdown of accessibility features implemented in each component.

## 🔄 Render Modes

Components support Blazor's render modes:

- **Static SSR**: Components render server-side HTML
- **Interactive Server**: Components use SignalR for interactivity
- **Interactive WebAssembly**: Components run in the browser

For interactive components (Accordion, Tabs, etc.), add `@rendermode InteractiveServer` or `@rendermode InteractiveWebAssembly` to your page.

## 📋 For AI Coding Agents

If you are an AI coding agent working with this library, here are key points:

### Adding Components to a Page

1. Ensure `@using Blazor.DesignSystem.Components` is in `_Imports.razor` or the page
2. Use the `GovUk` prefix for all components (e.g., `GovUkButton`, `GovUkInput`)
3. For interactive components, add `@rendermode InteractiveServer` to the page directive

### Common Patterns

```razor
@* Form with validation *@
<GovUkInput 
    Id="unique-id"
    Label="Field label"
    Hint="Optional hint text"
    HasError="@hasError"
    ErrorMessage="@errorMessage"
    @bind-Value="fieldValue" />

@* Button with click handler *@
<GovUkButton Text="Submit" OnClick="HandleSubmit" />

@* Checkbox group *@
<GovUkCheckboxes Name="options" Legend="Select options">
    <GovUkCheckboxItem Value="option1" Label="Option 1" />
    <GovUkCheckboxItem Value="option2" Label="Option 2" />
</GovUkCheckboxes>
```

### Component Naming Convention

- Components use PascalCase: `GovUkButton`, `GovUkErrorSummary`
- Child components follow parent naming: `GovUkAccordionSection`, `GovUkBreadcrumbsItem`

### Two-Way Binding

Most input components support `@bind-Value`:

```razor
<GovUkInput @bind-Value="myValue" />
<GovUkTextarea @bind-Value="myText" />
<GovUkSelect @bind-Value="selectedOption">...</GovUkSelect>
```

## 🤝 Contributing

Contributions are welcome! Please:

1. Check existing issues before creating new ones
2. Follow the existing code style
3. Test your changes with the demo site
4. Update documentation for any new features

## 📄 License

This project is licensed under the MIT License - see the [LICENSE.txt](LICENSE.txt) file for details.

This is consistent with the original [govuk-frontend](https://github.com/alphagov/govuk-frontend) which is also MIT licensed.

## 🔗 Links

- [GOV.UK Design System](https://design-system.service.gov.uk/)
- [govuk-frontend GitHub](https://github.com/alphagov/govuk-frontend)
- [Blazor Documentation](https://learn.microsoft.com/aspnet/core/blazor/)
