// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ConditionGridClass.cs
using LJCDBMessage;
using LJCDBViewDAL;
using LJCNetCommon;
using LJCWinFormCommon;
using LJCWinFormControls;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using static LJCViewEditor.ViewEditorList;

namespace LJCViewEditor
{
  // Provides ConditionGrid methods for the ViewEditorList window.
  internal class ConditionGridCode
  {
    #region Constructors

    // Initializes an object instance.
    internal ConditionGridCode(ViewEditorList parentList)
    {
      parentList.Cursor = Cursors.WaitCursor;
      EditList = parentList;
      ConditionGrid = EditList.ConditionGrid;
      ConditionSetGrid = EditList.ConditionSetGrid;
      ResetData();
      parentList.Cursor = Cursors.Default;
    }

    // Resets the DataConfig dependent objects.
    internal void ResetData()
    {
      Managers = EditList.Managers;
      ConditionManager = Managers.ViewConditionManager;
    }
    #endregion

    #region Data Methods

    // Retrieves the list rows.
    internal void DataRetrieve()
    {
      EditList.Cursor = Cursors.WaitCursor;
      ConditionGrid.Rows.Clear();

      SetupGrid();
      if (ConditionSetGrid.CurrentRow is LJCGridRow parentRow)
      {
        // Data from items.
        int parentID = parentRow.LJCGetInt32(ViewConditionSet.ColumnID);

        var result = ConditionManager.ResultWithParentID(parentID);
        if (DbResult.HasRows(result))
        {
          foreach (var dbRow in result.Rows)
          {
            RowAddValues(dbRow.Values);
          }
        }
      }
      EditList.Cursor = Cursors.Default;
      EditList.DoChange(Change.Condition);
    }

    // Adds a grid row and updates it with the record values.
    private LJCGridRow RowAdd(ViewCondition dataRecord)
    {
      var retValue = ConditionGrid.LJCRowAdd();
      SetStoredValues(retValue, dataRecord);
      retValue.LJCSetValues(ConditionGrid, dataRecord);
      return retValue;
    }

    // Adds a grid row and updates it with the result values.
    private LJCGridRow RowAddValues(DbValues dbValues)
    {
      var ljcGrid = ConditionGrid;
      var retValue = ljcGrid.LJCRowAdd();

      var columnName = ViewCondition.ColumnID;
      var id = dbValues.LJCGetInt16(columnName);
      retValue.LJCSetInt32(columnName, id);

      retValue.LJCSetValues(ljcGrid, dbValues);
      return retValue;
    }

    // Selects a row based on the key record values.
    private bool RowSelect(ViewCondition dataRecord)
    {
      bool retValue = false;

      if (dataRecord != null)
      {
        EditList.Cursor = Cursors.WaitCursor;
        foreach (LJCGridRow row in ConditionGrid.Rows)
        {
          var rowID = row.LJCGetInt32(ViewCondition.ColumnID);
          if (rowID == dataRecord.ID)
          {
            // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
            ConditionGrid.LJCSetCurrentRow(row, true);
            retValue = true;
            break;
          }
        }
        EditList.Cursor = Cursors.Default;
      }
      return retValue;
    }

    // Updates the current row with the record values.
    private void RowUpdate(ViewCondition dataRecord)
    {
      if (ConditionGrid.CurrentRow is LJCGridRow row)
      {
        SetStoredValues(row, dataRecord);
        row.LJCSetValues(ConditionGrid, dataRecord);
      }
    }

    // Sets the row stored values.
    private void SetStoredValues(LJCGridRow row
      , ViewCondition dataRecord)
    {
      row.LJCSetInt32(ViewCondition.ColumnID, dataRecord.ID);
    }
    #endregion

    #region Action Methods

    // Displays a detail dialog for a new record.
    internal void DoNew()
    {
      if (ConditionSetGrid.CurrentRow is LJCGridRow parentRow)
      {
        // Data from list items.
        int parentID = parentRow.LJCGetInt32(ViewConditionSet.ColumnID);
        string parentName = parentRow.LJCGetString(ViewConditionSet.ColumnBooleanOperator);

        var location = FormPoint.DialogScreenPoint(ConditionGrid);
        var detail = new ViewConditionDetail
        {
          LJCParentID = parentID,
          LJCParentName = parentName,
          LJCLocation = location,

          // Use table name to get table columns.
          LJCTableName = EditList.TableCombo.Text,
        };
        detail.LJCChange += Detail_Change;
        detail.LJCLocation = FormPoint.AdjustedLocation(detail, location);
        detail.ShowDialog();
      }
    }

    // Displays a detail dialog to edit an existing record.
    internal void DoEdit()
    {
      if (ConditionSetGrid.CurrentRow is LJCGridRow parentRow
        && ConditionGrid.CurrentRow is LJCGridRow row)
      {
        // Data from list items.
        int id = row.LJCGetInt32(ViewCondition.ColumnID);
        int parentID = parentRow.LJCGetInt32(ViewConditionSet.ColumnID);
        string parentName = parentRow.LJCGetString(ViewConditionSet.ColumnBooleanOperator);

        var grid = ConditionGrid;
        var location = FormPoint.DialogScreenPoint(grid);
        var detail = new ViewConditionDetail()
        {
          LJCID = id,
          LJCParentID = parentID,
          LJCParentName = parentName,
          LJCLocation = location,

          // Use table name to get table columns.
          LJCTableName = EditList.TableCombo.Text,
        };
        detail.LJCChange += Detail_Change;
        detail.LJCLocation = FormPoint.AdjustedLocation(detail, location);
        detail.ShowDialog();
      }
    }

    // Deletes the selected row.
    internal void DoDelete()
    {
      bool success = false;
      var row = ConditionGrid.CurrentRow as LJCGridRow;
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
        var id = row.LJCGetInt32(ViewCondition.ColumnID);

        var keyColumns = new DbColumns()
          {
            { ViewCondition.ColumnID, id }
          };
        ConditionManager.Delete(keyColumns);
        if (ConditionManager.AffectedCount < 1)
        {
          success = false;
          var message = FormCommon.DeleteError;
          MessageBox.Show(message, "Delete Error", MessageBoxButtons.OK
            , MessageBoxIcon.Exclamation);
        }
      }

      if (success)
      {
        ConditionGrid.Rows.Remove(row);
        EditList.TimedChange(Change.Condition);
      }
    }

    // Refreshes the list.
    internal void DoRefresh()
    {
      EditList.Cursor = Cursors.WaitCursor;
      int id = 0;
      if (ConditionGrid.CurrentRow is LJCGridRow row)
      {
        // Save the original row.
        id = row.LJCGetInt32(ViewCondition.ColumnID);
      }
      DataRetrieve();

      // Select the original row.
      if (id > 0)
      {
        var record = new ViewCondition()
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
      var detail = sender as ViewConditionDetail;
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
          ConditionGrid.LJCSetCurrentRow(row, true);
          EditList.TimedChange(Change.ConditionSet);
        }
      }
    }
    #endregion

    #region Other Methods

    // Configures the ViewCondition Grid.
    private void SetupGrid()
    {
      if (0 == ConditionGrid.Columns.Count)
      {
        List<string> propertyNames = new List<string>
        {
          ViewCondition.ColumnFirstValue,
          ViewCondition.ColumnComparisonOperator,
          ViewCondition.ColumnSecondValue
        };

        // Get the grid columns from the manager Data Definition.
        var manager = ConditionManager;
        var gridColumns = manager.GetColumns(propertyNames);

        // Setup the grid columns.
        ConditionGrid.LJCAddColumns(gridColumns);
      }
    }
    #endregion

    #region Properties

    // Gets or sets the Managers reference.
    internal ManagersDbView Managers { get; set; }

    // Gets or sets the ConditionGrid reference.
    private LJCDataGrid ConditionGrid { get; set; }

    // Gets or sets the Manager reference.
    private ViewConditionManager ConditionManager { get; set; }

    // Gets or sets the ConditionSetGrid reference.
    private LJCDataGrid ConditionSetGrid { get; set; }

    // Gets or sets the Parent List reference.
    private ViewEditorList EditList { get; set; }
    #endregion
  }
}
