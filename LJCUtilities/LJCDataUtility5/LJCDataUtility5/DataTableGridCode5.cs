// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DataTableGridCode5.cs
using LJCControls5;
using LJCDataUtilityDAL5;
using LJCDBClientLib5;
using LJCDBDataAccess5;
using LJCNetCommon5;

namespace LJCDataUtility5
{
  // Provides DataTableGrid methods for the DataUtilityList window.
  internal class DataTableGridCode
  {
    #region Constructor Methods

    // Initializes an object instance.
    internal DataTableGridCode(Form1 parentObject)
    {
      // Initialize property values.
      ParentObject = parentObject;
      ParentObject.Cursor = Cursors.WaitCursor;

      // Set Grid vars.
      TableGrid = parentObject.MainGrid;
      Managers = ParentObject.Managers;
      TableManager = Managers.DataTableManager!;

      // Grid events.
      var grid = TableGrid;
      grid.KeyDown += TableGrid_KeyDown;
      grid.MouseDoubleClick += TableGrid_MouseDoubleClick;
      grid.MouseDown += TableGrid_MouseDown;
      grid.SelectionChanged += TableGrid_SelectionChanged;

      parentObject.Cursor = Cursors.Default;
    }

    // Configures the Grid.
    internal void SetupGrid()
    {
      // Setup default grid columns if no columns are defined.
      if (0 == TableGrid.Columns.Count)
      {
        var gridColumns = new LJCDataColumns
        {
          DataUtilTable.ColumnName,
        };
        gridColumns.Add(DataUtilTable.ColumnDescription, caption: "A Description");

        // Setup the grid columns.
        TableGrid.LJCAddColumns(gridColumns);
      }
    }
    #endregion

    #region Data Methods

    // Retrieves the list rows.
    internal void DataRetrieve()
    {
      ParentObject.Cursor = Cursors.WaitCursor;
      TableGrid.LJCRowsClear();

      //var ljcItem = ModuleCombo.SelectedItem as LJCItem;
      //var moduleDbID = ljcItem.DbID;
      //var moduleID = ljcItem.ID;
      // Testing
      var moduleDbId = 1;
      short moduleId = 1;

      if (moduleDbId > 0
        && moduleId > 0)
      {
        var keyColumns = DataTableManager.ParentKey(moduleDbId, moduleId);
        var orderBy = new List<string>()
        {
          DataUtilTable.ColumnSequence
        };
        if (TableManager.Manager != null)
        {
          TableManager.Manager.OrderByNames = orderBy;
        }
        var items = TableManager.Load(keyColumns);
        if (LJC.HasListItems(items))
        {
          foreach (var item in items)
          {
            RowAdd(item);
          }
        }
      }
      SetControlState();
      ParentObject.Cursor = Cursors.Default;
      //ParentObject.DoChange(Change.Table);
    }

    // Adds a grid row and updates it with the record values.
    private LJCGridRow? RowAdd(DataUtilTable data)
    {
      var retValue = TableGrid.LJCRowAdd();
      if (retValue != null)
      {
        SetStoredValues(retValue, data);
        retValue.LJCSetValues(data);
      }
      return retValue;
    }

    // Selects a row based on the ID value.
    internal bool RowSelect(short dbId, long id)
    {
      var data = new DataUtilTable()
      {
        DbId = dbId,
        Id = id,
      };
      var retValue = RowSelect(data);
      return retValue;
    }

    // Selects a row based on the data values.
    private bool RowSelect(DataUtilTable data)
    {
      bool retValue = false;

      if (data != null)
      {
        ParentObject.Cursor = Cursors.WaitCursor;
        foreach (LJCGridRow row in TableGrid.Rows)
        {
          var rowDbId = row.LJCGetInt16(DataUtilColumn.ColumnDbId);
          var rowID = row.LJCGetInt64(DataUtilTable.ColumnId);
          if (rowDbId == data.DbId
            && rowID == data.Id)
          {
            // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
            TableGrid.LJCSetCurrentRow(row, true);
            retValue = true;
            break;
          }
        }
        ParentObject.Cursor = Cursors.Default;
      }
      return retValue;
    }

    // Updates the current row with the record values.
    private void RowUpdate(DataUtilTable data)
    {
      if (TableGrid.CurrentRow is LJCGridRow row)
      {
        SetStoredValues(row, data);
        row.LJCSetValues(data);
      }
    }

    // Sets the control states based on the current control values.
    private void SetControlState()
    {
    }

    // Sets the row stored values.
    private static void SetStoredValues(LJCGridRow row, DataUtilTable data)
    {
      row.LJCSetInt16(DataUtilTable.ColumnDbId, data.DbId);
      row.LJCSetInt64(DataUtilTable.ColumnId, data.Id);
      row.LJCSetString(DataUtilTable.ColumnName, data.Name);
    }
    #endregion

    #region Control Event Handlers

    // Handles the Grid KeyDown event.
    private void TableGrid_KeyDown(object? sender, KeyEventArgs e)
    {
      MessageBox.Show("KeyDown");
    }

    // Handles the Grid MouseDoubleClick event.
    private void TableGrid_MouseDoubleClick(object? sender, MouseEventArgs e)
    {
      if (TableGrid.LJCGetMouseRow(e) != null)
      {
        MessageBox.Show("MouseDoubleClick");
      }
    }

    // Handles the MouseDown event.
    private void TableGrid_MouseDown(object? sender, MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Right)
      {
        // LJCIsDifferentRow() Sets the LJCLastRowIndex for new row.
        if (TableGrid.LJCIsDifferentRow(e))
        {
          // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
          TableGrid.LJCSetCurrentRow(e);
          //SetControlState();
          //ParentObject.TimedChange(Change.Table);
        }
      }
    }

    // Handles the SelectionChanged event.
    private void TableGrid_SelectionChanged(object? sender, EventArgs e)
    {
      if (TableGrid.LJCAllowSelectionChange)
      {
        //SetControlState();
        //ParentObject.TimedChange(Change.Table);
      }
      TableGrid.LJCAllowSelectionChange = true;
    }
    #endregion

    #region Properties

    // Gets or sets the Parent List reference.
    private Form1 ParentObject { get; set; }

    // Gets or sets the Managers reference.
    private ManagersDataUtility Managers { get; set; }

    // Gets or sets the Grid reference.
    private LJCDataGrid TableGrid { get; set; }

    // Gets or sets the Manager reference.
    private DataTableManager TableManager { get; set; }
    #endregion
  }
}
