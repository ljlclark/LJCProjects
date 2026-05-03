// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// TestMappingProgram.cs

using LJCNetCommon5;

namespace TestTableMapping5
{
  // The entry class.
  internal class TableMappingProgram
  {
    // The entry method.
    static void Main()
    {
      TestCommon = new LJCTestCommon("LJCTableMapping");
      Console.WriteLine();
      Console.WriteLine("*** LJCTableMapping ***");

      // Methods
      AddColumnMap();
      AddTableMap();

      Console.WriteLine();
      Console.WriteLine("Press any key to continue...");
      Console.ReadKey();
    }

    #region Properties

    #region Methods

    // Adds a DataTable column map to the table mapping.
    private static void AddColumnMap()
    {
      var methodName = "AddColumnMap()";

      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write($"{methodName}", result, compare);
    }

    // Adds a DataTable map to the TableMaps collection.
    private static void AddTableMap()
    {
      var methodName = "AddTableMap()";

      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write($"{methodName}", result, compare);
    }
    #endregion

    // Gets or sets the TestCommon object.
    private static LJCTestCommon? TestCommon { get; set; }
    #endregion
  }
}
