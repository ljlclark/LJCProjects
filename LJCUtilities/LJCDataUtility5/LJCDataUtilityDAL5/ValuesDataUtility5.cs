// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ValuesDataUtility5.cs
using LJCDataAccessConfig5;
//using LJCDataSiteDAL;
using LJCNetCommon5;

namespace LJCDataUtilityDAL5
{
  // The Application values singleton class.
  /// <include file='../../LJCGenDoc5/Common/Data.xml'
  ///  path='members/ValuesDataUtility/*'/>
  public sealed class ValuesDataUtility
  {
    #region Constructors

    // Initializes an instance of the object.
    /// <include file='../../LJCGenDoc5/Common/Data.xml'
    ///  path='members/DefaultConstructor/*'/>
    public ValuesDataUtility()
    {
      _ArgError = new LJCArgError("LJCDataUtilityDAL.ValuesDataUtility");
      Errors = "";
      //StandardSettings = new StandardUISettings();
    }

    // Configures the settings.
    /// <include file='../../LJCGenDoc5/Common/Data.xml'
    ///  path='members/SetConfigFile/*'/>
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

    // Gets or sets the connection type value.
    /// <include file='Doc/ValuesDataUtility.xml'
    ///  path='members/ConnectionType/*'/>
    public string ConnectionType { get; set; } = null!;

    /// <include file='Doc/ValuesDataUtility.xml'
    ///  path='members/DataConfigName/*'/>
    public string DataConfigName { get; set; } = null!;

    // Gets or sets the connection type value.
    /// <include file='Doc/ValuesDataUtility.xml'
    ///  path='members/DbGroupId/*'/>
    public short DbGroupId { get; set; }

    // Gets the Error message
    /// <include file='Doc/ValuesDataUtility.xml'
    ///  path='members/Errors/*'/>
    public string Errors { get; private set; }

    // Gets the config FileSpec.
    /// <include file='Doc/ValuesDataUtility.xml'
    ///  path='members/FileSpec/*'/>
    public string FileSpec { get; private set; } = null!;

    // Gets or sets the generated page count.
    /// <include file='Doc/ValuesDataUtility.xml'
    ///  path='members/GenPageCount/*'/>
    public int GenPageCount { get; set; }

    // Gets the singleton instance.
    /// <include file='Doc/ValuesDataUtility.xml'
    ///  path='members/ValuesDataUtility/*'/>
    public static ValuesDataUtility Instance
    {
      get { return mInstance; }
    }

    // Gets or sets the Managers class reference.
    /// <include file='Doc/ValuesDataUtility.xml'
    ///  path='members/Managers/*'/>
    public ManagersDataUtility Managers { get; set; } = null!;

    // Gets or sets the SiteManagers class reference.
    // <include file='Doc/ValuesDataUtility.xml'
    //  path='members/SiteManagers/*'/>
    //public ManagersDataSite SiteManagers { get; set; }

    // Gets the StandardSettings value.
    // <include file='Doc/ValuesDataUtility.xml'
    //  path='members/StandardSettings/*'/>
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
