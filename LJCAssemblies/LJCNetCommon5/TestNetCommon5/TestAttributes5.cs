// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// TestAttributes5.cs
using LJCNetCommon5;

namespace TestNetCommon5
{
  // Methods to test LJCAttributes.
  internal class TestAttributes
  {
    #region Properties

    // Gets or sets the TestCommon object.
    private LJCTestCommon TestCommon { get; set; } = null!;
    #endregion

    #region Constructor Methods

    // Initializes an object instance.
    /// <include file='Doc/TestAttributes.xml'
    ///  path='members/Constructor/*'/>
    public TestAttributes()
    {
      TestCommon = new LJCTestCommon("LJCAttributes");
      Run();
    }

    // Runs the tests.
    private void Run()
    {
      // Collection Methods
      Add();
    }
    #endregion

    #region Collection Methods

    // Creates and adds an Attribute.
    private void Add()
    {
      var attribs = new LJCAttributes();
      var name = "id";
      // Test Method
      attribs.Add(name, "idValue");
      var attrib = attribs[0];
      var result = attrib.Name;
      var compare = "id";
      TestCommon.Write("Add()", result, compare);
    }
    #endregion
  }
}
