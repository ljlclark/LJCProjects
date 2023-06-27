// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// MethodHeadingGridCode.cs
using LJCDBMessage;
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

    /// <summary>Initializes an object instance.</summary>
    internal MethodHeadingGridCode(MethodHeadingSelect selectList)
    {
      mSelectList = selectList;
      Managers = mSelectList.Managers;
      mMethodHeadingGrid = mSelectList.MethodHeadingGrid;
    }
    #endregion

    #region Data Methods

    /// <summary>Retrieves the list rows.</summary>
    internal void DataRetrieve()
    {
      mSelectList.Cursor = Cursors.WaitCursor;
      mMethodHeadingGrid.LJCRowsClear();

      var manager = Managers.DocMethodGroupHeadingManager;
      var names = new List<string>()
      {
        DocMethodGroupHeading.ColumnSequence
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
      mSelectList.SetControlState();
      mSelectList.Cursor = Cursors.Default;
    }

    // Selects a row based on the key record values.
    /// <include path='items/RowSelect/*' file='../../../../LJCDocLib/Common/List.xml'/>
    internal bool RowSelect(DocMethodGroupHeading dataRecord)
    {
      bool retValue = false;

      if (dataRecord != null)
      {
        mSelectList.Cursor = Cursors.WaitCursor;
        foreach (LJCGridRow row in mMethodHeadingGrid.Rows)
        {
          if (MethodHeadingID(row) == dataRecord.ID)
          {
            // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
            mMethodHeadingGrid.LJCSetCurrentRow(row, true);
            retValue = true;
            break;
          }
        }
        mSelectList.Cursor = Cursors.Default;
      }
      return retValue;
    }

    // Adds a grid row and updates it with the record values.
    private LJCGridRow RowAdd(DocMethodGroupHeading dataRecord)
    {
      var retValue = mMethodHeadingGrid.LJCRowAdd();
      SetStoredValues(retValue, dataRecord);
      mMethodHeadingGrid.LJCRowSetValues(retValue, dataRecord);
      return retValue;
    }

    // Adds a grid row and updates it with the result values.
    private LJCGridRow RowAddValues(DbValues dbValues)
    {
      var retValue = mMethodHeadingGrid.LJCRowAdd();
      var columnName = DocMethodGroupHeading.ColumnID;
      retValue.LJCSetInt32(columnName, dbValues.LJCGetInt32(columnName));

      mMethodHeadingGrid.LJCRowSetValues(retValue, dbValues);
      return retValue;
    }

    // Updates the current row with the record values.
    private void RowUpdate(DocMethodGroupHeading dataRecord)
    {
      if (mMethodHeadingGrid.CurrentRow is LJCGridRow row)
      {
        SetStoredValues(row, dataRecord);
        mMethodHeadingGrid.LJCRowSetValues(row, dataRecord);
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

    /// <summary>Performs the default list action.</summary>
    internal void DoDefault()
    {
      if (mSelectList.LJCIsSelect)
      {
        DoSelect();
      }
      else
      {
        DoEdit();
      }
    }

    /// <summary>Displays a detail dialog for a new record.</summary>
    internal void DoNew()
    {
      var detail = new MethodHeadingDetail()
      {
        Managers = Managers
      };
      detail.LJCChange += Detail_Change;
      detail.ShowDialog();
    }

    /// <summary>Displays a detail dialog to edit an existing record.</summary>
    internal void DoEdit()
    {
      if (mMethodHeadingGrid.CurrentRow is LJCGridRow _)
      {
        var detail = new MethodHeadingDetail()
        {
          LJCID = MethodHeadingID(),
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

      var methodHeadingRow = mMethodHeadingGrid.CurrentRow as LJCGridRow;
      if (methodHeadingRow != null)
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
          { DocMethodGroupHeading.ColumnID, MethodHeadingID() }
        };
        var manager = Managers.DocMethodGroupHeadingManager;
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
        mMethodHeadingGrid.Rows.Remove(methodHeadingRow);
      }
    }

    /// <summary>Refreshes the list.</summary>
    internal void DoRefresh()
    {
      mSelectList.Cursor = Cursors.WaitCursor;

      // Save the original row.
      var methodHeadingID = MethodHeadingID();

      DataRetrieve();
      if (methodHeadingID > 0)
      {
        var dataRecord = new DocMethodGroupHeading()
        {
          ID = methodHeadingID
        };
        RowSelect(dataRecord);
      }
      mSelectList.Cursor = Cursors.Default;
    }

    /// <summary>Sets the selected item and returns to the parent form.</summary>
    internal void DoSelect()
    {
      mSelectList.LJCSelectedRecord = null;
      if (mMethodHeadingGrid.CurrentRow is LJCGridRow _)
      {
        mSelectList.Cursor = Cursors.WaitCursor;
        var manager = Managers.DocMethodGroupHeadingManager;
        var keyRecord = manager.GetIDKey(MethodHeadingID());
        var dataRecord = manager.Retrieve(keyRecord);
        if (dataRecord != null)
        {
          mSelectList.LJCSelectedRecord = dataRecord;
        }
        mSelectList.Cursor = Cursors.Default;
      }
      mSelectList.DialogResult = DialogResult.OK;
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
          DoRefresh();
        }
        else
        {
          // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
          var row = RowAdd(dataRecord);
          mMethodHeadingGrid.LJCSetCurrentRow(row, true);
          DoRefresh();
        }
      }
    }
    #endregion

    #region Other Methods

    // Retrieves the current row item ID.
    /// <include path='items/MethodHeadingID/*' file='../../Doc/MethodHeadingGridCode.xml'/>
    internal short MethodHeadingID(LJCGridRow methodheadingRow = null)
    {
      short retValue = 0;

      if (null == methodheadingRow)
      {
        methodheadingRow = mMethodHeadingGrid.CurrentRow as LJCGridRow;
      }
      if (methodheadingRow != null)
      {
        retValue = (short)methodheadingRow.LJCGetInt32(DocMethodGroupHeading.ColumnID);
      }
      return retValue;
    }

    /// <summary>Setup the grid display columns.</summary>
    internal void SetupGrid()
    {
      // Setup default display columns if no columns are defined.
      if (0 == mMethodHeadingGrid.Columns.Count)
      {
        List<string> columnNames = new List<string>()
        {
          DocMethodGroupHeading.ColumnName,
          DocMethodGroupHeading.ColumnHeading
        };

        // Get the display columns from the manager Data Definition.
        var methodManager = Managers.DocMethodGroupHeadingManager;
        DisplayColumns = methodManager.GetColumns(columnNames);

        // Setup the grid display columns.
        mMethodHeadingGrid.LJCAddDisplayColumns(DisplayColumns);
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

    private readonly LJCDataGrid mMethodHeadingGrid;
    private readonly MethodHeadingSelect mSelectList;
    #endregion
  }
}
