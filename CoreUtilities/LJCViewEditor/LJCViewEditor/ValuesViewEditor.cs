// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ValuesViewEditor.cs
using LJCDBClientLib;
using LJCDBViewDAL;
using LJCNetCommon;
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
      SetConfigFile("LJCViewEditor.exe.config");
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
        StandardSettings.SetProperties(fileName);

        var settings = StandardSettings;
        Managers = new ManagersDbView();
        Managers.SetDbProperties(settings.DbServiceRef
          , settings.DataConfigName);
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

    /// <summary>Gets or sets the Managers class reference.</summary>
    public ManagersDbView Managers { get; set; }

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
