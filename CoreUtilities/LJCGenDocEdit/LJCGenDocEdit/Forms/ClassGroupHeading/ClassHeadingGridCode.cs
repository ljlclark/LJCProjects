// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// ClassHeadingGridCode.cs
using LJCDBMessage;
using LJCDocLibDAL;
using LJCNetCommon;
using LJCWinFormCommon;
using LJCWinFormControls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace LJCGenDocEdit
{
  // The ClassHeading grid code.
  internal class ClassHeadingGridCode
  {
    #region Constructors

    /// <summary>Initializes an object instance.</summary>
    internal ClassHeadingGridCode(ClassHeadingSelect parentList)
    {
      mClassHeadingSelect = parentList;
      mClassHeadingGrid = mClassHeadingSelect.ClassHeadingGrid;
      Managers = mClassHeadingSelect.Managers;
    }
    #endregion

    #region Data Methods

    /// <summary>Retrieves the list rows.</summary>
    internal void DataRetrieve()
    {
      mClassHeadingSelect.Cursor = Cursors.WaitCursor;
      mClassHeadingGrid.LJCRowsClear();

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
      mClassHeadingSelect.SetControlState();
      mClassHeadingSelect.Cursor = Cursors.Default;
    }

    // Selects a row based on the key record values.
    /// <include path='items/RowSelect/*' file='../../../../LJCDocLib/Common/List.xml'/>
    internal bool RowSelect(DocClassGroupHeading dataRecord)
    {
      bool retValue = false;

      if (dataRecord != null)
      {
        mClassHeadingSelect.Cursor = Cursors.WaitCursor;
        foreach (LJCGridRow row in mClassHeadingGrid.Rows)
        {
          if (ClassHeadingID(row) == dataRecord.ID)
          {
            // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
            mClassHeadingGrid.LJCSetCurrentRow(row, true);
            retValue = true;
            break;
          }
        }
        mClassHeadingSelect.Cursor = Cursors.Default;
      }
      return retValue;
    }

    // Adds a grid row and updates it with the record values.
    private LJCGridRow RowAdd(DocClassGroupHeading dataRecord)
    {
      var retValue = mClassHeadingGrid.LJCRowAdd();
      SetStoredValues(retValue, dataRecord);
      retValue.LJCSetValues(mClassHeadingGrid, dataRecord);
      return retValue;
    }

    // Adds a grid row and updates it with the result values.
    private LJCGridRow RowAddValues(DbValues dbValues)
    {
      var retValue = mClassHeadingGrid.LJCRowAdd();
      var columnName = DocClassGroupHeading.ColumnID;
      retValue.LJCSetInt32(columnName, dbValues.LJCGetInt32(columnName));

      retValue.LJCSetValues(mClassHeadingGrid, dbValues);
      return retValue;
    }

    // Updates the current row with the record values.
    private void RowUpdate(DocClassGroupHeading dataRecord)
    {
      if (mClassHeadingGrid.CurrentRow is LJCGridRow row)
      {
        SetStoredValues(row, dataRecord);
        row.LJCSetValues(mClassHeadingGrid, dataRecord);
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

    /// <summary>Displays a detail dialog for a new record.</summary>
    internal void DoNew()
    {
      var detail = new ClassHeadingDetail()
      {
        Managers = Managers
      };
      detail.LJCChange += Detail_Change;
      detail.ShowDialog();
    }

    /// <summary>Displays a detail dialog to edit an existing record.</summary>
    internal void DoEdit()
    {
      if (mClassHeadingGrid.CurrentRow is LJCGridRow _)
      {
        var detail = new ClassHeadingDetail()
        {
          LJCHeadingID = ClassHeadingID(),
          Managers = Managers
        };
        detail.LJCChange += Detail_Change;
        detail.ShowDialog();
      }
    }

    /// <summary>Deletes the selected row.</summary>
    internal void DoDelete()
    {
      string title;
      string message;
      bool success = false;

      var classHeadingRow = mClassHeadingGrid.CurrentRow as LJCGridRow;
      if (classHeadingRow != null)
      {
        title = "Delete Confirmation";
        message = FormCommon.DeleteConfirm;
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
          message = FormCommon.DeleteError;
          MessageBox.Show(message, "Delete Error", MessageBoxButtons.OK
            , MessageBoxIcon.Exclamation);
        }
      }

      if (success)
      {
        mClassHeadingGrid.Rows.Remove(classHeadingRow);
      }
    }

    /// <summary>Refreshes the list.</summary>
    internal void DoRefresh()
    {
      mClassHeadingSelect.Cursor = Cursors.WaitCursor;

      // Save the original row.
      var classHeadingID = ClassHeadingID();

      DataRetrieve();
      if (classHeadingID > 0)
      {
        var dataRecord = new DocClassGroupHeading()
        {
          ID = classHeadingID
        };
        RowSelect(dataRecord);
      }
      mClassHeadingSelect.Cursor = Cursors.Default;
    }

    /// <summary>Sets the selected item and returns to the parent form.</summary>
    internal void DoSelect()
    {
      var selectList = mClassHeadingSelect;
      selectList.LJCSelectedRecord = null;
      // *** Begin *** Change - MultiSelect 10/29/23
      var rows = mClassHeadingGrid.SelectedRows;
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
      DoResetSequence();
      selectList.Cursor = Cursors.Default;
      // *** End *** Change - MultiSelect 10/29/23
      selectList.DialogResult = DialogResult.OK;
    }

    /// <summary>Resets the Sequence column values.</summary>
    internal void DoResetSequence()
    {
      var classHeadingManager = Managers.DocClassGroupHeadingManager;
      classHeadingManager.ResetSequence();
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
          CheckPreviousAndNext(detail);
          mClassHeadingGrid.LJCSetCurrentRow(row, true);
          DoRefresh();
        }
      }
    }
    #endregion

    #region Other Methods

    // Retrieves the current row item ID.
    /// <include path='items/ClassHeadingID/*' file='../../Doc/ClassHeadingGridCode.xml'/>
    internal short ClassHeadingID(LJCGridRow classHeadingRow = null)
    {
      short retValue = 0;

      if (null == classHeadingRow)
      {
        classHeadingRow = mClassHeadingGrid.CurrentRow as LJCGridRow;
      }
      if (classHeadingRow != null)
      {
        retValue = (short)classHeadingRow.LJCGetInt32(DocClassGroupHeading.ColumnID);
      }
      return retValue;
    }

    // The DragDrop method.
    /// <include path='items/DoDragDrop/*' file='../../../../LJCDocLib/Common/List.xml'/>
    internal void DoDragDrop(DragEventArgs e)
    {
      var sourceRow = e.Data.GetData(typeof(LJCGridRow)) as LJCGridRow;
      var dragDataName = sourceRow.LJCGetString("DragDataName");
      if (dragDataName == mClassHeadingGrid.LJCDragDataName)
      {
        var targetIndex = mClassHeadingGrid.LJCGetDragRowIndex(new Point(e.X, e.Y));
        if (targetIndex >= 0)
        {
          // Get source group.
          var sourceName = HeadingName(ClassHeadingID(sourceRow));
          var manager = Managers.DocClassGroupHeadingManager;
          var sourceGroup = manager.RetrieveWithUnique(sourceName);

          // Get target group.
          var targetRow = mClassHeadingGrid.Rows[targetIndex] as LJCGridRow;
          var targetName = HeadingName(ClassHeadingID(targetRow));
          var targetGroup = manager.RetrieveWithUnique(targetName);

          var sourceSequence = sourceGroup.Sequence;
          var targetSequence = targetGroup.Sequence;
          manager.ChangeSequence(sourceSequence, targetSequence);
          DoRefresh();
        }
      }
    }

    /// <summary>Setup the grid columns.</summary>
    internal void SetupGrid()
    {
      // *** Next Statement *** Add - MultiSelect 10/29/23
      mClassHeadingGrid.MultiSelect = true;

      // Setup default grid columns if no columns are defined.
      if (0 == mClassHeadingGrid.Columns.Count)
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
        mClassHeadingGrid.LJCAddColumns(GridColumns);
        mClassHeadingGrid.LJCDragDataName = "DocClassGroupHeading";
      }
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
        LJCDataGrid grid = mClassHeadingGrid;
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
        LJCDataGrid grid = mClassHeadingGrid;
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

    /// <summary>Gets or sets the GridColumns value.</summary>
    internal DbColumns GridColumns { get; set; }

    // The Managers object.
    private ManagersDocGen Managers { get; set; }
    #endregion

    #region Class Data

    private readonly LJCDataGrid mClassHeadingGrid;
    private readonly ClassHeadingSelect mClassHeadingSelect;
    #endregion
  }
}
