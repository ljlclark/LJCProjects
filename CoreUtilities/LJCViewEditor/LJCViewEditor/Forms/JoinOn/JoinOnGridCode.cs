// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// JoinOnGridClass.cs
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
  // Provides JoinOnGrid methods for the ViewEditorList window.
  internal class JoinOnGridCode
  {
    #region Constructors

    // Initializes an object instance.
    internal JoinOnGridCode(ViewEditorList parentList)
    {
      parentList.Cursor = Cursors.WaitCursor;
      EditList = parentList;
      JoinGrid = EditList.JoinGrid;
      JoinOnGrid = EditList.JoinOnGrid;
      ResetData();
      EditList.Cursor = Cursors.Default;
    }

    // Resets the DataConfig dependent objects.
    internal void ResetData()
    {
      Managers = EditList.Managers;
      ViewJoinOnManager = Managers.ViewJoinOnManager;
    }
    #endregion

    #region Data Methods

    // Retrieves the list rows.
    internal void DataRetrieve()
    {
      EditList.Cursor = Cursors.WaitCursor;
      JoinOnGrid.Rows.Clear();

      SetupGrid();
      if (JoinGrid.CurrentRow is LJCGridRow parentRow)
      {
        // Data from items.
        int parentID = parentRow.LJCGetInt32(ViewJoin.ColumnID);

        var manager = ViewJoinOnManager;
        var result = manager.ResultWithParentID(parentID);
        if (DbResult.HasRows(result))
        {
          foreach (DbRow dbRow in result.Rows)
          {
            RowAddValues(dbRow.Values);
          }
        }
      }
      EditList.Cursor = Cursors.Default;
      EditList.DoChange(Change.JoinOn);
    }

    // Adds a grid row and updates it with the record values.
    private LJCGridRow RowAdd(ViewJoinOn dataRecord)
    {
      var retValue = JoinOnGrid.LJCRowAdd();
      SetStoredValues(retValue, dataRecord);
      retValue.LJCSetValues(JoinOnGrid, dataRecord);
      return retValue;
    }

    // Adds a grid row and updates it with the result values.
    private LJCGridRow RowAddValues(DbValues dbValues)
    {
      var ljcGrid = JoinOnGrid;
      var retValue = ljcGrid.LJCRowAdd();

      var columnName = ViewJoinOn.ColumnID;
      var id = dbValues.LJCGetInt32(columnName);
      retValue.LJCSetInt32(columnName, id);

      retValue.LJCSetValues(ljcGrid, dbValues);
      return retValue;
    }

    // Selects a row based on the key record values.
    private bool RowSelect(ViewJoinOn dataRecord)
    {
      bool retValue = false;

      if (dataRecord != null)
      {
        EditList.Cursor = Cursors.WaitCursor;
        foreach (LJCGridRow row in JoinOnGrid.Rows)
        {
          var rowID = row.LJCGetInt32(ViewJoinOn.ColumnID);
          if (rowID == dataRecord.ID)
          {
            JoinOnGrid.LJCSetCurrentRow(row, true);
            retValue = true;
            break;
          }
        }
        EditList.Cursor = Cursors.Default;
      }
      return retValue;
    }

    // Updates the current row with the record values.
    private void RowUpdate(ViewJoinOn dataRecord)
    {
      if (JoinOnGrid.CurrentRow is LJCGridRow row)
      {
        SetStoredValues(row, dataRecord);
        row.LJCSetValues(JoinOnGrid, dataRecord);
      }
    }

    // Sets the row stored values.
    private void SetStoredValues(LJCGridRow row, ViewJoinOn dataRecord)
    {
      row.LJCSetInt32(ViewJoinOn.ColumnID, dataRecord.ID);
    }
    #endregion

    #region Action Methods

    // Displays a detail dialog for a new record.
    internal void DoNew()
    {
      if (JoinGrid.CurrentRow is LJCGridRow parentRow)
      {
        // Data from list items.
        int parentID = parentRow.LJCGetInt32(ViewJoin.ColumnID);
        string parentName = parentRow.LJCGetString(ViewJoin.ColumnTableName);

        var grid = JoinOnGrid;
        var location = FormCommon.GetDialogScreenPoint(grid);
        var detail = new ViewJoinOnDetail
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
      if (JoinGrid.CurrentRow is LJCGridRow parentRow
        && JoinOnGrid.CurrentRow is LJCGridRow row)
      {
        // Data from list items.
        int id = row.LJCGetInt32(ViewJoinOn.ColumnID);
        int parentID = parentRow.LJCGetInt32(ViewJoin.ColumnID);
        string parentName = parentRow.LJCGetString(ViewJoin.ColumnTableName);

        var grid = JoinOnGrid;
        var location = FormCommon.GetDialogScreenPoint(grid);
        var detail = new ViewJoinOnDetail()
        {
          LJCID = id,
          LJCLocation = location,
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
      bool success = false;
      var row = JoinOnGrid.CurrentRow as LJCGridRow;
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
        var keyColumns = new DbColumns()
        {
          { ViewJoinOn.ColumnID, row.LJCGetInt32(ViewJoinOn.ColumnID) }
        };
        ViewJoinOnManager.Delete(keyColumns);
        if (0 == ViewJoinOnManager.AffectedCount)
        {
          success = false;
          var message = FormCommon.DeleteError;
          MessageBox.Show(message, "Delete Error", MessageBoxButtons.OK
            , MessageBoxIcon.Exclamation);
        }
      }

      if (success)
      {
        JoinOnGrid.Rows.Remove(row);
        EditList.TimedChange(Change.JoinOn);
      }
    }

    // Refreshes the list.
    internal void DoRefresh()
    {
      EditList.Cursor = Cursors.WaitCursor;
      int id = 0;
      if (JoinOnGrid.CurrentRow is LJCGridRow row)
      {
        // Save the original row.
        id = row.LJCGetInt32(ViewJoinOn.ColumnID);
      }
      DataRetrieve();

      // Select the original row.
      if (id > 0)
      {
        var record = new ViewJoinOn()
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
      var detail = sender as ViewJoinOnDetail;
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
          JoinOnGrid.LJCSetCurrentRow(row, true);
          EditList.TimedChange(Change.JoinOn);
        }
      }
    }
    #endregion

    #region Other Methods

    // Configures the View JoinOn Grid.
    private void SetupGrid()
    {
      if (0 == JoinOnGrid.Columns.Count)
      {
        List<string> propertyNames = new List<string> {
          ViewJoinOn.ColumnFromColumnName,
          ViewJoinOn.ColumnJoinOnOperator,
          ViewJoinOn.ColumnToColumnName
        };

        // Get the grid columns from the manager Data Definition.
        var manager = ViewJoinOnManager;
        var gridColumns = manager.GetColumns(propertyNames);

        // Setup the grid columns.
        JoinOnGrid.LJCAddColumns(gridColumns);
      }
    }
    #endregion

    #region Properties

    internal ManagersDbView Managers { get; set; }

    // Gets or sets the Parent List reference.
    private ViewEditorList EditList { get; set; }

    // Gets or sets the JoinGrid reference.
    private LJCDataGrid JoinGrid { get; set; }

    // Gets or sets the JoinOnGrid reference.
    private LJCDataGrid JoinOnGrid { get; set; }

    // Gets or sets the Manager reference.
    private ViewJoinOnManager ViewJoinOnManager { get; set; }
    #endregion
  }
}
