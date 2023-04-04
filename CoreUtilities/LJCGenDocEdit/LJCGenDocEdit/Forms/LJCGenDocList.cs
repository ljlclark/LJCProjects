// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// LJCGenDocEdit.cs
using System;
using System.Drawing;
using System.Windows.Forms;

namespace LJCGenDocEdit
{
  // The list form.
  // <include path='items/ListFormDAW/*' file='../../LJCDocLib/Common/List.xml'/>
  internal partial class LJCGenDocList : Form
  {
    #region Constructors

    // Initializes an object instance.
    internal LJCGenDocList()
    {
      Cursor = Cursors.WaitCursor;
      InitializeComponent();

      // Initialize property values.

      // Set default class data.
      BeginColor = Color.AliceBlue;
      EndColor = Color.LightSkyBlue;
      Cursor = Cursors.Default;
    }
    #endregion

    #region Form Event Handlers

    // Configures the form and loads the initial control data.
    private void LJCGenDocEdit_Load(object sender, EventArgs e)
    {
      InitializeControls();
      ClassGroupMenuExit.Click += MenuExit_Click;
      CenterToParent();
    }
    #endregion

    #region Action Event Handlers

    #region Tabs

    // Performs a Move of the selected Main Tab to the TileTabs control.
    private void MainTabsMove_Click(object sender, EventArgs e)
    {
      MainTabs.LJCMoveTabPageRight(TileTabs, TabsSplit);
    }

    // Performs a Move of the selected Tile Tab to the MainTabs control.
    private void TileTabsMove_Click(object sender, EventArgs e)
    {
      TileTabs.LJCMoveTabPageLeft(MainTabs, TabsSplit);
    }
    #endregion

    #region Assembly Group

    // Refreshes the list.
    private void AssemblyGroupMenuRefresh_Click(object sender, EventArgs e)
    {
      mAssemblyGroupGridCode.DoRefresh();
    }

    // Export a text file.
    private void AssemblyGroupMenuText_Click(object sender, EventArgs e)
    {
      string extension = "txt";
      string fileSpec = $@"ExportFiles\AssemblyGroup.{extension}";
      AssemblyGroupGrid.LJCExportData(fileSpec);
    }

    // Export a CSV file.
    private void AssemblyGroupMenuCSV_Click(object sender, EventArgs e)
    {
      string fileSpec = $@"ExportFiles\AssemblyGroup.csv";
      AssemblyGroupGrid.LJCExportData(fileSpec);
    }

    // Performs the Exit function.
    private void MenuExit_Click(object sender, EventArgs e)
    {
      SaveControlValues();
      Close();
    }
    #endregion

    #region Assembly Item

    // Refreshes the list.
    private void AssemblyMenuRefresh_Click(object sender, EventArgs e)
    {
      mAssemblyItemGridCode.DoRefresh();
    }

    // Export a text file.
    private void AssemblyMenuText_Click(object sender, EventArgs e)
    {
      string extension = "txt";
      string fileSpec = $@"ExportFiles\Assembly.{extension}";
      AssemblyItemGrid.LJCExportData(fileSpec);
    }

    // Export a CSV file.
    private void AssemblyMenuCSV_Click(object sender, EventArgs e)
    {
      string fileSpec = $@"ExportFiles\Assembly.csv";
      AssemblyItemGrid.LJCExportData(fileSpec);
    }
    #endregion

    #region Class Group

    // Refreshes the list.
    private void ClassGroupMenuRefresh_Click(object sender, EventArgs e)
    {
      mClassGroupGridCode.DoRefresh();
    }

    // Export a text file.
    private void ClassGroupMenuText_Click(object sender, EventArgs e)
    {
      string extension = "txt";
      string fileSpec = $@"ExportFiles\ClassGroup.{extension}";
      ClassGroupGrid.LJCExportData(fileSpec);
    }

    // Export a CSV file.
    private void ClassGroupMenuCSV_Click(object sender, EventArgs e)
    {
      string fileSpec = $@"ExportFiles\ClassGroup.csv";
      ClassGroupGrid.LJCExportData(fileSpec);
    }
    #endregion

    #region Class Item

    // Refreshes the list.
    private void ClassMenuRefresh_Click(object sender, EventArgs e)
    {
      mClassItemGridCode.DoRefresh();
    }

    // Export a text file.
    private void ClassMenuText_Click(object sender, EventArgs e)
    {
      string extension = "txt";
      string fileSpec = $@"ExportFiles\Class.{extension}";
      ClassItemGrid.LJCExportData(fileSpec);
    }

    // Export a CSV file.
    private void ClassMenuCSV_Click(object sender, EventArgs e)
    {
      string fileSpec = $@"ExportFiles\Class.csv";
      ClassItemGrid.LJCExportData(fileSpec);
    }
    #endregion
    #endregion

    #region Control Event Handlers

    #region Tabs

    // Handles the MouseDown event.
    private void MainTabs_MouseDown(object sender, MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Right)
      {
        MainTabs.LJCSetCurrentTabPage(e);
      }
    }

    // Handles the MouseDown event.
    private void TileTabs_MouseDown(object sender, MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Right)
      {
        TileTabs.LJCSetCurrentTabPage(e);
      }
    }
    #endregion

    #region Assembly Group

    // Handles the MouseDown event.
    private void AssemblyGrid_MouseDown(object sender, MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Right)
      {
        // LJCIsDifferentRow() Sets the LJCLastRowIndex for new row.
        AssemblyGroupGrid.Select();
        if (AssemblyGroupGrid.LJCIsDifferentRow(e))
        {
          // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
          AssemblyGroupGrid.LJCSetCurrentRow(e);
          TimedChange(Change.AssemblyGroup);
        }
      }
    }

    // Handles the SelectionChanged event.
    private void AssemblyGrid_SelectionChanged(object sender, EventArgs e)
    {
      if (AssemblyGroupGrid.LJCAllowSelectionChange)
      {
        TimedChange(Change.AssemblyGroup);
      }
      AssemblyGroupGrid.LJCAllowSelectionChange = true;
    }
    #endregion

    #region Assembly Item

    // Handles the MouseDown event.
    private void AssemblyItemGrid_MouseDown(object sender, MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Right)
      {
        // LJCIsDifferentRow() Sets the LJCLastRowIndex for new row.
        AssemblyItemGrid.Select();
        if (AssemblyItemGrid.LJCIsDifferentRow(e))
        {
          // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
          AssemblyItemGrid.LJCSetCurrentRow(e);
          TimedChange(Change.AssemblyItem);
        }
      }
    }

    // Handles the SelectionChanged event.
    private void AssemblyItemGrid_SelectionChanged(object sender, EventArgs e)
    {
      if (AssemblyItemGrid.LJCAllowSelectionChange)
      {
        TimedChange(Change.AssemblyItem);
      }
      AssemblyItemGrid.LJCAllowSelectionChange = true;
    }
    #endregion

    #region Assembly Combo

    // Handles the SelectionChanged event.
    private void AssemblyCombo_SelectedIndexChanged(object sender, EventArgs e)
    {
      TimedChange(Change.AssemblyCombo);
    }
    #endregion

    #region Class Group

    // Handles the MouseDown event.
    private void ClassGroupGrid_MouseDown(object sender, MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Right)
      {
        // LJCIsDifferentRow() Sets the LJCLastRowIndex for new row.
        ClassGroupGrid.Select();
        if (ClassGroupGrid.LJCIsDifferentRow(e))
        {
          // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
          ClassGroupGrid.LJCSetCurrentRow(e);
          TimedChange(Change.ClassGroup);
        }
      }
    }

    // Handles the SelectionChanged event.
    private void ClassGrid_SelectionChanged(object sender, EventArgs e)
    {
      if (ClassGroupGrid.LJCAllowSelectionChange)
      {
        TimedChange(Change.ClassGroup);
      }
      ClassGroupGrid.LJCAllowSelectionChange = true;
    }
    #endregion

    #region Class Item

    // Handles the MouseDown event.
    private void ClassItemGrid_MouseDown(object sender, MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Right)
      {
        // LJCIsDifferentRow() Sets the LJCLastRowIndex for new row.
        ClassItemGrid.Select();
        if (ClassItemGrid.LJCIsDifferentRow(e))
        {
          // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
          ClassItemGrid.LJCSetCurrentRow(e);
          TimedChange(Change.ClassItem);
        }
      }
    }

    // Handles the SelectionChanged event.
    private void ClassItemGrid_SelectionChanged(object sender, EventArgs e)
    {
      if (ClassItemGrid.LJCAllowSelectionChange)
      {
        TimedChange(Change.ClassItem);
      }
      ClassItemGrid.LJCAllowSelectionChange = true;
    }
    #endregion

    #endregion
  }
}
