// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ProviderFactoryProgram.cs
using LJCNetCommon5;

namespace TestProviderFactory5
{
  // The entry class.
  internal class ProviderFactoryProgram
  {
    // The entry method.
    static void Main()
    {
      TestCommon = new LJCTestCommon("LJCProviderFactory");
      Console.WriteLine();
      Console.WriteLine("*** LJCProviderFactory ***");

      // Constructor Methods
      ConstructorParam();

      // Methods
      CloseConnection();
      CreateCommand();
      CreateConnection();
      CreateDataAdapter();
      OpenConnection();

      Console.WriteLine();
      Console.WriteLine("Press any key to continue...");
      Console.ReadKey();
    }

    #region Constructor Methods

    // Initializes an object instance with the supplied values.
    private static void ConstructorParam()
    {
      var methodName = "ConstructorParam()";

      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write($"{methodName}", result, compare);
    }
    #endregion

    #region Methods

    // 
    private static void CloseConnection()
    {
      var methodName = "CloseConnection()";

      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write($"{methodName}", result, compare);
    }

    // 
    private static void CreateCommand()
    {
      var methodName = "CreateCommand()";

      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write($"{methodName}", result, compare);
    }

    // 
    private static void CreateConnection()
    {
      var methodName = "CreateConnection()";

      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write($"{methodName}", result, compare);
    }

    // 
    private static void CreateDataAdapter()
    {
      var methodName = "CreateDataAdapter()";

      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write($"{methodName}", result, compare);
    }

    // 
    private static void OpenConnection()
    {
      var methodName = "OpenConnection()";

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
