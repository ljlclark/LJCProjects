// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// TestDBRows5.cs
using LJCNetCommon5;

namespace TestDBMessage5
{
  // Methods to test LJCDBRows.
  internal class TestDBRows
  {
    #region Properties

    // Gets or sets the TestCommon object.
    private LJCTestCommon TestCommon { get; set; } = null!;
    #endregion

    #region Constructor Methods

    // Initializes an object instance.
    /// <include file='Doc/TestDBRows.xml'
    ///  path='members/Constructor/*'/>
    public TestDBRows()
    {
      TestCommon = new LJCTestCommon("LJCDBRows");
      Run();
    }

    // Runs the tests.
    private void Run()
    {
      // Static Methods
      LJCDeserialize();

      // Constructor Methods
      ConstructorCopy();

      // Collection Methods
      Add();
      Clone();
      HasItems();
      Serialize();
    }
    #endregion

    #region Static Methods

    // Deserializes from the specified XML file.
    private void LJCDeserialize()
    {
      var methodName = "LJCDeserialize()";

      var result = "Not Implemented";
      var compare = "";
      TestCommon.Write($"{methodName}", result, compare);
    }
    #endregion

    #region Constructor Methods

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

    // Adds the supplied object.
    private void Add()
    {
      var methodName = "Add()";

      var result = "Not Implemented";
      var compare = "";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Clones the structure of the object.
    private void Clone()
    {
      var methodName = "Clone()";

      var result = "Not Implemented";
      var compare = "";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Clones the structure of the object.
    private void HasItems()
    {
      var methodName = "HasItems()";

      var result = "Not Implemented";
      var compare = "";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Serialize the object to the specified file.
    private void Serialize()
    {
      var methodName = "Serialize()";

      var result = "Not Implemented";
      var compare = "";
      TestCommon.Write($"{methodName}", result, compare);
    }
    #endregion
  }
}
