// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// ClassHeadingSelect.cs
using System;
using System.Drawing;
using System.Windows.Forms;

namespace LJCGenDocEdit
{
  // <summary>The ClassGroupHeading Selection List</summary>
  internal partial class ClassHeadingSelect : Form
  {
    #region Constructors

    // Initializes an object instance.
    internal ClassHeadingSelect()
    {
      Cursor = Cursors.WaitCursor;
      InitializeComponent();

      // Initialize property values.
      LJCIsSelect = false;
      Cursor = Cursors.Default;
    }
    #endregion

    #region Form Event Handlers

    // Configures the form and loads the initial control data.
    private void ClassHeadingSelect_Load(object sender, EventArgs e)
    {
      InitializeControls();
      CenterToParent();
    }
    #endregion

    #region Action Event Handlers

    // Calls the Edit method.
    private void HeadingEdit_Click(object sender, EventArgs e)
    {
      ClassHeadingGridCode.DoEdit();
    }

    // Calls the Refresh method.
    private void HeadingRefresh_Click(object sender, EventArgs e)
    {
      ClassHeadingGridCode.DoRefresh();
    }

    // Calls the Select method.
    private void HeadingSelect_Click(object sender, EventArgs e)
    {
      ClassHeadingGridCode.DoSelect();
    }

    // Export a text file.
    private void HeadingText_Click(object sender, EventArgs e)
    {
      string extension = mSettings.ExportTextExtension;
      string fileSpec = $@"ExportFiles\ClassHeading.{extension}";
      ClassHeadingGrid.LJCExportData(fileSpec);
    }

    // Export a CSV file.
    private void HeadingCSV_Click(object sender, EventArgs e)
    {
      string fileSpec = $@"ExportFiles\ClassHeading.csv";
      ClassHeadingGrid.LJCExportData(fileSpec);
    }

    // Performs the Close function.
    private void HeadingClose_Click(object sender, EventArgs e)
    {
      SaveControlValues();
      Close();
    }
    #endregion

    #region Control Event Handlers

    // Handles the form keys.
    private void ClassHeadingGrid_KeyDown(object sender, KeyEventArgs e)
    {
      switch (e.KeyCode)
      {
        case Keys.Enter:
          ClassHeadingGridCode.DoDefault();
          e.Handled = true;
          break;

        case Keys.F5:
          ClassHeadingGridCode.DoRefresh();
          e.Handled = true;
          break;
      }
    }

    // Handles the MouseDoubleClick event.
    private void ClassHeadingGrid_MouseDoubleClick(object sender, MouseEventArgs e)
    {
      if (ClassHeadingGrid.LJCGetMouseRow(e) != null)
      {
        ClassHeadingGridCode.DoDefault();
      }
    }

    // Handles the MouseDown event.
    private void ClassHeadingGrid_MouseDown(object sender, MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Right)
      {
        // LJCIsDifferentRow() Sets the LJCLastRowIndex for new row.
        ClassHeadingGrid.Select();
        if (ClassHeadingGrid.LJCIsDifferentRow(e))
        {
          // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
          ClassHeadingGrid.LJCSetCurrentRow(e);
          //TimedChange(Change._ClassName_);
        }
      }
    }

    // Handles the SelectionChanged event.
    private void ClassHeadingGrid_SelectionChanged(object sender, EventArgs e)
    {
      if (ClassHeadingGrid.LJCAllowSelectionChange)
      {
        //TimedChange(Change._ClassName_);
      }
      ClassHeadingGrid.LJCAllowSelectionChange = true;
    }
    #endregion
  }
}
