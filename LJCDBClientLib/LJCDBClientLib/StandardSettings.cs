// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// StandardSettings.cs
using LJCDataAccess;
using LJCDBDataAccess;
using LJCDBServiceLib;
using LJCNetCommon;
using System.Drawing;

namespace LJCDBClientLib
{
  /// <summary>The Standard Setting values.</summary>
  public class StandardSettings
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public StandardSettings()
    {
    }
    #endregion

    #region Public Methods

    /// <summary>
    /// Sets the Settings properties.
    /// </summary>
    /// <param name="programConfigFileName">The Program ConfigFile name.</param>
    public void SetProperties(string programConfigFileName)
    {
      AppSettings = new AppSettings(programConfigFileName);

      DataConfigName = AppSettings.GetString("DataConfigName");
      DbServiceRef = new DbServiceRef();

      LocalDbDataAccess = AppSettings.GetBool("LocalDbDataAccess");
      if (LocalDbDataAccess)
      {
        LocalDbService = false;
        DbServiceRef.DbDataAccess = new DbDataAccess(DataConfigName);
      }
      else
      {
        LocalDbService = AppSettings.GetBool("LocalDbService");
        if (LocalDbService)
        {
          DbServiceRef.DbService = new DbService();
        }
        else
        {
          string endPointConfigurationName = "NetTcpBinding_IDbService";
          DbServiceRef.DbServiceClient = new DbServiceClient(endPointConfigurationName);
        }
      }

      //StringBuilder builder = new StringBuilder(64);
      string connectionTypeName = AppSettings.GetString("ConnectionType");
      ConnectionType = DataCommon.GetConnectionType(connectionTypeName);

      BeginColor = AppSettings.GetColor("BeginColor", Color.AliceBlue);
      EndColor = AppSettings.GetColor("EndColor", Color.LightSkyBlue);
      ExportTextExtension = AppSettings.GetString("ExportTextExtension");
      if (false == NetString.HasValue(ExportTextExtension))
      {
        ExportTextExtension = "txt";
      }
    }
    #endregion

    #region Properties

    /// <summary>Gets or sets the AppSettings value..</summary>
    public AppSettings AppSettings { get; set; }

    /// <summary>The begin gradient color.</summary>
    public Color BeginColor { get; private set; }

    /// <summary>The connection string.</summary>
    public string ConnectionString { get; private set; }

    /// <summary>The database connection type.</summary>
    public ConnectionType ConnectionType { get; private set; }

    /// <summary>Gets or sets the data config name.</summary>
    public string DataConfigName
    {
      get { return mDataConfigName; }
      set { mDataConfigName = NetString.InitString(value); }
    }
    private string mDataConfigName;

    /// <summary>Gets or sets the DbService references.</summary>
    public DbServiceRef DbServiceRef { get; set; }

    /// <summary>The end gradient color.</summary>
    public Color EndColor { get; private set; }

    /// <summary>Gets or sets the export text file extension.</summary>
    public string ExportTextExtension
    {
      get { return mExportTextExtension; }
      set { mExportTextExtension = NetString.InitString(value); }
    }
    private string mExportTextExtension;

    /// <summary>Gets or sets the LocalDbServer flag.</summary>
    public bool LocalDbDataAccess { get; set; }

    /// <summary>Gets or sets the LocalDbService flag.</summary>
    public bool LocalDbService { get; set; }
    #endregion
  }
}
