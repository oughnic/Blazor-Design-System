using Bunit;
using Microsoft.AspNetCore.Components;
using Blazor.DesignSystem.Components;
using Xunit;

namespace Blazor.DesignSystem.Components.Tests.InteractiveComponents;

/// <summary>
/// Unit tests for GovUkTabs, GovUkTab, and GovUkTabPanel components covering rendering, 
/// interactions, accessibility, and state management.
/// </summary>
public class GovUkTabsTests : BunitContext
{
    #region Basic Rendering Tests

    [Fact]
    public void Tabs_RendersWithCorrectClass()
    {
        // Arrange & Act
        var cut = Render<GovUkTabs>(parameters => parameters
            .Add(p => p.TabList, (RenderFragment)(b => b.AddMarkupContent(0, "")))
            .Add(p => p.TabPanels, (RenderFragment)(b => b.AddMarkupContent(0, ""))));

        // Assert
        var tabs = cut.Find(".govuk-tabs");
        Assert.NotNull(tabs);
    }

    [Fact]
    public void Tabs_HasDataModuleAttribute()
    {
        // Arrange & Act
        var cut = Render<GovUkTabs>(parameters => parameters
            .Add(p => p.TabList, (RenderFragment)(b => b.AddMarkupContent(0, "")))
            .Add(p => p.TabPanels, (RenderFragment)(b => b.AddMarkupContent(0, ""))));

        // Assert
        var tabs = cut.Find(".govuk-tabs");
        Assert.Equal("govuk-tabs", tabs.GetAttribute("data-module"));
    }

    [Fact]
    public void Tabs_RendersTitle()
    {
        // Arrange & Act
        var cut = Render<GovUkTabs>(parameters => parameters
            .Add(p => p.Title, "Contents")
            .Add(p => p.TabList, (RenderFragment)(b => b.AddMarkupContent(0, "")))
            .Add(p => p.TabPanels, (RenderFragment)(b => b.AddMarkupContent(0, ""))));

        // Assert
        var title = cut.Find(".govuk-tabs__title");
        Assert.Equal("Contents", title.TextContent);
    }

    [Fact]
    public void Tabs_RendersTabList()
    {
        // Arrange & Act
        var cut = Render<GovUkTabs>(parameters => parameters
            .Add(p => p.TabList, (RenderFragment)(b => 
            {
                b.OpenComponent<GovUkTab>(0);
                b.AddAttribute(1, "Id", "tab-1");
                b.AddAttribute(2, "Label", "Tab 1");
                b.CloseComponent();
            }))
            .Add(p => p.TabPanels, (RenderFragment)(b => b.AddMarkupContent(0, ""))));

        // Assert
        var tabList = cut.Find(".govuk-tabs__list");
        Assert.NotNull(tabList);
        Assert.Equal("tablist", tabList.GetAttribute("role"));
    }

    [Fact]
    public void Tabs_AppliesCustomCssClass()
    {
        // Arrange & Act
        var cut = Render<GovUkTabs>(parameters => parameters
            .Add(p => p.CssClass, "custom-class")
            .Add(p => p.TabList, (RenderFragment)(b => b.AddMarkupContent(0, "")))
            .Add(p => p.TabPanels, (RenderFragment)(b => b.AddMarkupContent(0, ""))));

        // Assert
        var tabs = cut.Find(".govuk-tabs");
        Assert.Contains("custom-class", tabs.ClassName);
    }

    #endregion

    #region Tab Item Tests

    [Fact]
    public void Tab_RendersLabel()
    {
        // Arrange & Act
        var cut = Render<GovUkTabs>(parameters => parameters
            .Add(p => p.TabList, (RenderFragment)(b => 
            {
                b.OpenComponent<GovUkTab>(0);
                b.AddAttribute(1, "Id", "past-day");
                b.AddAttribute(2, "Label", "Past day");
                b.CloseComponent();
            }))
            .Add(p => p.TabPanels, (RenderFragment)(b => b.AddMarkupContent(0, ""))));

        // Assert
        var tab = cut.Find(".govuk-tabs__tab");
        Assert.Equal("Past day", tab.TextContent);
    }

    [Fact]
    public void Tab_HasCorrectRoleAttribute()
    {
        // Arrange & Act
        var cut = Render<GovUkTabs>(parameters => parameters
            .Add(p => p.TabList, (RenderFragment)(b => 
            {
                b.OpenComponent<GovUkTab>(0);
                b.AddAttribute(1, "Id", "tab-1");
                b.AddAttribute(2, "Label", "Tab 1");
                b.CloseComponent();
            }))
            .Add(p => p.TabPanels, (RenderFragment)(b => b.AddMarkupContent(0, ""))));

        // Assert
        var tab = cut.Find(".govuk-tabs__tab");
        Assert.Equal("tab", tab.GetAttribute("role"));
    }

    [Fact]
    public void Tab_HasAriaControls()
    {
        // Arrange & Act
        var cut = Render<GovUkTabs>(parameters => parameters
            .Add(p => p.TabList, (RenderFragment)(b => 
            {
                b.OpenComponent<GovUkTab>(0);
                b.AddAttribute(1, "Id", "tab-1");
                b.AddAttribute(2, "Label", "Tab 1");
                b.CloseComponent();
            }))
            .Add(p => p.TabPanels, (RenderFragment)(b => b.AddMarkupContent(0, ""))));

        // Assert
        var tab = cut.Find(".govuk-tabs__tab");
        Assert.Equal("tab-1", tab.GetAttribute("aria-controls"));
    }

    [Fact]
    public void Tab_FirstTabIsSelectedByDefault()
    {
        // Arrange & Act
        var cut = Render<GovUkTabs>(parameters => parameters
            .Add(p => p.TabList, (RenderFragment)(b => 
            {
                b.OpenComponent<GovUkTab>(0);
                b.AddAttribute(1, "Id", "tab-1");
                b.AddAttribute(2, "Label", "Tab 1");
                b.CloseComponent();
                b.OpenComponent<GovUkTab>(3);
                b.AddAttribute(4, "Id", "tab-2");
                b.AddAttribute(5, "Label", "Tab 2");
                b.CloseComponent();
            }))
            .Add(p => p.TabPanels, (RenderFragment)(b => b.AddMarkupContent(0, ""))));

        // Assert
        var tabs = cut.FindAll(".govuk-tabs__tab");
        Assert.Equal("true", tabs[0].GetAttribute("aria-selected"));
        Assert.Equal("false", tabs[1].GetAttribute("aria-selected"));
    }

    [Fact]
    public void Tab_SelectedTabHasCorrectClass()
    {
        // Arrange & Act
        var cut = Render<GovUkTabs>(parameters => parameters
            .Add(p => p.TabList, (RenderFragment)(b => 
            {
                b.OpenComponent<GovUkTab>(0);
                b.AddAttribute(1, "Id", "tab-1");
                b.AddAttribute(2, "Label", "Tab 1");
                b.CloseComponent();
            }))
            .Add(p => p.TabPanels, (RenderFragment)(b => b.AddMarkupContent(0, ""))));

        // Assert
        var tabItem = cut.Find(".govuk-tabs__list-item");
        Assert.Contains("govuk-tabs__list-item--selected", tabItem.ClassName);
    }

    [Fact]
    public void Tab_HasPresentationRole()
    {
        // Arrange & Act
        var cut = Render<GovUkTabs>(parameters => parameters
            .Add(p => p.TabList, (RenderFragment)(b => 
            {
                b.OpenComponent<GovUkTab>(0);
                b.AddAttribute(1, "Id", "tab-1");
                b.AddAttribute(2, "Label", "Tab 1");
                b.CloseComponent();
            }))
            .Add(p => p.TabPanels, (RenderFragment)(b => b.AddMarkupContent(0, ""))));

        // Assert
        var tabItem = cut.Find(".govuk-tabs__list-item");
        Assert.Equal("presentation", tabItem.GetAttribute("role"));
    }

    #endregion

    #region Tab Panel Tests

    [Fact]
    public void TabPanel_RendersContent()
    {
        // Arrange & Act
        var cut = Render<GovUkTabs>(parameters => parameters
            .Add(p => p.TabList, (RenderFragment)(b => 
            {
                b.OpenComponent<GovUkTab>(0);
                b.AddAttribute(1, "Id", "panel-1");
                b.AddAttribute(2, "Label", "Tab 1");
                b.CloseComponent();
            }))
            .Add(p => p.TabPanels, (RenderFragment)(b => 
            {
                b.OpenComponent<GovUkTabPanel>(0);
                b.AddAttribute(1, "Id", "panel-1");
                b.AddAttribute(2, "ChildContent", (RenderFragment)(c => c.AddMarkupContent(0, "<p>Panel content</p>")));
                b.CloseComponent();
            })));

        // Assert
        var panel = cut.Find(".govuk-tabs__panel");
        Assert.Contains("Panel content", panel.TextContent);
    }

    [Fact]
    public void TabPanel_HasCorrectRoleAttribute()
    {
        // Arrange & Act
        var cut = Render<GovUkTabs>(parameters => parameters
            .Add(p => p.TabList, (RenderFragment)(b => 
            {
                b.OpenComponent<GovUkTab>(0);
                b.AddAttribute(1, "Id", "panel-1");
                b.AddAttribute(2, "Label", "Tab 1");
                b.CloseComponent();
            }))
            .Add(p => p.TabPanels, (RenderFragment)(b => 
            {
                b.OpenComponent<GovUkTabPanel>(0);
                b.AddAttribute(1, "Id", "panel-1");
                b.AddAttribute(2, "ChildContent", (RenderFragment)(c => c.AddMarkupContent(0, "Content")));
                b.CloseComponent();
            })));

        // Assert
        var panel = cut.Find(".govuk-tabs__panel");
        Assert.Equal("tabpanel", panel.GetAttribute("role"));
    }

    [Fact]
    public void TabPanel_HasAriaLabelledBy()
    {
        // Arrange & Act
        var cut = Render<GovUkTabs>(parameters => parameters
            .Add(p => p.TabList, (RenderFragment)(b => 
            {
                b.OpenComponent<GovUkTab>(0);
                b.AddAttribute(1, "Id", "panel-1");
                b.AddAttribute(2, "Label", "Tab 1");
                b.CloseComponent();
            }))
            .Add(p => p.TabPanels, (RenderFragment)(b => 
            {
                b.OpenComponent<GovUkTabPanel>(0);
                b.AddAttribute(1, "Id", "panel-1");
                b.AddAttribute(2, "ChildContent", (RenderFragment)(c => c.AddMarkupContent(0, "Content")));
                b.CloseComponent();
            })));

        // Assert
        var panel = cut.Find(".govuk-tabs__panel");
        Assert.Equal("tab_panel-1", panel.GetAttribute("aria-labelledby"));
    }

    [Fact]
    public void TabPanel_ActivePanelIsVisible()
    {
        // Arrange & Act
        var cut = Render<GovUkTabs>(parameters => parameters
            .Add(p => p.TabList, (RenderFragment)(b => 
            {
                b.OpenComponent<GovUkTab>(0);
                b.AddAttribute(1, "Id", "panel-1");
                b.AddAttribute(2, "Label", "Tab 1");
                b.CloseComponent();
            }))
            .Add(p => p.TabPanels, (RenderFragment)(b => 
            {
                b.OpenComponent<GovUkTabPanel>(0);
                b.AddAttribute(1, "Id", "panel-1");
                b.AddAttribute(2, "ChildContent", (RenderFragment)(c => c.AddMarkupContent(0, "Content")));
                b.CloseComponent();
            })));

        // Assert
        var panel = cut.Find(".govuk-tabs__panel");
        Assert.DoesNotContain("govuk-tabs__panel--hidden", panel.ClassName);
    }

    [Fact]
    public void TabPanel_InactivePanelIsHidden()
    {
        // Arrange & Act
        var cut = Render<GovUkTabs>(parameters => parameters
            .Add(p => p.TabList, (RenderFragment)(b => 
            {
                b.OpenComponent<GovUkTab>(0);
                b.AddAttribute(1, "Id", "panel-1");
                b.AddAttribute(2, "Label", "Tab 1");
                b.CloseComponent();
                b.OpenComponent<GovUkTab>(3);
                b.AddAttribute(4, "Id", "panel-2");
                b.AddAttribute(5, "Label", "Tab 2");
                b.CloseComponent();
            }))
            .Add(p => p.TabPanels, (RenderFragment)(b => 
            {
                b.OpenComponent<GovUkTabPanel>(0);
                b.AddAttribute(1, "Id", "panel-1");
                b.AddAttribute(2, "ChildContent", (RenderFragment)(c => c.AddMarkupContent(0, "Content 1")));
                b.CloseComponent();
                b.OpenComponent<GovUkTabPanel>(3);
                b.AddAttribute(4, "Id", "panel-2");
                b.AddAttribute(5, "ChildContent", (RenderFragment)(c => c.AddMarkupContent(0, "Content 2")));
                b.CloseComponent();
            })));

        // Assert
        var panels = cut.FindAll(".govuk-tabs__panel");
        Assert.DoesNotContain("govuk-tabs__panel--hidden", panels[0].ClassName);
        Assert.Contains("govuk-tabs__panel--hidden", panels[1].ClassName);
    }

    #endregion

    #region Interaction Tests

    [Fact]
    public void Tabs_ClickingTabSwitchesPanels()
    {
        // Arrange
        var cut = Render<GovUkTabs>(parameters => parameters
            .Add(p => p.TabList, (RenderFragment)(b => 
            {
                b.OpenComponent<GovUkTab>(0);
                b.AddAttribute(1, "Id", "panel-1");
                b.AddAttribute(2, "Label", "Tab 1");
                b.CloseComponent();
                b.OpenComponent<GovUkTab>(3);
                b.AddAttribute(4, "Id", "panel-2");
                b.AddAttribute(5, "Label", "Tab 2");
                b.CloseComponent();
            }))
            .Add(p => p.TabPanels, (RenderFragment)(b => 
            {
                b.OpenComponent<GovUkTabPanel>(0);
                b.AddAttribute(1, "Id", "panel-1");
                b.AddAttribute(2, "ChildContent", (RenderFragment)(c => c.AddMarkupContent(0, "Content 1")));
                b.CloseComponent();
                b.OpenComponent<GovUkTabPanel>(3);
                b.AddAttribute(4, "Id", "panel-2");
                b.AddAttribute(5, "ChildContent", (RenderFragment)(c => c.AddMarkupContent(0, "Content 2")));
                b.CloseComponent();
            })));

        // Act
        var tabs = cut.FindAll(".govuk-tabs__tab");
        tabs[1].Click();

        // Assert
        var panels = cut.FindAll(".govuk-tabs__panel");
        Assert.Contains("govuk-tabs__panel--hidden", panels[0].ClassName);
        Assert.DoesNotContain("govuk-tabs__panel--hidden", panels[1].ClassName);
    }

    [Fact]
    public void Tabs_ClickingTabUpdatesAriaSelected()
    {
        // Arrange
        var cut = Render<GovUkTabs>(parameters => parameters
            .Add(p => p.TabList, (RenderFragment)(b => 
            {
                b.OpenComponent<GovUkTab>(0);
                b.AddAttribute(1, "Id", "panel-1");
                b.AddAttribute(2, "Label", "Tab 1");
                b.CloseComponent();
                b.OpenComponent<GovUkTab>(3);
                b.AddAttribute(4, "Id", "panel-2");
                b.AddAttribute(5, "Label", "Tab 2");
                b.CloseComponent();
            }))
            .Add(p => p.TabPanels, (RenderFragment)(b => b.AddMarkupContent(0, ""))));

        // Act
        var tabsBefore = cut.FindAll(".govuk-tabs__tab");
        tabsBefore[1].Click();
        
        // Re-query after click to get updated elements
        var tabsAfter = cut.FindAll(".govuk-tabs__tab");

        // Assert
        Assert.Equal("false", tabsAfter[0].GetAttribute("aria-selected"));
        Assert.Equal("true", tabsAfter[1].GetAttribute("aria-selected"));
    }

    #endregion
}
