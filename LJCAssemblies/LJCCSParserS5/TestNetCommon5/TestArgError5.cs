// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// TestArgError5.cs
using LJCNetCommon5;

namespace TestNetCommon5
{
  // Provides LJCArgError test methods.
  internal class TestArgError
  {
    #region Properties

    // Gets or sets the TestCommon object.
    private LJCTestCommon TestCommon { get; set; } = null!;
    #endregion

    #region Constructor Methods

    // Initializes an object instance.
    public TestArgError()
    {
      TestCommon = new LJCTestCommon("LJCArgError");
      Run();
    }

    // Runs the tests.
    private void Run()
    {
      // Data Class Test Methods
      ToStringMethod();

      // Method Test Methods
      Add1();
      Add2();
    }
    #endregion

    #region Data Class Test Methods

    private void ToStringMethod()
    {
      var argError = new LJCArgError("ArgErrorProgram")
      {
        MethodName = "ToStringMethod()"
      };

      // No arg errors.
      // Test Method
      var result = argError.ToString();
      var compare = "No Result";
      TestCommon.Write("ToStringMethod()1", result, compare);

      // Missing object argument.
      argError.Clear();
      LJCDataColumns argObject = null;
      argError.Add(argObject, nameof(argObject));
      // Test Method
      result = argError.ToString();
      compare = "ArgErrorProgram\r\n";
      compare += "ToStringMethod()\r\n";
      compare += "argObject is missing.\r\n";
      TestCommon.Write("ToStringMethod()2", result, compare);

      argError.Clear();
      var text = " ";
      argError.Add(text, nameof(text));
      // Test Method
      result = argError.ToString();
      compare = "ArgErrorProgram\r\n";
      compare += "ToStringMethod()\r\n";
      compare += "text is missing.\r\n";
      TestCommon.Write("ToStringMethod()3", result, compare);
    }
    #endregion

    #region Method Test Methods

    // Adds a message using the provided values.
    private void Add1()
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
      TestCommon.Write("Add1()1", result, compare);

      // Missing argObject and text arguments.
      var text = "  ";
      argError.Add(text, nameof(text));
      result = argError.ToString();
      compare = "ArgErrorProgram\r\n";
      compare += "Add1()\r\n";
      compare += "argObject is missing.\r\n";
      compare += "text is missing.\r\n";
      TestCommon.Write("Add1()2", result, compare);
    }

    // Adds a message.
    private void Add2()
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
      TestCommon.Write("Add1()", result, compare);
    }
    #endregion
  }
}
