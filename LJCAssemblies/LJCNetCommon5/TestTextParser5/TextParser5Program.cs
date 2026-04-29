// Copyright (c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// TestTextParser5Program.cs
using LJCNetCommon5;

namespace TestTextParser5
{
  // The entry class.
  internal class TextParser5Program
  {
    // The entry method.
    static void Main()
    {
      TestCommon = new LJCTestCommon("LJCTextParser");
      Console.WriteLine();
      Console.WriteLine("*** LJCTextParser ***");

      // Delimited String Methods

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

      // Both delimiters.
      DelimitedString7();

      // Retrieves a string including the supplied delimiters.
      StringWithDelimiters();

      // Find Tag Methods

      // Finds the first tag in a text value.
      FindTag();

      // Removes a section from a text value.
      RemoveSection();

      // Removes tags from a text value.
      RemoveTags();

      Console.WriteLine();
      Console.WriteLine("Press any key to continue...");
      Console.ReadKey();
    }

    #region Delimited String Methods

    // Begin delimiter only.
    private static void DelimitedString1()
    {
      var line = "<comment>";
      var beginDelimiter = "<comment>";

      var textParser = new LJCTextParser();
      var text = textParser.DelimitedString(line, beginDelimiter);
      var result = $"{text}, {textParser.BeginIndex}, {textParser.EndIndex}";
      result += $", {textParser.StartIndex}";
      var compare = ", 9, 9, -1";
      TestCommon?.Write("DelimitedString1()", result, compare);
    }

    // Begin delimiter and text.
    private static void DelimitedString2()
    {
      var line = "<comment>This is a comment.";
      var beginDelimiter = "<comment>";

      var parser = new LJCTextParser();
      var text = parser.DelimitedString(line, beginDelimiter);
      var result = $"{text}, {parser.BeginIndex}, {parser.EndIndex}";
      result += $", {parser.StartIndex}";
      var compare = "This is a comment., 9, 26, -1";
      TestCommon?.Write("DelimitedString2()", result, compare);
    }

    // Text only.
    private static void DelimitedString3()
    {
      var line = "This is a comment.";

      var parser = new LJCTextParser();
      var text = parser.DelimitedString(line);
      var result = $"{text}, {parser.BeginIndex}, {parser.EndIndex}";
      result += $", {parser.StartIndex}";
      var compare = "This is a comment., 0, 17, -1";
      TestCommon?.Write("DelimitedString3()", result, compare);
    }

    // Text and end delimiter.
    private static void DelimitedString4()
    {
      var line = "This is a comment.</comment>";
      var endDelimiter = "</comment>";

      var parser = new LJCTextParser();
      var text = parser.DelimitedString(line, null, endDelimiter);
      var result = $"{text}, {parser.BeginIndex}, {parser.EndIndex}";
      result += $", {parser.StartIndex}";
      var compare = "This is a comment., 0, 17, -1";
      TestCommon?.Write("DelimitedString4()", result, compare);
    }

    // End delimiter only.
    private static void DelimitedString5()
    {
      var line = "</comment>";
      var endDelimiter = "</comment>";

      var parser = new LJCTextParser();
      var text = parser.DelimitedString(line, null, endDelimiter);
      var result = $"{text}, {parser.BeginIndex}, {parser.EndIndex}";
      result += $", {parser.StartIndex}";
      var compare = ", 0, 0, -1";
      TestCommon?.Write("DelimitedString5()", result, compare);
    }

    // Multiple delimited values.
    private static void DelimitedString6()
    {
      var line = "Value (One), (Two)";
      var beginDelimiter = "(";
      var endDelimiter = ")";

      var parser = new LJCTextParser();
      var first = true;
      while (parser.StartIndex >= 0)
      {
        var text = parser.DelimitedString(line, beginDelimiter, endDelimiter);
        var result = $"{text}, {parser.BeginIndex}, {parser.EndIndex}";
        result += $", {parser.StartIndex}";
        string compare;
        if (first)
        {
          compare = "One, 7, 9, 11";
        }
        else
        {
          compare = "Two, 14, 16, -1";
        }
        first = false;
        TestCommon?.Write("DelimitedString6()", result, compare);
      }
    }

    // Both delimiters.
    private static void DelimitedString7()
    {
      var line = "Value (One)";
      var beginDelimiter = "(";
      var endDelimiter = ")";

      var parser = new LJCTextParser();
      var text = parser.DelimitedString(line, beginDelimiter, endDelimiter);
      var result = $"{text}, {parser.StartIndex}";
      var compare = "One, -1";
      TestCommon?.Write("DelimitedString7()", result, compare);
    }

    // Retrieves a string including the supplied delimiters.
    private static void StringWithDelimiters()
    {
      var line = "Value (One)";
      var beginDelimiter = "(";
      var endDelimiter = ")";

      var parser = new LJCTextParser();
      var text = parser.StringWithDelimiters(line, beginDelimiter, endDelimiter);
      var result = $"{text}, {parser.StartIndex}";
      var compare = "(One), -1";
      TestCommon?.Write("GetStringWithDelimiters()", result, compare);
    }
    #endregion

    #region Find Tag Methods

    // Finds the first tag in a text value.
    private static void FindTag()
    {
      var line = "<comment>A comment.</comment>";

      var textParser = new LJCTextParser();
      string? findTagName = null;
      var result = textParser.FindTag(line, ref findTagName);
      var compare = "<comment>";
      TestCommon?.Write("FindTag()1", result, compare);
      result = findTagName;
      compare = "comment";
      TestCommon?.Write("FindTag()2", result, compare);

      line = "<comment name=\"What?\">A comment.</comment>";
      findTagName = null;
      result = textParser.FindTag(line, ref findTagName);
      compare = "<comment>";
      TestCommon?.Write("FindTag()2", result, compare);
      result = findTagName;
      compare = "comment";
      TestCommon?.Write("FindTag()2", result, compare);

      findTagName = "/";
      result = textParser.FindTag(line, ref findTagName);
      compare = "</comment>";
      TestCommon?.Write("FindTag()3", result, compare);
      result = findTagName;
      compare = "/comment";
      TestCommon?.Write("FindTag()4", result, compare);
    }

    // Removes a section from a text value.
    private static void RemoveSection()
    {
      var line = "<comment>A comment.</comment>";

      var textParser = new LJCTextParser();
      var beginIndex = 0;
      var endIndex = 8;
      var result = textParser.RemoveSection(line, beginIndex, endIndex);
      var compare = "A comment.</comment>";
      TestCommon?.Write("RemoveSection1", result, compare);

      beginIndex = 19;
      endIndex = 28;
      result = textParser.RemoveSection(line, beginIndex, endIndex);
      compare = "<comment>A comment.";
      TestCommon?.Write("RemoveSection2", result, compare);
    }

    // Removes tags from a text value.
    private static void RemoveTags()
    {
      var line = "<comment>A comment.</comment>";

      var textParser = new LJCTextParser();
      var result = textParser.RemoveTags(line);
      var compare = "A comment.";
      TestCommon?.Write("RemoveTags", result, compare);
    }
    #endregion

    #region Class Data

    // Gets or sets the test common object.
    private static LJCTestCommon? TestCommon { get; set; }
    #endregion
  }
}
