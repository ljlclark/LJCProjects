﻿// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// CodeGroupGridCode.cs
using LJCNetCommon;
using LJCWinFormCommon;
using LJCWinFormControls;
using ProjectFilesDAL;
using System;
using System.Windows.Forms;
using static UpdateProjectFiles.CodeManagerList;

namespace UpdateProjectFiles
{
  internal class CodeGroupGridCode
  {
    #region Constructors

    // <summary>Initializes an object instance.</summary>
    internal CodeGroupGridCode(CodeManagerList parentList)
    {
      // Initialize property values.
      parentList.Cursor = Cursors.WaitCursor;
      ArgError = new ArgError("UpdateProjectFiles.CodeGroupGridCode");
      CodeList = parentList;
      CodeLineGrid = CodeList.CodeLineGrid;
      CodeGroupGrid = CodeList.CodeGroupGrid;
      ResetData();
      CodeList.Cursor = Cursors.Default;
    }

    // Resets the DataConfig dependent objects.
    internal void ResetData()
    {
      Data = CodeList.Data;
      CodeGroups = Data.CodeGroups;
      Managers = CodeList.Managers;
      CodeGroupManager = Managers.CodeGroupManager;
    }
    #endregion

    #region Data Methods

    // Retrieves the list rows.
    internal void DataRetrieve(string parentKey = null)
    {
      CodeList.Cursor = Cursors.WaitCursor;
      CodeGroupGrid.LJCRowsClear();

      //var codeGroups = CodeGroupManager.Load(parentKey);
      var codeGroups = CodeGroups.LJCLoad(parentKey);
      if (NetCommon.HasItems(codeGroups))
      {
        foreach (var codeGroup in codeGroups)
        {
          RowAdd(codeGroup);
        }
      }
      CodeList.Cursor = Cursors.Default;
      CodeList.DoChange(Change.CodeGroup);
    }

    // Adds a grid row and updates it with the record values.
    private LJCGridRow RowAdd(CodeGroup dataRecord)
    {
      ArgError.MethodName = "RowAdd(dataRecord)";
      ArgError.Add(dataRecord, "dataRecord");
      NetString.ThrowArgError(ArgError.ToString());

      var retValue = CodeGroupGrid.LJCRowAdd();
      SetStoredValues(retValue, dataRecord);
      retValue.LJCSetValues(CodeGroupGrid, dataRecord);
      return retValue;
    }

    // Selects a row based on the key record values.
    private bool RowSelect(CodeGroup dataRecord)
    {
      bool retValue = false;

      ArgError.MethodName = "RowSelect(dataRecord)";
      ArgError.Add(dataRecord, "dataRecord");
      NetString.ThrowArgError(ArgError.ToString());

      CodeList.Cursor = Cursors.WaitCursor;
      foreach (LJCGridRow row in CodeGroupGrid.Rows)
      {
        var codeLine = row.LJCGetString("CodeLine");
        var name = row.LJCGetString("Name");
        if (codeLine == dataRecord.CodeLine
          && name == dataRecord.Name)
        {
          // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
          CodeGroupGrid.LJCSetCurrentRow(row, true);
          retValue = true;
          break;
        }
        CodeList.Cursor = Cursors.Default;
      }
      return retValue;
    }

    // Updates the current row with the record values.
    private void RowUpdate(CodeGroup dataRecord)
    {
      if (CodeGroupGrid.CurrentRow is LJCGridRow row)
      {
        SetStoredValues(row, dataRecord);
        row.LJCSetValues(CodeGroupGrid, dataRecord);
      }
    }

    // Sets the row stored values.
    private void SetStoredValues(LJCGridRow row, CodeGroup dataRecord)
    {
      ArgError.MethodName = "SetStoredValues(row, dataRecod)";
      ArgError.Add(dataRecord, "dataRecord");
      NetString.ThrowArgError(ArgError.ToString());

      row.LJCSetString("CodeLine", dataRecord.CodeLine);
      row.LJCSetString("Name", dataRecord.Name);
    }
    #endregion

    #region Action Methods

    // Deletes the selected row.
    internal void DoDelete()
    {
      bool success = false;
      var parentRow = CodeLineGrid.CurrentRow as LJCGridRow;
      var row = CodeGroupGrid.CurrentRow as LJCGridRow;
      if (parentRow != null
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

      if (success)
      {
        // Data from items.
        var codeLineName = parentRow.LJCGetString("Name");
        var name = row.LJCGetString("Name");

        //CodeGroupManager.Delete(codeLineName, name);
        CodeGroups.LJCDelete(codeLineName, name);
        CodeGroupManager.RecreateFile(CodeGroups);
      }

      if (success)
      {
        CodeGroupGrid.Rows.Remove(row);
        CodeList.TimedChange(Change.CodeGroup);
      }
    }

    // Displays a detail dialog to edit a record.
    internal void DoEdit()
    {
      if (CodeLineGrid.CurrentRow is LJCGridRow parentRow
        && CodeGroupGrid.CurrentRow is LJCGridRow row)
      {
        // Data from items.
        var codeLineName = parentRow.LJCGetString("Name");
        var name = row.LJCGetString("Name");

        var location = FormCommon.GetDialogScreenPoint(CodeGroupGrid);
        var detail = new CodeGroupDetail()
        {
          LJCCodeLine = codeLineName,
          LJCName = name
        };
        detail.LJCChange += Detail_Change;
        detail.ShowDialog();
      }
    }

    // Displays a detail dialog for a new record.
    internal void DoNew()
    {
      if (CodeLineGrid.CurrentRow is LJCGridRow parentRow)
      {
        // Data from items.
        var codeLineName = parentRow.LJCGetString("Name");

        var location = FormCommon.GetDialogScreenPoint(CodeGroupGrid);
        var detail = new CodeGroupDetail()
        {
          LJCCodeLine = codeLineName
        };
        detail.LJCChange += Detail_Change;
        detail.ShowDialog();
      }
    }

    // Refreshes the list.
    internal void DoRefresh()
    {
      CodeList.Cursor = Cursors.WaitCursor;
      CodeGroup record = null;
      if (CodeLineGrid.CurrentRow is LJCGridRow _
        && CodeGroupGrid.CurrentRow is LJCGridRow row)
      {
        // Save the original row.
        var parentKey = CodeList.GetCodeGroupParentKey();
        record = new CodeGroup()
        {
          CodeLine = parentKey,
          Name = row.LJCGetString("Name"),
        };
        DataRetrieve(parentKey);
      }

      // Select the original row.
      if (record != null)
      {
        RowSelect(record);
      }
      CodeList.Cursor = Cursors.Default;
    }

    // Adds new row or updates row with record values.
    private void Detail_Change(object sender, EventArgs e)
    {
      var detail = sender as CodeGroupDetail;
      var record = detail.LJCRecord;
      if (record != null)
      {
        if (detail.LJCIsUpdate)
        {
          RowUpdate(record);
        }
        else
        {
          // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
          var row = RowAdd(record);
          CodeGroupGrid.LJCSetCurrentRow(row, true);
          CodeList.TimedChange(Change.CodeGroup);
        }
      }
    }
    #endregion

    #region Setup and Other Methods

    // Configures the CodeLine Grid.
    internal void SetupGrid()
    {
      // Setup default grid columns if no columns are defined.
      if (0 == CodeGroupGrid.Columns.Count)
      {
        // Get the grid columns from the manager Data Definition.
        var manager = CodeGroupManager;
        var gridColumns = manager.GetColumns();

        // Setup the grid columns.
        CodeGroupGrid.LJCAddColumns(gridColumns);
      }
    }
    #endregion

    #region Properties

    // Gets or sets the ArgError object.
    private ArgError ArgError { get; set; }

    // Gets or sets the CodeGroup Grid reference.
    private LJCDataGrid CodeGroupGrid { get; set; }

    // Gets or sets the CodeGroups collection.
    private CodeGroups CodeGroups { get; set; }

    // Gets or sets the CodeLine Grid reference.
    private LJCDataGrid CodeLineGrid { get; set; }

    // Gets or sets the Manager reference.
    private CodeGroupManager CodeGroupManager { get; set; }

    // Gets or sets the Parent List reference.
    private CodeManagerList CodeList { get; set; }

    // Gets or sets the Data object.
    private ProjectFilesData Data { get; set; }

    // Gets or sets the Managers reference.
    private ManagersProjectFiles Managers { get; set; }
    #endregion
  }
}
