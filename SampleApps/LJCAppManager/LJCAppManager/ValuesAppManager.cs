// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ValuesAppManager.cs
using System;
using LJCNetCommon;
using LJCDBClientLib;
using System.IO;

namespace LJCAppManager
{
	/// <summary>Application values singleton class.</summary>
	public sealed class ValuesAppManager
	{
		#region Constructors

		// Initializes an instance of the object.
		/// <include path = 'items/DefaultConstructor/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public ValuesAppManager()
		{
			StandardSettings = new StandardUISettings();
      var fileName = "LJCAppManager.exe.config";
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
    #endregion

    #region Properties

    /// <summary>Gets or sets the ConfigFile name.</summary>
    public string ConfigFileName { get; private set; }

    // The singleton instance.
    internal static ValuesAppManager Instance { get; } = new ValuesAppManager();

		// Gets or sets the StandardUISettings value.
		internal StandardUISettings StandardSettings { get; set; }

		// The administrator indicator.
		internal bool SignonIsAdministrator { get; private set; }

		// The signon ID value.
		internal int SignonID { get; set; }
		#endregion
	}
}
