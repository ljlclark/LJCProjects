// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// HTMLData.cs

using LJCDBMessage;
using LJCNetCommon;
using System.Data;

namespace LJCDataUtility
{
  /// <summary>Methods to set data in HTML elements.</summary>
  public class HTMLData
  {
    #region Static Functions

    // Create table headings from DataTable.
    /// <include path='items/DataTableHeadings/*' file='Doc/HTMLData.xml'/>
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

    // Creates an HTML table from DataTable.
    /// <include path='items/DataTableHTML/*' file='Doc/HTMLData.xml'/>
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

    // Creates table rows from DataTable.
    /// <include path='items/DataTableRows/*' file='Doc/HTMLData.xml'/>
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

    // Create table headings from DbResult.
    /// <include path='items/ResultHeadings/*' file='Doc/HTMLData.xml'/>
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

    // Creates an HTML table from DbResult.
    /// <include path='items/ResultHTML/*' file='Doc/HTMLData.xml'/>
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

    // Create table rows from result.
    /// <include path='items/ResultRows/*' file='Doc/HTMLData.xml'/>
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
