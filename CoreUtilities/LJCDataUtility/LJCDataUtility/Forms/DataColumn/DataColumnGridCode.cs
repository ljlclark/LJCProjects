﻿// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DataColumnGridCode.cs
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
  // Provides DataColumnGrid methods for the _AppName_List window.
  internal class DataColumnGridCode
  {
    #region Constructors

    // Initializes an object instance.
    // ********************
    internal DataColumnGridCode(DataUtilityList parentList)
    {
      // Initialize property values.
      UtilityList = parentList;
      UtilityList.Cursor = Cursors.WaitCursor;

      TableGrid = UtilityList.TableGrid;
      ColumnGrid = UtilityList.ColumnGrid;
      var columnMenu = UtilityList.ColumnMenu;
      Managers = UtilityList.Managers;
      ColumnManager = Managers.DataColumnManager;

      var fontFamily = UtilityList.Font.FontFamily;
      var style = UtilityList.Font.Style;
      ColumnGrid.Font = new Font(fontFamily, 11, style);
      columnMenu.Font = new Font(fontFamily, 11, style);
      _ = new GridFont(UtilityList, ColumnGrid);
      _ = new MenuFont(columnMenu);

      // Menu item events.
      var list = UtilityList;
      list.ColumnNew.Click += ColumnNew_Click;
      list.ColumnEdit.Click += ColumnEdit_Click;
      list.ColumnDelete.Click += ColumnDelete_Click;
      list.ColumnRefresh.Click += ColumnRefresh_Click;

      // Grid events.
      var grid = ColumnGrid;
      grid.DoubleClick += ColumnGrid_DoubleClick;
      grid.KeyDown += ColumnGrid_KeyDown;
      UtilityList.Cursor = Cursors.Default;
    }
    #endregion

    #region Data Methods

    // Retrieves the list rows.
    // ********************
    internal void DataRetrieve()
    {
      UtilityList.Cursor = Cursors.WaitCursor;
      ColumnGrid.LJCRowsClear();

      if (TableGrid.CurrentRow is LJCGridRow parentRow)
      {
        // Data from items.
        int parentID = parentRow.LJCGetInt32(DataTable.ColumnID);

        var keyColumns = ColumnManager.GetParentKey(parentID);
        var items = ColumnManager.Load(keyColumns);
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
    private LJCGridRow RowAdd(DataColumn dataRecord)
    {
      var retValue = ColumnGrid.LJCRowAdd();
      SetStoredValues(retValue, dataRecord);
      retValue.LJCSetValues(ColumnGrid, dataRecord);
      return retValue;
    }

    // Selects a row based on the key record values.
    // ********************
    private bool RowSelect(DataColumn dataRecord)
    {
      bool retValue = false;

      if (dataRecord != null)
      {
        UtilityList.Cursor = Cursors.WaitCursor;
        foreach (LJCGridRow row in ColumnGrid.Rows)
        {
          var rowID = row.LJCGetInt32(DataColumn.ColumnID);
          if (rowID == dataRecord.ID)
          {
            // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
            ColumnGrid.LJCSetCurrentRow(row, true);
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
    private void RowUpdate(DataColumn dataRecord)
    {
      if (ColumnGrid.CurrentRow is LJCGridRow row)
      {
        SetStoredValues(row, dataRecord);
        row.LJCSetValues(ColumnGrid, dataRecord);
      }
    }

    // Sets the row stored values.
    // ********************
    private void SetStoredValues(LJCGridRow row, DataColumn dataRecord)
    {
      row.LJCSetInt32(DataColumn.ColumnID, dataRecord.ID);
    }
    #endregion

    #region Action Methods

    // Deletes the selected row.
    // ********************
    internal void Delete()
    {
      bool success = false;
      var row = ColumnGrid.CurrentRow as LJCGridRow;
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
        var id = row.LJCGetInt32(DataColumn.ColumnID);

        var keyColumns = new DbColumns()
        {
          { DataColumn.ColumnID, id }
        };
        //ColumnManager.Delete(keyColumns);
        if (0 == ColumnManager.AffectedCount)
        {
          success = false;
          var message = FormCommon.DeleteError;
          MessageBox.Show(message, "Delete Error", MessageBoxButtons.OK
            , MessageBoxIcon.Exclamation);
        }
      }

      if (success)
      {
        ColumnGrid.Rows.Remove(row);
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
      if (TableGrid.CurrentRow is LJCGridRow parentRow)
      {
        // Data from list items.
        int parentID = parentRow.LJCGetInt32(DataTable.ColumnID);
        string parentName = parentRow.LJCGetString(DataTable.ColumnName);

        var location = FormCommon.GetDialogScreenPoint(TableGrid);
        var detail = new DataColumnDetail
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
      if (ColumnGrid.CurrentRow is LJCGridRow row)
      {
        // Save the original row.
        id = row.LJCGetInt32(DataColumn.ColumnID);
      }
      DataRetrieve();

      // Select the original row.
      if (id > 0)
      {
        var record = new DataColumn()
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
      if (0 == ColumnGrid.Columns.Count)
      {
        List<string> propertyNames = new List<string>()
        {
          DataColumn.ColumnDataTableID,
          DataColumn.ColumnName,
          DataColumn.ColumnDescription,
          DataColumn.ColumnSequence,
          DataColumn.ColumnTypeName,
          DataColumn.ColumnMaxLength
        };

        // Get the grid columns from the manager Data Definition.
        var gridColumns = ColumnManager.GetColumns(propertyNames);

        // Setup the grid columns.
        ColumnGrid.LJCAddColumns(gridColumns);
      }
    }
    #endregion

    #region Control Event Handlers

    // Handles the New menu item event.
    // ********************
    private void ColumnNew_Click(object sender, EventArgs e)
    {
      New();
    }

    // Handles the Edit menu item event.
    // ********************
    private void ColumnEdit_Click(object sender, EventArgs e)
    {
      Edit();
    }

    // Handles the Delete menu item event.
    // ********************
    private void ColumnDelete_Click(object sender, EventArgs e)
    {
      Delete();
    }

    // Handles the Refresh menu item event.
    // ********************
    private void ColumnRefresh_Click(object sender, EventArgs e)
    {
      Refresh();
    }

    // Handles the Grid Doubleclick event.
    // ********************
    private void ColumnGrid_DoubleClick(object sender, EventArgs e)
    {
      New();
    }

    // Handles the Grid KeyDown event.
    // ********************
    private void ColumnGrid_KeyDown(object sender, KeyEventArgs e)
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
            var position = FormCommon.GetMenuScreenPoint(ColumnGrid
              , Control.MousePosition);
            UtilityList.ColumnMenu.Show(position);
            UtilityList.ColumnMenu.Select();
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

    // Gets or sets the Grid reference.
    private LJCDataGrid ColumnGrid { get; set; }

    // Gets or sets the Manager reference.
    private DataColumnManager ColumnManager { get; set; }

    // Gets or sets the Managers reference.
    private ManagersDataUtility Managers { get; set; }

    // Gets or sets the parent Grid reference.
    private LJCDataGrid TableGrid { get; set; }

    // Gets or sets the parent List reference.
    private DataUtilityList UtilityList { get; set; }
    #endregion
  }
}
