// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// ManagersProjectFiles.cs

namespace ProjectFilesDAL
{
  /// <summary>Gets the Manager objects.</summary>
  public class ManagersProjectFiles
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../LJCGenDoc/Common/Data.xml'/>
    public ManagersProjectFiles()
    {
    }
    #endregion

    #region Properties

    /// <summary>Gets the CodeLineManager object.</summary>
    public CodeLineManager CodeLineManager
    {
      get
      {
        if (null == mCodeLineManager)
        {
          CodeLineManager = new CodeLineManager();
        }
        return mCodeLineManager;
      }

      private set
      {
        if (value != null)
        {
          mCodeLineManager = value;
        }
      }
    }
    private CodeLineManager mCodeLineManager;

    /// <summary>Gets the CodeGroupManager object.</summary>
    public CodeGroupManager CodeGroupManager
    {
      get
      {
        if (null == mCodeGroupManager)
        {
          CodeGroupManager = new CodeGroupManager();
        }
        return mCodeGroupManager;
      }

      private set
      {
        if (value != null)
        {
          mCodeGroupManager = value;
        }
      }
    }
    private CodeGroupManager mCodeGroupManager;

    /// <summary>Gets the SolutionManager object.</summary>
    public SolutionManager SolutionManager
    {
      get
      {
        if (null == mSolutionManager)
        {
          SolutionManager = new SolutionManager();
        }
        return mSolutionManager;
      }

      private set
      {
        if (value != null)
        {
          mSolutionManager = value;
        }
      }
    }
    private SolutionManager mSolutionManager;

    /// <summary>Gets the ProjectManager object.</summary>
    public ProjectManager ProjectManager
    {
      get
      {
        if (null == mProjectManager)
        {
          ProjectManager = new ProjectManager();
        }
        return mProjectManager;
      }

      private set
      {
        if (value != null)
        {
          mProjectManager = value;
        }
      }
    }
    private ProjectManager mProjectManager;

    /// <summary>Gets the ProjectFileManager object.</summary>
    public ProjectFileManager ProjectFileManager
    {
      get
      {
        if (null == mProjectFileManager)
        {
          ProjectFileManager = new ProjectFileManager();
        }
        return mProjectFileManager;
      }

      private set
      {
        if (value != null)
        {
          mProjectFileManager = value;
        }
      }
    }
    private ProjectFileManager mProjectFileManager;
    #endregion
  }
}
