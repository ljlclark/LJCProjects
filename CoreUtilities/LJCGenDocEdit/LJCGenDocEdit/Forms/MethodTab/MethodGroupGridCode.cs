// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// MethodGroupGridCode.cs
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
  /// <summary>The MethodGroup grid code.</summary>
  internal class MethodGroupGridCode
  {
    #region Constructors

    /// <summary>Initializes an object instance.</summary>
    internal MethodGroupGridCode(LJCGenDocList parentList)
    {
      mDocList = parentList;
      mClassGrid = mDocList.ClassItemGrid;
      Managers = mDocList.Managers;
      mMethodGroupGrid = mDocList.MethodGroupGrid;
    }
    #endregion

    #region Data Methods

    /// <summary>Retrieves the list rows.</summary>
    internal void DataRetrieve()
    {
      mMethodGroupGrid.LJCRowsClear();

      if (mClassGrid.CurrentRow is LJCGridRow _)
      {
        mDocList.Cursor = Cursors.WaitCursor;
        var manager = Managers.DocMethodGroupManager;
        var names = new List<string>()
        {
          DocMethodGroup.ColumnSequence
        };
        manager.SetOrderBy(names);

        var keyColumns = new DbColumns()
        {
          { DocMethodGroup.ColumnDocClassID, DocClassID() }
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
        foreach (LJCGridRow row in mMethodGroupGrid.Rows)
        {
          if (MethodGroupID(row) == dataRecord.ID)
          {
            // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
            mMethodGroupGrid.LJCSetCurrentRow(row, true);
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
      var retValue = mMethodGroupGrid.LJCRowAdd();
      SetStoredValues(retValue, dataRecord);
      retValue.LJCSetValues(mMethodGroupGrid, dataRecord);
      return retValue;
    }

    // Adds a grid row and updates it with the result values.
    private LJCGridRow RowAddValues(DbValues dbValues)  
    {
      var retValue = mMethodGroupGrid.LJCRowAdd();
      var columnName = DocMethodGroup.ColumnID;
      retValue.LJCSetInt32(columnName, dbValues.LJCGetInt32(columnName));
      //columnName = DocMethodGroup.ColumnDocClassID;
      //retValue.LJCSetInt32(columnName, dbValues.LJCGetInt32(columnName));

      retValue.LJCSetValues(mMethodGroupGrid, dbValues);
      return retValue;
    }

    // Updates the current row with the record values.
    private void RowUpdate(DocMethodGroup dataRecord)
    {
      if (mMethodGroupGrid.CurrentRow is LJCGridRow row)
      {
        SetStoredValues(row, dataRecord);
        row.LJCSetValues(mMethodGroupGrid, dataRecord);
      }
    }

    // Sets the row stored values.
    private void SetStoredValues(LJCGridRow row, DocMethodGroup dataRecord)
    {
      row.LJCSetInt32(DocMethodGroup.ColumnID, dataRecord.ID);
      //row.LJCSetInt32(DocMethodGroup.ColumnDocClassID
      //  , dataRecord.DocClassID);
    }
    #endregion

    #region Action Methods

    /// <summary>Displays a detail dialog for a new record.</summary>
    internal void DoNew()
    {
      if (mClassGrid.CurrentRow is LJCGridRow classRow)
      {
        var detail = new MethodGroupDetail()
        {
          LJCClassID = DocClassID(),
          Managers = Managers,
          Sequence = mMethodGroupGrid.Rows.Count + 1
        };
        detail.LJCChange += Detail_Change;
        detail.ShowDialog();
      }
    }

    /// <summary>Displays a detail dialog to edit an existing record.</summary>
    internal void DoEdit()
    {
      if (mClassGrid.CurrentRow is LJCGridRow _
        && mMethodGroupGrid.CurrentRow is LJCGridRow _)
      {
        var detail = new MethodGroupDetail()
        {
          LJCGroupID = MethodGroupID(),
          LJCClassID = DocClassID(),
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

      if (mClassGrid.CurrentRow is LJCGridRow _
        && mMethodGroupGrid.CurrentRow is LJCGridRow _)
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
          { DocMethodGroup.ColumnID, MethodGroupID() },
          { DocMethodGroup.ColumnDocClassID, DocClassID() }
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

      if (success
        && mMethodGroupGrid.CurrentRow is LJCGridRow methodGroupRow)
      {
        mMethodGroupGrid.Rows.Remove(methodGroupRow);
        mDocList.TimedChange(Change.MethodGroup);
      }
    }

    /// <summary>Refreshes the list.</summary> 
    internal void DoRefresh()
    {
      mDocList.Cursor = Cursors.WaitCursor;

      // Save the original row.
      var methodGroupID = MethodGroupID();

      DataRetrieve();
      if (methodGroupID > 0)
      {
        var dataRecord = new DocMethodGroup()
        {
          ID = methodGroupID
        };
        RowSelect(dataRecord);
      }
      mDocList.Cursor = Cursors.Default;
    }

    /// <summary>Resets the Sequence column values.</summary>
    internal void DoResetSequence()
    {
      var manager = Managers.DocMethodGroupManager;
      manager.ClassID = DocClassID();
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
          mMethodGroupGrid.LJCSetCurrentRow(row, true);
          DoRefresh();
          mDocList.TimedChange(Change.MethodGroup);
        }
      }
    }
    #endregion

    #region Other Methods

    // The DragDrop method.
    /// <include path='items/DoDragDrop/*' file='../../Doc/MethodGroupGridCode.xml'/>
    internal void DoDragDrop(short docClassID, DragEventArgs e)
    {
      var sourceRow = e.Data.GetData(typeof(LJCGridRow)) as LJCGridRow;
      var dragDataName = sourceRow.LJCGetString("DragDataName");
      if (dragDataName == mMethodGroupGrid.LJCDragDataName)
      {
        var targetIndex = mMethodGroupGrid.LJCGetDragRowIndex(new Point(e.X, e.Y));
        if (targetIndex >= 0)
        {
          // Get source group.
          var sourceName = MethodGroupHeadingName(MethodGroupID(sourceRow));
          var manager = Managers.DocMethodGroupManager;
          var sourceGroup = manager.RetrieveWithUnique(docClassID, sourceName);

          // Get target group.
          var targetRow = mMethodGroupGrid.Rows[targetIndex] as LJCGridRow;
          var targetName = MethodGroupHeadingName(MethodGroupID(targetRow));
          var targetGroup = manager.RetrieveWithUnique(docClassID, targetName);

          var sourceSequence = sourceGroup.Sequence;
          var targetSequence = targetGroup.Sequence;
          manager.ClassID = sourceGroup.DocClassID;
          manager.ChangeSequence(sourceSequence, targetSequence);
          DoRefresh();
        }
      }
    }

    /// <summary>Setup the grid columns.</summary>
    internal void SetupGrid()
    {
      // Setup default grid columns if no columns are defined.
      if (0 == mMethodGroupGrid.Columns.Count)
      {
        List<string> propertyNames = new List<string>()
        {
          DocMethodGroup.ColumnHeadingName,
          DocMethodGroup.ColumnHeadingTextCustom
        };

        // Get the grid columns from the manager Data Definition.
        var classGroupManager = Managers.DocMethodGroupManager;
        GridColumns = classGroupManager.GetColumns(propertyNames);

        // Setup the grid columns.
        mMethodGroupGrid.LJCAddColumns(GridColumns);
        mMethodGroupGrid.LJCDragDataName = "DocMethodGroup";
      }
    }

    // Retrieves the current row item ID.
    private short DocClassID(LJCGridRow docClassRow = null)
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

    // Retrieves the DocMethodGroup name.
    private string MethodGroupHeadingName(short methodGroupID)
    {
      string retValue = null;

      var docMethodGroup = MethodGroupWithID(methodGroupID);
      if (docMethodGroup != null)
      {
        retValue = docMethodGroup.HeadingName;
      }
      return retValue;
    }

    // Retrieves the current row item ID.
    /// <include path='items/MethodGroupID/*' file='../../Doc/MethodGroupGridCode.xml'/>
    private short MethodGroupID(LJCGridRow methodGroupRow = null)
    {
      short retValue = 0;

      if (null == methodGroupRow)
      {
        methodGroupRow = mMethodGroupGrid.CurrentRow as LJCGridRow;
      }
      if (methodGroupRow != null)
      {
        retValue = (short)methodGroupRow.LJCGetInt32(DocMethodGroup.ColumnID);
      }
      return retValue;
    }

    // Retrieves the MethodGroup with the ID value.
    private DocMethodGroup MethodGroupWithID(short methodGroupID)
    {
      DocMethodGroup retValue = null;

      if (methodGroupID > 0)
      {
        var manager = Managers.DocMethodGroupManager;
        retValue = manager.RetrieveWithID(methodGroupID);
      }
      return retValue;
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
        LJCDataGrid grid = mMethodGroupGrid;
        int currentIndex = grid.CurrentRow.Index;
        detail.LJCNext = false;
        if (currentIndex < grid.Rows.Count - 1)
        {
          grid.LJCSetCurrentRow(currentIndex + 1, true);
          var methodGroupID = MethodGroupID();
          if (methodGroupID > 0)
          {
            detail.LJCNext = true;
            detail.LJCGroupID = methodGroupID;
          }
        }
      }
    }

    // Checks for Previous item.
    private void PreviousItem(MethodGroupDetail detail)
    {
      if (detail.LJCPrevious)
      {
        LJCDataGrid groupGrid = mMethodGroupGrid;
        int currentIndex = groupGrid.CurrentRow.Index;
        detail.LJCPrevious = false;
        if (currentIndex > 0)
        {
          groupGrid.LJCSetCurrentRow(currentIndex - 1, true);
          var methodGroupID = MethodGroupID();
          if (methodGroupID > 0)
          {
            detail.LJCPrevious = true;
            detail.LJCGroupID = methodGroupID;
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

    private readonly LJCDataGrid mClassGrid;
    private readonly LJCGenDocList mDocList;
    private readonly LJCDataGrid mMethodGroupGrid;
    #endregion
  }
}
