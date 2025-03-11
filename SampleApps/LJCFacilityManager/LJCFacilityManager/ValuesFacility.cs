// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ValuesFacility.cs
using LJCDBClientLib;
using LJCNetCommon;
using System.IO;

namespace LJCFacilityManager
{
	/// <summary>Application values singleton class.</summary>
	public sealed class ValuesFacility
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public ValuesFacility()
		{
			StandardSettings = new StandardUISettings();
      var fileName = "LJCFacilityManager.exe.config";
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
    internal static ValuesFacility Instance { get; } = new ValuesFacility();

		// Gets or sets the StandardUISettings value.
		internal StandardUISettings StandardSettings { get; set; }

		// Indicates if the signed on user is the administrator.
		internal bool SignonIsAdministrator { get; private set; }

		// The signed on user windows ID.
		internal int SignonID { get; set; }
		#endregion
	}
}
