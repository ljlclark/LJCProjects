// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DBFiltersProgram.cs
using LJCNetCommon5;

namespace TestDBFilters5
{
  internal class DBFiltersProgram
  {
    // The entry method.
    static void Main()
    {
      TestCommon = new LJCTestCommon("LJCDBFilters");
      Console.WriteLine();
      Console.WriteLine("*** LJCDBFilters ***");

      // Static Methods
      //SQLSoundexFilters();
      //SoundeFilters();

      // Constructor Methods
      ConstructorCopy();

      // Collection Methods
      Add1();
      Add2();
      Clone();

      Console.WriteLine();
      Console.WriteLine("Press any key to continue...");
      Console.ReadKey();
    }

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

    // Creates and adds the element from the supplied values.
    private static void Add1()
    {
      var methodName = "Add1()";

      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write($"{methodName}", result, compare);
    }

    // Creates and adds the element from the supplied values.
    private static void Add2()
    {
      var methodName = "Add2()";

      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write($"{methodName}", result, compare);
    }

    // Creates and returns a clone of the object.
    private static void Clone()
    {
      var methodName = "Clone()";

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
