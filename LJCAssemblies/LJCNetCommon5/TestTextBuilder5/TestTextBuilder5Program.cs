// Copyright (c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// TestTextBuilder5Program.cs
using LJCNetCommon5;

namespace TestTextBuilder5
{
  // The entry class.
  internal class TestTextBuilder5Program
  {
    // The entry method.
    static void Main()
    {
      TestCommon = new LJCTestCommon5("TestTextBuilder5Program");
      Console.WriteLine();
      Console.WriteLine("*** LJCTextBuilder5 ***");

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
      GetDelimited();
      GetIndented();
      GetIndentString();
      GetLine();
      GetText();
      GetWrapped();
    }

    #region Methods

    // Changes the IndentCount by the provided value.
    private static void AddIndent()
    {
      var tb = new LJCTextBuilder5();

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

      var b = new LJCTextBuilder5();
      b.AddLine("This text is not indented.");
      b.AddLine("  This text is indented.");
      b.AddText("Not indented.  No start with newline.");
      var compare = b.ToString();
      TestCommon?.Write("AddIndent()", result, compare);
    }
    #endregion

    #region Append Text Methods

    private static void AddLine()
    {
      var tb = new LJCTextBuilder5();

      // Example Method:
      // Adds text that ends with a newline.
      tb.AddLine("This is an appended line.");

      tb.AddText(":");
      var result = tb.ToString();

      // result:
      // This is an appended line.
      // :

      var b = new LJCTextBuilder5();
      b.AddLine("This is an appended line.");
      b.AddText(":");
      var compare = b.ToString();
      TestCommon?.Write("AddLine()", result, compare);
    }

    // Adds text without modification.
    private static void AddText()
    {
      var tb = new LJCTextBuilder5();

      // Example Method:
      // Adds text without modification.
      tb.AddText("This is some appended text.");
      var result = tb.ToString();

      // result:
      // This is some appended text.

      var b = new LJCTextBuilder5();
      b.AddText("This is some appended text.");
      var compare = b.ToString();
      TestCommon?.Write("AddText()", result, compare);
    }

    // Adds a delimiter if not the first list item.
    private static void Item()
    {
      var tb = new LJCTextBuilder5();

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
      if (LJC5.HasValue(result)) { }

      tb.Clear();
      result = tb.Text(saveResult);

      // Eliminate compiler message
      if (LJC5.HasValue(result)) { }
    }

    // Adds a modified text line to the builder.
    private static void Line()
    {
      var tb = new LJCTextBuilder5();

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
      tb.Line();

      tb.Text("This is an indented line.");
      var result = tb.ToString();

      // result:
      // This is an appended line.
      //
      //   This is an indented line.

      var b = new LJCTextBuilder5();
      b.AddLine("This is an appended line.");
      b.AddLine();
      b.AddText("  This is an indented line.");
      var compare = b.ToString();
      TestCommon?.Write("Line()", result, compare);
    }

    // Adds modified text to the builder.
    private static void Text()
    {
      var tb = new LJCTextBuilder5();

      tb.Text("This is an appended line.");

      // The builder keeps track of the current number of indents.
      tb.AddIndent();

      // Example Method:
      // Starts the text with a newline if the builder already has text
      // and param allowNewLine = true.
      // The text begins with the current indent string if param
      // addIndent = true.
      tb.Text("This is an indented line.");
      var result = tb.ToString();

      // result:
      // This is an appended line.
      //   This is an indented line.

      var b = new LJCTextBuilder5();
      b.AddLine("This is an appended line.");
      b.AddText("  This is an indented line.");
      var compare = b.ToString();
      TestCommon?.Write("Text()", result, compare);
    }
    #endregion

    #region Get Text Methods

    // Indicates if the builder text ends with a newline.
    private static bool EndsWithNewLine()
    {
      var tb = new LJCTextBuilder5();

      bool retValue = tb.EndsWithNewLine();
      var result = retValue.ToString();

      // result:
      // False

      var b = new LJCTextBuilder5();
      b.AddText("False");
      var compare = b.ToString();
      TestCommon?.Write("EndsWithNewLine()", result, compare);
      return retValue;
    }

    // Allow text to start with a newline.
    private static bool StartWithNewLine()
    {
      var tb = new LJCTextBuilder5();

      bool retValue = tb.StartWithNewLine(true);
      var result = retValue.ToString();

      // result:
      // False

      var b = new LJCTextBuilder5();
      b.AddText("False");
      var compare = b.ToString();
      TestCommon?.Write("StartWithNewLine()", result, compare);
      return retValue;
    }

    // Adds a delimiter if not the first list item.
    private static void GetDelimited()
    {
      // Defaults: IndentCharCount = 2, LineLimit = 80, WrapEnabled = false.
      var tb = new LJCTextBuilder5();

      var result = "List: ";

      // Example Method
      result += tb.GetDelimited("One");
      result += tb.GetDelimited("Two");
      result += tb.GetDelimited("Three");

      // result:
      // One, Two, Three

      // Eliminate compiler message
      if (LJC5.HasValue(result)) { }
    }

    // Gets a new potentially indented line.
    private static void GetIndented()
    {
      var tb = new LJCTextBuilder5();

      // Example Method:
      var result = tb.GetIndented("This text is NOT indented.");

      // The builder keeps track of the current number of indents.
      tb.AddIndent(2);
      result += tb.GetLine();
      result += tb.GetIndented("This text is indented.");

      // result:
      // This text is NOT indented.
      //     This text is indented.

      var b = new LJCTextBuilder5();
      b.AddLine("This text is NOT indented.");
      b.AddText("    This text is indented.");
      var compare = b.ToString();
      TestCommon?.Write("GetIndented()", result, compare);
    }

    // Returns the current indent string.
    private static void GetIndentString()
    {
      // Defaults: IndentCharCount = 2, LineLimit = 80, WrapEnabled = false.
      var tb = new LJCTextBuilder5();

      tb.AddIndent(1);

      // Example Method:
      var result = tb.GetIndentString();

      result += tb.GetText(":", false);

      // result:
      //   :

      var compare = "  :";
      TestCommon?.Write("GetIndentString()", result, compare);
    }

    // Gets a modified text line.
    private static void GetLine()
    {
      var tb = new LJCTextBuilder5();

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
      var text = tb.GetLine();
      tb.AddText(text);

      tb.Text(":");
      var result = tb.ToString();

      // result:
      // This is an appended line.
      //
      //   :

      var b = new LJCTextBuilder5();
      b.AddLine("This is an appended line.");
      b.AddLine();
      b.AddText("  :");
      var compare = b.ToString();
      TestCommon?.Write("GetLine()", result, compare);
    }

    // Gets potentially indented and wrapped text.
    private static void GetText()
    {
      var tb = new LJCTextBuilder5();

      tb.Text("This is an appended line.");

      // The builder keeps track of the current number of indents.
      tb.AddIndent();

      // Example Method:
      // Starts the text with a newline if the builder already has text
      // and param allowNewLine = true and builder text does not end with
      // a newline.
      // The text begins with the current indent string if param
      // addIndent = true.
      var tempText = tb.GetText("This is an indented line.");
      tb.AddText(tempText);
      var result = tb.ToString();

      // result:
      // This is an appended line.
      //   This is an indented line.

      var b = new LJCTextBuilder5();
      b.AddLine("This is an appended line.");
      b.AddText("  This is an indented line.");
      var compare = b.ToString();
      TestCommon?.Write("GetText()", result, compare);
    }

    // Gets added text and new wrapped line if combined line > LineLimit.
    private static void GetWrapped()
    {
      var tb = new LJCTextBuilder5
      {
        WrapEnabled = true,
        WrapPrefix = "",
      };

      // Example Method:
      var b = new LJCTextBuilder5();
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
      TestCommon?.Write("GetWrapped()", result, compare);
    }
    #endregion

    #region Class Data

    // Gets or sets the test common object.
    private static LJCTestCommon5? TestCommon { get; set; }

    private const bool NoIndent = false;
    private const bool NoNewLine = false;
    #endregion
  }
}
