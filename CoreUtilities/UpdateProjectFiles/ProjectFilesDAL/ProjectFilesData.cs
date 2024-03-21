// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// Data.cs

namespace ProjectFilesDAL
{
  /// <summary>Gets the Data collections.</summary>
  public class ProjectFilesData
  {
    /// <summary>Gets the CodeGroups collection.</summary>
    public CodeGroups CodeGroups
    {
      get
      {
        if (null == mCodeGroups)
        {
          SetManagers();
          var codeGroupManager = mManagers.CodeGroupManager;
          CodeGroups = codeGroupManager.Load();
        }
        return mCodeGroups;
      }

      private set
      {
        if (value != null)
        {
          mCodeGroups = value;
        }
      }
    }
    private CodeGroups mCodeGroups;

    /// <summary>Gets the CodeLines collection.</summary>
    public CodeLines CodeLines
    {
      get
      {
        if (null == mCodeLines)
        {
          SetManagers();
          var codeLineManager = mManagers.CodeLineManager;
          CodeLines = codeLineManager.Load();
        }
        return mCodeLines;
      }

      private set
      {
        if (value != null)
        {
          mCodeLines = value;
        }
      }
    }
    private CodeLines mCodeLines;

    /// <summary>Gets the Projects collection.</summary>
    public Projects Projects
    {
      get
      {
        if (null == mProjects)
        {
          SetManagers();
          var projectManager = mManagers.ProjectManager;
          Projects = projectManager.Load();
        }
        return mProjects;
      }

      private set
      {
        if (value != null)
        {
          mProjects = value;
        }
      }
    }
    private Projects mProjects;

    /// <summary>Gets the ProjectFiles collection.</summary>
    public ProjectFiles ProjectFiles
    {
      get
      {
        if (null == mProjectFiles)
        {
          SetManagers();
          var projectFileManager = mManagers.ProjectFileManager;
          ProjectFiles = projectFileManager.Load();
        }
        return mProjectFiles;
      }

      private set
      {
        if (value != null)
        {
          mProjectFiles = value;
        }
      }
    }
    private ProjectFiles mProjectFiles;

    /// <summary>Gets the Solutions collection.</summary>
    public Solutions Solutions
    {
      get
      {
        if (null == mSolutions)
        {
          SetManagers();
          var solutionManager = mManagers.SolutionManager;
          Solutions = solutionManager.Load();
        }
        return mSolutions;
      }

      private set
      {
        if (value != null)
        {
          mSolutions = value;
        }
      }
    }
    private Solutions mSolutions;

    // Sets the Managers object.
    private void SetManagers()
    {
      if (null == mManagers)
      {
        var values = ValuesUpdateProjectFiles.Instance;
        mManagers = values.Managers;
      }
    }
    private ManagersProjectFiles mManagers;
  }
}