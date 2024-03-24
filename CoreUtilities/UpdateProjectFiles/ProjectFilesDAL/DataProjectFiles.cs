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

    #region Public Methods

    // Create a File Spec.
    public string GetFileSpec(string codeLineName, string codeGroupName
      , string solutionName, string projectName, string filePath
      , string fileName = null)
    {
      string retValue = null;

      if (NetString.HasValue(codeLineName)
        && NetString.HasValue(solutionName))
      {
        // Get CodeLine path.
        retValue = CodeLinePath(codeLineName);

        // Get CodeGroup path.
        if (NetString.HasValue(codeGroupName))
        {
          var codeGroupPath = CodeGroupPath(codeLineName
            , codeGroupName);
          retValue = Path.Combine(retValue, codeGroupPath);
        }

        // Get Solution path.
        var solutionParentKey = new SolutionParentKey()
        {
          CodeLine = codeLineName,
          CodeGroup = codeGroupName
        };
        var solutionPath = SolutionPath(solutionParentKey
          , solutionName);
        retValue = Path.Combine(retValue, solutionPath);

        // Get Project path.
        var projectParentKey = new ProjectParentKey()
        {
          CodeLine = codeLineName,
          CodeGroup = codeGroupName,
          Solution = solutionName
        };
        var projectPath = ProjectPath(projectParentKey
          , projectName);
        retValue = Path.Combine(retValue, projectPath);

        // Add ProjectFile path and file name.
        retValue = Path.Combine(retValue, filePath);
        if (NetString.HasValue(fileName))
        {
          retValue = Path.Combine(retValue, fileName);
        }
      }
      return retValue;
    }

    // Get the ProjectFile valus from a filespec.
    /// <summary>
    /// Get the ProjectFile valus from a filespec.
    /// </summary>
    /// <param name="fileSpec">The file specifiation.</param>
    /// <returns>A ProjectFile ojbect.</returns>
    public ProjectFile GetProjectFileValues(string fileSpec
      , string targetFilePath = null)
    {
      ProjectFile retValue = null;

      if (NetString.HasValue(fileSpec))
      {
        retValue = new ProjectFile();
        retValue.SourceFileName = Path.GetFileName(fileSpec);
        if (!NetString.HasValue(targetFilePath))
        {
          retValue.TargetFilePath = "External";
        }

        var path = Path.GetDirectoryName(fileSpec);
        var folders = path.Split('\\');
        var index = folders.Length - 1;

        retValue.SourceFilePath = folders[index];
        if (0 == string.Compare(folders[index], "debug", true))
        {
          retValue.SourceFilePath = $@"{folders[index]}\{folders[index - 1]}";
          index--;
        }
        index -= 4;

        // Get CodeLine name.
        var codeLinePath = CombineFolders(folders, 0, index
          , out string codeLineName);
        retValue.SourceCodeLine = CodeLineName(codeLinePath, codeLineName);
        index++;

        // Get CodeGroup name.
        var codeGroupPath = folders[index];
        var codeGroup = CodeGroupWithPath(codeLineName, codeGroupPath);
        var codeGroupName = codeGroupPath;
        if (codeGroup != null)
        {
          codeGroupName = codeGroup.Name;
        }
        retValue.SourceCodeGroup = codeGroupName;
        index++;

        // Get Solution name.
        var solutionPath = folders[index];
        var solutionParentKey = SolutionParentKey(codeLineName, codeGroupName);
        var solution = SolutionWithPath(solutionParentKey, solutionPath);
        var solutionName = solutionPath;
        if (solution != null)
        {
          solutionName = solution.Name;
        }
        retValue.SourceSolution = solutionName;
        index++;

        // Get Project name.
        var projectPath = folders[index];
        var projectParentKey = ProjectParentKey(codeLineName, codeGroupName
          , solutionName);
        var project = ProjectWithPath(projectParentKey, projectPath);
        var projectName = projectPath;
        if (project != null)
        {
          projectName = project.Name;
        }
        retValue.SourceProject = projectName;
      }
      return retValue;
    }

    private string CombineFolders(string[] folders, int startIndex
      , int stopIndex, out string lastFolderName)
    {
      string retValue = "";

      lastFolderName = null;
      for (int pathIndex = startIndex; pathIndex <= stopIndex; pathIndex++)
      {
        retValue = Path.Combine(retValue, folders[pathIndex]);
        if (folders[pathIndex].Contains(":"))
        {
          retValue += @"\";
        }
        if (pathIndex == stopIndex)
        {
          lastFolderName = folders[pathIndex];
        }
      }
      return retValue;
    }
    #endregion

    #region Retrieve Object Methods

    // Gets the CodeGroup object with name.
    /// <summary>
    /// Gets the CodeGroup object with name.
    /// </summary>
    /// <param name="codeLineName">The CodeLine name.</param>
    /// <param name="name">The name value.</param>
    /// <returns>The CodeGroup object.</returns>
    public CodeGroup CodeGroup(string codeLineName, string name)
    {
      CodeGroup retValue = null;

      if (NetString.HasValue(name))
      {
        var codeGroups = Data.CodeGroups;
        retValue = codeGroups.LJCRetrieve(codeLineName, name);
      }
      return retValue;
    }

    // Gets the CodeGroup object with path.
    /// <summary>
    /// Gets the CodeGroup object with path.
    /// </summary>
    /// <param name="codeLineName">The CodeLine name.</param>
    /// <param name="path">The path value.</param>
    /// <returns></returns>
    public CodeGroup CodeGroupWithPath(string codeLineName, string path)
    {
      CodeGroup retValue = null;

      if (NetString.HasValue(codeLineName)
        && NetString.HasValue(path))
      {
        var codeGroups = Data.CodeGroups;
        retValue = codeGroups.LJCRetrieveWithPath(codeLineName, path);
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

    // Gets the CodeLine object.
    /// <summary>
    /// Gets the CodeLine object.
    /// </summary>
    /// <param name="path">The path value.</param>
    /// <returns>The CodeLine object.</returns>
    public CodeLine CodeLineWithPath(string path)
    {
      CodeLine retValue = null;

      if (NetString.HasValue(path))
      {
        var codeLines = Data.CodeLines;
        retValue = codeLines.LJCRetrieveWithPath(path);
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

    // Gets the Project object with path.
    /// <summary>
    /// Gets the Project object with path.
    /// </summary>
    /// <param name="parentKey">The ParentKey object.</param>
    /// <param name="path">The path value.</param>
    /// <returns>The Solution object.</returns>
    public Project ProjectWithPath(ProjectParentKey parentKey, string path)
    {
      Project retValue = null;

      if (parentKey != null
        && NetString.HasValue(path))
      {
        var projects = Data.Projects;
        retValue = projects.LJCRetrieveWithPath(parentKey, path);
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

    // Gets the Solution object.
    /// <summary>
    /// Gets the Solution object.
    /// </summary>
    /// <param name="parentKey">The ParentKey object.</param>
    /// <param name="path">The path value.</param>
    /// <returns>The Solution object.</returns>
    public Solution SolutionWithPath(SolutionParentKey parentKey, string path)
    {
      Solution retValue = null;

      if (NetString.HasValue(path))
      {
        var solutions = Data.Solutions;
        retValue = solutions.LJCRetrieveWithPath(parentKey, path);
      }
      return retValue;
    }
    #endregion

    #region Retrieve Object Value Methods

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

    // Gets the CodeLine path value.
    /// <summary>
    /// Gets the CodeLine path value.
    /// </summary>
    /// <param name="path">The name value.</param>
    /// <param name="defautName">The default name value.</param>
    /// <returns>The CodeLine path value.</returns>
    public string CodeLineName(string path, string defautName = null)
    {
      var retValue = defautName;

      var codeLine = CodeLineWithPath(path);
      if (codeLine != null)
      {
        retValue = codeLine.Path;
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

    // Gets the ProjectFile source Path value.
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

    // Gets the Project Name value.
    /// <summary>
    /// Gets the Project Name value.
    /// </summary>
    /// <param name="parentKey">The ParentKey object.</param>
    /// <param name="path">The path value.</param>
    /// <param name="defaultprojectName">The default name value.</param>
    /// <returns>The Project path value.</returns>
    public string ProjectName(ProjectParentKey parentKey, string path
      , string defaultprojectName = null)
    {
      string retValue = defaultprojectName;

      var project = ProjectWithPath(parentKey, path);
      if (project != null)
      {
        retValue = project.Name;
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

    // Gets the Solution Name value.
    /// <summary>
    /// Gets the Solution Name value.
    /// </summary>
    /// <param name="parentKey">The ParentKey object.</param>
    /// <param name="path">The path value.</param>
    /// <returns>The Solution Name value.</returns>
    public string SolutionName(SolutionParentKey parentKey, string path)
    {
      string retValue = null;

      var solution = SolutionWithPath(parentKey, path);
      if (solution != null)
      {
        retValue = solution.Name;
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

    #region Create Parent Keys

    // Create Project parent key
    /// <summary>
    /// Create Project parent key
    /// </summary>
    /// <param name="codeLineName">The CodeLine name.</param>
    /// <param name="codeGroupName">The CodeGroup name.</param>
    /// <param name="solutionName">The Solution name.</param>
    /// <returns></returns>
    public ProjectParentKey ProjectParentKey(string codeLineName
      , string codeGroupName, string solutionName)
    {
      ProjectParentKey retValue = null;

      retValue = new ProjectParentKey()
      {
        CodeLine = codeLineName,
        CodeGroup = codeGroupName,
        Solution = solutionName
      };
      return retValue;
    }

    // Create Solution parent key
    /// <summary>
    /// Create Solution parent key
    /// </summary>
    /// <param name="codeLineName">The CodeLine name.</param>
    /// <param name="codeGroupName">The CodeGroup name.</param>
    /// <returns></returns>
    public SolutionParentKey SolutionParentKey(string codeLineName
      , string codeGroupName)
    {
      SolutionParentKey retValue = null;

      retValue = new SolutionParentKey()
      {
        CodeLine = codeLineName,
        CodeGroup = codeGroupName
      };
      return retValue;
    }
    #endregion

    #region Properties

    /// <summary>Gets or sets the Data object.</summary>
    public ProjectFilesData Data { get; set; }
    #endregion
  }
}
