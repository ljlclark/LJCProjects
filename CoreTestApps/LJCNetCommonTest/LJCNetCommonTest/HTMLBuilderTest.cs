using LJCNetCommon;

namespace LJCNetCommonTest
{
  internal class HTMLBuilderTest
  {
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
      // Not Indented.  No start with newline.

      // Eliminate compiler message
      if (NetString.HasValue(result)) { }
    }
    #endregion

    #region Append Text Methods (4)

    private static void AddLine()
    {
      // Defaults: IndentCharCount = 2, LineLimit = 80, WrapEnabled = false.
      var hb = new HTMLBuilder();

      // Adds text that ends with a newline.
      hb.AddLine("This is an appended line.");
      hb.AddText(":");
      var result = hb.ToString();

      // result:
      // This is an appended line.
      // :

      // Eliminate compiler message
      if (NetString.HasValue(result)) { }
    }

    private static void AddText()
    {
      // Defaults: IndentCharCount = 2, LineLimit = 80, WrapEnabled = false.
      var hb = new HTMLBuilder();

      // Adds text without modification.
      hb.AddText("This is some appended text.");
      var result = hb.ToString();

      // result:
      // This is some appended text.

      // Eliminate compiler message
      if (NetString.HasValue(result)) { }
    }

    private static void Line()
    {
      // Defaults: IndentCharCount = 2, LineLimit = 80, WrapEnabled = false.
      var hb = new HTMLBuilder();

      hb.Text("This is an appended line.");

      // The builder keeps track of the current number of indents.
      hb.AddIndent();

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

      // Eliminate compiler message
      if (NetString.HasValue(result)) { }
    }

    private static void Text()
    {
      // Defaults: IndentCharCount = 2, LineLimit = 80, WrapEnabled = false.
      var hb = new HTMLBuilder();

      hb.Text("This is an appended line.");

      // The builder keeps track of the current number of indents.
      hb.AddIndent();

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

      // Eliminate compiler message
      if (NetString.HasValue(result)) { }
    }
    #endregion

    #region Get Text Methods (5)

    private static void GetIndented()
    {
      // Defaults: IndentCharCount = 2, LineLimit = 80, WrapEnabled = false.
      var hb = new HTMLBuilder();

      var result = hb.GetIndented("This text is NOT indented.");

      // The builder keeps track of the current number of indents.
      hb.AddIndent(2);
      result += hb.GetLine();
      result += hb.GetIndented("This text is indented.");

      // result:
      // This text is NOT indented.
      //     This text is indented.

      // Eliminate compiler message
      if (NetString.HasValue(result)) { }
    }

    private static void GetIndentString()
    {
      // Defaults: IndentCharCount = 2, LineLimit = 80, WrapEnabled = false.
      var hb = new HTMLBuilder();

      hb.AddIndent(1);
      var result = hb.GetIndentString();
      result += hb.GetText(":", false);

      // result:
      //   :

      // Eliminate compiler message
      if (NetString.HasValue(result)) { }
    }

    private static void GetLine()
    {
      // Defaults: IndentCharCount = 2, LineLimit = 80, WrapEnabled = false.
      var hb = new HTMLBuilder();

      var result = hb.GetText("This is an appended line.");

      // The builder keeps track of the current number of indents.
      hb.AddIndent();
      // Defaults: addIndent = true, allowNewLine = true.
      result += hb.GetLine();
      result += hb.GetText(":");

      // result:
      // This is an appended line.
      //   :

      // Eliminate compiler message
      if (NetString.HasValue(result)) { }
    }

    private static void GetText()
    {
      // Defaults: IndentCharCount = 2, LineLimit = 80, WrapEnabled = false.
      var hb = new HTMLBuilder();

      var result = hb.GetText("This is an appended line.");

      // The builder keeps track of the current number of indents.
      hb.AddIndent();
      result += hb.GetLine();
      // Defaults: addIndent = true, allowNewLine = true.
      result += hb.GetText("This is an indented line.");

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
  }
}

