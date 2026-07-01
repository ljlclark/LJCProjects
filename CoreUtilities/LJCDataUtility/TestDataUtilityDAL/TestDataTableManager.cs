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

      // Additional Load and Retrieve Methods
      RetrieveWithID();
      RetrieveWithUnique();

      // Get Key Methods
      IDKey();
      ParentKey();
      UniqueKey();

      // Joins
      GetJoins();
    }

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
        result = null;
        compare = "No Result";
        TestCommon.Write($"{methodName}2", result, compare);
      }
    }

    // Retrieves a collection of data records.
    private void Load()
    {
    }

    // Retrieves a record from the database.
    private void Retrieve()
    {
      var methodName = "Retrieve()";

      var id = 1;
      var dataSiteID = 1;
      var keyColumns = DataTableManager.IDKey(id, dataSiteID);
      var dataUtilTable = DataTableManager.Retrieve(keyColumns);

      var result = dataUtilTable.Name;
      var compare = "DataModule";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Updates the record.
    private void Update()
    {
    }
    #endregion

    #region Info Methods

    // Creates a collection of columns that match the supplied list.
    private void Columns()
    {
    }

    // Creates a list of BaseDefinition property names.
    private void PropertyNames()
    {
    }
    #endregion

    #region Additional Load and Retrieve Methods

    // Retrieves a record with the supplied ID values.
    private void RetrieveWithID()
    {
    }

    // Retrieves a record with the supplied unique values.
    private void RetrieveWithUnique()
    {
    }
    #endregion

    #region GetKey Methods

    // Gets the ID key columns.
    private void IDKey()
    {
    }

    // Gets the Parent ID key columns.
    private void ParentKey()
    {
    }

    // Gets the Unique ID key columns.
    private void UniqueKey()
    {
    }
    #endregion

    #region Joins

    // Creates and returns the Load Joins object.
    private void GetJoins()
    {
    }
    #endregion

    #region Properties

    // Gets or sets the DataTableManager reference.
    private DataTableManager DataTableManager { get; set; }
    #endregion

    private TestCommon TestCommon { get; set; }
  }
}
