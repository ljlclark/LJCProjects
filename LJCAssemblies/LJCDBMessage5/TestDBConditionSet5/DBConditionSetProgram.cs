// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DBConditionSetProgram.cs
using LJCNetCommon5;

namespace TestDBConditionSet5
{
  // The entry class.
  internal class DBConditionSetProgram
  {
    // The entry method.
    static void Main()
    {
      TestCommon = new LJCTestCommon("LJCDBConditionSet");
      Console.WriteLine();
      Console.WriteLine("*** LJCDBConditionSet ***");

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

      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write($"{methodName}", result, compare);
    }
    #endregion

    #region Methods

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
