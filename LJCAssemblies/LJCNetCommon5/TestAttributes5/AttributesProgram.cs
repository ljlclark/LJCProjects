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

      Console.WriteLine();
      Console.WriteLine("Press any key to continue...");
      Console.ReadKey();
    }

    #region Collection Methods

    // Creates and adds an Attribute.
    private static void Add()
    {
      var attribs = new LJCAttributes();
      var name = "id";
      // Test Method
      attribs.Add(name, "idValue");
      var attrib = attribs[0];
      var result = attrib.Name;
      var compare = "id";
      TestCommon?.Write("Add()", result, compare);
    }
    #endregion

    #region Properties

    // Gets or sets the TestCommon object.
    private static LJCTestCommon? TestCommon { get; set; }
    #endregion
  }
}
