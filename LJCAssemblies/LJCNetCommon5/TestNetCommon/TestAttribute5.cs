// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// TestAttribute5.cs
using LJCNetCommon5;

namespace TestNetCommon5
{
  // Methods to test LJCAttribute.
  internal class TestAttribute
  {
    #region Properties

    // Gets or sets the TestCommon object.
    private LJCTestCommon TestCommon { get; set; } = null!;
    #endregion

    #region Constructor Methods

    // Initializes an object instance.
    /// <include file='Doc/TestAttribute.xml'
    ///  path='members/Constructor/*'/>
    public TestAttribute()
    {
      TestCommon = new LJCTestCommon("LJCAttribute");
      Run();
    }

    // Runs the tests.
    private void Run()
    {
      // Constructor Test Methods
      Constructor();
    }
    #endregion

    #region Constructor Test Methods

    // Initializes an object instance.
    private void Constructor()
    {
      var attrib = new LJCAttribute("Name", "Value");
      var result = attrib.Name;
      var compare = "Name";
      TestCommon.Write("Constructor1", result, compare);

      result = attrib.Value;
      compare = "Value";
      TestCommon.Write("Constructor2", result, compare);
    }
    #endregion
  }
}
