// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// HTMLBuilderTest.cs
using LJCNetCommon;
using System;

namespace LJCNetCommonTest
{
  internal class HTMLBuilderTest
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
      var hb = new HTMLBuilder();

      // Example Method:
      // The builder keeps track of the current number of indents.
      // Adds 1 indent by default.
      hb.AddIndent();

      // Adds text without modification.
      hb.AddText("This text is not indented.");

      // Starts the text with a newline if the builder already has text
      // and param allowNewLine = true.
      // The text begins with the current indent string if param
      // addIndent = true.
      // Defaults: addIndent = true, allowNewLine = true.
      hb.Text("This text is indented.");

      // No Indent
      var addIndent = false;
      hb.Text("Not indented.", addIndent);

      // Do not start a newline.
      hb.Text("No start with newline.", allowNewLine: false);
      var result = hb.ToString();

      // result:
      // This text is not indented.
      //   This text is indented.
      // Not indented.  No start with newline.

      var compare = "This text is not indented.\r\n";
      compare += "  This text is indented.\r\n";
      compare += "Not indented.  No start with newline.";
      //Console.WriteLine(result);
      //Console.WriteLine(compare);
      //for (int index = 0; index < compare.Length; index++)
      //{
      //  if (result[index] != compare[index])
      //  {
      //    var from = result[index];
      //    var to = compare[index];
      //    Console.WriteLine($"{index}: {from} != {to}");
      //    break;
      //  }
      //}
      if (result != compare)
      {
        Console.WriteLine("HTMLBuilder.AddIndent() Error");
      }
    }
    #endregion

    #region Append Text Methods (4)

    private static void AddLine()
    {
      // Defaults: IndentCharCount = 2, LineLimit = 80, WrapEnabled = false.
      var hb = new HTMLBuilder();

      // Example Method:
      // Adds text that ends with a newline.
      hb.AddLine("This is an appended line.");

      hb.AddText(":");
      var result = hb.ToString();

      // result:
      // This is an appended line.
      // :

      var compare = "This is an appended line.\r\n";
      compare += ":";
      if (result != compare)
      {
        Console.WriteLine("HTMLBuilder.AddLine() Error");
      }
    }

    private static void AddText()
    {
      // Defaults: IndentCharCount = 2, LineLimit = 80, WrapEnabled = false.
      var hb = new HTMLBuilder();

      // Example Method:
      // Adds text without modification.
      hb.AddText("This is some appended text.");
      var result = hb.ToString();

      // result:
      // This is some appended text.

      var compare = "This is some appended text.";
      if (result != compare)
      {
        Console.WriteLine("HTMLBuilder.AddText() Error");
      }
    }

    private static void Line()
    {
      // Defaults: IndentCharCount = 2, LineLimit = 80, WrapEnabled = false.
      var hb = new HTMLBuilder();

      hb.Text("This is an appended line.");

      // The builder keeps track of the current number of indents.
      hb.AddIndent();

      // Example Method:
      // Starts the text with a newline if the builder already has text
      // and param allowNewLine = true.
      // The text begins with the current indent string if param
      // addIndent = true.
      // Ends the text with a newline.
      // Defaults: addIndent = true, allowNewLine = true.
      hb.Line("");

      hb.Text("This is an indented line.");
      var result = hb.ToString();

      // result:
      // This is an appended line.
      //
      //
      //   This is an indented line.

      var compare = "This is an appended line.\r\n";
      compare += "\r\n";
      compare += "\r\n";
      compare += "  This is an indented line.";
      if (result != compare)
      {
        Console.WriteLine("HTMLBuilder.Line() Error");
      }
    }

    private static void Text()
    {
      // Defaults: IndentCharCount = 2, LineLimit = 80, WrapEnabled = false.
      var hb = new HTMLBuilder();

      hb.Text("This is an appended line.");

      // The builder keeps track of the current number of indents.
      hb.AddIndent();

      // Example Method:
      // Starts the text with a newline if the builder already has text
      // and param allowNewLine = true.
      // The text begins with the current indent string if param
      // addIndent = true.
      // Defaults: addIndent = true, allowNewLine = true.
      hb.Text("This is an indented line.");
      var result = hb.ToString();

      // result:
      // This is an appended line.
      //   This is an indented line.

      var compare = "This is an appended line.\r\n";
      compare += "  This is an indented line.";
      if (result != compare)
      {
        Console.WriteLine("HTMLBuilder.Text() Error");
      }
    }
    #endregion

    #region Get Text Methods (5)

    private static void GetAttribs()
    {
      // Root Method Begin
      var textState = new TextState();

      // Defaults: IndentCharCount = 2, LineLimit = 80, WrapEnabled = false.
      var hb = new HTMLBuilder(textState);

      var attribs = new Attributes()
      {
        { "class", "Selector" },
      };
      hb.Begin("div", textState, attribs);
      hb.End("div", textState);
      var result = hb.ToString();

      // result:
      // <div class="Selector">
      // <div>

      var compare = "<div class=\"Selector\">\r\n";
      compare += "</div>";
      if (result != compare)
      {
        Console.WriteLine("HTMLBuilder.GetAttribs() Error");
      }
    }

    private static void GetIndented()
    {
      // Defaults: IndentCharCount = 2, LineLimit = 80, WrapEnabled = false.
      var hb = new HTMLBuilder();

      // Example Method:
      var result = hb.GetIndented("This text is NOT indented.");

      // The builder keeps track of the current number of indents.
      hb.AddIndent(2);
      result += hb.GetLine();
      result += hb.GetIndented("This text is indented.");

      // result:
      // This text is NOT indented.
      //     This text is indented.

      var compare = "This text is NOT indented.\r\n";
      compare += "    This text is indented.";
      if (result != compare)
      {
        Console.WriteLine("HTMLBuilder.GetIndented() Error");
      }
    }

    private static void GetIndentString()
    {
      // Defaults: IndentCharCount = 2, LineLimit = 80, WrapEnabled = false.
      var hb = new HTMLBuilder();

      hb.AddIndent(1);

      // Example Method:
      var result = hb.GetIndentString();

      result += hb.GetText(":", false);

      // result:
      //   :

      var compare = "  :";
      if (result != compare)
      {
        Console.WriteLine("HTMLBuilder.GetIndentString() Error");
      }
    }

    private static void GetLine()
    {
      // Defaults: IndentCharCount = 2, LineLimit = 80, WrapEnabled = false.
      var hb = new HTMLBuilder();

      var result = hb.GetText("This is an appended line.");

      // The builder keeps track of the current number of indents.
      hb.AddIndent();

      // Example Method:
      // Starts the text with a newline if the builder already has text
      // and param allowNewLine = true.
      // The text begins with the current indent string if param
      // addIndent = true.
      // Ends the text with a newline.
      // Defaults: addIndent = true, allowNewLine = true.
      result += hb.GetLine();

      result += hb.GetText(":");

      // result:
      // This is an appended line.
      //   :

      var compare = "This is an appended line.\r\n";
      compare += "  :";
      if (result != compare)
      {
        Console.WriteLine("HTMLBuilder.GetLine() Error");
      }
    }

    private static void GetText()
    {
      // Defaults: IndentCharCount = 2, LineLimit = 80, WrapEnabled = false.
      var hb = new HTMLBuilder();

      var result = hb.GetText("This is an appended line.");

      // The builder keeps track of the current number of indents.
      hb.AddIndent();
      result += hb.GetLine();

      // Example Method:
      // Starts the text with a newline if the builder already has text
      // and param allowNewLine = true.
      // The text begins with the current indent string if param
      // addIndent = true.
      // Defaults: addIndent = true, allowNewLine = true.
      result += hb.GetText("This is an indented line.");

      // result:
      // This is an appended line.
      //   This is an indented line.

      var compare = "This is an appended line.\r\n";
      compare += "  This is an indented line.";
      if (result != compare)
      {
        Console.WriteLine("HTMLBuilder.GetText() Error");
      }
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
