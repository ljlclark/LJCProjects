// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// TabsCode.cs
using LJCControls5;
using LJCGenTextXAL5;
using LJCNetCommon5;
using static LJCGenTextEdit5.EditList;

namespace LJCGenTextEdit5
{
  // Contains the SectionGrid methods.
  internal class TabsCode
  {
    #region Parent Object Properties

    // Gets or sets the Parent List reference.
    private EditList EditList { get; set; }

    private SplitContainer MainSplit { get; set; }

    // Gets or sets the Tab reference.
    private LJCTabControl MainTabs { get; set; }

    private LJCRtControl OutputText { get; set; }

    // Gets or sets the Section Grid reference.
    private LJCDataGrid SectionGrid { get; set; }

    private LJCRtControl TemplateText { get; set; }

    private LJCTabControl TileTabs { get; set; }
    #endregion

    #region Constructors

    // Initializes an object instance.
    internal TabsCode(EditList parentList)
    {
      // Initialize property values.
      EditList = parentList;
      EditList.Cursor = Cursors.WaitCursor;

      MainTabs = EditList.MainTabs;
      MainSplit = EditList.MainSplit;
      OutputText = EditList.OutputRichText;
      SectionGrid = EditList.SectionGrid;
      TemplateText = EditList.TemplateRichText;
      TileTabs = EditList.TileTabs;

      MenuEventHandlers();
      ControlEventHandlers();
      EditList.Cursor = Cursors.Default;
    }

    // Creates the menu event handlers.
    private void MenuEventHandlers()
    {
      EditList.MainTabsMove.Click += MainTabsMove_Click;
      EditList.TileTabsMove.Click += TileTabsMove_Click;
    }

    // Creates the control event handlers.
    private void ControlEventHandlers()
    {
      MainTabs.MouseDown += MainTabs_MouseDown;
      TileTabs.MouseDown += TileTabs_MouseDown;
    }
    #endregion

    #region Action Event Handlers

    // Performs a Move of the selected Main Tab to the TileTabs control.
    private void MainTabsMove_Click(object? sender, EventArgs e)
    {
      if (MainTabs.TabPages.Count > 1)
      {
        MainSplit.Panel2Collapsed = false;
        if (MainTabs.SelectedTab != null)
        {
          MainTabs.SelectedTab.Parent = TileTabs;
        }
      }
    }

    // Performs a Move of the selected Tile Tab to the MainTabs control.
    private void TileTabsMove_Click(object? sender, EventArgs e)
    {
      if (TileTabs.SelectedTab != null)
      {
        TileTabs.SelectedTab.Parent = MainTabs;
        if (0 == TileTabs.TabPages.Count)
        {
          MainSplit.Panel2Collapsed = true;
        }
      }
    }
    #endregion

    #region Control Event Handlers

    // Handles the MouseDown event.
    private void MainTabs_MouseDown(object? sender, MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Right)
      {
        MainTabs.LJCSetCurrentTabPage(e);
      }
      var tabPage = MainTabs.LJCGetTabPage(e);
      if (tabPage != null)
      {
        SetFocusTab(tabPage);
      }
    }

    // Handles the MouseDown event.
    private void TileTabs_MouseDown(object? sender, MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Right)
      {
        TileTabs.LJCSetCurrentTabPage(e);
      }
      var tabPage = TileTabs.LJCGetTabPage(e);
      if (tabPage != null)
      {
        SetFocusTab(tabPage);
      }
    }

    // Sets the tab initial focus control.
    private void SetFocusTab(TabPage tabPage)
    {
      switch (tabPage.Name)
      {
        case "TemplateTab":
          TemplateText.Select();
          break;

        case "DataTab":
          SectionGrid.Select();
          break;

        case "OutputTab":
          OutputText.Select();
          break;
      }
    }
    #endregion
  }
}
