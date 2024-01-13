// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ValuesDataHelper.cs
using LJCDBClientLib;
using LJCNetCommon;
using System.IO;

namespace DataHelper
{
  /// <summary>
  /// 
  /// </summary>
  public sealed class ValuesDataHelper
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../LJCGenDoc/Common/Data.xml'/>
    public ValuesDataHelper()
    {
      StandardSettings = new StandardUISettings();
      var fileName = "DataHelper.exe.config";
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
        ConfigFileName = fileName.Trim();
          StandardSettings.SetProperties(fileName);
      }
    }
    #endregion

    #region Properties

    /// <summary>Gets the ConfigFile name.</summary>
    public string ConfigFileName { get; private set; }

    /// <summary>Gets the StandardSettings value.</summary>
    public StandardUISettings StandardSettings { get; private set; }

    /// <summary>Gets the singleton instance.</summary>
    public static ValuesDataHelper Instance
    {
      get { return mInstance; }
    }
    #endregion

    #region Class Data

    // The singleton instance.
    private static readonly ValuesDataHelper mInstance
      = new ValuesDataHelper();
    #endregion
  }
}
