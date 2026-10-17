// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// TestPropertyDelegate5.cs
using LJCNetCommon5;

namespace TestNetCommon5
{
  // Provides the LJCPropertyDelegate test methods.
  internal class TestPropertyDelegate
  {
    #region Properties

    // Gets or sets the TestCommon object.
    private LJCTestCommon TestCommon { get; set; } = null!;
    #endregion

    #region Constructor Methods

    // Initializes an object instance.
    /// <include file='Doc/TestPropertyDelegate.xml'
    ///  path='members/Constructor/*'/>
    public TestPropertyDelegate()
    {
      TestCommon = new LJCTestCommon("TestPropertyDelegate");
      Run();
    }

    // Runs the test methods.
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
