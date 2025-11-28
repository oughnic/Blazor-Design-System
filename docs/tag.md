# Tag Component

Use the tag component to show users the status of something.

## Basic Usage

```razor
@using Blazor.DesignSystem.Components

<GovUkTag Text="Completed" />
```

## Parameters

| Parameter | Type | Default | Description |
|-----------|------|---------|-------------|
| `Text` | `string` | **Required** | Tag text |
| `Color` | `string?` | `null` | Color variant |
| `CssClass` | `string?` | `null` | Additional CSS classes |
| `AdditionalAttributes` | `Dictionary<string, object>?` | `null` | Additional HTML attributes |

## Color Options

| Color | Use Case |
|-------|----------|
| (default/blue) | Active, in progress |
| `grey` | Inactive, not started |
| `green` | Success, completed |
| `turquoise` | Alternative success |
| `blue` | Default, active |
| `light-blue` | New, updated |
| `purple` | Received, sent |
| `pink` | Beta, experimental |
| `red` | Urgent, error |
| `orange` | Warning, declined |
| `yellow` | Pending, delayed |

## Examples

### Default Tag (Blue)

```razor
<GovUkTag Text="Active" />
```

### Colored Tags

```razor
<GovUkTag Text="Completed" Color="green" />
<GovUkTag Text="Pending" Color="yellow" />
<GovUkTag Text="Declined" Color="red" />
<GovUkTag Text="Inactive" Color="grey" />
```

### Status Examples

```razor
<GovUkTag Text="New" Color="light-blue" />
<GovUkTag Text="In progress" />
<GovUkTag Text="Awaiting review" Color="purple" />
<GovUkTag Text="Approved" Color="green" />
<GovUkTag Text="Rejected" Color="red" />
```

### In a Table

```razor
<GovUkTable Caption="Applications">
    <Head>
        <tr class="govuk-table__row">
            <th scope="col" class="govuk-table__header">Name</th>
            <th scope="col" class="govuk-table__header">Status</th>
        </tr>
    </Head>
    <ChildContent>
        <tr class="govuk-table__row">
            <td class="govuk-table__cell">John Smith</td>
            <td class="govuk-table__cell">
                <GovUkTag Text="Completed" Color="green" />
            </td>
        </tr>
        <tr class="govuk-table__row">
            <td class="govuk-table__cell">Jane Doe</td>
            <td class="govuk-table__cell">
                <GovUkTag Text="Pending" Color="yellow" />
            </td>
        </tr>
    </ChildContent>
</GovUkTable>
```

### In Summary List

```razor
<GovUkSummaryList>
    <GovUkSummaryListRow>
        <Key>Application status</Key>
        <Value>
            <GovUkTag Text="Approved" Color="green" />
        </Value>
    </GovUkSummaryListRow>
</GovUkSummaryList>
```

### Multiple Tags

```razor
<ul class="govuk-list">
    <li><GovUkTag Text="Alpha" Color="pink" /> - Testing phase</li>
    <li><GovUkTag Text="Beta" Color="pink" /> - Public testing</li>
    <li><GovUkTag Text="Live" Color="green" /> - Full release</li>
</ul>
```

## Accessibility

- Uses `<strong>` element for semantic emphasis
- Color alone should not convey meaning
- Text should clearly indicate the status

## For AI Coding Agents

When implementing tags:

1. Use consistent colors for the same status across your service
2. Choose colors that match the meaning (green=positive, red=negative)
3. Keep tag text short (1-2 words)
4. Don't rely on color alone - text should convey meaning
5. Use grey for inactive/neutral states

```razor
// Status patterns
<GovUkTag Text="Not started" Color="grey" />
<GovUkTag Text="In progress" />
<GovUkTag Text="Completed" Color="green" />
<GovUkTag Text="Rejected" Color="red" />

// Common use in tables
<td class="govuk-table__cell">
    <GovUkTag Text="@item.Status" Color="@GetStatusColor(item.Status)" />
</td>

@code {
    private string? GetStatusColor(string status) => status switch
    {
        "Completed" => "green",
        "Pending" => "yellow",
        "Rejected" => "red",
        "Not started" => "grey",
        _ => null // default blue
    };
}
```
