// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DbServiceRefProgram.cs
using LJCNetCommon5;

namespace TestDbServiceRef5
{
  // The entry class.
  internal class DbServiceRefProgram
  {
    // The entry method.
    static void Main()
    {
      TestCommon = new LJCTestCommon("LJCDbServiceRef");
      Console.WriteLine();
      Console.WriteLine("*** LJCDbServiceRef ***");

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
