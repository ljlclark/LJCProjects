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

      var compare = "This is some appended text.";
      if (result != compare)
      {
        Console.WriteLine("XMLBuilder.AddText() Error");
      }
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

      var compare = "This is an appended line.\r\n";
      compare += "\r\n";
      compare += "\r\n";
      compare += "  This is an indented line.";
      if (result != compare)
      {
        Console.WriteLine("XMLBuilder.Line() Error");
      }
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

      var compare = "This is an appended line.\r\n";
      compare += "  This is an indented line.";
      if (result != compare)
      {
        Console.WriteLine("XMLBuilder.Text() Error");
      }
    }
    #endregion

    #region Get Text Methods (5)

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

      var compare = "<Person name=\"Someone\">\r\n";
      compare += "</Person>";
      if (result != compare)
      {
        Console.WriteLine("XMLBuilder.GetAttribs() Error");
      }
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
      if (result != compare)
      {
        Console.WriteLine("XMLBuilder.GetIndented() Error");
      }
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
      if (result != compare)
      {
        Console.WriteLine("XMLBuilder.GetIndentString() Error");
      }
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
      if (result != compare)
      {
        Console.WriteLine("XMLBuilder.GetLine() Error");
      }
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
      if (result != compare)
      {
        Console.WriteLine("XMLBuilder.GetText() Error");
      }
    }

    private static void GetWrapped()
    {
      Console.WriteLine("XMLBuilder.GetWrapped() Not Implemented");
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
      if (result != compare)
      {
        Console.WriteLine("XMLBuilder.Begin() Error");
      }
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
      if (result != compare)
      {
        Console.WriteLine("XMLBuilder.Create() Error");
      }
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
      if (result != compare)
      {
        Console.WriteLine("XMLBuilder.GetAttribs() Error");
      }
    }
    #endregion

    #region Get Element Methods

    private static void AddChildIndent()
    {
      Console.WriteLine("XMLBuilder.AddChildIndent() Not Implemented");
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
      if (result != compare)
      {
        Console.WriteLine("XMLBuilder.GetBegin() Error");
      }
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
      if (result != compare)
      {
        Console.WriteLine("XMLBuilder.GetCreate() Error");
      }
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
      if (result != compare)
      {
        Console.WriteLine("XMLBuilder.GetEnd() Error");
      }
    }
    #endregion

    #region Get Attribs Methods

    public static void StartAttribs()
    {
      Console.WriteLine("XMLBuilder.StartAttribs() Not Implemented");
    }
    #endregion

    #region Class Data

    private const bool NoIndent = false;
    private const bool NoNewLine = false;
    #endregion
  }
}
