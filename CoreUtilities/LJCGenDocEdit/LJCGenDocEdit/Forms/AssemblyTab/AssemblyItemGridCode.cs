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
using LJCDBMessage;

namespace LJCGenDocEdit
{
  /// <summary>The AssemblyItem grid code.</summary>
  internal class AssemblyItemGridCode
  {
    #region Constructors

    /// <summary>Initializes an object instance.</summary>
    internal AssemblyItemGridCode(LJCGenDocList parent)
    {
      mParent = parent;
      mGrid = mParent.AssemblyItemGrid;
      Managers = mParent.Managers;
      mParentGrid = mParent.AssemblyGroupGrid;
      DocAssemblyManager = Managers.DocAssemblyManager;
    }
    #endregion

    #region Data Methods

    /// <summary>Retrieves the list rows.</summary>
    internal void DataRetrieve()
    {
      mGrid.LJCRowsClear();

      if (mParent.AssemblyGroupGrid.CurrentRow is LJCGridRow parentRow)
      {
        mParent.Cursor = Cursors.WaitCursor;
        var parentID = ParentID(parentRow);

        var manager = DocAssemblyManager;
        var names = new List<string>()
        {
          DocAssembly.ColumnSequence
        };
        manager.SetOrderBy(names);
        var keyColumns = new DbColumns()
        {
          { DocAssembly.ColumnDocAssemblyGroupID, parentID }
        };
        DbResult result = manager.LoadResult(keyColumns);

        if (DbResult.HasRows(result))
        {
          foreach (DbRow dbRow in result.Rows)
          {
            RowAddValues(dbRow.Values);
          }
        }
        mParent.Cursor = Cursors.Default;
        mParent.DoChange(Change.AssemblyItem);
      }
    }

    // Selects a row based on the key record values.
    /// <include path='items/RowSelect/*' file='../../../../LJCDocLib/Common/List.xml'/>
    internal bool RowSelect(DocAssembly dataRecord)
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
    private LJCGridRow RowAdd(DocAssembly dataRecord)
    {
      var retValue = mGrid.LJCRowAdd();
      SetStored(retValue, dataRecord);
      mGrid.LJCRowSetValues(retValue, dataRecord);
      return retValue;
    }

    // Adds a grid row and updates it with the result values.
    private LJCGridRow RowAddValues(DbValues dbValues)
    {
      var retValue = mGrid.LJCRowAdd();
      var columnName = DocAssembly.ColumnID;
      retValue.LJCSetInt32(columnName, dbValues.LJCGetInt32(columnName));

      mGrid.LJCRowSetValues(retValue, dbValues);
      return retValue;
    }

    // Updates the current row with the record values.
    private void RowUpdate(DocAssembly dataRecord)
    {
      if (mGrid.CurrentRow is LJCGridRow row)
      {
        SetStored(row, dataRecord);
        mGrid.LJCRowSetValues(row, dataRecord);
      }
    }

    // Sets the row stored values.
    private void SetStored(LJCGridRow row, DocAssembly dataRecord)
    {
      row.LJCSetInt32(DocAssembly.ColumnID, dataRecord.ID);
    }
    #endregion

    #region Action Methods

    /// <summary>Displays a detail dialog for a new record.</summary>
    internal void DoNew()
    {
      if (mParentGrid.CurrentRow is LJCGridRow parentRow)
      {
        // Data from items.
        var parentID = ParentID(parentRow);
        var parentData = CurrentParent();
        var parentName = parentData.Heading;

        var detail = new AssemblyDetail()
        {
          LJCParentID = parentID,
          LJCParentName = parentName,
          Managers = Managers
        };
        detail.LJCChange += Detail_Change;
        detail.ShowDialog();
      }
    }

    /// <summary>Displays a detail dialog to edit an existing record.</summary>
    internal void DoEdit()
    {
      if (mParentGrid.CurrentRow is LJCGridRow parentRow
        && mGrid.CurrentRow is LJCGridRow row)
      {
        // Data from items.
        var parentID = ParentID(parentRow);
        var parentData = CurrentParent();
        var parentName = parentData.Heading;
        var id = RowID();

        var detail = new AssemblyDetail()
        {
          LJCID = id,
          LJCParentID = parentID,
          LJCParentName = parentName,
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
        var parentID = ParentID(parentRow);
        var id = RowID();

        var keyRecord = new DbColumns()
        {
          { DocAssembly.ColumnDocAssemblyGroupID, parentID },
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

    /// <summary>Refreshes the list.</summary> 
    internal void DoRefresh()
    {
      mParent.Cursor = Cursors.WaitCursor;
      short id = RowID();
      //DataRetrieve();
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

    /// <summary>Resets the Sequence column values.</summary>
    internal void DoResetSequence()
    {
      var assemblyItem = CurrentItem();
      var manager = Managers.DocAssemblyManager;
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

    // Retrieves the current row item.
    /// <include path='items/CurrentItem/*' file='../../../../LJCDocLib/Common/List.xml'/>
    internal DocAssembly CurrentItem()
    {
      DocAssembly retValue = null;

      if (mGrid.CurrentRow is LJCGridRow _)
      {
        var id = RowID();
        if (id > 0)
        {
          var manager = DocAssemblyManager;
          retValue = manager.RetrieveWithID(id);
        }
      }
      return retValue;
    }

    /// <summary>
    /// Retrieves the current parent row item.
    /// </summary>
    /// <returns>The current parent row item.</returns>
    internal DocAssemblyGroup CurrentParent()
    {
      DocAssemblyGroup retValue = null;

      if (mParentGrid.CurrentRow is LJCGridRow parentRow)
      {
        var id = ParentID(parentRow);
        if (id > 0)
        {
          var manager = Managers.DocAssemblyGroupManager;
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

    /// <summary>
    /// Retrieves the row parent ID.
    /// </summary>
    /// <param name="parentRow">The parent row.</param>
    /// <returns>The parent row ID.</returns>
    internal short ParentID(LJCGridRow parentRow)
    {
      short retValue = 0;

      if (parentRow != null)
      {
        retValue = (short)parentRow.LJCGetInt32(DocAssemblyGroup.ColumnID);
      }
      return retValue;
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
        retValue = (short)row.LJCGetInt32(DocAssembly.ColumnID);
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
    private void PreviousItem(AssemblyDetail detail)
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
    internal DocAssemblyManager DocAssemblyManager { get; set; }

    // The Managers object.
    private ManagersDocGen Managers { get; set; }
    #endregion

    #region Class Data

    private readonly LJCDataGrid mGrid;
    private readonly LJCGenDocList mParent;
    private readonly LJCDataGrid mParentGrid;
    #endregion
  }
}
