// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// TestDBFilter5.cs
using LJCDBMessage5;
using LJCNetCommon5;

namespace TestDBMessage5
{
  // Provides LJCDBCommon test methods.
  internal class TestDBFilter
  {
    #region Properties

    // Gets or sets the TestCommon object.
    private LJCTestCommon TestCommon { get; set; } = null!;
    #endregion

    #region Constructor Methods

    // Initializes an object instance.
    public TestDBFilter()
    {
      TestCommon = new LJCTestCommon("LJCDBFilter");
      Run();
    }

    // Runs the tests.
    private void Run()
    {
      // Constructor Test Methods
      ConstructorCopy();

      // Methods
      Clone();
    }
    #endregion

    #region Constructor Test Methods

    // The Copy constructor.
    private void ConstructorCopy()
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
      TestCommon.Write($"{methodName}", result, compare);
    }
    #endregion

    #region Methods

    // Creates and returns a clone of the object.
    private void Clone()
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
      TestCommon.Write($"{methodName}", result, compare);
    }
    #endregion
  }
}
