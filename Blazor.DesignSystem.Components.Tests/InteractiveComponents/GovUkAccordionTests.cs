using Bunit;
using Microsoft.AspNetCore.Components;
using Blazor.DesignSystem.Components;
using Xunit;

namespace Blazor.DesignSystem.Components.Tests.InteractiveComponents;

/// <summary>
/// Unit tests for GovUkAccordion and GovUkAccordionSection components covering rendering, 
/// interactions, accessibility, and state management.
/// </summary>
public class GovUkAccordionTests : BunitContext
{
    #region Basic Rendering Tests

    [Fact]
    public void Accordion_RendersWithCorrectClass()
    {
        // Arrange & Act
        var cut = Render<GovUkAccordion>(parameters => parameters
            .Add(p => p.Id, "accordion-1"));

        // Assert
        var accordion = cut.Find(".govuk-accordion");
        Assert.NotNull(accordion);
    }

    [Fact]
    public void Accordion_HasDataModuleAttribute()
    {
        // Arrange & Act
        var cut = Render<GovUkAccordion>(parameters => parameters
            .Add(p => p.Id, "accordion-1"));

        // Assert
        var accordion = cut.Find(".govuk-accordion");
        Assert.Equal("govuk-accordion", accordion.GetAttribute("data-module"));
    }

    [Fact]
    public void Accordion_RendersWithId()
    {
        // Arrange & Act
        var cut = Render<GovUkAccordion>(parameters => parameters
            .Add(p => p.Id, "my-accordion"));

        // Assert
        var accordion = cut.Find(".govuk-accordion");
        Assert.Equal("my-accordion", accordion.Id);
    }

    [Fact]
    public void Accordion_RendersShowHideAllButton()
    {
        // Arrange & Act
        var cut = Render<GovUkAccordion>(parameters => parameters
            .Add(p => p.Id, "accordion-1"));

        // Assert
        var button = cut.Find(".govuk-accordion__show-all");
        Assert.NotNull(button);
        Assert.Contains("Show all sections", button.TextContent);
    }

    [Fact]
    public void Accordion_AppliesCustomCssClass()
    {
        // Arrange & Act
        var cut = Render<GovUkAccordion>(parameters => parameters
            .Add(p => p.Id, "accordion-1")
            .Add(p => p.CssClass, "custom-class"));

        // Assert
        var accordion = cut.Find(".govuk-accordion");
        Assert.Contains("custom-class", accordion.ClassName);
    }

    #endregion

    #region Accessibility Tests

    [Fact]
    public void Accordion_ShowAllButton_HasAriaExpanded()
    {
        // Arrange & Act
        var cut = Render<GovUkAccordion>(parameters => parameters
            .Add(p => p.Id, "accordion-1"));

        // Assert
        var button = cut.Find(".govuk-accordion__show-all");
        Assert.Equal("false", button.GetAttribute("aria-expanded"));
    }

    [Fact]
    public void Accordion_ShowAllButton_HasCorrectType()
    {
        // Arrange & Act
        var cut = Render<GovUkAccordion>(parameters => parameters
            .Add(p => p.Id, "accordion-1"));

        // Assert
        var button = cut.Find(".govuk-accordion__show-all");
        Assert.Equal("button", button.GetAttribute("type"));
    }

    #endregion
}

/// <summary>
/// Unit tests for GovUkAccordionSection component.
/// </summary>
public class GovUkAccordionSectionTests : BunitContext
{
    [Fact]
    public void AccordionSection_RendersWithinAccordion()
    {
        // Arrange & Act
        var cut = Render<GovUkAccordion>(parameters => parameters
            .Add(p => p.Id, "accordion-1")
            .AddChildContent<GovUkAccordionSection>(p => p
                .Add(x => x.Heading, "Section 1")
                .Add(x => x.Content, (RenderFragment)(b => b.AddMarkupContent(0, "Section 1 content")))));

        // Assert
        var section = cut.Find(".govuk-accordion__section");
        Assert.NotNull(section);
    }

    [Fact]
    public void AccordionSection_RendersHeading()
    {
        // Arrange & Act
        var cut = Render<GovUkAccordion>(parameters => parameters
            .Add(p => p.Id, "accordion-1")
            .AddChildContent<GovUkAccordionSection>(p => p
                .Add(x => x.Heading, "Writing well for the web")
                .Add(x => x.Content, (RenderFragment)(b => b.AddMarkupContent(0, "Content here")))));

        // Assert
        var heading = cut.Find(".govuk-accordion__section-heading");
        Assert.Contains("Writing well for the web", heading.TextContent);
    }

    [Fact]
    public void AccordionSection_RendersSummary()
    {
        // Arrange & Act
        var cut = Render<GovUkAccordion>(parameters => parameters
            .Add(p => p.Id, "accordion-1")
            .AddChildContent<GovUkAccordionSection>(p => p
                .Add(x => x.Heading, "Section 1")
                .Add(x => x.Summary, "A summary of this section")
                .Add(x => x.Content, (RenderFragment)(b => b.AddMarkupContent(0, "Content here")))));

        // Assert
        var summary = cut.Find(".govuk-accordion__section-summary");
        Assert.Contains("A summary of this section", summary.TextContent);
    }

    [Fact]
    public void AccordionSection_HasExpandCollapseButton()
    {
        // Arrange & Act
        var cut = Render<GovUkAccordion>(parameters => parameters
            .Add(p => p.Id, "accordion-1")
            .AddChildContent<GovUkAccordionSection>(p => p
                .Add(x => x.Heading, "Section 1")
                .Add(x => x.Content, (RenderFragment)(b => b.AddMarkupContent(0, "Content here")))));

        // Assert
        var button = cut.Find(".govuk-accordion__section-button");
        Assert.NotNull(button);
        Assert.Equal("button", button.GetAttribute("type"));
    }

    [Fact]
    public void AccordionSection_ContentHiddenByDefault()
    {
        // Arrange & Act
        var cut = Render<GovUkAccordion>(parameters => parameters
            .Add(p => p.Id, "accordion-1")
            .AddChildContent<GovUkAccordionSection>(p => p
                .Add(x => x.Heading, "Section 1")
                .Add(x => x.Content, (RenderFragment)(b => b.AddMarkupContent(0, "Content here")))));

        // Assert
        var content = cut.Find(".govuk-accordion__section-content");
        Assert.True(content.HasAttribute("hidden"));
    }

    [Fact]
    public void AccordionSection_ExpandsWhenExpanded()
    {
        // Arrange & Act
        var cut = Render<GovUkAccordion>(parameters => parameters
            .Add(p => p.Id, "accordion-1")
            .AddChildContent<GovUkAccordionSection>(p => p
                .Add(x => x.Heading, "Section 1")
                .Add(x => x.IsExpanded, true)
                .Add(x => x.Content, (RenderFragment)(b => b.AddMarkupContent(0, "Content here")))));

        // Assert
        var section = cut.Find(".govuk-accordion__section");
        Assert.Contains("govuk-accordion__section--expanded", section.ClassName);
        
        var content = cut.Find(".govuk-accordion__section-content");
        Assert.False(content.HasAttribute("hidden"));
    }

    [Fact]
    public void AccordionSection_Button_HasAriaExpanded()
    {
        // Arrange & Act
        var cut = Render<GovUkAccordion>(parameters => parameters
            .Add(p => p.Id, "accordion-1")
            .AddChildContent<GovUkAccordionSection>(p => p
                .Add(x => x.Heading, "Section 1")
                .Add(x => x.Content, (RenderFragment)(b => b.AddMarkupContent(0, "Content here")))));

        // Assert
        var button = cut.Find(".govuk-accordion__section-button");
        Assert.Equal("false", button.GetAttribute("aria-expanded"));
    }

    [Fact]
    public void AccordionSection_Button_HasAriaControls()
    {
        // Arrange & Act
        var cut = Render<GovUkAccordion>(parameters => parameters
            .Add(p => p.Id, "accordion-1")
            .AddChildContent<GovUkAccordionSection>(p => p
                .Add(x => x.Heading, "Section 1")
                .Add(x => x.Content, (RenderFragment)(b => b.AddMarkupContent(0, "Content here")))));

        // Assert
        var button = cut.Find(".govuk-accordion__section-button");
        Assert.NotNull(button.GetAttribute("aria-controls"));
    }

    [Fact]
    public void AccordionSection_TogglesOnClick()
    {
        // Arrange
        var cut = Render<GovUkAccordion>(parameters => parameters
            .Add(p => p.Id, "accordion-1")
            .AddChildContent<GovUkAccordionSection>(p => p
                .Add(x => x.Heading, "Section 1")
                .Add(x => x.Content, (RenderFragment)(b => b.AddMarkupContent(0, "Content here")))));

        // Act
        cut.Find(".govuk-accordion__section-button").Click();

        // Assert
        var section = cut.Find(".govuk-accordion__section");
        Assert.Contains("govuk-accordion__section--expanded", section.ClassName);
    }

    [Fact]
    public void AccordionSection_ContentHasAriaLabelledBy()
    {
        // Arrange & Act
        var cut = Render<GovUkAccordion>(parameters => parameters
            .Add(p => p.Id, "accordion-1")
            .AddChildContent<GovUkAccordionSection>(p => p
                .Add(x => x.Heading, "Section 1")
                .Add(x => x.Content, (RenderFragment)(b => b.AddMarkupContent(0, "Content here")))));

        // Assert
        var content = cut.Find(".govuk-accordion__section-content");
        Assert.NotNull(content.GetAttribute("aria-labelledby"));
    }
}
