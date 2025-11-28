# Input Component

Use the text input component to let users enter text that's no longer than a single line.

> ⚠️ **Note**: Use `@rendermode InteractiveServer` or `@rendermode InteractiveWebAssembly` for real-time value binding.

## Basic Usage

```razor
@rendermode InteractiveServer
@using Blazor.DesignSystem.Components

<GovUkInput 
    Id="full-name"
    Label="Full name"
    @bind-Value="fullName" />

@code {
    private string fullName = "";
}
```

## Parameters

| Parameter | Type | Default | Description |
|-----------|------|---------|-------------|
| `Id` | `string` | Auto-generated GUID | Unique identifier |
| `Name` | `string?` | `null` | Name attribute |
| `Type` | `string` | `"text"` | Input type |
| `Label` | `string?` | `null` | Label text |
| `LabelCssClass` | `string?` | `null` | CSS for label |
| `Hint` | `string?` | `null` | Hint text |
| `Value` | `string?` | `null` | Current value |
| `ValueChanged` | `EventCallback<string?>` | - | Value change callback |
| `Disabled` | `bool` | `false` | Whether disabled |
| `HasError` | `bool` | `false` | Whether has error |
| `ErrorMessage` | `string?` | `null` | Error message |
| `Width` | `string?` | `null` | Input width |
| `Autocomplete` | `string?` | `null` | Autocomplete attribute |
| `InputMode` | `string?` | `null` | Input mode |
| `Pattern` | `string?` | `null` | Pattern attribute |
| `Spellcheck` | `bool?` | `null` | Spellcheck attribute |
| `CssClass` | `string?` | `null` | CSS for form group |
| `InputCssClass` | `string?` | `null` | CSS for input |
| `AdditionalAttributes` | `Dictionary<string, object>?` | `null` | Additional attributes |

## Width Options

Character-based widths: `"2"`, `"3"`, `"4"`, `"5"`, `"10"`, `"20"`

Fluid widths: `"full"`, `"three-quarters"`, `"two-thirds"`, `"one-half"`, `"one-third"`, `"one-quarter"`

## Examples

### With Hint

```razor
<GovUkInput 
    Id="ni-number"
    Label="National Insurance number"
    Hint="It's on your National Insurance card. For example, 'QQ 12 34 56 C'."
    @bind-Value="niNumber" />
```

### With Fixed Width

```razor
<GovUkInput 
    Id="postcode"
    Label="Postcode"
    Width="10"
    @bind-Value="postcode" />
```

### With Error

```razor
<GovUkInput 
    Id="email"
    Label="Email address"
    Type="email"
    HasError="true"
    ErrorMessage="Enter a valid email address"
    @bind-Value="email" />
```

### Email Input

```razor
<GovUkInput 
    Id="email"
    Label="Email address"
    Type="email"
    Autocomplete="email"
    @bind-Value="email" />
```

### Phone Number

```razor
<GovUkInput 
    Id="telephone"
    Label="UK telephone number"
    Type="tel"
    Autocomplete="tel"
    @bind-Value="telephone" />
```

### Numeric Input

```razor
<GovUkInput 
    Id="account-number"
    Label="Account number"
    InputMode="numeric"
    Pattern="[0-9]*"
    Width="10"
    @bind-Value="accountNumber" />
```

### With Validation

```razor
@rendermode InteractiveServer

<GovUkInput 
    Id="name"
    Label="Full name"
    HasError="@nameError"
    ErrorMessage="Enter your full name"
    @bind-Value="name" />

<GovUkButton Text="Continue" OnClick="Validate" />

@code {
    private string name = "";
    private bool nameError = false;
    
    private void Validate()
    {
        nameError = string.IsNullOrWhiteSpace(name);
    }
}
```

## Accessibility

- Label is linked to input via `for` attribute
- Hint and error linked via `aria-describedby`
- Error prefix "Error:" is visually hidden

## For AI Coding Agents

```razor
@rendermode InteractiveServer

// Standard text input
<GovUkInput 
    Id="unique-id"
    Label="Label text"
    Hint="Optional hint"
    @bind-Value="value" />

// With validation
<GovUkInput 
    Id="field"
    Label="Field"
    HasError="@hasError"
    ErrorMessage="Error message"
    @bind-Value="field" />

// Common input types
<GovUkInput Id="email" Label="Email" Type="email" Autocomplete="email" />
<GovUkInput Id="tel" Label="Phone" Type="tel" Autocomplete="tel" />
<GovUkInput Id="postcode" Label="Postcode" Width="10" Autocomplete="postal-code" />
```
