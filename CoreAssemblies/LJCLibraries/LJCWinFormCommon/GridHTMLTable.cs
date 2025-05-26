// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// GridHTMLTable.cs
using LJCNetCommon;
using System.Windows.Forms;

namespace LJCWinFormCommon
{
  /// <summary>Methods to set data in HTML elements.</summary>
  public class GridHTMLTable
  {
    /// <summary>Create table headings from a DataGridView.</summary>
    /// <param name="grid">The DataGridView control.</param>
    /// <returns>The HTML table headings row.</returns>
    public static string Headings(DataGridView grid)
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
    public static string HTML(DataGridView grid)
    {
      string retValue = null;

      if (grid != null
        && grid.Rows.Count > 0)
      {
        var hb = new HTMLBuilder();
        var textState = new TextState();

        var attribs = hb.TableAttribs();
        hb.Begin("table", textState, attribs);
        var text = Headings(grid);
        hb.Text(text);
        text = Rows(grid);
        hb.Text(text);
        hb.End("table", textState);
        retValue = hb.ToString();
      }
      return retValue;
    }

    /// <summary>Create table rows from a DataGridView.</summary>
    /// <param name="grid">The DataGridView control.</param>
    /// <returns>The HTML table rows.</returns>
    public static string Rows(DataGridView grid)
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
  }
}
