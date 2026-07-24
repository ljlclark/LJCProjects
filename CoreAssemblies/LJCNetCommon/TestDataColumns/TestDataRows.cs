// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// TestDataRows.cs
using LJCNetCommon;
using System;

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

    private LJCDataRows CreateTestDataRows()
    {
      // Create a rows collection of data columns collections.
      var retDataRows = new LJCDataRows();

      // Create a row of data columns and add to the data rows.
      var rowColumns = new LJCDataColumns();
      var dataColumn = new LJCDataColumn("FirstName", "First First");
      rowColumns.Add(dataColumn);
      dataColumn = new LJCDataColumn("MiddleName", "First Middle");
      rowColumns.Add(dataColumn);
      dataColumn = new LJCDataColumn("LastName", "First Last");
      rowColumns.Add(dataColumn);
      retDataRows.Add(rowColumns);

      // Create a row of data columns and add to the data rows.
      rowColumns = new LJCDataColumns();
      dataColumn = new LJCDataColumn("FirstName", "Second First");
      rowColumns.Add(dataColumn);
      dataColumn = new LJCDataColumn("MiddleName", "Second Middle");
      rowColumns.Add(dataColumn);
      dataColumn = new LJCDataColumn("LastName", "Second Last");
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

      // Create a row of data columns and add to the data rows.
      rowColumns = new LJCDataColumns();
      dataColumn = new LJCDataColumn("FirstName", "Fourth First");
      rowColumns.Add(dataColumn);
      dataColumn = new LJCDataColumn("MiddleName", "Fourth Middle");
      rowColumns.Add(dataColumn);
      dataColumn = new LJCDataColumn("LastName", "Third Last");
      rowColumns.Add(dataColumn);
      retDataRows.Add(rowColumns);

      return retDataRows;
    }

    #region Constructor Methods

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
      var compare = "First First";
      TestCommon.Write($"{methodName}", result, compare);
    }
    #endregion

    #region Collection Methods

    // Gets property names list from data columns.
    private void LJCPropertyNames()
    {
      var methodName = "LJCPropertyNames()";

      var result = "";
      var compare = "Not Implemented";
      TestCommon.Write($"{methodName}", result, compare);
    }
    #endregion

    #region Collection Data Methods

    // Returns the row that matches the key columns.
    private void LJCGetUnique()
    {
      var methodName = "LJCBinarySearch()";

      // See: Constructor()
      var dataRows = CreateTestDataRows();

      // Add the unique compare values.
      // The row is identified by its column property name values and column
      // values.
      var rowColumnPropertyNameValue = "LastName";
      var rowColumnValue = "Third Last";
      var keyColumns = new LJCDataColumns()
      {
        { rowColumnPropertyNameValue, rowColumnValue },
      };
      dataRows.LJCKeyColumns = keyColumns;

      // Test Method
      var rowColumns = dataRows.LJCGetUnique();

      // Get the found data row and search column.
      string value = "";
      if (rowColumns != null)
      {
        // Add the unique compare values.
        // The column is identified by its property names and values.
        var dataColumnPropertyName = "PropertyName";
        var dataColumnValue = "LastName";
        keyColumns = new LJCDataColumns()
        {
          { dataColumnPropertyName, dataColumnValue },
        };
        var dataColumn = rowColumns.LJCGetUnique(keyColumns);
        value = $"{dataColumn.Value}";
      }

      var result = value;
      var compare = "Third Last";
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
      var keyColumns = new LJCDataColumns()
      {
        // KeyColumnName, ColumnValue
        { "LastName", "Third Last" },
      };
      dataRows.LJCKeyColumns = keyColumns;

      // Test Method
      dataRows.LJCSort();

      // Get the first data row.
      var rowColumns = dataRows[0];

      // Add the unique compare values.
      // The column is identified by its property names and values.
      keyColumns = new LJCDataColumns()
      {
        { "PropertyName", "LastName" },
      };
      var dataColumn = rowColumns.LJCGetUnique(keyColumns);

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

      // Add the unique compare values.
      // The search is identified by its column property name values and column
      // values.
      var keyColumns = new LJCDataColumns()
      {
        // KeyColumnName, ColumnValue
        { "LastName", "Third Last" },
      };
      dataRows.LJCKeyColumns = keyColumns;

      // Test Method
      // Do a binary search on the list items with the key columns and values.
      var index = dataRows.LJCBinarySearch();

      // Get the found data row and search column.
      string value = "";
      if (index != -1)
      {
        var rowColumns = dataRows[index];

        // Add the unique compare values.
        // The column is identified by its property names and values.
        keyColumns = new LJCDataColumns()
        {
          { "PropertyName", "LastName" },
        };
        var dataColumn = rowColumns.LJCGetUnique(keyColumns);
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
      var searchPropertyName = "LastName";
      foreach (var rowColumnsItem in dataRows)
      {
        // Add the unique compare values.
        // The column is identified by its property names and values.
        var rowsKeyColumns = new LJCDataColumns()
        {
          { "PropertyName", searchPropertyName },
        };
        var dataColumn = rowColumnsItem.LJCGetUnique(rowsKeyColumns);
        var dataColumnValue = $"{dataColumn.Value}";

        // Add the unique compare values.
        // The search is identified by its column property name values and column
        // values.
        var searchColumnValue = "Third Last";
        var columnsKeyColumns = new LJCDataColumns()
        {
          { searchPropertyName, searchColumnValue },
        };

        foreach (var keyColumn in columnsKeyColumns)
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

      var result = "";
      var compare = "Not Implemented";
      TestCommon.Write($"{methodName}", result, compare);
    }
    #endregion

    #region Class Data

    // The test common object.
    private static TestCommon TestCommon { get; set; }
    #endregion
  }
}
