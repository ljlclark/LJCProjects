// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// ProjectFileGridCode.cs
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
  internal class ProjectFileGridCode
  {
    #region Constructors

    // <summary>Initializes an object instance.</summary>
    internal ProjectFileGridCode(CodeManagerList parentList)
    {
      // Initialize property values.
      parentList.Cursor = Cursors.WaitCursor;
      CodeList = parentList;
      FileGrid = CodeList.FileGrid;
      ProjectGrid = CodeList.ProjectGrid;
      ResetData();
      CodeList.Cursor = Cursors.Default;
    }

    // Resets the DataConfig dependent objects.
    internal void ResetData()
    {
      Managers = CodeList.Managers;
      FileManager = Managers.ProjectFileManager;
    }
    #endregion

    #region Data Methods

    // Retrieves the list rows.
    internal void DataRetrieve(ProjectFileKey parentKey = null)
    {
      CodeList.Cursor = Cursors.WaitCursor;
      FileGrid.LJCRowsClear();

      var projectFiles = FileManager.Load(parentKey);
      if (NetCommon.HasItems(projectFiles))
      {
        foreach (var projectFile in projectFiles)
        {
          RowAdd(projectFile);
        }
      }
      CodeList.Cursor = Cursors.Default;
      CodeList.DoChange(Change.ProjectFile);
    }

    // Adds a grid row and updates it with the record values.
    private LJCGridRow RowAdd(ProjectFile dataRecord)
    {
      var retValue = FileGrid.LJCRowAdd();
      SetStoredValues(retValue, dataRecord);
      retValue.LJCSetValues(FileGrid, dataRecord);
      return retValue;
    }

    // Selects a row based on the key record values.
    private bool RowSelect(ProjectFile dataRecord)
    {
      bool retValue = false;

      if (dataRecord != null)
      {
        CodeList.Cursor = Cursors.WaitCursor;
        foreach (LJCGridRow row in FileGrid.Rows)
        {
          var codeLine = row.LJCGetString("SourceCodeLine");
          var codeGroup = row.LJCGetString("SourceCodeGrouop");
          var solution = row.LJCGetString("SourceSolution");
          var project = row.LJCGetString("SourceProject");
          var fileSpec = row.LJCGetString("SourceFileSpec");
          if (codeLine == dataRecord.SourceCodeLine
            && codeGroup == dataRecord.SourceCodeGroup
            && solution == dataRecord.SourceSolution
            && project == dataRecord.SourceProject
            && fileSpec == dataRecord.SourceFileSpec)
          {
            // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
            FileGrid.LJCSetCurrentRow(row, true);
            retValue = true;
            break;
          }
        }
        CodeList.Cursor = Cursors.Default;
      }
      return retValue;
    }

    // Updates the current row with the record values.
    private void RowUpdate(ProjectFile dataRecord)
    {
      if (FileGrid.CurrentRow is LJCGridRow row)
      {
        SetStoredValues(row, dataRecord);
        row.LJCSetValues(FileGrid, dataRecord);
      }
    }

    // Sets the row stored values.
    private void SetStoredValues(LJCGridRow row, ProjectFile dataRecord)
    {
      row.LJCSetString("SourceCodeLine", dataRecord.SourceCodeLine);
      row.LJCSetString("SourceCodeGroup", dataRecord.SourceCodeGroup);
      row.LJCSetString("SourceSolution", dataRecord.SourceSolution);
      row.LJCSetString("SourceProject", dataRecord.SourceProject);
      row.LJCSetString("SourceFileSpec", dataRecord.SourceFileSpec);
    }
    #endregion

    #region Action Methods

    // Deletes the selected row.
    internal void DoDelete()
    {
      bool success = false;
      var parentRow = ProjectGrid.CurrentRow as LJCGridRow;
      var row = FileGrid.CurrentRow as LJCGridRow;
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
        var parentKey = new ProjectFileKey()
        {
          CodeLine = parentRow.LJCGetString("CodeLineName"),
          CodeGroup = parentRow.LJCGetString("CodeGroupName"),
          Solution = parentRow.LJCGetString("SolutionName")
        };

        FileManager.Delete(parentKey, name);
      }

      if (success)
      {
        FileGrid.Rows.Remove(row);
        CodeList.TimedChange(Change.ProjectFile);
      }
    }

    // Displays a detail dialog to edit a record.
    internal void DoEdit()
    {
      var parentRow = ProjectGrid.CurrentRow as LJCGridRow;
      var row = FileGrid.CurrentRow as LJCGridRow;
      if (parentRow != null
        && row != null)
      {
        // Data from items.
        var codeLineName = parentRow.LJCGetString("CodeLineName");
        var codeGroupName = parentRow.LJCGetString("CodeGroupName");
        var solutionName = parentRow.LJCGetString("SolutionName");
        var projectName = parentRow.LJCGetString("projectsName");
        var name = row.LJCGetString("Name");

        var location = FormCommon.GetDialogScreenPoint(ProjectGrid);
        var detail = new ProjectFileDetail()
        {
          LJCCodeLine = codeLineName,
          LJCCodeGroup = codeGroupName,
          LJCSolution = solutionName,
          LJCProject = projectName,
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
      var parentRow = ProjectGrid.CurrentRow as LJCGridRow;
      if (parentRow != null)
      {
        // Data from items.
        var codeLineName = parentRow.LJCGetString("CodeLineName");
        var codeGroupName = parentRow.LJCGetString("CodeGroupName");
        var solutionName = parentRow.LJCGetString("SolutionName");
        var projectName = parentRow.LJCGetString("projectsName");

        var location = FormCommon.GetDialogScreenPoint(ProjectGrid);
        var detail = new ProjectFileDetail()
        {
          LJCCodeLine = codeLineName,
          LJCCodeGroup = codeGroupName,
          LJCSolution = solutionName,
          LJCProject = projectName,
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
      ProjectFile record = null;
      if (ProjectGrid.CurrentRow is LJCGridRow parentRow
        && FileGrid.CurrentRow is LJCGridRow row)
      {
        // Save the original row.
        record = new ProjectFile()
        {
          TargetCodeLine = parentRow.LJCGetString("TargetCodeLine"),
          TargetCodeGroup = parentRow.LJCGetString("TargetCodeGroup"),
          TargetSolution = parentRow.LJCGetString("TargetSolution"),
          TargetProject = parentRow.LJCGetString("TargetProject"),
          SourceFileName = row.LJCGetString("SourceFileName"),
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
      var detail = sender as ProjectFileDetail;
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
          FileGrid.LJCSetCurrentRow(row, true);
          CodeList.TimedChange(Change.ProjectFile);
        }
      }
    }
    #endregion

    #region Setup and Other Methods

    // Configures the CodeLine Grid.
    internal void SetupGrid()
    {
      // Setup default grid columns if no columns are defined.
      if (0 == FileGrid.Columns.Count)
      {
        // Get the grid columns from the manager Data Definition.
        var manager = FileManager;
        List<string> propertyNames = new List<string>()
        {
          "SourceCodeLine",
          "SourceCodeGroup",
          "SourceSolution",
          "SourceProject",
          "SourceFileSpec",
        };
        var gridColumns = manager.GetColumns(propertyNames);

        // Setup the grid columns.
        FileGrid.LJCAddColumns(gridColumns);
      }
    }
    #endregion

    #region Properties

    // Gets or sets the Parent List reference.
    private CodeManagerList CodeList { get; set; }

    // Gets or sets the Managers reference.
    private ManagersProjectFiles Managers { get; set; }

    // Gets or sets the Grid reference.
    private LJCDataGrid FileGrid { get; set; }

    // Gets or sets the Manager reference.
    private ProjectFileManager FileManager { get; set; }

    // Gets or sets the Grid reference.
    private LJCDataGrid ProjectGrid { get; set; }
    #endregion
  }
}
