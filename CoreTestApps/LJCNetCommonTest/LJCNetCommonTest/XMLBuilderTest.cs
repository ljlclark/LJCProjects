// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// XMLBuilderTest.cs
using LJCNetCommon;
using System;

namespace LJCNetCommonTest
{
  internal class XMLBuilderTest
  {
    // Performs the tests.
    internal static void Test()
    {
      // Methods
      AddIndent();

      // Append Text Methods
      AddLine();
      AddText();
      Line();
      Text();

      // Get Text Methods
      GetAttribs();
      GetIndented();
      GetIndentString();
      GetLine();
      GetText();
      GetWrapped();
    }

    #region Methods

    private static void AddIndent()
    {
      // Defaults: IndentCharCount = 2, LineLimit = 80, WrapEnabled = false.
      var xb = new XMLBuilder();

      // Example Method:
      // The builder keeps track of the current number of indents.
      // Adds 1 indent by default.
      xb.AddIndent();

      // Adds text without modification.
      xb.AddText("This text is not indented.");

      // Starts the text with a newline if the builder already has text
      // and param allowNewLine = true.
      // The text begins with the current indent string if param
      // addIndent = true.
      // Defaults: addIndent = true, allowNewLine = true.
      xb.Text("This text is indented.");

      // No Indent
      var addIndent = false;
      xb.Text("Not indented.", addIndent);

      // Do not start a newline.
      xb.Text("No start with newline.", allowNewLine: false);
      var result = xb.ToString();

      // result:
      // This text is not indented.
      //   This text is indented.
      // Not indented.  No start with newLine.

      var compare = "This text is not indented.\r\n";
      compare += "  This text is indented.\r\n";
      compare += "Not indented.  No start with newline.";
      if (result != compare)
      {
        Console.WriteLine("XMLBuilder.AddIndent() Error");
      }
    }
    #endregion

    #region Append Text Methods (4)

    private static void AddLine()
    {
      // Defaults: IndentCharCount = 2, LineLimit = 80, WrapEnabled = false.
      var xb = new XMLBuilder();

      // Example Method:
      // Adds text that ends with a newline.
      xb.AddLine("This is an appended line.");

      xb.AddText(":");
      var result = xb.ToString();

      // result:
      // This is an appended line.
      // :

      var compare = "This is an appended line.\r\n";
      compare += ":";
      if (result != compare)
      {
        Console.WriteLine("XMLBuilder.AddLine() Error");
      }
    }

    private static void AddText()
    {
      // Defaults: IndentCharCount = 2, LineLimit = 80, WrapEnabled = false.
      var xb = new XMLBuilder();

      // Example Method:
      // Adds text without modification.
      xb.AddText("This is some appended text.");
      var result = xb.ToString();

      // result:
      // This is some appended text.

      // Eliminate compiler message
      if (NetString.HasValue(result)) { }
    }

    private static void Line()
    {
      // Defaults: IndentCharCount = 2, LineLimit = 80, WrapEnabled = false.
      var xb = new XMLBuilder();

      xb.Text("This is an appended line.");

      // The builder keeps track of the current number of indents.
      xb.AddIndent();

      // Example Method:
      // Starts the text with a newline if the builder already has text
      // and param allowNewLine = true.
      // The text begins with the current indent string if param
      // addIndent = true.
      // Ends the text with a newline.
      // Defaults: addIndent = true, allowNewLine = true.
      xb.Line();

      xb.Text("This is an indented line.");
      var result = xb.ToString();

      // result:
      // This is an appended line.
      //
      //
      //   This is an indented line.

      // Eliminate compiler message
      if (NetString.HasValue(result)) { }
    }

    private static void Text()
    {
      // Defaults: IndentCharCount = 2, LineLimit = 80, WrapEnabled = false.
      var xb = new XMLBuilder();

      xb.Text("This is an appended line.");

      // The builder keeps track of the current number of indents.
      xb.AddIndent();

      // Example Method:
      // Starts the text with a newline if the builder already has text
      // and param allowNewLine = true.
      // The text begins with the current indent string if param
      // addIndent = true.
      // Defaults: addIndent = true, allowNewLine = true.
      xb.Text("This is an indented line.");
      var result = xb.ToString();

      // result:
      // This is an appended line.
      //   This is an indented line.

      // Eliminate compiler message
      if (NetString.HasValue(result)) { }
    }
    #endregion

    #region Get Text Methods (5)

    private static void GetAttribs()
    {
      // Root Method Begin
      var textState = new TextState();

      // Defaults: IndentCharCount = 2, LineLimit = 80, WrapEnabled = false.
      var xb = new XMLBuilder(textState);

      var attribs = new Attributes()
      {
        { "name", "Someone" },
      };
      xb.Begin("Person", textState, attribs);
      xb.End("Person", textState);
      var result = xb.ToString();

      // result:
      // &amp;lt;Person name="Someone"&amp;gt;
      // &amp;lt;Person&amp;gt;

      // Eliminate compiler message
      if (NetString.HasValue(result)) { }
    }

    private static void GetIndented()
    {
      // Defaults: IndentCharCount = 2, LineLimit = 80, WrapEnabled = false.
      var xb = new XMLBuilder();

      // Example Method:
      var result = xb.GetIndented("This text is NOT indented.");

      // The builder keeps track of the current number of indents.
      xb.AddIndent(2);
      result += xb.GetLine();
      result += xb.GetIndented("This text is indented.");

      // result:
      // This text is NOT indented.
      //     This text is indented.

      // Eliminate compiler message
      if (NetString.HasValue(result)) { }
    }

    private static void GetIndentString()
    {
      // Defaults: IndentCharCount = 2, LineLimit = 80, WrapEnabled = false.
      var xb = new XMLBuilder();

      xb.AddIndent(1);

      // Example Method:
      var result = xb.GetIndentString();

      result += xb.GetText(":", false);

      // result:
      //   :

      // Eliminate compiler message
      if (NetString.HasValue(result)) { }
    }

    private static void GetLine()
    {
      // Defaults: IndentCharCount = 2, LineLimit = 80, WrapEnabled = false.
      var xb = new XMLBuilder();

      var result = xb.GetText("This is an appended line.");

      // The builder keeps track of the current number of indents.
      xb.AddIndent();

      // Example Method:
      // Starts the text with a newline if the builder already has text
      // and param allowNewLine = true.
      // The text begins with the current indent string if param
      // addIndent = true.
      // Ends the text with a newline.
      // Defaults: addIndent = true, allowNewLine = true.
      result += xb.GetLine();

      result += xb.GetText(":");

      // result:
      // This is an appended line.
      //   :

      // Eliminate compiler message
      if (NetString.HasValue(result)) { }
    }

    private static void GetText()
    {
      // Defaults: IndentCharCount = 2, LineLimit = 80, WrapEnabled = false.
      var xb = new XMLBuilder();

      var result = xb.GetText("This is an appended line.");

      // The builder keeps track of the current number of indents.
      xb.AddIndent();
      result += xb.GetLine();

      // Example Method:
      // Starts the text with a newline if the builder already has text
      // and param allowNewLine = true.
      // The text begins with the current indent string if param
      // addIndent = true.
      // Defaults: addIndent = true, allowNewLine = true.
      result += xb.GetText("This is an indented line.");

      // result:
      // This is an appended line.
      //   This is an indented line.

      // Eliminate compiler message
      if (NetString.HasValue(result)) { }
    }

    private static void GetWrapped()
    {
    }
    #endregion

    #region Class Data

    private const bool NoIndent = false;
    private const bool NoNewLine = false;
    #endregion
  }
}
