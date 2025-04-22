using LJCNetCommon;

namespace LJCNetCommonTest
{
  internal class XMLBuilderTest
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
      var xb = new XMLBuilder();

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
      // Not Indented.  No start with newLine.

      // Eliminate compiler message
      if (NetString.HasValue(result)) { }
    }
    #endregion

    #region Append Text Methods (4)

    private static void AddLine()
    {
      // Defaults: IndentCharCount = 2, LineLimit = 80, WrapEnabled = false.
      var xb = new XMLBuilder();

      // Adds text that ends with a newline.
      xb.AddLine("This is an appended line.");

      xb.AddText(":");
      var result = xb.ToString();

      // result:
      // This is an appended line.
      // :

      // Eliminate compiler message
      if (NetString.HasValue(result)) { }
    }

    private static void AddText()
    {
      // Defaults: IndentCharCount = 2, LineLimit = 80, WrapEnabled = false.
      var xb = new XMLBuilder();

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

    private static void GetIndented()
    {
      // Defaults: IndentCharCount = 2, LineLimit = 80, WrapEnabled = false.
      var xb = new XMLBuilder();

      // Example Method
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

      // Example Method
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
  }
}
