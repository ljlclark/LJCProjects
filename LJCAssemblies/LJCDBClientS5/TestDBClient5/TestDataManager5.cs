// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// TestDataManager5.cs
using LJCNetCommon5;

namespace TestDataManager5
{
  // Provides LJCDataManager test methods.
  internal class TestDataManager
  {
    #region Properties

    // Gets or sets the TestCommon object.
    private LJCTestCommon TestCommon { get; set; }
    #endregion

    #region Constructor Methods

    // Initializes an object instance.
    public TestDataManager()
    {
      TestCommon = new LJCTestCommon("TestDataManager");
      Run();
    }

    // Run the tests.
    private void Run()
    {
      // Methods
      Test();
    }
    #endregion

    #region Methods

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
