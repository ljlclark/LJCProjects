// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// MethodHeadingGridCode.cs
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
  // The MethodHeading grid code.
  internal class MethodHeadingGridCode
  {
    #region Constructors

    /// <summary>Initializes an object instance.</summary>
    internal MethodHeadingGridCode(MethodHeadingSelect parentList)
    {
      mMethodHeadingSelect = parentList;
      Managers = mMethodHeadingSelect.Managers;
      mMethodHeadingGrid = mMethodHeadingSelect.MethodHeadingGrid;
    }
    #endregion

    #region Data Methods

    /// <summary>Retrieves the list rows.</summary>
    internal void DataRetrieve()
    {
      mMethodHeadingSelect.Cursor = Cursors.WaitCursor;
      mMethodHeadingGrid.LJCRowsClear();

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
      mMethodHeadingSelect.SetControlState();
      mMethodHeadingSelect.Cursor = Cursors.Default;
    }

    // Selects a row based on the key record values.
    /// <include path='items/RowSelect/*' file='../../../../LJCDocLib/Common/List.xml'/>
    internal bool RowSelect(DocMethodGroupHeading dataRecord)
    {
      bool retValue = false;

      if (dataRecord != null)
      {
        mMethodHeadingSelect.Cursor = Cursors.WaitCursor;
        foreach (LJCGridRow row in mMethodHeadingGrid.Rows)
        {
          if (MethodHeadingID(row) == dataRecord.ID)
          {
            // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
            mMethodHeadingGrid.LJCSetCurrentRow(row, true);
            retValue = true;
            break;
          }
        }
        mMethodHeadingSelect.Cursor = Cursors.Default;
      }
      return retValue;
    }

    // Adds a grid row and updates it with the record values.
    private LJCGridRow RowAdd(DocMethodGroupHeading dataRecord)
    {
      var retValue = mMethodHeadingGrid.LJCRowAdd();
      SetStoredValues(retValue, dataRecord);
      retValue.LJCSetValues(mMethodHeadingGrid, dataRecord);
      return retValue;
    }

    // Adds a grid row and updates it with the result values.
    private LJCGridRow RowAddValues(DbValues dbValues)
    {
      var retValue = mMethodHeadingGrid.LJCRowAdd();
      var columnName = DocMethodGroupHeading.ColumnID;
      retValue.LJCSetInt32(columnName, dbValues.LJCGetInt32(columnName));

      retValue.LJCSetValues(mMethodHeadingGrid, dbValues);
      return retValue;
    }

    // Updates the current row with the record values.
    private void RowUpdate(DocMethodGroupHeading dataRecord)
    {
      if (mMethodHeadingGrid.CurrentRow is LJCGridRow row)
      {
        SetStoredValues(row, dataRecord);
        row.LJCSetValues(mMethodHeadingGrid, dataRecord);
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

    /// <summary>Displays a detail dialog for a new record.</summary>
    internal void DoNew()
    {
      var detail = new MethodHeadingDetail()
      {
        Managers = Managers
      };
      detail.LJCChange += Detail_Change;
      detail.ShowDialog();
    }

    /// <summary>Displays a detail dialog to edit an existing record.</summary>
    internal void DoEdit()
    {
      if (mMethodHeadingGrid.CurrentRow is LJCGridRow _)
      {
        var detail = new MethodHeadingDetail()
        {
          LJCHeadingID = MethodHeadingID(),
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

      var methodHeadingRow = mMethodHeadingGrid.CurrentRow as LJCGridRow;
      if (methodHeadingRow != null)
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
          { DocMethodGroupHeading.ColumnID, MethodHeadingID() }
        };
        var manager = Managers.DocMethodGroupHeadingManager;
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
        mMethodHeadingGrid.Rows.Remove(methodHeadingRow);
      }
    }

    /// <summary>Refreshes the list.</summary>
    internal void DoRefresh()
    {
      mMethodHeadingSelect.Cursor = Cursors.WaitCursor;

      // Save the original row.
      var methodHeadingID = MethodHeadingID();

      DataRetrieve();
      if (methodHeadingID > 0)
      {
        var dataRecord = new DocMethodGroupHeading()
        {
          ID = methodHeadingID
        };
        RowSelect(dataRecord);
      }
      mMethodHeadingSelect.Cursor = Cursors.Default;
    }

    /// <summary>Sets the selected item and returns to the parent form.</summary>
    internal void DoSelect()
    {
      mMethodHeadingSelect.LJCSelectedRecord = null;
      if (mMethodHeadingGrid.CurrentRow is LJCGridRow _)
      {
        mMethodHeadingSelect.Cursor = Cursors.WaitCursor;
        var manager = Managers.DocMethodGroupHeadingManager;
        var keyRecord = manager.GetIDKey(MethodHeadingID());
        var dataRecord = manager.Retrieve(keyRecord);
        if (dataRecord != null)
        {
          mMethodHeadingSelect.LJCSelectedRecord = dataRecord;
        }
        mMethodHeadingSelect.Cursor = Cursors.Default;
      }
      mMethodHeadingSelect.DialogResult = DialogResult.OK;
    }

    /// <summary>Resets the Sequence column values.</summary>
    internal void DoResetSequence()
    {
      var methodHeadingManager = Managers.DocMethodGroupHeadingManager;
      methodHeadingManager.ResetSequence();
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
          CheckPreviousAndNext(detail);
          mMethodHeadingGrid.LJCSetCurrentRow(row, true);
          DoRefresh();
        }
      }
    }
    #endregion

    #region Other Methods

    // The DragDrop method.
    /// <include path='items/DoDragDrop/*' file='../../../../LJCDocLib/Common/List.xml'/>
    internal void DoDragDrop(DragEventArgs e)
    {
      var sourceRow = e.Data.GetData(typeof(LJCGridRow)) as LJCGridRow;
      var dragDataName = sourceRow.LJCGetString("DragDataName");
      if (dragDataName == mMethodHeadingGrid.LJCDragDataName)
      {
        var targetIndex = mMethodHeadingGrid.LJCGetDragRowIndex(new Point(e.X, e.Y));
        if (targetIndex >= 0)
        {
          // Get source group.
          var sourceName = HeadingName(MethodHeadingID(sourceRow));
          var manager = Managers.DocMethodGroupHeadingManager;
          var sourceGroup = manager.RetrieveWithUnique(sourceName);

          // Get target group.
          var targetRow = mMethodHeadingGrid.Rows[targetIndex] as LJCGridRow;
          var targetName = HeadingName(MethodHeadingID(targetRow));
          var targetGroup = manager.RetrieveWithUnique(targetName);

          var sourceSequence = sourceGroup.Sequence;
          var targetSequence = targetGroup.Sequence;
          manager.ChangeSequence(sourceSequence, targetSequence);
          DoRefresh();
        }
      }
    }

    // Retrieves the current row item ID.
    /// <include path='items/MethodHeadingID/*' file='../../Doc/MethodHeadingGridCode.xml'/>
    internal short MethodHeadingID(LJCGridRow methodheadingRow = null)
    {
      short retValue = 0;

      if (null == methodheadingRow)
      {
        methodheadingRow = mMethodHeadingGrid.CurrentRow as LJCGridRow;
      }
      if (methodheadingRow != null)
      {
        retValue = (short)methodheadingRow.LJCGetInt32(DocMethodGroupHeading.ColumnID);
      }
      return retValue;
    }

    /// <summary>Setup the grid columns.</summary>
    internal void SetupGrid()
    {
      // Setup default grid columns if no columns are defined.
      if (0 == mMethodHeadingGrid.Columns.Count)
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
        mMethodHeadingGrid.LJCAddColumns(GridColumns);
        mMethodHeadingGrid.LJCDragDataName = "DocMethodGroupHeading";
      }
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
        LJCDataGrid grid = mMethodHeadingGrid;
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
        LJCDataGrid grid = mMethodHeadingGrid;
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

    /// <summary>Gets or sets the GridColumns value.</summary>
    internal DbColumns GridColumns { get; set; }

    // The Managers object.
    private ManagersDocGen Managers { get; set; }
    #endregion

    #region Class Data

    private readonly LJCDataGrid mMethodHeadingGrid;
    private readonly MethodHeadingSelect mMethodHeadingSelect;
    #endregion
  }
}
