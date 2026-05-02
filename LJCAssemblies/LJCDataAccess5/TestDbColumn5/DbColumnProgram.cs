// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DbColumnProgram.cs
using LJCDataAccess5;
using LJCNetCommon5;
using System.Data;

namespace TestDbColumn5
{
  // The entry class.
  internal class DbColumnProgram
  {
    // The entry method.
    static void Main()
    {
      TestCommon = new LJCTestCommon("LJCDbColumn");
      Console.WriteLine();
      Console.WriteLine("*** LJCDbColumn ***");

      // Static Methods
      Clone();
      ToDataColumn();

      Console.WriteLine();
      Console.WriteLine("Press any key to continue...");
      Console.ReadKey();
    }

    #region Static Methods

    // Clones a DataColumn object.
    private static void Clone()
    {
      var methodName = "Clone()";

      var dbColumn = new DataColumn()
      {
        AllowDBNull = false,
        AutoIncrement = true,
        Caption = "Name",
        ColumnName = "Name",
        DataType = typeof(string),
        DefaultValue = null,
        MaxLength = 60,
        Unique = false,
      };

      // Test Method
      var cloneColumn = LJCDbColumn.Clone(dbColumn);
      var result = "";
      if (cloneColumn != null)
      {
        result = cloneColumn.ColumnName;
      }
      var compare = "Name";
      TestCommon?.Write($"{methodName}", result, compare);
    }

    private static void ToDataColumn()
    {
      var methodName = "ToDataColumn()";

      var dbColumn = new DataColumn()
      {
        AllowDBNull = false,
        AutoIncrement = true,
        Caption = "Name",
        ColumnName = "Name",
        DataType = typeof(string),
        DefaultValue = null,
        MaxLength = 60,
        Unique = false,
      };

      // Test Method
      var dataColumn = LJCDbColumn.ToDataColumn(dbColumn);
      var result = "";
      if (dataColumn != null)
      {
        result = dataColumn.ColumnName;
      }
      var compare = "Name";
      TestCommon?.Write($"{methodName}", result, compare);
    }
    #endregion

    #region Properties

    // Gets or sets the TestCommon object.
    private static LJCTestCommon? TestCommon { get; set; }
    #endregion
  }
}
