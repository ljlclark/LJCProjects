// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ValuesViewEditor.cs
using LJCDBClientLib;
using LJCNetCommon;
using System;
using System.IO;

namespace LJCViewEditor
{
	//  Application config values singleton.
	internal sealed class ValuesViewEditor
	{
		#region Constructors

		// Initializes an object instance.
		internal ValuesViewEditor()
		{
			StandardSettings = new StandardUISettings();
      var fileName = "LJCViewEditor.exe.config";
      if (File.Exists(fileName))
      {
        SetConfigFile(fileName);
      }
    }

    /// <summary>Configures the settings.</summary>
    /// <param name="fileName">The config file name.</param>
    public void SetConfigFile(string fileName)
    {
      if (NetString.HasValue(fileName))
      {
        // No config file set or new file name.
        if (false == NetString.HasValue(ConfigFileName)
          || fileName.Trim().ToLower() != ConfigFileName.ToLower())
        {
          ConfigFileName = fileName.Trim();
          StandardSettings.SetProperties(fileName);
        }
      }
    }
    #endregion

    #region Properties

    /// <summary>Gets the ConfigFile name.</summary>
    public string ConfigFileName { get; private set; }

    /// <summary>Gets the singleton instance.</summary>
    public static ValuesViewEditor Instance
    {
      get { return mInstance; }
    }

    // Gets or sets the StandardSettings value.
    public StandardUISettings StandardSettings { get; private set; }
    #endregion

    #region Class Data

    /// <summary>Initialize Singleton.</summary>
    private static readonly ValuesViewEditor mInstance
      = new ValuesViewEditor();
    #endregion
  }
}
