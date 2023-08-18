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
        var manager = Managers.DocClassGroupManager;
        var names = new List<string>()
        {
          DocClassGroup.ColumnSequence
        };
        manager.SetOrderBy(names);

        var keyColumns = new DbColumns()
        {
          { DocClassGroup.ColumnDocAssemblyID, DocAssemblyID() }
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
      retValue.LJCSetValues(mClassGroupGrid, dataRecord);
      return retValue;
    }

    // Adds a grid row and updates it with the result values.
    private LJCGridRow RowAddValues(DbValues dbValues)
    {
      var retValue = mClassGroupGrid.LJCRowAdd();
      var columnName = DocClassGroup.ColumnID;
      retValue.LJCSetInt32(columnName, dbValues.LJCGetInt32(columnName));

      retValue.LJCSetValues(mClassGroupGrid, dbValues);
      return retValue;
    }

    // Updates the current row with the record values.
    private void RowUpdate(DocClassGroup dataRecord)
    {
      if (mClassGroupGrid.CurrentRow is LJCGridRow row)
      {
        SetStoredValues(row, dataRecord);
        row.LJCSetValues(mClassGroupGrid, dataRecord);
      }
    }

    // Sets the row stored values.
    private void SetStoredValues(LJCGridRow row, DocClassGroup dataRecord)
    {
      row.LJCSetInt32(DocClassGroup.ColumnID, dataRecord.ID);
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
          LJCAssemblyID = DocAssemblyID(),
          Managers = Managers,
          Sequence = mClassGroupGrid.Rows.Count + 1
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
          LJCAssemblyID = DocAssemblyID(),
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
          { DocClass.ColumnDocAssemblyID, DocAssemblyID() },
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

      // Save the original row.
      var classGroupID = ClassGroupID();

      DataRetrieve();
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
      var procedure = true;
      if (procedure)
      {
        var classGroupManager = Managers.DocClassGroupManager;
        classGroupManager.AssemblyID = DocAssemblyID();
        classGroupManager.ResetSequence();
      }
      else
      {
        var dataManager = Managers.DocClassGroupManager.Manager;
        var assemblyID = DocAssemblyID();
        var where = $"where DocAssemblyID = {assemblyID}";
        dataManager.Resequence("ID", "Sequence", where);
      }
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

    // The DragDrop method.
    /// <include path='items/DoDragDrop/*' file='../../Doc/ClassGroupGridCode.xml'/>
    internal void DoDragDrop(short assemblyID, DragEventArgs e)
    {
      var sourceRow = e.Data.GetData(typeof(LJCGridRow)) as LJCGridRow;
      var dragDataName = sourceRow.LJCGetString("DragDataName");
      if (dragDataName == mClassGroupGrid.LJCDragDataName)
      {
        var targetIndex = mClassGroupGrid.LJCGetDragRowIndex(new Point(e.X, e.Y));
        if (targetIndex >= 0)
        {
          // Get source group.
          var sourceName = ClassGroupHeadingName(ClassGroupID(sourceRow));
          var manager = Managers.DocClassGroupManager;
          var sourceGroup = manager.RetrieveWithUnique(assemblyID, sourceName);

          // Get target group.
          var targetRow = mClassGroupGrid.Rows[targetIndex] as LJCGridRow;
          var targetName = ClassGroupHeadingName(ClassGroupID(targetRow));
          var targetGroup = manager.RetrieveWithUnique(assemblyID, targetName);

          var sourceSequence = sourceGroup.Sequence;
          var targetSequence = targetGroup.Sequence;
          manager.AssemblyID = sourceGroup.DocAssemblyID;
          manager.ChangeSequence(sourceSequence, targetSequence);
          DoRefresh();
        }
      }
    }

    /// <summary>Setup the grid columns.</summary>
    internal void SetupGrid()
    {
      // Setup default grid columns if no columns are defined.
      if (0 == mClassGroupGrid.Columns.Count)
      {
        List<string> propertyNames = new List<string>()
        {
          DocClassGroup.ColumnHeadingName,
          DocClassGroup.ColumnHeadingTextCustom
        };

        // Get the grid columns from the manager Data Definition.
        var classManager = Managers.DocClassGroupManager;
        GridColumns = classManager.GetColumns(propertyNames);

        // Setup the grid columns.
        mClassGroupGrid.LJCAddColumns(GridColumns);
        mClassGroupGrid.LJCDragDataName = "DocClassGroup";
      }
    }

    // Retrieves the AssemblyItem name.
    private string ClassGroupHeadingName(short classGroupID)
    {
      string retValue = null;

      var docClassGroup = ClassGroupWithID(classGroupID);
      if (docClassGroup != null)
      {
        retValue = docClassGroup.HeadingName;
      }
      return retValue;
    }

    // Retrieves the current row item ID.
    private short ClassGroupID(LJCGridRow classGroupRow = null)
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

    // Retrieves the ClassGroup with the ID value.
    private DocClassGroup ClassGroupWithID(short classGroupID)
    {
      DocClassGroup retValue = null;

      if (classGroupID > 0)
      {
        var manager = Managers.DocClassGroupManager;
        retValue = manager.RetrieveWithID(classGroupID);
      }
      return retValue;
    }

    // Retrieves the current row item ID.
    private short DocAssemblyID(LJCGridRow docAssemblyRow = null)
    {
      short retValue = 0;

      if (null == docAssemblyRow)
      {
        docAssemblyRow = mAssemblyGrid.CurrentRow as LJCGridRow;
      }
      if (docAssemblyRow != null)
      {
        retValue = (short)docAssemblyRow.LJCGetInt32(DocAssembly.ColumnID);
      }
      return retValue;
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

    /// <summary>Gets or sets the GridColumns value.</summary>
    internal DbColumns GridColumns { get; set; }

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
