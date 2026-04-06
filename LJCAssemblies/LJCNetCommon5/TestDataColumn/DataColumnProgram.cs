// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DataColumnProgram.cs
using LJCNetCommon5;

namespace TestDataColumn5
{
  // The entry class.
  internal class DataColumnProgram
  {
    // The entry method.
    static void Main()
    {
      TestCommon = new LJCTestCommon("LJCDataColumn");
      Console.WriteLine();
      Console.WriteLine("*** LJCDataColumn ***");

      // Constructor Methods
      Constructor();
      CopyConstructor();
      ParmConstructor();

      // Data Class Methods
      Add();
    }

    #region Constructor Methods

    // Initializes an object instance.
    private static void Constructor()
    {
      var dataColumn = new LJCDataColumn();
      var result = dataColumn.DataTypeName;
      var compare = "String";
      TestCommon?.CompareMessage("Constructor", result, compare);
    }

    // The Copy constructor.
    private static void CopyConstructor()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("CopyConstructor()", result, compare);
    }

    // Initializes an object instance with the supplied values.
    private static void ParmConstructor()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("ParmConstructor()", result, compare);
    }
    #endregion

    #region Data Class Methods

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
