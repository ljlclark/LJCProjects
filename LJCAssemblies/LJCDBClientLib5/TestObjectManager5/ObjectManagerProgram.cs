// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ObjectManagerProgram.cs
using LJCNetCommon5;

namespace TestObjectManager5
{
  internal class ObjectManagerProgram
  {
    // The entry method.
    static void Main()
    {
      TestCommon = new LJCTestCommon("LJCObjectManager");
      Console.WriteLine();
      Console.WriteLine("*** LJCObjectManager ***");

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
