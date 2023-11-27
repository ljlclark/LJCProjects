// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// MethodHeadingGridCode.cs
using LJCDBMessage;
using LJCGenDocDAL;
using LJCNetCommon;
using LJCWinFormCommon;
using LJCWinFormControls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace LJCGenDocEdit
{
  // The MethodHeading grid code.
  // Provides MethodheadingGrid methods for the MethodheadingSelect window.
  internal class MethodHeadingGridCode
  {
    #region Constructors

    // Initializes an object instance.
    internal MethodHeadingGridCode(MethodHeadingSelect parentList)
    {
      MethodHeadingSelect = parentList;
      Managers = MethodHeadingSelect.Managers;
      MethodHeadingGrid = MethodHeadingSelect.MethodHeadingGrid;
    }
    #endregion

    #region Data Methods

    // Retrieves the list rows.
    internal void DataRetrieve()
    {
      MethodHeadingSelect.Cursor = Cursors.WaitCursor;
      MethodHeadingGrid.LJCRowsClear();

      var manager = Managers.DocMethodGroupHeadingManager;
      var names = new List<string>()
      {
        DocMethodGroupHeading.ColumnSequence
      };
      manager.SetOrderBy(names);

      DbResult result = manager.LoadResult();
      if (DbResult.HasRows(result))
      {
        foreach (DbRow dbRow in result.Rows)
        {
          RowAddValues(dbRow.Values);
        }
      }
      MethodHeadingSelect.SetControlState();
      MethodHeadingSelect.Cursor = Cursors.Default;
    }

    // Adds a grid row and updates it with the record values.
    private LJCGridRow RowAdd(DocMethodGroupHeading dataRecord)
    {
      var retValue = MethodHeadingGrid.LJCRowAdd();
      SetStoredValues(retValue, dataRecord);
      retValue.LJCSetValues(MethodHeadingGrid, dataRecord);
      return retValue;
    }

    // Adds a grid row and updates it with the result values.
    private LJCGridRow RowAddValues(DbValues dbValues)
    {
      var retValue = MethodHeadingGrid.LJCRowAdd();

      var columnName = DocMethodGroupHeading.ColumnID;
      var id = dbValues.LJCGetInt32(columnName);
      retValue.LJCSetInt32(columnName, id);

      retValue.LJCSetValues(MethodHeadingGrid, dbValues);
      return retValue;
    }

    // Selects a row based on the key record values.
    private bool RowSelect(DocMethodGroupHeading dataRecord)
    {
      bool retValue = false;

      if (dataRecord != null)
      {
        MethodHeadingSelect.Cursor = Cursors.WaitCursor;
        foreach (LJCGridRow row in MethodHeadingGrid.Rows)
        {
          if (MethodHeadingID(row) == dataRecord.ID)
          {
            // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
            MethodHeadingGrid.LJCSetCurrentRow(row, true);
            retValue = true;
            break;
          }
        }
        MethodHeadingSelect.Cursor = Cursors.Default;
      }
      return retValue;
    }

    // Updates the current row with the record values.
    private void RowUpdate(DocMethodGroupHeading dataRecord)
    {
      if (MethodHeadingGrid.CurrentRow is LJCGridRow row)
      {
        SetStoredValues(row, dataRecord);
        row.LJCSetValues(MethodHeadingGrid, dataRecord);
      }
    }

    // Sets the row stored values.
    private void SetStoredValues(LJCGridRow row
      , DocMethodGroupHeading dataRecord)
    {
      row.LJCSetInt32(DocMethodGroupHeading.ColumnID, dataRecord.ID);
    }
    #endregion

    #region Action Methods

    // Displays a detail dialog for a new record.
    internal void DoNew()
    {
      var detail = new MethodHeadingDetail()
      {
        LJCManagers = Managers
      };
      detail.LJCChange += Detail_Change;
      detail.ShowDialog();
    }

    // Displays a detail dialog to edit an existing record.
    internal void DoEdit()
    {
      if (MethodHeadingGrid.CurrentRow is LJCGridRow _)
      {
        var detail = new MethodHeadingDetail()
        {
          LJCHeadingID = MethodHeadingID(),
          LJCManagers = Managers
        };
        detail.LJCChange += Detail_Change;
        detail.ShowDialog();
      }
    }

    // Deletes the selected row.
    internal void DoDelete()
    {
      bool success = false;
      var row = MethodHeadingGrid.CurrentRow as LJCGridRow;
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
        var keyRecord = new DbColumns()
        {
          { DocMethodGroupHeading.ColumnID, MethodHeadingID() }
        };
        var manager = Managers.DocMethodGroupHeadingManager;
        manager.Delete(keyRecord);
        if (0 == manager.Manager.AffectedCount)
        {
          success = false;
          var message = FormCommon.DeleteError;
          MessageBox.Show(message, "Delete Error", MessageBoxButtons.OK
            , MessageBoxIcon.Exclamation);
        }
      }

      if (success)
      {
        MethodHeadingGrid.Rows.Remove(row);
      }
    }

    // Refreshes the list.
    internal void DoRefresh()
    {
      MethodHeadingSelect.Cursor = Cursors.WaitCursor;
      short id = 0;
      if (MethodHeadingGrid.CurrentRow is LJCGridRow _)
      {
        // Save the original row.
        id = MethodHeadingID();
      }
      DataRetrieve();

      if (id > 0)
      {
        var dataRecord = new DocMethodGroupHeading()
        {
          ID = id
        };
        RowSelect(dataRecord);
      }
      MethodHeadingSelect.Cursor = Cursors.Default;
    }

    // Sets the selected item and returns to the parent form.
    internal void DoSelect()
    {
      var selectList = MethodHeadingSelect;
      selectList.LJCSelectedRecord = null;
      var rows = MethodHeadingGrid.SelectedRows;
      var startIndex = rows.Count - 1;
      for (var index = startIndex; index >= 0; index--)
      {
        selectList.Cursor = Cursors.WaitCursor;
        var row = rows[index] as LJCGridRow;
        var manager = Managers.DocMethodGroupHeadingManager;
        var keyRecord = manager.GetIDKey(MethodHeadingID(row));
        var dataObject = manager.Retrieve(keyRecord);
        if (dataObject != null)
        {
          selectList.LJCSelectedRecord = dataObject;
          selectList.LastMultiSelect = false;
          if (0 == index)
          {
            selectList.LastMultiSelect = true;
          }
          selectList.LJCOnChange();
        }
      }
      DoResetParentSequence();
      selectList.Cursor = Cursors.Default;
      selectList.DialogResult = DialogResult.OK;
    }

    // Resets the Sequence column values.
    internal void DoResetParentSequence()
    {
      var manager = Managers.DocMethodGroupManager;
      manager.ClassID = MethodHeadingSelect.LJClassID;
      manager.ResetSequence();
    }

    // Resets the Sequence column values.
    internal void DoResetSequence()
    {
      var manager = Managers.DocMethodGroupHeadingManager;
      manager.ResetSequence();
    }

    // Adds new row or updates row with changes from the detail dialog.
    private void Detail_Change(object sender, EventArgs e)
    {
      var detail = sender as MethodHeadingDetail;
      if (detail.LJCRecord != null)
      {
        var dataRecord = detail.LJCRecord;
        if (detail.LJCIsUpdate)
        {
          RowUpdate(dataRecord);
          CheckPreviousAndNext(detail);
          DoRefresh();
        }
        else
        {
          // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
          var row = RowAdd(dataRecord);
          MethodHeadingGrid.LJCSetCurrentRow(row, true);
          CheckPreviousAndNext(detail);
          DoRefresh();
        }
      }
    }
    #endregion

    #region Other Methods

    // The DragDrop method.
    internal void DoDragDrop(DragEventArgs e)
    {
      var sourceRow = e.Data.GetData(typeof(LJCGridRow)) as LJCGridRow;
      var dragDataName = sourceRow.LJCGetString("DragDataName");
      if (dragDataName == MethodHeadingGrid.LJCDragDataName)
      {
        var targetIndex = MethodHeadingGrid.LJCGetDragRowIndex(new Point(e.X, e.Y));
        if (targetIndex >= 0)
        {
          // Get source group.
          var sourceName = HeadingName(MethodHeadingID(sourceRow));
          var manager = Managers.DocMethodGroupHeadingManager;
          var sourceGroup = manager.RetrieveWithUnique(sourceName);

          // Get target group.
          var targetRow = MethodHeadingGrid.Rows[targetIndex] as LJCGridRow;
          var targetName = HeadingName(MethodHeadingID(targetRow));
          var targetGroup = manager.RetrieveWithUnique(targetName);

          var sourceSequence = sourceGroup.Sequence;
          var targetSequence = targetGroup.Sequence;
          manager.ChangeSequence(sourceSequence, targetSequence);
          DoRefresh();
        }
      }
    }

    // Setup the grid columns.
    internal void SetupGrid()
    {
      MethodHeadingGrid.MultiSelect = true;

      // Setup default grid columns if no columns are defined.
      if (0 == MethodHeadingGrid.Columns.Count)
      {
        List<string> propertyNames = new List<string>()
        {
          DocMethodGroupHeading.ColumnName,
          DocMethodGroupHeading.ColumnHeading
        };

        // Get the grid columns from the manager Data Definition.
        var methodManager = Managers.DocMethodGroupHeadingManager;
        GridColumns = methodManager.GetColumns(propertyNames);

        // Setup the grid columns.
        MethodHeadingGrid.LJCAddColumns(GridColumns);
        FormCommon.NotSortable(MethodHeadingGrid);
        MethodHeadingGrid.LJCDragDataName = "DocMethodGroupHeading";
      }
    }
    #endregion

    #region Get Data Methods

    // Retrieves the current row item ID.
    internal short MethodHeadingID(LJCGridRow methodheadingRow = null)
    {
      short retValue = 0;

      if (null == methodheadingRow)
      {
        methodheadingRow = MethodHeadingGrid.CurrentRow as LJCGridRow;
      }
      if (methodheadingRow != null)
      {
        retValue = (short)methodheadingRow.LJCGetInt32(DocMethodGroupHeading.ColumnID);
      }
      return retValue;
    }

    // Retrieves the DocClassGroupHeading name.
    private string HeadingName(short methodHeadingID)
    {
      string retValue = null;

      var methodHeading = MethodHeadingWithID(methodHeadingID);
      if (methodHeading != null)
      {
        retValue = methodHeading.Name;
      }
      return retValue;
    }

    // Retrieves the DocMethodGroupHeading with the ID value.
    private DocMethodGroupHeading MethodHeadingWithID(short methodHeadingID)
    {
      DocMethodGroupHeading retValue = null;

      if (methodHeadingID > 0)
      {
        var manager = Managers.DocMethodGroupHeadingManager;
        retValue = manager.RetrieveWithID(methodHeadingID);
      }
      return retValue;
    }
    #endregion

    #region Private Methods

    // Checks for Previous and Next items.
    private void CheckPreviousAndNext(MethodHeadingDetail detail)
    {
      PreviousItem(detail);
      NextItem(detail);
    }

    // Checks for Next item.
    private void NextItem(MethodHeadingDetail detail)
    {
      if (detail.LJCNext)
      {
        LJCDataGrid grid = MethodHeadingGrid;
        int currentIndex = grid.CurrentRow.Index;
        detail.LJCNext = false;
        if (currentIndex < grid.Rows.Count - 1)
        {
          grid.LJCSetCurrentRow(currentIndex + 1, true);
          if (MethodHeadingID() > 0)
          {
            detail.LJCNext = true;
            detail.LJCHeadingID = MethodHeadingID();
          }
        }
      }
    }

    // Checks for Previous item.
    private void PreviousItem(MethodHeadingDetail detail)
    {
      if (detail.LJCPrevious)
      {
        LJCDataGrid grid = MethodHeadingGrid;
        int currentIndex = grid.CurrentRow.Index;
        detail.LJCPrevious = false;
        if (currentIndex > 0)
        {
          grid.LJCSetCurrentRow(currentIndex - 1, true);
          if (MethodHeadingID() > 0)
          {
            detail.LJCPrevious = true;
            detail.LJCHeadingID = MethodHeadingID();
          }
        }
      }
    }
    #endregion

    #region Properties

    // Gets or sets the GridColumns value.
    internal DbColumns GridColumns { get; set; }

    // The Managers object.
    private ManagersDocGen Managers { get; set; }

    // Gets or sets the MethodHeading grid reference.
    private LJCDataGrid MethodHeadingGrid { get; set; }

    // Gets or sets the Parent List reference.
    private MethodHeadingSelect MethodHeadingSelect { get; set; }
    #endregion
  }
}
