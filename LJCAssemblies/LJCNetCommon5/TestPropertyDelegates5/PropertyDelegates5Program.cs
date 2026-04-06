// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// PropertyDelegates5Program.cs
using LJCNetCommon5;

namespace TestPropertyDelegates5
{
  // The entry class.
  internal class PropertyDelegates5Program
  {
    // The entry method.
    static void Main()
    {
      TestCommon = new LJCTestCommon("LJCPropertyDelegates");
      Console.WriteLine();
      Console.WriteLine("*** LJCPropertyDelegates ***");
    }

    #region Class Data

    private static LJCTestCommon? TestCommon { get; set; }
    #endregion
  }
}
