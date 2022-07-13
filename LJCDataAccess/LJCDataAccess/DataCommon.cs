// Copyright (c) Lester J Clark 2021,2022 - All Rights Reserved
using System;
using System.Data.Common;
using System.Data.SqlTypes;
using LJCNetCommon;
using MySql.Data.MySqlClient;

namespace LJCDataAccess
{
  // Provides common data methods.
  /// <include path='items/DataCommon/*' file='Doc/DataCommon.xml'/>
  public class DataCommon
  {
    #region Data Access Methods

    // Retrieves the database connection ConnectionType value.
    /// <include path='items/GetConnectionType/*' file='Doc/DataCommon.xml'/>
    public static ConnectionType GetConnectionType(string connectionTypeName)
    {
      ConnectionType retVal = ConnectionType.SqlServer;

      if (connectionTypeName != null)
      {
        if (NetString.IsEqual(connectionTypeName, "OleDB"))
        {
          retVal = ConnectionType.OleDb;
        }
        if (NetString.IsEqual(connectionTypeName, "ODBC"))
        {
          retVal = ConnectionType.Odbc;
        }
        if (NetString.IsEqual(connectionTypeName, "SQLServer"))
        {
          retVal = ConnectionType.SqlServer;
        }
        if (NetString.IsEqual(connectionTypeName, "MySQL"))
        {
          retVal = ConnectionType.MySql;
        }
        if (NetString.IsEqual(connectionTypeName, "Access"))
        {
          retVal = ConnectionType.Access;
        }
      }
      return retVal;
    }

    // Sets the data adapter table mappings.
    /// <include path='items/SetTableMapping/*' file='Doc/DataCommon.xml'/>
    public static void SetTableMapping(DbDataAdapter dataAdapter
      , DataTableMappingCollection tableMapping)
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

    // Sets the data adapter table mappings.
    /// <include path='items/SetTableMappingMySql/*' file='Doc/DataCommon.xml'/>
    public static void SetTableMappingMySql(MySqlDataAdapter dataAdapter
      , DataTableMappingCollection tableMapping)
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
    /// <include path='items/GetDbDate/*' file='Doc/DataCommon.xml'/>
    public static DateTime GetDbDate(string dateText
      , ConnectionType connectionType = ConnectionType.SqlServer)
    {
      DateTime retValue;

      retValue = GetMinDateTime(connectionType);
      if (NetString.HasValue(dateText)
        && dateText.Trim() != "/  /")
      {
        retValue = DateTime.Parse(dateText);
      }
      return retValue;
    }

    // Formats the DateTime value to a date string in database format.
    /// <include path='items/GetDbDateString/*' file='Doc/DataCommon.xml'/>
    public static string GetDbDateString(DateTime dateTime
      , ConnectionType dbType = ConnectionType.SqlServer)
    {
      string retValue;

      dateTime = GetDbDate(dateTime.ToString(), dbType);
      retValue = dateTime.ToString("yyyy/MM/dd");
      return retValue;
    }

    // Formats the DateTime value to a date/time string in database format.
    /// <include path='items/GetDbDateTimeString/*' file='Doc/DataCommon.xml'/>
    public static string GetDbDateTimeString(DateTime dateTime)
    {
      string retValue;

      retValue = dateTime.ToString("yyyy/MM/dd HH:mm:ss");
      return retValue;
    }

    // Get the minimum date/time value.
    /// <include path='items/GetMinDateTime/*' file='Doc/DataCommon.xml'/>
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
      }
      return retValue;
    }

    // Get the minimum date/time string formatted for display.
    /// <include path='items/GetMinUIDateTimeString/*' file='Doc/DataCommon.xml'/>
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
    /// <include path='items/GetUIDateString/*' file='Doc/DataCommon.xml'/>
    public static string GetUIDateString(DateTime dateTime)
    {
      string retValue = null;

      if (false == IsDbMinDate(dateTime))
      {
        retValue = dateTime.ToString("MM/dd/yyyy");
      }
      return retValue;
    }

    // Format the date/time value for display.
    /// <include path='items/GetUIDateTimeString/*' file='Doc/DataCommon.xml'/>
    public static string GetUIDateTimeString(DateTime dateTime)
    {
      string retValue = null;

      if (false == IsDbMinDate(dateTime))
      {
        retValue = dateTime.ToString("MM/dd/yyyy HH:mm:ss");
      }
      return retValue;
    }

    // Format the date/time to time for display.
    /// <include path='items/GetUITimeString/*' file='Doc/DataCommon.xml'/>
    public static string GetUITimeString(DateTime dateTime)
    {
      string retValue = null;

      if (false == IsDbMinDate(dateTime))
      {
        retValue = dateTime.ToShortTimeString();
        if (7 == retValue.Length)
        {
          retValue = "0" + retValue;
        }
      }
      return retValue;
    }

    // Check for DB Minimum date or less.
    /// <include path='items/IsDbMinDate/*' file='Doc/DataCommon.xml'/>
    public static bool IsDbMinDate(DateTime dateTime)
    {
      bool retValue = false;
      if (dateTime.Year < 1753)
      {
        retValue = true;
      }
      if (1753 == dateTime.Year
        && 1 == dateTime.Month
        && 1 == dateTime.Day)
      {
        retValue = true;
      }
      return retValue;
    }
    #endregion
  }
}
