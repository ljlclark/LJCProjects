﻿// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// ValuesUpdateProjectFiles.cs
using LJCNetCommon;
using System.Drawing;
using System.IO;
using System.Runtime.Remoting.Contexts;

namespace ProjectFilesDAL
{
  /// <summary>Application config values singleton.</summary>
  public sealed class ValuesProjectFiles
  {
    #region Constructors

    /// <summary>Initializes an object instance</summary>
    public ValuesProjectFiles()
    {
      ArgError = new ArgError("ProjectFilesDAL.ValuesProjectFiles");
      Errors = "";
    }
    #endregion

    #region Public Methods

    /// <summary>Configures the settings.</summary>
    /// <param name="fileSpec">The config FileSpec.</param>
    public void SetConfigFile(string fileSpec = "UpdateProjectFiles.exe.config")
    {
      Errors = "";
      if (!File.Exists(fileSpec))
      {
        ArgError.MethodName = "SetConfigFile(fileSpec)";
        var message = ArgError.ToString();
        message += $"File {fileSpec} was not found.\r\n";
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
      var filePath = AppSettings.GetString("DataFilePath");
      Managers = new ManagersProjectFiles(filePath);
      Data = new ProjectFilesData();
    }
    #endregion

    #region Properties

    /// <summary>The begin gradient color.</summary>
    public Color BeginColor { get; private set; }

    /// <summary>Gets the Data class reference.</summary>
    public ProjectFilesData Data { get; private set; }

    /// <summary>The end gradient color.</summary>
    public Color EndColor { get; private set; }

    /// <summary>Gets the Error message</summary>
    public string Errors { get; private set; }

    /// <summary>Gets the config FileSpec.</summary>
    public string FileSpec { get; private set; }

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

    // Represents Argument errors.
    private ArgError ArgError { get; set; }

    // The singleton instance.
    private static readonly ValuesProjectFiles mInstance
      = new ValuesProjectFiles();
    #endregion
  }
}
