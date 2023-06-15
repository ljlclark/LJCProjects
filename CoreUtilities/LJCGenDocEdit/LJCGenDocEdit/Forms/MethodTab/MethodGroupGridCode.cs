// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// MethodGroupGridCode.cs
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
  /// <summary>The MethodGroup grid code.</summary>
  internal class MethodGroupGridCode
  {
    #region Constructors

    /// <summary>Initializes an object instance.</summary>
    internal MethodGroupGridCode(LJCGenDocList parentList)
    {
      mDocList = parentList;
      mClassGrid = mDocList.ClassItemGrid;
      mGroupGrid = mDocList.MethodGroupGrid;
      Managers = mDocList.Managers;
    }
    #endregion

    #region Data Methods

    /// <summary>Retrieves the list rows.</summary>
    internal void DataRetrieve()
    {
      mGroupGrid.LJCRowsClear();

      if (mClassGrid.CurrentRow is LJCGridRow classRow)
      {
        mDocList.Cursor = Cursors.WaitCursor;
        var parentID = ClassID(classRow);

        var manager = Managers.DocMethodGroupManager;
        var names = new List<string>()
        {
          DocMethodGroup.ColumnSequence
        };
        manager.SetOrderBy(names);
        var keyColumns = new DbColumns()
        {
          { DocMethodGroup.ColumnDocClassID, parentID }
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
        mDocList.DoChange(Change.MethodGroup);
      }
    }

    // Selects a row based on the key record values.
    /// <include path='items/RowSelect/*' file='../../../../LJCDocLib/Common/List.xml'/>
    internal bool RowSelect(DocMethodGroup dataRecord)
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
    private LJCGridRow RowAdd(DocMethodGroup dataRecord)
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
      var columnName = DocMethodGroup.ColumnID;
      retValue.LJCSetInt32(columnName, dbValues.LJCGetInt32(columnName));
      columnName = DocMethodGroup.ColumnHeadingName;
      retValue.LJCSetString(columnName, dbValues.LJCGetValue(columnName));
      columnName = DocMethodGroup.ColumnDocClassID;
      retValue.LJCSetInt32(columnName, dbValues.LJCGetInt32(columnName));

      mGroupGrid.LJCRowSetValues(retValue, dbValues);
      return retValue;
    }

    // Updates the current row with the record values.
    private void RowUpdate(DocMethodGroup dataRecord)
    {
      if (mGroupGrid.CurrentRow is LJCGridRow row)
      {
        SetStoredValues(row, dataRecord);
        mGroupGrid.LJCRowSetValues(row, dataRecord);
      }
    }

    // Sets the row stored values.
    private void SetStoredValues(LJCGridRow row, DocMethodGroup dataRecord)
    {
      row.LJCSetInt32(DocMethodGroup.ColumnID, dataRecord.ID);
      row.LJCSetString(DocMethodGroup.ColumnHeadingName, dataRecord.HeadingName);
      row.LJCSetInt32(DocMethodGroup.ColumnDocClassID
        , dataRecord.DocClassID);
    }
    #endregion

    #region Action Methods

    /// <summary>Displays a detail dialog for a new record.</summary>
    internal void DoNew()
    {
      if (mClassGrid.CurrentRow is LJCGridRow classRow)
      {
        // Data from items.
        var classID = ClassID(classRow);
        string className = null;
        if (classID > 0)
        {
          var classData = CurrentClass();
          className = classData.Name;
        }

        var detail = new MethodGroupDetail()
        {
          LJCClassID = classID,
          LJCClassName = className,
          Managers = Managers
        };
        detail.LJCChange += Detail_Change;
        detail.ShowDialog();
      }
    }

    /// <summary>Displays a detail dialog to edit an existing record.</summary>
    internal void DoEdit()
    {
      if (mClassGrid.CurrentRow is LJCGridRow classRow
        && mGroupGrid.CurrentRow is LJCGridRow row)
      {
        // Data from items.
        var classID = ClassID(classRow);
        string className = null;
        if (classID > 0)
        {
          var classData = CurrentClass();
          className = classData.Name;
        }
        var id = GroupID();

        var detail = new MethodGroupDetail()
        {
          LJCID = id,
          LJCClassID = classID,
          LJCClassName = className,
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

      var classRow = mClassGrid.CurrentRow as LJCGridRow;
      var row = mGroupGrid.CurrentRow as LJCGridRow;
      if (classRow != null
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
        var classID = ClassID(classRow);
        var id = GroupID();

        var keyRecord = new DbColumns()
        {
          { DocMethodGroup.ColumnID, classID },
          { DocClass.ColumnID, id }
        };
        var manager = Managers.DocMethodGroupManager;
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
        mDocList.TimedChange(Change.MethodGroup);
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
        var dataRecord = new DocMethodGroup()
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
      var methodGroup = CurrentGroup();
      var manager = Managers.DocMethodGroupManager;
      manager.ClassID = methodGroup.DocClassID;
      manager.ResetSequence();
    }

    // Adds new row or updates row with changes from the detail dialog.
    private void Detail_Change(object sender, EventArgs e)
    {
      var detail = sender as MethodGroupDetail;
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
          mDocList.TimedChange(Change.MethodGroup);
        }
      }
    }
    #endregion

    #region Other Methods

    /// <summary>
    /// Retrieves the parent Class ID.
    /// </summary>
    /// <param name="classRow">The parent Class row.</param>
    /// <returns>The parent Class row ID.</returns>
    internal short ClassID(LJCGridRow classRow)
    {
      short retValue = 0;

      if (classRow != null)
      {
        retValue = (short)classRow.LJCGetInt32(DocClass.ColumnID);
      }
      return retValue;
    }

    /// <summary>
    /// Retrieves the current parent Class row item.
    /// </summary>
    /// <returns>The current parent Class row item.</returns>
    internal DocClass CurrentClass()
    {
      DocClass retValue = null;

      if (mClassGrid.CurrentRow is LJCGridRow classRow)
      {
        var id = ClassID(classRow);
        if (id > 0)
        {
          var manager = Managers.DocClassManager;
          retValue = manager.RetrieveWithID(id);
        }
      }
      return retValue;
    }

    // Retrieves the current row item.
    /// <include path='items/CurrentItem/*' file='../../../../LJCDocLib/Common/List.xml'/>
    internal DocMethodGroup CurrentGroup()
    {
      DocMethodGroup retValue = null;

      if (mGroupGrid.CurrentRow is LJCGridRow _)
      {
        var id = GroupID();
        if (id > 0)
        {
          var manager = Managers.DocMethodGroupManager;
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
          var sourceName = sourceRow.LJCGetString(DocMethodGroup.ColumnHeadingName);
          var manager = Managers.DocMethodGroupManager;
          var sourceGroup = manager.RetrieveWithUnique(parentID, sourceName);

          // Get target group.
          var targetRow = mGroupGrid.Rows[targetIndex] as LJCGridRow;
          var targetName = targetRow.LJCGetString(DocMethodGroup.ColumnHeadingName);
          var targetGroup = manager.RetrieveWithUnique(parentID, targetName);

          var sourceSequence = sourceGroup.Sequence;
          var targetSequence = targetGroup.Sequence;
          manager.ClassID = sourceGroup.DocClassID;
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
        retValue = (short)row.LJCGetInt32(DocMethodGroup.ColumnID);
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
          DocMethodGroup.ColumnHeadingName,
          DocMethodGroup.ColumnHeadingTextCustom
        };

        // Get the display columns from the manager Data Definition.
        var classGroupManager = Managers.DocMethodGroupManager;
        DisplayColumns = classGroupManager.GetColumns(columnNames);

        // Setup the grid display columns.
        mGroupGrid.LJCAddDisplayColumns(DisplayColumns);
        mGroupGrid.LJCDragDataName = "DocMethodGroup";
      }
    }
    #endregion

    #region Private Methods

    // Checks for Previous and Next items.
    private void CheckPreviousAndNext(MethodGroupDetail detail)
    {
      PreviousItem(detail);
      NextItem(detail);
    }

    // Checks for Next item.
    private void NextItem(MethodGroupDetail detail)
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
    private void PreviousItem(MethodGroupDetail detail)
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

    private readonly LJCDataGrid mClassGrid;
    private readonly LJCGenDocList mDocList;
    private readonly LJCDataGrid mGroupGrid;
    #endregion
  }
}
