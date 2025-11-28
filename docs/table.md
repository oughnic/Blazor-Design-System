# Table Component

Use the table component to make information easier to compare and scan.

## Basic Usage

```razor
@using Blazor.DesignSystem.Components

<GovUkTable Caption="Monthly expenses">
    <Head>
        <tr class="govuk-table__row">
            <th scope="col" class="govuk-table__header">Month</th>
            <th scope="col" class="govuk-table__header">Amount</th>
        </tr>
    </Head>
    <ChildContent>
        <tr class="govuk-table__row">
            <td class="govuk-table__cell">January</td>
            <td class="govuk-table__cell">£85</td>
        </tr>
        <tr class="govuk-table__row">
            <td class="govuk-table__cell">February</td>
            <td class="govuk-table__cell">£95</td>
        </tr>
    </ChildContent>
</GovUkTable>
```

## Parameters

| Parameter | Type | Default | Description |
|-----------|------|---------|-------------|
| `Caption` | `string?` | `null` | Table caption |
| `CaptionCssClass` | `string?` | `null` | CSS for caption |
| `Head` | `RenderFragment?` | `null` | Table header rows |
| `ChildContent` | `RenderFragment?` | `null` | Table body rows |
| `CssClass` | `string?` | `null` | Additional CSS |
| `AdditionalAttributes` | `Dictionary<string, object>?` | `null` | Additional attributes |

## Examples

### Basic Table

```razor
<GovUkTable Caption="Dates and amounts">
    <Head>
        <tr class="govuk-table__row">
            <th scope="col" class="govuk-table__header">Date</th>
            <th scope="col" class="govuk-table__header">Amount</th>
            <th scope="col" class="govuk-table__header">Status</th>
        </tr>
    </Head>
    <ChildContent>
        <tr class="govuk-table__row">
            <td class="govuk-table__cell">1 Jan 2024</td>
            <td class="govuk-table__cell">£100.00</td>
            <td class="govuk-table__cell">Paid</td>
        </tr>
        <tr class="govuk-table__row">
            <td class="govuk-table__cell">1 Feb 2024</td>
            <td class="govuk-table__cell">£150.00</td>
            <td class="govuk-table__cell">Pending</td>
        </tr>
    </ChildContent>
</GovUkTable>
```

### With Row Headers

```razor
<GovUkTable Caption="Services by region">
    <Head>
        <tr class="govuk-table__row">
            <th scope="col" class="govuk-table__header">Region</th>
            <th scope="col" class="govuk-table__header govuk-table__header--numeric">Population</th>
            <th scope="col" class="govuk-table__header govuk-table__header--numeric">Services</th>
        </tr>
    </Head>
    <ChildContent>
        <tr class="govuk-table__row">
            <th scope="row" class="govuk-table__header">North</th>
            <td class="govuk-table__cell govuk-table__cell--numeric">5,000,000</td>
            <td class="govuk-table__cell govuk-table__cell--numeric">42</td>
        </tr>
        <tr class="govuk-table__row">
            <th scope="row" class="govuk-table__header">South</th>
            <td class="govuk-table__cell govuk-table__cell--numeric">8,000,000</td>
            <td class="govuk-table__cell govuk-table__cell--numeric">67</td>
        </tr>
    </ChildContent>
</GovUkTable>
```

### With Numeric Alignment

```razor
<GovUkTable Caption="Budget summary">
    <Head>
        <tr class="govuk-table__row">
            <th scope="col" class="govuk-table__header">Category</th>
            <th scope="col" class="govuk-table__header govuk-table__header--numeric">Budget</th>
            <th scope="col" class="govuk-table__header govuk-table__header--numeric">Spent</th>
        </tr>
    </Head>
    <ChildContent>
        <tr class="govuk-table__row">
            <td class="govuk-table__cell">Personnel</td>
            <td class="govuk-table__cell govuk-table__cell--numeric">£50,000</td>
            <td class="govuk-table__cell govuk-table__cell--numeric">£48,500</td>
        </tr>
    </ChildContent>
</GovUkTable>
```

### Dynamic Data

```razor
<GovUkTable Caption="User list">
    <Head>
        <tr class="govuk-table__row">
            <th scope="col" class="govuk-table__header">Name</th>
            <th scope="col" class="govuk-table__header">Email</th>
            <th scope="col" class="govuk-table__header">Role</th>
        </tr>
    </Head>
    <ChildContent>
        @foreach (var user in users)
        {
            <tr class="govuk-table__row">
                <td class="govuk-table__cell">@user.Name</td>
                <td class="govuk-table__cell">@user.Email</td>
                <td class="govuk-table__cell">@user.Role</td>
            </tr>
        }
    </ChildContent>
</GovUkTable>

@code {
    private List<User> users = new()
    {
        new("John Smith", "john@example.com", "Admin"),
        new("Jane Doe", "jane@example.com", "User")
    };
    
    public record User(string Name, string Email, string Role);
}
```

### Caption Sizes

```razor
<GovUkTable Caption="Small caption" CaptionCssClass="govuk-table__caption--s">
    ...
</GovUkTable>

<GovUkTable Caption="Medium caption" CaptionCssClass="govuk-table__caption--m">
    ...
</GovUkTable>

<GovUkTable Caption="Large caption" CaptionCssClass="govuk-table__caption--l">
    ...
</GovUkTable>
```

## CSS Classes Reference

| Class | Purpose |
|-------|---------|
| `govuk-table__header--numeric` | Right-align header for numbers |
| `govuk-table__cell--numeric` | Right-align cell for numbers |
| `govuk-table__caption--s/m/l` | Caption size |

## Accessibility

- Use `<caption>` for table title
- Use `scope="col"` for column headers
- Use `scope="row"` for row headers
- Right-align numeric data for easier comparison

## For AI Coding Agents

When implementing tables:

1. Always include a caption describing the table
2. Use `scope` attributes on header cells
3. Right-align numeric data
4. Use row headers when first column identifies the row
5. Keep tables simple - avoid complex merged cells

```razor
// Standard table pattern
<GovUkTable Caption="Table description">
    <Head>
        <tr class="govuk-table__row">
            <th scope="col" class="govuk-table__header">Column 1</th>
            <th scope="col" class="govuk-table__header">Column 2</th>
        </tr>
    </Head>
    <ChildContent>
        @foreach (var item in items)
        {
            <tr class="govuk-table__row">
                <td class="govuk-table__cell">@item.Col1</td>
                <td class="govuk-table__cell">@item.Col2</td>
            </tr>
        }
    </ChildContent>
</GovUkTable>

// With numeric data
<th scope="col" class="govuk-table__header govuk-table__header--numeric">Amount</th>
<td class="govuk-table__cell govuk-table__cell--numeric">£100</td>
```
