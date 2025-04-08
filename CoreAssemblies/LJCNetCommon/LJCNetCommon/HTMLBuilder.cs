// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// HTMLBuilder.cs
using System.Text;

namespace LJCNetCommon
{
  /// <summary>Provides methods for creating HTML text.</summary>
  public class HTMLBuilder
  {
    #region Constructors

    /// <summary>Initializes an object instance.</summary>
    public HTMLBuilder(TextState textState = null)
    {
      IndentCharCount = 2;
      IndentCount = 0;
      if (textState != null)
      {
        IndentCount = textState.IndentCount;
      }
      LineLength = 0;
      LineLimit = 80;
      WrapEnabled = false;
      HTML = "";
      if (textState != null)
      {
        ParentHasText = textState.HasText;
      }
    }
    #endregion

    #region Data Class Methods

    /// <summary>Implements the ToString() method.</summary>
    public override string ToString()
    {
      return HTML;
    }
    #endregion

    #region Methods

    // Changes the IndentCount by the supplied value.
    /// <include path='items/AddIndent/*' file='Doc/HTMLBuilder.xml'/>
    public int AddIndent(int increment = 1)
    {
      IndentCount += increment;
      if (IndentCount < 0)
      {
        IndentCount = 0;
      }
      return IndentCount;
    }
    #endregion

    // Line() and Text() methods start with a new line
    // if the parm hasText = true;
    #region Append Text Methods

    // Adds text without modification.
    /// <include path='items/Add/*' file='Doc/HTMLBuilder.xml'/>
    public void Add(string text)
    {
      HTML += text;
    }

    // Adds a modified text line to the builder.
    // Start with a new line if the hasText parm = true.
    /// <include path='items/Line/*' file='Doc/HTMLBuilder.xml'/>
    public string Line(string text, bool hasText)
    {
      var retText = GetText(text, hasText);
      retText += "\r\n";
      HTML += retText;
      return retText;
    }

    // Adds modified text to the builder.
    // Start with a new line if the hasText parm = true.
    /// <include path='items/Text/*' file='Doc/HTMLBuilder.xml'/>
    public string Text(string text, bool hasText)
    {
      var retText = GetText(text, hasText);
      HTML += retText;
      return retText;
    }
    #endregion

    // GetLine() and GetText() methods start with a new line
    // if the parm hasText = true;
    #region Get Modified Text Methods

    // Gets the attributes text.
    /// <summary>Gets the attributes text.</summary>
    /// <include path='items/GetAttribs/*' file='Doc/HTMLBuilder.xml'/>
    public string GetAttribs(Attributes htmlAttribs)
    {
      string retText = "";

      if (NetCommon.HasItems(htmlAttribs))
      {
        var tb = new TextBuilder();
        var isFirst = true;
        foreach (Attribute htmlAttrib in htmlAttribs)
        {
          if (!isFirst)
          {
            // Wrap line for large attribute value.
            if (NetString.HasValue(htmlAttrib.Value)
              && htmlAttrib.Value.Length > 35)
            {
              tb.Text($"\r\n{GetIndentString()}   ");
            }
          }
          isFirst = false;

          tb.Text($" {htmlAttrib.Name}");
          if (NetString.HasValue(htmlAttrib.Value))
          {
            tb.Text($"=\"{htmlAttrib.Value}\"");
          }
        }
        retText = tb.ToString();
      }
      return retText;
    }

    // Gets a new line with indent.
    /// <include path='items/GetIndented/*' file='Doc/HTMLBuilder.xml'/>
    public string GetIndented(string text)
    {
      string retText = "";

      // Allow add of blank characters.
      if (text != null)
      {
        retText = GetIndentString();
        retText += text;
      }
      return retText;
    }

    // Returns the current indent string.
    /// <include path='items/GetIndentString/*' file='Doc/HTMLBuilder.xml'/>
    public string GetIndentString()
    {
      var retValue = new string(' ', IndentLength);
      return retValue;
    }

    // Gets a modified text line.
    // Start with a new line if the hasText parm = true.
    // Ends with a new line.
    /// <include path='items/GetLine/*' file='Doc/HTMLBuilder.xml'/>
    public string GetLine(string text, bool hasText)
    {
      var retLine = "";

      retLine += GetText(text, hasText);
      retLine += "\r\n";
      return retLine;
    }

    // Gets potentially indented and wrapped text.
    // Start with a new line if the hasText parm = true.
    /// <include path='items/GetText/*' file='Doc/HTMLBuilder.xml'/>
    public string GetText(string text, bool hasText)
    {
      var retText = "";

      if (hasText)
      {
        retText += "\r\n";
      }
      retText += GetIndented(text);

      if (WrapEnabled)
      {
        retText = GetWrapped(retText);
      }
      return retText;
    }

    // Appends added text and new wrapped line if combined line > LineLimit.
    /// <include path='items/GetWrapped/*' file='Doc/HTMLBuilder.xml'/>
    public string GetWrapped(string text)
    {
      string retText = text;

      string buildText = "";
      var workText = text;
      var totalLength = LineLength + TextLength(workText);
      if (totalLength < LineLimit)
      {
        // No wrap.
        LineLength += TextLength(text);
      }

      while (totalLength > LineLimit)
      {
        // Index where text can be added to the current line
        // and the remainder is wrapped.
        var wrapIndex = WrapIndex(workText);
        if (wrapIndex > -1)
        {
          // Adds leading space if line exists and wrapIndex > 0.
          var addText = AddText(retText, wrapIndex);
          buildText += $"{addText}\r\n";

          // Next text up to LineLimit - prepend length without leading space.
          string wrapText = WrapText(workText, wrapIndex);
          var lineText = $"{GetIndentString()}{wrapText}";
          LineLength = lineText.Length;
          buildText += lineText;

          // End loop unless there is more text.
          totalLength = 0;

          // Get index of next section.
          var nextIndex = wrapIndex + wrapText.Length;
          if (!workText.StartsWith(","))
          {
            // Adjust for removed leading space.
            nextIndex++;
          }

          // Get next work text if available.
          if (nextIndex < workText.Length)
          {
            var tempText = workText.Substring(nextIndex);
            workText = tempText;
            totalLength = LineLength + TextLength(workText);
          }
        }
      }

      if (buildText != null
        && buildText.Length > 0)
      {
        retText = buildText;
      }
      return retText;
    }
    #endregion

    // All append element methods start with a new line
    // if the parm hasText = true.
    #region Append Element Methods

    // Creates the element begin tag.
    /// <include path='items/Begin/*' file='Doc/HTMLBuilder.xml'/>
    public string Begin(string name, bool hasText, string text = null
      , Attributes htmlAttributes = null, bool applyIndent = true)
    {
      return Create(name, text, hasText, htmlAttributes, applyIndent, false
        , false);
    }

    // Creates an element. No new line.
    /// <include path='items/Create/*' file='Doc/HTMLBuilder.xml'/>
    public string Create(string name, string text, bool hasText
      , Attributes htmlAttributes = null, bool applyIndent = true
      , bool isEmpty = false, bool close = true)
    {
      var tb = new TextBuilder(128);
      if (hasText)
      {
        tb.Line();
      }

      // Add the element name and attributes.
      if (applyIndent)
      {
        tb.Text($"{GetIndentString()}");
      }
      tb.Text($"<{name}");
      tb.Text(GetAttribs(htmlAttributes));
      if (isEmpty)
      {
        tb.Text(" /");
      }
      tb.Text(">");
      var content = Content(text, HasText, isEmpty, out bool isWrapped);
      if (NetString.HasValue(content))
      {
        tb.Text(content);
      }

      // Set indent for child elements.
      if (!isEmpty
        && applyIndent)
      {
        AddIndent();
      }

      // Close the element.
      if (!isEmpty
        && close)
      {
        if (isWrapped)
        {
          tb.Line();
          if (applyIndent)
          {
            // Reset indent for element end.
            AddIndent(-1);
          }
          applyIndent = false;
          tb.Text(GetIndentString());
        }
        tb.Text($"</{name}>");
        if (applyIndent)
        {
          // Reset indent for next element.
          AddIndent(-1);
        }
      }

      // Get value and update the HTML.
      string retElement = tb.ToString();
      HTML += retElement;
      return retElement;
    }

    // Creates the element end tag.
    /// <include path='items/End/*' file='Doc/HTMLBuilder.xml'/>
    public string End(string name, bool hasText, bool applyIndent = true)
    {
      string retEnd;

      var tb = new TextBuilder(128);
      if (hasText)
      {
        tb.Line();
      }

      if (applyIndent)
      {
        AddIndent(-1);
        tb.Text($"{GetIndentString()}");
      }
      tb.Text($"</{name}>");
      retEnd = tb.ToString();
      HTML += retEnd;
      return retEnd;
    }
    #endregion

    // Create Element methods start with a new line
    // if the parm textState.HasText = true .
    #region Create Element Methods

    // Creates the HTML beginning up to and including <head>.
    /// <include path='items/CreateHTMLBegin/*' file='Doc/HTMLBuilder.xml'/>
    public string CreateHTMLBegin(TextState textState, string[] copyright = null
      , string fileName = null)
    {
      var retValue = GetHTMLBegin(textState, copyright, fileName);
      Text(retValue, HasText);
      return retValue;
    }

    // Creates a <link> element for a style sheet.
    /// <include path='items/CreateLink/*' file='Doc/HTMLBuilder.xml'/>
    public string CreateLink(string fileName, TextState textState)
    {
      var retValue = GetLink(fileName, textState);
      Text(retValue, textState.HasText);
      return retValue;
    }

    // Creates a <meta> element.
    /// <include path='items/CreateMeta/*' file='Doc/HTMLBuilder.xml'/>
    public string CreateMeta(string name, string content, TextState textState)
    {
      var retValue = GetMeta(name, content, textState);
      Text(retValue, textState.HasText);
      return retValue;
    }

    // Creates common <meta> elements.
    /// <include path='items/CreateMetas/*' file='Doc/HTMLBuilder.xml'/>
    public string CreateMetas(string author, TextState textState
      , string description = null, string keywords = null
      , string charSet = "utf-8")
    {
      var retValue = GetMetas(author, textState, description, keywords
        , charSet);
      Text(retValue, textState.HasText);
      return retValue;
    }

    // Creates a <link> element for a style sheet.
    /// <include path='items/CreateScript/*' file='Doc/HTMLBuilder.xml'/>
    public string CreateScript(string fileName, TextState textState)
    {
      var retValue = GetScript(fileName, textState);
      Text(retValue, HasText);
      return retValue;
    }
    #endregion

    // Get methods do NOT start with a new line.
    #region Create Element Get Methods

    // Gets the HTML beginning up to <head>.
    /// <include path='items/GetHTMLBegin/*' file='Doc/HTMLBuilder.xml'/>
    public string GetHTMLBegin(TextState textState, string[] copyright = null
      , string fileName = null)
    {
      var tempTextState = SetHasTextFalse(textState);
      var hb = new HTMLBuilder(tempTextState);
      hb.Text("<!DOCTYPE html>", hb.HasText);
      if (NetCommon.HasElements(copyright))
      {
        foreach (string text in copyright)
        {
          hb.Text($"<!-- {text} -->", hb.HasText);
        }
      }
      if (NetString.HasValue(fileName))
      {
        hb.Text($"<!-- {fileName} -->", hb.HasText);
      }
      var startAttribs = hb.StartAttribs();
      hb.Begin("html", hb.HasText, null, startAttribs, NoIndent);
      hb.Begin("head", hb.HasText, applyIndent: NoIndent);
      var retValue = hb.ToString();
      return retValue;
    }

    /// <summary>Gets the HTML end &lt;body&gt; and &lt;html&gt;.</summary>
    public string GetHTMLEnd(TextState textState)
    {
      var tempTextState = SetHasTextFalse(textState);
      var hb = new HTMLBuilder(tempTextState);
      hb.End("body", NoIndent);
      hb.End("html", NoIndent);
      var retValue = hb.ToString();
      return retValue;
    }

    // Gets the <link> element for a style sheet.
    /// <include path='items/GetLink/*' file='Doc/HTMLBuilder.xml'/>
    public string GetLink(string fileName, TextState textState)
    {
      var tempTextState = SetHasTextFalse(textState);
      var hb = new HTMLBuilder(tempTextState);
      var attribs = new Attributes()
      {
        { "rel", "stylesheet" },
        { "type", "text/css" },
        { "href", fileName },
      };
      hb.Create("link", null, hb.HasText, attribs, isEmpty: true);
      var retValue = hb.ToString();
      return retValue;
    }

    // Gets a <meta> element.
    /// <include path='items/GetMeta/*' file='Doc/HTMLBuilder.xml'/>
    public string GetMeta(string name, string content, TextState textState)
    {
      var tempTextState = SetHasTextFalse(textState);
      var hb = new HTMLBuilder(tempTextState);
      var attribs = new Attributes()
      {
        { "name", name },
        { "content", content },
      };
      hb.Create("meta", null, hb.HasText, attribs, isEmpty: true);
      var retValue = hb.ToString();
      return retValue;
    }

    // Gets common <meta> elements.
    /// <include path='items/GetMetas/*' file='Doc/HTMLBuilder.xml'/>
    public string GetMetas(string author, TextState textState
      , string description = null, string keywords = null
      , string charSet = "utf-8")
    {
      var tempTextState = SetHasTextFalse(textState);
      var hb = new HTMLBuilder(tempTextState);
      var attribs = new Attributes()
      {
        { "charset", charSet }
      };
      hb.Create("meta", null, hb.HasText, attribs, isEmpty: true);
      if (NetString.HasValue(description))
      {
        hb.CreateMeta("description", description, hb.TextState);
      }
      if (NetString.HasValue(keywords))
      {
        hb.CreateMeta("keywords", keywords, hb.TextState);
      }
      hb.CreateMeta("author", author, hb.TextState);
      var content = "width=device-width initial-scale=1";
      hb.CreateMeta("viewport", content, hb.TextState);
      var retValue = hb.ToString();
      return retValue;
    }

    // Gets the <script> element.
    /// <include path='items/GetScript/*' file='Doc/HTMLBuilder.xml'/>
    public string GetScript(string fileName, TextState textState)
    {
      var tempTextState = SetHasTextFalse(textState);
      var hb = new HTMLBuilder(tempTextState);
      var attribs = new Attributes()
      {
        { "src", fileName },
      };
      hb.Create("script", null, hb.HasText, attribs);
      var retValue = hb.ToString();
      return retValue;
    }
    #endregion

    // Attrib methods do NOT start with a new line.
    #region Element Attribs Methods

    // Gets common element attributes.
    /// <include path='items/Attribs/*' file='Doc/HTMLBuilder.xml'/>
    public Attributes Attribs(string className = null, string id = null)
    {
      var retAttribs = new Attributes();
      if (NetString.HasValue(id))
      {
        retAttribs.Add("id", id);
      }
      if (NetString.HasValue(className))
      {
        retAttribs.Add("class", className);
      }
      return retAttribs;
    }

    // Creates the HTML element attributes.
    /// <include path='items/StartAttribs/*' file='Doc/HTMLBuilder.xml'/>
    public Attributes StartAttribs()
    {
      var retAttributes = new Attributes()
      {
        { "lang", "en" },
        { "xmlns", "http://www.w3.org/1999/xhtml" },
      };
      return retAttributes;
    }

    // Gets common table attributes.
    /// <include path='items/TableAttribs/*' file='Doc/HTMLBuilder.xml'/>
    public Attributes TableAttribs(int border = 1, int cellSpacing = 0
      , int cellPadding = 2, string className = null, string id = null)
    {
      var retAttribs = Attribs(className, id);
      retAttribs.Add("border", border.ToString());
      retAttribs.Add("cellspacing", cellSpacing.ToString());
      retAttribs.Add("cellpadding", cellPadding.ToString());
      return retAttribs;
    }
    #endregion

    #region Private Methods

    // Gets the text to add to the existing line.
    private string AddText(string text, int addLength)
    {
      var retText = text.Substring(0, addLength);
      if (LineLength > 0
        && addLength > 0)
      {
        // Add a leading space.
        retText = $" {retText}";
      }
      return retText;
    }

    // Creates the content text.
    private string Content(string text, bool hasText, bool isEmpty
      , out bool isWrapped)
    {
      string retValue = "";

      isWrapped = false;
      // Add text content.
      if (!isEmpty
        && NetString.HasValue(text))
      {
        if (text.Length > 80 - IndentLength)
        {
          isWrapped = true;
          retValue += "\r\n";
          AddIndent();
          var textValue = GetText(text, hasText);
          retValue += textValue;
          AddIndent(-1);
          retValue += "\r\n";
          // *** Next Statement *** Add 4/8/25
          LineLength = 0;
        }
        else
        {
          retValue += text;
        }
        // *** Next Statement *** Delete 4/8/25
        //LineLength = 0;
      }
      return retValue;
    }

    // Sets TextState.HasText to false.
    private TextState SetHasTextFalse(TextState textState)
    {
      var retTextState = new TextState();

      if (textState != null)
      {
        retTextState = new TextState()
        {
          HasText = false,
          IndentCount = textState.IndentCount,
        };
      }
      return retTextState;
    }

    // Gets the text length if not null.
    private int TextLength(string text)
    {
      int retLength = 0;
      if (text != null)
      {
        retLength = text.Length;
      }
      return retLength;
    }

    // Calculates the index at which to wrap the text.
    private int WrapIndex(string text)
    {
      int retIndex = -1;

      var totalLength = LineLength + TextLength(text);
      if (totalLength > LineLimit)
      {
        // Length of additional characters that fit in LineLimit.
        // Only get up to next LineLimit length;
        var currentLength = LineLength;
        if (currentLength > LineLimit)
        {
          currentLength = LineLimit;
        }
        var wrapLength = LineLimit - currentLength;

        // Get wrap point in allowed length.
        // Wrap on a space.
        retIndex = text.LastIndexOf(" ", wrapLength);
        if (-1 == retIndex)
        {
          // Wrap index not found; Wrap at new text.
          retIndex = 0;
        }
      }
      return retIndex;
    }

    // Get next text up to LineLimit without leading space.
    private string WrapText(string text, int wrapIndex)
    {
      string retText;

      var nextLength = text.Length - wrapIndex;

      // Leave room for prepend text.
      if (nextLength <= LineLimit - TextLength(GetIndentString()))
      {
        // Get text at the wrap index.
        retText = text.Substring(wrapIndex, nextLength);
        if (retText.StartsWith(" "))
        {
          // Remove leading space.
          retText = retText.Substring(1);
        }
      }
      else
      {
        // Get text from next section.
        var startIndex = wrapIndex;
        var tempText = text.Substring(startIndex);
        if (tempText.StartsWith(" "))
        {
          tempText = tempText.Substring(1);
          startIndex++;
        }
        nextLength = LineLimit - TextLength(GetIndentString());
        nextLength = tempText.LastIndexOf(" ", nextLength);
        retText = text.Substring(startIndex, nextLength);
      }
      return retText;
    }
    #endregion

    #region Properties

    /// <summary>Indicates if the builder has text.</summary>
    public bool HasText
    {
      get
      {
        bool retValue = false;
        if (HTML.Length > 0
          || ParentHasText)
        {
          retValue = true;
        }
        return retValue;
      }
    }

    /// <summary>Gets or sets the indent character count.</summary>
    public int IndentCharCount { get; set; }

    /// <summary>Gets the current indent length.</summary>
    public int IndentLength
    {
      get { return IndentCount * IndentCharCount; }
    }

    /// <summary>Gets the current length.</summary>
    public int LineLength { get; private set; }

    /// <summary>Gets or sets the character limit.</summary>
    public int LineLimit { get; set; }

    /// <summary>Indicates if line wrapping is enabled.</summary>
    public bool WrapEnabled { get; set; }

    /// <summary>Gets the current indent value.</summary>
    public int IndentCount { get; private set; }

    /// <summary>Gets the current text state.</summary>
    public TextState TextState
    {
      get
      {
        var retState = new TextState()
        {
          HasText = HasText,
          IndentCount = IndentCount,
        };
        return retState;
      }
    }

    // Gets or sets the XML text.
    private string HTML { get; set; }

    // Gets or sets the parent HasText value.
    private bool ParentHasText { get; set; }
    #endregion

    #region Class Values

    const bool NoIndent = false;
    #endregion
  }

  /// <summary>Represents the text state.</summary>
  public class TextState
  {
    /// <summary>Indicates if the text has a value.</summary>
    public bool HasText { get; set; }

    /// <summary>The current indent count.</summary>
    public int IndentCount { get; set; }
  }
}
