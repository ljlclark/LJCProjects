// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCDataHTMLTable.cs
using LJCNetCommon5;
using System.Data;

namespace LJCDBMessage5
{
  /// <summary>Methods to set data in HTML elements.</summary>
  public class LJCDataHTMLTable
  {
    #region Static DataTable Functions

    // Create table headings from a DataTable.
    /// <include path='items/DataTableHeadings/*' file='Doc/HTMLTableData.xml'/>
    public static string? DataTableHeadings(DataTable dataTable)
    {
      string? retValue = null;

      if (LJC.HasData(dataTable))
      {
        var hb = new LJCHTMLBuilder();
        var textState = new LJCTextState();

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
    public static string? DataTableHTML(DataTable dataTable, int maxRows = 0)
    {
      string? retValue = null;

      if (LJC.HasData(dataTable))
      {
        var hb = new LJCHTMLBuilder();
        var textState = new LJCTextState();

        var attribs = hb.TableAttribs();
        hb.Begin("table", textState, attribs);
        var text = DataTableHeadings(dataTable);
        if (LJC.HasValue(text))
        {
          hb.Text(text);
        }
        text = DataTableRows(dataTable, maxRows);
        if (LJC.HasValue(text))
        {
          hb.Text(text);
        }
        hb.End("table", textState);
        retValue = hb.ToString();
      }
      return retValue;
    }

    // Creates table rows from a DataTable.
    /// <include path='items/DataTableRows/*' file='Doc/HTMLTableData.xml'/>
    public static string? DataTableRows(DataTable dataTable, int maxRows = 0)
    {
      string? retValue = null;

      if (LJC.HasData(dataTable))
      {
        var hb = new LJCHTMLBuilder();
        var textState = new LJCTextState();

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
            var value = row[column.ColumnName].ToString();
            if (LJC.HasValue(value))
            {
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

    #region Static Data Object Functions

    // Create table headings from a Data Object.
    /// <include path='items/DataHeadings/*' file='Doc/HTMLTableData.xml'/>
    public static string? DataHeadings(List<object> dataObjects
      , List<string>? propertyNames = null)
    {
      string? retValue = null;

      if (LJC.HasItems(dataObjects))
      {
        var hb = new LJCHTMLBuilder();
        var textState = new LJCTextState();

        hb.Begin("tr", textState);
        var dataObject = dataObjects[0];
        var reflect = new LJCReflect(dataObject);
        List<string> names = reflect.GetPropertyNames();
        foreach (string name in names)
        {
          var success = true;
          if (name != "ChangedNames")
          {
            if (LJC.HasElements(propertyNames)
              && !propertyNames.Contains(name))
            {
              success = false;
            }
            if (success)
            {
              hb.Create("th", name, textState);
            }
          }
        }
        hb.End("tr", textState);
        retValue = hb.ToString();
      }
      return retValue;
    }

    // Create an HTML table from a Data Object.
    /// <include path='items/DataHTML/*' file='Doc/HTMLTableData.xml'/>
    public static string? DataHTML(List<object> dataObjects
      , List<string> propertyNames, int maxRows = 0)
    {
      string? retValue = null;

      if (LJC.HasItems(dataObjects))
      {
        var hb = new LJCHTMLBuilder();
        var textState = new LJCTextState();

        var attribs = hb.TableAttribs();
        hb.Begin("table", textState, attribs);
        var text = DataHeadings(dataObjects, propertyNames);
        if (LJC.HasValue(text))
        {
          hb.Text(text);
        }
        text = DataRows(dataObjects, propertyNames, maxRows);
        if (LJC.HasValue(text))
        {
          hb.Text(text);
        }
        hb.End("table", textState);
        retValue = hb.ToString();
      }
      return retValue;
    }

    // Create table rows from a Data Object.
    /// <include path='items/DataRows/*' file='Doc/HTMLTableData.xml'/>
    public static string? DataRows(List<object> dataObjects
      , List<string>? propertyNames = null, int maxRows = 0)
    {
      string? retValue = null;

      if (LJC.HasItems(dataObjects))
      {
        var hb = new LJCHTMLBuilder();
        var textState = new LJCTextState();

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
            if (name != "ChangedNames")
            {
              var success = true;
              if (LJC.HasElements(propertyNames)
                && !propertyNames.Contains(name))
              {
                success = false;
              }
              if (success)
              {
                string? value = "";
                var objectValue = reflect.GetValue(name);
                if (objectValue != null)
                {
                  value = objectValue.ToString();
                  if (!LJC.HasValue(value))
                  {
                    value = "";
                  }
                }
                hb.Create("td", value, textState);
              }
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
    public static string? ResultHeadings(LJCDBResult dbResult
      , LJCTextState textState)
    {
      string? retValue = null;

      if (LJCDBResult.HasRows(dbResult))
      {
        var hb = new LJCHTMLBuilder(textState);

        hb.Begin("tr", textState);
        var values = dbResult.Rows[0].Values;
        if (LJC.HasItems(values))
        {
          foreach (var value in values)
          {
            hb.Create("th", value.PropertyName, textState);
          }
          hb.End("tr", textState);
          retValue = hb.ToString();
        }
      }
      return retValue;
    }

    // Creates an HTML table from a DbResult.
    /// <include path='items/ResultHTML/*' file='Doc/HTMLTableData.xml'/>
    public static string? ResultHTML(LJCDBResult dbResult, LJCTextState textState
      , int maxRows = 0)
    {
      string? retValue = null;

      if (LJC.HasItems(dbResult.Rows))
      {
        var hb = new LJCHTMLBuilder(textState);

        var attribs = hb.TableAttribs();
        hb.Begin("table", textState, attribs);
        // Begin already indents for child elements.
        var text = ResultHeadings(dbResult, textState);
        if (LJC.HasValue(text))
        {
          // Use NoIndent after a "GetText" method.
          hb.Text(text, NoIndent);
        }
        text = ResultRows(dbResult, textState, maxRows);
        if (LJC.HasValue(text))
        {
          // Use NoIndent after a "GetText" method.
          hb.Text(text, NoIndent);
        }
        hb.End("table", textState);
        retValue = hb.ToString();
      }
      return retValue;
    }

    // Create table rows from a DbResult.
    /// <include path='items/ResultRows/*' file='Doc/HTMLTableData.xml'/>
    public static string? ResultRows(LJCDBResult dbResult, LJCTextState textState
      , int maxRows = 0)
    {
      string? retValue = null;

      if (LJCDBResult.HasRows(dbResult))
      {
        var hb = new LJCHTMLBuilder(textState);

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
          var values = row.Values;
          if (LJC.HasItems(values))
          {
            foreach (var value in values)
            {
              string? valueText = null;
              if (value.Value != null)
              {
                valueText = value.Value.ToString();
              }
              hb.Create("td", valueText, textState);
            }
            hb.End("tr", textState);
          }
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
