// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// MethodHeadingSelect.cs
using System.Drawing;
using System.Windows.Forms;

namespace LJCGenDocEdit
{
  // <summary>The ClassGroupHeading Selection List</summary>
  internal partial class MethodHeadingSelect : Form
  {
    #region Constructors

    /// <summary>Initializes an object instance.</summary>
    internal MethodHeadingSelect()
    {
      Cursor = Cursors.WaitCursor;
      InitializeComponent();

      // Initialize property values.
      LJCIsSelect = false;

      // Set default class data.
      Cursor = Cursors.Default;
    }
    #endregion

    #region Form Event Handlers

    // Configures the form and loads the initial control data.
    private void MethodHeadingSelect_Load(object sender, System.EventArgs e)
    {
      InitializeControls();
      CenterToParent();
    }
    #endregion

    #region Action Event Handlers

    // Calls the Edit method.
    private void HeadingEdit_Click(object sender, System.EventArgs e)
    {
      MethodHeadingGridCode.DoEdit();
    }

    // Calls the Refresh method.
    private void HeadingRefresh_Click(object sender, System.EventArgs e)
    {
      MethodHeadingGridCode.DoRefresh();
    }

    // Calls the Select method.
    private void HeadingSelect_Click(object sender, System.EventArgs e)
    {
      MethodHeadingGridCode.DoSelect();
    }

    // Export a text file.
    private void HeadingText_Click(object sender, System.EventArgs e)
    {
      string extension = mSettings.ExportTextExtension;
      string fileSpec = $@"ExportFiles\MethodHeading.{extension}";
      MethodHeadingGrid.LJCExportData(fileSpec);
    }

    // Export a CSV file.
    private void HeadingCSV_Click(object sender, System.EventArgs e)
    {
      string fileSpec = $@"ExportFiles\MethodHeading.csv";
      MethodHeadingGrid.LJCExportData(fileSpec);
    }

    // Performs the Close function.
    private void HeadingClose_Click(object sender, System.EventArgs e)
    {
      SaveControlValues();
      Close();
    }
    #endregion

    #region Control Event Handlers

    // Handles the form keys.
    private void MethodHeadingGrid_KeyDown(object sender, KeyEventArgs e)
    {
      switch (e.KeyCode)
      {
        case Keys.Enter:
          MethodHeadingGridCode.DoDefault();
          e.Handled = true;
          break;

        case Keys.F5:
          MethodHeadingGridCode.DoRefresh();
          e.Handled = true;
          break;
      }
    }

    // Handles the MouseDoubleClick event.
    private void MethodHeadingGrid_MouseDoubleClick(object sender, MouseEventArgs e)
    {
      if (MethodHeadingGrid.LJCGetMouseRow(e) != null)
      {
        MethodHeadingGridCode.DoDefault();
      }
    }

    // Handles the MouseDown event.
    private void MethodHeadingGrid_MouseDown(object sender, MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Right)
      {
        // LJCIsDifferentRow() Sets the LJCLastRowIndex for new row.
        MethodHeadingGrid.Select();
        if (MethodHeadingGrid.LJCIsDifferentRow(e))
        {
          // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
          MethodHeadingGrid.LJCSetCurrentRow(e);
          //TimedChange(Change._ClassName_);
        }
      }
    }

    // Handles the SelectionChanged event.
    private void MethodHeadingGrid_SelectionChanged(object sender, System.EventArgs e)
    {
      if (MethodHeadingGrid.LJCAllowSelectionChange)
      {
        //TimedChange(Change._ClassName_);
      }
      MethodHeadingGrid.LJCAllowSelectionChange = true;
    }
    #endregion
  }
}
