// Copyright (c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// TestDataColumnManager5.cs
using LJCDataUtilityDAL5;
using LJCDBClientLib5;
using LJCDBDataAccess5;
using LJCNetCommon5;

namespace TestDataUtilityDAL5
{
  // Tests the DataColumnManager object.
  internal class TestDataColumnManager
  {
    #region Properties

    // Gets or sets the TestCommon reference.
    private LJCTestCommon TestCommon { get; set; }
    #endregion

    #region Constructor Methods

    // Initializes an object instance.
    /// <include file='Doc/TestDataColumnManager.xml'
    ///  path='members/Constructor/*'/>
    public TestDataColumnManager()
    {
      TestCommon = new LJCTestCommon("TestDataColumnManager");
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
      RetrieveWithID();
      RetrieveUnique();

      // Get Key Methods
      IdKey();
      ParentKey();
      UniqueKey();
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
      var dataColumnManager = new DataColumnManager(dbServiceRef
        , dataConfigName);

      var propertyNames = new List<string>()
      {
        "DataTableDbId",
        "DataTableId",
        "Name",
      };

      // Test Method
      var columns = dataColumnManager.Columns(propertyNames);

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
      var dataColumnManager = new DataColumnManager(dbServiceRef
        , dataConfigName);

      // Test Method
      var names = dataColumnManager.PropertyNames();

      var result = "";
      if (LJC.HasListItems(names))
      {
        result = names[2];
      }
      var compare = "DataTableId";
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
      var dataColumnManager = new DataColumnManager(dbServiceRef
        , dataConfigName);

      short dbId = dataColumnManager.DbId;
      short dataTableDbId = 1;
      long dataTableId = 1;
      string name = "TestColumnName";

      // Create the test record.
      var dataColumn = new DataUtilColumn()
      {
        DbId = dbId,
        DataTableDbId = dataTableDbId,
        DataTableId = dataTableId,
        Name = name,
        Description = "The test column.",
        Sequence = 5,
        AllowNull = true,
        DefaultValue = null,
        IdentityStart = 1,
        IdentityIncrement = 1,
        MaxLength = 1,
        NewName = null,
        NewMaxLength = 1,
        TypeName = "TypeName",
      };

      // Test Method
      dataColumnManager.Add(dataColumn);

      // Verify the test record was created.
      var uniqueColumns = new LJCDataColumns()
      {
        { "DataTableDbId", dataTableDbId },
        { "DataTableId", dataTableId },
        { "Name", name },
      };
      dataColumn = dataColumnManager.Retrieve(uniqueColumns);
      var result = "";
      if (dataColumn != null)
      {
        result = dataColumn.Name;
      }
      var compare = "TestColumnName";
      TestCommon.Write($"{methodName}", result, compare);

      // Delete the test record.
      dataColumnManager.Delete(uniqueColumns);
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
      var dataColumnManager = new DataColumnManager(dbServiceRef
        , dataConfigName);

      short dataTableDbId = 1;
      long dataTableId = 1;
      string name = "TestColumnName";

      // Create the test record.
      var uniqueColumns = new LJCDataColumns()
      {
        { "DataTableDbId", dataTableDbId },
        { "DataTableId", dataTableId },
        { "Name", name },
      };
      var dataColumn = dataColumnManager.Retrieve(uniqueColumns);
      if (dataColumn != null)
      {
        var result = dataColumn.Name;
        var compare = "TestColumnName";
        TestCommon.Write($"{methodName}1", result, compare);

        // Test Method
        dataColumnManager.Delete(uniqueColumns);

        // Verify the test record was deleted.
        dataColumn = dataColumnManager.Retrieve(uniqueColumns);
        if (null == dataColumn)
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
      var dataColumnManager = new DataColumnManager(dbServiceRef
        , dataConfigName);

      short dbId = dataColumnManager.DbId;
      short dataTableDbId = 1;
      long dataTableId = 1;
      string name = "TestColumnName";

      // Create the test record.
      var dataUtilColumn = new DataUtilColumn()
      {
        DbId = dbId,
        DataTableDbId = dataTableDbId,
        DataTableId = dataTableId,
        Name = name,
        Description = "The test table.",
        Sequence = 5,
        AllowNull = true,
        DefaultValue = null,
        IdentityStart = 1,
        IdentityIncrement = 1,
        MaxLength = 1,
        NewName = null,
        NewMaxLength = 1,
        TypeName = "TypeName",
      };
      dataColumnManager.Add(dataUtilColumn);

      // Test Method
      var dataColumns = dataColumnManager.Load();

      // Verify the records were loaded.
      var result = "";
      if (LJC.HasListItems(dataColumns))
      {
        result = "HasItems";
      }
      var compare = "HasItems";
      TestCommon.Write($"{methodName}", result, compare);

      // Delete the test record.
      var uniqueColumns = new LJCDataColumns()
      {
        { "DataTableDbId", dataTableDbId },
        { "DataTableId", dataTableId },
        { "Name", (object)name },
      };
      dataColumnManager.Delete(uniqueColumns);
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
      var dataColumnManager = new DataColumnManager(dbServiceRef
        , dataConfigName);

      short dbId = dataColumnManager.DbId;
      short dataTableDbId = 1;
      long dataTableId = 1;
      string name = "TestColumnName";

      // Create the test record.
      var dataColumn = new DataUtilColumn()
      {
        DbId = dbId,
        DataTableDbId = dataTableDbId,
        DataTableId = dataTableId,
        Name = name,
        Description = "The test table.",
        Sequence = 5,
        AllowNull = true,
        DefaultValue = null,
        IdentityStart = 1,
        IdentityIncrement = 1,
        MaxLength = 1,
        NewName = null,
        NewMaxLength = 1,
        TypeName = "TypeName",
      };
      dataColumnManager.Add(dataColumn);

      // Verify the test record was created.
      var uniqueColumns = new LJCDataColumns()
      {
        { "DataTableDbId", dataTableDbId },
        { "DataTableId", dataTableId },
        { "Name", name },
      };

      // Test Method
      dataColumn = dataColumnManager.Retrieve(uniqueColumns);

      var result = "";
      if (dataColumn != null)
      {
        result = dataColumn.Name;
      }
      var compare = "TestColumnName";
      TestCommon.Write($"{methodName}", result, compare);

      // Delete the test record.
      dataColumnManager.Delete(uniqueColumns);
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
      var dataColumnManager = new DataColumnManager(dbServiceRef
        , dataConfigName);

      short dbId = dataColumnManager.DbId;
      short dataTableDbId = 1;
      long dataTableId = 1;
      string name = "TestColumnName";

      // Create the test record.
      var dataColumn = new DataUtilColumn()
      {
        DbId = dbId,
        DataTableDbId = dataTableDbId,
        DataTableId = dataTableId,
        Name = name,
        Description = "The test column.",
        Sequence = 5,
        AllowNull = true,
        DefaultValue = null,
        IdentityStart = 1,
        IdentityIncrement = 1,
        MaxLength = 1,
        NewName = null,
        NewMaxLength = 1,
        TypeName = "TypeName",
      };
      dataColumnManager.Add(dataColumn);

      // Verify the test record was created.
      var uniqueColumns = new LJCDataColumns()
        {
          { "DataTableDbId", dataTableDbId },
          { "DataTableId", dataTableId },
          { "Name", (object)name },
        };
      dataColumn = dataColumnManager.Retrieve(uniqueColumns);
      var result = "";
      if (dataColumn != null)
      {
        result = dataColumn.Name;
      }
      var compare = "TestColumnName";
      TestCommon.Write($"{methodName}1", result, compare);

      while (true)
      {
        if (null == dataColumn)
        {
          break;
        }

        // Update the test record.
        dbId = dataColumn.DbId;
        var id = dataColumn.Id;
        var idKey = DataColumnManager.IdKey(dbId, id);
        dataColumn.Description += " Updated";
        var propertyNames = new List<string>()
        {
          "Description",
        };

        // Test Method
        dataColumnManager.Update(dataColumn, idKey, propertyNames);

        // Verify the test record was updated.
        dataColumn = dataColumnManager.RetrieveWithId(dbId, id);
        result = "";
        if (dataColumn != null)
        {
          result = dataColumn.Description;
        }
        compare = "The test column. Updated";
        TestCommon.Write($"{methodName}2", result, compare);

        // Delete the test record.
        dataColumnManager.Delete(uniqueColumns);
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
      var dataColumnManager = new DataColumnManager(dbServiceRef
        , dataConfigName);

      short dbId = dataColumnManager.DbId;
      short dataTableDbId = 1;
      long dataTableId = 1;
      string name = "TestColumnName";

      // Create the test record.
      var dataColumn = new DataUtilColumn()
      {
        DbId = dbId,
        DataTableDbId = dataTableDbId,
        DataTableId = dataTableId,
        Name = name,
        Description = "The test table.",
        Sequence = 5,
        AllowNull = true,
        DefaultValue = null,
        IdentityStart = 1,
        IdentityIncrement = 1,
        MaxLength = 1,
        NewName = null,
        NewMaxLength = 1,
        TypeName = "TypeName",
      };
      dataColumnManager.Add(dataColumn);

      // Verify the test record was created.
      var uniqueColumns = new LJCDataColumns()
      {
        { "DataTableDbId", dataTableDbId },
        { "DataTableId", dataTableId },
        { "Name", (object)name },
      };
      dataColumn = dataColumnManager.Retrieve(uniqueColumns);

      var result = "";
      if (dataColumn != null)
      {
        result = dataColumn.Name;
      }
      var compare = "TestColumnName";
      TestCommon.Write($"{methodName}1", result, compare);

      while (true)
      {
        if (null == dataColumn)
        {
          break;
        }

        // Retrieve with ID.
        var id = dataColumn.Id;

        // Test Method
        dataColumn = dataColumnManager.RetrieveWithId(dbId, id);

        // Verify the record was retrieved.
        result = "";
        if (dataColumn != null)
        {
          result = dataColumn.Name;
        }
        compare = "TestColumnName";
        TestCommon.Write($"{methodName}2", result, compare);

        // Delete the test record.
        dataColumnManager.Delete(uniqueColumns);
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
      var dataColumnManager = new DataColumnManager(dbServiceRef
        , dataConfigName);

      short dbId = dataColumnManager.DbId;
      short dataTableDbId = 1;
      long dataTableId = 1;
      string name = "TestTableName";

      // Create the test record.
      var dataColumn = new DataUtilColumn()
      {
        DbId = dbId,
        DataTableDbId = dataTableDbId,
        DataTableId = dataTableId,
        Name = name,
        Description = "The test table.",
        Sequence = 5,
        AllowNull = true,
        DefaultValue = null,
        IdentityStart = 1,
        IdentityIncrement = 1,
        MaxLength = 1,
        NewName = null,
        NewMaxLength = 1,
        TypeName = "TypeName",
      };
      dataColumnManager.Add(dataColumn);

      // Test Method
      dataColumn = dataColumnManager.RetrieveUnique(dataTableDbId
        , dataTableId, name);

      var result = "";
      if (dataColumn != null)
      {
        result = dataColumn.Name;
      }
      var compare = "TestTableName";
      TestCommon.Write($"{methodName}", result, compare);

      // Delete the test record.
      var uniqueColumns = new LJCDataColumns()
      {
        { "DataTableDbId", dataTableDbId },
        { "DataTableId", dataTableId },
        { "Name", (object)name },
      };
      dataColumnManager.Delete(uniqueColumns);
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
      var dataColumnManager = new DataColumnManager(dbServiceRef
        , dataConfigName);

#pragma warning disable IDE0059 // Unnecessary assignment of a value
      short dbId = dataColumnManager.DbId;
#pragma warning restore IDE0059 // Unnecessary assignment of a value
      dbId = 1;
      long id = 1;

      // Test Method
      var idKey = DataColumnManager.IdKey(dbId, id);

      var result = "";
      if (idKey != null)
      {
        var key = idKey[DataUtilColumn.ColumnId];
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
      var idKey = DataColumnManager.ParentKey(dataTableDbId, dataTableId);

      var result = "";
      if (idKey != null)
      {
        var key = idKey[DataUtilColumn.ColumnDataTableId];
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
      string name = "TestColumnName";

      // Test Method
      var uniqueKey = DataColumnManager.UniqueKey(dataModuleDbId, dataModuleId
        , name);

      var result = "";
      if (uniqueKey != null)
      {
        var key = uniqueKey[DataUtilColumn.ColumnName];
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
  }
}
