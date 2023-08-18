// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// SQLTests.cs
using LJCDataAccess;
using LJCDataAccessConfig;
using LJCGridDataLib;
using LJCWinFormControls;
using System.Collections.Generic;
using System.Data.Common;

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

      // Configure Grid Columns
      mLJCGrid.Columns.Clear();

      DataAccess dataAccess = SetupSQL();
      var sql = "select * from Province";
      var dataTable = dataAccess.GetSchemaOnly(sql);
      var definition = TableGridData.GetDbColumns(dataTable.Columns);
      var propertyNames = new List<string>()
      {
        { "Name" },
        { "Description" },
        { "Abbreviation" }
      };
      var gridColumns = definition.LJCGetColumns(propertyNames);
      mLJCGrid.LJCAddDisplayColumns(gridColumns);

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
      string connectionString;
      string providerName;
      DataAccess retValue;

      // Create Data Configuration values.
      //var databaseName = "DatabaseName";
      var databaseName = "LJCData";

      bool useInternal = false;
      if (useInternal)
      {
        // Use internal configuration.
        DbConnectionStringBuilder connectionBuilder;
        connectionBuilder = new DbConnectionStringBuilder()
        {
          { "Data Source", "DataServiceName" },
          { "Initial Catalog", databaseName },
          { "Integrated Security", "True" }
        };

        connectionString = connectionBuilder.ConnectionString;
        providerName = "System.Data.SqlClient";
      }
      else
      {
        // Or use external configuration.
        var configName = "LJCData";
        DataConfigs dataConfigs = new DataConfigs();
        dataConfigs.LJCLoadData();
        var dataConfig = dataConfigs.LJCGetByName(configName);

        connectionString = dataConfig.GetConnectionString();
        providerName = dataConfig.GetProviderName();
      }

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

    private enum SetupCase
    {
      FromTable,
      FromDataObject
    }

    private enum DataCase
    {
      WithLoad,
      WithAdd,
      WithValues
    }
    #endregion
  }
}
