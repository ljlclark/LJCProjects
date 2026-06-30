// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// TestDataTable2.cs
using LJCDataUtilityDAL;
using LJCNetCommon;
using static LJCDataUtilityDAL.DataTable2;

namespace TestDataUtilityDAL
{
  // Tests the DataUtilTable object.
  internal class TestDataTable2
  {
    // Initializes an object instance.
    public TestDataTable2()
    {
      TestCommon = new TestCommon("TestDataUtilTableNew");
    }

    // Run the tests.
    public void Run()
    {
      // Data Object Methods
      CopyConstructor();
    }

    #region Data Object Methods

    // Initializes an object instance with the supplied object.
    public void CopyConstructor()
    {
      var methodName = "CopyConstructor()";

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

      var newDataTable = new DataTable2(dataTable);

      var result = newDataTable.LJCGetString(ColumnID);
      var compare = "1";
      TestCommon.Write($"{methodName}1", result, compare);

      result = newDataTable.LJCGetString(ColumnDataSiteID);
      compare = "2";
      TestCommon.Write($"{methodName}1", result, compare);

      result = newDataTable.LJCGetString(ColumnDataModuleID);
      compare = "3";
      TestCommon.Write($"{methodName}2", result, compare);

      result = newDataTable.LJCGetString(ColumnDataModuleSiteID);
      compare = "4";
      TestCommon.Write($"{methodName}1", result, compare);

      result = newDataTable.LJCGetString(ColumnName);
      compare = "Name";
      TestCommon.Write($"{methodName}3", result, compare);

      result = newDataTable.LJCGetString(ColumnDescription);
      compare = "Description";
      TestCommon.Write($"{methodName}4", result, compare);

      result = newDataTable.LJCGetString(ColumnSequence);
      compare = "1";
      TestCommon.Write($"{methodName}5", result, compare);

      result = newDataTable.LJCGetString(ColumnSchemaName);
      compare = "SchemaName";
      TestCommon.Write($"{methodName}6", result, compare);

      result = newDataTable.LJCGetString(ColumnNewName);
      compare = "NewName";
      TestCommon.Write($"{methodName}7", result, compare);
    }
    #endregion

    #region Properties

    // Gets or sets the TestCommon object.
    private TestCommon TestCommon { get; set; }
    #endregion
  }
}
