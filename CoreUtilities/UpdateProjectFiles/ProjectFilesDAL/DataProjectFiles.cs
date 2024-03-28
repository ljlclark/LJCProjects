// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// DataProjectFiles.cs
using LJCNetCommon;
using System.IO;

namespace ProjectFilesDAL
{
  /// <summary>Provides ProjectFile data helper methods.</summary>
  public class DataProjectFiles
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DataProjectFilesC/*' file='Doc/DataProjectFiles.xml'/>
    public DataProjectFiles(ProjectFilesData data)
    {
      Data = data;
    }
    #endregion

    #region Public Methods

    // Create a File Spec from project file data.
    /// <include path='items/GetFileSpec/*' file='Doc/DataProjectFiles.xml'/>
    public string GetFileSpec(string codeLineName, string codeGroupName
      , string solutionName, string projectName, string projectFilePath
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
        var solutionParentKey = SolutionParentKey(codeLineName, codeGroupName);
        var solutionPath = SolutionPath(solutionParentKey
          , solutionName);
        retValue = Path.Combine(retValue, solutionPath);

        // Get Project path.
        var projectParentKey = ProjectParentKey(codeLineName, codeGroupName
          , solutionName);
        var projectPath = ProjectPath(projectParentKey
          , projectName);
        retValue = Path.Combine(retValue, projectPath);

        // Add ProjectFile path and file name.
        retValue = Path.Combine(retValue, projectFilePath);
        if (NetString.HasValue(fileName))
        {
          retValue = Path.Combine(retValue, fileName);
        }
      }
      return retValue;
    }

    // Get the ProjectFile valus from a filespec.
    /// <include path='items/GetProjectFileValues/*' file='Doc/DataProjectFiles.xml'/>
    public ProjectFile GetProjectFileValues(string fileSpec
      , string targetFilePath = null)
    {
      ProjectFile retValue = null;

      if (NetString.HasValue(fileSpec))
      {
        retValue = new ProjectFile();
        retValue.FileName = Path.GetFileName(fileSpec);
        if (!NetString.HasValue(targetFilePath))
        {
          retValue.TargetFilePath = "External";
          // *** Next Statement *** Add - 3/28/24
          retValue.TargetPathProject = null;
        }

        var path = Path.GetDirectoryName(fileSpec);
        var folders = path.Split('\\');
        var index = folders.Length - 1;

        retValue.SourceFilePath = folders[index];
        if (0 == string.Compare(folders[index], "debug", true))
        {
          // *** Next Statement *** Change- 3/28/24
          retValue.SourceFilePath = $@"{folders[index - 1]}\{folders[index]}";
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

    // Combines elements from Folder array.
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
    /// <include path='items/CodeGroup/*' file='Doc/DataProjectFiles.xml'/>
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
    /// <include path='items/CodeGroupWithPath/*' file='Doc/DataProjectFiles.xml'/>
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
    /// <include path='items/CodeLline/*' file='Doc/DataProjectFiles.xml'/>
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
    /// <include path='items/CodeLineWithPath/*' file='Doc/DataProjectFiles.xml'/>
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
    /// <include path='items/Project/*' file='Doc/DataProjectFiles.xml'/>
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
    /// <include path='items/ProjectWithPath/*' file='Doc/DataProjectFiles.xml'/>
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
    /// <include path='items/ProjectFile/*' file='Doc/DataProjectFiles.xml'/>
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
    /// <include path='items/Solution/*' file='Doc/DataProjectFiles.xml'/>
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
    /// <include path='items/SolutionWithPath/*' file='Doc/DataProjectFiles.xml'/>
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
    /// <include path='items/CodeGroupPath/*' file='Doc/DataProjectFiles.xml'/>
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
    /// <include path='items/CodeLineName/*' file='Doc/DataProjectFiles.xml'/>
    public string CodeLineName(string path, string defautName = null)
    {
      var retValue = defautName;

      var codeLine = CodeLineWithPath(path);
      if (codeLine != null)
      {
        retValue = codeLine.Name;
      }
      return retValue;
    }

    // Gets the CodeLine path value.
    /// <include path='items/CodeLinePath/*' file='Doc/DataProjectFiles.xml'/>
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
    /// <include path='items/ProjectFileSourcePath/*' file='Doc/DataProjectFiles.xml'/>
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
    /// <include path='items/ProjectName/*' file='Doc/DataProjectFiles.xml'/>
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
    /// <include path='items/ProjectPath/*' file='Doc/DataProjectFiles.xml'/>
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
    /// <include path='items/SolutionName/*' file='Doc/DataProjectFiles.xml'/>
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
    /// <include path='items/SolutionPath/*' file='Doc/DataProjectFiles.xml'/>
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
    /// <include path='items/ProjectParentKey/*' file='Doc/DataProjectFiles.xml'/>
    public ProjectParentKey ProjectParentKey(string codeLineName
      , string codeGroupName, string solutionName)
    {
      string message = "";
      NetString.AddMissingArgument(message, codeLineName);
      NetString.AddMissingArgument(message, codeGroupName);
      NetString.AddMissingArgument(message, solutionName);
      NetString.ThrowInvalidArgument(message);

      var retValue = new ProjectParentKey()
      {
        CodeLine = codeLineName,
        CodeGroup = codeGroupName,
        Solution = solutionName
      };
      return retValue;
    }

    // Create Solution parent key
    /// <include path='items/SolutionParentKey/*' file='Doc/DataProjectFiles.xml'/>
    public SolutionParentKey SolutionParentKey(string codeLineName
      , string codeGroupName)
    {
      string message = "";
      NetString.AddMissingArgument(message, codeLineName);
      NetString.AddMissingArgument(message, codeGroupName);
      NetString.ThrowInvalidArgument(message);

      var retValue = new SolutionParentKey()
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
