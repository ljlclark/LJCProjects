// Copyright(c) Lester J.Clark and Contributors.
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
using LJCDBMessage;

namespace LJCGenDocEdit
{
  /// <summary>The ClassGroup grid code.</summary>
  internal class ClassGroupGridCode
  {
    #region Constructors

    /// <summary>Initializes an object instance.</summary>
    internal ClassGroupGridCode(LJCGenDocList parentList)
    {
      mDocList = parentList;
      mAssemblyGrid = mDocList.AssemblyItemGrid;
      mGroupGrid = mDocList.ClassGroupGrid;
      Managers = mDocList.Managers;
    }
    #endregion

    #region Data Methods

    /// <summary>Retrieves the list rows.</summary>
    internal void DataRetrieve()
    {
      mGroupGrid.LJCRowsClear();

      if (mDocList.AssemblyItemGrid.CurrentRow is LJCGridRow assemblyRow)
      {
        mDocList.Cursor = Cursors.WaitCursor;
        var assemblyID = AssemblyID(assemblyRow);

        var docClassGroup = new DocClassGroup()
        {
          ID = 0,
          HeadingName = "Ungrouped Classes",
          DocAssemblyID = assemblyID
        };
        RowAdd(docClassGroup);

        var manager = Managers.DocClassGroupManager;
        var names = new List<string>()
        {
          DocClassGroup.ColumnSequence
        };
        manager.SetOrderBy(names);

        var keyColumns = new DbColumns()
        {
          { DocClassGroup.ColumnDocAssemblyID, assemblyID }
        };
        DbResult result = manager.LoadResult(keyColumns);

        if (DbResult.HasRows(result))
        {
          foreach (DbRow dbRow in result.Rows)
          {
            RowAddValues(dbRow.Values);
          }
        }
        mDocList.Cursor = Cursors.Default;
        mDocList.DoChange(Change.ClassGroup);
      }
    }

    // Selects a row based on the key record values.
    /// <include path='items/RowSelect/*' file='../../../../LJCDocLib/Common/List.xml'/>
    internal bool RowSelect(DocClassGroup dataRecord)
    {
      bool retValue = false;

      if (dataRecord != null)
      {
        mDocList.Cursor = Cursors.WaitCursor;
        foreach (LJCGridRow row in mGroupGrid.Rows)
        {
          var rowID = GroupID(row);
          if (rowID == dataRecord.ID)
          {
            // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
            mGroupGrid.LJCSetCurrentRow(row, true);
            retValue = true;
            break;
          }
        }
        mDocList.Cursor = Cursors.Default;
      }
      return retValue;
    }

    // Adds a grid row and updates it with the record values.
    private LJCGridRow RowAdd(DocClassGroup dataRecord)
    {
      var retValue = mGroupGrid.LJCRowAdd();
      SetStoredValues(retValue, dataRecord);
      mGroupGrid.LJCRowSetValues(retValue, dataRecord);
      return retValue;
    }

    // Adds a grid row and updates it with the result values.
    private LJCGridRow RowAddValues(DbValues dbValues)
    {
      var retValue = mGroupGrid.LJCRowAdd();
      var columnName = DocClassGroup.ColumnID;
      retValue.LJCSetInt32(columnName, dbValues.LJCGetInt32(columnName));
      columnName = DocClassGroup.ColumnHeadingName;
      retValue.LJCSetString(columnName, dbValues.LJCGetValue(columnName));
      columnName = DocClassGroup.ColumnDocAssemblyID;
      retValue.LJCSetInt32(columnName, dbValues.LJCGetInt32(columnName));

      mGroupGrid.LJCRowSetValues(retValue, dbValues);
      return retValue;
    }

    // Updates the current row with the record values.
    private void RowUpdate(DocClassGroup dataRecord)
    {
      if (mGroupGrid.CurrentRow is LJCGridRow row)
      {
        SetStoredValues(row, dataRecord);
        mGroupGrid.LJCRowSetValues(row, dataRecord);
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

    /// <summary>Displays a detail dialog for a new record.</summary>
    internal void DoNew()
    {
      if (mAssemblyGrid.CurrentRow is LJCGridRow parentRow)
      {
        // Data from items. (DocAssembly)
        var assemblyID = AssemblyID(parentRow);
        string assemblyName = null;
        if (assemblyID > 0)
        {
          var assemblyData = CurrentAssembly();
          assemblyName = assemblyData.Name;
        }

        var detail = new ClassGroupDetail()
        {
          LJCAssemblyID = assemblyID,
          LJCAssemblyName = assemblyName,
          Managers = Managers
        };
        detail.LJCChange += Detail_Change;
        detail.ShowDialog();
      }
    }

    /// <summary>Displays a detail dialog to edit an existing record.</summary>
    internal void DoEdit()
    {
      if (mAssemblyGrid.CurrentRow is LJCGridRow assemblyRow
        && mGroupGrid.CurrentRow is LJCGridRow row)
      {
        // Data from items. (DocAssembly)
        var assemblyID = AssemblyID(assemblyRow);
        string assemblyName = null;
        if (assemblyID > 0)
        {
          var assemblyData = CurrentAssembly();
          assemblyName = assemblyData.Name;
        }
        var id = GroupID();

        var detail = new ClassGroupDetail()
        {
          LJCID = id,
          LJCAssemblyID = assemblyID,
          LJCAssemblyName = assemblyName,
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

      var assemblyRow = mAssemblyGrid.CurrentRow as LJCGridRow;
      var row = mGroupGrid.CurrentRow as LJCGridRow;
      if (assemblyRow != null
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
        var assemblyID = AssemblyID(assemblyRow);
        var id = GroupID();

        var keyRecord = new DbColumns()
        {
          { DocAssembly.ColumnID, assemblyID },
          { DocClass.ColumnID, id }
        };
        var manager = Managers.DocClassGroupManager;
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
        mGroupGrid.Rows.Remove(row);
        mDocList.TimedChange(Change.ClassGroup);
      }
    }

    /// <summary>Refreshes the list.</summary> 
    internal void DoRefresh()
    {
      mDocList.Cursor = Cursors.WaitCursor;
      short id = GroupID();
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
      mDocList.Cursor = Cursors.Default;
    }

    /// <summary>Resets the Sequence column values.</summary>
    internal void DoResetSequence()
    {
      var classGroup = CurrentGroup();
      var manager = Managers.DocClassGroupManager;
      manager.AssemblyID = classGroup.DocAssemblyID;
      manager.ResetSequence();
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
          CheckPreviousAndNext(detail);
          DoRefresh();
        }
        else
        {
          // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
          var row = RowAdd(dataRecord);
          CheckPreviousAndNext(detail);
          mGroupGrid.LJCSetCurrentRow(row, true);
          DoRefresh();
          mDocList.TimedChange(Change.ClassGroup);
        }
      }
    }
    #endregion

    #region Other Methods

    // Retrieves the current row item.
    /// <include path='items/CurrentItem/*' file='../../../../LJCDocLib/Common/List.xml'/>
    internal DocClassGroup CurrentGroup()
    {
      DocClassGroup retValue = null;

      if (mGroupGrid.CurrentRow is LJCGridRow _)
      {
        var id = GroupID();
        if (id > 0)
        {
          var manager = Managers.DocClassGroupManager;
          retValue = manager.RetrieveWithID(id);
        }
      }
      return retValue;
    }

    /// <summary>
    /// Retrieves the current parent Assembly row item.
    /// </summary>
    /// <returns>The current parent Assembly row item.</returns>
    internal DocAssembly CurrentAssembly()
    {
      DocAssembly retValue = null;

      if (mAssemblyGrid.CurrentRow is LJCGridRow assemblyRow)
      {
        var id = AssemblyID(assemblyRow);
        if (id > 0)
        {
          var manager = Managers.DocAssemblyManager;
          retValue = manager.RetrieveWithID(id);
        }
      }
      return retValue;
    }

    // The DragDrop method.
    /// <include path='items/DoDragDrop1/*' file='../../../../LJCDocLib/Common/List.xml'/>
    internal void DoDragDrop(short parentID, DragEventArgs e)
    {
      var sourceRow = e.Data.GetData(typeof(LJCGridRow)) as LJCGridRow;
      var dragDataName = sourceRow.LJCGetString("DragDataName");
      if (dragDataName == mGroupGrid.LJCDragDataName)
      {
        var targetIndex = mGroupGrid.LJCGetDragRowIndex(new Point(e.X, e.Y));
        if (targetIndex >= 0)
        {
          // Get source group.
          var sourceName = sourceRow.LJCGetString(DocClassGroup.ColumnHeadingName);
          var manager = Managers.DocClassGroupManager;
          var sourceGroup = manager.RetrieveWithUnique(parentID, sourceName);

          // Get target group.
          var targetRow = mGroupGrid.Rows[targetIndex] as LJCGridRow;
          var targetName = targetRow.LJCGetString(DocClassGroup.ColumnHeadingName);
          var targetGroup = manager.RetrieveWithUnique(parentID, targetName);

          var sourceSequence = sourceGroup.Sequence;
          var targetSequence = targetGroup.Sequence;
          manager.AssemblyID = sourceGroup.DocAssemblyID;
          manager.ChangeSequence(sourceSequence, targetSequence);
          DoRefresh();
        }
      }
    }

    /// <summary>
    /// Retrieves the parent Assembly ID.
    /// </summary>
    /// <param name="assemblyRow">The parent Assembly row.</param>
    /// <returns>The parent Assembly row ID.</returns>
    internal short AssemblyID(LJCGridRow assemblyRow)
    {
      short retValue = 0;

      if (assemblyRow != null)
      {
        retValue = (short)assemblyRow.LJCGetInt32(DocAssembly.ColumnID);
      }
      return retValue;
    }

    // Retrieves the current row item ID.
    /// <include path='items/RowID/*' file='../../../../LJCDocLib/Common/List.xml'/>
    internal short GroupID(LJCGridRow row = null)
    {
      short retValue = 0;

      if (null == row)
      {
        row = mGroupGrid.CurrentRow as LJCGridRow;
      }
      if (row != null)
      {
        retValue = (short)row.LJCGetInt32(DocClassGroup.ColumnID);
      }
      return retValue;
    }

    /// <summary>Setup the grid display columns.</summary>
    internal void SetupGrid()
    {
      // Setup default display columns if no columns are defined.
      if (0 == mGroupGrid.Columns.Count)
      {
        List<string> columnNames = new List<string>()
        {
          DocClassGroup.ColumnHeadingName,
          DocClassGroup.ColumnHeadingTextCustom
        };

        // Get the display columns from the manager Data Definition.
        var classManager = Managers.DocClassGroupManager;
        DisplayColumns = classManager.GetColumns(columnNames);

        // Setup the grid display columns.
        mGroupGrid.LJCAddDisplayColumns(DisplayColumns);
        mGroupGrid.LJCDragDataName = "DocClassGroup";
      }
    }
    #endregion

    #region Private Methods

    // Checks for Previous and Next items.
    private void CheckPreviousAndNext(ClassGroupDetail detail)
    {
      PreviousItem(detail);
      NextItem(detail);
    }

    // Checks for Next item.
    private void NextItem(ClassGroupDetail detail)
    {
      if (detail.LJCNext)
      {
        LJCDataGrid grid = mGroupGrid;
        int currentIndex = grid.CurrentRow.Index;
        detail.LJCNext = false;
        if (currentIndex < grid.Rows.Count - 1)
        {
          grid.LJCSetCurrentRow(currentIndex + 1, true);
          var id = GroupID();
          if (id > 0)
          {
            detail.LJCNext = true;
            detail.LJCID = id;
          }
        }
      }
    }

    // Checks for Previous item.
    private void PreviousItem(ClassGroupDetail detail)
    {
      if (detail.LJCPrevious)
      {
        LJCDataGrid grid = mGroupGrid;
        int currentIndex = grid.CurrentRow.Index;
        detail.LJCPrevious = false;
        if (currentIndex > 0)
        {
          grid.LJCSetCurrentRow(currentIndex - 1, true);
          var id = GroupID();
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

    // The Managers object.
    private ManagersDocGen Managers { get; set; }
    #endregion

    #region Class Data

    private readonly LJCDataGrid mAssemblyGrid;
    private readonly LJCGenDocList mDocList;
    private readonly LJCDataGrid mGroupGrid;
    #endregion
  }
}
