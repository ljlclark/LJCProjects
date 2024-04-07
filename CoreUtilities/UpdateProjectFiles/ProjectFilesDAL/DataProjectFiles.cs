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
      string message = "";
      string context = ClassContext + "DataProjectFiles()";
      NetString.ArgError(ref message, data, "data", context);
      NetString.ThrowArgError(message);

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
      string message = "";
      string context = ClassContext + "GetFileSpec()";
      NetString.ArgError(ref message, codeLineName, "codeLineName", context);
      NetString.ArgError(ref message, solutionName, "solutionName");
      NetString.ArgError(ref message, projectFilePath, "projectFilePath");
      NetString.ThrowArgError(message);

      // Get CodeLine path.
      var retValue = CodeLinePath(codeLineName);

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
      return retValue;
    }

    // Clears or Updates the Project dependencies.
    /// <include path='items/ProjectDependencies1/*' file='Doc/DataProjectFiles.xml'/>
    public void ProjectDependencies(ProjectFileParentKey parentKey
      , string action = null)
    {
      string message = "";
      string context = ClassContext + "ProjectDependencies()";
      NetString.ArgError(ref message, parentKey, "parentKey", context);
      ProjectFilesDAL.ProjectFile.ParentKeyValues(ref message, parentKey);
      NetString.ThrowArgError(message);

      var projectFiles = ProjectFiles(parentKey);
      if (NetCommon.HasItems(projectFiles))
      {
        foreach (var projectFile in projectFiles)
        {
          var sourceFileSpec = SourceFileSpec(projectFile);
          var targetFileSpec = TargetFileSpec(projectFile);
          if (NetString.HasValue(sourceFileSpec)
            && NetString.HasValue(targetFileSpec))
          {
            switch (action)
            {
              case "Delete":
                File.Delete(targetFileSpec);
                break;

              default:
                File.Copy(sourceFileSpec, targetFileSpec, true);
                break;
            }
          }
        }
      }
    }

    // Get the ProjectFile valus from a filespec.
    /// <include path='items/GetProjectFileValues/*' file='Doc/DataProjectFiles.xml'/>
    public ProjectFile ProjectFileValues(string currentSpec
      , string targetFilePath = null)
    {
      string message = "";
      string context = ClassContext + "ProjectFileValues()";
      NetString.ArgError(ref message, currentSpec, "currentSpec", context);
      NetString.ThrowArgError(message);

      var retValue = new ProjectFile
      {
        FileName = Path.GetFileName(currentSpec)
      };
      if (!NetString.HasValue(targetFilePath))
      {
        retValue.TargetFilePath = "External";
        retValue.TargetPathProject = null;
      }

      var path = Path.GetDirectoryName(currentSpec);
      var folders = path.Split('\\');
      var index = folders.Length - 1;

      retValue.SourceFilePath = folders[index];
      if (0 == string.Compare(folders[index], "debug", true))
      {
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
      return retValue;
    }

    // Clears or Updates the Solution dependencies.
    /// <include path='items/SolutionDependencies/*' file='Doc/DataProjectFiles.xml'/>
    public void SolutionDependencies(ProjectParentKey parentKey
      , string action = null)
    {
      string message = "";
      string context = ClassContext + "SolutionDependencies()";
      NetString.ArgError(ref message, parentKey, "parentKey", context);
      ProjectFilesDAL.Project.ParentKeyValues(ref message, parentKey);
      NetString.ThrowArgError(message);

      var items = Data.Projects;
      var projects = items.LJCLoad(parentKey);
      if (NetCommon.HasItems(projects))
      {
        foreach (Project project in projects)
        {
          var fileParentKey = ProjectFileParentKey(project);
          ProjectDependencies(fileParentKey, action);
        }
      }
    }

    // Create the ProjectFile Source File Spec.
    /// <include path='items/SourceFileSpec/*' file='Doc/DataProjectFiles.xml'/>
    public string SourceFileSpec(ProjectFile projectFile)
    {
      string message = "";
      string context = ClassContext + "SourceFileSpec()";
      NetString.ArgError(ref message, projectFile, "projectFile", context);
      ProjectFilesDAL.ProjectFile.ItemSourceValues(ref message, projectFile);
      NetString.ThrowArgError(message);

      var retValue = GetFileSpec(projectFile.SourceCodeLine
        , projectFile.SourceCodeGroup, projectFile.SourceSolution
        , projectFile.SourceProject, projectFile.SourceFilePath
        , projectFile.FileName);
      return retValue;
    }

    // Create the ProjectFile Target File Spec.
    /// <include path='items/TargetFileSpec/*' file='Doc/DataProjectFiles.xml'/>
    public string TargetFileSpec(ProjectFile projectFile)
    {
      string message = "";
      string context = ClassContext + "TargetFileSpec()";
      NetString.ArgError(ref message, projectFile, "projectFile", context);
      ProjectFilesDAL.ProjectFile.ItemValues(ref message, projectFile);
      NetString.ThrowArgError(message);

      var codeLineName = projectFile.TargetCodeLine;
      var retValue = CodeLinePath(codeLineName);

      // May not have a Target CodeGroup path.
      CodeGroup codeGroup = null;
      string codeGroupName = null;
      var targetPathCodeGroup = projectFile.TargetPathCodeGroup;
      if (targetPathCodeGroup != null)
      {
        codeGroup = CodeGroup(codeLineName, targetPathCodeGroup);
        codeGroupName = codeGroup.Name;
        retValue = Path.Combine(retValue, codeGroup.Path);
      }

      var solutionParentKey = SolutionParentKey(codeLineName, codeGroupName);
      var targetPathSolution = projectFile.TargetPathSolution;
      var solution = Solution(solutionParentKey, targetPathSolution);
      retValue = Path.Combine(retValue, solution.Path);

      // May not have a Target Project path.
      var targetPathProject = projectFile.TargetPathProject;
      if (NetString.HasValue(targetPathProject))
      {
        var projectParentKey = ProjectParentKey(codeLineName, codeGroup.Name
          , solution.Name);
        var projectPath = ProjectPath(projectParentKey
          , targetPathProject);
        retValue = Path.Combine(retValue, projectPath);
      }

      retValue = Path.Combine(retValue, projectFile.TargetFilePath);
      retValue = Path.Combine(retValue, projectFile.FileName);
      return retValue;
    }

    // Combines elements from Folder array.
    private string CombineFolders(string[] folders, int startIndex
      , int stopIndex, out string lastFolderName)
    {
      string retValue = "";

      string message = "";
      string context = ClassContext + "CombineFolders()";
      NetString.ArgError(ref message, folders, "folders");
      NetString.ArgError(ref message, startIndex, "startIndex", context);
      NetString.ArgError(ref message, stopIndex, "stopIndex");
      NetString.ThrowArgError(message);

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
      string message = "";
      string context = ClassContext + "CodeGroup()";
      NetString.ArgError(ref message, codeLineName, "codeLineName", context);
      NetString.ArgError(ref message, name, "name");
      NetString.ThrowArgError(message);

      var codeGroups = Data.CodeGroups;
      var retValue = codeGroups.LJCRetrieve(codeLineName, name);
      return retValue;
    }

    // Gets the CodeGroup object with path.
    /// <include path='items/CodeGroupWithPath/*' file='Doc/DataProjectFiles.xml'/>
    public CodeGroup CodeGroupWithPath(string codeLineName, string path)
    {
      string message = "";
      string context = ClassContext + "CodeGroupWithPath()";
      NetString.ArgError(ref message, codeLineName, "codeLineName", context);
      NetString.ArgError(ref message, path, "path");
      NetString.ThrowArgError(message);

      var codeGroups = Data.CodeGroups;
      var retValue = codeGroups.LJCRetrieveWithPath(codeLineName, path);
      return retValue;
    }

    // Gets the CodeLine object.
    /// <include path='items/CodeLline/*' file='Doc/DataProjectFiles.xml'/>
    public CodeLine CodeLine(string name)
    {
      string message = "";
      string context = ClassContext + "CodeLines()";
      NetString.ArgError(ref message, name, "name", context);
      NetString.ThrowArgError(message);

      var codeLines = Data.CodeLines;
      var retValue = codeLines.LJCRetrieve(name);
      return retValue;
    }

    // Gets the CodeLine object.
    /// <include path='items/CodeLineWithPath/*' file='Doc/DataProjectFiles.xml'/>
    public CodeLine CodeLineWithPath(string path)
    {
      string message = "";
      string context = ClassContext + "CodeLineWithPath()";
      NetString.ArgError(ref message, path, "path", context);
      NetString.ThrowArgError(message);

      var codeLines = Data.CodeLines;
      var retValue = codeLines.LJCRetrieveWithPath(path);
      return retValue;
    }

    // Gets the Project object.
    /// <include path='items/Project/*' file='Doc/DataProjectFiles.xml'/>
    public Project Project(ProjectParentKey parentKey, string name)
    {
      string message = "";
      string context = ClassContext + "Project()";
      NetString.ArgError(ref message, name, "name", context);
      ProjectFilesDAL.Project.ParentKeyValues(ref message, parentKey);
      NetString.ThrowArgError(message);

      var projects = Data.Projects;
      var retValue = projects.LJCRetrieve(parentKey, name);
      return retValue;
    }

    // Gets the Project object with path.
    /// <include path='items/ProjectWithPath/*' file='Doc/DataProjectFiles.xml'/>
    public Project ProjectWithPath(ProjectParentKey parentKey, string path)
    {
      string message = "";
      string context = ClassContext + "ProjectWithPath()";
      NetString.ArgError(ref message, path, "path", context);
      ProjectFilesDAL.Project.ParentKeyValues(ref message, parentKey);
      NetString.ThrowArgError(message);

      var projects = Data.Projects;
      var retValue = projects.LJCRetrieveWithPath(parentKey, path);
      return retValue;
    }

    // Gets the ProjectFiles object.
    /// <include path='items/ProjectFile/*' file='Doc/DataProjectFiles.xml'/>
    public ProjectFile ProjectFile(ProjectFileParentKey parentKey
      , string name)
    {
      string message = "";
      string context = ClassContext + "ProjectFile()";
      NetString.ArgError(ref message, name, "name", context);
      ProjectFilesDAL.ProjectFile.ParentKeyValues(ref message, parentKey);
      NetString.ThrowArgError(message);

      var projectFiles = Data.ProjectFiles;
      var retValue = projectFiles.LJCRetrieve(parentKey, name);
      return retValue;
    }

    // Gets the Project dependencies.
    /// <include path='items/ProjectFiles/*' file='Doc/DataProjectFiles.xml'/>
    public ProjectFiles ProjectFiles(ProjectFileParentKey parentKey)
    {
      string message = "";
      string context = ClassContext + "ProjectFiles()";
      NetString.ArgError(ref message, parentKey, "parentKey", context); ;
      ProjectFilesDAL.ProjectFile.ParentKeyValues(ref message, parentKey);
      NetString.ThrowArgError(message);

      var projectFiles = Data.ProjectFiles;
      var retValue = projectFiles.LJCLoad(parentKey);
      return retValue;
    }

    // Gets the Solution object.
    /// <include path='items/Solution/*' file='Doc/DataProjectFiles.xml'/>
    public Solution Solution(SolutionParentKey parentKey, string name)
    {
      string message = "";
      string context = ClassContext + "Solution()";
      NetString.ArgError(ref message, name, "name", context);
      ProjectFilesDAL.Solution.ParentKeyValues(ref message, parentKey);
      NetString.ThrowArgError(message);

      var solutions = Data.Solutions;
      var retValue = solutions.LJCRetrieve(parentKey, name);
      return retValue;
    }

    // Gets the Solution object.
    /// <include path='items/SolutionWithPath/*' file='Doc/DataProjectFiles.xml'/>
    public Solution SolutionWithPath(SolutionParentKey parentKey, string path)
    {
      string message = "";
      string context = ClassContext + "SolutionWithPath()";
      NetString.ArgError(ref message, path, "path", context);
      ProjectFilesDAL.Solution.ParentKeyValues(ref message, parentKey);
      NetString.ThrowArgError(message);

      var solutions = Data.Solutions;
      var retValue = solutions.LJCRetrieveWithPath(parentKey, path);
      return retValue;
    }
    #endregion

    #region Retrieve Object Value Methods

    // Gets the CodeGroup path value.
    /// <include path='items/CodeGroupPath/*' file='Doc/DataProjectFiles.xml'/>
    public string CodeGroupPath(string codeLineName, string name)
    {
      string retValue = null;

      string message = "";
      string context = ClassContext + "CodeGroupPath()";
      NetString.ArgError(ref message, codeLineName, "codeLineName", context);
      NetString.ArgError(ref message, name, "name");
      NetString.ThrowArgError(message);

      var codeGroup = CodeGroup(codeLineName, name);
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

      string message = "";
      string context = ClassContext + "CodeLlineName()";
      NetString.ArgError(ref message, path, "path", context);
      NetString.ThrowArgError(message);

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

      string message = "";
      string context = ClassContext + "CodeLinePath()";
      NetString.ArgError(ref message, name, "name", context);
      NetString.ThrowArgError(message);

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

      string message = "";
      string context = ClassContext + "ProjectFileSourcePath()";
      NetString.ArgError(ref message, name, "name", context);
      ProjectFilesDAL.ProjectFile.ParentKeyValues(ref message, parentKey);
      NetString.ThrowArgError(message);

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

      string message = "";
      string context = ClassContext + "ProjectName()";
      NetString.ArgError(ref message, path, "path", context);
      ProjectFilesDAL.Project.ParentKeyValues(ref message, parentKey);
      NetString.ThrowArgError(message);

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

      string message = "";
      string context = ClassContext + "ProjectPath()";
      NetString.ArgError(ref message, name, "name", context);
      ProjectFilesDAL.Project.ParentKeyValues(ref message, parentKey);
      NetString.ThrowArgError(message);

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

      string message = "";
      string context = ClassContext + "SolutionName()";
      NetString.ArgError(ref message, path, "path", context);
      ProjectFilesDAL.Solution.ParentKeyValues(ref message, parentKey);
      NetString.ThrowArgError(message);

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

      string message = "";
      string context = ClassContext + "SolutionPath()";
      NetString.ArgError(ref message, name, "name", context);
      ProjectFilesDAL.Solution.ParentKeyValues(ref message, parentKey);
      NetString.ThrowArgError(message);

      var solution = Solution(parentKey, name);
      if (solution != null)
      {
        retValue = solution.Path;
      }
      return retValue;
    }
    #endregion

    #region Create Parent Keys

    // Create ProjectFile parent key.
    /// <include path='items/ProjectFileParentKey/*' file='Doc/DataProjectFiles.xml'/>
    public ProjectFileParentKey ProjectFileParentKey(Project project)
    {
      string message = "";
      string context = ClassContext + "ProjectFileParentnKey()";
      NetString.ArgError(ref message, project, "project", context);
      NetString.ThrowArgError(message);

      var retValue = ProjectFileParentKey(project.CodeLine, project.CodeGroup
        , project.Solution, project.Name);
      return retValue;
    }

    // Create ProjectFile parent key.
    /// <include path='items/ProjectFileParentKey1/*' file='Doc/DataProjectFiles.xml'/>
    public ProjectFileParentKey ProjectFileParentKey(string codeLineName
      , string codeGroupName, string solutionName, string projectName)
    {
      string message = "";
      string context = ClassContext + "ProjectFileParentKey()";
      NetString.ArgError(ref message, codeLineName, "codeLineName", context);
      NetString.ArgError(ref message, codeGroupName, "codeGroupName");
      NetString.ArgError(ref message, solutionName,"SolutionName");
      NetString.ArgError(ref message, projectName, "projectName");
      NetString.ThrowArgError(message);

      var retValue = new ProjectFileParentKey()
      {
        CodeLine = codeLineName,
        CodeGroup = codeGroupName,
        Solution = solutionName,
        Project = projectName
      };
      return retValue;
    }

    // Create Project parent key
    /// <include path='items/ProjectParentKey/*' file='Doc/DataProjectFiles.xml'/>
    public ProjectParentKey ProjectParentKey(Solution solution)
    {
      string message = "";
      string context = ClassContext + "ProjectParentKey()";
      NetString.ArgError(ref message, solution, "solution", context);
      NetString.ThrowArgError(message);

      var retValue = ProjectParentKey(solution.CodeLine, solution.CodeGroup
        , solution.Name);
      return retValue;
    }

    // Create Project parent key
    /// <include path='items/ProjectParentKey1/*' file='Doc/DataProjectFiles.xml'/>
    public ProjectParentKey ProjectParentKey(string codeLineName
      , string codeGroupName, string solutionName)
    {
      string message = "";
      string context = ClassContext + "ProjectParentKey()";
      NetString.ArgError(ref message, codeLineName, "codeLineName", context);
      NetString.ArgError(ref message, codeGroupName, "codeGroupName");
      NetString.ArgError(ref message, solutionName, "solutionName");
      NetString.ThrowArgError(message);

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
    public SolutionParentKey SolutionParentKey(CodeGroup codeGroup)
    {
      string message = "";
      string context = ClassContext + "SolutionParentKey()";
      NetString.ArgError(ref message, codeGroup, "codeGroup", context);
      NetString.ThrowArgError(message);

      var retValue = SolutionParentKey(codeGroup.CodeLine, codeGroup.Name);
      return retValue;
    }

    // Create Solution parent key
    /// <include path='items/SolutionParentKey1/*' file='Doc/DataProjectFiles.xml'/>
    public SolutionParentKey SolutionParentKey(string codeLineName
      , string codeGroupName)
    {
      string message = "";
      string context = ClassContext + "SolutionParentKey()";
      NetString.ArgError(ref message, codeLineName, "codeLineName", context);
      NetString.ArgError(ref message, codeGroupName, "codeGroupName");
      NetString.ThrowArgError(message);

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

    #region Class Data

    private const string ClassContext = "DataProjectFilesDAL.ProjectFiles.";
    #endregion
  }
}
