// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// TestHTMLBuilder5.cs
using LJCNetCommon5;

namespace TestNetCommon5
{
  // Provides LJCHTMLBuilder test methods.
  internal class TestHTMLBuilder
  {
    #region Properties

    // Gets or sets the TestCommon object.
    private LJCTestCommon TestCommon { get; set; } = null!;
    #endregion

    #region Constructor Methods

    // Initializes an object instance.
    public TestHTMLBuilder()
    {
      TestCommon = new LJCTestCommon("TestHTMLBuilder");
      Run();
    }

    // Runs the test methods.
    private void Run()
    {
      // Data Class Test Methods
      ToStringMethod();

      // Methods Test Methods
      AddChildIndent();
      AddIndent();
      EndsWithNewLine();
      HasText();
      IndentLength();
      StartsWithNewLine();

      // Text Methods Test Methods
      AddLine();
      AddText();
      Line();
      Text();

      // Get Text Methods Test Methods
      GetAttribs();
      GetIndented();
      GetIndentedString();
      GetLine();
      GetText();
      GetWrapped();

      // Element Methods Test Methods
      Begin();
      Create();
      End();

      // Get Element Methods Test Methods
      GetBegin();
      GetBeginSelector();
      GetCreate();
      GetEnd();

      // Create Element Methods Test Methods
      Link();
      Meta();
      Metas();
      Script();
      GetLink();
      GetMeta();
      GetMetas();
      GetScript();

      // HTML Methods Test Methods
      HTMLBegin();
      GetHTMLBegin();
      GetHTMLEnd();
      GetHTMLHead();

      // Get Attribs Methods Test Methods
      Attribs();
      StartAttribs();
      StartXMLAttribs();
      TableAttribs();
    }
    #endregion

    #region Data Class Test Methods

    // Retrieves the object text.
    private void ToStringMethod()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon.Write("ToStringMethod()", result, compare);
    }
    #endregion

    #region Methods Test Methods

    // Adds the new (child) indents.
    // Use AddChildIndent after beginning an element.
    private void AddChildIndent()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon.Write("AddChildIndent()", result, compare);
    }

    // Changes the IndentCount by the provided value.
    private void AddIndent()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon.Write("AddIndent()", result, compare);
    }

    // Indicates if the builder text ends with a newline.
    private void EndsWithNewLine()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon.Write("EndsWithNewLine()", result, compare);
    }

    // Indicates if the builder has text.
    private void HasText()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon.Write("HasText()", result, compare);
    }

    // Gets the current indent length.
    private void IndentLength()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon.Write("IndentLength()", result, compare);
    }

    // Gets the current indent length.
    private void StartsWithNewLine()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon.Write("StartsWithNewLine()", result, compare);
    }
    #endregion

    #region Text Methods Test Methods

    // Adds a text line without modification.
    private void AddLine()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon.Write("AddLine()", result, compare);
    }

    // Adds text without modification.
    private void AddText()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon.Write("AddText()", result, compare);
    }

    // Adds a potentially modified text line to the builder.
    private void Line()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon.Write("Line()", result, compare);
    }

    // Adds potentially modified text to the builder.
    private void Text()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon.Write("Text()", result, compare);
    }
    #endregion

    #region Get Text Methods Test Methods

    // Gets the attributes text.
    private void GetAttribs()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon.Write("GetAttribs()", result, compare);
    }

    // Gets a new potentially indented line.
    private void GetIndented()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon.Write("GetIndented()", result, compare);
    }

    // Returns the current indent string.
    private void GetIndentedString()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon.Write("GetIndentedString()", result, compare);
    }

    // Gets a modified text line.
    private void GetLine()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon.Write("GetLine()", result, compare);
    }

    // Gets potentially indented and wrapped text.
    private void GetText()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon.Write("GetText()", result, compare);
    }

    // Appends added text and new wrapped line if combined line > LineLimit.
    private void GetWrapped()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon.Write("GetWrapped()", result, compare);
    }
    #endregion

    #region Element Methods Test Methods

    // Appends the element begin tag.
    private void Begin()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon.Write("Begin()", result, compare);
    }

    // Appends an element.
    private void Create()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon.Write("Create()", result, compare);
    }

    // Appends the element end tag.
    private void End()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon.Write("End()", result, compare);
    }
    #endregion

    #region Get Element Methods Test Methods

    // Gets the element begin tag.
    private void GetBegin()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon.Write("GetBegin()", result, compare);
    }

    // Gets beginning of style selector.
    private void GetBeginSelector()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon.Write("GetBeginSelector()", result, compare);
    }

    // Gets the element text.
    private void GetCreate()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon.Write("GetCreate()", result, compare);
    }

    // Gets the element end tag.
    private void GetEnd()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon.Write("GetEnd()", result, compare);
    }
    #endregion

    #region Create Element Methods Test Methods

    // Appends a <link> element for a style sheet.
    private void Link()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon.Write("Link()", result, compare);
    }

    // Appends a <meta> element.
    private void Meta()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon.Write("Meta()", result, compare);
    }

    // Appends common <meta> elements.
    private void Metas()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon.Write("Metas()", result, compare);
    }

    // Appends a <script> element for a script file.
    private void Script()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon.Write("Script()", result, compare);
    }

    // Gets the <link> element for a style sheet.
    private void GetLink()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon.Write("GetLink()", result, compare);
    }

    // Gets a <meta> element.
    private void GetMeta()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon.Write("GetMeta()", result, compare);
    }

    // Gets common <meta> elements.
    private void GetMetas()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon.Write("GetMetas()", result, compare);
    }

    // Gets the <script> element.
    private void GetScript()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon.Write("GetScript()", result, compare);
    }
    #endregion

    #region HTML Methods Test Methods

    // Creates the HTML beginning up to and including <head>.
    private void HTMLBegin()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon.Write("HTMLBegin()", result, compare);
    }

    // Gets the HTML beginning up to <head>.
    private void GetHTMLBegin()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon.Write("GetHTMLBegin()", result, compare);
    }

    // Gets the HTML end <body> and <html>.
    private void GetHTMLEnd()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon.Write("GetHTMLEnd()", result, compare);
    }

    // Gets the main HTML Head elements.
    private void GetHTMLHead()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon.Write("GetHTMLHead()", result, compare);
    }
    #endregion

    #region Get Attribs Methods Test Methods

    // Gets common element attributes.
    private void Attribs()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon.Write("Attribs()", result, compare);
    }

    // Creates the HTML element attributes.
    private void StartAttribs()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon.Write("StartAttribs()", result, compare);
    }

    // Creates the XML start attributes.
    private void StartXMLAttribs()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon.Write("StartXMLAttribs()", result, compare);
    }

    // Gets common table attributes.
    private void TableAttribs()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon.Write("TableAttribs()", result, compare);
    }
    #endregion
  }
}
