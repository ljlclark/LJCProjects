// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// SQLTests.cs
using LJCDataAccess;
using LJCDataAccessConfig;
using LJCDBClientLib;
using LJCDBMessage;
using LJCGridDataLib;
using LJCNetCommon;
using LJCWinFormControls;
using System.Collections.Generic;
using System.Data;

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

    #region DataAccess

    // Loading Grid rows from a DataTable.
    internal void DataAccessRetrieve(DataAccess dataAccess, LJCDataGrid ljcGrid)
    {
      // Configure GridColumns.
      DataTable dataTable;
      DbColumns gridColumns = null;
      DbColumns baseDefinition = null;
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
          // Gets columns in "Select" column order.
          // If "*" gets columns in database order.
          dataTable = dataAccess.GetSchemaOnly(sql);
          baseDefinition = TableData.GetDbColumns(dataTable.Columns);
          var propertyNames = new List<string>()
          {
            { "Name" },
            { "Description" },
            { "Abbreviation" }
          };
          // Gets grid column definitions in propertyNames order.
          // If propertyNames is null, gets definitions in baseDefinition order.
          gridColumns = baseDefinition.LJCGetColumns(propertyNames);
          break;
      }
      // Sets grid column names to DbColumn PropertyNames in grid columns order.
      ljcGrid.LJCAddColumns(gridColumns);

      // Load the data.
      // The DataAccess class requires the developer
      // to create the SQL statements to pass to its methods.
      sql = "select * from Province";
      dataTable = dataAccess.GetDataTable(sql);
      if (NetCommon.HasData(dataTable))
      {
        // Create and populate the grid rows individually.
        foreach (DataRow dataRow in dataTable.Rows)
        {
          var ljcGridRow = ljcGrid.LJCRowAdd();
          // Adds the values in grid columns order.
          TableData.RowSetValues(ljcGridRow, dataRow, baseDefinition);
        }
      }
    }

    internal void GetRowDataObject(DataAccess dataAccess
      , LJCGridRow ljcGridRow)
    {
      if (ljcGridRow != null)
      {
        var name = ljcGridRow.LJCGetCellText("Name");

        var sql = $"select * from Province where Name=\'{name}\'";
        var dataTable = dataAccess.GetDataTable(sql);
        if (NetCommon.HasData(dataTable))
        {
          // Use definition for different values.
          DbColumns dataDefinition = new DbColumns()
          {
            // Property name is different than DB column name.
            { "ColumnName", "PropertyName" },
            // DataTable Column and Property name different than DB column name.
            { "ColumnName", "PropertyName", "RenameAs" }
          };

          // Create the Data Object.
          // Sets object values where DataTable column names match
          // the object property names and DataTable column row
          // values are not null.
          // If DataRow is not provided, first row is used if available.
          var converter = new ResultConverter<Province, Provinces>();
          var province = converter.CreateDataFromTable(dataTable
            , dataDefinition: dataDefinition);
        }
      }
    }
    #endregion

    #region SQLManager

    // Add a data record.
    internal Province Add(Province provider, string connectionString
      , string providerName)
    {
      Province retValue;

      // Create the SQLManager.
      var sqlManager = new SQLManager(null, "Province", connectionString
        , providerName)
      {
        DbAssignedColumns = new List<string>()
        {
          "ID"
        }
      };
      sqlManager.SetLookupColumns(new string[]
      {
        "Name"
      });

      // The data record must not contain a value for DB Assigned columns.
      provider.RegionID = 1;
      var propertyNames = new List<string>()
      {
        { "RegionID" },
        { "Name" },
        { "Description" },
        { "Abbreviation" }
      };
      DataTable dataTable = sqlManager.Add(provider, propertyNames);

      // Create the Data Object with added DB Assigned values.
      // Assumes DataTable column names same as object property names.
      // Sets object values where DataTable column names match
      // the object property names and DataTable column row
      // values are not null.
      // If DataRow is not provided, first row is used if available.
      var converter = new ResultConverter<Province, Provinces>();
      retValue = converter.CreateDataFromTable(dataTable);
      return retValue;
    }

    // Delete a data record.
    internal void Delete(string connectionString, string providerName)
    {
      // Create the SQLManager.
      var sqlManager = new SQLManager(null, "Province", connectionString
        , providerName);

      // Identify the records to be deleted.
      var keyColumns = new DbColumns()
      {
        { "ID" , 1 }
      };

      // Perform the Delete
      sqlManager.Delete(keyColumns);
    }

    // Retrieve data with keys.
    internal Province Retrieve(string connectionString
      , string providerName)
    {
      Province retValue;

      // Create the SQLManager.
      var sqlManager = new SQLManager(null, "Province", connectionString
        , providerName);

      // Identify the records and properties to be selected.
      var keyColumns = new DbColumns()
      {
        { "ID" , 1 }
      };
      var propertyNames = new List<string>()
      {
        { "Name" },
        { "Description" },
        { "Abbreviation" }
      };

      // Perform the Select
      var dataTable = sqlManager.GetDataTable(keyColumns, propertyNames);

      // Assumes DataTable column names same as object property names.
      // Sets object values where DataTable column names match
      // the object property names and DataTable column row
      // values are not null.
      // If DataRow is not provided, first row is used if available.
      var converter = new ResultConverter<Province, Provinces>();
      retValue = converter.CreateDataFromTable(dataTable);
      return retValue;
    }

    // Retrieve data using saved row values.
    internal void RetrieveWithRowValues(LJCDataGrid ljcGrid
      , string connectionString, string providerName)
    {
      // Create the SQLManager.
      var sqlManager = new SQLManager(null, "Province", connectionString
        , providerName);

      // Load the data and save the "ID" value in each row.
      var dataTable = sqlManager.GetDataTable();
      if (NetCommon.HasData(dataTable))
      {
        foreach (DataRow dataRow in dataTable.Rows)
        {
          var ljcGridRow = ljcGrid.LJCRowAdd();
          ljcGridRow.LJCSetInt32("ID", (int)dataRow["ID"]);
          TableData.RowSetValues(ljcGridRow, dataRow
            , sqlManager.BaseDefinition);
        }
      }

      // Retrieve the data with the saved row values.
      var ljcRow = ljcGrid.CurrentRow as LJCGridRow;
      var id = ljcRow.LJCGetInt32("ID");
      var keyColumns = new DbColumns()
      {
        { "ID" , id }
      };
      dataTable = sqlManager.GetDataTable(keyColumns);

      // Create the Data Object.
      // Assumes DataTable column names same as object property names.
      // Sets object values where DataTable column names match
      // the object property names and DataTable column row
      // values are not null.
      // If DataRow is not provided, first row is used if available.
      var converter = new ResultConverter<Province, Provinces>();
      var province = converter.CreateDataFromTable(dataTable);
    }

    // Retrieves data with join values.
    internal Province RetrieveWithJoins(string connectionString
      , string providerName)
    {
      Province retValue;

      // Create the SQLManager.
      var sqlManager = new SQLManager(null, "Province", connectionString
        , providerName);

      // Identify the records and properties to be selected.
      var keyColumns = new DbColumns()
      {
        { "ID" , 1 }
      };
      // Join columns are provided in DbJoins, not in propertyNames.
      var propertyNames = new List<string>()
      {
        { "Name" },
        { "RegionID" },
        { "Description" },
        { "Abbreviation" }
      };

      // Create the Join definition.
      DbJoins dbJoins = new DbJoins();
      DbJoin dbJoin = new DbJoin()
      {
        TableName = "Region",
        Columns = new DbColumns()
        {
          // PropertyName is "RegionName" as data object cannot have duplicate
          // properties.
          // RenameAs is "JoinName" as DataTable cannot have duplicate columns.
          { "Name", "RegionName", "JoinName" }
        },
        JoinOns = new DbJoinOns()
        {
          // By default the fromColumnName is qualified with the base table name
          // and the toColumnName is qualified with the join table name.
          // To override this behavior, simple include your own qualifier.
          { "RegionID", "ID" }
        }
      };
      dbJoins.Add(dbJoin);

      // Perform the Select
      var dataTable = sqlManager.GetDataTable(keyColumns, propertyNames
        , joins: dbJoins);
      //Created SQL statement - sqlManager.SQLStatement is:
      // select
      //   Province.Name,
      //   Province.Description,
      //   Province.Abbreviation,
      //   Region.Name as JoinName
      // from Province
      // left join Region
      //   on ((Province.RegionID = Region.ID))
      // where Province.ID = 1;

      // Create the Data Object.
      // Use definition for different values.
      // Must add "RegionName" property to Province to receive the join value.
      DbColumns dataDefinition = new DbColumns()
      {
        // DataTable Column and Property name different than DB column name.
        { "Region.Name", "RegionName", "JoinName" }
      };

      // Assumes DataTable column names same as object property names.
      // Sets object values where DataTable column names match
      // the object property names and DataTable column row
      // values are not null.
      // If DataRow is not provided, first row is used if available.
      var converter = new ResultConverter<Province, Provinces>();
      retValue = converter.CreateDataFromTable(dataTable
        , dataDefinition: dataDefinition);
      return retValue;
    }

    // Updating data with Filters.
    internal void UpdateWithFilters(Province province, string connectionString
      , string providerName)
    {
      // Create the SQLManager.
      var sqlManager = new SQLManager(null, "Province", connectionString
        , providerName);

      // Change Values
      province.Description = "-null";

      // Select the records and properties to be updated.
      var conditionSet = new DbConditionSet();
      // Where ID > 0
      conditionSet.Conditions.Add("ID", "0", ">");
      var filters = new DbFilters()
      {
        { "ID", conditionSet }
      };
      var propertyNames = new List<string>()
      {
        { "Description" },
      };

      // Perform the Update
      sqlManager.Update(province, null, propertyNames, filters);
    }

    // Updating data with Keys.
    internal void UpdateWithKeys(Province province, string connectionString
      , string providerName)
    {
      // Create the SQLManager.
      var sqlManager = new SQLManager(null, "Province", connectionString
        , providerName);

      // Change Values
      //province.Description = "";
      province.Description = "-null";

      // Select the records and properties to be updated.
      var keyColumns = new DbColumns()
      {
        { "ID" , province.ID }
      };
      var propertyNames = new List<string>()
      {
        { "Description" },
      };

      // Perform the Update
      sqlManager.Update(province, keyColumns, propertyNames);
    }
    #endregion

    #region ProvinceSQLManager

    // Add a data record.
    internal void AddProvince(string connectionString, string providerName)
    {
      var manager = new ProvinceSQLManager(null, null, connectionString
        , providerName);

      // Create the Data Object.
      var province = new Province()
      {
        RegionID = 1,
        Name = "ProvinceName",
        Description = "A Sample Province",
        Abbreviation = "PN"
      };
      var resultProvince = manager.Add(province);
      var addedID = resultProvince.ID;
    }

    // Populate a grid with data.
    internal void LoadProvince(LJCDataGrid ljcGrid, string connectionString
      , string providerName)
    {
      var manager = new ProvinceSQLManager(null, null, connectionString
        , providerName);

      // Create grid columns.
      ljcGrid.Columns.Clear();
      // Use DataDefinition to include join columns.
      var gridColumns = manager.DataDefinition.Clone();
      gridColumns.LJCRemoveColumn("ID");
      ljcGrid.LJCAddColumns(gridColumns);

      // Add data to the grid.
      // Join columns are provided in DbJoins, not in propertyNames.
      gridColumns.LJCRemoveColumn("Region.Name");
      var propertyNames = gridColumns.LJCGetPropertyNames();
      var dbJoins = manager.GetRegionJoins();
      var dataTable = manager.LoadDataTable(propertyNames: propertyNames
        , joins: dbJoins);
      if (NetCommon.HasData(dataTable))
      {
        // Create and load the grid rows individually.
        foreach (DataRow dataRow in dataTable.Rows)
        {
          var ljcGridRow = ljcGrid.LJCRowAdd();
          TableData.RowSetValues(ljcGridRow, dataRow, manager.DataDefinition);
        }
      }
    }

    // Retrieve a Data Object.
    internal Province RetrieveProvince(string connectionString, string providerName)
    {
      Province retValue;

      var manager = new ProvinceSQLManager(null, null, connectionString
        , providerName);

      // Identify the records to be selected.
      var keyColumns = manager.GetIDKey(1);
      retValue = manager.Retrieve(keyColumns);
      return retValue;
    }

    // Update a Data Object.
    internal void UpdateProvince(Province province, string connectionString
      , string providerName)
    {
      var manager = new ProvinceSQLManager(null, null, connectionString
        , providerName);

      province.Description = "Updated Description";

      // Identify the records to be selected.
      var keyColumns = manager.GetIDKey(1);
      manager.Update(province, keyColumns);
    }
    #endregion

    // Runs the tests.
    internal void Run()
    {
      DataAccess dataAccess = SetupSQL();

      // Configure Grid Columns
      mLJCGrid.Columns.Clear();
      string sql;
      DataTable dataTable;
      var gridColumns = new DbColumns();
      DbColumns baseDefinition = null;

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
          baseDefinition = TableData.GetDbColumns(dataTable.Columns);
          var propertyNames = new List<string>()
          {
            { "Name" },
            { "Description" },
            { "Abbreviation" }
          };
          gridColumns = baseDefinition.LJCGetColumns(propertyNames);
          break;
      }
      mLJCGrid.LJCAddColumns(gridColumns);

      // Load Data including MaxLength.
      sql = "select * from Province";
      dataTable = dataAccess.GetDataTable(sql);
      if (NetCommon.HasData(dataTable))
      {
        // Create and load the grid rows individually.
        foreach (DataRow dataRow in dataTable.Rows)
        {
          var ljcGridRow = mLJCGrid.LJCRowAdd();
          TableData.RowSetValues(ljcGridRow, dataRow, baseDefinition);
        }
      }
    }

    // Setup SQL DataAccess.
    private DataAccess SetupSQL()
    {
      DataAccess retValue = null;

      // Create Data Configuration values.
      var databaseName = "LJCData";

      // *** Test Setting ***
      var setupCase = SetupCase.External;
      switch (setupCase)
      {
        case SetupCase.Internal:
          retValue = DataAccess.GetDataAccess("DataServiceName", databaseName
            , "System.Data.SqlClient");
          break;

        case SetupCase.External:
          // Or use external configuration.
          DataConfigs dataConfigs = new DataConfigs();
          dataConfigs.LJCLoadData();
          var dataConfig = dataConfigs.LJCGetByName("LJCData");
          if (dataConfig != null)
          {
            retValue = new DataAccess()
            {
              ConnectionString = dataConfig.GetConnectionString(),
              ProviderName = "System.Data.SqlClient"
            };
          }
          break;
      }
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

  //// Represents a Province
  //public class Province
  //{
  //  /// <summary>Gets or sets the Primary key value.</summary>
  //  public int ID { get; set; }

  //  /// <summary>Gets or sets the Region ID.</summary>
  //  public int RegionID { get; set; }

  //  /// <summary>Gets or sets the Name.</summary>
  //  public string Name { get; set; }

  //  /// <summary>Gets or sets the Description.</summary>
  //  public string Description { get; set; }

  //  /// <summary>Gets or sets the Abbreviation.</summary>
  //  public string Abbreviation { get; set; }
  //}

  // Represents a Collection of Province objects.
  public class Provinces : List<Province> { }
}
