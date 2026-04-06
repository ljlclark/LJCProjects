// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// AttributesProgram.cs
using LJCNetCommon5;

namespace TestAttributes5
{
  // The entry class.
  internal class AttributesProgram
  {
    // The entry method.
    static void Main()
    {
      TestCommon = new LJCTestCommon("LJCAttributes");
      Console.WriteLine();
      Console.WriteLine("*** LJCAttributes ***");

      // Collection Methods
      Add();
    }

    #region Collection Methods

    // Creates and adds an Attribute.
    private static void Add()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("Add()", result, compare);
    }
    #endregion

    #region Properties

    // Gets or sets the TestCommon object.
    private static LJCTestCommon? TestCommon { get; set; }
    #endregion
  }
}
