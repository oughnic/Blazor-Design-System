using Bunit;
using Blazor.DesignSystem.Components;
using Xunit;

namespace Blazor.DesignSystem.Components.Tests.ContentComponents;

/// <summary>
/// Unit tests for GovUkNotificationBanner component covering rendering and accessibility.
/// </summary>
public class GovUkNotificationBannerTests : BunitContext
{
    #region Basic Rendering Tests

    [Fact]
    public void NotificationBanner_RendersWithCorrectClass()
    {
        // Arrange & Act
        var cut = Render<GovUkNotificationBanner>(parameters => parameters
            .AddChildContent("<p>Important message</p>"));

        // Assert
        var banner = cut.Find(".govuk-notification-banner");
        Assert.NotNull(banner);
    }

    [Fact]
    public void NotificationBanner_HasDataModuleAttribute()
    {
        // Arrange & Act
        var cut = Render<GovUkNotificationBanner>(parameters => parameters
            .AddChildContent("<p>Message</p>"));

        // Assert
        var banner = cut.Find(".govuk-notification-banner");
        Assert.Equal("govuk-notification-banner", banner.GetAttribute("data-module"));
    }

    [Fact]
    public void NotificationBanner_RendersDefaultTitle()
    {
        // Arrange & Act
        var cut = Render<GovUkNotificationBanner>(parameters => parameters
            .AddChildContent("<p>Message</p>"));

        // Assert
        var title = cut.Find(".govuk-notification-banner__title");
        Assert.Equal("Important", title.TextContent);
    }

    [Fact]
    public void NotificationBanner_RendersCustomTitle()
    {
        // Arrange & Act
        var cut = Render<GovUkNotificationBanner>(parameters => parameters
            .Add(p => p.Title, "Success")
            .AddChildContent("<p>Message</p>"));

        // Assert
        var title = cut.Find(".govuk-notification-banner__title");
        Assert.Equal("Success", title.TextContent);
    }

    [Fact]
    public void NotificationBanner_RendersContent()
    {
        // Arrange & Act
        var cut = Render<GovUkNotificationBanner>(parameters => parameters
            .AddChildContent("<p>Your application has been submitted</p>"));

        // Assert
        var content = cut.Find(".govuk-notification-banner__content");
        Assert.Contains("Your application has been submitted", content.TextContent);
    }

    [Fact]
    public void NotificationBanner_AppliesCustomCssClass()
    {
        // Arrange & Act
        var cut = Render<GovUkNotificationBanner>(parameters => parameters
            .Add(p => p.CssClass, "custom-class")
            .AddChildContent("<p>Message</p>"));

        // Assert
        var banner = cut.Find(".govuk-notification-banner");
        Assert.Contains("custom-class", banner.ClassName);
    }

    #endregion

    #region Success Variant Tests

    [Fact]
    public void NotificationBanner_SuccessType_HasSuccessClass()
    {
        // Arrange & Act
        var cut = Render<GovUkNotificationBanner>(parameters => parameters
            .Add(p => p.Type, "success")
            .AddChildContent("<p>Success message</p>"));

        // Assert
        var banner = cut.Find(".govuk-notification-banner");
        Assert.Contains("govuk-notification-banner--success", banner.ClassName);
    }

    [Fact]
    public void NotificationBanner_SuccessType_HasAlertRole()
    {
        // Arrange & Act
        var cut = Render<GovUkNotificationBanner>(parameters => parameters
            .Add(p => p.Type, "success")
            .AddChildContent("<p>Success message</p>"));

        // Assert
        var banner = cut.Find(".govuk-notification-banner");
        Assert.Equal("alert", banner.GetAttribute("role"));
    }

    [Fact]
    public void NotificationBanner_StandardType_HasRegionRole()
    {
        // Arrange & Act
        var cut = Render<GovUkNotificationBanner>(parameters => parameters
            .AddChildContent("<p>Standard message</p>"));

        // Assert
        var banner = cut.Find(".govuk-notification-banner");
        Assert.Equal("region", banner.GetAttribute("role"));
    }

    #endregion

    #region Accessibility Tests

    [Fact]
    public void NotificationBanner_HasAriaLabelledBy()
    {
        // Arrange & Act
        var cut = Render<GovUkNotificationBanner>(parameters => parameters
            .Add(p => p.TitleId, "banner-title")
            .AddChildContent("<p>Message</p>"));

        // Assert
        var banner = cut.Find(".govuk-notification-banner");
        Assert.Equal("banner-title", banner.GetAttribute("aria-labelledby"));
    }

    [Fact]
    public void NotificationBanner_TitleId_MatchesAriaLabelledBy()
    {
        // Arrange & Act
        var cut = Render<GovUkNotificationBanner>(parameters => parameters
            .Add(p => p.TitleId, "my-banner-title")
            .AddChildContent("<p>Message</p>"));

        // Assert
        var banner = cut.Find(".govuk-notification-banner");
        var title = cut.Find(".govuk-notification-banner__title");
        
        Assert.Equal("my-banner-title", title.Id);
        Assert.Equal("my-banner-title", banner.GetAttribute("aria-labelledby"));
    }

    #endregion

    #region Structure Tests

    [Fact]
    public void NotificationBanner_HasHeader()
    {
        // Arrange & Act
        var cut = Render<GovUkNotificationBanner>(parameters => parameters
            .AddChildContent("<p>Message</p>"));

        // Assert
        var header = cut.Find(".govuk-notification-banner__header");
        Assert.NotNull(header);
    }

    [Fact]
    public void NotificationBanner_TitleIsH2()
    {
        // Arrange & Act
        var cut = Render<GovUkNotificationBanner>(parameters => parameters
            .AddChildContent("<p>Message</p>"));

        // Assert
        var title = cut.Find("h2.govuk-notification-banner__title");
        Assert.NotNull(title);
    }

    #endregion

    #region Additional Attributes Tests

    [Fact]
    public void NotificationBanner_PassesAdditionalAttributes()
    {
        // Arrange & Act
        var cut = Render<GovUkNotificationBanner>(parameters => parameters
            .AddChildContent("<p>Message</p>")
            .AddUnmatched("data-test-id", "test-banner")
            .AddUnmatched("id", "my-banner"));

        // Assert
        var banner = cut.Find(".govuk-notification-banner");
        Assert.Equal("test-banner", banner.GetAttribute("data-test-id"));
        Assert.Equal("my-banner", banner.Id);
    }

    #endregion
}
