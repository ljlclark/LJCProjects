// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ConditionGridClass.cs
using LJCNetCommon;
using LJCWinFormCommon;
using LJCWinFormControls;
using LJCDBViewDAL;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using LJCDBMessage;

namespace LJCViewEditor
{
  // Provides ConditionGrid methods for the ViewEditorList window.
  internal class ConditionGridClass
  {
    #region Constructors

    // Initializes an object instance.
    internal ConditionGridClass(ViewEditorList parent)
    {
      Parent = parent;
      ConditionGrid = Parent.ConditionGrid;
      ConditionSetGrid = Parent.ConditionSetGrid;
      ResetData();
    }

    // Resets the DataConfig dependent objects.
    internal void ResetData()
    {
      Managers = Parent.Managers;
      mConditionManager = Managers.ViewConditionManager;
    }
    #endregion

    #region Data Methods

    // Retrieves the list rows.
    internal void DataRetrieve()
    {
      Parent.Cursor = Cursors.WaitCursor;
      ConditionGrid.Rows.Clear();

      SetupGridCondition();
      if (ConditionSetGrid.CurrentRow is LJCGridRow parentRow)
      {
        // Data from items.
        int viewConditionSetID = parentRow.LJCGetInt32(ViewConditionSet.ColumnID);

        var result = mConditionManager.ResultWithParentID(viewConditionSetID);
        if (DbResult.HasRows(result))
        {
          foreach (var dbRow in result.Rows)
          {
            RowAddValues(dbRow.Values);
          }
        }
      }
      Parent.Cursor = Cursors.Default;
      Parent.DoChange(ViewEditorList.Change.Condition);
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

    // Selects a row based on the key record values.
    private void RowSelect(ViewCondition record)
    {
      if (record != null)
      {
        Parent.Cursor = Cursors.WaitCursor;
        foreach (LJCGridRow row in ConditionGrid.Rows)
        {
          var rowID = row.LJCGetInt32(ViewCondition.ColumnID);
          if (rowID == record.ID)
          {
            // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
            ConditionGrid.LJCSetCurrentRow(row, true);
            break;
          }
        }
        Parent.Cursor = Cursors.Default;
      }
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

        var location = FormCommon.GetDialogScreenPoint(ConditionGrid);
        var detail = new ViewConditionDetail
        {
          LJCParentID = parentID,
          LJCParentName = parentName,
          LJCLocation = location,

          // Use table name to get table columns.
          LJCTableName = Parent.TableCombo.Text,
        };
        detail.LJCChange += ConditionDetail_Change;
        detail.ShowDialog();
      }
    }

    // Displays a detail dialog to edit an existing record.
    internal void DoEdit()
    {
      if (ConditionSetGrid.CurrentRow is LJCGridRow parentRow
        && ConditionGrid.CurrentRow is LJCGridRow row)
      {
        int id = row.LJCGetInt32(ViewCondition.ColumnID);
        int parentID = parentRow.LJCGetInt32(ViewConditionSet.ColumnID);
        string parentName = parentRow.LJCGetString(ViewConditionSet.ColumnBooleanOperator);

        var grid = ConditionGrid;
        var location = FormCommon.GetDialogScreenPoint(grid);
        var detail = new ViewConditionDetail()
        {
          LJCID = id,
          LJCParentID = parentID,
          LJCParentName = parentName,
          LJCLocation = location,

          // Use table name to get table columns.
          LJCTableName = Parent.TableCombo.Text,
        };
        detail.LJCChange += ConditionDetail_Change;
        detail.ShowDialog();
      }
    }

    // Deletes the selected row.
    internal void DoDelete()
    {
      if (ConditionGrid.CurrentRow is LJCGridRow row)
      {
        var title = "Delete Confirmation";
        var message = FormCommon.DeleteConfirm;
        if (MessageBox.Show(message, title, MessageBoxButtons.YesNo
          , MessageBoxIcon.Question) == DialogResult.Yes)
        {
          var keyColumns = new DbColumns()
          {
            { ViewCondition.ColumnID, row.LJCGetInt32(ViewCondition.ColumnID) }
          };
          mConditionManager.Delete(keyColumns);
          if (mConditionManager.AffectedCount < 1)
          {
            message = FormCommon.DeleteError;
            MessageBox.Show(message, "Delete Error", MessageBoxButtons.OK
              , MessageBoxIcon.Exclamation);
          }
          else
          {
            ConditionGrid.Rows.Remove(row);
            Parent.TimedChange(ViewEditorList.Change.Condition);
          }
        }
      }
    }

    // Refreshes the list.
    internal void DoRefresh()
    {
      int id = 0;
      if (ConditionGrid.CurrentRow is LJCGridRow row)
      {
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
    }

    // Adds new row or updates existing row with changes from the detail dialog.
    private void ConditionDetail_Change(object sender, EventArgs e)
    {
      var detail = sender as ViewConditionDetail;
      var record = detail.LJCRecord;
      if (detail.LJCIsUpdate)
      {
        RowUpdate(record);
      }
      else
      {
        // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
        var row = RowAdd(record);
        ConditionGrid.LJCSetCurrentRow(row, true);
        Parent.TimedChange(ViewEditorList.Change.ConditionSet);
      }
    }
    #endregion

    #region Setup Methods

    // Configures the View Condition Grid.
    private void SetupGridCondition()
    {
      if (0 == ConditionGrid.Columns.Count)
      {
        List<string> propertyNames = new List<string> {
          ViewCondition.ColumnFirstValue,
          ViewCondition.ColumnComparisonOperator,
          ViewCondition.ColumnSecondValue
        };

        // Get the grid columns from the manager Data Definition.
        var conditionGridColumns
          = mConditionManager.GetColumns(propertyNames);

        // Setup the grid columns.
        ConditionGrid.LJCAddColumns(conditionGridColumns);
      }
    }
    #endregion

    #region Properties

    internal ManagersDbView Managers { get; set; }

    private LJCDataGrid ConditionGrid { get; set; }

    private LJCDataGrid ConditionSetGrid { get; set; }
    #endregion

    #region Class Data

    private readonly ViewEditorList Parent;
    private ViewConditionManager mConditionManager;
    #endregion
  }
}
