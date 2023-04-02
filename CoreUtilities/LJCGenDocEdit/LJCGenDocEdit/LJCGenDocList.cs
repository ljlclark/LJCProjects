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
      CenterToParent();
    }
    #endregion

    #region Control Event Handlers

    // Handles the MouseDown event.
    private void AssemblyGrid_MouseDown(object sender, MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Right)
      {
        // LJCIsDifferentRow() Sets the LJCLastRowIndex for new row.
        AssemblyGrid.Select();
        if (AssemblyGrid.LJCIsDifferentRow(e))
        {
          // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
          AssemblyGrid.LJCSetCurrentRow(e);
          TimedChange(Change.AssemblyGroup);
        }
      }
    }

    // Handles the SelectionChanged event.
    private void AssemblyGrid_SelectionChanged(object sender, EventArgs e)
    {
      if (AssemblyGrid.LJCAllowSelectionChange)
      {
        TimedChange(Change.AssemblyGroup);
      }
      AssemblyGrid.LJCAllowSelectionChange = true;
    }

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
  }
}
