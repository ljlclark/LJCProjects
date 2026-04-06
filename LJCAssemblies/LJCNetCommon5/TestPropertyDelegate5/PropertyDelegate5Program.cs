// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// PropertyDelegate5Program.cs
using LJCNetCommon5;

namespace TestPropertyDelegate5
{
  // The entry class.
  internal class PropertyDelegate5Program
  {
    // The entry method.
    static void Main()
    {
      TestCommon = new LJCTestCommon("LJCPropertyDelegate");
      Console.WriteLine();
      Console.WriteLine("*** LJCPropertyDelegate ***");
    }

    #region Class Data

    private static LJCTestCommon? TestCommon { get; set; }
    #endregion
  }
}
