// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ValuesDataUtility.cs
using LJCDataAccessConfig;
using LJCDataSiteDAL;
using LJCDataUtilityDAL;
using LJCDBClientLib;
using LJCNetCommon;
using System.IO;

namespace LJCDataUtilityDAL
{
  /// <summary>The Application values singleton class.</summary>
  public sealed class ValuesDataUtility
  {
    #region Constructors

    // Initializes an instance of the object.
    /// <include path='items/DefaultConstructor/*' file='../../LJCGenDoc/Common/Data.xml'/>
    public ValuesDataUtility()
    {
      mArgError = new ArgError("LJCDataUtilityDAL.ValuesDataUtility");
      Errors = "";
      StandardSettings = new StandardUISettings();
    }

    /// <summary>Configures the settings.</summary>
    /// <param name="fileSpec">The config file name.</param>
    public void SetConfigFile(string fileSpec = "LJCDataUtility.exe.config")
    {
      mArgError.MethodName = "SetConfigFile(fileSpec)";
      if (!File.Exists(fileSpec))
      {
        var message = $"File {fileSpec} was not found.\r\n";
        mArgError.Add(message);
        Errors += mArgError.ToString();
      }
      else
      {
        // Update for changed file name.
        fileSpec = fileSpec.Trim();
        if (!NetString.IsEqual(fileSpec, FileSpec))
        {
          FileSpec = fileSpec;
          StandardSettings.SetProperties(fileSpec);

          var settings = StandardSettings;
          Managers = new ManagersDataUtility();
          Managers.SetDBProperties(settings.DbServiceRef
            , settings.DataConfigName);
          SiteManagers = new ManagersDataSite();
          SiteManagers.SetDBProperties(settings.DbServiceRef
            , settings.DataConfigName);

          // *** Begin *** Add 1/29/25
          var dataConfigs = new DataConfigs();
          dataConfigs.LJCLoadData();
          var dataConfig = dataConfigs.LJCGetByName(settings.DataConfigName);
          ConnectionType = dataConfig.ConnectionType;
          // *** End   *** Add 1/29/25
        }
      }
    }
    #endregion

    #region Properties

    /// <summary>Gets or sets the connection type value.</summary>
    public string ConnectionType { get; set; }

    /// <summary>Gets the Error message</summary>
    public string Errors { get; private set; }

    /// <summary>Gets the config FileSpec.</summary>
    public string FileSpec { get; private set; }

    /// <summary>Gets or sets the generated page count.</summary>
    public int GenPageCount { get; set; }

    /// <summary>Gets the singleton instance.</summary>
    public static ValuesDataUtility Instance
    {
      get { return mInstance; }
    }

    /// <summary>Gets or sets the Managers class reference.</summary>
    public ManagersDataUtility Managers { get; set; }

    /// <summary>Gets or sets the SiteManagers class reference.</summary>
    public ManagersDataSite SiteManagers { get; set; }

    /// <summary>Gets the StandardSettings value.</summary>
    public StandardUISettings StandardSettings { get; private set; }
    #endregion

    #region Class Data

    // Represents Argument errors.
    private readonly ArgError mArgError;

    // Initialize Singleton.
    private static readonly ValuesDataUtility mInstance
      = new ValuesDataUtility();
    #endregion
  }
}
