// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// TestManagerCommon5.cs
using LJCNetCommon5;

namespace TestDataManager5
{
  // Provides LJCManagerCommon test methods.
  internal class TestManagerCommon
  {
    #region Properties

    // Gets or sets the TestCommon object.
    private LJCTestCommon TestCommon { get; set; }
    #endregion

    #region Constructor Methods

    // Initializes an object instance.
    public TestManagerCommon()
    {
      TestCommon = new LJCTestCommon("TestManagerCommon");
      Run();
    }

    // Runs the tests.
    private void Run()
    {
      Test();
    }

    // A test method placeholder.
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
