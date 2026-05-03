// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DataHTMLProgram.cs
using LJCNetCommon5;

namespace TestDataHTMLTable5
{
  // The entry class.
  internal class DataHTMLTableProgram
  {
    // The entry method.
    static void Main(string[] args)
    {
      TestCommon = new LJCTestCommon("LJCDataAccess");
      Console.WriteLine();
      Console.WriteLine("*** LJCDataAccess ***");

      // Static DataTable Methods
      DataTableHeadings();
      DataTableHTML();
      DataTableRows();

      // Static Data Object Methods
      DataHeadings();
      DataHTML();
      DataRows();

      // Static DbResult Methods
      ResultHeadings();
      ResultHTML();
      ResultRows();

      Console.WriteLine();
      Console.WriteLine("Press any key to continue...");
      Console.ReadKey();
    }

    #region Static DataTable Methods

    private static void DataTableHeadings()
    {
      var methodName = "DataTableHeadings()";

      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write($"{methodName}", result, compare);
    }

    private static void DataTableHTML()
    {
      var methodName = "DataTableHTML()";

      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write($"{methodName}", result, compare);
    }

    private static void DataTableRows()
    {
      var methodName = "DataTableRows()";

      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write($"{methodName}", result, compare);
    }
    #endregion

    #region Static Data Object Methods

    private static void DataHeadings()
    {
      var methodName = "DataHeadings()";

      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write($"{methodName}", result, compare);
    }

    private static void DataHTML()
    {
      var methodName = "DataHTML()";

      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write($"{methodName}", result, compare);
    }

    private static void DataRows()
    {
      var methodName = "DataRows()";

      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write($"{methodName}", result, compare);
    }
    #endregion

    #region Static DbResult Methods

    private static void ResultHeadings()
    {
      var methodName = "ResultHeadings()";

      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write($"{methodName}", result, compare);
    }

    private static void ResultHTML()
    {
      var methodName = "ResultHTML()";

      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write($"{methodName}", result, compare);
    }

    private static void ResultRows()
    {
      var methodName = "ResultRows()";

      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write($"{methodName}", result, compare);
    }
    #endregion

    #region Properties

    // Gets or sets the TestCommon object.
    private static LJCTestCommon? TestCommon { get; set; }
    #endregion
  }
}
