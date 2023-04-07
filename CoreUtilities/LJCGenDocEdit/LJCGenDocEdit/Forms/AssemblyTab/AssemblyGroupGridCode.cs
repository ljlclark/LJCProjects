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
          var manager = mManagers.DocAssemblyGroupManager;
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
        var assemblyGroupManager = mManagers.DocAssemblyGroupManager;
        DisplayColumns = assemblyGroupManager.GetColumns(columnNames);

        // Setup the grid display columns.
        mGrid.LJCAddDisplayColumns(DisplayColumns);
      }
    }
    #endregion

    #region Data Methods

    // Retrieves the list rows.
    internal void DataRetrieve()
    {
      mParent.Cursor = Cursors.WaitCursor;
      mGrid.LJCRowsClear();

      var manager = mManagers.DocAssemblyGroupManager;
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
    }
    #endregion

    #region Action Methods

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

    internal DbColumns DisplayColumns { get; set; }

    #region Class Data

    private readonly LJCDataGrid mGrid;
    private readonly ManagersDocGen mManagers;
    private readonly LJCGenDocList mParent;
    #endregion
  }
}
