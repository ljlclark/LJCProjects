// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// StandardUISettings.cs
using LJCDataAccess;
using LJCDBDataAccess;
using LJCNetCommon;
using System.Drawing;

namespace LJCDBClientLib
{
  /// <summary>The Standard Setting values.</summary>
  public class StandardUISettings
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
    public StandardUISettings()
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
      DbServiceRef = new DbServiceRef
      {
        DbDataAccess = new DbDataAccess(DataConfigName)
      };

      string connectionTypeName = AppSettings.GetString("ConnectionType");
      ConnectionType = DataCommon.GetConnectionType(connectionTypeName);

      BeginColor = AppSettings.GetColor("BeginColor", Color.AliceBlue);
      EndColor = AppSettings.GetColor("EndColor", Color.LightSkyBlue);
      ExportTextExtension = AppSettings.GetString("ExportTextExtension");
      if (!NetString.HasValue(ExportTextExtension))
      {
        ExportTextExtension = "txt";
      }

      string siteID = AppSettings.GetString("SiteID");
      long.TryParse(siteID, out long id);
      SiteID = id;
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

    /// <summary>The SiteID.</summary>
    public long SiteID { get; private set; }
    #endregion
  }
}
