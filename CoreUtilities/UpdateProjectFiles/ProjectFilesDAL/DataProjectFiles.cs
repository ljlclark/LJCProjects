// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// DataProjectFiles.cs
using LJCNetCommon;
using System.IO;

namespace ProjectFilesDAL
{
  /// <summary>
  /// Provides ProjectFile data helper methods.
  /// </summary>
  public class DataProjectFiles
  {
    // Initializes an object instance.
    /// <summary>
    /// Initializes an object instance.
    /// </summary>
    /// <param name="data"></param>
    public DataProjectFiles(ProjectFilesData data)
    {
      Data = data;
    }

    #region Data Methods

    // Create a File Spec.
    public string GetFileSpec(string codeLineName, string codeGroupName
      , string solutionName, string projectName, string filePath
      , string fileName = null)
    {
      string retValue = null;

      if (NetString.HasValue(codeLineName)
        && NetString.HasValue(solutionName))
      {
        retValue = CodeLinePath(codeLineName);

        if (NetString.HasValue(codeGroupName))
        {
          var codeGroupPath = CodeGroupPath(codeLineName
            , codeGroupName);
          retValue = Path.Combine(retValue, codeGroupPath);
        }

        var solutionParentKey = new SolutionParentKey()
        {
          CodeLine = codeLineName,
          CodeGroup = codeGroupName
        };
        var solutionPath = SolutionPath(solutionParentKey
          , solutionName);
        retValue = Path.Combine(retValue, solutionPath);

        var projectParentKey = new ProjectParentKey()
        {
          CodeLine = codeLineName,
          CodeGroup = codeGroupName,
          Solution = solutionName
        };
        var projectPath = ProjectPath(projectParentKey
          , projectName);
        retValue = Path.Combine(retValue, projectPath);

        retValue = Path.Combine(retValue, filePath);
        if (NetString.HasValue(fileName))
        {
          retValue = Path.Combine(retValue, fileName);
        }
      }
      return retValue;
    }

    // Gets the CodeGroup object.
    /// <summary>
    /// Gets the CodeGroup object.
    /// </summary>
    /// <param name="codeLine">The CodeLine name.</param>
    /// <param name="name">The name value.</param>
    /// <returns>The CodeGroup object.</returns>
    public CodeGroup CodeGroup(string codeLine, string name)
    {
      CodeGroup retValue = null;

      if (NetString.HasValue(name))
      {
        var codeGroups = Data.CodeGroups;
        retValue = codeGroups.LJCRetrieve(codeLine, name);
      }
      return retValue;
    }

    // Gets the CodeGroup path value.
    /// <summary>
    /// Gets the CodeGroup path value.
    /// </summary>
    /// <param name="codeLine">The CodeLine name.</param>
    /// <param name="name">The name value.</param>
    /// <returns>The CodeGroup path value.</returns>
    public string CodeGroupPath(string codeLine, string name)
    {
      string retValue = null;

      var codeGroup = CodeGroup(codeLine, name);
      if (codeGroup != null)
      {
        retValue = codeGroup.Path;
      }
      return retValue;
    }

    // Gets the CodeLine object.
    /// <summary>
    /// Gets the CodeLine object.
    /// </summary>
    /// <param name="name">The name value.</param>
    /// <returns>The CodeLine object.</returns>
    public CodeLine CodeLine(string name)
    {
      CodeLine retValue = null;

      if (NetString.HasValue(name))
      {
        var codeLines = Data.CodeLines;
        retValue = codeLines.LJCRetrieve(name);
      }
      return retValue;
    }

    // Gets the CodeLine path value.
    /// <summary>
    /// Gets the CodeLine path value.
    /// </summary>
    /// <param name="name">The name value.</param>
    /// <returns>The CodeLine path value.</returns>
    public string CodeLinePath(string name)
    {
      string retValue = null;

      var codeLine = CodeLine(name);
      if (codeLine != null)
      {
        retValue = codeLine.Path;
      }
      return retValue;
    }

    // Gets the Project object.
    /// <summary>
    /// Gets the Project object.
    /// </summary>
    /// <param name="parentKey">The ParentKey object.</param>
    /// <param name="name">The name value.</param>
    /// <returns>The Solution object.</returns>
    public Project Project(ProjectParentKey parentKey, string name)
    {
      Project retValue = null;

      if (NetString.HasValue(name))
      {
        var projects = Data.Projects;
        retValue = projects.LJCRetrieve(parentKey, name);
      }
      return retValue;
    }

    // Gets the Project path value.
    /// <summary>
    /// Gets the Project path value.
    /// </summary>
    /// <param name="parentKey">The ParentKey object.</param>
    /// <param name="name">The name value.</param>
    /// <returns>The Project path value.</returns>
    public string ProjectPath(ProjectParentKey parentKey
      , string name)
    {
      string retValue = null;

      var project = Project(parentKey, name);
      if (project != null)
      {
        retValue = project.Path;
      }
      return retValue;
    }

    // Gets the ProjectFiles object.
    /// <summary>
    /// Gets the ProjectFile object.
    /// </summary>
    /// <param name="parentKey">The ParentKey object.</param>
    /// <param name="name">The name value.</param>
    /// <returns>The Solution object.</returns>
    public ProjectFile ProjectFile(ProjectFileParentKey parentKey, string name)
    {
      ProjectFile retValue = null;

      if (NetString.HasValue(name))
      {
        var projectFiles = Data.ProjectFiles;
        retValue = projectFiles.LJCRetrieve(parentKey, name);
      }
      return retValue;
    }

    // Gets the ProjectFile source Spath value.
    /// <summary>
    /// Gets the ProjectFile source path value.
    /// </summary>
    /// <param name="parentKey">The ParentKey object.</param>
    /// <param name="name">The name value.</param>
    /// <returns>The ProjectFile path value.</returns>
    public string ProjectFileSourcePath(ProjectFileParentKey parentKey
      , string name)
    {
      string retValue = null;

      var projectFile = ProjectFile(parentKey, name);
      if (projectFile != null)
      {
        retValue = projectFile.SourceFilePath;
      }
      return retValue;
    }

    // Gets the Solution object.
    /// <summary>
    /// Gets the Solution object.
    /// </summary>
    /// <param name="parentKey">The ParentKey object.</param>
    /// <param name="name">The name value.</param>
    /// <returns>The Solution object.</returns>
    public Solution Solution(SolutionParentKey parentKey, string name)
    {
      Solution retValue = null;

      if (NetString.HasValue(name))
      {
        var solutions = Data.Solutions;
        retValue = solutions.LJCRetrieve(parentKey, name);
      }
      return retValue;
    }

    // Gets the CodeLine path value.
    /// <summary>
    /// Gets the CodeLine path value.
    /// </summary>
    /// <param name="parentKey">The ParentKey object.</param>
    /// <param name="name">The name value.</param>
    /// <returns>The Solution path value.</returns>
    public string SolutionPath(SolutionParentKey parentKey
      , string name)
    {
      string retValue = null;

      var solution = Solution(parentKey, name);
      if (solution != null)
      {
        retValue = solution.Path;
      }
      return retValue;
    }
    #endregion

    #region Properties

    /// <summary>Gets or sets the Data object.</summary>
    public ProjectFilesData Data { get; set; }
    #endregion
  }
}
