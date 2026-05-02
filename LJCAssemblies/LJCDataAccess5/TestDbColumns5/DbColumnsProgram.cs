// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DbColumnsProgram.cs

using LJCNetCommon5;
using System.Data;

namespace TestDbColumns5
{
  // The entry class.
  internal class DbColumnsProgram
  {
    // The entry method.
    static void Main()
    {
      TestCommon = new LJCTestCommon("LJCDbColumns");
      Console.WriteLine();
      Console.WriteLine("*** LJCDbColumns ***");

      // Static Methods

      Clone();
      ColumnNames();
      Columns();
      CreateColumns();
      HasColumns();
      ToDataColumns();

      Console.WriteLine();
      Console.WriteLine("Press any key to continue...");
      Console.ReadKey();
    }

    #region Static Methods

    // Clones a DataColumnCollection.
    private static void Clone()
    {
      var methodName = "Clone()";

      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write($"{methodName}", result, compare);
    }

    // Creates a Column Names list from a DataColumnCollection.
    private static void ColumnNames()
    {
      var methodName = "ColumnNames()";

      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write($"{methodName}", result, compare);
    }

    // Returns a set of DataColumns that match the supplied list.
    private static void Columns()
    {
      var methodName = "Columns()";

      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write($"{methodName}", result, compare);
    }

    // Creates a new DataColumns object.
    private static void CreateColumns()
    {
      var methodName = "CreateColumns()";

      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write($"{methodName}", result, compare);
    }

    // Checks the DataColumnCollection object for items.
    private static void HasColumns()
    {
      var methodName = "HasColumns()";

      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write($"{methodName}", result, compare);
    }

    // Creates an LJCDataColumns collection from a DataColumnCollection.
    private static void ToDataColumns()
    {
      var methodName = "ToDataColumns()";

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
