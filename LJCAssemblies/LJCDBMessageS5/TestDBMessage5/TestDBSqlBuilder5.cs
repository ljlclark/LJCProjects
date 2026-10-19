// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// TestDBSqlBuilder5.cs
using LJCNetCommon5;

namespace TestDBMessage5
{
  // Methods to test LJCDBSqlBuilder.
  internal class TestDBSqlBuilder
  {
    #region Properties

    // Gets or sets the TestCommon object.
    private LJCTestCommon TestCommon { get; set; } = null!;
    #endregion

    #region Constructor Methods

    // Initializes an object instance.
    /// <include file='Doc/TestDBSqlBuilder.xml'
    ///  path='members/Constructor/*'/>
    public TestDBSqlBuilder()
    {
      TestCommon = new LJCTestCommon("LJCDBSqlBuilder");
      Run();
    }

    // Runs the tests.
    private void Run()
    {
      // Test Methods
      Test();
    }
    #endregion

    #region Test Methods

    private void Test()
    {
      var methodName = "Test()";

      var result = "Not Implemented";
      var compare = "";
      TestCommon.Write($"{methodName}", result, compare);
    }
    #endregion
  }
}
