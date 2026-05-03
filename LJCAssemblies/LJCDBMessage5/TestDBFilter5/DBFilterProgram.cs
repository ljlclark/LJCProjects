// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DBFilterProgram.cs
using LJCDBMessage5;
using LJCNetCommon5;

namespace TestDBFilter5
{
  // The entry class.
  internal class DBFilterProgram
  {
    // The entry method.
    static void Main()
    {
      TestCommon = new LJCTestCommon("LJCDBFilter");
      Console.WriteLine();
      Console.WriteLine("*** LJCDBFilter ***");

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

      var dbConditionSet = new LJCDBConditionSet()
      {
        BooleanOperator = "and",
        Conditions = [],
      };
      var dbFilter = new LJCDBFilter()
      {
        BooleanOperator = "and",
        ConditionSet = dbConditionSet,
        Filters = [],
        Name = "Filter",
      };
      var copy = new LJCDBFilter(dbFilter);
      var result = copy.Name;
      var compare = "Filter";
      TestCommon?.Write($"{methodName}", result, compare);
    }
    #endregion

    #region Methods

    // Creates and returns a clone of the object.
    private static void Clone()
    {
      var methodName = "Clone()";

      var dbConditionSet = new LJCDBConditionSet()
      {
        BooleanOperator = "and",
        Conditions = [],
      };
      var dbFilter = new LJCDBFilter()
      {
        BooleanOperator = "and",
        ConditionSet = dbConditionSet,
        Filters = [],
        Name = "Filter",
      };
      var clone = dbFilter.Clone();
      var result = clone?.Name;
      var compare = "Filter";
      TestCommon?.Write($"{methodName}", result, compare);
    }
    #endregion

    #region Properties

    // Gets or sets the TestCommon object.
    private static LJCTestCommon? TestCommon { get; set; }
    #endregion
  }
}
