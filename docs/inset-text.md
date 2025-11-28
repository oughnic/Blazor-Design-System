# Inset Text Component

Use the inset text component to differentiate a block of text from surrounding content.

## Basic Usage

```razor
@using Blazor.DesignSystem.Components

<GovUkInsetText>
    It can take up to 8 weeks to register a lasting power of attorney if there are no mistakes in the application.
</GovUkInsetText>
```

## Parameters

| Parameter | Type | Default | Description |
|-----------|------|---------|-------------|
| `ChildContent` | `RenderFragment?` | `null` | The inset text content |
| `CssClass` | `string?` | `null` | Additional CSS classes |
| `AdditionalAttributes` | `Dictionary<string, object>?` | `null` | Additional HTML attributes |

## Examples

### Basic Inset Text

```razor
<GovUkInsetText>
    You need to apply by 31 March to get your refund.
</GovUkInsetText>
```

### With Multiple Paragraphs

```razor
<GovUkInsetText>
    <p class="govuk-body">You'll need to provide:</p>
    <ul class="govuk-list govuk-list--bullet">
        <li>your passport number</li>
        <li>your National Insurance number</li>
    </ul>
</GovUkInsetText>
```

### With Custom Styling

```razor
<GovUkInsetText CssClass="govuk-!-margin-bottom-8">
    Important information that needs extra spacing below.
</GovUkInsetText>
```

## When to Use

Use inset text to:
- Highlight important information
- Provide key facts or instructions
- Draw attention to deadlines or requirements

Do not use inset text:
- For quotes (use a blockquote)
- For warnings (use warning text component)
- For errors (use error components)

## Accessibility

- Uses standard `<div>` element
- Visual styling (left border) provides visual distinction
- Content is read in normal document flow

## For AI Coding Agents

When implementing inset text:

1. Use for important but non-critical information
2. Keep content concise
3. Can contain multiple elements (paragraphs, lists)
4. Do not overuse - loses impact if used too frequently

```razor
// Basic usage
<GovUkInsetText>
    Key information for the user.
</GovUkInsetText>

// With structured content
<GovUkInsetText>
    <p class="govuk-body">You will need:</p>
    <ul class="govuk-list govuk-list--bullet">
        <li>Item one</li>
        <li>Item two</li>
    </ul>
</GovUkInsetText>
```
