# Button Component

Use the button component to help users carry out an action like starting an application or saving their information.

## Basic Usage

```razor
@using Blazor.DesignSystem.Components

<GovUkButton Text="Save and continue" OnClick="HandleClick" />

@code {
    private void HandleClick()
    {
        // Handle button click
    }
}
```

## Parameters

| Parameter | Type | Default | Description |
|-----------|------|---------|-------------|
| `Text` | `string?` | `null` | The button text |
| `ChildContent` | `RenderFragment?` | `null` | Custom button content (overrides Text) |
| `CssClass` | `string?` | `null` | Additional CSS classes |
| `Href` | `string?` | `null` | If set, renders as a link |
| `Type` | `string` | `"submit"` | Button type (button, submit, reset) |
| `Disabled` | `bool` | `false` | Whether the button is disabled |
| `IsSecondary` | `bool` | `false` | Use secondary button styling |
| `IsWarning` | `bool` | `false` | Use warning button styling |
| `IsStart` | `bool` | `false` | Use start button styling with arrow |
| `OnClick` | `EventCallback<MouseEventArgs>` | - | Click event callback |
| `AdditionalAttributes` | `Dictionary<string, object>?` | `null` | Additional HTML attributes |

## Examples

### Primary Button (Default)

```razor
<GovUkButton Text="Save and continue" OnClick="HandleSave" />
```

### Secondary Button

Use for actions that are not the primary action:

```razor
<GovUkButton Text="Find address" IsSecondary="true" />
```

### Warning Button

Use for potentially destructive actions:

```razor
<GovUkButton Text="Delete account" IsWarning="true" />
```

### Disabled Button

```razor
<GovUkButton Text="Submit" Disabled="true" />
```

### Start Button

Use at the beginning of a service or journey:

```razor
<GovUkButton Text="Start now" IsStart="true" Href="/start" />
```

### Button as Link

When you need button styling on a navigation element:

```razor
<GovUkButton Text="Go to homepage" Href="/" />
```

### Button Group

Group related buttons together:

```razor
<div class="govuk-button-group">
    <GovUkButton Text="Save and continue" />
    <GovUkButton Text="Save as draft" IsSecondary="true" />
</div>
```

### Custom Button Content

```razor
<GovUkButton>
    <span>Custom content with <strong>bold text</strong></span>
</GovUkButton>
```

### Button with Different Type

```razor
@* Regular button (not submit) *@
<GovUkButton Text="Calculate" Type="button" OnClick="Calculate" />

@* Submit button (default) *@
<GovUkButton Text="Submit form" />

@* Reset button *@
<GovUkButton Text="Reset" Type="reset" IsSecondary="true" />
```

### Preventing Double Submission

```razor
<GovUkButton 
    Text="@(isSubmitting ? "Submitting..." : "Submit")" 
    Disabled="@isSubmitting"
    OnClick="HandleSubmit" />

@code {
    private bool isSubmitting = false;
    
    private async Task HandleSubmit()
    {
        isSubmitting = true;
        StateHasChanged();
        
        try
        {
            await SubmitFormAsync();
        }
        finally
        {
            isSubmitting = false;
        }
    }
}
```

## Styling Combinations

Do not combine multiple styling modifiers:

```razor
@* WRONG - don't do this *@
<GovUkButton Text="Bad" IsSecondary="true" IsWarning="true" />

@* CORRECT - use only one modifier *@
<GovUkButton Text="Good" IsWarning="true" />
```

## Accessibility

- Uses proper `<button>` element with `type` attribute
- Disabled buttons use both `disabled` and `aria-disabled`
- Link buttons use `role="button"` and `draggable="false"`
- Start button includes an arrow icon with `aria-hidden="true"`

## For AI Coding Agents

When implementing buttons:

1. Use primary buttons (default) for the main action on a page
2. Use secondary buttons for less important actions
3. Use warning buttons only for destructive actions
4. Start buttons should link to the beginning of a service
5. For form submission, the default `Type="submit"` is correct
6. For non-form actions, set `Type="button"`
7. Implement disabled states during async operations

```razor
// Form with submit button
<EditForm Model="model" OnValidSubmit="HandleSubmit">
    <GovUkInput @bind-Value="model.Name" Label="Name" />
    <GovUkButton Text="Continue" />
</EditForm>

// Non-form action button
<GovUkButton 
    Text="Calculate" 
    Type="button" 
    OnClick="CalculateTotal" />

// Navigation button
<GovUkButton Text="Start" IsStart="true" Href="/start" />

// Button group pattern
<div class="govuk-button-group">
    <GovUkButton Text="Save and continue" />
    <GovUkButton Text="Cancel" IsSecondary="true" OnClick="Cancel" />
</div>
```

### Common Click Handler Pattern

```razor
@rendermode InteractiveServer

<GovUkButton Text="Process" Type="button" OnClick="Process" />

@code {
    private async Task Process()
    {
        // Async processing
    }
}
```
