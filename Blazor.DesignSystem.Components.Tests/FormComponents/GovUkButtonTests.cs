using Bunit;
using Bunit.TestDoubles;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Blazor.DesignSystem.Components;
using Xunit;

namespace Blazor.DesignSystem.Components.Tests.FormComponents;

/// <summary>
/// Unit tests for GovUkButton component covering rendering, styling, accessibility, and interactions.
/// </summary>
public class GovUkButtonTests : BunitContext
{
    #region Basic Rendering Tests

    [Fact]
    public void Button_RendersWithDefaultType()
    {
        // Arrange & Act
        var cut = Render<GovUkButton>(parameters => parameters
            .Add(p => p.Text, "Submit"));

        // Assert
        var button = cut.Find("button");
        Assert.Equal("submit", button.GetAttribute("type"));
        Assert.Contains("govuk-button", button.ClassName);
    }

    [Fact]
    public void Button_RendersWithCustomType()
    {
        // Arrange & Act
        var cut = Render<GovUkButton>(parameters => parameters
            .Add(p => p.Text, "Reset")
            .Add(p => p.Type, "reset"));

        // Assert
        var button = cut.Find("button");
        Assert.Equal("reset", button.GetAttribute("type"));
    }

    [Fact]
    public void Button_RendersTextContent()
    {
        // Arrange & Act
        var cut = Render<GovUkButton>(parameters => parameters
            .Add(p => p.Text, "Click me"));

        // Assert
        var button = cut.Find("button");
        Assert.Contains("Click me", button.TextContent);
    }

    [Fact]
    public void Button_RendersChildContent()
    {
        // Arrange & Act
        var cut = Render<GovUkButton>(parameters => parameters
            .AddChildContent("<strong>Custom Content</strong>"));

        // Assert
        var button = cut.Find("button");
        var strong = cut.Find("button strong");
        Assert.Contains("Custom Content", strong.TextContent);
    }

    [Fact]
    public void Button_ChildContent_TakesPrecedenceOverText()
    {
        // Arrange & Act
        var cut = Render<GovUkButton>(parameters => parameters
            .Add(p => p.Text, "Text Content")
            .AddChildContent("Child Content"));

        // Assert
        var button = cut.Find("button");
        Assert.Contains("Child Content", button.TextContent);
        Assert.DoesNotContain("Text Content", button.TextContent);
    }

    #endregion

    #region Styling Tests

    [Fact]
    public void Button_AppliesSecondaryClass()
    {
        // Arrange & Act
        var cut = Render<GovUkButton>(parameters => parameters
            .Add(p => p.Text, "Secondary")
            .Add(p => p.IsSecondary, true));

        // Assert
        var button = cut.Find("button");
        Assert.Contains("govuk-button--secondary", button.ClassName);
    }

    [Fact]
    public void Button_AppliesWarningClass()
    {
        // Arrange & Act
        var cut = Render<GovUkButton>(parameters => parameters
            .Add(p => p.Text, "Warning")
            .Add(p => p.IsWarning, true));

        // Assert
        var button = cut.Find("button");
        Assert.Contains("govuk-button--warning", button.ClassName);
    }

    [Fact]
    public void Button_AppliesStartClass_AndRendersIcon()
    {
        // Arrange & Act
        var cut = Render<GovUkButton>(parameters => parameters
            .Add(p => p.Text, "Start")
            .Add(p => p.IsStart, true));

        // Assert
        var button = cut.Find("button");
        Assert.Contains("govuk-button--start", button.ClassName);
        var svg = cut.Find("button svg");
        Assert.NotNull(svg);
        Assert.Contains("govuk-button__start-icon", svg.ClassName);
    }

    [Fact]
    public void Button_AppliesCustomCssClass()
    {
        // Arrange & Act
        var cut = Render<GovUkButton>(parameters => parameters
            .Add(p => p.Text, "Custom")
            .Add(p => p.CssClass, "custom-class"));

        // Assert
        var button = cut.Find("button");
        Assert.Contains("custom-class", button.ClassName);
    }

    [Fact]
    public void Button_DisabledAppliesClass_AndAttribute()
    {
        // Arrange & Act
        var cut = Render<GovUkButton>(parameters => parameters
            .Add(p => p.Text, "Disabled")
            .Add(p => p.Disabled, true));

        // Assert
        var button = cut.Find("button");
        Assert.Contains("govuk-button--disabled", button.ClassName);
        Assert.True(button.HasAttribute("disabled"));
    }

    #endregion

    #region Link Button Tests

    [Fact]
    public void Button_RendersAsLink_WhenHrefProvided()
    {
        // Arrange & Act
        var cut = Render<GovUkButton>(parameters => parameters
            .Add(p => p.Text, "Link")
            .Add(p => p.Href, "/destination"));

        // Assert
        var link = cut.Find("a");
        Assert.Equal("/destination", link.GetAttribute("href"));
        Assert.Equal("button", link.GetAttribute("role"));
        Assert.Contains("govuk-button", link.ClassName);
    }

    [Fact]
    public void Button_LinkHasDraggableFalse()
    {
        // Arrange & Act
        var cut = Render<GovUkButton>(parameters => parameters
            .Add(p => p.Text, "Link")
            .Add(p => p.Href, "/destination"));

        // Assert
        var link = cut.Find("a");
        Assert.Equal("false", link.GetAttribute("draggable"));
    }

    [Fact]
    public void Button_LinkRendersStartIcon()
    {
        // Arrange & Act
        var cut = Render<GovUkButton>(parameters => parameters
            .Add(p => p.Text, "Start")
            .Add(p => p.Href, "/start")
            .Add(p => p.IsStart, true));

        // Assert
        var svg = cut.Find("a svg");
        Assert.NotNull(svg);
        Assert.Contains("govuk-button__start-icon", svg.ClassName);
    }

    #endregion

    #region Accessibility Tests

    [Fact]
    public void Button_HasDataModuleAttribute()
    {
        // Arrange & Act
        var cut = Render<GovUkButton>(parameters => parameters
            .Add(p => p.Text, "Submit"));

        // Assert
        var button = cut.Find("button");
        Assert.Equal("govuk-button", button.GetAttribute("data-module"));
    }

    [Fact]
    public void Button_DisabledHasAriaDisabled()
    {
        // Arrange & Act
        var cut = Render<GovUkButton>(parameters => parameters
            .Add(p => p.Text, "Disabled")
            .Add(p => p.Disabled, true));

        // Assert
        var button = cut.Find("button");
        Assert.Equal("true", button.GetAttribute("aria-disabled"));
    }

    [Fact]
    public void Button_NotDisabledDoesNotHaveAriaDisabled()
    {
        // Arrange & Act
        var cut = Render<GovUkButton>(parameters => parameters
            .Add(p => p.Text, "Enabled")
            .Add(p => p.Disabled, false));

        // Assert
        var button = cut.Find("button");
        Assert.Null(button.GetAttribute("aria-disabled"));
    }

    [Fact]
    public void Button_StartIconIsHiddenFromScreenReaders()
    {
        // Arrange & Act
        var cut = Render<GovUkButton>(parameters => parameters
            .Add(p => p.Text, "Start")
            .Add(p => p.IsStart, true));

        // Assert
        var svg = cut.Find("button svg");
        Assert.Equal("true", svg.GetAttribute("aria-hidden"));
        Assert.Equal("false", svg.GetAttribute("focusable"));
    }

    #endregion

    #region Event Handling Tests

    [Fact]
    public void Button_InvokesOnClick_WhenClicked()
    {
        // Arrange
        var clicked = false;
        var cut = Render<GovUkButton>(parameters => parameters
            .Add(p => p.Text, "Click")
            .Add(p => p.OnClick, EventCallback.Factory.Create<MouseEventArgs>(this, _ => clicked = true)));

        // Act
        cut.Find("button").Click();

        // Assert
        Assert.True(clicked);
    }

    [Fact]
    public void Button_DoesNotInvokeOnClick_WhenDisabled()
    {
        // Arrange
        var clicked = false;
        var cut = Render<GovUkButton>(parameters => parameters
            .Add(p => p.Text, "Click")
            .Add(p => p.Disabled, true)
            .Add(p => p.OnClick, EventCallback.Factory.Create<MouseEventArgs>(this, _ => clicked = true)));

        // Act
        cut.Find("button").Click();

        // Assert
        Assert.False(clicked);
    }

    #endregion

    #region Additional Attributes Tests

    [Fact]
    public void Button_PassesAdditionalAttributes()
    {
        // Arrange & Act
        var cut = Render<GovUkButton>(parameters => parameters
            .Add(p => p.Text, "Submit")
            .AddUnmatched("data-test-id", "test-button")
            .AddUnmatched("id", "my-button"));

        // Assert
        var button = cut.Find("button");
        Assert.Equal("test-button", button.GetAttribute("data-test-id"));
        Assert.Equal("my-button", button.Id);
    }

    #endregion
}
