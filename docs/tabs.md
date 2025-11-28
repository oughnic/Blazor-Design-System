# Tabs Component

The tabs component lets users navigate between related sections of content.

> ⚠️ **Note**: Use `@rendermode InteractiveServer` or `@rendermode InteractiveWebAssembly` for interactive functionality.

## Components

| Component | Description |
|-----------|-------------|
| `GovUkTabs` | The main tabs container |
| `GovUkTab` | Individual tab headers |
| `GovUkTabPanel` | Tab content panels |

## Basic Usage

```razor
@rendermode InteractiveServer
@using Blazor.DesignSystem.Components

<GovUkTabs>
    <TabList>
        <GovUkTab Id="past-day" Label="Past day" />
        <GovUkTab Id="past-week" Label="Past week" />
        <GovUkTab Id="past-month" Label="Past month" />
    </TabList>
    <TabPanels>
        <GovUkTabPanel Id="past-day">
            <h2 class="govuk-heading-l">Past day</h2>
            <p class="govuk-body">Content for past day.</p>
        </GovUkTabPanel>
        <GovUkTabPanel Id="past-week">
            <h2 class="govuk-heading-l">Past week</h2>
            <p class="govuk-body">Content for past week.</p>
        </GovUkTabPanel>
        <GovUkTabPanel Id="past-month">
            <h2 class="govuk-heading-l">Past month</h2>
            <p class="govuk-body">Content for past month.</p>
        </GovUkTabPanel>
    </TabPanels>
</GovUkTabs>
```

## GovUkTabs Parameters

| Parameter | Type | Default | Description |
|-----------|------|---------|-------------|
| `Title` | `string` | `"Contents"` | Screen reader title |
| `TabList` | `RenderFragment?` | `null` | Tab items |
| `TabPanels` | `RenderFragment?` | `null` | Tab panels |
| `CssClass` | `string?` | `null` | Additional CSS |
| `AdditionalAttributes` | `Dictionary<string, object>?` | `null` | Additional attributes |

## GovUkTab Parameters

| Parameter | Type | Default | Description |
|-----------|------|---------|-------------|
| `Id` | `string` | **Required** | Unique identifier (must match panel) |
| `Label` | `string` | **Required** | Tab label text |

## GovUkTabPanel Parameters

| Parameter | Type | Default | Description |
|-----------|------|---------|-------------|
| `Id` | `string` | **Required** | Unique identifier (must match tab) |
| `ChildContent` | `RenderFragment?` | `null` | Panel content |
| `CssClass` | `string?` | `null` | Additional CSS |
| `AdditionalAttributes` | `Dictionary<string, object>?` | `null` | Additional attributes |

## Examples

### Case History Tabs

```razor
<GovUkTabs Title="Case history">
    <TabList>
        <GovUkTab Id="case-info" Label="Case information" />
        <GovUkTab Id="documents" Label="Documents" />
        <GovUkTab Id="history" Label="History" />
    </TabList>
    <TabPanels>
        <GovUkTabPanel Id="case-info">
            <h2 class="govuk-heading-l">Case information</h2>
            <GovUkSummaryList>
                <GovUkSummaryListRow>
                    <Key>Reference</Key>
                    <Value>ABC123</Value>
                </GovUkSummaryListRow>
            </GovUkSummaryList>
        </GovUkTabPanel>
        <GovUkTabPanel Id="documents">
            <h2 class="govuk-heading-l">Documents</h2>
            <ul class="govuk-list">
                <li><a class="govuk-link" href="#">Application form</a></li>
                <li><a class="govuk-link" href="#">Supporting evidence</a></li>
            </ul>
        </GovUkTabPanel>
        <GovUkTabPanel Id="history">
            <h2 class="govuk-heading-l">History</h2>
            <p class="govuk-body">Timeline of events...</p>
        </GovUkTabPanel>
    </TabPanels>
</GovUkTabs>
```

### With Tables

```razor
<GovUkTabs>
    <TabList>
        <GovUkTab Id="uk" Label="UK" />
        <GovUkTab Id="europe" Label="Europe" />
        <GovUkTab Id="world" Label="World" />
    </TabList>
    <TabPanels>
        <GovUkTabPanel Id="uk">
            <GovUkTable Caption="UK Statistics">
                <Head>
                    <tr class="govuk-table__row">
                        <th scope="col" class="govuk-table__header">Item</th>
                        <th scope="col" class="govuk-table__header govuk-table__header--numeric">Value</th>
                    </tr>
                </Head>
                <ChildContent>
                    <tr class="govuk-table__row">
                        <td class="govuk-table__cell">Population</td>
                        <td class="govuk-table__cell govuk-table__cell--numeric">67M</td>
                    </tr>
                </ChildContent>
            </GovUkTable>
        </GovUkTabPanel>
        @* Other panels *@
    </TabPanels>
</GovUkTabs>
```

## Important Notes

- Tab `Id` must match the corresponding `GovUkTabPanel` `Id`
- First tab is automatically selected on load
- Only one panel is visible at a time

## Accessibility

- Uses `role="tablist"`, `role="tab"`, and `role="tabpanel"`
- `aria-selected` indicates active tab
- `aria-controls` links tab to panel
- `aria-labelledby` links panel to tab
- Keyboard navigation supported

## For AI Coding Agents

When implementing tabs:

1. Tab `Id` must exactly match panel `Id`
2. First tab is automatically active
3. Use `@rendermode InteractiveServer` for interactivity
4. Each panel should have a heading matching the tab
5. Keep tab labels short

```razor
@rendermode InteractiveServer

// Standard pattern
<GovUkTabs>
    <TabList>
        <GovUkTab Id="tab-1" Label="Tab 1" />
        <GovUkTab Id="tab-2" Label="Tab 2" />
    </TabList>
    <TabPanels>
        <GovUkTabPanel Id="tab-1">
            <h2 class="govuk-heading-l">Tab 1</h2>
            <p class="govuk-body">Content for tab 1</p>
        </GovUkTabPanel>
        <GovUkTabPanel Id="tab-2">
            <h2 class="govuk-heading-l">Tab 2</h2>
            <p class="govuk-body">Content for tab 2</p>
        </GovUkTabPanel>
    </TabPanels>
</GovUkTabs>
```

Note: IDs must match exactly between `GovUkTab` and `GovUkTabPanel`.
