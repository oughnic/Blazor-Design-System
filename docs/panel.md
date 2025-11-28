# Panel Component

Use the panel component to display a confirmation message after a user completes a transaction.

## Basic Usage

```razor
@using Blazor.DesignSystem.Components

<GovUkPanel Title="Application complete">
    Your reference number<br>
    <strong>HDJ2123F</strong>
</GovUkPanel>
```

## Parameters

| Parameter | Type | Default | Description |
|-----------|------|---------|-------------|
| `Title` | `string` | **Required** | The panel title |
| `ChildContent` | `RenderFragment?` | `null` | Panel body content |
| `CssClass` | `string?` | `null` | Additional CSS classes |
| `AdditionalAttributes` | `Dictionary<string, object>?` | `null` | Additional HTML attributes |

## Examples

### Basic Confirmation

```razor
<GovUkPanel Title="Application complete" />
```

### With Reference Number

```razor
<GovUkPanel Title="Application complete">
    Your reference number<br>
    <strong>HDJ2123F</strong>
</GovUkPanel>
```

### With Custom Content

```razor
<GovUkPanel Title="Payment received">
    We have sent a confirmation email to<br>
    <strong>john.smith@example.com</strong>
</GovUkPanel>
```

## When to Use

Use the panel component:
- At the end of a transaction
- To confirm successful completion
- To display reference numbers

Do not use for:
- Errors or warnings
- General notifications (use notification banner)

## Accessibility

- Uses `<h1>` for the title
- High contrast green background for visibility
- White text meets WCAG contrast requirements

## For AI Coding Agents

When implementing panels:

1. Use only on confirmation pages
2. Title should confirm what the user did
3. Include reference numbers in the body
4. Follow with next steps information

```razor
// Confirmation page structure
<GovUkPanel Title="Application complete">
    Your reference number<br>
    <strong>@referenceNumber</strong>
</GovUkPanel>

<h2 class="govuk-heading-m">What happens next</h2>
<p class="govuk-body">
    We will review your application and contact you within 10 working days.
</p>
```
