// Copyright (c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// TestDataModuleManager5.cs
using LJCDataAccess5;
using LJCDataAccessConfig5;
using LJCDataUtilityDAL5;
using LJCDBClientLib5;
using LJCDBDataAccess5;
using LJCNetCommon5;
using Microsoft.Data.SqlClient;
using System.Data;

namespace TestDataUtilityDAL5
{
  // Tests the DataTableManager object.
  internal class TestDataModuleManager
  {
    #region Properties

    // Gets or sets the TestCommon reference.
    private LJCTestCommon TestCommon { get; set; }
    #endregion

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

      TestCommon = new LJCTestCommon("TestDataModuleManager");
      Run();
    }

    // Create managers collection from program configuration Singleton.
    private static void CreateManagersProgramConfig()
    {
      var values = ValuesDataUtility.Instance;
      values.SetConfigFile("LJCDataUtility.exe.config");
      var errors = values.Errors;
      if (LJC.HasText(errors))
      {
        throw new System.Exception(errors);
      }
      var managers = values.Managers;
      var dataModuleManager = managers.DataModuleManager;
      if (dataModuleManager != null) { }
    }

    // Create managers collection using DataConfigs.xml.
    private static void CreateManagers()
    {
      // See: Run() for the DataConfigs.xml file example.

      // Create managers collection with DbServiceRef from DataConfigs.xml.
      var dataConfigName = "DataUtility";
      var dbServiceRef = new LJCDbServiceRef()
      {
        DbDataAccess = new DbDataAccess(dataConfigName),
      };
      var managers = new ManagersDataUtility(dataConfigName, dbServiceRef);
      var dataModuleManager = managers.DataModuleManager;
      if (dataModuleManager != null) { }
    }

    // Create managers collection using DataConfigs.xml manually.
    private static void CreateManagersManual()
    {
      // See: Run() for the DataConfigs.xml file example.

      var dataConfigName = "DataUtility";
      // Use DataConfigs.xml manually.
      var dataConfigs = new LJCDataConfigs();
      dataConfigs.LoadData();
      var dataConfig = dataConfigs.Retrieve(dataConfigName);
      var databaseName = dataConfig.Database;
      var connectionType = ConnectionType.SqlServer.ToString();
      var connectionString = dataConfig.ConnectionString(connectionType);
      var providerName = LJCDataConfig.ProviderName();

      if (LJC.HasText(databaseName))
      {
        var dbServiceRef = new LJCDbServiceRef()
        {
          DbDataAccess = new DbDataAccess(databaseName, connectionString
          , providerName)
        };
        var managers = new ManagersDataUtility(dataConfigName, dbServiceRef);
        var dataModuleManager = managers.DataModuleManager;
        if (dataModuleManager != null) { }
      }
    }

    // Create managers collection using No DataConfigs.xml.
    private static void CreateManagersNoDataConfigs()
    {
      var databaseName = "LJCDataUtility";
      // databaseName = "DatabaseName";
      // using Microsoft.Data.SqlClient;
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
      //var providerName = "System.Data.SqlClient";
      var providerName = "Microsoft.Data.SqlClient";
      var dbServiceRef = new LJCDbServiceRef()
      {
        DbDataAccess = new DbDataAccess(databaseName, connectionString, providerName)
      };
      var managers = new ManagersDataUtility("", dbServiceRef);
      var dataModuleManager = managers.DataModuleManager;
      if (dataModuleManager != null) { }
    }

    // Create manager object using DataConfigs.xml.
    private static void CreateOnlyManager()
    {
      // See: Run() for the DataConfigs.xml file example.

      // Create the data table manager.
      var dataConfigName = "DataUtility";
      var dbServiceRef = new LJCDbServiceRef()
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
      RetrieveWithId();
      RetrieveWithUnique();

      // Get Key Methods
      IdKey();
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
      var dbServiceRef = new LJCDbServiceRef()
      {
        DbDataAccess = new DbDataAccess(dataConfigName),
      };
      var dataModuleManager = new DataModuleManager(dbServiceRef
        , dataConfigName);

      string name = "TestModuleName";

      // Delete the test record.
      var uniqueColumns = new LJCDataColumns()
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
      var dbServiceRef = new LJCDbServiceRef()
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

      var result = "";
      if (LJC.HasListItems(columns))
      {
        var column = columns["Name"];
        if (column != null)
        {
          result = column.PropertyName;
        }
      }
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
      var dbServiceRef = new LJCDbServiceRef()
      {
        DbDataAccess = new DbDataAccess(dataConfigName),
      };
      var dataModuleManager = new DataModuleManager(dbServiceRef
        , dataConfigName);

      // Test Method
      var names = dataModuleManager.PropertyNames();

      var result = "";
      if (LJC.HasListItems(names))
      {
        result = names[2];
      }
      var compare = "Id";
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
      var dbServiceRef = new LJCDbServiceRef()
      {
        DbDataAccess = new DbDataAccess(dataConfigName),
      };
      var dataModuleManager = new DataModuleManager(dbServiceRef
        , dataConfigName);

      short dbId = dataModuleManager.DbId;
      string name = "TestModuleName";

      // Create the test record.
      var dataModule = new DataModule()
      {
        DbId = dbId,
        Name = name,
        Description = "The test table.",
      };

      // Test Method
      dataModuleManager.Add(dataModule);

      // Verify the test record was created.
      var uniqueColumns = new LJCDataColumns()
      {
        { "Name", (object)name },
      };
      dataModule = dataModuleManager.Retrieve(uniqueColumns);
      var result = "";
      if (dataModule != null)
      {
        result = dataModule.Name;
      }
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
      var dbServiceRef = new LJCDbServiceRef()
      {
        DbDataAccess = new DbDataAccess(dataConfigName),
      };
      var dataModuleManager = new DataModuleManager(dbServiceRef
        , dataConfigName);

      string name = "TestModuleName";

      // Create the test record.
      var uniqueColumns = new LJCDataColumns()
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
      var dbServiceRef = new LJCDbServiceRef()
      {
        DbDataAccess = new DbDataAccess(dataConfigName),
      };
      var dataModuleManager = new DataModuleManager(dbServiceRef
        , dataConfigName);

      short dbId = dataModuleManager.DbId;
      string name = "TestModuleName";

      // Create the test record.
      var dataModule = new DataModule()
      {
        DbId = dbId,
        Name = name,
        Description = "The test table.",
      };
      dataModuleManager.Add(dataModule);

      // Test Method
      var dataModules = dataModuleManager.Load();

      // Verify the records were loaded.
      var result = "";
      if (LJC.HasListItems(dataModules))
      {
        result = "HasItems";
      }
      var compare = "HasItems";
      TestCommon.Write($"{methodName}", result, compare);

      // Delete the test record.
      var uniqueColumns = new LJCDataColumns()
      {
        { "Name", name },
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
      var dbServiceRef = new LJCDbServiceRef()
      {
        DbDataAccess = new DbDataAccess(dataConfigName),
      };
      var dataModuleManager = new DataModuleManager(dbServiceRef
        , dataConfigName);

      short dbId = dataModuleManager.DbId;
      string name = "TestModuleName";

      // Create the test record.
      var dataModule = new DataModule()
      {
        DbId = dbId,
        Name = name,
        Description = "The test table.",
      };
      dataModuleManager.Add(dataModule);

      // Verify the test record was created.
      var uniqueColumns = new LJCDataColumns()
      {
        { "Name", (object)name },
      };

      // Test Method
      dataModule = dataModuleManager.Retrieve(uniqueColumns);

      var result = "";
      if (dataModule != null)
      {
        result = dataModule.Name;
      }
      var compare = name;
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
      var dbServiceRef = new LJCDbServiceRef()
      {
        DbDataAccess = new DbDataAccess(dataConfigName),
      };
      var dataModuleManager = new DataModuleManager(dbServiceRef
        , dataConfigName);

      short dbId = dataModuleManager.DbId;
      string name = "TestModuleName";

      // Create the test record.
      var dataModule = new DataModule()
      {
        DbId = dbId,
        Name = name,
        Description = "The test table.",
      };
      dataModuleManager.Add(dataModule);

      // Verify the test record was created.
      var uniqueColumns = new LJCDataColumns()
        {
          { "Name", name },
        };
      dataModule = dataModuleManager.Retrieve(uniqueColumns);
      var result = "";
      if (dataModule != null)
      {
        result = dataModule.Name;
      }
      var compare = name;
      TestCommon.Write($"{methodName}1", result, compare);

      while (true)
      {
        if (null == dataModule)
        {
          break;
        }

        // Update the test record.
        dbId = dataModule.DbId;
        var id = dataModule.Id;
        var idKey = DataModuleManager.IdKey(dbId, id);
        dataModule.Description += " Updated";
        var propertyNames = new List<string>()
        {
          "Description",
        };

        // Test Method
        dataModuleManager.Update(dataModule, idKey, propertyNames);

        // Verify the test record was updated.
        dataModule = dataModuleManager.RetrieveWithId(dbId, id);
        result = "";
        if (dataModule != null)
        {
          result = dataModule.Description;
        }
        compare = "The test table. Updated";
        TestCommon.Write($"{methodName}2", result, compare);

        // Delete the test record.
        dataModuleManager.Delete(uniqueColumns);
        break;
      }
    }
    #endregion

    #region Custom Data Methods

    // Retrieves a record with the supplied ID values.
    private void RetrieveWithId()
    {
      var methodName = "RetrieveWithId()";

      // See: Run() for the DataConfigs.xml file example.

      // Create the data table manager.
      var dataConfigName = "DataUtility";
      var dbServiceRef = new LJCDbServiceRef()
      {
        DbDataAccess = new DbDataAccess(dataConfigName),
      };
      var dataModuleManager = new DataModuleManager(dbServiceRef
        , dataConfigName);

      short dbId = dataModuleManager.DbId;
      string name = "TestModuleName";

      // Create the test record.
      var dataModule = new DataModule()
      {
        DbId = dbId,
        Name = name,
        Description = "The test table.",
      };
      dataModuleManager.Add(dataModule);

      // Verify the test record was created.
      var uniqueColumns = new LJCDataColumns()
      {
        { "Name", (object)name },
      };
      dataModule = dataModuleManager.Retrieve(uniqueColumns);

      var result = "";
      if (dataModule != null)
      {
        result = dataModule.Name;
      }
      var compare = name;
      TestCommon.Write($"{methodName}1", result, compare);

      while (true)
      {
        if (null == dataModule)
        {
          break;
        }

        // Retrieve with ID.
        var id = dataModule.Id;

        // Test Method
        dataModule = dataModuleManager.RetrieveWithId(dbId, id);

        // Verify the record was retrieved.
        result = "";
        if (dataModule != null)
        {
          result = dataModule.Name;
        }
        compare = name;
        TestCommon.Write($"{methodName}2", result, compare);

        // Delete the test record.
        dataModuleManager.Delete(uniqueColumns);
        break;
      }
    }

    // Retrieves a record with the supplied unique values.
    private void RetrieveWithUnique()
    {
      var methodName = "RetrieveWithUnique()";

      // See: Run() for the DataConfigs.xml file example.

      // Create the data table manager.
      var dataConfigName = "DataUtility";
      var dbServiceRef = new LJCDbServiceRef()
      {
        DbDataAccess = new DbDataAccess(dataConfigName),
      };
      var dataModuleManager = new DataModuleManager(dbServiceRef
        , dataConfigName);

      short dbId = dataModuleManager.DbId;
      string name = "TestModuleName";

      // Create the test record.
      var dataModule = new DataModule()
      {
        DbId = dbId,
        Name = name,
        Description = "The test table.",
      };
      dataModuleManager.Add(dataModule);

      // Test Method
      dataModule = dataModuleManager.RetrieveUnique(name);

      var result = "";
      if (dataModule != null)
      {
        result = dataModule.Name;
      }
      var compare = "TestModuleName";
      TestCommon.Write($"{methodName}", result, compare);

      // Delete the test record.
      var uniqueColumns = new LJCDataColumns()
      {
        { "Name", name },
      };
      dataModuleManager.Delete(uniqueColumns);
    }
    #endregion

    #region GetKey Methods

    // Gets the ID key columns.
    private void IdKey()
    {
      var methodName = "IDKey()";

      // See: Run() for the DataConfigs.xml file example.

      // Create the data table manager.
      var dataConfigName = "DataUtility";
      var dbServiceRef = new LJCDbServiceRef()
      {
        DbDataAccess = new DbDataAccess(dataConfigName),
      };
      var dataModuleManager = new DataModuleManager(dbServiceRef
        , dataConfigName);

#pragma warning disable IDE0059 // Unnecessary assignment of a value
      short dbId = dataModuleManager.DbId;
#pragma warning restore IDE0059 // Unnecessary assignment of a value
      dbId = 1;
      long id = 1;

      // Test Method
      var idKey = DataModuleManager.IdKey(dbId, id);

      var result = "";
      if (idKey != null)
      {
        var key = idKey[DataModule.ColumnId];
        if (key != null)
        {
          var propertyName = key.PropertyName;
          var value = key.Value;
          result = $"{propertyName}: {value}";
        }
      }
      var compare = "Id: 1";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Gets the Unique ID key columns.
    private void UniqueKey()
    {
      var methodName = "UniqueKey()";

      // See: Run() for the DataConfigs.xml file example.

      string name = "TestModuleName";

      // Test Method
      var uniqueKey = DataModuleManager.UniqueKey(name);

      var result = "";
      if (uniqueKey != null)
      {
        var key = uniqueKey[DataModule.ColumnName];
        if (key != null)
        {
          var propertyName = key.PropertyName;
          var value = key.Value;
          result = $"{propertyName}: {value}";
        }
      }
      var compare = "Name: TestModuleName";
      TestCommon.Write($"{methodName}", result, compare);
    }
    #endregion
  }
}
