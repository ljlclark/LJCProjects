// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// TestDBCondition5.cs
using LJCDBMessage5;
using LJCNetCommon5;

namespace TestDBMessage5
{
  // Methods to test LJCDBCommon.
  internal class TestDBCondition
  {
    #region Properties

    // Gets or sets the TestCommon object.
    private LJCTestCommon TestCommon { get; set; } = null!;
    #endregion

    #region Constructor Methods

    // Initializes an object instance.
    /// <include file='Doc/TestDBCondition.xml'
    ///  path='members/Constructor/*'/>
    public TestDBCondition()
    {
      TestCommon = new LJCTestCommon("LJCDBCondition");
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

    #region Constructor Methods

    // The Copy constructor.
    private void ConstructorCopy()
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
      TestCommon.Write($"{methodName}", result, compare);
    }
    #endregion

    #region Methods

    // Creates and returns a clone of the object.
    private void Clone()
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
      TestCommon.Write($"{methodName}", result, compare);
    }
    #endregion
  }
}
