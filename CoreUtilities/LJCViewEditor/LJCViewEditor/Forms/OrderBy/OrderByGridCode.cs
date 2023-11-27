// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
//OrderByGridClass.cs
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
  // Provides OrderByGrid methods for the ViewEditorList window.
  internal class OrderByGridCode
  {
    #region Constructors

    // Initializes an object instance.
    internal OrderByGridCode(ViewEditorList parentList)
    {
      parentList.Cursor = Cursors.WaitCursor;
      EditList = parentList;
      OrderByGrid = EditList.OrderByGrid;
      ViewGrid = EditList.ViewGrid;
      ResetData();
      EditList.Cursor = Cursors.Default;
    }

    // Resets the DataConfig dependent objects.
    internal void ResetData()
    {
      Managers = EditList.DataDbView.Managers;
      ViewOrderByManager = Managers.ViewOrderByManager;
    }
    #endregion

    #region Data Methods

    // Retrieves the list rows.
    internal void DataRetrieve()
    {
      EditList.Cursor = Cursors.WaitCursor;
      OrderByGrid.Rows.Clear();

      SetupGrid();
      if (ViewGrid.CurrentRow is LJCGridRow parentRow)
      {
        // Data from items.
        int parentID = parentRow.LJCGetInt32(ViewData.ColumnID);

        var manager = ViewOrderByManager;
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
    }

    // Adds a grid row and updates it with the record values.
    private LJCGridRow RowAdd(ViewOrderBy dataRecord)
    {
      var retValue = OrderByGrid.LJCRowAdd();
      SetStoredValues(retValue, dataRecord);
      retValue.LJCSetValues(OrderByGrid, dataRecord);
      return retValue;
    }

    // Adds a grid row and updates it with the result values.
    private LJCGridRow RowAddValues(DbValues dbValues)
    {
      var ljcGrid = OrderByGrid;
      var retValue = ljcGrid.LJCRowAdd();

      var columnName = ViewOrderBy.ColumnID;
      var id = dbValues.LJCGetInt32(columnName);
      retValue.LJCSetInt32(columnName, id);

      retValue.LJCSetValues(ljcGrid, dbValues);
      return retValue;
    }

    // Selects a row based on the key record values.
    private bool RowSelect(ViewOrderBy dataRecord)
    {
      bool retValue = false;

      if (dataRecord != null)
      {
        EditList.Cursor = Cursors.WaitCursor;
        foreach (LJCGridRow row in OrderByGrid.Rows)
        {
          var rowID = row.LJCGetInt32(ViewOrderBy.ColumnID);
          if (rowID == dataRecord.ID)
          {
            // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
            OrderByGrid.LJCSetCurrentRow(row, true);
            retValue = true;
            break;
          }
        }
        EditList.Cursor = Cursors.Default;
      }
      return retValue;
    }

    // Updates the current row with the record values.
    private void RowUpdate(ViewOrderBy dataRecord)
    {
      if (OrderByGrid.CurrentRow is LJCGridRow row)
      {
        SetStoredValues(row, dataRecord);
        row.LJCSetValues(OrderByGrid, dataRecord);
      }
    }

    // Sets the row stored values.
    private void SetStoredValues(LJCGridRow row, ViewOrderBy dataRecord)
    {
      row.LJCSetInt32(ViewOrderBy.ColumnID, dataRecord.ID);
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

        var grid = OrderByGrid;
        var location = FormCommon.GetDialogScreenPoint(grid);
        var detail = new ViewOrderByDetail
        {
          LJCLocation = location,
          LJCParentID = parentID,
          LJCParentName = parentName,
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
        && OrderByGrid.CurrentRow is LJCGridRow row)
      {
        // Data from list items.
        int id = row.LJCGetInt32(ViewOrderBy.ColumnID);
        int parentID = parentRow.LJCGetInt32(ViewData.ColumnID);
        string parentName = parentRow.LJCGetString(ViewData.ColumnName);

        var grid = OrderByGrid;
        var location = FormCommon.GetDialogScreenPoint(grid);
        var detail = new ViewOrderByDetail()
        {
          LJCID = id,
          LJCLocation = location,
          LJCParentID = parentID,
          LJCParentName = parentName,
          LJCTableName = EditList.TableCombo.Text,
        };
        detail.LJCChange += Detail_Change;
        detail.ShowDialog();
      }
    }

    // Deletes the selected row.
    internal void DoDelete()
    {
      bool success = false;
      var row = OrderByGrid.CurrentRow as LJCGridRow;
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
        var id = row.LJCGetInt32(ViewOrderBy.ColumnID);

        var keyColumns = new DbColumns()
        {
          { ViewOrderBy.ColumnID, id }
        };
        ViewOrderByManager.Delete(keyColumns);
        if (0 == ViewOrderByManager.AffectedCount)
        {
          success = false;
          var message = FormCommon.DeleteError;
          MessageBox.Show(message, "Delete Error", MessageBoxButtons.OK
            , MessageBoxIcon.Exclamation);
        }
      }

      if (success)
      {
        OrderByGrid.Rows.Remove(row);
        EditList.TimedChange(Change.OrderBy);
      }
    }

    // Refreshes the list.
    internal void DoRefresh()
    {
      EditList.Cursor = Cursors.WaitCursor;
      int id = 0;
      if (OrderByGrid.CurrentRow is LJCGridRow row)
      {
        id = row.LJCGetInt32(ViewOrderBy.ColumnID);
      }
      DataRetrieve();

      // Select the original row.
      if (id > 0)
      {
        var record = new ViewOrderBy()
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
      var detail = sender as ViewOrderByDetail;
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
          OrderByGrid.LJCSetCurrentRow(row, true);
          EditList.TimedChange(Change.OrderBy);
        }
      }
    }
    #endregion

    #region Other Methods

    // Configures the View OrderBy Grid.
    private void SetupGrid()
    {
      if (0 == OrderByGrid.Columns.Count)
      {
        List<string> propertyNames = new List<string>
        {
          ViewOrderBy.ColumnColumnName
        };

        // Get the grid columns from the manager Data Definition.
        var manager = ViewOrderByManager;
        var orderByGridColumns = manager.GetColumns(propertyNames);

        // Setup the grid columns.
        OrderByGrid.LJCAddColumns(orderByGridColumns);
      }
    }
    #endregion

    #region Properties

    // Gets or sets the Managers reference.
    internal ManagersDbView Managers { get; set; }

    // Gets or sets the Parent List reference.
    private ViewEditorList EditList { get; set; }

    // Gets or sets the OrderByGrid reference.
    private LJCDataGrid OrderByGrid { get; set; }

    // Gets or sets the ViewGrid reference.
    private LJCDataGrid ViewGrid { get; set; }

    // Gets or sets the Manager reference.
    private ViewOrderByManager ViewOrderByManager;
    #endregion
  }
}
