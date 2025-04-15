// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ColumnHTML.cs

using LJCDataAccess;
using LJCDataAccessConfig;
using LJCDataUtilityDAL;
using LJCDBMessage;
using LJCNetCommon;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace LJCDataUtility
{
  /// <summary>Custom DataColumn HTML methods.</summary>
  internal class ColumnHTMLTable
  {
    #region Constructors
    internal ColumnHTMLTable(DataUtilityList parentObject
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

    #region Main Methods

    // Gets the Table HTML document.
    /// <include path='items/GetHTML/*' file='Doc/ColumnHtmlTable.xml'/>
    internal string ColumnHTMLDoc(string dataType)
    {
      // Root GetText Method
      var hb = new HTMLBuilder();
      var textState = new TextState();

      string text;
      switch (dataType.ToLower())
      {
        case "dataobject":
          text = DataHTML(textState);
          hb.Text(text);
          break;

        case "datatable":
          text = DataTableHTML(textState);
          hb.Text(text);
          break;

        case "dbresult":
          text = ResultHTML(textState);
          hb.Add(text);
          break;
      }
      var retValue = hb.ToString();
      return retValue;
    }
    #endregion

    #region HTML Methods

    // Gets the HTML Table document from a DataObject.
    /// <include path='items/DataHTML/*' file='Doc/ColumnHtmlTable.xml'/>
    internal string DataHTML(TextState textState)
    {
      // GetText Method
      var hb = new HTMLBuilder(textState);
      var syncState = hb.GetSyncIndent(textState);

      var text = GetBegin(syncState);
      hb.Add(text);

      var dataColumns = GetDataColumns();
      if (NetCommon.HasItems(dataColumns))
      {
        var dataObjects = dataColumns.ToList<object>();
        var propertyNames = GetPropertyNames();
        text = HTMLTableData.DataHTML(dataObjects, propertyNames);
        hb.Add(text);
        text = hb.GetHTMLEnd(syncState);
        hb.Add(text);
      }
      var retValue = hb.ToString();

      hb.SyncState(textState, syncState);
      return retValue;
    }

    // Gets the HTML Table document from DataTable.
    /// <include path='items/DataTableHTML/*' file='Doc/ColumnHtmlTable.xml'/>
    internal string DataTableHTML(TextState textState)
    {
      // GetText Method
      var hb = new HTMLBuilder();
      var syncState = hb.GetSyncIndent(textState);

      var text = GetBegin(syncState);
      hb.Add(text);

      var dataTable = GetDataTable();
      text = HTMLTableData.DataTableHTML(dataTable);
      hb.Add(text);
      text = hb.GetHTMLEnd(syncState);
      hb.Add(text);
      var retValue = hb.ToString();

      hb.SyncState(textState, syncState);
      return retValue;
    }

    // Gets the HTML Table document from DbResult.
    /// <include path='items/ResultHTML/*' file='Doc/ColumnHtmlTable.xml'/>
    internal string ResultHTML(TextState textState)
    {
      // GetText Method
      var hb = new HTMLBuilder();
      var syncState = hb.GetSyncIndent(textState);

      var text = GetBegin(syncState);
      hb.Text(text);
      var dbResult = GetResult();
      text = HTMLTableData.ResultHTML(dbResult, syncState);
      hb.Text(text);  
      text = hb.GetHTMLEnd(syncState);
      hb.Text(text);
      var retValue = hb.ToString();

      hb.SyncState(textState, syncState);
      return retValue;
    }
    #endregion

    #region Data Methods

    // Gets the DataTable object.
    /// <include path='items/GetDataTable/*' file='Doc/ColumnHtmlTable.xml'/>
    internal DataTable GetDataTable()
    {
      SetConfigData();
      var dataAccess = new DataAccess(ConnectionString, ProviderName);
      var loadSQL = GetLoadSQL();
      var retTable = dataAccess.GetDataTable(loadSQL);
      return retTable;
    }

    // Gets the Data Object.
    /// <include path='items/GetDataColumns/*' file='Doc/ColumnHtmlTable.xml'/>
    internal DataColumns GetDataColumns()
    {
      var keyColumns = GetKeyColumns();
      var propertyNames = GetPropertyNames();
      var orderByNames = GetOrderByNames();
      ColumnManager.Manager.OrderByNames = orderByNames;
      var retData = ColumnManager.Load(keyColumns, propertyNames);
      return retData;
    }

    // Gets the DbResult object.
    /// <include path='items/GetResult/*' file='Doc/ColumnHtmlTable.xml'/>
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
    private string GetBegin(TextState textState)
    {
      // GetText Method
      var hb = new HTMLBuilder();
      var syncState = hb.GetSyncIndent(textState);

      // Creates to including <head>.
      var getText = LJCHTML.GetHTMLBegin(syncState, FileName);
      hb.Text(getText);
      hb.AddChildIndent(syncState);

      getText = GetHTMLHead(syncState);
      hb.Text(getText);
      hb.End("head", syncState, NoIndent);
      hb.Begin("body", syncState);
      var retValue = hb.ToString();

      hb.SyncState(textState, syncState);
      return retValue;
    }

    // The custom HTML Head method.
    private string GetHTMLHead(TextState textState, string author = null)
    {
      // GetText Method
      var hb = new HTMLBuilder();
      var syncState = hb.GetSyncIndent(textState);

      // Handle in NetCommon?
      if (!NetString.HasValue(author))
      {
        author = LJCHTML.Author();
      }

      hb.Create("title", "Creates an HTML Document", syncState
        , childIndent: false);
      hb.Metas(author, syncState, "Create HTML Document");
      hb.Begin("style", syncState, childIndent: false);
      var text = BeginSelector("th", hb.IndentCount);
      hb.Text(text);
      hb.AddIndent();
      hb.Text("background-color: rgb(214, 234, 248);");

      // *** Manually added indent ***
      hb.AddIndent(-1);
      syncState.IndentCount += -1;

      hb.Text("}");
      hb.End("style", syncState);
      var retValue = hb.ToString();

      hb.SyncState(textState, syncState);
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

    private string BeginSelector(string selectorName
      , int indentCount = 0)
    {
      var hb = new HTMLBuilder();
      hb.Text(selectorName);
      hb.AddIndent(indentCount);
      hb.Text("{");
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
