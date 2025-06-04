// Copyright (c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// XMLBuilderTest.cs
using LJCNetCommon;
using System;

namespace LJCNetCommonTest
{
  internal class XMLBuilderTest
  {
    #region Methods

    // Performs the tests.
    internal static void Test()
    {
      TestCommon = new TestCommon("XMLBuilder");
      Console.WriteLine();
      Console.WriteLine("*** XMLBuilder ***");

      // Methods
      AddIndent();

      // Append Text Methods
      AddLine();
      AddText();
      Line();
      Text();

      // Get Text Methods
      EndsWithNewLine();
      StartWithNewLine();
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

      // Get Element Methods
      AddChildIndent();
      GetBegin();
      GetCreate();
      GetEnd();

      // Get Attribute Methods
      StartAttribs();
    }

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
      // and param allowNewLine = true and builder text does not end with
      // a newline.
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

      var b = new XMLBuilder();
      b.AddLine("This text is not indented.");
      b.AddLine("  This text is indented.");
      b.AddText("Not indented.  No start with newline.");
      var compare = b.ToString();
      TestCommon.Write("AddIndent()", result, compare);
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

      var b = new XMLBuilder();
      b.AddLine("This is an appended line.");
      b.AddText(":");
      var compare = b.ToString();
      TestCommon.Write("AddLine()", result, compare);
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

      var compare = "This is some appended text.";
      TestCommon.Write("AddText()", result, compare);
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
      // and param allowNewLine = true and builder text does not end with
      // a newline.
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
      //   This is an indented line.

      var b = new XMLBuilder();
      b.AddLine("This is an appended line.");
      b.AddLine();
      b.AddText("  This is an indented line.");
      var compare = b.ToString();
      TestCommon.Write("Line()", result, compare);
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

      var b = new XMLBuilder();
      b.AddLine("This is an appended line.");
      b.AddText("  This is an indented line.");
      var compare = b.ToString();
      TestCommon.Write("Text()", result, compare);
    }
    #endregion

    #region Get Text Methods (5)

    private static bool EndsWithNewLine()
    {
      var xb = new XMLBuilder();

      bool retValue = xb.EndsWithNewLine();
      var result = retValue.ToString();

      // result:
      // False

      var b = new XMLBuilder();
      b.AddText("False");
      var compare = b.ToString();
      TestCommon.Write("EndsWithNewLine()", result, compare);
      return retValue;
    }

    private static bool StartWithNewLine()
    {
      var xb = new XMLBuilder();

      bool retValue = xb.StartWithNewLine(true);
      var result = retValue.ToString();

      // result:
      // False

      var b = new XMLBuilder();
      b.AddText("False");
      var compare = b.ToString();
      TestCommon.Write("StartWithNewLine()", result, compare);
      return retValue;
    }

    private static void GetAttribs()
    {
      // Root Method Begin
      var textState = new TextState();

      // Defaults: IndentCharCount = 2, LineLimit = 80, WrapEnabled = false.
      var xb = new XMLBuilder(textState);

      // Example Method:
      var attribs = new Attributes()
      {
        { "name", "Someone" },
      };
      xb.Begin("Person", textState, attribs);
      xb.End("Person", textState);
      var result = xb.ToString();

      // result:
      // <Person name="Someone">
      // </Person>

      var b = new XMLBuilder();
      b.AddLine("<Person name=\"Someone\">");
      b.AddText("</Person>");
      var compare = b.ToString();
      TestCommon.Write("GetAttribs()", result, compare);
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

      var compare = "This text is NOT indented.\r\n";
      compare += "    This text is indented.";
      TestCommon.Write("GetIndented()", result, compare);
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

      var compare = "  :";
      TestCommon.Write("GetIndentString()", result, compare);
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

      var compare = "This is an appended line.\r\n";
      compare += "  :";
      TestCommon.Write("GetLine()", result, compare);
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

      var compare = "This is an appended line.\r\n";
      compare += "  This is an indented line.";
      TestCommon.Write("GetText()", result, compare);
    }

    private static void GetWrapped()
    {
      // Defaults: IndentCharCount = 2, LineLimit = 80, WrapEnabled = false.
      var xb = new XMLBuilder()
      {
        WrapEnabled = true
      };

      // Example Method:
      var b = new XMLBuilder();
      b.AddText("Now is the time for all good men to come to the aid of their");
      b.AddText(" country.");
      b.AddText(" Now is the time for all good men to come to the aid of their");
      b.AddText(" country.");
      var text = b.ToString();
      var result = xb.GetWrapped(text);

      // result:
      // Now is the time for all good men to come to the aid of
      // their country.

      b = new XMLBuilder();
      b.AddText("Now is the time for all good men to come to the aid of");
      b.AddLine(" their country. Now is the");
      b.AddText("time for all good men to come to the aid of their country.");
      var compare = b.ToString();
      TestCommon.Write("GetWrapped()", result, compare);
    }
    #endregion

    #region Append Element Methods

    private static void Begin()
    {
      // Root Method Begin
      var textState = new TextState();

      // Defaults: IndentCharCount = 2, LineLimit = 80, WrapEnabled = false.
      var xb = new XMLBuilder(textState);

      // Example Method:
      var attribs = new Attributes()
      {
        { "name", "Someone" },
      };
      xb.Begin("Person", textState, attribs);

      xb.End("Person", textState);
      var result = xb.ToString();

      // result:
      // <Person name="Someone">
      // </Person>

      var compare = "<Person name=\"Someone\">\r\n";
      compare += "</Person>";
      TestCommon.Write("Begin()", result, compare);
    }

    private static void Create()
    {
      // Root Method Begin
      var textState = new TextState();

      // Defaults: IndentCharCount = 2, LineLimit = 80, WrapEnabled = false.
      var xb = new XMLBuilder(textState);

      // Example Method:
      var attribs = new Attributes()
      {
        { "name", "Someone" },
      };
      // Defaults: close = true.
      xb.Create("Person", textState, null, attribs);
      var result = xb.ToString();

      // result:
      // <Person name="Someone"></Person>

      var compare = "<Person name=\"Someone\"></Person>";
      TestCommon.Write("Create()", result, compare);
    }

    private static void End()
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

      // Example Method:
      xb.End("Person", textState);
      var result = xb.ToString();

      // result:
      // <Person name="Someone">
      // </Person>

      var compare = "<Person name=\"Someone\">\r\n";
      compare += "</Person>";
      TestCommon.Write("GetAttribs()", result, compare);
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
      TestCommon.Write("AddChildIndent()", result, compare);
    }

    private static string CustomBegin(string name, TextState textState
      , Attributes attribs = null, bool addIndent = true
      , bool childIndent = true)
    {
      // Defaults: IndentCharCount = 2, LineLimit = 80, WrapEnabled = false.
      var xb = new HTMLBuilder(textState);

      var createText = xb.GetBegin(name, textState, attribs, addIndent
        , childIndent);
      // Use NoIndent after a "GetText" method.
      xb.Text(createText, NoIndent);
      // Use AddChildIndent after beginning an element.
      xb.AddChildIndent(createText, textState);
      var result = xb.ToString();

      // Append Method
      //xb.UpdateState(textState);
      return result;
    }

    private static void GetBegin()
    {
      // Root Method Begin
      var textState = new TextState();

      // Defaults: IndentCharCount = 2, LineLimit = 80, WrapEnabled = false.
      var xb = new XMLBuilder(textState);

      // Example Method:
      var attribs = new Attributes()
      {
        { "name", "Someone" },
      };
      var result = xb.GetBegin("Person", textState, attribs);

      // result:
      // <Person name="Someone">

      var compare = "<Person name=\"Someone\">";
      TestCommon.Write("GetBegin()", result, compare);
    }

    private static void GetCreate()
    {
      // Root Method Begin
      var textState = new TextState();

      // Defaults: IndentCharCount = 2, LineLimit = 80, WrapEnabled = false.
      var xb = new XMLBuilder(textState);

      // Example Method:
      var attribs = new Attributes()
      {
        { "name", "Someone" },
      };
      // Defaults: close = true.
      var result = xb.GetCreate("Person", textState, null, attribs);

      // result:
      // <Person name="Someone"></Person>

      var compare = "<Person name=\"Someone\"></Person>";
      TestCommon.Write("GetCreate()", result, compare);
    }

    private static void GetEnd()
    {
      // Root Method Begin
      var textState = new TextState();

      // Defaults: IndentCharCount = 2, LineLimit = 80, WrapEnabled = false.
      var xb = new XMLBuilder(textState);

      // Example Method:
      var result = xb.GetEnd("Person", textState);

      // result:
      // <Person>

      var compare = "</Person>";
      TestCommon.Write("GetEnd()", result, compare);
    }
    #endregion

    #region Get Attribs Methods

    public static void StartAttribs()
    {
      // Root Method Begin
      var textState = new TextState();

      var xb = new XMLBuilder(textState);

      // Example Method:
      var attribs = xb.StartAttribs();
      var result = xb.GetAttribs(attribs, textState);

      // result:
      //  xmlns:xsd="http://www.w3.org/2001/XMLSchema"
      //  xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"

      var b = new HTMLBuilder();
      b.AddLine(" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"");
      b.AddText(" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"");
      var compare = b.ToString();
      TestCommon.Write("StartAttribs()", result, compare);
    }
    #endregion

    #region Class Data

    private static TestCommon TestCommon { get; set; }

    private const bool NoIndent = false;
    private const bool NoNewLine = false;
    #endregion
  }
}
