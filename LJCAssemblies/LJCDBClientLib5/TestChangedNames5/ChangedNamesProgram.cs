// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ChangedNamesProgram.cs
using LJCNetCommon5;

namespace TestChangedNames5
{
  // The entry class.
  internal class ChangedNamesProgram
  {
    // The entry method.
    static void Main()
    {
      TestCommon = new LJCTestCommon("LJCChangedNames");
      Console.WriteLine();
      Console.WriteLine("*** LJCChangedNames ***");

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
