# Notification Banner Component

Use the notification banner to tell users about something they need to know, or have done.

## Basic Usage

```razor
@using Blazor.DesignSystem.Components

<GovUkNotificationBanner Title="Important">
    <p class="govuk-notification-banner__heading">
        You have 7 days left to submit your application
    </p>
</GovUkNotificationBanner>
```

## Parameters

| Parameter | Type | Default | Description |
|-----------|------|---------|-------------|
| `Title` | `string` | `"Important"` | Banner title |
| `TitleId` | `string` | Auto-generated GUID | ID for title element |
| `Type` | `string?` | `null` | Type: "success" or empty |
| `ChildContent` | `RenderFragment?` | `null` | Banner content |
| `CssClass` | `string?` | `null` | Additional CSS classes |
| `AdditionalAttributes` | `Dictionary<string, object>?` | `null` | Additional HTML attributes |

## Examples

### Standard Banner

```razor
<GovUkNotificationBanner Title="Important">
    <p class="govuk-notification-banner__heading">
        This service will be unavailable from 6pm to 8pm on Friday.
    </p>
</GovUkNotificationBanner>
```

### Success Banner

```razor
<GovUkNotificationBanner Title="Success" Type="success">
    <h3 class="govuk-notification-banner__heading">
        Your application has been submitted
    </h3>
    <p class="govuk-body">
        We've sent a confirmation email to <strong>john@example.com</strong>
    </p>
</GovUkNotificationBanner>
```

### With Link

```razor
<GovUkNotificationBanner Title="Important">
    <p class="govuk-notification-banner__heading">
        <a class="govuk-notification-banner__link" href="/documents">
            Upload your documents
        </a> to complete your application.
    </p>
</GovUkNotificationBanner>
```

### Custom Title

```razor
<GovUkNotificationBanner Title="Update available">
    <p class="govuk-notification-banner__heading">
        A new version of this service is available
    </p>
</GovUkNotificationBanner>
```

## Banner Types

| Type | Use Case | ARIA Role |
|------|----------|-----------|
| (default) | General information | `region` |
| `success` | Confirmations | `alert` |

## Accessibility

- Standard banners use `role="region"` - read in document order
- Success banners use `role="alert"` - announced immediately
- `aria-labelledby` links to the title

## For AI Coding Agents

When implementing notification banners:

1. Use standard banners for general information
2. Use `Type="success"` for confirmations after actions
3. Success banners interrupt screen readers - use sparingly
4. Place at the top of the main content area

```razor
// Information banner
<GovUkNotificationBanner Title="Important">
    <p class="govuk-notification-banner__heading">
        Important message here.
    </p>
</GovUkNotificationBanner>

// Success banner (after form submission)
<GovUkNotificationBanner Title="Success" Type="success">
    <h3 class="govuk-notification-banner__heading">
        Action completed successfully
    </h3>
</GovUkNotificationBanner>
```
