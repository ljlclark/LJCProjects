// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// TestDataRows.cs
using LJCNetCommon;
using System;
using LJC = LJCNetCommon.NetCommon;

namespace TestData
{
  // Provides the LJCDataRows test methods.
  internal class TestDataRows
  {
    #region Constructor Methods

    // Initializes an object instance.
    public TestDataRows()
    {
      TestCommon = new TestCommon("TestDataRows");
      Console.WriteLine();
      Console.WriteLine("**********************");
      Console.Write("*** LJCDataRows ***");
      Run();
    }

    // Runs the test methods.
    private void Run()
    {
      #region Constructor Methods

      Constructor();
      CopyConstructor();
      #endregion

      #region Collection Methods

      // Gets property names list from data columns.
      LJCPropertyNames();
      #endregion

      #region Collection Data Methods

      // Returns the row that matches the key columns.
      LJCGetUnique();
      LJCGetUniqueMultiKey();

      // Sorts on the supplied property names.
      LJCSort();
      #endregion

      #region Custom Data Methods

      // Dynamic binary search with key columns.
      LJCBinarySearch();

      // Compares column value to key column value.
      LJCCompareColumn();
      #endregion

      #region Properties

      // Gets or sets the key columns.
      LJCKeyColumns();
      #endregion
    }
    #endregion

    // Creates sample data rows for testing.
    private LJCDataRows CreateTestDataRows()
    {
      // Create a rows collection of data columns collections.
      var retDataRows = new LJCDataRows();

      // Create a row of data columns and add to the data rows.
      var rowColumns = new LJCDataColumns();
      var dataColumn = new LJCDataColumn("FirstName", "Second First");
      rowColumns.Add(dataColumn);
      dataColumn = new LJCDataColumn("MiddleName", "Second Middle");
      rowColumns.Add(dataColumn);
      dataColumn = new LJCDataColumn("LastName", "Second Last");
      rowColumns.Add(dataColumn);
      retDataRows.Add(rowColumns);

      // Create a row of data columns and add to the data rows.
      rowColumns = new LJCDataColumns();
      dataColumn = new LJCDataColumn("FirstName", "First First");
      rowColumns.Add(dataColumn);
      dataColumn = new LJCDataColumn("MiddleName", "First Middle");
      rowColumns.Add(dataColumn);
      dataColumn = new LJCDataColumn("LastName", "First Last");
      rowColumns.Add(dataColumn);
      retDataRows.Add(rowColumns);

      // Create a row of data columns and add to the data rows.
      rowColumns = new LJCDataColumns();
      dataColumn = new LJCDataColumn("FirstName", "Third First");
      rowColumns.Add(dataColumn);
      dataColumn = new LJCDataColumn("MiddleName", "Third Middle");
      rowColumns.Add(dataColumn);
      dataColumn = new LJCDataColumn("LastName", "Third Last");
      rowColumns.Add(dataColumn);
      retDataRows.Add(rowColumns);

      return retDataRows;
    }

    #region Constructor Test Methods

    // Initializes an object instance.
    private void Constructor()
    {
      var methodName = "Constructor()";

      // Test Method
      var dataRows = CreateTestDataRows();

      var dataRow = dataRows[0];
      var dataColumn = dataRow[0];
      var result = dataColumn.PropertyName;
      result += $", {dataColumn.ColumnName}";
      var compare = "FirstName, FirstName";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Initializes an object from the supplied item.
    private void CopyConstructor()
    {
      var methodName = "CopyConstructor()";

      // See: Constructor()
      var dataRows = CreateTestDataRows();

      // Test Method
      var newDataRows = new LJCDataRows(dataRows);

      var dataRow = newDataRows[0];
      var dataColumn = dataRow[0];
      var result = $"{dataColumn.Value}";
      var compare = "Second First";
      TestCommon.Write($"{methodName}", result, compare);
    }
    #endregion

    #region Collection Methods

    // Gets property names list from data columns.
    private void LJCPropertyNames()
    {
      var methodName = "LJCPropertyNames()";

      // See: Constructor()
      var dataRows = CreateTestDataRows();

      var dataColumns = dataRows[0];
      var propertyNamesList = dataRows.LJCPropertyNames(dataColumns);
      var result = propertyNamesList[0];
      var compare = "FirstName";
      TestCommon.Write($"{methodName}", result, compare);
    }
    #endregion

    #region Collection Data Methods

    // Returns the row that matches the key columns.
    private void LJCGetUnique()
    {
      var methodName = "LJCGetUnique()";

      // See: Constructor()
      var dataRows = CreateTestDataRows();

      // Add the unique compare values.
      // The row is identified by its column property name values and column
      // values.
      // Get where DataColumn.PropertyName = "LastName", value = "Third Last"
      var keys = LJC.Keys("LastName", "Third Last");
      dataRows.LJCKeys = keys;

      // PropertyNames: FirstName, MiddleName, LastName
      // Before Row Sort on LastName
      // 0 - "Second First", "Second Middle", LastName: "Second Last"
      // 1 - "First First", "First Middle", LastName: "First Last"
      // 2 - "Third First", "Third Middle", LastName: "Third Last"

      // Test Method
      var rowColumns = dataRows.LJCGetUnique();

      // After Row Sort on LastName
      // 0 - "First First", "First Middle", LastName: "First Last"
      // 1 - "Second First", "Second Middle", LastName: "Second Last"
      // 2 - "Third First", "Third Middle", LastName: "Third Last"

      // Retrieves:
      // 2 - "Third First", "Third Middle", LastName: "Third Last"

      // Get the found data row and search column.
      string value = "";
      if (rowColumns != null)
      {
        // Add the unique compare values.
        // The column is identified by its property names and values.
        // Get where DataColumn property = "PropertyName", value = "LastName".
        keys = LJC.Keys("PropertyName", "LastName");
        var dataColumn = rowColumns.LJCGetUnique(keys);
        value = $"{dataColumn.Value}";
      }

      var result = value;
      var compare = "Third Last";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Returns the row that matches multiple key columns.
    private void LJCGetUniqueMultiKey()
    {
      var methodName = "LJCGetUniqueMultiKey()";

      // See: Constructor()
      var dataRows = CreateTestDataRows();

      // Create a row of data columns and add to the data rows.
      var rowColumns = new LJCDataColumns();
      var dataColumn = new LJCDataColumn("FirstName", "Fourth First");
      rowColumns.Add(dataColumn);
      dataColumn = new LJCDataColumn("MiddleName", "Fourth Middle");
      rowColumns.Add(dataColumn);
      dataColumn = new LJCDataColumn("LastName", "Third Last");
      rowColumns.Add(dataColumn);
      dataRows.Add(rowColumns);

      // Add the unique compare values.
      // The row is identified by its column property name values and column
      // values.
      // Get where DataColumn.PropertyName = "LastName", value = "Third Last"
      // and DataColumn.PropertyName = "FirstName", value = "Fourth First".
      var keys = LJC.Keys("LastName", "Third Last");
      keys.Add("FirstName", "Fourth First");
      dataRows.LJCKeys = keys;

      // PropertyNames: FirstName, MiddleName, LastName
      // Before Row Sort on LastName, FirstName:
      // 0 - "Second First", "Second Middle", "Second Last"
      // 1 - "First First", "First Middle", "First Last"
      // 2 - "Third First", "Third Middle", "Third Last"
      // 3 - "Fourth First", "Fourth Middle", "Third Last"

      // Test Method
      rowColumns = dataRows.LJCGetUnique();

      // After Row Sort on LastName, FirstName:
      // 0 - "First First", "First Middle", "First Last"
      // 1 - "Second First", "Second Middle", "Second Last"
      // 2 - "Fourth First", "Fourth Middle", "Third Last"
      // 3 - "Third First", "Third Middle", "Third Last"

      // Retrieves:
      // 2 - "Fourth First", "Fourth Middle", "Third Last"

      // Get the found data row and search column.
      string value = "";
      if (rowColumns != null)
      {
        // Add the unique compare values.
        // The column is identified by its property names and values.
        // Get where DataColumn property = "PropertyName", value = "FirstName".
        keys = LJC.Keys(LJCDataColumn.ColumnPropertyName, "FirstName");
        dataColumn = rowColumns.LJCGetUnique(keys);
        value = $"{dataColumn.Value}";
      }

      var result = value;
      var compare = "Fourth First";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Sorts on the supplied property names.
    private void LJCSort()
    {
      var methodName = "LJCSort()";

      // See: Constructor()
      var dataRows = CreateTestDataRows();

      // Add the unique compare values.
      // The sort is identified by its column property name values and column
      // values.
      // Get where DataColumn.PropertyName = "LastName", value = "Third Last"
      var keys = LJC.Keys("LastName", "Third Last");
      dataRows.LJCKeys = keys;

      // Test Method
      dataRows.LJCSort();

      // Get the first data row.
      var rowColumns = dataRows[0];

      // The column is identified by its property names and values.
      // Get where DataColumn property = "PropertyName", value = "LastName".
      keys = LJC.Keys(LJCDataColumn.ColumnPropertyName, "LastName");
      var dataColumn = rowColumns.LJCGetUnique(keys);

      var result = $"{dataColumn.Value}";
      var compare = "First Last";
      TestCommon.Write($"{methodName}", result, compare);
    }
    #endregion

    #region Custom Data Methods

    // Dynamic binary search with key columns.
    private void LJCBinarySearch()
    {
      var methodName = "LJCBinarySearch()";

      // See: Constructor()
      var dataRows = CreateTestDataRows();

      // The search is identified by its column property name values and column
      // values.
      // Get where DataColumn.PropertyName = "LastName", value = "Third Last"
      var keys = LJC.Keys("LastName", "Third Last");
      dataRows.LJCKeys = keys;

      // Test Method
      // Do a binary search on the list items with the key columns and values.
      var index = dataRows.LJCBinarySearch();

      // Get the found data row and search column.
      string value = "";
      if (index != -1)
      {
        var rowColumns = dataRows[index];

        // The column is identified by its property names and values.
        // Get where DataColumn property = "PropertyName", value = "LastName".
        keys = LJC.Keys(LJCDataColumn.ColumnPropertyName, "LastName");
        var dataColumn = rowColumns.LJCGetUnique(keys);
        value = $"{dataColumn.Value}";
      }

      var result = value;
      var compare = "Third Last";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Compares column value to key column value.
    private void LJCCompareColumn()
    {
      var methodName = "LJCCompareColumn()";

      // See: Constructor()
      var dataRows = CreateTestDataRows();

      var result = "";
      var found = false;

      // LJCCompareColumn() is called in LJCDataRows.BinarySearch().
      // This code loops through the row columns to demonstrate
      // LJCCompareColumn().
      foreach (var rowColumnsItem in dataRows)
      {
        // The column is identified by its property names and values.
        // Get where DataColumn.PropertyName = "PropertyName", value = "LastName"
        var rowKeys = LJC.Keys(LJCDataColumn.ColumnPropertyName, "LastName");
        var dataColumn = rowColumnsItem.LJCGetUnique(rowKeys);
        var dataColumnValue = $"{dataColumn.Value}";

        // The search is identified by its column property name values and column
        // values.
        var columnKeys = LJC.Keys("LastName", "Third Last");

        foreach (var keyColumn in columnKeys)
        {
          // Test Method
          var compareValue = dataRows.LJCCompareColumn(dataColumnValue
            , keyColumn);

          if (NetString.CompareEqual == compareValue)
          {
            found = true;
            result = $"{dataColumn.Value}";
            break;
          }
        }
        if (found)
        {
          break;
        }
      }
      var compare = "Third Last";
      TestCommon.Write($"{methodName}", result, compare);
    }
    #endregion

    #region Properties

    // Gets or sets the key columns.
    private void LJCKeyColumns()
    {
      var methodName = "LJCKeyColumns()";

      // Get where DataColumn property = "PropertyName", value = "ID".
      var keys = LJC.Keys(LJCDataColumn.ColumnPropertyName, "ID");
      var key = keys[0];
      var result = key.ColumnName;
      result += $", {key.Value}";
      var compare = "PropertyName, ID";
      TestCommon.Write($"{methodName}", result, compare);
    }
    #endregion

    #region Class Data

    // The test common object.
    private static TestCommon TestCommon { get; set; }
    #endregion
  }
}
