// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// HTMLBuilderTest.cs
using LJCNetCommon;
using System;

namespace LJCNetCommonTest
{
  internal class HTMLBuilderTest
  {
    #region Methods

    // Performs the tests.
    internal static void Test()
    {
      Console.WriteLine();
      Console.WriteLine("*** HTMLBuilder ***");

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

      // Append Element Methods
      Begin();
      Create();
      End();
      Link();
      Meta();
      Metas();
      Script();

      // Get Element Methods
      AddChildIndent();
      GetBegin();
      GetBeginSelector();
      GetCreate();
      GetEnd();
      GetLink();
      GetMeta();
      GetMetas();
      GetScript();

      // Append HTML Methods
      HTMLBegin();

      // Get HTML Methods
      GetHTMLBegin();
      GetHTMLEnd();
      GetHTMLHead();

      // Get Attribs Methods
      Attribs();
      StartAttribs();
      TableAttribs();
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

      var b = new HTMLBuilder();
      b.AddLine("This text is not indented.");
      b.AddLine("  This text is indented.");
      b.AddText("Not indented.  No start with newline.");
      var compare = b.ToString();
      if (result != compare)
      {
        Console.WriteLine("\r\nHTMLBuilder.AddIndent()");
        Console.WriteLine(result);
        Console.WriteLine(" !=");
        Console.WriteLine(compare);
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

      var b = new HTMLBuilder();
      b.AddLine("This is an appended line.");
      b.AddText(":");
      var compare = b.ToString();
      if (result != compare)
      {
        Console.WriteLine("\r\nHTMLBuilder.AddLine()");
        Console.WriteLine(result);
        Console.WriteLine(" !=");
        Console.WriteLine(compare);
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
        Console.WriteLine("\r\nHTMLBuilder.AddText()");
        Console.WriteLine(result);
        Console.WriteLine(" !=");
        Console.WriteLine(compare);
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

      var b = new HTMLBuilder();
      b.AddLine("This is an appended line.");
      b.AddLine();
      b.AddLine();
      b.AddText("  This is an indented line.");
      var compare = b.ToString();
      if (result != compare)
      {
        Console.WriteLine("\r\nHTMLBuilder.Line()");
        Console.WriteLine(result);
        Console.WriteLine(" !=");
        Console.WriteLine(compare);
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

      var b = new HTMLBuilder();
      b.AddLine("This is an appended line.");
      b.AddText("  This is an indented line.");
      var compare = b.ToString();
      if (result != compare)
      {
        Console.WriteLine("\r\nHTMLBuilder.Text()");
        Console.WriteLine(result);
        Console.WriteLine(" !=");
        Console.WriteLine(compare);
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

      var b = new HTMLBuilder();
      b.AddLine("<div class=\"Selector\">");
      b.AddText("</div>");
      var compare = b.ToString();
      if (result != compare)
      {
        Console.WriteLine("\r\nHTMLBuilder.GetAttribs()");
        Console.WriteLine(result);
        Console.WriteLine(" !=");
        Console.WriteLine(compare);
      }
    }

    private static void GetIndented()
    {
      // Defaults: IndentCharCount = 2, LineLimit = 80, WrapEnabled = false.
      var hb = new HTMLBuilder();

      // Example Method:
      var result = hb.GetIndented("This text is NOT indented.");
      hb.AddText(result);

      // The builder keeps track of the current number of indents.
      hb.AddIndent(2);
      hb.AddLine();
      result = hb.GetIndented("This text is indented.");
      hb.AddText(result);
      result = hb.ToString();

      // result:
      // This text is NOT indented.
      //     This text is indented.

      var b = new HTMLBuilder();
      b.AddLine("This text is NOT indented.");
      b.AddText("    This text is indented.");
      var compare = b.ToString();
      if (result != compare)
      {
        Console.WriteLine("\r\nHTMLBuilder.GetIndented()");
        Console.WriteLine(result);
        Console.WriteLine(" !=");
        Console.WriteLine(compare);
      }
    }

    private static void GetIndentString()
    {
      // Defaults: IndentCharCount = 2, LineLimit = 80, WrapEnabled = false.
      var hb = new HTMLBuilder();

      hb.AddIndent(1);

      // Example Method:
      var result = hb.GetIndentString();
      hb.AddText(result);

      hb.AddText(":");
      result = hb.ToString();

      // result:
      //   :

      var compare = "  :";
      if (result != compare)
      {
        Console.WriteLine("\r\nHTMLBuilder.GetIndentString()");
        Console.WriteLine(result);
        Console.WriteLine(" !=");
        Console.WriteLine(compare);
      }
    }

    private static void GetLine()
    {
      // Defaults: IndentCharCount = 2, LineLimit = 80, WrapEnabled = false.
      var hb = new HTMLBuilder();

      hb.AddText("This is an appended line.");

      // The builder keeps track of the current number of indents.
      hb.AddIndent();

      // Example Method:
      // Starts the text with a newline if the builder already has text
      // and param allowNewLine = true.
      // The text begins with the current indent string if param
      // addIndent = true.
      // Ends the text with a newline.
      // Defaults: addIndent = true, allowNewLine = true.
      var text = hb.GetLine();
      hb.AddText(text);

      hb.AddText(":");
      var result = hb.ToString();

      // result:
      // This is an appended line.
      //
      // :

      var b = new HTMLBuilder();
      b.AddLine("This is an appended line.");
      b.AddLine();
      b.AddText(":");
      var compare = b.ToString();
      if (result != compare)
      {
        Console.WriteLine("\r\nHTMLBuilder.GetLine()");
        Console.WriteLine(result);
        Console.WriteLine(" !=");
        Console.WriteLine(compare);
      }
    }

    private static void GetText()
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
      var text = hb.GetText("This is an indented line.");
      hb.AddText(text);
      var result = hb.ToString();

      // result:
      // This is an appended line.
      //   This is an indented line.

      var b = new HTMLBuilder();
      b.AddLine("This is an appended line.");
      b.AddText("  This is an indented line.");
      var compare = b.ToString();
      if (result != compare)
      {
        Console.WriteLine("\r\nHTMLBuilder.GetText()");
        Console.WriteLine(result);
        Console.WriteLine(" !=");
        Console.WriteLine(compare);
      }
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
      b.AddText("Now is the time for all good men to come to the aid of their");
      b.AddText(" country.");
      b.AddText(" Now is the time for all good men to come to the aid of their");
      b.AddText(" country.");
      var text = b.ToString();
      var result = hb.GetWrapped(text);

      // result:
      // Now is the time for all good men to come to the aid of
      // their country.

      b = new HTMLBuilder();
      b.AddText("Now is the time for all good men to come to the aid of");
      b.AddLine(" their country. Now is the");
      b.AddText("time for all good men to come to the aid of their country.");
      var compare = b.ToString();
      if (result != compare)
      {
        Console.WriteLine("\r\nHTMLBuilder.GetWrapped()");
        Console.WriteLine(result);
        Console.WriteLine(" !=");
        Console.WriteLine(compare);
      }
    }
    #endregion

    #region Append Element Methods

    private static void Begin()
    {
      // Root Method Begin
      var textState = new TextState();

      // Defaults: IndentCharCount = 2, LineLimit = 80, WrapEnabled = false.
      var hb = new HTMLBuilder(textState);

      var attribs = hb.StartAttribs();

      // Example Method:
      // Starts the text with a newline if the builder already has text
      // and param allowNewLine = true.
      // The text begins with the current indent string if param
      // addIndent = true.
      // Defaults: addIndent = true, allowNewLine = true.
      hb.Begin("html", textState, attribs);

      hb.End("html", textState);
      var result = hb.ToString();

      // result:
      // <html lang=\"en\" xmlns=\"http://www.w3.org/1999/xhtml\">
      // </html>

      var b = new HTMLBuilder();
      b.AddLine("<html lang=\"en\" xmlns=\"http://www.w3.org/1999/xhtml\">");
      b.AddText("</html>");
      var compare = b.ToString();
      if (result != compare)
      {
        Console.WriteLine("\r\nHTMLBuilder.Begin()");
        Console.WriteLine(result);
        Console.WriteLine(" !=");
        Console.WriteLine(compare);
      }
    }

    private static void Create()
    {
      // Root Method Begin
      var textState = new TextState();

      // Defaults: IndentCharCount = 2, LineLimit = 80, WrapEnabled = false.
      var hb = new HTMLBuilder(textState);

      // Example Method:
      var attribs = hb.StartAttribs();
      // Defaults: close = true.
      hb.Create("html", null, textState, attribs);
      var result = hb.ToString();

      // result:
      // <html lang="en" xmlns="http://www.w3.org/1999/xhtml"></html>

      var b = new HTMLBuilder();
      b.AddText("<html lang=\"en\"");
      b.AddText(" xmlns=\"http://www.w3.org/1999/xhtml\">");
      b.AddText("</html>");
      var compare = b.ToString();
      if (result != compare)
      {
        Console.WriteLine("\r\nHTMLBuilder.Create()");
        Console.WriteLine(result);
        Console.WriteLine(" !=");
        Console.WriteLine(compare);
      }
    }

    private static void End()
    {
      // Root Method Begin
      var textState = new TextState();

      // Defaults: IndentCharCount = 2, LineLimit = 80, WrapEnabled = false.
      var hb = new HTMLBuilder(textState);

      var attribs = new Attributes()
      {
        { "name", "Someone" },
      };
      hb.Begin("Person", textState, attribs);

      // Example Method:
      var result = hb.GetEnd("html", textState);

      // result:
      // </html>

      var compare = "</html>";
      if (result != compare)
      {
        Console.WriteLine("\r\nHTMLBuilder.End()");
        Console.WriteLine(result);
        Console.WriteLine(" !=");
        Console.WriteLine(compare);
      }
    }

    private static void Link()
    {
      // Root Method Begin
      var textState = new TextState();

      // Defaults: IndentCharCount = 2, LineLimit = 80, WrapEnabled = false.
      var hb = new HTMLBuilder(textState);

      // Example Method:
      hb.Link("File.css", textState);
      var result = hb.ToString();

      // result:
      // <link rel="stylesheet" type="text/css" href="File.css" />

      var b = new HTMLBuilder();
      b.AddText("<link rel=\"stylesheet\" type=\"text/css\"");
      b.AddText(" href=\"File.css\" />");
      var compare = b.ToString();
      if (result != compare)
      {
        Console.WriteLine("\r\nHTMLBuilder.Link()");
        Console.WriteLine(result);
        Console.WriteLine(" !=");
        Console.WriteLine(compare);
      }
    }

    private static void Meta()
    {
      // Root Method Begin
      var textState = new TextState();

      // Defaults: IndentCharCount = 2, LineLimit = 80, WrapEnabled = false.
      var hb = new HTMLBuilder(textState);

      // Example Method:
      var content = "width=device-width initial-scale=1";
      hb.Meta("viewport", content, textState);
      var result = hb.ToString();

      // result:
      // <meta name="viewport" content="width=device-width initial-scale=1" />

      var b = new HTMLBuilder();
      b.AddText("<meta name=\"viewport\"");
      b.AddText($" content=\"{content}\" />");
      var compare = b.ToString();
      if (result != compare)
      {
        Console.WriteLine("\r\nHTMLBuilder.Meta()");
        Console.WriteLine(result);
        Console.WriteLine(" !=");
        Console.WriteLine(compare);
      }
    }

    private static void Metas()
    {
      // Root Method Begin
      var textState = new TextState();

      // Defaults: IndentCharCount = 2, LineLimit = 80, WrapEnabled = false.
      var hb = new HTMLBuilder(textState);

      // Example Method:
      hb.Metas("Mr. Smith", textState, "The Description");
      var result = hb.ToString();

      // result:
      // <meta charset="utf-8" />
      // <meta name="description" content="The Description" />
      // <meta name="author" content="Mr. Smith" />
      // <meta name="viewport" content="width=device-width initial-scale=1" />

      var b = new HTMLBuilder();
      b.AddLine("<meta charset=\"utf-8\" />");
      b.AddLine("<meta name=\"description\" content=\"The Description\" />");
      b.AddLine("<meta name=\"author\" content=\"Mr. Smith\" />");
      var content = "width=device-width initial-scale=1";
      b.AddText($"<meta name=\"viewport\" content=\"{content}\" />");
      var compare = b.ToString();
      if (result != compare)
      {
        Console.WriteLine("\r\nHTMLBuilder.Metas()");
        Console.WriteLine(result);
        Console.WriteLine(" !=");
        Console.WriteLine(compare);
      }
    }

    private static void Script()
    {
      // Root Method Begin
      var textState = new TextState();

      // Defaults: IndentCharCount = 2, LineLimit = 80, WrapEnabled = false.
      var hb = new HTMLBuilder(textState);

      // Example Method:
      hb.Script("File.js", textState);
      var result = hb.ToString();

      // result:
      // <script src="File.js"></script>

      var compare = "<script src=\"File.js\"></script>";
      if (result != compare)
      {
        Console.WriteLine("\r\nHTMLBuilder.Script()");
        Console.WriteLine(result);
        Console.WriteLine(" !=");
        Console.WriteLine(compare);
      }
    }
    #endregion

    #region Get Element Methods

    private static void AddChildIndent()
    {
      // Root Method Begin
      var textState = new TextState();

      var result = CustomBegin("body", textState);

      // result:
      // <body>

      var compare = "<body>";
      if (result != compare)
      {
        Console.WriteLine("\r\nHTMLBuilder.AddChildIndent()");
        Console.WriteLine(result);
        Console.WriteLine(" !=");
        Console.WriteLine(compare);
      }
    }

    private static string CustomBegin(string name, TextState textState
      , Attributes attribs = null, bool addIndent = true
      , bool childIndent = true)
    {
      // Defaults: IndentCharCount = 2, LineLimit = 80, WrapEnabled = false.
      var hb = new HTMLBuilder(textState);

      var createText = hb.GetBegin(name, textState, attribs, addIndent
        , childIndent);
      // Use NoIndent after a "GetText" method.
      hb.Text(createText, NoIndent);
      // Use AddChildIndent after beginning an element.
      hb.AddChildIndent(createText, textState);
      var result = hb.ToString();

      // Append Method
      //hb.UpdateState(textState);
      return result;
    }

    private static void GetBegin()
    {
      // Root Method Begin
      var textState = new TextState();

      // Defaults: IndentCharCount = 2, LineLimit = 80, WrapEnabled = false.
      var hb = new HTMLBuilder();

      // Example Method:
      // Starts the text with a newline if the builder already has text
      // and param allowNewLine = true.
      // The text begins with the current indent string if param
      // addIndent = true.
      // Defaults: addIndent = true, allowNewLine = true.
      var result = hb.GetBegin("body", textState);

      // result:
      // <body>

      var compare = "<body>";
      if (result != compare)
      {
        Console.WriteLine("\r\nHTMLBuilder.GetBegin()");
        Console.WriteLine(result);
        Console.WriteLine(" !=");
        Console.WriteLine(compare);
      }
    }

    private static void GetBeginSelector()
    {
      // Root Method Begin
      var textState = new TextState();

      // Defaults: IndentCharCount = 2, LineLimit = 80, WrapEnabled = false.
      var hb = new HTMLBuilder();

      // Example Method:
      // Starts the text with a newline if the builder already has text
      // and param allowNewLine = true.
      // The text begins with the current indent string if param
      // addIndent = true.
      // Defaults: addIndent = true, allowNewLine = true.
      var result = hb.GetBeginSelector("tr", textState);

      // result:
      // tr {

      var compare = "tr {";
      if (result != compare)
      {
        Console.WriteLine("\r\nHTMLBuilder.GetBeginSelector()");
        Console.WriteLine(result);
        Console.WriteLine(" !=");
        Console.WriteLine(compare);
      }
    }

    private static void GetCreate()
    {
      // Root Method Begin
      var textState = new TextState();

      // Defaults: IndentCharCount = 2, LineLimit = 80, WrapEnabled = false.
      var hb = new HTMLBuilder(textState);

      // Example Method:
      // Defaults: close = true.
      hb.Create("head", null, textState);
      var result = hb.ToString();

      // result:
      // <head></head>

      var compare = "<head></head>";
      if (result != compare)
      {
        Console.WriteLine("\r\nHTMLBuilder.GetCreate()");
        Console.WriteLine(result);
        Console.WriteLine(" !=");
        Console.WriteLine(compare);
      }
    }

    private static void GetEnd()
    {
      // Root Method Begin
      var textState = new TextState();

      // Defaults: IndentCharCount = 2, LineLimit = 80, WrapEnabled = false.
      var hb = new HTMLBuilder(textState);

      // Example Method:
      // Defaults: close = true.
      hb.End("head", textState);
      var result = hb.ToString();

      // result:
      // </head>

      var compare = "</head>";
      if (result != compare)
      {
        Console.WriteLine("\r\nHTMLBuilder.GetEnd()");
        Console.WriteLine(result);
        Console.WriteLine(" !=");
        Console.WriteLine(compare);
      }
    }

    private static void GetLink()
    {
      // Root Method Begin
      var textState = new TextState();

      // Defaults: IndentCharCount = 2, LineLimit = 80, WrapEnabled = false.
      var hb = new HTMLBuilder(textState);

      // Example Method:
      var result = hb.Link("File.css", textState);

      // result:
      // <link rel="stylesheet" type="text/css" href="File.css" />

      var b = new HTMLBuilder();
      b.AddText("<link rel=\"stylesheet\" type=\"text/css\"");
      b.AddText(" href=\"File.css\" />");
      var compare = b.ToString();
      if (result != compare)
      {
        Console.WriteLine("\r\nHTMLBuilder.GetLink()");
        Console.WriteLine(result);
        Console.WriteLine(" !=");
        Console.WriteLine(compare);
      }
    }

    private static void GetMeta()
    {
      // Root Method Begin
      var textState = new TextState();

      // Defaults: IndentCharCount = 2, LineLimit = 80, WrapEnabled = false.
      var hb = new HTMLBuilder(textState);

      // Example Method:
      var content = "width=device-width initial-scale=1";
      var result = hb.Meta("viewport", content, textState);

      // result:
      // <meta name="viewport" content="width=device-width initial-scale=1" />

      var b = new HTMLBuilder();
      b.AddText("<meta name=\"viewport\"");
      b.AddText($" content=\"{content}\" />");
      var compare = b.ToString();
      if (result != compare)
      {
        Console.WriteLine("\r\nHTMLBuilder.GetMeta()");
        Console.WriteLine(result);
        Console.WriteLine(" !=");
        Console.WriteLine(compare);
      }
    }

    private static void GetMetas()
    {
      // Root Method Begin
      var textState = new TextState();

      // Defaults: IndentCharCount = 2, LineLimit = 80, WrapEnabled = false.
      var hb = new HTMLBuilder(textState);

      // Example Method:
      var result = hb.GetMetas("Mr. Smith", textState, "The Description");

      // result:
      // <meta charset="utf-8" />
      // <meta name="description" content="The Description" />
      // <meta name="author" content="Mr. Smith" />
      // <meta name="viewport" content="width=device-width initial-scale=1" />

      var b = new HTMLBuilder();
      b.AddLine("<meta charset=\"utf-8\" />");
      b.AddLine("<meta name=\"description\" content=\"The Description\" />");
      b.AddLine("<meta name=\"author\" content=\"Mr. Smith\" />");
      var content = "width=device-width initial-scale=1";
      b.AddText($"<meta name=\"viewport\" content=\"{content}\" />");
      var compare = b.ToString();
      if (result != compare)
      {
        Console.WriteLine("\r\nHTMLBuilder.GetMetas()");
        Console.WriteLine(result);
        Console.WriteLine(" !=");
        Console.WriteLine(compare);
      }
    }

    private static void GetScript()
    {
      // Root Method Begin
      var textState = new TextState();

      // Defaults: IndentCharCount = 2, LineLimit = 80, WrapEnabled = false.
      var hb = new HTMLBuilder(textState);

      // Example Method:
      var result = hb.Script("File.js", textState);

      // result:
      // <script src="File.js"></script>

      var compare = "<script src=\"File.js\"></script>";
      if (result != compare)
      {
        Console.WriteLine("\r\nHTMLBuilder.GetScript()");
        Console.WriteLine(result);
        Console.WriteLine(" !=");
        Console.WriteLine(compare);
      }
    }
    #endregion

    #region Append HTML Methods

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
      hb.HTMLBegin(textState, copyright, fileName);
      var result = hb.ToString();

      // result:
      // <!DOCTYPE html>
      // <!-- Copyright (c) First Line -->
      // <!-- Second Line -->
      // <!-- File.html -->
      // <html lang="en" xmlns="http://www.w3.org/1999/xhtml">
      // <head>

      var b = new HTMLBuilder();
      b.AddLine("<!DOCTYPE html>");
      b.AddLine("<!-- Copyright (c) First Line -->");
      b.AddLine("<!-- Second Line -->");
      b.AddLine("<!-- File.html -->");
      b.AddLine("<html lang=\"en\" xmlns=\"http://www.w3.org/1999/xhtml\">");
      b.AddText("<head>");
      var compare = b.ToString();
      if (result != compare)
      {
        Console.WriteLine("\r\nHTMLBuilder.HTMLBegin()");
        Console.WriteLine(result);
        Console.WriteLine(" !=");
        Console.WriteLine(compare);
      }
    }
    #endregion

    #region Get HTML Methods

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
      var result = hb.GetHTMLBegin(textState, copyright, fileName);

      // result:
      // <!DOCTYPE html>
      // <!-- Copyright (c) First Line -->
      // <!-- Second Line -->
      // <!-- File.html -->
      // <html lang="en" xmlns="http://www.w3.org/1999/xhtml">
      // <head>

      var b = new HTMLBuilder();
      b.AddLine("<!DOCTYPE html>");
      b.AddLine("<!-- Copyright (c) First Line -->");
      b.AddLine("<!-- Second Line -->");
      b.AddLine("<!-- File.html -->");
      b.AddLine("<html lang=\"en\" xmlns=\"http://www.w3.org/1999/xhtml\">");
      b.AddText("<head>");
      var compare = b.ToString();
      if (result != compare)
      {
        Console.WriteLine("\r\nHTMLBuilder.GetHTMLBegin()");
        Console.WriteLine(result);
        Console.WriteLine(" !=");
        Console.WriteLine(compare);
      }
    }

    private static void GetHTMLEnd()
    {
      // Root Method Begin
      var textState = new TextState();

      // Defaults: IndentCharCount = 2, LineLimit = 80, WrapEnabled = false.
      var hb = new HTMLBuilder(textState);

      // Example Method:
      var result = hb.GetHTMLEnd(textState);

      // result:
      // </body>
      // </html>

      var b = new HTMLBuilder();
      b.AddLine("</body>");
      b.AddText("</html>");
      var compare = b.ToString();
      if (result != compare)
      {
        Console.WriteLine("\r\nHTMLBuilder.GetHTMLEnd()");
        Console.WriteLine(result);
        Console.WriteLine(" !=");
        Console.WriteLine(compare);
      }
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
      // Defaults: title = null, author = null, description = null.
      var result = hb.GetHTMLHead(textState, title, author, description);

      // result:
      // <title>The Title</title>
      // <meta charset="utf-8"
      // <meta name="description" content="The Description" />
      // <meta name="author" content="Mr. Smith" />
      // <meta name="viewport" content="width=device-width initial-scale=1" />

      var b = new HTMLBuilder();
      b.AddLine("<title>The Title</title>");
      b.AddLine("<meta charset=\"utf-8\" />");
      b.AddLine("<meta name=\"description\" content=\"The Description\" />");
      b.AddLine("<meta name=\"author\" content=\"Mr. Smith\" />");
      b.AddText("<meta name=\"viewport\" content=\"width=device-width initial-scale=1\" />");
      var compare = b.ToString();
      if (result != compare)
      {
        Console.WriteLine("\r\nHTMLBuilder.GetHTMLHead()");
        Console.WriteLine(result);
        Console.WriteLine(" !=");
        Console.WriteLine(compare);
      }
    }
    #endregion

    #region Get Attribs Methods

    public static void Attribs()
    {
      // Root Method Begin
      var textState = new TextState();

      var hb = new HTMLBuilder(textState);

      // Example Method:
      var className = "className";
      var id = "id";
      Attributes attribs = hb.Attribs(className, id);

      // result:
      // List<string>()
      // {
      //   { "className", "id" },
      // };

      var result = hb.GetAttribs(attribs, textState);
      var compare = " id=\"id\" class=\"className\"";
      if (result != compare)
      {
        Console.WriteLine("\r\nHTMLBuilder.Attribs()");
        Console.WriteLine(result);
        Console.WriteLine(" !=");
        Console.WriteLine(compare);
      }
    }

    public static void StartAttribs()
    {
      // Root Method Begin
      var textState = new TextState();

      var hb = new HTMLBuilder(textState);

      // Example Method:
      var attribs = hb.StartAttribs();
      var result = hb.GetAttribs(attribs, textState);

      // result:
      // lang=\"en\" xmlns=\"http://www.w3.org/1999/xhtml"

      var compare = " lang=\"en\" xmlns=\"http://www.w3.org/1999/xhtml\"";
      if (result != compare)
      {
        Console.WriteLine("\r\nHTMLBuilder.StartAttribs()");
        Console.WriteLine(result);
        Console.WriteLine(" !=");
        Console.WriteLine(compare);
      }
    }

    public static void TableAttribs()
    {
      // Root Method Begin
      var textState = new TextState();

      var hb = new HTMLBuilder(textState);

      // Example Method:
      var border = 1;
      var cellspacing = 2;
      var cellpadding = 3;
      // Defaults: border = 1, cellspacing = 0, cellpadding = 2. 
      var attribs = hb.TableAttribs(border, cellspacing, cellpadding);
      var result = hb.GetAttribs(attribs, textState);

      // result:
      // border="1" cellspacing="2" cellpadding="3"

      var compare = " border=\"1\" cellspacing=\"2\" cellpadding=\"3\"";
      if (result != compare)
      {
        Console.WriteLine("\r\nHTMLBuilder.TableAttribs()");
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
