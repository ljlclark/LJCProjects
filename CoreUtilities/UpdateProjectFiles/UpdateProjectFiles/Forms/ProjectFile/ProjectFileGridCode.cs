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
      ProjectFileGrid = CodeList.ProjectFileGrid;
      ProjectGrid = CodeList.ProjectGrid;
      ResetData();
      CodeList.Cursor = Cursors.Default;
    }

    // Resets the DataConfig dependent objects.
    internal void ResetData()
    {
      Data = CodeList.Data;
      ProjectFiles = Data.ProjectFiles;
      Managers = CodeList.Managers;
      ProjectFileManager = Managers.ProjectFileManager;
    }
    #endregion

    #region Data Methods

    // Retrieves the list rows.
    internal void DataRetrieve(ProjectFileParentKey parentKey = null)
    {
      CodeList.Cursor = Cursors.WaitCursor;
      ProjectFileGrid.LJCRowsClear();

      //var projectFiles = ProjectFileManager.Load(parentKey);
      var projectFiles = ProjectFiles.LJCLoad(parentKey);
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
      var retValue = ProjectFileGrid.LJCRowAdd();
      SetStoredValues(retValue, dataRecord);
      retValue.LJCSetValues(ProjectFileGrid, dataRecord);
      return retValue;
    }

    // Selects a row based on the key record values.
    private bool RowSelect(ProjectFile dataRecord)
    {
      bool retValue = false;

      if (dataRecord != null)
      {
        CodeList.Cursor = Cursors.WaitCursor;
        foreach (LJCGridRow row in ProjectFileGrid.Rows)
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
            && fileSpec == dataRecord.SourceFilePath)
          {
            // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
            ProjectFileGrid.LJCSetCurrentRow(row, true);
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
      if (ProjectFileGrid.CurrentRow is LJCGridRow row)
      {
        SetStoredValues(row, dataRecord);
        row.LJCSetValues(ProjectFileGrid, dataRecord);
      }
    }

    // Sets the row stored values.
    private void SetStoredValues(LJCGridRow row, ProjectFile dataRecord)
    {
      row.LJCSetString("SourceCodeLine", dataRecord.SourceCodeLine);
      row.LJCSetString("SourceCodeGroup", dataRecord.SourceCodeGroup);
      row.LJCSetString("SourceSolution", dataRecord.SourceSolution);
      row.LJCSetString("SourceProject", dataRecord.SourceProject);
      row.LJCSetString("FileName", dataRecord.FileName);
    }
    #endregion

    #region Action Methods

    // Deletes the selected row.
    internal void DoDelete()
    {
      bool success = false;
      var parentRow = ProjectGrid.CurrentRow as LJCGridRow;
      var row = ProjectFileGrid.CurrentRow as LJCGridRow;
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
        var name = row.LJCGetString("FileName");
        var parentKey = new ProjectFileParentKey()
        {
          CodeLine = parentRow.LJCGetString("CodeLine"),
          CodeGroup = parentRow.LJCGetString("CodeGroup"),
          Solution = parentRow.LJCGetString("Solution"),
          Project = parentRow.LJCGetString("Name")
        };

        //ProjectFileManager.Delete(parentKey, name);
        ProjectFiles.LJCDelete(parentKey, name);
        ProjectFileManager.RecreateFile(ProjectFiles);
      }

      if (success)
      {
        ProjectFileGrid.Rows.Remove(row);
        CodeList.TimedChange(Change.ProjectFile);
      }
    }

    // Displays a detail dialog to edit a record.
    internal void DoEdit()
    {
      if (ProjectGrid.CurrentRow is LJCGridRow parentRow
        && ProjectFileGrid.CurrentRow is LJCGridRow row)
      {
        // Data from items.
        var codeLineName = parentRow.LJCGetString("CodeLine");
        var codeGroupName = parentRow.LJCGetString("CodeGroup");
        var solutionName = parentRow.LJCGetString("Solution");
        var projectName = parentRow.LJCGetString("Name");
        var name = row.LJCGetString("FileName");

        var location = FormCommon.GetDialogScreenPoint(ProjectGrid);
        var detail = new ProjectFileDetail()
        {
          LJCTargetLine = codeLineName,
          LJCTargetGroup = codeGroupName,
          LJCTargetSolution = solutionName,
          LJCTargetProject = projectName,
          LJCFileName = name
        };
        detail.LJCChange += Detail_Change;
        detail.ShowDialog();
      }
    }

    // Displays a detail dialog for a new record.
    internal void DoNew()
    {
      if (ProjectGrid.CurrentRow is LJCGridRow parentRow)
      {
        // Data from items.
        var codeLineName = parentRow.LJCGetString("CodeLine");
        var codeGroupName = parentRow.LJCGetString("CodeGroup");
        var solutionName = parentRow.LJCGetString("Solution");
        var projectName = parentRow.LJCGetString("Name");

        var location = FormCommon.GetDialogScreenPoint(ProjectGrid);
        var detail = new ProjectFileDetail()
        {
          LJCTargetLine = codeLineName,
          LJCTargetGroup = codeGroupName,
          LJCTargetSolution = solutionName,
          LJCTargetProject = projectName
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
      if (ProjectGrid.CurrentRow is LJCGridRow _
        && ProjectFileGrid.CurrentRow is LJCGridRow row)
      {
        // Save the original row.
        var parentKey = CodeList.GetProjectFileParentKey();
        record = new ProjectFile()
        {
          TargetCodeLine = parentKey.CodeLine,
          TargetCodeGroup = parentKey.CodeGroup,
          TargetSolution = parentKey.Solution,
          TargetProject = parentKey.Project,
          FileName = row.LJCGetString("FileName"),
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
          ProjectFileGrid.LJCSetCurrentRow(row, true);
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
      if (0 == ProjectFileGrid.Columns.Count)
      {
        // Get the grid columns from the manager Data Definition.
        var manager = ProjectFileManager;
        List<string> propertyNames = new List<string>()
        {
          "SourceCodeLine",
          "SourceCodeGroup",
          "SourceSolution",
          "SourceProject",
          "FileName",
        };
        var gridColumns = manager.GetColumns(propertyNames);

        // Setup the grid columns.
        ProjectFileGrid.LJCAddColumns(gridColumns);
      }
    }
    #endregion

    #region Properties

    // Gets or sets the Parent List reference.
    private CodeManagerList CodeList { get; set; }

    // Gets or sets the Data object.
    private ProjectFilesData Data { get; set; }

    // Gets or sets the Managers reference.
    private ManagersProjectFiles Managers { get; set; }

    // Gets or sets the Grid reference.
    private LJCDataGrid ProjectFileGrid { get; set; }

    // Gets or sets the ProjectFiles collection.
    private ProjectFiles ProjectFiles { get; set; }

    // Gets or sets the Manager reference.
    private ProjectFileManager ProjectFileManager { get; set; }

    // Gets or sets the Grid reference.
    private LJCDataGrid ProjectGrid { get; set; }
    #endregion
  }
}
