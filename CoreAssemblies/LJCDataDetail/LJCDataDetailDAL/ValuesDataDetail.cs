// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ValuesDataDetail.cs
using LJCDBClientLib;
using LJCNetCommon;
using System.IO;

namespace LJCDataDetailDAL
{
  /// <summary>Application config values singleton.</summary>
  public sealed class ValuesDataDetail
  {
    #region Constructors

    /// <summary>Initializes an object instance.</summary>
    public ValuesDataDetail()
    {
      StandardSettings = new StandardUISettings();
      var fileName = "LJCDataDetail.exe.config";
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

        var settings = StandardSettings;
        Managers = new DataDetailManagers();
        Managers.SetDBProperties(settings.DbServiceRef
          , settings.DataConfigName);
      }
    }
    #endregion

    #region Properties

    /// <summary>Gets or sets the ConfigFile name.</summary>
    public string ConfigFileName { get; private set; }

    /// <summary>Gets the singleton instance.</summary>
    public static ValuesDataDetail Instance
    {
      get { return mInstance; }
    }

    /// <summary>Gets or sets the Managers class reference.</summary>
    public DataDetailManagers Managers { get; private set; }

    /// <summary>Gets or sets the StandardSettings value.</summary>
    public StandardUISettings StandardSettings { get; private set; }
    #endregion

    #region Class Data

    // The singleton instance.
    private static readonly ValuesDataDetail mInstance
      = new ValuesDataDetail();
    #endregion
  }
}
