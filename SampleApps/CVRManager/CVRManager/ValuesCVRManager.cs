// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ValuesCVRManager.cs
using System;
using LJCNetCommon;
using LJCDBClientLib;
using System.IO;

namespace CVRManager
{
	/// <summary>Application values singleton class.</summary>
	internal sealed class ValuesCVRManager
	{
    #region Constructors

    internal ValuesCVRManager()
    {
      StandardSettings = new StandardUISettings();
      var fileName = "CVRManager.exe.config";
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

        var appSettings = StandardSettings.AppSettings;
        TemperatureUnitCode = appSettings.GetString("TemperatureUnitCode");
        if (null == TemperatureUnitCode
          || "dCdF".IndexOf(TemperatureUnitCode) < 0)
        {
          TemperatureUnitCode = "dF";
        }

        string value = appSettings.GetString("TemperatureHighValue");
        decimal.TryParse(value, out decimal decimalHighValue);

        value = appSettings.GetString("TemperatureLowValue");
        decimal.TryParse(value, out decimal decimalLowValue);

        switch (TemperatureUnitCode)
        {
          case "dC":
            if (decimalHighValue < 37.1m)
            {
              decimalHighValue = 38m;
            }
            TemperatureHighValue = decimalHighValue;

            if (decimalLowValue < 35m)
            {
              decimalLowValue = 35m;
            }
            TemperatureLowValue = decimalLowValue;
            break;

          case "dF":
            if (decimalHighValue < 98.8m)
            {
              decimalHighValue = 100.4m;
            }
            TemperatureHighValue = decimalHighValue;

            if (decimalLowValue < 95m)
            {
              decimalLowValue = 95m;
            }
            TemperatureLowValue = decimalLowValue;
            break;
        }
      }
    }
    #endregion

    #region Properties

    /// <summary>Gets or sets the ConfigFile name.</summary>
    public string ConfigFileName { get; private set; }

    // The singleton instance.
    internal static ValuesCVRManager Instance { get; }
			= new ValuesCVRManager();

		// Gets or sets the StandardUISettings value.
		internal StandardUISettings StandardSettings { get; set; }

		// Gets or sets the Temperature Low value.
		internal decimal TemperatureLowValue { get; set; }

		// Gets or sets the Temperature High value.
		internal decimal TemperatureHighValue { get; set; }

		// Gets or sets the Temperature unit code.
		internal string TemperatureUnitCode { get; set; }
		#endregion
	}
}
