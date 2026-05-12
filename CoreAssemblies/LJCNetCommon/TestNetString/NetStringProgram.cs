// Copyright (c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// NetStringProgram.cs
using LJCNetCommon;
using System;

namespace TestNetString
{
  internal class NetStringProgram
  {
    #region Entry Methods

    /// <summary>
    /// 
    /// </summary>
    static void Main()
    {
      TestCommon = new TestCommon("NetStringProgram");
      Console.WriteLine();
      Console.WriteLine("*** NetString ***");

      // Parsing Delimited String

      // Begin delimiter only.
      // <comment>
      DelimitedString1();

      // Begin delimiter and text.
      // <comment>This is a comment.
      DelimitedString2();

      // Text only.
      // This is a comment.
      DelimitedString3();

      // Text and end delimiter.
      // This is a comment.</comment>
      DelimitedString4();

      // End delimiter only.
      // </comment>
      DelimitedString5();

      // Multiple delimited values.
      // Value (One), (Two)
      DelimitedString6();

      // Delimited string.
      DelimitedString7();

      // A delimited string with delimiters.
      StringWithDelimiters();
    }
    #endregion

    #region Methods

    // Begin delimiter only.
    /// <summary>
    /// 
    /// </summary>
    public static void DelimitedString1()
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
    /// <summary>
    /// 
    /// </summary>
    public static void DelimitedString2()
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
    /// <summary>
    /// 
    /// </summary>
    public static void DelimitedString3()
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
    /// <summary>
    /// 
    /// </summary>
    public static void DelimitedString4()
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
    /// <summary>
    /// 
    /// </summary>
    public static void DelimitedString5()
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
    /// <summary>
    /// 
    /// </summary>
    public static void DelimitedString6()
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
    /// <summary>
    /// 
    /// </summary>
    public static void DelimitedString7()
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
    /// <summary>
    /// 
    /// </summary>
    public static void StringWithDelimiters()
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

    #region Class Data

    // The test common object.
    private static TestCommon TestCommon { get; set; }
    #endregion
  }
}
