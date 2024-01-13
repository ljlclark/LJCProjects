// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ValuesGenDocEdit.cs
using LJCDBClientLib;
using LJCNetCommon;
using System.IO;

namespace LJCGenDocEdit
{
  // Application config values singleton.
  internal sealed class ValuesGenDocEdit
  {
    #region Constructors

    // Initializes an object instance.
    internal ValuesGenDocEdit()
    {
      StandardSettings = new StandardUISettings();
      var fileName = "LJCGenDocEdit.exe.config";
      SetConfigFile(fileName);
    }

    /// <summary>Configures the settings.</summary>
    /// <param name="fileName">The config file name.</param>
    public void SetConfigFile(string fileName)
    {
      bool success = true;
      if (!NetString.HasValue(fileName))
      {
        // Do not continue if no fileName.
        success = false;
      }

      if (success)
      {
        fileName = fileName.Trim();
        if (NetString.HasValue(ConfigFileName)
          && !NetString.IsEqual(fileName, ConfigFileName))
        {
          // Do not continue if fileName equals ConfigFileName.
          success = false;
        }
      }

      if (success
        && File.Exists(fileName))
      {
        // Process if changed fileName exists.
        ConfigFileName = fileName.Trim();
        StandardSettings.SetProperties(fileName);
      }
    }
    #endregion

    #region Properties

    /// <summary>Gets or sets the ConfigFile name.</summary>
    public string ConfigFileName { get; private set; }

    /// <summary>Gets the singleton instance.</summary>
    public static ValuesGenDocEdit Instance
    {
      get { return mInstance; }
    }

    // Gets or sets the StandardSettings value.
    public StandardUISettings StandardSettings { get; private set; }
    #endregion

    #region Class Data

    // The singleton instance.
    private static readonly ValuesGenDocEdit mInstance
      = new ValuesGenDocEdit();
    #endregion
  }
}
