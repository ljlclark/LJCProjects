// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// TestDataRows.cs
using LJCNetCommon;
using System;
using System.Collections.Generic;

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
      #region Custom Data Methods

      // Dynamic binary search with key columns.
      LJCBinarySearch();

      // Compares column value to key column value.
      LJCCompareColumn();

      // Returns the row that matches the key columns.
      LJCGetRow();

      // Sorts on the supplied property names.
      LJCSort();
      #endregion
    }
    #endregion

    #region Custom Data Methods

    // Dynamic binary search with key columns.
    private void LJCBinarySearch()
    {
      var methodName = "LJCBinarySearch()";

      // Create a collection of data rows.
      var dataRows = new LJCDataRows();

      // Create a row item and add to the list.
      var rowDataColumns = new LJCDataColumns();
      var dataColumn = new LJCDataColumn("FirstName", "First Name");
      rowDataColumns.Add(dataColumn);
      dataColumn = new LJCDataColumn("MiddleName", "Middle Name");
      rowDataColumns.Add(dataColumn);
      dataColumn = new LJCDataColumn("LastName", "First Last Name");
      rowDataColumns.Add(dataColumn);
      dataRows.Add(rowDataColumns);

      // Create a row item and add to the list.
      rowDataColumns = new LJCDataColumns();
      dataColumn = new LJCDataColumn("FirstName", "First Name");
      rowDataColumns.Add(dataColumn);
      dataColumn = new LJCDataColumn("MiddleName", "Middle Name");
      rowDataColumns.Add(dataColumn);
      dataColumn = new LJCDataColumn("LastName", "Second Last Name");
      rowDataColumns.Add(dataColumn);
      dataRows.Add(rowDataColumns);

      // Create a row item and add to the list.
      rowDataColumns = new LJCDataColumns();
      dataColumn = new LJCDataColumn("FirstName", "First Name");
      rowDataColumns.Add(dataColumn);
      dataColumn = new LJCDataColumn("MiddleName", "Middle Name");
      rowDataColumns.Add(dataColumn);
      dataColumn = new LJCDataColumn("LastName", "Final Last Name");
      rowDataColumns.Add(dataColumn);
      dataRows.Add(rowDataColumns);

      // Add the unique compare values.
      var keyColumns = new LJCDataColumns()
      {
        // KeyColumnName, ColumnValue
        { "LastName", (object)"Final Last Name" },
      };
      dataRows.LJCKeyColumns = keyColumns;

      // Test Method
      // Do a binary search on the list items with the key columns and values.
      var index = dataRows.LJCBinarySearch();

      // Get the found data row and search column.
      string value = "";
      if (index != -1)
      {
        rowDataColumns = dataRows[index];
        dataColumn = rowDataColumns.LJCGetWithPropertyName("LastName");
        value = $"{dataColumn.Value}";
      }

      var result = value;
      var compare = "Final Last Name";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Compares column value to key column value.
    private void LJCCompareColumn()
    {
      var methodName = "LJCCompareColumn()";

      // Create a collection of data rows.
      var dataRows = new LJCDataRows();

      // Create a row item and add to the list.
      var rowDataColumns = new LJCDataColumns();
      var dataColumn = new LJCDataColumn("FirstName", "First Name");
      rowDataColumns.Add(dataColumn);
      dataColumn = new LJCDataColumn("MiddleName", "Middle Name");
      rowDataColumns.Add(dataColumn);
      dataColumn = new LJCDataColumn("LastName", "First Last Name");
      rowDataColumns.Add(dataColumn);
      dataRows.Add(rowDataColumns);

      // Create a row item and add to the list.
      rowDataColumns = new LJCDataColumns();
      dataColumn = new LJCDataColumn("FirstName", "First Name");
      rowDataColumns.Add(dataColumn);
      dataColumn = new LJCDataColumn("MiddleName", "Middle Name");
      rowDataColumns.Add(dataColumn);
      dataColumn = new LJCDataColumn("LastName", "Second Last Name");
      rowDataColumns.Add(dataColumn);
      dataRows.Add(rowDataColumns);

      // Create a row item and add to the list.
      rowDataColumns = new LJCDataColumns();
      dataColumn = new LJCDataColumn("FirstName", "First Name");
      rowDataColumns.Add(dataColumn);
      dataColumn = new LJCDataColumn("MiddleName", "Middle Name");
      rowDataColumns.Add(dataColumn);
      dataColumn = new LJCDataColumn("LastName", "Final Last Name");
      rowDataColumns.Add(dataColumn);
      dataRows.Add(rowDataColumns);

      // Create the unique compare values.
      var keyColumns = new LJCDataColumns()
      {
        // KeyColumnName, ColumnValue
        { "LastName", (object)"Final Last Name" },
      };

      var result = "";
      var found = false;
      var propertyName = "LastName";
      foreach (var dataColumns in dataRows)
      {
        dataColumn = dataColumns.LJCGetWithPropertyName(propertyName);
        var columnValue = $"{dataColumn.Value}";

        foreach (var keyColumn in keyColumns)
        {
          // Test Method
          var compareValue = dataRows.LJCCompareColumn(columnValue, keyColumn);

          if (NetString.CompareEqual == compareValue)
          {
            found = true;
            result = columnValue;
            break;
          }
        }
        if (found)
        {
          break;
        }
      }
      var compare = "Final Last Name";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Returns the row that matches the key columns.
    private void LJCGetRow()
    {
      var methodName = "LJCBinarySearch()";

      // Create a collection of data rows.
      var dataRows = new LJCDataRows();

      // Create a row item and add to the list.
      var rowDataColumns = new LJCDataColumns();
      var dataColumn = new LJCDataColumn("FirstName", "First Name");
      rowDataColumns.Add(dataColumn);
      dataColumn = new LJCDataColumn("MiddleName", "Middle Name");
      rowDataColumns.Add(dataColumn);
      dataColumn = new LJCDataColumn("LastName", "First Last Name");
      rowDataColumns.Add(dataColumn);
      dataRows.Add(rowDataColumns);

      // Create a row item and add to the list.
      rowDataColumns = new LJCDataColumns();
      dataColumn = new LJCDataColumn("FirstName", "First Name");
      rowDataColumns.Add(dataColumn);
      dataColumn = new LJCDataColumn("MiddleName", "Middle Name");
      rowDataColumns.Add(dataColumn);
      dataColumn = new LJCDataColumn("LastName", "Second Last Name");
      rowDataColumns.Add(dataColumn);
      dataRows.Add(rowDataColumns);

      // Create a row item and add to the list.
      rowDataColumns = new LJCDataColumns();
      dataColumn = new LJCDataColumn("FirstName", "First Name");
      rowDataColumns.Add(dataColumn);
      dataColumn = new LJCDataColumn("MiddleName", "Middle Name");
      rowDataColumns.Add(dataColumn);
      dataColumn = new LJCDataColumn("LastName", "Final Last Name");
      rowDataColumns.Add(dataColumn);
      dataRows.Add(rowDataColumns);

      // Add the unique compare values.
      var keyColumns = new LJCDataColumns()
      {
        // KeyColumnName, ColumnValue
        { "LastName", (object)"Final Last Name" },
      };
      dataRows.LJCKeyColumns = keyColumns;

      // Test Method
      rowDataColumns = dataRows.LJCGetRow();

      // Get the found data row and search column.
      string value = "";
      if (rowDataColumns != null)
      {
        dataColumn = rowDataColumns.LJCGetWithPropertyName("LastName");
        value = $"{dataColumn.Value}";
      }

      var result = value;
      var compare = "Final Last Name";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Sorts on the supplied property names.
    private void LJCSort()
    {
      var methodName = "LJCSort()";

      // Create a collection of data rows.
      var dataRows = new LJCDataRows();

      // Create a row item and add to the list.
      var rowDataColumns = new LJCDataColumns();
      var dataColumn = new LJCDataColumn("FirstName", "First Name");
      rowDataColumns.Add(dataColumn);
      dataColumn = new LJCDataColumn("MiddleName", "Middle Name");
      rowDataColumns.Add(dataColumn);
      dataColumn = new LJCDataColumn("LastName", "First Last Name");
      rowDataColumns.Add(dataColumn);
      dataRows.Add(rowDataColumns);

      // Create a row item and add to the list.
      rowDataColumns = new LJCDataColumns();
      dataColumn = new LJCDataColumn("FirstName", "First Name");
      rowDataColumns.Add(dataColumn);
      dataColumn = new LJCDataColumn("MiddleName", "Middle Name");
      rowDataColumns.Add(dataColumn);
      dataColumn = new LJCDataColumn("LastName", "Second Last Name");
      rowDataColumns.Add(dataColumn);
      dataRows.Add(rowDataColumns);

      // Create a row item and add to the list.
      rowDataColumns = new LJCDataColumns();
      dataColumn = new LJCDataColumn("FirstName", "First Name");
      rowDataColumns.Add(dataColumn);
      dataColumn = new LJCDataColumn("MiddleName", "Middle Name");
      rowDataColumns.Add(dataColumn);
      dataColumn = new LJCDataColumn("LastName", "Final Last Name");
      rowDataColumns.Add(dataColumn);
      dataRows.Add(rowDataColumns);

      // Add the unique compare values.
      var keyColumns = new LJCDataColumns()
      {
        // KeyColumnName, ColumnValue
        { "LastName", (object)"Final Last Name" },
      };
      dataRows.LJCKeyColumns = keyColumns;

      // Test Method
      dataRows.LJCSort();

      // Get the first data row and search column.
      rowDataColumns = dataRows[0];
      dataColumn = rowDataColumns.LJCGetWithPropertyName("LastName");

      var result = $"{dataColumn.Value}";
      var compare = "Final Last Name";
      TestCommon.Write($"{methodName}", result, compare);
    }
    #endregion

    #region Class Data

    // The test common object.
    private static TestCommon TestCommon { get; set; }
    #endregion
  }
}
