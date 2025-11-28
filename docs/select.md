# Select Component

Use the select component (dropdown) to let users choose an option from a long list.

> ⚠️ **Note**: Use `@rendermode InteractiveServer` or `@rendermode InteractiveWebAssembly` for interactive functionality.

## Basic Usage

```razor
@rendermode InteractiveServer
@using Blazor.DesignSystem.Components

<GovUkSelect 
    Id="sort"
    Label="Sort by"
    @bind-Value="sortOrder">
    <option value="">Select an option</option>
    <option value="newest">Newest first</option>
    <option value="oldest">Oldest first</option>
    <option value="name">Name A-Z</option>
</GovUkSelect>

@code {
    private string? sortOrder;
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
| `Value` | `string?` | `null` | Currently selected value |
| `ValueChanged` | `EventCallback<string?>` | - | Value change callback |
| `Disabled` | `bool` | `false` | Whether disabled |
| `HasError` | `bool` | `false` | Whether has error |
| `ErrorMessage` | `string?` | `null` | Error message |
| `ChildContent` | `RenderFragment?` | `null` | Option elements |
| `CssClass` | `string?` | `null` | CSS for form group |
| `SelectCssClass` | `string?` | `null` | CSS for select |
| `AdditionalAttributes` | `Dictionary<string, object>?` | `null` | Additional attributes |

## Examples

### With Hint

```razor
<GovUkSelect 
    Id="country"
    Label="Country"
    Hint="Select the country where you live"
    @bind-Value="country">
    <option value="">Select a country</option>
    <option value="england">England</option>
    <option value="scotland">Scotland</option>
    <option value="wales">Wales</option>
    <option value="northern-ireland">Northern Ireland</option>
</GovUkSelect>
```

### With Error

```razor
<GovUkSelect 
    Id="location"
    Label="Location"
    HasError="true"
    ErrorMessage="Select a location"
    @bind-Value="location">
    <option value="">Select a location</option>
    <option value="london">London</option>
    <option value="manchester">Manchester</option>
</GovUkSelect>
```

### With Pre-selected Value

```razor
<GovUkSelect 
    Id="year"
    Label="Year"
    Value="2024"
    @bind-Value="year">
    <option value="2023">2023</option>
    <option value="2024">2024</option>
    <option value="2025">2025</option>
</GovUkSelect>
```

### With Dynamic Options

```razor
<GovUkSelect 
    Id="category"
    Label="Category"
    @bind-Value="selectedCategory">
    <option value="">Select a category</option>
    @foreach (var category in categories)
    {
        <option value="@category.Id">@category.Name</option>
    }
</GovUkSelect>

@code {
    private string? selectedCategory;
    private List<Category> categories = new()
    {
        new("1", "Category 1"),
        new("2", "Category 2"),
        new("3", "Category 3")
    };
    
    public record Category(string Id, string Name);
}
```

### Disabled Select

```razor
<GovUkSelect 
    Id="status"
    Label="Status"
    Disabled="true"
    Value="active">
    <option value="active">Active</option>
    <option value="inactive">Inactive</option>
</GovUkSelect>
```

## When to Use

Use select when:
- Users need to choose from a long list (more than 5-6 options)
- The options don't need explanation
- Options can be presented in a logical order

Consider radios instead when:
- There are fewer than 6 options
- Users need to see all options at once
- Options need additional explanation

## Accessibility

- Label linked to select via `for` attribute
- Hint and error linked via `aria-describedby`
- Native select element is keyboard accessible
- Error prefix "Error:" is visually hidden

## For AI Coding Agents

When implementing selects:

1. Always include a blank/prompt option as first choice
2. Use `@bind-Value` for two-way data binding
3. Consider radios for fewer than 6 options
4. Validate selection on form submission
5. Options should be in a logical order (alphabetical, numerical)

```razor
@rendermode InteractiveServer

// Standard pattern
<GovUkSelect 
    Id="unique-id"
    Label="Select label"
    HasError="@hasError"
    ErrorMessage="Select an option"
    @bind-Value="selectedValue">
    <option value="">Select an option</option>
    <option value="1">Option 1</option>
    <option value="2">Option 2</option>
</GovUkSelect>

@code {
    private string? selectedValue;
    private bool hasError = false;
    
    private void Validate()
    {
        hasError = string.IsNullOrEmpty(selectedValue);
    }
}

// With dynamic options
<GovUkSelect 
    Id="items"
    Label="Choose an item"
    @bind-Value="selectedItem">
    <option value="">Please select</option>
    @foreach (var item in items)
    {
        <option value="@item.Value">@item.Text</option>
    }
</GovUkSelect>
```
