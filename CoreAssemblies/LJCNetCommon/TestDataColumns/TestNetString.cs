// Copyright (c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// TestNetString.cs
using LJCNetCommon;
using System;

namespace TestData
{
  internal class TestNetString
  {
    #region Class Data

    private static LJCTestCommon TestCommon { get; set; }
    #endregion

    #region Constructor Methods

    // Initializes an object instance.
    public TestNetString()
    {
      TestCommon = new LJCTestCommon("TestNetString");
      Console.WriteLine();
      Console.WriteLine("**********************");
      Console.Write("*** LJCNetString ***");
      Run();
    }

    // Runs the test methods.
    private void Run()
    {
      // Parsing Delimited String
      DelimitedString1();
      DelimitedString2();
      DelimitedString3();
      DelimitedString4();
      DelimitedString5();
      //DelimitedString6();
      DelimitedString7();
      StringWithDelimiters();
    }
    #endregion

    #region Methods

    // Begin delimiter only.
    // <comment>
    private static void DelimitedString1()
    {
      var line = "<comment>";
      var beginDelimiter = "<comment>";

      var parser = new LJCParser();
      var text = parser.DelimitedString(line, beginDelimiter);
      var result = $"{text}, {parser.BeginIndex}, {parser.EndIndex}";
      result += $", {parser.StartIndex}";
      var compare = ", 9, 9, -1";
      TestCommon.Write("DelimitedString1()", result, compare);
    }

    // Begin delimiter and text.
    // <comment>This is a comment.
    private static void DelimitedString2()
    {
      var line = "<comment>This is a comment.";
      var beginDelimiter = "<comment>";

      var parser = new LJCParser();
      var text = parser.DelimitedString(line, beginDelimiter);
      var result = $"{text}, {parser.BeginIndex}, {parser.EndIndex}";
      result += $", {parser.StartIndex}";
      var compare = "This is a comment., 9, 27, -1";
      TestCommon.Write("DelimitedString2()", result, compare);
    }

    // Text only.
    // This is a comment.
    private static void DelimitedString3()
    {
      var line = "This is a comment.";

      var parser = new LJCParser();
      var text = parser.DelimitedString(line);
      var result = $"{text}, {parser.BeginIndex}, {parser.EndIndex}";
      result += $", {parser.StartIndex}";
      var compare = "This is a comment., 0, 18, -1";
      TestCommon.Write("DelimitedString3()", result, compare);
    }

    // Text and end delimiter.
    // This is a comment.</comment>
    private static void DelimitedString4()
    {
      var line = "This is a comment.</comment>";
      var endDelimiter = "</comment>";

      var parser = new LJCParser();
      var text = parser.DelimitedString(line, null, endDelimiter);
      var result = $"{text}, {parser.BeginIndex}, {parser.EndIndex}";
      result += $", {parser.StartIndex}";
      var compare = "This is a comment., 0, 18, -1";
      TestCommon.Write("DelimitedString4()", result, compare);
    }

    // End delimiter only.
    // </comment>
    private static void DelimitedString5()
    {
      var line = "</comment>";
      var endDelimiter = "</comment>";

      var parser = new LJCParser();
      var text = parser.DelimitedString(line, null, endDelimiter);
      var result = $"{text}, {parser.BeginIndex}, {parser.EndIndex}";
      result += $", {parser.StartIndex}";
      var compare = ", 0, 0, -1";
      TestCommon.Write("DelimitedString5()", result, compare);
    }

    // Multiple delimited values.
    // Value (One), (Two)
    private static void DelimitedString6()
    {
      var line = "Value (One), (Two)";
      var beginDelimiter = "(";
      var endDelimiter = ")";

      var parser = new LJCParser();
      var first = true;
      while (parser.StartIndex >= 0)
      {
        var text = parser.DelimitedString(line, beginDelimiter, endDelimiter);
        var result = $"{text}, {parser.BeginIndex}, {parser.EndIndex}";
        result += $", {parser.StartIndex}";
        string compare;
        if (first)
        {
          compare = "One, 7, 10, 11";
        }
        else
        {
          compare = "Two, 14, 17, -1";
        }
        first = false;
        TestCommon.Write("DelimitedString6()", result, compare);
      }
    }

    // A delimited string.
    private static void DelimitedString7()
    {
      var line = "Value (One)";
      var beginDelimiter = "(";
      var endDelimiter = ")";

      var parser = new LJCParser();
      var text = parser.DelimitedString(line, beginDelimiter, endDelimiter);
      var result = $"{text}, {parser.StartIndex}";
      var compare = "One, -1";
      TestCommon.Write("DelimitedString7()", result, compare);
    }

    // A delimited string with delimiters.
    private static void StringWithDelimiters()
    {
      var line = "Value (One)";
      var beginDelimiter = "(";
      var endDelimiter = ")";

      var parser = new LJCParser();
      var text = parser.StringWithDelimiters(line, beginDelimiter, endDelimiter);
      var result = $"{text}, {parser.StartIndex}";
      var compare = "(One), -1";
      TestCommon.Write("GetStringWithDelimiters()", result, compare);
    }
    #endregion
  }
}
