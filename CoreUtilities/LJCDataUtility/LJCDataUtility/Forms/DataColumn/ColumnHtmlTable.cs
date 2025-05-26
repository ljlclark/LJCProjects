// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ColumnHTMLTable.cs
using LJCDataAccess;
using LJCDataAccessConfig;
using LJCDataUtilityDAL;
using LJCDBClientLib;
using LJCDBMessage;
using LJCNetCommon;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace LJCDataUtility
{
  internal class ColumnHTMLTable
  {
    #region Constructors

    // Initializes an object instance.
    internal ColumnHTMLTable(DataUtilityList parentObject)
    {
      // Initialize property values.
      ParentObject = parentObject;
      ParentObject.Cursor = Cursors.WaitCursor;

      // Set Data vars.
      Managers = ParentObject.Managers;
      ColumnManager = Managers.DataColumnManager;
      ParentObject.Cursor = Cursors.Default;
    }
    #endregion

    #region Methods

    // Get DataColumns collection.
    internal DataColumns GetDataColumns()
    {
      DataColumns retColumns;

      var parentID = ParentObject.DataTableRowID(out long parentSiteID);
      var keyColumns = ColumnManager.ParentKey(parentID, parentSiteID);
      var propertyNames = GetColumnPropertyNames();
      var orderByNames = new List<string>()
      {
        DataUtilColumn.ColumnSequence
      };
      var manager = ColumnManager.Manager;
      manager.OrderByNames = orderByNames;
      retColumns = ColumnManager.Load(keyColumns, propertyNames);
      return retColumns;
    }

    // Get DataUtilColumn DataTable.
    internal DataTable GetColumnDataTable()
    {
      DataTable retDataTable;

      var dataConfig = GetSettingsDataConfig(ParentObject.Settings);
      var connectionString = dataConfig.GetConnectionString();
      var providerName = dataConfig.GetProviderName();
      var dataAccess = new DataAccess(connectionString, providerName);

      var parentID = ParentObject.DataTableRowID(out long parentSiteID);
      var keyColumns = ColumnManager.ParentKey(parentID, parentSiteID);
      var propertyNames = GetColumnPropertyNames();
      var orderByNames = new List<string>()
      {
        DataUtilColumn.ColumnSequence
      };
      var manager = ColumnManager.Manager;
      manager.OrderByNames = orderByNames;
      var dataRequest = manager.CreateLoadRequest(keyColumns, propertyNames);
      var sqlBuilder = new DbSqlBuilder(dataRequest);
      var loadSql = sqlBuilder.CreateLoadSql(dataRequest);
      retDataTable = dataAccess.GetDataTable(loadSql);
      return retDataTable;
    }

    // Get DataUtilColumn property names.
    internal List<string> GetColumnPropertyNames()
    {
      var retNames = new List<string>()
      {
        DataUtilColumn.ColumnDataTableID,
        DataUtilColumn.ColumnDataTableSiteID,
        DataUtilColumn.ColumnName,
        DataUtilColumn.ColumnDescription,
        DataUtilColumn.ColumnSequence,
        DataUtilColumn.ColumnTypeName,
        DataUtilColumn.ColumnMaxLength,
        DataUtilColumn.ColumnAllowNull,
        DataUtilColumn.ColumnDefaultValue,
      };
      return retNames;
    }

    // Get DataUtilColumn DbResult.
    internal DbResult GetColumnResult()
    {
      DbResult retResult;

      var parentID = ParentObject.DataTableRowID(out long parentSiteID);
      var keyColumns = ColumnManager.ParentKey(parentID, parentSiteID);
      var propertyNames = GetColumnPropertyNames();
      var orderByNames = new List<string>()
      {
        DataUtilColumn.ColumnSequence
      };
      var manager = ColumnManager.Manager;
      manager.OrderByNames = orderByNames;
      var dataRequest = manager.CreateLoadRequest(keyColumns, propertyNames);
      var sqlBuilder = new DbSqlBuilder(dataRequest);
      var loadSql = sqlBuilder.CreateLoadSql(dataRequest);
      retResult = manager.ExecuteClientSql(RequestType.LoadSQL, loadSql);
      return retResult;
    }
    #endregion

    #region Move to Reusable Code?

    /// <summary>Gets a DataConfig from the DataConfigs.xml file.</summary>
    /// <param name="configName">The DataConfig name.</param>
    /// <returns>The DataConfig object.</returns>
    public DataConfig GetDataConfig(string configName)
    {
      DataConfig retConfig = null;

      if (NetString.HasValue(configName))
      {
        var dataConfigs = new DataConfigs();
        dataConfigs.LJCLoadData();
        retConfig = dataConfigs.LJCGetByName(configName);
      }
      return retConfig;
    }

    /// <summary>Gets a DataConfig name from the settings.</summary>
    /// <param name="settings">The settings object.</param>
    /// <returns>The DataConfig name.</returns>
    public string GetSettingsDataConfigName(StandardUISettings settings)
    {
      string retName = null;

      if (settings != null)
      {
        retName = settings.DataConfigName;
      }
      return retName;
    }

    /// <summary>Gets a DataConfig from the settings.</summary>
    /// <param name="settings">The settings object.</param>
    /// <returns>The DataConfig object.</returns>
    public DataConfig GetSettingsDataConfig(StandardUISettings settings)
    {
      DataConfig retConfig = null;

      var configName = GetSettingsDataConfigName(settings);
      if (configName != null)
      {
        retConfig = GetDataConfig(configName);
      }
      return retConfig;
    }
    #endregion

    #region Properties

    // Gets or sets the parent List reference.
    private DataUtilityList ParentObject { get; set; }

    // Gets or sets the Manager reference.
    private DataColumnManager ColumnManager { get; set; }

    // Gets or sets the Managers reference.
    private ManagersDataUtility Managers { get; set; }
    #endregion
  }
}
