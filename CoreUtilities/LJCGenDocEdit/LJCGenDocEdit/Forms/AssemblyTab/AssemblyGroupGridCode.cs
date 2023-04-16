// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// AssemblyGroupGridCode.cs
using LJCDocLibDAL;
using LJCNetCommon;
using LJCWinFormControls;
using static LJCGenDocEdit.LJCGenDocList;
using System.Windows.Forms;
using System.Collections.Generic;
using System;
using LJCWinFormCommon;
using System.Drawing;

namespace LJCGenDocEdit
{
  // Initializes an object instance.
  internal class AssemblyGroupGridCode
  {
    #region Constructors

    // Initializes an object instance.
    internal AssemblyGroupGridCode(LJCGenDocList parent)
    {
      mParent = parent;
      mGrid = mParent.AssemblyGroupGrid;
      mManagers = mParent.Managers;
      DocAssemblyGroupManager = mManagers.DocAssemblyGroupManager;
    }
    #endregion

    #region Methods

    /// <summary>
    /// Retrieves the currently selecteditem.
    /// </summary>
    /// <returns>The currently selected item.</returns>
    internal DocAssemblyGroup CurrentItem()
    {
      DocAssemblyGroup retValue = null;

      if (mGrid.CurrentRow is LJCGridRow row)
      {
        var id = (short)row.LJCGetInt32(DocAssemblyGroup.ColumnID);
        if (id > 0)
        {
          var manager = DocAssemblyGroupManager;
          retValue = manager.RetrieveWithID(id);
        }
      }
      return retValue;
    }

    // Setup the grid display columns.
    internal void SetupGrid()
    {
      // Setup default display columns if no columns are defined.
      if (0 == mGrid.Columns.Count)
      {
        List<string> columnNames = new List<string>()
        {
          DocAssemblyGroup.ColumnName,
          DocAssemblyGroup.ColumnHeading
        };

        // Get the display columns from the manager Data Definition.
        var manager = DocAssemblyGroupManager;
        DisplayColumns = manager.GetColumns(columnNames);

        // Setup the grid display columns.
        mGrid.LJCAddDisplayColumns(DisplayColumns);
        mGrid.LJCDragDataName = "DocAssemblyGroup";
      }
    }
    #endregion

    #region Data Methods

    // Retrieves the list rows.
    internal void DataRetrieve()
    {
      mParent.Cursor = Cursors.WaitCursor;
      mGrid.LJCRowsClear();

      var manager = DocAssemblyGroupManager;
      var names = new List<string>()
      {
        DocAssemblyGroup.ColumnSequence
      };
      manager.SetOrderBy(names);
      var dataRecords = manager.Load();

      if (NetCommon.HasItems(dataRecords))
      {
        foreach (DocAssemblyGroup dataRecord in dataRecords)
        {
          RowAdd(dataRecord);
        }
      }
      mParent.Cursor = Cursors.Default;
      mParent.DoChange(Change.AssemblyGroup);
      //mParent.TimedChange(Change.AssemblyGroup);
    }

    // Selects a row based on the key record values.
    /// <summary>
    /// Selects a row based on the key record values.
    /// </summary>
    /// <param name="dataRecord">The data record to select.</param>
    /// <returns>True if the row was selected, otherwise false.</returns>
    internal bool RowSelect(DocAssemblyGroup dataRecord)
    {
      bool retValue = false;

      if (dataRecord != null)
      {
        mParent.Cursor = Cursors.WaitCursor;
        foreach (LJCGridRow row in mGrid.Rows)
        {
          var rowID = row.LJCGetInt32(DocAssemblyGroup.ColumnID);
          if (rowID == dataRecord.ID)
          {
            // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
            mGrid.LJCSetCurrentRow(row, true);
            retValue = true;
            break;
          }
        }
        mParent.Cursor = Cursors.Default;
      }
      return retValue;
    }

    // Adds a grid row and updates it with the record values.
    private LJCGridRow RowAdd(DocAssemblyGroup dataRecord)
    {
      var retValue = mGrid.LJCRowAdd();
      SetStoredValues(retValue, dataRecord);

      // Sets the row values from a data object.
      mGrid.LJCRowSetValues(retValue, dataRecord);
      return retValue;
    }

    // Updates the current row with the record values.
    private void RowUpdate(DocAssemblyGroup dataRecord)
    {
      if (mGrid.CurrentRow is LJCGridRow row)
      {
        SetStoredValues(row, dataRecord);
        mGrid.LJCRowSetValues(row, dataRecord);
      }
    }

    // Sets the row stored values.
    private void SetStoredValues(LJCGridRow row, DocAssemblyGroup dataRecord)
    {
      row.LJCSetInt32(DocAssemblyGroup.ColumnID, dataRecord.ID);
      row.LJCSetString(DocAssemblyGroup.ColumnName, dataRecord.Name);
      row.LJCSetString(DocAssemblyGroup.ColumnHeading, dataRecord.Heading);
    }
    #endregion

    #region Action Methods

    // Displays a detail dialog for a new record.
    internal void DoNew()
    {
      var detail = new AssemblyGroupDetail()
      {
        Managers = mManagers
      };
      detail.LJCChange += Detail_Change;
      detail.ShowDialog();
    }

    // Displays a detail dialog to edit an existing record.
    internal void DoEdit()
    {
      if (mGrid.CurrentRow is LJCGridRow row)
      {
        // Data from items.
        var id = (short)row.LJCGetInt32(DocAssemblyGroup.ColumnID);

        var detail = new AssemblyGroupDetail()
        {
          LJCID = id,
          Managers = mManagers
        };
        detail.LJCChange += Detail_Change;
        detail.ShowDialog();
      }
    }

    // Deletes the selected row.
    internal void DoDelete()
    {
      string title;
      string message;
      bool success = false;

      var row = mGrid.CurrentRow as LJCGridRow;
      if (row != null)
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
        var id = row.LJCGetInt32(DocAssemblyGroup.ColumnID);

        var keyRecord = new DbColumns()
        {
          { DocAssemblyGroup.ColumnID, id }
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
        mGrid.Rows.Remove(row);
        mParent.TimedChange(Change.AssemblyGroup);
      }
    }

    // Refreshes the list.
    internal void DoRefresh()
    {
      short id = 0;

      mParent.Cursor = Cursors.WaitCursor;
      if (mGrid.CurrentRow is LJCGridRow row)
      {
        id = (short)row.LJCGetInt32(DocAssemblyGroup.ColumnID);
      }
      DataRetrieve();

      // Select the original row.
      if (id > 0)
      {
        var dataRecord = new DocAssemblyGroup()
        {
          ID = id
        };
        RowSelect(dataRecord);
      }
      mParent.Cursor = Cursors.Default;
    }

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
        }
        else
        {
          // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
          var row = RowAdd(dataRecord);
          mGrid.LJCSetCurrentRow(row, true);
          mParent.TimedChange(Change.AssemblyGroup);
        }
      }
    }
    #endregion

    // The DragDrop method.
    internal void DoDragDrop(DragEventArgs e)
    {
      var sourceRow = e.Data.GetData(typeof(LJCGridRow)) as LJCGridRow;
      var dragDataName = sourceRow.LJCGetString("DragDataName");
      if (dragDataName == mGrid.LJCDragDataName)
      {
        var targetIndex = mGrid.LJCGetDragRowIndex(new Point(e.X, e.Y));
        if (targetIndex >= 0)
        {
          // Get source group.
          var sourceName = sourceRow.LJCGetString(DocAssemblyGroup.ColumnName);
          var manager = mManagers.DocAssemblyGroupManager;
          var sourceGroup = manager.RetrieveWithUnique(sourceName);

          // Get target group.
          var targetRow = mGrid.Rows[targetIndex] as LJCGridRow;
          var targetName = targetRow.LJCGetString(DocGenGroup.ColumnName);
          var targetGroup = manager.RetrieveWithUnique(targetName);

          var sourceSequence = sourceGroup.Sequence;
          var targetSequence = targetGroup.Sequence;
          manager.ChangeSequence(sourceSequence, targetSequence);
        }
      }
    }

    internal DbColumns DisplayColumns { get; set; }

    internal DocAssemblyGroupManager DocAssemblyGroupManager { get; set; }

    #region Class Data

    private readonly LJCDataGrid mGrid;
    private readonly ManagersDocGen mManagers;
    private readonly LJCGenDocList mParent;
    #endregion
  }
}
