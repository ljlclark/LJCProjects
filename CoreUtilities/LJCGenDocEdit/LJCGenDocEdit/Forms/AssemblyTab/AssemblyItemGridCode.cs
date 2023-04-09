// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// AssemblyItemGridCode.cs
using LJCDocLibDAL;
using LJCNetCommon;
using LJCWinFormControls;
using static LJCGenDocEdit.LJCGenDocList;
using System.Collections.Generic;
using System.Windows.Forms;
using System;

namespace LJCGenDocEdit
{
  internal class AssemblyItemGridCode
  {
    #region Constructors

    // Initializes an object instance.
    internal AssemblyItemGridCode(LJCGenDocList parent)
    {
      mParent = parent;
      mGrid = mParent.AssemblyItemGrid;
      mManagers = mParent.Managers;
    }
    #endregion

    #region Data Methods

    /// <summary>
    /// Retrieves the currently selecteditem.
    /// </summary>
    /// <returns>The currently selected item.</returns>
    internal DocAssembly CurrentItem()
    {
      DocAssembly retValue = null;

      if (mGrid.CurrentRow is LJCGridRow row)
      {
        var id = (short)row.LJCGetInt32(DocAssembly.ColumnID);
        if (id > 0)
        {
          var manager = mManagers.DocAssemblyManager;
          retValue = manager.RetrieveWithID(id);
        }
      }
      return retValue;
    }

    // Retrieves the list rows.
    internal void DataRetrieve()
    {
      mGrid.LJCRowsClear();

      if (mParent.AssemblyGroupGrid.CurrentRow is LJCGridRow parentRow)
      {
        mParent.Cursor = Cursors.WaitCursor;
        var parentID = (short)parentRow.LJCGetInt32(DocAssembly.ColumnID);

        var manager = mManagers.DocAssemblyManager;
        var dataRecords = manager.LoadWithParent(parentID);

        if (NetCommon.HasItems(dataRecords))
        {
          foreach (DocAssembly dataRecord in dataRecords)
          {
            RowAdd(dataRecord);
          }
        }
        mParent.Cursor = Cursors.Default;
        mParent.DoChange(Change.AssemblyItem);
        //mParent.TimedChange(Change.AssemblyItem);
      }
    }

    // Selects a row based on the key record values.
    /// <summary>
    /// Selects a row based on the key record values.
    /// </summary>
    /// <param name="dataRecord">The data record to select.</param>
    /// <returns>True if the row was selected, otherwise false.</returns>
    internal bool RowSelect(DocAssembly dataRecord)
    {
      bool retValue = false;

      if (dataRecord != null)
      {
        mParent.Cursor = Cursors.WaitCursor;
        foreach (LJCGridRow row in mGrid.Rows)
        {
          var rowID = row.LJCGetInt32(DocAssembly.ColumnID);
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

    // Setup the grid display columns.
    internal void SetupGrid()
    {
      // Setup default display columns if no columns are defined.
      if (0 == mGrid.Columns.Count)
      {
        List<string> columnNames = new List<string>()
        {
          DocAssembly.ColumnName,
          DocAssembly.ColumnDescription
        };

        // Get the display columns from the manager Data Definition.
        var assemblyManager = mManagers.DocAssemblyManager;
        DisplayColumns = assemblyManager.GetColumns(columnNames);

        // Setup the grid display columns.
        mGrid.LJCAddDisplayColumns(DisplayColumns);
      }
    }

    // Adds a grid row and updates it with the record values.
    private LJCGridRow RowAdd(DocAssembly dataRecord)
    {
      var retValue = mGrid.LJCRowAdd();
      SetStoredValues(retValue, dataRecord);

      // Sets the row values from a data object.
      mGrid.LJCRowSetValues(retValue, dataRecord);
      return retValue;
    }

    // Updates the current row with the record values.
    private void RowUpdate(DocAssembly dataRecord)
    {
      if (mGrid.CurrentRow is LJCGridRow row)
      {
        SetStoredValues(row, dataRecord);
        mGrid.LJCRowSetValues(row, dataRecord);
      }
    }

    // Sets the row stored values.
    private void SetStoredValues(LJCGridRow row, DocAssembly dataRecord)
    {
      row.LJCSetInt32(DocAssembly.ColumnID, dataRecord.ID);
      row.LJCSetString(DocAssembly.ColumnName, dataRecord.Name);
    }
    #endregion

    #region Action Methods

    // Displays a detail dialog to edit an existing record.
    internal void DoEdit()
    {
      if (mParent.AssemblyGroupGrid.CurrentRow is LJCGridRow parentRow
        && mGrid.CurrentRow is LJCGridRow row)
      {
        // Data from items.
        var parentID = (short)parentRow.LJCGetInt32(DocAssemblyGroup.ColumnID);
        var parentName = parentRow.LJCGetString(DocAssemblyGroup.ColumnHeading);
        var id = (short)row.LJCGetInt32(DocAssembly.ColumnID);

        var detail = new AssemblyDetail()
        {
          LJCID = id,
          LJCParentID = parentID,
          LJCParentName = parentName,
          Managers = mManagers
        };
        detail.LJCChange += Detail_Change;
        detail.ShowDialog();
      }
    }

    // Refreshes the list.
    internal void DoRefresh()
    {
      short id = 0;

      mParent.Cursor = Cursors.WaitCursor;
      if (mGrid.CurrentRow is LJCGridRow row)
      {
        id = (short)row.LJCGetInt32(DocAssembly.ColumnID);
      }
      DataRetrieve();

      // Select the original row.
      if (id > 0)
      {
        var dataRecord = new DocAssembly()
        {
          ID = id
        };
        RowSelect(dataRecord);
      }
      mParent.Cursor = Cursors.Default;
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
        }
        else
        {
          // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
          var row = RowAdd(dataRecord);
          mGrid.LJCSetCurrentRow(row, true);
          mParent.TimedChange(Change.AssemblyItem);
        }
      }
    }
    #endregion

    internal DbColumns DisplayColumns { get; set; }

    #region Class Data

    private readonly LJCDataGrid mGrid;
    private readonly ManagersDocGen mManagers;
    private readonly LJCGenDocList mParent;
    #endregion
  }
}
