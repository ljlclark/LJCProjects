// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DataCommon.cs
using System.Data;
using System.Data.Common;
using System.Data.SqlTypes;
using LJCNetCommon5;
using MySql.Data.MySqlClient;

namespace LJCDataAccess5
{
  // Provides common data methods.
  /// <include file='Doc/DataCommon.xml'
  ///  path='items/DataCommon/*'/>
  public class LJCDataCommon
  {
    #region Data Access Methods

    // Sets the data adapter table mappings.
    /// <include file='Doc/DataCommon.xml'
    ///  path='items/SetTableMapping/*'/>
    public static void SetTableMapping(DbDataAdapter? dataAdapter
      , DataTableMappingCollection? tableMapping)
    {
      if (dataAdapter != null
        && tableMapping != null)
      {
        foreach (DataTableMapping tableMap in tableMapping)
        {
          DataTableMapping map = dataAdapter.TableMappings.Add(tableMap.SourceTable, tableMap.DataSetTable);
          foreach (DataColumnMapping columnMap in tableMap.ColumnMappings)
          {
            map.ColumnMappings.Add(columnMap.SourceColumn, columnMap.DataSetColumn);
          }
        }
      }
    }

    // Sets the data adapter table mappings.
    /// <include file='Doc/DataCommon.xml'
    ///  path='items/SetTableMappingMySql/*'/>
    public static void SetTableMappingMySql(MySqlDataAdapter dataAdapter
      , DataTableMappingCollection? tableMapping)
    {
      if (tableMapping != null)
      {
        foreach (DataTableMapping tableMap in tableMapping)
        {
          DataTableMapping map = dataAdapter.TableMappings.Add(tableMap.SourceTable, tableMap.DataSetTable);
          foreach (DataColumnMapping columnMap in tableMap.ColumnMappings)
          {
            map.ColumnMappings.Add(columnMap.SourceColumn, columnMap.DataSetColumn);
          }
        }
      }
    }
    #endregion

    #region Data Conversion Methods

    // Converts the date string to a DateTime value.
    /// <include file='Doc/DataCommon.xml'
    ///  path='items/GetDbDate/*'/>
    public static DateTime GetDbDate(string dateText
      , ConnectionType connectionType = ConnectionType.SqlServer)
    {
      DateTime retValue;

      retValue = GetMinDateTime(connectionType);
      if (LJC.HasText(dateText)
        && dateText.Trim() != "/  /")
      {
        retValue = DateTime.Parse(dateText);
        retValue = retValue.Date;
      }
      return retValue;
    }

    // Converts the date/time string to a DateTime value.
    /// <include file='Doc/DataCommon.xml'
    ///  path='items/GetDbDateTime/*'/>
    public static DateTime GetDbDateTime(string dateText
      , ConnectionType connectionType = ConnectionType.SqlServer)
    {
      DateTime retValue;

      retValue = GetMinDateTime(connectionType);
      if (LJC.HasText(dateText)
        && dateText.Trim() != "/  /")
      {
        retValue = DateTime.Parse(dateText);
      }
      return retValue;
    }

    // Formats the DateTime value to a date string in database format.
    /// <include file='Doc/DataCommon.xml'
    ///  path='items/GetDbDateString/*'/>
    public static string GetDbDateString(DateTime dateTime
      , ConnectionType dbType = ConnectionType.SqlServer)
    {
      string retValue;

      dateTime = GetDbDate(dateTime.ToString(), dbType);
      retValue = dateTime.ToString("yyyy/MM/dd");
      return retValue;
    }

    // Formats the DateTime value to a date/time string in database format.
    /// <include file='Doc/DataCommon.xml'
    ///  path='items/GetDbDateTimeString/*'/>
    public static string GetDbDateTimeString(DateTime dateTime)
    {
      string retValue;

      retValue = dateTime.ToString("yyyy/MM/dd HH:mm:ss");
      return retValue;
    }

    // Get the minimum date/time value.
    /// <include file='Doc/DataCommon.xml'
    ///  path='items/GetMinDateTime/*'/>
    public static DateTime GetMinDateTime(ConnectionType connectionType
      = ConnectionType.SqlServer)
    {
      DateTime retValue = DateTime.MinValue;

      switch (connectionType)
      {
        case ConnectionType.SqlServer:
          // "1753/01/01 00:00:00";
          retValue = DateTime.Parse(SqlDateTime.MinValue.ToString());
          break;

        case ConnectionType.MySql:
          break;

        default:
          retValue = DateTime.MinValue;
          break;
      }
      return retValue;
    }

    // Get the minimum date/time string formatted for display.
    /// <include file='Doc/DataCommon.xml'
    ///  path='items/GetMinUIDateTimeString/*'/>
    public static string GetMinUIDateTimeString(ConnectionType connectionType
      = ConnectionType.SqlServer)
    {
      string retValue;

      retValue = string.Format(DateTime.MinValue.ToString()
        , "MM/dd/yyyy HH:mm:ss");
      if (connectionType == ConnectionType.SqlServer)
      {
        // "1753/01/01 00:00:00";
        retValue = string.Format(SqlDateTime.MinValue.ToString()
          , "MM/dd/yyyy HH:mm:ss");
      }
      return retValue;
    }

    // Format the date value for display.
    /// <include file='Doc/DataCommon.xml'
    ///  path='items/GetUIDateString/*'/>
    public static string? GetUIDateString(DateTime? dateTime)
    {
      string? retValue = null;

      if (dateTime != null
        && !LJC.IsDbMinDate(dateTime))
      {
        DateTime tempDateTime = (DateTime)dateTime;
        retValue = tempDateTime.ToString("MM/dd/yyyy");
      }
      return retValue;
    }

    // Format the date/time value for display.
    /// <include file='Doc/DataCommon.xml'
    ///  path='items/GetUIDateTimeString/*'/>
    public static string? GetUIDateTimeString(DateTime dateTime)
    {
      string? retValue = null;

      if (!LJC.IsDbMinDate(dateTime))
      {
        retValue = dateTime.ToString("MM/dd/yyyy HH:mm:ss");
      }
      return retValue;
    }

    // Format the date/time to time for display.
    /// <include file='Doc/DataCommon.xml'
    ///  path='items/GetUITimeString/*'/>
    public static string? GetUITimeString(DateTime dateTime)
    {
      string? retValue = null;

      if (!LJC.IsDbMinDate(dateTime))
      {
        retValue = dateTime.ToShortTimeString();
        if (7 == retValue.Length)
        {
          retValue = "0" + retValue;
        }
      }
      return retValue;
    }
    #endregion
  }
}
