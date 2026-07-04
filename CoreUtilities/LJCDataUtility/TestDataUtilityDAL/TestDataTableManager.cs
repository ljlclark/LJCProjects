// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// TestDataTableManager.cs
using LJCDataUtilityDAL;
using LJCDBMessage;
using LJCNetCommon;
using System.Reflection;
using System.Runtime.InteropServices;

namespace TestDataUtilityDAL
{
  // Tests the DataTableManager object.
  internal class TestDataTableManager
  {
    // Initializes an object instance.
    public TestDataTableManager()
    {
      // Set program configuration Singleton.
      var values = ValuesDataUtility.Instance;
      values.SetConfigFile("LJCDataUtility.exe.config");
      var errors = values.Errors;
      if (NetString.HasValue(errors))
      {
        throw new System.Exception(errors);
      }

      DataTableManager = values.Managers.DataTableManager;
      TestCommon = new TestCommon("DataTableManager");
    }

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

      // DataTable Info Methods
      Columns();
      PropertyNames();

      // Custom Data Methods
      RetrieveWithID();
      RetrieveWithUnique();

      // Get Key Methods
      IDKey();
      ParentKey();
      UniqueKey();

      // Joins
      GetJoins();
    }

    // Remove the test record.
    private void CleanUp()
    {
      var methodName = "CleanUp()";

      // DataUtility module.
      long dataModuleID = 1;
      long dataModuleSiteID = 1;

      string name = "TestTableName";

      var uniqueColumns = new DbColumns()
      {
        { "DataModuleID", dataModuleID },
        { "DataModuleSiteID", dataModuleSiteID },
        { "Name", (object)name },
      };
      DataTableManager.Delete(uniqueColumns);

      var utilTable = DataTableManager.Retrieve(uniqueColumns);
      var result = "HasObject";
      if (null == utilTable)
      {
        result = null;
      }
      var compare = "No Result";
      TestCommon.Write($"{methodName}1", result, compare);
    }

    #region Data Methods

    // Adds a record to the database.
    private void Add()
    {
      string methodName = "Add()";

      long dataSiteID = 1;

      // DataUtility module.
      long dataModuleID = 1;
      long dataModuleSiteID = 1;
      string name = "TestTableName";

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

      // Test Method
      DataTableManager.Add(dataUtilTable);

      var uniqueColumns = new DbColumns()
      {
        { "DataModuleID", dataModuleID },
        { "DataModuleSiteID", dataModuleSiteID },
        { "Name", (object)name },
      };
      var utilTable = DataTableManager.Retrieve(uniqueColumns);
      var result = utilTable.Name;
      var compare = "TestTableName";
      TestCommon.Write($"{methodName}", result, compare);

      DataTableManager.Delete(uniqueColumns);
    }

    // Deletes the records with the specified key values.
    private void Delete()
    {
      string methodName = "Delete()";

      // DataUtility module.
      long dataModuleID = 1;
      long dataModuleSiteID = 1;

      string name = "TestTableName";

      var uniqueColumns = new DbColumns()
      {
        { "DataModuleID", dataModuleID },
        { "DataModuleSiteID", dataModuleSiteID },
        { "Name", (object)name },
      };
      var utilTable = DataTableManager.Retrieve(uniqueColumns);
      if (utilTable != null)
      {
        var result = utilTable.Name;
        var compare = "TestTableName";
        TestCommon.Write($"{methodName}1", result, compare);

        // Test Method
        DataTableManager.Delete(uniqueColumns);

        utilTable = DataTableManager.Retrieve(uniqueColumns);
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
      long dataSiteID = 1;

      // DataUtility module.
      long dataModuleID = 1;
      long dataModuleSiteID = 1;
      string name = "TestTableName";

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
      DataTableManager.Add(dataUtilTable);

      // Test Method
      var dataTables = DataTableManager.Load();

      var result = "";
      if (dataTables.Count > 0)
      {
        result = "HasItems";
      }
      var compare = "HasItems";
      TestCommon.Write($"{methodName}", result, compare);

      var uniqueColumns = new DbColumns()
      {
        { "DataModuleID", dataModuleID },
        { "DataModuleSiteID", dataModuleSiteID },
        { "Name", (object)name },
      };
      DataTableManager.Delete(uniqueColumns);
    }

    // Retrieves a record from the database.
    private void Retrieve()
    {
      var methodName = "Retrieve()";

      long dataSiteID = 1;

      // DataUtility module.
      long dataModuleID = 1;
      long dataModuleSiteID = 1;
      string name = "TestTableName";

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
      DataTableManager.Add(dataUtilTable);

      var uniqueColumns = new DbColumns()
      {
        { "DataModuleID", dataModuleID },
        { "DataModuleSiteID", dataModuleSiteID },
        { "Name", (object)name },
      };

      // Test Method
      var utilTable = DataTableManager.Retrieve(uniqueColumns);

      var result = utilTable.Name;
      var compare = "TestTableName";
      TestCommon.Write($"{methodName}", result, compare);

      DataTableManager.Delete(uniqueColumns);
    }

    // Updates the record.
    private void Update()
    {
      var methodName = "Update()";

      long dataSiteID = 1;

      // DataUtility module.
      long dataModuleID = 1;
      long dataModuleSiteID = 1;
      string name = "TestTableName";

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

      // Test Method
      DataTableManager.Add(dataUtilTable);

      var uniqueColumns = new DbColumns()
      {
        { "DataModuleID", dataModuleID },
        { "DataModuleSiteID", dataModuleSiteID },
        { "Name", (object)name },
      };
      var utilTable = DataTableManager.Retrieve(uniqueColumns);
      var result = utilTable.Name;
      var compare = "TestTableName";
      TestCommon.Write($"{methodName}", result, compare);

      DataTableManager.Delete(uniqueColumns);
    }
    #endregion

    #region Custom Data Methods

    // Retrieves a record with the supplied ID values.
    private void RetrieveWithID()
    {
      var methodName = "RetrieveWithID()";

      var result = "";
      var compare = "Not Implemented";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Retrieves a record with the supplied unique values.
    private void RetrieveWithUnique()
    {
      var methodName = "RetrieveWithUnique()";

      var result = "";
      var compare = "Not Implemented";
      TestCommon.Write($"{methodName}", result, compare);
    }
    #endregion

    #region GetKey Methods

    // Gets the ID key columns.
    private void IDKey()
    {
      var methodName = "IDKey()";

      var result = "";
      var compare = "Not Implemented";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Gets the Parent ID key columns.
    private void ParentKey()
    {
      var methodName = "ParentKey()";

      var result = "";
      var compare = "Not Implemented";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Gets the Unique ID key columns.
    private void UniqueKey()
    {
      var methodName = "UniqueKey()";

      var result = "";
      var compare = "Not Implemented";
      TestCommon.Write($"{methodName}", result, compare);
    }
    #endregion

    #region Info Methods

    // Creates a collection of columns that match the supplied list.
    private void Columns()
    {
      var methodName = "Columns()";

      var result = "";
      var compare = "Not Implemented";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Creates a list of BaseDefinition property names.
    private void PropertyNames()
    {
      var methodName = "PropertyNames()";

      var result = "";
      var compare = "Not Implemented";
      TestCommon.Write($"{methodName}", result, compare);
    }
    #endregion

    #region Joins

    // Creates and returns the Load Joins object.
    private void GetJoins()
    {
      var methodName = "GetJoins()";

      var result = "";
      var compare = "Not Implemented";
      TestCommon.Write($"{methodName}", result, compare);
    }
    #endregion

    #region Properties

    // Gets or sets the DataTableManager reference.
    private DataTableManager DataTableManager { get; set; }
    #endregion

    private TestCommon TestCommon { get; set; }
  }
}
