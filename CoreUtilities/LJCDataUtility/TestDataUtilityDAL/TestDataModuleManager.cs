// Copyright(c) Lester J.Clark and Contributors.
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
    public TestDataModuleManager()
    {
      CreateManagersProgramConfig();
      CreateManagers();

      CreateManagersManual();
      CreateManagersNoDataConfigs();
      CreateOnlyManager();

      TestCommon = new TestCommon("DataTableManager");
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
      // The DataConfigs.xml file example.
      //<?xml version='1.0'?>
      //<DataConfigs
      //  xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance'
      //  xmlns:xsd='http://www.w3.org/2001/XMLSchema'>
      //  <DataConfig>
      //    <Name>DataUtility</Name>
      //    <ConnectionType>SQLServer</ConnectionType>
      //    <DbServer>dbServerName</DbServer>
      //    <Database>databaseName</Database>
      //  </DataConfig >
      //</DataConfigs>

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
      // The DataConfigs.xml file example.
      //<?xml version='1.0'?>
      //<DataConfigs
      //  xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance'
      //  xmlns:xsd='http://www.w3.org/2001/XMLSchema'>
      //  <DataConfig>
      //    <Name>DataUtility</Name>
      //    <ConnectionType>SQLServer</ConnectionType>
      //    <DbServer>dbServerName</DbServer>
      //    <Database>databaseName</Database>
      //  </DataConfig >
      //</DataConfigs>

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
      // The DataConfigs.xml file example.
      //<?xml version='1.0'?>
      //<DataConfigs
      //  xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance'
      //  xmlns:xsd='http://www.w3.org/2001/XMLSchema'>
      //  <DataConfig>
      //    <Name>DataUtility</Name>
      //    <ConnectionType>SQLServer</ConnectionType>
      //    <DbServer>dbServerName</DbServer>
      //    <Database>databaseName</Database>
      //  </DataConfig >
      //</DataConfigs>

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
    public void Run()
    {
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
      ParentKey();
      UniqueKey();

      // DataTable Info Methods
      Columns();
      PropertyNames();

      // Joins
      GetJoins();
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
      var dataTableManager = new DataTableManager(dbServiceRef, dataConfigName);

      long dataModuleID = 1;
      long dataModuleSiteID = 1;
      string name = "TestTableName";

      // Delete the test record.
      var uniqueColumns = new DbColumns()
      {
        { "DataModuleID", dataModuleID },
        { "DataModuleSiteID", dataModuleSiteID },
        { "Name", (object)name },
      };
      dataTableManager.Delete(uniqueColumns);

      // Verify the test record does not exist.
      var utilTable = dataTableManager.Retrieve(uniqueColumns);
      var result = "HasObject";
      if (null == utilTable)
      {
        result = null;
      }
      var compare = "No Result";
      TestCommon.Write($"{methodName}1", result, compare);
    }
    #endregion

    #region Data Methods

    // Adds a record to the database.
    private void Add()
    {
      string methodName = "Add()";

      // The DataConfigs.xml file example.
      //<?xml version='1.0'?>
      //<DataConfigs
      //  xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance'
      //  xmlns:xsd='http://www.w3.org/2001/XMLSchema'>
      //  <DataConfig>
      //    <Name>DataUtility</Name>
      //    <ConnectionType>SQLServer</ConnectionType>
      //    <DbServer>dbServerName</DbServer>
      //    <Database>databaseName</Database>
      //  </DataConfig >
      //</DataConfigs>

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

      // The DataConfigs.xml file example.
      //<?xml version='1.0'?>
      //<DataConfigs
      //  xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance'
      //  xmlns:xsd='http://www.w3.org/2001/XMLSchema'>
      //  <DataConfig>
      //    <Name>DataUtility</Name>
      //    <ConnectionType>SQLServer</ConnectionType>
      //    <DbServer>dbServerName</DbServer>
      //    <Database>databaseName</Database>
      //  </DataConfig >
      //</DataConfigs>

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

      // The DataConfigs.xml file example.
      //<?xml version='1.0'?>
      //<DataConfigs
      //  xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance'
      //  xmlns:xsd='http://www.w3.org/2001/XMLSchema'>
      //  <DataConfig>
      //    <Name>DataUtility</Name>
      //    <ConnectionType>SQLServer</ConnectionType>
      //    <DbServer>dbServerName</DbServer>
      //    <Database>databaseName</Database>
      //  </DataConfig >
      //</DataConfigs>

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

      // The DataConfigs.xml file example.
      //<?xml version='1.0'?>
      //<DataConfigs
      //  xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance'
      //  xmlns:xsd='http://www.w3.org/2001/XMLSchema'>
      //  <DataConfig>
      //    <Name>DataUtility</Name>
      //    <ConnectionType>SQLServer</ConnectionType>
      //    <DbServer>dbServerName</DbServer>
      //    <Database>databaseName</Database>
      //  </DataConfig >
      //</DataConfigs>

      // Create the data table manager.
      var dataConfigName = "DataUtility";
      var dbServiceRef = new DbServiceRef()
      {
        DbDataAccess = new DbDataAccess(dataConfigName),
      };
      var dataTableManager = new DataTableManager(dbServiceRef, dataConfigName);

      long dataSiteID = 1;
      long dataModuleID = 1;
      long dataModuleSiteID = 1;
      string name = "TestTableName";

      // Create the test record.
      var dataUtilTable = new DataUtilTable()
      {
        DataSiteID = dataSiteID,
        DataModuleID = dataModuleID,
        DataModuleSiteID = dataModuleSiteID,
        Name = name,
        Description = "The test table.",
        Sequence = 5,
        SchemaName = "dbo",
        NewName = null,
      };
      dataTableManager.Add(dataUtilTable);

      // Verify the test record was created.
      var uniqueColumns = new DbColumns()
      {
        { "DataModuleID", dataModuleID },
        { "DataModuleSiteID", dataModuleSiteID },
        { "Name", (object)name },
      };

      // Test Method
      var utilTable = dataTableManager.Retrieve(uniqueColumns);

      var result = utilTable.Name;
      var compare = "TestTableName";
      TestCommon.Write($"{methodName}", result, compare);

      // Delete the test record.
      dataTableManager.Delete(uniqueColumns);
    }

    // Updates the record.
    private void Update()
    {
      var methodName = "Update()";

      // The DataConfigs.xml file example.
      //<?xml version='1.0'?>
      //<DataConfigs
      //  xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance'
      //  xmlns:xsd='http://www.w3.org/2001/XMLSchema'>
      //  <DataConfig>
      //    <Name>DataUtility</Name>
      //    <ConnectionType>SQLServer</ConnectionType>
      //    <DbServer>dbServerName</DbServer>
      //    <Database>databaseName</Database>
      //  </DataConfig >
      //</DataConfigs>

      // Create the data table manager.
      var dataConfigName = "DataUtility";
      var dbServiceRef = new DbServiceRef()
      {
        DbDataAccess = new DbDataAccess(dataConfigName),
      };
      var dataTableManager = new DataTableManager(dbServiceRef, dataConfigName);

      short dbID = 1;
      long dataModuleID = 1;
      long dataModuleSiteID = 1;
      string name = "TestTableName";

      // Create the test record.
      var dataUtilTable = new DataUtilTable()
      {
        DataSiteID = dbID,
        DataModuleID = dataModuleID,
        DataModuleSiteID = dataModuleSiteID,
        Name = name,
        Description = "The test table.",
        Sequence = 5,
        SchemaName = "dbo",
        NewName = null,
      };
      dataTableManager.Add(dataUtilTable);

      // Verify the test record was created.
      var uniqueColumns = new DbColumns()
      {
        { "DataModuleID", dataModuleID },
        { "DataModuleSiteID", dataModuleSiteID },
        { "Name", (object)name },
      };
      var utilTable = dataTableManager.Retrieve(uniqueColumns);
      var result = utilTable.Name;
      var compare = "TestTableName";
      TestCommon.Write($"{methodName}1", result, compare);

      // Update the test record.
      var id = utilTable.ID;
      dbID = (short)utilTable.DataSiteID;
      var idKey = dataTableManager.IDKey(id, dbID);
      utilTable.Description += " Updated";
      var propertyNames = new List<string>()
      {
        "Description",
      };

      // Test Method
      dataTableManager.Update(utilTable, idKey, propertyNames);

      // Verify the test record was updated.
      utilTable = dataTableManager.RetrieveWithID(id, dbID);
      result = utilTable.Description;
      compare = "The test table. Updated";
      TestCommon.Write($"{methodName}2", result, compare);

      // Delete the test record.
      dataTableManager.Delete(uniqueColumns);
    }
    #endregion

    #region Custom Data Methods

    // Retrieves a record with the supplied ID values.
    private void RetrieveWithID()
    {
      var methodName = "RetrieveWithID()";

      // The DataConfigs.xml file example.
      //<?xml version='1.0'?>
      //<DataConfigs
      //  xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance'
      //  xmlns:xsd='http://www.w3.org/2001/XMLSchema'>
      //  <DataConfig>
      //    <Name>DataUtility</Name>
      //    <ConnectionType>SQLServer</ConnectionType>
      //    <DbServer>dbServerName</DbServer>
      //    <Database>databaseName</Database>
      //  </DataConfig >
      //</DataConfigs>

      // Create the data table manager.
      var dataConfigName = "DataUtility";
      var dbServiceRef = new DbServiceRef()
      {
        DbDataAccess = new DbDataAccess(dataConfigName),
      };
      var dataTableManager = new DataTableManager(dbServiceRef, dataConfigName);

      short dbID = 1;
      long dataModuleID = 1;
      long dataModuleSiteID = 1;
      string name = "TestTableName";

      // Create the test record.
      var dataUtilTable = new DataUtilTable()
      {
        DataSiteID = dbID,
        DataModuleID = dataModuleID,
        DataModuleSiteID = dataModuleSiteID,
        Name = name,
        Description = "The test table.",
        Sequence = 5,
        SchemaName = "dbo",
        NewName = null,
      };
      dataTableManager.Add(dataUtilTable);

      // Verify the test record was created.
      var uniqueColumns = new DbColumns()
      {
        { "DataModuleID", dataModuleID },
        { "DataModuleSiteID", dataModuleSiteID },
        { "Name", (object)name },
      };
      var utilTable = dataTableManager.Retrieve(uniqueColumns);

      var result = utilTable.Name;
      var compare = "TestTableName";
      TestCommon.Write($"{methodName}1", result, compare);

      // Retrieve with ID.
      var id = utilTable.ID;

      // Test Method
      utilTable = dataTableManager.RetrieveWithID(id, dbID);

      // Verify the record was retrieved.
      result = utilTable.Name;
      compare = "TestTableName";
      TestCommon.Write($"{methodName}2", result, compare);

      // Delete the test record.
      dataTableManager.Delete(uniqueColumns);
    }

    // Retrieves a record with the supplied unique values.
    private void RetrieveWithUnique()
    {
      var methodName = "RetrieveWithUnique()";

      // The DataConfigs.xml file example.
      //<?xml version='1.0'?>
      //<DataConfigs
      //  xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance'
      //  xmlns:xsd='http://www.w3.org/2001/XMLSchema'>
      //  <DataConfig>
      //    <Name>DataUtility</Name>
      //    <ConnectionType>SQLServer</ConnectionType>
      //    <DbServer>dbServerName</DbServer>
      //    <Database>databaseName</Database>
      //  </DataConfig >
      //</DataConfigs>

      // Create the data table manager.
      var dataConfigName = "DataUtility";
      var dbServiceRef = new DbServiceRef()
      {
        DbDataAccess = new DbDataAccess(dataConfigName),
      };
      var dataTableManager = new DataTableManager(dbServiceRef, dataConfigName);

      short dbID = 1;
      long dataModuleID = 1;
      short dataModuleDbID = 1;
      string name = "TestTableName";

      // Create the test record.
      var dataUtilTable = new DataUtilTable()
      {
        DataSiteID = dbID,
        DataModuleID = dataModuleID,
        DataModuleSiteID = dataModuleDbID,
        Name = name,
        Description = "The test table.",
        Sequence = 5,
        SchemaName = "dbo",
        NewName = null,
      };
      dataTableManager.Add(dataUtilTable);

      // Test Method
      var utilTable = dataTableManager.RetrieveWithUnique(dataModuleID
        , dataModuleDbID, name);

      var result = utilTable.Name;
      var compare = "TestTableName";
      TestCommon.Write($"{methodName}", result, compare);

      // Delete the test record.
      var uniqueColumns = new DbColumns()
      {
        { "DataModuleID", dataModuleID },
        { "DataModuleSiteID", dataModuleDbID },
        { "Name", (object)name },
      };
      dataTableManager.Delete(uniqueColumns);
    }
    #endregion

    #region GetKey Methods

    // Gets the ID key columns.
    private void IDKey()
    {
      var methodName = "IDKey()";

      // The DataConfigs.xml file example.
      //<?xml version='1.0'?>
      //<DataConfigs
      //  xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance'
      //  xmlns:xsd='http://www.w3.org/2001/XMLSchema'>
      //  <DataConfig>
      //    <Name>DataUtility</Name>
      //    <ConnectionType>SQLServer</ConnectionType>
      //    <DbServer>dbServerName</DbServer>
      //    <Database>databaseName</Database>
      //  </DataConfig >
      //</DataConfigs>

      // Create the data table manager.
      var dataConfigName = "DataUtility";
      var dbServiceRef = new DbServiceRef()
      {
        DbDataAccess = new DbDataAccess(dataConfigName),
      };
      var dataTableManager = new DataTableManager(dbServiceRef, dataConfigName);

      long id = 1;
      short dbID = 1;

      // Test Method
      var idKey = dataTableManager.IDKey(id, dbID);

      var key = idKey.LJCSearchPropertyName(DataUtilTable.ColumnID);
      var propertyName = key.PropertyName;
      var value = key.Value;
      var result = $"{propertyName}: {value}";
      var compare = "ID: 1";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Gets the Parent ID key columns.
    private void ParentKey()
    {
      var methodName = "ParentKey()";

      // The DataConfigs.xml file example.
      //<?xml version='1.0'?>
      //<DataConfigs
      //  xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance'
      //  xmlns:xsd='http://www.w3.org/2001/XMLSchema'>
      //  <DataConfig>
      //    <Name>DataUtility</Name>
      //    <ConnectionType>SQLServer</ConnectionType>
      //    <DbServer>dbServerName</DbServer>
      //    <Database>databaseName</Database>
      //  </DataConfig >
      //</DataConfigs>

      // Create the data table manager.
      var dataConfigName = "DataUtility";
      var dbServiceRef = new DbServiceRef()
      {
        DbDataAccess = new DbDataAccess(dataConfigName),
      };
      var dataTableManager = new DataTableManager(dbServiceRef, dataConfigName);

      long dataModuleID = 1;
      short dataModuleDbID = 1;

      // Test Method
      var idKey = dataTableManager.ParentKey(dataModuleID, dataModuleDbID);

      var key = idKey.LJCSearchPropertyName(DataUtilTable.ColumnDataModuleID);
      var propertyName = key.PropertyName;
      var value = key.Value;
      var result = $"{propertyName}: {value}";
      var compare = "DataModuleID: 1";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Gets the Unique ID key columns.
    private void UniqueKey()
    {
      var methodName = "UniqueKey()";

      // The DataConfigs.xml file example.
      //<?xml version='1.0'?>
      //<DataConfigs
      //  xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance'
      //  xmlns:xsd='http://www.w3.org/2001/XMLSchema'>
      //  <DataConfig>
      //    <Name>DataUtility</Name>
      //    <ConnectionType>SQLServer</ConnectionType>
      //    <DbServer>dbServerName</DbServer>
      //    <Database>databaseName</Database>
      //  </DataConfig >
      //</DataConfigs>

      // Create the data table manager.
      var dataConfigName = "DataUtility";
      var dbServiceRef = new DbServiceRef()
      {
        DbDataAccess = new DbDataAccess(dataConfigName),
      };
      var dataTableManager = new DataTableManager(dbServiceRef, dataConfigName);

      long dataModuleID = 1;
      short dataModuleDbID = 1;
      string name = "TestTableName";

      // Test Method
      var idKey = dataTableManager.UniqueKey(dataModuleID, dataModuleDbID
        , name);

      var key = idKey.LJCSearchPropertyName(DataUtilTable.ColumnName);
      var propertyName = key.PropertyName;
      var value = key.Value;
      var result = $"{propertyName}: {value}";
      var compare = "Name: TestTableName";
      TestCommon.Write($"{methodName}", result, compare);
    }
    #endregion

    #region Info Methods

    // Creates a collection of columns that match the supplied list.
    private void Columns()
    {
      var methodName = "Columns()";

      // The DataConfigs.xml file example.
      //<?xml version='1.0'?>
      //<DataConfigs
      //  xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance'
      //  xmlns:xsd='http://www.w3.org/2001/XMLSchema'>
      //  <DataConfig>
      //    <Name>DataUtility</Name>
      //    <ConnectionType>SQLServer</ConnectionType>
      //    <DbServer>dbServerName</DbServer>
      //    <Database>databaseName</Database>
      //  </DataConfig >
      //</DataConfigs>

      // Create the data table manager.
      var dataConfigName = "DataUtility";
      var dbServiceRef = new DbServiceRef()
      {
        DbDataAccess = new DbDataAccess(dataConfigName),
      };
      var dataTableManager = new DataTableManager(dbServiceRef, dataConfigName);

      var propertyNames = new List<string>()
      {
        "DataModuleID",
        "DataModuleSiteID",
        "Name",
      };

      // Test Method
      var columns = dataTableManager.Columns(propertyNames);

      var column = columns["Name"];
      var result = column.PropertyName;
      var compare = "Name";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Creates a list of BaseDefinition property names.
    private void PropertyNames()
    {
      var methodName = "PropertyNames()";

      // The DataConfigs.xml file example.
      //<?xml version='1.0'?>
      //<DataConfigs
      //  xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance'
      //  xmlns:xsd='http://www.w3.org/2001/XMLSchema'>
      //  <DataConfig>
      //    <Name>DataUtility</Name>
      //    <ConnectionType>SQLServer</ConnectionType>
      //    <DbServer>dbServerName</DbServer>
      //    <Database>databaseName</Database>
      //  </DataConfig >
      //</DataConfigs>

      // Create the data table manager.
      var dataConfigName = "DataUtility";
      var dbServiceRef = new DbServiceRef()
      {
        DbDataAccess = new DbDataAccess(dataConfigName),
      };
      var dataTableManager = new DataTableManager(dbServiceRef, dataConfigName);

      // Test Method
      var names = dataTableManager.PropertyNames();
      var result = names[2];
      var compare = "DataSiteID";
      TestCommon.Write($"{methodName}", result, compare);
    }
    #endregion

    #region Joins

    // Creates and returns the Load Joins object.
    private void GetJoins()
    {
      var methodName = "GetJoins()";

      // The DataConfigs.xml file example.
      //<?xml version='1.0'?>
      //<DataConfigs
      //  xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance'
      //  xmlns:xsd='http://www.w3.org/2001/XMLSchema'>
      //  <DataConfig>
      //    <Name>DataUtility</Name>
      //    <ConnectionType>SQLServer</ConnectionType>
      //    <DbServer>dbServerName</DbServer>
      //    <Database>databaseName</Database>
      //  </DataConfig >
      //</DataConfigs>

      // Create the data table manager.
      var dataConfigName = "DataUtility";
      var dbServiceRef = new DbServiceRef()
      {
        DbDataAccess = new DbDataAccess(dataConfigName),
      };
      var dataTableManager = new DataTableManager(dbServiceRef, dataConfigName);

      // Test Method
      var joins = dataTableManager.GetJoins();

      var result = joins[0].TableName;
      var compare = "DataModule";
      TestCommon.Write($"{methodName}", result, compare);
    }
    #endregion

    #region Properties
    #endregion

    private TestCommon TestCommon { get; set; }
  }
}
