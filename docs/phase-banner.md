# Phase Banner Component

Use the phase banner to show users your service is still being worked on.

## Basic Usage

```razor
@using Blazor.DesignSystem.Components

<GovUkPhaseBanner Tag="Alpha">
    This is a new service – your <a class="govuk-link" href="/feedback">feedback</a> will help us to improve it.
</GovUkPhaseBanner>
```

## Parameters

| Parameter | Type | Default | Description |
|-----------|------|---------|-------------|
| `Tag` | `string` | `"Beta"` | Phase tag text (Alpha, Beta, etc.) |
| `ChildContent` | `RenderFragment?` | `null` | Banner text content |
| `CssClass` | `string?` | `null` | Additional CSS classes |
| `AdditionalAttributes` | `Dictionary<string, object>?` | `null` | Additional HTML attributes |

## Examples

### Alpha Phase

```razor
<GovUkPhaseBanner Tag="Alpha">
    This is a new service – your <a class="govuk-link" href="/feedback">feedback</a> will help us to improve it.
</GovUkPhaseBanner>
```

### Beta Phase

```razor
<GovUkPhaseBanner Tag="Beta">
    This is a new service – your <a class="govuk-link" href="/feedback">feedback</a> will help us to improve it.
</GovUkPhaseBanner>
```

### Live (Public Beta)

```razor
<GovUkPhaseBanner Tag="Beta">
    This is a public beta. <a class="govuk-link" href="/help">Get help</a> if you need it.
</GovUkPhaseBanner>
```

## Placement

Place the phase banner after the header and before the main content:

```razor
<GovUkHeader OrganisationName="GOV.UK" ProductName="Service" />

<div class="govuk-width-container">
    <GovUkPhaseBanner Tag="Alpha">
        This is a new service – <a class="govuk-link" href="/feedback">give feedback</a>.
    </GovUkPhaseBanner>
    
    <main class="govuk-main-wrapper" id="main-content">
        @* Page content *@
    </main>
</div>
```

## Phase Definitions

| Phase | Description |
|-------|-------------|
| Alpha | Testing with a small group, expect issues |
| Beta | Available to the public, still being improved |
| Live | Service is fully operational (remove banner) |

## Accessibility

- Uses standard paragraph and strong elements
- Tag is styled as a GOV.UK tag component
- Feedback link should be keyboard accessible

## For AI Coding Agents

When implementing phase banners:

1. Use "Alpha" for early testing phase
2. Use "Beta" when publicly available but not final
3. Always include a feedback link
4. Place after header, before main content
5. Remove when service is fully live

```razor
// Standard placement
<GovUkHeader OrganisationName="GOV.UK" ProductName="My Service" />

<div class="govuk-width-container">
    <GovUkPhaseBanner Tag="@phase">
        This is a new service – your <a class="govuk-link" href="/feedback">feedback</a> will help us improve it.
    </GovUkPhaseBanner>
    
    <main class="govuk-main-wrapper" id="main-content">
        @Body
    </main>
</div>

@code {
    private string phase = "Alpha"; // or "Beta"
}
```
