// Copyright (c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// TestDataKeyManager.cs
using LJCDataUtilityDAL;
using LJCDBClientLib;
using LJCDBDataAccess;
using LJCNetCommon;
using System.Collections.Generic;

namespace TestDataUtilityDAL
{
  // Tests the DataKeyManager object.
  internal class TestDataKeyManager
  {
    #region Constructor Methods

    // Initializes an object instance.
    /// <include file='Doc/TestDataKeyManager.xml'
    ///  path='members/Constructor/*'/>
    public TestDataKeyManager()
    {
      TestCommon = new TestCommon("TestDataKeyManager");
      Run();
    }
    #endregion

    #region Methods

    // Run the tests.
    /// <include file='Doc/TestDataKeyManager.xml'
    ///  path='members/Run/*'/>
    private void Run()
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

      // Manager Methods
      Columns();
      PropertyNames();

      // Data Methods
      Add();
      Delete();
      Load();
      Retrieve();
      Update();

      // Custom Data Methods
      LoadWithForeign();
      LoadWithParent();
      LoadWithParentType();
      LoadWithType();
      RetrieveWithID();
      RetrieveWithParentType();
      RetrieveWithUnique();

      // Get Key Methods
      ForeignKey();
      IDKey();
      ParentKey();
      ParentTypeKey();
      UniqueKey();

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
      var dataKeyManager = new DataKeyManager(dbServiceRef, dataConfigName);

      long dataTableID = 1;
      short dataTableDbID = 1;
      string name = "TestKeyName";

      // Delete the test record.
      var uniqueColumns = new DbColumns()
      {
        { "DataTableID", dataTableID },
        { "DataTableSiteID", dataTableDbID },
        { "Name", (object)name },
      };
      dataKeyManager.Delete(uniqueColumns);

      // Verify the test record does not exist.
      var dataKey = dataKeyManager.Retrieve(uniqueColumns);
      var result = "HasObject";
      if (null == dataKey)
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
      var dataKeyManager = new DataKeyManager(dbServiceRef, dataConfigName);

      var propertyNames = new List<string>()
      {
        "DataTableID",
        "DataTableSiteID",
        "Name",
      };

      // Test Method
      var columns = dataKeyManager.Columns(propertyNames);

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
      var dataKeyManager = new DataKeyManager(dbServiceRef, dataConfigName);

      // Test Method
      var names = dataKeyManager.PropertyNames();
      var result = names[2];
      var compare = "DataTableSiteID";
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
      var dataKeyManager = new DataKeyManager(dbServiceRef
        , dataConfigName);

      short dbID = dataKeyManager.DbID;
      long dataTableID = 1;
      short dataTableDbID = 1;
      string name = "TestKeyName";

      // Create the test record.
      var dataKey = new DataKey()
      {
        DataSiteID = dbID,
        DataTableID = dataTableID,
        DataTableSiteID = dataTableDbID,
        Name = name,
      };

      // Test Method
      dataKeyManager.Add(dataKey);

      // Verify the test record was created.
      var uniqueColumns = new DbColumns()
      {
        { "DataTableID", dataTableID },
        { "DataTableSiteID", dataTableDbID },
        { "Name", (object)name },
      };
      dataKey = dataKeyManager.Retrieve(uniqueColumns);
      var result = dataKey.Name;
      var compare = "TestKeyName";
      TestCommon.Write($"{methodName}", result, compare);

      // Delete the test record.
      dataKeyManager.Delete(uniqueColumns);
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
      var dataKeyManager = new DataKeyManager(dbServiceRef
        , dataConfigName);

      long dataTableID = 1;
      short dataTableDbID = 1;
      string name = "TestKeyName";

      // Create the test record.
      var uniqueColumns = new DbColumns()
      {
        { "DataTableID", dataTableID },
        { "DataTableSiteID", dataTableDbID },
        { "Name", (object)name },
      };
      var dataKey = dataKeyManager.Retrieve(uniqueColumns);
      if (dataKey != null)
      {
        var result = dataKey.Name;
        var compare = "TestKeyName";
        TestCommon.Write($"{methodName}1", result, compare);

        // Test Method
        dataKeyManager.Delete(uniqueColumns);

        // Verify the test record was deleted.
        dataKey = dataKeyManager.Retrieve(uniqueColumns);
        if (null == dataKey)
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
      var dataKeyManager = new DataKeyManager(dbServiceRef
        , dataConfigName);

      short dbID = dataKeyManager.DbID;
      long dataTableID = 1;
      short dataTableSiteID = 1;
      string name = "TestKeyName";

      // Create the test record.
      var dataKey = new DataKey()
      {
        DataSiteID = dbID,
        DataTableID = dataTableID,
        DataTableSiteID = dataTableSiteID,
        Name = name,
      };
      dataKeyManager.Add(dataKey);

      // Test Method
      var dataKeys = dataKeyManager.Load();

      // Verify the records were loaded.
      var result = "";
      if (dataKeys.Count > 0)
      {
        result = "HasItems";
      }
      var compare = "HasItems";
      TestCommon.Write($"{methodName}", result, compare);

      // Delete the test record.
      var uniqueColumns = new DbColumns()
      {
        { "DataTableID", dataTableID },
        { "DataTableSiteID", dataTableSiteID },
        { "Name", (object)name },
      };
      dataKeyManager.Delete(uniqueColumns);
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
      var dataKeyManager = new DataKeyManager(dbServiceRef
        , dataConfigName);

      short dbID = dataKeyManager.DbID;
      long dataTableID = 1;
      short dataTableDbID = 1;
      string name = "TestKeyName";

      // Create the test record.
      var dataKey = new DataKey()
      {
        DataSiteID = dbID,
        DataTableID = dataTableID,
        DataTableSiteID = dataTableDbID,
        Name = name,
      };
      dataKeyManager.Add(dataKey);

      // Verify the test record was created.
      var uniqueColumns = new DbColumns()
      {
        { "DataTableID", dataTableID },
        { "DataTableSiteID", dataTableDbID },
        { "Name", (object)name },
      };

      // Test Method
      dataKey = dataKeyManager.Retrieve(uniqueColumns);

      var result = dataKey.Name;
      var compare = "TestKeyName";
      TestCommon.Write($"{methodName}", result, compare);

      // Delete the test record.
      dataKeyManager.Delete(uniqueColumns);
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
      var dataKeyManager = new DataKeyManager(dbServiceRef
        , dataConfigName);

      short dbID = dataKeyManager.DbID;
      long dataTableID = 1;
      short dataTableDbID = 1;
      string name = "TestKeyName";

      // Create the test record.
      var dataKey = new DataKey()
      {
        DataSiteID = dbID,
        DataTableID = dataTableID,
        DataTableSiteID = dataTableDbID,
        Name = name,
      };
      dataKeyManager.Add(dataKey);

      // Verify the test record was created.
      var uniqueColumns = new DbColumns()
      {
        { "DataTableID", dataTableID },
        { "DataTableSiteID", dataTableDbID },
        { "Name", (object)name },
      };
      dataKey = dataKeyManager.Retrieve(uniqueColumns);
      var result = dataKey.Name;
      var compare = "TestKeyName";
      TestCommon.Write($"{methodName}1", result, compare);

      // Update the test record.
      var id = dataKey.ID;
      dbID = dataKey.DataSiteID;
      var idKey = dataKeyManager.IDKey(id, dbID);
      dataKey.Name += " Updated";
      var propertyNames = new List<string>()
      {
        "Name",
      };

      // Test Method
      dataKeyManager.Update(dataKey, idKey, propertyNames);

      // Verify the test record was updated.
      dataKey = dataKeyManager.RetrieveWithID(id, dbID);
      result = dataKey.Name;
      compare = "TestKeyName Updated";
      TestCommon.Write($"{methodName}2", result, compare);

      // Delete the test record.
      var nameColumn = uniqueColumns.LJCSearchPropertyName("Name");
      nameColumn.Value = "TestKeyName Updated";
      dataKeyManager.Delete(uniqueColumns);
    }
    #endregion

    #region Custom Data Methods

    // Loads records with the supplied values.
    private void LoadWithForeign()
    {
      var methodName = "LoadWithForeign()";

      // See: Run() for the DataConfigs.xml file example.

      // Create the data table manager.
      var dataConfigName = "DataUtility";
      var dbServiceRef = new DbServiceRef()
      {
        DbDataAccess = new DbDataAccess(dataConfigName),
      };
      var dataKeyManager = new DataKeyManager(dbServiceRef, dataConfigName);

      short dbID = dataKeyManager.DbID;
      long dataTableID = 1;
      short dataTableDbID = 1;
      string name = "TestKeyName";
      short foreignKey = 3;

      // Create the test record.
      var dataKey = new DataKey()
      {
        DataSiteID = dbID,
        DataTableID = dataTableID,
        DataTableSiteID = dataTableDbID,
        Name = name,
        KeyType = foreignKey,
        IsAscending = true,
        IsClustered = false,
        SourceColumnName = "SourceColumnName",
        TargetTableName = "TargetTableName",
        TargetColumnName = "TargetColumnName",
      };
      dataKeyManager.Add(dataKey);

      // Verify the test record was created.
      var uniqueColumns = new DbColumns()
      {
        { "DataTableID", dataTableID },
        { "DataTableSiteID", dataTableDbID },
        { "Name", (object)name },
      };
      dataKey = dataKeyManager.Retrieve(uniqueColumns);

      var result = dataKey.Name;
      var compare = "TestKeyName";
      TestCommon.Write($"{methodName}1", result, compare);

      // Load with parent foreign keys.
      //dataTableID = dataKey.DataTableID;
      //dataTableDbID = (short)dataKey.DataTableSiteID;
      var tableName = "DataTable";

      // Test Method
      var dataKeys = dataKeyManager.LoadWithForeign(tableName);

      // Verify the record was retrieved.
      dataTableID = 4;
      name = "fk_DataKey_DataTable";
      dataKey = dataKeys.LJCGetWithUnique(dataTableID, dataTableDbID, name);
      result = dataKey.Name;
      compare = "fk_DataKey_DataTable";
      TestCommon.Write($"{methodName}2", result, compare);

      // Delete the test record.
      dataKeyManager.Delete(uniqueColumns);
    }

    // Loads records with the supplied values.
    private void LoadWithParent()
    {
      var methodName = "LoadWithParent()";

      // See: Run() for the DataConfigs.xml file example.

      // Create the data table manager.
      var dataConfigName = "DataUtility";
      var dbServiceRef = new DbServiceRef()
      {
        DbDataAccess = new DbDataAccess(dataConfigName),
      };
      var dataKeyManager = new DataKeyManager(dbServiceRef, dataConfigName);

      short dbID = dataKeyManager.DbID;
      long dataTableID = 1;
      short dataTableDbID = 1;
      string name = "TestKeyName";

      // Create the test record.
      var dataKey = new DataKey()
      {
        DataSiteID = dbID,
        DataTableID = dataTableID,
        DataTableSiteID = dataTableDbID,
        Name = name,
        KeyType = 1,
        IsAscending = true,
        IsClustered = false,
        SourceColumnName = "SourceColumnName",
        TargetTableName = "TargetTableName",
        TargetColumnName = "TargetColumnName",
      };
      dataKeyManager.Add(dataKey);

      // Verify the test record was created.
      var uniqueColumns = new DbColumns()
      {
        { "DataTableID", dataTableID },
        { "DataTableSiteID", dataTableDbID },
        { "Name", (object)name },
      };
      dataKey = dataKeyManager.Retrieve(uniqueColumns);

      var result = dataKey.Name;
      var compare = "TestKeyName";
      TestCommon.Write($"{methodName}1", result, compare);

      // Load with parent.
      dataTableID = dataKey.DataTableID;
      dataTableDbID = (short)dataKey.DataTableSiteID;

      // Test Method
      var dataKeys = dataKeyManager.LoadWithParent(dataTableID, dataTableDbID);

      // Verify the record was retrieved.
      dataKey = dataKeys.LJCGetWithUnique(dataTableID, dataTableDbID, name);
      result = dataKey.Name;
      compare = "TestKeyName";
      TestCommon.Write($"{methodName}2", result, compare);

      // Delete the test record.
      dataKeyManager.Delete(uniqueColumns);
    }

    // Loads records with the supplied values.
    private void LoadWithParentType()
    {
      var methodName = "LoadWithParentType()";

      // See: Run() for the DataConfigs.xml file example.

      // Create the data table manager.
      var dataConfigName = "DataUtility";
      var dbServiceRef = new DbServiceRef()
      {
        DbDataAccess = new DbDataAccess(dataConfigName),
      };
      var dataKeyManager = new DataKeyManager(dbServiceRef, dataConfigName);

      short dbID = dataKeyManager.DbID;
      long dataTableID = 1;
      short dataTableDbID = 1;
      string name = "TestKeyName";

      // Create the test record.
      var dataKey = new DataKey()
      {
        DataSiteID = dbID,
        DataTableID = dataTableID,
        DataTableSiteID = dataTableDbID,
        Name = name,
        KeyType = 1,
        IsAscending = true,
        IsClustered = false,
        SourceColumnName = "SourceColumnName",
        TargetTableName = "TargetTableName",
        TargetColumnName = "TargetColumnName",
      };
      dataKeyManager.Add(dataKey);

      // Verify the test record was created.
      var uniqueColumns = new DbColumns()
      {
        { "DataTableID", dataTableID },
        { "DataTableSiteID", dataTableDbID },
        { "Name", (object)name },
      };
      dataKey = dataKeyManager.Retrieve(uniqueColumns);

      var result = dataKey.Name;
      var compare = "TestKeyName";
      TestCommon.Write($"{methodName}1", result, compare);

      // Load with parent.
      dataTableID = dataKey.DataTableID;
      dataTableDbID = (short)dataKey.DataTableSiteID;
      var keyType = 1;

      // Test Method
      var dataKeys = dataKeyManager.LoadWithParentType(dataTableID
        , dataTableDbID, keyType);

      // Verify the record was retrieved.
      dataKey = dataKeys.LJCGetWithUnique(dataTableID, dataTableDbID, name);
      result = dataKey.Name;
      compare = "TestKeyName";
      TestCommon.Write($"{methodName}2", result, compare);

      // Delete the test record.
      dataKeyManager.Delete(uniqueColumns);
    }

    // Loads records with the supplied values.
    private void LoadWithType()
    {
      var methodName = "LoadWithType()";

      // See: Run() for the DataConfigs.xml file example.

      // Create the data table manager.
      var dataConfigName = "DataUtility";
      var dbServiceRef = new DbServiceRef()
      {
        DbDataAccess = new DbDataAccess(dataConfigName),
      };
      var dataKeyManager = new DataKeyManager(dbServiceRef, dataConfigName);

      short dbID = dataKeyManager.DbID;
      long dataTableID = 1;
      short dataTableDbID = 1;
      string name = "TestKeyName";

      // Create the test record.
      var dataKey = new DataKey()
      {
        DataSiteID = dbID,
        DataTableID = dataTableID,
        DataTableSiteID = dataTableDbID,
        Name = name,
        KeyType = 1,
        IsAscending = true,
        IsClustered = false,
        SourceColumnName = "SourceColumnName",
        TargetTableName = "TargetTableName",
        TargetColumnName = "TargetColumnName",
      };
      dataKeyManager.Add(dataKey);

      // Verify the test record was created.
      var uniqueColumns = new DbColumns()
      {
        { "DataTableID", dataTableID },
        { "DataTableSiteID", dataTableDbID },
        { "Name", (object)name },
      };
      dataKey = dataKeyManager.Retrieve(uniqueColumns);

      var result = dataKey.Name;
      var compare = "TestKeyName";
      TestCommon.Write($"{methodName}1", result, compare);

      // Load with type.
      var id = dataKey.ID;
      dbID = (short)dataKey.DataTableSiteID;
      short keyType = 1;

      // Test Method
      //var dataKeys = dataKeyManager.LoadWithType(id, dbID, keyType);
      var dataKeys = dataKeyManager.LoadWithType(dataTableID, dataTableDbID
        , keyType);

      // Verify the record was retrieved.
      dataKey = dataKeys.LJCGetWithID(id, dbID);
      result = dataKey.Name;
      compare = "TestKeyName";
      TestCommon.Write($"{methodName}2", result, compare);

      // Delete the test record.
      dataKeyManager.Delete(uniqueColumns);
    }

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
      var dataKeyManager = new DataKeyManager(dbServiceRef, dataConfigName);

      short dbID = dataKeyManager.DbID;
      long dataTableID = 1;
      short dataTableDbID = 1;
      string name = "TestKeyName";

      // Create the test record.
      var dataKey = new DataKey()
      {
        DataSiteID = dbID,
        DataTableID = dataTableID,
        DataTableSiteID = dataTableDbID,
        Name = name,
        KeyType = 1,
        IsAscending = true,
        IsClustered = false,
        SourceColumnName = "SourceColumnName",
        TargetTableName = "TargetTableName",
        TargetColumnName = "TargetColumnName",
      };
      dataKeyManager.Add(dataKey);

      // Verify the test record was created.
      var uniqueColumns = new DbColumns()
      {
        { "DataTableID", dataTableID },
        { "DataTableSiteID", dataTableDbID },
        { "Name", (object)name },
      };
      dataKey = dataKeyManager.Retrieve(uniqueColumns);

      var result = dataKey.Name;
      var compare = "TestKeyName";
      TestCommon.Write($"{methodName}1", result, compare);

      // Retrieve with ID.
      var id = dataKey.ID;

      // Test Method
      dataKey = dataKeyManager.RetrieveWithID(id, dbID);

      // Verify the record was retrieved.
      result = dataKey.Name;
      compare = "TestKeyName";
      TestCommon.Write($"{methodName}2", result, compare);

      // Delete the test record.
      dataKeyManager.Delete(uniqueColumns);
    }

    // Retrieves a record with the supplied values.
    private void RetrieveWithParentType()
    {
      var methodName = "RetrieveWithParentType()";

      // See: Run() for the DataConfigs.xml file example.

      // Create the data table manager.
      var dataConfigName = "DataUtility";
      var dbServiceRef = new DbServiceRef()
      {
        DbDataAccess = new DbDataAccess(dataConfigName),
      };
      var dataKeyManager = new DataKeyManager(dbServiceRef, dataConfigName);

      short dbID = dataKeyManager.DbID;
      long dataTableID = 1;
      short dataTableDbID = 1;
      string name = "TestKeyName";

      // Create the test record.
      var dataKey = new DataKey()
      {
        DataSiteID = dbID,
        DataTableID = dataTableID,
        DataTableSiteID = dataTableDbID,
        Name = name,
        KeyType = 1,
        IsAscending = true,
        IsClustered = false,
        SourceColumnName = "SourceColumnName",
        TargetTableName = "TargetTableName",
        TargetColumnName = "TargetColumnName",
      };
      dataKeyManager.Add(dataKey);

      // Verify the test record was created.
      var uniqueColumns = new DbColumns()
      {
        { "DataTableID", dataTableID },
        { "DataTableSiteID", dataTableDbID },
        { "Name", (object)name },
      };
      dataKey = dataKeyManager.Retrieve(uniqueColumns);

      var result = dataKey.Name;
      var compare = "TestKeyName";
      TestCommon.Write($"{methodName}1", result, compare);

      // Retrieve with parent KeyType.
      var parentID = dataKey.DataTableID;
      short parentDbID = (short)dataKey.DataTableSiteID;
      var keyType = dataKey.KeyType;

      // Test Method
      dataKey = dataKeyManager.RetrieveWithParentType(parentID, parentDbID
        , keyType);

      // Verify the record was retrieved.
      result = dataKey.Name;
      compare = "pk_DataModule";
      TestCommon.Write($"{methodName}2", result, compare);

      // Delete the test record.
      dataKeyManager.Delete(uniqueColumns);
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
      var dataKeyManager = new DataKeyManager(dbServiceRef, dataConfigName);

      short dbID = dataKeyManager.DbID;
      long dataTableID = 1;
      short dataTableDbID = 1;
      string name = "TestKeyName";

      // Create the test record.
      var dataKey = new DataKey()
      {
        DataSiteID = dbID,
        DataTableID = dataTableID,
        DataTableSiteID = dataTableDbID,
        Name = name,
        KeyType = 1,
        IsAscending = true,
        IsClustered = false,
        SourceColumnName = "SourceColumnName",
        TargetTableName = "TargetTableName",
        TargetColumnName = "TargetColumnName",
      };
      dataKeyManager.Add(dataKey);

      // Test Method
      dataKey = dataKeyManager.RetrieveWithUnique(dataTableID, dataTableDbID
        , name);

      var result = dataKey.Name;
      var compare = "TestKeyName";
      TestCommon.Write($"{methodName}", result, compare);

      // Delete the test record.
      var uniqueColumns = new DbColumns()
      {
        { "DataTableID", dataTableID },
        { "DataTableSiteID", dataTableDbID },
        { "Name", (object)name },
      };
      dataKeyManager.Delete(uniqueColumns);
    }
    #endregion

    #region GetKey Methods

    // Gets the Foreign key columns.
    private void ForeignKey()
    {
      var methodName = "ForeignKey()";

      // See: Run() for the DataConfigs.xml file example.

      // Create the data table manager.
      var dataConfigName = "DataUtility";
      var dbServiceRef = new DbServiceRef()
      {
        DbDataAccess = new DbDataAccess(dataConfigName),
      };
      var dataKeyManager = new DataKeyManager(dbServiceRef, dataConfigName);

      var targetTableName = "DataKey";

      // Test Method
      var idKey = dataKeyManager.ForeignKey(targetTableName);

      var key = idKey.LJCSearchPropertyName(DataKey.ColumnKeyType);
      var propertyName = key.PropertyName;
      var value = key.Value;
      var result = $"{propertyName}: {value}";
      var compare = "KeyType: 3";
      TestCommon.Write($"{methodName}", result, compare);
    }

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
      var dataKeyManager = new DataKeyManager(dbServiceRef, dataConfigName);

      long id = 1;
      short dbID = dataKeyManager.DbID;

      // Test Method
      var idKey = dataKeyManager.IDKey(id, dbID);

      var key = idKey.LJCSearchPropertyName(DataKey.ColumnID);
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

      // See: Run() for the DataConfigs.xml file example.

      // Create the data table manager.
      var dataConfigName = "DataUtility";
      var dbServiceRef = new DbServiceRef()
      {
        DbDataAccess = new DbDataAccess(dataConfigName),
      };
      var dataKeyManager = new DataKeyManager(dbServiceRef, dataConfigName);

      long dataTableID = 1;
      short dataTableDbID = 1;

      // Test Method
      var idKey = dataKeyManager.ParentKey(dataTableID, dataTableDbID);

      var key = idKey.LJCSearchPropertyName(DataKey.ColumnDataTableID);
      var propertyName = key.PropertyName;
      var value = key.Value;
      var result = $"{propertyName}: {value}";
      var compare = "DataTableID: 1";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Gets the parent KeyType key columns.
    private void ParentTypeKey()
    {
      var methodName = "ParentTypeKey()";

      // See: Run() for the DataConfigs.xml file example.

      // Create the data table manager.
      var dataConfigName = "DataUtility";
      var dbServiceRef = new DbServiceRef()
      {
        DbDataAccess = new DbDataAccess(dataConfigName),
      };
      var dataKeyManager = new DataKeyManager(dbServiceRef, dataConfigName);

      long dataTableID = 1;
      short dataTableDbID = 1;
      int keyType = 1;

      // Test Method
      var idKey = dataKeyManager.ParentTypeKey(dataTableID, dataTableDbID
        , keyType);

      var key = idKey.LJCSearchPropertyName(DataKey.ColumnDataTableID);
      var propertyName = key.PropertyName;
      var value = key.Value;
      var result = $"{propertyName}: {value}";
      var compare = "DataTableID: 1";
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
      var dataKeyManager = new DataKeyManager(dbServiceRef, dataConfigName);

      long dataModuleID = 1;
      short dataModuleDbID = 1;
      string name = "TestKeyName";

      // Test Method
      var idKey = dataKeyManager.UniqueKey(dataModuleID, dataModuleDbID
        , name);

      var key = idKey.LJCSearchPropertyName(DataKey.ColumnName);
      var propertyName = key.PropertyName;
      var value = key.Value;
      var result = $"{propertyName}: {value}";
      var compare = "Name: TestKeyName";
      TestCommon.Write($"{methodName}", result, compare);
    }
    #endregion

    #region Joins

    // Creates and returns the Joins object.
    private void GetJoins()
    {
      var methodName = "GetJoins()";

      // See: Run() for the DataConfigs.xml file example.

      // Create the data table manager.
      var dataConfigName = "DataUtility";
      var dbServiceRef = new DbServiceRef()
      {
        DbDataAccess = new DbDataAccess(dataConfigName),
      };
      var dataKeyManager = new DataKeyManager(dbServiceRef, dataConfigName);

      // Test Method
      var joins = dataKeyManager.GetJoins();

      var result = joins[0].TableName;
      var compare = "DataTable";
      TestCommon.Write($"{methodName}", result, compare);
    }
    #endregion

    #region Properties

    // Gets or sets the TestCommon reference.
    private TestCommon TestCommon { get; set; }
    #endregion
  }
}
