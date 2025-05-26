// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// GenHTMLTable.cs
//using LJCDataAccessConfig;
using LJCDataUtilityDAL;
using LJCDBMessage;
using LJCNetCommon;
using LJCWinFormCommon;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace LJCDataUtility
{
  /// <summary>Generate HTML Table methods.</summary>
  internal class GenHTMLTable
  {
    #region Constructors

    internal GenHTMLTable(string fileName)
    {
      // Initialize property values.
      FileName = fileName;
    }
    #endregion

    #region Create Table HTML Document Methods

    // Creates the HTML Table document from a DataObject.
    /// <include path='items/DataHTML/*' file='Doc/GenHTMLTable.xml'/>
    internal string DataHTML(List<object> dataObjects
      , string heading, List<string> propertyNames, TextState textState)
    {
      // GetText Method Begin
      var hb = new HTMLBuilder(textState);

      // Create custom beginning of document.
      var text = CreateHTMLDocBegin(textState);
      hb.AddText(text);

      hb.End("head", textState, NoIndent);
      hb.Begin("body", textState, null, NoIndent);
      hb.Text(CreatePage(textState), NoIndent);
      hb.Text(CreateHeading(heading, textState), NoIndent);

      // Create HTML table.
      text = DataHTMLTable.DataHTML(dataObjects, propertyNames);
      hb.AddText(text);

      // End "page"
      hb.End("div", textState);

      text = hb.GetHTMLEnd(textState);
      hb.AddText(text);

      var retValue = hb.ToString();
      return retValue;
    }

    // Creates the HTML Table document from DataTable.
    /// <include path='items/DataTableHTML/*' file='Doc/GenHTMLTable.xml'/>
    internal string DataTableHTML(DataTable dataTable, string heading
      , TextState textState)
    {
      var hb = new HTMLBuilder(textState);

      // Create custom beginning of document.
      var text = CreateHTMLDocBegin(textState);
      hb.AddText(text);

      hb.End("head", textState, NoIndent);
      hb.Begin("body", textState, null, NoIndent);
      hb.Text(CreatePage(textState), NoIndent);
      hb.Text(CreateHeading(heading, textState), NoIndent);

      // Create HTML table.
      text = DataHTMLTable.DataTableHTML(dataTable);
      hb.AddText(text);

      // End "page"
      hb.End("div", textState);

      text = hb.GetHTMLEnd(textState);
      hb.AddText(text);

      var retValue = hb.ToString();
      return retValue;
    }

    // Creates the HTML Table document from DbResult.
    /// <include path='items/ResultHTML/*' file='Doc/GenHTMLTable.xml'/>
    internal string ResultHTML(DbResult dbResult, string heading
      , TextState textState)
    {
      var hb = new HTMLBuilder(textState);

      // Create custom beginning of document.
      var createText = CreateHTMLDocBegin(textState);
      hb.Text(createText, NoIndent);

      hb.End("head", textState, NoIndent);
      hb.Begin("body", textState, null, NoIndent);
      hb.Text(CreatePage(textState), NoIndent);
      hb.Text(CreateHeading(heading, textState), NoIndent);

      // HTML table.
      var tableText = DataHTMLTable.ResultHTML(dbResult, textState);
      hb.Text(tableText, NoIndent);

      // End "page"
      hb.End("div", textState);

      createText = hb.GetHTMLEnd(textState);
      hb.Text(createText, NoIndent);

      var retValue = hb.ToString();
      return retValue;
    }

    // Creates the HTML Table document from a DataGridView.
    /// <include path='items/DataGridHTML/*' file='Doc/GenHTMLTable.xml'/>
    internal string DataGridHTML(DataGridView grid, string heading
      , TextState textState)
    {
      var hb = new HTMLBuilder(textState);

      // Create custom beginning of document.
      var createText = CreateHTMLDocBegin(textState);
      hb.Text(createText, NoIndent);

      hb.End("head", textState, NoIndent);
      hb.Begin("body", textState, null, NoIndent);
      hb.Text(CreatePage(textState), NoIndent);
      hb.Text(CreateHeading(heading, textState), NoIndent);

      // HTML table.
      var text = GridHTMLTable.HTML(grid);
      hb.AddText(text);

      // End "page"
      hb.End("div", textState);

      createText = hb.GetHTMLEnd(textState);
      hb.Text(createText, NoIndent);

      var retValue = hb.ToString();
      return retValue;
    }
    #endregion

    #region Private Methods

    // Creates the Header div.
    private string CreateHeading(string text, TextState textState)
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
