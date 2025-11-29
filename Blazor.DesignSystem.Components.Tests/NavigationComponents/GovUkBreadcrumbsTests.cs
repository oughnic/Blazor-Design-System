using Bunit;
using Blazor.DesignSystem.Components;
using Xunit;

namespace Blazor.DesignSystem.Components.Tests.NavigationComponents;

/// <summary>
/// Unit tests for GovUkBreadcrumbs and GovUkBreadcrumbsItem components covering rendering and accessibility.
/// </summary>
public class GovUkBreadcrumbsTests : BunitContext
{
    #region Basic Rendering Tests

    [Fact]
    public void Breadcrumbs_RendersAsNavElement()
    {
        // Arrange & Act
        var cut = Render<GovUkBreadcrumbs>();

        // Assert
        var nav = cut.Find("nav.govuk-breadcrumbs");
        Assert.NotNull(nav);
    }

    [Fact]
    public void Breadcrumbs_RendersList()
    {
        // Arrange & Act
        var cut = Render<GovUkBreadcrumbs>();

        // Assert
        var list = cut.Find("ol.govuk-breadcrumbs__list");
        Assert.NotNull(list);
    }

    [Fact]
    public void Breadcrumbs_AppliesCustomCssClass()
    {
        // Arrange & Act
        var cut = Render<GovUkBreadcrumbs>(parameters => parameters
            .Add(p => p.CssClass, "custom-class"));

        // Assert
        var nav = cut.Find("nav.govuk-breadcrumbs");
        Assert.Contains("custom-class", nav.ClassName);
    }

    #endregion

    #region Accessibility Tests

    [Fact]
    public void Breadcrumbs_HasAriaLabelBreadcrumb()
    {
        // Arrange & Act
        var cut = Render<GovUkBreadcrumbs>();

        // Assert
        var nav = cut.Find("nav.govuk-breadcrumbs");
        Assert.Equal("Breadcrumb", nav.GetAttribute("aria-label"));
    }

    #endregion

    #region Breadcrumb Item Tests

    [Fact]
    public void BreadcrumbItem_RendersLink()
    {
        // Arrange & Act
        var cut = Render<GovUkBreadcrumbs>(parameters => parameters
            .AddChildContent<GovUkBreadcrumbsItem>(p => p
                .Add(x => x.Text, "Home")
                .Add(x => x.Href, "/")));

        // Assert
        var item = cut.Find(".govuk-breadcrumbs__list-item");
        var link = cut.Find("a.govuk-breadcrumbs__link");
        Assert.Equal("Home", link.TextContent);
        Assert.Equal("/", link.GetAttribute("href"));
    }

    [Fact]
    public void BreadcrumbItem_CurrentPage_HasNoLink()
    {
        // Arrange & Act
        var cut = Render<GovUkBreadcrumbs>(parameters => parameters
            .AddChildContent<GovUkBreadcrumbsItem>(p => p
                .Add(x => x.Text, "Current page")
                .Add(x => x.IsCurrentPage, true)));

        // Assert
        var item = cut.Find(".govuk-breadcrumbs__list-item");
        Assert.Equal("Current page", item.TextContent);
        Assert.Throws<Bunit.ElementNotFoundException>(() => cut.Find("a.govuk-breadcrumbs__link"));
    }

    [Fact]
    public void BreadcrumbItem_CurrentPage_HasAriaCurrent()
    {
        // Arrange & Act
        var cut = Render<GovUkBreadcrumbs>(parameters => parameters
            .AddChildContent<GovUkBreadcrumbsItem>(p => p
                .Add(x => x.Text, "Current page")
                .Add(x => x.IsCurrentPage, true)));

        // Assert
        var item = cut.Find(".govuk-breadcrumbs__list-item");
        Assert.Equal("page", item.GetAttribute("aria-current"));
    }

    [Fact]
    public void BreadcrumbItem_NotCurrentPage_NoAriaCurrent()
    {
        // Arrange & Act
        var cut = Render<GovUkBreadcrumbs>(parameters => parameters
            .AddChildContent<GovUkBreadcrumbsItem>(p => p
                .Add(x => x.Text, "Home")
                .Add(x => x.Href, "/")));

        // Assert
        var item = cut.Find(".govuk-breadcrumbs__list-item");
        Assert.Null(item.GetAttribute("aria-current"));
    }

    [Fact]
    public void Breadcrumbs_MultipleItems()
    {
        // Arrange & Act
        var cut = Render<GovUkBreadcrumbs>(parameters => parameters
            .AddChildContent<GovUkBreadcrumbsItem>(p => p
                .Add(x => x.Text, "Home")
                .Add(x => x.Href, "/"))
            .AddChildContent<GovUkBreadcrumbsItem>(p => p
                .Add(x => x.Text, "Services")
                .Add(x => x.Href, "/services"))
            .AddChildContent<GovUkBreadcrumbsItem>(p => p
                .Add(x => x.Text, "Current")
                .Add(x => x.IsCurrentPage, true)));

        // Assert
        var items = cut.FindAll(".govuk-breadcrumbs__list-item");
        Assert.Equal(3, items.Count);
        
        var links = cut.FindAll("a.govuk-breadcrumbs__link");
        Assert.Equal(2, links.Count);
    }

    #endregion
}
