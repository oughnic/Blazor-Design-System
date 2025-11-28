# Accordion Component

The accordion component lets users show and hide sections of related content on a page.

> ⚠️ **Note**: This component requires `@rendermode InteractiveServer` or `@rendermode InteractiveWebAssembly` for interactive functionality.

## Components

| Component | Description |
|-----------|-------------|
| `GovUkAccordion` | The main accordion container |
| `GovUkAccordionSection` | Individual sections within the accordion |

## Basic Usage

```razor
@page "/my-page"
@rendermode InteractiveServer
@using Blazor.DesignSystem.Components

<GovUkAccordion Id="my-accordion">
    <GovUkAccordionSection Heading="Section 1">
        <Content>
            <p class="govuk-body">Content for section 1.</p>
        </Content>
    </GovUkAccordionSection>
    
    <GovUkAccordionSection Heading="Section 2">
        <Content>
            <p class="govuk-body">Content for section 2.</p>
        </Content>
    </GovUkAccordionSection>
</GovUkAccordion>
```

## GovUkAccordion Parameters

| Parameter | Type | Default | Description |
|-----------|------|---------|-------------|
| `Id` | `string?` | Auto-generated GUID | The unique identifier for the accordion |
| `CssClass` | `string?` | `null` | Additional CSS classes to apply |
| `ChildContent` | `RenderFragment?` | `null` | The accordion sections |

## GovUkAccordionSection Parameters

| Parameter | Type | Default | Description |
|-----------|------|---------|-------------|
| `Heading` | `string` | **Required** | The heading text for the section |
| `Summary` | `string?` | `null` | Optional summary text below the heading |
| `Content` | `RenderFragment?` | `null` | The content to display when expanded |
| `IsExpanded` | `bool` | `false` | Whether the section is initially expanded |
| `IsExpandedChanged` | `EventCallback<bool>` | - | Callback when expanded state changes |

## Examples

### With Summary Text

```razor
<GovUkAccordion Id="summary-example">
    <GovUkAccordionSection 
        Heading="Understanding agile project management" 
        Summary="Introductory information about agile frameworks">
        <Content>
            <p class="govuk-body">Agile project management is an iterative approach.</p>
        </Content>
    </GovUkAccordionSection>
</GovUkAccordion>
```

### With Initial Section Expanded

```razor
<GovUkAccordion Id="expanded-example">
    <GovUkAccordionSection Heading="Section 1" IsExpanded="true">
        <Content>
            <p class="govuk-body">This section starts expanded.</p>
        </Content>
    </GovUkAccordionSection>
    
    <GovUkAccordionSection Heading="Section 2">
        <Content>
            <p class="govuk-body">This section starts collapsed.</p>
        </Content>
    </GovUkAccordionSection>
</GovUkAccordion>
```

### With Two-Way Binding on Expansion State

```razor
<GovUkAccordion Id="binding-example">
    <GovUkAccordionSection 
        Heading="Controlled Section" 
        @bind-IsExpanded="isSectionOpen">
        <Content>
            <p class="govuk-body">The expanded state can be tracked.</p>
        </Content>
    </GovUkAccordionSection>
</GovUkAccordion>

<p>Section is @(isSectionOpen ? "open" : "closed")</p>

@code {
    private bool isSectionOpen = false;
}
```

## Accessibility

- Each section heading is a button with `aria-expanded` and `aria-controls` attributes
- Content panels have `aria-labelledby` linking to their heading
- "Show all" / "Hide all" button updates `aria-expanded` based on state
- Hidden content uses the `hidden` attribute

## For AI Coding Agents

When implementing accordion functionality:

1. Always wrap in a parent component with `@rendermode InteractiveServer`
2. Use unique IDs for each accordion on a page
3. The `Heading` parameter on `GovUkAccordionSection` is required
4. Use the `Content` RenderFragment for section content, not `ChildContent`
5. For programmatic control, use `@bind-IsExpanded` on sections

```razor
// Correct usage pattern
<GovUkAccordionSection Heading="My Heading">
    <Content>
        <p>Content here</p>
    </Content>
</GovUkAccordionSection>

// NOT like this
<GovUkAccordionSection Heading="My Heading">
    <p>This won't work as expected</p>
</GovUkAccordionSection>
```
