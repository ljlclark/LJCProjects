// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// ManagersProjectFiles.cs

using LJCNetCommon;
using System.IO;
using System.Xml.Linq;

namespace ProjectFilesDAL
{
  /// <summary>Gets the Manager objects.</summary>
  public class ManagersProjectFiles
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../LJCGenDoc/Common/Data.xml'/>
    public ManagersProjectFiles(string filePath)
    {
      string message = "";
      string context = ClassContext + "ManagerProjectFiles()";
      NetString.ArgError(ref message, filePath, "filePath", context);
      NetString.ThrowArgError(message);

      FilePath = filePath;
    }
    #endregion

    #region Manager Properties

    /// <summary>Gets the CodeGroupManager object.</summary>
    public CodeGroupManager CodeGroupManager
    {
      get
      {
        if (null == mCodeGroupManager)
        {
          string fileSpec = Path.Combine(FilePath, "CodeGroup.txt");
          CodeGroupManager = new CodeGroupManager(fileSpec);
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

    /// <summary>Gets the CodeLineManager object.</summary>
    public CodeLineManager CodeLineManager
    {
      get
      {
        if (null == mCodeLineManager)
        {
          string fileSpec = Path.Combine(FilePath, "CodeLine.txt"); 
          CodeLineManager = new CodeLineManager(fileSpec);
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

    /// <summary>Gets the ProjectManager object.</summary>
    public ProjectManager ProjectManager
    {
      get
      {
        if (null == mProjectManager)
        {
          string fileSpec = Path.Combine(FilePath, "Project.txt");
          ProjectManager = new ProjectManager(fileSpec);
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
          string fileSpec = Path.Combine(FilePath, "ProjectFile.txt");
          ProjectFileManager = new ProjectFileManager(fileSpec);
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

    /// <summary>Gets the SolutionManager object.</summary>
    public SolutionManager SolutionManager
    {
      get
      {
        if (null == mSolutionManager)
        {
          string fileSpec = Path.Combine(FilePath, "Solution.txt");
          SolutionManager = new SolutionManager(fileSpec);
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
    #endregion

    #region Class Properties
    private string FilePath { get; set; }
    #endregion

    #region Class Data

    private const string ClassContext = "DataProjectFilesDAL.ManagersProjectFiles.";
    #endregion
  }
}
