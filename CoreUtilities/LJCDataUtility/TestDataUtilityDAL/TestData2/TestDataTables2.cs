// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// TestDataTables2.cs
using LJCDataUtilityDAL;
using LJCNetCommon;
using System.Collections.Generic;
using static LJCDataUtilityDAL.DataTable2;

namespace TestDataUtilityDAL
{
  // Tests the DataUtilTable object.
  internal class TestDataTables2
  {
    // Initializes an object instance.
    public TestDataTables2()
    {
      TestCommon = new TestCommon("TestDataTables2");
      Run();
    }

    // Run the tests.
    public void Run()
    {
      // Static Methods
      LJCGetCollection();

      // Constructor Methods
      CopyConstructor();

      // Collection Methods
      Clone();
      LJCGetWithUnique();

      // Properties
      UniqueIndexer();
    }

    #region Static Methods

    // Get custom collection from List<T>.
    public void LJCGetCollection()
    {
      var methodName = "LJCGetCollection()";

      var dataTables = new List<DataTable2>();
      var dataTable = new DataTable2();
      dataTable.LJCSetValue(ColumnID, 1);
      dataTable.LJCSetValue(ColumnDataSiteID, 2);
      dataTable.LJCSetValue(ColumnDataModuleID, 3);
      dataTable.LJCSetValue(ColumnDataModuleSiteID, 4);
      dataTable.LJCSetValue(ColumnName, "Name");
      dataTable.LJCSetValue(ColumnDescription, "Description");
      dataTable.LJCSetValue(ColumnSequence, 1);
      dataTable.LJCSetValue(ColumnSchemaName, "SchemaName");
      dataTable.LJCSetValue(ColumnNewName, "NewName");
      dataTables.Add(dataTable);

      dataTable = new DataTable2();
      dataTable.LJCSetValue(ColumnID, 2);
      dataTable.LJCSetValue(ColumnDataSiteID, 2);
      dataTable.LJCSetValue(ColumnDataModuleID, 3);
      dataTable.LJCSetValue(ColumnDataModuleSiteID, 4);
      dataTable.LJCSetValue(ColumnName, "First");
      dataTables.Add(dataTable);

      // Test Method
      //var testDataTables = new DataTables2();
      var newDataTables = DataTables2.LJCGetCollection(dataTables);

      var testDataTable = newDataTables[3, 4, "Name"];
      if (null == testDataTable)
      {
        var result = "HasValue";
        var compare = "IsNull";
        TestCommon.Write($"{methodName}1", result, compare);
      }

      if (testDataTable != null)
      {
        var result = testDataTable.LJCGetString("Name");
        var compare = "Name";
        TestCommon.Write($"{methodName}2", result, compare);
      }
    }
    #endregion

    #region Constructor Methods

    // The Copy constructor.
    public void CopyConstructor()
    {
      var methodName = "CopyConstructor()";

      var dataTables = new DataTables2();
      var dataTable = new DataTable2();
      dataTable.LJCSetValue(ColumnID, 1);
      dataTable.LJCSetValue(ColumnDataSiteID, 2);
      dataTable.LJCSetValue(ColumnDataModuleID, 3);
      dataTable.LJCSetValue(ColumnDataModuleSiteID, 4);
      dataTable.LJCSetValue(ColumnName, "Name");
      dataTable.LJCSetValue(ColumnDescription, "Description");
      dataTable.LJCSetValue(ColumnSequence, 1);
      dataTable.LJCSetValue(ColumnSchemaName, "SchemaName");
      dataTable.LJCSetValue(ColumnNewName, "NewName");
      dataTables.Add(dataTable);

      dataTable = new DataTable2();
      dataTable.LJCSetValue(ColumnID, 2);
      dataTable.LJCSetValue(ColumnDataSiteID, 2);
      dataTable.LJCSetValue(ColumnDataModuleID, 3);
      dataTable.LJCSetValue(ColumnDataModuleSiteID, 4);
      dataTable.LJCSetValue(ColumnName, "First");
      dataTables.Add(dataTable);

      // Test Method
      var testDataTables = new DataTables2(dataTables);

      var testDataTable = testDataTables[3, 4, "Name"];
      var result = testDataTable.LJCGetString("Name");
      var compare = "Name";
      TestCommon.Write($"{methodName}", result, compare);
    }
    #endregion

    #region Collection Methods

    // Creates and returns a clone of the object.
    public void Clone()
    {
      var methodName = "Clone()";

      var dataTables = new DataTables2();
      var dataTable = new DataTable2();
      dataTable.LJCSetValue(ColumnID, 1);
      dataTable.LJCSetValue(ColumnDataSiteID, 2);
      dataTable.LJCSetValue(ColumnDataModuleID, 3);
      dataTable.LJCSetValue(ColumnDataModuleSiteID, 4);
      dataTable.LJCSetValue(ColumnName, "Name");
      dataTable.LJCSetValue(ColumnDescription, "Description");
      dataTable.LJCSetValue(ColumnSequence, 1);
      dataTable.LJCSetValue(ColumnSchemaName, "SchemaName");
      dataTable.LJCSetValue(ColumnNewName, "NewName");
      dataTables.Add(dataTable);

      dataTable = new DataTable2();
      dataTable.LJCSetValue(ColumnID, 2);
      dataTable.LJCSetValue(ColumnDataSiteID, 2);
      dataTable.LJCSetValue(ColumnDataModuleID, 3);
      dataTable.LJCSetValue(ColumnDataModuleSiteID, 4);
      dataTable.LJCSetValue(ColumnName, "First");
      dataTables.Add(dataTable);

      // Test Method
      var testDataTables = dataTables.Clone();

      var testDataTable = testDataTables[3, 4, "Name"];
      if (null == testDataTable)
      {
        var result = "HasValue";
        var compare = "IsNull";
        TestCommon.Write($"{methodName}1", result, compare);
      }

      if (testDataTable != null)
      {
        var result = testDataTable.LJCGetString("Name");
        var compare = "Name";
        TestCommon.Write($"{methodName}2", result, compare);
      }
    }

    // Initializes an object instance with the supplied object.
    public void LJCGetWithUnique()
    {
      var methodName = "LJCGetWithName()";

      var dataTables = new DataTables2();
      var dataTable = new DataTable2();
      dataTable.LJCSetValue(ColumnID, 1);
      dataTable.LJCSetValue(ColumnDataSiteID, 2);
      dataTable.LJCSetValue(ColumnDataModuleID, 3);
      dataTable.LJCSetValue(ColumnDataModuleSiteID, 4);
      dataTable.LJCSetValue(ColumnName, "Name");
      dataTable.LJCSetValue(ColumnDescription, "Description");
      dataTable.LJCSetValue(ColumnSequence, 1);
      dataTable.LJCSetValue(ColumnSchemaName, "SchemaName");
      dataTable.LJCSetValue(ColumnNewName, "NewName");
      dataTables.Add(dataTable);

      dataTable = new DataTable2();
      dataTable.LJCSetValue(ColumnID, 2);
      dataTable.LJCSetValue(ColumnDataSiteID, 2);
      dataTable.LJCSetValue(ColumnDataModuleID, 3);
      dataTable.LJCSetValue(ColumnDataModuleSiteID, 4);
      dataTable.LJCSetValue(ColumnName, "First");
      dataTables.Add(dataTable);

      dataTable = new DataTable2();
      dataTable.LJCSetValue(ColumnID, 3);
      dataTable.LJCSetValue(ColumnDataSiteID, 2);
      dataTable.LJCSetValue(ColumnDataModuleID, 3);
      dataTable.LJCSetValue(ColumnDataModuleSiteID, 4);
      dataTable.LJCSetValue(ColumnName, "BeforeName");
      dataTables.Add(dataTable);

      // Test Method
      var keyColumns = new DbColumns()
      {
        { "DataModuleID",  3},
        { "DataModuleSiteID",  4},
        { "Name",  (object)"Name" },
      };
      var testDataTable = dataTables.LJCGetWithUnique(keyColumns);

      if (null == testDataTable)
      {
        var result = "HasValue";
        var compare = "IsNull";
        TestCommon.Write($"{methodName}1", result, compare);
      }

      if (testDataTable != null)
      {
        var result = testDataTable.LJCGetString(ColumnID);
        var compare = "1";
        TestCommon.Write($"{methodName}2", result, compare);

        result = testDataTable.LJCGetString(ColumnDataSiteID);
        compare = "2";
        TestCommon.Write($"{methodName}3", result, compare);

        result = testDataTable.LJCGetString(ColumnDataModuleID);
        compare = "3";
        TestCommon.Write($"{methodName}4", result, compare);

        result = testDataTable.LJCGetString(ColumnDataModuleSiteID);
        compare = "4";
        TestCommon.Write($"{methodName}5", result, compare);

        result = testDataTable.LJCGetString(ColumnName);
        compare = "Name";
        TestCommon.Write($"{methodName}6", result, compare);

        result = testDataTable.LJCGetString(ColumnDescription);
        compare = "Description";
        TestCommon.Write($"{methodName}7", result, compare);

        result = testDataTable.LJCGetString(ColumnSequence);
        compare = "1";
        TestCommon.Write($"{methodName}8", result, compare);

        result = testDataTable.LJCGetString(ColumnSchemaName);
        compare = "SchemaName";
        TestCommon.Write($"{methodName}9", result, compare);

        result = testDataTable.LJCGetString(ColumnNewName);
        compare = "NewName";
        TestCommon.Write($"{methodName}10", result, compare);
      }
    }
    #endregion

    #region Properties

    // The item for the supplied values.
    public void UniqueIndexer()
    {
      var methodName = "UniqueIndexer()";

      var dataTables = new DataTables2();
      var dataTable = new DataTable2();
      dataTable.LJCSetValue(ColumnID, 1);
      dataTable.LJCSetValue(ColumnDataSiteID, 2);
      dataTable.LJCSetValue(ColumnDataModuleID, 3);
      dataTable.LJCSetValue(ColumnDataModuleSiteID, 4);
      dataTable.LJCSetValue(ColumnName, "Name");
      dataTable.LJCSetValue(ColumnDescription, "Description");
      dataTable.LJCSetValue(ColumnSequence, 1);
      dataTable.LJCSetValue(ColumnSchemaName, "SchemaName");
      dataTable.LJCSetValue(ColumnNewName, "NewName");
      dataTables.Add(dataTable);

      dataTable = new DataTable2();
      dataTable.LJCSetValue(ColumnID, 2);
      dataTable.LJCSetValue(ColumnDataSiteID, 2);
      dataTable.LJCSetValue(ColumnDataModuleID, 3);
      dataTable.LJCSetValue(ColumnDataModuleSiteID, 4);
      dataTable.LJCSetValue(ColumnName, "First");
      dataTables.Add(dataTable);

      // Test Method
      var testDataTable = dataTables[3, 4, "Name"];

      if (null == testDataTable)
      {
        var result = "HasValue";
        var compare = "IsNull";
        TestCommon.Write($"{methodName}1", result, compare);
      }

      if (testDataTable != null)
      {
        var result = testDataTable.LJCGetString("Name");
        var compare = "Name";
        TestCommon.Write($"{methodName}", result, compare);
      }
    }
    #endregion

    #region Properties

    // Gets or sets the TestCommon object.
    private TestCommon TestCommon { get; set; }
    #endregion
  }
}
