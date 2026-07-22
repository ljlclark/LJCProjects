// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// TestDataRows.cs
using LJCNetCommon;
using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.Remoting.Proxies;

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
      var rowColumns = new LJCDataColumns();
      var dataColumn = new LJCDataColumn("FirstName", "First Name");
      rowColumns.Add(dataColumn);
      dataColumn = new LJCDataColumn("MiddleName", "Middle Name");
      rowColumns.Add(dataColumn);
      dataColumn = new LJCDataColumn("LastName", "First Last Name");
      rowColumns.Add(dataColumn);
      dataRows.Add(rowColumns);

      // Create a row item and add to the list.
      rowColumns = new LJCDataColumns();
      dataColumn = new LJCDataColumn("FirstName", "First Name");
      rowColumns.Add(dataColumn);
      dataColumn = new LJCDataColumn("MiddleName", "Middle Name");
      rowColumns.Add(dataColumn);
      dataColumn = new LJCDataColumn("LastName", "Second Last Name");
      rowColumns.Add(dataColumn);
      dataRows.Add(rowColumns);

      // Create a row item and add to the list.
      rowColumns = new LJCDataColumns();
      dataColumn = new LJCDataColumn("FirstName", "First Name");
      rowColumns.Add(dataColumn);
      dataColumn = new LJCDataColumn("MiddleName", "Middle Name");
      rowColumns.Add(dataColumn);
      dataColumn = new LJCDataColumn("LastName", "Final Last Name");
      rowColumns.Add(dataColumn);
      dataRows.Add(rowColumns);

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
        rowColumns = dataRows[index];
        keyColumns = LJCDataColumns.LJCPropertyNameKeys("LastName");
        dataColumn = rowColumns.LJCGetWith(keyColumns);
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
      var rowColumns = new LJCDataColumns();
      var dataColumn = new LJCDataColumn("FirstName", "First Name");
      rowColumns.Add(dataColumn);
      dataColumn = new LJCDataColumn("MiddleName", "Middle Name");
      rowColumns.Add(dataColumn);
      dataColumn = new LJCDataColumn("LastName", "First Last Name");
      rowColumns.Add(dataColumn);
      dataRows.Add(rowColumns);

      // Create a row item and add to the list.
      rowColumns = new LJCDataColumns();
      dataColumn = new LJCDataColumn("FirstName", "First Name");
      rowColumns.Add(dataColumn);
      dataColumn = new LJCDataColumn("MiddleName", "Middle Name");
      rowColumns.Add(dataColumn);
      dataColumn = new LJCDataColumn("LastName", "Second Last Name");
      rowColumns.Add(dataColumn);
      dataRows.Add(rowColumns);

      // Create a row item and add to the list.
      rowColumns = new LJCDataColumns();
      dataColumn = new LJCDataColumn("FirstName", "First Name");
      rowColumns.Add(dataColumn);
      dataColumn = new LJCDataColumn("MiddleName", "Middle Name");
      rowColumns.Add(dataColumn);
      dataColumn = new LJCDataColumn("LastName", "Final Last Name");
      rowColumns.Add(dataColumn);
      dataRows.Add(rowColumns);

      var result = "";
      var found = false;
      var searchPropertyName = "LastName";
      foreach (var rowColumnsItem in dataRows)
      {
        // Gets the data column with the matching property name.
        //var keyColumns = LJCDataColumns.LJCPropertyNameKeys(searchPropertyName);
        var rowsKeyColumns = new LJCDataColumns();
        rowsKeyColumns.LJCAddValue("PropertyName", searchPropertyName);
        dataColumn = rowColumnsItem.LJCGetWith(rowsKeyColumns);

        // Create key columns for matching column value.
        var searchColumnValue = "Final Last Name";
        var columnsKeyColumns = new LJCDataColumns();
        columnsKeyColumns.LJCAddValue(searchPropertyName, searchColumnValue);

        foreach (var keyColumn in columnsKeyColumns)
        {
          // Test Method
          var compareValue = dataRows.LJCCompareColumn(searchColumnValue
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
      var rowColumns = new LJCDataColumns();
      var dataColumn = new LJCDataColumn("FirstName", "First Name");
      rowColumns.Add(dataColumn);
      dataColumn = new LJCDataColumn("MiddleName", "Middle Name");
      rowColumns.Add(dataColumn);
      dataColumn = new LJCDataColumn("LastName", "First Last Name");
      rowColumns.Add(dataColumn);
      dataRows.Add(rowColumns);

      // Create a row item and add to the list.
      rowColumns = new LJCDataColumns();
      dataColumn = new LJCDataColumn("FirstName", "First Name");
      rowColumns.Add(dataColumn);
      dataColumn = new LJCDataColumn("MiddleName", "Middle Name");
      rowColumns.Add(dataColumn);
      dataColumn = new LJCDataColumn("LastName", "Second Last Name");
      rowColumns.Add(dataColumn);
      dataRows.Add(rowColumns);

      // Create a row item and add to the list.
      rowColumns = new LJCDataColumns();
      dataColumn = new LJCDataColumn("FirstName", "First Name");
      rowColumns.Add(dataColumn);
      dataColumn = new LJCDataColumn("MiddleName", "Middle Name");
      rowColumns.Add(dataColumn);
      dataColumn = new LJCDataColumn("LastName", "Final Last Name");
      rowColumns.Add(dataColumn);
      dataRows.Add(rowColumns);

      // Add the unique compare values.
      var keyColumns = new LJCDataColumns()
      {
        // KeyColumnName, ColumnValue
        { "LastName", (object)"Final Last Name" },
      };
      dataRows.LJCKeyColumns = keyColumns;

      // Test Method
      rowColumns = dataRows.LJCGetRow();

      // Get the found data row and search column.
      string value = "";
      if (rowColumns != null)
      {
        keyColumns = LJCDataColumns.LJCPropertyNameKeys("LastName");
        dataColumn = rowColumns.LJCGetWith(keyColumns);
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
      var rowColumns = new LJCDataColumns();
      var dataColumn = new LJCDataColumn("FirstName", "First Name");
      rowColumns.Add(dataColumn);
      dataColumn = new LJCDataColumn("MiddleName", "Middle Name");
      rowColumns.Add(dataColumn);
      dataColumn = new LJCDataColumn("LastName", "First Last Name");
      rowColumns.Add(dataColumn);
      dataRows.Add(rowColumns);

      // Create a row item and add to the list.
      rowColumns = new LJCDataColumns();
      dataColumn = new LJCDataColumn("FirstName", "First Name");
      rowColumns.Add(dataColumn);
      dataColumn = new LJCDataColumn("MiddleName", "Middle Name");
      rowColumns.Add(dataColumn);
      dataColumn = new LJCDataColumn("LastName", "Second Last Name");
      rowColumns.Add(dataColumn);
      dataRows.Add(rowColumns);

      // Create a row item and add to the list.
      rowColumns = new LJCDataColumns();
      dataColumn = new LJCDataColumn("FirstName", "First Name");
      rowColumns.Add(dataColumn);
      dataColumn = new LJCDataColumn("MiddleName", "Middle Name");
      rowColumns.Add(dataColumn);
      dataColumn = new LJCDataColumn("LastName", "Final Last Name");
      rowColumns.Add(dataColumn);
      dataRows.Add(rowColumns);

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
      rowColumns = dataRows[0];
      keyColumns = LJCDataColumns.LJCPropertyNameKeys("LastName");
      dataColumn = rowColumns.LJCGetWith(keyColumns);

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
