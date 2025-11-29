using Bunit;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Blazor.DesignSystem.Components;
using Blazor.DesignSystem.Components.Services;
using Xunit;

namespace Blazor.DesignSystem.Components.Tests.InteractiveComponents;

/// <summary>
/// Unit tests for GovUkCookieBanner component covering rendering and visibility.
/// </summary>
public class GovUkCookieBannerTests : BunitContext
{
    #region Basic Rendering Tests

    [Fact]
    public void CookieBanner_RendersWithCorrectClass()
    {
        // Arrange & Act
        var cut = Render<GovUkCookieBanner>(parameters => parameters
            .AddChildContent("<p>Cookie message</p>"));

        // Assert
        var banner = cut.Find(".govuk-cookie-banner");
        Assert.NotNull(banner);
    }

    [Fact]
    public void CookieBanner_HasRegionRole()
    {
        // Arrange & Act
        var cut = Render<GovUkCookieBanner>(parameters => parameters
            .AddChildContent("<p>Cookie message</p>"));

        // Assert
        var banner = cut.Find(".govuk-cookie-banner");
        Assert.Equal("region", banner.GetAttribute("role"));
    }

    [Fact]
    public void CookieBanner_HasDataNosnippet()
    {
        // Arrange & Act
        var cut = Render<GovUkCookieBanner>(parameters => parameters
            .AddChildContent("<p>Cookie message</p>"));

        // Assert
        var banner = cut.Find(".govuk-cookie-banner");
        Assert.True(banner.HasAttribute("data-nosnippet"));
    }

    [Fact]
    public void CookieBanner_RendersDefaultAriaLabel()
    {
        // Arrange & Act
        var cut = Render<GovUkCookieBanner>(parameters => parameters
            .AddChildContent("<p>Cookie message</p>"));

        // Assert
        var banner = cut.Find(".govuk-cookie-banner");
        Assert.Equal("Cookies on this service", banner.GetAttribute("aria-label"));
    }

    [Fact]
    public void CookieBanner_RendersCustomAriaLabel()
    {
        // Arrange & Act
        var cut = Render<GovUkCookieBanner>(parameters => parameters
            .Add(p => p.AriaLabel, "Cookies on Example Service")
            .AddChildContent("<p>Cookie message</p>"));

        // Assert
        var banner = cut.Find(".govuk-cookie-banner");
        Assert.Equal("Cookies on Example Service", banner.GetAttribute("aria-label"));
    }

    [Fact]
    public void CookieBanner_RendersChildContent()
    {
        // Arrange & Act
        var cut = Render<GovUkCookieBanner>(parameters => parameters
            .AddChildContent("<p>We use cookies</p>"));

        // Assert
        Assert.Contains("We use cookies", cut.Markup);
    }

    [Fact]
    public void CookieBanner_AppliesCustomCssClass()
    {
        // Arrange & Act
        var cut = Render<GovUkCookieBanner>(parameters => parameters
            .Add(p => p.CssClass, "my-custom-class")
            .AddChildContent("<p>Cookie message</p>"));

        // Assert
        var banner = cut.Find(".govuk-cookie-banner");
        Assert.Contains("my-custom-class", banner.ClassName);
    }

    #endregion

    #region Visibility Tests

    [Fact]
    public void CookieBanner_HidesContentWhenHidden()
    {
        // Arrange & Act
        var cut = Render<GovUkCookieBanner>(parameters => parameters
            .Add(p => p.IsHidden, true)
            .AddChildContent("<p>Cookie message</p>"));

        // Assert
        Assert.DoesNotContain("Cookie message", cut.Markup);
    }

    [Fact]
    public void CookieBanner_ShowsContentWhenNotHidden()
    {
        // Arrange & Act
        var cut = Render<GovUkCookieBanner>(parameters => parameters
            .Add(p => p.IsHidden, false)
            .AddChildContent("<p>Cookie message</p>"));

        // Assert
        Assert.Contains("Cookie message", cut.Markup);
    }

    #endregion
}

/// <summary>
/// Unit tests for GovUkCookieBannerMessage component.
/// </summary>
public class GovUkCookieBannerMessageTests : BunitContext
{
    [Fact]
    public void CookieBannerMessage_RendersWithCorrectClass()
    {
        // Arrange & Act
        var cut = Render<GovUkCookieBannerMessage>(parameters => parameters
            .AddChildContent("<p>Message content</p>"));

        // Assert
        var message = cut.Find(".govuk-cookie-banner__message");
        Assert.NotNull(message);
    }

    [Fact]
    public void CookieBannerMessage_RendersHeading()
    {
        // Arrange & Act
        var cut = Render<GovUkCookieBannerMessage>(parameters => parameters
            .Add(p => p.Heading, "Cookies on this service")
            .AddChildContent("<p>Message content</p>"));

        // Assert
        var heading = cut.Find(".govuk-cookie-banner__heading");
        Assert.Equal("Cookies on this service", heading.TextContent);
    }

    [Fact]
    public void CookieBannerMessage_HeadingIsH2()
    {
        // Arrange & Act
        var cut = Render<GovUkCookieBannerMessage>(parameters => parameters
            .Add(p => p.Heading, "Cookies")
            .AddChildContent("<p>Message content</p>"));

        // Assert
        var heading = cut.Find("h2.govuk-cookie-banner__heading");
        Assert.NotNull(heading);
    }

    [Fact]
    public void CookieBannerMessage_NoHeadingWhenEmpty()
    {
        // Arrange & Act
        var cut = Render<GovUkCookieBannerMessage>(parameters => parameters
            .AddChildContent("<p>Message content</p>"));

        // Assert
        Assert.Throws<ElementNotFoundException>(() => cut.Find(".govuk-cookie-banner__heading"));
    }

    [Fact]
    public void CookieBannerMessage_RendersContent()
    {
        // Arrange & Act
        var cut = Render<GovUkCookieBannerMessage>(parameters => parameters
            .AddChildContent("<p>We use cookies</p>"));

        // Assert
        var content = cut.Find(".govuk-cookie-banner__content");
        Assert.Contains("We use cookies", content.TextContent);
    }

    [Fact]
    public void CookieBannerMessage_RendersActions()
    {
        // Arrange & Act
        var cut = Render<GovUkCookieBannerMessage>(parameters => parameters
            .AddChildContent("<p>Message</p>")
            .Add(p => p.Actions, builder => builder.AddMarkupContent(0, "<button class=\"govuk-button\">Accept</button>")));

        // Assert
        var buttonGroup = cut.Find(".govuk-button-group");
        Assert.NotNull(buttonGroup);
        Assert.Contains("Accept", buttonGroup.TextContent);
    }

    [Fact]
    public void CookieBannerMessage_HiddenWhenPropertySet()
    {
        // Arrange & Act
        var cut = Render<GovUkCookieBannerMessage>(parameters => parameters
            .Add(p => p.Hidden, true)
            .AddChildContent("<p>Message content</p>"));

        // Assert
        Assert.Throws<ElementNotFoundException>(() => cut.Find(".govuk-cookie-banner__message"));
    }

    [Fact]
    public void CookieBannerMessage_AppliesCustomCssClass()
    {
        // Arrange & Act
        var cut = Render<GovUkCookieBannerMessage>(parameters => parameters
            .Add(p => p.CssClass, "custom-message-class")
            .AddChildContent("<p>Message content</p>"));

        // Assert
        var message = cut.Find(".govuk-cookie-banner__message");
        Assert.Contains("custom-message-class", message.ClassName);
    }

    [Fact]
    public void CookieBannerMessage_PassesAdditionalAttributes()
    {
        // Arrange & Act
        var cut = Render<GovUkCookieBannerMessage>(parameters => parameters
            .AddChildContent("<p>Message content</p>")
            .AddUnmatched("data-test-id", "cookie-message")
            .AddUnmatched("id", "my-message"));

        // Assert
        var message = cut.Find(".govuk-cookie-banner__message");
        Assert.Equal("cookie-message", message.GetAttribute("data-test-id"));
        Assert.Equal("my-message", message.Id);
    }
}

/// <summary>
/// Unit tests for GovUkManagedCookieBanner component.
/// </summary>
public class GovUkManagedCookieBannerTests : BunitContext
{
    private readonly MockCookieConsentService _mockCookieService;

    public GovUkManagedCookieBannerTests()
    {
        _mockCookieService = new MockCookieConsentService();
        Services.AddSingleton<ICookieConsentService>(_mockCookieService);
    }

    [Fact]
    public void ManagedCookieBanner_RendersWithCorrectClass()
    {
        // Arrange & Act
        var cut = Render<GovUkManagedCookieBanner>();

        // Assert
        var banner = cut.Find(".govuk-cookie-banner");
        Assert.NotNull(banner);
    }

    [Fact]
    public void ManagedCookieBanner_HiddenInitiallyWhenConsentExists()
    {
        // Arrange
        _mockCookieService.HasConsent = true;

        // Act
        var cut = Render<GovUkManagedCookieBanner>();

        // Assert - banner content should be hidden when consent already exists
        Assert.DoesNotContain("govuk-cookie-banner__message", cut.Markup);
    }

    [Fact]
    public void ManagedCookieBanner_ShowsConsentMessageWhenNoConsent()
    {
        // Arrange
        _mockCookieService.HasConsent = false;

        // Act
        var cut = Render<GovUkManagedCookieBanner>();
        cut.WaitForState(() => cut.Markup.Contains("govuk-cookie-banner__message"));

        // Assert
        Assert.Contains("Accept analytics cookies", cut.Markup);
        Assert.Contains("Reject analytics cookies", cut.Markup);
    }

    [Fact]
    public void ManagedCookieBanner_UsesCustomHeading()
    {
        // Arrange
        _mockCookieService.HasConsent = false;

        // Act
        var cut = Render<GovUkManagedCookieBanner>(p => p
            .Add(c => c.Heading, "Cookies on My Service"));
        cut.WaitForState(() => cut.Markup.Contains("govuk-cookie-banner__heading"));

        // Assert
        var heading = cut.Find(".govuk-cookie-banner__heading");
        Assert.Equal("Cookies on My Service", heading.TextContent);
    }

    [Fact]
    public void ManagedCookieBanner_UsesCustomButtonText()
    {
        // Arrange
        _mockCookieService.HasConsent = false;

        // Act
        var cut = Render<GovUkManagedCookieBanner>(p => p
            .Add(c => c.AcceptButtonText, "Yes, accept cookies")
            .Add(c => c.RejectButtonText, "No, reject cookies"));
        cut.WaitForState(() => cut.Markup.Contains("govuk-button"));

        // Assert
        Assert.Contains("Yes, accept cookies", cut.Markup);
        Assert.Contains("No, reject cookies", cut.Markup);
    }

    [Fact]
    public void ManagedCookieBanner_ShowsViewCookiesLink()
    {
        // Arrange
        _mockCookieService.HasConsent = false;

        // Act
        var cut = Render<GovUkManagedCookieBanner>(p => p
            .Add(c => c.ViewCookiesUrl, "/cookies")
            .Add(c => c.ViewCookiesLinkText, "View cookie settings"));
        cut.WaitForState(() => cut.Markup.Contains("govuk-link"));

        // Assert
        var link = cut.Find("a.govuk-link");
        Assert.Equal("/cookies", link.GetAttribute("href"));
        Assert.Equal("View cookie settings", link.TextContent);
    }

    [Fact]
    public void ManagedCookieBanner_AcceptCookies_SetsConsentAndShowsConfirmation()
    {
        // Arrange
        _mockCookieService.HasConsent = false;

        var cut = Render<GovUkManagedCookieBanner>();
        cut.WaitForState(() => cut.Markup.Contains("Accept analytics cookies"));

        // Act
        var acceptButton = cut.FindAll(".govuk-button").First(b => b.TextContent.Contains("Accept"));
        acceptButton.Click();

        // Assert
        Assert.True(_mockCookieService.AnalyticsConsent);
        Assert.Contains("You've accepted analytics cookies", cut.Markup);
        Assert.Contains("Hide cookie message", cut.Markup);
    }

    [Fact]
    public void ManagedCookieBanner_RejectCookies_SetsConsentAndShowsConfirmation()
    {
        // Arrange
        _mockCookieService.HasConsent = false;

        var cut = Render<GovUkManagedCookieBanner>();
        cut.WaitForState(() => cut.Markup.Contains("Reject analytics cookies"));

        // Act
        var rejectButton = cut.FindAll(".govuk-button").First(b => b.TextContent.Contains("Reject"));
        rejectButton.Click();

        // Assert
        Assert.False(_mockCookieService.AnalyticsConsent);
        Assert.Contains("You've rejected analytics cookies", cut.Markup);
    }

    [Fact]
    public void ManagedCookieBanner_HideButton_HidesBanner()
    {
        // Arrange
        _mockCookieService.HasConsent = false;

        var cut = Render<GovUkManagedCookieBanner>();
        cut.WaitForState(() => cut.Markup.Contains("Accept analytics cookies"));

        // Accept cookies first
        var acceptButton = cut.FindAll(".govuk-button").First(b => b.TextContent.Contains("Accept"));
        acceptButton.Click();

        // Act
        var hideButton = cut.FindAll(".govuk-button").First(b => b.TextContent.Contains("Hide"));
        hideButton.Click();

        // Assert
        Assert.DoesNotContain("govuk-cookie-banner__message", cut.Markup);
    }

    [Fact]
    public void ManagedCookieBanner_InvokesOnConsentChangedCallback()
    {
        // Arrange
        _mockCookieService.HasConsent = false;
        bool? consentValue = null;

        var cut = Render<GovUkManagedCookieBanner>(p => p
            .Add(c => c.OnConsentChanged, EventCallback.Factory.Create<bool>(this, v => consentValue = v)));
        cut.WaitForState(() => cut.Markup.Contains("Accept analytics cookies"));

        // Act
        var acceptButton = cut.FindAll(".govuk-button").First(b => b.TextContent.Contains("Accept"));
        acceptButton.Click();

        // Assert
        Assert.True(consentValue);
    }

    /// <summary>
    /// Mock implementation of ICookieConsentService for testing.
    /// </summary>
    private class MockCookieConsentService : ICookieConsentService
    {
        public bool HasConsent { get; set; }
        public bool? AnalyticsConsent { get; private set; }

        public Task<bool> HasConsentAsync() => Task.FromResult(HasConsent);

        public Task<bool> GetAnalyticsConsentAsync() => Task.FromResult(AnalyticsConsent ?? false);

        public Task SetConsentAsync(bool acceptAnalytics)
        {
            AnalyticsConsent = acceptAnalytics;
            HasConsent = true;
            return Task.CompletedTask;
        }

        public Task ClearConsentAsync()
        {
            HasConsent = false;
            AnalyticsConsent = null;
            return Task.CompletedTask;
        }
    }
}
