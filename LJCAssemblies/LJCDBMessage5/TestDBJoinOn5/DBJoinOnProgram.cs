// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DBJoinOnProgram.cs
using LJCDBMessage5;
using LJCNetCommon5;

namespace TestDBJoinOn5
{
  // The entry class.
  internal class DBJoinOnProgram
  {
    // The entry method.
    static void Main()
    {
      TestCommon = new LJCTestCommon("LJCDBJoinOn");
      Console.WriteLine();
      Console.WriteLine("*** LJCDBJoinOn ***");

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

      var dbJoinOn = new LJCDBJoinOn()
      {
        FromColumnName = "From",
        ToColumnName = "To",
      };
      var result = dbJoinOn.FromColumnName;
      var compare = "From";
      TestCommon?.Write($"{methodName}", result, compare);
    }
    #endregion

    #region Methods

    // Creates and returns a clone of the object.
    private static void Clone()
    {
      var methodName = "Clone()";

      var dbJoinOn = new LJCDBJoinOn()
      {
        FromColumnName = "From",
        ToColumnName = "To",
      };
      var clone = dbJoinOn.Clone();
      var result = clone?.FromColumnName;
      var compare = "From";
      TestCommon?.Write($"{methodName}", result, compare);
    }
    #endregion

    #region Properties

    // Gets or sets the TestCommon object.
    private static LJCTestCommon? TestCommon { get; set; }
    #endregion
  }
}
