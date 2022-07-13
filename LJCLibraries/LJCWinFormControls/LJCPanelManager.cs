// Copyright (c) Lester J. Clark 2021,2022 - All Rights Reserved
// LJCPanelManager.cs
using System;
using System.Windows.Forms;

namespace LJCWinFormControls
{
  /// <summary>Provides funtionality to manage a collapsable panel splitter.</summary>
  public class LJCPanelManager
  {
    #region Constructors

    // Initializes an instance of the class.
    /// <include path='items/LJCPanelManagerC/*' file='Doc/LJCPanelManager.xml'/>
    public LJCPanelManager(SplitContainer split, LJCTabControl mainTabs, LJCTabControl tileTabs)
    {
      ParentSplit = split;
      MainLJCTabs = mainTabs;
      TileLJCTabs = tileTabs;
      ParentSplit.Panel2Collapsed = true;

      // Prevent drag from main control if only one tab page exists.
      MainLJCTabs.MouseDown += MainLJCTabs_MouseDown;

      // Collapse tile panel if no tab pages.
      MainLJCTabs.ControlAdded += LJCTabs_ControlAdded;
      TileLJCTabs.ControlAdded += LJCTabs_ControlAdded;

      // Add and remove temporary panel as required.
      MainLJCTabs.LJCPanelAdd += LJCTabs_LJCPanelAdd;
      MainLJCTabs.LJCPanelRemove += LJCTabs_LJCPanelRemove;
      TileLJCTabs.LJCPanelAdd += LJCTabs_LJCPanelAdd;
      TileLJCTabs.LJCPanelRemove += LJCTabs_LJCPanelRemove;
    }
    #endregion

    #region Control Event Handlers

    // The Main tabs MouseDown event handler.
    private void MainLJCTabs_MouseDown(object sender, MouseEventArgs e)
    {
      if (MainLJCTabs.TabCount > 1)
      {
        MainLJCTabs.LJCAllowDrag = true;
      }
      else
      {
        // Prevent drag from main control where only one tab page exists.
        MainLJCTabs.LJCAllowDrag = false;
      }
    }

    // The Control ControlAdded event handler.
    private void LJCTabs_ControlAdded(object sender, ControlEventArgs e)
    {
      TabPage tabPage;

      // Collapse unused panel.
      if (TileLJCTabs.TabCount == 0)
      {
        ParentSplit.Panel2Collapsed = true;
      }
      else
      {
        if (sender is LJCTabControl tabControl
          && tabControl.Name == TileLJCTabs.Name)
        {
          tabPage = TileLJCTabs.TabPages[0];
          if (tabPage.Name != "")
          {
            tabPage = MainLJCTabs.TabPages[0];
            int startDistance = tabPage.Width - (tabPage.Width / 5);
            if (ParentSplit.SplitterDistance > startDistance)
            {
              ParentSplit.SplitterDistance = tabPage.Width - (tabPage.Width / 2);
            }
          }
        }
      }
    }

    // The tab controls LJCPanelAdd event handler.
    private void LJCTabs_LJCPanelAdd(object sender, EventArgs e)
    {
      // Add temporary target panel.
      if (ParentSplit.Panel2Collapsed)
      {
        TileLJCTabs.TabPages.Add("");
        ParentSplit.Panel2Collapsed = false;
      }
    }

    // The tab controls LJCPanelRemove event handler.
    private void LJCTabs_LJCPanelRemove(object sender, EventArgs e)
    {
      bool collapse = false;

      // Collapse the empty panel.
      if (ParentSplit.Panel2Collapsed == false)
      {
        if (TileLJCTabs.TabCount == 0)
        {
          collapse = true;
        }
        else
        {
          if (TileLJCTabs.TabCount == 1
          && TileLJCTabs.TabPages[0].Name == "")
          {
            // Remove the temporary target panel and collapse the panel.
            collapse = true;
            TileLJCTabs.TabPages.Remove(TileLJCTabs.TabPages[0]);
          }
        }
        if (collapse)
        {
          ParentSplit.Panel2Collapsed = true;
        }
      }
    }
    #endregion

    #region Properties

    /// <summary>The parent splitter.</summary>
    public SplitContainer ParentSplit { get; private set; }

    /// <summary>The Main tabs.</summary>
    public LJCTabControl MainLJCTabs { get; private set; }

    /// <summary>The right tile tabs.</summary>
    public LJCTabControl TileLJCTabs { get; private set; }
    #endregion
  }
}
