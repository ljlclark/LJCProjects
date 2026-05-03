// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DBJoinsProgram.cs
using LJCNetCommon5;

namespace TestDBJoins5
{
  internal class DBJoinsProgram
  {
    // The entry method.
    static void Main()
    {
      TestCommon = new LJCTestCommon("LJCDBJoins");
      Console.WriteLine();
      Console.WriteLine("*** LJCDBJoins ***");

      // Constructor Methods
      ConstructorCopy();

      // Collection Methods
      Add();
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

      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write($"{methodName}", result, compare);
    }
    #endregion

    #region Collection Methods

    // Creates and adds the element from the supplied values.
    private static void Add()
    {
      var methodName = "Add()";

      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write($"{methodName}", result, compare);
    }

    // Creates and returns a clone of the object.
    private static void Clone()
    {
      var methodName = "Clone()";

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
