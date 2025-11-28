# Error Message Component

Use the error message component to show error messages for form fields.

## Basic Usage

```razor
@using Blazor.DesignSystem.Components

<GovUkErrorMessage>Enter your full name</GovUkErrorMessage>
```

## Parameters

| Parameter | Type | Default | Description |
|-----------|------|---------|-------------|
| `Id` | `string?` | `null` | ID for the error message element |
| `ChildContent` | `RenderFragment?` | `null` | The error message content |
| `CssClass` | `string?` | `null` | Additional CSS classes |
| `AdditionalAttributes` | `Dictionary<string, object>?` | `null` | Additional HTML attributes |

## Examples

### Basic Error Message

```razor
<GovUkErrorMessage>Enter your email address</GovUkErrorMessage>
```

### With ID for Accessibility

```razor
<GovUkErrorMessage Id="email-error">
    Enter a valid email address
</GovUkErrorMessage>
```

### Multiple Errors for One Field

```razor
<GovUkErrorMessage Id="password-error">
    <span>Password must:</span>
    <ul class="govuk-list govuk-list--bullet govuk-!-margin-top-1">
        <li>be at least 8 characters</li>
        <li>contain a number</li>
    </ul>
</GovUkErrorMessage>
```

## Usage with Form Fields

Most form components have built-in error handling. Use the `HasError` and `ErrorMessage` parameters:

```razor
<GovUkInput 
    Id="email"
    Label="Email address"
    HasError="@hasError"
    ErrorMessage="Enter a valid email address"
    @bind-Value="email" />

@code {
    private string email = "";
    private bool hasError = false;
}
```

### Standalone Use

Use the standalone error message component when you need custom error layouts:

```razor
<div class="govuk-form-group govuk-form-group--error">
    <label class="govuk-label" for="custom-field">
        Custom field
    </label>
    <GovUkErrorMessage Id="custom-field-error">
        Enter a value for this field
    </GovUkErrorMessage>
    <input class="govuk-input govuk-input--error" 
           id="custom-field" 
           type="text"
           aria-describedby="custom-field-error" />
</div>
```

## Accessibility

- Includes visually hidden "Error:" prefix for screen readers
- Should be linked to form fields via `aria-describedby`
- Use unique IDs when multiple error messages exist
- Error styling uses red border and text color for visual indication

## Screen Reader Announcement

The component renders:
```html
<p class="govuk-error-message">
    <span class="govuk-visually-hidden">Error:</span> Your error message here
</p>
```

This ensures screen readers announce "Error: Your error message here".

## For AI Coding Agents

When implementing error messages:

1. Prefer using built-in `HasError`/`ErrorMessage` on form components
2. Use standalone `GovUkErrorMessage` for custom form layouts only
3. Always link errors to fields via `aria-describedby`
4. Provide specific, actionable error messages
5. Keep error messages concise but helpful

```razor
// Preferred: Use built-in error handling
<GovUkInput 
    Id="name"
    Label="Full name"
    HasError="@nameError"
    ErrorMessage="Enter your full name"
    @bind-Value="name" />

// Standalone usage (when needed)
<div class="govuk-form-group govuk-form-group--error">
    <label class="govuk-label" for="custom">Label</label>
    <GovUkErrorMessage Id="custom-error">Error message</GovUkErrorMessage>
    <input 
        class="govuk-input govuk-input--error" 
        id="custom" 
        aria-describedby="custom-error" />
</div>
```

Error message best practices:
- Say what went wrong AND how to fix it
- Use "Enter" instead of "Please enter"
- Be specific: "Enter a date in the past" not "Invalid date"
