// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// TestDBJoinOn5.cs
using LJCDBMessage5;
using LJCNetCommon5;

namespace TestDBMessage5
{
  // Methods to test LJCDBCommon.
  internal class TestDBJoinOn
  {
    #region Properties

    // Gets or sets the TestCommon object.
    private LJCTestCommon TestCommon { get; set; } = null!;
    #endregion

    #region Constructor Methods

    // Initializes an object instance.
    /// <include file='Doc/TestDBJoinOn.xml'
    ///  path='members/Constructor/*'/>
    public TestDBJoinOn()
    {
      TestCommon = new LJCTestCommon("LJCDBJoinOn");
      Run();
    }

    // Runs the tests.
    private void Run()
    {
      // Constructor Methods
      ConstructorCopy();

      // Collection Methods
      Clone();
    }
    #endregion

    #region Constructor Test Methods

    // The Copy constructor.
    private void ConstructorCopy()
    {
      var methodName = "ConstructorCopy()";

      var dbJoinOn = new LJCDBJoinOn()
      {
        FromColumnName = "From",
        ToColumnName = "To",
      };
      var result = dbJoinOn.FromColumnName;
      var compare = "From";
      TestCommon.Write($"{methodName}", result, compare);
    }
    #endregion

    #region Collection Methods

    // Creates and returns a clone of the object.
    private void Clone()
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
      TestCommon.Write($"{methodName}", result, compare);
    }
    #endregion
  }
}
