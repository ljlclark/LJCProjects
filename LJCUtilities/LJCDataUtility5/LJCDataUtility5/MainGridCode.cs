// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// MainGridCode.cs
using LJCControls5;
using LJCNetCommon5;

namespace LJCDataUtility5
{
  internal class MainGridCode
  {
    #region Constructor Methods

    public MainGridCode(Form1 parentObject)
    {
      // Initialize property values.
      ParentObject = parentObject;
      ParentObject.Cursor = Cursors.WaitCursor;

      // Set Grid vars.
      MainGrid = parentObject.MainGrid;

      // Grid events.
      var grid = MainGrid;
      grid.KeyDown += MainGrid_KeyDown;
      grid.MouseDoubleClick += MainGrid_MouseDoubleClick;
      grid.MouseDown += MainGrid_MouseDown;
      grid.SelectionChanged += MainGrid_SelectionChanged;

      parentObject.Cursor = Cursors.Default;
    }

    // Configures the Grid.
    internal void SetupGrid()
    {
      // Setup default grid columns if no columns are defined.
      if (0 == MainGrid.Columns.Count)
      {
        var gridColumns = new LJCDataColumns();
        gridColumns.Add("Name", caption: "Name");
        gridColumns.Add("Description", caption: "A Description");

        // Setup the grid columns.
        MainGrid.LJCAddColumns(gridColumns);
      }
    }
    #endregion

    #region Control Event Handlers

    // Handles the Grid KeyDown event.
    private void MainGrid_KeyDown(object? sender, KeyEventArgs e)
    {
      MessageBox.Show("KeyDown");
    }

    // Handles the Grid MouseDoubleClick event.
    private void MainGrid_MouseDoubleClick(object? sender, MouseEventArgs e)
    {
      if (MainGrid.LJCGetMouseRow(e) != null)
      {
        MessageBox.Show("MouseDoubleClick");
      }
    }

    // Handles the MouseDown event.
    private void MainGrid_MouseDown(object? sender, MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Right)
      {
        // LJCIsDifferentRow() Sets the LJCLastRowIndex for new row.
        if (MainGrid.LJCIsDifferentRow(e))
        {
          // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
          MainGrid.LJCSetCurrentRow(e);
          //SetControlState();
          //ParentObject.TimedChange(Change.Table);
        }
      }
    }

    // Handles the SelectionChanged event.
    private void MainGrid_SelectionChanged(object? sender, EventArgs e)
    {
      if (MainGrid.LJCAllowSelectionChange)
      {
        //SetControlState();
        //ParentObject.TimedChange(Change.Table);
      }
      MainGrid.LJCAllowSelectionChange = true;
    }
    #endregion

    #region Properties

    private Form1 ParentObject { get; set; }

    private LJCDataGrid5 MainGrid { get; set; }
    #endregion
  }
}
