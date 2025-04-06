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
        hb.Begin("tr");
        foreach (DataColumn column in dataTable.Columns)
        {
          hb.Create("th", column.ColumnName);
        }
        hb.End("tr");
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
        hb.Begin("table", null, attribs);
        hb.Text(DataTableHeadings(dataTable));
        hb.Text(DataTableRows(dataTable, maxRows));
        hb.End("table");
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
        foreach (DataRow row in dataTable.Rows)
        {
          count++;
          if (maxRows > 0
            && count > maxRows)
          {
            break;
          }
          hb.Begin("tr");
          foreach (DataColumn column in dataTable.Columns)
          {
            string value = row[column.ColumnName].ToString();
            hb.Create("td", value);
          }
          hb.End("tr");
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
        hb.Begin("tr");
        var dataObject = dataObjects[0];
        var reflect = new LJCReflect(dataObject);
        List<string> names = reflect.GetPropertyNames();
        foreach (string name in names)
        {
          if (name != "ChangedNames"
            && propertyNames.Contains(name))
          {
            hb.Create("th", name);
          }
        }
        hb.End("tr");
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
        hb.Begin("table", null, attribs);
        hb.Text(DataHeadings(dataObjects, propertyNames));
        hb.Text(DataRows(dataObjects, propertyNames, maxRows));
        hb.End("table");
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
        foreach (var dataObject in dataObjects)
        {
          count++;
          if (maxRows > 0
            && count > maxRows)
          {
            break;
          }
          hb.Begin("tr");
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
              hb.Create("td", value);
            }
          }
          hb.End("tr");
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
        hb.Begin("tr");
        foreach (var value in dbResult.Rows[0].Values)
        {
          hb.Create("th", value.PropertyName);
        }
        hb.End("tr");
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
        hb.Begin("table", null, attribs);
        hb.Text(ResultHeadings(dbResult));
        hb.Text(ResultRows(dbResult, maxRows));
        hb.End("table");
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
        foreach (var row in dbResult.Rows)
        {
          count++;
          if (maxRows > 0
            && count > maxRows)
          {
            break;
          }
          hb.Begin("tr");
          foreach (var value in row.Values)
          {
            string valueText = null;
            if (value.Value != null)
            {
              valueText = value.Value.ToString();
            }
            hb.Create("td", valueText);
          }
          hb.End("tr");
        }
        retValue = hb.ToString();
      }
      return retValue;
    }
  }
  #endregion
}
