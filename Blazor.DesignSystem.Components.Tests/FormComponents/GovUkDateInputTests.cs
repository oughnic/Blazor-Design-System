using Bunit;
using Microsoft.AspNetCore.Components;
using Blazor.DesignSystem.Components;
using Xunit;

namespace Blazor.DesignSystem.Components.Tests.FormComponents;

/// <summary>
/// Unit tests for GovUkDateInput component covering rendering, styling, accessibility, data binding, and error states.
/// </summary>
public class GovUkDateInputTests : BunitContext
{
    #region Basic Rendering Tests

    [Fact]
    public void DateInput_RendersWithFormGroupWrapper()
    {
        // Arrange & Act
        var cut = Render<GovUkDateInput>(parameters => parameters
            .Add(p => p.Legend, "Date of birth"));

        // Assert
        var formGroup = cut.Find(".govuk-form-group");
        Assert.NotNull(formGroup);
    }

    [Fact]
    public void DateInput_RendersFieldset()
    {
        // Arrange & Act
        var cut = Render<GovUkDateInput>(parameters => parameters
            .Add(p => p.Legend, "Date of birth"));

        // Assert
        var fieldset = cut.Find("fieldset.govuk-fieldset");
        Assert.NotNull(fieldset);
        Assert.Equal("group", fieldset.GetAttribute("role"));
    }

    [Fact]
    public void DateInput_RendersLegend()
    {
        // Arrange & Act
        var cut = Render<GovUkDateInput>(parameters => parameters
            .Add(p => p.Legend, "Date of birth"));

        // Assert
        var legend = cut.Find("legend.govuk-fieldset__legend");
        Assert.Contains("Date of birth", legend.TextContent);
    }

    [Fact]
    public void DateInput_RendersLegendAsHeading()
    {
        // Arrange & Act
        var cut = Render<GovUkDateInput>(parameters => parameters
            .Add(p => p.Legend, "Date of birth")
            .Add(p => p.IsPageHeading, true));

        // Assert
        var heading = cut.Find("legend h1.govuk-fieldset__heading");
        Assert.Equal("Date of birth", heading.TextContent);
    }

    [Fact]
    public void DateInput_RendersHint()
    {
        // Arrange & Act
        var cut = Render<GovUkDateInput>(parameters => parameters
            .Add(p => p.Legend, "Date of birth")
            .Add(p => p.Id, "dob")
            .Add(p => p.Hint, "For example, 31 3 1980"));

        // Assert
        var hint = cut.Find(".govuk-hint");
        Assert.Equal("For example, 31 3 1980", hint.TextContent);
        Assert.Equal("dob-hint", hint.Id);
    }

    [Fact]
    public void DateInput_RendersThreeInputs()
    {
        // Arrange & Act
        var cut = Render<GovUkDateInput>(parameters => parameters
            .Add(p => p.Legend, "Date of birth")
            .Add(p => p.Id, "dob"));

        // Assert
        var inputs = cut.FindAll(".govuk-date-input__input");
        Assert.Equal(3, inputs.Count);
        
        Assert.Equal("dob-day", inputs[0].Id);
        Assert.Equal("dob-month", inputs[1].Id);
        Assert.Equal("dob-year", inputs[2].Id);
    }

    [Fact]
    public void DateInput_RendersLabelsForInputs()
    {
        // Arrange & Act
        var cut = Render<GovUkDateInput>(parameters => parameters
            .Add(p => p.Legend, "Date of birth")
            .Add(p => p.Id, "dob"));

        // Assert
        var labels = cut.FindAll(".govuk-date-input__label");
        Assert.Equal(3, labels.Count);
        Assert.Equal("Day", labels[0].TextContent);
        Assert.Equal("Month", labels[1].TextContent);
        Assert.Equal("Year", labels[2].TextContent);
    }

    [Fact]
    public void DateInput_CustomLabels()
    {
        // Arrange & Act
        var cut = Render<GovUkDateInput>(parameters => parameters
            .Add(p => p.Legend, "Date of birth")
            .Add(p => p.DayLabel, "Diwrnod")
            .Add(p => p.MonthLabel, "Mis")
            .Add(p => p.YearLabel, "Blwyddyn"));

        // Assert
        var labels = cut.FindAll(".govuk-date-input__label");
        Assert.Equal("Diwrnod", labels[0].TextContent);
        Assert.Equal("Mis", labels[1].TextContent);
        Assert.Equal("Blwyddyn", labels[2].TextContent);
    }

    #endregion

    #region Data Binding Tests

    [Fact]
    public void DateInput_DisplaysValues()
    {
        // Arrange & Act
        var cut = Render<GovUkDateInput>(parameters => parameters
            .Add(p => p.Legend, "Date of birth")
            .Add(p => p.Id, "dob")
            .Add(p => p.Day, "31")
            .Add(p => p.Month, "3")
            .Add(p => p.Year, "1980"));

        // Assert
        var inputs = cut.FindAll(".govuk-date-input__input");
        Assert.Equal("31", inputs[0].GetAttribute("value"));
        Assert.Equal("3", inputs[1].GetAttribute("value"));
        Assert.Equal("1980", inputs[2].GetAttribute("value"));
    }

    [Fact]
    public void DateInput_InvokesDayChanged_OnInput()
    {
        // Arrange
        string? newDay = null;
        var cut = Render<GovUkDateInput>(parameters => parameters
            .Add(p => p.Legend, "Date of birth")
            .Add(p => p.Id, "dob")
            .Add(p => p.DayChanged, EventCallback.Factory.Create<string?>(this, v => newDay = v)));

        // Act
        cut.Find("#dob-day").Input("15");

        // Assert
        Assert.Equal("15", newDay);
    }

    [Fact]
    public void DateInput_InvokesMonthChanged_OnInput()
    {
        // Arrange
        string? newMonth = null;
        var cut = Render<GovUkDateInput>(parameters => parameters
            .Add(p => p.Legend, "Date of birth")
            .Add(p => p.Id, "dob")
            .Add(p => p.MonthChanged, EventCallback.Factory.Create<string?>(this, v => newMonth = v)));

        // Act
        cut.Find("#dob-month").Input("6");

        // Assert
        Assert.Equal("6", newMonth);
    }

    [Fact]
    public void DateInput_InvokesYearChanged_OnInput()
    {
        // Arrange
        string? newYear = null;
        var cut = Render<GovUkDateInput>(parameters => parameters
            .Add(p => p.Legend, "Date of birth")
            .Add(p => p.Id, "dob")
            .Add(p => p.YearChanged, EventCallback.Factory.Create<string?>(this, v => newYear = v)));

        // Act
        cut.Find("#dob-year").Input("2000");

        // Assert
        Assert.Equal("2000", newYear);
    }

    #endregion

    #region Error State Tests

    [Fact]
    public void DateInput_ErrorState_AppliesErrorClass()
    {
        // Arrange & Act
        var cut = Render<GovUkDateInput>(parameters => parameters
            .Add(p => p.Legend, "Date of birth")
            .Add(p => p.HasError, true)
            .Add(p => p.ErrorMessage, "Enter your date of birth"));

        // Assert
        var formGroup = cut.Find(".govuk-form-group");
        Assert.Contains("govuk-form-group--error", formGroup.ClassName);
    }

    [Fact]
    public void DateInput_ErrorState_RendersErrorMessage()
    {
        // Arrange & Act
        var cut = Render<GovUkDateInput>(parameters => parameters
            .Add(p => p.Legend, "Date of birth")
            .Add(p => p.Id, "dob")
            .Add(p => p.HasError, true)
            .Add(p => p.ErrorMessage, "Enter your date of birth"));

        // Assert
        var errorMessage = cut.Find(".govuk-error-message");
        Assert.Contains("Enter your date of birth", errorMessage.TextContent);
        Assert.Equal("dob-error", errorMessage.Id);
    }

    [Fact]
    public void DateInput_ErrorState_AppliesErrorToAllInputs()
    {
        // Arrange & Act
        var cut = Render<GovUkDateInput>(parameters => parameters
            .Add(p => p.Legend, "Date of birth")
            .Add(p => p.HasError, true)
            .Add(p => p.ErrorMessage, "Enter your date of birth"));

        // Assert - when no specific field error, all inputs should have error class
        var inputs = cut.FindAll(".govuk-date-input__input");
        Assert.All(inputs, input => Assert.Contains("govuk-input--error", input.ClassName));
    }

    [Fact]
    public void DateInput_FieldSpecificError_OnlyDayHasError()
    {
        // Arrange & Act
        var cut = Render<GovUkDateInput>(parameters => parameters
            .Add(p => p.Legend, "Date of birth")
            .Add(p => p.Id, "dob")
            .Add(p => p.HasError, true)
            .Add(p => p.ErrorMessage, "Day must be between 1 and 31")
            .Add(p => p.HasDayFieldError, true));

        // Assert
        var dayInput = cut.Find("#dob-day");
        var monthInput = cut.Find("#dob-month");
        var yearInput = cut.Find("#dob-year");
        
        Assert.Contains("govuk-input--error", dayInput.ClassName);
        Assert.DoesNotContain("govuk-input--error", monthInput.ClassName);
        Assert.DoesNotContain("govuk-input--error", yearInput.ClassName);
    }

    [Fact]
    public void DateInput_FieldSpecificError_OnlyMonthHasError()
    {
        // Arrange & Act
        var cut = Render<GovUkDateInput>(parameters => parameters
            .Add(p => p.Legend, "Date of birth")
            .Add(p => p.Id, "dob")
            .Add(p => p.HasError, true)
            .Add(p => p.ErrorMessage, "Month must be between 1 and 12")
            .Add(p => p.HasMonthFieldError, true));

        // Assert
        var dayInput = cut.Find("#dob-day");
        var monthInput = cut.Find("#dob-month");
        var yearInput = cut.Find("#dob-year");
        
        Assert.DoesNotContain("govuk-input--error", dayInput.ClassName);
        Assert.Contains("govuk-input--error", monthInput.ClassName);
        Assert.DoesNotContain("govuk-input--error", yearInput.ClassName);
    }

    [Fact]
    public void DateInput_FieldSpecificError_OnlyYearHasError()
    {
        // Arrange & Act
        var cut = Render<GovUkDateInput>(parameters => parameters
            .Add(p => p.Legend, "Date of birth")
            .Add(p => p.Id, "dob")
            .Add(p => p.HasError, true)
            .Add(p => p.ErrorMessage, "Enter a valid year")
            .Add(p => p.HasYearFieldError, true));

        // Assert
        var dayInput = cut.Find("#dob-day");
        var monthInput = cut.Find("#dob-month");
        var yearInput = cut.Find("#dob-year");
        
        Assert.DoesNotContain("govuk-input--error", dayInput.ClassName);
        Assert.DoesNotContain("govuk-input--error", monthInput.ClassName);
        Assert.Contains("govuk-input--error", yearInput.ClassName);
    }

    #endregion

    #region Accessibility Tests

    [Fact]
    public void DateInput_FieldsetHasRole()
    {
        // Arrange & Act
        var cut = Render<GovUkDateInput>(parameters => parameters
            .Add(p => p.Legend, "Date of birth"));

        // Assert
        var fieldset = cut.Find("fieldset.govuk-fieldset");
        Assert.Equal("group", fieldset.GetAttribute("role"));
    }

    [Fact]
    public void DateInput_HasAriaDescribedBy_WithHint()
    {
        // Arrange & Act
        var cut = Render<GovUkDateInput>(parameters => parameters
            .Add(p => p.Legend, "Date of birth")
            .Add(p => p.Id, "dob")
            .Add(p => p.Hint, "For example, 31 3 1980"));

        // Assert
        var fieldset = cut.Find("fieldset.govuk-fieldset");
        Assert.Equal("dob-hint", fieldset.GetAttribute("aria-describedby"));
    }

    [Fact]
    public void DateInput_HasAriaDescribedBy_WithError()
    {
        // Arrange & Act
        var cut = Render<GovUkDateInput>(parameters => parameters
            .Add(p => p.Legend, "Date of birth")
            .Add(p => p.Id, "dob")
            .Add(p => p.HasError, true)
            .Add(p => p.ErrorMessage, "Enter your date of birth"));

        // Assert
        var fieldset = cut.Find("fieldset.govuk-fieldset");
        Assert.Equal("dob-error", fieldset.GetAttribute("aria-describedby"));
    }

    [Fact]
    public void DateInput_InputsHaveNumericInputMode()
    {
        // Arrange & Act
        var cut = Render<GovUkDateInput>(parameters => parameters
            .Add(p => p.Legend, "Date of birth")
            .Add(p => p.Id, "dob"));

        // Assert
        var inputs = cut.FindAll(".govuk-date-input__input");
        Assert.All(inputs, input => Assert.Equal("numeric", input.GetAttribute("inputmode")));
    }

    [Fact]
    public void DateInput_InputsHaveNumericPattern()
    {
        // Arrange & Act
        var cut = Render<GovUkDateInput>(parameters => parameters
            .Add(p => p.Legend, "Date of birth")
            .Add(p => p.Id, "dob"));

        // Assert
        var inputs = cut.FindAll(".govuk-date-input__input");
        Assert.All(inputs, input => Assert.Equal("[0-9]*", input.GetAttribute("pattern")));
    }

    [Fact]
    public void DateInput_DayInputHasCorrectWidth()
    {
        // Arrange & Act
        var cut = Render<GovUkDateInput>(parameters => parameters
            .Add(p => p.Legend, "Date of birth")
            .Add(p => p.Id, "dob"));

        // Assert
        var dayInput = cut.Find("#dob-day");
        Assert.Contains("govuk-input--width-2", dayInput.ClassName);
    }

    [Fact]
    public void DateInput_MonthInputHasCorrectWidth()
    {
        // Arrange & Act
        var cut = Render<GovUkDateInput>(parameters => parameters
            .Add(p => p.Legend, "Date of birth")
            .Add(p => p.Id, "dob"));

        // Assert
        var monthInput = cut.Find("#dob-month");
        Assert.Contains("govuk-input--width-2", monthInput.ClassName);
    }

    [Fact]
    public void DateInput_YearInputHasCorrectWidth()
    {
        // Arrange & Act
        var cut = Render<GovUkDateInput>(parameters => parameters
            .Add(p => p.Legend, "Date of birth")
            .Add(p => p.Id, "dob"));

        // Assert
        var yearInput = cut.Find("#dob-year");
        Assert.Contains("govuk-input--width-4", yearInput.ClassName);
    }

    [Fact]
    public void DateInput_LabelsLinkedToInputs()
    {
        // Arrange & Act
        var cut = Render<GovUkDateInput>(parameters => parameters
            .Add(p => p.Legend, "Date of birth")
            .Add(p => p.Id, "dob"));

        // Assert
        var labels = cut.FindAll(".govuk-date-input__label");
        Assert.Equal("dob-day", labels[0].GetAttribute("for"));
        Assert.Equal("dob-month", labels[1].GetAttribute("for"));
        Assert.Equal("dob-year", labels[2].GetAttribute("for"));
    }

    #endregion

    #region Autocomplete Tests

    [Fact]
    public void DateInput_AppliesAutocomplete()
    {
        // Arrange & Act
        var cut = Render<GovUkDateInput>(parameters => parameters
            .Add(p => p.Legend, "Date of birth")
            .Add(p => p.Id, "dob")
            .Add(p => p.Autocomplete, "bday"));

        // Assert
        var inputs = cut.FindAll(".govuk-date-input__input");
        Assert.All(inputs, input => Assert.Equal("bday", input.GetAttribute("autocomplete")));
    }

    [Fact]
    public void DateInput_AppliesIndividualAutocomplete()
    {
        // Arrange & Act
        var cut = Render<GovUkDateInput>(parameters => parameters
            .Add(p => p.Legend, "Date of birth")
            .Add(p => p.Id, "dob")
            .Add(p => p.DayAutocomplete, "bday-day")
            .Add(p => p.MonthAutocomplete, "bday-month")
            .Add(p => p.YearAutocomplete, "bday-year"));

        // Assert
        var dayInput = cut.Find("#dob-day");
        var monthInput = cut.Find("#dob-month");
        var yearInput = cut.Find("#dob-year");
        
        Assert.Equal("bday-day", dayInput.GetAttribute("autocomplete"));
        Assert.Equal("bday-month", monthInput.GetAttribute("autocomplete"));
        Assert.Equal("bday-year", yearInput.GetAttribute("autocomplete"));
    }

    #endregion
}
