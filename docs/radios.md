# Radios Component

Use the radios component when users can only select one option from a list.

> ⚠️ **Note**: Use `@rendermode InteractiveServer` or `@rendermode InteractiveWebAssembly` for interactive functionality.

## Components

| Component | Description |
|-----------|-------------|
| `GovUkRadios` | The container for radio group |
| `GovUkRadioItem` | Individual radio items |

## Basic Usage

```razor
@rendermode InteractiveServer
@using Blazor.DesignSystem.Components

<GovUkRadios Name="contact" Legend="How would you prefer to be contacted?">
    <GovUkRadioItem Value="email" Label="Email" />
    <GovUkRadioItem Value="phone" Label="Phone" />
    <GovUkRadioItem Value="text" Label="Text message" />
</GovUkRadios>
```

## GovUkRadios Parameters

| Parameter | Type | Default | Description |
|-----------|------|---------|-------------|
| `Name` | `string` | **Required** | Name for the radio group |
| `Legend` | `string?` | `null` | Legend text |
| `LegendCssClass` | `string?` | `null` | CSS for legend |
| `IsPageHeading` | `bool` | `false` | Style legend as heading |
| `Hint` | `string?` | `null` | Hint text |
| `HasError` | `bool` | `false` | Whether has error |
| `ErrorMessage` | `string?` | `null` | Error message |
| `IsSmall` | `bool` | `false` | Use smaller radios |
| `IsInline` | `bool` | `false` | Display inline |
| `CssClass` | `string?` | `null` | Additional CSS |
| `SelectedValue` | `string?` | `null` | Currently selected value |
| `SelectedValueChanged` | `EventCallback<string?>` | - | Selection change callback |

## GovUkRadioItem Parameters

| Parameter | Type | Default | Description |
|-----------|------|---------|-------------|
| `Id` | `string?` | Auto-generated | Unique identifier |
| `Value` | `string` | **Required** | Value when selected |
| `Label` | `string?` | `null` | Label text |
| `ChildContent` | `RenderFragment?` | `null` | Custom label |
| `Hint` | `string?` | `null` | Hint text |
| `Disabled` | `bool` | `false` | Whether disabled |
| `HasConditional` | `bool` | `false` | Has conditional content |
| `ConditionalContent` | `RenderFragment?` | `null` | Conditional content |

## Examples

### With Two-Way Binding

```razor
<GovUkRadios 
    Name="answer"
    Legend="Have you changed your name?"
    @bind-SelectedValue="answer">
    <GovUkRadioItem Value="yes" Label="Yes" />
    <GovUkRadioItem Value="no" Label="No" />
</GovUkRadios>

<p>Selected: @answer</p>

@code {
    private string? answer;
}
```

### With Hints

```razor
<GovUkRadios 
    Name="sign-in"
    Legend="How do you want to sign in?"
    Hint="Choose your preferred method">
    <GovUkRadioItem 
        Value="gateway" 
        Label="Government Gateway"
        Hint="You'll need your Government Gateway ID" />
    <GovUkRadioItem 
        Value="verify" 
        Label="GOV.UK Verify"
        Hint="You'll need an identity account" />
</GovUkRadios>
```

### Inline Radios

```razor
<GovUkRadios 
    Name="changed-name"
    Legend="Have you changed your name?"
    IsInline="true"
    @bind-SelectedValue="changedName">
    <GovUkRadioItem Value="yes" Label="Yes" />
    <GovUkRadioItem Value="no" Label="No" />
</GovUkRadios>
```

### Small Radios

```razor
<GovUkRadios 
    Name="filter"
    Legend="Filter by"
    IsSmall="true">
    <GovUkRadioItem Value="newest" Label="Newest first" />
    <GovUkRadioItem Value="oldest" Label="Oldest first" />
</GovUkRadios>
```

### With Error

```razor
<GovUkRadios 
    Name="contact"
    Legend="How would you like to be contacted?"
    HasError="true"
    ErrorMessage="Select how you would like to be contacted"
    @bind-SelectedValue="contact">
    <GovUkRadioItem Value="email" Label="Email" />
    <GovUkRadioItem Value="phone" Label="Phone" />
</GovUkRadios>
```

### Legend as Page Heading

```razor
<GovUkRadios 
    Name="location"
    Legend="Where do you live?"
    LegendCssClass="govuk-fieldset__legend--l"
    IsPageHeading="true"
    @bind-SelectedValue="location">
    <GovUkRadioItem Value="england" Label="England" />
    <GovUkRadioItem Value="scotland" Label="Scotland" />
    <GovUkRadioItem Value="wales" Label="Wales" />
    <GovUkRadioItem Value="ni" Label="Northern Ireland" />
</GovUkRadios>
```

### With Conditional Content

```razor
<GovUkRadios Name="contact" Legend="Contact preference" @bind-SelectedValue="contactMethod">
    <GovUkRadioItem 
        Value="email" 
        Label="Email"
        HasConditional="true">
        <ConditionalContent>
            <GovUkInput 
                Id="email-address"
                Label="Email address"
                Type="email"
                @bind-Value="email" />
        </ConditionalContent>
    </GovUkRadioItem>
    <GovUkRadioItem 
        Value="phone" 
        Label="Phone"
        HasConditional="true">
        <ConditionalContent>
            <GovUkInput 
                Id="phone-number"
                Label="Phone number"
                Type="tel"
                @bind-Value="phone" />
        </ConditionalContent>
    </GovUkRadioItem>
</GovUkRadios>

@code {
    private string? contactMethod;
    private string email = "";
    private string phone = "";
}
```

## Accessibility

- Uses `<fieldset>` with `<legend>` for grouping
- `aria-describedby` links to hint and error
- Conditional content uses `aria-controls`
- Error prefix "Error:" is visually hidden

## For AI Coding Agents

When implementing radios:

1. The `Name` parameter is required
2. Use `@bind-SelectedValue` for two-way binding
3. Each `GovUkRadioItem` needs a unique `Value`
4. Use inline for yes/no type questions
5. Use conditional content for revealing related fields

```razor
@rendermode InteractiveServer

// Standard pattern
<GovUkRadios 
    Name="group-name"
    Legend="Question?"
    @bind-SelectedValue="selected">
    <GovUkRadioItem Value="option1" Label="Option 1" />
    <GovUkRadioItem Value="option2" Label="Option 2" />
</GovUkRadios>

@code {
    private string? selected;
    
    private void HandleSubmit()
    {
        if (string.IsNullOrEmpty(selected))
        {
            // Show error
        }
    }
}

// Yes/No question
<GovUkRadios 
    Name="agree"
    Legend="Do you agree?"
    IsInline="true"
    @bind-SelectedValue="agree">
    <GovUkRadioItem Value="yes" Label="Yes" />
    <GovUkRadioItem Value="no" Label="No" />
</GovUkRadios>
```
