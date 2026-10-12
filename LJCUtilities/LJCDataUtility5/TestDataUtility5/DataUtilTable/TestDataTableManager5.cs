// Copyright (c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// TestDataTableManager5.cs
using LJCDataUtilityDAL5;
using LJCDBClientLib5;
using LJCDBDataAccess5;
using LJCNetCommon5;
using System.Data;

namespace TestDataUtilityDAL5
{
  // Tests the DataTableManager object.
  internal class TestDataTableManager
  {
    #region Properties

    // Gets or sets the TestCommon reference.
    private LJCTestCommon TestCommon { get; set; }
    #endregion

    #region Constructor Methods

    // Initializes an object instance.
    /// <include file='Doc/TestDataTableManager.xml'
    ///  path='members/Constructor/*'/>
    public TestDataTableManager()
    {
      TestCommon = new LJCTestCommon("TestDataTableManager");
      Run();
    }
    #endregion

    #region Methods

    // Run the tests.
    /// <include file='Doc/TestDataTableManager.xml'
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

      // DataTable Data Methods
      Add();
      Delete();
      Load();
      Retrieve();
      Update();

      // Custom Data Methods
      RetrieveWithID();
      RetrieveUnique();

      // Get Key Methods
      IdKey();
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
      var dbServiceRef = new LJCDbServiceRef()
      {
        DbDataAccess = new DbDataAccess(dataConfigName),
      };
      var dataTableManager = new DataTableManager(dbServiceRef, dataConfigName);

      short dataModuleDbId = 1;
      long dataModuleId = 1;
      string name = "TestTableName";

      // Delete the test record.
      var uniqueColumns = new LJCDataColumns()
      {
        { "DataModuleDbId", dataModuleDbId },
        { "DataModuleId", dataModuleId },
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
      var dataTableManager = new DataTableManager(dbServiceRef, dataConfigName);

      var propertyNames = new List<string>()
      {
        "DataModuleDbId",
        "DataModuleId",
        "Name",
      };

      // Test Method
      var columns = dataTableManager.Columns(propertyNames);

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
      var dataTableManager = new DataTableManager(dbServiceRef, dataConfigName);

      // Test Method
      var names = dataTableManager.PropertyNames();

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
      var dataTableManager = new DataTableManager(dbServiceRef, dataConfigName);

      short dbId = dataTableManager.DbId;
      short dataModuleDbId = 1;
      long dataModuleId = 1;
      string name = "TestTableName";

      // Create the test record.
      var dataUtilTable = new DataUtilTable()
      {
        DbId = dbId,
        DataModuleDbId = dataModuleDbId,
        DataModuleId = dataModuleId,
        Name = name,
        Description = "The test table.",
        Sequence = 5,
        SchemaName = "dbo",
        NewName = null,
      };

      // Test Method
      dataTableManager.Add(dataUtilTable);

      // Verify the test record was created.
      var uniqueColumns = new LJCDataColumns()
      {
        { "DataModuleDbId", dataModuleDbId },
        { "DataModuleId", dataModuleId },
        { "Name", name },
      };
      dataUtilTable = dataTableManager.Retrieve(uniqueColumns);
      var result = "";
      if (dataUtilTable != null)
      {
        result = dataUtilTable.Name;
      }
      var compare = "TestTableName";
      TestCommon.Write($"{methodName}", result, compare);

      // Delete the test record.
      dataTableManager.Delete(uniqueColumns);
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
      var dataTableManager = new DataTableManager(dbServiceRef, dataConfigName);

      long dataModuleDbId = 1;
      long dataModuleId = 1;
      string name = "TestTableName";

      // Create the test record.
      var uniqueColumns = new LJCDataColumns()
      {
        { "DataModuleDbId", dataModuleDbId },
        { "DataModuleId", dataModuleId },
        { "Name", name },
      };
      var utilTable = dataTableManager.Retrieve(uniqueColumns);
      if (utilTable != null)
      {
        var result = utilTable.Name;
        var compare = "TestTableName";
        TestCommon.Write($"{methodName}1", result, compare);

        // Test Method
        dataTableManager.Delete(uniqueColumns);

        // Verify the test record was deleted.
        utilTable = dataTableManager.Retrieve(uniqueColumns);
        if (null == utilTable)
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
      var dataTableManager = new DataTableManager(dbServiceRef, dataConfigName);

      short dbId = dataTableManager.DbId;
      short dataModuleDbId = 1;
      long dataModuleId = 1;
      string name = "TestTableName";

      // Create the test record.
      var dataUtilTable = new DataUtilTable()
      {
        DbId = dbId,
        DataModuleDbId = dataModuleDbId,
        DataModuleId = dataModuleId,
        Name = name,
        Description = "The test table.",
        Sequence = 5,
        SchemaName = "dbo",
        NewName = null,
      };
      dataTableManager.Add(dataUtilTable);

      // Test Method
      var dataTables = dataTableManager.Load();

      // Verify the records were loaded.
      var result = "";
      if (LJC.HasListItems(dataTables))
      {
        result = "HasItems";
      }
      var compare = "HasItems";
      TestCommon.Write($"{methodName}", result, compare);

      // Delete the test record.
      var uniqueColumns = new LJCDataColumns()
      {
        { "DataModuleDbId", dataModuleDbId },
        { "DataModuleId", dataModuleId },
        { "Name", (object)name },
      };
      dataTableManager.Delete(uniqueColumns);
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
      var dataTableManager = new DataTableManager(dbServiceRef, dataConfigName);

      short dbID = dataTableManager.DbId;
      short dataModuleDbId = 1;
      long dataModuleId = 1;
      string name = "TestTableName";

      // Create the test record.
      var dataUtilTable = new DataUtilTable()
      {
        DbId = dbID,
        DataModuleDbId = dataModuleDbId,
        DataModuleId = dataModuleId,
        Name = name,
        Description = "The test table.",
        Sequence = 5,
        SchemaName = "dbo",
        NewName = null,
      };
      dataTableManager.Add(dataUtilTable);

      // Verify the test record was created.
      var uniqueColumns = new LJCDataColumns()
      {
        { "DataModuleDbId", dataModuleDbId },
        { "DataModuleId", dataModuleId },
        { "Name", name },
      };

      // Test Method
      var utilTable = dataTableManager.Retrieve(uniqueColumns);

      var result = "";
      if (utilTable != null)
      {
        result = utilTable.Name;
      }
      var compare = "TestTableName";
      TestCommon.Write($"{methodName}", result, compare);

      // Delete the test record.
      dataTableManager.Delete(uniqueColumns);
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
      var dataTableManager = new DataTableManager(dbServiceRef, dataConfigName);

      short dbId = dataTableManager.DbId;
      short dataModuleDbId = 1;
      long dataModuleId = 1;
      string name = "TestTableName";

      // Create the test record.
      var dataUtilTable = new DataUtilTable()
      {
        DbId = dbId,
        DataModuleDbId = dataModuleDbId,
        DataModuleId = dataModuleId,
        Name = name,
        Description = "The test table.",
        Sequence = 5,
        SchemaName = "dbo",
        NewName = null,
      };
      dataTableManager.Add(dataUtilTable);

      // Verify the test record was created.
      var uniqueColumns = new LJCDataColumns()
      {
        { "DataModuleDbId", dataModuleDbId },
        { "DataModuleId", dataModuleId },
        { "Name", (object)name },
      };
      var utilTable = dataTableManager.Retrieve(uniqueColumns);
      var result = "";
      if (utilTable != null)
      {
        result = utilTable.Name;
      }
      var compare = "TestTableName";
      TestCommon.Write($"{methodName}1", result, compare);

      while (true)
      {
        if (null == utilTable)
        {
          break;
        }

        // Update the test record.
        dbId = utilTable.DbId;
        var id = utilTable.Id;
        var idKey = DataTableManager.IdKey(dbId, id);
        utilTable.Description += " Updated";
        var propertyNames = new List<string>()
        {
          "Description",
        };

        // Test Method
        dataTableManager.Update(utilTable, idKey, propertyNames);

        // Verify the test record was updated.
        utilTable = dataTableManager.RetrieveWithId(dbId, id);
        result = "";
        if (utilTable != null)
        {
          result = utilTable.Description;
        }
        compare = "The test table. Updated";
        TestCommon.Write($"{methodName}2", result, compare);

        // Delete the test record.
        dataTableManager.Delete(uniqueColumns);
        break;
      }
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
      var dbServiceRef = new LJCDbServiceRef()
      {
        DbDataAccess = new DbDataAccess(dataConfigName),
      };
      var dataTableManager = new DataTableManager(dbServiceRef, dataConfigName);

      short dbId = dataTableManager.DbId;
      short dataModuleDbId = 1;
      long dataModuleId = 1;
      string name = "TestTableName";

      // Create the test record.
      var dataUtilTable = new DataUtilTable()
      {
        DbId = dbId,
        DataModuleDbId = dataModuleDbId,
        DataModuleId = dataModuleId,
        Name = name,
        Description = "The test table.",
        Sequence = 5,
        SchemaName = "dbo",
        NewName = null,
      };
      dataTableManager.Add(dataUtilTable);

      // Verify the test record was created.
      var uniqueColumns = new LJCDataColumns()
      {
        { "DataModuleDbId", dataModuleDbId },
        { "DataModuleId", dataModuleId },
        { "Name", (object)name },
      };
      var utilTable = dataTableManager.Retrieve(uniqueColumns);

      var result = "";
      if (utilTable != null)
      {
        result = utilTable.Name;
      }
      var compare = "TestTableName";
      TestCommon.Write($"{methodName}1", result, compare);

      while (true)
      {
        if (null == utilTable)
        {
          break;
        }

        // Retrieve with ID.
        var id = utilTable.Id;

        // Test Method
        utilTable = dataTableManager.RetrieveWithId(dbId, id);

        // Verify the record was retrieved.
        result = "";
        if (utilTable != null)
        {
          result = utilTable.Name;
        }
        compare = "TestTableName";
        TestCommon.Write($"{methodName}2", result, compare);

        // Delete the test record.
        dataTableManager.Delete(uniqueColumns);
        break;
      }
    }

    // Retrieves a record with the supplied unique values.
    private void RetrieveUnique()
    {
      var methodName = "RetrieveUnique()";

      // See: Run() for the DataConfigs.xml file example.

      // Create the data table manager.
      var dataConfigName = "DataUtility";
      var dbServiceRef = new LJCDbServiceRef()
      {
        DbDataAccess = new DbDataAccess(dataConfigName),
      };
      var dataTableManager = new DataTableManager(dbServiceRef, dataConfigName);

      short dbId = dataTableManager.DbId;
      short dataModuleDbId = 1;
      long dataModuleId = 1;
      string name = "TestTableName";

      // Create the test record.
      var dataUtilTable = new DataUtilTable()
      {
        DbId = dbId,
        DataModuleDbId = dataModuleDbId,
        DataModuleId = dataModuleId,
        Name = name,
        Description = "The test table.",
        Sequence = 5,
        SchemaName = "dbo",
        NewName = null,
      };
      dataTableManager.Add(dataUtilTable);

      // Test Method
      var utilTable = dataTableManager.RetrieveUnique(dataModuleDbId
        , dataModuleId, name);

      var result = "";
      if (utilTable != null)
      {
        result = utilTable.Name;
      }
      var compare = "TestTableName";
      TestCommon.Write($"{methodName}", result, compare);

      // Delete the test record.
      var uniqueColumns = new LJCDataColumns()
      {
        { "DataModuleDbId", dataModuleDbId },
        { "DataModuleId", dataModuleId },
        { "Name", (object)name },
      };
      dataTableManager.Delete(uniqueColumns);
    }
    #endregion

    #region GetKey Methods

    // Gets the ID key columns.
    private void IdKey()
    {
      var methodName = "IdKey()";

      // See: Run() for the DataConfigs.xml file example.

      // Create the data table manager.
      var dataConfigName = "DataUtility";
      var dbServiceRef = new LJCDbServiceRef()
      {
        DbDataAccess = new DbDataAccess(dataConfigName),
      };
      var dataTableManager = new DataTableManager(dbServiceRef, dataConfigName);

      short dbId = dataTableManager.DbId;
      long id = 1;

      // Test Method
      var idKey = DataTableManager.IdKey(dbId, id);

      var result = "";
      if (idKey != null)
      {
        var key = idKey[DataUtilTable.ColumnId];
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

      //// Create the data table manager.
      //var dataConfigName = "DataUtility";
      //var dbServiceRef = new DbServiceRef()
      //{
      //  DbDataAccess = new DbDataAccess(dataConfigName),
      //};
      //var dataTableManager = new DataTableManager(dbServiceRef, dataConfigName);

      short dataModuleDbId = 1;
      long dataModuleId = 1;

      // Test Method
      var idKey = DataTableManager.ParentKey(dataModuleDbId, dataModuleId);

      var result = "";
      if (idKey != null)
      {
        var key = idKey[DataUtilTable.ColumnDataModuleId];
        if (key != null)
        {
          var propertyName = key.PropertyName;
          var value = key.Value;
          result = $"{propertyName}: {value}";
        }
      }
      var compare = "DataModuleId: 1";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Gets the Unique ID key columns.
    private void UniqueKey()
    {
      var methodName = "UniqueKey()";

      // See: Run() for the DataConfigs.xml file example.

      short dataModuleDbId = 1;
      long dataModuleId = 1;
      string name = "TestTableName";

      // Test Method
      var idKey = DataTableManager.UniqueKey(dataModuleDbId, dataModuleId
        , name);

      var result = "";
      if (idKey != null)
      {
        var key = idKey[DataUtilTable.ColumnName];
        if (key != null)
        {
          var propertyName = key.PropertyName;
          var value = key.Value;
          result = $"{propertyName}: {value}";
        }
      }
      var compare = $"Name: {name}";
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
      var joins = DataTableManager.GetJoins();

      var result = joins[0].TableName;
      var compare = "DataModule";
      TestCommon.Write($"{methodName}", result, compare);
    }
    #endregion
  }
}
