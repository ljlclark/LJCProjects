// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// ClassHeadingGridCode.cs
using LJCDocLibDAL;
using LJCNetCommon;
using LJCWinFormControls;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using static LJCGenDocEdit.LJCGenDocList;

namespace LJCGenDocEdit
{
  // The ClassHeading grid code.
  internal class ClassHeadingGridCode
  {
    #region Constructors

    // Initializes an object instance.
    internal ClassHeadingGridCode(ClassHeadingSelect parent)
    {
      mParent = parent;
      mGrid = mParent.ClassHeadingGrid;
      mManagers = mParent.Managers;
    }
    #endregion

    #region Data Methods

    // Retrieves the list rows.
    internal void DataRetrieve()
    {
      mGrid.LJCRowsClear();

      mParent.Cursor = Cursors.WaitCursor;

      var manager = mManagers.DocClassGroupHeadingManager;
      var dataRecords = manager.Load();

      if (NetCommon.HasItems(dataRecords))
      {
        foreach (DocClassGroupHeading dataRecord in dataRecords)
        {
          RowAdd(dataRecord);
        }
      }
      mParent.SetControlState();
      mParent.Cursor = Cursors.Default;
    }

    // Selects a row based on the key record values.
    /// <summary>
    /// Selects a row based on the key record values.
    /// </summary>
    /// <param name="dataRecord">The data record to select.</param>
    /// <returns>True if the row was selected, otherwise false.</returns>
    internal bool RowSelect(DocClassGroupHeading dataRecord)
    {
      bool retValue = false;

      if (dataRecord != null)
      {
        mParent.Cursor = Cursors.WaitCursor;
        foreach (LJCGridRow row in mGrid.Rows)
        {
          var rowID = row.LJCGetInt32(DocClassGroupHeading.ColumnID);
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
    private LJCGridRow RowAdd(DocClassGroupHeading dataRecord)
    {
      var retValue = mGrid.LJCRowAdd();
      SetStoredValues(retValue, dataRecord);

      // Sets the row values from a data object.
      mGrid.LJCRowSetValues(retValue, dataRecord);
      return retValue;
    }

    // Updates the current row with the record values.
    private void RowUpdate(DocClassGroupHeading dataRecord)
    {
      if (mGrid.CurrentRow is LJCGridRow row)
      {
        SetStoredValues(row, dataRecord);
        mGrid.LJCRowSetValues(row, dataRecord);
      }
    }

    // Sets the row stored values.
    private void SetStoredValues(LJCGridRow row
      , DocClassGroupHeading dataRecord)
    {
      row.LJCSetInt32(DocClassGroupHeading.ColumnID, dataRecord.ID);
    }
    #endregion

    #region Action Methods

    // Performs the default list action.
    internal void DoDefault()
    {
      if (mParent.LJCIsSelect)
      {
        DoSelect();
      }
      else
      {
        DoEdit();
      }
    }

    // Displays a detail dialog for a new record.
    internal void DoNew()
    {
    }

    // Displays a detail dialog to edit an existing record.
    internal void DoEdit()
    {
    }


    // Deletes the selected row.
    internal void DoDelete()
    {
    }

    // Refreshes the list.
    internal void DoRefresh()
    {
      short id = 0;

      mParent.Cursor = Cursors.WaitCursor;
      if (mGrid.CurrentRow is LJCGridRow row)
      {
        id = (short)row.LJCGetInt32(DocClassGroupHeading.ColumnID);
      }
      DataRetrieve();

      // Select the original row.
      if (id > 0)
      {
        var dataRecord = new DocClassGroupHeading()
        {
          ID = id
        };
        RowSelect(dataRecord);
      }
      mParent.Cursor = Cursors.Default;
    }

    // Sets the selected item and returns to the parent form.
    internal void DoSelect()
    {
      mParent.LJCSelectedRecord = null;
      if (mGrid.CurrentRow is LJCGridRow row)
      {
        mParent.Cursor = Cursors.WaitCursor;
        var id = row.LJCGetInt32(DocClassGroupHeading.ColumnID);

        var manager = mManagers.DocClassGroupHeadingManager;
        var keyRecord = manager.GetIDKey(id);
        var dataRecord = manager.Retrieve(keyRecord);
        if (dataRecord != null)
        {
          mParent.LJCSelectedRecord = dataRecord;
        }
        mParent.Cursor = Cursors.Default;
      }
      mParent.DialogResult = DialogResult.OK;
    }
    #endregion

    #region Other Methods

    /// <summary>
    /// Retrieves the currently selecteditem.
    /// </summary>
    /// <returns>The currently selected item.</returns>
    internal DocClassGroupHeading CurrentItem()
    {
      DocClassGroupHeading retValue = null;

      if (mGrid.CurrentRow is LJCGridRow row)
      {
        var id = (short)row.LJCGetInt32(DocClassGroupHeading.ColumnID);
        if (id > 0)
        {
          var manager = mManagers.DocClassGroupHeadingManager;
          retValue = manager.RetrieveWithID(id);
        }
      }
      return retValue;
    }

    // Selects the AssemblyCombo item with the current AssemblyItem.
    private void Select()
    {
      var classHeading = CurrentItem();
      if (classHeading != null)
      {
        RowSelect(classHeading);
      }
    }

    // Setup the grid display columns.
    internal void SetupGrid()
    {
      // Setup default display columns if no columns are defined.
      if (0 == mGrid.Columns.Count)
      {
        List<string> columnNames = new List<string>()
        {
          DocClassGroupHeading.ColumnName,
          DocClassGroupHeading.ColumnHeading
        };

        // Get the display columns from the manager Data Definition.
        var classManager = mManagers.DocClassGroupHeadingManager;
        DisplayColumns = classManager.GetColumns(columnNames);

        // Setup the grid display columns.
        mGrid.LJCAddDisplayColumns(DisplayColumns);
      }
    }
    #endregion

    #region Properties

    internal DbColumns DisplayColumns { get; set; }
    #endregion

    #region Class Data

    private readonly LJCDataGrid mGrid;
    private readonly ManagersDocGen mManagers;
    private readonly ClassHeadingSelect mParent;
    #endregion
  }
}
