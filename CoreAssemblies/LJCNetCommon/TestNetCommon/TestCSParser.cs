// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// TestCSParser.cs
using LJCNetCommon;
using System;

namespace TestNetCommon
{
  // Provides the LJCCSParser test methods.
  internal class TestCSParser
  {
    #region Class Data

    // The test common object.
    private static LJCTestCommon TestCommon { get; set; }
    #endregion

    #region Constructor Methods

    // Initializes an object instance.
    public TestCSParser()
    {
      TestCommon = new LJCTestCommon("TestCSParser");
      Console.WriteLine();
      Console.WriteLine("*********************");
      Console.Write("*** LJCCSParser ***");
      Run();
    }

    // Runs the test methods.
    private void Run()
    {
      // Methods
      ParseClass();
      ParseMethod();
      ParseProperty();
    }
    #endregion

    #region Methods

    // Test class parsing.
    private static void ParseClass()
    {
      ParseClassLine("  class ClassName");
      ParseClassLine("  public class ClassName");
      ParseClassLine("  public abstract class ClassName");
    }

    // Test class line parsing.
    private static void ParseClassLine(string line)
    {
      var codeParser = new LJCCSParser();
      var result = codeParser.ClassName(line);
      var compare = "ClassName";
      if (null == result)
      {
        compare = "No Result";
      }
      TestCommon.Write("ParseClassLine", result, compare);
    }

    // Test method parsing.
    private static void ParseMethod()
    {
      ParseMethodLine("  string Method()");
      ParseMethodLine("  public string Method()");
      ParseMethodLine("  public static string Method()");
    }

    // Test method line parsing.
    private static void ParseMethodLine(string line)
    {
      var codeParser = new LJCCSParser();
      var result = codeParser.MethodName(line);
      var compare = "Method";
      if (null == result)
      {
        compare = "No Result";
      }
      TestCommon.Write("ParseMethodLine", result, compare);
    }

    // Test property parsing.
    private static void ParseProperty()
    {
      ParsePropertyLine("  type Property {", null);
      ParsePropertyLine("  type Property", "{");
      ParsePropertyLine("  #region Methods", null);
      ParsePropertyLine("  #region Constructor Methods", null);
      ParsePropertyLine("  type Method()", null);
      ParsePropertyLine("  type VarName;", null);
      ParsePropertyLine("  VarName =", null);

      ParsePropertyLine("  public type Property", null);
      ParsePropertyLine("  type VarName =", null);
      ParsePropertyLine("  public type Method()", null);
      ParsePropertyLine("  public type VarName;", null);
      ParsePropertyLine("  public type VarName =", null);
    }

    // Test property line parsing.
    private static void ParsePropertyLine(string line, string nextLine)
    {
      var codeParser = new LJCCSParser();
      var result = codeParser.PropertyName(line, nextLine);
      var compare = "Property";
      if (null == result)
      {
        compare = "No Result";
      }
      TestCommon.Write("ParsePropertyLine", result, compare);
    }
    #endregion
  }
}
