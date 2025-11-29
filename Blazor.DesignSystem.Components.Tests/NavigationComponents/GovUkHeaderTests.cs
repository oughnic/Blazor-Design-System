using Bunit;
using Microsoft.AspNetCore.Components;
using Blazor.DesignSystem.Components;
using Xunit;

namespace Blazor.DesignSystem.Components.Tests.NavigationComponents;

/// <summary>
/// Unit tests for GovUkHeader and GovUkHeaderNavItem components covering rendering and accessibility.
/// </summary>
public class GovUkHeaderTests : BunitContext
{
    #region Basic Rendering Tests

    [Fact]
    public void Header_RendersWithCorrectClass()
    {
        // Arrange & Act
        var cut = Render<GovUkHeader>();

        // Assert
        var header = cut.Find("header.govuk-header");
        Assert.NotNull(header);
    }

    [Fact]
    public void Header_HasBannerRole()
    {
        // Arrange & Act
        var cut = Render<GovUkHeader>();

        // Assert
        var header = cut.Find("header.govuk-header");
        Assert.Equal("banner", header.GetAttribute("role"));
    }

    [Fact]
    public void Header_HasDataModuleAttribute()
    {
        // Arrange & Act
        var cut = Render<GovUkHeader>();

        // Assert
        var header = cut.Find("header.govuk-header");
        Assert.Equal("govuk-header", header.GetAttribute("data-module"));
    }

    [Fact]
    public void Header_RendersDefaultOrganisationName()
    {
        // Arrange & Act
        var cut = Render<GovUkHeader>();

        // Assert
        var logoText = cut.Find(".govuk-header__logotype-text");
        Assert.Equal("GOV.UK", logoText.TextContent);
    }

    [Fact]
    public void Header_RendersCustomOrganisationName()
    {
        // Arrange & Act
        var cut = Render<GovUkHeader>(parameters => parameters
            .Add(p => p.OrganisationName, "My Organisation"));

        // Assert
        var logoText = cut.Find(".govuk-header__logotype-text");
        Assert.Equal("My Organisation", logoText.TextContent);
    }

    [Fact]
    public void Header_RendersProductName()
    {
        // Arrange & Act
        var cut = Render<GovUkHeader>(parameters => parameters
            .Add(p => p.ProductName, "Design System"));

        // Assert
        var productName = cut.Find(".govuk-header__product-name");
        Assert.Equal("Design System", productName.TextContent);
    }

    [Fact]
    public void Header_NoProductName_WhenNotProvided()
    {
        // Arrange & Act
        var cut = Render<GovUkHeader>();

        // Assert
        Assert.Throws<Bunit.ElementNotFoundException>(() => cut.Find(".govuk-header__product-name"));
    }

    [Fact]
    public void Header_AppliesCustomCssClass()
    {
        // Arrange & Act
        var cut = Render<GovUkHeader>(parameters => parameters
            .Add(p => p.CssClass, "custom-class"));

        // Assert
        var header = cut.Find("header.govuk-header");
        Assert.Contains("custom-class", header.ClassName);
    }

    #endregion

    #region Homepage Link Tests

    [Fact]
    public void Header_HomepageLinkDefaultsToRoot()
    {
        // Arrange & Act
        var cut = Render<GovUkHeader>();

        // Assert
        var link = cut.Find(".govuk-header__link--homepage");
        Assert.Equal("/", link.GetAttribute("href"));
    }

    [Fact]
    public void Header_CustomHomepageUrl()
    {
        // Arrange & Act
        var cut = Render<GovUkHeader>(parameters => parameters
            .Add(p => p.HomepageUrl, "/home"));

        // Assert
        var link = cut.Find(".govuk-header__link--homepage");
        Assert.Equal("/home", link.GetAttribute("href"));
    }

    #endregion

    #region Navigation Tests

    [Fact]
    public void Header_RendersNavigation()
    {
        // Arrange & Act
        var cut = Render<GovUkHeader>(parameters => parameters
            .Add(p => p.Navigation, (RenderFragment)(b => 
            {
                b.OpenComponent<GovUkHeaderNavItem>(0);
                b.AddAttribute(1, "Text", "Menu item");
                b.AddAttribute(2, "Href", "/item");
                b.CloseComponent();
            })));

        // Assert
        var nav = cut.Find("nav.govuk-header__navigation");
        Assert.NotNull(nav);
    }

    [Fact]
    public void Header_NavigationHasAriaLabel()
    {
        // Arrange & Act
        var cut = Render<GovUkHeader>(parameters => parameters
            .Add(p => p.Navigation, (RenderFragment)(b => b.AddMarkupContent(0, ""))));

        // Assert
        var nav = cut.Find("nav.govuk-header__navigation");
        Assert.Equal("Menu", nav.GetAttribute("aria-label"));
    }

    [Fact]
    public void Header_CustomNavigationLabel()
    {
        // Arrange & Act
        var cut = Render<GovUkHeader>(parameters => parameters
            .Add(p => p.NavigationLabel, "Main navigation")
            .Add(p => p.Navigation, (RenderFragment)(b => b.AddMarkupContent(0, ""))));

        // Assert
        var nav = cut.Find("nav.govuk-header__navigation");
        Assert.Equal("Main navigation", nav.GetAttribute("aria-label"));
    }

    [Fact]
    public void Header_NoNavigation_WhenNotProvided()
    {
        // Arrange & Act
        var cut = Render<GovUkHeader>();

        // Assert
        Assert.Throws<Bunit.ElementNotFoundException>(() => cut.Find("nav.govuk-header__navigation"));
    }

    [Fact]
    public void Header_RendersNavigationList()
    {
        // Arrange & Act
        var cut = Render<GovUkHeader>(parameters => parameters
            .Add(p => p.Navigation, (RenderFragment)(b => b.AddMarkupContent(0, ""))));

        // Assert
        var list = cut.Find("ul.govuk-header__navigation-list");
        Assert.NotNull(list);
        Assert.Equal("navigation", list.Id);
    }

    #endregion

    #region Menu Button Tests

    [Fact]
    public void Header_ShowsMenuButton_ByDefault()
    {
        // Arrange & Act
        var cut = Render<GovUkHeader>(parameters => parameters
            .Add(p => p.Navigation, (RenderFragment)(b => b.AddMarkupContent(0, ""))));

        // Assert
        var menuButton = cut.Find(".govuk-header__menu-button");
        Assert.NotNull(menuButton);
    }

    [Fact]
    public void Header_MenuButton_HasCorrectAttributes()
    {
        // Arrange & Act
        var cut = Render<GovUkHeader>(parameters => parameters
            .Add(p => p.Navigation, (RenderFragment)(b => b.AddMarkupContent(0, ""))));

        // Assert
        var menuButton = cut.Find(".govuk-header__menu-button");
        Assert.Equal("button", menuButton.GetAttribute("type"));
        Assert.Equal("navigation", menuButton.GetAttribute("aria-controls"));
        Assert.Equal("Show or hide menu", menuButton.GetAttribute("aria-label"));
    }

    #endregion

    #region Container Tests

    [Fact]
    public void Header_HasDefaultContainerClass()
    {
        // Arrange & Act
        var cut = Render<GovUkHeader>();

        // Assert
        var container = cut.Find(".govuk-header__container");
        Assert.Contains("govuk-width-container", container.ClassName);
    }

    [Fact]
    public void Header_CustomContainerClass()
    {
        // Arrange & Act
        var cut = Render<GovUkHeader>(parameters => parameters
            .Add(p => p.ContainerClass, "custom-container"));

        // Assert
        var container = cut.Find(".govuk-header__container");
        Assert.Contains("custom-container", container.ClassName);
    }

    #endregion
}
