// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// AssemblyGroupGridCode.cs
using LJCDocLibDAL;
using LJCNetCommon;
using LJCWinFormControls;
using static LJCGenDocEdit.LJCGenDocList;
using System.Windows.Forms;
using System.Collections.Generic;
using System;
using LJCWinFormCommon;
using System.Drawing;

namespace LJCGenDocEdit
{
  /// <summary>The AssemblyGroup grid code.</summary>
  internal class AssemblyGroupGridCode
  {
    #region Constructors

    /// <summary>Initializes an object instance.</summary>
    internal AssemblyGroupGridCode(LJCGenDocList parent)
    {
      mParent = parent;
      mGrid = mParent.AssemblyGroupGrid;
      Managers = mParent.Managers;
      DocAssemblyGroupManager = Managers.DocAssemblyGroupManager;
    }
    #endregion

    #region Data Methods

    /// <summary>Retrieves the list rows.</summary>
    internal void DataRetrieve()
    {
      mParent.Cursor = Cursors.WaitCursor;
      mGrid.LJCRowsClear();

      var manager = DocAssemblyGroupManager;
      var names = new List<string>()
      {
        DocAssemblyGroup.ColumnSequence
      };
      manager.SetOrderBy(names);
      var dataRecords = manager.Load();

      if (NetCommon.HasItems(dataRecords))
      {
        foreach (DocAssemblyGroup dataRecord in dataRecords)
        {
          RowAdd(dataRecord);
        }
      }
      mParent.Cursor = Cursors.Default;
      mParent.DoChange(Change.AssemblyGroup);
    }

    // Selects a row based on the key record values.
    /// <include path='items/RowSelect/*' file='../../../../LJCDocLib/Common/List.xml'/>
    internal bool RowSelect(DocAssemblyGroup dataRecord)
    {
      bool retValue = false;

      if (dataRecord != null)
      {
        mParent.Cursor = Cursors.WaitCursor;
        foreach (LJCGridRow row in mGrid.Rows)
        {
          var rowID = RowID(row);
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
    private LJCGridRow RowAdd(DocAssemblyGroup dataRecord)
    {
      var retValue = mGrid.LJCRowAdd();
      SetStoredValues(retValue, dataRecord);

      // Sets the row values from a data object.
      mGrid.LJCRowSetValues(retValue, dataRecord);
      return retValue;
    }

    // Updates the current row with the record values.
    private void RowUpdate(DocAssemblyGroup dataRecord)
    {
      if (mGrid.CurrentRow is LJCGridRow row)
      {
        SetStoredValues(row, dataRecord);
        mGrid.LJCRowSetValues(row, dataRecord);
      }
    }

    // Sets the row stored values.
    private void SetStoredValues(LJCGridRow row, DocAssemblyGroup dataRecord)
    {
      row.LJCSetInt32(DocAssemblyGroup.ColumnID, dataRecord.ID);
      row.LJCSetString(DocAssemblyGroup.ColumnName, dataRecord.Name);
      row.LJCSetString(DocAssemblyGroup.ColumnHeading, dataRecord.Heading);
    }
    #endregion

    #region Action Methods

    /// <summary>Displays a detail dialog for a new record.</summary>
    internal void DoNew()
    {
      var detail = new AssemblyGroupDetail()
      {
        Managers = Managers
      };
      detail.LJCChange += Detail_Change;
      detail.ShowDialog();
    }

    /// <summary>Displays a detail dialog to edit an existing record.</summary>
    internal void DoEdit()
    {
      if (mGrid.CurrentRow is LJCGridRow row)
      {
        // Data from items.
        var id = RowID();

        var detail = new AssemblyGroupDetail()
        {
          LJCID = id,
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

      var row = mGrid.CurrentRow as LJCGridRow;
      if (row != null)
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
        var id = RowID();

        var keyRecord = new DbColumns()
        {
          { DocAssemblyGroup.ColumnID, id }
        };
        var manager = DocAssemblyGroupManager;
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
        mParent.TimedChange(Change.AssemblyGroup);
      }
    }

    /// <summary>Refreshes the list.</summary> 
    internal void DoRefresh()
    {
      mParent.Cursor = Cursors.WaitCursor;
      short id = RowID();
      DataRetrieve();

      // Select the original row.
      if (id > 0)
      {
        var dataRecord = new DocAssemblyGroup()
        {
          ID = id
        };
        RowSelect(dataRecord);
      }
      mParent.Cursor = Cursors.Default;
    }

    /// <summary>Resets the Sequence column values.</summary>
    internal void DoResetSequence()
    {
      DocAssemblyGroupManager.ResetSequence();
    }

    // Adds new row or updates row with changes from the detail dialog.
    private void Detail_Change(object sender, EventArgs e)
    {
      var detail = sender as AssemblyGroupDetail;
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
          mParent.TimedChange(Change.AssemblyGroup);
        }
      }
    }
    #endregion

    #region Other Methods

    // Retrieves the current row item.
    /// <include path='items/CurrentItem/*' file='../../../../LJCDocLib/Common/List.xml'/>
    internal DocAssemblyGroup CurrentItem()
    {
      DocAssemblyGroup retValue = null;

      if (mGrid.CurrentRow is LJCGridRow _)
      {
        var id = RowID();
        if (id > 0)
        {
          var manager = DocAssemblyGroupManager;
          retValue = manager.RetrieveWithID(id);
        }
      }
      return retValue;
    }

    // The DragDrop method.
    /// <include path='items/DoDragDrop/*' file='../../../../LJCDocLib/Common/List.xml'/>
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
          var sourceName = sourceRow.LJCGetString(DocAssemblyGroup.ColumnName);
          var manager = Managers.DocAssemblyGroupManager;
          var sourceGroup = manager.RetrieveWithUnique(sourceName);

          // Get target group.
          var targetRow = mGrid.Rows[targetIndex] as LJCGridRow;
          var targetName = targetRow.LJCGetString(DocGenGroup.ColumnName);
          var targetGroup = manager.RetrieveWithUnique(targetName);

          var sourceSequence = sourceGroup.Sequence;
          var targetSequence = targetGroup.Sequence;
          manager.ChangeSequence(sourceSequence, targetSequence);
          DoRefresh();
        }
      }
    }

    // Retrieves the current row item ID.
    /// <include path='items/RowID/*' file='../../../../LJCDocLib/Common/List.xml'/>
    internal short RowID(LJCGridRow row = null)
    {
      short retValue = 0;

      if (null == row)
      {
        row = mGrid.CurrentRow as LJCGridRow;
      }
      if (row != null)
      {
        retValue = (short)row.LJCGetInt32(DocAssemblyGroup.ColumnID);
      }
      return retValue;
    }

    /// <summary>Setup the grid display columns.</summary>
    internal void SetupGrid()
    {
      // Setup default display columns if no columns are defined.
      if (0 == mGrid.Columns.Count)
      {
        List<string> columnNames = new List<string>()
        {
          DocAssemblyGroup.ColumnName,
          DocAssemblyGroup.ColumnHeading,
          DocAssemblyGroup.ColumnSequence
        };

        // Get the display columns from the manager Data Definition.
        var manager = DocAssemblyGroupManager;
        DisplayColumns = manager.GetColumns(columnNames);

        // Setup the grid display columns.
        mGrid.LJCAddDisplayColumns(DisplayColumns);
        mGrid.LJCDragDataName = "DocAssemblyGroup";
      }
    }
    #endregion

    #region Private Methods

    // Checks for Previous and Next items.
    private void CheckPreviousAndNext(AssemblyGroupDetail detail)
    {
      PreviousItem(detail);
      NextItem(detail);
    }

    // Checks for Next item.
    private void NextItem(AssemblyGroupDetail detail)
    {
      if (detail.LJCNext)
      {
        int currentIndex = mGrid.CurrentRow.Index;
        detail.LJCNext = false;
        if (currentIndex < mGrid.Rows.Count - 1)
        {
          mGrid.LJCSetCurrentRow(currentIndex + 1, true);
          var id = RowID();
          if (id > 0)
          {
            detail.LJCNext = true;
            detail.LJCID = id;
          }
        }
      }
    }

    // Checks for Previous item.
    private void PreviousItem(AssemblyGroupDetail detail)
    {
      if (detail.LJCPrevious)
      {
        int currentIndex = mGrid.CurrentRow.Index;
        detail.LJCPrevious = false;
        if (currentIndex > 0)
        {
          mGrid.LJCSetCurrentRow(currentIndex - 1, true);
          var id = RowID();
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

    /// <summary>Gets or sets the DisplayColumns value.</summary>
    internal DbColumns DisplayColumns { get; set; }

    /// <summary>Gets or sets the Manager value.</summary>
    internal DocAssemblyGroupManager DocAssemblyGroupManager { get; set; }

    // The Managers object.
    private ManagersDocGen Managers { get; set; }
    #endregion

    #region Class Data

    private readonly LJCDataGrid mGrid;
    private readonly LJCGenDocList mParent;
    #endregion
  }
}
