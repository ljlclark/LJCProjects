// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// StandardUISettings5.cs
using LJCDataAccess5;
using System.Drawing;

namespace LJCDBClientLib5
{
  // The Standard Setting values.
  /// <include file='Doc/ProjectStandardUISettings.xml'
  ///  path='members/LJCStandardUISettings/*'/>
  public class LJCStandardUISettings
  {
    #region Constructors

    // Initializes an object instance.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'
    ///  path='members/Constructor/*'/>
    public LJCStandardUISettings()
    {
      ConnectionString = "";
      mDataConfigName = "";
      DbServiceRef = new LJCDbServiceRef();
      mExportTextExtension = ".txt";
    }
    #endregion

    #region Public Methods

    // Sets the Settings properties.
    /// <include file='Doc/StandardUISettings.xml'
    ///  path='members/SetProperties/*'/>
#pragma warning disable IDE0060 // Remove unused parameter
    public void SetProperties(string programConfigFileName)
#pragma warning restore IDE0060 // Remove unused parameter
    {
      //AppSettings = new AppSettings(programConfigFileName);

      //DataConfigName = AppSettings.GetString("DataConfigName");
      //DbServiceRef = new LJCDbServiceRef
      //{
      //  DbDataAccess = new DbDataAccess(DataConfigName)
      //};

      //string connectionTypeName = AppSettings.GetString("ConnectionType");
      //ConnectionType = LJCDataCommon.GetConnectionType(connectionTypeName);

      //BeginColor = AppSettings.GetColor("BeginColor", Color.AliceBlue);
      //EndColor = AppSettings.GetColor("EndColor", Color.LightSkyBlue);
      //ExportTextExtension = AppSettings.GetString("ExportTextExtension");
      //if (!LJC.HasValue(ExportTextExtension))
      //{
      //  ExportTextExtension = "txt";
      //}

      //string siteID = AppSettings.GetString("SiteID");
      //long.TryParse(siteID, out long id);
      //SiteID = id;
    }
    #endregion

    #region Properties

    // Gets or sets the AppSettings value.
    // <include file='Doc/StandardUISettings.xml'
    //  path='members/AppSettings/*'/>
    //public AppSettings AppSettings { get; set; }

    // The begin gradient color.
    /// <include file='Doc/StandardUISettings.xml'
    ///  path='members/BeginColor/*'/>
    public Color BeginColor { get; private set; }

    // The connection string.
    /// <include file='Doc/StandardUISettings.xml'
    ///  path='members/ConnectionString/*'/>
    public string ConnectionString { get; private set; }

    // The database connection type.
    /// <include file='Doc/StandardUISettings.xml'
    ///  path='members/ConnectionType/*'/>
    public ConnectionType ConnectionType { get; private set; }

    // Gets or sets the data config name.
    /// <include file='Doc/StandardUISettings.xml'
    ///  path='members/DataConfigName/*'/>
    public string DataConfigName
    {
      get => mDataConfigName;
      set => mDataConfigName = value.Trim();
    }
    private string mDataConfigName;

    // Gets or sets the DbService references.
    /// <include file='Doc/StandardUISettings.xml'
    ///  path='members/DbServiceRef/*'/>
    public LJCDbServiceRef DbServiceRef { get; set; }

    // The end gradient color.
    /// <include file='Doc/StandardUISettings.xml'
    ///  path='members/EndColor/*'/>
    public Color EndColor { get; private set; }

    // Gets or sets the export text file extension.
    /// <include file='Doc/StandardUISettings.xml'
    ///  path='members/ExportTextExtension/*'/>
    public string ExportTextExtension
    {
      get => mExportTextExtension;
      set => mExportTextExtension = value.Trim();
    }
    private string mExportTextExtension;

    // The SiteID.
    /// <include file='Doc/StandardUISettings.xml'
    ///  path='members/SiteID/*'/>
    public long SiteID { get; private set; }
    #endregion
  }
}
