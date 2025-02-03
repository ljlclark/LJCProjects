// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ValuesGenDocEdit.cs
using LJCDBClientLib;
using LJCNetCommon;
using System;
using System.IO;

namespace LJCGenDocEdit
{
  // Application config values singleton.
  internal sealed class ValuesGenDocEdit
  {
    #region Constructors

    // Initializes an object instance.
    internal ValuesGenDocEdit(string fileSpec = "LJCGenDocEdit.ValuesGenDocEdit")
    {
      ArgError = new ArgError("LJCGenDocEdit.exe.config");
      Errors = "";
      StandardSettings = new StandardUISettings();
    }

    /// <summary>Configures the settings.</summary>
    /// <param name="fileSpec">The config file name.</param>
    public void SetConfigFile(string fileSpec = "LJCGenDocEdit.exe.config")
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
          StandardSettings.SetProperties(FileSpec);
        }
      }
    }
    #endregion

    #region Properties

    /// <summary>Gets the Error message</summary>
    public string Errors { get; private set; }

    /// <summary>Gets the config FileSpec.</summary>
    public string FileSpec { get; private set; }

    /// <summary>Gets the singleton instance.</summary>
    public static ValuesGenDocEdit Instance
    {
      get { return mInstance; }
    }

    // Gets or sets the StandardSettings value.
    public StandardUISettings StandardSettings { get; private set; }

    // Represents Argument errors.
    private ArgError ArgError { get; set; }
    #endregion

    #region Class Data

    // The singleton instance.
    private static readonly ValuesGenDocEdit mInstance
      = new ValuesGenDocEdit();
    #endregion
  }
}
