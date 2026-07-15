// Copyright (c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// TestDataModuleManager.cs
using LJCDataAccess;
using LJCDataAccessConfig;
using LJCDataUtilityDAL;
using LJCDBClientLib;
using LJCDBDataAccess;
using LJCNetCommon;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace TestDataUtilityDAL
{
  // Tests the DataTableManager object.
  internal class TestDataModuleManager
  {
    #region Constructor Methods

    // Initializes an object instance.
    /// <include file='Doc/TestDataModuleManager.xml'
    ///  path='members/Constructor/*'/>
    public TestDataModuleManager()
    {
      CreateManagersProgramConfig();
      CreateManagers();

      CreateManagersManual();
      CreateManagersNoDataConfigs();
      CreateOnlyManager();

      TestCommon = new TestCommon("TestDataModuleManager");
      Run();
    }

    // Create managers collection from program configuration Singleton.
    private void CreateManagersProgramConfig()
    {
      var values = ValuesDataUtility.Instance;
      values.SetConfigFile("LJCDataUtility.exe.config");
      var errors = values.Errors;
      if (NetString.HasValue(errors))
      {
        throw new System.Exception(errors);
      }
      var managers = values.Managers;
      var dataModuleManager = managers.DataModuleManager;
      if (dataModuleManager != null) { }
    }

    // Create managers collection using DataConfigs.xml.
    private void CreateManagers()
    {
      // See: Run() for the DataConfigs.xml file example.

      // Create managers collection with DbServiceRef from DataConfigs.xml.
      var dataConfigName = "DataUtility";
      var dbServiceRef = new DbServiceRef()
      {
        DbDataAccess = new DbDataAccess(dataConfigName),
      };
      var managers = new ManagersDataUtility();
      managers.SetDBProperties(dbServiceRef, dataConfigName);
      var dataModuleManager = managers.DataModuleManager;
      if (dataModuleManager != null) { }
    }

    // Create managers collection using DataConfigs.xml manually.
    private void CreateManagersManual()
    {
      // See: Run() for the DataConfigs.xml file example.

      var dataConfigName = "DataUtility";
      // Use DataConfigs.xml manually.
      var dataConfigs = new DataConfigs();
      dataConfigs.LJCLoadData();
      var dataConfig = dataConfigs.LJCGetByName(dataConfigName);
      var databaseName = dataConfig.Database;
      var connectionType = ConnectionType.SqlServer.ToString();
      var connectionString = dataConfig.GetConnectionString(connectionType);
      var providerName = dataConfig.GetProviderName();
      var dbServiceRef = new DbServiceRef()
      {
        DbDataAccess = new DbDataAccess(databaseName, connectionString, providerName)
      };
      var managers = new ManagersDataUtility();
      managers.SetDBProperties(dbServiceRef, dataConfigName);
      var dataModuleManager = managers.DataModuleManager;
      if (dataModuleManager != null) { }
    }

    // Create managers collection using No DataConfigs.xml.
    private void CreateManagersNoDataConfigs()
    {
      var databaseName = "LJCDataUtility";
      // databaseName = "DatabaseName";
      var builder = new SqlConnectionStringBuilder
      {
        DataSource = "DESKTOP-PDPBE34",
        //DataSource = "DbServerName";
        InitialCatalog = databaseName,
        //UserID = "userID",
        //Password = "password",
      };
      var connectionString = builder.ConnectionString;
      connectionString += "; Integrated Security=True";
      var providerName = "System.Data.SqlClient";
      var dbServiceRef = new DbServiceRef()
      {
        DbDataAccess = new DbDataAccess(databaseName, connectionString, providerName)
      };
      var managers = new ManagersDataUtility();
      managers.SetDBProperties(dbServiceRef, null);
      var dataModuleManager = managers.DataModuleManager;
      if (dataModuleManager != null) { }
    }

    // Create manager object using DataConfigs.xml.
    private void CreateOnlyManager()
    {
      // See: Run() for the DataConfigs.xml file example.

      // Create the data table manager.
      var dataConfigName = "DataUtility";
      var dbServiceRef = new DbServiceRef()
      {
        DbDataAccess = new DbDataAccess(dataConfigName),
      };
      var dataModuleManager = new DataTableManager(dbServiceRef, dataConfigName);
      if (dataModuleManager != null) { }
    }
    #endregion

    #region Methods

    // Run the tests.
    /// <include file='Doc/TestDataModuleManager.xml'
    ///  path='members/Run/*'/>
    public void Run()
    {
      // The DataConfigs.xml file example.
      //<?xml version='1.0'?>
      //<DataConfigs
      //  xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance'
      //  xmlns:xsd='http://www.w3.org/2001/XMLSchema'>
      //  <DataConfig>
      //    <Name>DataUtility</Name>
      //    <ConnectionType>SQLServer</ConnectionType>
      //    <DbServer>DBServerName</DbServer>
      //    <Database>DatabaseName</Database>
      //  </DataConfig >
      //</DataConfigs>

      CleanUp();

      // DataTable Data Methods
      Add();
      Delete();
      Load();
      Retrieve();
      Update();

      // Custom Data Methods
      RetrieveWithID();
      RetrieveWithUnique();

      // Get Key Methods
      IDKey();
      UniqueKey();

      // DataTable Info Methods
      Columns();
      PropertyNames();
    }

    // Remove the test record.
    private void CleanUp()
    {
      var methodName = "CleanUp()";

      // Create the data table manager.
      var dataConfigName = "DataUtility";
      var dbServiceRef = new DbServiceRef()
      {
        DbDataAccess = new DbDataAccess(dataConfigName),
      };
      var dataModuleManager = new DataModuleManager(dbServiceRef
        , dataConfigName);

      string name = "TestModuleName";

      // Delete the test record.
      var uniqueColumns = new DbColumns()
      {
        { "Name", (object)name },
      };
      dataModuleManager.Delete(uniqueColumns);

      // Verify the test record does not exist.
      var utilTable = dataModuleManager.Retrieve(uniqueColumns);
      var result = "HasObject";
      if (null == utilTable)
      {
        result = null;
      }
      var compare = "No Result";
      TestCommon.Write($"{methodName}1", result, compare);
    }
    #endregion

    #region Manager Methods

    // Creates a collection of columns that match the supplied list.
    private void Columns()
    {
      var methodName = "Columns()";

      // See: Run() for the DataConfigs.xml file example.

      // Create the data table manager.
      var dataConfigName = "DataUtility";
      var dbServiceRef = new DbServiceRef()
      {
        DbDataAccess = new DbDataAccess(dataConfigName),
      };
      var dataModuleManager = new DataModuleManager(dbServiceRef
        , dataConfigName);

      var propertyNames = new List<string>()
      {
        "Name",
      };

      // Test Method
      var columns = dataModuleManager.Columns(propertyNames);

      var column = columns["Name"];
      var result = column.PropertyName;
      var compare = "Name";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Creates a list of BaseDefinition property names.
    private void PropertyNames()
    {
      var methodName = "PropertyNames()";

      // See: Run() for the DataConfigs.xml file example.

      // Create the data table manager.
      var dataConfigName = "DataUtility";
      var dbServiceRef = new DbServiceRef()
      {
        DbDataAccess = new DbDataAccess(dataConfigName),
      };
      var dataModuleManager = new DataModuleManager(dbServiceRef
        , dataConfigName);

      // Test Method
      var names = dataModuleManager.PropertyNames();
      var result = names[2];
      var compare = "ID";
      TestCommon.Write($"{methodName}", result, compare);
    }
    #endregion

    #region Data Methods

    // Adds a record to the database.
    private void Add()
    {
      string methodName = "Add()";

      // See: Run() for the DataConfigs.xml file example.

      // Create the data table manager.
      var dataConfigName = "DataUtility";
      var dbServiceRef = new DbServiceRef()
      {
        DbDataAccess = new DbDataAccess(dataConfigName),
      };
      var dataModuleManager = new DataModuleManager(dbServiceRef
        , dataConfigName);

      long dataSiteID = 1;
      string name = "TestModuleName";

      // Create the test record.
      var dataModule = new DataModule()
      {
        DataSiteID = dataSiteID,
        Name = name,
        Description = "The test table.",
      };

      // Test Method
      dataModuleManager.Add(dataModule);

      // Verify the test record was created.
      var uniqueColumns = new DbColumns()
      {
        { "Name", (object)name },
      };
      dataModule = dataModuleManager.Retrieve(uniqueColumns);
      var result = dataModule.Name;
      var compare = "TestModuleName";
      TestCommon.Write($"{methodName}", result, compare);

      // Delete the test record.
      dataModuleManager.Delete(uniqueColumns);
    }

    // Deletes the records with the specified key values.
    private void Delete()
    {
      string methodName = "Delete()";

      // See: Run() for the DataConfigs.xml file example.

      // Create the data table manager.
      var dataConfigName = "DataUtility";
      var dbServiceRef = new DbServiceRef()
      {
        DbDataAccess = new DbDataAccess(dataConfigName),
      };
      var dataModuleManager = new DataModuleManager(dbServiceRef
        , dataConfigName);

      string name = "TestModuleName";

      // Create the test record.
      var uniqueColumns = new DbColumns()
      {
        { "Name", (object)name },
      };
      var dataModule = dataModuleManager.Retrieve(uniqueColumns);
      if (dataModule != null)
      {
        var result = dataModule.Name;
        var compare = "TestModuleName";
        TestCommon.Write($"{methodName}1", result, compare);

        // Test Method
        dataModuleManager.Delete(uniqueColumns);

        // Verify the test record was deleted.
        dataModule = dataModuleManager.Retrieve(uniqueColumns);
        if (null == dataModule)
        {
          result = null;
        }
        compare = "No Result";
        TestCommon.Write($"{methodName}2", result, compare);
      }
    }

    // Retrieves a collection of data records.
    private void Load()
    {
      var methodName = "Load()";

      // See: Run() for the DataConfigs.xml file example.

      // Create the data table manager.
      var dataConfigName = "DataUtility";
      var dbServiceRef = new DbServiceRef()
      {
        DbDataAccess = new DbDataAccess(dataConfigName),
      };
      var dataModuleManager = new DataModuleManager(dbServiceRef
        , dataConfigName);

      long dataSiteID = 1;
      string name = "TestModuleName";

      // Create the test record.
      var dataModule = new DataModule()
      {
        DataSiteID = dataSiteID,
        Name = name,
        Description = "The test table.",
      };
      dataModuleManager.Add(dataModule);

      // Test Method
      var dataModules = dataModuleManager.Load();

      // Verify the records were loaded.
      var result = "";
      if (dataModules.Count > 0)
      {
        result = "HasItems";
      }
      var compare = "HasItems";
      TestCommon.Write($"{methodName}", result, compare);

      // Delete the test record.
      var uniqueColumns = new DbColumns()
      {
        { "Name", (object)name },
      };
      dataModuleManager.Delete(uniqueColumns);
    }

    // Retrieves a record from the database.
    private void Retrieve()
    {
      var methodName = "Retrieve()";

      // See: Run() for the DataConfigs.xml file example.

      // Create the data table manager.
      var dataConfigName = "DataUtility";
      var dbServiceRef = new DbServiceRef()
      {
        DbDataAccess = new DbDataAccess(dataConfigName),
      };
      var dataModuleManager = new DataModuleManager(dbServiceRef
        , dataConfigName);

      long dataSiteID = 1;
      string name = "TestModuleName";

      // Create the test record.
      var dataModule = new DataModule()
      {
        DataSiteID = dataSiteID,
        Name = name,
        Description = "The test table.",
      };
      dataModuleManager.Add(dataModule);

      // Verify the test record was created.
      var uniqueColumns = new DbColumns()
      {
        { "Name", (object)name },
      };

      // Test Method
      dataModule = dataModuleManager.Retrieve(uniqueColumns);

      var result = dataModule.Name;
      var compare = "TestModuleName";
      TestCommon.Write($"{methodName}", result, compare);

      // Delete the test record.
      dataModuleManager.Delete(uniqueColumns);
    }

    // Updates the record.
    private void Update()
    {
      var methodName = "Update()";

      // See: Run() for the DataConfigs.xml file example.

      // Create the data table manager.
      var dataConfigName = "DataUtility";
      var dbServiceRef = new DbServiceRef()
      {
        DbDataAccess = new DbDataAccess(dataConfigName),
      };
      var dataModuleManager = new DataModuleManager(dbServiceRef
        , dataConfigName);

      short dbID = 1;
      string name = "TestModuleName";

      // Create the test record.
      var dataModule = new DataModule()
      {
        DataSiteID = dbID,
        Name = name,
        Description = "The test table.",
      };
      dataModuleManager.Add(dataModule);

      // Verify the test record was created.
      var uniqueColumns = new DbColumns()
      {
        { "Name", (object)name },
      };
      dataModule = dataModuleManager.Retrieve(uniqueColumns);
      var result = dataModule.Name;
      var compare = "TestModuleName";
      TestCommon.Write($"{methodName}1", result, compare);

      // Update the test record.
      var id = dataModule.ID;
      dbID = (short)dataModule.DataSiteID;
      var idKey = dataModuleManager.IDKey(id, dbID);
      dataModule.Description += " Updated";
      var propertyNames = new List<string>()
      {
        "Description",
      };

      // Test Method
      dataModuleManager.Update(dataModule, idKey, propertyNames);

      // Verify the test record was updated.
      dataModule = dataModuleManager.RetrieveWithID(id, dbID);
      result = dataModule.Description;
      compare = "The test table. Updated";
      TestCommon.Write($"{methodName}2", result, compare);

      // Delete the test record.
      dataModuleManager.Delete(uniqueColumns);
    }
    #endregion

    #region Custom Data Methods

    // Retrieves a record with the supplied ID values.
    private void RetrieveWithID()
    {
      var methodName = "RetrieveWithID()";

      // See: Run() for the DataConfigs.xml file example.

      // Create the data table manager.
      var dataConfigName = "DataUtility";
      var dbServiceRef = new DbServiceRef()
      {
        DbDataAccess = new DbDataAccess(dataConfigName),
      };
      var dataModuleManager = new DataModuleManager(dbServiceRef
        , dataConfigName);

      short dbID = 1;
      string name = "TestModuleName";

      // Create the test record.
      var dataModule = new DataModule()
      {
        DataSiteID = dbID,
        Name = name,
        Description = "The test table.",
      };
      dataModuleManager.Add(dataModule);

      // Verify the test record was created.
      var uniqueColumns = new DbColumns()
      {
        { "Name", (object)name },
      };
      dataModule = dataModuleManager.Retrieve(uniqueColumns);

      var result = dataModule.Name;
      var compare = "TestModuleName";
      TestCommon.Write($"{methodName}1", result, compare);

      // Retrieve with ID.
      var id = dataModule.ID;

      // Test Method
      dataModule = dataModuleManager.RetrieveWithID(id, dbID);

      // Verify the record was retrieved.
      result = dataModule.Name;
      compare = "TestModuleName";
      TestCommon.Write($"{methodName}2", result, compare);

      // Delete the test record.
      dataModuleManager.Delete(uniqueColumns);
    }

    // Retrieves a record with the supplied unique values.
    private void RetrieveWithUnique()
    {
      var methodName = "RetrieveWithUnique()";

      // See: Run() for the DataConfigs.xml file example.

      // Create the data table manager.
      var dataConfigName = "DataUtility";
      var dbServiceRef = new DbServiceRef()
      {
        DbDataAccess = new DbDataAccess(dataConfigName),
      };
      var dataModuleManager = new DataModuleManager(dbServiceRef
        , dataConfigName);

      short dbID = 1;
      string name = "TestModuleName";

      // Create the test record.
      var dataModule = new DataModule()
      {
        DataSiteID = dbID,
        Name = name,
        Description = "The test table.",
      };
      dataModuleManager.Add(dataModule);

      // Test Method
      dataModule = dataModuleManager.RetrieveWithUnique(name);

      var result = dataModule.Name;
      var compare = "TestModuleName";
      TestCommon.Write($"{methodName}", result, compare);

      // Delete the test record.
      var uniqueColumns = new DbColumns()
      {
        { "Name", (object)name },
      };
      dataModuleManager.Delete(uniqueColumns);
    }
    #endregion

    #region GetKey Methods

    // Gets the ID key columns.
    private void IDKey()
    {
      var methodName = "IDKey()";

      // See: Run() for the DataConfigs.xml file example.

      // Create the data table manager.
      var dataConfigName = "DataUtility";
      var dbServiceRef = new DbServiceRef()
      {
        DbDataAccess = new DbDataAccess(dataConfigName),
      };
      var dataModuleManager = new DataModuleManager(dbServiceRef
        , dataConfigName);

      long id = 1;
      short dbID = 1;

      // Test Method
      var idKey = dataModuleManager.IDKey(id, dbID);

      var key = idKey.LJCSearchPropertyName(DataModule.ColumnID);
      var propertyName = key.PropertyName;
      var value = key.Value;
      var result = $"{propertyName}: {value}";
      var compare = "ID: 1";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Gets the Unique ID key columns.
    private void UniqueKey()
    {
      var methodName = "UniqueKey()";

      // See: Run() for the DataConfigs.xml file example.

      // Create the data table manager.
      var dataConfigName = "DataUtility";
      var dbServiceRef = new DbServiceRef()
      {
        DbDataAccess = new DbDataAccess(dataConfigName),
      };
      var dataModuleManager = new DataModuleManager(dbServiceRef
        , dataConfigName);

      string name = "TestModuleName";

      // Test Method
      var uniqueKey = dataModuleManager.UniqueKey(name);

      var key = uniqueKey.LJCSearchPropertyName(DataModule.ColumnName);
      var propertyName = key.PropertyName;
      var value = key.Value;
      var result = $"{propertyName}: {value}";
      var compare = "Name: TestModuleName";
      TestCommon.Write($"{methodName}", result, compare);
    }
    #endregion

    #region Properties

    // Gets or sets the TestCommon reference.
    private TestCommon TestCommon { get; set; }
    #endregion
  }
}
