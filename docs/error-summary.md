# Error Summary Component

Use the error summary component at the top of a page to list all errors that prevent form submission.

## Basic Usage

```razor
@using Blazor.DesignSystem.Components

<GovUkErrorSummary Errors="@errors" />

@code {
    private List<GovUkErrorSummary.ErrorItem> errors = new()
    {
        new() { Message = "Enter your full name", FieldId = "full-name" },
        new() { Message = "Enter a valid email address", FieldId = "email" }
    };
}
```

## Parameters

| Parameter | Type | Default | Description |
|-----------|------|---------|-------------|
| `Title` | `string` | `"There is a problem"` | The error summary title |
| `Errors` | `IEnumerable<ErrorItem>` | Empty | List of errors |
| `CssClass` | `string?` | `null` | Additional CSS classes |
| `AdditionalAttributes` | `Dictionary<string, object>?` | `null` | Additional HTML attributes |

## ErrorItem Properties

| Property | Type | Description |
|----------|------|-------------|
| `Message` | `string` | The error message text |
| `FieldId` | `string?` | ID of the field with the error (for linking) |

## Examples

### Basic Error Summary

```razor
<GovUkErrorSummary Errors="@errors" />

@code {
    private List<GovUkErrorSummary.ErrorItem> errors = new()
    {
        new() { Message = "Enter your date of birth", FieldId = "date-of-birth" },
        new() { Message = "Select your nationality", FieldId = "nationality" }
    };
}
```

### With Custom Title

```razor
<GovUkErrorSummary 
    Title="Please fix the following errors"
    Errors="@errors" />
```

### Complete Form Validation Example

```razor
@rendermode InteractiveServer

@if (errors.Any())
{
    <GovUkErrorSummary Errors="@errors" />
}

<GovUkInput 
    Id="full-name"
    Label="Full name"
    HasError="@HasError("full-name")"
    ErrorMessage="Enter your full name"
    @bind-Value="fullName" />

<GovUkInput 
    Id="email"
    Label="Email address"
    Type="email"
    HasError="@HasError("email")"
    ErrorMessage="Enter a valid email address"
    @bind-Value="email" />

<GovUkButton Text="Submit" OnClick="ValidateAndSubmit" />

@code {
    private string fullName = "";
    private string email = "";
    private List<GovUkErrorSummary.ErrorItem> errors = new();
    
    private bool HasError(string fieldId) => errors.Any(e => e.FieldId == fieldId);
    
    private void ValidateAndSubmit()
    {
        errors.Clear();
        
        if (string.IsNullOrWhiteSpace(fullName))
        {
            errors.Add(new() { Message = "Enter your full name", FieldId = "full-name" });
        }
        
        if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
        {
            errors.Add(new() { Message = "Enter a valid email address", FieldId = "email" });
        }
        
        if (!errors.Any())
        {
            // Proceed with form submission
        }
    }
}
```

### Errors Without Field Links

When an error isn't specific to a field:

```razor
<GovUkErrorSummary Errors="@errors" />

@code {
    private List<GovUkErrorSummary.ErrorItem> errors = new()
    {
        new() { Message = "Your session has expired", FieldId = null }
    };
}
```

### Dynamic Error Management

```razor
@code {
    private List<GovUkErrorSummary.ErrorItem> errors = new();
    
    private void AddError(string message, string? fieldId = null)
    {
        errors.Add(new GovUkErrorSummary.ErrorItem 
        { 
            Message = message, 
            FieldId = fieldId 
        });
    }
    
    private void ClearErrors()
    {
        errors.Clear();
    }
    
    private void ClearError(string fieldId)
    {
        errors.RemoveAll(e => e.FieldId == fieldId);
    }
}
```

## Placement

The error summary should appear:
1. At the top of the main content area
2. After the page heading (if there is one)
3. Before the form fields

```razor
<h1 class="govuk-heading-l">Register</h1>

@if (errors.Any())
{
    <GovUkErrorSummary Errors="@errors" />
}

<form>
    @* Form fields *@
</form>
```

## Focus Management

After form submission with errors:
1. The error summary should appear
2. Focus should move to the error summary (use JavaScript)

```razor
@inject IJSRuntime JS

@code {
    private async Task ValidateAndFocusSummary()
    {
        errors.Clear();
        // ... validation ...
        
        if (errors.Any())
        {
            StateHasChanged();
            await JS.InvokeVoidAsync("eval", 
                "document.querySelector('.govuk-error-summary')?.focus()");
        }
    }
}
```

## Accessibility

- Uses `role="alert"` for immediate screen reader announcement
- Error links allow users to jump directly to problem fields
- Title provides clear indication something went wrong
- Listed errors help users understand all issues at once

## For AI Coding Agents

When implementing error summaries:

1. Only show when there are validation errors
2. Include `FieldId` for each error so users can jump to the field
3. Clear errors before re-validating
4. Order errors by field order on the page
5. Use the same error message text as in the field error

```razor
@rendermode InteractiveServer

// Standard validation pattern
@if (errors.Any())
{
    <GovUkErrorSummary Errors="@errors" />
}

<GovUkInput 
    Id="field-id"
    Label="Field label"
    HasError="@errors.Any(e => e.FieldId == "field-id")"
    ErrorMessage="Error message"
    @bind-Value="fieldValue" />

<GovUkButton Text="Submit" OnClick="Validate" />

@code {
    private string fieldValue = "";
    private List<GovUkErrorSummary.ErrorItem> errors = new();
    
    private void Validate()
    {
        errors.Clear();
        
        if (string.IsNullOrWhiteSpace(fieldValue))
        {
            errors.Add(new() 
            { 
                Message = "Error message", 
                FieldId = "field-id" 
            });
        }
        
        if (!errors.Any())
        {
            // Submit form
        }
    }
}
```

Best practices for error messages:
- Start with a verb: "Enter", "Select", "Check"
- Be specific about what's wrong
- Tell users how to fix the problem
