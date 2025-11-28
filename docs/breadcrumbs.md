# Breadcrumbs Component

The breadcrumbs component helps users understand where they are within a website and navigate between levels.

## Components

| Component | Description |
|-----------|-------------|
| `GovUkBreadcrumbs` | The main breadcrumb container |
| `GovUkBreadcrumbsItem` | Individual breadcrumb items |

## Basic Usage

```razor
@using Blazor.DesignSystem.Components

<GovUkBreadcrumbs>
    <GovUkBreadcrumbsItem Href="/" Text="Home" />
    <GovUkBreadcrumbsItem Href="/section" Text="Section" />
    <GovUkBreadcrumbsItem Text="Current page" IsCurrentPage="true" />
</GovUkBreadcrumbs>
```

## GovUkBreadcrumbs Parameters

| Parameter | Type | Default | Description |
|-----------|------|---------|-------------|
| `CssClass` | `string?` | `null` | Additional CSS classes |
| `ChildContent` | `RenderFragment?` | `null` | The breadcrumb items |

## GovUkBreadcrumbsItem Parameters

| Parameter | Type | Default | Description |
|-----------|------|---------|-------------|
| `Href` | `string?` | `null` | The URL to link to |
| `Text` | `string` | **Required** | The text to display |
| `IsCurrentPage` | `bool` | `false` | Whether this is the current page |

## Examples

### Simple Breadcrumb Trail

```razor
<GovUkBreadcrumbs>
    <GovUkBreadcrumbsItem Href="/" Text="Home" />
    <GovUkBreadcrumbsItem Href="/services" Text="Services" />
    <GovUkBreadcrumbsItem Href="/services/apply" Text="Apply" />
    <GovUkBreadcrumbsItem Text="Your details" IsCurrentPage="true" />
</GovUkBreadcrumbs>
```

### Current Page without Link

When `IsCurrentPage="true"`, the item renders as plain text without a link:

```razor
<GovUkBreadcrumbs>
    <GovUkBreadcrumbsItem Href="/" Text="Home" />
    <GovUkBreadcrumbsItem Text="About us" IsCurrentPage="true" />
</GovUkBreadcrumbs>
```

### Dynamic Breadcrumbs

```razor
@code {
    private List<BreadcrumbItem> breadcrumbs = new()
    {
        new("Home", "/"),
        new("Products", "/products"),
        new("Electronics", "/products/electronics"),
        new("Laptops", null, true)
    };
    
    public record BreadcrumbItem(string Text, string? Href = null, bool IsCurrent = false);
}

<GovUkBreadcrumbs>
    @foreach (var crumb in breadcrumbs)
    {
        <GovUkBreadcrumbsItem 
            Text="@crumb.Text" 
            Href="@crumb.Href" 
            IsCurrentPage="@crumb.IsCurrent" />
    }
</GovUkBreadcrumbs>
```

## Placement

Breadcrumbs should be placed at the top of a page, usually after the header and before the main page heading.

```razor
<GovUkHeader>...</GovUkHeader>

<div class="govuk-width-container">
    <GovUkBreadcrumbs>
        <GovUkBreadcrumbsItem Href="/" Text="Home" />
        <GovUkBreadcrumbsItem Text="Current page" IsCurrentPage="true" />
    </GovUkBreadcrumbs>
    
    <main class="govuk-main-wrapper">
        <h1 class="govuk-heading-xl">Page title</h1>
        ...
    </main>
</div>
```

## When Not to Use

- Do not use breadcrumbs for linear journeys (multi-step forms) - use a back link instead
- Do not use on the homepage
- Do not use if users cannot navigate between levels

## Accessibility

- Uses `<nav>` element with `aria-label="Breadcrumb"`
- Current page is marked with `aria-current="page"`
- Uses an ordered list (`<ol>`) for proper semantic structure

## For AI Coding Agents

When implementing breadcrumbs:

1. Always include a "Home" link as the first item
2. Mark the last item as `IsCurrentPage="true"` if it represents the current page
3. Current page items should not have an `Href`
4. Keep breadcrumb trails short and meaningful
5. Use consistent naming that matches page titles

```razor
// Standard pattern
<GovUkBreadcrumbs>
    <GovUkBreadcrumbsItem Href="/" Text="Home" />
    <GovUkBreadcrumbsItem Href="/parent" Text="Parent page" />
    <GovUkBreadcrumbsItem Text="Current page" IsCurrentPage="true" />
</GovUkBreadcrumbs>
```

Note: Do not use breadcrumbs and back links together - choose one or the other based on the navigation pattern.
