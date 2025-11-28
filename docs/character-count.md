# Character Count Component

The character count component helps users know how many characters they can enter into a textarea and how many they have remaining.

> ⚠️ **Note**: For interactive character counting, use `@rendermode InteractiveServer` or `@rendermode InteractiveWebAssembly`.

## Basic Usage

```razor
@rendermode InteractiveServer
@using Blazor.DesignSystem.Components

<GovUkCharacterCount 
    Id="description"
    Label="Describe your issue"
    MaxLength="200"
    @bind-Value="description" />

@code {
    private string description = "";
}
```

## Parameters

| Parameter | Type | Default | Description |
|-----------|------|---------|-------------|
| `Id` | `string` | Auto-generated GUID | Unique identifier |
| `Name` | `string?` | `null` | Name attribute for form submission |
| `Label` | `string?` | `null` | Label text |
| `LabelCssClass` | `string?` | `null` | Additional CSS for label |
| `Hint` | `string?` | `null` | Hint text below label |
| `MaxLength` | `int` | `200` | Maximum characters allowed |
| `Rows` | `int` | `5` | Number of textarea rows |
| `Value` | `string?` | `null` | Current value |
| `ValueChanged` | `EventCallback<string?>` | - | Value change callback |
| `HasError` | `bool` | `false` | Whether field has error |
| `ErrorMessage` | `string?` | `null` | Error message to display |
| `CssClass` | `string?` | `null` | Additional CSS classes |
| `TextAreaCssClass` | `string?` | `null` | CSS classes for textarea |
| `Threshold` | `int` | `100` | Percentage threshold for showing count |

## Examples

### Basic Character Count

```razor
<GovUkCharacterCount 
    Id="bio"
    Label="Personal biography"
    MaxLength="500"
    @bind-Value="bio" />

@code {
    private string bio = "";
}
```

### With Hint Text

```razor
<GovUkCharacterCount 
    Id="feedback"
    Label="Your feedback"
    Hint="Include as much detail as possible"
    MaxLength="300"
    @bind-Value="feedback" />
```

### With Custom Rows

```razor
<GovUkCharacterCount 
    Id="essay"
    Label="Write your essay"
    MaxLength="1000"
    Rows="10"
    @bind-Value="essay" />
```

### With Error State

```razor
<GovUkCharacterCount 
    Id="message"
    Label="Your message"
    MaxLength="200"
    HasError="true"
    ErrorMessage="Message is required"
    @bind-Value="message" />
```

### Complete Form Example

```razor
@rendermode InteractiveServer

<h1 class="govuk-heading-l">Contact us</h1>

<GovUkInput 
    Id="email"
    Label="Email address"
    @bind-Value="email" />

<GovUkCharacterCount 
    Id="message"
    Label="Your message"
    Hint="Please provide details about your enquiry"
    MaxLength="500"
    HasError="@(submitted && string.IsNullOrEmpty(message))"
    ErrorMessage="Enter your message"
    @bind-Value="message" />

<p class="govuk-body">Characters entered: @(message?.Length ?? 0) of 500</p>

<GovUkButton Text="Send message" OnClick="Submit" />

@code {
    private string email = "";
    private string message = "";
    private bool submitted = false;
    
    private void Submit()
    {
        submitted = true;
        if (!string.IsNullOrEmpty(message))
        {
            // Process submission
        }
    }
}
```

## Character Count Display

The component displays:
- "You have X characters remaining" when under the limit
- "You have 1 character remaining" (singular)
- "You have X characters too many" when over the limit
- "You have 1 character too many" (singular)

## Accessibility

- Character count message uses `aria-describedby`
- Error states include visually hidden "Error:" prefix
- Hint text is linked via `aria-describedby`
- Live character count announced to screen readers

## For AI Coding Agents

When implementing character count:

1. Always set a meaningful `MaxLength` based on your data requirements
2. Use `@bind-Value` for two-way data binding
3. The component requires interactive rendering for real-time counting
4. Validate on the server that content doesn't exceed `MaxLength`
5. Consider UX - don't use character counts for short fields

```razor
@rendermode InteractiveServer

// Standard pattern
<GovUkCharacterCount 
    Id="unique-id"
    Label="Field label"
    MaxLength="@maxChars"
    @bind-Value="fieldValue" />

@code {
    private int maxChars = 200;
    private string fieldValue = "";
}

// With validation
<GovUkCharacterCount 
    Id="description"
    Label="Description"
    MaxLength="500"
    HasError="@(!string.IsNullOrEmpty(descriptionError))"
    ErrorMessage="@descriptionError"
    @bind-Value="description" />

@code {
    private string description = "";
    private string? descriptionError = null;
    
    private void Validate()
    {
        if (string.IsNullOrEmpty(description))
            descriptionError = "Enter a description";
        else if (description.Length > 500)
            descriptionError = "Description must be 500 characters or less";
        else
            descriptionError = null;
    }
}
```
