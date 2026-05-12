// Copyright (c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// CodeParseProgram.cs
using LJCNetCommon;
using System;

namespace TestCSParser
{
  // Provides methods to test class LJCCSParser.
  internal class CSParserProgram
  {
    #region Entry Methods

    // The entry method.
    private static void Main()
    {
      TestCommon = new TestCommon("CSParserProgram");
      Console.WriteLine();
      Console.WriteLine("*** CSParser ***");

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

    #region Class Data

    // The test common object.
    private static TestCommon TestCommon { get; set; }
    #endregion
  }
}
