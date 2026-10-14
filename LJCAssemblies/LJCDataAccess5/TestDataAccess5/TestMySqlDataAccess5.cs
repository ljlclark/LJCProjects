// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// TestMySqlDataAccess5.cs
using LJCNetCommon5;

namespace TestDataAccess5
{
  internal class TestMySqlDataAccess
  {
    #region Properties

    // Gets or sets the TestCommon object.
    private LJCTestCommon TestCommon { get; set; }
    #endregion

    #region Constructor Methods

    // Initializes an object instance.
    /// <include file='Doc/TestMySqlDataAccess.xml'
    ///  path='members/Constructor/*'/>
    public TestMySqlDataAccess()
    {
      TestCommon = new LJCTestCommon("TestMySqlDataAccess");
      Run();
    }

    // Run the tests.
    private void Run()
    {
      // Constructor Methods
      ConstructorParam();

      // Methods
      ExecuteNonQuery();
      FillDataTable();
      GetDataReader();
      GetDataSet();
      GetDataTable();
      GetDataTableFromReader();
      GetProcedureDataTable();
      GetSchemaOnly();
    }
    #endregion

    #region Constructor Methods

    private void ConstructorParam()
    {
      var methodName = "ConstructorParam()";

      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write($"{methodName}", result, compare);
    }
    #endregion

    #region Methods

    private void ExecuteNonQuery()
    {
      var methodName = "ExecuteNonQuery()";

      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write($"{methodName}", result, compare);
    }

    private void FillDataTable()
    {
      var methodName = "FillDataTable()";

      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write($"{methodName}", result, compare);
    }

    private void GetDataReader()
    {
      var methodName = "GetDataReader()";

      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write($"{methodName}", result, compare);
    }

    private void GetDataSet()
    {
      var methodName = "GetDataSet()";

      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write($"{methodName}", result, compare);
    }

    private void GetDataTable()
    {
      var methodName = "GetDataTable()";

      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write($"{methodName}", result, compare);
    }

    private void GetDataTableFromReader()
    {
      var methodName = "GetDataTableFromReader()";

      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write($"{methodName}", result, compare);
    }

    private void GetProcedureDataTable()
    {
      var methodName = "GetProcedureDataTable()";

      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write($"{methodName}", result, compare);
    }

    private void GetSchemaOnly()
    {
      var methodName = "GetSchemaOnly()";

      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write($"{methodName}", result, compare);
    }
    #endregion
  }
}
