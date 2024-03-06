// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// CodeGroupGridCode.cs
using LJCNetCommon;
using LJCWinFormControls;
using ProjectFilesDAL;
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
      CodeList = parentList;
      CodeGroupGrid = CodeList.CodeGroupGrid;
      ResetData();
      CodeList.Cursor = Cursors.Default;
    }

    // Resets the DataConfig dependent objects.
    internal void ResetData()
    {
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

      //SetupGrid();
      var codeGroups = CodeGroupManager.Load(parentKey);
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
      var retValue = CodeGroupGrid.LJCRowAdd();
      SetStoredValues(retValue, dataRecord);
      retValue.LJCSetValues(CodeGroupGrid, dataRecord);
      return retValue;
    }

    // Selects a row based on the key record values.
    private bool RowSelect(CodeGroup dataRecord)
    {
      bool retValue = false;

      if (dataRecord != null)
      {
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
      row.LJCSetString("CodeLine", dataRecord.CodeLine);
      row.LJCSetString("Name", dataRecord.Name);
    }
    #endregion

    #region Action Methods

    // Deletes the selected row.
    internal void DoDelete()
    {
    }

    // Displays a detail dialog to edit a record.
    internal void DoEdit()
    {
    }

    // Displays a detail dialog for a new record.
    internal void DoNew()
    {
    }

    // Refreshes the list.
    internal void DoRefresh()
    {
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

    // Gets or sets the CodeGroup Grid reference.
    private LJCDataGrid CodeGroupGrid { get; set; }

    // Gets or sets the Manager reference.
    private CodeGroupManager CodeGroupManager { get; set; }

    // Gets or sets the Parent List reference.
    private CodeManagerList CodeList { get; set; }

    // Gets or sets the Managers reference.
    private ManagersProjectFiles Managers { get; set; }
    #endregion
  }
}
