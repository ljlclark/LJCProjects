// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// ClassHeadingGridCode.cs
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
  // The ClassHeading grid code.
  internal class ClassHeadingGridCode
  {
    #region Constructors

    /// <summary>Initializes an object instance.</summary>
    internal ClassHeadingGridCode(ClassHeadingSelect selectList)
    {
      mSelectList = selectList;
      mClassHeadingGrid = mSelectList.ClassHeadingGrid;
      Managers = mSelectList.Managers;
    }
    #endregion

    #region Data Methods

    /// <summary>Retrieves the list rows.</summary>
    internal void DataRetrieve()
    {
      mSelectList.Cursor = Cursors.WaitCursor;
      mClassHeadingGrid.LJCRowsClear();

      var manager = Managers.DocClassGroupHeadingManager;
      var names = new List<string>()
      {
        DocClassGroupHeading.ColumnSequence
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
    internal bool RowSelect(DocClassGroupHeading dataRecord)
    {
      bool retValue = false;

      if (dataRecord != null)
      {
        mSelectList.Cursor = Cursors.WaitCursor;
        foreach (LJCGridRow row in mClassHeadingGrid.Rows)
        {
          if (ClassHeadingID(row) == dataRecord.ID)
          {
            // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
            mClassHeadingGrid.LJCSetCurrentRow(row, true);
            retValue = true;
            break;
          }
        }
        mSelectList.Cursor = Cursors.Default;
      }
      return retValue;
    }

    // Adds a grid row and updates it with the record values.
    private LJCGridRow RowAdd(DocClassGroupHeading dataRecord)
    {
      var retValue = mClassHeadingGrid.LJCRowAdd();
      SetStoredValues(retValue, dataRecord);
      mClassHeadingGrid.LJCRowSetValues(retValue, dataRecord);
      return retValue;
    }

    // Adds a grid row and updates it with the result values.
    private LJCGridRow RowAddValues(DbValues dbValues)
    {
      var retValue = mClassHeadingGrid.LJCRowAdd();
      var columnName = DocClassGroupHeading.ColumnID;
      retValue.LJCSetInt32(columnName, dbValues.LJCGetInt32(columnName));

      mClassHeadingGrid.LJCRowSetValues(retValue, dbValues);
      return retValue;
    }

    // Updates the current row with the record values.
    private void RowUpdate(DocClassGroupHeading dataRecord)
    {
      if (mClassHeadingGrid.CurrentRow is LJCGridRow row)
      {
        SetStoredValues(row, dataRecord);
        mClassHeadingGrid.LJCRowSetValues(row, dataRecord);
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
      var detail = new ClassHeadingDetail()
      {
        Managers = Managers
      };
      detail.LJCChange += Detail_Change;
      detail.ShowDialog();
    }

    /// <summary>Displays a detail dialog to edit an existing record.</summary>
    internal void DoEdit()
    {
      if (mClassHeadingGrid.CurrentRow is LJCGridRow _)
      {
        var detail = new ClassHeadingDetail()
        {
          LJCID = ClassHeadingID(),
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

      var classHeadingRow = mClassHeadingGrid.CurrentRow as LJCGridRow;
      if (classHeadingRow != null)
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
          { DocClassGroupHeading.ColumnID, ClassHeadingID() }
        };
        var manager = Managers.DocClassGroupHeadingManager;
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
        mClassHeadingGrid.Rows.Remove(classHeadingRow);
      }
    }

    /// <summary>Refreshes the list.</summary>
    internal void DoRefresh()
    {
      mSelectList.Cursor = Cursors.WaitCursor;
      DataRetrieve();

      // Select the original row.
      var classHeadingID = ClassHeadingID();
      if (classHeadingID > 0)
      {
        var dataRecord = new DocClassGroupHeading()
        {
          ID = classHeadingID
        };
        RowSelect(dataRecord);
      }
      mSelectList.Cursor = Cursors.Default;
    }

    /// <summary>Sets the selected item and returns to the parent form.</summary>
    internal void DoSelect()
    {
      mSelectList.LJCSelectedRecord = null;
      if (mClassHeadingGrid.CurrentRow is LJCGridRow _)
      {
        mSelectList.Cursor = Cursors.WaitCursor;
        var manager = Managers.DocClassGroupHeadingManager;
        var keyRecord = manager.GetIDKey(ClassHeadingID());
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
      var detail = sender as ClassHeadingDetail;
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
          mClassHeadingGrid.LJCSetCurrentRow(row, true);
          DoRefresh();
        }
      }
    }
    #endregion

    #region Other Methods

    // Retrieves the current row item ID.
    /// <summary>
    /// Retrieves the current row item ID.
    /// </summary>
    /// <param name="classHeadingRow">The ClassGroupHeading grid row.</param>
    /// <returns>The ClassGroupHeading ID.</returns>
    internal short ClassHeadingID(LJCGridRow classHeadingRow = null)
    {
      short retValue = 0;

      if (null == classHeadingRow)
      {
        classHeadingRow = mClassHeadingGrid.CurrentRow as LJCGridRow;
      }
      if (classHeadingRow != null)
      {
        retValue = (short)classHeadingRow.LJCGetInt32(DocClassGroupHeading.ColumnID);
      }
      return retValue;
    }

    /// <summary>Setup the grid display columns.</summary>
    internal void SetupGrid()
    {
      // Setup default display columns if no columns are defined.
      if (0 == mClassHeadingGrid.Columns.Count)
      {
        List<string> columnNames = new List<string>()
        {
          DocClassGroupHeading.ColumnName,
          DocClassGroupHeading.ColumnHeading
        };

        // Get the display columns from the manager Data Definition.
        var classManager = Managers.DocClassGroupHeadingManager;
        DisplayColumns = classManager.GetColumns(columnNames);

        // Setup the grid display columns.
        mClassHeadingGrid.LJCAddDisplayColumns(DisplayColumns);
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

    private readonly LJCDataGrid mClassHeadingGrid;
    private readonly ClassHeadingSelect mSelectList;
    #endregion
  }
}
