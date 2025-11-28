# Details Component

Use the details component to make a page easier to scan by letting users reveal more detailed information only if they need it.

## Basic Usage

```razor
@using Blazor.DesignSystem.Components

<GovUkDetails Summary="Help with nationality">
    <p class="govuk-body">
        If you're not sure about your nationality, try to find out from an official document like a passport or national identity card.
    </p>
</GovUkDetails>
```

## Parameters

| Parameter | Type | Default | Description |
|-----------|------|---------|-------------|
| `Summary` | `string` | **Required** | The summary text shown when closed |
| `ChildContent` | `RenderFragment?` | `null` | The detail content |
| `CssClass` | `string?` | `null` | Additional CSS classes |
| `AdditionalAttributes` | `Dictionary<string, object>?` | `null` | Additional HTML attributes |

## Examples

### Basic Details

```razor
<GovUkDetails Summary="What is a National Insurance number?">
    <p class="govuk-body">
        It's on your National Insurance card, benefit letter, payslip or P60. 
        For example, 'QQ 12 34 56 C'.
    </p>
</GovUkDetails>
```

### With Multiple Paragraphs

```razor
<GovUkDetails Summary="More information about applying">
    <p class="govuk-body">
        Before you apply, you need to make sure you have all the required documents.
    </p>
    <p class="govuk-body">
        You will need:
    </p>
    <ul class="govuk-list govuk-list--bullet">
        <li>your passport</li>
        <li>proof of address</li>
        <li>bank statements from the last 3 months</li>
    </ul>
</GovUkDetails>
```

### Initially Open

Use the `open` attribute to make the details open by default:

```razor
<GovUkDetails Summary="Important information" open="true">
    <p class="govuk-body">
        This content is visible by default.
    </p>
</GovUkDetails>
```

### With Custom CSS

```razor
<GovUkDetails Summary="Contact details" CssClass="govuk-!-margin-bottom-8">
    <p class="govuk-body">
        Email: contact@example.gov.uk<br>
        Phone: 0300 123 4567
    </p>
</GovUkDetails>
```

### Multiple Details Components

```razor
<GovUkDetails Summary="How to prove your identity">
    <p class="govuk-body">You can use a passport, driving licence, or national identity card.</p>
</GovUkDetails>

<GovUkDetails Summary="What counts as proof of address">
    <p class="govuk-body">You can use a utility bill, bank statement, or council tax bill from the last 3 months.</p>
</GovUkDetails>

<GovUkDetails Summary="Help with documents from abroad">
    <p class="govuk-body">If your documents are not in English, you'll need to get them translated.</p>
</GovUkDetails>
```

## When to Use

Use the details component to:
- Make a page easier to scan
- Provide optional help text
- Show additional information that not all users need

Do not use the details component to:
- Hide information that most users need
- Replace proper page structure
- Collapse error messages

## Native HTML Element

The component uses the native HTML5 `<details>` and `<summary>` elements, which:
- Work without JavaScript
- Are accessible by default
- Have built-in keyboard support (Enter/Space to toggle)
- Remember state during page navigation in most browsers

## Accessibility

- Uses native `<details>` element for built-in accessibility
- Summary text is always visible and clickable
- Keyboard accessible (Enter or Space to expand/collapse)
- Screen readers announce the expanded/collapsed state

## For AI Coding Agents

When implementing details components:

1. Use for genuinely optional information
2. The `Summary` parameter is required
3. Use `ChildContent` for the hidden content
4. No interactivity directive needed (works without JS)
5. Keep summary text concise and descriptive
6. Content can include any HTML elements

```razor
// Standard pattern
<GovUkDetails Summary="Brief question or title">
    <p class="govuk-body">
        Detailed answer or information here.
    </p>
</GovUkDetails>

// With structured content
<GovUkDetails Summary="What you need to provide">
    <p class="govuk-body">You will need:</p>
    <ul class="govuk-list govuk-list--bullet">
        <li>Item one</li>
        <li>Item two</li>
        <li>Item three</li>
    </ul>
</GovUkDetails>

// With additional attributes
<GovUkDetails Summary="Open by default" open="true">
    <p class="govuk-body">This starts expanded.</p>
</GovUkDetails>
```

Note: The details component is not for hiding critical information. Users should be able to complete their task without opening the details.
