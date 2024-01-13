// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ValuesCVRDAL.cs
using System;
using System.IO;
using LJCDBClientLib;
using LJCNetCommon;

namespace CVRDAL
{
  /// <summary>The CVRDAL values.</summary>
  public class ValuesCVRDAL
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*'
    ///   file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
    public ValuesCVRDAL()
    {
      StandardSettings = new StandardUISettings();
      var fileName = "CVRManager.exe.config";
      SetConfigFile(fileName);
    }
    #endregion

    #region Public Methods

    /// <summary>Sets the property values.</summary>
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
        StandardSettings.SetProperties(fileName);

        var settings = StandardSettings;
        Managers = new CVRManagers();
        Managers.SetDBProperties(settings.DbServiceRef, settings.DataConfigName);
      }
    }
    #endregion

    #region Properties

    /// <summary>Gets or sets the ConfigFile name.</summary>
    public string ConfigFileName { get; private set; }

    /// <summary>The singleton instance.</summary>
    public static ValuesCVRDAL Instance { get; }
      = new ValuesCVRDAL();

    /// <summary>Gets the CVRManagers object.</summary>
    public CVRManagers Managers { get; private set; }

    // Gets or sets the StandardSettings value.
    internal StandardUISettings StandardSettings { get; set; }
    #endregion
  }
}
