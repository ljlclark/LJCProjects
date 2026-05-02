// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// MySqlDataAccessProgram.cs

using LJCNetCommon5;

namespace TestMySqlDataAccess5
{
  // The entry class.
  internal class MySqlDataAccessProgram
  {
    // The entry method.
    static void Main()
    {
      TestCommon = new LJCTestCommon("LJCMySqlDataAccess");
      Console.WriteLine();
      Console.WriteLine("*** LJCMySqlDataAccess ***");

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

      Console.WriteLine();
      Console.WriteLine("Press any key to continue...");
      Console.ReadKey();
    }

    #region Constructor Methods

    private static void ConstructorParam()
    {
      var methodName = "ConstructorParam()";

      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write($"{methodName}", result, compare);
    }
    #endregion

    #region Methods

    private static void ExecuteNonQuery()
    {
      var methodName = "ExecuteNonQuery()";

      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write($"{methodName}", result, compare);
    }

    private static void FillDataTable()
    {
      var methodName = "FillDataTable()";

      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write($"{methodName}", result, compare);
    }

    private static void GetDataReader()
    {
      var methodName = "GetDataReader()";

      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write($"{methodName}", result, compare);
    }

    private static void GetDataSet()
    {
      var methodName = "GetDataSet()";

      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write($"{methodName}", result, compare);
    }

    private static void GetDataTable()
    {
      var methodName = "GetDataTable()";

      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write($"{methodName}", result, compare);
    }

    private static void GetDataTableFromReader()
    {
      var methodName = "GetDataTableFromReader()";

      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write($"{methodName}", result, compare);
    }

    private static void GetProcedureDataTable()
    {
      var methodName = "GetProcedureDataTable()";

      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write($"{methodName}", result, compare);
    }

    private static void GetSchemaOnly()
    {
      var methodName = "GetSchemaOnly()";

      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write($"{methodName}", result, compare);
    }
    #endregion

    #region Properties

    // Gets or sets the TestCommon object.
    private static LJCTestCommon? TestCommon { get; set; }
    #endregion
  }
}
