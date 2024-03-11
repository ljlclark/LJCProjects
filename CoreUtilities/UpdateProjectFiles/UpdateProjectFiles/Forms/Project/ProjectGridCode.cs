// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// ProjectGridCode.cs
using LJCNetCommon;
using LJCWinFormCommon;
using LJCWinFormControls;
using ProjectFilesDAL;
using System;
using System.Collections.Generic;
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
      SolutionGrid = CodeList.SolutionGrid;
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
    internal void DataRetrieve(ProjectParentKey parentKey = null)
    {
      CodeList.Cursor = Cursors.WaitCursor;
      ProjectGrid.LJCRowsClear();

      var projects = ProjectManager.Load(parentKey);
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
          var codeGroup = row.LJCGetString("CodeGroup");
          var solution = row.LJCGetString("Solution");
          var name = row.LJCGetString("Name");
          if (codeLine == dataRecord.CodeLine
            && codeGroup == dataRecord.CodeGroup
            && solution == dataRecord.Solution
            && name == dataRecord.Name)
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
      bool success = false;
      var parentRow = SolutionGrid.CurrentRow as LJCGridRow;
      var row = ProjectGrid.CurrentRow as LJCGridRow;
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
        var parentKey = new ProjectParentKey()
        {
          CodeLine = parentRow.LJCGetString("CodeLineName"),
          CodeGroup = parentRow.LJCGetString("CodeGroupName"),
          Solution = parentRow.LJCGetString("SolutionName")
        };

        ProjectManager.Delete(parentKey, name);
      }

      if (success)
      {
        ProjectGrid.Rows.Remove(row);
        CodeList.TimedChange(Change.Project);
      }
    }

    // Displays a detail dialog to edit a record.
    internal void DoEdit()
    {
      if (SolutionGrid.CurrentRow is LJCGridRow parentRow
        && ProjectGrid.CurrentRow is LJCGridRow row)
      {
        // Data from items.
        var codeLineName = parentRow.LJCGetString("CodeLineName");
        var codeGroupName = parentRow.LJCGetString("CodeGroupName");
        var solutionName = parentRow.LJCGetString("SolutionName");
        var name = row.LJCGetString("Name");

        var location = FormCommon.GetDialogScreenPoint(ProjectGrid);
        var detail = new ProjectDetail()
        {
          LJCCodeLine = codeLineName,
          LJCCodeGroup = codeGroupName,
          LJCSolution = solutionName,
          LJCName = name
        };
        detail.LJCChange += Detail_Change;
        detail.ShowDialog();
      }
    }

    // Displays a detail dialog for a new record.
    internal void DoNew()
    {
      if (SolutionGrid.CurrentRow is LJCGridRow parentRow)
      {
        // Data from items.
        var codeLineName = parentRow.LJCGetString("CodeLineName");
        var codeGroupName = parentRow.LJCGetString("CodeGroupName");
        var solutionName = parentRow.LJCGetString("SolutionName");

        var location = FormCommon.GetDialogScreenPoint(ProjectGrid);
        var detail = new ProjectDetail()
        {
          LJCCodeLine = codeLineName,
          LJCCodeGroup = codeGroupName,
          LJCSolution = solutionName
        };
        detail.LJCChange += Detail_Change;
        detail.ShowDialog();
      }
    }

    // Refreshes the list.
    internal void DoRefresh()
    {
      CodeList.Cursor = Cursors.WaitCursor;
      Project record = null;
      if (SolutionGrid.CurrentRow is LJCGridRow parentRow
        && ProjectGrid.CurrentRow is LJCGridRow row)
      {
        // Save the original row.
        record = new Project()
        {
          CodeLine = parentRow.LJCGetString("CodeLine"),
          CodeGroup = parentRow.LJCGetString("CodeGroup"),
          Solution = parentRow.LJCGetString("Solution"),
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
      var detail = sender as ProjectDetail;
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
          ProjectGrid.LJCSetCurrentRow(row, true);
          CodeList.TimedChange(Change.Project);
        }
      }
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
        List<string> propertyNames = new List<string>()
        {
          "CodeLine",
          "CodeGroup",
          "Solution",
          "Name",
        };
        var gridColumns = manager.GetColumns(propertyNames);

        // Setup the grid columns.
        ProjectGrid.LJCAddColumns(gridColumns);
      }
    }
    #endregion

    #region Properties

    // Gets or sets the Manager reference.
    //private CodeGroupManager CodeGroupManager { get; set; }

    // Gets or sets the Parent List reference.
    private CodeManagerList CodeList { get; set; }

    // Gets or sets the Managers reference.
    private ManagersProjectFiles Managers { get; set; }

    // Gets or sets the Grid reference.
    private LJCDataGrid ProjectGrid { get; set; }

    // Gets or sets the Manager reference.
    private ProjectManager ProjectManager { get; set; }

    // Gets or sets the SolutionGrid reference.
    private LJCDataGrid SolutionGrid { get; set; }
    #endregion
  }
}
