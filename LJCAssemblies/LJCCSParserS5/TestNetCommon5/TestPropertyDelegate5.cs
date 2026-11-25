// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// TestPropertyDelegate5.cs
using LJCNetCommon5;

namespace TestNetCommon5
{
  // Provides LJCPropertyDelegate test methods.
  internal class TestPropertyDelegate
  {
    #region Properties

    // Gets or sets the TestCommon object.
    private LJCTestCommon TestCommon { get; set; } = null!;
    #endregion

    #region Constructor Methods

    // Initializes an object instance.
    public TestPropertyDelegate()
    {
      TestCommon = new LJCTestCommon("TestPropertyDelegate");
      Run();
    }

    // Runs the test methods.
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
