// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// TestResultConverter5.cs
using LJCNetCommon5;

namespace TestDBMessage5
{
  // Povides LJCResultConverter test methods.
  internal class TestResultConverter
  {
    #region Properties

    // Gets or sets the TestCommon object.
    private LJCTestCommon TestCommon { get; set; } = null!;
    #endregion

    #region Constructor Methods

    // Initializes an object instance.
    public TestResultConverter()
    {
      TestCommon = new LJCTestCommon("LJCResultConverter");
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
