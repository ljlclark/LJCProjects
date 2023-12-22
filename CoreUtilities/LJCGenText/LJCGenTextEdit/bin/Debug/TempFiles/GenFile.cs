// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DocAssemblyGridCode.cs
using LJCDBClientLib;
using LJCDBMessage;
using LJCNetCommon;
using LJCViewEditorDAL;
using LJCWinFormCommon;
using LJCWinFormControls;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using static LJCGenDocEdit.GenDocEditList;

namespace LJCGenDocEdit
{
  // Provides DocAssemblyGrid methods for the GenDocEditList window.
  internal class DocAssemblyGridCode
  {
    #region Constructors

    // Initializes an object instance.
    internal DocAssemblyGridCode(GenDocEditList parentList)
    {
      // Initialize property values.
      GenDocEditList = parentList;
      GenDocEditList.Cursor = Cursors.WaitCursor;
      DocAssemblyGrid = GenDocEditList.DocAssemblyGrid;
      DocAssemblyGroupGrid = GenDocEditList.DocAssemblyGroupGrid;
      ResetData();
      GenDocEditList.Cursor = Cursors.Default;
    }

    // Resets the DataConfig dependent objects.
    internal void ResetData()
    {
      Managers = GenDocEditList.Managers;
      ClassName_Manager = Managers.DocAssemblyManager;
    }
    #endregion

    #region Data Methods

    // Retrieves the combo items.
    internal void DataRetrieveCombo()
    {
      //ComboRecords dataRecords;

      //Cursor = Cursors.WaitCursor;
      //Combo.Items.Clear();

      //dataRecords = mComboManager.Load();

      //if (dataRecords != null && records.Count > 0)
      //{
      //	foreach (ComboRecord dataRecord in dataRecords)
      //	{
      //		Combo.Items.Add(dataRecord);
      //	}
      //	if (Combo.Items.Count > 0)
      //	{
      //		Combo.SelectedIndex = 0;
      //	}
      //}
      //Cursor = Cursors.Default;
    }

    // Retrieves the list rows.
    internal void DataRetrieve()
    {
      GenDocEditList.Cursor = Cursors.WaitCursor;
      DocAssemblyGrid.LJCRowsClear();

      //SetupGrid();
      if (DocAssemblyGroupGrid.CurrentRow is LJCGridRow parentRow)
      {
        // Data from items.
        int parentID = parentRow.LJCGetInt32(DocAssemblyGroup.ColumnID);

        var result = ClassName_Manager.ResultWithParentID(parentID);
        if (DbResult.HasRows(result))
        {
          foreach (var dbRow in result.Rows)
          {
            RowAddValues(dbRow.Values);
          }
        }
      }
      GenDocEditList.Cursor = Cursors.Default;
      GenDocEditList.DoChange(Change.DocAssembly);
    }

    // Adds a grid row and updates it with the record values.
    private LJCGridRow RowAdd(DocAssembly dataRecord)
    {
      var retValue = DocAssemblyGrid.LJCRowAdd();
      SetStoredValues(retValue, dataRecord);
      retValue.LJCSetValues(DocAssemblyGrid, dataRecord);
      return retValue;
    }

    // Adds a grid row and updates it with the result values.
    private LJCGridRow RowAddValues(DbValues dbValues)
    {
      var ljcGrid = DocAssemblyGrid;
      var retValue = ljcGrid.LJCRowAdd();

      var columnName = DocAssembly.ColumnID;
      var id = dbValues.LJCGetInt32(columnName);
      retValue.LJCSetInt32(columnName, id);

      retValue.LJCSetValues(ljcGrid, dbValues);
      return retValue;
    }

    // Selects a row based on the key record values.
    private bool RowSelect(DocAssembly dataRecord)
    {
      bool retValue = false;

      if (dataRecord != null)
      {
        GenDocEditList.Cursor = Cursors.WaitCursor;
        foreach (LJCGridRow row in DocAssemblyGrid.Rows)
        {
          var rowID = row.LJCGetInt32(DocAssembly.ColumnID);
          if (rowID == dataRecord.ID)
          {
            // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
            DocAssemblyGrid.LJCSetCurrentRow(row, true);
            retValue = true;
            break;
          }
        }
        GenDocEditList.Cursor = Cursors.Default;
      }
      return retValue;
    }

    // Updates the current row with the record values.
    private void RowUpdate(DocAssembly dataRecord)
    {
      if (DocAssemblyGrid.CurrentRow is LJCGridRow row)
      {
        SetStoredValues(row, dataRecord);
        row.LJCSetValues(DocAssemblyGrid, dataRecord);
      }
    }

    // Sets the row stored values.
    private void SetStoredValues(LJCGridRow row, DocAssembly dataRecord)
    {
      row.LJCSetInt32(DocAssembly.ColumnID, dataRecord.ID);
    }
    #endregion

    #region Action Methods

    // Performs the default list action.
    internal void DoDefaultDocAssembly()
    {
      if (LJCIsSelect)
      {
        DoSelect();
      }
      else
      {
        DoEdit();
      }
    }

    // Deletes the selected row.
    internal void DoDelete()
    {
      bool success = false;
      var row = DocAssemblyGrid.CurrentRow as LJCGridRow;
      if (DocAssemblyGroupGrid.CurrentRow is LJCGridRow parentRow
        && row != null)
      {
        var title = "Delete Confirmation";
        var message = FormCommon.DeleteConfirm;
        if (MessageBox.Show(message, title, MessageBoxButtons.YesNo
          , MessageBoxIcon.Question) == DialogResult.Yes)
        {
          success = true;
        }
      }

      //int id = 0;
      if (success)
      {
        // Data from items.
        var id = row.LJCGetInt32(DocAssembly.ColumnID);

        var keyColumns = new DbColumns()
        {
          { DocAssembly.ColumnID, id }
        };
        DocAssemblyManager.Delete(keyColumns);
        if (0 == ClassName_Manager.AffectedCount)
        {
          success = false;
          var message = FormCommon.DeleteError;
          MessageBox.Show(message, "Delete Error", MessageBoxButtons.OK
            , MessageBoxIcon.Exclamation);
        }
      }

      if (success)
      {
        DocAssemblyGrid.Rows.Remove(row);
        GenDocEditList.TimedChange(Change.DocAssembly);
      }
    }

    // Displays a detail dialog to edit an existing record.
    internal void DoEdit()
    {
      if (DocAssemblyGroupGrid.CurrentRow is LJCGridRow parentRow
        && DocAssemblyGrid.CurrentRow is LJCGridRow row)
      {
        // Data from items.
        int id = row.LJCGetInt32(DocAssembly.ColumnID);
        int parentID = parentRow.LJCGetInt32(DocAssemblyGroup.ColumnID);
        string parentName = parentRow.LJCGetString(DocAssemblyGroup.ColumnName);

        var location = FormCommon.GetDialogScreenPoint(DocAssemblyGrid);
        var detail = new DocAssemblyDetail()
        {
          LJCID = id,
          LJCLocation = location,
          LJCManagers = Managers,
          LJCParentID = parentID,
          LJCParentName = parentName,
        };
        detail.LJCChange += Detail_Change;
        detail.ShowDialog();
      }
    }

    // Shows the help page
    internal void DoHelp()
    {
      Help.ShowHelp(DocList, "GenDocEdit.chm", HelpNavigator.Topic
        , "DocAssemblyList.html");
    }

    // Displays a detail dialog for a new record.
    internal void DoNew()
    {
      if (DocAssemblyGroupGrid.CurrentRow is LJCGridRow parentRow)
      {
        // Data from list items.
        int parentID = parentRow.LJCGetInt32(DocAssemblyGroup.ColumnID);
        string parentName = parentRow.LJCGetString(DocAssemblyGroup.ColumnName);

        var location = FormCommon.GetDialogScreenPoint(DocAssemblyGrid);
        var detail = new DocAssemblyDetail
        {
          LJCLocation = location,
          LJCManagers = Managers,
          LJCParentID = parentID,
          LJCParentName = parentName
        };
        detail.LJCChange += Detail_Change;
        detail.ShowDialog();
      }
    }

    // Refreshes the list.
    internal void DoRefresh()
    {
      GenDocEditList.Cursor = Cursors.WaitCursor;
      int id = 0;
      if (DocAssemblyGrid.CurrentRow is LJCGridRow _)
      {
        // Save the original row.
        id = row.LJCGetInt32(DocAssembly.ColumnID);
      }
      DataRetrieve();

      // Select the original row.
      if (id > 0)
      {
        var record = new DocAssembly()
        {
          ID = id
        };
        RowSelect(record);
      }
      GenDocEditList.Cursor = Cursors.Default;
    }

    // Sets the selected item and returns to the parent form.
    private void DoSelect()
    {
      LJCSelectedRecord = null;
      if (DocAssemblyGrid.CurrentRow is LJCGridRow row)
      {
        Cursor = Cursors.WaitCursor;
        var id = row.LJCGetInt32(DocAssembly.ColumnID);

        var manager = mManagers.DocAssemblyManager;
        var keyRecord = manager.GetIDKey(id);
        dataRecord = manager.Retrieve(keyRecord);
        if (dataRecord != null)
        {
          LJCSelectedRecord = dataRecord;
        }
        Cursor = Cursors.Default;
      }
      DialogResult = DialogResult.OK;
    }

    // Adds new row or updates row with
    private void Detail_Change(object sender, EventArgs e)
    {
      var detail = sender as DocAssemblyDetail;
      var record = detail.LJCRecord;
      if (record != null)
      {
        if (detail.LJCIsUpdate)
        {
          RowUpdate(record);
          //CheckPreviousAndNext(detail);
          //DoRefresh();
        }
        else
        {
          // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
          var row = RowAdd(record);
          DocAssemblyGrid.LJCSetCurrentRow(row, true);
          //CheckPreviousAndNext(detail);
          //DoRefresh();
          GenDocEditList.TimedChange(Change.DocAssembly);
        }
      }
    }
    #endregion

    #region Setup and Other Methods

    // Configures the DocAssembly Grid.
    private void SetupGrid()
    {
      // Setup default grid columns if no columns are defined.
      if (0 == DocAssemblyGrid.Columns.Count)
      {
        List<string> propertyNames = new List<string>()
        {
          DocAssembly.ColumnName,
          DocAssembly.ColumnDescription
        };

        // Get the grid columns from the manager Data Definition.
        var manager = DocAssemblyManager;
        var gridColumns = manager.GetColumns(propertyNames);

        // Setup the grid columns.
        DocAssemblyGrid.LJCAddColumns(gridColumns);
        //mDocAssemblyGrid.LJCDragDataName = "DocAssembly";
      }
    }
    #endregion

    #region Get Data Methods

    // Retrieves the row item.
    private DocAssembly GetDocAssembly(LJCGridRow row = null)
    {
      DocAssembly retValue = null;

      if (DocAssemblyGrid.CurrentRow is LJCGridRow row)
      {
        var id = DocAssemblyID(row);
        if (id > 0)
        {
          var manager = Managers.DocAssemblyManager;
          retValue = manager.RetrieveWithID(id);
        }
      }
      return retValue;
    }

    // Retrieves the current row item ID.
    private long DocAssemblyID(LJCGridRow row = null)
    {
      long retValue = 0;

      if (null == row)
      {
        row = DocAssemblyGrid.CurrentRow as LJCGridRow;
      }
      if (row != null)
      {
        retValue = row.LJCGetInt64(DocAssembly.ColumnID);
      }
      return retValue;
    }
    #endregion

    #region Private Methods

    // Checks for Previous and Next items.
    private void CheckPreviousAndNext(DocAssemblyDetail detail)
    {
      PreviousItem(detail);
      NextItem(detail);
    }

    // Checks for Next item.
    private void NextItem(DocAssemblyDetail detail)
    {
      if (detail.LJCNext)
      {
        LJCDataGrid grid = mDocAssemblyGrid;
        int currentIndex = grid.CurrentRow.Index;
        detail.LJCNext = false;
        if (currentIndex < grid.Rows.Count - 1)
        {
          grid.LJCSetCurrentRow(currentIndex + 1, true);
          var id = DocAssemblyID();
          if (id > 0)
          {
            detail.ID = id;
            detail.LJCNext = true;
          }
        }
      }
    }

    // Checks for Previous item.
    private void PreviousItem(DocAssemblyDetail detail)
    {
      if (detail.LJCPrevious)
      {
        LJCDataGrid grid = mDocAssemblyGrid;
        int currentIndex = grid.CurrentRow.Index;
        detail.LJCPrevious = false;
        if (currentIndex > 0)
        {
          grid.LJCSetCurrentRow(currentIndex - 1, true);
          var id = DocAssemblyID();
          if (id > 0)
          {
            detail.ID = id;
            detail.LJCPrevious = true;
          }
        }
      }
    }
    #endregion

    #region Properties

    // Gets or sets the Parent List reference.
    private GenDocEditList GenDocEditList { get; set; }

    // Gets or sets the DocAssembly Grid reference.
    private LJCDataGrid DocAssemblyGrid { get; set; }

    // Gets or sets the Manager reference.
    private DocAssemblyManager ClassName_Manager { get; set; }

    // Gets or sets the Managers reference.
    private ManagersGenDocEdit Managers { get; set; }

    // Gets or sets the DocAssemblyGroup Grid reference.
    private LJCDataGrid DocAssemblyGroupGrid { get; set; }
    #endregion
  }
}
