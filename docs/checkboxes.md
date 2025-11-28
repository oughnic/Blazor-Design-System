# Checkboxes Component

Use the checkboxes component when you need to help users select multiple options from a list.

> ⚠️ **Note**: Use `@rendermode InteractiveServer` or `@rendermode InteractiveWebAssembly` for interactive functionality.

## Components

| Component | Description |
|-----------|-------------|
| `GovUkCheckboxes` | The container for checkbox group |
| `GovUkCheckboxItem` | Individual checkbox items |

## Basic Usage

```razor
@rendermode InteractiveServer
@using Blazor.DesignSystem.Components

<GovUkCheckboxes Name="nationality" Legend="What is your nationality?">
    <GovUkCheckboxItem Value="british" Label="British" />
    <GovUkCheckboxItem Value="irish" Label="Irish" />
    <GovUkCheckboxItem Value="other" Label="Citizen of another country" />
</GovUkCheckboxes>
```

## GovUkCheckboxes Parameters

| Parameter | Type | Default | Description |
|-----------|------|---------|-------------|
| `Name` | `string` | **Required** | Name attribute for the group |
| `Legend` | `string?` | `null` | Legend text for the fieldset |
| `LegendCssClass` | `string?` | `null` | Additional CSS for legend |
| `IsPageHeading` | `bool` | `false` | Style legend as page heading |
| `Hint` | `string?` | `null` | Hint text below legend |
| `HasError` | `bool` | `false` | Whether field has error |
| `ErrorMessage` | `string?` | `null` | Error message to display |
| `IsSmall` | `bool` | `false` | Use smaller checkboxes |
| `CssClass` | `string?` | `null` | Additional CSS classes |
| `ChildContent` | `RenderFragment?` | `null` | The checkbox items |
| `SelectedValues` | `List<string>?` | `null` | Currently selected values |
| `SelectedValuesChanged` | `EventCallback<List<string>>` | - | Selection change callback |

## GovUkCheckboxItem Parameters

| Parameter | Type | Default | Description |
|-----------|------|---------|-------------|
| `Id` | `string?` | Auto-generated | Unique identifier |
| `Value` | `string` | **Required** | Value when checked |
| `Label` | `string?` | `null` | Label text |
| `ChildContent` | `RenderFragment?` | `null` | Custom label content |
| `Hint` | `string?` | `null` | Hint text for the item |
| `Disabled` | `bool` | `false` | Whether item is disabled |
| `HasConditional` | `bool` | `false` | Has conditional content |
| `ConditionalContent` | `RenderFragment?` | `null` | Content shown when checked |

## Examples

### With Hint Text

```razor
<GovUkCheckboxes 
    Name="contact"
    Legend="How would you like to be contacted?"
    Hint="Select all that apply">
    <GovUkCheckboxItem Value="email" Label="Email" />
    <GovUkCheckboxItem Value="phone" Label="Phone" />
    <GovUkCheckboxItem Value="text" Label="Text message" />
</GovUkCheckboxes>
```

### With Two-Way Binding

```razor
<GovUkCheckboxes 
    Name="services"
    Legend="Select the services you need"
    @bind-SelectedValues="selectedServices">
    <GovUkCheckboxItem Value="consultation" Label="Consultation" />
    <GovUkCheckboxItem Value="installation" Label="Installation" />
    <GovUkCheckboxItem Value="support" Label="Support" />
</GovUkCheckboxes>

<p>Selected: @string.Join(", ", selectedServices)</p>

@code {
    private List<string> selectedServices = new();
}
```

### With Hints on Items

```razor
<GovUkCheckboxes Name="pets" Legend="Do you have any pets?">
    <GovUkCheckboxItem 
        Value="dog" 
        Label="Dog" 
        Hint="Including assistance dogs" />
    <GovUkCheckboxItem 
        Value="cat" 
        Label="Cat" />
    <GovUkCheckboxItem 
        Value="other" 
        Label="Other" 
        Hint="Rabbits, birds, fish, etc." />
</GovUkCheckboxes>
```

### Small Checkboxes

```razor
<GovUkCheckboxes 
    Name="options"
    Legend="Filter by"
    IsSmall="true">
    <GovUkCheckboxItem Value="available" Label="Available" />
    <GovUkCheckboxItem Value="popular" Label="Popular" />
</GovUkCheckboxes>
```

### With Error State

```razor
<GovUkCheckboxes 
    Name="terms"
    Legend="Terms and conditions"
    HasError="true"
    ErrorMessage="Select at least one option">
    <GovUkCheckboxItem Value="agree" Label="I agree to the terms" />
</GovUkCheckboxes>
```

### Legend as Page Heading

```razor
<GovUkCheckboxes 
    Name="waste"
    Legend="Which types of waste do you transport?"
    LegendCssClass="govuk-fieldset__legend--l"
    IsPageHeading="true">
    <GovUkCheckboxItem Value="animal" Label="Waste from animal carcasses" />
    <GovUkCheckboxItem Value="mines" Label="Waste from mines or quarries" />
    <GovUkCheckboxItem Value="farm" Label="Farm or agricultural waste" />
</GovUkCheckboxes>
```

### With Conditional Content

```razor
<GovUkCheckboxes Name="contact" Legend="Contact preferences">
    <GovUkCheckboxItem 
        Value="email" 
        Label="Email"
        HasConditional="true">
        <ConditionalContent>
            <GovUkInput 
                Id="email-address"
                Label="Email address"
                Type="email"
                @bind-Value="emailAddress" />
        </ConditionalContent>
    </GovUkCheckboxItem>
    <GovUkCheckboxItem 
        Value="phone" 
        Label="Phone"
        HasConditional="true">
        <ConditionalContent>
            <GovUkInput 
                Id="phone-number"
                Label="Phone number"
                Type="tel"
                @bind-Value="phoneNumber" />
        </ConditionalContent>
    </GovUkCheckboxItem>
</GovUkCheckboxes>

@code {
    private string emailAddress = "";
    private string phoneNumber = "";
}
```

## Accessibility

- Uses `<fieldset>` with `<legend>` for proper grouping
- `aria-describedby` links to hint and error messages
- Conditional content uses `aria-controls`
- Error prefix "Error:" is visually hidden but read by screen readers

## For AI Coding Agents

When implementing checkboxes:

1. The `Name` parameter is required for all checkbox groups
2. Use `@bind-SelectedValues` to track selections
3. Each `GovUkCheckboxItem` must have a unique `Value`
4. Use conditional content for revealing related form fields
5. Always validate selections on form submission

```razor
@rendermode InteractiveServer

// Basic pattern with binding
<GovUkCheckboxes 
    Name="options"
    Legend="Select options"
    @bind-SelectedValues="selected">
    <GovUkCheckboxItem Value="a" Label="Option A" />
    <GovUkCheckboxItem Value="b" Label="Option B" />
</GovUkCheckboxes>

@code {
    private List<string> selected = new();
    
    private void HandleSubmit()
    {
        // Check selections
        bool hasOptionA = selected.Contains("a");
    }
}

// With validation
<GovUkCheckboxes 
    Name="terms"
    HasError="@hasError"
    ErrorMessage="@errorMessage"
    @bind-SelectedValues="selections">
    <GovUkCheckboxItem Value="accept" Label="I accept" />
</GovUkCheckboxes>

@code {
    private List<string> selections = new();
    private bool hasError = false;
    private string? errorMessage = null;
    
    private bool Validate()
    {
        if (!selections.Contains("accept"))
        {
            hasError = true;
            errorMessage = "You must accept the terms";
            return false;
        }
        hasError = false;
        errorMessage = null;
        return true;
    }
}
```
