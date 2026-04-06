// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// CSParserProgram.cs
using LJCNetCommon5;
using System.ComponentModel;

namespace TestCSParser5
{
  // The entry class.
  internal class CSParserProgram
  {
    // The entry method.
    static void Main()
    {
      TestCommon = new LJCTestCommon("LJCCSParser");
      Console.WriteLine();
      Console.WriteLine("*** LJCCSParser ***");

      // Static Methods
      ScrubMethodName();

      // Methods
      ClassName();
      MethodName();
      PropertyName();
    }

    #region Static Methods

    // Gets the method name.
    private static void ScrubMethodName()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("ScrubMethodName()", result, compare);
    }
    #endregion

    #region Methods

    // Attempts to parse a class name.
    private static void ClassName()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("ClassName()", result, compare);
    }

    // Attempts to parse a method name.
    private static void MethodName()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("MethodName()", result, compare);
    }

    // Attempts to parse a property name.
    private static void PropertyName()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("PropertyName()", result, compare);
    }
    #endregion

    #region Properties

    // Gets or sets the TestCommon object.
    private static LJCTestCommon? TestCommon { get; set; }
    #endregion
  }
}
