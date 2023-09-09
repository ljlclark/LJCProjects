// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// ManagerTests.cs
using LJCDBClientLib;
using LJCDataAccessConfig;
using LJCDBDataAccess;
using LJCWinFormControls;
using LJCDataAccess;

namespace LJCGridDataTests
{
  // Contains the DataManagaer Grid tests.
  internal class ManagerTests
  {
    // Initializes the object instance.
    internal ManagerTests(LJCDataGrid ljcGrid)
    {
      mLJCGrid = ljcGrid;
    }

    // Runs the tests.
    internal void Run()
    {
      // Configure Grid Columns
      var dataManager = SetupManager();

      // *** Test Setting ***
      var gridCase = GridCase.ManagerGridSetup;
      switch (gridCase)
      {
        case GridCase.GridSetup:
          var gridSetup = new GridSetup(mLJCGrid);
          gridSetup.CreateColumns();
          break;

        case GridCase.ManagerGridSetup:
          var resultSetup = new ManagerSetup(mLJCGrid, dataManager);
          var gridColumns = resultSetup.CreateColumns();

          // Configure the grid columns.
          mLJCGrid.Columns.Clear();
          mLJCGrid.LJCAddColumns(gridColumns);
          break;
      }

      // Load Data
      switch (gridCase)
      {
        case GridCase.GridSetup:
          object[] values = new object[]
          {
            "Province",
            "This is a province.",
            "ZDN"
          };
          mLJCGrid.Rows.Add(values);
          break;

        case GridCase.ManagerGridSetup:
          var managerData = new ManagerData(mLJCGrid, dataManager);
          managerData.LoadRows();
          break;
      }
    }

    #region Class Data

    // Setup DataManager
    private DataManager SetupManager()
    {
      string connectionString;
      string providerName;
      DataManager retValue;

      // Create Data Configuration values.
      var databaseName = "LJCData";

      bool useInternal = false;
      if (useInternal)
      {
        // Use internal configuration.
        connectionString = DataAccess.GetConnectionString("DataServiceName"
          , databaseName);
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

      // Create DataManager.
      var dbServiceRef = new DbServiceRef()
      {
        DbDataAccess = new DbDataAccess(databaseName
          , connectionString, providerName)
      };
      var tableName = "Province";
      retValue = new DataManager(dbServiceRef, null, tableName);
      return retValue;
    }

    private readonly LJCDataGrid mLJCGrid;

    private enum GridCase
    {
      GridSetup,
      ManagerGridSetup
    }
    #endregion
  }
}
