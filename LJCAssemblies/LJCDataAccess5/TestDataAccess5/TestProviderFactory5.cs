// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// TestProviderFactory5.cs
using LJCNetCommon5;

namespace TestDataAccess5
{
  internal class TestProviderFactory
  {
    #region Properties

    // Gets or sets the TestCommon object.
    private LJCTestCommon TestCommon { get; set; }
    #endregion

    #region Constructor Methods

    // Initializes an object instance.
    /// <include file='Doc/TestDataKey.xml'
    ///  path='members/Constructor/*'/>
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

      // Methods
      CloseConnection();
      CreateCommand();
      CreateConnection();
      CreateDataAdapter();
      OpenConnection();
    }
    #endregion
    #region Constructor Methods

    // Initializes an object instance with the supplied values.
    private void ConstructorParam()
    {
      var methodName = "ConstructorParam()";

      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write($"{methodName}", result, compare);
    }
    #endregion

    #region Methods

    // 
    private void CloseConnection()
    {
      var methodName = "CloseConnection()";

      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write($"{methodName}", result, compare);
    }

    // 
    private void CreateCommand()
    {
      var methodName = "CreateCommand()";

      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write($"{methodName}", result, compare);
    }

    // 
    private void CreateConnection()
    {
      var methodName = "CreateConnection()";

      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write($"{methodName}", result, compare);
    }

    // 
    private void CreateDataAdapter()
    {
      var methodName = "CreateDataAdapter()";

      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write($"{methodName}", result, compare);
    }

    // 
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
