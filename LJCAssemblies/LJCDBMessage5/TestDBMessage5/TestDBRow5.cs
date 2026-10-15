// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// TestDBRow5.cs
using LJCDBMessage5;
using LJCNetCommon5;

namespace TestDBMessage5
{
  // Methods to test LJCDBRow.
  internal class TestDBRow
  {
    #region Properties

    // Gets or sets the TestCommon object.
    private LJCTestCommon TestCommon { get; set; } = null!;
    #endregion

    #region Constructor Methods

    // Initializes an object instance.
    /// <include file='Doc/TestDBRow.xml'
    ///  path='members/Constructor/*'/>
    public TestDBRow()
    {
      TestCommon = new LJCTestCommon("LJCDBRow");
      Run();
    }

    // Runs the tests.
    private void Run()
    {
      // Constructor Test Methods
      ConstructorCopy();
      Clone();
    }
    #endregion

    #region Constructor Test Methods

    // The Copy constructor.
    private void ConstructorCopy()
    {
      var methodName = "ConstructorCopy()";

      var dataValues = new LJCDataColumns()
      {
        { "ColumnName", (object)"Value" }
      };
      var dbRow = new LJCDBRow()
      {
        Values = dataValues,
      };
      var copy = new LJCDBRow(dbRow);
      var value = copy[0];
      var result = "";
      result = value?.PropertyName;
      var compare = "ColumnName";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Creates and returns a clone of the object.
    private void Clone()
    {
      var methodName = "Clone()";

      var dataValues = new LJCDataColumns()
      {
        { "ColumnName", (object)"Value" }
      };
      var dbRow = new LJCDBRow()
      {
        Values = dataValues,
      };
      var clone = dbRow.Clone();
      var result = "";
      if (clone != null)
      {
        var value = clone[0];
        result = value?.PropertyName;
      }
      var compare = "ColumnName";
      TestCommon.Write($"{methodName}", result, compare);
    }
    #endregion
  }
}
