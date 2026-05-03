// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DBRowProgram.cs
using LJCDBMessage5;
using LJCNetCommon5;

namespace TestDBRow5
{
  // The entry class.
  internal class DBRowProgram
  {
    // The entry method.
    static void Main()
    {
      TestCommon = new LJCTestCommon("LJCDBRow");
      Console.WriteLine();
      Console.WriteLine("*** LJCDBRow ***");

      // Constructor Methods
      ConstructorCopy();
      Clone();

      Console.WriteLine();
      Console.WriteLine("Press any key to continue...");
      Console.ReadKey();
    }

    #region Constructor Methods

    // The Copy constructor.
    private static void ConstructorCopy()
    {
      var methodName = "ConstructorCopy()";

      var dataValues = new LJCDataColumns()
      {
        { "ColumnName", (object)"Value" }
      };
      var dbRow = new LJCDBRow()
      {
        Values = dataValues,
      };
      var copy = new LJCDBRow(dbRow);
      var value = copy[0];
      var result = "";
      result = value?.PropertyName;
      var compare = "ColumnName";
      TestCommon?.Write($"{methodName}", result, compare);
    }

    // Creates and returns a clone of the object.
    private static void Clone()
    {
      var methodName = "Clone()";

      var dataValues = new LJCDataColumns()
      {
        { "ColumnName", (object)"Value" }
      };
      var dbRow = new LJCDBRow()
      {
        Values = dataValues,
      };
      var clone = dbRow.Clone();
      var result = "";
      if (clone != null)
      {
        var value = clone[0];
        result = value?.PropertyName;
      }
      var compare = "ColumnName";
      TestCommon?.Write($"{methodName}", result, compare);
    }
    #endregion

    #region Properties

    // Gets or sets the TestCommon object.
    private static LJCTestCommon? TestCommon { get; set; }
    #endregion
  }
}
