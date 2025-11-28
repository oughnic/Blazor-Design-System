# Label Component

Use the label component to add a label to a form input.

## Basic Usage

```razor
@using Blazor.DesignSystem.Components

<GovUkLabel For="my-input">Full name</GovUkLabel>
<input class="govuk-input" id="my-input" type="text" />
```

## Parameters

| Parameter | Type | Default | Description |
|-----------|------|---------|-------------|
| `For` | `string?` | `null` | ID of the associated input |
| `ChildContent` | `RenderFragment?` | `null` | The label content |
| `IsPageHeading` | `bool` | `false` | Style as page heading |
| `Size` | `string?` | `null` | Size: "s", "m", "l", "xl" |
| `CssClass` | `string?` | `null` | Additional CSS classes |
| `AdditionalAttributes` | `Dictionary<string, object>?` | `null` | Additional HTML attributes |

## Examples

### Basic Label

```razor
<GovUkLabel For="email">Email address</GovUkLabel>
```

### Large Label

```razor
<GovUkLabel For="name" Size="l">What is your name?</GovUkLabel>
```

### As Page Heading

```razor
<GovUkLabel For="description" IsPageHeading="true" Size="l">
    Describe the problem
</GovUkLabel>
```

## Built-in Labels

Most form components have a built-in `Label` parameter:

```razor
<GovUkInput Id="email" Label="Email address" @bind-Value="email" />
```

## Label Sizes

| Size | CSS Class | Typical Use |
|------|-----------|-------------|
| `"s"` | `govuk-label--s` | Labels within fieldsets |
| `"m"` | `govuk-label--m` | Standard form labels |
| `"l"` | `govuk-label--l` | Page headings |
| `"xl"` | `govuk-label--xl` | Prominent questions |

## Accessibility

- Uses `<label>` element for semantic meaning
- `for` attribute links to input ID
- Screen readers announce label when input is focused

## For AI Coding Agents

When implementing labels:

1. Prefer using the built-in `Label` parameter on form components
2. Use standalone `GovUkLabel` for custom layouts only
3. Always set `For` to the associated input ID

```razor
// Preferred: Use built-in label
<GovUkInput Id="name" Label="Full name" @bind-Value="name" />

// Standalone (when needed)
<GovUkLabel For="custom-input" Size="l">Custom field</GovUkLabel>
<input class="govuk-input" id="custom-input" />
```
