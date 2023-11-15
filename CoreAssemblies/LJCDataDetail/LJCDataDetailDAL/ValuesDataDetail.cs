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
          ConfigFileName = fileName;
          StandardSettings.SetProperties(ConfigFileName);
          var settings = StandardSettings;
          Managers = new DataDetailManagers();
          Managers.SetDBProperties(settings.DbServiceRef
            , settings.DataConfigName);
        }
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
