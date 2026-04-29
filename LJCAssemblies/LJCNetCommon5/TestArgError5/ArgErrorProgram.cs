// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ArgErrorProgram.cs
using LJCNetCommon5;

namespace TestArgError5
{
  // The entry class.
  internal class ArgErrorProgram
  {
    // The entry method.
    static void Main()
    {
      TestCommon = new LJCTestCommon("LJCArgError");
      Console.WriteLine();
      Console.WriteLine("*** LJCArgError ***");

      // Data Class Methods
      ToStringMethod();

      // Methods
      Add1();
      Add2();

      Console.WriteLine();
      Console.WriteLine("Press any key to continue...");
      Console.ReadKey();
    }

    #region Data Class Methods

    private static void ToStringMethod()
    {
      var argError = new LJCArgError("ArgErrorProgram")
      {
        MethodName = "ToStringMethod()"
      };

      // No arg errors.
      // Test Method
      var result = argError.ToString();
      var compare = "No Result";
      TestCommon?.Write("ToStringMethod()1", result, compare);

      // Missing object argument.
      argError.Clear();
      LJCDataColumns argObject = null;
      argError.Add(argObject, nameof(argObject));
      // Test Method
      result = argError.ToString();
      compare = "ArgErrorProgram\r\n";
      compare += "ToStringMethod()\r\n";
      compare += "argObject is missing.\r\n";
      TestCommon?.Write("ToStringMethod()2", result, compare);

      argError.Clear();
      var text = " ";
      argError.Add(text, nameof(text));
      // Test Method
      result = argError.ToString();
      compare = "ArgErrorProgram\r\n";
      compare += "ToStringMethod()\r\n";
      compare += "text is missing.\r\n";
      TestCommon?.Write("ToStringMethod()3", result, compare);
    }
    #endregion

    #region Methods

    // Adds a message using the provided values.
    private static void Add1()
    {
      var argError = new LJCArgError("ArgErrorProgram");
      argError.MethodName = "Add1()";

      // Missing object argument.
      LJCDataColumn argObject = null;
      // Test Method
      argError.Add(argObject, nameof(argObject));
      var result = argError.ToString();
      var compare = "ArgErrorProgram\r\n";
      compare += "Add1()\r\n";
      compare += "argObject is missing.\r\n";
      TestCommon?.Write("Add1()1", result, compare);

      // Missing argObject and text arguments.
      var text = "  ";
      argError.Add(text, nameof(text));
      result = argError.ToString();
      compare = "ArgErrorProgram\r\n";
      compare += "Add1()\r\n";
      compare += "argObject is missing.\r\n";
      compare += "text is missing.\r\n";
      TestCommon?.Write("Add1()2", result, compare);
    }

    // Adds a message.
    private static void Add2()
    {
      var argError = new LJCArgError("ArgErrorProgram");
      argError.MethodName = "Add2()";

      var message = "valueArg is greater than 5.";
      // Test Method
      argError.Add(message);
      var result = argError.ToString();
      var compare = "ArgErrorProgram\r\n";
      compare += "Add2()\r\n";
      compare += "valueArg is greater than 5.";
      TestCommon?.Write("Add1()", result, compare);
    }
    #endregion

    #region Properties

    // Gets or sets the TestCommon object.
    private static LJCTestCommon? TestCommon { get; set; }
    #endregion
  }
}
