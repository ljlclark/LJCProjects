// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// HTMLData.cs

using LJCDBMessage;

namespace LJCDataUtility
{
  /// <summary>Methods to set data in HTML elements.</summary>
  public class HTMLData
  {
    #region Static Functions

    // Create table headings from result.
    /// <include path='items/ResultTableHeadings/*' file='Doc/HTMLData.xml'/>
    public static string ResultTableHeadings(DbResult dbResult)
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

    // Create table rows from result.
    /// <include path='items/ResultTableRows/*' file='Doc/HTMLData.xml'/>
    public static string ResultTableRows(DbResult dbResult, int limit = 10)
    {
      string retValue = null;

      if (DbResult.HasRows(dbResult))
      {
        var count = 0;
        var hb = new HTMLBuilder();
        foreach (var row in dbResult.Rows)
        {
          count++;
          if (limit > 0
            && count > limit)
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
