// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ValuesRegion.cs
using LJCDBClientLib;
using LJCNetCommon;
using System;
using System.IO;

namespace LJCRegionManager
{
  // Application config values singleton.
  internal sealed class ValuesRegion
  {
    // Initializes an object instance.
    internal ValuesRegion()
    {
      StandardSettings = new StandardUISettings();
      var fileName = "LJCRegionManager.exe.config";
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
        ConfigFileName = fileName;
        StandardSettings.SetProperties(ConfigFileName);
      }
    }

    #region Properties

    /// <summary>Gets or sets the ConfigFile name.</summary>
    public string ConfigFileName { get; private set; }

    // The singleton instance.
    internal static ValuesRegion Instance { get; } = new ValuesRegion();

    // Gets or sets the StandardSettings value.
    internal StandardUISettings StandardSettings { get; set; }
    #endregion
  }
}
