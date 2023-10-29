// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// ClassSelect.cs
using System;
using System.Windows.Forms;

namespace LJCGenDocEdit
{
  /// <summary>The Class Selection List</summary>
  internal partial class ClassSelect : Form
  {
    #region Constructors

    /// <summary>Initializes an object instance.</summary>
    internal ClassSelect()
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
    private void ClassSelect_Load(object sender, EventArgs e)
    {
      InitializeControls();
      CenterToParent();
    }
    #endregion

    #region Action Event Handlers

    // Calls the Select method.
    private void ClassSelectItem_Click(object sender, EventArgs e)
    {
      ClassGridCode.DoSelect();
    }

    // Export a text file.
    private void ClassText_Click(object sender, EventArgs e)
    {
      string extension = mSettings.ExportTextExtension;
      string fileSpec = $@"ExportFiles\ClassName.{extension}";
      ClassGrid.LJCExportData(fileSpec);
    }

    // Export a CSV file.
    private void ClassCSV_Click(object sender, EventArgs e)
    {
      string fileSpec = $@"ExportFiles\ClassName.csv";
      ClassGrid.LJCExportData(fileSpec);
    }

    // Performs the Close function.
    private void ClassClose_Click(object sender, EventArgs e)
    {
      SaveControlValues();
      Close();
    }

    // Shows the help page.
    private void ClassHelp_Click(object sender, EventArgs e)
    {
      Help.ShowHelp(this, "GenDocEdit.chm", HelpNavigator.Topic
        , @"Class\ClassSelect.html");
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
    private void ClassGrid_KeyDown(object sender, KeyEventArgs e)
    {
      switch (e.KeyCode)
      {
        case Keys.Enter:
          e.Handled = true;
          break;
      }
    }

    // Handles the MouseDoubleClick event.
    private void ClassGrid_MouseDoubleClick(object sender, MouseEventArgs e)
    {
      if (ClassGrid.LJCGetMouseRow(e) != null)
      {
        //ClassGridCode.DoSelect();
      }
    }

    // Handles the MouseDown event.
    private void ClassGrid_MouseDown(object sender, MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Right)
      {
        // LJCIsDifferentRow() Sets the LJCLastRowIndex for new row.
        ClassGrid.Select();
        // *** Next Statement *** Add - MultiSelect 10/29/23 
        if (1 == ClassGrid.SelectedRows.Count)
        {
          if (ClassGrid.LJCIsDifferentRow(e))
          {
            // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
            ClassGrid.LJCSetCurrentRow(e);
          }
        }
      }
    }

    // Handles the SelectionChanged event.
    private void ClassGrid_SelectionChanged(object sender, EventArgs e)
    {
      ClassGrid.LJCAllowSelectionChange = true;
    }
    #endregion

    public bool LastMultiSelect { get; set; }

    #region Class Data

    /// <summary>The Change event.</summary>
    /// // *** Next Statement *** Add - MultiSelect 10/29/23
    public event EventHandler<EventArgs> LJCChange;
    #endregion
  }
}
