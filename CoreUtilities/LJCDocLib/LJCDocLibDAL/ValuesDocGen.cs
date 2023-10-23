// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ValuesDocLib.cs
using LJCDBClientLib;
using LJCNetCommon;

namespace LJCDocLibDAL
{
  /// <summary>The Application values singleton class.</summary>
  public sealed class ValuesDocGen
  {
    #region Constructors

    // Initializes an instance of the object.
    /// <include path='items/DefaultConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public ValuesDocGen()
    {
      StandardSettings = new StandardUISettings();
    }

    /// <summary>
    /// Configures the settings.
    /// </summary>
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
          Managers = new ManagersDocGen();
          Managers.SetDBProperties(StandardSettings.DbServiceRef
            , StandardSettings.DataConfigName);
        }
      }
    }
    #endregion

    #region Properties

    /// <summary>Gets or sets the ConfigFile name.</summary>
    public string ConfigFileName { get; private set; }

    /// <summary>Gets or sets the generated page count.</summary>
    public int GenPageCount { get; set; }

    /// <summary>Gets the singleton instance.</summary>
    public static ValuesDocGen Instance
    {
      get { return mInstance; }
    }

    /// <summary>Gets or sets the Managers class reference.</summary>
    public ManagersDocGen Managers { get; set; }
    #endregion

    #region Class Data

    /// <summary>Initialize Singleton.</summary>
    internal static readonly ValuesDocGen mInstance = new ValuesDocGen();

    // Gets or sets the StandardSettings value.
    internal StandardUISettings StandardSettings { get; set; }
    #endregion
  }
}
