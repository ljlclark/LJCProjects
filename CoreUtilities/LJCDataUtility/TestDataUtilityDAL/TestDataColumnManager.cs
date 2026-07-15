// Copyright (c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// TestDataColumnManager.cs
using LJCDataUtilityDAL;
using LJCDBClientLib;
using LJCDBDataAccess;
using LJCNetCommon;
using System.Collections.Generic;

namespace TestDataUtilityDAL
{
  // Tests the DataColumnManager object.
  internal class TestDataColumnManager
  {
    #region Constructor Methods

    // Initializes an object instance.
    /// <include file='Doc/TestDataColumnManager.xml'
    ///  path='members/Constructor/*'/>
    public TestDataColumnManager()
    {
      TestCommon = new TestCommon("TestDataColumnManager");
      Run();
    }
    #endregion

    #region Methods

    // Run the tests.
    /// <include file='Doc/TestDataColumnManager.xml'
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
      RetrieveWithID();
      RetrieveWithUnique();

      // Get Key Methods
      IDKey();
      ParentKey();
      UniqueKey();
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
      var dataColumnManager = new DataColumnManager(dbServiceRef
        , dataConfigName);

      var propertyNames = new List<string>()
      {
        "DataTableID",
        "DataTableSiteID",
        "Name",
      };

      // Test Method
      var columns = dataColumnManager.Columns(propertyNames);

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
      var dataColumnManager = new DataColumnManager(dbServiceRef
        , dataConfigName);

      // Test Method
      var names = dataColumnManager.PropertyNames();
      var result = names[2];
      var compare = "DataTableID";
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
      var dataColumnManager = new DataColumnManager(dbServiceRef
        , dataConfigName);

      short dataSiteID = 1;
      long dataTableID = 1;
      short dataTableSiteID = 1;
      string name = "TestColumnName";

      // Create the test record.
      var dataColumn = new DataUtilColumn()
      {
        DataSiteID = dataSiteID,
        DataTableID = dataTableID,
        DataTableSiteID = dataTableSiteID,
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
      var uniqueColumns = new DbColumns()
      {
        { "DataTableID", dataTableID },
        { "DataTableSiteID", dataTableSiteID },
        { "Name", (object)name },
      };
      dataColumn = dataColumnManager.Retrieve(uniqueColumns);
      var result = dataColumn.Name;
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
      var dbServiceRef = new DbServiceRef()
      {
        DbDataAccess = new DbDataAccess(dataConfigName),
      };
      var dataColumnManager = new DataColumnManager(dbServiceRef
        , dataConfigName);

      long dataTableID = 1;
      short dataTableSiteID = 1;
      string name = "TestColumnName";

      // Create the test record.
      var uniqueColumns = new DbColumns()
      {
        { "DataTableID", dataTableID },
        { "DataTableSiteID", dataTableSiteID },
        { "Name", (object)name },
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
      var dbServiceRef = new DbServiceRef()
      {
        DbDataAccess = new DbDataAccess(dataConfigName),
      };
      var dataColumnManager = new DataColumnManager(dbServiceRef
        , dataConfigName);

      short dataSiteID = 1;
      long dataTableID = 1;
      short dataTableSiteID = 1;
      string name = "TestColumnName";

      // Create the test record.
      var dataUtilTable = new DataUtilColumn()
      {
        DataSiteID = dataSiteID,
        DataTableID = dataTableID,
        DataTableSiteID = dataTableSiteID,
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
      dataColumnManager.Add(dataUtilTable);

      // Test Method
      var dataColumns = dataColumnManager.Load();

      // Verify the records were loaded.
      var result = "";
      if (dataColumns.Count > 0)
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
      dataColumnManager.Delete(uniqueColumns);
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
      var dataColumnManager = new DataColumnManager(dbServiceRef
        , dataConfigName);

      short dataSiteID = 1;
      long dataTableID = 1;
      short dataTableSiteID = 1;
      string name = "TestColumnName";

      // Create the test record.
      var dataColumn = new DataUtilColumn()
      {
        DataSiteID = dataSiteID,
        DataTableID = dataTableID,
        DataTableSiteID = dataTableSiteID,
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
      var uniqueColumns = new DbColumns()
      {
        { "DataTableID", dataTableID },
        { "DataTableSiteID", dataTableSiteID },
        { "Name", (object)name },
      };

      // Test Method
      dataColumn = dataColumnManager.Retrieve(uniqueColumns);

      var result = dataColumn.Name;
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
      var dbServiceRef = new DbServiceRef()
      {
        DbDataAccess = new DbDataAccess(dataConfigName),
      };
      var dataColumnManager = new DataColumnManager(dbServiceRef
        , dataConfigName);

      short dbID = 1;
      long dataTableID = 1;
      short dataTableSiteID = 1;
      string name = "TestColumnName";

      // Create the test record.
      var dataColumn = new DataUtilColumn()
      {
        DataSiteID = dbID,
        DataTableID = dataTableID,
        DataTableSiteID = dataTableSiteID,
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
      var uniqueColumns = new DbColumns()
      {
        { "DataTableID", dataTableID },
        { "DataTableSiteID", dataTableSiteID },
        { "Name", (object)name },
      };
      dataColumn = dataColumnManager.Retrieve(uniqueColumns);
      var result = dataColumn.Name;
      var compare = "TestColumnName";
      TestCommon.Write($"{methodName}1", result, compare);

      // Update the test record.
      var id = dataColumn.ID;
      dbID = dataColumn.DataSiteID;
      var idKey = dataColumnManager.IDKey(id, dbID);
      dataColumn.Description += " Updated";
      var propertyNames = new List<string>()
      {
        "Description",
      };

      // Test Method
      dataColumnManager.Update(dataColumn, idKey, propertyNames);

      // Verify the test record was updated.
      dataColumn = dataColumnManager.RetrieveWithID(id, dbID);
      result = dataColumn.Description;
      compare = "The test column. Updated";
      TestCommon.Write($"{methodName}2", result, compare);

      // Delete the test record.
      dataColumnManager.Delete(uniqueColumns);
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
      var dataColumnManager = new DataColumnManager(dbServiceRef
        , dataConfigName);

      short dbID = 1;
      long dataTableID = 1;
      short dataTableSiteID = 1;
      string name = "TestColumnName";

      // Create the test record.
      var dataColumn = new DataUtilColumn()
      {
        DataSiteID = dbID,
        DataTableID = dataTableID,
        DataTableSiteID = dataTableSiteID,
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
      var uniqueColumns = new DbColumns()
      {
        { "DataTableID", dataTableID },
        { "DataTableSiteID", dataTableSiteID },
        { "Name", (object)name },
      };
      dataColumn = dataColumnManager.Retrieve(uniqueColumns);

      var result = dataColumn.Name;
      var compare = "TestColumnName";
      TestCommon.Write($"{methodName}1", result, compare);

      // Retrieve with ID.
      var id = dataColumn.ID;

      // Test Method
      dataColumn = dataColumnManager.RetrieveWithID(id, dbID);

      // Verify the record was retrieved.
      result = dataColumn.Name;
      compare = "TestColumnName";
      TestCommon.Write($"{methodName}2", result, compare);

      // Delete the test record.
      dataColumnManager.Delete(uniqueColumns);
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
      var dataColumnManager = new DataColumnManager(dbServiceRef
        , dataConfigName);

      short dbID = 1;
      long dataTableID = 1;
      short dataTableDbID = 1;
      string name = "TestTableName";

      // Create the test record.
      var dataColumn = new DataUtilColumn()
      {
        DataSiteID = dbID,
        DataTableID = dataTableID,
        DataTableSiteID = dataTableDbID,
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
      var utilTable = dataColumnManager.RetrieveWithUnique(dataTableID
        , dataTableDbID, name);

      var result = utilTable.Name;
      var compare = "TestTableName";
      TestCommon.Write($"{methodName}", result, compare);

      // Delete the test record.
      var uniqueColumns = new DbColumns()
      {
        { "DataTableID", dataTableID },
        { "DataTableSiteID", dataTableDbID },
        { "Name", (object)name },
      };
      dataColumnManager.Delete(uniqueColumns);
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
      var dataColumnManager = new DataColumnManager(dbServiceRef
        , dataConfigName);

      long id = 1;
      short dbID = 1;

      // Test Method
      var idKey = dataColumnManager.IDKey(id, dbID);

      var key = idKey.LJCSearchPropertyName(DataUtilColumn.ColumnID);
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
      var dataColumnManager = new DataColumnManager(dbServiceRef
        , dataConfigName);

      long dataTableID = 1;
      short dataTableDbID = 1;

      // Test Method
      var idKey = dataColumnManager.ParentKey(dataTableID, dataTableDbID);

      var key = idKey.LJCSearchPropertyName(DataUtilColumn.ColumnDataTableID);
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
      var dataColumnManager = new DataColumnManager(dbServiceRef
        , dataConfigName);

      long dataModuleID = 1;
      short dataModuleDbID = 1;
      string name = "TestTableName";

      // Test Method
      var idKey = dataColumnManager.UniqueKey(dataModuleID, dataModuleDbID
        , name);

      var key = idKey.LJCSearchPropertyName(DataUtilColumn.ColumnName);
      var propertyName = key.PropertyName;
      var value = key.Value;
      var result = $"{propertyName}: {value}";
      var compare = "Name: TestTableName";
      TestCommon.Write($"{methodName}", result, compare);
    }
    #endregion

    #region Properties

    // Gets or sets the TestCommon reference.
    private TestCommon TestCommon { get; set; }
    #endregion
  }
}
