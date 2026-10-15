// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// TestDBJoin5.cs
using LJCDBMessage5;
using LJCNetCommon5;

namespace TestDBMessage5
{
  // Methods to test LJCDBCommon.
  internal class TestDBJoin
  {
    #region Properties

    // Gets or sets the TestCommon object.
    private LJCTestCommon TestCommon { get; set; } = null!;
    #endregion

    #region Constructor Methods

    // Initializes an object instance.
    /// <include file='Doc/TestDBJoin.xml'
    ///  path='members/Constructor/*'/>
    public TestDBJoin()
    {
      TestCommon = new LJCTestCommon("LJCDBJoin");
      Run();
    }

    // Runs the tests.
    private void Run()
    {
      // Constructor Methods
      ConstructorCopy();

      // Collection Methods
      Clone();
    }
    #endregion

    #region Constructor Test Methods

    // The Copy constructor.
    private void ConstructorCopy()
    {
      var methodName = "ConstructorCopy()";

      var dbJoin = new LJCDBJoin()
      {
        TableAlias = "t",
        TableName = "TableName",
      };
      var copy = new LJCDBJoin(dbJoin);
      var result = copy.TableName;
      var compare = "TableName";
      TestCommon.Write($"{methodName}", result, compare);
    }
    #endregion

    #region Collection Methods

    // Creates and returns a clone of the object.
    private void Clone()
    {
      var methodName = "Clone()";

      var dbJoin = new LJCDBJoin()
      {
        TableAlias = "t",
        TableName = "TableName",
      };
      var clone = dbJoin.Clone();
      var result = clone.TableName;
      var compare = "TableName";
      TestCommon.Write($"{methodName}", result, compare);
    }
    #endregion
  }
}
