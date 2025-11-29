using Bunit;
using Blazor.DesignSystem.Components;
using Xunit;

namespace Blazor.DesignSystem.Components.Tests.NavigationComponents;

/// <summary>
/// Unit tests for GovUkSkipLink component covering rendering and accessibility.
/// </summary>
public class GovUkSkipLinkTests : BunitContext
{
    #region Basic Rendering Tests

    [Fact]
    public void SkipLink_RendersWithCorrectClass()
    {
        // Arrange & Act
        var cut = Render<GovUkSkipLink>();

        // Assert
        var link = cut.Find("a.govuk-skip-link");
        Assert.NotNull(link);
    }

    [Fact]
    public void SkipLink_HasDataModuleAttribute()
    {
        // Arrange & Act
        var cut = Render<GovUkSkipLink>();

        // Assert
        var link = cut.Find("a.govuk-skip-link");
        Assert.Equal("govuk-skip-link", link.GetAttribute("data-module"));
    }

    [Fact]
    public void SkipLink_DefaultText()
    {
        // Arrange & Act
        var cut = Render<GovUkSkipLink>();

        // Assert
        var link = cut.Find("a.govuk-skip-link");
        Assert.Equal("Skip to main content", link.TextContent);
    }

    [Fact]
    public void SkipLink_CustomText()
    {
        // Arrange & Act
        var cut = Render<GovUkSkipLink>(parameters => parameters
            .Add(p => p.Text, "Skip to content"));

        // Assert
        var link = cut.Find("a.govuk-skip-link");
        Assert.Equal("Skip to content", link.TextContent);
    }

    [Fact]
    public void SkipLink_DefaultHref()
    {
        // Arrange & Act
        var cut = Render<GovUkSkipLink>();

        // Assert
        var link = cut.Find("a.govuk-skip-link");
        Assert.Equal("#main-content", link.GetAttribute("href"));
    }

    [Fact]
    public void SkipLink_CustomHref()
    {
        // Arrange & Act
        var cut = Render<GovUkSkipLink>(parameters => parameters
            .Add(p => p.Href, "#content"));

        // Assert
        var link = cut.Find("a.govuk-skip-link");
        Assert.Equal("#content", link.GetAttribute("href"));
    }

    [Fact]
    public void SkipLink_AppliesCustomCssClass()
    {
        // Arrange & Act
        var cut = Render<GovUkSkipLink>(parameters => parameters
            .Add(p => p.CssClass, "custom-class"));

        // Assert
        var link = cut.Find("a.govuk-skip-link");
        Assert.Contains("custom-class", link.ClassName);
    }

    #endregion

    #region Accessibility Tests

    [Fact]
    public void SkipLink_TargetsMainContent()
    {
        // Arrange & Act
        var cut = Render<GovUkSkipLink>();

        // Assert
        var link = cut.Find("a.govuk-skip-link");
        var href = link.GetAttribute("href");
        Assert.StartsWith("#", href);
    }

    #endregion

    #region Additional Attributes Tests

    [Fact]
    public void SkipLink_PassesAdditionalAttributes()
    {
        // Arrange & Act
        var cut = Render<GovUkSkipLink>(parameters => parameters
            .AddUnmatched("data-test-id", "test-skiplink")
            .AddUnmatched("id", "my-skiplink"));

        // Assert
        var link = cut.Find("a.govuk-skip-link");
        Assert.Equal("test-skiplink", link.GetAttribute("data-test-id"));
        Assert.Equal("my-skiplink", link.Id);
    }

    #endregion
}
