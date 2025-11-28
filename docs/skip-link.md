# Skip Link Component

Use the skip link component to help keyboard-only users skip to the main content.

## Basic Usage

```razor
@using Blazor.DesignSystem.Components

<GovUkSkipLink />
```

## Parameters

| Parameter | Type | Default | Description |
|-----------|------|---------|-------------|
| `Href` | `string` | `"#main-content"` | Target anchor |
| `Text` | `string` | `"Skip to main content"` | Link text |
| `CssClass` | `string?` | `null` | Additional CSS classes |
| `AdditionalAttributes` | `Dictionary<string, object>?` | `null` | Additional HTML attributes |

## Examples

### Default Skip Link

```razor
<GovUkSkipLink />
```

### Custom Target

```razor
<GovUkSkipLink Href="#content" />
```

### Custom Text

```razor
<GovUkSkipLink Text="Skip navigation" />
```

## Placement

Place the skip link as the first focusable element on the page:

```razor
<body>
    <GovUkSkipLink />
    <GovUkHeader />
    
    <div class="govuk-width-container">
        <main class="govuk-main-wrapper" id="main-content">
            @* Main content here *@
        </main>
    </div>
    
    <GovUkFooter />
</body>
```

## Main Content Target

Ensure your main content has the matching ID:

```razor
<main class="govuk-main-wrapper" id="main-content" role="main">
    @* Page content *@
</main>
```

## Behavior

- Hidden by default (off-screen)
- Becomes visible when focused via keyboard (Tab)
- Clicking or pressing Enter skips to main content
- Returns to hidden state after use

## Accessibility

- Essential for keyboard navigation
- Allows users to bypass repetitive content (header, navigation)
- Uses standard anchor link behavior
- Must be the first focusable element

## For AI Coding Agents

When implementing skip links:

1. Place as first element inside `<body>`
2. Before header and navigation
3. Ensure `id="main-content"` exists on main element
4. Default values work for most cases
5. Test with keyboard navigation

```razor
// Standard page structure
<body>
    <GovUkSkipLink />
    
    <GovUkHeader OrganisationName="GOV.UK" />
    
    <div class="govuk-width-container">
        <GovUkPhaseBanner Tag="Alpha">...</GovUkPhaseBanner>
        
        <main class="govuk-main-wrapper" id="main-content">
            <h1 class="govuk-heading-xl">Page title</h1>
            @* Content *@
        </main>
    </div>
    
    <GovUkFooter />
</body>
```
