// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ResultConverterProgram.cs
using LJCNetCommon5;

namespace TestResultConverter5
{
  internal class ResultConverterProgram
  {
    // The entry method.
    static void Main()
    {
      TestCommon = new LJCTestCommon("LJCResultConverter");
      Console.WriteLine();
      Console.WriteLine("*** LJCResultConverter ***");

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
