// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// ProjectGridCode.cs
using LJCNetCommon;
using LJCWinFormControls;
using ProjectFilesDAL;
using System.Windows.Forms;
using static UpdateProjectFiles.CodeManagerList;

namespace UpdateProjectFiles
{
  internal class ProjectGridCode
  {
    #region Constructors

    // <summary>Initializes an object instance.</summary>
    internal ProjectGridCode(CodeManagerList parentList)
    {
      // Initialize property values.
      parentList.Cursor = Cursors.WaitCursor;
      CodeList = parentList;
      ProjectGrid = CodeList.ProjectGrid;
      ResetData();
      CodeList.Cursor = Cursors.Default;
    }

    // Resets the DataConfig dependent objects.
    internal void ResetData()
    {
      Managers = CodeList.Managers;
      ProjectManager = Managers.ProjectManager;
    }
    #endregion

    #region Data Methods

    // Retrieves the list rows.
    internal void DataRetrieve()
    {
      CodeList.Cursor = Cursors.WaitCursor;
      ProjectGrid.LJCRowsClear();

      //SetupGrid();
      var projects = ProjectManager.Load();
      if (NetCommon.HasItems(projects))
      {
        foreach (var project in projects)
        {
          RowAdd(project);
        }
      }
      CodeList.Cursor = Cursors.Default;
      CodeList.DoChange(Change.Project);
    }

    // Adds a grid row and updates it with the record values.
    private LJCGridRow RowAdd(Project dataRecord)
    {
      var retValue = ProjectGrid.LJCRowAdd();
      SetStoredValues(retValue, dataRecord);
      retValue.LJCSetValues(ProjectGrid, dataRecord);
      return retValue;
    }

    // Selects a row based on the key record values.
    private bool RowSelect(Project dataRecord)
    {
      bool retValue = false;

      if (dataRecord != null)
      {
        CodeList.Cursor = Cursors.WaitCursor;
        foreach (LJCGridRow row in ProjectGrid.Rows)
        {
          var codeLine = row.LJCGetString("CodeLine");
          var codeGroup = row.LJCGetString("          if (rowID == dataRecord.Name)\r\n          if (rowID == dataRecord.Name)\r\n          if (rowID == dataRecord.Name)\r\n");
          var solution = row.LJCGetString("Solution");
          var rowID = row.LJCGetString("Name");
          if (codeLine == dataRecord.CodeLine
            && codeGroup == dataRecord.CodeGroup
            && solution == dataRecord.Solution
            && rowID == dataRecord.Name)
          {
            // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
            ProjectGrid.LJCSetCurrentRow(row, true);
            retValue = true;
            break;
          }
        }
        CodeList.Cursor = Cursors.Default;
      }
      return retValue;
    }

    // Updates the current row with the record values.
    private void RowUpdate(Project dataRecord)
    {
      if (ProjectGrid.CurrentRow is LJCGridRow row)
      {
        SetStoredValues(row, dataRecord);
        row.LJCSetValues(ProjectGrid, dataRecord);
      }
    }

    // Sets the row stored values.
    private void SetStoredValues(LJCGridRow row, Project dataRecord)
    {
      //row.LJCSetString(CodeLine.ColumnID, dataRecord.ID);
      row.LJCSetString("CodeLine", dataRecord.CodeLine);
      row.LJCSetString("CodeGroup", dataRecord.CodeGroup);
      row.LJCSetString("Solution", dataRecord.Solution);
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
      if (0 == ProjectGrid.Columns.Count)
      {
        // Get the grid columns from the manager Data Definition.
        var manager = ProjectManager;
        var gridColumns = manager.GetColumns();

        // Setup the grid columns.
        ProjectGrid.LJCAddColumns(gridColumns);
      }
    }
    #endregion

    #region Properties

    // Gets or sets the Parent List reference.
    private CodeManagerList CodeList { get; set; }

    // Gets or sets the Managers reference.
    private ManagersProjectFiles Managers { get; set; }

    // Gets or sets the _ClassName_ Grid reference.
    private LJCDataGrid ProjectGrid { get; set; }

    // Gets or sets the Manager reference.
    private ProjectManager ProjectManager { get; set; }
    #endregion
  }
}
