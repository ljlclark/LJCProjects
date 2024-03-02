﻿using ProjectFilesDAL;
using System.Security.Policy;
using System.Text;
using System.Windows.Forms;

namespace UpdateProjectFiles
{
  internal partial class Form1 : Form
  {
    internal Form1()
    {
      InitializeComponent();

      //TestCodeLine();
      //TestCodeGroup();
      //TestSolution();
      //TestProject();
    }

    // Test the CodeLine data managermethods.
    internal void TestCodeLine()
    {
      var manager = new CodeLineManager();

      // Add
      var newCodeLineName = "LJCProjects";
      var codeLine = manager.Add(newCodeLineName, "NewPath");
      ShowCodeLine(manager, "Add");

      // Load and Sort
      manager.SortFile();

      // Update
      if (codeLine != null)
      {
        codeLine.Path = $"{codeLine.Path}Updated";
        manager.Update(codeLine);
        ShowCodeLine(manager, "Update");
      }

      // Delete
      manager.Delete(newCodeLineName);
    }

    // Test the CodeGroup data managermethods.
    internal void TestCodeGroup()
    {
      var manager = new CodeGroupManager();

      // Retrieve
      var parentKey = "LJCProjectsDev";
      var codeGroupName = "CoreAssemblies";
      manager.Retrieve(parentKey, codeGroupName);
      ShowCodeGroup(manager, "Retrieve");

      // Add
      var newCodeGroupName = "ANewCodeGroup";
      var codeGroup = manager.Add(parentKey, newCodeGroupName, "NewPath");
      ShowCodeGroup(manager, "Add");

      // Load and Sort
      manager.SortFile();

      // Update
      if (codeGroup != null)
      {
        codeGroup.Path = $"{codeGroup.Path}Updated";
        manager.Update(codeGroup);
        ShowCodeGroup(manager, "Update");
      }

      // Delete
      manager.Delete(parentKey, newCodeGroupName);
    }

    // Test the Solution data managermethods.
    internal void TestSolution()
    {
      var manager = new SolutionManager();

      // Retrieve
      var parentKey = new SolutionParentKey()
      {
        CodeLine = "LJCProjectsDev",
        CodeGroup = "CoreAssemblies"
      };
      var solutionName = "LJCNetCommon";
      manager.Retrieve(parentKey, solutionName);
      ShowSolution(manager, "Retrieve");

      // Add
      var newSolutionName = "ANewSolution";
      int sequence = 3;
      var solution = manager.Add(parentKey, newSolutionName, sequence
        , "NewPath");
      ShowSolution(manager, "Add");

      // Load and Sort
      manager.SortFile();

      // Update
      if (solution != null)
      {
        solution.Path = $"{solution.Path}Updated";
        manager.Update(solution);
        ShowSolution(manager, "Update");
      }

      // Delete
      manager.Delete(parentKey, newSolutionName);
    }

    // Test the Project data managermethods.
    internal void TestProject()
    {
      var manager = new ProjectManager();

      // Retrieve
      var parentKey = new ProjectParentKey()
      {
        CodeLine = "LJCProjectsDev",
        CodeGroup = "CoreAssemblies",
        Solution = "LJCNetCommon"
      };
      var projectName = "LJCNetCommon";
      manager.Retrieve(parentKey, projectName);
      ShowProject(manager, "Retrieve");

      // Add
      var newProjectName = "ANewProject";
      var project = manager.Add(parentKey, newProjectName, "NewPath");
      ShowProject(manager, "Add");

      // Load and Sort
      manager.SortFile();

      // Update
      if (project != null)
      {
        project.Path = $"{project.Path}Updated";
        manager.Update(project);
        ShowProject(manager, "Update");
      }

      // Delete
      manager.Delete(parentKey, newProjectName);
    }

    private void ShowCodeGroup(CodeGroupManager manager, string text)
    {
      var codeGroup = manager.CurrentDataObject();
      if (codeGroup != null)
      {
        var builder = new StringBuilder(256);
        builder.AppendLine($"CodeGroup {text}");
        builder.AppendLine($"CodeLine: {codeGroup.CodeLine}");
        builder.AppendLine($"Name: {codeGroup.Name}");
        builder.AppendLine($"Path: {codeGroup.Path}");
        var message = builder.ToString();
        MessageBox.Show(message);
      }
    }

    private void ShowCodeLine(CodeLineManager manager, string text)
    {
      var codeLine = manager.CurrentDataObject();
      if (codeLine != null)
      {
        var builder = new StringBuilder(256);
        builder.AppendLine($"CodeLine {text}");
        builder.AppendLine($"Name: {codeLine.Name}");
        builder.AppendLine($"Path: {codeLine.Path}");
        var message = builder.ToString();
        MessageBox.Show(message);
      }
    }

    private void ShowSolution(SolutionManager manager, string text)
    {
      var solution = manager.CurrentDataObject();
      if (solution != null)
      {
        var builder = new StringBuilder(256);
        builder.AppendLine($"Solution {text}");
        builder.AppendLine($"CodeLine: {solution.CodeLine}");
        builder.AppendLine($"CodeGroup: {solution.CodeGroup}");
        builder.AppendLine($"Name: {solution.Name}");
        builder.AppendLine($"Path: {solution.Path}");
        var message = builder.ToString();
        MessageBox.Show(message);
      }
    }

    private void ShowProject(ProjectManager manager, string text)
    {
      var project = manager.CurrentDataObject();
      if (project != null)
      {
        var builder = new StringBuilder(256);
        builder.AppendLine($"Project {text}");
        builder.AppendLine($"CodeLine: {project.CodeLine}");
        builder.AppendLine($"CodeGroup: {project.CodeGroup}");
        builder.AppendLine($"Solution: {project.Solution}");
        builder.AppendLine($"Name: {project.Name}");
        builder.AppendLine($"Path: {project.Path}");
        var message = builder.ToString();
        MessageBox.Show(message);
      }
    }
  }
}
