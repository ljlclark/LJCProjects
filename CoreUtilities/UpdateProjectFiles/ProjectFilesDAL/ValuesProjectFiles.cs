// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// ValuesUpdateProjectFiles.cs
using LJCNetCommon;
using System.Drawing;
using System.IO;

namespace ProjectFilesDAL
{
  public sealed class ValuesProjectFiles
  {
    #region Constructors

    // Initializes an object instance.
    public ValuesProjectFiles()
    {
      SetConfigFileSpec("UpdateProjectFiles.exe.config");
    }
    #endregion

    #region Public Methods

    public void SetConfigFileSpec(string fileSpec)
    {
      bool success = true;
      if (!NetString.HasValue(fileSpec))
      {
        // Do not continue if no fileSpec.
        success = false;
      }

      if (success)
      {
        fileSpec = fileSpec.Trim();
        if (NetString.HasValue(FileSpec)
          && !NetString.IsEqual(fileSpec, FileSpec))
        {
          // Do not continue if fileSpec equals FileSpec.
          success = false;
        }
      }

      if (success
        && File.Exists(fileSpec))
      {
        // Process if changed fileName exists.
        FileSpec = fileSpec;
        SetProperties(FileSpec);
      }
    }
    #endregion

    #region Private Methods

    // Sets the Settings properties.
    public void SetProperties(string fileSpec)
    {
      AppSettings = new AppSettings(fileSpec);
      BeginColor = AppSettings.GetColor("BeginColor", Color.AliceBlue);
      EndColor = AppSettings.GetColor("EndColor", Color.LightSkyBlue);
      Managers = new ManagersProjectFiles();
      Data = new ProjectFilesData();
    }
    #endregion

    #region Properties

    /// <summary>The begin gradient color.</summary>
    public Color BeginColor { get; private set; }

    /// <summary>Gets the Data class reference.</summary>
    public ProjectFilesData Data { get; private set; }

    /// <summary>Gets the config FileSpec.</summary>
    public string FileSpec { get; private set; }

    /// <summary>The end gradient color.</summary>
    public Color EndColor { get; private set; }

    // Gets the singleton instance.
    public static ValuesProjectFiles Instance
    {
      get { return mInstance; }
    }

    /// <summary>Gets the Managers class reference.</summary>
    public ManagersProjectFiles Managers { get; private set; }

    // Gets or sets the AppSettings value.
    internal AppSettings AppSettings { get; private set; }
    #endregion

    #region Class Data

    // The singleton instance.
    private static readonly ValuesProjectFiles mInstance
      = new ValuesProjectFiles();
    #endregion
  }
}
