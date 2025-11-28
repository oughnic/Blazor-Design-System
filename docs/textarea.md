# Textarea Component

Use the textarea component when you need users to enter an amount of text that's longer than a single line.

> ⚠️ **Note**: Use `@rendermode InteractiveServer` or `@rendermode InteractiveWebAssembly` for real-time value binding.

## Basic Usage

```razor
@rendermode InteractiveServer
@using Blazor.DesignSystem.Components

<GovUkTextarea 
    Id="description"
    Label="Describe the issue"
    @bind-Value="description" />

@code {
    private string description = "";
}
```

## Parameters

| Parameter | Type | Default | Description |
|-----------|------|---------|-------------|
| `Id` | `string` | Auto-generated GUID | Unique identifier |
| `Name` | `string?` | `null` | Name attribute |
| `Label` | `string?` | `null` | Label text |
| `LabelCssClass` | `string?` | `null` | CSS for label |
| `Hint` | `string?` | `null` | Hint text |
| `Rows` | `int` | `5` | Number of rows |
| `Value` | `string?` | `null` | Current value |
| `ValueChanged` | `EventCallback<string?>` | - | Value change callback |
| `Disabled` | `bool` | `false` | Whether disabled |
| `HasError` | `bool` | `false` | Whether has error |
| `ErrorMessage` | `string?` | `null` | Error message |
| `Autocomplete` | `string?` | `null` | Autocomplete attribute |
| `Spellcheck` | `bool?` | `null` | Spellcheck attribute |
| `CssClass` | `string?` | `null` | CSS for form group |
| `TextareaCssClass` | `string?` | `null` | CSS for textarea |
| `AdditionalAttributes` | `Dictionary<string, object>?` | `null` | Additional attributes |

## Examples

### With Hint

```razor
<GovUkTextarea 
    Id="feedback"
    Label="Give feedback"
    Hint="Do not include personal or financial information"
    @bind-Value="feedback" />
```

### With More Rows

```razor
<GovUkTextarea 
    Id="essay"
    Label="Write your essay"
    Rows="10"
    @bind-Value="essay" />
```

### With Error

```razor
<GovUkTextarea 
    Id="details"
    Label="Provide details"
    HasError="true"
    ErrorMessage="Enter more details about your issue"
    @bind-Value="details" />
```

### With Spellcheck Disabled

```razor
<GovUkTextarea 
    Id="reference"
    Label="Reference numbers"
    Hint="Enter each reference on a new line"
    Spellcheck="false"
    @bind-Value="references" />
```

### Complete Form Example

```razor
@rendermode InteractiveServer

<h1 class="govuk-heading-l">Contact us</h1>

<GovUkInput 
    Id="name"
    Label="Full name"
    @bind-Value="name" />

<GovUkTextarea 
    Id="message"
    Label="Your message"
    Hint="Include as much detail as possible"
    Rows="8"
    HasError="@messageError"
    ErrorMessage="Enter your message"
    @bind-Value="message" />

<GovUkButton Text="Send message" OnClick="Submit" />

@code {
    private string name = "";
    private string message = "";
    private bool messageError = false;
    
    private void Submit()
    {
        messageError = string.IsNullOrWhiteSpace(message);
        if (!messageError)
        {
            // Process submission
        }
    }
}
```

## When to Use

Use textarea when:
- Users need to enter multiple lines of text
- The expected answer is longer than a single sentence
- Free-form text is needed (descriptions, comments, etc.)

Use character count component (`GovUkCharacterCount`) when:
- There is a specific character limit
- Users need to see how many characters they've entered

## Accessibility

- Label linked to textarea via `for` attribute
- Hint and error linked via `aria-describedby`
- Error prefix "Error:" is visually hidden
- Spellcheck can be disabled for technical input

## For AI Coding Agents

When implementing textareas:

1. Set appropriate number of `Rows` for expected content length
2. Use `@bind-Value` for two-way data binding
3. Use `GovUkCharacterCount` when there's a limit
4. Disable spellcheck for reference numbers, codes, etc.
5. Validate content length on submission

```razor
@rendermode InteractiveServer

// Standard textarea
<GovUkTextarea 
    Id="unique-id"
    Label="Field label"
    Hint="Optional hint text"
    Rows="5"
    @bind-Value="fieldValue" />

// With validation
<GovUkTextarea 
    Id="description"
    Label="Description"
    HasError="@hasError"
    ErrorMessage="Enter a description"
    @bind-Value="description" />

@code {
    private string description = "";
    private bool hasError = false;
    
    private void Validate()
    {
        hasError = string.IsNullOrWhiteSpace(description);
    }
}

// For longer content
<GovUkTextarea 
    Id="essay"
    Label="Write your response"
    Rows="10"
    @bind-Value="response" />
```
