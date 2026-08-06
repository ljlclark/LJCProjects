// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ManagerCommon.cs
using LJCDataAccess;
using LJCDataAccessConfig;
using LJCDBMessage;
using LJCNetCommon;
using System;

namespace LJCDBClientLib
{
  /// <summary>Contains common static manager methods.</summary>
  public class ManagerCommon
  {
    // Creates the DbRequest object.
    /// <include file='Doc/ManagerCommon.xml'
    ///  path='items/CreateRequest/*'/>
    public static DbRequest CreateRequest(RequestType requestType
      , string tableName, LJCDataColumns requestColumns, string dataConfigName
      , string schemaName, LJCDataColumns keyColumns = null
      , DbFilters filters = null, DbJoins joins = null)
    {
      DbRequest retValue;

      // Create a request with the retrieve columns.
      // The retrieved columns should include the DB assigned columns.
      retValue = new DbRequest(requestType, tableName)
      {
        Columns = requestColumns,
        DataConfigName = dataConfigName,
        Filters = filters,
        Joins = joins,
        KeyColumns = keyColumns,
        SchemaName = schemaName
      };
      return retValue;
    }

    // Run the CreateTables script.
    /// <include file='Doc/ManagerCommon.xml'
    ///  path='items/CreateTables/*'/>
    public static bool CreateTables(string dataConfigName, string[] fileSpecs)
    {
      bool retValue;

      if (!NetString.HasValue(dataConfigName))
      {
        throw new ArgumentException("message", nameof(dataConfigName));
      }
      if (fileSpecs == null)
      {
        throw new ArgumentNullException(nameof(fileSpecs));
      }

      GetConfigValues(dataConfigName, out _, out string connectionString
        , out string providerName);
      DataAccess dataAccess = new DataAccess(connectionString, providerName);
      foreach (string fileSpec in fileSpecs)
      {
        dataAccess.ExecuteScript(fileSpec);
      }
      retValue = true;
      return retValue;
    }

    // Gets the additional Config values.
    /// <include file='Doc/ManagerCommon.xml'
    ///  path='items/GetConfigValues/*'/>
    public static void GetConfigValues(string dataConfigName
      , out string connectionType, out string connectionString
      , out string providerName)
    {
      DataConfigs dataConfigs = new DataConfigs();
      dataConfigs.LJCLoadData();
      DataConfig dataConfig = dataConfigs.LJCGetByName(dataConfigName);
      connectionType = dataConfig.ConnectionType;
      connectionString = dataConfig.GetConnectionString();
      providerName = dataConfig.GetProviderName();
    }

    // Gets the Missing Table ErrorCode.
    /// <include file='Doc/ManagerCommon.xml'
    ///  path='items/GetMissingTableErrorCode/*'/>
    public static int GetMissingTableErrorCode(string dataConfigName)
    {
      int retValue;

      GetConfigValues(dataConfigName, out string connectionTypeName
        , out _, out _);

      switch (connectionTypeName)
      {
        case "MySQL":
          // MySQL - Table x doesn't exist.
          retValue = -2147467259;
          break;

        case "SQLServer":
          // Invalid object.
          retValue = -2146232060;
          break;

        default:
          retValue = -2146232060;
          break;
      }
      return retValue;
    }
  }
}
