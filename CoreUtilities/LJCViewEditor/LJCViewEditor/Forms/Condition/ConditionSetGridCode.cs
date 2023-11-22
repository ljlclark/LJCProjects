// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ConditionSetGridClass.cs
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
  // Provides ConditionSetGrid methods for the ViewEditorList window.
  internal class ConditionSetGridCode
  {
    #region Constructors

    // Initializes an object instance.
    internal ConditionSetGridCode(ViewEditorList parentList)
    {
      parentList.Cursor = Cursors.WaitCursor;
      EditList = parentList;
      ConditionSetGrid = EditList.ConditionSetGrid;
      ResetData();
      FilterGrid = EditList.FilterGrid;
      EditList.Cursor = Cursors.Default;
    }

    // Resets the DataConfig dependent objects.
    internal void ResetData()
    {
      Managers = EditList.Managers;
      mViewConditionSetManager = Managers.ViewConditionSetManager;
    }
    #endregion

    #region Data Methods

    // Retrieves the list rows.
    /// <include path='items/DataRetrieve/*' file='../../LJCGenDoc/Common/List.xml'/>
    internal void DataRetrieve()
    {
      EditList.Cursor = Cursors.WaitCursor;
      ConditionSetGrid.Rows.Clear();

      SetupGrid();
      if (FilterGrid.CurrentRow is LJCGridRow EditListRow)
      {
        // Data from items.
        int parentID = EditListRow.LJCGetInt32(ViewFilter.ColumnID);

        var manager = mViewConditionSetManager;
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
      EditList.DoChange(Change.ConditionSet);
    }

    // Adds a grid row and updates it with the record values.
    private LJCGridRow RowAdd(ViewConditionSet dataRecord)
    {
      var retValue = ConditionSetGrid.LJCRowAdd();
      SetStoredValues(retValue, dataRecord);
      retValue.LJCSetValues(ConditionSetGrid, dataRecord);
      return retValue;
    }

    // Adds a grid row and updates it with the result values.
    private LJCGridRow RowAddValues(DbValues dbValues)
    {
      var ljcGrid = ConditionSetGrid;
      var retValue = ljcGrid.LJCRowAdd();

      var columnName = ViewConditionSet.ColumnID;
      var id = dbValues.LJCGetInt32(columnName);
      retValue.LJCSetInt32(columnName, id);

      columnName = ViewConditionSet.ColumnBooleanOperator;
      var name = dbValues.LJCGetString(columnName);
      retValue.LJCSetString(columnName, name);

      retValue.LJCSetValues(ljcGrid, dbValues);
      return retValue;
    }

    // Updates the current row with the record values.
    private void RowUpdate(ViewConditionSet dataRecord)
    {
      if (ConditionSetGrid.CurrentRow is LJCGridRow row)
      {
        SetStoredValues(row, dataRecord);
        row.LJCSetValues(ConditionSetGrid, dataRecord);
      }
    }

    // Sets the row stored values.
    private void SetStoredValues(LJCGridRow row
      , ViewConditionSet dataRecord)
    {
      row.LJCSetInt32(ViewConditionSet.ColumnID, dataRecord.ID);
      row.LJCSetString(ViewConditionSet.ColumnBooleanOperator
        , dataRecord.BooleanOperator);
    }

    // Selects a row based on the key record values.
    private bool RowSelect(ViewConditionSet dataRecord)
    {
      bool retValue = false;
      if (dataRecord != null)
      {
        EditList.Cursor = Cursors.WaitCursor;
        foreach (LJCGridRow row in ConditionSetGrid.Rows)
        {
          var rowID = row.LJCGetInt32(ViewConditionSet.ColumnID);
          if (rowID == dataRecord.ID)
          {
            // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
            ConditionSetGrid.LJCSetCurrentRow(row, true);
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
      if (EditList.FilterGrid.CurrentRow is LJCGridRow EditListRow)
      {
        int parentID = EditListRow.LJCGetInt32(ViewFilter.ColumnID);
        string parentName = EditListRow.LJCGetString(ViewFilter.ColumnName);

        var grid = ConditionSetGrid;
        var location = FormCommon.GetDialogScreenPoint(grid);
        var detail = new ViewConditionSetDetail
        {
          LJCParentID = parentID,
          LJCParentName = parentName,
          LJCLocation = location
        };
        detail.LJCChange += ConditionSetDetail_Change;
        detail.ShowDialog();
      }
    }

    // Displays a detail dialog to edit an existing record.
    internal void DoEdit()
    {
      if (FilterGrid.CurrentRow is LJCGridRow EditListRow
        && ConditionSetGrid.CurrentRow is LJCGridRow row)
      {
        // Data from list items.
        int id = row.LJCGetInt32(ViewConditionSet.ColumnID);
        int parentID = EditListRow.LJCGetInt32(ViewFilter.ColumnID);
        string parentName = EditListRow.LJCGetString(ViewFilter.ColumnName);

        var grid = ConditionSetGrid;
        var location = FormCommon.GetDialogScreenPoint(grid);
        var detail = new ViewConditionSetDetail()
        {
          LJCID = id,
          LJCParentID = parentID,
          LJCParentName = parentName,
          LJCLocation = location
        };
        detail.LJCChange += ConditionSetDetail_Change;
        detail.ShowDialog();
      }
    }

    // Deletes the selected row.
    internal void DoDelete()
    {
      if (ConditionSetGrid.CurrentRow is LJCGridRow row)
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
          var id = row.LJCGetInt32(ViewConditionSet.ColumnID);

          var keyColumns = new DbColumns()
          {
            { ViewConditionSet.ColumnID, id }
          };
          mViewConditionSetManager.Delete(keyColumns);
          if (mViewConditionSetManager.AffectedCount < 1)
          {
            success = false;
            message = FormCommon.DeleteError;
            MessageBox.Show(message, "Delete Error", MessageBoxButtons.OK
              , MessageBoxIcon.Exclamation);
          }
        }

        if (success)
        {
          ConditionSetGrid.Rows.Remove(row);
          EditList.TimedChange(Change.ConditionSet);
        }
      }
    }

    // Refreshes the list.
    internal void DoRefresh()
    {
      EditList.Cursor = Cursors.WaitCursor;
      int id = 0;
      if (ConditionSetGrid.CurrentRow is LJCGridRow row)
      {
        // Save the original row.
        id = row.LJCGetInt32(ViewConditionSet.ColumnID);
      }
      DataRetrieve();

      // Select the original row.
      if (id > 0)
      {
        var record = new ViewConditionSet()
        {
          ID = id
        };
        RowSelect(record);
      }
      EditList.Cursor = Cursors.Default;
    }

    // Adds new row or updates existing row with changes from the detail dialog.
    private void ConditionSetDetail_Change(object sender, EventArgs e)
    {
      var detail = sender as ViewConditionSetDetail;
      var record = detail.LJCRecord;
      if (detail.LJCIsUpdate)
      {
        RowUpdate(record);
      }
      else
      {
        // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
        var row = RowAdd(record);
        ConditionSetGrid.LJCSetCurrentRow(row, true);
        EditList.TimedChange(Change.ConditionSet);
      }
    }
    #endregion

    #region Setup Methods

    // Configures the ViewConditionSet Grid.
    private void SetupGrid()
    {
      if (0 == ConditionSetGrid.Columns.Count)
      {
        List<string> propertyNames = new List<string>
        {
          ViewConditionSet.ColumnBooleanOperator
        };

        // Get the grid columns from the manager Data Definition.
        DbColumns gridColumns
          = mViewConditionSetManager.GetColumns(propertyNames);

        // Setup the grid columns.
        ConditionSetGrid.LJCAddColumns(gridColumns);
      }
    }
    #endregion

    #region Properties

    // Gets or sets the Managers reference.
    internal ManagersDbView Managers { get; set; }

    // Gets or sets the ConditionSetGrid reference.
    private LJCDataGrid ConditionSetGrid { get; set; }

    // Gets or sets the EditList List reference.
    private ViewEditorList EditList { get; set; }

    // Gets or sets the FilterGrid reference.
    private LJCDataGrid FilterGrid { get; set; }
    #endregion

    #region Class Data

    private ViewConditionSetManager mViewConditionSetManager;
    #endregion
  }
}
