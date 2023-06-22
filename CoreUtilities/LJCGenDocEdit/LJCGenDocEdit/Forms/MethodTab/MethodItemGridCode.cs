// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// MethodItemGridCode.cs
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
  /// <summary>The MethodItem grid code.</summary>
  internal class MethodItemGridCode
  {
    #region Constructors

    /// <summary>Initializes an object instance.</summary>
    internal MethodItemGridCode(LJCGenDocList parentList)
    {
      mDocList = parentList;
      mClassGrid = mDocList.ClassItemGrid;
      mMethodGrid = mDocList.MethodItemGrid;
      mMethodGroupGrid = mDocList.MethodGroupGrid;
      Managers = mDocList.Managers;
    }
    #endregion

    #region Data Methods

    /// <summary>Retrieves the list rows.</summary>
    internal void DataRetrieve()
    {
      mMethodGrid.LJCRowsClear();

      if (mDocList.MethodGroupGrid.CurrentRow is LJCGridRow _)
      {
        mDocList.Cursor = Cursors.WaitCursor;
        var manager = Managers.DocMethodManager;
        var names = new List<string>()
        {
          DocMethod.ColumnSequence
        };
        manager.SetOrderBy(names);

        var keyColumns = new DbColumns()
        {
          { DocMethod.ColumnDocMethodGroupID, MethodGroupID() }
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
        mDocList.DoChange(Change.MethodItem);
      }
    }

    // Selects a row based on the key record values.
    /// <include path='items/RowSelect/*' file='../../../../LJCDocLib/Common/List.xml'/>
    internal bool RowSelect(DocMethod dataRecord)
    {
      bool retValue = false;

      if (dataRecord != null)
      {
        mDocList.Cursor = Cursors.WaitCursor;
        foreach (LJCGridRow row in mMethodGrid.Rows)
        {
          if (MethodID(row) == dataRecord.ID)
          {
            // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
            mMethodGrid.LJCSetCurrentRow(row, true);
            retValue = true;
            break;
          }
        }
        mDocList.Cursor = Cursors.Default;
      }
      return retValue;
    }

    // Adds a grid row and updates it with the record values.
    private LJCGridRow RowAdd(DocMethod dataRecord)
    {
      var retValue = mMethodGrid.LJCRowAdd();
      SetStoredValues(retValue, dataRecord);
      mMethodGrid.LJCRowSetValues(retValue, dataRecord);
      return retValue;
    }

    // Adds a grid row and updates it with the result values.
    private LJCGridRow RowAddValues(DbValues dbValues)
    {
      var retValue = mMethodGrid.LJCRowAdd();
      var columnName = DocMethod.ColumnID;
      retValue.LJCSetInt32(columnName, dbValues.LJCGetInt32(columnName));

      mMethodGrid.LJCRowSetValues(retValue, dbValues);
      return retValue;
    }

    // Updates the current row with the record values.
    private void RowUpdate(DocMethod dataRecord)
    {
      if (mMethodGrid.CurrentRow is LJCGridRow row)
      {
        SetStoredValues(row, dataRecord);
        mMethodGrid.LJCRowSetValues(row, dataRecord);
      }
    }

    // Sets the row stored values.
    private void SetStoredValues(LJCGridRow row, DocMethod dataRecord)
    {
      row.LJCSetInt32(DocMethod.ColumnID, dataRecord.ID);
    }
    #endregion

    #region Action Methods

    /// <summary>Displays a detail dialog for a new record.</summary>
    internal void DoNew()
    {
      if (mClassGrid.CurrentRow is LJCGridRow _)
      {
        var detail = new MethodDetail()
        {
          LJCGroupID = MethodGroupID(),
          LJCClassID = ClassID(),
          Managers = Managers
        };
        detail.LJCChange += Detail_Change;
        detail.ShowDialog();
      }
    }

    /// <summary>Displays a detail dialog to edit an existing record.</summary>
    internal void DoEdit()
    {
      if (mClassGrid.CurrentRow is LJCGridRow _
        && mMethodGrid.CurrentRow is LJCGridRow _)
      {
        var detail = new MethodDetail()
        {
          LJCID = MethodID(),
          LJCGroupID = MethodGroupID(),
          LJCClassID = ClassID(),
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

      var methodRow = mMethodGrid.CurrentRow as LJCGridRow;
      if (methodRow != null)
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
          { DocMethod.ColumnID, MethodID() }
        };
        var manager = Managers.DocMethodManager;
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
        mMethodGrid.Rows.Remove(methodRow);
        mDocList.TimedChange(Change.MethodItem);
      }
    }

    /// <summary>Refreshes the list.</summary> 
    internal void DoRefresh()
    {
      mDocList.Cursor = Cursors.WaitCursor;
      DataRetrieve();

      // Select the original row.
      var methodID = MethodID();
      if (methodID > 0)
      {
        var dataRecord = new DocMethod()
        {
          ID = methodID
        };
        RowSelect(dataRecord);
      }
      mDocList.Cursor = Cursors.Default;
    }

    /// <summary>Resets the Sequence column values.</summary>
    internal void DoResetSequence()
    {
      var manager = Managers.DocMethodManager;
      manager.MethodGroupID = MethodGroupID();
      manager.ResetSequence();
    }

    // Adds new row or updates row with changes from the detail dialog.
    private void Detail_Change(object sender, EventArgs e)
    {
      var detail = sender as MethodDetail;
      if (detail.LJCRecord != null)
      {
        var dataRecord = detail.LJCRecord;
        if (detail.LJCIsUpdate)
        {
          RowUpdate(dataRecord);
          CheckPreviousAndNext(detail);
        }
        else
        {
          // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
          var row = RowAdd(dataRecord);
          CheckPreviousAndNext(detail);
          mMethodGrid.LJCSetCurrentRow(row, true);
          mDocList.TimedChange(Change.MethodItem);
        }
      }
    }
    #endregion

    #region Other Methods

    // Retrieves the current row item ID.
    /// <summary>
    /// Retrieves the current row item ID.
    /// </summary>
    /// <param name="classRow">The DocClass grid row.</param>
    /// <returns>The DocClass ID.</returns>
    internal short ClassID(LJCGridRow classRow = null)
    {
      short retValue = 0;

      if (null == classRow)
      {
        classRow = mClassGrid.CurrentRow as LJCGridRow;
      }
      if (classRow != null)
      {
        retValue = (short)classRow.LJCGetInt32(DocClass.ColumnID);
      }
      return retValue;
    }

    // The DragDrop method.
    /// <include path='items/DoDragDrop1/*' file='../../../../LJCDocLib/Common/List.xml'/>
    internal void DoDragDrop(short parentID, DragEventArgs e)
    {
      var sourceRow = e.Data.GetData(typeof(LJCGridRow)) as LJCGridRow;
      var dragDataName = sourceRow.LJCGetString("DragDataName");
      if (dragDataName == mMethodGrid.LJCDragDataName)
      {
        var targetIndex = mMethodGrid.LJCGetDragRowIndex(new Point(e.X, e.Y));
        if (targetIndex >= 0)
        {
          // Get source group.
          var sourceName = sourceRow.LJCGetString(DocMethod.ColumnName);
          var manager = Managers.DocMethodManager;
          var sourceGroup = manager.RetrieveWithUnique(parentID, sourceName);

          // Get target group.
          var targetRow = mMethodGrid.Rows[targetIndex] as LJCGridRow;
          var targetName = targetRow.LJCGetString(DocMethod.ColumnName);
          var targetGroup = manager.RetrieveWithUnique(parentID, targetName);

          var sourceSequence = sourceGroup.Sequence;
          var targetSequence = targetGroup.Sequence;
          manager.MethodGroupID = sourceGroup.DocMethodGroupID;
          manager.ChangeSequence(sourceSequence, targetSequence);
          DoRefresh();
        }
      }
    }

    // Retrieves the current row item ID.
    /// <summary>
    /// Retrieves the current row item ID.
    /// </summary>
    /// <param name="methodGroupRow">The MethodGroup grid row.</param>
    /// <returns>The MethodGroup ID.</returns>
    internal short MethodGroupID(LJCGridRow methodGroupRow = null)
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

    // Retrieves the current row item ID.
    /// <summary>
    /// Retrieves the current row item ID.
    /// </summary>
    /// <param name="methodRow">The DocMethod grid row.</param>
    /// <returns>The DocMethod ID.</returns>
    internal short MethodID(LJCGridRow methodRow = null)
    {
      short retValue = 0;

      if (null == methodRow)
      {
        methodRow = mMethodGrid.CurrentRow as LJCGridRow;
      }
      if (methodRow != null)
      {
        retValue = (short)methodRow.LJCGetInt32(DocMethod.ColumnID);
      }
      return retValue;
    }

    /// <summary>Setup the grid display columns.</summary>
    internal void SetupGrid()
    {
      // Setup default display columns if no columns are defined.
      if (0 == mMethodGrid.Columns.Count)
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
        mMethodGrid.LJCAddDisplayColumns(DisplayColumns);
        mMethodGrid.LJCDragDataName = "DocMethod";
      }
    }
    #endregion

    #region Private Methods

    // Checks for Previous and Next items.
    private void CheckPreviousAndNext(MethodDetail detail)
    {
      PreviousItem(detail);
      NextItem(detail);
    }

    // Checks for Next item.
    private void NextItem(MethodDetail detail)
    {
      if (detail.LJCNext)
      {
        LJCDataGrid grid = mMethodGrid;
        int currentIndex = grid.CurrentRow.Index;
        detail.LJCNext = false;
        if (currentIndex < grid.Rows.Count - 1)
        {
          grid.LJCSetCurrentRow(currentIndex + 1, true);
          var methodID = MethodID();
          if (methodID > 0)
          {
            detail.LJCNext = true;
            detail.LJCID = methodID;
          }
        }
      }
    }

    // Checks for Previous item.
    private void PreviousItem(MethodDetail detail)
    {
      if (detail.LJCPrevious)
      {
        LJCDataGrid grid = mMethodGrid;
        int currentIndex = grid.CurrentRow.Index;
        detail.LJCPrevious = false;
        if (currentIndex > 0)
        {
          grid.LJCSetCurrentRow(currentIndex - 1, true);
          var methodID = MethodID();
          if (methodID > 0)
          {
            detail.LJCPrevious = true;
            detail.LJCID = methodID;
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
    private readonly LJCDataGrid mMethodGrid;
    private readonly LJCDataGrid mMethodGroupGrid;
    #endregion
  }
}
