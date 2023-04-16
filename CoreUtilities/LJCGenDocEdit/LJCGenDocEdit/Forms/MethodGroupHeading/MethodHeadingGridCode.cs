// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// MethodHeadingGridCode.cs
using LJCDocLibDAL;
using LJCNetCommon;
using LJCWinFormCommon;
using LJCWinFormControls;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace LJCGenDocEdit
{
  // The MethodHeading grid code.
  internal class MethodHeadingGridCode
  {
    #region Constructors

    // Initializes an object instance.
    internal MethodHeadingGridCode(MethodHeadingSelect parent)
    {
      mParent = parent;
      mGrid = mParent.MethodHeadingGrid;
      mManagers = mParent.Managers;
    }
    #endregion

    #region Data Methods

    // Retrieves the list rows.
    internal void DataRetrieve()
    {
      mGrid.LJCRowsClear();

      mParent.Cursor = Cursors.WaitCursor;

      var manager = mManagers.DocMethodGroupHeadingManager;
      var dataRecords = manager.Load();

      if (NetCommon.HasItems(dataRecords))
      {
        foreach (DocMethodGroupHeading dataRecord in dataRecords)
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
    internal bool RowSelect(DocMethodGroupHeading dataRecord)
    {
      bool retValue = false;

      if (dataRecord != null)
      {
        mParent.Cursor = Cursors.WaitCursor;
        foreach (LJCGridRow row in mGrid.Rows)
        {
          var rowID = row.LJCGetInt32(DocMethodGroupHeading.ColumnID);
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
    private LJCGridRow RowAdd(DocMethodGroupHeading dataRecord)
    {
      var retValue = mGrid.LJCRowAdd();
      SetStoredValues(retValue, dataRecord);

      // Sets the row values from a data object.
      mGrid.LJCRowSetValues(retValue, dataRecord);
      return retValue;
    }

    // Updates the current row with the record values.
    private void RowUpdate(DocMethodGroupHeading dataRecord)
    {
      if (mGrid.CurrentRow is LJCGridRow row)
      {
        SetStoredValues(row, dataRecord);
        mGrid.LJCRowSetValues(row, dataRecord);
      }
    }

    // Sets the row stored values.
    private void SetStoredValues(LJCGridRow row
      , DocMethodGroupHeading dataRecord)
    {
      row.LJCSetInt32(DocMethodGroupHeading.ColumnID, dataRecord.ID);
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
      if (mGrid.CurrentRow is LJCGridRow row)
      {
        // Data from items.
        var id = (short)row.LJCGetInt32(DocMethodGroupHeading.ColumnID);

        var detail = new MethodHeadingDetail()
        {
          LJCID = id,
          Managers = mManagers
        };
        detail.LJCChange += Detail_Change;
        detail.ShowDialog();
      }
    }

    // Displays a detail dialog to edit an existing record.
    internal void DoEdit()
    {
      if (mGrid.CurrentRow is LJCGridRow row)
      {
        // Data from items.
        var id = (short)row.LJCGetInt32(DocMethodGroupHeading.ColumnID);

        var detail = new ClassHeadingDetail()
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
        var id = row.LJCGetInt32(DocMethodGroupHeading.ColumnID);

        var keyRecord = new DbColumns()
        {
          { DocMethodGroupHeading.ColumnID, id }
        };
        var manager = mManagers.DocMethodGroupHeadingManager;
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
      }
    }

    // Refreshes the list.
    internal void DoRefresh()
    {
      short id = 0;

      mParent.Cursor = Cursors.WaitCursor;
      if (mGrid.CurrentRow is LJCGridRow row)
      {
        id = (short)row.LJCGetInt32(DocMethodGroupHeading.ColumnID);
      }
      DataRetrieve();

      // Select the original row.
      if (id > 0)
      {
        var dataRecord = new DocMethodGroupHeading()
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
        var id = row.LJCGetInt32(DocMethodGroupHeading.ColumnID);

        var manager = mManagers.DocMethodGroupHeadingManager;
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

    // Adds new row or updates row with changes from the detail dialog.
    private void Detail_Change(object sender, EventArgs e)
    {
      var detail = sender as MethodHeadingDetail;
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
        }
      }
    }
    #endregion

    #region Other Methods

    /// <summary>
    /// Retrieves the currently selecteditem.
    /// </summary>
    /// <returns>The currently selected item.</returns>
    internal DocMethodGroupHeading CurrentItem()
    {
      DocMethodGroupHeading retValue = null;

      if (mGrid.CurrentRow is LJCGridRow row)
      {
        var id = (short)row.LJCGetInt32(DocMethodGroupHeading.ColumnID);
        if (id > 0)
        {
          var manager = mManagers.DocMethodGroupHeadingManager;
          retValue = manager.RetrieveWithID(id);
        }
      }
      return retValue;
    }

    // Selects the current item.
    private void Select()
    {
      var methodHeading = CurrentItem();
      if (methodHeading != null)
      {
        RowSelect(methodHeading);
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
          DocMethodGroupHeading.ColumnName,
          DocMethodGroupHeading.ColumnHeading
        };

        // Get the display columns from the manager Data Definition.
        var methodManager = mManagers.DocMethodGroupHeadingManager;
        DisplayColumns = methodManager.GetColumns(columnNames);

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
    private readonly MethodHeadingSelect mParent;
    #endregion
  }
}
