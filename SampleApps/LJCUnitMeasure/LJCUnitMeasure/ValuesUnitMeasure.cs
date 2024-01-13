// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ValuesUnitMeasure.cs
using System;
using System.IO;
using LJCDBClientLib;
using LJCNetCommon;

namespace LJCUnitMeasure
{
	// Application config values singleton.
	internal sealed class ValuesUnitMeasure
	{
		// Initializes an object instance.
		internal ValuesUnitMeasure()
		{

			StandardSettings = new StandardUISettings();
      string fileName = "LJCUnitMeasure.exe.config";
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
    internal static ValuesUnitMeasure Instance { get; } = new ValuesUnitMeasure();

		// Gets or sets the StandardSettings value.
		internal StandardUISettings StandardSettings { get; set; }
		#endregion
	}
}
