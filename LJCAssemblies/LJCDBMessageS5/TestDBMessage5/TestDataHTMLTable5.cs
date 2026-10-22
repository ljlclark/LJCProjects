// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// TestDataHTMLTable5.cs
using LJCNetCommon5;

namespace TestDBMessage5
{
  // Provides LJCDataHTMLTable test methods.
  internal class TestDataHTMLTable
  {
    #region Properties

    // Gets or sets the TestCommon object.
    private LJCTestCommon TestCommon { get; set; } = null!;
    #endregion

    #region Constructor Methods

    // Initializes an object instance.
    public TestDataHTMLTable()
    {
      TestCommon = new LJCTestCommon("LJCDataHTML");
      Run();
    }

    // Runs the tests.
    private void Run()
    {
      // DataTable Methods
      DataTableHeadings();
      DataTableHTML();
      DataTableRows();

      // Data Object Methods
      DataHeadings();
      DataHTML();
      DataRows();

      // DbResult Methods
      ResultHeadings();
      ResultHTML();
      ResultRows();
    }
    #endregion

    #region DataTable Methods

    // Create table headings from a DataTable.
    private void DataTableHeadings()
    {
      var methodName = "DataTableHeadings()";

      var result = "Not Implemented";
      var compare = "";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Creates an HTML table from a DataTable.
    private void DataTableHTML()
    {
      var methodName = "DataTableHTML()";

      var result = "Not Implemented";
      var compare = "";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Creates table rows from a DataTable.
    private void DataTableRows()
    {
      var methodName = "DataTableRows()";

      var result = "Not Implemented";
      var compare = "";
      TestCommon.Write($"{methodName}", result, compare);
    }
    #endregion

    #region Data Object Methods

    // Create table headings from a Data Object.
    private void DataHeadings()
    {
      var methodName = "DataHeadings()";

      var result = "Not Implemented";
      var compare = "";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Create an HTML table from a Data Object.
    private void DataHTML()
    {
      var methodName = "DataHTML()";

      var result = "Not Implemented";
      var compare = "";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Create table rows from a Data Object.
    private void DataRows()
    {
      var methodName = "DataRows()";

      var result = "Not Implemented";
      var compare = "";
      TestCommon.Write($"{methodName}", result, compare);
    }
    #endregion

    #region DbResult Methods

    // Create table headings from a DbResult.
    private void ResultHeadings()
    {
      var methodName = "ResultHeadings()";

      var result = "Not Implemented";
      var compare = "";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Creates an HTML table from a DbResult.
    private void ResultHTML()
    {
      var methodName = "ResultHTML()";

      var result = "Not Implemented";
      var compare = "";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Create table rows from a DbResult.
    private void ResultRows()
    {
      var methodName = "ResultRows()";

      var result = "Not Implemented";
      var compare = "";
      TestCommon.Write($"{methodName}", result, compare);
    }
    #endregion
  }
}
