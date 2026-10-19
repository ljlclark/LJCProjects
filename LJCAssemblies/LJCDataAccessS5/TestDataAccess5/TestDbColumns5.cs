// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// TestDbColumns5.cs
using LJCDataAccess5;
using LJCNetCommon5;
using System.Data;

namespace TestDataAccess5
{
  internal class TestDbColumns
  {
    #region Properties

    // Gets or sets the TestCommon object.
    private LJCTestCommon TestCommon { get; set; }
    #endregion

    #region Constructor Methods

    // Initializes an object instance.
    /// <include file='Doc/TestDataKey.xml'
    ///  path='members/Constructor/*'/>
    public TestDbColumns()
    {
      TestCommon = new LJCTestCommon("TestDbColumns");
      Run();
    }

    // Run the tests.
    private void Run()
    {
      // Static Methods
      Clone();
      ColumnNames();
      Columns();
      CreateColumns();
      HasColumns();
      ToDataColumns();
    }
    #endregion

    #region Static Methods

    // Clones a DataColumnCollection.
    private void Clone()
    {
      var methodName = "Clone()";

      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write($"{methodName}", result, compare);
    }

    // Creates a Column Names list from a DataColumnCollection.
    private void ColumnNames()
    {
      var methodName = "ColumnNames()";

      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write($"{methodName}", result, compare);
    }

    // Returns a set of DataColumns that match the supplied list.
    private void Columns()
    {
      var methodName = "Columns()";

      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write($"{methodName}", result, compare);
    }

    // Creates a new DataColumns object.
    private void CreateColumns()
    {
      var methodName = "CreateColumns()";

      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write($"{methodName}", result, compare);
    }

    // Checks the DataColumnCollection object for items.
    private void HasColumns()
    {
      var methodName = "HasColumns()";

      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write($"{methodName}", result, compare);
    }

    // Creates an LJCDataColumns collection from a DataColumnCollection.
    private void ToDataColumns()
    {
      var methodName = "ToDataColumns()";

      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write($"{methodName}", result, compare);
    }
    #endregion
  }
}
