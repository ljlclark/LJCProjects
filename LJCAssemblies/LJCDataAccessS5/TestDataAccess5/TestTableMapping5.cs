// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// TestTableMapping5.cs
using LJCNetCommon5;

namespace TestDataAccess5
{
  internal class TestTableMapping
  {
    #region Properties

    // Gets or sets the TestCommon object.
    private LJCTestCommon TestCommon { get; set; }
    #endregion

    #region Constructor Methods

    // Initializes an object instance.
    /// <include file='Doc/TestDataKey.xml'
    ///  path='members/Constructor/*'/>
    public TestTableMapping()
    {
      TestCommon = new LJCTestCommon("TestTableMapping");
      Run();
    }

    // Run the tests.
    private void Run()
    {
      // Methods
      AddColumnMap();
      AddTableMap();
    }
    #endregion

    #region Methods

    // Adds a DataTable column map to the table mapping.
    private void AddColumnMap()
    {
      var methodName = "AddColumnMap()";

      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write($"{methodName}", result, compare);
    }

    // Adds a DataTable map to the TableMaps collection.
    private void AddTableMap()
    {
      var methodName = "AddTableMap()";

      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write($"{methodName}", result, compare);
    }
    #endregion
  }
}
