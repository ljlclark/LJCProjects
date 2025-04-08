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
    internal string TableHTMLDoc(string dataType)
    {
      var hb = new HTMLBuilder();
      string text;
      switch (dataType.ToLower())
      {
        case "dataobject":
          text = DataHTML(hb.TextState);
          hb.Text(text, hb.HasText);
          break;

        case "datatable":
          text = DataTableHTML(hb.TextState);
          hb.Text(text, hb.HasText);
          break;

        case "dbresult":
          text = ResultHTML(hb.TextState);
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
      var hb = new HTMLBuilder(textState);
      var text = Begin(hb.TextState);
      hb.Add(text);

      var dataColumns = GetDataColumns();
      if (NetCommon.HasItems(dataColumns))
      {
        var dataObjects = dataColumns.ToList<object>();
        var propertyNames = GetPropertyNames();
        text = HTMLTableData.DataHTML(dataObjects, propertyNames);
        hb.Add(text);
        text = hb.GetHTMLEnd(hb.TextState);
        hb.Add(text);
      }
      var retValue = hb.ToString();
      return retValue;
    }

    // Gets the HTML Table document from DataTable.
    /// <include path='items/DataTableHTML/*' file='Doc/ColumnHtmlTable.xml'/>
    internal string DataTableHTML(TextState textState)
    {
      var hb = new HTMLBuilder(textState);
      var text = Begin(hb.TextState);
      hb.Add(text);

      var dataTable = GetDataTable();
      text = HTMLTableData.DataTableHTML(dataTable);
      hb.Add(text);
      text = hb.GetHTMLEnd(hb.TextState);
      hb.Add(text);
      var retValue = hb.ToString();
      return retValue;
    }

    // Gets the HTML Table document from DbResult.
    /// <include path='items/ResultHTML/*' file='Doc/ColumnHtmlTable.xml'/>
    internal string ResultHTML(TextState textState)
    {
      var hb = new HTMLBuilder(textState);
      var text = Begin(hb.TextState);
      hb.Add(text);

      var dbResult = GetResult();
      text = HTMLTableData.ResultHTML(dbResult);
      hb.Add(text);
      text = hb.GetHTMLEnd(hb.TextState);
      hb.Add(text);
      var retValue = hb.ToString();
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
    private string Begin(TextState textState)
    {
      var hb = new HTMLBuilder(textState);
      var text = LJCHTML.GetHTMLBegin(hb.TextState, FileName);
      //hb.Text(text, hb.HasText);
      hb.Add(text);
      text = HTMLHead(hb.TextState);
      //hb.Text(text, hb.HasText);
      hb.Add(text);
      hb.End("head", NoIndent);
      hb.Begin("body", hb.HasText, applyIndent: NoIndent);
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
    private string HTMLHead(TextState textState, string author = null)
    {
      // Create HTML Table.
      var hb = new HTMLBuilder(textState);

      // Handle in NetCommon?
      if (!NetString.HasValue(author))
      {
        author = LJCHTML.Author();
      }

      hb.Create("title", "Creates an HTML Document", hb.HasText);
      hb.CreateMetas(author, hb.TextState, "Create HTML Document");

      hb.Begin("style", hb.HasText);
      //hb.Line(null, hb.HasText);
      var text = BeginSelector("th", hb.IndentCount);
      hb.Text(text, hb.HasText);
      hb.AddIndent();
      hb.Line("background-color: rgb(214, 234, 248);", hb.HasText);
      hb.AddIndent(-1);
      hb.Text("}", hb.HasText);
      hb.End("style", hb.HasText);

      var retValue = hb.ToString();
      return retValue;
    }

    private string BeginSelector(string selectorName
      , int indentCount = 0)
    {
      var hb = new HTMLBuilder();

      // First line indent is handled by calling builder.
      hb.Line(selectorName, hb.HasText);
      hb.AddIndent(indentCount);

      hb.Line("{", hb.HasText);
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
