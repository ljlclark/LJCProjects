// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// SQLTests.cs
using LJCDataAccess;
using LJCDataAccessConfig;
using LJCGridDataLib;
using LJCNetCommon;
using LJCWinFormControls;
using System.Collections.Generic;
using System.Data.Common;
using DbColumn = LJCNetCommon.DbColumn;

namespace LJCGridDataTests
{
  // Contains the SQL Grid tests.
  internal class SQLTests
  {
    // Initializes the object instance.
    internal SQLTests(LJCDataGrid ljcGrid)
    {
      mLJCGrid = ljcGrid;
    }

    // Runs the tests.
    internal void Run()
    {
      DataAccess dataAccess = SetupSQL();
      var sql = "select * from Province";
      var dataTable = dataAccess.GetSchemaOnly(sql);

      // Configure Grid Columns
      mLJCGrid.Columns.Clear();
      var gridColumns = new DbColumns();

      // *** Test Setting ***
      var setupCase = ColumnsCase.FromTable;
      switch (setupCase)
      {
        case ColumnsCase.FromColumns:
          gridColumns.Add("Name", maxLength: 60);
          gridColumns.Add("Description", maxLength: 100);
          gridColumns.Add("Abbreviation", maxLength: 3);
          break;

        case ColumnsCase.FromTable:
          var dbColumns = TableGridData.GetDbColumns(dataTable.Columns);
          var propertyNames = new List<string>()
          {
            { "Name" },
            { "Description" },
            { "Abbreviation" }
          };
          gridColumns = dbColumns.LJCGetColumns(propertyNames);
          break;
      }
      mLJCGrid.LJCAddColumns(gridColumns);

      // Load Data including MaxLength.
      dataAccess.FillDataTable(sql, dataTable);
      var sqlData = new SQLData(mLJCGrid, dataTable);

      // *** Test Setting ***
      var dataCase = DataCase.WithLoad;
      var tableGridData = new LJCGridDataLib.TableGridData(mLJCGrid);
      switch (dataCase)
      {
        case DataCase.WithLoad:
          sqlData.LoadRows(tableGridData);
          break;

        case DataCase.WithAdd:
          sqlData.RowAdd(tableGridData);
          break;

        case DataCase.WithValues:
          sqlData.RowSetValues(tableGridData);
          break;
      }
    }

    // Setup SQL DataAccess.
    private DataAccess SetupSQL()
    {
      string connectionString = null;
      DataAccess retValue;

      // Create Data Configuration values.
      var databaseName = "LJCData";

      // *** Test Setting ***
      var setupCase = SetupCase.External;
      switch (setupCase)
      {
        case SetupCase.Internal:
          // Use internal configuration.
          DbConnectionStringBuilder connectionBuilder;
          connectionBuilder = new DbConnectionStringBuilder()
          {
            { "Data Source", "DataServiceName" },
            { "Initial Catalog", databaseName },
            { "Integrated Security", "True" }
          };
          connectionString = connectionBuilder.ConnectionString;
          break;

        case SetupCase.External:
          // Or use external configuration.
          var configName = "LJCData";
          DataConfigs dataConfigs = new DataConfigs();
          dataConfigs.LJCLoadData();
          var dataConfig = dataConfigs.LJCGetByName(configName);
          connectionString = dataConfig.GetConnectionString();
          break;
      }
      var providerName = "System.Data.SqlClient";

      // Create DataAccess.
      retValue = new DataAccess()
      {
        ConnectionString = connectionString,
        ProviderName = providerName
      };
      return retValue;
    }

    #region Class Data

    private readonly LJCDataGrid mLJCGrid;

    private enum ColumnsCase
    {
      FromColumns,
      FromTable
    }

    private enum DataCase
    {
      WithLoad,
      WithAdd,
      WithValues
    }

    private enum SetupCase
    {
      Internal,
      External
    }
    #endregion
  }
}
