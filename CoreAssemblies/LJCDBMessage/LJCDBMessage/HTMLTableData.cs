// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// HTMLData.cs
using LJCNetCommon;
using System.Collections.Generic;
using System.Data;

namespace LJCDBMessage
{
  /// <summary>Methods to set data in HTML elements.</summary>
  public class HTMLTableData
  {
    #region Static DataTable Functions

    // Create table headings from a DataTable.
    /// <include path='items/DataTableHeadings/*' file='Doc/HTMLTableData.xml'/>
    public static string DataTableHeadings(DataTable dataTable)
    {
      string retValue = null;

      if (NetCommon.HasData(dataTable))
      {
        var hb = new HTMLBuilder();
        var textState = new TextState();

        hb.Begin("tr", textState);
        foreach (DataColumn column in dataTable.Columns)
        {
          hb.Create("th", column.ColumnName, textState);
        }
        hb.End("tr", textState);
        retValue = hb.ToString();
      }
      return retValue;
    }

    // Creates an HTML table from a DataTable.
    /// <include path='items/DataTableHTML/*' file='Doc/HTMLTableData.xml'/>
    public static string DataTableHTML(DataTable dataTable, int maxRows = 0)
    {
      string retValue = null;

      if (NetCommon.HasData(dataTable))
      {
        var hb = new HTMLBuilder();
        var textState = new TextState();

        var attribs = hb.TableAttribs();
        hb.Begin("table", textState, attribs);
        var text = DataTableHeadings(dataTable);
        hb.Text(text);
        text = DataTableRows(dataTable, maxRows);
        hb.Text(text);
        hb.End("table", textState);
        retValue = hb.ToString();
      }
      return retValue;
    }

    // Creates table rows from a DataTable.
    /// <include path='items/DataTableRows/*' file='Doc/HTMLTableData.xml'/>
    public static string DataTableRows(DataTable dataTable, int maxRows = 0)
    {
      string retValue = null;

      if (NetCommon.HasData(dataTable))
      {
        var hb = new HTMLBuilder();
        var textState = new TextState();

        var count = 0;
        foreach (DataRow row in dataTable.Rows)
        {
          count++;
          if (maxRows > 0
            && count > maxRows)
          {
            break;
          }
          hb.Begin("tr", textState);
          foreach (DataColumn column in dataTable.Columns)
          {
            string value = row[column.ColumnName].ToString();
            hb.Create("td", value, textState);
          }
          hb.End("tr", textState);
        }
        retValue = hb.ToString();
      }
      return retValue;
    }
    #endregion

    #region Static Data Object Functions

    // Create table headings from a Data Object.
    /// <include path='items/DataHeadings/*' file='Doc/HTMLTableData.xml'/>
    public static string DataHeadings(List<object> dataObjects
      , List<string> propertyNames = null)
    {
      string retValue = null;

      if (NetCommon.HasItems(dataObjects))
      {
        var hb = new HTMLBuilder();
        var textState = new TextState();

        hb.Begin("tr", textState);
        var dataObject = dataObjects[0];
        var reflect = new LJCReflect(dataObject);
        List<string> names = reflect.GetPropertyNames();
        foreach (string name in names)
        {
          if (name != "ChangedNames"
            && propertyNames.Contains(name))
          {
            hb.Create("th", name, textState);
          }
        }
        hb.End("tr", textState);
        retValue = hb.ToString();
      }
      return retValue;
    }

    // Create an HTML table from a Data Object.
    /// <include path='items/DataHTML/*' file='Doc/HTMLTableData.xml'/>
    public static string DataHTML(List<object> dataObjects
      , List<string> propertyNames, int maxRows = 0)
    {
      string retValue = null;

      if (NetCommon.HasItems(dataObjects))
      {
        var hb = new HTMLBuilder();
        var textState = new TextState();

        var attribs = hb.TableAttribs();
        hb.Begin("table", textState, attribs);
        var text = DataHeadings(dataObjects, propertyNames);
        hb.Text(text);
        text = DataRows(dataObjects, propertyNames, maxRows);
        hb.Text(text);
        hb.End("table", textState);
        retValue = hb.ToString();
      }
      return retValue;
    }

    // Create table rows from a Data Object.
    /// <include path='items/DataRows/*' file='Doc/HTMLTableData.xml'/>
    public static string DataRows(List<object> dataObjects
      , List<string> propertyNames = null, int maxRows = 0)
    {
      string retValue = null;

      if (NetCommon.HasItems(dataObjects))
      {
        var hb = new HTMLBuilder();
        var textState = new TextState();

        var count = 0;
        foreach (var dataObject in dataObjects)
        {
          count++;
          if (maxRows > 0
            && count > maxRows)
          {
            break;
          }
          hb.Begin("tr", textState);
          var reflect = new LJCReflect(dataObject);
          List<string> names = reflect.GetPropertyNames();
          foreach (string name in names)
          {
            if (name != "ChangedNames"
              && propertyNames.Contains(name))
            {
              string value = "";
              var objectValue = reflect.GetValue(name);
              if (objectValue != null)
              {
                value = objectValue.ToString();
              }
              hb.Create("td", value, textState);
            }
          }
          hb.End("tr", textState);
        }
        retValue = hb.ToString();
      }
      return retValue;
    }
    #endregion

    #region Static DbResult Functions.

    // Create table headings from a DbResult.
    /// <include path='items/ResultHeadings/*' file='Doc/HTMLTableData.xml'/>
    /// <param name="textState"></param>
    public static string ResultHeadings(DbResult dbResult, TextState textState)
    {
      string retValue = null;

      if (DbResult.HasRows(dbResult))
      {
        var hb = new HTMLBuilder(textState);

        hb.Begin("tr", textState);
        foreach (var value in dbResult.Rows[0].Values)
        {
          hb.Create("th", value.PropertyName, textState);
        }
        hb.End("tr", textState);
        retValue = hb.ToString();
      }
      return retValue;
    }

    // Creates an HTML table from a DbResult.
    /// <include path='items/ResultHTML/*' file='Doc/HTMLTableData.xml'/>
    public static string ResultHTML(DbResult dbResult, TextState textState
      , int maxRows = 0)
    {
      string retValue = null;

      if (NetCommon.HasItems(dbResult.Rows))
      {
        var hb = new HTMLBuilder(textState);

        var attribs = hb.TableAttribs();
        hb.Begin("table", textState, attribs);
        // Begin already indents for child elements.
        var text = ResultHeadings(dbResult, textState);
        // Use NoIndent after a "GetText" method.
        hb.Text(text, NoIndent);
        text = ResultRows(dbResult, textState, maxRows);
        // Use NoIndent after a "GetText" method.
        hb.Text(text, NoIndent);
        hb.End("table", textState);
        retValue = hb.ToString();
      }
      return retValue;
    }

    // Create table rows from a DbResult.
    /// <include path='items/ResultRows/*' file='Doc/HTMLTableData.xml'/>
    /// <param name="textState"></param>
    public static string ResultRows(DbResult dbResult, TextState textState
      , int maxRows = 0)
    {
      string retValue = null;

      if (DbResult.HasRows(dbResult))
      {
        var hb = new HTMLBuilder(textState);

        var count = 0;
        foreach (var row in dbResult.Rows)
        {
          count++;
          if (maxRows > 0
            && count > maxRows)
          {
            break;
          }
          hb.Begin("tr", textState);
          // Begin already indents for child elements.
          foreach (var value in row.Values)
          {
            string valueText = null;
            if (value.Value != null)
            {
              valueText = value.Value.ToString();
            }
            hb.Create("td", valueText, textState);
          }
          hb.End("tr", textState);
        }
        retValue = hb.ToString();
      }
      return retValue;
    }
    #endregion

    #region Class Data

    private const bool NoIndent = false;
    #endregion
  }
}
