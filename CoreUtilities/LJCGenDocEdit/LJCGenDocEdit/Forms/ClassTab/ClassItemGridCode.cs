// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// ClassItemGridCode.cs
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
  /// <summary>The ClassItem grid code.</summary>
  internal class ClassItemGridCode
  {
    #region Constructors

    /// <summary>Initializes an object instance.</summary>
    internal ClassItemGridCode(LJCGenDocList parentList)
    {
      mDocList = parentList;
      mClassGrid = mDocList.ClassItemGrid;
      mGroupGrid = mDocList.ClassGroupGrid;
      Managers = mDocList.Managers;
    }
    #endregion

    #region Data Methods

    /// <summary>Retrieves the list rows.</summary>
    internal void DataRetrieve()
    {
      mClassGrid.LJCRowsClear();

      mDocList.Cursor = Cursors.WaitCursor;
      var groupRow = mGroupGrid.CurrentRow as LJCGridRow;
      var groupID = GroupID();

      var manager = Managers.DocClassManager;
      var names = new List<string>()
      {
        DocClass.ColumnSequence
      };
      manager.SetOrderBy(names);

      DbColumns keyColumns;
      if (0 == groupID)
      {
        // Get ungrouped classes.
        var assemblyID = AssemblyID(groupRow);
        keyColumns = new DbColumns()
        {
          { DocClass.ColumnDocClassGroupID, (object)"'-null'" },
          { DocClass.ColumnDocAssemblyID, assemblyID }
        };
      }
      else
      {
        keyColumns = new DbColumns()
        {
          { DocClass.ColumnDocClassGroupID, groupID }
        };
      }
      DbResult result = manager.LoadResult(keyColumns);

      if (DbResult.HasRows(result))
      {
        foreach (DbRow dbRow in result.Rows)
        {
          RowAddValues(dbRow.Values);
        }
      }
      mDocList.Cursor = Cursors.Default;
      mDocList.DoChange(Change.ClassItem);
    }

    // Selects a row based on the key record values.
    /// <include path='items/RowSelect/*' file='../../../../LJCDocLib/Common/List.xml'/>
    internal bool RowSelect(DocClass dataRecord)
    {
      bool retValue = false;

      if (dataRecord != null)
      {
        mDocList.Cursor = Cursors.WaitCursor;
        foreach (LJCGridRow row in mClassGrid.Rows)
        {
          var rowID = ClassID(row);
          if (rowID == dataRecord.ID)
          {
            // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
            mClassGrid.LJCSetCurrentRow(row, true);
            retValue = true;
            break;
          }
        }
        mDocList.Cursor = Cursors.Default;
      }
      return retValue;
    }

    // Adds a grid row and updates it with the record values.
    private LJCGridRow RowAdd(DocClass dataRecord)
    {
      var retValue = mClassGrid.LJCRowAdd();
      SetStoredValues(retValue, dataRecord);
      mClassGrid.LJCRowSetValues(retValue, dataRecord);
      return retValue;
    }

    // Adds a grid row and updates it with the result values.
    private LJCGridRow RowAddValues(DbValues dbValues)
    {
      var retValue = mClassGrid.LJCRowAdd();
      var columnName = DocClass.ColumnID;
      retValue.LJCSetInt32(columnName, dbValues.LJCGetInt32(columnName));
      columnName = DocClass.ColumnName;
      retValue.LJCSetString(columnName, dbValues.LJCGetValue(columnName));

      mClassGrid.LJCRowSetValues(retValue, dbValues);
      return retValue;
    }

    // Updates the current row with the record values.
    private void RowUpdate(DocClass dataRecord)
    {
      if (mClassGrid.CurrentRow is LJCGridRow row)
      {
        SetStoredValues(row, dataRecord);
        mClassGrid.LJCRowSetValues(row, dataRecord);
      }
    }

    // Sets the row stored values.
    private void SetStoredValues(LJCGridRow row, DocClass dataRecord)
    {
      row.LJCSetInt32(DocClass.ColumnID, dataRecord.ID);
      row.LJCSetString(DocClass.ColumnName, dataRecord.Name);
    }
    #endregion

    #region Action Methods

    /// <summary>Displays a detail dialog for a new record.</summary>
    internal void DoNew()
    {
      if (mGroupGrid.CurrentRow is LJCGridRow groupRow)
      {
        // Data from items.
        var assemblyID = AssemblyID(groupRow);
        var groupID = GroupID();
        string groupName = null;
        if (groupID > 0)
        {
          var groupData = CurrentGroup();
          groupName = groupData.Heading;
        }

        var detail = new ClassDetail()
        {
          LJCAssemblyID = assemblyID,
          LJCGroupID = groupID,
          LJCGroupName = groupName,
          Managers = Managers
        };
        detail.LJCChange += Detail_Change;
        detail.ShowDialog();
      }
    }

    /// <summary>Displays a detail dialog to edit an existing record.</summary>
    internal void DoEdit()
    {
      if (mGroupGrid.CurrentRow is LJCGridRow groupRow
        && mClassGrid.CurrentRow is LJCGridRow row)
      {
        // Data from items. (DocClassGroup)
        var assemblyID = AssemblyID(groupRow);
        var groupID = GroupID();
        string groupName = null;
        if (groupID > 0)
        {
          var groupData = CurrentGroup();
          groupName = groupData.Heading;
        }
        var id = ClassID();

        var detail = new ClassDetail()
        {
          LJCAssemblyID = assemblyID,
          LJCID = id,
          LJCGroupID = groupID,
          LJCGroupName = groupName,
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

      var groupRow = mGroupGrid.CurrentRow as LJCGridRow;
      var row = mClassGrid.CurrentRow as LJCGridRow;
      if (groupRow != null
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
        var groupID = GroupID();
        var id = ClassID();

        var keyRecord = new DbColumns()
        {
          { DocClass.ColumnDocClassGroupID, groupID },
          { DocClass.ColumnID, id }
        };
        var manager = Managers.DocClassManager;
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
        mClassGrid.Rows.Remove(row);
        mDocList.TimedChange(Change.ClassItem);
      }
    }

    /// <summary>Refreshes the list.</summary> 
    internal void DoRefresh()
    {
      mDocList.Cursor = Cursors.WaitCursor;
      short id = ClassID();
      DataRetrieve();

      // Select the original row.
      if (id > 0)
      {
        var dataRecord = new DocClass()
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
      var classItem = CurrentClass();
      var manager = Managers.DocClassManager;
      manager.ClassGroupID = classItem.DocClassGroupID;
      manager.ResetSequence();
    }

    // Adds new row or updates row with changes from the detail dialog.
    private void Detail_Change(object sender, EventArgs e)
    {
      var detail = sender as ClassDetail;
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
          mClassGrid.LJCSetCurrentRow(row, true);
          DoRefresh();
          mDocList.TimedChange(Change.AssemblyItem);
        }
      }
    }
    #endregion

    #region Other Methods

    /// <summary>
    /// Retrieves the row parent ID.
    /// </summary>
    /// <param name="groupRow">The group row.</param>
    /// <returns>The group row Assembly ID.</returns>
    internal short AssemblyID(LJCGridRow groupRow)
    {
      short retValue = 0;

      if (groupRow != null)
      {
        retValue = (short)groupRow.LJCGetInt32(DocClassGroup.ColumnDocAssemblyID);
      }
      return retValue;
    }

    // Retrieves the current row item ID.
    /// <include path='items/RowID/*' file='../../../../LJCDocLib/Common/List.xml'/>
    internal short ClassID(LJCGridRow row = null)
    {
      short retValue = 0;

      if (null == row)
      {
        row = mClassGrid.CurrentRow as LJCGridRow;
      }
      if (row != null)
      {
        retValue = (short)row.LJCGetInt32(DocClass.ColumnID);
      }
      return retValue;
    }

    // Retrieves the current row item.
    /// <include path='items/CurrentItem/*' file='../../../../LJCDocLib/Common/List.xml'/>
    internal DocClass CurrentClass()
    {
      DocClass retValue = null;

      if (mClassGrid.CurrentRow is LJCGridRow _)
      {
        var id = ClassID();
        if (id > 0)
        {
          var manager = Managers.DocClassManager;
          retValue = manager.RetrieveWithID(id);
        }
      }
      return retValue;
    }

    /// <summary>
    /// Retrieves the current parent Group row item.
    /// </summary>
    /// <returns>The current parent Group row item.</returns>
    internal DocClassGroup CurrentGroup()
    {
      DocClassGroup retValue = null;

      if (mGroupGrid.CurrentRow is LJCGridRow groupRow)
      {
        var id = GroupID(groupRow);
        if (id > 0)
        {
          var manager = Managers.DocClassGroupManager;
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
      if (dragDataName == mClassGrid.LJCDragDataName)
      {
        var targetIndex = mClassGrid.LJCGetDragRowIndex(new Point(e.X, e.Y));
        if (targetIndex >= 0)
        {
          // Get source group.
          var sourceName = sourceRow.LJCGetString(DocClass.ColumnName);
          var manager = Managers.DocClassManager;
          var sourceGroup = manager.RetrieveWithUnique(parentID, sourceName);

          // Get target group.
          var targetRow = mClassGrid.Rows[targetIndex] as LJCGridRow;
          var targetName = targetRow.LJCGetString(DocClass.ColumnName);
          var targetGroup = manager.RetrieveWithUnique(parentID, targetName);

          var sourceSequence = sourceGroup.Sequence;
          var targetSequence = targetGroup.Sequence;
          manager.ClassGroupID = sourceGroup.DocClassGroupID;
          manager.ChangeSequence(sourceSequence, targetSequence);
          DoRefresh();
        }
      }
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
      if (0 == mClassGrid.Columns.Count)
      {
        List<string> columnNames = new List<string>()
        {
          DocClass.ColumnName,
          DocClass.ColumnDescription
        };

        // Get the display columns from the manager Data Definition.
        var classManager = Managers.DocClassManager;
        DisplayColumns = classManager.GetColumns(columnNames);

        // Setup the grid display columns.
        mClassGrid.LJCAddDisplayColumns(DisplayColumns);
        mClassGrid.LJCDragDataName = "DocClass";
      }
    }
    #endregion

    #region Private Methods

    // Checks for Previous and Next items.
    private void CheckPreviousAndNext(ClassDetail detail)
    {
      PreviousItem(detail);
      NextItem(detail);
    }

    // Checks for Next item.
    private void NextItem(ClassDetail detail)
    {
      if (detail.LJCNext)
      {
        LJCDataGrid grid = mClassGrid;
        int currentIndex = grid.CurrentRow.Index;
        detail.LJCNext = false;
        if (currentIndex < grid.Rows.Count - 1)
        {
          grid.LJCSetCurrentRow(currentIndex + 1, true);
          var id = ClassID();
          if (id > 0)
          {
            detail.LJCNext = true;
            detail.LJCID = id;
          }
        }
      }
    }

    // Checks for Previous item.
    private void PreviousItem(ClassDetail detail)
    {
      if (detail.LJCPrevious)
      {
        LJCDataGrid grid = mClassGrid;
        int currentIndex = grid.CurrentRow.Index;
        detail.LJCPrevious = false;
        if (currentIndex > 0)
        {
          grid.LJCSetCurrentRow(currentIndex - 1, true);
          var id = ClassID();
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

    private readonly LJCDataGrid mClassGrid;
    private readonly LJCGenDocList mDocList;
    private readonly LJCDataGrid mGroupGrid;
    #endregion
  }
}
