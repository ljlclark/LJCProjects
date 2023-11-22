// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// JoinColumnGridClass.cs
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
  // Provides JoinColumnGrid methods for the ViewEditorList window.
  internal class JoinColumnGridClass
  {
    #region Constructors

    // Initializes an object instance.
    internal JoinColumnGridClass(ViewEditorList parentList)
    {
      parentList.Cursor = Cursors.WaitCursor;
      EditList = parentList;
      JoinColumnGrid = EditList.JoinColumnGrid;
      JoinGrid = EditList.JoinGrid;
      ResetData();
      EditList.Cursor = Cursors.Default;
    }

    // Resets the DataConfig dependent objects.
    internal void ResetData()
    {
      Managers = EditList.Managers;
      mViewJoinColumnManager = Managers.ViewJoinColumnManager;
    }
    #endregion

    #region Data Methods

    // Retrieves the list rows.
    internal void DataRetrieve()
    {
      EditList.Cursor = Cursors.WaitCursor;
      JoinColumnGrid.Rows.Clear();

      SetupGrid();
      if (JoinGrid.CurrentRow is LJCGridRow parentRow)
      {
        // Data from items.
        int parentID = parentRow.LJCGetInt32(ViewJoin.ColumnID);

        var manager = mViewJoinColumnManager;
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
      EditList.DoChange(Change.JoinColumn);
    }

    // Adds a grid row and updates it with the record values.
    private LJCGridRow RowAdd(ViewJoinColumn dataRecord)
    {
      LJCGridRow retValue;

      retValue = JoinColumnGrid.LJCRowAdd();
      SetStoredValues(retValue, dataRecord);
      retValue.LJCSetValues(JoinColumnGrid, dataRecord);
      return retValue;
    }

    // Adds a grid row and updates it with the result values.
    private LJCGridRow RowAddValues(DbValues dbValues)
    {
      var ljcGrid = JoinColumnGrid;
      var retValue = ljcGrid.LJCRowAdd();

      var columnName = ViewJoinColumn.ColumnID;
      var id = dbValues.LJCGetInt32(columnName);
      retValue.LJCSetInt32(columnName, id);

      retValue.LJCSetValues(ljcGrid, dbValues);
      return retValue;
    }

    // Updates the current row with the record values.
    private void RowUpdate(ViewJoinColumn dataRecord)
    {
      if (JoinColumnGrid.CurrentRow is LJCGridRow row)
      {
        SetStoredValues(row, dataRecord);
        row.LJCSetValues(JoinColumnGrid, dataRecord);
      }
    }

    // Sets the row stored values.
    private void SetStoredValues(LJCGridRow row
      , ViewJoinColumn dataRecord)
    {
      row.LJCSetInt32(ViewJoinColumn.ColumnID, dataRecord.ID);
    }

    // Selects a row based on the key record values.
    private bool RowSelect(ViewJoinColumn record)
    {
      bool retValue = false;
      if (record != null)
      {
        EditList.Cursor = Cursors.WaitCursor;
        foreach (LJCGridRow row in JoinColumnGrid.Rows)
        {
          var rowID = row.LJCGetInt32(ViewJoinColumn.ColumnID);
          if (rowID == record.ID)
          {
            // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
            JoinColumnGrid.LJCSetCurrentRow(row, true);
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
      if (JoinGrid.CurrentRow is LJCGridRow parentRow)
      {
        // Data from list items.
        int parentID = parentRow.LJCGetInt32(ViewJoin.ColumnID);
        string parentName = parentRow.LJCGetString(ViewJoin.ColumnTableName);

        var grid = JoinColumnGrid;
        var location = FormCommon.GetDialogScreenPoint(grid);
        var detail = new ViewJoinColumnDetail
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
        && JoinColumnGrid.CurrentRow is LJCGridRow row)
      {
        // Data from list items.
        int id = row.LJCGetInt32(ViewJoinColumn.ColumnID);
        int parentID = parentRow.LJCGetInt32(ViewJoin.ColumnID);
        string parentName = parentRow.LJCGetString(ViewJoin.ColumnTableName);

        var grid = JoinColumnGrid;
        var location = FormCommon.GetDialogScreenPoint(grid);
        var detail = new ViewJoinColumnDetail()
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
      if (JoinColumnGrid.CurrentRow is LJCGridRow row)
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
          var id = row.LJCGetInt32(ViewJoinColumn.ColumnID);

          var keyColumns = new DbColumns()
          {
            { ViewJoinColumn.ColumnID, id }
          };
          mViewJoinColumnManager.Delete(keyColumns);
          if (mViewJoinColumnManager.AffectedCount < 1)
          {
            success = false;
            message = FormCommon.DeleteError;
            MessageBox.Show(message, "Delete Error", MessageBoxButtons.OK
              , MessageBoxIcon.Exclamation);
          }
        }

        if (success)
        {
          JoinColumnGrid.Rows.Remove(row);
          EditList.TimedChange(Change.JoinColumn);
        }
      }
    }

    // Refreshes the list.
    internal void DoRefresh()
    {
      EditList.Cursor = Cursors.WaitCursor;
      int id = 0;
      if (JoinColumnGrid.CurrentRow is LJCGridRow row)
      {
        // Save the original row.
        id = row.LJCGetInt32(ViewJoinColumn.ColumnID);
      }
      DataRetrieve();

      // Select the original row.
      if (id > 0)
      {
        var record = new ViewJoinColumn()
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
      var detail = sender as ViewJoinColumnDetail;
      var record = detail.LJCRecord;
      if (detail.LJCIsUpdate)
      {
        RowUpdate(record);
      }
      else
      {
        // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
        var row = RowAdd(record);
        JoinColumnGrid.LJCSetCurrentRow(row, true);
        EditList.TimedChange(Change.JoinColumn);
      }
    }
    #endregion

    #region Setup Methods

    // Configures the ViewJoinColumn Grid.
    private void SetupGrid()
    {
      if (0 == JoinColumnGrid.Columns.Count)
      {
        List<string> propertyNames = new List<string> {
          ViewJoinColumn.ColumnColumnName,
          ViewJoinColumn.ColumnPropertyName,
          ViewJoinColumn.ColumnRenameAs
        };

        // Get the grid columns from the manager Data Definition.
        DbColumns gridColumns
          = mViewJoinColumnManager.GetColumns(propertyNames);

        // Setup the grid columns.
        JoinColumnGrid.LJCAddColumns(gridColumns);
      }
    }
    #endregion

    #region Properties

    internal ManagersDbView Managers { get; set; }

    // Gets or sets the Parent List reference.
    private ViewEditorList EditList { get; set; }

    // Gets or sets the JoinColumnGrid reference.
    private LJCDataGrid JoinColumnGrid { get; set; }

    // Gets or sets the JoinGrid reference.
    private LJCDataGrid JoinGrid { get; set; }
    #endregion

    #region Class Data

    private ViewJoinColumnManager mViewJoinColumnManager;
    #endregion
  }
}
