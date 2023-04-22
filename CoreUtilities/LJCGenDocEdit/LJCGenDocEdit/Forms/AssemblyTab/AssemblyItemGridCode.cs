// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// AssemblyItemGridCode.cs
using LJCDocLibDAL;
using LJCNetCommon;
using LJCWinFormControls;
using static LJCGenDocEdit.LJCGenDocList;
using System.Collections.Generic;
using System.Windows.Forms;
using System;
using LJCWinFormCommon;
using System.Drawing;

namespace LJCGenDocEdit
{
  internal class AssemblyItemGridCode
  {
    #region Constructors

    // Initializes an object instance.
    internal AssemblyItemGridCode(LJCGenDocList parent)
    {
      mParent = parent;
      mGrid = mParent.AssemblyItemGrid;
      mManagers = mParent.Managers;
      mParentGrid = mParent.AssemblyGroupGrid;
      DocAssemblyManager = mManagers.DocAssemblyManager;
    }
    #endregion

    #region Data Methods

    // Retrieves the list rows.
    internal void DataRetrieve()
    {
      mGrid.LJCRowsClear();

      if (mParent.AssemblyGroupGrid.CurrentRow is LJCGridRow parentRow)
      {
        mParent.Cursor = Cursors.WaitCursor;
        var parentID = (short)parentRow.LJCGetInt32(DocAssembly.ColumnID);

        var manager = DocAssemblyManager;
        var names = new List<string>()
        {
          DocAssembly.ColumnSequence
        };
        manager.SetOrderBy(names);
        var dataRecords = manager.LoadWithParent(parentID);

        if (NetCommon.HasItems(dataRecords))
        {
          foreach (DocAssembly dataRecord in dataRecords)
          {
            RowAdd(dataRecord);
          }
        }
        mParent.Cursor = Cursors.Default;
        mParent.DoChange(Change.AssemblyItem);
      }
    }

    // Selects a row based on the key record values.
    /// <summary>
    /// Selects a row based on the key record values.
    /// </summary>
    /// <param name="dataRecord">The data record to select.</param>
    /// <returns>True if the row was selected, otherwise false.</returns>
    internal bool RowSelect(DocAssembly dataRecord)
    {
      bool retValue = false;

      if (dataRecord != null)
      {
        mParent.Cursor = Cursors.WaitCursor;
        foreach (LJCGridRow row in mGrid.Rows)
        {
          var rowID = row.LJCGetInt32(DocAssembly.ColumnID);
          if (rowID == dataRecord.ID)
          {
            // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
            mGrid.LJCSetCurrentRow(row, true);
            retValue = true;
            break;
          }
        }
        mParent.Cursor = Cursors.Default;
      }
      return retValue;
    }

    // Adds a grid row and updates it with the record values.
    private LJCGridRow RowAdd(DocAssembly dataRecord)
    {
      var retValue = mGrid.LJCRowAdd();
      SetStoredValues(retValue, dataRecord);

      // Sets the row values from a data object.
      mGrid.LJCRowSetValues(retValue, dataRecord);
      return retValue;
    }

    // Updates the current row with the record values.
    private void RowUpdate(DocAssembly dataRecord)
    {
      if (mGrid.CurrentRow is LJCGridRow row)
      {
        SetStoredValues(row, dataRecord);
        mGrid.LJCRowSetValues(row, dataRecord);
      }
    }

    // Sets the row stored values.
    private void SetStoredValues(LJCGridRow row, DocAssembly dataRecord)
    {
      row.LJCSetInt32(DocAssembly.ColumnID, dataRecord.ID);
      row.LJCSetString(DocAssembly.ColumnName, dataRecord.Name);
    }
    #endregion

    #region Action Methods

    // Displays a detail dialog for a new record.
    internal void DoNew()
    {
      if (mParentGrid.CurrentRow is LJCGridRow parentRow)
      {
        // Data from items.
        var parentID = (short)parentRow.LJCGetInt32(DocAssemblyGroup.ColumnID);
        var parentName = parentRow.LJCGetString(DocAssemblyGroup.ColumnHeading);

        var detail = new AssemblyDetail()
        {
          LJCParentID = parentID,
          LJCParentName = parentName,
          Managers = mManagers
        };
        detail.LJCChange += Detail_Change;
        detail.ShowDialog();
      }
    }

    // Displays a detail dialog to edit an existing record.
    internal void DoEdit()
    {
      if (mParentGrid.CurrentRow is LJCGridRow parentRow
        && mGrid.CurrentRow is LJCGridRow row)
      {
        // Data from items.
        var parentID = (short)parentRow.LJCGetInt32(DocAssemblyGroup.ColumnID);
        var parentName = parentRow.LJCGetString(DocAssemblyGroup.ColumnHeading);
        var id = (short)row.LJCGetInt32(DocAssembly.ColumnID);

        var detail = new AssemblyDetail()
        {
          LJCID = id,
          LJCParentID = parentID,
          LJCParentName = parentName,
          Managers = mManagers
        };
        detail.LJCChange += Detail_Change;
        detail.ShowDialog();
      }
    }

    // Deletes the selected row.
    internal void DoDelete()
    {
      string title;
      string message;
      bool success = false;

      var parentRow = mParentGrid.CurrentRow as LJCGridRow;
      var row = mGrid.CurrentRow as LJCGridRow;
      if (parentRow != null
        && row != null)
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
        // Data from items.
        var parentID = parentRow.LJCGetInt32(DocAssemblyGroup.ColumnID);
        var id = row.LJCGetInt32(DocAssembly.ColumnID);

        var keyRecord = new DbColumns()
        {
          { DocAssemblyGroup.ColumnID, parentID },
          { DocAssembly.ColumnID, id }
        };
        var manager = DocAssemblyManager;
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
        mGrid.Rows.Remove(row);
        mParent.TimedChange(Change.AssemblyItem);
      }
    }

    // Refreshes the list.
    internal void DoRefresh()
    {
      short id = 0;

      mParent.Cursor = Cursors.WaitCursor;
      if (mGrid.CurrentRow is LJCGridRow row)
      {
        id = (short)row.LJCGetInt32(DocAssembly.ColumnID);
      }
      DataRetrieve();

      // Select the original row.
      if (id > 0)
      {
        var dataRecord = new DocAssembly()
        {
          ID = id
        };
        RowSelect(dataRecord);
      }
      mParent.Cursor = Cursors.Default;
    }

    internal void DoResetSequence()
    {
      var assemblyItem = CurrentItem();
      var manager = mManagers.DocAssemblyManager;
      manager.AssemblyGroupID = assemblyItem.DocAssemblyGroupID;
      manager.ResetSequence();
    }

    // Adds new row or updates row with changes from the detail dialog.
    private void Detail_Change(object sender, EventArgs e)
    {
      var detail = sender as AssemblyDetail;
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
          mGrid.LJCSetCurrentRow(row, true);
          DoRefresh();
          mParent.TimedChange(Change.AssemblyItem);
        }
      }
    }
    #endregion

    #region Other Methods

    /// <summary>
    /// Retrieves the currently selecteditem.
    /// </summary>
    /// <returns>The currently selected item.</returns>
    internal DocAssembly CurrentItem()
    {
      DocAssembly retValue = null;

      if (mGrid.CurrentRow is LJCGridRow row)
      {
        var id = (short)row.LJCGetInt32(DocAssembly.ColumnID);
        if (id > 0)
        {
          var manager = DocAssemblyManager;
          retValue = manager.RetrieveWithID(id);
        }
      }
      return retValue;
    }

    // The DragDrop method.
    internal void DoDragDrop(DragEventArgs e)
    {
      var sourceRow = e.Data.GetData(typeof(LJCGridRow)) as LJCGridRow;
      var dragDataName = sourceRow.LJCGetString("DragDataName");
      if (dragDataName == mGrid.LJCDragDataName)
      {
        var targetIndex = mGrid.LJCGetDragRowIndex(new Point(e.X, e.Y));
        if (targetIndex >= 0)
        {
          // Get source group.
          var sourceName = sourceRow.LJCGetString(DocAssembly.ColumnName);
          var manager = DocAssemblyManager;
          var sourceGroup = manager.RetrieveWithName(sourceName);

          // Get target group.
          var targetRow = mGrid.Rows[targetIndex] as LJCGridRow;
          var targetName = targetRow.LJCGetString(DocAssembly.ColumnName);
          var targetGroup = manager.RetrieveWithName(targetName);

          var sourceSequence = sourceGroup.Sequence;
          var targetSequence = targetGroup.Sequence;
          manager.AssemblyGroupID = sourceGroup.DocAssemblyGroupID;
          manager.ChangeSequence(sourceSequence, targetSequence);
          DoRefresh();
        }
      }
    }

    // Setup the grid display columns.
    internal void SetupGrid()
    {
      // Setup default display columns if no columns are defined.
      if (0 == mGrid.Columns.Count)
      {
        List<string> columnNames = new List<string>()
        {
          DocAssembly.ColumnName,
          DocAssembly.ColumnDescription
        };

        // Get the display columns from the manager Data Definition.
        var assemblyManager = DocAssemblyManager;
        DisplayColumns = assemblyManager.GetColumns(columnNames);

        // Setup the grid display columns.
        mGrid.LJCAddDisplayColumns(DisplayColumns);
        mGrid.LJCDragDataName = "DocAssembly";
      }
    }
    #endregion

    #region Private Methods

    // Checks for Previous and Next items.
    private void CheckPreviousAndNext(AssemblyDetail detail)
    {
      PreviousItem(detail);
      NextItem(detail);
    }

    // Checks for Next item.
    private void NextItem(AssemblyDetail detail)
    {
      if (detail.LJCNext)
      {
        int currentIndex = mGrid.CurrentRow.Index;
        detail.LJCNext = false;
        if (currentIndex < mGrid.Rows.Count - 1)
        {
          mGrid.LJCSetCurrentRow(currentIndex + 1, true);
          var row = mGrid.CurrentRow as LJCGridRow;
          var id = (short)row.LJCGetInt32(DocAssembly.ColumnID);
          if (id > 0)
          {
            detail.LJCNext = true;
            detail.LJCID = id;
          }
        }
      }
    }

    // Checks for Previous item.
    private void PreviousItem(AssemblyDetail detail)
    {
      if (detail.LJCPrevious)
      {
        int currentIndex = mGrid.CurrentRow.Index;
        detail.LJCPrevious = false;
        if (currentIndex > 0)
        {
          mGrid.LJCSetCurrentRow(currentIndex - 1, true);
          var row = mGrid.CurrentRow as LJCGridRow;
          var id = (short)row.LJCGetInt32(DocAssembly.ColumnID);
          if (id > 0)
          {
            detail.LJCPrevious = true;
            detail.LJCID = id;
          }
        }
      }
    }
    #endregion

    #region Properties
    internal DbColumns DisplayColumns { get; set; }

    internal DocAssemblyManager DocAssemblyManager { get; set; }
    #endregion

    #region Class Data

    private readonly LJCDataGrid mGrid;
    private readonly ManagersDocGen mManagers;
    private readonly LJCGenDocList mParent;
    private readonly LJCDataGrid mParentGrid;
    #endregion
  }
}
