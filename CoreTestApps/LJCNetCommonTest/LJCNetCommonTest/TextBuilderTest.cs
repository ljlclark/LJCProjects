using LJCNetCommon;

namespace LJCNetCommonTest
{
  internal class TextBuilderTest
  {
    internal static void Test()
    {
      // Methods
      AddIndent();

      // Append Text Methods
      AddLine();
      AddText();
      Item();
      Line();
      Text();

      // Get Text Methods
      GetDelimited();
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
      var tb = new TextBuilder();

      // The builder keeps track of the current number of indents.
      // Adds 1 indent by default.
      tb.AddIndent();

      // Adds text without modification.
      tb.AddText("This text is not indented.");

      // Starts the text with a newline if the builder already has text
      // and param allowNewLine = true.
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

      // Eliminate compiler message
      if (NetString.HasValue(result)) { }
    }
    #endregion

    #region Append Text Methods (5)

    private static void AddLine()
    {
      // Defaults: IndentCharCount = 2, LineLimit = 80, WrapEnabled = false.
      var tb = new TextBuilder();

      // Adds text that ends with a newline.
      tb.AddLine("This is an appended line.");

      tb.AddText(":");
      var result = tb.ToString();

      // result:
      // This is an appended line.
      // :

      // Eliminate compiler message
      if (NetString.HasValue(result)) { }
    }

    private static void AddText()
    {
      // Defaults: IndentCharCount = 2, LineLimit = 80, WrapEnabled = false.
      var tb = new TextBuilder();

      // Adds text without modification.
      tb.AddText("This is some appended text.");
      var result = tb.ToString();

      // result:
      // This is some appended text.

      // Eliminate compiler message
      if (NetString.HasValue(result)) { }
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
        // Example Method
        var text = $"{index}**-";
        tb.Item(text, NoIndent, NoNewLine);
      }
      var result = tb.ToString();

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

      // Starts the text with a newline if the builder already has text
      // and param allowNewLine = true.
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
      //
      //   This is an indented line.

      // Eliminate compiler message
      if (NetString.HasValue(result)) { }
    }

    private static void Text()
    {
      // Defaults: IndentCharCount = 2, LineLimit = 80, WrapEnabled = false.
      var tb = new TextBuilder();

      tb.Text("This is an appended line.");

      // The builder keeps track of the current number of indents.
      tb.AddIndent();

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

      // Eliminate compiler message
      if (NetString.HasValue(result)) { }
    }
    #endregion

    #region Get Text Methods (6)

    private static void GetDelimited()
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

    private static void GetIndented()
    {
      // Defaults: IndentCharCount = 2, LineLimit = 80, WrapEnabled = false.
      var tb = new TextBuilder();

      // Example Method
      var result = tb.GetIndented("This text is NOT indented.");

      // The builder keeps track of the current number of indents.
      tb.AddIndent(2);
      result += tb.GetLine();

      result += tb.GetIndented("This text is indented.");

      // result:
      // This text is NOT indented.
      //     This text is indented.

      // Eliminate compiler message
      if (NetString.HasValue(result)) { }
    }

    private static void GetIndentString()
    {
      // Defaults: IndentCharCount = 2, LineLimit = 80, WrapEnabled = false.
      var tb = new TextBuilder();

      tb.AddIndent(1);

      // Example Method
      var result = tb.GetIndentString();

      result += tb.GetText(":", false);

      // result:
      //   :

      // Eliminate compiler message
      if (NetString.HasValue(result)) { }
    }

    private static void GetLine()
    {
      // Defaults: IndentCharCount = 2, LineLimit = 80, WrapEnabled = false.
      var tb = new TextBuilder();

      var result = tb.GetText("This is an appended line.");

      // The builder keeps track of the current number of indents.
      tb.AddIndent();

      // Starts the text with a newline if the builder already has text
      // and param allowNewLine = true.
      // The text begins with the current indent string if param
      // addIndent = true.
      // Ends the text with a newline.
      // Defaults: addIndent = true, allowNewLine = true.
      result += tb.GetLine();

      result += tb.GetText(":");

      // result:
      // This is an appended line.
      //   :

      // Eliminate compiler message
      if (NetString.HasValue(result)) { }
    }

    private static void GetText()
    {
      // Defaults: IndentCharCount = 2, LineLimit = 80, WrapEnabled = false.
      var tb = new TextBuilder();

      var result = tb.GetText("This is an appended line.");

      // The builder keeps track of the current number of indents.
      tb.AddIndent();
      result += tb.GetLine();

      // Starts the text with a newline if the builder already has text
      // and param allowNewLine = true.
      // The text begins with the current indent string if param
      // addIndent = true.
      // Defaults: addIndent = true, allowNewLine = true.
      result += tb.GetText("This is an indented line.");

      // result:
      //   This is an appended line.
      //     This is an indented line.

      // Eliminate compiler message
      if (NetString.HasValue(result)) { }
    }

    private static void GetWrapped()
    {
    }
    #endregion

    private const bool NoIndent = false;
    private const bool NoNewLine = false;
  }
}
