using Bunit;
using Microsoft.AspNetCore.Components;
using Blazor.DesignSystem.Components;
using Xunit;

namespace Blazor.DesignSystem.Components.Tests.FormComponents;

/// <summary>
/// Unit tests for GovUkCheckboxes and GovUkCheckboxItem components covering rendering, 
/// styling, accessibility, data binding, and error states.
/// </summary>
public class GovUkCheckboxesTests : BunitContext
{
    #region Basic Rendering Tests

    [Fact]
    public void Checkboxes_RendersWithFormGroupWrapper()
    {
        // Arrange & Act
        var cut = Render<GovUkCheckboxes>(parameters => parameters
            .Add(p => p.Name, "nationality")
            .Add(p => p.Legend, "What is your nationality?"));

        // Assert
        var formGroup = cut.Find(".govuk-form-group");
        Assert.NotNull(formGroup);
    }

    [Fact]
    public void Checkboxes_RendersFieldset()
    {
        // Arrange & Act
        var cut = Render<GovUkCheckboxes>(parameters => parameters
            .Add(p => p.Name, "nationality")
            .Add(p => p.Legend, "What is your nationality?"));

        // Assert
        var fieldset = cut.Find("fieldset.govuk-fieldset");
        Assert.NotNull(fieldset);
    }

    [Fact]
    public void Checkboxes_RendersLegend()
    {
        // Arrange & Act
        var cut = Render<GovUkCheckboxes>(parameters => parameters
            .Add(p => p.Name, "nationality")
            .Add(p => p.Legend, "What is your nationality?"));

        // Assert
        var legend = cut.Find("legend.govuk-fieldset__legend");
        Assert.Contains("What is your nationality?", legend.TextContent);
    }

    [Fact]
    public void Checkboxes_RendersLegendAsHeading()
    {
        // Arrange & Act
        var cut = Render<GovUkCheckboxes>(parameters => parameters
            .Add(p => p.Name, "nationality")
            .Add(p => p.Legend, "What is your nationality?")
            .Add(p => p.IsPageHeading, true));

        // Assert
        var heading = cut.Find("legend h1.govuk-fieldset__heading");
        Assert.Equal("What is your nationality?", heading.TextContent);
    }

    [Fact]
    public void Checkboxes_RendersHint()
    {
        // Arrange & Act
        var cut = Render<GovUkCheckboxes>(parameters => parameters
            .Add(p => p.Name, "nationality")
            .Add(p => p.Legend, "What is your nationality?")
            .Add(p => p.Hint, "Select all that apply"));

        // Assert
        var hint = cut.Find(".govuk-hint");
        Assert.Equal("Select all that apply", hint.TextContent);
        Assert.Equal("nationality-hint", hint.Id);
    }

    [Fact]
    public void Checkboxes_RendersCheckboxItems()
    {
        // Arrange & Act
        var cut = Render<GovUkCheckboxes>(parameters => parameters
            .Add(p => p.Name, "nationality")
            .Add(p => p.Legend, "What is your nationality?")
            .AddChildContent<GovUkCheckboxItem>(p => p
                .Add(x => x.Value, "british")
                .Add(x => x.Label, "British"))
            .AddChildContent<GovUkCheckboxItem>(p => p
                .Add(x => x.Value, "irish")
                .Add(x => x.Label, "Irish")));

        // Assert
        var checkboxItems = cut.FindAll(".govuk-checkboxes__item");
        Assert.Equal(2, checkboxItems.Count);
    }

    [Fact]
    public void Checkboxes_HasDataModule()
    {
        // Arrange & Act
        var cut = Render<GovUkCheckboxes>(parameters => parameters
            .Add(p => p.Name, "nationality")
            .Add(p => p.Legend, "What is your nationality?"));

        // Assert
        var checkboxesDiv = cut.Find(".govuk-checkboxes");
        Assert.Equal("govuk-checkboxes", checkboxesDiv.GetAttribute("data-module"));
    }

    #endregion

    #region Data Binding Tests

    [Fact]
    public void Checkboxes_SelectedValues_InitializesCorrectly()
    {
        // Arrange
        var selectedValues = new List<string> { "british" };
        
        // Act
        var cut = Render<GovUkCheckboxes>(parameters => parameters
            .Add(p => p.Name, "nationality")
            .Add(p => p.Legend, "What is your nationality?")
            .Add(p => p.SelectedValues, selectedValues)
            .AddChildContent<GovUkCheckboxItem>(p => p
                .Add(x => x.Value, "british")
                .Add(x => x.Label, "British"))
            .AddChildContent<GovUkCheckboxItem>(p => p
                .Add(x => x.Value, "irish")
                .Add(x => x.Label, "Irish")));

        // Assert
        var checkboxInputs = cut.FindAll(".govuk-checkboxes__input");
        Assert.True(checkboxInputs[0].HasAttribute("checked"));
        Assert.False(checkboxInputs[1].HasAttribute("checked"));
    }

    [Fact]
    public void Checkboxes_InvokesSelectedValuesChanged_OnSelection()
    {
        // Arrange
        List<string>? newSelection = null;
        var cut = Render<GovUkCheckboxes>(parameters => parameters
            .Add(p => p.Name, "nationality")
            .Add(p => p.Legend, "What is your nationality?")
            .Add(p => p.SelectedValuesChanged, EventCallback.Factory.Create<List<string>>(this, v => newSelection = v))
            .AddChildContent<GovUkCheckboxItem>(p => p
                .Add(x => x.Value, "british")
                .Add(x => x.Label, "British")));

        // Act
        cut.Find(".govuk-checkboxes__input").Change(true);

        // Assert
        Assert.NotNull(newSelection);
        Assert.Contains("british", newSelection);
    }

    [Fact]
    public void Checkboxes_AllowsMultipleSelections()
    {
        // Arrange
        var selectedValues = new List<string> { "british", "irish" };
        
        // Act
        var cut = Render<GovUkCheckboxes>(parameters => parameters
            .Add(p => p.Name, "nationality")
            .Add(p => p.Legend, "What is your nationality?")
            .Add(p => p.SelectedValues, selectedValues)
            .AddChildContent<GovUkCheckboxItem>(p => p
                .Add(x => x.Value, "british")
                .Add(x => x.Label, "British"))
            .AddChildContent<GovUkCheckboxItem>(p => p
                .Add(x => x.Value, "irish")
                .Add(x => x.Label, "Irish")));

        // Assert
        var checkboxInputs = cut.FindAll(".govuk-checkboxes__input");
        Assert.True(checkboxInputs[0].HasAttribute("checked"));
        Assert.True(checkboxInputs[1].HasAttribute("checked"));
    }

    #endregion

    #region Error State Tests

    [Fact]
    public void Checkboxes_ErrorState_AppliesErrorClass()
    {
        // Arrange & Act
        var cut = Render<GovUkCheckboxes>(parameters => parameters
            .Add(p => p.Name, "nationality")
            .Add(p => p.Legend, "What is your nationality?")
            .Add(p => p.HasError, true)
            .Add(p => p.ErrorMessage, "Select at least one nationality"));

        // Assert
        var formGroup = cut.Find(".govuk-form-group");
        Assert.Contains("govuk-form-group--error", formGroup.ClassName);
    }

    [Fact]
    public void Checkboxes_ErrorState_RendersErrorMessage()
    {
        // Arrange & Act
        var cut = Render<GovUkCheckboxes>(parameters => parameters
            .Add(p => p.Name, "nationality")
            .Add(p => p.Legend, "What is your nationality?")
            .Add(p => p.HasError, true)
            .Add(p => p.ErrorMessage, "Select at least one nationality"));

        // Assert
        var errorMessage = cut.Find(".govuk-error-message");
        Assert.Contains("Select at least one nationality", errorMessage.TextContent);
        Assert.Equal("nationality-error", errorMessage.Id);
    }

    [Fact]
    public void Checkboxes_ErrorState_HasVisuallyHiddenPrefix()
    {
        // Arrange & Act
        var cut = Render<GovUkCheckboxes>(parameters => parameters
            .Add(p => p.Name, "nationality")
            .Add(p => p.Legend, "What is your nationality?")
            .Add(p => p.HasError, true)
            .Add(p => p.ErrorMessage, "Select at least one nationality"));

        // Assert
        var visuallyHidden = cut.Find(".govuk-error-message .govuk-visually-hidden");
        Assert.Contains("Error", visuallyHidden.TextContent);
    }

    #endregion

    #region Accessibility Tests

    [Fact]
    public void Checkboxes_FieldsetHasAriaDescribedBy_WithHint()
    {
        // Arrange & Act
        var cut = Render<GovUkCheckboxes>(parameters => parameters
            .Add(p => p.Name, "nationality")
            .Add(p => p.Legend, "What is your nationality?")
            .Add(p => p.Hint, "Select all that apply"));

        // Assert
        var fieldset = cut.Find("fieldset.govuk-fieldset");
        Assert.Contains("nationality-hint", fieldset.GetAttribute("aria-describedby"));
    }

    [Fact]
    public void Checkboxes_FieldsetHasAriaDescribedBy_WithError()
    {
        // Arrange & Act
        var cut = Render<GovUkCheckboxes>(parameters => parameters
            .Add(p => p.Name, "nationality")
            .Add(p => p.Legend, "What is your nationality?")
            .Add(p => p.HasError, true)
            .Add(p => p.ErrorMessage, "Select at least one nationality"));

        // Assert
        var fieldset = cut.Find("fieldset.govuk-fieldset");
        Assert.Contains("nationality-error", fieldset.GetAttribute("aria-describedby"));
    }

    [Fact]
    public void CheckboxItem_HasLabelLinkedToInput()
    {
        // Arrange & Act
        var cut = Render<GovUkCheckboxes>(parameters => parameters
            .Add(p => p.Name, "nationality")
            .Add(p => p.Legend, "What is your nationality?")
            .AddChildContent<GovUkCheckboxItem>(p => p
                .Add(x => x.Value, "british")
                .Add(x => x.Label, "British")
                .Add(x => x.Id, "checkbox-british")));

        // Assert
        var label = cut.Find(".govuk-checkboxes__label");
        var input = cut.Find(".govuk-checkboxes__input");
        Assert.Equal(input.Id, label.GetAttribute("for"));
        Assert.Equal("checkbox-british", input.Id);
    }

    [Fact]
    public void CheckboxItem_InputHasCorrectType()
    {
        // Arrange & Act
        var cut = Render<GovUkCheckboxes>(parameters => parameters
            .Add(p => p.Name, "nationality")
            .Add(p => p.Legend, "What is your nationality?")
            .AddChildContent<GovUkCheckboxItem>(p => p
                .Add(x => x.Value, "british")
                .Add(x => x.Label, "British")));

        // Assert
        var input = cut.Find(".govuk-checkboxes__input");
        Assert.Equal("checkbox", input.GetAttribute("type"));
    }

    [Fact]
    public void CheckboxItem_InputHasCorrectName()
    {
        // Arrange & Act
        var cut = Render<GovUkCheckboxes>(parameters => parameters
            .Add(p => p.Name, "nationality")
            .Add(p => p.Legend, "What is your nationality?")
            .AddChildContent<GovUkCheckboxItem>(p => p
                .Add(x => x.Value, "british")
                .Add(x => x.Label, "British")));

        // Assert
        var input = cut.Find(".govuk-checkboxes__input");
        Assert.Equal("nationality", input.GetAttribute("name"));
    }

    [Fact]
    public void CheckboxItem_WithHint_HasAriaDescribedBy()
    {
        // Arrange & Act
        var cut = Render<GovUkCheckboxes>(parameters => parameters
            .Add(p => p.Name, "nationality")
            .Add(p => p.Legend, "What is your nationality?")
            .AddChildContent<GovUkCheckboxItem>(p => p
                .Add(x => x.Value, "british")
                .Add(x => x.Label, "British")
                .Add(x => x.Hint, "Including English, Scottish, Welsh and Northern Irish")
                .Add(x => x.Id, "checkbox-british")));

        // Assert
        var input = cut.Find(".govuk-checkboxes__input");
        Assert.Equal("checkbox-british-hint", input.GetAttribute("aria-describedby"));
    }

    #endregion

    #region Styling Tests

    [Fact]
    public void Checkboxes_AppliesSmallVariant()
    {
        // Arrange & Act
        var cut = Render<GovUkCheckboxes>(parameters => parameters
            .Add(p => p.Name, "nationality")
            .Add(p => p.Legend, "What is your nationality?")
            .Add(p => p.IsSmall, true));

        // Assert
        var checkboxesDiv = cut.Find(".govuk-checkboxes");
        Assert.Contains("govuk-checkboxes--small", checkboxesDiv.ClassName);
    }

    [Fact]
    public void Checkboxes_AppliesCustomCssClass()
    {
        // Arrange & Act
        var cut = Render<GovUkCheckboxes>(parameters => parameters
            .Add(p => p.Name, "nationality")
            .Add(p => p.Legend, "What is your nationality?")
            .Add(p => p.CssClass, "custom-class"));

        // Assert
        var formGroup = cut.Find(".govuk-form-group");
        Assert.Contains("custom-class", formGroup.ClassName);
    }

    [Fact]
    public void Checkboxes_AppliesLegendCssClass()
    {
        // Arrange & Act
        var cut = Render<GovUkCheckboxes>(parameters => parameters
            .Add(p => p.Name, "nationality")
            .Add(p => p.Legend, "What is your nationality?")
            .Add(p => p.LegendCssClass, "govuk-fieldset__legend--l"));

        // Assert
        var legend = cut.Find("legend.govuk-fieldset__legend");
        Assert.Contains("govuk-fieldset__legend--l", legend.ClassName);
    }

    #endregion

    #region Checkbox Item Tests

    [Fact]
    public void CheckboxItem_RendersLabel()
    {
        // Arrange & Act
        var cut = Render<GovUkCheckboxes>(parameters => parameters
            .Add(p => p.Name, "nationality")
            .Add(p => p.Legend, "What is your nationality?")
            .AddChildContent<GovUkCheckboxItem>(p => p
                .Add(x => x.Value, "british")
                .Add(x => x.Label, "British")));

        // Assert
        var label = cut.Find(".govuk-checkboxes__label");
        Assert.Contains("British", label.TextContent);
    }

    [Fact]
    public void CheckboxItem_RendersHint()
    {
        // Arrange & Act
        var cut = Render<GovUkCheckboxes>(parameters => parameters
            .Add(p => p.Name, "nationality")
            .Add(p => p.Legend, "What is your nationality?")
            .AddChildContent<GovUkCheckboxItem>(p => p
                .Add(x => x.Value, "british")
                .Add(x => x.Label, "British")
                .Add(x => x.Hint, "Including English, Scottish, Welsh and Northern Irish")));

        // Assert
        var hint = cut.Find(".govuk-checkboxes__hint");
        Assert.Contains("Including English, Scottish, Welsh and Northern Irish", hint.TextContent);
    }

    [Fact]
    public void CheckboxItem_Disabled()
    {
        // Arrange & Act
        var cut = Render<GovUkCheckboxes>(parameters => parameters
            .Add(p => p.Name, "nationality")
            .Add(p => p.Legend, "What is your nationality?")
            .AddChildContent<GovUkCheckboxItem>(p => p
                .Add(x => x.Value, "british")
                .Add(x => x.Label, "British")
                .Add(x => x.Disabled, true)));

        // Assert
        var input = cut.Find(".govuk-checkboxes__input");
        Assert.True(input.HasAttribute("disabled"));
    }

    [Fact]
    public void CheckboxItem_ConditionalContent_Hidden_WhenNotChecked()
    {
        // Arrange & Act
        var cut = Render<GovUkCheckboxes>(parameters => parameters
            .Add(p => p.Name, "contact")
            .Add(p => p.Legend, "How would you like to be contacted?")
            .AddChildContent<GovUkCheckboxItem>(p => p
                .Add(x => x.Value, "email")
                .Add(x => x.Label, "Email")
                .Add(x => x.Id, "contact-email")
                .Add(x => x.HasConditional, true)
                .Add(x => x.ConditionalContent, (RenderFragment)(b => b.AddMarkupContent(0, "<p>Email content</p>")))));

        // Assert
        var conditional = cut.Find(".govuk-checkboxes__conditional");
        Assert.Contains("govuk-checkboxes__conditional--hidden", conditional.ClassName);
    }

    [Fact]
    public void CheckboxItem_ConditionalContent_HasAriaControls()
    {
        // Arrange & Act
        var cut = Render<GovUkCheckboxes>(parameters => parameters
            .Add(p => p.Name, "contact")
            .Add(p => p.Legend, "How would you like to be contacted?")
            .AddChildContent<GovUkCheckboxItem>(p => p
                .Add(x => x.Value, "email")
                .Add(x => x.Label, "Email")
                .Add(x => x.Id, "contact-email")
                .Add(x => x.HasConditional, true)
                .Add(x => x.ConditionalContent, (RenderFragment)(b => b.AddMarkupContent(0, "<p>Email content</p>")))));

        // Assert
        var input = cut.Find(".govuk-checkboxes__input");
        Assert.Equal("conditional-contact-email", input.GetAttribute("aria-controls"));
    }

    #endregion
}
