// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// TestDbServiceRef5.cs
using LJCNetCommon5;

namespace TestDataManager5
{
  // Provides LJCDbServiceRef test methods.
  internal class TestDbServiceRef
  {
    #region Properties

    // Gets or sets the TestCommon object.
    private LJCTestCommon TestCommon { get; set; }
    #endregion

    #region Constructor Methods

    // Initializes an object instance.
    public TestDbServiceRef()
    {
      TestCommon = new LJCTestCommon("TestDbServiceRef");
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
