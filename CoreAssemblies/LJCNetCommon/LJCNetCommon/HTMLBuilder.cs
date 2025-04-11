// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// HTMLBuilder.cs

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

    #region Append Text Methods (3)

    // Adds text without modification.
    /// <include path='items/Add/*' file='Doc/HTMLBuilder.xml'/>
    public void Add(string text)
    {
      HTML += text;
    }

    // Adds a modified text line to the builder.
    /// <include path='items/Line/*' file='Doc/HTMLBuilder.xml'/>
    public string Line(string text, TextState textState)
    {
      // Begin "Append" method.
      var syncState = textState;

      var retText = GetText(text, syncState);
      retText += "\r\n";
      if (TextLength(retText) > 0)
      {
        HTML += retText;
        syncState.HasText = true;
      }
      syncState.HasText = true;

      // End "Append" method.
      textState.HasText = syncState.HasText;
      textState.IndentCount = syncState.IndentCount;
      return retText;
    }

    // Adds modified text to the builder.
    /// <include path='items/Text/*' file='Doc/HTMLBuilder.xml'/>
    public string Text(string text, TextState textState)
    {
      // Begin "Append" method.
      var syncState = textState;

      var retText = GetText(text, syncState);
      if (TextLength(retText) > 0)
      {
        HTML += retText;
        syncState.HasText = true;
      }

      // End "Append" method.
      textState.HasText = syncState.HasText;
      textState.IndentCount = syncState.IndentCount;
      return retText;
    }
    #endregion

    #region Get Text Methods (6)

    // Gets the attributes text.
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
    /// <include path='items/GetLine/*' file='Doc/HTMLBuilder.xml'/>
    public string GetLine(string text, TextState textState)
    {
      var retLine = "";

      retLine += GetText(text, textState);
      retLine += "\r\n";
      return retLine;
    }

    // Gets potentially indented and wrapped text.
    /// <include path='items/GetText/*' file='Doc/HTMLBuilder.xml'/>
    public string GetText(string text, TextState textState)
    {
      var retText = "";

      if (textState.HasText)
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

    #region Append Element Methods (7)

    // Appends the element begin tag.
    /// <include path='items/Begin/*' file='Doc/HTMLBuilder.xml'/>
    public string Begin(string name, TextState textState
      , Attributes htmlAttributes = null, bool applyIndent = true)
    {
      // Begin "Append" method.
      var syncState = textState;

      var retText = GetBegin(name, syncState, htmlAttributes, applyIndent);
      if (TextLength(retText) > 0)
      {
        // Use textState for first "Append" line after "Get String" method.
        Text(retText, textState);
        syncState.HasText = true;
      }

      // End "Append" method.
      textState.HasText = syncState.HasText;
      textState.IndentCount = syncState.IndentCount;
      return retText;
    }

    // Appends an element.
    /// <include path='items/Create/*' file='Doc/HTMLBuilder.xml'/>
    public string Create(string name, string text, TextState textState
      , Attributes htmlAttributes = null, bool applyIndent = true
      , bool isEmpty = false, bool close = true)
    {
      // Begin "Append" method.
      var syncState = textState;

      var retText = GetCreate(name, text, syncState, htmlAttributes, applyIndent
        , isEmpty, close);
      if (TextLength(retText) > 0)
      {
        // Use textState for first "Append" line after "Get String" method.
        Text(retText, textState);
        syncState.HasText = true;
      }

      // End "Append" method.
      textState.HasText = syncState.HasText;
      textState.IndentCount = syncState.IndentCount;
      return retText;
    }

    // Appends the element end tag.
    /// <include path='items/End/*' file='Doc/HTMLBuilder.xml'/>
    public string End(string name, TextState textState
      , bool applyIndent = true)
    {
      // Begin "Append" method.
      var syncState = textState;

      var retText = GetEnd(name, textState, applyIndent);
      if (TextLength(retText) > 0)
      {
        // Use textState for first "Append" line after "Get String" method.
        Text(retText, textState);
        syncState.HasText = true;
      }

      // End "Append" method.
      textState.HasText = syncState.HasText;
      textState.IndentCount = syncState.IndentCount;
      return retText;
    }

    // Appends a <link> element for a style sheet.
    /// <include path='items/Link/*' file='Doc/HTMLBuilder.xml'/>
    public string Link(string fileName, TextState textState)
    {
      // Begin "Append" method.
      var syncState = textState;

      var retValue = GetLink(fileName, syncState);
      if (TextLength(retValue) > 0)
      {
        // Use textState for first "Append" line after "Get String" method.
        Text(retValue, textState);
        syncState.HasText = true;
      }

      // End "Append" method.
      textState.HasText = syncState.HasText;
      textState.IndentCount = syncState.IndentCount;
      return retValue;
    }

    // Appends a <meta> element.
    /// <include path='items/Meta/*' file='Doc/HTMLBuilder.xml'/>
    public string Meta(string name, string content, TextState textState)
    {
      // Begin "Append" method.
      var syncState = textState;

      var retValue = GetMeta(name, content, syncState);
      if (TextLength(retValue) > 0)
      {
        // Use textState for first "Append" line after "Get String" method.
        Text(retValue, textState);
        syncState.HasText = true;
      }

      // End "Append" method.
      textState.HasText = syncState.HasText;
      textState.IndentCount = syncState.IndentCount;
      return retValue;
    }

    // Appends common <meta> elements.
    /// <include path='items/Metas/*' file='Doc/HTMLBuilder.xml'/>
    public string Metas(string author, TextState textState
      , string description = null, string keywords = null
      , string charSet = "utf-8")
    {
      // Begin "Append" method.
      var syncState = textState;

      var retValue = GetMetas(author, syncState, description, keywords
        , charSet);
      if (TextLength(retValue) > 0)
      {
        // Use textState for first "Append" line after "Get String" method.
        Text(retValue, textState);
        syncState.HasText = true;
      }

      // End "Append" method.
      textState.HasText = syncState.HasText;
      textState.IndentCount = syncState.IndentCount;
      return retValue;
    }

    // Appends a <link> element for a style sheet.
    /// <include path='items/Script/*' file='Doc/HTMLBuilder.xml'/>
    public string Script(string fileName, TextState textState)
    {
      // Begin "Append" method.
      var syncState = textState;

      var retValue = GetScript(fileName, syncState);
      if (TextLength(retValue) > 0)
      {
        // Use textState for first "Append" line after "Get String" method.
        Text(retValue, textState);
        syncState.HasText = true;
      }

      // End "Append" method.
      textState.HasText = syncState.HasText;
      textState.IndentCount = syncState.IndentCount;
      return retValue;
    }
    #endregion

    #region Get Element Methods (6)

    /// <summary></summary>
    public string GetBegin(string name, TextState textState
      , Attributes htmlAttributes = null, bool applyIndent = true)
    {
      // Begin "Get String" method.
      var hb = new HTMLBuilder();
      hb.AddIndent(textState.IndentCount);
      var syncState = hb.TextState;

      var createText = GetCreate(name, null, syncState, htmlAttributes
        , applyIndent, false, false);
      if (TextLength(createText) > 0)
      {
        // Use textState for first "Append" line after "Get String" method.
        hb.Text(createText, textState);
      }

      // End "Get String" method.
      textState.IndentCount = syncState.IndentCount;
      var retValue = hb.ToString();
      return retValue;
    }

    // Gets an element.
    /// <include path='items/GetCreate/*' file='Doc/HTMLBuilder.xml'/>
    public string GetCreate(string name, string text, TextState textState
      , Attributes htmlAttributes = null, bool applyIndent = true
      , bool isEmpty = false, bool close = true)
    {
      // Begin "Get String" method.
      var tb = new TextBuilder(128);
      tb.AddIndent(textState.IndentCount);
      var syncState = tb.TextState;
      if (textState.HasText)
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
      var content = Content(text, syncState, isEmpty, out bool isWrapped);
      if (NetString.HasValue(content))
      {
        tb.Text(content);
      }

      // Set indent for child elements.
      if (!isEmpty
        && applyIndent)
      {
        AddSyncIndent(this, tb, syncState);
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
            AddSyncIndent(this, tb, syncState, -1);
          }
          applyIndent = false;
          tb.Text(GetIndentString());
        }
        tb.Text($"</{name}>");
        if (applyIndent)
        {
          // Reset indent for next element.
          AddSyncIndent(this, tb, syncState);
        }
      }

      // End "Get String" method.
      textState.IndentCount = syncState.IndentCount;
      string retElement = tb.ToString();
      return retElement;
    }

    // Gets the element end tag.
    /// <include path='items/GetEnd/*' file='Doc/HTMLBuilder.xml'/>
    public string GetEnd(string name, TextState textState
      , bool applyIndent = true)
    {
      // Begin "Get String" method.
      var tb = new TextBuilder(128);
      tb.AddIndent(textState.IndentCount);
      var syncState = tb.TextState;
      if (textState.HasText)
      {
        tb.Line();
      }

      AddSyncIndent(this, tb, syncState, -1);
      if (applyIndent)
      {
        tb.Text($"{GetIndentString()}");
      }
      tb.Text($"</{name}>");

      // End "Get String" method.
      textState.IndentCount = syncState.IndentCount;
      string retElement = tb.ToString();
      return retElement;
    }

    // Gets the <link> element for a style sheet.
    /// <include path='items/GetLink/*' file='Doc/HTMLBuilder.xml'/>
    public string GetLink(string fileName, TextState textState)
    {
      // Begin "Get String" method.
      var hb = new HTMLBuilder();
      hb.AddIndent(textState.IndentCount);
      var syncState = hb.TextState;

      var attribs = new Attributes()
      {
        { "rel", "stylesheet" },
        { "type", "text/css" },
        { "href", fileName },
      };
      var createText = hb.GetCreate("link", null, syncState, attribs
        , isEmpty: true);
      if (TextLength(createText) > 0)
      {
        // Use textState for first "Append" line after "Get String" method.
        hb.Text(createText, textState);
      }

      // End "Get String" method.
      textState.IndentCount = syncState.IndentCount;
      var retValue = hb.ToString();
      return retValue;
    }

    // Gets a <meta> element.
    /// <include path='items/GetMeta/*' file='Doc/HTMLBuilder.xml'/>
    public string GetMeta(string name, string content, TextState textState)
    {
      // Begin "Get String" method.
      var hb = new HTMLBuilder();
      hb.AddIndent(textState.IndentCount);
      var syncState = hb.TextState;

      var attribs = new Attributes()
      {
        { "name", name },
        { "content", content },
      };
      var createText = hb.GetCreate("meta", null, hb.TextState, attribs
        , isEmpty: true);
      if (TextLength(createText) > 0)
      {
        // Use textState for first "Append" line after "Get String" method.
        hb.Text(createText, textState);
      }

      // End "Get String" method.
      textState.IndentCount = syncState.IndentCount;
      var retValue = hb.ToString();
      return retValue;
    }

    // Gets common <meta> elements.
    /// <include path='items/GetMetas/*' file='Doc/HTMLBuilder.xml'/>
    public string GetMetas(string author, TextState textState
      , string description = null, string keywords = null
      , string charSet = "utf-8")
    {
      // Begin "Get String" method.
      var hb = new HTMLBuilder();
      hb.AddIndent(textState.IndentCount);
      var syncState = hb.TextState;

      var attribs = new Attributes()
      {
        { "charset", charSet }
      };
      var createText = hb.GetCreate("meta", null, hb.TextState, attribs
        , isEmpty: true);
      if (TextLength(createText) > 0)
      {
        // Use textState for first "Append" line after "Get String" method.
        hb.Text(createText, textState);
      }
      if (NetString.HasValue(description))
      {
        hb.Meta("description", description, hb.TextState);
      }
      if (NetString.HasValue(keywords))
      {
        hb.Meta("keywords", keywords, hb.TextState);
      }
      hb.Meta("author", author, hb.TextState);
      var content = "width=device-width initial-scale=1";
      hb.Meta("viewport", content, hb.TextState);

      // End "Get String" method.
      textState.IndentCount = syncState.IndentCount;
      var retValue = hb.ToString();
      return retValue;
    }

    // Gets the <script> element.
    /// <include path='items/GetScript/*' file='Doc/HTMLBuilder.xml'/>
    public string GetScript(string fileName, TextState textState)
    {
      // Begin "Get String" method.
      var hb = new HTMLBuilder();
      hb.AddIndent(textState.IndentCount);
      var syncState = hb.TextState;

      var attribs = new Attributes()
      {
        { "src", fileName },
      };
      var createText = hb.GetCreate("script", null, hb.TextState, attribs);
      if (TextLength(createText) > 0)
      {
        // Use textState for first "Append" line after "Get String" method.
        hb.Text(createText, textState);
      }

      // End "Get String" method.
      textState.IndentCount = syncState.IndentCount;
      var retValue = hb.ToString();
      return retValue;
    }
    #endregion

    #region Append HTML Methods

    // Creates the HTML beginning up to and including <head>.
    /// <include path='items/HTMLBegin/*' file='Doc/HTMLBuilder.xml'/>
    public string HTMLBegin(TextState textState, string[] copyright = null
      , string fileName = null)
    {
      // Begin "Append" method.
      var syncState = textState;

      var retValue = GetHTMLBegin(syncState, copyright, fileName);
      // Use textState for first "Append" line after "Get String" method.
      Text(retValue, textState);

      // End "Append" method.
      textState.HasText = syncState.HasText;
      textState.IndentCount = syncState.IndentCount;
      return retValue;
    }
    #endregion

    #region Get HTML Methods (2)

    // Gets the HTML beginning up to <head>.
    /// <include path='items/GetHTMLBegin/*' file='Doc/HTMLBuilder.xml'/>
    public string GetHTMLBegin(TextState textState, string[] copyright = null
      , string fileName = null)
    {
      // Begin "Get String" method.
      var hb = new HTMLBuilder();
      hb.AddIndent(textState.IndentCount);
      var syncState = hb.TextState;

      hb.Text("<!DOCTYPE html>", syncState);
      if (NetCommon.HasElements(copyright))
      {
        foreach (string line in copyright)
        {
          hb.Text($"<!-- {line} -->", syncState);
        }
      }
      if (NetString.HasValue(fileName))
      {
        hb.Text($"<!-- {fileName} -->", syncState);
      }
      var startAttribs = hb.StartAttribs();
      var text = hb.GetCreate("html", null, syncState, startAttribs
        , NoIndent, close: false);
      // Use textState for first "Append" line after "Get String" method.
      hb.Text(text, textState);

      text = hb.GetCreate("head", null, syncState, applyIndent: NoIndent);
      hb.Text(text, syncState);
      AddSyncIndent(hb, null, syncState);

      // End "Get String" method.
      textState.IndentCount = syncState.IndentCount;
      var retValue = hb.ToString();
      return retValue;
    }

    // Gets the HTML end <body> and <html>.
    /// <include path='items/GetHTMLEnd/*' file='Doc/HTMLBuilder.xml'/>
    public string GetHTMLEnd(TextState textState)
    {
      // Begin "Get String" method.
      var hb = new HTMLBuilder();
      hb.AddIndent(textState.IndentCount);
      var syncState = hb.TextState;

      var text = hb.GetEnd("body", syncState, NoIndent);
      // Use textState for first "Append" line after "Get String" method.
      hb.Text(text, textState);

      text = hb.GetEnd("html", syncState, NoIndent);
      hb.Text(text, syncState);
      AddSyncIndent(hb, null, syncState);

      // End "Get String" method.
      textState.IndentCount = syncState.IndentCount;
      var retValue = hb.ToString();
      return retValue;
    }
    #endregion

    #region Get Element Attribs Methods (3)

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

    // Adds indent to builders and sync object.
    private void AddSyncIndent(HTMLBuilder hb, TextBuilder tb
      , TextState syncState, int value = 1)
    {
      hb?.AddIndent(value);
      tb?.AddIndent(value);
      syncState.IndentCount += value;
    }

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
    private string Content(string text, TextState textState, bool isEmpty
      , out bool isWrapped)
    {
      string retValue = "";

      // Begin "Get String" method.
      var hb = new HTMLBuilder();
      hb.AddIndent(textState.IndentCount);
      var syncState = hb.TextState;

      isWrapped = false;
      // Add text content.
      if (!isEmpty
        && NetString.HasValue(text))
      {
        if (text.Length > 80 - IndentLength)
        {
          isWrapped = true;
          retValue += "\r\n";
          AddSyncIndent(hb, null, syncState);
          var textValue = GetText(text, syncState);
          retValue += textValue;
          AddSyncIndent(hb, null, syncState, -1);
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

      // End "Get String" method.
      textState.IndentCount = syncState.IndentCount;
      return retValue;
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
        // ToDo: Remove ParentHasText?
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

    /// <summary>Gets the indent count.</summary>
    public int IndentCount { get; private set; }

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

    /// <summary>Gets the current text state.</summary>
    public TextState TextState
    {
      get
      {
        if (mTextState == null)
        {
          mTextState = new TextState();
        }
        mTextState.HasText = HasText;
        mTextState.IndentCount = IndentCount;
        return mTextState;
      }
      set
      {
        if (value != null)
        {
          mTextState = value;
        }
      }
    }
    private TextState mTextState;

    // Gets or sets the XML text.
    private string HTML { get; set; }

    // Gets or sets the parent HasText value.
    private bool ParentHasText { get; set; }
    #endregion

    #region Class Values

    const bool NoIndent = false;
    #endregion
  }
}
