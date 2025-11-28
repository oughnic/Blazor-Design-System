# Summary List Component

Use the summary list to summarise information, such as a user's answers at the end of a form.

## Components

| Component | Description |
|-----------|-------------|
| `GovUkSummaryList` | The summary list container |
| `GovUkSummaryListRow` | Individual rows in the list |

## Basic Usage

```razor
@using Blazor.DesignSystem.Components

<GovUkSummaryList>
    <GovUkSummaryListRow>
        <Key>Name</Key>
        <Value>John Smith</Value>
    </GovUkSummaryListRow>
    <GovUkSummaryListRow>
        <Key>Date of birth</Key>
        <Value>5 January 1978</Value>
    </GovUkSummaryListRow>
</GovUkSummaryList>
```

## GovUkSummaryList Parameters

| Parameter | Type | Default | Description |
|-----------|------|---------|-------------|
| `ChildContent` | `RenderFragment?` | `null` | The summary list rows |
| `NoBorder` | `bool` | `false` | Hide borders |
| `CssClass` | `string?` | `null` | Additional CSS classes |
| `AdditionalAttributes` | `Dictionary<string, object>?` | `null` | Additional HTML attributes |

## GovUkSummaryListRow Parameters

| Parameter | Type | Default | Description |
|-----------|------|---------|-------------|
| `Key` | `RenderFragment?` | `null` | The key/label |
| `Value` | `RenderFragment?` | `null` | The value |
| `Actions` | `RenderFragment?` | `null` | Action links |
| `NoActions` | `bool` | `false` | Hide actions column |
| `CssClass` | `string?` | `null` | Additional CSS classes |

## Examples

### With Change Links

```razor
<GovUkSummaryList>
    <GovUkSummaryListRow>
        <Key>Name</Key>
        <Value>Sarah Philips</Value>
        <Actions>
            <a class="govuk-link" href="/change-name">
                Change<span class="govuk-visually-hidden"> name</span>
            </a>
        </Actions>
    </GovUkSummaryListRow>
    <GovUkSummaryListRow>
        <Key>Date of birth</Key>
        <Value>5 January 1978</Value>
        <Actions>
            <a class="govuk-link" href="/change-dob">
                Change<span class="govuk-visually-hidden"> date of birth</span>
            </a>
        </Actions>
    </GovUkSummaryListRow>
    <GovUkSummaryListRow>
        <Key>Contact information</Key>
        <Value>
            72 Guild Street<br>
            London<br>
            SE23 6FH
        </Value>
        <Actions>
            <a class="govuk-link" href="/change-contact">
                Change<span class="govuk-visually-hidden"> contact information</span>
            </a>
        </Actions>
    </GovUkSummaryListRow>
</GovUkSummaryList>
```

### Without Borders

```razor
<GovUkSummaryList NoBorder="true">
    <GovUkSummaryListRow>
        <Key>Name</Key>
        <Value>John Smith</Value>
    </GovUkSummaryListRow>
    <GovUkSummaryListRow>
        <Key>Email</Key>
        <Value>john@example.com</Value>
    </GovUkSummaryListRow>
</GovUkSummaryList>
```

### Without Actions

```razor
<GovUkSummaryList>
    <GovUkSummaryListRow NoActions="true">
        <Key>Reference</Key>
        <Value>HDJ2123F</Value>
    </GovUkSummaryListRow>
    <GovUkSummaryListRow NoActions="true">
        <Key>Status</Key>
        <Value>Submitted</Value>
    </GovUkSummaryListRow>
</GovUkSummaryList>
```

### Mixed Actions

```razor
<GovUkSummaryList>
    <GovUkSummaryListRow>
        <Key>Name</Key>
        <Value>John Smith</Value>
        <Actions>
            <a class="govuk-link" href="/change">Change</a>
        </Actions>
    </GovUkSummaryListRow>
    <GovUkSummaryListRow NoActions="true">
        <Key>Reference</Key>
        <Value>ABC123</Value>
    </GovUkSummaryListRow>
</GovUkSummaryList>
```

### Check Your Answers Page

```razor
<h1 class="govuk-heading-l">Check your answers</h1>

<h2 class="govuk-heading-m">Personal details</h2>
<GovUkSummaryList>
    <GovUkSummaryListRow>
        <Key>Name</Key>
        <Value>@model.Name</Value>
        <Actions>
            <a class="govuk-link" href="/name">
                Change<span class="govuk-visually-hidden"> name</span>
            </a>
        </Actions>
    </GovUkSummaryListRow>
    @* More rows *@
</GovUkSummaryList>

<h2 class="govuk-heading-m">Contact details</h2>
<GovUkSummaryList>
    @* Contact rows *@
</GovUkSummaryList>

<GovUkButton Text="Submit application" />
```

## Accessibility

- Uses `<dl>`, `<dt>`, and `<dd>` elements for semantic structure
- Action links should include visually hidden context text
- Consistent key-value structure aids screen reader navigation

## For AI Coding Agents

When implementing summary lists:

1. Use for "check your answers" pages
2. Include change links with visually hidden context
3. Use `NoActions="true"` for read-only values
4. Use `NoBorder="true"` for compact layouts

```razor
// Standard check answers pattern
<GovUkSummaryList>
    <GovUkSummaryListRow>
        <Key>Field label</Key>
        <Value>@model.FieldValue</Value>
        <Actions>
            <a class="govuk-link" href="/change-field">
                Change<span class="govuk-visually-hidden"> field label</span>
            </a>
        </Actions>
    </GovUkSummaryListRow>
</GovUkSummaryList>

// Read-only summary
<GovUkSummaryList>
    <GovUkSummaryListRow NoActions="true">
        <Key>Reference</Key>
        <Value>@reference</Value>
    </GovUkSummaryListRow>
</GovUkSummaryList>
```
