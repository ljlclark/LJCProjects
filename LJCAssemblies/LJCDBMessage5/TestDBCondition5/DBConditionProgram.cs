// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DBConditionProgram.cs
using LJCDBMessage5;
using LJCNetCommon5;

namespace TestDBCondition5
{
  // The entry class.
  internal class DBConditionProgram
  {
    // The entry method.
    static void Main()
    {
      TestCommon = new LJCTestCommon("LJCDBCondition");
      Console.WriteLine();
      Console.WriteLine("*** LJCDBCondition ***");

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

      var dbCondition = new LJCDBCondition()
      {
        ComparisonOperator = "=",
        FirstValue = "First",
        SecondValue = "Second",
      };
      var copy = new LJCDBCondition(dbCondition);
      var result = copy.FirstValue;
      var compare = "First";
      TestCommon?.Write($"{methodName}", result, compare);
    }
    #endregion

    #region Methods

    // Creates and returns a clone of the object.
    private static void Clone()
    {
      var methodName = "Clone()";

      var dbCondition = new LJCDBCondition()
      {
        ComparisonOperator = "=",
        FirstValue = "First",
        SecondValue = "Second",
      };
      var clone = dbCondition.Clone();
      var result = clone?.FirstValue;
      var compare = "First";
      TestCommon?.Write($"{methodName}", result, compare);
    }
    #endregion

    #region Properties

    // Gets or sets the TestCommon object.
    private static LJCTestCommon? TestCommon { get; set; }
    #endregion
  }
}
