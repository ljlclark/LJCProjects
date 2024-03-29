// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ValuesGenDoc.cs
using LJCDBClientLib;
using LJCNetCommon;
using System.IO;

namespace LJCGenDocDAL
{
  /// <summary>The Application values singleton class.</summary>
  public sealed class ValuesGenDoc
  {
    #region Constructors

    // Initializes an instance of the object.
    /// <include path='items/DefaultConstructor/*' file='../../LJCGenDoc/Common/Data.xml'/>
    public ValuesGenDoc()
    {
      StandardSettings = new StandardUISettings();
      var fileName = "LJCGenDoc.exe.config";
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
        if (!NetString.HasValue(ConfigFileName)
          || fileName.Trim().ToLower() != ConfigFileName.ToLower())
        {
          ConfigFileName = fileName.Trim();
          StandardSettings.SetProperties(fileName);

          var settings = StandardSettings;
          Managers = new ManagersGenDoc();
          Managers.SetDBProperties(settings.DbServiceRef
            , settings.DataConfigName);
        }
      }
    }
    #endregion

    #region Properties

    /// <summary>Gets the ConfigFile name.</summary>
    public string ConfigFileName { get; private set; }

    /// <summary>Gets or sets the generated page count.</summary>
    public int GenPageCount { get; set; }

    /// <summary>Gets the singleton instance.</summary>
    public static ValuesGenDoc Instance
    {
      get { return mInstance; }
    }

    /// <summary>Gets or sets the Managers class reference.</summary>
    public ManagersGenDoc Managers { get; set; }

    /// <summary>Gets the StandardSettings value.</summary>
    public StandardUISettings StandardSettings { get; private set; }
    #endregion

    #region Class Data

    /// <summary>Initialize Singleton.</summary>
    private static readonly ValuesGenDoc mInstance
      = new ValuesGenDoc();
    #endregion
  }
}
