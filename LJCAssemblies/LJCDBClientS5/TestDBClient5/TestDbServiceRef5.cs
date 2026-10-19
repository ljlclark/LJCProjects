// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// TestDbServiceRef5.cs
using LJCNetCommon5;

namespace TestDataManager5
{
  internal class TestDbServiceRef
  {
    #region Properties

    // Gets or sets the TestCommon object.
    private LJCTestCommon TestCommon { get; set; }
    #endregion

    #region Constructor Methods

    // Initializes an object instance.
    /// <include file='Doc/TestDbServiceRef.xml'
    ///  path='members/Constructor/*'/>
    public TestDbServiceRef()
    {
      TestCommon = new LJCTestCommon("TestDbServiceRef");
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
