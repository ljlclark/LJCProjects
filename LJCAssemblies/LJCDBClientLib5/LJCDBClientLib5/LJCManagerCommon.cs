// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ManagerCommon.cs
using LJCDataAccess5;
using LJCDataAccessConfig5;
using LJCDBMessage5;
using LJCNetCommon5;
using System;

namespace LJCDBClientLib5
{
  /// <summary>Contains common static manager methods.</summary>
  public class LJCManagerCommon
  {
    // Creates the LJCDBRequest object.
    /// <include path='items/CreateRequest/*' file='Doc/ManagerCommon.xml'/>
    public static LJCDBRequest CreateRequest(RequestType requestType
      , string? tableName, LJCDataColumns? requestColumns
      , string dataConfigName, string schemaName
      , LJCDataColumns? keyColumns = null, LJCDBFilters? filters = null
      , LJCDBJoins? joins = null)
    {
      LJCDBRequest retValue;

      // Create a request with the retrieve columns.
      // The retrieved columns should include the DB assigned columns.
      retValue = new LJCDBRequest(requestType, tableName)
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
    /// <include path='items/CreateTables/*' file='Doc/ManagerCommon.xml'/>
    public static bool CreateTables(string dataConfigName, string[] fileSpecs)
    {
      bool retValue = false;

      if (!LJC.HasValue(dataConfigName))
      {
        throw new ArgumentException("message", nameof(dataConfigName));
      }
      //if (fileSpecs == null)
      //{
      //  throw new ArgumentNullException(nameof(fileSpecs));
      //}
      ArgumentNullException.ThrowIfNull(fileSpecs);

      GetConfigValues(dataConfigName, out _, out string? connectionString
        , out string providerName);
      if (LJC.HasValue(connectionString))
      {
        var dataAccess = new LJCDataAccess(connectionString, providerName);
        foreach (string fileSpec in fileSpecs)
        {
          dataAccess.ExecuteScript(fileSpec);
        }
        retValue = true;
      }
      return retValue;
    }

    // Gets the additional Config values.
    /// <include path='items/GetConfigValues/*' file='Doc/ManagerCommon.xml'/>
    public static void GetConfigValues(string dataConfigName
      , out string? connectionType, out string? connectionString
      , out string providerName)
    {
      var dataConfigs = new LJCDataConfigs();
      dataConfigs.LoadData();
      LJCDataConfig dataConfig = dataConfigs.Retrieve(dataConfigName);
      connectionType = dataConfig.ConnectionType;
      connectionString = dataConfig.ConnectionString(connectionType);
      providerName = LJCDataConfig.ProviderName(connectionType);
    }

    // Gets the Missing Table ErrorCode.
    /// <include path='items/GetMissingTableErrorCode/*' file='Doc/ManagerCommon.xml'/>
    public static int GetMissingTableErrorCode(string dataConfigName)
    {
      int retValue;

      GetConfigValues(dataConfigName, out string? connectionTypeName
        , out _, out _);

#pragma warning disable IDE0066 // Convert switch statement to expression
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
#pragma warning restore IDE0066 // Convert switch statement to expression
      return retValue;
    }
  }
}
