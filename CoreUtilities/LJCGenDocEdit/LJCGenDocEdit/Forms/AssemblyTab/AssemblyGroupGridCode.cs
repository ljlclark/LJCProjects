// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// AssemblyGroupGridCode.cs
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
  /// <summary>The AssemblyGroup grid code.</summary>
  internal class AssemblyGroupGridCode
  {
    #region Constructors

    /// <summary>Initializes an object instance.</summary>
    internal AssemblyGroupGridCode(LJCGenDocList parentList)
    {
      // Initialize property values.
      mDocList = parentList;
      mAssemblyGroupGrid = mDocList.AssemblyGroupGrid;
      Managers = mDocList.Managers;
      DocAssemblyGroupManager = Managers.DocAssemblyGroupManager;
    }
    #endregion

    #region Data Methods

    /// <summary>Retrieves the list rows.</summary>
    internal void DataRetrieve()
    {
      mDocList.Cursor = Cursors.WaitCursor;
      mAssemblyGroupGrid.LJCRowsClear();

      var manager = DocAssemblyGroupManager;
      var names = new List<string>()
      {
        DocAssemblyGroup.ColumnSequence
      };
      manager.SetOrderBy(names);

      DbResult result = manager.LoadResult();

      if (DbResult.HasRows(result))
      {
        foreach (DbRow dbRow in result.Rows)
        {
          RowAddValues(dbRow.Values);
        }
      }
      mDocList.Cursor = Cursors.Default;
      mDocList.DoChange(Change.AssemblyGroup);
    }

    // Selects a row based on the key record values.
    /// <include path='items/RowSelect/*' file='../../../../LJCDocLib/Common/List.xml'/>
    internal bool RowSelect(DocAssemblyGroup dataRecord)
    {
      bool retValue = false;

      if (dataRecord != null)
      {
        mDocList.Cursor = Cursors.WaitCursor;
        foreach (LJCGridRow row in mAssemblyGroupGrid.Rows)
        {
          if (AssemblyGroupID(row) == dataRecord.ID)
          {
            // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
            mAssemblyGroupGrid.LJCSetCurrentRow(row, true);
            retValue = true;
            break;
          }
        }
        mDocList.Cursor = Cursors.Default;
      }
      return retValue;
    }

    // Adds a grid row and updates it with the record values.
    private LJCGridRow RowAdd(DocAssemblyGroup dataRecord)
    {
      var retValue = mAssemblyGroupGrid.LJCRowAdd();
      SetStoredValues(retValue, dataRecord);
      retValue.LJCSetValues(mAssemblyGroupGrid, dataRecord);
      return retValue;
    }

    // Adds a grid row and updates it with the result values.
    private LJCGridRow RowAddValues(DbValues dbValues)
    {
      var retValue = mAssemblyGroupGrid.LJCRowAdd();

      var columnName = DocAssemblyGroup.ColumnID;
      retValue.LJCSetInt32(columnName, dbValues.LJCGetInt16(columnName));

      retValue.LJCSetValues(mAssemblyGroupGrid, dbValues);
      return retValue;
    }

    // Updates the current row with the record values.
    private void RowUpdate(DocAssemblyGroup dataRecord)
    {
      if (mAssemblyGroupGrid.CurrentRow is LJCGridRow row)
      {
        SetStoredValues(row, dataRecord);
        row.LJCSetValues(mAssemblyGroupGrid, dataRecord);
      }
    }

    // Sets the row stored record values.
    private void SetStoredValues(LJCGridRow row, DocAssemblyGroup dataRecord)
    {
      row.LJCSetInt32(DocAssemblyGroup.ColumnID, dataRecord.ID);
    }
    #endregion

    #region Action Methods

    /// <summary>Displays a detail dialog for a new record.</summary>
    internal void DoNew()
    {
      var detail = new AssemblyGroupDetail()
      {
        Managers = Managers,
        Sequence = mAssemblyGroupGrid.Rows.Count + 1
      };
      detail.LJCChange += Detail_Change;
      detail.ShowDialog();
    }

    /// <summary>Displays a detail dialog to edit an existing record.</summary>
    internal void DoEdit()
    {
      if (mAssemblyGroupGrid.CurrentRow is LJCGridRow _)
      {
        var detail = new AssemblyGroupDetail()
        {
          LJCGroupID = AssemblyGroupID(),
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

      var groupRow = mAssemblyGroupGrid.CurrentRow as LJCGridRow;
      if (groupRow != null)
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
          { DocAssemblyGroup.ColumnID, AssemblyGroupID() }
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
        mAssemblyGroupGrid.Rows.Remove(groupRow);
        mDocList.TimedChange(Change.AssemblyGroup);
      }
    }

    /// <summary>Refreshes the list.</summary> 
    internal void DoRefresh()
    {
      mDocList.Cursor = Cursors.WaitCursor;

      // Save the original row.
      var assemblyGroupID = AssemblyGroupID();

      DataRetrieve();
      if (assemblyGroupID > 0)
      {
        var dataRecord = new DocAssemblyGroup()
        {
          ID = assemblyGroupID
        };
        RowSelect(dataRecord);
      }
      mDocList.Cursor = Cursors.Default;
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
          mAssemblyGroupGrid.LJCSetCurrentRow(row, true);
          DoRefresh();
          mDocList.TimedChange(Change.AssemblyGroup);
        }
      }
    }
    #endregion

    #region Other Methods

    // The DragDrop method.
    /// <include path='items/DoDragDrop/*' file='../../../../LJCDocLib/Common/List.xml'/>
    internal void DoDragDrop(DragEventArgs e)
    {
      var sourceRow = e.Data.GetData(typeof(LJCGridRow)) as LJCGridRow;
      var dragDataName = sourceRow.LJCGetString("DragDataName");
      if (dragDataName == mAssemblyGroupGrid.LJCDragDataName)
      {
        var targetIndex = mAssemblyGroupGrid.LJCGetDragRowIndex(new Point(e.X, e.Y));
        if (targetIndex >= 0)
        {
          // Get source group.
          var sourceName = AssemblyGroupName(AssemblyGroupID(sourceRow));
          var manager = Managers.DocAssemblyGroupManager;
          var sourceGroup = manager.RetrieveWithUnique(sourceName);

          // Get target group.
          var targetRow = mAssemblyGroupGrid.Rows[targetIndex] as LJCGridRow;
          var targetName = AssemblyGroupName(AssemblyGroupID(targetRow));
          var targetGroup = manager.RetrieveWithUnique(targetName);

          var sourceSequence = sourceGroup.Sequence;
          var targetSequence = targetGroup.Sequence;
          manager.ChangeSequence(sourceSequence, targetSequence);
          DoRefresh();
        }
      }
    }

    /// <summary>Setup the grid columns.</summary>
    internal void SetupGrid()
    {
      // Setup default grid columns if no columns are defined.
      if (0 == mAssemblyGroupGrid.Columns.Count)
      {
        List<string> propertyNames = new List<string>()
        {
          DocAssemblyGroup.ColumnName,
          DocAssemblyGroup.ColumnHeading,
          DocAssemblyGroup.ColumnSequence
        };

        // Get the grid columns from the manager Data Definition.
        var manager = DocAssemblyGroupManager;
        GridColumns = manager.GetColumns(propertyNames);

        // Setup the grid columns.
        mAssemblyGroupGrid.LJCAddColumns(GridColumns);
        mAssemblyGroupGrid.LJCDragDataName = "DocAssemblyGroup";
      }
    }

    // Retrieves the current row item ID.
    private short AssemblyGroupID(LJCGridRow assemblyGroupRow = null)
    {
      short retValue = 0;

      if (null == assemblyGroupRow)
      {
        assemblyGroupRow = mAssemblyGroupGrid.CurrentRow as LJCGridRow;
      }
      if (assemblyGroupRow != null)
      {
        retValue = (short)assemblyGroupRow.LJCGetInt32(DocAssemblyGroup.ColumnID);
      }
      return retValue;
    }

    // Retrieves the AssemblyGroup name.
    private string AssemblyGroupName(short assemblyGroupID)
    {
      string retValue = null;

      var assemblyGroup = AssemblyGroupWithID(assemblyGroupID);
      if (assemblyGroup != null)
      {
        retValue = assemblyGroup.Name;
      }
      return retValue;
    }

    // Retrieves the AssemblyGroup with the ID value.
    private DocAssemblyGroup AssemblyGroupWithID(short assemblyGroupID)
    {
      DocAssemblyGroup retValue = null;

      if (assemblyGroupID > 0)
      {
        var manager = Managers.DocAssemblyGroupManager;
        retValue = manager.RetrieveWithID(assemblyGroupID);
      }
      return retValue;
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
        LJCDataGrid groupGrid = mAssemblyGroupGrid;
        int currentIndex = groupGrid.CurrentRow.Index;
        detail.LJCNext = false;
        if (currentIndex < groupGrid.Rows.Count - 1)
        {
          groupGrid.LJCSetCurrentRow(currentIndex + 1, true);
          var assemblyGroupID = AssemblyGroupID();
          if (assemblyGroupID > 0)
          {
            detail.LJCNext = true;
            detail.LJCGroupID = assemblyGroupID;
          }
        }
      }
    }

    // Checks for Previous item.
    private void PreviousItem(AssemblyGroupDetail detail)
    {
      if (detail.LJCPrevious)
      {
        LJCDataGrid groupGrid = mAssemblyGroupGrid;
        int currentIndex = groupGrid.CurrentRow.Index;
        detail.LJCPrevious = false;
        if (currentIndex > 0)
        {
          groupGrid.LJCSetCurrentRow(currentIndex - 1, true);
          if (AssemblyGroupID() > 0)
          {
            detail.LJCPrevious = true;
            detail.LJCGroupID = AssemblyGroupID();
          }
        }
      }
    }
    #endregion

    #region Properties

    /// <summary>Gets or sets the GridColumns value.</summary>
    internal DbColumns GridColumns { get; set; }

    /// <summary>Gets or sets the Manager value.</summary>
    internal DocAssemblyGroupManager DocAssemblyGroupManager { get; set; }

    // The Managers object.
    internal ManagersDocGen Managers { get; set; }
    #endregion

    #region Class Data

    private readonly LJCDataGrid mAssemblyGroupGrid;
    private readonly LJCGenDocList mDocList;
    #endregion
  }
}
