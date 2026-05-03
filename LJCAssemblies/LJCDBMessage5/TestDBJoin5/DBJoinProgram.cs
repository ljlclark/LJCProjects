// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DBJoinProgram.cs
using LJCDBMessage5;
using LJCNetCommon5;

namespace TestDBJoin5
{
  // The entry class.
  internal class DBJoinProgram
  {
    // The entry method.
    static void Main()
    {
      TestCommon = new LJCTestCommon("LJCDBJoin");
      Console.WriteLine();
      Console.WriteLine("*** LJCDBJoin ***");

      // Constructor Methods
      ConstructorCopy();

      // Methods
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

      var dbJoin = new LJCDBJoin()
      {
        TableAlias = "t",
        TableName = "TableName",
      };
      var copy = new LJCDBJoin(dbJoin);
      var result = copy.TableName;
      var compare = "TableName";
      TestCommon?.Write($"{methodName}", result, compare);
    }
    #endregion

    #region Methods

    // Creates and returns a clone of the object.
    private static void Clone()
    {
      var methodName = "Clone()";

      var dbJoin = new LJCDBJoin()
      {
        TableAlias = "t",
        TableName = "TableName",
      };
      var clone = dbJoin.Clone();
      var result = clone.TableName;
      var compare = "TableName";
      TestCommon?.Write($"{methodName}", result, compare);
    }
    #endregion

    #region Properties

    // Gets or sets the TestCommon object.
    private static LJCTestCommon? TestCommon { get; set; }
    #endregion
  }
}
