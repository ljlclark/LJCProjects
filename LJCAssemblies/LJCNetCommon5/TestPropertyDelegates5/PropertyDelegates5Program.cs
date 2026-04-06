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

      // Collection Methods
      Add();

      LJCSearchName();
      LJCCreateDelegate();
    }

    #region Collection Methods

    // Creates and adds a PropertyDelegate object to the collection.
    private static void Add()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("Add()", result, compare);
    }
    #endregion

    // Returns the PropertyDelegate object if found in the list.
    private static void LJCSearchName()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("LJCSearchName", result, compare);
    }

    // Creates and returns the delegate for the named property.
    private static void LJCCreateDelegate()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("LJCCreateDelegate", result, compare);
    }

    #region Properties

    // Gets or sets the TestCommon object.
    private static LJCTestCommon? TestCommon { get; set; }
    #endregion
  }
}
