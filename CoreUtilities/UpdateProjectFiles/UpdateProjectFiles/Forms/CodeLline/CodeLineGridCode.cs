// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// CodeLineGridCode.cs
using LJCNetCommon;
using LJCWinFormCommon;
using LJCWinFormControls;
using ProjectFilesDAL;
using System;
using System.Windows.Forms;
using static UpdateProjectFiles.CodeManagerList;

namespace UpdateProjectFiles
{
  internal class CodeLineGridCode
  {
    #region Constructors

    // <summary>Initializes an object instance.</summary>
    internal CodeLineGridCode(CodeManagerList parentList)
    {
      // Initialize property values.
      parentList.Cursor = Cursors.WaitCursor;
      CodeList = parentList;
      CodeLineGrid = CodeList.CodeLineGrid;
      ResetData();
      CodeList.Cursor = Cursors.Default;
    }

    // Resets the DataConfig dependent objects.
    internal void ResetData()
    {
      Managers = CodeList.Managers;
      CodeLineManager = Managers.CodeLineManager;
    }
    #endregion

    #region Data Methods

    // Retrieves the list rows.
    internal void DataRetrieve()
    {
      CodeList.Cursor = Cursors.WaitCursor;
      CodeLineGrid.LJCRowsClear();

      //SetupGrid();
      var codeLines = CodeLineManager.Load();
      if (NetCommon.HasItems(codeLines))
      {
        foreach (var codeLine in codeLines)
        {
          RowAdd(codeLine);
        }
      }
      CodeList.Cursor = Cursors.Default;
      CodeList.DoChange(Change.CodeLine);
    }

    // Adds a grid row and updates it with the record values.
    private LJCGridRow RowAdd(CodeLine dataRecord)
    {
      var retValue = CodeLineGrid.LJCRowAdd();
      SetStoredValues(retValue, dataRecord);
      retValue.LJCSetValues(CodeLineGrid, dataRecord);
      return retValue;
    }

    // Selects a row based on the key record values.
    private bool RowSelect(CodeLine dataRecord)
    {
      bool retValue = false;

      if (dataRecord != null)
      {
        CodeList.Cursor = Cursors.WaitCursor;
        foreach (LJCGridRow row in CodeLineGrid.Rows)
        {
          var rowID = row.LJCGetString("Name");
          if (rowID == dataRecord.Name)
          {
            // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
            CodeLineGrid.LJCSetCurrentRow(row, true);
            retValue = true;
            break;
          }
        }
        CodeList.Cursor = Cursors.Default;
      }
      return retValue;
    }

    // Updates the current row with the record values.
    private void RowUpdate(CodeLine dataRecord)
    {
      if (CodeLineGrid.CurrentRow is LJCGridRow row)
      {
        SetStoredValues(row, dataRecord);
        row.LJCSetValues(CodeLineGrid, dataRecord);
      }
    }

    // Sets the row stored values.
    private void SetStoredValues(LJCGridRow row, CodeLine dataRecord)
    {
      row.LJCSetString("Name", dataRecord.Name);
    }
    #endregion

    #region Action Methods

    // Deletes the selected row.
    internal void DoDelete()
    {
      bool success = false;
      var row = CodeLineGrid.CurrentRow as LJCGridRow;
      if (row != null)
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

        CodeLineManager.Delete(name);
      }

      if (success)
      {
        CodeLineGrid.Rows.Remove(row);
        CodeList.TimedChange(Change.CodeLine);
      }
    }

    // Displays a detail dialog to edit a record.
    internal void DoEdit()
    {
      if (CodeLineGrid.CurrentRow is LJCGridRow row)
      {
        // Data from items.
        var name = row.LJCGetString("Name");

        var location = FormCommon.GetDialogScreenPoint(CodeLineGrid);
        var detail = new CodeLineDetail()
        {
          LJCName = name
        };
        detail.LJCChange += Detail_Change;
        detail.ShowDialog();
      }
    }

    // Displays a detail dialog for a new record.
    internal void DoNew()
    {
      var location = FormCommon.GetDialogScreenPoint(CodeLineGrid);
      var detail = new CodeLineDetail()
      {
      };
      detail.LJCChange += Detail_Change;
      detail.ShowDialog();
    }

    // Refreshes the list.
    internal void DoRefresh()
    {
      CodeList.Cursor = Cursors.WaitCursor;
      CodeLine record = null;
      if (CodeLineGrid.CurrentRow is LJCGridRow row)
      {
        // Save the original row.
        record = new CodeLine()
        {
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
      var detail = sender as CodeLineDetail;
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
          CodeLineGrid.LJCSetCurrentRow(row, true);
          CodeList.TimedChange(Change.CodeLine);
        }
      }
    }
    #endregion

    #region Setup and Other Methods

    // Configures the CodeLine Grid.
    internal void SetupGrid()
    {
      // Setup default grid columns if no columns are defined.
      if (0 == CodeLineGrid.Columns.Count)
      {
        // Get the grid columns from the manager Data Definition.
        var manager = CodeLineManager;
        var gridColumns = manager.GetColumns();

        // Setup the grid columns.
        CodeLineGrid.LJCAddColumns(gridColumns);
      }
    }
    #endregion

    #region Properties

    // Gets or sets the Parent List reference.
    private CodeManagerList CodeList { get; set; }

    // Gets or sets the _ClassName_ Grid reference.
    private LJCDataGrid CodeLineGrid { get; set; }

    // Gets or sets the Manager reference.
    private CodeLineManager CodeLineManager { get; set; }

    // Gets or sets the Managers reference.
    private ManagersProjectFiles Managers { get; set; }
    #endregion
  }
}
