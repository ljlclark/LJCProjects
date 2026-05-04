// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DBCommonProgram.cs
using LJCNetCommon5;

namespace TestDBCommon5
{
  // The entry class.
  internal class DBCommonProgram
  {
    // The entry method.
    static void Main(string[] args)
    {
      TestCommon = new LJCTestCommon("LJCDBCommon");
      Console.WriteLine();
      Console.WriteLine("*** LJCDBCommon ***");

      // Test Methods
      Test();

      Console.WriteLine();
      Console.WriteLine("Press any key to continue...");
      Console.ReadKey();
    }

    #region Test Methods

    private static void Test()
    {
      var methodName = "Test()";

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
