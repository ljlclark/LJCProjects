// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// SQLTests.cs
using LJCDataAccess;
using LJCDataAccessConfig;
using LJCDBClientLib;
using LJCDBDataAccess;
using LJCDBMessage;
using LJCGridDataLib;
using LJCNetCommon;
using LJCWinFormControls;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Reflection;

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

    internal static void DataRetrieve(LJCDataGrid ljcGrid)
    {
      // Configure DataAccess using internal configuration.
      DbConnectionStringBuilder connectionBuilder;
      connectionBuilder = new DbConnectionStringBuilder()
      {
        { "Data Source", "DESKTOP-PDPBE34" },
        { "Initial Catalog", "LJCData" },
        { "Integrated Security", "True" }
      };
      var connectionString = connectionBuilder.ConnectionString;
      var providerName = "System.Data.SqlClient";
      var dataAccess = new DataAccess(connectionString, providerName);

      // Configure GridColumns.
      DataTable dataTable;
      DbColumns gridColumns = null;
      string sql;

      // *** Test Setting ***
      //var configType = "Manually";
      var configType = "FromTableColumns";
      switch (configType)
      {
        case "Manually":
          // Create Grid Columns manually.
          gridColumns = new DbColumns();
          gridColumns.Add("Name", maxLength: 60);
          gridColumns.Add("Description", maxLength: 100);
          gridColumns.Add("Abbreviation", maxLength: 3);
          break;

        case "FromTableColumns":
          // Create Grid Columns from DataTable.Columns.
          sql = "select * from Province";
          dataTable = dataAccess.GetSchemaOnly(sql);
          var baseDefinition = TableData.GetDbColumns(dataTable.Columns);
          var propertyNames = new List<string>()
          {
            { "Name" },
            { "Description" },
            { "Abbreviation" }
          };
          gridColumns = baseDefinition.LJCGetColumns(propertyNames);
          break;
      }
      ljcGrid.LJCAddColumns(gridColumns);

      // Load the data.
      sql = "select * from Province";
      dataTable = dataAccess.GetSchemaOnly(sql);
      dataAccess.FillDataTable(sql, dataTable);
      if (NetCommon.HasData(dataTable))
      {
        // Create and load the grid rows individually.
        foreach (DataRow dataRow in dataTable.Rows)
        {
          var ljcGridRow = ljcGrid.LJCRowAdd();
          TableData.RowSetValues(ljcGridRow, dataRow);
        }
      }

      // Create the typed objects.
      var converter = new ResultConverter<Province, Provinces>();
      var provinces = converter.CreateCollectionFromTable(dataTable);
      var province = converter.CreateDataFromTable(dataTable);

      // Change Values
      //province.Description = "";
      province.Description = "-null";

      // Create the SQLManager.
      var dataConfigName = "LJCData";
      var sqlManager = new SQLManager(dataConfigName, "Province");

      // Select the records and properties to be updated.
      //var keyColumns = new DbColumns()
      //{
      //  { "ID" , province.ID }
      //};
      var condition = new DbCondition()
      {
        FirstValue = "ID",
        ComparisonOperator = ">",
        SecondValue = "0"
      };
      var conditionSet = new DbConditionSet();
      conditionSet.Conditions.Add(condition);
      var filters = new DbFilters()
      {
        { "ID", conditionSet }
      };
      var xpropertyNames = new List<string>()
      {
        { "Description" },
      };

      // Perform the Update
      //sqlManager.Update(province, keyColumns, xpropertyNames);
      sqlManager.Update(province, null, xpropertyNames, filters);
    }

    private class Province
    {
      public long ID { get; set; }

      public string Name { get; set; }

      public string Description { get; set; }

      public string Abbreviation { get; set; }
    }

    private class Provinces : List<Province> { }

    // Runs the tests.
    internal void Run()
    {
      DataAccess dataAccess = SetupSQL();

      // Configure Grid Columns
      mLJCGrid.Columns.Clear();
      string sql;
      DataTable dataTable;
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
          sql = "select * from Province";
          dataTable = dataAccess.GetSchemaOnly(sql);
          var dbColumns = TableData.GetDbColumns(dataTable.Columns);
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
      sql = "select * from Province";
      dataTable = dataAccess.GetSchemaOnly(sql);
      dataAccess.FillDataTable(sql, dataTable);
      if (NetCommon.HasData(dataTable))
      {
        // Create and load the grid rows individually.
        foreach (DataRow dataRow in dataTable.Rows)
        {
          var ljcGridRow = mLJCGrid.LJCRowAdd();
          TableData.RowSetValues(ljcGridRow, dataRow);
        }
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

    private enum SetupCase
    {
      Internal,
      External
    }
    #endregion
  }
}
