// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ColumnGridClass.cs
using LJCDBMessage;
using LJCDBViewDAL;
using LJCNetCommon;
using LJCViewEditorDAL;
using LJCWinFormCommon;
using LJCWinFormControls;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using static LJCViewEditor.ViewEditorList;

namespace LJCViewEditor
{
  // Provides ColumnGrid methods for the ViewEditorList window.
  internal class ColumnGridCode
  {
    #region Constructors

    // Initializes an object instance.
    internal ColumnGridCode(ViewEditorList parentList)
    {
      parentList.Cursor = Cursors.WaitCursor;
      EditList = parentList;
      ColumnGrid = EditList.ColumnGrid;
      ViewGrid = EditList.ViewGrid;
      ResetData();
      EditList.Cursor = Cursors.Default;
    }

    // Resets the DataConfig dependent objects.
    internal void ResetData()
    {
      Managers = EditList.Managers;
      mColumnManager = Managers.ViewColumnManager;
    }
    #endregion

    #region Data Methods

    // Retrieves the list rows.
    internal void DataRetrieve()
    {
      EditList.Cursor = Cursors.WaitCursor;
      ColumnGrid.LJCRowsClear();

      SetupGrid();
      if (ViewGrid.CurrentRow is LJCGridRow parentRow)
      {
        // Data from items.
        int parentID = parentRow.LJCGetInt32(ViewData.ColumnID);

        var result = mColumnManager.ResultWithParentID(parentID);
        if (DbResult.HasRows(result))
        {
          foreach (var dbRow in result.Rows)
          {
            RowAddValues(dbRow.Values);
          }
        }
      }
      EditList.Cursor = Cursors.Default;
      EditList.DoChange(Change.Column);
    }

    // Adds a grid row and updates it with the record values.
    private LJCGridRow RowAdd(ViewColumn dataRecord)
    {
      var retValue = ColumnGrid.LJCRowAdd();
      SetStoredValues(retValue, dataRecord);
      retValue.LJCSetValues(ColumnGrid, dataRecord);
      return retValue;
    }

    // Adds a grid row and updates it with the result values.
    private LJCGridRow RowAddValues(DbValues dbValues)
    {
      var ljcGrid = ColumnGrid;
      var retValue = ljcGrid.LJCRowAdd();

      var columnName = ViewColumn.ColumnID;
      var id = dbValues.LJCGetInt32(columnName);
      retValue.LJCSetInt32(columnName, id);

      retValue.LJCSetValues(ljcGrid, dbValues);
      return retValue;
    }

    // Updates the current row with the record values.
    private void RowUpdate(ViewColumn dataRecord)
    {
      if (ColumnGrid.CurrentRow is LJCGridRow row)
      {
        SetStoredValues(row, dataRecord);
        row.LJCSetValues(ColumnGrid, dataRecord);
      }
    }

    // Sets the row stored values.
    private void SetStoredValues(LJCGridRow row, ViewColumn dataRecord)
    {
      row.LJCSetInt32(ViewColumn.ColumnID, dataRecord.ID);
    }

    // Selects a row based on the key record values.
    private bool RowSelect(ViewColumn dataRecord)
    {
      bool retValue = false;
      if (dataRecord != null)
      {
        EditList.Cursor = Cursors.WaitCursor;
        foreach (LJCGridRow row in ColumnGrid.Rows)
        {
          var rowID = row.LJCGetInt32(ViewColumn.ColumnID);
          if (rowID == dataRecord.ID)
          {
            // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
            ColumnGrid.LJCSetCurrentRow(row, true);
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

    // Adds all missing columns.
    internal void DoAddAll(string tableName)
    {
      if (ViewGrid.CurrentRow is LJCGridRow parentRow)
      {
        // Data from list items.
        int parentID = parentRow.LJCGetInt32(ViewData.ColumnID);

        var dataDbView = new DataDbView(Managers);
        var dataHelper = new DataHelper(Managers.DbServiceRef
          , Managers.DataConfigName);
        var tableColumns = dataHelper.GetTableColumns(tableName);
        foreach (var dbColumn in tableColumns)
        {
          var viewColumn = dataDbView.GetViewColumnFromDbColumn(dbColumn);
          viewColumn.ViewDataID = parentID;

          var keyColumns = new DbColumns()
          {
            { ViewColumn.ColumnViewDataID, viewColumn.ViewDataID },
            { ViewColumn.ColumnColumnName, (object)viewColumn.ColumnName }
          };
          var lookupRecord = mColumnManager.Retrieve(keyColumns);
          if (false == mColumnManager.IsDuplicate(lookupRecord, viewColumn, false))
          {
            var addedRecord = mColumnManager.AddWithFlags(viewColumn);
            if (addedRecord != null)
            {
              viewColumn.ID = addedRecord.ID;
              var row = RowAdd(viewColumn);
              ColumnGrid.LJCSetCurrentRow(row, true);
            }
          }
        }
      }
    }

    // Displays a detail dialog for a new record.
    internal void DoNew()
    {
      if (ViewGrid.CurrentRow is LJCGridRow parentRow)
      {
        // Data from list items.
        int parentID = parentRow.LJCGetInt32(ViewData.ColumnID);
        string parentName = parentRow.LJCGetString(ViewData.ColumnName);

        var location = FormCommon.GetDialogScreenPoint(ColumnGrid);
        var detail = new ViewColumnDetail
        {
          LJCLocation = location,
          LJCParentID = parentID,
          LJCParentName = parentName,

          // Use table name to get table columns.
          LJCTableName = EditList.TableCombo.Text,
        };
        detail.LJCChange += Detail_Change;
        detail.ShowDialog();
      }
    }

    // Displays a detail dialog to edit an existing record.
    internal void DoEdit()
    {
      if (ViewGrid.CurrentRow is LJCGridRow parentRow
        && ColumnGrid.CurrentRow is LJCGridRow row)
      {
        // Data from list items.
        int id = row.LJCGetInt32(ViewColumn.ColumnID);
        int parentID = parentRow.LJCGetInt32(ViewData.ColumnID);
        string parentName = parentRow.LJCGetString(ViewData.ColumnName);

        var location = FormCommon.GetDialogScreenPoint(ColumnGrid);
        var detail = new ViewColumnDetail()
        {
          LJCID = id,
          LJCLocation = location,
          LJCParentID = parentID,
          LJCParentName = parentName,

          // Use table name to get table columns.
          LJCTableName = EditList.TableCombo.Text,
        };
        detail.LJCChange += Detail_Change;
        detail.ShowDialog();
      }
    }

    // Deletes the selected row.
    internal void DoDelete()
    {
      if (ViewGrid.CurrentRow is LJCGridRow parentRow
        && ColumnGrid.CurrentRow is LJCGridRow row)
      {
        bool success = false;
        var title = "Delete Confirmation";
        var message = FormCommon.DeleteConfirm;
        if (MessageBox.Show(message, title, MessageBoxButtons.YesNo
          , MessageBoxIcon.Question) == DialogResult.Yes)
        {
          success = true;
        }

        int id = 0;
        if (success)
        {
          // Data from items.
          id = row.LJCGetInt32(ViewColumn.ColumnID);
          var parentID = parentRow.LJCGetInt32(ViewData.ColumnID);

          var gridColumnManager	= Managers.ViewGridColumnManager;
          var keyGridColumns = new DbColumns()
          {
            { ViewGridColumn.ColumnViewDataID, parentID },
            { ViewGridColumn.ColumnViewColumnID, id }
          };
          gridColumnManager.Delete(keyGridColumns);
        }

        if (success)
        {
          var keyColumns = new DbColumns()
          {
            { ViewColumn.ColumnID, id }
          };
          mColumnManager.Delete(keyColumns);
          if (mColumnManager.AffectedCount < 1)
          {
            success = false;
            message = FormCommon.DeleteError;
            MessageBox.Show(message, "Delete Error", MessageBoxButtons.OK
              , MessageBoxIcon.Exclamation);
          }
        }

        if (success)
        {
          ColumnGrid.Rows.Remove(row);
          EditList.TimedChange(Change.Column);
        }
      }
    }

    // Refreshes the list.
    internal void DoRefresh()
    {
      EditList.Cursor = Cursors.WaitCursor;
      int id = 0;
      if (ColumnGrid.CurrentRow is LJCGridRow row)
      {
        // Save the original row.
        id = row.LJCGetInt32(ViewColumn.ColumnID);
      }
      DataRetrieve();

      // Select the original row.
      if (id > 0)
      {
        var record = new ViewColumn()
        {
          ID = id
        };
        RowSelect(record);
      }
      EditList.Cursor = Cursors.Default;
    }

    // Adds new row or updates row with changes from the detail dialog.
    private void Detail_Change(object sender, EventArgs e)
    {
      var detail = sender as ViewColumnDetail;
      var record = detail.LJCRecord;
      if (detail.LJCIsUpdate)
      {
        RowUpdate(record);
      }
      else
      {
        // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
        var row = RowAdd(record);
        ColumnGrid.LJCSetCurrentRow(row, true);
        EditList.TimedChange(Change.Column);
      }
    }
    #endregion

    #region Setup Methods

    // Configures the View Column Grid.
    private void SetupGrid()
    {
      if (0 == ColumnGrid.Columns.Count)
      {
        List<string> propertyNames = new List<string> {
          ViewColumn.ColumnCaption,
          ViewColumn.ColumnColumnName,
          ViewColumn.ColumnPropertyName,
          ViewColumn.ColumnRenameAs,
          ViewColumn.ColumnDataTypeName
        };

        // Get the grid columns from the manager Data Definition.
        DbColumns gridColumns
          = mColumnManager.GetColumns(propertyNames);

        // Setup the grid columns.
        ColumnGrid.LJCAddColumns(gridColumns);
      }
    }
    #endregion

    #region Properties

    // Gets or sets the Managers reference.
    internal ManagersDbView Managers { get; set; }

    // Gets or sets the ColumnGrid reference.
    private LJCDataGrid ColumnGrid { get; set; }

    // Gets or sets the Parent List reference.
    private ViewEditorList EditList { get; set; }

    // Gets or sets the ViewGrid reference.
    private LJCDataGrid ViewGrid { get; set; }
    #endregion

    #region Class Data

    private ViewColumnManager mColumnManager;
    #endregion
  }
}
