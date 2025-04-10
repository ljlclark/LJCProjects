// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// HTMLData.cs
using LJCDBMessage;
using LJCNetCommon;
using System.Collections.Generic;
using System.Data;

namespace LJCDataUtility
{
  // Move to ?
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
        hb.Begin("tr", hb.TextState);
        var state = new TextState(hb.HasText, hb.IndentCount);
        foreach (DataColumn column in dataTable.Columns)
        {
          state.HasText = hb.HasText;
          hb.Create("th", column.ColumnName, state);
        }
        hb.End("tr", hb.HasText);
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
        var attribs = hb.TableAttribs();
        hb.Begin("table", hb.TextState, null, attribs);
        var text = DataTableHeadings(dataTable);
        hb.Text(text, hb.TextState);
        text = DataTableRows(dataTable, maxRows);
        hb.Text(text, hb.TextState);
        hb.End("table", hb.HasText);
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
        var count = 0;
        var hb = new HTMLBuilder();
        var state = new TextState(hb.HasText, hb.IndentCount);
        foreach (DataRow row in dataTable.Rows)
        {
          count++;
          if (maxRows > 0
            && count > maxRows)
          {
            break;
          }
          hb.Begin("tr", hb.TextState);
          foreach (DataColumn column in dataTable.Columns)
          {
            string value = row[column.ColumnName].ToString();
            state.HasText = hb.HasText;
            hb.Create("td", value, state);
          }
          hb.End("tr", hb.HasText);
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
        var state = new TextState(hb.HasText, hb.IndentCount);
        hb.Begin("tr", hb.TextState);
        var dataObject = dataObjects[0];
        var reflect = new LJCReflect(dataObject);
        List<string> names = reflect.GetPropertyNames();
        foreach (string name in names)
        {
          if (name != "ChangedNames"
            && propertyNames.Contains(name))
          {
            state.HasText = hb.HasText;
            hb.Create("th", name, state);
          }
        }
        hb.End("tr", hb.HasText);
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
        var attribs = hb.TableAttribs();
        hb.Begin("table", hb.TextState, null, attribs);
        var text = DataHeadings(dataObjects, propertyNames);
        hb.Text(text, hb.TextState);
        text = DataRows(dataObjects, propertyNames, maxRows);
        hb.Text(text, hb.TextState);
        hb.End("table", hb.HasText);
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
        var count = 0;
        var hb = new HTMLBuilder();
        var state = new TextState(hb.HasText, hb.IndentCount);
        foreach (var dataObject in dataObjects)
        {
          count++;
          if (maxRows > 0
            && count > maxRows)
          {
            break;
          }
          hb.Begin("tr", hb.TextState);
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
              state.HasText = hb.HasText;
              hb.Create("td", value, state);
            }
          }
          hb.End("tr", hb.HasText);
        }
        retValue = hb.ToString();
      }
      return retValue;
    }
    #endregion

    #region Static DbResult Functions.

    // Create table headings from a DbResult.
    /// <include path='items/ResultHeadings/*' file='Doc/HTMLTableData.xml'/>
    public static string ResultHeadings(DbResult dbResult)
    {
      string retValue = null;

      if (DbResult.HasRows(dbResult))
      {
        var hb = new HTMLBuilder();
        var state = new TextState(hb.HasText, hb.IndentCount);
        hb.Begin("tr", hb.TextState);
        foreach (var value in dbResult.Rows[0].Values)
        {
          state.HasText = hb.HasText;
          hb.Create("th", value.PropertyName, state);
        }
        hb.End("tr", hb.HasText);
        retValue = hb.ToString();
      }
      return retValue;
    }

    // Creates an HTML table from a DbResult.
    /// <include path='items/ResultHTML/*' file='Doc/HTMLTableData.xml'/>
    public static string ResultHTML(DbResult dbResult, int maxRows = 0)
    {
      string retValue = null;

      if (NetCommon.HasItems(dbResult.Rows))
      {
        var hb = new HTMLBuilder();
        var attribs = hb.TableAttribs();
        hb.Begin("table", hb.TextState, null, attribs);
        var text = ResultHeadings(dbResult);
        hb.Text(text, hb.TextState);
        text = ResultRows(dbResult, maxRows);
        hb.Text(text, hb.TextState);
        hb.End("table", hb.HasText);
        retValue = hb.ToString();
      }
      return retValue;
    }

    // Create table rows from a DbResult.
    /// <include path='items/ResultRows/*' file='Doc/HTMLTableData.xml'/>
    public static string ResultRows(DbResult dbResult, int maxRows = 0)
    {
      string retValue = null;

      if (DbResult.HasRows(dbResult))
      {
        var count = 0;
        var hb = new HTMLBuilder();
        var state = new TextState(hb.HasText, hb.IndentCount);
        foreach (var row in dbResult.Rows)
        {
          count++;
          if (maxRows > 0
            && count > maxRows)
          {
            break;
          }
          hb.Begin("tr", hb.TextState);
          foreach (var value in row.Values)
          {
            string valueText = null;
            if (value.Value != null)
            {
              valueText = value.Value.ToString();
            }
            state.HasText = hb.HasText;
            hb.Create("td", valueText, state);
          }
          hb.End("tr", hb.HasText);
        }
        retValue = hb.ToString();
      }
      return retValue;
    }
  }
  #endregion
}
