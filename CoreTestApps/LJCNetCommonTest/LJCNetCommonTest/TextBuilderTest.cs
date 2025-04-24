// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// TextBuilderTest.cs
using LJCNetCommon;
using System;

namespace LJCNetCommonTest
{
  internal class TextBuilderTest
  {
    #region Methods

    // Performs the tests.
    internal static void Test()
    {
      Console.WriteLine();
      Console.WriteLine("*** TextBuilder ***");

      // Methods
      AddIndent();

      // Append Text Methods
      AddLine();
      AddText();
      Item();
      Line();
      Text();

      // Get Text Methods
      EndsWithNewLine();
      StartWithNewLine();
      GetItem();
      GetIndented();
      GetIndentString();
      GetLine();
      GetText();
      GetWrapped();
    }

    private static void AddIndent()
    {
      // Defaults: IndentCharCount = 2, LineLimit = 80, WrapEnabled = false.
      var tb = new TextBuilder();

      // Example Method:
      // The builder keeps track of the current number of indents.
      // Adds 1 indent by default.
      tb.AddIndent();

      // Adds text without modification.
      tb.AddText("This text is not indented.");

      // Starts the text with a newline if the builder already has text
      // and param allowNewLine = true and builder text does not end with
      // a newline.
      // The text begins with the current indent string if param
      // addIndent = true.
      // Defaults: addIndent = true, allowNewLine = true.
      tb.Text("This text is indented.");

      // No Indent
      var addIndent = false;
      tb.Text("Not indented.", addIndent);

      // Do not start a new line.
      tb.Text("No start with newline.", allowNewLine: false);
      var result = tb.ToString();

      // result:
      // This text is not indented.
      //   This text is indented.
      // Not Indented.  No start with newline.

      var b = new TextBuilder();
      b.AddLine("This text is not indented.");
      b.AddLine("  This text is indented.");
      b.AddText("Not indented.  No start with newline.");
      var compare = b.ToString();
      if (result != compare)
      {
        Console.WriteLine("\r\nTextBuilder.AddIndent()");
        Console.WriteLine(result);
        Console.WriteLine(" !=");
        Console.WriteLine(compare);
      }
    }
    #endregion

    #region Append Text Methods (5)

    private static void AddLine()
    {
      // Defaults: IndentCharCount = 2, LineLimit = 80, WrapEnabled = false.
      var tb = new TextBuilder();

      // Example Method:
      // Adds text that ends with a newline.
      tb.AddLine("This is an appended line.");

      tb.AddText(":");
      var result = tb.ToString();

      // result:
      // This is an appended line.
      // :

      var b = new TextBuilder();
      b.AddLine("This is an appended line.");
      b.AddText(":");
      var compare = b.ToString();
      if (result != compare)
      {
        Console.WriteLine("\r\nTextBuilder.AddLine()");
        Console.WriteLine(result);
        Console.WriteLine(" !=");
        Console.WriteLine(compare);
      }
    }

    private static void AddText()
    {
      // Defaults: IndentCharCount = 2, LineLimit = 80, WrapEnabled = false.
      var tb = new TextBuilder();

      // Example Method:
      // Adds text without modification.
      tb.AddText("This is some appended text.");
      var result = tb.ToString();

      // result:
      // This is some appended text.

      var b = new TextBuilder();
      b.AddText("This is some appended text.");
      var compare = b.ToString();
      if (result != compare)
      {
        Console.WriteLine("\r\nTextBuilder.AddText()");
        Console.WriteLine(result);
        Console.WriteLine(" !=");
        Console.WriteLine(compare);
      }
    }

    private static void Item()
    {
      // Defaults: IndentCharCount = 2, LineLimit = 80, WrapEnabled = false.
      var tb = new TextBuilder();

      // Defaults: IsFirst = true, Delimiter = ", ".
      for (int index = 0; index < 14; index++)
      {
        // Example Method
        var text = $"{index}**-";
        tb.Item(text, NoIndent, NoNewLine);
      }
      var saveResult = tb.ToString();

      // result:
      // 0**-, 1**-, 2**-, 3**-, 4**-, 5**-, 6**-, 7**-, 8**-, 9**-, 10**-, 11**-, 12**-, 13**-, 14**-

      tb.Clear();
      tb.WrapEnabled = true;
      for (int index = 0; index < 14; index++)
      {
        // Example Method:
        var text = $"{index}**-";
        tb.Item(text, NoIndent, NoNewLine);
      }
      var result = tb.ToString();

      // Eliminate compiler message
      if (NetString.HasValue(result)) { }

      tb.Clear();
      result = tb.Text(saveResult);

      // Eliminate compiler message
      if (NetString.HasValue(result)) { }
    }

    private static void Line()
    {
      // Defaults: IndentCharCount = 2, LineLimit = 80, WrapEnabled = false.
      var tb = new TextBuilder();

      tb.Text("This is an appended line.");

      // The builder keeps track of the current number of indents.
      tb.AddIndent();

      // Example Method:
      // Starts the text with a newline if the builder already has text
      // and param allowNewLine = true and builder text does not end with
      // a newline.
      // The text begins with the current indent string if param
      // addIndent = true.
      // Ends the text with a newline.
      // Defaults: addIndent = true, allowNewLine = true.
      tb.Line();

      tb.Text("This is an indented line.");
      var result = tb.ToString();

      // result:
      // This is an appended line.
      //
      //   This is an indented line.

      var b = new TextBuilder();
      b.AddLine("This is an appended line.");
      b.AddLine();
      b.AddText("  This is an indented line.");
      var compare = b.ToString();
      if (result != compare)
      {
        Console.WriteLine("\r\nTextBuilder.Line()");
        Console.WriteLine(result);
        Console.WriteLine(" !=");
        Console.WriteLine(compare);
      }
    }

    private static void Text()
    {
      // Defaults: IndentCharCount = 2, LineLimit = 80, WrapEnabled = false.
      var tb = new TextBuilder();

      tb.Text("This is an appended line.");

      // The builder keeps track of the current number of indents.
      tb.AddIndent();

      // Example Method:
      // Starts the text with a newline if the builder already has text
      // and param allowNewLine = true.
      // The text begins with the current indent string if param
      // addIndent = true.
      // Defaults: addIndent = true, allowNewLine = true.
      tb.Text("This is an indented line.");
      var result = tb.ToString();

      // result:
      // This is an appended line.
      //   This is an indented line.

      var b = new TextBuilder();
      b.AddLine("This is an appended line.");
      b.AddText("  This is an indented line.");
      var compare = b.ToString();
      if (result != compare)
      {
        Console.WriteLine("\r\nTextBuilder.Text()");
        Console.WriteLine(result);
        Console.WriteLine(" !=");
        Console.WriteLine(compare);
      }
    }
    #endregion

    #region Get Text Methods (6)

    private static bool EndsWithNewLine()
    {
      var tb = new TextBuilder();

      bool retValue = tb.EndsWithNewLine();
      var result = retValue.ToString();

      // result:
      // False

      var b = new TextBuilder();
      b.AddText("False");
      var compare = b.ToString();
      if (result != compare)
      {
        Console.WriteLine("\r\nTextBuilder.EndsWithNewLine()");
        Console.WriteLine(result);
        Console.WriteLine(" !=");
        Console.WriteLine(compare);
      }
      return retValue;
    }

    private static bool StartWithNewLine()
    {
      var tb = new TextBuilder();

      bool retValue = tb.StartWithNewLine(true);
      var result = retValue.ToString();

      // result:
      // False

      var b = new TextBuilder();
      b.AddText("False");
      var compare = b.ToString();
      if (result != compare)
      {
        Console.WriteLine("\r\nTextBuilder.StartWithNewLine()");
        Console.WriteLine(result);
        Console.WriteLine(" !=");
        Console.WriteLine(compare);
      }
      return retValue;
    }

    private static void GetIndented()
    {
      // Defaults: IndentCharCount = 2, LineLimit = 80, WrapEnabled = false.
      var tb = new TextBuilder();

      // Example Method:
      var result = tb.GetIndented("This text is NOT indented.");

      // The builder keeps track of the current number of indents.
      tb.AddIndent(2);
      result += tb.GetLine();
      result += tb.GetIndented("This text is indented.");

      // result:
      // This text is NOT indented.
      //     This text is indented.

      var b = new TextBuilder();
      b.AddLine("This text is NOT indented.");
      b.AddText("    This text is indented.");
      var compare = b.ToString();
      if (result != compare)
      {
        Console.WriteLine("\r\nTextBuilder.GetIndented()");
        Console.WriteLine(result);
        Console.WriteLine(" !=");
        Console.WriteLine(compare);
      }
    }

    private static void GetIndentString()
    {
      // Defaults: IndentCharCount = 2, LineLimit = 80, WrapEnabled = false.
      var tb = new TextBuilder();

      tb.AddIndent(1);

      // Example Method:
      var result = tb.GetIndentString();

      result += tb.GetText(":", false);

      // result:
      //   :

      var compare = "  :";
      if (result != compare)
      {
        Console.WriteLine("\r\nTextBuilder.GetIndentString()");
        Console.WriteLine(result);
        Console.WriteLine(" !=");
        Console.WriteLine(compare);
      }
    }

    private static void GetItem()
    {
      // Defaults: IndentCharCount = 2, LineLimit = 80, WrapEnabled = false.
      var tb = new TextBuilder();

      var result = "List: ";

      // Example Method
      // Defaults: tb.IsFirst = true, tb.Delimiter = ","
      result += tb.GetDelimited("One");
      result += tb.GetDelimited("Two");
      result += tb.GetDelimited("Three");

      // result:
      // One, Two, Three

      // Eliminate compiler message
      if (NetString.HasValue(result)) { }
    }

    private static void GetLine()
    {
      // Defaults: IndentCharCount = 2, LineLimit = 80, WrapEnabled = false.
      var tb = new TextBuilder();

      tb.Text("This is an appended line.");

      // The builder keeps track of the current number of indents.
      tb.AddIndent();

      // Example Method:
      // Starts the text with a newline if the builder already has text
      // and param allowNewLine = true and builder text does not end with
      // a newline.
      // The text begins with the current indent string if param
      // addIndent = true.
      // Ends the text with a newline.
      // Defaults: addIndent = true, allowNewLine = true.
      var text = tb.GetLine();
      tb.AddText(text);

      tb.Text(":");
      var result = tb.ToString();

      // result:
      // This is an appended line.
      //
      //   :

      var b = new TextBuilder();
      b.AddLine("This is an appended line.");
      b.AddLine();
      b.AddText("  :");
      var compare = b.ToString();
      if (result != compare)
      {
        Console.WriteLine("\r\nTextBuilder.GetLine()");
        Console.WriteLine(result);
        Console.WriteLine(" !=");
        Console.WriteLine(compare);
      }
    }

    private static void GetText()
    {
      // Defaults: IndentCharCount = 2, LineLimit = 80, WrapEnabled = false.
      var tb = new TextBuilder();

      tb.Text("This is an appended line.");

      // The builder keeps track of the current number of indents.
      tb.AddIndent();

      // Example Method:
      // Starts the text with a newline if the builder already has text
      // and param allowNewLine = true and builder text does not end with
      // a newline.
      // The text begins with the current indent string if param
      // addIndent = true.
      // Defaults: addIndent = true, allowNewLine = true.
      var tempText = tb.GetText("This is an indented line.");
      tb.AddText(tempText);
      var result = tb.ToString();

      // result:
      // This is an appended line.
      //   This is an indented line.

      var b = new TextBuilder();
      b.AddLine("This is an appended line.");
      b.AddText("  This is an indented line.");
      var compare = b.ToString();
      if (result != compare)
      {
        Console.WriteLine("\r\nTextBuilder.GetText()");
        Console.WriteLine(result);
        Console.WriteLine(" !=");
        Console.WriteLine(compare);
      }
    }

    private static void GetWrapped()
    {
      // Defaults: IndentCharCount = 2, LineLimit = 80, WrapEnabled = false.
      var tb = new TextBuilder
      {
        WrapEnabled = true,
        WrapPrefix = "",
      };

      // Example Method:
      var b = new TextBuilder();
      b.AddText("Now is the time for all good men to come to the aid of their");
      b.AddText(" country.");
      b.AddText(" Now is the time for all good men to come to the aid of their");
      b.AddText(" country.");
      var text = b.ToString();
      var result = tb.GetWrapped(text);

      // result:
      // Now is the time for all good men to come to the aid of their country. Now is the
      // time for all good men to come to the aid of their country.

      b.Clear();
      b.AddText("Now is the time for all good men to come to the aid of");
      b.AddLine(" their country. Now is the");
      b.AddText("time for all good men to come to the aid of their country.");
      var compare = b.ToString();
      if (result != compare)
      {
        Console.WriteLine("\r\nTextBuilder.GetWrapped()");
        Console.WriteLine(result);
        Console.WriteLine(" !=");
        Console.WriteLine(compare);
      }
    }
    #endregion

    #region Class Data

    private const bool NoIndent = false;
    private const bool NoNewLine = false;
    #endregion
  }
}
