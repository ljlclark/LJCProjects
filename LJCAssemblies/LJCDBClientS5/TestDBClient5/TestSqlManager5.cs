// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// TestSqlManager5.cs
using LJCNetCommon5;

namespace TestDataManager5
{
  internal class TestSqlManager
  {
    #region Properties

    // Gets or sets the TestCommon object.
    private LJCTestCommon TestCommon { get; set; }
    #endregion

    #region Constructor Methods

    // Initializes an object instance.
    /// <include file='Doc/TestDbServiceRef.xml'
    ///  path='members/Constructor/*'/>
    public TestSqlManager()
    {
      TestCommon = new LJCTestCommon("TestSqlManager");
      Run();
    }

    // Run the tests.
    private void Run()
    {
      Test();
    }

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
