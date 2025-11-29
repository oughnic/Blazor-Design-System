using Bunit;
using Blazor.DesignSystem.Components;
using Xunit;

namespace Blazor.DesignSystem.Components.Tests.NavigationComponents;

/// <summary>
/// Unit tests for GovUkBackLink component covering rendering and accessibility.
/// </summary>
public class GovUkBackLinkTests : BunitContext
{
    #region Basic Rendering Tests

    [Fact]
    public void BackLink_RendersWithCorrectClass()
    {
        // Arrange & Act
        var cut = Render<GovUkBackLink>();

        // Assert
        var link = cut.Find("a.govuk-back-link");
        Assert.NotNull(link);
    }

    [Fact]
    public void BackLink_DefaultText()
    {
        // Arrange & Act
        var cut = Render<GovUkBackLink>();

        // Assert
        var link = cut.Find("a.govuk-back-link");
        Assert.Equal("Back", link.TextContent);
    }

    [Fact]
    public void BackLink_CustomText()
    {
        // Arrange & Act
        var cut = Render<GovUkBackLink>(parameters => parameters
            .Add(p => p.Text, "Go back"));

        // Assert
        var link = cut.Find("a.govuk-back-link");
        Assert.Equal("Go back", link.TextContent);
    }

    [Fact]
    public void BackLink_DefaultHref()
    {
        // Arrange & Act
        var cut = Render<GovUkBackLink>();

        // Assert
        var link = cut.Find("a.govuk-back-link");
        Assert.Equal("#", link.GetAttribute("href"));
    }

    [Fact]
    public void BackLink_CustomHref()
    {
        // Arrange & Act
        var cut = Render<GovUkBackLink>(parameters => parameters
            .Add(p => p.Href, "/previous-page"));

        // Assert
        var link = cut.Find("a.govuk-back-link");
        Assert.Equal("/previous-page", link.GetAttribute("href"));
    }

    [Fact]
    public void BackLink_AppliesCustomCssClass()
    {
        // Arrange & Act
        var cut = Render<GovUkBackLink>(parameters => parameters
            .Add(p => p.CssClass, "custom-class"));

        // Assert
        var link = cut.Find("a.govuk-back-link");
        Assert.Contains("custom-class", link.ClassName);
    }

    #endregion

}
