// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// TestMySqlDataAccess5.cs
using LJCNetCommon5;

namespace TestDataAccess5
{
  // Provides LJCMySqlDataAccess test methods.
  internal class TestMySqlDataAccess
  {
    #region Properties

    // Gets or sets the TestCommon object.
    private LJCTestCommon TestCommon { get; set; }
    #endregion

    #region Constructor Methods

    // Initializes an object instance.
    public TestMySqlDataAccess()
    {
      TestCommon = new LJCTestCommon("TestMySqlDataAccess");
      Run();
    }

    // Run the tests.
    private void Run()
    {
      // Constructor  Test Methods
      ConstructorParam();

      // Method Test Methods
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

    #region Constructor Test Methods

    // Initializes an object instance with the supplied values.
    private void ConstructorParam()
    {
      var methodName = "ConstructorParam()";

      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write($"{methodName}", result, compare);
    }
    #endregion

    #region Method Test Methods

    // Executes an Insert, Update or Delete statement.
    private void ExecuteNonQuery()
    {
      var methodName = "ExecuteNonQuery()";

      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write($"{methodName}", result, compare);
    }

    // Executes a Select statement and fills the specified DataTable.
    private void FillDataTable()
    {
      var methodName = "FillDataTable()";

      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write($"{methodName}", result, compare);
    }

    // Executes a Select statement and retrieves the MySqlDataReader object.
    private void GetDataReader()
    {
      var methodName = "GetDataReader()";

      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write($"{methodName}", result, compare);
    }

    // Executes a Select statement and retrieves the DataSet object.
    private void GetDataSet()
    {
      var methodName = "GetDataSet()";

      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write($"{methodName}", result, compare);
    }

    // Executes a Select statement and retrieves the DataTable object.
    private void GetDataTable()
    {
      var methodName = "GetDataTable()";

      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write($"{methodName}", result, compare);
    }

    // Executes a Select statement and retrieves the DataTable object.
    private void GetDataTableFromReader()
    {
      var methodName = "GetDataTableFromReader()";

      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write($"{methodName}", result, compare);
    }

    // Executes a Stored Procedure and retrieves the DataTable object.
    private void GetProcedureDataTable()
    {
      var methodName = "GetProcedureDataTable()";

      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write($"{methodName}", result, compare);
    }

    // Retrieves the DataTable object with schema only.
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
