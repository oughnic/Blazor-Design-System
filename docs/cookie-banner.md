# Cookie Banner Component

Use the cookie banner component to tell users about cookies on your service and let them accept or reject them.

> ⚠️ **Note**: Use `@rendermode InteractiveServer` or `@rendermode InteractiveWebAssembly` for interactive functionality.

## Components

| Component | Description |
|-----------|-------------|
| `GovUkCookieBanner` | The main cookie banner container |
| `GovUkCookieBannerMessage` | Individual messages within the banner |

## Basic Usage

```razor
@rendermode InteractiveServer
@using Blazor.DesignSystem.Components

<GovUkCookieBanner @bind-IsHidden="cookiesAccepted">
    <GovUkCookieBannerMessage Heading="Cookies on this service">
        <p class="govuk-body">We use cookies to collect information about how you use this service.</p>
        <Actions>
            <GovUkButton Text="Accept analytics cookies" OnClick="AcceptCookies" />
            <GovUkButton Text="Reject analytics cookies" IsSecondary="true" OnClick="RejectCookies" />
            <a class="govuk-link" href="/cookies">View cookies</a>
        </Actions>
    </GovUkCookieBannerMessage>
</GovUkCookieBanner>

@code {
    private bool cookiesAccepted = false;
    
    private void AcceptCookies() => cookiesAccepted = true;
    private void RejectCookies() => cookiesAccepted = true;
}
```

## GovUkCookieBanner Parameters

| Parameter | Type | Default | Description |
|-----------|------|---------|-------------|
| `AriaLabel` | `string` | `"Cookies on this service"` | Aria label for the region |
| `IsHidden` | `bool` | `false` | Whether banner is hidden |
| `IsHiddenChanged` | `EventCallback<bool>` | - | Hidden state change callback |
| `CssClass` | `string?` | `null` | Additional CSS classes |
| `ChildContent` | `RenderFragment?` | `null` | The banner messages |

## GovUkCookieBannerMessage Parameters

| Parameter | Type | Default | Description |
|-----------|------|---------|-------------|
| `Heading` | `string?` | `null` | Message heading |
| `ChildContent` | `RenderFragment?` | `null` | Message content |
| `Actions` | `RenderFragment?` | `null` | Action buttons |
| `CssClass` | `string?` | `null` | Additional CSS classes |
| `Hidden` | `bool` | `false` | Whether message is hidden |
| `AdditionalAttributes` | `Dictionary<string, object>?` | `null` | Additional HTML attributes |

## Examples

### Complete Cookie Banner Flow

```razor
@rendermode InteractiveServer

<GovUkCookieBanner>
    @if (!cookieChoiceMade)
    {
        <GovUkCookieBannerMessage Heading="Cookies on this service">
            <p class="govuk-body">We use some essential cookies to make this service work.</p>
            <p class="govuk-body">We'd also like to use analytics cookies so we can understand how you use the service.</p>
            <Actions>
                <GovUkButton Text="Accept analytics cookies" OnClick="AcceptAll" />
                <GovUkButton Text="Reject analytics cookies" IsSecondary="true" OnClick="RejectAll" />
                <a class="govuk-link" href="/cookies">View cookies</a>
            </Actions>
        </GovUkCookieBannerMessage>
    }
    else if (showConfirmation)
    {
        <GovUkCookieBannerMessage Hidden="@(!showConfirmation)">
            <p class="govuk-body">
                You've @(acceptedAnalytics ? "accepted" : "rejected") analytics cookies. 
                You can <a class="govuk-link" href="/cookies">change your cookie settings</a> at any time.
            </p>
            <Actions>
                <GovUkButton Text="Hide cookie message" IsSecondary="true" OnClick="HideBanner" />
            </Actions>
        </GovUkCookieBannerMessage>
    }
</GovUkCookieBanner>

@code {
    private bool cookieChoiceMade = false;
    private bool showConfirmation = false;
    private bool acceptedAnalytics = false;
    
    private void AcceptAll()
    {
        acceptedAnalytics = true;
        cookieChoiceMade = true;
        showConfirmation = true;
        // Set cookie preference
    }
    
    private void RejectAll()
    {
        acceptedAnalytics = false;
        cookieChoiceMade = true;
        showConfirmation = true;
        // Set cookie preference
    }
    
    private void HideBanner()
    {
        showConfirmation = false;
    }
}
```

### With Custom Aria Label

```razor
<GovUkCookieBanner AriaLabel="Cookie preferences for Example Service">
    <GovUkCookieBannerMessage Heading="Cookies">
        <p class="govuk-body">This site uses cookies.</p>
        <Actions>
            <GovUkButton Text="Accept" OnClick="Accept" />
        </Actions>
    </GovUkCookieBannerMessage>
</GovUkCookieBanner>
```

### Programmatic Show/Hide

```razor
<GovUkCookieBanner @ref="cookieBanner">
    <GovUkCookieBannerMessage Heading="Cookies">
        <p class="govuk-body">We use cookies on this site.</p>
        <Actions>
            <GovUkButton Text="OK" OnClick="HideBanner" />
        </Actions>
    </GovUkCookieBannerMessage>
</GovUkCookieBanner>

@code {
    private GovUkCookieBanner cookieBanner = default!;
    
    private async Task HideBanner()
    {
        await cookieBanner.Hide();
    }
}
```

## Placement

The cookie banner should appear at the top of the page, before the skip link and header:

```razor
<body>
    <GovUkCookieBanner>...</GovUkCookieBanner>
    <GovUkSkipLink />
    <GovUkHeader>...</GovUkHeader>
    ...
</body>
```

## Accessibility

- Uses `role="region"` with `aria-label`
- Banner should be at the start of the document
- Focus management should be considered when banner is dismissed

## For AI Coding Agents

When implementing cookie banners:

1. Place at the very top of the page structure
2. Implement both accept and reject options
3. Show a confirmation message after choice
4. Provide a link to full cookie policy
5. Remember preference (e.g., localStorage or cookies)
6. Hide banner after user makes a choice

```razor
@rendermode InteractiveServer

// Standard implementation pattern
<GovUkCookieBanner @bind-IsHidden="isHidden">
    @if (!hasChosen)
    {
        <GovUkCookieBannerMessage Heading="Cookies on this service">
            <p class="govuk-body">We use cookies...</p>
            <Actions>
                <GovUkButton Text="Accept" OnClick="Accept" />
                <GovUkButton Text="Reject" IsSecondary="true" OnClick="Reject" />
            </Actions>
        </GovUkCookieBannerMessage>
    }
    else
    {
        <GovUkCookieBannerMessage>
            <p class="govuk-body">You've made your choice.</p>
            <Actions>
                <GovUkButton Text="Hide" IsSecondary="true" OnClick="Hide" />
            </Actions>
        </GovUkCookieBannerMessage>
    }
</GovUkCookieBanner>

@code {
    private bool isHidden = false;
    private bool hasChosen = false;
    
    private void Accept() { hasChosen = true; /* Set cookie */ }
    private void Reject() { hasChosen = true; /* Set cookie */ }
    private void Hide() { isHidden = true; }
}
```
