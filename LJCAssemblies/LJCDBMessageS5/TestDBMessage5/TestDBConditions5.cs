// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// TestDBConditions5.cs
using LJCNetCommon5;

namespace TestDBMessage5
{
  // Provides LJCDBConditions test methods.
  internal class TestDBConditions
  {
    #region Properties

    // Gets or sets the TestCommon object.
    private LJCTestCommon TestCommon { get; set; } = null!;
    #endregion

    #region Constructor Methods

    // Initializes an object instance.
    public TestDBConditions()
    {
      TestCommon = new LJCTestCommon("LJCDBConditions");
      Run();
    }

    // Runs the tests.
    private void Run()
    {
      // Constructor Test Methods
      ConstructorCopy();

      // Collection Methods
      Add();
      Clone();
      HasItems();
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
    private void Add()
    {
      var methodName = "Add()";

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

    // Checks if the collection has items.
    private void HasItems()
    {
      var methodName = "HasItems()";

      var result = "Not Implemented";
      var compare = "";
      TestCommon.Write($"{methodName}", result, compare);
    }
    #endregion
  }
}
