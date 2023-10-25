// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ValuesGenDocEdit.cs
using LJCDBClientLib;
using LJCNetCommon;
using System.IO;

namespace LJCGenDocEdit
{
  // Application config values singleton.
  internal sealed class ValuesGenDocEdit
  {
    #region Constructors

    // Initializes an object instance.
    internal ValuesGenDocEdit()
    {
      StandardSettings = new StandardUISettings();
      var fileName = "LJCGenDocEdit.exe.config";
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

    /// <summary>Gets or sets the ConfigFile name.</summary>
    public string ConfigFileName { get; private set; }

    // Gets or sets the StandardSettings value.
    public StandardUISettings StandardSettings { get; private set; }

    /// <summary>Gets the singleton instance.</summary>
    public static ValuesGenDocEdit Instance
    {
      get { return mInstance; }
    }
    #endregion

    #region Class Data

    // The singleton instance.
    private static readonly ValuesGenDocEdit mInstance
      = new ValuesGenDocEdit();
    #endregion
  }
}
