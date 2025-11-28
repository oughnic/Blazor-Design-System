# Hint Component

Use the hint component to provide additional guidance for form fields.

## Basic Usage

```razor
@using Blazor.DesignSystem.Components

<GovUkHint Id="name-hint">
    Enter your full name as it appears on your passport
</GovUkHint>
```

## Parameters

| Parameter | Type | Default | Description |
|-----------|------|---------|-------------|
| `Id` | `string?` | `null` | ID for the hint element |
| `ChildContent` | `RenderFragment?` | `null` | The hint content |
| `CssClass` | `string?` | `null` | Additional CSS classes |
| `AdditionalAttributes` | `Dictionary<string, object>?` | `null` | Additional HTML attributes |

## Examples

### Basic Hint

```razor
<GovUkHint>
    Your name as it appears on official documents
</GovUkHint>
```

### With ID for Accessibility

```razor
<GovUkHint Id="email-hint">
    We'll only use this to contact you about your application
</GovUkHint>
```

### With HTML Content

```razor
<GovUkHint Id="phone-hint">
    Include the country code. For example, +44 7700 900 000
</GovUkHint>
```

## Built-in Hints

Most form components have a built-in `Hint` parameter:

```razor
<GovUkInput 
    Id="national-insurance"
    Label="National Insurance number"
    Hint="It's on your National Insurance card, payslip or P60. For example, 'QQ 12 34 56 C'."
    @bind-Value="niNumber" />
```

### Standalone Use

Use the standalone hint component when you need custom layouts:

```razor
<label class="govuk-label" for="custom">Custom field</label>
<GovUkHint Id="custom-hint">
    <p>Complex hint with multiple elements:</p>
    <ul class="govuk-list govuk-list--bullet">
        <li>First point</li>
        <li>Second point</li>
    </ul>
</GovUkHint>
<input 
    class="govuk-input" 
    id="custom" 
    aria-describedby="custom-hint" />
```

## Accessibility

- Use `Id` to enable `aria-describedby` linking
- Hint text is read by screen readers when field receives focus
- Keep hints concise but helpful

## For AI Coding Agents

When implementing hints:

1. Prefer using the built-in `Hint` parameter on form components
2. Use standalone `GovUkHint` for custom layouts only
3. Always link hints to fields via `aria-describedby`
4. Keep hint text concise and actionable

```razor
// Preferred: Use built-in hint parameter
<GovUkInput 
    Id="field"
    Label="Field label"
    Hint="Helpful hint text"
    @bind-Value="value" />

// Standalone (when needed)
<GovUkHint Id="field-hint">Hint text</GovUkHint>
<input aria-describedby="field-hint" />
```
