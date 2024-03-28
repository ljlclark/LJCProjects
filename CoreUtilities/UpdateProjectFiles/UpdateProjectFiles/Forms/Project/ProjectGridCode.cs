// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// ProjectGridCode.cs
using LJCNetCommon;
using LJCWinFormCommon;
using LJCWinFormControls;
using ProjectFilesDAL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
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
      // *** Begin *** Add - Data
      Data = CodeList.Data;
      Projects = Data.Projects;
      DataHelper = new DataProjectFiles(Data);
      // *** End   *** Add - Data
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

      // *** Begin *** Change - Datas
      //var projects = ProjectManager.Load(parentKey);
      var projects = Projects.LJCLoad(parentKey);
      // *** End   *** Change - Datas
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
          CodeLine = parentRow.LJCGetString("CodeLine"),
          CodeGroup = parentRow.LJCGetString("CodeGroup"),
          Solution = parentRow.LJCGetString("Name")
        };

        // *** Begin *** Change - Datas
        //ProjectManager.Delete(parentKey, name);
        Projects.LJCDelete(parentKey, name);
        //ProjectManager.WriteBackup();
        ProjectManager.RecreateFile(Projects);
        // *** End   *** Change - Datas
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
        var codeLineName = parentRow.LJCGetString("CodeLine");
        var codeGroupName = parentRow.LJCGetString("CodeGroup");
        var solutionName = parentRow.LJCGetString("Name");
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
        var codeLineName = parentRow.LJCGetString("CodeLine");
        var codeGroupName = parentRow.LJCGetString("CodeGroup");
        var solutionName = parentRow.LJCGetString("Name");

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
      if (SolutionGrid.CurrentRow is LJCGridRow _
        && ProjectGrid.CurrentRow is LJCGridRow row)
      {
        // Save the original row.
        var parentKey = CodeList.GetProjectParentKey();
        record = new Project()
        {
          CodeLine = parentKey.CodeLine,
          CodeGroup = parentKey.CodeGroup,
          Solution = parentKey.Solution,
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
      if (SolutionGrid.CurrentRow is LJCGridRow _)
      {
        // Data from items.
        var projectFileParentKey = CodeList.GetProjectFileParentKey();

        var projectFiles = Data.ProjectFiles;
        var files = projectFiles.LJCLoad(projectFileParentKey);
        if (NetCommon.HasItems(files))
        {
          foreach (var projectFile in files)
          {
            var sourceFileSpec = SourceFileSpec(projectFile);
            var targetFileSpec = TargetFileSpec(projectFile);
            if (NetString.HasValue(sourceFileSpec)
              && NetString.HasValue(targetFileSpec))
            {
              File.Copy(sourceFileSpec, targetFileSpec, true);
            }
          }
          var title = "Solution Update";
          var message = "Solution Dependencies Update is Complete";
          MessageBox.Show(message, title, MessageBoxButtons.OK
            , MessageBoxIcon.Information);
        }
      }
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
          "Path"
        };
        var gridColumns = manager.GetColumns(propertyNames);

        // Setup the grid columns.
        ProjectGrid.LJCAddColumns(gridColumns);
      }
    }

    // Create the ProjectFile Source File Spec.
    internal string SourceFileSpec(ProjectFile projectFile)
    {
      string retValue = null;

      if (NetString.HasValue(projectFile.SourceCodeLine)
        && NetString.HasValue(projectFile.SourceCodeGroup)
        && NetString.HasValue(projectFile.SourceSolution)
        && NetString.HasValue(projectFile.SourceProject)
        && NetString.HasValue(projectFile.SourceFilePath)
        && NetString.HasValue(projectFile.FileName))
      {
        retValue = DataHelper.GetFileSpec(projectFile.SourceCodeLine
          , projectFile.SourceCodeGroup, projectFile.SourceSolution
          , projectFile.SourceProject, projectFile.SourceFilePath
          , projectFile.FileName);
      }
      return retValue;
    }

    // Create the ProjectFile Target File Spec.
    internal string TargetFileSpec(ProjectFile projectFile)
    {
      string retValue = null;

      if (NetString.HasValue(projectFile.TargetCodeLine)
        && NetString.HasValue(projectFile.TargetPathSolution)
        && NetString.HasValue(projectFile.TargetFilePath)
        && NetString.HasValue(projectFile.FileName))
      {
        var codeLineName = projectFile.TargetCodeLine;
        retValue = DataHelper.CodeLinePath(codeLineName);

        if (projectFile.TargetPathCodeGroup != null)
        {
          var codeGroupPath = DataHelper.CodeGroupPath(codeLineName
            , projectFile.TargetPathCodeGroup);
          retValue = Path.Combine(retValue, codeGroupPath);
        }

        var solutionParentKey = CodeList.GetSolutionParentKey();
        var solutionPath = DataHelper.SolutionPath(solutionParentKey
          , projectFile.TargetPathSolution);
        retValue = Path.Combine(retValue, solutionPath);

        if (NetString.HasValue(projectFile.TargetPathProject))
        {
          var projectParentKey = CodeList.GetProjectParentKey();
          var projectPath = DataHelper.ProjectPath(projectParentKey
            , projectFile.TargetPathProject);
          retValue = Path.Combine(retValue, projectPath);
        }

        retValue = Path.Combine(retValue, projectFile.TargetFilePath);
        retValue = Path.Combine(retValue, projectFile.FileName);
      }
      return retValue;
    }
    #endregion

    #region Properties

    // Gets or sets the Data object.
    // *** Next Line *** Add - Data
    private DataProjectFiles DataHelper { get; set; }

    // Gets or sets the Parent List reference.
    private CodeManagerList CodeList { get; set; }

    // Gets or sets the Data object.
    // *** Next Line *** Add - Data
    private ProjectFilesData Data { get; set; }

    // Gets or sets the Managers reference.
    private ManagersProjectFiles Managers { get; set; }

    // Gets or sets the Grid reference.
    private LJCDataGrid ProjectGrid { get; set; }

    // Gets or sets the Manager reference.
    private ProjectManager ProjectManager { get; set; }

    // Gets or sets the Projects collection.
    // *** Next Line *** Add - Data
    private Projects Projects { get; set; }

    // Gets or sets the SolutionGrid reference.
    private LJCDataGrid SolutionGrid { get; set; }
    #endregion
  }
}
