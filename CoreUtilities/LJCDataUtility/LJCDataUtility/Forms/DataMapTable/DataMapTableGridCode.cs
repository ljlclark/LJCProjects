// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DataMapTableGridCode.cs
using static LJCDataUtility.DataUtilityList;
using LJCDataUtilityDAL;
using LJCNetCommon;
using LJCWinFormCommon;
using LJCWinFormControls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace LJCDataUtility
{
  // Provides DataMapTableGrid methods for the DataUtilityList window.
  internal class DataMapTableGridCode
  {
    #region Constructors

    // Initialize property values.
    // ********************
    internal DataMapTableGridCode(DataUtilityList parentList)
    {
      // Initialize property values.
      UtilityList = parentList;
      UtilityList.Cursor = Cursors.WaitCursor;

      TableGrid = UtilityList.TableGrid;
      //MapTableGrid = UtilityList.MapTableGrid;
      MapTableMenu = UtilityList.MapTableMenu;
      Managers = UtilityList.Managers;
      MapTableManager = Managers.DataMapTableManager;

      var fontFamily = UtilityList.Font.FontFamily;
      var style = UtilityList.Font.Style;
      MapTableGrid.Font = new Font(fontFamily, 11, style);
      MapTableMenu.Font = new Font(fontFamily, 11, style);
      _ = new GridFont(UtilityList, MapTableGrid);
      _ = new MenuFont(MapTableMenu);

      // Menu item events.
      var list = UtilityList;
      list.MapTableNew.Click += MapTableNew_Click;
      list.MapTableEdit.Click += MapTableEdit_Click;
      list.MapTableDelete.Click += MapTableDelete_Click;
      list.MapTableRefresh.Click += MapTableRefresh_Click;
      list.MapTableExit.Click += list.Exit_Click;

      // Grid events.
      var grid = MapTableGrid;
      grid.KeyDown += MapTableGrid_KeyDown;
      grid.MouseDoubleClick += MapTableGrid_MouseDoubleClick;
      grid.MouseDown += MapTableGrid_MouseDown;
      grid.SelectionChanged += MapTableGrid_SelectionChanged;
      UtilityList.Cursor = Cursors.Default;
    }
    #endregion

    #region Data Methods

    // Retrieves the list rows.
    // ********************
    internal void DataRetrieve()
    {
      UtilityList.Cursor = Cursors.WaitCursor;
      MapTableGrid.LJCRowsClear();

      if (TableGrid.CurrentRow is LJCGridRow parentRow)
      {
        // Data from items.
        int parentID = parentRow.LJCGetInt32(DataUtilTable.ColumnID);

        var keyColumns = MapTableManager.ParentKey(parentID);
        var items = MapTableManager.Load(keyColumns);
        if (NetCommon.HasItems(items))
        {
          foreach (var item in items)
          {
            RowAdd(item);
          }
        }
      }
      SetControlState();
      UtilityList.Cursor = Cursors.Default;
      UtilityList.DoChange(Change.MapTable);
    }

    // Adds a grid row and updates it with the record values.
    // ********************
    private LJCGridRow RowAdd(DataMapTable dataRecord)
    {
      var retValue = MapTableGrid.LJCRowAdd();
      SetStoredValues(retValue, dataRecord);
      retValue.LJCSetValues(MapTableGrid, dataRecord);
      return retValue;
    }

    // Selects a row based on the key record values.
    // ********************
    private bool RowSelect(DataMapTable dataRecord)
    {
      bool retValue = false;

      if (dataRecord != null)
      {
        UtilityList.Cursor = Cursors.WaitCursor;
        foreach (LJCGridRow row in MapTableGrid.Rows)
        {
          var rowID = row.LJCGetInt32(DataMapTable.ColumnDataTableID);
          if (rowID == dataRecord.DataTableID)
          {
            // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
            MapTableGrid.LJCSetCurrentRow(row, true);
            retValue = true;
            break;
          }
        }
        UtilityList.Cursor = Cursors.Default;
      }
      return retValue;
    }

    // Updates the current row with the record values.
    // ********************
    private void RowUpdate(DataMapTable dataRecord)
    {
      if (MapTableGrid.CurrentRow is LJCGridRow row)
      {
        SetStoredValues(row, dataRecord);
        row.LJCSetValues(MapTableGrid, dataRecord);
      }
    }

    // Sets the control states based on the current control values.
    // ********************
    private void SetControlState()
    {
      bool enableNew = TableGrid.CurrentRow != null;
      bool enableEdit = MapTableGrid.CurrentRow != null;
      FormCommon.SetMenuState(MapTableMenu, enableNew, enableEdit);
      UtilityList.MapTableHeading.Enabled = true;
    }

    // Sets the row stored values.
    // ********************
    private void SetStoredValues(LJCGridRow row, DataMapTable dataRecord)
    {
      row.LJCSetInt32(DataMapTable.ColumnDataTableID
        , dataRecord.DataTableID);
    }
    #endregion

    #region Action Methods

    // Deletes the selected row.
    // ********************
    internal void Delete()
    {
      bool success = false;
      var row = MapTableGrid.CurrentRow as LJCGridRow;
      if (row != null)
      {
        var title = "Delete Confirmation";
        var message = FormCommon.DeleteConfirm;
        if (MessageBox.Show(message, title, MessageBoxButtons.YesNo
          , MessageBoxIcon.Question) == DialogResult.Yes)
        {
          success = true;
        }
      }

      if (success)
      {
        // Data from items.
        var id = row.LJCGetInt32(DataUtilTable.ColumnID);

        var keyColumns = new DbColumns()
        {
          { DataUtilTable.ColumnID, id }
        };
        MapTableManager.Delete(keyColumns);
        if (0 == MapTableManager.AffectedCount)
        {
          success = false;
          var message = FormCommon.DeleteError;
          MessageBox.Show(message, "Delete Error", MessageBoxButtons.OK
            , MessageBoxIcon.Exclamation);
        }
      }

      if (success)
      {
        MapTableGrid.Rows.Remove(row);
        SetControlState();
        UtilityList.TimedChange(Change.MapTable);
      }
    }

    // Displays a detail dialog to edit a record.
    // ********************
    internal void Edit()
    {
      if (TableGrid.CurrentRow is LJCGridRow parentRow)
      {
        // Data from items.
        int parentID = parentRow.LJCGetInt32(DataUtilTable.ColumnID);
        string parentName = parentRow.LJCGetString(DataUtilTable.ColumnName);

        var detail = new DataColumnDetail()
        {
          LJCManagers = Managers,
          LJCParentID = parentID,
          LJCParentName = parentName,
        };
        detail.LJCChange += Detail_Change;
        detail.ShowDialog();
      }
    }

    // Displays a detail dialog for a new record.
    // ********************
    internal void New()
    {
      if (TableGrid.CurrentRow is LJCGridRow parentRow)
      {
        // Data from list items.
        int parentID = parentRow.LJCGetInt32(DataUtilTable.ColumnID);
        string parentName = parentRow.LJCGetString(DataUtilTable.ColumnName);

        var detail = new DataMapTableDetail
        {
          LJCManagers = Managers,
          LJCParentID = parentID,
          LJCParentName = parentName
        };
        detail.LJCChange += Detail_Change;
        detail.ShowDialog();
      }
    }

    // Refreshes the list.
    // ********************
    internal void Refresh()
    {
      UtilityList.Cursor = Cursors.WaitCursor;
      int id = 0;
      if (MapTableGrid.CurrentRow is LJCGridRow row)
      {
        // Save the original row.
        id = row.LJCGetInt32(DataMapTable.ColumnDataTableID);
      }
      DataRetrieve();

      // Select the original row.
      if (id > 0)
      {
        var record = new DataMapTable()
        {
          DataTableID = id
        };
        RowSelect(record);
      }
      UtilityList.Cursor = Cursors.Default;
    }

    // Shows the help page
    // ********************
    internal void ShowHelp()
    {
      //Help.ShowHelp(DocList, "_AppName_.chm", HelpNavigator.Topic
      //  , "_ClassName_List.html");
    }

    // Adds new row or updates row with control values.
    // ********************
    private void Detail_Change(object sender, EventArgs e)
    {
      var detail = sender as DataMapTableDetail;
      var record = detail.LJCRecord;
      if (record != null)
      {
        if (detail.LJCIsUpdate)
        {
          RowUpdate(record);
        }
        else
        {
          // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
          var row = RowAdd(record);
          MapTableGrid.LJCSetCurrentRow(row, true);
          SetControlState();
          UtilityList.TimedChange(Change.MapTable);
        }
      }
    }
    #endregion

    #region Setup and Other Methods

    // Configures the Grid.
    // ********************
    internal void SetupGrid()
    {
      // Setup default grid columns if no columns are defined.
      if (0 == MapTableGrid.Columns.Count)
      {
        List<string> propertyNames = new List<string>()
        {
          DataMapTable.ColumnDataTableID,
          DataMapTable.ColumnNewTableName
        };

        // Get the grid columns from the manager Data Definition.
        var gridColumns = MapTableManager.GetColumns(propertyNames);

        // Setup the grid columns.
        MapTableGrid.LJCAddColumns(gridColumns);
      }
    }
    #endregion

    #region Action Event Handlers

    // Handles the New menu item event.
    // ********************
    private void MapTableNew_Click(object sender, EventArgs e)
    {
      New();
    }

    // Handles the Edit menu item event.
    // ********************
    private void MapTableEdit_Click(object sender, EventArgs e)
    {
      Edit();
    }

    // Handles the Delete menu item event.
    // ********************
    private void MapTableDelete_Click(object sender, EventArgs e)
    {
      Delete();
    }

    // Handles the Refresh menu item event.
    // ********************
    private void MapTableRefresh_Click(object sender, EventArgs e)
    {
      Refresh();
    }
    #endregion

    #region Control Event Handlers

    // Handles the Grid KeyDown event.
    // ********************
    private void MapTableGrid_KeyDown(object sender, KeyEventArgs e)
    {
      switch (e.KeyCode)
      {
        case Keys.Enter:
          Edit();
          e.Handled = true;
          break;

        case Keys.F1:
          ShowHelp();
          e.Handled = true;
          break;

        case Keys.F5:
          Refresh();
          e.Handled = true;
          break;

        case Keys.M:
          if (e.Control)
          {
            var position = FormCommon.GetMenuScreenPoint(MapTableGrid
              , Control.MousePosition);
            var menu = UtilityList.ColumnMenu;
            menu.Show(position);
            menu.Select();
            e.Handled = true;
          }
          break;

        case Keys.Tab:
          if (e.Shift)
          {
            UtilityList.MainTabs.Select();
          }
          else
          {
            UtilityList.MainTabs.Select();
          }
          e.Handled = true;
          break;
      }
    }

    // Handles the Grid MouseDoubleClick event.
    // ********************
    private void MapTableGrid_MouseDoubleClick(object sender, MouseEventArgs e)
    {
      if (MapTableGrid.LJCGetMouseRow(e) != null)
      {
        New();
      }
    }

    // Handles the MouseDown event.
    private void MapTableGrid_MouseDown(object sender, MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Right)
      {
        // LJCIsDifferentRow() Sets the LJCLastRowIndex for new row.
        MapTableGrid.Select();
        if (MapTableGrid.LJCIsDifferentRow(e))
        {
          // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
          MapTableGrid.LJCSetCurrentRow(e);
          UtilityList.TimedChange(Change.MapTable);
        }
      }
    }

    // Handles the SelectionChanged event.
    private void MapTableGrid_SelectionChanged(object sender, EventArgs e)
    {
      if (MapTableGrid.LJCAllowSelectionChange)
      {
        UtilityList.TimedChange(Change.MapTable);
      }
      MapTableGrid.LJCAllowSelectionChange = true;
    }
    #endregion

    #region Properties

    // Gets or sets the Parent List reference.
    private DataUtilityList UtilityList { get; set; }

    // Gets or sets the parent Grid reference.
    private LJCDataGrid TableGrid { get; set; }

    // Gets or sets the Grid reference.
    private LJCDataGrid MapTableGrid { get; set; }

    // Gets or sets the Menu reference.
    private ContextMenuStrip MapTableMenu { get; set; }

    // Gets or sets the Managers reference.
    private ManagersDataUtility Managers { get; set; }

    // Gets or sets the Manager reference.
    private DataMapTableManager MapTableManager { get; set; }
    #endregion
  }
}
