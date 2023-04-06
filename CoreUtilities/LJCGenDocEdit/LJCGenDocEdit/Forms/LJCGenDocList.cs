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
      ClassGroupExit.Click += MenuExit_Click;
      MethodGroupExit.Click += MenuExit_Click;
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
    private void AssemblyGroupRefresh_Click(object sender, EventArgs e)
    {
      mAssemblyGroupGridCode.DoRefresh();
    }

    // Export a text file.
    private void AssemblyGroupText_Click(object sender, EventArgs e)
    {
      string extension = "txt";
      string fileSpec = $@"ExportFiles\AssemblyGroup.{extension}";
      AssemblyGroupGrid.LJCExportData(fileSpec);
    }

    // Export a CSV file.
    private void AssemblyGroupCSV_Click(object sender, EventArgs e)
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
    private void AssemblyRefresh_Click(object sender, EventArgs e)
    {
      mAssemblyItemGridCode.DoRefresh();
    }

    // Export a text file.
    private void AssemblyText_Click(object sender, EventArgs e)
    {
      string extension = "txt";
      string fileSpec = $@"ExportFiles\Assembly.{extension}";
      AssemblyItemGrid.LJCExportData(fileSpec);
    }

    // Export a CSV file.
    private void AssemblyCSV_Click(object sender, EventArgs e)
    {
      string fileSpec = $@"ExportFiles\Assembly.csv";
      AssemblyItemGrid.LJCExportData(fileSpec);
    }
    #endregion

    #region Class Group

    // Refreshes the list.
    private void ClassGroupRefresh_Click(object sender, EventArgs e)
    {
      mClassGroupGridCode.DoRefresh();
    }

    // Export a text file.
    private void ClassGroupText_Click(object sender, EventArgs e)
    {
      string extension = "txt";
      string fileSpec = $@"ExportFiles\ClassGroup.{extension}";
      ClassGroupGrid.LJCExportData(fileSpec);
    }

    // Export a CSV file.
    private void ClassGroupCSV_Click(object sender, EventArgs e)
    {
      string fileSpec = $@"ExportFiles\ClassGroup.csv";
      ClassGroupGrid.LJCExportData(fileSpec);
    }
    #endregion

    #region Class Item

    // Refreshes the list.
    private void ClassRefresh_Click(object sender, EventArgs e)
    {
      mClassItemGridCode.DoRefresh();
    }

    // Export a text file.
    private void ClassText_Click(object sender, EventArgs e)
    {
      string extension = "txt";
      string fileSpec = $@"ExportFiles\Class.{extension}";
      ClassItemGrid.LJCExportData(fileSpec);
    }

    // Export a CSV file.
    private void ClassCSV_Click(object sender, EventArgs e)
    {
      string fileSpec = $@"ExportFiles\Class.csv";
      ClassItemGrid.LJCExportData(fileSpec);
    }
    #endregion

    #region Method Group

    // Refreshes the list.
    private void MethodGroupRefresh_Click(object sender, EventArgs e)
    {
      mMethodGroupGridCode.DoRefresh();
    }

    // Export a text file.
    private void MethodGroupText_Click(object sender, EventArgs e)
    {
      string extension = "txt";
      string fileSpec = $@"ExportFiles\MethodGroup.{extension}";
      MethodGroupGrid.LJCExportData(fileSpec);
    }

    // Export a CSV file.
    private void MethodGroupCSV_Click(object sender, EventArgs e)
    {
      string fileSpec = $@"ExportFiles\MethodGroup.csv";
      MethodGroupGrid.LJCExportData(fileSpec);
    }
    #endregion

    #region Method Item

    // Refreshes the list.
    private void MethodItemRefresh_Click(object sender, EventArgs e)
    {
      mMethodItemGridCode.DoRefresh();
    }

    // Export a text file.
    private void MethodItemText_Click(object sender, EventArgs e)
    {
      string extension = "txt";
      string fileSpec = $@"ExportFiles\Method.{extension}";
      MethodItemGrid.LJCExportData(fileSpec);
    }

    // Export a CSV file.
    private void MethodItemCSV_Click(object sender, EventArgs e)
    {
      string fileSpec = $@"ExportFiles\Method.csv";
      MethodItemGrid.LJCExportData(fileSpec);
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

    #region Class Combo

    // Handles the SelectionChanged event.
    private void ClassCombo_SelectedIndexChanged(object sender, EventArgs e)
    {
      TimedChange(Change.ClassCombo);
    }
    #endregion

    #region Method Group

    // Handles the MouseDown event.
    private void MethodGroupGrid_MouseDown(object sender, MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Right)
      {
        // LJCIsDifferentRow() Sets the LJCLastRowIndex for new row.
        MethodGroupGrid.Select();
        if (MethodGroupGrid.LJCIsDifferentRow(e))
        {
          // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
          MethodGroupGrid.LJCSetCurrentRow(e);
          TimedChange(Change.MethodGroup);
        }
      }
    }

    // Handles the SelectionChanged event.
    private void MethodGroupGrid_SelectionChanged(object sender, EventArgs e)
    {
      if (MethodGroupGrid.LJCAllowSelectionChange)
      {
        TimedChange(Change.MethodGroup);
      }
      MethodGroupGrid.LJCAllowSelectionChange = true;
    }
    #endregion

    #region Method Item

    // Handles the MouseDown event.
    private void MethodItemGrid_MouseDown(object sender, MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Right)
      {
        // LJCIsDifferentRow() Sets the LJCLastRowIndex for new row.
        MethodItemGrid.Select();
        if (MethodItemGrid.LJCIsDifferentRow(e))
        {
          // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
          MethodItemGrid.LJCSetCurrentRow(e);
          TimedChange(Change.MethodItem);
        }
      }
    }

    // Handles the SelectionChanged event.
    private void MethodItemGrid_SelectionChanged(object sender, EventArgs e)
    {
      if (MethodItemGrid.LJCAllowSelectionChange)
      {
        TimedChange(Change.MethodItem);
      }
      MethodItemGrid.LJCAllowSelectionChange = true;
    }
    #endregion

    #endregion
  }
}
