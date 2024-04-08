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
      ArgError = new ArgError("ProjectFilesDAL.DataProjectFiles")
      {
        MethodName = "DataProjectFiles()"
      };
      ArgError.Add(data, "data");
      NetString.ThrowArgError(ArgError.ToString());

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
      ArgError.MethodName = "GetFileSpec";
      ArgError.Add(codeLineName, "codeLineName");
      ArgError.Add(solutionName, "solutionName");
      // *** Next Statement *** Delete - 4/8/24
      //ArgError.Add(projectFilePath, "projectFilePath");
      NetString.ThrowArgError(ArgError.ToString());

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
      var solutionPath = SolutionPath(solutionParentKey, solutionName);
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
      ArgError.MethodName = "ProjectDependencies";
      ArgError.Add(ProjectFilesDAL.ProjectFile.ParentKeyValues(parentKey));
      NetString.ThrowArgError(ArgError.ToString());

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
      ArgError.MethodName = "ProjectFileValues()";
      ArgError.Add(currentSpec, "currentSpec");
      NetString.ThrowArgError(ArgError.ToString());

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
      ArgError.MethodName = "SolutionDependencies()";
      ArgError.Add(ProjectFilesDAL.Project.ParentKeyValues(parentKey));
      NetString.ThrowArgError(ArgError.ToString());

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
      ArgError.MethodName = "SourceFileSpec()";
      ArgError.Add(ProjectFilesDAL.ProjectFile.ItemSourceValues(projectFile));
      NetString.ThrowArgError(ArgError.ToString());

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
      ArgError.MethodName = "TargetFileSpec()";
      ArgError.Add(ProjectFilesDAL.ProjectFile.ItemValues(projectFile));
      NetString.ThrowArgError(ArgError.ToString());

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

      ArgError.MethodName = "CombineFolders()";
      ArgError.Add(folders, "folders");
      ArgError.Add(startIndex, "startIndex");
      ArgError.Add(stopIndex, "stopIndex");
      NetString.ThrowArgError(ArgError.ToString());

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
      ArgError.MethodName = "CodeGroup()";
      ArgError.Add(codeLineName, "codeLineName");
      ArgError.Add(name, "name");
      NetString.ThrowArgError(ArgError.ToString());

      var codeGroups = Data.CodeGroups;
      var retValue = codeGroups.LJCRetrieve(codeLineName, name);
      return retValue;
    }

    // Gets the CodeGroup object with path.
    /// <include path='items/CodeGroupWithPath/*' file='Doc/DataProjectFiles.xml'/>
    public CodeGroup CodeGroupWithPath(string codeLineName, string path)
    {
      ArgError.MethodName = "CodeGroupWithPath()";
      ArgError.Add(codeLineName, "codeLineName");
      ArgError.Add(path, "path");
      NetString.ThrowArgError(ArgError.ToString());

      var codeGroups = Data.CodeGroups;
      var retValue = codeGroups.LJCRetrieveWithPath(codeLineName, path);
      return retValue;
    }

    // Gets the CodeLine object.
    /// <include path='items/CodeLline/*' file='Doc/DataProjectFiles.xml'/>
    public CodeLine CodeLine(string name)
    {
      ArgError.MethodName = "CodeLine()";
      ArgError.Add(name, "name");
      NetString.ThrowArgError(ArgError.ToString());

      var codeLines = Data.CodeLines;
      var retValue = codeLines.LJCRetrieve(name);
      return retValue;
    }

    // Gets the CodeLine object.
    /// <include path='items/CodeLineWithPath/*' file='Doc/DataProjectFiles.xml'/>
    public CodeLine CodeLineWithPath(string path)
    {
      ArgError.MethodName = "CodeLineWithPath()";
      ArgError.Add(path, "path");
      NetString.ThrowArgError(ArgError.ToString());

      var codeLines = Data.CodeLines;
      var retValue = codeLines.LJCRetrieveWithPath(path);
      return retValue;
    }

    // Gets the Project object.
    /// <include path='items/Project/*' file='Doc/DataProjectFiles.xml'/>
    public Project Project(ProjectParentKey parentKey, string name)
    {
      ArgError.MethodName = "Project()";
      ArgError.Add(ProjectFilesDAL.Project.ParentKeyValues(parentKey));
      ArgError.Add(name, "name");
      NetString.ThrowArgError(ArgError.ToString());

      var projects = Data.Projects;
      var retValue = projects.LJCRetrieve(parentKey, name);
      return retValue;
    }

    // Gets the Project object with path.
    /// <include path='items/ProjectWithPath/*' file='Doc/DataProjectFiles.xml'/>
    public Project ProjectWithPath(ProjectParentKey parentKey, string path)
    {
      ArgError.MethodName = "ProjectWithPath()";
      ArgError.Add(ProjectFilesDAL.Project.ParentKeyValues(parentKey));
      ArgError.Add(path, "path");
      NetString.ThrowArgError(ArgError.ToString());

      var projects = Data.Projects;
      var retValue = projects.LJCRetrieveWithPath(parentKey, path);
      return retValue;
    }

    // Gets the ProjectFiles object.
    /// <include path='items/ProjectFile/*' file='Doc/DataProjectFiles.xml'/>
    public ProjectFile ProjectFile(ProjectFileParentKey parentKey
      , string name)
    {
      ArgError.MethodName = "ProjectFile()";
      ArgError.Add(ProjectFilesDAL.ProjectFile.ParentKeyValues(parentKey));
      ArgError.Add(name, "name");
      NetString.ThrowArgError(ArgError.ToString());

      var projectFiles = Data.ProjectFiles;
      var retValue = projectFiles.LJCRetrieve(parentKey, name);
      return retValue;
    }

    // Gets the Project dependencies.
    /// <include path='items/ProjectFiles/*' file='Doc/DataProjectFiles.xml'/>
    public ProjectFiles ProjectFiles(ProjectFileParentKey parentKey)
    {
      ArgError.MethodName = "ProjectFiles()";
      ArgError.Add(ProjectFilesDAL.ProjectFile.ParentKeyValues(parentKey));
      NetString.ThrowArgError(ArgError.ToString());

      var projectFiles = Data.ProjectFiles;
      var retValue = projectFiles.LJCLoad(parentKey);
      return retValue;
    }

    // Gets the Solution object.
    /// <include path='items/Solution/*' file='Doc/DataProjectFiles.xml'/>
    public Solution Solution(SolutionParentKey parentKey, string name)
    {
      ArgError.MethodName = "Solution()";
      ArgError.Add(ProjectFilesDAL.Solution.ParentKeyValues(parentKey));
      ArgError.Add(name, "name");
      NetString.ThrowArgError(ArgError.ToString());

      var solutions = Data.Solutions;
      var retValue = solutions.LJCRetrieve(parentKey, name);
      return retValue;
    }

    // Gets the Solution object.
    /// <include path='items/SolutionWithPath/*' file='Doc/DataProjectFiles.xml'/>
    public Solution SolutionWithPath(SolutionParentKey parentKey, string path)
    {
      ArgError.MethodName = "SolutionWithPath()";
      ArgError.Add(ProjectFilesDAL.Solution.ParentKeyValues(parentKey));
      ArgError.Add(path, "path");
      NetString.ThrowArgError(ArgError.ToString());

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
      // *** Next Statement *** Change - 4/8/24
      string retValue = name;

      ArgError.MethodName = "CodeGroupPath()";
      ArgError.Add(codeLineName, "codeLineName");
      ArgError.Add(name, "name");
      NetString.ThrowArgError(ArgError.ToString());

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

      ArgError.MethodName = "CodeLineName()";
      ArgError.Add(path, "path");
      NetString.ThrowArgError(ArgError.ToString());

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

      ArgError.MethodName = "CodeLinePath()";
      ArgError.Add(name, "name");
      NetString.ThrowArgError(ArgError.ToString());

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

      ArgError.MethodName = "ProjectFileSourcePath()";
      ArgError.Add(name, "name");
      ArgError.Add(ProjectFilesDAL.ProjectFile.ParentKeyValues(parentKey));
      NetString.ThrowArgError(ArgError.ToString());

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

      ArgError.MethodName = "ProjectName()";
      ArgError.Add(path, "path");
      ArgError.Add(ProjectFilesDAL.Project.ParentKeyValues(parentKey));
      NetString.ThrowArgError(ArgError.ToString());

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
      // *** Next Statement *** Change - 4/8/24
      string retValue = name;

      ArgError.MethodName = "ProjectPath()";
      ArgError.Add(name, "name");
      ArgError.Add(ProjectFilesDAL.Project.ParentKeyValues(parentKey));
      NetString.ThrowArgError(ArgError.ToString());

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

      ArgError.MethodName = "SolutionName()";
      ArgError.Add(path, "path");
      ArgError.Add(ProjectFilesDAL.Solution.ParentKeyValues(parentKey));
      NetString.ThrowArgError(ArgError.ToString());

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
      // *** Next Statement *** Change - 4/8/24
      string retValue = name;

      ArgError.MethodName = "SolutionPath()";
      ArgError.Add(name, "name");
      ArgError.Add(ProjectFilesDAL.Solution.ParentKeyValues(parentKey));
      NetString.ThrowArgError(ArgError.ToString());

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
      ArgError.MethodName = "ProjectFileParentnKey()";
      ArgError.Add(project, "project");
      NetString.ThrowArgError(ArgError.ToString());

      var retValue = ProjectFileParentKey(project.CodeLine, project.CodeGroup
        , project.Solution, project.Name);
      return retValue;
    }

    // Create ProjectFile parent key.
    /// <include path='items/ProjectFileParentKey1/*' file='Doc/DataProjectFiles.xml'/>
    public ProjectFileParentKey ProjectFileParentKey(string codeLineName
      , string codeGroupName, string solutionName, string projectName)
    {
      ArgError.MethodName = "ProjectFileParentKey()";
      ArgError.Add(codeLineName, "codeLineName");
      ArgError.Add(codeGroupName, "codeGroupName");
      ArgError.Add(solutionName, "SolutionName");
      ArgError.Add(projectName, "projectName");
      NetString.ThrowArgError(ArgError.ToString());

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
      ArgError.MethodName = "ProjectParentKey()";
      ArgError.Add(solution, "solution");
      NetString.ThrowArgError(ArgError.ToString());

      var retValue = ProjectParentKey(solution.CodeLine, solution.CodeGroup
        , solution.Name);
      return retValue;
    }

    // Create Project parent key
    /// <include path='items/ProjectParentKey1/*' file='Doc/DataProjectFiles.xml'/>
    public ProjectParentKey ProjectParentKey(string codeLineName
      , string codeGroupName, string solutionName)
    {
      ArgError.MethodName = "ProjectParentKey()";
      ArgError.Add(codeLineName, "codeLineName");
      ArgError.Add(codeGroupName, "codeGroupName");
      ArgError.Add(solutionName, "solutionName");
      NetString.ThrowArgError(ArgError.ToString());

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
      ArgError.MethodName = "SolutionParentKey()";
      ArgError.Add(codeGroup, "codeGroup");
      NetString.ThrowArgError(ArgError.ToString());

      var retValue = SolutionParentKey(codeGroup.CodeLine, codeGroup.Name);
      return retValue;
    }

    // Create Solution parent key
    /// <include path='items/SolutionParentKey1/*' file='Doc/DataProjectFiles.xml'/>
    public SolutionParentKey SolutionParentKey(string codeLineName
      , string codeGroupName)
    {
      ArgError.MethodName = "SolutionParentKey()";
      ArgError.Add(codeLineName, "codeLineName");
      ArgError.Add(codeGroupName, "codeGroupName");
      NetString.ThrowArgError(ArgError.ToString());

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

    // Represents Argument errors.
    private ArgError ArgError { get; set; }
    #endregion
  }
}
