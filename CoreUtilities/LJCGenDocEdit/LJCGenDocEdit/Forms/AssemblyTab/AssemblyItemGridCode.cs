// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// AssemblyItemGridCode.cs
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
  /// <summary>The AssemblyItem grid code.</summary>
  internal class AssemblyItemGridCode
  {
    #region Constructors

    /// <summary>Initializes an object instance.</summary>
    internal AssemblyItemGridCode(LJCGenDocList parentList)
    {
      mDocList = parentList;
      mAssemblyGrid = mDocList.AssemblyItemGrid;
      mAssemblyGroupGrid = mDocList.AssemblyGroupGrid;
      Managers = mDocList.Managers;
      DocAssemblyManager = Managers.DocAssemblyManager;
    }
    #endregion

    #region Data Methods

    /// <summary>Retrieves the list rows.</summary>
    internal void DataRetrieve()
    {
      mAssemblyGrid.LJCRowsClear();

      if (mDocList.AssemblyGroupGrid.CurrentRow is LJCGridRow _)
      {
        mDocList.Cursor = Cursors.WaitCursor;

        var manager = DocAssemblyManager;
        var names = new List<string>()
        {
          DocAssembly.ColumnSequence
        };
        manager.SetOrderBy(names);

        var keyColumns = new DbColumns()
        {
          { DocAssembly.ColumnDocAssemblyGroupID, AssemblyGroupID() }
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
        mDocList.DoChange(Change.AssemblyItem);
      }
    }

    // Selects a row based on the key record values.
    /// <include path='items/RowSelect/*' file='../../../../LJCDocLib/Common/List.xml'/>
    internal bool RowSelect(DocAssembly dataRecord)
    {
      bool retValue = false;

      if (dataRecord != null)
      {
        mDocList.Cursor = Cursors.WaitCursor;
        foreach (LJCGridRow row in mAssemblyGrid.Rows)
        {
          if (AssemblyID(row) == dataRecord.ID)
          {
            // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
            mAssemblyGrid.LJCSetCurrentRow(row, true);
            retValue = true;
            break;
          }
        }
        mDocList.Cursor = Cursors.Default;
      }
      return retValue;
    }

    // Adds a grid row and updates it with the record values.
    private LJCGridRow RowAdd(DocAssembly dataRecord)
    {
      var retValue = mAssemblyGrid.LJCRowAdd();
      SetStored(retValue, dataRecord);
      mAssemblyGrid.LJCRowSetValues(retValue, dataRecord);
      return retValue;
    }

    // Adds a grid row and updates it with the result values.
    private LJCGridRow RowAddValues(DbValues dbValues)
    {
      var retValue = mAssemblyGrid.LJCRowAdd();
      var columnName = DocAssembly.ColumnID;
      retValue.LJCSetInt32(columnName, dbValues.LJCGetInt32(columnName));

      mAssemblyGrid.LJCRowSetValues(retValue, dbValues);
      return retValue;
    }

    // Updates the current row with the record values.
    private void RowUpdate(DocAssembly dataRecord)
    {
      if (mAssemblyGrid.CurrentRow is LJCGridRow row)
      {
        SetStored(row, dataRecord);
        mAssemblyGrid.LJCRowSetValues(row, dataRecord);
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
      if (mAssemblyGroupGrid.CurrentRow is LJCGridRow _)
      {
        var detail = new AssemblyDetail()
        {
          LJCGroupID = AssemblyGroupID(),
          Managers = Managers
        };
        detail.LJCChange += Detail_Change;
        detail.ShowDialog();
      }
    }

    /// <summary>Displays a detail dialog to edit an existing record.</summary>
    internal void DoEdit()
    {
      if (mAssemblyGroupGrid.CurrentRow is LJCGridRow _
        && mAssemblyGrid.CurrentRow is LJCGridRow _)
      {
        var detail = new AssemblyDetail()
        {
          LJCAssemblyID = AssemblyID(),
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

      var assemblyGroupRow = mAssemblyGroupGrid.CurrentRow as LJCGridRow;
      var assemblyRow = mAssemblyGrid.CurrentRow as LJCGridRow;
      if (assemblyGroupRow != null
        && assemblyRow != null)
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
          { DocAssembly.ColumnDocAssemblyGroupID, AssemblyGroupID() },
          { DocAssembly.ColumnID, AssemblyID() }
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
        mAssemblyGrid.Rows.Remove(assemblyRow);
        mDocList.TimedChange(Change.AssemblyItem);
      }
    }

    /// <summary>Refreshes the list.</summary> 
    internal void DoRefresh()
    {
      mDocList.Cursor = Cursors.WaitCursor;

      // Save the original row.
      var assemblyID = AssemblyID();

      DataRetrieve();
      if (assemblyID > 0)
      {
        var dataRecord = new DocAssembly()
        {
          ID = assemblyID
        };
        RowSelect(dataRecord);
      }
      mDocList.Cursor = Cursors.Default;
    }

    /// <summary>Resets the Sequence column values.</summary>
    internal void DoResetSequence()
    {
      var manager = Managers.DocAssemblyManager;
      manager.AssemblyGroupID = AssemblyGroupID();
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
          mAssemblyGrid.LJCSetCurrentRow(row, true);
          DoRefresh();
          mDocList.TimedChange(Change.AssemblyItem);
        }
      }
    }
    #endregion

    #region Other Methods

    // Retrieves the current row item ID.
    /// <include path='items/AssemblyGroupID/*' file='../../Doc/AssemblyItemGridCode.xml'/>
    internal short AssemblyGroupID(LJCGridRow assemblyGroupRow = null)
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

    // Retrieves the current row item ID.
    /// <include path='items/AssemblyID/*' file='../../Doc/AssemblyItemGridCode.xml'/>
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

    // Retrieves the current row item.
    /// <include path='items/CurrentItem/*' file='../../../../LJCDocLib/Common/List.xml'/>
    internal DocAssembly CurrentAssembly()
    {
      DocAssembly retValue = null;

      if (mAssemblyGrid.CurrentRow is LJCGridRow _)
      {
        var assemblyID = AssemblyID();
        if (assemblyID > 0)
        {
          var manager = DocAssemblyManager;
          retValue = manager.RetrieveWithID(assemblyID);
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
      if (dragDataName == mAssemblyGrid.LJCDragDataName)
      {
        var targetIndex = mAssemblyGrid.LJCGetDragRowIndex(new Point(e.X, e.Y));
        if (targetIndex >= 0)
        {
          // Get source group.
          var sourceName = sourceRow.LJCGetString(DocAssembly.ColumnName);
          var manager = DocAssemblyManager;
          var sourceGroup = manager.RetrieveWithName(sourceName);

          // Get target group.
          var targetRow = mAssemblyGrid.Rows[targetIndex] as LJCGridRow;
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

    /// <summary>Setup the grid display columns.</summary>
    internal void SetupGrid()
    {
      // Setup default display columns if no columns are defined.
      if (0 == mAssemblyGrid.Columns.Count)
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
        mAssemblyGrid.LJCAddDisplayColumns(DisplayColumns);
        mAssemblyGrid.LJCDragDataName = "DocAssembly";
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
        LJCDataGrid grid = mAssemblyGrid;
        int currentIndex = grid.CurrentRow.Index;
        detail.LJCNext = false;
        if (currentIndex < grid.Rows.Count - 1)
        {
          grid.LJCSetCurrentRow(currentIndex + 1, true);
          if (AssemblyID() > 0)
          {
            detail.LJCNext = true;
            detail.LJCAssemblyID = AssemblyID();
          }
        }
      }
    }

    // Checks for Previous item.
    private void PreviousItem(AssemblyDetail detail)
    {
      if (detail.LJCPrevious)
      {
        LJCDataGrid grid = mAssemblyGrid;
        int currentIndex = grid.CurrentRow.Index;
        detail.LJCPrevious = false;
        if (currentIndex > 0)
        {
          grid.LJCSetCurrentRow(currentIndex - 1, true);
          if (AssemblyID() > 0)
          {
            detail.LJCPrevious = true;
            detail.LJCAssemblyID = AssemblyID();
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

    private readonly LJCDataGrid mAssemblyGrid;
    private readonly LJCDataGrid mAssemblyGroupGrid;
    private readonly LJCGenDocList mDocList;
    #endregion
  }
}
