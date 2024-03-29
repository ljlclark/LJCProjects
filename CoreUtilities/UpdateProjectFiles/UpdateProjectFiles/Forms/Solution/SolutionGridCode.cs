﻿// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// SolutionGridCode.cs
using LJCNetCommon;
using LJCWinFormCommon;
using LJCWinFormControls;
using ProjectFilesDAL;
using System;
using System.Windows.Forms;
using static UpdateProjectFiles.CodeManagerList;

namespace UpdateProjectFiles
{
  internal class SolutionGridCode
  {
    #region Constructors

    // <summary>Initializes an object instance.</summary>
    internal SolutionGridCode(CodeManagerList parentList)
    {
      // Initialize property values.
      parentList.Cursor = Cursors.WaitCursor;
      CodeList = parentList;
      CodeGroupGrid = CodeList.CodeGroupGrid;
      SolutionGrid = CodeList.SolutionGrid;
      ResetData();
      CodeList.Cursor = Cursors.Default;
    }

    // Resets the DataConfig dependent objects.
    internal void ResetData()
    {
      // *** Begin *** Add - Data
      Data = CodeList.Data;
      Solutions = Data.Solutions;
      // *** End   *** Add - Data
      Managers = CodeList.Managers;
      SolutionManager = Managers.SolutionManager;
    }
    #endregion

    #region Data Methods

    // Retrieves the list rows.
    internal void DataRetrieve(SolutionParentKey parentKey = null)
    {
      CodeList.Cursor = Cursors.WaitCursor;
      SolutionGrid.LJCRowsClear();

      // *** Begin *** Change - Datas
      //var solutions = SolutionManager.Load(parentKey);
      var solutions = Solutions.LJCLoad(parentKey);
      // *** End   *** Change - Datas
      if (NetCommon.HasItems(solutions))
      {
        foreach (var solution in solutions)
        {
          RowAdd(solution);
        }
      }
      CodeList.Cursor = Cursors.Default;
      CodeList.DoChange(Change.Solution);
    }

    // Adds a grid row and updates it with the record values.
    private LJCGridRow RowAdd(Solution dataRecord)
    {
      var retValue = SolutionGrid.LJCRowAdd();
      SetStoredValues(retValue, dataRecord);
      retValue.LJCSetValues(SolutionGrid, dataRecord);
      return retValue;
    }

    // Selects a row based on the key record values.
    private bool RowSelect(Solution dataRecord)
    {
      bool retValue = false;

      if (dataRecord != null)
      {
        CodeList.Cursor = Cursors.WaitCursor;
        foreach (LJCGridRow row in SolutionGrid.Rows)
        {
          var codeLine = row.LJCGetString("CodeLine");
          var codeGroup = row.LJCGetString("CodeGroup");
          var name = row.LJCGetString("Name");
          if (codeLine == dataRecord.CodeLine
            && codeGroup == dataRecord.CodeGroup
            && name == dataRecord.Name)
          {
            // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
            SolutionGrid.LJCSetCurrentRow(row, true);
            retValue = true;
            break;
          }
        }
        CodeList.Cursor = Cursors.Default;
      }
      return retValue;
    }

    // Updates the current row with the record values.
    private void RowUpdate(Solution dataRecord)
    {
      if (SolutionGrid.CurrentRow is LJCGridRow row)
      {
        SetStoredValues(row, dataRecord);
        row.LJCSetValues(SolutionGrid, dataRecord);
      }
    }

    // Sets the row stored values.
    private void SetStoredValues(LJCGridRow row, Solution dataRecord)
    {
      row.LJCSetString("CodeLine", dataRecord.CodeLine);
      row.LJCSetString("CodeGroup", dataRecord.CodeGroup);
      row.LJCSetString("Name", dataRecord.Name);
    }
    #endregion

    #region Action Methods

    // Deletes the selected row.
    internal void DoDelete()
    {
      bool success = false;
      var parentRow = CodeGroupGrid.CurrentRow as LJCGridRow;
      var row = SolutionGrid.CurrentRow as LJCGridRow;
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
        var name = row.LJCGetString("Name");
        var parentKey = new SolutionParentKey()
        {
          CodeLine = parentRow.LJCGetString("CodeLine"),
          CodeGroup = parentRow.LJCGetString("Name"),
        };

        // *** Begin *** Change - Data
        //SolutionManager.Delete(parentKey, name);
        Solutions.LJCDelete(parentKey, name);
        SolutionManager.RecreateFile(Solutions);
        // *** End   *** Change - Datas
      }

      if (success)
      {
        SolutionGrid.Rows.Remove(row);
        CodeList.TimedChange(Change.Solution);
      }
    }

    // Displays a detail dialog to edit a record.
    internal void DoEdit()
    {
      if (CodeGroupGrid.CurrentRow is LJCGridRow parentRow
        && SolutionGrid.CurrentRow is LJCGridRow row)
      {
        // Data from items.
        var codeLineName = parentRow.LJCGetString("CodeLine");
        var codeGroupName = parentRow.LJCGetString("Name");
        var name = row.LJCGetString("Name");

        var location = FormCommon.GetDialogScreenPoint(SolutionGrid);
        var detail = new SolutionDetail()
        {
          LJCCodeLine = codeLineName,
          LJCCodeGroup = codeGroupName,
          LJCName = name
        };
        detail.LJCChange += Detail_Change;
        detail.ShowDialog();
      }
    }

    // Displays a detail dialog for a new record.
    internal void DoNew()
    {
      if (CodeGroupGrid.CurrentRow is LJCGridRow parentRow)
      {
        // Data from items.
        var codeLineName = parentRow.LJCGetString("CodeLine");
        var codeGroupName = parentRow.LJCGetString("Name");

        var location = FormCommon.GetDialogScreenPoint(SolutionGrid);
        var detail = new SolutionDetail()
        {
          LJCCodeLine = codeLineName,
          LJCCodeGroup = codeGroupName
        };
        detail.LJCChange += Detail_Change;
        detail.ShowDialog();
      }
    }

    // Refreshes the list.
    internal void DoRefresh()
    {
      CodeList.Cursor = Cursors.WaitCursor;
      Solution record = null;
      if (CodeGroupGrid.CurrentRow is LJCGridRow _
        && SolutionGrid.CurrentRow is LJCGridRow row)
      {
        // Save the original row.
        var parentKey = CodeList.GetSolutionParentKey();
        record = new Solution()
        {
          CodeLine = parentKey.CodeLine,
          CodeGroup = parentKey.CodeGroup,
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

    // Clears the solution dependencies
    internal void ClearDependencies()
    {
    }

    // Updates the solution dependencies.
    internal void UpdateDependencies()
    {
    }

    // Adds new row or updates row with record values.
    private void Detail_Change(object sender, EventArgs e)
    {
      var detail = sender as SolutionDetail;
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
          SolutionGrid.LJCSetCurrentRow(row, true);
          CodeList.TimedChange(Change.Solution);
        }
      }
    }
    #endregion

    #region Setup and Other Methods

    // Configures the CodeLine Grid.
    internal void SetupGrid()
    {
      // Setup default grid columns if no columns are defined.
      if (0 == SolutionGrid.Columns.Count)
      {
        // Get the grid columns from the manager Data Definition.
        var manager = SolutionManager;
        var gridColumns = manager.GetColumns();

        // Setup the grid columns.
        SolutionGrid.LJCAddColumns(gridColumns);
      }
    }
    #endregion

    #region Properties

    // Gets or sets the Solution Grid reference.
    private LJCDataGrid CodeGroupGrid { get; set; }

    // Gets or sets the Parent List reference.
    private CodeManagerList CodeList { get; set; }

    // Gets or sets the Data object.
    // *** Next Line *** Add - Data
    private ProjectFilesData Data { get; set; }

    // Gets or sets the Managers reference.
    private ManagersProjectFiles Managers { get; set; }

    // Gets or sets the Solution Grid reference.
    private LJCDataGrid SolutionGrid { get; set; }

    // Gets or sets the Manager reference.
    private SolutionManager SolutionManager { get; set; }

    // Gets or sets the Solutions collection.
    // *** Next Line *** Add - Data
    private Solutions Solutions { get; set; }
    #endregion
  }
}
