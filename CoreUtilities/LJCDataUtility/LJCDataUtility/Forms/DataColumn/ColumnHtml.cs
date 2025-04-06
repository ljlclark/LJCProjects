﻿// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ColumnHTML.cs

using LJCDataAccess;
using LJCDataAccessConfig;
using LJCDataUtilityDAL;
using LJCDBMessage;
using LJCNetCommon;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace LJCDataUtility
{
  /// <summary>Custom DataColumn HTML methods.</summary>
  internal class ColumnHTML
  {
    #region Constructors
    internal ColumnHTML(DataUtilityList parentObject
      , DataColumnManager columnManager, string fileName)
    {
      // Initialize property values.
      ParentObject = parentObject;
      ParentObject.Cursor = Cursors.WaitCursor;
      ColumnManager = columnManager;
      FileName = fileName;
      ParentObject.Cursor = Cursors.Default;
    }
    #endregion

    #region HTML Methods

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    internal string DataTableHTML()
    {
      var hb = new HTMLBuilder();
      hb.Text(GetBegin());

      var dataTable = GetDataTable();
      hb.Text(HTMLData.DataTableHTML(dataTable));
      hb.Text(hb.GetHTMLEnd());
      var retValue = hb.ToString();
      return retValue;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dataType"></param>
    /// <returns></returns>
    internal string GetHTML(string dataType)
    {
      var hb = new HTMLBuilder();
      hb.Text(GetBegin());

      switch (dataType.ToLower())
      {
        case "dataobject":
          break;

        case "datatable":
          hb.Text(DataTableHTML());
          break;

        case "dbresult":
          hb.Text(ResultHTML());
          break;
      }
      var retValue = hb.ToString();
      return retValue;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    internal string ResultHTML()
    {
      var hb = new HTMLBuilder();
      hb.Text(GetBegin());

      var dbResult = GetResult();
      hb.Text(HTMLData.ResultHTML(dbResult));
      hb.Text(hb.GetHTMLEnd());
      var retValue = hb.ToString();
      return retValue;
    }
    #endregion

    #region Data Methods

    internal DataTable GetDataTable()
    {
      SetConfigData();
      var dataAccess = new DataAccess(ConnectionString, ProviderName);
      var loadSQL = GetLoadSQL();
      var retTable = dataAccess.GetDataTable(loadSQL);
      return retTable;
    }

    internal DbResult GetResult()
    {
      var manager = ColumnManager.Manager;
      var loadSQL = GetLoadSQL();
      var retResult = manager.ExecuteClientSql(RequestType.LoadSQL, loadSQL);
      return retResult;
    }
    #endregion

    #region Private Methods

    // Gets beginning of HTML including <body> tag.
    private string GetBegin()
    {
      var hb = new HTMLBuilder();
      hb.Text(LJC.GetHTMLBegin(FileName));
      hb.Line();
      hb.Text(HTMLHead());
      hb.End("head", NoIndent);
      hb.Begin("body", null, null, NoIndent);
      var retValue = hb.ToString();
      return retValue;
    }

    // Gets the key columns.
    private DbColumns GetKeyColumns()
    {
      var parentID = ParentObject.DataTableRowID(out long parentSiteID);
      var retColumns = ColumnManager.ParentKey(parentID, parentSiteID);
      return retColumns;
    }

    // Gets the load SQL.
    private string GetLoadSQL()
    {
      var keyColumns = GetKeyColumns();
      var propertyNames = GetPropertyNames();
      var orderByNames = GetOrderByNames();

      var manager = ColumnManager.Manager;
      manager.OrderByNames = orderByNames;
      var dataRequest = manager.CreateLoadRequest(keyColumns, propertyNames);
      var sqlBuilder = new DbSqlBuilder(dataRequest);
      var retSQL = sqlBuilder.CreateLoadSql(dataRequest);
      return retSQL;
    }

    // Gets the orderby names.
    private List<string> GetOrderByNames()
    {
      var retItems = new List<string>()
      {
        DataUtilColumn.ColumnSequence
      };
      return retItems;
    }

    // Gets the property names.
    private List<string> GetPropertyNames()
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

    // The custom HTML Head method.
    private string HTMLHead(string author = null)
    {
      // Create HTML Table.
      var hb = new HTMLBuilder();
      if (!NetString.HasValue(author))
      {
        author = LJC.Author();
      }
      hb.Create("title", "Creates an HTML Document");
      hb.CreateMetas(author, "Create HTML Document");
      hb.CreateLink("Style.css");
      hb.CreateScript("Source.js");
      var retValue = hb.ToString();
      return retValue;
    }

    // Sets the config properties.
    private void SetConfigData()
    {
      var settings = ParentObject.Settings;
      var configName = settings.DataConfigName;

      DataConfigs dataConfigs = new DataConfigs();
      dataConfigs.LJCLoadData();
      DataConfig = dataConfigs.LJCGetByName(configName);
      ConnectionString = DataConfig.GetConnectionString();
      ProviderName = DataConfig.GetProviderName();
    }
    #endregion

    #region Properties

    // Gets or sets the manager reference.
    private DataColumnManager ColumnManager { get; set; }

    // Gets or sets the connection string.
    private string ConnectionString { get; set; }

    // Gets or sets the DataConfig value.
    private DataConfig DataConfig { get; set; }

    // Gets or sets the file name.
    private string FileName { get; set; }

    // Gets or sets the parent object reference.
    private DataUtilityList ParentObject { get; set; }

    // Gets or sets the provider name.
    private string ProviderName { get; set; }
    #endregion

    #region Class Values

    const bool NoIndent = false;
    #endregion
  }
}
