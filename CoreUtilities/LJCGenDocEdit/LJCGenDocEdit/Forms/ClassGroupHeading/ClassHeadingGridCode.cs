// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// ClassHeadingGridCode.cs
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
  // Provides ClassHeadingGrid methods for the ClassHeadingSelect window.
  internal class ClassHeadingGridCode
  {
    #region Constructors

    // Initializes an object instance.
    internal ClassHeadingGridCode(ClassHeadingSelect parentList)
    {
      ClassHeadingSelect = parentList;
      ClassHeadingGrid = ClassHeadingSelect.ClassHeadingGrid;
      Managers = ClassHeadingSelect.Managers;
    }
    #endregion

    #region Data Methods

    // Retrieves the list rows.
    internal void DataRetrieve()
    {
      ClassHeadingSelect.Cursor = Cursors.WaitCursor;
      ClassHeadingGrid.LJCRowsClear();

      var manager = Managers.DocClassGroupHeadingManager;
      var names = new List<string>()
      {
        DocClassGroupHeading.ColumnSequence
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
      ClassHeadingSelect.SetControlState();
      ClassHeadingSelect.Cursor = Cursors.Default;
    }

    // Adds a grid row and updates it with the record values.
    private LJCGridRow RowAdd(DocClassGroupHeading dataRecord)
    {
      var retValue = ClassHeadingGrid.LJCRowAdd();
      SetStoredValues(retValue, dataRecord);
      retValue.LJCSetValues(ClassHeadingGrid, dataRecord);
      return retValue;
    }

    // Adds a grid row and updates it with the result values.
    private LJCGridRow RowAddValues(DbValues dbValues)
    {
      var retValue = ClassHeadingGrid.LJCRowAdd();

      var columnName = DocClassGroupHeading.ColumnID;
      var id = dbValues.LJCGetInt32(columnName);
      retValue.LJCSetInt32(columnName, id);

      retValue.LJCSetValues(ClassHeadingGrid, dbValues);
      return retValue;
    }

    // Selects a row based on the key record values.
    private bool RowSelect(DocClassGroupHeading dataRecord)
    {
      bool retValue = false;

      if (dataRecord != null)
      {
        ClassHeadingSelect.Cursor = Cursors.WaitCursor;
        foreach (LJCGridRow row in ClassHeadingGrid.Rows)
        {
          if (ClassHeadingID(row) == dataRecord.ID)
          {
            // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
            ClassHeadingGrid.LJCSetCurrentRow(row, true);
            retValue = true;
            break;
          }
        }
        ClassHeadingSelect.Cursor = Cursors.Default;
      }
      return retValue;
    }

    // Updates the current row with the record values.
    private void RowUpdate(DocClassGroupHeading dataRecord)
    {
      if (ClassHeadingGrid.CurrentRow is LJCGridRow row)
      {
        SetStoredValues(row, dataRecord);
        row.LJCSetValues(ClassHeadingGrid, dataRecord);
      }
    }

    // Sets the row stored values.
    private void SetStoredValues(LJCGridRow row
      , DocClassGroupHeading dataRecord)
    {
      row.LJCSetInt32(DocClassGroupHeading.ColumnID, dataRecord.ID);
    }
    #endregion

    #region Action Methods

    // Displays a detail dialog for a new record.
    internal void DoNew()
    {
      var detail = new ClassHeadingDetail()
      {
        LJCManagers = Managers
      };
      detail.LJCChange += Detail_Change;
      detail.ShowDialog();
    }

    // Displays a detail dialog to edit an existing record.
    internal void DoEdit()
    {
      if (ClassHeadingGrid.CurrentRow is LJCGridRow _)
      {
        var detail = new ClassHeadingDetail()
        {
          LJCHeadingID = ClassHeadingID(),
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
      var row = ClassHeadingGrid.CurrentRow as LJCGridRow;
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
          { DocClassGroupHeading.ColumnID, ClassHeadingID() }
        };
        var manager = Managers.DocClassGroupHeadingManager;
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
        ClassHeadingGrid.Rows.Remove(row);
      }
    }

    // Refreshes the list.
    internal void DoRefresh()
    {
      ClassHeadingSelect.Cursor = Cursors.WaitCursor;
      short id = 0;
      if (ClassHeadingGrid.CurrentRow is LJCGridRow _)
      {
        // Save the original row.
        id = ClassHeadingID();
      }
      DataRetrieve();

      // Select the original row.
      if (id > 0)
      {
        var dataRecord = new DocClassGroupHeading()
        {
          ID = id
        };
        RowSelect(dataRecord);
      }
      ClassHeadingSelect.Cursor = Cursors.Default;
    }

    // Sets the selected item and returns to the parent form.
    internal void DoSelect()
    {
      var selectList = ClassHeadingSelect;
      selectList.LJCSelectedRecord = null;
      var rows = ClassHeadingGrid.SelectedRows;
      var startIndex = rows.Count - 1;
      for (var index = startIndex; index >= 0; index--)
      {
        selectList.Cursor = Cursors.WaitCursor;
        var row = rows[index] as LJCGridRow;
        var manager = Managers.DocClassGroupHeadingManager;
        var keyRecord = manager.GetIDKey(ClassHeadingID(row));
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
      var manager = Managers.DocClassGroupManager;
      manager.AssemblyID = ClassHeadingSelect.LJCAssemblyID;
      manager.ResetSequence();
    }

    // Resets the Sequence column values.
    internal void DoResetSequence()
    {
      var manager = Managers.DocClassGroupHeadingManager;
      manager.ResetSequence();
    }

    // Adds new row or updates row with changes from the detail dialog.
    private void Detail_Change(object sender, EventArgs e)
    {
      var detail = sender as ClassHeadingDetail;
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
          ClassHeadingGrid.LJCSetCurrentRow(row, true);
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
      if (dragDataName == ClassHeadingGrid.LJCDragDataName)
      {
        var targetIndex = ClassHeadingGrid.LJCGetDragRowIndex(new Point(e.X, e.Y));
        if (targetIndex >= 0)
        {
          // Get source group.
          var sourceName = HeadingName(ClassHeadingID(sourceRow));
          var manager = Managers.DocClassGroupHeadingManager;
          var sourceGroup = manager.RetrieveWithUnique(sourceName);

          // Get target group.
          var targetRow = ClassHeadingGrid.Rows[targetIndex] as LJCGridRow;
          var targetName = HeadingName(ClassHeadingID(targetRow));
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
      ClassHeadingGrid.MultiSelect = true;

      // Setup default grid columns if no columns are defined.
      if (0 == ClassHeadingGrid.Columns.Count)
      {
        List<string> propertyNames = new List<string>()
        {
          DocClassGroupHeading.ColumnName,
          DocClassGroupHeading.ColumnHeading
        };

        // Get the grid columns from the manager Data Definition.
        var classManager = Managers.DocClassGroupHeadingManager;
        GridColumns = classManager.GetColumns(propertyNames);

        // Setup the grid  columns.
        ClassHeadingGrid.LJCAddColumns(GridColumns);
        FormCommon.NotSortable(ClassHeadingGrid);
        ClassHeadingGrid.LJCDragDataName = "DocClassGroupHeading";
      }
    }
    #endregion

    #region Get Data Methods

    // Retrieves the current row item ID.
    internal short ClassHeadingID(LJCGridRow classHeadingRow = null)
    {
      short retValue = 0;

      if (null == classHeadingRow)
      {
        classHeadingRow = ClassHeadingGrid.CurrentRow as LJCGridRow;
      }
      if (classHeadingRow != null)
      {
        retValue = (short)classHeadingRow.LJCGetInt32(DocClassGroupHeading.ColumnID);
      }
      return retValue;
    }

    // Retrieves the DocClassGroupHeading name.
    private string HeadingName(short classHeadingID)
    {
      string retValue = null;

      var classHeading = ClassHeadingWithID(classHeadingID);
      if (classHeading != null)
      {
        retValue = classHeading.Name;
      }
      return retValue;
    }

    // Retrieves the DocClassGroupHeading with the ID value.
    private DocClassGroupHeading ClassHeadingWithID(short classHeadingID)
    {
      DocClassGroupHeading retValue = null;

      if (classHeadingID > 0)
      {
        var manager = Managers.DocClassGroupHeadingManager;
        retValue = manager.RetrieveWithID(classHeadingID);
      }
      return retValue;
    }
    #endregion

    #region Private Methods

    // Checks for Previous and Next items.
    private void CheckPreviousAndNext(ClassHeadingDetail detail)
    {
      PreviousItem(detail);
      NextItem(detail);
    }

    // Checks for Next item.
    private void NextItem(ClassHeadingDetail detail)
    {
      if (detail.LJCNext)
      {
        LJCDataGrid grid = ClassHeadingGrid;
        int currentIndex = grid.CurrentRow.Index;
        detail.LJCNext = false;
        if (currentIndex < grid.Rows.Count - 1)
        {
          grid.LJCSetCurrentRow(currentIndex + 1, true);
          if (ClassHeadingID() > 0)
          {
            detail.LJCNext = true;
            detail.LJCHeadingID = ClassHeadingID();
          }
        }
      }
    }

    // Checks for Previous item.
    private void PreviousItem(ClassHeadingDetail detail)
    {
      if (detail.LJCPrevious)
      {
        LJCDataGrid grid = ClassHeadingGrid;
        int currentIndex = grid.CurrentRow.Index;
        detail.LJCPrevious = false;
        if (currentIndex > 0)
        {
          grid.LJCSetCurrentRow(currentIndex - 1, true);
          if (ClassHeadingID() > 0)
          {
            detail.LJCPrevious = true;
            detail.LJCHeadingID = ClassHeadingID();
          }
        }
      }
    }
    #endregion

    #region Properties

    // Gets or sets the ClassHeading grid reference.
    private LJCDataGrid ClassHeadingGrid { get; set; }

    // Gets or sets the Parent List reference.
    private ClassHeadingSelect ClassHeadingSelect { get; set; }

    // Gets or sets the GridColumns value.
    internal DbColumns GridColumns { get; set; }

    // Gets or sets the Managers reference.
    private ManagersDocGen Managers { get; set; }
    #endregion
  }
}
