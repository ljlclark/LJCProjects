// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ValuesTransformManager.cs
using System;
using System.Drawing;
using System.IO;
using LJCDBClientLib;
using LJCNetCommon;

namespace LJCTransformManager
{
	/// <summary>Application values singleton class.</summary>
	internal sealed class ValuesTransformManager
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		internal ValuesTransformManager()
		{
			StandardSettings = new StandardUISettings();
      var fileName = "LJCTransformManager.exe.Config";
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
    internal static ValuesTransformManager Instance { get; }
			= new ValuesTransformManager();

		/// <summary>The end gradient color.</summary>
		public Color SelectColor { get; private set; }

		// Gets or sets the StandardSettings value.
		internal StandardUISettings StandardSettings { get; set; }
		#endregion
	}
}
