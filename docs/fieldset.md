# Fieldset Component

Use the fieldset component to group related form inputs.

## Basic Usage

```razor
@using Blazor.DesignSystem.Components

<GovUkFieldset Legend="Address">
    <GovUkInput Id="address-1" Label="Address line 1" @bind-Value="line1" />
    <GovUkInput Id="address-2" Label="Address line 2 (optional)" @bind-Value="line2" />
    <GovUkInput Id="city" Label="Town or city" @bind-Value="city" />
    <GovUkInput Id="postcode" Label="Postcode" Width="10" @bind-Value="postcode" />
</GovUkFieldset>

@code {
    private string line1 = "";
    private string line2 = "";
    private string city = "";
    private string postcode = "";
}
```

## Parameters

| Parameter | Type | Default | Description |
|-----------|------|---------|-------------|
| `Legend` | `string?` | `null` | Legend text for the fieldset |
| `LegendCssClass` | `string?` | `null` | Additional CSS classes for legend |
| `IsPageHeading` | `bool` | `false` | Style legend as page heading |
| `AriaDescribedBy` | `string?` | `null` | IDs of elements describing fieldset |
| `ChildContent` | `RenderFragment?` | `null` | Fieldset content |
| `CssClass` | `string?` | `null` | Additional CSS classes |
| `AdditionalAttributes` | `Dictionary<string, object>?` | `null` | Additional HTML attributes |

## Examples

### Basic Fieldset

```razor
<GovUkFieldset Legend="Personal details">
    <GovUkInput Id="first-name" Label="First name" @bind-Value="firstName" />
    <GovUkInput Id="last-name" Label="Last name" @bind-Value="lastName" />
</GovUkFieldset>
```

### Legend as Page Heading

```razor
<GovUkFieldset 
    Legend="What is your address?"
    LegendCssClass="govuk-fieldset__legend--l"
    IsPageHeading="true">
    <GovUkInput Id="address-1" Label="Address line 1" @bind-Value="line1" />
    <GovUkInput Id="postcode" Label="Postcode" Width="10" @bind-Value="postcode" />
</GovUkFieldset>
```

### With Different Legend Sizes

```razor
@* Small legend *@
<GovUkFieldset Legend="Name" LegendCssClass="govuk-fieldset__legend--s">
    ...
</GovUkFieldset>

@* Medium legend (default) *@
<GovUkFieldset Legend="Name" LegendCssClass="govuk-fieldset__legend--m">
    ...
</GovUkFieldset>

@* Large legend *@
<GovUkFieldset Legend="Name" LegendCssClass="govuk-fieldset__legend--l">
    ...
</GovUkFieldset>

@* Extra large legend *@
<GovUkFieldset Legend="Name" LegendCssClass="govuk-fieldset__legend--xl">
    ...
</GovUkFieldset>
```

### With Aria Described By

```razor
<div id="address-hint" class="govuk-hint">
    Enter your UK address
</div>

<GovUkFieldset Legend="Address" AriaDescribedBy="address-hint">
    <GovUkInput Id="address-1" Label="Address line 1" @bind-Value="line1" />
    <GovUkInput Id="postcode" Label="Postcode" @bind-Value="postcode" />
</GovUkFieldset>
```

### Without Legend

```razor
<GovUkFieldset>
    @* Content without visible legend *@
</GovUkFieldset>
```

## When to Use

Use fieldsets to:
- Group related inputs (like address fields)
- Wrap radio button or checkbox groups (though these components include their own fieldset)
- Create logical form sections

## Built-in Fieldsets

These components include their own fieldset:
- `GovUkRadios` - Groups radio buttons
- `GovUkCheckboxes` - Groups checkboxes
- `GovUkDateInput` - Groups date fields

## Accessibility

- Uses native `<fieldset>` and `<legend>` elements
- Screen readers announce the legend when entering the fieldset
- Groups related inputs for better navigation
- `aria-describedby` can link to additional instructions

## For AI Coding Agents

When implementing fieldsets:

1. Use when you have multiple related form fields
2. Don't nest fieldsets inside other fieldsets
3. Use `IsPageHeading="true"` when legend is the page question
4. Radio/checkbox groups already have fieldsets built-in
5. Use `LegendCssClass` to control legend size

```razor
// Standard fieldset pattern
<GovUkFieldset Legend="Section title">
    <GovUkInput Id="field-1" Label="Field 1" @bind-Value="value1" />
    <GovUkInput Id="field-2" Label="Field 2" @bind-Value="value2" />
</GovUkFieldset>

// As page heading (common for single-question pages)
<GovUkFieldset 
    Legend="What is your address?"
    LegendCssClass="govuk-fieldset__legend--l"
    IsPageHeading="true">
    <GovUkInput Id="line1" Label="Building and street" @bind-Value="line1" />
    <GovUkInput Id="city" Label="Town or city" @bind-Value="city" />
    <GovUkInput Id="postcode" Label="Postcode" Width="10" @bind-Value="postcode" />
</GovUkFieldset>

// DON'T do this - radios/checkboxes have built-in fieldsets
<GovUkFieldset Legend="Options">
    <GovUkRadios Name="choice" Legend="Choose one">  @* This creates nested fieldsets! *@
        ...
    </GovUkRadios>
</GovUkFieldset>
```

Address fieldset pattern:
```razor
<GovUkFieldset Legend="Address" LegendCssClass="govuk-fieldset__legend--m">
    <GovUkInput Id="address-line-1" Label="Address line 1" @bind-Value="line1" />
    <GovUkInput Id="address-line-2" Label="Address line 2 (optional)" @bind-Value="line2" />
    <GovUkInput Id="town" Label="Town or city" @bind-Value="town" />
    <GovUkInput Id="county" Label="County (optional)" @bind-Value="county" />
    <GovUkInput Id="postcode" Label="Postcode" Width="10" @bind-Value="postcode" />
</GovUkFieldset>
```
