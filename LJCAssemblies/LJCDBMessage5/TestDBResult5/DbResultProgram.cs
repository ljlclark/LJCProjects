// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DBResultProgram.cs
using LJCNetCommon5;

namespace TestDBResult5
{
  // The entry class.
  internal class DbResultProgram
  {
    // The entry method.
    static void Main()
    {
      TestCommon = new LJCTestCommon("LJCDBResult");
      Console.WriteLine();
      Console.WriteLine("*** LJCDBResult ***");

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
