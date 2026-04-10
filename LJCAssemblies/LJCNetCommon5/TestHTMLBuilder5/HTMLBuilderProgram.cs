// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// HTMLBuilderProgram.cs
using LJCNetCommon5;

namespace TestHTMLBuilder5
{
  // The entry class.
  internal class HTMLBuilderProgram
  {
    // The entry method.
    static void Main()
    {
      TestCommon = new LJCTestCommon("LJCHTMLBuilder");
      Console.WriteLine();
      Console.WriteLine("*** LJCHTMLBuilder ***");

      // Data Class Methods
      ToStringMethod();

      // Methods
      AddChildIndent();
      AddIndent();
      EndsWithNewLine();
      HasText();
      IndentLength();
      StartsWithNewLine();

      // Text Methods
      AddLine();
      AddText();
      Line();
      Text();

      // Get Text Methods
      GetAttribs();
      GetIndented();
      GetIndentedString();
      GetLine();
      GetText();
      GetWrapped();

      // Element Methods
      Begin();
      Create();
      End();

      // Get Element Methods
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
      StartXMLAttribs();
      TableAttribs();
    }

    #region Data Class Methods

    // Retrieves the object text.
    private static void ToStringMethod()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("ToStringMethod()", result, compare);
    }
    #endregion

    #region Methods

    // Adds the new (child) indents.
    // Use AddChildIndent after beginning an element.
    private static void AddChildIndent()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("AddChildIndent()", result, compare);
    }

    // Changes the IndentCount by the provided value.
    private static void AddIndent()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("AddIndent()", result, compare);
    }

    // Indicates if the builder text ends with a newline.
    private static void EndsWithNewLine()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("EndsWithNewLine()", result, compare);
    }

    // Indicates if the builder has text.
    private static void HasText()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("HasText()", result, compare);
    }

    // Gets the current indent length.
    private static void IndentLength()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("IndentLength()", result, compare);
    }

    // Gets the current indent length.
    private static void StartsWithNewLine()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("StartsWithNewLine()", result, compare);
    }
    #endregion

    #region Text Methods

    // Adds a text line without modification.
    private static void AddLine()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("AddLine()", result, compare);
    }

    // Adds text without modification.
    private static void AddText()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("AddText()", result, compare);
    }

    // Adds a potentially modified text line to the builder.
    private static void Line()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("Line()", result, compare);
    }

    // Adds potentially modified text to the builder.
    private static void Text()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("Text()", result, compare);
    }
    #endregion

    #region Get Text Methods

    // Gets the attributes text.
    private static void GetAttribs()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("GetAttribs()", result, compare);
    }

    // Gets a new potentially indented line.
    private static void GetIndented()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("GetIndented()", result, compare);
    }

    // Returns the current indent string.
    private static void GetIndentedString()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("GetIndentedString()", result, compare);
    }

    // Gets a modified text line.
    private static void GetLine()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("GetLine()", result, compare);
    }

    // Gets potentially indented and wrapped text.
    private static void GetText()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("GetText()", result, compare);
    }

    // Appends added text and new wrapped line if combined line > LineLimit.
    private static void GetWrapped()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("GetWrapped()", result, compare);
    }
    #endregion

    #region Element Methods

    // Appends the element begin tag.
    private static void Begin()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("Begin()", result, compare);
    }

    // Appends an element.
    private static void Create()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("Create()", result, compare);
    }

    // Appends the element end tag.
    private static void End()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("End()", result, compare);
    }
    #endregion

    #region Get Element Methods

    // Gets the element begin tag.
    private static void GetBegin()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("GetBegin()", result, compare);
    }

    // Gets beginning of style selector.
    private static void GetBeginSelector()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("GetBeginSelector()", result, compare);
    }

    // Gets the element text.
    private static void GetCreate()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("GetCreate()", result, compare);
    }

    // Gets the element end tag.
    private static void GetEnd()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("GetEnd()", result, compare);
    }
    #endregion

    #region Create Element Methods

    // Appends a <link> element for a style sheet.
    private static void Link()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("Link()", result, compare);
    }

    // Appends a <meta> element.
    private static void Meta()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("Meta()", result, compare);
    }

    // Appends common <meta> elements.
    private static void Metas()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("Metas()", result, compare);
    }

    // Appends a <script> element for a script file.
    private static void Script()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("Script()", result, compare);
    }

    // Gets the <link> element for a style sheet.
    private static void GetLink()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("GetLink()", result, compare);
    }

    // Gets a <meta> element.
    private static void GetMeta()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("GetMeta()", result, compare);
    }

    // Gets common <meta> elements.
    private static void GetMetas()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("GetMetas()", result, compare);
    }

    // Gets the <script> element.
    private static void GetScript()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("GetScript()", result, compare);
    }
    #endregion

    #region HTML Methods

    // Creates the HTML beginning up to and including <head>.
    private static void HTMLBegin()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("HTMLBegin()", result, compare);
    }

    // Gets the HTML beginning up to <head>.
    private static void GetHTMLBegin()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("GetHTMLBegin()", result, compare);
    }

    // Gets the HTML end <body> and <html>.
    private static void GetHTMLEnd()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("GetHTMLEnd()", result, compare);
    }

    // Gets the main HTML Head elements.
    private static void GetHTMLHead()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("GetHTMLHead()", result, compare);
    }
    #endregion

    #region Get Attribs Methods

    // Gets common element attributes.
    private static void Attribs()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("Attribs()", result, compare);
    }

    // Creates the HTML element attributes.
    private static void StartAttribs()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("StartAttribs()", result, compare);
    }

    // Creates the XML start attributes.
    private static void StartXMLAttribs()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("StartXMLAttribs()", result, compare);
    }

    // Gets common table attributes.
    private static void TableAttribs()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("TableAttribs()", result, compare);
    }
    #endregion

    #region Class Data

    private static LJCTestCommon? TestCommon { get; set; }
    #endregion
  }
}
