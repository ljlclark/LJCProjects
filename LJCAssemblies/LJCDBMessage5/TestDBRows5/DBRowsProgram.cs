// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DBRowsProgram.cs
using LJCNetCommon5;

namespace TestDBRows5
{
  internal class DBRowsProgram
  {
    // The entry method.
    static void Main()
    {
      TestCommon = new LJCTestCommon("LJCDBRows");
      Console.WriteLine();
      Console.WriteLine("*** LJCDBRows ***");

      // Static Methods
      LJCDeserialize();

      // Constructor Methods
      ConstructorCopy();

      // Collection Methods
      Add();
      Clone();
      HasItems();
      Serialize();

      Console.WriteLine();
      Console.WriteLine("Press any key to continue...");
      Console.ReadKey();
    }

    #region Static Methods

    // Deserializes from the specified XML file.
    private static void LJCDeserialize()
    {
      var methodName = "LJCDeserialize()";

      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write($"{methodName}", result, compare);
    }
    #endregion

    #region Constructor Methods

    // The Copy constructor.
    private static void ConstructorCopy()
    {
      var methodName = "ConstructorCopy()";

      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write($"{methodName}", result, compare);
    }
    #endregion

    #region Collection Methods

    // Adds the supplied object.
    private static void Add()
    {
      var methodName = "Add()";

      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write($"{methodName}", result, compare);
    }

    // Clones the structure of the object.
    private static void Clone()
    {
      var methodName = "Clone()";

      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write($"{methodName}", result, compare);
    }

    // Clones the structure of the object.
    private static void HasItems()
    {
      var methodName = "HasItems()";

      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write($"{methodName}", result, compare);
    }

    // Serialize the object to the specified file.
    private static void Serialize()
    {
      var methodName = "Serialize()";

      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write($"{methodName}", result, compare);
    }
    #endregion

    #region Properties

    // Gets or sets the TestCommon object.
    private static LJCTestCommon? TestCommon { get; set; }
    #endregion
  }
}
