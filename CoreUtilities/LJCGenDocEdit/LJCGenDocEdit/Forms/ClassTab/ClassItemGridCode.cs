// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// ClassItemGridCode.cs
using LJCDBMessage;
using LJCDocLibDAL;
using LJCNetCommon;
using LJCWinFormCommon;
using LJCWinFormControls;
using static LJCGenDocEdit.LJCGenDocList;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

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
      mClassGroupGrid = mDocList.ClassGroupGrid;
      Managers = mDocList.Managers;
    }
    #endregion

    #region Data Methods

    /// <summary>Retrieves the list rows.</summary>
    internal void DataRetrieve()
    {
      mDocList.Cursor = Cursors.WaitCursor;
      mClassGrid.LJCRowsClear();

      var manager = Managers.DocClassManager;
      var names = new List<string>()
      {
        DocClass.ColumnSequence
      };
      manager.SetOrderBy(names);

      DbColumns keyColumns;
      if (0 == ClassGroupID())
      {
        // Get ungrouped classes.
        keyColumns = new DbColumns()
        {
          { DocClass.ColumnDocClassGroupID, (object)"'-null'" },
          { DocClass.ColumnDocAssemblyID, AssemblyID() }
        };
      }
      else
      {
        keyColumns = new DbColumns()
        {
          { DocClass.ColumnDocClassGroupID, ClassGroupID() }
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
          if (ClassID(row) == dataRecord.ID)
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
    }
    #endregion

    #region Action Methods

    /// <summary>Displays a detail dialog for a new record.</summary>
    internal void DoNew()
    {
      if (mClassGroupGrid.CurrentRow is LJCGridRow _)
      {
        var detail = new ClassDetail()
        {
          LJCGroupID = ClassGroupID(),
          LJCAssemblyID = AssemblyID(),
          Managers = Managers
        };
        detail.LJCChange += Detail_Change;
        detail.ShowDialog();
      }
    }

    /// <summary>Displays a detail dialog to edit an existing record.</summary>
    internal void DoEdit()
    {
      if (mClassGroupGrid.CurrentRow is LJCGridRow _
        && mClassGrid.CurrentRow is LJCGridRow _)
      {
        var detail = new ClassDetail()
        {
          LJCClassID = ClassID(),
          LJCGroupID = ClassGroupID(),
          LJCAssemblyID = AssemblyID(),
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

      var classGroupRow = mClassGroupGrid.CurrentRow as LJCGridRow;
      var classRow = mClassGrid.CurrentRow as LJCGridRow;
      if (classGroupRow != null
        && classRow != null)
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
          { DocClass.ColumnDocClassGroupID, ClassGroupID() },
          { DocClass.ColumnID, ClassID() }
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
        mClassGrid.Rows.Remove(classRow);
        mDocList.TimedChange(Change.ClassItem);
      }
    }

    /// <summary>Refreshes the list.</summary> 
    internal void DoRefresh()
    {
      mDocList.Cursor = Cursors.WaitCursor;

      // Save the original row.
      var classID = ClassID();

      DataRetrieve();
      if (classID > 0)
      {
        var dataRecord = new DocClass()
        {
          ID = classID
        };
        RowSelect(dataRecord);
      }
      mDocList.Cursor = Cursors.Default;
    }

    /// <summary>Resets the Sequence column values.</summary>
    internal void DoResetSequence()
    {
      var manager = Managers.DocClassManager;
      manager.ClassGroupID = ClassGroupID();
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

    // Retrieves the current row item ID.
    /// <include path='items/AssemblyID/*' file='../../Doc/ClassItemGridCode.xml'/>
    internal short AssemblyID(LJCGridRow classGroupRow = null)
    {
      short retValue = 0;

      if (null == classGroupRow)
      {
        classGroupRow = mClassGroupGrid.CurrentRow as LJCGridRow;
      }
      if (classGroupRow != null)
      {
        retValue = (short)classGroupRow.LJCGetInt32(DocClassGroup.ColumnDocAssemblyID);
      }
      return retValue;
    }

    // Retrieves the current row item ID.
    /// <include path='items/ClassGroupID/*' file='../../Doc/ClassItemGridCode.xml'/>
    internal short ClassGroupID(LJCGridRow classGroupRow = null)
    {
      short retValue = 0;

      if (null == classGroupRow)
      {
        classGroupRow = mClassGroupGrid.CurrentRow as LJCGridRow;
      }
      if (classGroupRow != null)
      {
        retValue = (short)classGroupRow.LJCGetInt32(DocClassGroup.ColumnID);
      }
      return retValue;
    }

    // Retrieves the current row item ID.
    /// <include path='items/ClassID/*' file='../../Doc/ClassItemGridCode.xml'/>
    internal short ClassID(LJCGridRow docClassRow = null)
    {
      short retValue = 0;

      if (null == docClassRow)
      {
        docClassRow = mClassGrid.CurrentRow as LJCGridRow;
      }
      if (docClassRow != null)
      {
        retValue = (short)docClassRow.LJCGetInt32(DocClass.ColumnID);
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
        var classID = ClassID();
        if (classID > 0)
        {
          var manager = Managers.DocClassManager;
          retValue = manager.RetrieveWithID(classID);
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
          if (ClassID() > 0)
          {
            detail.LJCNext = true;
            detail.LJCClassID = ClassID();
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
          if (ClassID() > 0)
          {
            detail.LJCPrevious = true;
            detail.LJCClassID = ClassID();
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
    private readonly LJCDataGrid mClassGroupGrid;
    private readonly LJCGenDocList mDocList;
    #endregion
  }
}
