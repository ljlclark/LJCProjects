// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DbManagerProgram.cs
using LJCNetCommon5;

namespace TestDbManager5
{
  // The entry class.
  internal class DbManagerProgram
  {
    // The entry method.
    static void Main()
    {
      TestCommon = new LJCTestCommon("LJCDbManager");
      Console.WriteLine();
      Console.WriteLine("*** LJCDbManager ***");

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
