// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// JoinGridClass.cs
using LJCNetCommon;
using LJCWinFormCommon;
using LJCWinFormControls;
using LJCDBViewDAL;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using LJCDBMessage;
using static LJCViewEditor.ViewEditorList;

namespace LJCViewEditor
{
  // Provides JoinGrid methods for the ViewEditorList window.
  internal class JoinGridCode
  {
    #region Constructors

    // Initializes an object instance.
    internal JoinGridCode(ViewEditorList parentList)
    {
      parentList.Cursor = Cursors.WaitCursor;
      EditList = parentList;
      JoinGrid = EditList.JoinGrid;
      ViewGrid = EditList.ViewGrid;
      ResetData();
      EditList.Cursor = Cursors.Default;
    }

    // Resets the DataConfig dependent objects.
    internal void ResetData()
    {
      Managers = EditList.Managers;
      mViewJoinManager = Managers.ViewJoinManager;
    }
    #endregion

    #region Data Methods

    // Retrieves the list rows.
    internal void DataRetrieve()
    {
      EditList.Cursor = Cursors.WaitCursor;
      JoinGrid.Rows.Clear();
      EditList.JoinOnGrid.Rows.Clear();
      EditList.JoinColumnGrid.Rows.Clear();

      SetupGrid();
      if (ViewGrid.CurrentRow is LJCGridRow parentRow)
      {
        // Data from items.
        int parentID = parentRow.LJCGetInt32(ViewData.ColumnID);

        var manager = mViewJoinManager;
        var result = manager.ResultWithParentID(parentID);
        if (DbResult.HasRows(result))
        {
          foreach (var dbRow in result.Rows)
          {
            RowAddValues(dbRow.Values);
          }
        }
      }
      EditList.Cursor = Cursors.Default;
      EditList.DoChange(Change.Join);
    }

    // Adds a grid row and updates it with the record values.
    private LJCGridRow RowAdd(ViewJoin dataRecord)
    {
      var retValue = JoinGrid.LJCRowAdd();
      SetStoredValues(retValue, dataRecord);
      retValue.LJCSetValues(JoinGrid, dataRecord);
      return retValue;
    }

    // Adds a grid row and updates it with the result values.
    private LJCGridRow RowAddValues(DbValues dbValues)
    {
      var ljcGrid = JoinGrid;
      var retValue = ljcGrid.LJCRowAdd();

      var columnName = ViewJoin.ColumnID;
      var id = dbValues.LJCGetInt32(columnName);
      retValue.LJCSetInt32(columnName, id);

      columnName = ViewJoin.ColumnTableName;
      var name = dbValues.LJCGetString(columnName);
      retValue.LJCSetString(columnName, name);

      retValue.LJCSetValues(ljcGrid, dbValues);
      return retValue;
    }

    // Updates the current row with the record values.
    private void RowUpdate(ViewJoin dataRecord)
    {
      if (JoinGrid.CurrentRow is LJCGridRow row)
      {
        SetStoredValues(row, dataRecord);
        row.LJCSetValues(JoinGrid, dataRecord);
      }
    }

    // Sets the row stored values.
    private void SetStoredValues(LJCGridRow row, ViewJoin dataRecord)
    {
      row.LJCSetInt32(ViewJoin.ColumnID, dataRecord.ID);
      row.LJCSetString(ViewJoin.ColumnTableName, dataRecord.JoinTableName);
    }

    // Selects a row based on the key record values.
    private bool RowSelect(ViewJoin record)
    {
      bool retValue = false;
      if (record != null)
      {
        EditList.Cursor = Cursors.WaitCursor;
        foreach (LJCGridRow row in JoinGrid.Rows)
        {
          var rowID = row.LJCGetInt32(ViewJoin.ColumnID);
          if (rowID == record.ID)
          {
            // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
            JoinGrid.LJCSetCurrentRow(row, true);
            retValue = true;
            break;
          }
        }
        EditList.Cursor = Cursors.Default;
      }
      return retValue;
    }
    #endregion

    #region Action Methods

    // Displays a detail dialog for a new record.
    internal void DoNew()
    {
      if (ViewGrid.CurrentRow is LJCGridRow parentRow)
      {
        // Data from list items.
        int parentID = parentRow.LJCGetInt32(ViewData.ColumnID);
        string parentName = parentRow.LJCGetString(ViewData.ColumnName);

        var grid = JoinGrid;
        var location = FormCommon.GetDialogScreenPoint(grid);
        var detail = new ViewJoinDetail
        {
          LJCLocation = location,
          LJCParentID = parentID,
          LJCParentName = parentName
        };
        detail.LJCChange += Detail_Change;
        detail.ShowDialog();
      }
    }

    // Displays a detail dialog to edit an existing record.
    internal void DoEdit()
    {
      if (ViewGrid.CurrentRow is LJCGridRow parentRow
        && JoinGrid.CurrentRow is LJCGridRow row)
      {
        // Data from list items.
        int id = row.LJCGetInt32(ViewJoin.ColumnID);
        int parentID = parentRow.LJCGetInt32(ViewData.ColumnID);
        string parentName = parentRow.LJCGetString(ViewData.ColumnName);

        var grid = JoinGrid;
        var location = FormCommon.GetDialogScreenPoint(grid);
        var detail = new ViewJoinDetail()
        {
          LJCLocation = location,
          LJCID = id,
          LJCParentID = parentID,
          LJCParentName = parentName
        };
        detail.LJCChange += Detail_Change;
        detail.ShowDialog();
      }
    }

    // Deletes the selected row.
    internal void DoDelete()
    {
      if (JoinGrid.CurrentRow is LJCGridRow row)
      {
        bool success = false;
        var title = "Delete Confirmation";
        var message = FormCommon.DeleteConfirm;
        if (MessageBox.Show(message, title, MessageBoxButtons.YesNo
          , MessageBoxIcon.Question) == DialogResult.Yes)
        {
          success = true;
        }

        if (success)
        {
          // Data from items.
          var id = row.LJCGetInt32(ViewJoin.ColumnID);

          var keyColumns = new DbColumns()
          {
            { ViewJoin.ColumnID, id }
          };
          mViewJoinManager.Delete(keyColumns);
          if (mViewJoinManager.AffectedCount < 1)
          {
            success = false;
            message = FormCommon.DeleteError;
            MessageBox.Show(message, "Delete Error", MessageBoxButtons.OK
              , MessageBoxIcon.Exclamation);
          }
        }

        if (success)
        {
          JoinGrid.Rows.Remove(row);
          EditList.TimedChange(Change.Join);
        }
      }
    }

    // Refreshes the list.
    internal void DoRefresh()
    {
      EditList.Cursor = Cursors.WaitCursor;
      int id = 0;
      if (JoinGrid.CurrentRow is LJCGridRow row)
      {
        // Save the original row.
        id = row.LJCGetInt32(ViewJoin.ColumnID);
      }
      DataRetrieve();

      // Select the original row.
      if (id > 0)
      {
        var record = new ViewJoin()
        {
          ID = id
        };
        RowSelect(record);
      }
      EditList.Cursor = Cursors.Default;
    }

    // Adds new row or updates existing row with changes from the detail dialog.
    private void Detail_Change(object sender, EventArgs e)
    {
      var detail = sender as ViewJoinDetail;
      var record = detail.LJCRecord;
      if (detail.LJCIsUpdate)
      {
        RowUpdate(record);
      }
      else
      {
        // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
        var row = RowAdd(record);
        JoinGrid.LJCSetCurrentRow(row, true);
        EditList.TimedChange(Change.Join);
      }
    }
    #endregion

    #region Setup Methods

    // Configures the ViewJoin Grid.
    private void SetupGrid()
    {
      if (0 == JoinGrid.Columns.Count)
      {
        List<string> propertyNames = new List<string> {
          ViewJoin.PropertyTableName,
          ViewJoin.ColumnJoinType,
          ViewJoin.ColumnTableAlias
        };

        // Get the grid columns from the manager Data Definition.
        var gridColumns
          = mViewJoinManager.GetColumns(propertyNames);

        // Setup the grid columns.
        JoinGrid.LJCAddColumns(gridColumns);
      }
    }
    #endregion

    #region Properties

    // Gets or sets the Parent List reference.
    private ViewEditorList EditList { get; set; }

    // Gets or sets the JoinGrid reference.
    private LJCDataGrid JoinGrid { get; set; }

    // Gets or sets the Managers reference.
    internal ManagersDbView Managers { get; set; }

    // Gets or sets the ViewGrid reference.
    private LJCDataGrid ViewGrid { get; set; }
    #endregion

    #region Class Data

    private ViewJoinManager mViewJoinManager;
    #endregion
  }
}
