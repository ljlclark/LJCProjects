// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// TestProviderFactory5.cs
using LJCNetCommon5;

namespace TestDataAccess5
{
  // Provides LJCProviderFactory test methods.
  internal class TestProviderFactory
  {
    #region Properties

    // Gets or sets the TestCommon object.
    private LJCTestCommon TestCommon { get; set; }
    #endregion

    #region Constructor Methods

    // Initializes an object instance.
    public TestProviderFactory()
    {
      TestCommon = new LJCTestCommon("TestProviderFactory");
      Run();
    }

    // Run the tests.
    private void Run()
    {
      // Constructor Methods
      ConstructorParam();

      // Method Test Methods
      CloseConnection();
      CreateCommand();
      CreateConnection();
      CreateDataAdapter();
      OpenConnection();
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

    // Closes the database connection.
    private void CloseConnection()
    {
      var methodName = "CloseConnection()";

      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write($"{methodName}", result, compare);
    }

    // Retrieves the DbCommand object.
    private void CreateCommand()
    {
      var methodName = "CreateCommand()";

      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write($"{methodName}", result, compare);
    }

    // Retrieves the DbConnection object.
    private void CreateConnection()
    {
      var methodName = "CreateConnection()";

      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write($"{methodName}", result, compare);
    }

    // Creates the DbDataAdapter object.
    private void CreateDataAdapter()
    {
      var methodName = "CreateDataAdapter()";

      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write($"{methodName}", result, compare);
    }

    // Opens the database connection.
    private void OpenConnection()
    {
      var methodName = "OpenConnection()";

      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write($"{methodName}", result, compare);
    }
    #endregion
  }
}
