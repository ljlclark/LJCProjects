// Copyright (c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// TestHTMLBuilder.cs
using LJCNetCommon;
using System;

namespace TestHTMLBuilder
{
  internal class HTMLBuilderTest
  {
    // Performs the tests.
    private static void Main()
    {
      TestCommon = new TestCommon("HTMLBuilder");
      Console.WriteLine();
      Console.WriteLine("*** HTMLBuilder ***");

      // Simple HTML build.
      Build();

      // Methods
      AddChildIndent();
      AddIndent();
      EndsWithNewLine();
      StartWithNewLine();
      HasText();

      // Text Methods
      AddLine();
      AddText();
      Line();
      Text();
      GetAttribs();
      GetIndented();
      GetIndentString();
      GetLine();
      GetText();
      GetWrapped();

      // Element Methods
      BeginHTML();  // HTML Element
      CreateHTML();  // HTML Element
      EndHTML();  // HTML Element
      GetBegin();
      GetBeginSelector();
      GetCreate();
      GetEnd();

      // Create Element Methods
      Link();
      Meta();
      Metas();
      Script();
      GetLink();
      GetMeta();
      GetMetas();
      GetScript();

      // HTML Methods
      HTMLBegin();
      GetHTMLBegin();
      GetHTMLEnd();
      GetHTMLHead();

      // Get Attribs Methods
      Attribs();
      StartAttribs();
      StartAttribsXML();
      TableAttribs();

      Console.WriteLine();
      Console.WriteLine("Press any key to continue . . .");
      Console.ReadKey();
    }

    // Simple HTML build.
    public static void Build()
    {
      // Root Method Begin
      var textState = new TextState();

      // Defaults: IndentCharCount = 2, LineLimit = 80, WrapEnabled = false.
      var hb = new HTMLBuilder();

      var copyright = new string[]
      {
        "Copyright (c) Lester J. Clark and Contributors",
        "Licensed under the MIT License.",
      };
      var fileName = "TestHTMLBuilderOutput.html";

      // Creates the HTML beginning up to and including <head>.
      hb.HTMLBegin(textState, copyright, fileName);
      // Add head items here.
      // Appends the element end tag.
      hb.End("head", textState);

      // Appends the element begin tag.
      hb.Begin("body", textState, addIndent: false);
      // Use AddChildIndent after beginning an element.
      hb.AddChildIndent(" ", textState);

      // Gets the HTML end <body> and <html>.
      var text = hb.GetHTMLEnd(textState);
      // The "Get" method has already applied the indent.
      var addIndent = false;
      // Adds potentially modified text to the builder.
      hb.Text(text, addIndent);
      var result = hb.ToString();

      var b = new HTMLBuilder();
      b.AddLine("<!DOCTYPE html>");
      b.AddLine("<!-- Copyright (c) Lester J. Clark and Contributors -->");
      b.AddLine("<!-- Licensed under the MIT License. -->");
      b.AddLine("<!-- TestHTMLBuilderOutput.html -->");
      //b.AddLine("<html lang=\"en\" xmlns=\"http://www.w3.org/1999/xhtml\">");
      b.AddLine("<html lang=\"en\">");
      b.AddLine("<head>");
      b.AddLine("</head>");
      b.AddLine("<body>");
      b.AddLine("</body>");
      b.AddText("</html>");
      var compare = b.ToString();
      TestCommon.Write("Build()", result, compare);
    }

    #region Methods

    private static void AddChildIndent()
    {
      // Root Method Begin
      var textState = new TextState();

      var result = CustomBegin("body", textState);

      var compare = "<body>";
      TestCommon.Write("AddChildIndent()", result, compare);
    }

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

      // Adds potentially modified text to the builder.
      hb.Text("This text is indented.");

      // No Indent
      var addIndent = false;
      hb.Text("Not indented.", addIndent);

      // Do not start a newline.
      hb.AddText(" No start with newline.");
      var result = hb.ToString();

      var b = new HTMLBuilder();
      b.AddLine("This text is not indented.");
      b.AddLine("  This text is indented.");
      b.AddText("Not indented. No start with newline.");
      var compare = b.ToString();
      TestCommon.Write("AddIndent()", result, compare);
    }

    private static bool EndsWithNewLine()
    {
      // Defaults: IndentCharCount = 2, LineLimit = 80, WrapEnabled = false.
      var hb = new HTMLBuilder();

      // Example Method:
      // Indicates if the builder text ends with a newline.
      bool retValue = hb.EndsWithNewLine();
      var result = retValue.ToString();

      var b = new HTMLBuilder();
      b.AddText("False");
      var compare = b.ToString();
      TestCommon.Write("EndsWithNewLine()", result, compare);
      return retValue;
    }

    private static bool StartWithNewLine()
    {
      // Defaults: IndentCharCount = 2, LineLimit = 80, WrapEnabled = false.
      var hb = new HTMLBuilder();

      // Example Method:
      // Checks if text can start with a newline.
      bool retValue = hb.StartWithNewLine(true);
      var result = retValue.ToString();

      var b = new HTMLBuilder();
      b.AddText("False");
      var compare = b.ToString();
      TestCommon.Write("StartWithNewLine()", result, compare);
      return retValue;
    }

    private static bool HasText()
    {
      // Defaults: IndentCharCount = 2, LineLimit = 80, WrapEnabled = false.
      var hb = new HTMLBuilder();

      // Example Method:
      // Adds text without modification.
      hb.AddText("Some Text.");

      // Indicates if the builder has text.
      var retValue = hb.HasText();
      var result = retValue.ToString();

      var b = new HTMLBuilder();
      b.AddText("True");
      var compare = b.ToString();
      TestCommon.Write("HasText()", result, compare);
      return retValue;
    }
    #endregion

    #region Text Methods

    private static void AddLine()
    {
      // Defaults: IndentCharCount = 2, LineLimit = 80, WrapEnabled = false.
      var hb = new HTMLBuilder();

      // Example Method:
      // Adds a text line without modification.
      hb.AddLine("This is an appended line.");

      // Adds text without modification.
      hb.AddText(":");
      var result = hb.ToString();

      var b = new HTMLBuilder();
      b.AddLine("This is an appended line.");
      b.AddText(":");
      var compare = b.ToString();
      TestCommon.Write("AddLine()", result, compare);
    }

    private static void AddText()
    {
      // Defaults: IndentCharCount = 2, LineLimit = 80, WrapEnabled = false.
      var hb = new HTMLBuilder();

      // Example Method:
      // Adds text without modification.
      hb.AddText("This is some appended text.");
      var result = hb.ToString();

      var compare = "This is some appended text.";
      TestCommon.Write("AddText()", result, compare);
    }

    private static void Line()
    {
      // Defaults: IndentCharCount = 2, LineLimit = 80, WrapEnabled = false.
      var hb = new HTMLBuilder();

      // Adds potentially modified text to the builder.
      hb.Text("This is an appended line.");

      // Changes the IndentCount by the provided value.
      hb.AddIndent();

      // Example Method:
      // Starts the text with a newline if the builder already has text
      // and param allowNewLine = true and builder text does not end with
      // a newline.
      // The text begins with the current indent string if param
      // addIndent = true.
      // Ends the text with a newline.

      // Adds a modified text line to the builder.
      // Defaults: addIndent = true, allowNewLine = true.
      hb.Line();

      hb.Text("This is an indented line.");
      var result = hb.ToString();

      var b = new HTMLBuilder();
      b.AddLine("This is an appended line.");
      b.AddLine();
      b.AddText("  This is an indented line.");
      var compare = b.ToString();
      TestCommon.Write("Line()", result, compare);
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
      // and param allowNewLine = true and builder text does not end with
      // a newline.
      // The text begins with the current indent string if param
      // addIndent = true.

      // Adds potentially modified text to the builder.
      // Defaults: addIndent = true, allowNewLine = true.
      hb.Text("This is an indented line.");
      var result = hb.ToString();

      var b = new HTMLBuilder();
      b.AddLine("This is an appended line.");
      b.AddText("  This is an indented line.");
      var compare = b.ToString();
      TestCommon.Write("Text()", result, compare);
    }

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
      // Appends the element begin tag.
      hb.Begin("div", textState, attribs);
      // Appends the element end tag.
      hb.End("div", textState);
      var result = hb.ToString();

      var b = new HTMLBuilder();
      b.AddLine("<div class=\"Selector\">");
      b.AddText("</div>");
      var compare = b.ToString();
      TestCommon.Write("GetAttribs()", result, compare);
    }

    private static void GetIndented()
    {
      // Defaults: IndentCharCount = 2, LineLimit = 80, WrapEnabled = false.
      var hb = new HTMLBuilder();

      // Example Method:
      // Gets a new potentially indented line.
      var result = hb.GetIndented("This text is NOT indented.");
      // Adds text without modification.
      hb.AddText(result);

      // Changes the IndentCount by the provided value.
      hb.AddIndent(2);
      // Adds a text line without modification.
      hb.AddLine();
      result = hb.GetIndented("This text is indented.");
      // Adds potentially modified text to the builder.
      var addIndent = false;
      hb.Text(result, addIndent);
      result = hb.ToString();

      var b = new HTMLBuilder();
      b.AddLine("This text is NOT indented.");
      b.AddText("    This text is indented.");
      var compare = b.ToString();
      TestCommon.Write("GetIndented()", result, compare);
    }

    private static void GetIndentString()
    {
      // Defaults: IndentCharCount = 2, LineLimit = 80, WrapEnabled = false.
      var hb = new HTMLBuilder();

      // Changes the IndentCount by the provided value.
      hb.AddIndent(1);

      // Example Method:
      // Returns the current indent string.
      var result = hb.GetIndentString();
      var addIndent = false;
      // Adds potentially modified text to the builder.
      hb.Text(result, addIndent);

      // Adds text without modification.
      hb.AddText("  :");
      result = hb.ToString();

      var compare = "  :";
      TestCommon.Write("GetIndentString()", result, compare);
    }

    private static void GetLine()
    {
      // Defaults: IndentCharCount = 2, LineLimit = 80, WrapEnabled = false.
      var hb = new HTMLBuilder();

      // Adds text without modification.
      hb.AddText("This is an appended line.");

      // Changes the IndentCount by the provided value.
      hb.AddIndent();

      // Example Method:
      // Starts the text with a newline if the builder already has text
      // and param allowNewLine = true and builder text does not end with
      // a newline.
      // The text begins with the current indent string if param
      // addIndent = true.
      // Ends the text with a newline.

      // Gets a modified text line.
      // Defaults: addIndent = true, allowNewLine = true.
      var text = hb.GetLine();
      // Adds potentially modified text to the builder.
      var addIndent = false;
      hb.Text(text, addIndent);

      hb.Text(":");
      var result = hb.ToString();

      var b = new HTMLBuilder();
      b.AddLine("This is an appended line.");
      b.AddText("  :");
      var compare = b.ToString();
      TestCommon.Write("GetLine()", result, compare);
    }

    private static void GetText()
    {
      // Defaults: IndentCharCount = 2, LineLimit = 80, WrapEnabled = false.
      var hb = new HTMLBuilder();

      // Adds potentially modified text to the builder.
      hb.Text("This is an appended line.");

      // Changes the IndentCount by the provided value.
      hb.AddIndent();

      // Example Method:
      // Starts the text with a newline if the builder already has text
      // and param allowNewLine = true and builder text does not end with
      // a newline.
      // The text begins with the current indent string if param
      // addIndent = true.

      // Gets potentially indented and wrapped text.
      // Defaults: addIndent = true, allowNewLine = true.
      var text = hb.GetText("This is an indented line.");
      // Adds text without modification.
      hb.AddText(text);
      var result = hb.ToString();

      var b = new HTMLBuilder();
      b.AddLine("This is an appended line.");
      b.AddText("  This is an indented line.");
      var compare = b.ToString();
      TestCommon.Write("GetText()", result, compare);
    }

    private static void GetWrapped()
    {
      // Defaults: IndentCharCount = 2, LineLimit = 80, WrapEnabled = false.
      var hb = new HTMLBuilder()
      {
        WrapEnabled = true
      };

      // Example Method:
      var b = new HTMLBuilder();
      // Adds text without modification.
      b.AddText("Now is the time for all good men to come to the aid of their");
      b.AddText(" country.");
      b.AddText(" Now is the time for all good men to come to the aid of their");
      b.AddText(" country.");
      var text = b.ToString();

      // Appends added text and new wrapped line if combined line > LineLimit.
      var result = hb.GetWrapped(text);

      b = new HTMLBuilder();
      b.AddText("Now is the time for all good men to come to the aid of");
      b.AddLine(" their country. Now is the");
      b.AddText("time for all good men to come to the aid of their country.");
      var compare = b.ToString();
      TestCommon.Write("GetWrapped()", result, compare);
    }
    #endregion

    #region Element Methods

    private static void BeginHTML()
    {
      // Root Method Begin
      var textState = new TextState();

      // Defaults: IndentCharCount = 2, LineLimit = 80, WrapEnabled = false.
      var hb = new HTMLBuilder(textState);

      // Creates the HTML element attributes.
      var attribs = hb.StartAttribs();

      // Example Method:
      // Appends the element begin tag.
      // Starts the text with a newline if the builder already has text
      // and param allowNewLine = true and builder text does not end with
      // a newline.
      // The text begins with the current indent string if param
      // addIndent = true.

      // Appends the element begin tag.
      // Defaults: addIndent = true, allowNewLine = true.
      // *** Change ***
      var addIndent = false;
      hb.Begin("html", textState, attribs, addIndent);

      // Appends the element end tag.
      hb.End("html", textState);
      var result = hb.ToString();

      var b = new HTMLBuilder();
      //b.AddLine("<html lang=\"en\" xmlns=\"http://www.w3.org/1999/xhtml\">");
      b.AddLine("<html lang=\"en\">");
      b.AddText("</html>");
      var compare = b.ToString();
      TestCommon.Write("Begin()", result, compare);
    }

    private static void CreateHTML()
    {
      // Root Method Begin
      var textState = new TextState();

      // Defaults: IndentCharCount = 2, LineLimit = 80, WrapEnabled = false.
      var hb = new HTMLBuilder(textState);

      // Example Method:
      // Creates the HTML element attributes.
      var attribs = hb.StartAttribs();

      // Appends an element.
      // *** Change ***
      var addIndent = false;
      // Defaults: close = true.
      hb.Create("html", null, textState, attribs, addIndent);
      var result = hb.ToString();

      var b = new HTMLBuilder();
      b.AddText("<html lang=\"en\">");
      //b.AddText(" xmlns=\"http://www.w3.org/1999/xhtml\">");
      b.AddText("</html>");
      var compare = b.ToString();
      TestCommon.Write("Create()", result, compare);
    }

    private static void EndHTML()
    {
      // Root Method Begin
      var textState = new TextState();

      // Defaults: IndentCharCount = 2, LineLimit = 80, WrapEnabled = false.
      var hb = new HTMLBuilder(textState);

      // Example Method:
      // Gets the element end tag.
      var result = hb.GetEnd("html", textState);

      var compare = "</html>";
      TestCommon.Write("End()", result, compare);
    }

    private static string CustomBegin(string name, TextState textState
      , Attributes attribs = null, bool addIndent = true
      , bool childIndent = true)
    {
      // Defaults: IndentCharCount = 2, LineLimit = 80, WrapEnabled = false.
      var hb = new HTMLBuilder(textState);

      // Gets the element begin tag.
      var createText = hb.GetBegin(name, textState, attribs, addIndent
        , childIndent);
      // Adds potentially modified text to the builder.
      var indent = false;
      hb.Text(createText, indent);

      // Use AddChildIndent after beginning an element.
      hb.AddChildIndent(createText, textState);

      var result = hb.ToString();
      return result;
    }

    private static void GetBegin()
    {
      // Root Method Begin
      var textState = new TextState();

      // Defaults: IndentCharCount = 2, LineLimit = 80, WrapEnabled = false.
      var hb = new HTMLBuilder();

      // Example Method:
      // Gets the element begin tag.
      // Starts the text with a newline if the builder already has text
      // and param allowNewLine = true and builder text does not end with
      // a newline.
      // The text begins with the current indent string if param
      // addIndent = true.

      // Gets the element begin tag.
      // Defaults: addIndent = true, allowNewLine = true.
      var result = hb.GetBegin("body", textState);

      var compare = "<body>";
      TestCommon.Write("GetBegin()", result, compare);
    }

    private static void GetBeginSelector()
    {
      // Root Method Begin
      var textState = new TextState();

      // Defaults: IndentCharCount = 2, LineLimit = 80, WrapEnabled = false.
      var hb = new HTMLBuilder();

      // Example Method:
      // Gets beginning of style selector.
      // Starts the text with a newline if the builder already has text
      // and param allowNewLine = true and builder text does not end with
      // a newline.
      // The text begins with the current indent string if param
      // addIndent = true.

      // Gets beginning of style selector.
      // Defaults: addIndent = true, allowNewLine = true.
      var result = hb.GetBeginSelector("tr", textState);

      var compare = "tr {";
      TestCommon.Write("GetBeginSelector()", result, compare);
    }

    private static void GetCreate()
    {
      // Root Method Begin
      var textState = new TextState();

      // Defaults: IndentCharCount = 2, LineLimit = 80, WrapEnabled = false.
      var hb = new HTMLBuilder(textState);

      // Example Method:
      // Gets the element text.
      // Defaults: close = true.
      var result = hb.GetCreate("head", null, textState);

      var compare = "<head></head>";
      TestCommon.Write("GetCreate()", result, compare);
    }

    private static void GetEnd()
    {
      // Root Method Begin
      var textState = new TextState();

      // Defaults: IndentCharCount = 2, LineLimit = 80, WrapEnabled = false.
      var hb = new HTMLBuilder(textState);

      // Example Method:
      // Gets the element end tag.
      // Defaults: close = true.
      var result = hb.GetEnd("head", textState);

      var compare = "</head>";
      TestCommon.Write("GetEnd()", result, compare);
    }
    #endregion

    #region Create Element Methods

    private static void Link()
    {
      // Root Method Begin
      var textState = new TextState();

      // Defaults: IndentCharCount = 2, LineLimit = 80, WrapEnabled = false.
      var hb = new HTMLBuilder(textState);

      // Example Method:
      // Appends a <link> element for a style sheet.
      hb.Link("File.css", textState);
      var result = hb.ToString();

      var b = new HTMLBuilder();
      b.AddText("<link rel=\"stylesheet\" type=\"text/css\"");
      b.AddText(" href=\"File.css\" />");
      var compare = b.ToString();
      TestCommon.Write("Link()", result, compare);
    }

    private static void Meta()
    {
      // Root Method Begin
      var textState = new TextState();

      // Defaults: IndentCharCount = 2, LineLimit = 80, WrapEnabled = false.
      var hb = new HTMLBuilder(textState);

      // Example Method:
      var content = "width=device-width initial-scale=1";

      // Appends a <meta> element.
      hb.Meta("viewport", content, textState);
      var result = hb.ToString();

      var b = new HTMLBuilder();
      b.AddText("<meta name=\"viewport\"");
      b.AddText($" content=\"{content}\" />");
      var compare = b.ToString();
      TestCommon.Write("Meta()", result, compare);
    }

    private static void Metas()
    {
      // Root Method Begin
      var textState = new TextState();

      // Defaults: IndentCharCount = 2, LineLimit = 80, WrapEnabled = false.
      var hb = new HTMLBuilder(textState);

      // Example Method:
      // Appends common <meta> elements.
      hb.Metas("Mr. Smith", textState, "The Description");
      var result = hb.ToString();

      var b = new HTMLBuilder();
      b.AddLine("<meta charset=\"utf-8\" />");
      b.AddLine("<meta name=\"description\" content=\"The Description\" />");
      b.AddLine("<meta name=\"author\" content=\"Mr. Smith\" />");
      var content = "width=device-width initial-scale=1";
      b.AddText($"<meta name=\"viewport\" content=\"{content}\" />");
      var compare = b.ToString();
      TestCommon.Write("Metas()", result, compare);
    }

    private static void Script()
    {
      // Root Method Begin
      var textState = new TextState();

      // Defaults: IndentCharCount = 2, LineLimit = 80, WrapEnabled = false.
      var hb = new HTMLBuilder(textState);

      // Example Method:
      // Appends a <script> element for a script file.
      hb.Script("File.js", textState);
      var result = hb.ToString();

      var compare = "<script src=\"File.js\"></script>";
      TestCommon.Write("Script()", result, compare);
    }

    private static void GetLink()
    {
      // Root Method Begin
      var textState = new TextState();

      // Defaults: IndentCharCount = 2, LineLimit = 80, WrapEnabled = false.
      var hb = new HTMLBuilder(textState);

      // Example Method:
      // Gets the <link> element for a style sheet.
      var result = hb.GetLink("File.css", textState);

      var b = new HTMLBuilder();
      b.AddText("<link rel=\"stylesheet\" type=\"text/css\"");
      b.AddText(" href=\"File.css\" />");
      var compare = b.ToString();
      TestCommon.Write("GetLink()", result, compare);
    }

    private static void GetMeta()
    {
      // Root Method Begin
      var textState = new TextState();

      // Defaults: IndentCharCount = 2, LineLimit = 80, WrapEnabled = false.
      var hb = new HTMLBuilder(textState);

      // Example Method:
      var content = "width=device-width initial-scale=1";

      // Gets a <meta> element.
      var result = hb.GetMeta("viewport", content, textState);

      var b = new HTMLBuilder();
      b.AddText("<meta name=\"viewport\"");
      b.AddText($" content=\"{content}\" />");
      var compare = b.ToString();
      TestCommon.Write("GetMeta()", result, compare);
    }

    private static void GetMetas()
    {
      // Root Method Begin
      var textState = new TextState();

      // Defaults: IndentCharCount = 2, LineLimit = 80, WrapEnabled = false.
      var hb = new HTMLBuilder(textState);

      // Example Method:
      // Gets common <meta> elements.
      var result = hb.GetMetas("Mr. Smith", textState, "The Description");

      var b = new HTMLBuilder();
      b.AddLine("<meta charset=\"utf-8\" />");
      b.AddLine("<meta name=\"description\" content=\"The Description\" />");
      b.AddLine("<meta name=\"author\" content=\"Mr. Smith\" />");
      var content = "width=device-width initial-scale=1";
      b.AddText($"<meta name=\"viewport\" content=\"{content}\" />");
      var compare = b.ToString();
      TestCommon.Write("GetMetas()", result, compare);
    }

    private static void GetScript()
    {
      // Root Method Begin
      var textState = new TextState();

      // Defaults: IndentCharCount = 2, LineLimit = 80, WrapEnabled = false.
      var hb = new HTMLBuilder(textState);

      // Example Method:
      // Gets the <script> element.
      var result = hb.GetScript("File.js", textState);

      var compare = "<script src=\"File.js\"></script>";
      TestCommon.Write("GetScript()", result, compare);
    }
    #endregion

    #region HTML Methods

    private static void HTMLBegin()
    {
      // Root Method Begin
      var textState = new TextState();

      // Defaults: IndentCharCount = 2, LineLimit = 80, WrapEnabled = false.
      var hb = new HTMLBuilder(textState);

      // Example Method:
      var copyright = new string[]
      {
        "Copyright (c) First Line",
        "Second Line",
      };
      var fileName = "File.html";

      // Creates the HTML beginning up to and including <head>.
      hb.HTMLBegin(textState, copyright, fileName);
      var result = hb.ToString();

      var b = new HTMLBuilder();
      b.AddLine("<!DOCTYPE html>");
      b.AddLine("<!-- Copyright (c) First Line -->");
      b.AddLine("<!-- Second Line -->");
      b.AddLine("<!-- File.html -->");
      //b.AddLine("<html lang=\"en\" xmlns=\"http://www.w3.org/1999/xhtml\">");
      b.AddLine("<html lang=\"en\">");
      b.AddText("<head>");
      var compare = b.ToString();
      TestCommon.Write("HTMLBegin()", result, compare);
    }

    private static void GetHTMLBegin()
    {
      // Root Method Begin
      var textState = new TextState();

      // Defaults: IndentCharCount = 2, LineLimit = 80, WrapEnabled = false.
      var hb = new HTMLBuilder(textState);

      // Example Method:
      var copyright = new string[]
      {
        "Copyright (c) First Line",
        "Second Line",
      };
      var fileName = "File.html";

      // Gets the HTML beginning up to <head>.
      var result = hb.GetHTMLBegin(textState, copyright, fileName);

      var b = new HTMLBuilder();
      b.AddLine("<!DOCTYPE html>");
      b.AddLine("<!-- Copyright (c) First Line -->");
      b.AddLine("<!-- Second Line -->");
      b.AddLine("<!-- File.html -->");
      //b.AddLine("<html lang=\"en\" xmlns=\"http://www.w3.org/1999/xhtml\">");
      b.AddLine("<html lang=\"en\">");
      b.AddText("<head>");
      var compare = b.ToString();
      TestCommon.Write("GetHTMLBegin()", result, compare);
    }

    private static void GetHTMLEnd()
    {
      // Root Method Begin
      var textState = new TextState();

      // Defaults: IndentCharCount = 2, LineLimit = 80, WrapEnabled = false.
      var hb = new HTMLBuilder(textState);

      // Example Method:
      // Gets the HTML end <body> and <html>.
      var result = hb.GetHTMLEnd(textState);

      var b = new HTMLBuilder();
      b.AddLine("</body>");
      b.AddText("</html>");
      var compare = b.ToString();
      TestCommon.Write("GetHTMLEnd()", result, compare);
    }

    private static void GetHTMLHead()
    {
      // Root Method Begin
      var textState = new TextState();

      // Defaults: IndentCharCount = 2, LineLimit = 80, WrapEnabled = false.
      var hb = new HTMLBuilder(textState);

      // Example Method:
      var title = "The Title";
      var author = "Mr. Smith";
      var description = "The Description";

      // Gets the main HTML Head elements.
      // Defaults: title = null, author = null, description = null.
      var result = hb.GetHTMLHead(textState, title, author, description);

      var b = new HTMLBuilder();
      b.AddLine("<title>The Title</title>");
      b.AddLine("<meta charset=\"utf-8\" />");
      b.AddLine("<meta name=\"description\" content=\"The Description\" />");
      b.AddLine("<meta name=\"author\" content=\"Mr. Smith\" />");
      b.AddText("<meta name=\"viewport\" content=\"width=device-width initial-scale=1\" />");
      var compare = b.ToString();
      TestCommon.Write("GetHTMLHead()", result, compare);
    }
    #endregion

    #region Get Attribs Methods

    public static void Attribs()
    {
      // Root Method Begin
      var textState = new TextState();

      // Defaults: IndentCharCount = 2, LineLimit = 80, WrapEnabled = false.
      var hb = new HTMLBuilder(textState);

      // Example Method:
      var className = "className";
      var id = "id";

      // Gets common element attributes.
      Attributes attribs = hb.Attribs(className, id);

      var result = hb.GetAttribs(attribs, textState);
      var compare = " id=\"id\" class=\"className\"";
      TestCommon.Write("Attribs()", result, compare);
    }

    public static void StartAttribs()
    {
      // Root Method Begin
      var textState = new TextState();

      // Defaults: IndentCharCount = 2, LineLimit = 80, WrapEnabled = false.
      var hb = new HTMLBuilder(textState);

      // Example Method:
      // Creates the HTML element attributes.
      var attribs = hb.StartAttribs();

      var result = hb.GetAttribs(attribs, textState);

      var compare = " lang=\"en\"";
      TestCommon.Write("StartAttribs()", result, compare);
    }

    public static void StartAttribsXML()
    {
      // Root Method Begin
      var textState = new TextState();

      // Defaults: IndentCharCount = 2, LineLimit = 80, WrapEnabled = false.
      var hb = new HTMLBuilder(textState);

      // Example Method:
      // Creates the HTML element attributes.
      var attribs = hb.StartXMLAttribs();

      var result = hb.GetAttribs(attribs, textState);

      var b = new HTMLBuilder();
      b.AddLine(" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"");
      b.AddText(" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"");
      var compare = b.ToString();
      TestCommon.Write("StartAttribs()", result, compare);
    }

    public static void TableAttribs()
    {
      // Root Method Begin
      var textState = new TextState();

      // Defaults: IndentCharCount = 2, LineLimit = 80, WrapEnabled = false.
      var hb = new HTMLBuilder(textState);

      // Example Method:
      var border = 1;
      var cellspacing = 2;
      var cellpadding = 3;

      // Gets common table attributes.
      // Defaults: border = 1, cellspacing = 0, cellpadding = 2. 
      var attribs = hb.TableAttribs(border, cellspacing, cellpadding);

      var result = hb.GetAttribs(attribs, textState);

      var compare = " border=\"1\" cellspacing=\"2\" cellpadding=\"3\"";
      TestCommon.Write("TableAttribs()", result, compare);
    }
    #endregion

    #region Class Data

    private static TestCommon TestCommon { get; set; }
    #endregion
  }
}
