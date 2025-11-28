# Header Component

The header shows users which service they are using and lets them navigate around.

## Components

| Component | Description |
|-----------|-------------|
| `GovUkHeader` | The main header container |
| `GovUkHeaderNavItem` | Navigation items in the header |

## Basic Usage

```razor
@using Blazor.DesignSystem.Components

<GovUkHeader OrganisationName="GOV.UK" HomepageUrl="/" />
```

## Parameters

| Parameter | Type | Default | Description |
|-----------|------|---------|-------------|
| `HomepageUrl` | `string` | `"/"` | URL for the homepage link |
| `OrganisationName` | `string` | `"GOV.UK"` | Organisation name in header |
| `ProductName` | `string?` | `null` | Product name after logo |
| `LogoContent` | `string?` | `null` | Custom logo HTML |
| `Navigation` | `RenderFragment?` | `null` | Navigation items |
| `NavigationLabel` | `string` | `"Menu"` | Aria-label for navigation |
| `ShowMenuButton` | `bool` | `true` | Show mobile menu button |
| `ContainerClass` | `string` | `"govuk-width-container"` | Container CSS class |
| `CssClass` | `string?` | `null` | Additional CSS classes |
| `AdditionalAttributes` | `Dictionary<string, object>?` | `null` | Additional HTML attributes |

## GovUkHeaderNavItem Parameters

| Parameter | Type | Default | Description |
|-----------|------|---------|-------------|
| `Href` | `string?` | `null` | URL for the navigation item |
| `Text` | `string?` | `null` | Navigation item text |
| `ChildContent` | `RenderFragment?` | `null` | Custom content (overrides Text) |
| `IsActive` | `bool` | `false` | Whether this is the current page |
| `AdditionalAttributes` | `Dictionary<string, object>?` | `null` | Additional HTML attributes |

## Examples

### Basic Header

```razor
<GovUkHeader OrganisationName="GOV.UK" />
```

### With Product Name

```razor
<GovUkHeader 
    OrganisationName="GOV.UK" 
    ProductName="Design System" />
```

### With Custom Organisation Name

```razor
<GovUkHeader 
    OrganisationName="My Department" 
    ProductName="My Service" />
```

### With Navigation

```razor
<GovUkHeader OrganisationName="GOV.UK" ProductName="Service">
    <Navigation>
        <GovUkHeaderNavItem Href="/" Text="Home" />
        <GovUkHeaderNavItem Href="/about" Text="About" />
        <GovUkHeaderNavItem Href="/contact" Text="Contact" />
    </Navigation>
</GovUkHeader>
```

### With Active Navigation Item

```razor
<GovUkHeader OrganisationName="GOV.UK" ProductName="Service">
    <Navigation>
        <GovUkHeaderNavItem Href="/" Text="Home" />
        <GovUkHeaderNavItem Href="/about" Text="About" IsActive="true" />
        <GovUkHeaderNavItem Href="/contact" Text="Contact" />
    </Navigation>
</GovUkHeader>
```

### With Custom Navigation Label

```razor
<GovUkHeader 
    OrganisationName="GOV.UK" 
    NavigationLabel="Service navigation">
    <Navigation>
        <GovUkHeaderNavItem Href="/dashboard" Text="Dashboard" />
        <GovUkHeaderNavItem Href="/settings" Text="Settings" />
    </Navigation>
</GovUkHeader>
```

### With Custom Logo

```razor
<GovUkHeader 
    LogoContent="<svg width='100' height='30'><text>LOGO</text></svg>"
    ProductName="My Service" />
```

### Complete Page Layout

```razor
<GovUkSkipLink />

<GovUkHeader 
    OrganisationName="GOV.UK" 
    ProductName="Apply for a licence">
    <Navigation>
        <GovUkHeaderNavItem Href="/" Text="Home" />
        <GovUkHeaderNavItem Href="/applications" Text="Applications" IsActive="true" />
        <GovUkHeaderNavItem Href="/help" Text="Help" />
    </Navigation>
</GovUkHeader>

<div class="govuk-width-container">
    <GovUkPhaseBanner Tag="Beta">
        This is a new service – <a class="govuk-link" href="/feedback">give feedback</a> to help us improve.
    </GovUkPhaseBanner>
    
    <main class="govuk-main-wrapper" id="main-content">
        @* Page content *@
    </main>
</div>

<GovUkFooter />
```

### Dynamic Active State

```razor
@inject NavigationManager NavigationManager

<GovUkHeader OrganisationName="GOV.UK">
    <Navigation>
        <GovUkHeaderNavItem 
            Href="/" 
            Text="Home" 
            IsActive="@IsActivePage("/")" />
        <GovUkHeaderNavItem 
            Href="/about" 
            Text="About" 
            IsActive="@IsActivePage("/about")" />
    </Navigation>
</GovUkHeader>

@code {
    private bool IsActivePage(string path)
    {
        return NavigationManager.Uri.EndsWith(path) || 
               (path == "/" && NavigationManager.Uri.EndsWith(NavigationManager.BaseUri));
    }
}
```

## Accessibility

- Uses `<header>` element with `role="banner"`
- Navigation uses `aria-label` for screen readers
- Mobile menu button has appropriate ARIA attributes
- Logo link goes to homepage

## For AI Coding Agents

When implementing headers:

1. Include skip link before the header
2. Set appropriate `OrganisationName` for your service
3. Use `ProductName` for the specific service name
4. Use `IsActive="true"` to highlight the current page
5. Consider mobile navigation requirements

```razor
// Minimal header
<GovUkHeader OrganisationName="GOV.UK" />

// Service header with navigation
<GovUkHeader 
    OrganisationName="GOV.UK" 
    ProductName="My Service"
    HomepageUrl="/">
    <Navigation>
        <GovUkHeaderNavItem Href="/" Text="Home" />
        <GovUkHeaderNavItem Href="/section" Text="Section" IsActive="true" />
    </Navigation>
</GovUkHeader>

// Complete page structure
<GovUkSkipLink />
<GovUkHeader OrganisationName="GOV.UK" ProductName="Service">
    <Navigation>
        <GovUkHeaderNavItem Href="/" Text="Home" />
    </Navigation>
</GovUkHeader>

<div class="govuk-width-container">
    <GovUkPhaseBanner Tag="Alpha">
        This is a new service.
    </GovUkPhaseBanner>
    
    <main class="govuk-main-wrapper" id="main-content">
        @Body
    </main>
</div>

<GovUkFooter />
```
