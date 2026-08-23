// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ValuesDataUtility5.cs
using LJCDataAccessConfig5;
//using LJCDataSiteDAL;
using LJCNetCommon5;

namespace LJCDataUtilityDAL5
{
  // The Application values singleton class.
  /// <include file='../../LJCGenDoc/Common/Data.xml'
  ///  path='members/ValuesDataUtility/*'/>
  public sealed class ValuesDataUtility
  {
    #region Constructors

    // Initializes an instance of the object.
    /// <include file='../../LJCGenDoc/Common/Data.xml'
    ///  path='members/DefaultConstructor/*'/>
    public ValuesDataUtility()
    {
      _ArgError = new LJCArgError("LJCDataUtilityDAL.ValuesDataUtility");
      Errors = "";
      //StandardSettings = new StandardUISettings();
    }

    /// <summary>Configures the settings.</summary>
    /// <param name="fileSpec">The config file name.</param>
    public void SetConfigFile(string fileSpec = "LJCDataUtility.exe.config")
    {
      _ArgError.MethodName = "SetConfigFile(fileSpec)";

      if (!File.Exists(fileSpec))
      {
        var message = $"File {fileSpec} was not found.\r\n";
        _ArgError.Add(message);
        Errors += _ArgError.ToString();
      }
      else
      {
        // Update for changed file name.
        fileSpec = fileSpec.Trim();
        if (!LJC.IsEqual(fileSpec, FileSpec))
        {
          FileSpec = fileSpec;
          //StandardSettings.SetProperties(fileSpec);

          //var settings = StandardSettings;
          //Managers = new ManagersDataUtility(settings.DataConfigName);
          //Managers.SetDBProperties(settings.DbServiceRef
          //  , settings.DataConfigName);
          //SiteManagers = new ManagersDataSite();
          //SiteManagers.SetDBProperties(settings.DbServiceRef
          //  , settings.DataConfigName);
          //DbGroupID = SiteManagers.DbGroupManager.DbID;
          // *** Begin *** Testing
          DataConfigName = "DataUtility";
          Managers = new ManagersDataUtility(DataConfigName);
          DbGroupId = 1;
          // *** End ***

          var dataConfigs = new LJCDataConfigs();
          dataConfigs.LoadData();
          //var dataConfig = dataConfigs.Retrieve(settings.DataConfigName);
          var dataConfig = dataConfigs.Retrieve(DataConfigName);
          if (LJC.HasText(dataConfig.ConnectionType))
          {
            ConnectionType = dataConfig.ConnectionType;
          }
        }
      }
    }
    #endregion

    #region Properties

    /// <summary>Gets or sets the connection type value.</summary>
    public string ConnectionType { get; set; } = null!;

    public string DataConfigName { get; set; } = null!;

    /// <summary>Gets or sets the connection type value.</summary>
    public short DbGroupId { get; set; }

    /// <summary>Gets the Error message</summary>
    public string Errors { get; private set; }

    /// <summary>Gets the config FileSpec.</summary>
    public string FileSpec { get; private set; } = null!;

    /// <summary>Gets or sets the generated page count.</summary>
    public int GenPageCount { get; set; }

    /// <summary>Gets the singleton instance.</summary>
    public static ValuesDataUtility Instance
    {
      get { return mInstance; }
    }

    /// <summary>Gets or sets the Managers class reference.</summary>
    public ManagersDataUtility Managers { get; set; } = null!;

    /// <summary>Gets or sets the SiteManagers class reference.</summary>
    //public ManagersDataSite SiteManagers { get; set; }

    /// <summary>Gets the StandardSettings value.</summary>
    //public LJCStandardUISettings StandardSettings { get; private set; }
    #endregion

    #region Class Data

    // Represents Argument errors.
    private readonly LJCArgError _ArgError;

    // Initialize Singleton.
    private static readonly ValuesDataUtility mInstance
      = new();
    #endregion
  }
}
