// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// TestDBFilters5.cs
using LJCNetCommon5;

namespace TestDBMessage5
{
  // Methods to test LJCDBCommon.
  internal class TestDBFilters
  {
    #region Properties

    // Gets or sets the TestCommon object.
    private LJCTestCommon TestCommon { get; set; } = null!;
    #endregion

    #region Constructor Methods

    // Initializes an object instance.
    /// <include file='Doc/TestDBFilters.xml'
    ///  path='members/Constructor/*'/>
    public TestDBFilters()
    {
      TestCommon = new LJCTestCommon("LJCDBFilters");
      Run();
    }

    // Runs the tests.
    private void Run()
    {
      // Static Methods
      //SQLSoundexFilters();
      //SoundexFilters();

      // Constructor Methods
      ConstructorCopy();

      // Collection Methods
      Add1();
      Add2();
      Clone();
    }
    #endregion

    #region Constructor Test Methods

    // The Copy constructor.
    private void ConstructorCopy()
    {
      var methodName = "ConstructorCopy()";

      var result = "Not Implemented";
      var compare = "";
      TestCommon.Write($"{methodName}", result, compare);
    }
    #endregion

    #region Collection Methods

    // Creates and adds the element from the supplied values.
    private void Add1()
    {
      var methodName = "Add1()";

      var result = "Not Implemented";
      var compare = "";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Creates and adds the element from the supplied values.
    private void Add2()
    {
      var methodName = "Add2()";

      var result = "Not Implemented";
      var compare = "";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Creates and returns a clone of the object.
    private void Clone()
    {
      var methodName = "Clone()";

      var result = "Not Implemented";
      var compare = "";
      TestCommon.Write($"{methodName}", result, compare);
    }
    #endregion
  }
}
