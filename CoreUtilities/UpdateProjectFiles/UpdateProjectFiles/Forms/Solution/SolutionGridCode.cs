// Copyright(c) Lester J.Clark and Contributors.
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

      //SetupGrid();
      var solutions = SolutionManager.Load(parentKey);
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
          CodeGroup = parentRow.LJCGetString("CodeGroup"),
        };

        SolutionManager.Delete(parentKey, name);
      }
    }

    // Displays a detail dialog to edit a record.
    internal void DoEdit()
    {
      var parentRow = CodeGroupGrid.CurrentRow as LJCGridRow;
      var row = SolutionGrid.CurrentRow as LJCGridRow;
      if (parentRow != null
        && row != null)
      {
        // Data from items.
        var codeLineName = parentRow.LJCGetString("CodeLine");
        var codeGroupName = parentRow.LJCGetString("CodeGroup");
        var name = row.LJCGetString("Name");

        var location = FormCommon.GetDialogScreenPoint(SolutionGrid);
        var detail = new SolutionDetail()
        {
          LJCCodeLine = codeLineName,
          LJCCodeGroup = codeGroupName,
          LJCName = name,
          Managers = Managers
        };
        detail.LJCChange += Detail_Change;
        detail.ShowDialog();
      }
    }

    // Displays a detail dialog for a new record.
    internal void DoNew()
    {
      var parentRow = CodeGroupGrid.CurrentRow as LJCGridRow;
      if (parentRow != null)
      {
        // Data from items.
        var codeLineName = parentRow.LJCGetString("CodeLine");
        var codeGroupName = parentRow.LJCGetString("CodeGroup");

        var location = FormCommon.GetDialogScreenPoint(SolutionGrid);
        var detail = new SolutionDetail()
        {
          LJCCodeLine = codeLineName,
          LJCCodeGroup = codeGroupName,
          Managers = Managers
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
      if (CodeGroupGrid.CurrentRow is LJCGridRow parentRow
        && SolutionGrid.CurrentRow is LJCGridRow row)
      {
        // Save the original row.
        record = new Solution()
        {
          CodeLine = parentRow.LJCGetString("CodeLine"),
          CodeGroup = parentRow.LJCGetString("CodeGroup"),
          Name = row.LJCGetString("Name"),
        };
      }
      DataRetrieve();

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

    // Gets or sets the Managers reference.
    private ManagersProjectFiles Managers { get; set; }

    // Gets or sets the Solution Grid reference.
    private LJCDataGrid SolutionGrid { get; set; }

    // Gets or sets the Manager reference.
    private SolutionManager SolutionManager { get; set; }
    #endregion
  }
}
