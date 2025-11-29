using Bunit;
using Blazor.DesignSystem.Components;
using Xunit;

namespace Blazor.DesignSystem.Components.Tests.NavigationComponents;

/// <summary>
/// Unit tests for GovUkPagination and GovUkPaginationItem components covering rendering and accessibility.
/// </summary>
public class GovUkPaginationTests : BunitContext
{
    #region Basic Rendering Tests

    [Fact]
    public void Pagination_RendersAsNavElement()
    {
        // Arrange & Act
        var cut = Render<GovUkPagination>();

        // Assert
        var nav = cut.Find("nav.govuk-pagination");
        Assert.NotNull(nav);
    }

    [Fact]
    public void Pagination_HasDefaultAriaLabel()
    {
        // Arrange & Act
        var cut = Render<GovUkPagination>();

        // Assert
        var nav = cut.Find("nav.govuk-pagination");
        Assert.Equal("Pagination", nav.GetAttribute("aria-label"));
    }

    [Fact]
    public void Pagination_CustomAriaLabel()
    {
        // Arrange & Act
        var cut = Render<GovUkPagination>(parameters => parameters
            .Add(p => p.AriaLabel, "Results pagination"));

        // Assert
        var nav = cut.Find("nav.govuk-pagination");
        Assert.Equal("Results pagination", nav.GetAttribute("aria-label"));
    }

    [Fact]
    public void Pagination_AppliesCustomCssClass()
    {
        // Arrange & Act
        var cut = Render<GovUkPagination>(parameters => parameters
            .Add(p => p.CssClass, "custom-class"));

        // Assert
        var nav = cut.Find("nav.govuk-pagination");
        Assert.Contains("custom-class", nav.ClassName);
    }

    #endregion

    #region Previous Link Tests

    [Fact]
    public void Pagination_RendersPreviousLink()
    {
        // Arrange & Act
        var cut = Render<GovUkPagination>(parameters => parameters
            .Add(p => p.PreviousHref, "/page/1"));

        // Assert
        var prevLink = cut.Find(".govuk-pagination__prev a");
        Assert.NotNull(prevLink);
        Assert.Equal("/page/1", prevLink.GetAttribute("href"));
        Assert.Equal("prev", prevLink.GetAttribute("rel"));
    }

    [Fact]
    public void Pagination_PreviousLinkDefaultText()
    {
        // Arrange & Act
        var cut = Render<GovUkPagination>(parameters => parameters
            .Add(p => p.PreviousHref, "/page/1"));

        // Assert
        var prevLink = cut.Find(".govuk-pagination__prev a");
        Assert.Contains("Previous", prevLink.TextContent);
    }

    [Fact]
    public void Pagination_PreviousLinkCustomText()
    {
        // Arrange & Act
        var cut = Render<GovUkPagination>(parameters => parameters
            .Add(p => p.PreviousHref, "/page/1")
            .Add(p => p.PreviousText, "Back"));

        // Assert
        var prevLink = cut.Find(".govuk-pagination__prev a");
        Assert.Contains("Back", prevLink.TextContent);
    }

    [Fact]
    public void Pagination_PreviousLinkWithLabel()
    {
        // Arrange & Act
        var cut = Render<GovUkPagination>(parameters => parameters
            .Add(p => p.PreviousHref, "/page/1")
            .Add(p => p.PreviousLabel, "1 of 3"));

        // Assert
        var label = cut.Find(".govuk-pagination__prev .govuk-pagination__link-label");
        Assert.Equal("1 of 3", label.TextContent);
    }

    [Fact]
    public void Pagination_NoPreviousLink_WhenHrefNotSet()
    {
        // Arrange & Act
        var cut = Render<GovUkPagination>();

        // Assert
        Assert.Throws<Bunit.ElementNotFoundException>(() => cut.Find(".govuk-pagination__prev"));
    }

    #endregion

    #region Next Link Tests

    [Fact]
    public void Pagination_RendersNextLink()
    {
        // Arrange & Act
        var cut = Render<GovUkPagination>(parameters => parameters
            .Add(p => p.NextHref, "/page/3"));

        // Assert
        var nextLink = cut.Find(".govuk-pagination__next a");
        Assert.NotNull(nextLink);
        Assert.Equal("/page/3", nextLink.GetAttribute("href"));
        Assert.Equal("next", nextLink.GetAttribute("rel"));
    }

    [Fact]
    public void Pagination_NextLinkDefaultText()
    {
        // Arrange & Act
        var cut = Render<GovUkPagination>(parameters => parameters
            .Add(p => p.NextHref, "/page/3"));

        // Assert
        var nextLink = cut.Find(".govuk-pagination__next a");
        Assert.Contains("Next", nextLink.TextContent);
    }

    [Fact]
    public void Pagination_NextLinkCustomText()
    {
        // Arrange & Act
        var cut = Render<GovUkPagination>(parameters => parameters
            .Add(p => p.NextHref, "/page/3")
            .Add(p => p.NextText, "Forward"));

        // Assert
        var nextLink = cut.Find(".govuk-pagination__next a");
        Assert.Contains("Forward", nextLink.TextContent);
    }

    [Fact]
    public void Pagination_NextLinkWithLabel()
    {
        // Arrange & Act
        var cut = Render<GovUkPagination>(parameters => parameters
            .Add(p => p.NextHref, "/page/3")
            .Add(p => p.NextLabel, "3 of 3"));

        // Assert
        var label = cut.Find(".govuk-pagination__next .govuk-pagination__link-label");
        Assert.Equal("3 of 3", label.TextContent);
    }

    [Fact]
    public void Pagination_NoNextLink_WhenHrefNotSet()
    {
        // Arrange & Act
        var cut = Render<GovUkPagination>();

        // Assert
        Assert.Throws<Bunit.ElementNotFoundException>(() => cut.Find(".govuk-pagination__next"));
    }

    #endregion

    #region Combined Navigation Tests

    [Fact]
    public void Pagination_BothPreviousAndNext()
    {
        // Arrange & Act
        var cut = Render<GovUkPagination>(parameters => parameters
            .Add(p => p.PreviousHref, "/page/1")
            .Add(p => p.NextHref, "/page/3"));

        // Assert
        Assert.NotNull(cut.Find(".govuk-pagination__prev"));
        Assert.NotNull(cut.Find(".govuk-pagination__next"));
    }

    #endregion

    #region Accessibility Tests

    [Fact]
    public void Pagination_PreviousLink_HasRelPrev()
    {
        // Arrange & Act
        var cut = Render<GovUkPagination>(parameters => parameters
            .Add(p => p.PreviousHref, "/page/1"));

        // Assert
        var prevLink = cut.Find(".govuk-pagination__prev a");
        Assert.Equal("prev", prevLink.GetAttribute("rel"));
    }

    [Fact]
    public void Pagination_NextLink_HasRelNext()
    {
        // Arrange & Act
        var cut = Render<GovUkPagination>(parameters => parameters
            .Add(p => p.NextHref, "/page/3"));

        // Assert
        var nextLink = cut.Find(".govuk-pagination__next a");
        Assert.Equal("next", nextLink.GetAttribute("rel"));
    }

    [Fact]
    public void Pagination_HasLinkClass()
    {
        // Arrange & Act
        var cut = Render<GovUkPagination>(parameters => parameters
            .Add(p => p.NextHref, "/page/3"));

        // Assert
        var link = cut.Find(".govuk-pagination__next a");
        Assert.Contains("govuk-link", link.ClassName);
        Assert.Contains("govuk-pagination__link", link.ClassName);
    }

    #endregion
}
