// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// KeyItem5Program.cs
using LJCNetCommon5;

namespace TestKeyItem5
{
  // The entry class.
  internal class KeyItem5Program
  {
    // The entry method.
    static void Main()
    {
      TestCommon = new LJCTestCommon("LJCKeyItem");
      Console.WriteLine();
      Console.WriteLine("*** LJCKeyItem ***");

      // Constructor Methods
      ParmConstructor();

      // Data Methods
      Clone();
      ToStringMethod();

      // Search and Sort Methods
      CompareTo();

      Console.WriteLine();
      Console.WriteLine("Press any key to continue...");
      Console.ReadKey();
    }

    #region Constructor Methods

    // Initializes an object instance.
    private static void ParmConstructor()
    {
      // Test Method
      var keyItem = new LJCKeyItem("ID", 1, "The primary key.");

      var result = keyItem.PropertyName;
      var compare = "ID";
      TestCommon?.Write("ParmConstructor()", result, compare);
    }
    #endregion

    #region Data Methods

    // Creates and returns a clone of this object.
    private static void Clone()
    {
      var keyItem = new LJCKeyItem("ID", 1, "The primary key.");

      // Test Method


      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("Clone()", result, compare);
    }

    // The object string identifier.
    private static void ToStringMethod()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("ToStringMethod()", result, compare);
    }
    #endregion

    #region Search and Sort Methods

    // Provides the default Sort functionality.
    private static void CompareTo()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("CompareTo()", result, compare);
    }
    #endregion

    #region Properties

    // Gets or sets the TestCommon object.
    private static LJCTestCommon? TestCommon { get; set; }
    #endregion
  }
}
