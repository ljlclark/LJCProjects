// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ListHelper.cs
using System.Drawing;
using System.Windows.Forms;

namespace LJCWinFormControls
{
  /// <summary>
  /// Provides methods for setting a complex list control
  /// when AutoScaleMode.Font is used.
  /// </summary>
  public class ListHelper
  {
    // Sets size and position for child SplitterContainer in a SplitterPanel.
    /// <include file='Doc/ListHelper.xml'
    ///  path='items/SetPanelSplitControl/*'/>
    public static void SetPanelSplitControl(SplitterPanel panel
      , SplitContainer childSplit, int adjust = 0, int splitLeft = 0
      , int splitTop = 0)
    {
      childSplit.Location = new Point(splitLeft, splitTop);
      childSplit.Width = panel.Width - childSplit.Left + adjust;
      childSplit.Height = panel.Height - childSplit.Top + adjust;
    }

    // Sets size and position for child SplitterContainer in a TabPage control.
    /// <include file='Doc/ListHelper.xml'
    ///  path='items/SetPageSplitControl/*'/>
    public static void SetPageSplitControl(TabPage page
      , SplitContainer childSplit, int adjust = 0, int splitLeft = 0
      , int splitTop = 0)
    {
      childSplit.Location = new Point(splitLeft, splitTop);
      childSplit.Width = page.Width - childSplit.Left + adjust;
      childSplit.Height = page.Height - childSplit.Top + adjust;
    }

    // Sets the size for a TabControl within a SplitterPanel.
    /// <include file='Doc/ListHelper.xml'
    ///  path='items/SetPanelTabControl/*'/>
    public static void SetPanelTabControl(SplitterPanel panel
      , TabControl tabControl, int adjust = 0, int tabLeft = 0
      , int tabTop = 0)
    {
      tabControl.Location = new Point(tabLeft, tabTop);
      tabControl.Width = panel.Width - tabLeft + adjust;
      tabControl.Height = panel.Height - tabTop + adjust;
    }

    // Sets size and position for standard list controls in a SplitterPanel.
    /// <include file='Doc/ListHelper.xml'
    ///  path='items/SetPanelControls1/*'/>
    public static void SetPanelControls(SplitterPanel panel
      , LJCHeaderBox headerBox, Panel toolPanel, LJCDataGrid dataGrid
      , int adjustAll = 0, int gridLeft = 0, int gridTop = 0)
    {
      PanelControlsAdjust adjust = new PanelControlsAdjust(adjustAll
        , adjustAll, adjustAll, adjustAll, gridLeft, gridTop);
      SetPanelControls(panel, headerBox, toolPanel, dataGrid, adjust);
    }

    // Sets size and position for standard list controls in a SplitterPanel.
    /// <include file='Doc/ListHelper.xml'
    ///  path='items/SetPanelControls2/*'/>
    public static void SetPanelControls(SplitterPanel panel
      , LJCHeaderBox headerBox, Panel toolPanel, LJCDataGrid dataGrid
      , PanelControlsAdjust adjust)
    {
      if (headerBox != null)
      {
        headerBox.Width = panel.Width - headerBox.Left + adjust.HeaderWidthAdjust;
        adjust.GridTop += headerBox.Height - 1;
      }
      if (toolPanel != null)
      {
        toolPanel.Width = panel.Width - toolPanel.Left + adjust.PanelWidthAdjust;
        adjust.GridTop += toolPanel.Height;
      }
      dataGrid.Location = new Point(adjust.GridLeft, adjust.GridTop);
      dataGrid.Width = panel.Width - adjust.GridLeft + adjust.GridWidthAdjust;
      dataGrid.Height = panel.Height - adjust.GridTop + adjust.GridHeightAdjust;
    }

    // Sets size and position for standard list controls in a TabPage control.
    /// <include file='Doc/ListHelper.xml'
    ///  path='items/SetPageControls1/*'/>
    public static void SetPageControls(TabPage page, LJCHeaderBox headerBox
      , Panel toolPanel, LJCDataGrid dataGrid, int adjustAll = 0, int gridLeft = 0
      , int gridTop = 0)
    {
      PanelControlsAdjust adjust = new PanelControlsAdjust(adjustAll
        , adjustAll, adjustAll, adjustAll, gridLeft, gridTop);
      SetPageControls(page, headerBox, toolPanel, dataGrid, adjust);
    }

    // Sets size and position for standard list controls in a TabPage control.
    /// <include file='Doc/ListHelper.xml'
    ///  path='items/SetPageControls2/*'/>
    public static void SetPageControls(TabPage page, LJCHeaderBox headerBox
      , Panel toolPanel, LJCDataGrid dataGrid, PanelControlsAdjust adjust)
    {
      if (headerBox != null)
      {
        headerBox.Width = page.Width - headerBox.Left + adjust.HeaderWidthAdjust;
        adjust.GridTop += headerBox.Height - 1;
      }
      if (toolPanel != null)
      {
        toolPanel.Width = page.Width - toolPanel.Left + adjust.PanelWidthAdjust;
        adjust.GridTop += toolPanel.Height;
      }
      dataGrid.Location = new Point(adjust.GridLeft, adjust.GridTop);
      dataGrid.Width = page.Width - adjust.GridLeft + adjust.GridWidthAdjust;
      dataGrid.Height = page.Height - adjust.GridTop + adjust.GridHeightAdjust;
    }
  }
}
