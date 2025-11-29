# Blazor GOV.UK Design System Components

> ⚠️ **ALPHA VERSION** - This project is in early development. APIs may change, and components may have bugs or incomplete features.

A Blazor component library that implements the [GOV.UK Design System](https://design-system.service.gov.uk/) for building government-style services using .NET Blazor.

## Installation

```bash
dotnet add package Blazor.DesignSystem.Components
```

Or via Package Manager:

```powershell
Install-Package Blazor.DesignSystem.Components
```

## Getting Started

### 1. Add the GOV.UK Frontend CSS

Add the bundled GOV.UK Frontend CSS to your `App.razor` or `_Host.cshtml`:

```html
<link rel="stylesheet" href="_content/Blazor.DesignSystem.Components/css/govuk-frontend.css" />
```

This CSS file is bundled with the package and includes all necessary GOV.UK Design System styles, fonts, and images.

Optionally, you can also include the accessibility JavaScript helper for focus management:

```html
<script src="_content/Blazor.DesignSystem.Components/js/govuk-accessibility.js"></script>
```

### 2. Add the namespace

Add the namespace to your `_Imports.razor`:

```razor
@using Blazor.DesignSystem.Components
```

### 3. Use the components

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

## Available Components

All 32 GOV.UK Design System components are implemented:

- **Form Components**: Button, Input, Textarea, Select, Checkboxes, Radios, Date Input, Character Count, File Upload, Password Input
- **Navigation**: Breadcrumbs, Pagination, Header, Back Link, Skip Link, Footer
- **Content**: Accordion, Tabs, Details, Table, Tag, Panel, Warning Text, Inset Text, Notification Banner, Phase Banner, Cookie Banner
- **Form Support**: Error Message, Error Summary, Fieldset, Hint, Label, Summary List, Task List

## Render Modes

Components support Blazor's render modes:

- **Static SSR**: Components render server-side HTML
- **Interactive Server**: Components use SignalR for interactivity
- **Interactive WebAssembly**: Components run in the browser

For interactive components (Accordion, Tabs, etc.), add `@rendermode InteractiveServer` or `@rendermode InteractiveWebAssembly` to your page.

## Accessibility

All components follow GOV.UK Design System accessibility standards:

- Proper ARIA attributes and roles
- Keyboard navigation support
- Screen reader announcements for dynamic content
- Error states with proper labelling
- Focus management

## Two-Way Binding

Most input components support `@bind-Value`:

```razor
<GovUkInput @bind-Value="myValue" />
<GovUkTextarea @bind-Value="myText" />
<GovUkSelect @bind-Value="selectedOption">...</GovUkSelect>
```

## Attribution

This project is a derivative work based on the [GOV.UK Design System](https://design-system.service.gov.uk/) and [govuk-frontend](https://github.com/alphagov/govuk-frontend) created by the Government Digital Service (GDS).

## License

MIT License - see [LICENSE.txt](https://github.com/oughnic/Blazor-Design-System/blob/main/LICENSE.txt) for details.

## Links

- [GitHub Repository](https://github.com/oughnic/Blazor-Design-System)
- [GOV.UK Design System](https://design-system.service.gov.uk/)
- [govuk-frontend GitHub](https://github.com/alphagov/govuk-frontend)
