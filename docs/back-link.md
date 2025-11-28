# Back Link Component

The back link component helps users navigate back to the previous page in a multi-page transaction.

## Basic Usage

```razor
@using Blazor.DesignSystem.Components

<GovUkBackLink Href="/previous-page" />
```

## Parameters

| Parameter | Type | Default | Description |
|-----------|------|---------|-------------|
| `Href` | `string` | `"#"` | The URL to link to |
| `Text` | `string` | `"Back"` | The link text |
| `CssClass` | `string?` | `null` | Additional CSS classes |
| `OnClick` | `EventCallback<MouseEventArgs>` | - | Callback for click events |
| `PreventDefault` | `bool` | `false` | Whether to prevent default navigation |

## Examples

### Default Back Link

```razor
<GovUkBackLink Href="/previous-page" />
```

Renders as: "Back" with a left-pointing arrow

### Custom Text

```razor
<GovUkBackLink Href="/previous-page" Text="Go back" />
```

### With Click Handler

```razor
@rendermode InteractiveServer

<GovUkBackLink 
    Href="#" 
    PreventDefault="true" 
    OnClick="HandleBackClick" />

@code {
    private void HandleBackClick()
    {
        // Custom navigation logic
        NavigationManager.NavigateTo("/my-previous-page");
    }
}
```

### Using Browser History

```razor
@inject IJSRuntime JS
@rendermode InteractiveServer

<GovUkBackLink 
    Href="#" 
    PreventDefault="true" 
    OnClick="GoBack" />

@code {
    private async Task GoBack()
    {
        await JS.InvokeVoidAsync("history.back");
    }
}
```

## Placement

The back link should be placed at the top of a page, before the `<h1>` heading. It is typically placed after the phase banner (if present) and before the main content.

```razor
<GovUkPhaseBanner Tag="Alpha">
    This is a new service – your <a class="govuk-link" href="/feedback">feedback</a> will help us improve it.
</GovUkPhaseBanner>

<GovUkBackLink Href="/previous-page" />

<h1 class="govuk-heading-xl">Page title</h1>
```

## Accessibility

- Uses a standard `<a>` element for proper keyboard navigation
- The visual arrow is rendered via CSS (no separate icon element)
- Clear, descriptive text helps screen reader users understand the navigation

## For AI Coding Agents

When implementing back link functionality:

1. Always place the back link at the top of the page content
2. For multi-step forms, link to the actual previous page URL
3. If using JavaScript navigation, set `PreventDefault="true"`
4. Do not use back links on the first page of a journey
5. Consider using NavigationManager for client-side navigation in Blazor

```razor
// Standard usage
<GovUkBackLink Href="/step-1" />

// With custom click handling
<GovUkBackLink 
    Href="#" 
    PreventDefault="true" 
    OnClick="HandleBack" />

@code {
    [Inject] private NavigationManager NavManager { get; set; } = default!;
    
    private void HandleBack()
    {
        NavManager.NavigateTo("/previous-step");
    }
}
```
