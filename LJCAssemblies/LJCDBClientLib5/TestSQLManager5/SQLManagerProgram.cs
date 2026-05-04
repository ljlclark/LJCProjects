// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// SQLManagerProgram.cs
using LJCNetCommon5;

namespace TestSQLManager5
{
  internal class SQLManagerProgram
  {
    // The entry method.
    static void Main()
    {
      TestCommon = new LJCTestCommon("LJCSQLManager");
      Console.WriteLine();
      Console.WriteLine("*** LJCSQLManager ***");

      Console.WriteLine();
      Console.WriteLine("Press any key to continue...");
      Console.ReadKey();
    }

    #region Properties

    // Gets or sets the TestCommon object.
    private static LJCTestCommon? TestCommon { get; set; }
    #endregion
  }
}
