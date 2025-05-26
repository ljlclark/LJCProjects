// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ColumnHTML.cs
//using LJCDataAccessConfig;
using LJCDataUtilityDAL;
using LJCDBMessage;
using LJCNetCommon;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace LJCDataUtility
{
  /// <summary>Custom DataColumn HTML methods.</summary>
  internal class ColumnHTMLTable
  {
    #region Constructors

    internal ColumnHTMLTable(string fileName)
    {
      // Initialize property values.
      FileName = fileName;
    }
    #endregion

    #region Create Table HTML Document Methods

    // Creates the HTML Table document from a DataObject.
    /// <include path='items/DataHTML/*' file='Doc/ColumnHtmlTable.xml'/>
    internal string DataHTML(List<object> dataObjects
      , List<string> propertyNames, TextState textState)
    {
      // GetText Method Begin
      var hb = new HTMLBuilder(textState);

      // Create custom beginning of document.
      var text = CreateHTMLDocBegin(textState);
      hb.AddText(text);

      hb.End("head", textState, NoIndent);
      hb.Begin("body", textState, null, NoIndent);
      hb.Text(CreatePage(textState), NoIndent);
      hb.Text(CreateHeader("Data Columns", textState), NoIndent);

      // Create HTML table.
      text = HTMLTableData.DataHTML(dataObjects, propertyNames);
      hb.AddText(text);

      // End "page"
      hb.End("div", textState);

      text = hb.GetHTMLEnd(textState);
      hb.AddText(text);

      var retValue = hb.ToString();
      return retValue;
    }

    // Creates the HTML Table document from DataTable.
    /// <include path='items/DataTableHTML/*' file='Doc/ColumnHtmlTable.xml'/>
    internal string DataTableHTML(DataTable dataTable, TextState textState)
    {
      var hb = new HTMLBuilder(textState);

      // Create custom beginning of document.
      var text = CreateHTMLDocBegin(textState);
      hb.AddText(text);

      hb.End("head", textState, NoIndent);
      hb.Begin("body", textState, null, NoIndent);
      hb.Text(CreatePage(textState), NoIndent);
      hb.Text(CreateHeader("Data Columns", textState), NoIndent);

      // Create HTML table.
      text = HTMLTableData.DataTableHTML(dataTable);
      hb.AddText(text);

      // End "page"
      hb.End("div", textState);

      text = hb.GetHTMLEnd(textState);
      hb.AddText(text);

      var retValue = hb.ToString();
      return retValue;
    }

    // Creates the HTML Table document from DbResult.
    /// <include path='items/ResultHTML/*' file='Doc/ColumnHtmlTable.xml'/>
    internal string ResultHTML(DbResult dbResult, TextState textState)
    {
      var hb = new HTMLBuilder(textState);

      // Create custom beginning of document.
      var createText = CreateHTMLDocBegin(textState);
      hb.Text(createText, NoIndent);

      hb.End("head", textState, NoIndent);
      hb.Begin("body", textState, null, NoIndent);
      hb.Text(CreatePage(textState), NoIndent);
      hb.Text(CreateHeader("Data Columns", textState), NoIndent);

      // HTML table.
      var tableText = HTMLTableData.ResultHTML(dbResult, textState);
      hb.Text(tableText, NoIndent);

      // End "page"
      hb.End("div", textState);

      createText = hb.GetHTMLEnd(textState);
      hb.Text(createText, NoIndent);

      var retValue = hb.ToString();
      return retValue;
    }

    /// <summary>
    /// Creates the HTML Table document from a DataGridView.
    /// </summary>
    /// <param name="grid">The DataGridView object.</param>
    /// <param name="textState">The TextState object.</param>
    /// <returns>The HTML Table document.</returns>
    internal string DataGridHTML(DataGridView grid, TextState textState)
    {
      var hb = new HTMLBuilder(textState);

      // Create custom beginning of document.
      var createText = CreateHTMLDocBegin(textState);
      hb.Text(createText, NoIndent);

      hb.End("head", textState, NoIndent);
      hb.Begin("body", textState, null, NoIndent);
      hb.Text(CreatePage(textState), NoIndent);
      hb.Text(CreateHeader("Data Columns", textState), NoIndent);

      // HTML table.
      var text = GridTableHTML(grid);
      hb.AddText(text);

      // End "page"
      hb.End("div", textState);

      createText = hb.GetHTMLEnd(textState);
      hb.Text(createText, NoIndent);

      var retValue = hb.ToString();
      return retValue;
    }
    #endregion

    #region Create HTML Element Methods

    // Creates the Header div.
    private string CreateHeader(string text, TextState textState)
    {
      var hb = new HTMLBuilder(textState);

      // Adds indents based on textState
      hb.Create("br", null, textState, isEmpty: true);
      var attribs = hb.Attribs(id: "Header");
      hb.Begin("div", textState, attribs);
      attribs = hb.Attribs(id: "Title");
      hb.Create("div", text, textState, attribs);
      hb.End("div", textState);
      hb.Create("br", null, textState, isEmpty: true);
      var retHeader = hb.ToString();
      return retHeader;
    }

    // Creates the Page div start.
    private string CreatePage(TextState textState)
    {
      var hb = new HTMLBuilder(textState);

      // Adds indents based on textState
      var attribs = hb.Attribs("page");
      hb.Begin("div", textState, attribs);
      var retPage = hb.ToString();
      return retPage;
    }
    #endregion

    #region Move to LJCDBMessage.HTMLTableData

    /// <summary>Create table headings from a DataGridView.</summary>
    /// <param name="grid">The DataGridView control.</param>
    /// <returns>The HTML table headings row.</returns>
    public static string GridTableHeadings(DataGridView grid)
    {
      string retValue = null;

      if (grid != null
        && grid.Columns.Count > 0)
      {
        var hb = new HTMLBuilder();
        var textState = new TextState();

        hb.Begin("tr", textState);
        foreach (DataGridViewColumn column in grid.Columns)
        {
          hb.Create("th", column.Name, textState);
        }
        hb.End("tr", textState);
        retValue = hb.ToString();
      }
      return retValue;
    }

    /// <summary>Create an HTML table from a DataGridView.</summary>
    /// <param name="grid">The DataGridView control.</param>
    /// <returns>The HTML table headings row.</returns>
    public static string GridTableHTML(DataGridView grid)
    {
      string retValue = null;

      if (grid != null
        && grid.Rows.Count > 0)
      {
        var hb = new HTMLBuilder();
        var textState = new TextState();

        var attribs = hb.TableAttribs();
        hb.Begin("table", textState, attribs);
        var text = GridTableHeadings(grid);
        hb.Text(text);
        text = GridTableRows(grid);
        hb.Text(text);
        hb.End("table", textState);
        retValue = hb.ToString();
      }
      return retValue;
    }

    /// <summary>Create table rows from a DataGridView.</summary>
    /// <param name="grid">The DataGridView control.</param>
    /// <returns>The HTML table rows.</returns>
    public static string GridTableRows(DataGridView grid)
    {
      string retValue = null;

      if (grid != null
        && grid.Rows.Count > 0)
      {
        var hb = new HTMLBuilder();
        var textState = new TextState();

        foreach (DataGridViewRow row in grid.Rows)
        {
          hb.Begin("tr", textState);
          foreach (DataGridViewColumn column in grid.Columns)
          {
            var cell = row.Cells[column.Name];
            string value = cell.Value.ToString();
            hb.Create("td", value, textState);
          }
          hb.End("tr", textState);
        }
        retValue = hb.ToString();
      }
      return retValue;
    }
    #endregion

    #region Custom HTML Methods

    // Gets beginning of HTML including <body> tag.
    private string CreateHTMLDocBegin(TextState textState)
    {
      var hb = new HTMLBuilder(textState);

      // Creates to including <head>.
      string[] copyright = new string[]
      {
        "Copyright (c) Lester J. Clark and Contributors",
        "Licensed under the MIT License",
      };
      var createText = hb.GetHTMLBegin(textState, copyright, FileName);
      hb.Text(createText, NoIndent);
      hb.AddChildIndent(createText, textState);

      createText = CreateHTMLDocHead(textState);
      hb.Text(createText, NoIndent);
      var retValue = hb.ToString();
      return retValue;
    }

    // The custom HTML Head method.
    private string CreateHTMLDocHead(TextState textState)
    {
      var hb = new HTMLBuilder(textState);

      var title = "Creates an HTML Document";
      var description = "Creates an HTML Document";
      var author = "Lester J. Clark";
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

    #region Properties

    // Gets or sets the manager reference.
    private DataColumnManager ColumnManager { get; set; }

    // Gets or sets the connection string.
    private string ConnectionString { get; set; }

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
