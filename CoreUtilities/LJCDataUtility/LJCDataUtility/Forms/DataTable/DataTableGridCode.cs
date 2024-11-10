// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DataTableGridCode.cs
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
  // Provides DataTableGrid methods for the _AppName_List window.
  internal class DataTableGridCode
  {
    #region Constructors

    // Initializes an object instance.
    // ********************
    internal DataTableGridCode(DataUtilityList parentList)
    {
      // Initialize property values.
      UtilityList = parentList;
      UtilityList.Cursor = Cursors.WaitCursor;

      ModuleGrid = UtilityList.ModuleGrid;
      TableGrid = UtilityList.TableGrid;
      var tableMenu = UtilityList.TableMenu;
      Managers = UtilityList.Managers;
      TableManager = Managers.DataTableManager;

      var fontFamily = UtilityList.Font.FontFamily;
      var style = UtilityList.Font.Style;
      TableGrid.Font = new Font(fontFamily, 11, style);
      tableMenu.Font = new Font(fontFamily, 11, style);
      _ = new GridFont(UtilityList, TableGrid);
      _ = new MenuFont(tableMenu);

      // Menu item events.
      var list = UtilityList;
      list.TableNew.Click += TableNew_Click;
      list.TableEdit.Click += TableEdit_Click;
      list.TableDelete.Click += TableDelete_Click;
      list.TableRefresh.Click += TableRefresh_Click;

      // Grid events.
      var grid = TableGrid;
      grid.MouseDoubleClick += TableGrid_MouseDoubleClick;
      grid.KeyDown += TableGrid_KeyDown;
      UtilityList.Cursor = Cursors.Default;
    }
    #endregion

    #region Data Methods

    // Retrieves the list rows.
    // ********************
    internal void DataRetrieve()
    {
      UtilityList.Cursor = Cursors.WaitCursor;
      TableGrid.LJCRowsClear();

      if (ModuleGrid.CurrentRow is LJCGridRow parentRow)
      {
        // Data from items.
        int parentID = parentRow.LJCGetInt32(DataModule.ColumnID);

        var keyColumns = TableManager.GetIDKey(parentID);
        var items = TableManager.Load(keyColumns);
        if (items != null
          && items.Count > 0)
        {
          foreach (var item in items)
          {
            RowAdd(item);
          }
        }
      }
      UtilityList.Cursor = Cursors.Default;
    }

    // Adds a grid row and updates it with the record values.
    // ********************
    private LJCGridRow RowAdd(DataTable dataRecord)
    {
      var retValue = TableGrid.LJCRowAdd();
      SetStoredValues(retValue, dataRecord);
      retValue.LJCSetValues(TableGrid, dataRecord);
      return retValue;
    }

    // Selects a row based on the key record values.
    // ********************
    private bool RowSelect(DataTable dataRecord)
    {
      bool retValue = false;

      if (dataRecord != null)
      {
        UtilityList.Cursor = Cursors.WaitCursor;
        foreach (LJCGridRow row in TableGrid.Rows)
        {
          var rowID = row.LJCGetInt32(DataTable.ColumnID);
          if (rowID == dataRecord.ID)
          {
            // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
            TableGrid.LJCSetCurrentRow(row, true);
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
    private void RowUpdate(DataTable dataRecord)
    {
      if (TableGrid.CurrentRow is LJCGridRow row)
      {
        SetStoredValues(row, dataRecord);
        row.LJCSetValues(TableGrid, dataRecord);
      }
    }

    // Sets the row stored values.
    // ********************
    private void SetStoredValues(LJCGridRow row, DataTable dataRecord)
    {
      row.LJCSetInt32(DataTable.ColumnID, dataRecord.ID);
    }
    #endregion

    #region Action Methods

    // Deletes the selected row.
    // ********************
    internal void Delete()
    {
      bool success = false;
      var row = TableGrid.CurrentRow as LJCGridRow;
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
        var id = row.LJCGetInt32(DataTable.ColumnID);

        var keyColumns = new DbColumns()
        {
          { DataTable.ColumnID, id }
        };
        TableManager.Delete(keyColumns);
        if (0 == TableManager.AffectedCount)
        {
          success = false;
          var message = FormCommon.DeleteError;
          MessageBox.Show(message, "Delete Error", MessageBoxButtons.OK
            , MessageBoxIcon.Exclamation);
        }
      }

      if (success)
      {
        TableGrid.Rows.Remove(row);
        //UtilityList.TimedChange(Change._ClassName_);
      }
    }

    // Displays a detail dialog to edit a record.
    // ********************
    internal void Edit()
    {
    }

    // Displays a detail dialog for a new record.
    // ********************
    internal void New()
    {
      if (ModuleGrid.CurrentRow is LJCGridRow parentRow)
      {
        // Data from list items.
        int parentID = parentRow.LJCGetInt32(DataModule.ColumnID);
        string parentName = parentRow.LJCGetString(DataModule.ColumnName);

        var location = FormCommon.GetDialogScreenPoint(TableGrid);
        var detail = new DataTableDetail
        {
          //LJCLocation = location,
          //LJCManagers = Managers,
          //LJCParentID = parentID,
          //LJCParentName = parentName
        };
        //detail.LJCChange += Detail_Change;
        detail.ShowDialog();
      }
    }

    // Refreshes the list.
    // ********************
    internal void Refresh()
    {
      UtilityList.Cursor = Cursors.WaitCursor;
      int id = 0;
      if (TableGrid.CurrentRow is LJCGridRow row)
      {
        // Save the original row.
        id = row.LJCGetInt32(DataTable.ColumnID);
      }
      DataRetrieve();

      // Select the original row.
      if (id > 0)
      {
        var record = new DataTable()
        {
          ID = id
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
    }
    #endregion

    #region Setup and Other Methods

    // Configures the Grid.
    internal void SetupGrid()
    {
      // Setup default grid columns if no columns are defined.
      if (0 == TableGrid.Columns.Count)
      {
        List<string> propertyNames = new List<string>()
        {
          DataTable.ColumnDataModuleID,
          DataTable.ColumnName,
          DataTable.ColumnDescription
        };

        // Get the grid columns from the manager Data Definition.
        var gridColumns = TableManager.GetColumns(propertyNames);

        // Setup the grid columns.
        TableGrid.LJCAddColumns(gridColumns);
      }
    }
    #endregion

    #region Control Event Handlers

    // Handles the New menu item event.
    // ********************
    private void TableNew_Click(object sender, EventArgs e)
    {
      New();
    }

    // Handles the Edit menu item event.
    // ********************
    private void TableEdit_Click(object sender, EventArgs e)
    {
      Edit();
    }

    // Handles the Delete menu item event.
    // ********************
    private void TableDelete_Click(object sender, EventArgs e)
    {
      Delete();
    }

    // Handles the Refresh menu item event.
    // ********************
    private void TableRefresh_Click(object sender, EventArgs e)
    {
      Refresh();
    }

    // Handles the Grid Doubleclick event.
    // ********************
    private void TableGrid_MouseDoubleClick(object sender, MouseEventArgs e)
    {
      if (TableGrid.LJCGetMouseRow(e) != null)
      {
        New();
      }
    }

    // Handles the Grid KeyDown event.
    // ********************
    private void TableGrid_KeyDown(object sender, KeyEventArgs e)
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

        //case Keys.M:
        //  if (e.Control)
        //  {
        //    var position = FormCommon.GetMenuScreenPoint(TableGrid
        //      , Control.MousePosition);
        //    UtilityList.TableMenu.Show(position);
        //    UtilityList.TableMenu.Select();
        //    e.Handled = true;
        //  }
        //  break;

        case Keys.Tab:
          if (e.Shift)
          {
            UtilityList.ljcTabControl1.Select();
          }
          else
          {
            UtilityList.ljcTabControl1.Select();
          }
          e.Handled = true;
          break;
      }
    }
    #endregion

    #region Properties

    // Gets or sets the Managers reference.
    private ManagersDataUtility Managers { get; set; }

    // Gets or sets the parent Grid reference.
    private LJCDataGrid ModuleGrid { get; set; }

    // Gets or sets the Grid reference.
    private LJCDataGrid TableGrid { get; set; }

    // Gets or sets the Manager reference.
    private DataTableManager TableManager { get; set; }

    // Gets or sets the Parent List reference.
    private DataUtilityList UtilityList { get; set; }
    #endregion
  }
}
