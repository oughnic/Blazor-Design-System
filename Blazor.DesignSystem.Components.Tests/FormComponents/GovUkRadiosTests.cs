using Bunit;
using Microsoft.AspNetCore.Components;
using Blazor.DesignSystem.Components;
using Xunit;

namespace Blazor.DesignSystem.Components.Tests.FormComponents;

/// <summary>
/// Unit tests for GovUkRadios and GovUkRadioItem components covering rendering, 
/// styling, accessibility, data binding, and error states.
/// </summary>
public class GovUkRadiosTests : BunitContext
{
    #region Basic Rendering Tests

    [Fact]
    public void Radios_RendersWithFormGroupWrapper()
    {
        // Arrange & Act
        var cut = Render<GovUkRadios>(parameters => parameters
            .Add(p => p.Name, "contact")
            .Add(p => p.Legend, "How would you like to be contacted?"));

        // Assert
        var formGroup = cut.Find(".govuk-form-group");
        Assert.NotNull(formGroup);
    }

    [Fact]
    public void Radios_RendersFieldset()
    {
        // Arrange & Act
        var cut = Render<GovUkRadios>(parameters => parameters
            .Add(p => p.Name, "contact")
            .Add(p => p.Legend, "How would you like to be contacted?"));

        // Assert
        var fieldset = cut.Find("fieldset.govuk-fieldset");
        Assert.NotNull(fieldset);
    }

    [Fact]
    public void Radios_RendersLegend()
    {
        // Arrange & Act
        var cut = Render<GovUkRadios>(parameters => parameters
            .Add(p => p.Name, "contact")
            .Add(p => p.Legend, "How would you like to be contacted?"));

        // Assert
        var legend = cut.Find("legend.govuk-fieldset__legend");
        Assert.Contains("How would you like to be contacted?", legend.TextContent);
    }

    [Fact]
    public void Radios_RendersLegendAsHeading()
    {
        // Arrange & Act
        var cut = Render<GovUkRadios>(parameters => parameters
            .Add(p => p.Name, "contact")
            .Add(p => p.Legend, "How would you like to be contacted?")
            .Add(p => p.IsPageHeading, true));

        // Assert
        var heading = cut.Find("legend h1.govuk-fieldset__heading");
        Assert.Equal("How would you like to be contacted?", heading.TextContent);
    }

    [Fact]
    public void Radios_RendersHint()
    {
        // Arrange & Act
        var cut = Render<GovUkRadios>(parameters => parameters
            .Add(p => p.Name, "contact")
            .Add(p => p.Legend, "How would you like to be contacted?")
            .Add(p => p.Hint, "Select one option"));

        // Assert
        var hint = cut.Find(".govuk-hint");
        Assert.Equal("Select one option", hint.TextContent);
        Assert.Equal("contact-hint", hint.Id);
    }

    [Fact]
    public void Radios_RendersRadioItems()
    {
        // Arrange & Act
        var cut = Render<GovUkRadios>(parameters => parameters
            .Add(p => p.Name, "contact")
            .Add(p => p.Legend, "How would you like to be contacted?")
            .AddChildContent<GovUkRadioItem>(p => p
                .Add(x => x.Value, "email")
                .Add(x => x.Label, "Email"))
            .AddChildContent<GovUkRadioItem>(p => p
                .Add(x => x.Value, "phone")
                .Add(x => x.Label, "Phone")));

        // Assert
        var radioItems = cut.FindAll(".govuk-radios__item");
        Assert.Equal(2, radioItems.Count);
    }

    [Fact]
    public void Radios_HasDataModule()
    {
        // Arrange & Act
        var cut = Render<GovUkRadios>(parameters => parameters
            .Add(p => p.Name, "contact")
            .Add(p => p.Legend, "How would you like to be contacted?"));

        // Assert
        var radiosDiv = cut.Find(".govuk-radios");
        Assert.Equal("govuk-radios", radiosDiv.GetAttribute("data-module"));
    }

    #endregion

    #region Data Binding Tests

    [Fact]
    public void Radios_SelectedValue_InitializesCorrectly()
    {
        // Arrange & Act
        var cut = Render<GovUkRadios>(parameters => parameters
            .Add(p => p.Name, "contact")
            .Add(p => p.Legend, "How would you like to be contacted?")
            .Add(p => p.SelectedValue, "email")
            .AddChildContent<GovUkRadioItem>(p => p
                .Add(x => x.Value, "email")
                .Add(x => x.Label, "Email"))
            .AddChildContent<GovUkRadioItem>(p => p
                .Add(x => x.Value, "phone")
                .Add(x => x.Label, "Phone")));

        // Assert
        var radioInputs = cut.FindAll(".govuk-radios__input");
        Assert.True(radioInputs[0].HasAttribute("checked"));
        Assert.False(radioInputs[1].HasAttribute("checked"));
    }

    [Fact]
    public void Radios_InvokesSelectedValueChanged_OnSelection()
    {
        // Arrange
        string? newSelection = null;
        var cut = Render<GovUkRadios>(parameters => parameters
            .Add(p => p.Name, "contact")
            .Add(p => p.Legend, "How would you like to be contacted?")
            .Add(p => p.SelectedValueChanged, EventCallback.Factory.Create<string?>(this, v => newSelection = v))
            .AddChildContent<GovUkRadioItem>(p => p
                .Add(x => x.Value, "email")
                .Add(x => x.Label, "Email")));

        // Act
        cut.Find(".govuk-radios__input").Change("email");

        // Assert
        Assert.Equal("email", newSelection);
    }

    [Fact]
    public void Radios_OnlyOneCanBeSelected()
    {
        // Arrange & Act
        var cut = Render<GovUkRadios>(parameters => parameters
            .Add(p => p.Name, "contact")
            .Add(p => p.Legend, "How would you like to be contacted?")
            .Add(p => p.SelectedValue, "phone")
            .AddChildContent<GovUkRadioItem>(p => p
                .Add(x => x.Value, "email")
                .Add(x => x.Label, "Email"))
            .AddChildContent<GovUkRadioItem>(p => p
                .Add(x => x.Value, "phone")
                .Add(x => x.Label, "Phone")));

        // Assert - only phone should be selected
        var radioInputs = cut.FindAll(".govuk-radios__input");
        Assert.False(radioInputs[0].HasAttribute("checked"));
        Assert.True(radioInputs[1].HasAttribute("checked"));
    }

    #endregion

    #region Error State Tests

    [Fact]
    public void Radios_ErrorState_AppliesErrorClass()
    {
        // Arrange & Act
        var cut = Render<GovUkRadios>(parameters => parameters
            .Add(p => p.Name, "contact")
            .Add(p => p.Legend, "How would you like to be contacted?")
            .Add(p => p.HasError, true)
            .Add(p => p.ErrorMessage, "Select how you would like to be contacted"));

        // Assert
        var formGroup = cut.Find(".govuk-form-group");
        Assert.Contains("govuk-form-group--error", formGroup.ClassName);
    }

    [Fact]
    public void Radios_ErrorState_RendersErrorMessage()
    {
        // Arrange & Act
        var cut = Render<GovUkRadios>(parameters => parameters
            .Add(p => p.Name, "contact")
            .Add(p => p.Legend, "How would you like to be contacted?")
            .Add(p => p.HasError, true)
            .Add(p => p.ErrorMessage, "Select how you would like to be contacted"));

        // Assert
        var errorMessage = cut.Find(".govuk-error-message");
        Assert.Contains("Select how you would like to be contacted", errorMessage.TextContent);
        Assert.Equal("contact-error", errorMessage.Id);
    }

    [Fact]
    public void Radios_ErrorState_HasVisuallyHiddenPrefix()
    {
        // Arrange & Act
        var cut = Render<GovUkRadios>(parameters => parameters
            .Add(p => p.Name, "contact")
            .Add(p => p.Legend, "How would you like to be contacted?")
            .Add(p => p.HasError, true)
            .Add(p => p.ErrorMessage, "Select how you would like to be contacted"));

        // Assert
        var visuallyHidden = cut.Find(".govuk-error-message .govuk-visually-hidden");
        Assert.Contains("Error", visuallyHidden.TextContent);
    }

    #endregion

    #region Accessibility Tests

    [Fact]
    public void Radios_FieldsetHasAriaDescribedBy_WithHint()
    {
        // Arrange & Act
        var cut = Render<GovUkRadios>(parameters => parameters
            .Add(p => p.Name, "contact")
            .Add(p => p.Legend, "How would you like to be contacted?")
            .Add(p => p.Hint, "Select one option"));

        // Assert
        var fieldset = cut.Find("fieldset.govuk-fieldset");
        Assert.Contains("contact-hint", fieldset.GetAttribute("aria-describedby"));
    }

    [Fact]
    public void Radios_FieldsetHasAriaDescribedBy_WithError()
    {
        // Arrange & Act
        var cut = Render<GovUkRadios>(parameters => parameters
            .Add(p => p.Name, "contact")
            .Add(p => p.Legend, "How would you like to be contacted?")
            .Add(p => p.HasError, true)
            .Add(p => p.ErrorMessage, "Select how you would like to be contacted"));

        // Assert
        var fieldset = cut.Find("fieldset.govuk-fieldset");
        Assert.Contains("contact-error", fieldset.GetAttribute("aria-describedby"));
    }

    [Fact]
    public void RadioItem_HasLabelLinkedToInput()
    {
        // Arrange & Act
        var cut = Render<GovUkRadios>(parameters => parameters
            .Add(p => p.Name, "contact")
            .Add(p => p.Legend, "How would you like to be contacted?")
            .AddChildContent<GovUkRadioItem>(p => p
                .Add(x => x.Value, "email")
                .Add(x => x.Label, "Email")
                .Add(x => x.Id, "radio-email")));

        // Assert
        var label = cut.Find(".govuk-radios__label");
        var input = cut.Find(".govuk-radios__input");
        Assert.Equal(input.Id, label.GetAttribute("for"));
        Assert.Equal("radio-email", input.Id);
    }

    [Fact]
    public void RadioItem_InputHasCorrectType()
    {
        // Arrange & Act
        var cut = Render<GovUkRadios>(parameters => parameters
            .Add(p => p.Name, "contact")
            .Add(p => p.Legend, "How would you like to be contacted?")
            .AddChildContent<GovUkRadioItem>(p => p
                .Add(x => x.Value, "email")
                .Add(x => x.Label, "Email")));

        // Assert
        var input = cut.Find(".govuk-radios__input");
        Assert.Equal("radio", input.GetAttribute("type"));
    }

    [Fact]
    public void RadioItem_InputHasCorrectName()
    {
        // Arrange & Act
        var cut = Render<GovUkRadios>(parameters => parameters
            .Add(p => p.Name, "contact")
            .Add(p => p.Legend, "How would you like to be contacted?")
            .AddChildContent<GovUkRadioItem>(p => p
                .Add(x => x.Value, "email")
                .Add(x => x.Label, "Email")));

        // Assert
        var input = cut.Find(".govuk-radios__input");
        Assert.Equal("contact", input.GetAttribute("name"));
    }

    [Fact]
    public void RadioItem_WithHint_HasAriaDescribedBy()
    {
        // Arrange & Act
        var cut = Render<GovUkRadios>(parameters => parameters
            .Add(p => p.Name, "contact")
            .Add(p => p.Legend, "How would you like to be contacted?")
            .AddChildContent<GovUkRadioItem>(p => p
                .Add(x => x.Value, "email")
                .Add(x => x.Label, "Email")
                .Add(x => x.Hint, "We will only use this to confirm your order")
                .Add(x => x.Id, "radio-email")));

        // Assert
        var input = cut.Find(".govuk-radios__input");
        Assert.Equal("radio-email-hint", input.GetAttribute("aria-describedby"));
    }

    #endregion

    #region Styling Tests

    [Fact]
    public void Radios_AppliesSmallVariant()
    {
        // Arrange & Act
        var cut = Render<GovUkRadios>(parameters => parameters
            .Add(p => p.Name, "contact")
            .Add(p => p.Legend, "How would you like to be contacted?")
            .Add(p => p.IsSmall, true));

        // Assert
        var radiosDiv = cut.Find(".govuk-radios");
        Assert.Contains("govuk-radios--small", radiosDiv.ClassName);
    }

    [Fact]
    public void Radios_AppliesInlineVariant()
    {
        // Arrange & Act
        var cut = Render<GovUkRadios>(parameters => parameters
            .Add(p => p.Name, "contact")
            .Add(p => p.Legend, "How would you like to be contacted?")
            .Add(p => p.IsInline, true));

        // Assert
        var radiosDiv = cut.Find(".govuk-radios");
        Assert.Contains("govuk-radios--inline", radiosDiv.ClassName);
    }

    [Fact]
    public void Radios_AppliesCustomCssClass()
    {
        // Arrange & Act
        var cut = Render<GovUkRadios>(parameters => parameters
            .Add(p => p.Name, "contact")
            .Add(p => p.Legend, "How would you like to be contacted?")
            .Add(p => p.CssClass, "custom-class"));

        // Assert
        var formGroup = cut.Find(".govuk-form-group");
        Assert.Contains("custom-class", formGroup.ClassName);
    }

    [Fact]
    public void Radios_AppliesLegendCssClass()
    {
        // Arrange & Act
        var cut = Render<GovUkRadios>(parameters => parameters
            .Add(p => p.Name, "contact")
            .Add(p => p.Legend, "How would you like to be contacted?")
            .Add(p => p.LegendCssClass, "govuk-fieldset__legend--l"));

        // Assert
        var legend = cut.Find("legend.govuk-fieldset__legend");
        Assert.Contains("govuk-fieldset__legend--l", legend.ClassName);
    }

    #endregion

    #region Radio Item Tests

    [Fact]
    public void RadioItem_RendersLabel()
    {
        // Arrange & Act
        var cut = Render<GovUkRadios>(parameters => parameters
            .Add(p => p.Name, "contact")
            .Add(p => p.Legend, "How would you like to be contacted?")
            .AddChildContent<GovUkRadioItem>(p => p
                .Add(x => x.Value, "email")
                .Add(x => x.Label, "Email")));

        // Assert
        var label = cut.Find(".govuk-radios__label");
        Assert.Contains("Email", label.TextContent);
    }

    [Fact]
    public void RadioItem_RendersHint()
    {
        // Arrange & Act
        var cut = Render<GovUkRadios>(parameters => parameters
            .Add(p => p.Name, "contact")
            .Add(p => p.Legend, "How would you like to be contacted?")
            .AddChildContent<GovUkRadioItem>(p => p
                .Add(x => x.Value, "email")
                .Add(x => x.Label, "Email")
                .Add(x => x.Hint, "We will only use this to confirm your order")));

        // Assert
        var hint = cut.Find(".govuk-radios__hint");
        Assert.Contains("We will only use this to confirm your order", hint.TextContent);
    }

    [Fact]
    public void RadioItem_Disabled()
    {
        // Arrange & Act
        var cut = Render<GovUkRadios>(parameters => parameters
            .Add(p => p.Name, "contact")
            .Add(p => p.Legend, "How would you like to be contacted?")
            .AddChildContent<GovUkRadioItem>(p => p
                .Add(x => x.Value, "email")
                .Add(x => x.Label, "Email")
                .Add(x => x.Disabled, true)));

        // Assert
        var input = cut.Find(".govuk-radios__input");
        Assert.True(input.HasAttribute("disabled"));
    }

    [Fact]
    public void RadioItem_ConditionalContent_Hidden_WhenNotChecked()
    {
        // Arrange & Act
        var cut = Render<GovUkRadios>(parameters => parameters
            .Add(p => p.Name, "contact")
            .Add(p => p.Legend, "How would you like to be contacted?")
            .AddChildContent<GovUkRadioItem>(p => p
                .Add(x => x.Value, "email")
                .Add(x => x.Label, "Email")
                .Add(x => x.Id, "contact-email")
                .Add(x => x.HasConditional, true)
                .Add(x => x.ConditionalContent, (RenderFragment)(b => b.AddMarkupContent(0, "<p>Email content</p>")))));

        // Assert
        var conditional = cut.Find(".govuk-radios__conditional");
        Assert.Contains("govuk-radios__conditional--hidden", conditional.ClassName);
    }

    [Fact]
    public void RadioItem_ConditionalContent_HasAriaControls()
    {
        // Arrange & Act
        var cut = Render<GovUkRadios>(parameters => parameters
            .Add(p => p.Name, "contact")
            .Add(p => p.Legend, "How would you like to be contacted?")
            .AddChildContent<GovUkRadioItem>(p => p
                .Add(x => x.Value, "email")
                .Add(x => x.Label, "Email")
                .Add(x => x.Id, "contact-email")
                .Add(x => x.HasConditional, true)
                .Add(x => x.ConditionalContent, (RenderFragment)(b => b.AddMarkupContent(0, "<p>Email content</p>")))));

        // Assert
        var input = cut.Find(".govuk-radios__input");
        Assert.Equal("conditional-contact-email", input.GetAttribute("aria-controls"));
    }

    #endregion
}
