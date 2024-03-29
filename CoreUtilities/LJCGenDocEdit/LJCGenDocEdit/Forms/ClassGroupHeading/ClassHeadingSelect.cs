﻿// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// ClassHeadingSelect.cs
using System;
using System.Windows.Forms;

namespace LJCGenDocEdit
{
  // <summary>The ClassGroupHeading Selection List</summary>
  internal partial class ClassHeadingSelect : Form
  {
    #region Constructors

    /// <summary>Initializes an object instance.</summary>
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

    // Displays a detail dialog for a new record.
    private void HeadingNew_Click(object sender, EventArgs e)
    {
      ClassHeadingGridCode.DoNew();
    }

    // Calls the Edit method.
    private void HeadingEdit_Click(object sender, EventArgs e)
    {
      ClassHeadingGridCode.DoEdit();
    }

    // Deletes the selected row.
    private void HeadingDelete_Click(object sender, EventArgs e)
    {
      ClassHeadingGridCode.DoDelete();
    }

    // Calls the Refresh method.
    private void HeadingRefresh_Click(object sender, EventArgs e)
    {
      ClassHeadingGridCode.DoRefresh();
    }

    // Resets the sequence values.
    private void HeadingReset_Click(object sender, EventArgs e)
    {
      ClassHeadingGridCode.DoResetSequence();
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

    // Shows the help page.
    private void HeadingHelp_Click(object sender, EventArgs e)
    {
      Help.ShowHelp(this, "GenDocEdit.chm", HelpNavigator.Topic
        , @"Class\ClassHeadingSelect.html");
    }
    #endregion

    #region Control Event Handlers

    // Fires the Change event.
    /// <include path='items/LJCOnChange/*' file='../../LJCGenDoc/Common/Detail.xml'/>
    internal void LJCOnChange()
    {
      LJCChange?.Invoke(this, new EventArgs());
    }

    // Handles the DragDrop event.
    private void ClassHeadingGrid_DragDrop(object sender, DragEventArgs e)
    {
      ClassHeadingGridCode.DoDragDrop(e);
    }

    // Handles the form keys.
    private void ClassHeadingGrid_KeyDown(object sender, KeyEventArgs e)
    {
      switch (e.KeyCode)
      {
        case Keys.Enter:
          ClassHeadingGridCode.DoEdit();
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
        ClassHeadingGridCode.DoEdit();
      }
    }

    // Handles the MouseDown event.
    private void ClassHeadingGrid_MouseDown(object sender, MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Right)
      {
        // LJCIsDifferentRow() Sets the LJCLastRowIndex for new row.
        ClassHeadingGrid.Select();

        // If only one row is selected.
        if (1 == ClassHeadingGrid.SelectedRows.Count)
        {
          if (ClassHeadingGrid.LJCIsDifferentRow(e))
          {
            // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
            ClassHeadingGrid.LJCSetCurrentRow(e);
          }
        }
      }
    }

    // Handles the SelectionChanged event.
    private void ClassHeadingGrid_SelectionChanged(object sender, EventArgs e)
    {
      ClassHeadingGrid.LJCAllowSelectionChange = true;
    }
    #endregion

    #region Properties

    /// <summary>Gets or sets the parent Assembly ID value.</summary>
    public short LJCAssemblyID { get; set; }

    // Gets or sets the indicator for last multiselect row.
    public bool LastMultiSelect { get; set; }
    #endregion

    #region Class Data

    /// <summary>The Change event.</summary>
    public event EventHandler<EventArgs> LJCChange;
    #endregion
  }
}
