// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ManagerCommonProgram.cs
using LJCNetCommon5;

namespace TestManagerCommon5
{
  internal class ManagerCommonProgram
  {
    // The entry method.
    static void Main()
    {
      TestCommon = new LJCTestCommon("LJCManagerCommon");
      Console.WriteLine();
      Console.WriteLine("*** LJCManagerCommon ***");

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
