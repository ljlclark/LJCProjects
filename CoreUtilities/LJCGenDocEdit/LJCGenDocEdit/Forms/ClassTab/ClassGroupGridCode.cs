// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// ClassGroupGridCode.cs
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
  /// <summary>The ClassGroup grid code.</summary>
  internal class ClassGroupGridCode
  {
    #region Constructors

    /// <summary>Initializes an object instance.</summary>
    internal ClassGroupGridCode(LJCGenDocList parentList)
    {
      mDocList = parentList;
      mAssemblyGrid = mDocList.AssemblyItemGrid;
      mClassGroupGrid = mDocList.ClassGroupGrid;
      Managers = mDocList.Managers;
    }
    #endregion

    #region Data Methods

    /// <summary>Retrieves the list rows.</summary>
    internal void DataRetrieve()
    {
      mClassGroupGrid.LJCRowsClear();

      if (mDocList.AssemblyItemGrid.CurrentRow is LJCGridRow _)
      {
        mDocList.Cursor = Cursors.WaitCursor;
        var docClassGroup = new DocClassGroup()
        {
          ID = 0,
          HeadingName = "Ungrouped Classes",
          DocAssemblyID = AssemblyID()
        };
        //RowAdd(docClassGroup);

        var manager = Managers.DocClassGroupManager;
        var names = new List<string>()
        {
          DocClassGroup.ColumnSequence
        };
        manager.SetOrderBy(names);

        var keyColumns = new DbColumns()
        {
          { DocClassGroup.ColumnDocAssemblyID, AssemblyID() }
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
        foreach (LJCGridRow row in mClassGroupGrid.Rows)
        {
          if (ClassGroupID(row) == dataRecord.ID)
          {
            // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
            mClassGroupGrid.LJCSetCurrentRow(row, true);
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
      var retValue = mClassGroupGrid.LJCRowAdd();
      SetStoredValues(retValue, dataRecord);
      mClassGroupGrid.LJCRowSetValues(retValue, dataRecord);
      return retValue;
    }

    // Adds a grid row and updates it with the result values.
    private LJCGridRow RowAddValues(DbValues dbValues)
    {
      var retValue = mClassGroupGrid.LJCRowAdd();
      var columnName = DocClassGroup.ColumnID;
      retValue.LJCSetInt32(columnName, dbValues.LJCGetInt32(columnName));
      columnName = DocClassGroup.ColumnDocAssemblyID;
      retValue.LJCSetInt32(columnName, dbValues.LJCGetInt32(columnName));

      mClassGroupGrid.LJCRowSetValues(retValue, dbValues);
      return retValue;
    }

    // Updates the current row with the record values.
    private void RowUpdate(DocClassGroup dataRecord)
    {
      if (mClassGroupGrid.CurrentRow is LJCGridRow row)
      {
        SetStoredValues(row, dataRecord);
        mClassGroupGrid.LJCRowSetValues(row, dataRecord);
      }
    }

    // Sets the row stored values.
    private void SetStoredValues(LJCGridRow row, DocClassGroup dataRecord)
    {
      row.LJCSetInt32(DocClassGroup.ColumnID, dataRecord.ID);
      row.LJCSetInt32(DocClassGroup.ColumnDocAssemblyID
        , dataRecord.DocAssemblyID);
    }
    #endregion

    #region Action Methods

    /// <summary>Displays a detail dialog for a new record.</summary>
    internal void DoNew()
    {
      if (mAssemblyGrid.CurrentRow is LJCGridRow _)
      {
        var detail = new ClassGroupDetail()
        {
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
      if (mAssemblyGrid.CurrentRow is LJCGridRow _
        && mClassGroupGrid.CurrentRow is LJCGridRow _)
      {
        var detail = new ClassGroupDetail()
        {
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

      var assemblyRow = mAssemblyGrid.CurrentRow as LJCGridRow;
      var classGroupRow = mClassGroupGrid.CurrentRow as LJCGridRow;
      if (assemblyRow != null
        && classGroupRow != null)
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
          { DocAssembly.ColumnID, AssemblyID() },
          { DocClass.ColumnID, ClassGroupID() }
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
        mClassGroupGrid.Rows.Remove(classGroupRow);
        mDocList.TimedChange(Change.ClassGroup);
      }
    }

    /// <summary>Refreshes the list.</summary> 
    internal void DoRefresh()
    {
      mDocList.Cursor = Cursors.WaitCursor;
      DataRetrieve();

      // Select the original row.
      var classGroupID = ClassGroupID();
      if (classGroupID > 0)
      {
        var dataRecord = new DocClassGroup()
        {
          ID = classGroupID
        };
        RowSelect(dataRecord);
      }
      mDocList.Cursor = Cursors.Default;
    }

    /// <summary>Resets the Sequence column values.</summary>
    internal void DoResetSequence()
    {
      var manager = Managers.DocClassGroupManager;
      manager.AssemblyID = AssemblyID();
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
          mClassGroupGrid.LJCSetCurrentRow(row, true);
          DoRefresh();
          mDocList.TimedChange(Change.ClassGroup);
        }
      }
    }
    #endregion

    #region Other Methods

    // Retrieves the current row item ID.
    /// <include path='items/AssemblyID/*' file='../../Doc/ClassGroupGridCode.xml'/>
    internal short AssemblyID(LJCGridRow assemblyRow = null)
    {
      short retValue = 0;

      if (null == assemblyRow)
      {
        assemblyRow = mAssemblyGrid.CurrentRow as LJCGridRow;
      }
      if (assemblyRow != null)
      {
        retValue = (short)assemblyRow.LJCGetInt32(DocAssembly.ColumnID);
      }
      return retValue;
    }

    // Retrieves the current row item ID.
    /// <include path='items/ClassGroupID/*' file='../../Doc/ClassGroupGridCode.xml'/>
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
        if (0 == retValue)
        {
          retValue = -1;
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
      if (dragDataName == mClassGroupGrid.LJCDragDataName)
      {
        var targetIndex = mClassGroupGrid.LJCGetDragRowIndex(new Point(e.X, e.Y));
        if (targetIndex >= 0)
        {
          // Get source group.
          var sourceName = sourceRow.LJCGetString(DocClassGroup.ColumnHeadingName);
          var manager = Managers.DocClassGroupManager;
          var sourceGroup = manager.RetrieveWithUnique(parentID, sourceName);

          // Get target group.
          var targetRow = mClassGroupGrid.Rows[targetIndex] as LJCGridRow;
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

    /// <summary>Setup the grid display columns.</summary>
    internal void SetupGrid()
    {
      // Setup default display columns if no columns are defined.
      if (0 == mClassGroupGrid.Columns.Count)
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
        mClassGroupGrid.LJCAddDisplayColumns(DisplayColumns);
        mClassGroupGrid.LJCDragDataName = "DocClassGroup";
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
        LJCDataGrid groupGrid = mClassGroupGrid;
        int currentIndex = groupGrid.CurrentRow.Index;
        detail.LJCNext = false;
        if (currentIndex < groupGrid.Rows.Count - 1)
        {
          groupGrid.LJCSetCurrentRow(currentIndex + 1, true);
          var classGroupID = ClassGroupID();
          if (classGroupID > 0)
          {
            detail.LJCNext = true;
            detail.LJCGroupID = classGroupID;
          }
        }
      }
    }

    // Checks for Previous item.
    private void PreviousItem(ClassGroupDetail detail)
    {
      if (detail.LJCPrevious)
      {
        LJCDataGrid groupGrid = mClassGroupGrid;
        int currentIndex = groupGrid.CurrentRow.Index;
        detail.LJCPrevious = false;
        if (currentIndex > 0)
        {
          groupGrid.LJCSetCurrentRow(currentIndex - 1, true);
          var classGroupID = ClassGroupID();
          if (classGroupID > 0)
          {
            detail.LJCPrevious = true;
            detail.LJCGroupID = classGroupID;
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
    private readonly LJCDataGrid mClassGroupGrid;
    private readonly LJCGenDocList mDocList;
    #endregion
  }
}
