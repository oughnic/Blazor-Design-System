# Warning Text Component

Use the warning text component when you need to warn users about something important.

## Basic Usage

```razor
@using Blazor.DesignSystem.Components

<GovUkWarningText>
    You can be fined up to £5,000 if you do not register.
</GovUkWarningText>
```

## Parameters

| Parameter | Type | Default | Description |
|-----------|------|---------|-------------|
| `ChildContent` | `RenderFragment?` | `null` | The warning text content |
| `AssistiveText` | `string` | `"Warning"` | Text for screen readers |
| `CssClass` | `string?` | `null` | Additional CSS classes |
| `AdditionalAttributes` | `Dictionary<string, object>?` | `null` | Additional HTML attributes |

## Examples

### Default Warning

```razor
<GovUkWarningText>
    You can be fined up to £5,000 if you do not register.
</GovUkWarningText>
```

### With Custom Assistive Text

```razor
<GovUkWarningText AssistiveText="Important">
    You must complete this form within 28 days.
</GovUkWarningText>
```

### Multiple Warnings

```razor
<GovUkWarningText>
    You must be over 18 to apply.
</GovUkWarningText>

<GovUkWarningText>
    Your application will be cancelled if you do not respond within 14 days.
</GovUkWarningText>
```

### In Context

```razor
<h1 class="govuk-heading-l">Register your vehicle</h1>

<p class="govuk-body">
    You need to register your vehicle if you want to use it on public roads.
</p>

<GovUkWarningText>
    You can be fined up to £1,000 if you do not register your vehicle.
</GovUkWarningText>

<GovUkButton Text="Start registration" IsStart="true" Href="/register" />
```

## When to Use

Use warning text for:
- Legal consequences of actions
- Deadlines that have serious implications
- Actions that cannot be undone
- Information that could cause harm if ignored

Do not use for:
- General information (use inset text)
- Success messages (use notification banner)
- Form validation errors (use error components)

## Accessibility

- Includes a visually hidden prefix for screen readers
- The "!" icon is decorative (`aria-hidden="true"`)
- Screen readers announce: "Warning: [your text]"
- High-contrast styling ensures visibility

## For AI Coding Agents

When implementing warning text:

1. Use sparingly - only for genuinely important warnings
2. Place close to the relevant content
3. Keep messages concise and actionable
4. Default `AssistiveText` of "Warning" is usually appropriate
5. Don't use for form errors or success messages

```razor
// Standard usage
<GovUkWarningText>
    Important warning message here.
</GovUkWarningText>

// With context
<p class="govuk-body">Explanation of the process...</p>

<GovUkWarningText>
    Legal warning about consequences.
</GovUkWarningText>

<GovUkButton Text="Continue" />

// Custom assistive text (rarely needed)
<GovUkWarningText AssistiveText="Deadline warning">
    Your application expires on 31 March.
</GovUkWarningText>
```
