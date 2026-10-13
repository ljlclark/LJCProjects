// Copyright (c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// TestDataKeyManager5.cs
using LJCDataUtilityDAL5;
using LJCDBClientLib5;
using LJCDBDataAccess5;
using LJCNetCommon5;

namespace TestDataUtilityDAL5
{
  // Tests the DataKeyManager object.
  internal class TestDataKeyManager
  {
    #region Properties

    // Gets or sets the TestCommon reference.
    private LJCTestCommon TestCommon { get; set; }
    #endregion

    #region Constructor Methods

    // Initializes an object instance.
    /// <include file='Doc/TestDataKeyManager.xml'
    ///  path='members/Constructor/*'/>
    public TestDataKeyManager()
    {
      TestCommon = new LJCTestCommon("TestDataKeyManager");
      Run();
    }
    #endregion

    #region Methods

    // Run the tests.
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
      RetrieveWithId();
      RetrieveWithParentType();
      RetrieveWithUnique();

      // Get Key Methods
      ForeignKey();
      IdKey();
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
      var dbServiceRef = new LJCDbServiceRef()
      {
        DbDataAccess = new DbDataAccess(dataConfigName),
      };
      var dataKeyManager = new DataKeyManager(dbServiceRef, dataConfigName);

      short dataTableDbId = 1;
      long dataTableId = 1;
      string name = "TestKeyName";

      // Delete the test record.
      var uniqueColumns = new LJCDataColumns()
      {
        { DataKey.ColumnDataTableDbId, dataTableDbId },
        { DataKey.ColumnDataTableId, dataTableId },
        { DataKey.ColumnName, (object)name },
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
      var dbServiceRef = new LJCDbServiceRef()
      {
        DbDataAccess = new DbDataAccess(dataConfigName),
      };
      var dataKeyManager = new DataKeyManager(dbServiceRef, dataConfigName);

      var propertyNames = new List<string>()
      {
        DataKey.ColumnDataTableDbId,
        DataKey.ColumnDataTableId,
        DataKey.ColumnName,
      };

      // Test Method
      var columns = dataKeyManager.Columns(propertyNames);

      var result = "";
      if (LJC.HasListItems(columns))
      {
        var column = columns[DataKey.ColumnName];
        if (column != null)
        {
          result = column.PropertyName;
        }
      }
      var compare = DataKey.ColumnName;
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
      var dataKeyManager = new DataKeyManager(dbServiceRef, dataConfigName);

      // Test Method
      var names = dataKeyManager.PropertyNames();

      var result = "";
      if (LJC.HasListItems(names))
      {
        result = names[2];
      }
      var compare = "DbId";
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
      var dataKeyManager = new DataKeyManager(dbServiceRef
        , dataConfigName);

      short dbId = dataKeyManager.DbId;
      short dataTableDbId = 1;
      long dataTableId = 1;
      string name = "TestKeyName";

      // Create the test record.
      var dataKey = new DataKey()
      {
        DbId = dbId,
        DataTableDbId = dataTableDbId,
        DataTableId = dataTableId,
        Name = name,
      };

      // Test Method
      dataKeyManager.Add(dataKey);

      // Verify the test record was created.
      var uniqueColumns = new LJCDataColumns()
      {
        { DataKey.ColumnDataTableDbId, dataTableDbId },
        { DataKey.ColumnDataTableId, dataTableId },
        { DataKey.ColumnName, name },
      };
      dataKey = dataKeyManager.Retrieve(uniqueColumns);
      var result = "";
      if (dataKey != null)
      {
        result = dataKey.Name;
      }
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
      var dbServiceRef = new LJCDbServiceRef()
      {
        DbDataAccess = new DbDataAccess(dataConfigName),
      };
      var dataKeyManager = new DataKeyManager(dbServiceRef
        , dataConfigName);

      short dataTableDbId = 1;
      long dataTableId = 1;
      string name = "TestKeyName";

      // Create the test record.
      var uniqueColumns = new LJCDataColumns()
      {
        { DataKey.ColumnDataTableDbId, dataTableDbId },
        { DataKey.ColumnDataTableId, dataTableId },
        { DataKey.ColumnName, (object)name },
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
      var dbServiceRef = new LJCDbServiceRef()
      {
        DbDataAccess = new DbDataAccess(dataConfigName),
      };
      var dataKeyManager = new DataKeyManager(dbServiceRef
        , dataConfigName);

      short dbId = dataKeyManager.DbId;
      short dataTableDbId = 1;
      long dataTableId = 1;
      string name = "TestKeyName";

      // Create the test record.
      var dataKey = new DataKey()
      {
        DbId = dbId,
        DataTableId = dataTableId,
        DataTableDbId = dataTableDbId,
        Name = name,
      };
      dataKeyManager.Add(dataKey);

      // Test Method
      var dataKeys = dataKeyManager.Load();

      // Verify the records were loaded.
      var result = "";
      if (LJC.HasListItems(dataKeys))
      {
        result = "HasItems";
      }
      var compare = "HasItems";
      TestCommon.Write($"{methodName}", result, compare);

      // Delete the test record.
      var uniqueColumns = new LJCDataColumns()
      {
        { DataKey.ColumnDataTableDbId, dataTableDbId },
        { DataKey.ColumnDataTableId, dataTableId },
        { DataKey.ColumnName, (object)name },
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
      var dbServiceRef = new LJCDbServiceRef()
      {
        DbDataAccess = new DbDataAccess(dataConfigName),
      };
      var dataKeyManager = new DataKeyManager(dbServiceRef
        , dataConfigName);

      short dbId = dataKeyManager.DbId;
      short dataTableDbId = 1;
      long dataTableId = 1;
      string name = "TestKeyName";

      // Create the test record.
      var dataKey = new DataKey()
      {
        DbId = dbId,
        DataTableId = dataTableId,
        DataTableDbId = dataTableDbId,
        Name = name,
      };
      dataKeyManager.Add(dataKey);

      // Verify the test record was created.
      var uniqueColumns = new LJCDataColumns()
      {
        { DataKey.ColumnDataTableDbId, dataTableDbId },
        { DataKey.ColumnDataTableId, dataTableId },
        { DataKey.ColumnName, (object)name },
      };

      // Test Method
      dataKey = dataKeyManager.Retrieve(uniqueColumns);

      var result = "";
      if (dataKey != null)
      {
        result = dataKey.Name;
      }
      var compare = name;
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
      var dbServiceRef = new LJCDbServiceRef()
      {
        DbDataAccess = new DbDataAccess(dataConfigName),
      };
      var dataKeyManager = new DataKeyManager(dbServiceRef
        , dataConfigName);

      short dbId = dataKeyManager.DbId;
      short dataTableDbId = 1;
      long dataTableId = 1;
      string name = "TestKeyName";

      // Create the test record.
      var dataKey = new DataKey()
      {
        DbId = dbId,
        DataTableDbId = dataTableDbId,
        DataTableId = dataTableId,
        Name = name,
      };
      dataKeyManager.Add(dataKey);

      // Verify the test record was created.
      var uniqueColumns = new LJCDataColumns()
        {
          { DataKey.ColumnDataTableDbId, dataTableDbId },
          { DataKey.ColumnDataTableId, dataTableId },
          { DataKey.ColumnName, name },
        };
      dataKey = dataKeyManager.Retrieve(uniqueColumns);
      var result = "";
      if (dataKey != null)
      {
        result = dataKey.Name;
      }
      var compare = name;
      TestCommon.Write($"{methodName}1", result, compare);

      while (true)
      {
        if (null == dataKey)
        {
          break;
        }

        // Update the test record.
        dbId = dataKey.DbId;
        var id = dataKey.Id;
        var idKey = DataKeyManager.IdKey(dbId, id);
        dataKey.Name += " Updated";
        var propertyNames = new List<string>()
        {
          DataKey.ColumnName,
        };

        // Test Method
        dataKeyManager.Update(dataKey, idKey, propertyNames);

        // Verify the test record was updated.
        dataKey = dataKeyManager.RetrieveWithId(dbId, id);
        result = "";
        if (dataKey != null)
        {
          result = dataKey.Name;
        }
        compare = "TestKeyName Updated";
        TestCommon.Write($"{methodName}2", result, compare);

        // Delete the test record.
        var nameColumn = uniqueColumns[DataKey.ColumnName];
        if (nameColumn != null)
        {
          nameColumn.Value = "TestKeyName Updated";
          dataKeyManager.Delete(uniqueColumns);
        }
        break;
      }
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
      var dbServiceRef = new LJCDbServiceRef()
      {
        DbDataAccess = new DbDataAccess(dataConfigName),
      };
      var dataKeyManager = new DataKeyManager(dbServiceRef, dataConfigName);

      short dbId = dataKeyManager.DbId;
      short dataTableDbId = 1;
      long dataTableId = 1;
      string name = "TestKeyName";
      short foreignKey = 3;

      // Create the test record.
      var dataKey = new DataKey()
      {
        DbId = dbId,
        DataTableDbId = dataTableDbId,
        DataTableId = dataTableId,
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
      var uniqueColumns = new LJCDataColumns()
      {
        { DataKey.ColumnDataTableDbId, dataTableDbId },
        { DataKey.ColumnDataTableId, dataTableId },
        { DataKey.ColumnName, name },
      };
      dataKey = dataKeyManager.Retrieve(uniqueColumns);
      var result = "";
      if (dataKey != null)
      {
        result = dataKey.Name;
      }
      var compare = "TestKeyName";
      TestCommon.Write($"{methodName}1", result, compare);

      // Load with parent foreign keys.
      var tableName = "DataTable";

      // Test Method
      var dataKeys = dataKeyManager.LoadWithForeign(tableName);

      // Verify the record was retrieved.
      dataTableId = 4;
      name = "fk_DataKey_DataTable";
      result = "";
      if (LJC.HasListItems(dataKeys))
      {
        dataKey = dataKeys.LJCGetUnique(dataTableDbId, dataTableId, name);
        if (dataKey != null)
        {
          result = dataKey.Name;
        }
      }
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
      var dbServiceRef = new LJCDbServiceRef()
      {
        DbDataAccess = new DbDataAccess(dataConfigName),
      };
      var dataKeyManager = new DataKeyManager(dbServiceRef, dataConfigName);

      short dbId = dataKeyManager.DbId;
      short dataTableDbId = 1;
      long dataTableId = 1;
      string name = "TestKeyName";

      // Create the test record.
      var dataKey = new DataKey()
      {
        DbId = dbId,
        DataTableId = dataTableId,
        DataTableDbId = dataTableDbId,
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
      var uniqueColumns = new LJCDataColumns()
      {
        { DataKey.ColumnDataTableDbId, dataTableDbId },
        { DataKey.ColumnDataTableId, dataTableId },
        { DataKey.ColumnName, (object)name },
      };
      dataKey = dataKeyManager.Retrieve(uniqueColumns);

      var result = "";
      if (dataKey != null)
      {
        result = dataKey.Name;
      }
      var compare = "TestKeyName";
      TestCommon.Write($"{methodName}1", result, compare);

      while (true)
      {
        if (null == dataKey)
        {
          break;
        }

        // Load with parent.
        dataTableDbId = dataKey.DataTableDbId;
        dataTableId = dataKey.DataTableId;

        // Test Method
        var dataKeys = dataKeyManager.LoadWithParent(dataTableDbId, dataTableId);

        // Verify the record was retrieved.
        result = "";
        if (LJC.HasListItems(dataKeys))
        {
          dataKey = dataKeys.LJCGetUnique(dataTableDbId, dataTableId, name);
          if (dataKey != null)
          {
            result = dataKey.Name;
          }
        }
        compare = "TestKeyName";
        TestCommon.Write($"{methodName}2", result, compare);
        break;
      }

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
      var dbServiceRef = new LJCDbServiceRef()
      {
        DbDataAccess = new DbDataAccess(dataConfigName),
      };
      var dataKeyManager = new DataKeyManager(dbServiceRef, dataConfigName);

      short dbId = dataKeyManager.DbId;
      short dataTableDbId = 1;
      long dataTableId = 1;
      string name = "TestKeyName";

      // Create the test record.
      var dataKey = new DataKey()
      {
        DbId = dbId,
        DataTableDbId = dataTableDbId,
        DataTableId = dataTableId,
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
      var uniqueColumns = new LJCDataColumns()
      {
        { DataKey.ColumnDataTableDbId, dataTableDbId },
        { DataKey.ColumnDataTableId, dataTableId },
        { DataKey.ColumnName, (object)name },
      };
      dataKey = dataKeyManager.Retrieve(uniqueColumns);

      while (true)
      {
        if (null == dataKey)
        {
          break;
        }

        var result = dataKey.Name;
        var compare = "TestKeyName";
        TestCommon.Write($"{methodName}1", result, compare);

        // Load with parent.
        dataTableDbId = dataKey.DataTableDbId;
        dataTableId = dataKey.DataTableId;
        var keyType = 1;

        // Test Method
        var dataKeys = dataKeyManager.LoadWithParentType(dataTableDbId
          , dataTableId, keyType);

        // Verify the record was retrieved.
        result = "";
        if (LJC.HasListItems(dataKeys))
        {
          dataKey = dataKeys.LJCGetUnique(dataTableDbId, dataTableId, name);
          if (dataKey != null)
          {
            result = dataKey.Name;
          }
        }
        compare = "TestKeyName";
        TestCommon.Write($"{methodName}2", result, compare);
        break;
      }

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
      var dbServiceRef = new LJCDbServiceRef()
      {
        DbDataAccess = new DbDataAccess(dataConfigName),
      };
      var dataKeyManager = new DataKeyManager(dbServiceRef, dataConfigName);

      //short dbId = dataKeyManager.DbId;
      short dbId = 1;
      short dataTableDbId = 1;
      long dataTableId = 1;
      string name = "TestKeyName";

      // Create the test record.
      var dataKey = new DataKey()
      {
        DbId = dbId,
        DataTableDbId = dataTableDbId,
        DataTableId = dataTableId,
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
      var uniqueColumns = new LJCDataColumns()
      {
        { DataKey.ColumnDataTableDbId, dataTableDbId },
        { DataKey.ColumnDataTableId, dataTableId },
        { DataKey.ColumnName, (object)name },
      };
      dataKey = dataKeyManager.Retrieve(uniqueColumns);

      while (true)
      {
        if (null == dataKey)
        {
          break;
        }

        var result = dataKey.Name;
        var compare = "TestKeyName";
        TestCommon.Write($"{methodName}1", result, compare);

        // Load with type.
        dataTableDbId = dataKey.DataTableDbId;
        dataTableId = dataKey.DataTableId;
        short keyType = 1;

        // Test Method
        //var dataKeys = dataKeyManager.LoadWithType(id, dbID, keyType);
        var dataKeys = dataKeyManager.LoadWithType(dataTableDbId, dataTableId
          , keyType);

        // Verify the record was retrieved.
        result = "";
        if (LJC.HasListItems(dataKeys))
        {
          dataKey = dataKeys.LJCGetWithID(dataKey.DbId, dataKey.Id);
          if (dataKey != null)
          {
            result = dataKey.Name;
          }
        }
        compare = "TestKeyName";
        TestCommon.Write($"{methodName}2", result, compare);
        break;
      }

      // Delete the test record.
      dataKeyManager.Delete(uniqueColumns);
    }

    // Retrieves a record with the supplied ID values.
    private void RetrieveWithId()
    {
      var methodName = "RetrieveWithID()";

      // See: Run() for the DataConfigs.xml file example.

      // Create the data table manager.
      var dataConfigName = "DataUtility";
      var dbServiceRef = new LJCDbServiceRef()
      {
        DbDataAccess = new DbDataAccess(dataConfigName),
      };
      var dataKeyManager = new DataKeyManager(dbServiceRef, dataConfigName);

      short dbId = dataKeyManager.DbId;
      short dataTableDbId = 1;
      long dataTableId = 1;
      string name = "TestKeyName";

      // Create the test record.
      var dataKey = new DataKey()
      {
        DbId = dbId,
        DataTableDbId = dataTableDbId,
        DataTableId = dataTableId,
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
      var uniqueColumns = new LJCDataColumns()
      {
        { DataKey.ColumnDataTableDbId, dataTableDbId },
        { DataKey.ColumnDataTableId, dataTableId },
        { DataKey.ColumnName, (object)name },
      };
      dataKey = dataKeyManager.Retrieve(uniqueColumns);

      var result = "";
      if (dataKey != null)
      {
        result = dataKey.Name;
      }
      var compare = name;
      TestCommon.Write($"{methodName}1", result, compare);

      while (true)
      {
        if (null == dataKey)
        {
          break;
        }

        // Retrieve with ID.
        var id = dataKey.Id;

        // Test Method
        dataKey = dataKeyManager.RetrieveWithId(dbId, id);

        // Verify the record was retrieved.
        result = "";
        if (dataKey != null)
        {
          result = dataKey.Name;
        }
        compare = name;
        TestCommon.Write($"{methodName}2", result, compare);
        break;
      }

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
      var dbServiceRef = new LJCDbServiceRef()
      {
        DbDataAccess = new DbDataAccess(dataConfigName),
      };
      var dataKeyManager = new DataKeyManager(dbServiceRef, dataConfigName);

      //short dbId = dataKeyManager.DbId;
      short dbId = 1;
      short dataTableDbId = 1;
      long dataTableId = 1;
      string name = "TestKeyName";

      // Create the test record.
      var dataKey = new DataKey()
      {
        DbId = dbId,
        DataTableDbId = dataTableDbId,
        DataTableId = dataTableId,
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
      var uniqueColumns = new LJCDataColumns()
      {
        { DataKey.ColumnDataTableDbId, dataTableDbId },
        { DataKey.ColumnDataTableId, dataTableId },
        { DataKey.ColumnName, name },
      };
      dataKey = dataKeyManager.Retrieve(uniqueColumns);

      var result = "";
      if (dataKey != null)
      {
        result = dataKey.Name;
      }
      var compare = "TestKeyName";
      TestCommon.Write($"{methodName}1", result, compare);

      while (true)
      {
        if (null == dataKey)
        {
          break;
        }

        // Retrieve with parent KeyType.
        short parentDbId = dataKey.DataTableDbId;
        var parentId = dataKey.DataTableId;
        var keyType = dataKey.KeyType;

        // Test Method
        dataKey = dataKeyManager.RetrieveWithParentType(parentDbId, parentId
          , keyType);

        // Verify the record was retrieved.
        result = "";
        if (dataKey != null)
        {
          result = dataKey.Name;
        }
        compare = "pk_DataModule";
        TestCommon.Write($"{methodName}2", result, compare);
        break;
      }

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
      var dbServiceRef = new LJCDbServiceRef()
      {
        DbDataAccess = new DbDataAccess(dataConfigName),
      };
      var dataKeyManager = new DataKeyManager(dbServiceRef, dataConfigName);

      short dbId = dataKeyManager.DbId;
      short dataTableDbId = 1;
      long dataTableId = 1;
      string name = "TestKeyName";

      // Create the test record.
      var dataKey = new DataKey()
      {
        DbId = dbId,
        DataTableDbId = dataTableDbId,
        DataTableId = dataTableId,
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
      dataKey = dataKeyManager.RetrieveUnique(dataTableDbId, dataTableId
        , name);

      var result = "";
      if (dataKey != null)
      {
        result = dataKey.Name;
      }
      var compare = "TestKeyName";
      TestCommon.Write($"{methodName}", result, compare);

      // Delete the test record.
      var uniqueColumns = new LJCDataColumns()
      {
        { DataKey.ColumnDataTableDbId, dataTableDbId },
        { DataKey.ColumnDataTableId, dataTableId },
        { DataKey.ColumnName, name },
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

      var targetTableName = "DataKey";

      // Test Method
      var idKey = DataKeyManager.ForeignKey(targetTableName);

      var result = "";
      if (idKey != null)
      {
        //var key = idKey.LJCSearchPropertyName(DataKey.ColumnKeyType);
        var key = idKey[DataKey.ColumnKeyType];
        if (key != null)
        {
          var propertyName = key.PropertyName;
          var value = key.Value;
          result = $"{propertyName}: {value}";
        }
      }
      var compare = "KeyType: 3";
      TestCommon.Write($"{methodName}", result, compare);
    }

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
      var dataKeyManager = new DataKeyManager(dbServiceRef, dataConfigName);

#pragma warning disable IDE0059 // Unnecessary assignment of a value
      short dbId = dataKeyManager.DbId;
#pragma warning restore IDE0059 // Unnecessary assignment of a value
      dbId = 1;
      long id = 1;

      // Test Method
      var idKey = DataKeyManager.IdKey(dbId, id);

      var result = "";
      if (idKey != null)
      {
        var key = idKey[DataKey.ColumnId];
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

    // Gets the Parent ID key columns.
    private void ParentKey()
    {
      var methodName = "ParentKey()";

      // See: Run() for the DataConfigs.xml file example.

      short dataTableDbId = 1;
      long dataTableId = 1;

      // Test Method
      var idKey = DataKeyManager.ParentKey(dataTableDbId, dataTableId);

      var result = "";
      if (idKey != null)
      {
        var key = idKey[DataKey.ColumnDataTableId];
        if (key != null)
        {
          var propertyName = key.PropertyName;
          var value = key.Value;
          result = $"{propertyName}: {value}";
        }
      }
      var compare = "DataTableId: 1";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Gets the parent KeyType key columns.
    private void ParentTypeKey()
    {
      var methodName = "ParentTypeKey()";

      // See: Run() for the DataConfigs.xml file example.

      short dataTableDbId = 1;
      long dataTableId = 1;
      int keyType = 1;

      // Test Method
      var idKey = DataKeyManager.ParentTypeKey(dataTableDbId, dataTableId
        , keyType);

      var result = "";
      if (idKey != null)
      {
        var key = idKey[DataKey.ColumnDataTableId];
        if (key != null)
        {
          var propertyName = key.PropertyName;
          var value = key.Value;
          result = $"{propertyName}: {value}";
        }
      }
      var compare = "DataTableId: 1";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Gets the Unique ID key columns.
    private void UniqueKey()
    {
      var methodName = "UniqueKey()";

      // See: Run() for the DataConfigs.xml file example.

      short dataModuleDbId = 1;
      long dataModuleId = 1;
      string name = "TestKeyName";

      // Test Method
      var uniqueKey = DataKeyManager.UniqueKey(dataModuleDbId, dataModuleId
        , name);

      var result = "";
      if (uniqueKey != null)
      {
        var key = uniqueKey[DataKey.ColumnName];
        if (key != null)
        {
          var propertyName = key.PropertyName;
          var value = key.Value;
          result = $"{propertyName}: {value}";
        }
      }
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

      // Test Method
      var joins = DataKeyManager.GetJoins();

      var result = "";
      if (LJC.HasListItems(joins))
      {
        result = joins[0].TableName;
      }
      var compare = "DataTable";
      TestCommon.Write($"{methodName}", result, compare);
    }
    #endregion
  }
}
