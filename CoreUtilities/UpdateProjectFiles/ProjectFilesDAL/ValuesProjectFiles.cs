// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// ValuesUpdateProjectFiles.cs
using LJCNetCommon;
using System.Drawing;
using System.IO;

namespace ProjectFilesDAL
{
  /// <summary></summary>
  public sealed class ValuesProjectFiles
  {
    #region Constructors

    /// <summary>Initializes an object instance</summary>
    public ValuesProjectFiles()
    {
      Errors = "";
      SetConfigFileSpec("UpdateProjectFiles.exe.config");
    }
    #endregion

    #region Public Methods

    /// <summary>
    /// 
    /// </summary>
    /// <param name="fileSpec"></param>
    public void SetConfigFileSpec(string fileSpec)
    {
      var message = NetString.ArgError(null, fileSpec);
      if (!File.Exists(fileSpec))
      {
        message += $"File {fileSpec} was not found.";
        Errors += message;
      }
      else
      {
        // Update for changed file name.
        fileSpec = fileSpec.Trim();
        if (!NetString.IsEqual(fileSpec, FileSpec))
        {
          FileSpec = fileSpec;
          SetProperties(FileSpec);
        }
      }
    }

    // Sets the Settings properties.
    /// <summary>
    /// 
    /// </summary>
    /// <param name="fileSpec"></param>
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

    /// <summary>Gets the Error message</summary>
    public string Errors { get; private set; }

    /// <summary>Gets the config FileSpec.</summary>
    public string FileSpec { get; private set; }

    /// <summary>The end gradient color.</summary>
    public Color EndColor { get; private set; }

    /// <summary>Gets the singleton instance.</summary>
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
