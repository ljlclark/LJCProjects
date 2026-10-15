// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// TestDBRequest5.cs
using LJCNetCommon5;

namespace TestDBMessage5
{
  // Methods to test LJCDBCommon.
  internal class TestDBRequest
  {
    #region Properties

    // Gets or sets the TestCommon object.
    private LJCTestCommon TestCommon { get; set; } = null!;
    #endregion

    #region Constructor Methods

    // Initializes an object instance.
    /// <include file='Doc/TestDBJoinOns.xml'
    ///  path='members/Constructor/*'/>
    public TestDBRequest()
    {
      TestCommon = new LJCTestCommon("LJCDBRequest");
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
