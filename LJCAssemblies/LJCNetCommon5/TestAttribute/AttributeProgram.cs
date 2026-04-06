// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// AttributeProgram.cs
using LJCNetCommon5;

namespace TestAttribute5
{
  // The entry class.
  internal class AttributeProgram
  {
    // The entry method.
    static void Main()
    {
      TestCommon = new LJCTestCommon("LJCAttribute");
      Console.WriteLine();
      Console.WriteLine("*** LJCAttribute ***");

      // Constructor Methods
      Constructor();
    }

    #region Constructor Methods

    // Initializes an object instance.
    private static void Constructor()
    {
      var attrib = new LJCAttribute("Name", "Value");
      var result = attrib.Name;
      var compare = "Name";
      TestCommon?.Write("Constructor1", result, compare);

      result = attrib.Value;
      compare = "Value";
      TestCommon?.Write("Constructor2", result, compare);
    }
    #endregion

    #region Properties

    // Gets or sets the TestCommon object.
    private static LJCTestCommon? TestCommon { get; set; }
    #endregion
  }
}
