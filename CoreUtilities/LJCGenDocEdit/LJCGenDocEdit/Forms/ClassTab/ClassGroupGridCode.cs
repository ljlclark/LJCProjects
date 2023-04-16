﻿// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// ClassGroupGridCode.cs
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
  internal class ClassGroupGridCode
  {
    #region Constructors

    // Initializes an object instance.
    internal ClassGroupGridCode(LJCGenDocList parent)
    {
      mParent = parent;
      mGrid = mParent.ClassGroupGrid;
      mManagers = mParent.Managers;
      mParentGrid = mParent.AssemblyItemGrid;
    }
    #endregion

    #region Methods

    /// <summary>
    /// Retrieves the currently selecteditem.
    /// </summary>
    /// <returns>The currently selected item.</returns>
    internal DocClassGroup CurrentItem()
    {
      DocClassGroup retValue = null;

      if (mGrid.CurrentRow is LJCGridRow row)
      {
        var id = (short)row.LJCGetInt32(DocClassGroup.ColumnID);
        if (id > 0)
        {
          var manager = mManagers.DocClassGroupManager;
          retValue = manager.RetrieveWithID(id);
        }
      }
      return retValue;
    }

    // Setup the grid display columns.
    internal void SetupGrid()
    {
      // Setup default display columns if no columns are defined.
      if (0 == mGrid.Columns.Count)
      {
        List<string> columnNames = new List<string>()
        {
          DocClassGroup.ColumnHeadingName,
          DocClassGroup.ColumnHeadingTextCustom
        };

        // Get the display columns from the manager Data Definition.
        var classManager = mManagers.DocClassGroupManager;
        DisplayColumns = classManager.GetColumns(columnNames);

        // Setup the grid display columns.
        mGrid.LJCAddDisplayColumns(DisplayColumns);
      }
    }
    #endregion

    #region Data Methods

    // Retrieves the list rows.
    internal void DataRetrieve()
    {
      mGrid.LJCRowsClear();

      if (mParent.AssemblyItemGrid.CurrentRow is LJCGridRow parentRow)
      {
        mParent.Cursor = Cursors.WaitCursor;
        var parentID = (short)parentRow.LJCGetInt32(DocAssembly.ColumnID);

        var manager = mManagers.DocClassGroupManager;
        var dataRecords = manager.LoadWithParent(parentID);

        if (NetCommon.HasItems(dataRecords))
        {
          foreach (DocClassGroup dataRecord in dataRecords)
          {
            RowAdd(dataRecord);
          }
        }
        mParent.Cursor = Cursors.Default;
        mParent.DoChange(Change.ClassGroup);
        //mParent.TimedChange(Change.ClassGroup);
      }
    }

    // Selects a row based on the key record values.
    /// <summary>
    /// Selects a row based on the key record values.
    /// </summary>
    /// <param name="dataRecord">The data record to select.</param>
    /// <returns>True if the row was selected, otherwise false.</returns>
    internal bool RowSelect(DocClassGroup dataRecord)
    {
      bool retValue = false;

      if (dataRecord != null)
      {
        mParent.Cursor = Cursors.WaitCursor;
        foreach (LJCGridRow row in mGrid.Rows)
        {
          var rowID = row.LJCGetInt32(DocClassGroup.ColumnID);
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
    private LJCGridRow RowAdd(DocClassGroup dataRecord)
    {
      var retValue = mGrid.LJCRowAdd();
      SetStoredValues(retValue, dataRecord);

      // Sets the row values from a data object.
      mGrid.LJCRowSetValues(retValue, dataRecord);
      return retValue;
    }

    // Updates the current row with the record values.
    private void RowUpdate(DocClassGroup dataRecord)
    {
      if (mGrid.CurrentRow is LJCGridRow row)
      {
        SetStoredValues(row, dataRecord);
        mGrid.LJCRowSetValues(row, dataRecord);
      }
    }

    // Sets the row stored values.
    private void SetStoredValues(LJCGridRow row, DocClassGroup dataRecord)
    {
      row.LJCSetInt32(DocClassGroup.ColumnID, dataRecord.ID);
      row.LJCSetString(DocClassGroup.ColumnHeadingName, dataRecord.HeadingName);
      row.LJCSetInt32(DocClassGroup.ColumnDocAssemblyID
        , dataRecord.DocAssemblyID);
    }
    #endregion

    #region Action Methods

    // Displays a detail dialog for a new record.
    internal void DoNew()
    {
      if (mParentGrid.CurrentRow is LJCGridRow parentRow)
      {
        // Data from items.
        var parentID = (short)parentRow.LJCGetInt32(DocAssembly.ColumnID);
        var parentName = parentRow.LJCGetString(DocAssembly.ColumnName);

        var detail = new ClassGroupDetail()
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
        var id = (short)row.LJCGetInt32(DocClassGroup.ColumnID);
        var parentID = (short)parentRow.LJCGetInt32(DocAssembly.ColumnID);
        var parentName = parentRow.LJCGetString(DocAssembly.ColumnName);

        var detail = new ClassGroupDetail()
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
        var groupID = parentRow.LJCGetInt32(DocAssembly.ColumnID);
        var id = row.LJCGetInt32(DocClass.ColumnID);

        var keyRecord = new DbColumns()
        {
          { DocAssembly.ColumnID, groupID },
          { DocClass.ColumnID, id }
        };
        var manager = mManagers.DocClassGroupManager;
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
        mParent.TimedChange(Change.ClassGroup);
      }
    }

    // Refreshes the list.
    internal void DoRefresh()
    {
      short id = 0;

      mParent.Cursor = Cursors.WaitCursor;
      if (mGrid.CurrentRow is LJCGridRow row)
      {
        id = (short)row.LJCGetInt32(DocClassGroup.ColumnID);
      }
      DataRetrieve();

      // Select the original row.
      if (id > 0)
      {
        var dataRecord = new DocClassGroup()
        {
          ID = id
        };
        RowSelect(dataRecord);
      }
      mParent.Cursor = Cursors.Default;
    }

    // Adds new row or updates row with changes from the detail dialog.
    private void Detail_Change(object sender, EventArgs e)
    {
      var detail = sender as ClassGroupDetail;
      if (detail.LJCRecord != null)
      {
        var dataRecord = detail.LJCRecord;
        if (detail.LJCIsUpdate)
        {
          RowUpdate(dataRecord);
        }
        else
        {
          // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
          var row = RowAdd(dataRecord);
          mGrid.LJCSetCurrentRow(row, true);
          mParent.TimedChange(Change.ClassGroup);
        }
      }
    }
    #endregion

    #region Other Methods

    // The DragDrop method.
    internal void DoDragDrop(short assemblyID, DragEventArgs e)
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
          var manager = mManagers.DocClassGroupManager;
          var sourceGroup = manager.RetrieveWithUnique(assemblyID, sourceName);

          // Get target group.
          var targetRow = mGrid.Rows[targetIndex] as LJCGridRow;
          var targetName = targetRow.LJCGetString(DocAssembly.ColumnName);
          var targetGroup = manager.RetrieveWithUnique(assemblyID, targetName);

          var sourceSequence = sourceGroup.Sequence;
          var targetSequence = targetGroup.Sequence;
          manager.ChangeSequence(sourceSequence, targetSequence);
        }
      }
    }
    #endregion

    #region Properties

    internal DbColumns DisplayColumns { get; set; }
    #endregion

    #region Class Data

    private readonly LJCDataGrid mGrid;
    private readonly ManagersDocGen mManagers;
    private readonly LJCGenDocList mParent;
    private readonly LJCDataGrid mParentGrid;
    #endregion
  }
}
