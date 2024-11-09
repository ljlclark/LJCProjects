// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DataKeyGridCode.cs
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
  // Provides DataKeyGrid methods for the _AppName_List window.
  internal class DataKeyGridCode
  {
    #region Constructors

    // Initializes an object instance.
    // ********************
    internal DataKeyGridCode(DataUtilityList parentList)
    {
      // Initialize property values.
      UtilityList = parentList;
      UtilityList.Cursor = Cursors.WaitCursor;
      TableGrid = UtilityList.TableGrid;
      KeyGrid = UtilityList.KeyGrid;
      Managers = UtilityList.Managers;
      KeyManager = Managers.DataKeyManager;

      var fontFamily = UtilityList.Font.FontFamily;
      var style = UtilityList.Font.Style;
      KeyGrid.Font = new Font(fontFamily, 12, style);
      _ = new GridFont(UtilityList, KeyGrid);

      // Menu item events.
      var list = UtilityList;
      list.KeyNew.Click += KeyNew_Click;
      list.KeyEdit.Click += KeyEdit_Click;
      list.KeyDelete.Click += KeyDelete_Click;
      list.KeyRefresh.Click += KeyRefresh_Click;

      // Grid events.
      var grid = KeyGrid;
      grid.KeyDown += KeyGrid_KeyDown;
      grid.DoubleClick += KeyGrid_DoubleClick;
      UtilityList.Cursor = Cursors.Default;
    }
    #endregion

    #region Data Methods

    // Retrieves the list rows.
    // ********************
    internal void DataRetrieve()
    {
      UtilityList.Cursor = Cursors.WaitCursor;
      KeyGrid.LJCRowsClear();

      if (TableGrid.CurrentRow is LJCGridRow parentRow)
      {
        // Data from items.
        int parentID = parentRow.LJCGetInt32(DataTable.ColumnID);

        var keyColumns = KeyManager.GetIDKey(parentID);
        var items = KeyManager.Load(keyColumns);
        if (NetCommon.HasItems(items))
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
    private LJCGridRow RowAdd(DataKey dataRecord)
    {
      var retValue = KeyGrid.LJCRowAdd();
      SetStoredValues(retValue, dataRecord);
      retValue.LJCSetValues(KeyGrid, dataRecord);
      return retValue;
    }

    // Selects a row based on the key record values.
    // ********************
    private bool RowSelect(DataKey dataRecord)
    {
      bool retValue = false;

      if (dataRecord != null)
      {
        UtilityList.Cursor = Cursors.WaitCursor;
        foreach (LJCGridRow row in KeyGrid.Rows)
        {
          var rowID = row.LJCGetInt32(DataKey.ColumnID);
          if (rowID == dataRecord.ID)
          {
            // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
            KeyGrid.LJCSetCurrentRow(row, true);
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
    //private void RowUpdate(DataKey dataRecord)
    //{
    //  if (KeyGrid.CurrentRow is LJCGridRow row)
    //  {
    //    SetStoredValues(row, dataRecord);
    //    row.LJCSetValues(KeyGrid, dataRecord);
    //  }
    //}

    // Sets the row stored values.
    // ********************
    private void SetStoredValues(LJCGridRow row, DataKey dataRecord)
    {
      row.LJCSetInt32(DataKey.ColumnID, dataRecord.ID);
    }
    #endregion

    #region Action Methods

    // Deletes the selected row.
    // ********************
    internal void Delete()
    {
      bool success = false;
      var row = KeyGrid.CurrentRow as LJCGridRow;
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
        //KeyManager.Delete(keyColumns);
        if (0 == KeyManager.AffectedCount)
        {
          success = false;
          var message = FormCommon.DeleteError;
          MessageBox.Show(message, "Delete Error", MessageBoxButtons.OK
            , MessageBoxIcon.Exclamation);
        }
      }

      if (success)
      {
        KeyGrid.Rows.Remove(row);
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
    }

    // Refreshes the list.
    // ********************
    internal void Refresh()
    {
      UtilityList.Cursor = Cursors.WaitCursor;
      int id = 0;
      if (KeyGrid.CurrentRow is LJCGridRow row)
      {
        // Save the original row.
        id = row.LJCGetInt32(DataKey.ColumnID);
      }
      DataRetrieve();

      // Select the original row.
      if (id > 0)
      {
        var record = new DataKey()
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
    // ********************
    internal void SetupGrid()
    {
      // Setup default grid columns if no columns are defined.
      if (0 == KeyGrid.Columns.Count)
      {
        List<string> propertyNames = new List<string>()
        {
          DataKey.ColumnDataTableID,
          DataKey.ColumnName,
          DataKey.ColumnKeyType,
          DataKey.ColumnSourceColumnName,
          DataKey.ColumnTargetTableName,
          DataKey.ColumnTargetColumnName
        };

        // Get the grid columns from the manager Data Definition.
        var gridColumns = KeyManager.GetColumns(propertyNames);

        // Setup the grid columns.
        KeyGrid.LJCAddColumns(gridColumns);
      }
    }
    #endregion

    #region Control Event Handlers

    // ********************
    private void KeyNew_Click(object sender, EventArgs e)
    {
      New();
    }

    // ********************
    private void KeyEdit_Click(object sender, EventArgs e)
    {
      Edit();
    }

    // ********************
    private void KeyDelete_Click(object sender, EventArgs e)
    {
      Delete();
    }

    // ********************
    private void KeyRefresh_Click(object sender, EventArgs e)
    {
      Refresh();
    }

    // ********************
    private void KeyGrid_DoubleClick(object sender, EventArgs e)
    {
      New();
    }

    // Handles the Grid KeyDown event.
    // ********************
    private void KeyGrid_KeyDown(object sender, KeyEventArgs e)
    {
      switch (e.KeyCode)
      {
        case Keys.Enter:
          //Edit();
          e.Handled = true;
          break;

        case Keys.F1:
          //ShowHelp();
          e.Handled = true;
          break;

        case Keys.F5:
          Refresh();
          e.Handled = true;
          break;

        case Keys.M:
          if (e.Control)
          {
            var position = FormCommon.GetMenuScreenPoint(KeyGrid
              , Control.MousePosition);
            UtilityList.KeyMenu.Show(position);
            UtilityList.KeyMenu.Select();
            e.Handled = true;
          }
          break;

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

    // Gets or sets the Manager reference.
    private DataKeyManager KeyManager { get; set; }

    // Gets or sets the Grid reference.
    private LJCDataGrid KeyGrid { get; set; }

    // Gets or sets the Managers reference.
    private ManagersDataUtility Managers { get; set; }

    // Gets or sets the parent Grid reference.
    private LJCDataGrid TableGrid { get; set; }

    // Gets or sets the Parent List reference.
    private DataUtilityList UtilityList { get; set; }
    #endregion
  }
}
