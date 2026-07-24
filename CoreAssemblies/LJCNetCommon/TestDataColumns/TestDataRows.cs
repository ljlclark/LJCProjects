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

      // Create a rows collection of data columns collections.
      var dataRows = new LJCDataRows();

      // Create a row of data columns and add to the data rows.
      var rowColumns = new LJCDataColumns();
      var dataColumn = new LJCDataColumn("FirstName", "First Name");
      rowColumns.Add(dataColumn);
      dataColumn = new LJCDataColumn("MiddleName", "Middle Name");
      rowColumns.Add(dataColumn);
      dataColumn = new LJCDataColumn("LastName", "First Last Name");
      rowColumns.Add(dataColumn);
      dataRows.Add(rowColumns);

      // Create a row of data columns and add to the data rows.
      rowColumns = new LJCDataColumns();
      dataColumn = new LJCDataColumn("FirstName", "First Name");
      rowColumns.Add(dataColumn);
      dataColumn = new LJCDataColumn("MiddleName", "Middle Name");
      rowColumns.Add(dataColumn);
      dataColumn = new LJCDataColumn("LastName", "Second Last Name");
      rowColumns.Add(dataColumn);
      dataRows.Add(rowColumns);

      // Create a row of data columns and add to the data rows.
      rowColumns = new LJCDataColumns();
      dataColumn = new LJCDataColumn("FirstName", "First Name");
      rowColumns.Add(dataColumn);
      dataColumn = new LJCDataColumn("MiddleName", "Middle Name");
      rowColumns.Add(dataColumn);
      dataColumn = new LJCDataColumn("LastName", "Final Last Name");
      rowColumns.Add(dataColumn);
      dataRows.Add(rowColumns);

      // Create a row of data columns and add to the data rows.
      rowColumns = new LJCDataColumns();
      dataColumn = new LJCDataColumn("FirstName", "Different First");
      rowColumns.Add(dataColumn);
      dataColumn = new LJCDataColumn("MiddleName", "Different Middle");
      rowColumns.Add(dataColumn);
      dataColumn = new LJCDataColumn("LastName", "Final Last Name");
      rowColumns.Add(dataColumn);
      dataRows.Add(rowColumns);

      // Add the unique compare values.
      // The row is identified by its column property name values and column
      // values.
      var rowColumnPropertyNameValue = "LastName";
      var rowColumnValue = "Final Last Name";
      var keyColumns = new LJCDataColumns()
      {
        { rowColumnPropertyNameValue, rowColumnValue },
      };
      dataRows.LJCKeyColumns = keyColumns;

      // Test Method
      rowColumns = dataRows.LJCGetUnique();

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
        dataColumn = rowColumns.LJCGetUnique(keyColumns);
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

      // Create a rows collection of data columns collections.
      var dataRows = new LJCDataRows();

      // Create a row of data columns and add to the data rows.
      var rowColumns = new LJCDataColumns();
      var dataColumn = new LJCDataColumn("FirstName", "First");
      rowColumns.Add(dataColumn);
      dataColumn = new LJCDataColumn("MiddleName", "Middle");
      rowColumns.Add(dataColumn);
      dataColumn = new LJCDataColumn("LastName", "First Last");
      rowColumns.Add(dataColumn);
      dataRows.Add(rowColumns);

      // Create a row of data columns and add to the data rows.
      rowColumns = new LJCDataColumns();
      dataColumn = new LJCDataColumn("FirstName", "First");
      rowColumns.Add(dataColumn);
      dataColumn = new LJCDataColumn("MiddleName", "Middle");
      rowColumns.Add(dataColumn);
      dataColumn = new LJCDataColumn("LastName", "Second Last");
      rowColumns.Add(dataColumn);
      dataRows.Add(rowColumns);

      // Create a row of data columns and add to the data rows.
      rowColumns = new LJCDataColumns();
      dataColumn = new LJCDataColumn("FirstName", "First");
      rowColumns.Add(dataColumn);
      dataColumn = new LJCDataColumn("MiddleName", "Middle");
      rowColumns.Add(dataColumn);
      dataColumn = new LJCDataColumn("LastName", "Final Last");
      rowColumns.Add(dataColumn);
      dataRows.Add(rowColumns);

      // Add the unique compare values.
      // The sort is identified by its column property name values and column
      // values.
      var keyColumns = new LJCDataColumns()
      {
        // KeyColumnName, ColumnValue
        { "LastName", "Final Last" },
      };
      dataRows.LJCKeyColumns = keyColumns;

      // Test Method
      dataRows.LJCSort();

      // Get the first data row.
      rowColumns = dataRows[0];

      // Add the unique compare values.
      // The column is identified by its property names and values.
      keyColumns = new LJCDataColumns()
      {
        { "PropertyName", "LastName" },
      };
      dataColumn = rowColumns.LJCGetUnique(keyColumns);

      var result = $"{dataColumn.Value}";
      var compare = "Final Last";
      TestCommon.Write($"{methodName}", result, compare);
    }
    #endregion

    #region Custom Data Methods

    // Dynamic binary search with key columns.
    private void LJCBinarySearch()
    {
      var methodName = "LJCBinarySearch()";

      // Create a rows collection of data columns collections.
      var dataRows = new LJCDataRows();

      // Create a row of data columns and add to the data rows.
      var rowColumns = new LJCDataColumns();
      var dataColumn = new LJCDataColumn("FirstName", "First");
      rowColumns.Add(dataColumn);
      dataColumn = new LJCDataColumn("MiddleName", "Middle");
      rowColumns.Add(dataColumn);
      dataColumn = new LJCDataColumn("LastName", "First Last");
      rowColumns.Add(dataColumn);
      dataRows.Add(rowColumns);

      // Create a row of data columns and add to the data rows.
      rowColumns = new LJCDataColumns();
      dataColumn = new LJCDataColumn("FirstName", "First");
      rowColumns.Add(dataColumn);
      dataColumn = new LJCDataColumn("MiddleName", "Middle");
      rowColumns.Add(dataColumn);
      dataColumn = new LJCDataColumn("LastName", "Second Last");
      rowColumns.Add(dataColumn);
      dataRows.Add(rowColumns);

      // Create a row of data columns and add to the data rows.
      rowColumns = new LJCDataColumns();
      dataColumn = new LJCDataColumn("FirstName", "First");
      rowColumns.Add(dataColumn);
      dataColumn = new LJCDataColumn("MiddleName", "Middle");
      rowColumns.Add(dataColumn);
      dataColumn = new LJCDataColumn("LastName", "Final Last");
      rowColumns.Add(dataColumn);
      dataRows.Add(rowColumns);

      // Add the unique compare values.
      // The search is identified by its column property name values and column
      // values.
      var keyColumns = new LJCDataColumns()
      {
        // KeyColumnName, ColumnValue
        { "LastName", "Final Last" },
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

        // Add the unique compare values.
        // The column is identified by its property names and values.
        keyColumns = new LJCDataColumns()
        {
          { "PropertyName", "LastName" },
        };
        dataColumn = rowColumns.LJCGetUnique(keyColumns);
        value = $"{dataColumn.Value}";
      }

      var result = value;
      var compare = "Final Last";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Compares column value to key column value.
    private void LJCCompareColumn()
    {
      var methodName = "LJCCompareColumn()";

      // Create a rows collection of data columns collections.
      var dataRows = new LJCDataRows();

      // Create a row of data columns and add to the data rows.
      var rowColumns = new LJCDataColumns();
      // PropertyName, Value
      var dataColumn = new LJCDataColumn("FirstName", "First");
      rowColumns.Add(dataColumn);
      dataColumn = new LJCDataColumn("MiddleName", "Middle");
      rowColumns.Add(dataColumn);
      dataColumn = new LJCDataColumn("LastName", "First Last");
      rowColumns.Add(dataColumn);
      dataRows.Add(rowColumns);

      // Create a row of data columns and add to the data rows.
      rowColumns = new LJCDataColumns();
      dataColumn = new LJCDataColumn("FirstName", "First");
      rowColumns.Add(dataColumn);
      dataColumn = new LJCDataColumn("MiddleName", "Middle");
      rowColumns.Add(dataColumn);
      dataColumn = new LJCDataColumn("LastName", "Second Last");
      rowColumns.Add(dataColumn);
      dataRows.Add(rowColumns);

      // Create a row of data columns and add to the data rows.
      rowColumns = new LJCDataColumns();
      dataColumn = new LJCDataColumn("FirstName", "First");
      rowColumns.Add(dataColumn);
      dataColumn = new LJCDataColumn("MiddleName", "Middle");
      rowColumns.Add(dataColumn);
      dataColumn = new LJCDataColumn("LastName", "Final Last");
      rowColumns.Add(dataColumn);
      dataRows.Add(rowColumns);

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
        dataColumn = rowColumnsItem.LJCGetUnique(rowsKeyColumns);
        var dataColumnValue = $"{dataColumn.Value}";

        // Add the unique compare values.
        // The search is identified by its column property name values and column
        // values.
        var searchColumnValue = "Final Last";
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
      var compare = "Final Last";
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
