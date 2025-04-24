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
      // Root GetText Method Begin
      var hb = new HTMLBuilder();
      var textState = new TextState();

      string getText;
      switch (dataType.ToLower())
      {
        case "dataobject":
          getText = DataHTML(textState);
          hb.Text(getText, NoIndent);
          break;

        case "datatable":
          getText = DataTableHTML(textState);
          hb.Text(getText, NoIndent);
          break;

        case "dbresult":
          getText = ResultHTML(textState);
          hb.AddText(getText);
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
      // GetText Method Begin
      var hb = new HTMLBuilder(textState);

      // Create custom beginning of document.
      var text = GetHTMLDocBegin(textState);
      hb.AddText(text);

      // Create HTML table.
      var dataColumns = GetDataColumns();
      if (NetCommon.HasItems(dataColumns))
      {
        var dataObjects = dataColumns.ToList<object>();
        var propertyNames = GetPropertyNames();
        text = HTMLTableData.DataHTML(dataObjects, propertyNames);
        hb.AddText(text);
      }

      text = hb.GetHTMLEnd(textState);
      hb.AddText(text);
      var retValue = hb.ToString();
      return retValue;
    }

    // Gets the HTML Table document from DataTable.
    /// <include path='items/DataTableHTML/*' file='Doc/ColumnHtmlTable.xml'/>
    internal string DataTableHTML(TextState textState)
    {
      var hb = new HTMLBuilder(textState);

      // Create custom beginning of document.
      var text = GetHTMLDocBegin(textState);
      hb.AddText(text);

      // Create HTML table.
      var dataTable = GetDataTable();
      text = HTMLTableData.DataTableHTML(dataTable);
      hb.AddText(text);

      text = hb.GetHTMLEnd(textState);
      hb.AddText(text);
      var retValue = hb.ToString();
      return retValue;
    }

    // Gets the HTML Table document from DbResult.
    /// <include path='items/ResultHTML/*' file='Doc/ColumnHtmlTable.xml'/>
    internal string ResultHTML(TextState textState)
    {
      var hb = new HTMLBuilder(textState);

      // Create custom beginning of document.
      var createText = GetHTMLDocBegin(textState);
      hb.Text(createText, NoIndent);
      hb.End("head", textState, NoIndent);
      hb.Begin("body", textState, null, NoIndent);
      var attribs = hb.Attribs("page");
      hb.Begin("div", textState, attribs);

      // Header
      hb.Create("br", null, textState, isEmpty: true);
      attribs = hb.Attribs(id: "Header");
      hb.Begin("div", textState, attribs);
      attribs = hb.Attribs(id: "Title");
      hb.Create("div", "Data Columns", textState, attribs);
      hb.End("div", textState);
      hb.Create("br", null, textState, isEmpty: true);

      // HTML table.
      var dbResult = GetResult();
      var tableText = HTMLTableData.ResultHTML(dbResult, textState);
      hb.Text(tableText, NoIndent);

      // End "page"
      hb.End("div", textState);
      createText = hb.GetHTMLEnd(textState);
      hb.Text(createText, NoIndent);
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

    #region Private HTML Methods

    // Gets beginning of HTML including <body> tag.
    private string GetHTMLDocBegin(TextState textState)
    {
      var hb = new HTMLBuilder(textState);

      // Creates to including <head>.
      string[] copyright = new string[]
      {
        "Copyright(c) Lester J. Clark and Contributors",
        "Licensed under the MIT License",
      };
      var createText = hb.GetHTMLBegin(textState, copyright, FileName);
      hb.Text(createText, NoIndent);
      hb.AddChildIndent(createText, textState);

      createText = GetHTMLDocHead(textState);
      hb.Text(createText, NoIndent);
      var retValue = hb.ToString();
      return retValue;
    }

    // The custom HTML Head method.
    private string GetHTMLDocHead(TextState textState)
    {
      var hb = new HTMLBuilder(textState);

      var title = "Creates an HTML Document";
      var author = "Lester J. Clark";
      var description = "Creates an HTML Document";
      var getText = hb.GetHTMLHead(textState, title, author, description);
      hb.Text(getText, NoIndent);
      hb.Link("CSS/CodeDoc.css", textState);
      hb.Script("File.js", textState);

      hb.Begin("style", textState);
      var text = hb.GetBeginSelector("th", textState);
      hb.Text(text, NoIndent);
      hb.AddIndent();
      hb.Text("background-color: rgb(214, 234, 248);");

      // *** Move indent left ***
      hb.AddIndent(-1);
      textState.IndentCount += -1;

      hb.Text("}");
      hb.End("style", textState);
      var retValue = hb.ToString();
      return retValue;
    }
    #endregion

    #region Private Data Methods

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

    #region Class Data

    private const bool NoIndent = false;
    #endregion
  }
}
