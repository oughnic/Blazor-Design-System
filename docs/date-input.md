# Date Input Component

Use the date input component to help users enter a memorable date or one they can easily look up.

> ⚠️ **Note**: Use `@rendermode InteractiveServer` or `@rendermode InteractiveWebAssembly` for interactive functionality.

## Basic Usage

```razor
@rendermode InteractiveServer
@using Blazor.DesignSystem.Components

<GovUkDateInput 
    Id="date-of-birth"
    Legend="What is your date of birth?"
    @bind-Day="day"
    @bind-Month="month"
    @bind-Year="year" />

@code {
    private string? day;
    private string? month;
    private string? year;
}
```

## Parameters

| Parameter | Type | Default | Description |
|-----------|------|---------|-------------|
| `Id` | `string` | Auto-generated GUID | Unique identifier |
| `Name` | `string?` | `null` | Name attribute prefix |
| `Legend` | `string?` | `null` | Legend text for fieldset |
| `LegendCssClass` | `string?` | `null` | CSS classes for legend |
| `IsPageHeading` | `bool` | `false` | Style legend as page heading |
| `Hint` | `string?` | `null` | Hint text |
| `Day` | `string?` | `null` | Day value |
| `DayChanged` | `EventCallback<string?>` | - | Day change callback |
| `Month` | `string?` | `null` | Month value |
| `MonthChanged` | `EventCallback<string?>` | - | Month change callback |
| `Year` | `string?` | `null` | Year value |
| `YearChanged` | `EventCallback<string?>` | - | Year change callback |
| `HasError` | `bool` | `false` | Whether field has error |
| `ErrorMessage` | `string?` | `null` | Error message |
| `CssClass` | `string?` | `null` | Additional CSS classes |

## Examples

### With Hint Text

```razor
<GovUkDateInput 
    Id="passport-expiry"
    Legend="When does your passport expire?"
    Hint="For example, 27 3 2025"
    @bind-Day="day"
    @bind-Month="month"
    @bind-Year="year" />
```

### As Page Heading

```razor
<GovUkDateInput 
    Id="start-date"
    Legend="When do you want to start?"
    LegendCssClass="govuk-fieldset__legend--l"
    IsPageHeading="true"
    Hint="For example, 12 11 2024"
    @bind-Day="day"
    @bind-Month="month"
    @bind-Year="year" />
```

### With Error State

```razor
<GovUkDateInput 
    Id="birth-date"
    Legend="Date of birth"
    Hint="For example, 31 3 1980"
    HasError="true"
    ErrorMessage="Enter a valid date"
    @bind-Day="day"
    @bind-Month="month"
    @bind-Year="year" />
```

### Complete Form Example

```razor
@rendermode InteractiveServer

<h1 class="govuk-heading-l">Personal details</h1>

<GovUkInput 
    Id="full-name"
    Label="Full name"
    @bind-Value="fullName" />

<GovUkDateInput 
    Id="date-of-birth"
    Legend="Date of birth"
    Hint="For example, 31 3 1980"
    HasError="@dateError"
    ErrorMessage="@dateErrorMessage"
    @bind-Day="dobDay"
    @bind-Month="dobMonth"
    @bind-Year="dobYear" />

<GovUkButton Text="Continue" OnClick="HandleSubmit" />

@code {
    private string fullName = "";
    private string? dobDay;
    private string? dobMonth;
    private string? dobYear;
    private bool dateError = false;
    private string? dateErrorMessage;
    
    private void HandleSubmit()
    {
        dateError = false;
        dateErrorMessage = null;
        
        if (string.IsNullOrEmpty(dobDay) || string.IsNullOrEmpty(dobMonth) || string.IsNullOrEmpty(dobYear))
        {
            dateError = true;
            dateErrorMessage = "Enter your date of birth";
            return;
        }
        
        if (!int.TryParse(dobDay, out int day) || day < 1 || day > 31)
        {
            dateError = true;
            dateErrorMessage = "Enter a valid day";
            return;
        }
        
        // Continue processing...
    }
}
```

### Date Validation Helper

```razor
@code {
    private bool ValidateDate(string? day, string? month, string? year, out DateTime? result)
    {
        result = null;
        
        if (string.IsNullOrEmpty(day) || string.IsNullOrEmpty(month) || string.IsNullOrEmpty(year))
            return false;
        
        if (!int.TryParse(day, out int d) || !int.TryParse(month, out int m) || !int.TryParse(year, out int y))
            return false;
        
        try
        {
            result = new DateTime(y, m, d);
            return true;
        }
        catch
        {
            return false;
        }
    }
}
```

## Input Attributes

The date input fields have the following attributes set:
- `type="text"` - For better accessibility
- `inputmode="numeric"` - Shows numeric keyboard on mobile
- `pattern="[0-9]*"` - Accepts only numbers

## Accessibility

- Uses `<fieldset>` with `<legend>` for grouping
- `role="group"` on the fieldset for screen readers
- `aria-describedby` links to hint and error messages
- Error messages include visually hidden "Error:" prefix
- Individual fields have appropriate labels (Day, Month, Year)

## For AI Coding Agents

When implementing date inputs:

1. Always provide hint text showing the expected format
2. Use individual binding for Day, Month, Year values
3. Values are strings (not integers) to preserve leading zeros
4. Validate dates on form submission
5. Show specific error messages (e.g., "Enter a valid month")
6. Consider date range validation for business rules

```razor
@rendermode InteractiveServer

// Standard pattern
<GovUkDateInput 
    Id="unique-id"
    Legend="Enter the date"
    Hint="For example, 27 3 2025"
    HasError="@hasDateError"
    ErrorMessage="@dateErrorMessage"
    @bind-Day="day"
    @bind-Month="month"
    @bind-Year="year" />

@code {
    private string? day;
    private string? month;
    private string? year;
    private bool hasDateError = false;
    private string? dateErrorMessage;
    
    private bool ValidateAndParse(out DateTime date)
    {
        hasDateError = false;
        dateErrorMessage = null;
        date = default;
        
        // Check for empty values
        if (string.IsNullOrWhiteSpace(day) && 
            string.IsNullOrWhiteSpace(month) && 
            string.IsNullOrWhiteSpace(year))
        {
            hasDateError = true;
            dateErrorMessage = "Enter a date";
            return false;
        }
        
        // Try to parse
        if (int.TryParse(day, out int d) && 
            int.TryParse(month, out int m) && 
            int.TryParse(year, out int y))
        {
            try
            {
                date = new DateTime(y, m, d);
                return true;
            }
            catch (ArgumentOutOfRangeException)
            {
                hasDateError = true;
                dateErrorMessage = "Enter a real date";
            }
        }
        else
        {
            hasDateError = true;
            dateErrorMessage = "Enter the date using numbers only";
        }
        
        return false;
    }
}
```
