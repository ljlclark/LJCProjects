// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// MethodSelect.cs
using System;
using System.Windows.Forms;

namespace LJCGenDocEdit
{
  /// <summary>The Method Selection List</summary>
  internal partial class MethodSelect : Form
  {
    #region Constructors

    /// <summary>Initializes an object instance.</summary>
    internal MethodSelect()
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
    private void MethodSelect_Load(object sender, EventArgs e)
    {
      InitializeControls();
      CenterToParent();
    }
    #endregion

    #region Action Event Handlers

    // Calls the Select method.
    private void MethodSelectItem_Click(object sender, EventArgs e)
    {
      MethodGridCode.DoSelect();
    }

    // Export a text file.
    private void MethodText_Click(object sender, EventArgs e)
    {
      string extension = mSettings.ExportTextExtension;
      string fileSpec = $@"ExportFiles\MethodName.{extension}";
      MethodGrid.LJCExportData(fileSpec);
    }

    // Export a CSV file.
    private void MethodCSV_Click(object sender, EventArgs e)
    {
      string fileSpec = $@"ExportFiles\MethodName.csv";
      MethodGrid.LJCExportData(fileSpec);
    }

    // Performs the Close function.
    private void MethodClose_Click(object sender, EventArgs e)
    {
      SaveControlValues();
      Close();
    }

    // Shows the help page.
    private void MethodHelp_Click(object sender, EventArgs e)
    {
      Help.ShowHelp(this, "GenDocEdit.chm", HelpNavigator.Topic
        , @"Method\MethodSelect.html");
    }
    #endregion

    #region Control Event Handlers

    // Fires the Change event.
    /// <include path='items/LJCOnChange/*' file='../../LJCDocLib/Common/Detail.xml'/>
    // *** Add Method *** MultiSelect 10/29/23
    internal void LJCOnChange()
    {
      LJCChange?.Invoke(this, new EventArgs());
    }

    // Handles the form keys.
    private void MethodGrid_KeyDown(object sender, KeyEventArgs e)
    {
      switch (e.KeyCode)
      {
        case Keys.Enter:
          e.Handled = true;
          break;
      }
    }

    // Handles the MouseDoubleClick event.
    private void MethodGrid_MouseDoubleClick(object sender, MouseEventArgs e)
    {
      if (MethodGrid.LJCGetMouseRow(e) != null)
      {
        //MethodGridCode.DoSelect();
      }
    }

    // Handles the MouseDown event.
    private void MethodGrid_MouseDown(object sender, MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Right)
      {
        // LJCIsDifferentRow() Sets the LJCLastRowIndex for new row.
        MethodGrid.Select();
        // *** Next Statement *** Add - MultiSelect 10/29/23 
        if (1 == MethodGrid.SelectedRows.Count)
        {
          if (MethodGrid.LJCIsDifferentRow(e))
          {
            // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
            MethodGrid.LJCSetCurrentRow(e);
          }
        }
      }
    }

    // Handles the SelectionChanged event.
    private void MethodGrid_SelectionChanged(object sender, EventArgs e)
    {
      MethodGrid.LJCAllowSelectionChange = true;
    }
    #endregion

    #region Properties

    public bool LastMultiSelect { get; set; }
    #endregion

    #region Class Data

    /// <summary>The Change event.</summary>
    /// // *** Next Statement *** Add - MultiSelect 10/29/23
    public event EventHandler<EventArgs> LJCChange;
    #endregion
  }
}
