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
      Builder = new StringBuilder(128);
      IndentCharCount = 2;
      IndentCount = 0;
      if (textState != null)
      {
        // Sync the important values.
        AddIndent(textState.IndentCount);
      }
      LineLength = 0;
      LineLimit = 80;
      WrapEnabled = false;
    }
    #endregion

    #region Data Class Methods

    /// <summary>Implements the ToString() method.</summary>
    public override string ToString()
    {
      return Builder.ToString();
    }
    #endregion

    #region Methods

    // Changes the IndentCount by the provided value.
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

    #region Append Text Methods (4)

    // Adds text without modification.
    /// <include path='items/AddText/*' file='Doc/HTMLBuilder.xml'/>
    public void AddText(string text)
    {
      if (TextLength(text) > 0)
      {
        Builder.Append(text);
      }
    }

    // Adds a text line without modification.
    /// <include path='items/AddLine/*' file='Doc/HTMLBuilder.xml'/>
    public string AddLine(string text)
    {
      string retText = "";
      if (TextLength(text) > 0)
      {
        retText = text;
      }
      Builder.AppendLine(retText);
      retText = $"{retText}\r\n";
      return retText;
    }

    // Adds a modified text line to the builder.
    /// <include path='items/Line/*' file='Doc/HTMLBuilder.xml'/>
    public string Line(string text)
    {
      string retText = "";
      if (TextLength(text) > 0)
      {
        retText = GetText(text);
      }
      Builder.AppendLine(retText);
      retText = $"{retText}\r\n";
      return retText;
    }

    // Adds modified text to the builder.
    /// <include path='items/Text/*' file='Doc/HTMLBuilder.xml'/>
    public string Text(string text, bool addIndent = true
      , bool allowNewLine = true)
    {
      var retText = GetText(text, addIndent, allowNewLine);
      if (TextLength(retText) > 0)
      {
        Builder.Append(retText);
      }
      return retText;
    }
    #endregion

    #region Get Text Methods (6)

    // Gets the attributes text.
    /// <include path='items/GetAttribs/*' file='Doc/HTMLBuilder.xml'/>
    public string GetAttribs(Attributes htmlAttribs, TextState textState)
    {
      string retText = "";

      if (NetCommon.HasItems(htmlAttribs))
      {
        var tb = new TextBuilder(textState);
        var isFirst = true;
        foreach (Attribute htmlAttrib in htmlAttribs)
        {
          if (!isFirst)
          {
            // Wrap line for large attribute value.
            if (NetString.HasValue(htmlAttrib.Value)
              && htmlAttrib.Value.Length > 35)
            {
              tb.AddText($"\r\n{GetIndentString()}");
            }
          }
          isFirst = false;

          tb.AddText($" {htmlAttrib.Name}");
          if (NetString.HasValue(htmlAttrib.Value))
          {
            tb.AddText($"=\"{htmlAttrib.Value}\"");
          }
        }
        retText = tb.ToString();
      }
      return retText;
    }

    // Gets a new potentially indented line.
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
    public string GetLine(string text, bool addIndent = true
      , bool allowNewLine = true)
    {
      var retLine = "";

      retLine += GetText(text, addIndent, allowNewLine);
      retLine += "\r\n";
      return retLine;
    }

    // Gets potentially indented and wrapped text.
    /// <include path='items/GetText/*' file='Doc/HTMLBuilder.xml'/>
    public string GetText(string text, bool addIndent = true
      , bool allowNewLine = true)
    {
      var retText = "";

      if (TextLength(text) > 0)
      {
        retText = text;
        if (addIndent)
        {
          retText = GetIndented(text);
        }

        if (allowNewLine
          && HasText)
        {
          retText = "\r\n";
          if (addIndent)
          {
            retText += GetIndentString();
          }
          retText += text;
        }

        if (WrapEnabled)
        {
          retText = GetWrapped(retText);
        }
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
          var addText = GetAddText(retText, wrapIndex);
          buildText += $"{addText}\r\n";

          // Next text up to LineLimit - prepend length without leading space.
          string wrapText = WrapText(workText, wrapIndex);
          // *** Different than TextBuilder ***
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
      , Attributes htmlAttribs = null, bool addIndent = true
      , bool childIndent = true)
    {
      var createText = GetBegin(name, textState, htmlAttribs, addIndent
        , childIndent);
      // Use NoIndent after a "GetText" method.
      Text(createText, NoIndent);
      // Use AddChildIndent after beginning an element.
      AddChildIndent(createText, textState);

      // Append Method
      UpdateState(textState);
      return createText;
    }

    // Appends an element.
    /// <include path='items/Create/*' file='Doc/HTMLBuilder.xml'/>
    public string Create(string name, string text, TextState textState
      , Attributes htmlAttribs = null, bool addIndent = true
      , bool childIndent = true, bool isEmpty = false, bool close = true)
    {
      // Adds the indent string.
      var createText = GetCreate(name, text, textState, htmlAttribs
        , addIndent, childIndent, isEmpty, close);
      // Use NoIndent after a "GetText" method.
      Text(createText, NoIndent);
      if (!close)
      {
        // Use AddChildIndent after beginning an element.
        AddChildIndent(createText, textState);
      }

      // Append Method
      UpdateState(textState);
      return createText;
    }

    // Appends the element end tag.
    /// <include path='items/End/*' file='Doc/HTMLBuilder.xml'/>
    public string End(string name, TextState textState
      , bool addIndent = true)
    {
      var createText = GetEnd(name, textState, addIndent);
      // Use NoIndent after a GetEnd().
      Text(createText, NoIndent);

      // Append Method
      UpdateState(textState);
      return createText;
    }

    // Appends a <link> element for a style sheet.
    /// <include path='items/Link/*' file='Doc/HTMLBuilder.xml'/>
    public string Link(string fileName, TextState textState)
    {
      var createText = GetLink(fileName, textState);
      // Use NoIndent after a "GetText" method.
      Text(createText, NoIndent);

      // Append Method
      UpdateState(textState);
      return createText;
    }

    // Appends a <meta> element.
    /// <include path='items/Meta/*' file='Doc/HTMLBuilder.xml'/>
    public string Meta(string name, string content, TextState textState)
    {
      var createText = GetMeta(name, content, textState);
      // Use NoIndent after a "GetText" method.
      Text(createText, NoIndent);

      // Append Method
      UpdateState(textState);
      return createText;
    }

    // Appends common <meta> elements.
    /// <include path='items/Metas/*' file='Doc/HTMLBuilder.xml'/>
    public string Metas(string author, TextState textState
      , string description = null, string keywords = null
      , string charSet = "utf-8")
    {
      var createText = GetMetas(author, textState, description, keywords
        , charSet);
      // Use NoIndent after a "GetText" method.
      Text(createText, NoIndent);

      // Append Method
      UpdateState(textState);
      return createText;
    }

    // Appends a <script> element for a style sheet.
    /// <include path='items/Script/*' file='Doc/HTMLBuilder.xml'/>
    public string Script(string fileName, TextState textState)
    {
      var createText = GetScript(fileName, textState);
      // Use NoIndent after a "GetText" method.
      Text(createText, NoIndent);

      // Append Method
      UpdateState(textState);
      return createText;
    }
    #endregion

    #region Get Element Methods (9)

    // Adds the new (child) indents.
    /// <include path='items/AddChildIndent/*' file='Doc/HTMLBuilder.xml'/>
    public void AddChildIndent(string createText, TextState textState)
    {
      if (TextLength(createText) > 0
        && textState.ChildIndentCount > 0)
      {
        AddIndent(textState.ChildIndentCount);
        textState.IndentCount += textState.ChildIndentCount;
        textState.ChildIndentCount = 0;
      }
    }

    // Gets the element begin tag.
    /// <include path='items/GetBegin/*' file='Doc/HTMLBuilder.xml'/>
    public string GetBegin(string name, TextState textState
      , Attributes htmlAttribs = null, bool addIndent = true
      , bool childIndent = true)
    {
      var hb = new HTMLBuilder(textState);
      var createText = GetCreate(name, null, textState, htmlAttribs
        , addIndent, childIndent, close: false);
      // Use NoIndent after a "GetText" method.
      hb.Text(createText, NoIndent);
      // Only use AddChildIndent() if additional text is added in this method.
      var retValue = hb.ToString();
      return retValue;
    }

    // Gets beginning of style selector.
    /// <include path='items/GetBeginSelector/*' file='Doc/HTMLBuilder.xml'/>
    public string GetBeginSelector(string selectorName, TextState textState)
    {
      var hb = new HTMLBuilder(textState);
      hb.Text(selectorName);
      hb.Text("{");
      var retValue = hb.ToString();
      return retValue;
    }

    // Gets the element text.
    /// <include path='items/GetCreate/*' file='Doc/HTMLBuilder.xml'/>
    public string GetCreate(string name, string text, TextState textState
      , Attributes htmlAttribs = null, bool addIndent = true
      , bool childIndent = true, bool isEmpty = false, bool close = true)
    {
      textState.ChildIndentCount = 0; // ?
      var tb = new TextBuilder(textState);

      // Start text with the opening tag.
      tb.Text($"<{name}", addIndent);
      var getText = GetAttribs(htmlAttribs, textState);
      tb.AddText(getText);
      if (isEmpty)
      {
        tb.AddText(" /");
        close = false;
      }
      tb.AddText(">");

      // Content is added if not an empty element.
      var isWrapped = false;
      if (!isEmpty)
      {
        var content = Content(text, textState, isEmpty, out isWrapped);
        tb.AddText(content);
      }

      // Close the element.
      if (close)
      {
        if (isWrapped)
        {
          tb.Line();
          tb.AddText(GetIndentString());
        }
        tb.AddText($"</{name}>");
      }

      // Increment ChildIndentCount if not empty and not closed.
      if (!isEmpty
        && !close
        && childIndent)
      {
        textState.ChildIndentCount++;
      }
      var retElement = tb.ToString();
      return retElement;
    }

    // Gets the element end tag.
    /// <include path='items/GetEnd/*' file='Doc/HTMLBuilder.xml'/>
    public string GetEnd(string name, TextState textState
      , bool addIndent = true)
    {
      var tb = new TextBuilder(textState);
      AddSyncIndent(this, tb, textState, -1);
      if (addIndent)
      {
        tb.AddText($"{GetIndentString()}");
      }
      tb.AddText($"</{name}>");
      var retElement = tb.ToString();
      return retElement;
    }

    // Gets the <link> element for a style sheet.
    /// <include path='items/GetLink/*' file='Doc/HTMLBuilder.xml'/>
    public string GetLink(string fileName, TextState textState)
    {
      var hb = new HTMLBuilder(textState);
      var attribs = new Attributes()
      {
        { "rel", "stylesheet" },
        { "type", "text/css" },
        { "href", fileName },
      };
      var createText = hb.GetCreate("link", null, textState, attribs
        , isEmpty: true);
      // Use NoIndent after a "GetText" method.
      hb.Text(createText, NoIndent);
      var retValue = hb.ToString();
      return retValue;
    }

    // Gets a <meta> element.
    /// <include path='items/GetMeta/*' file='Doc/HTMLBuilder.xml'/>
    public string GetMeta(string name, string content, TextState textState)
    {
      var hb = new HTMLBuilder(textState);
      var attribs = new Attributes()
      {
        { "name", name },
        { "content", content },
      };
      var createText = hb.GetCreate("meta", null, textState, attribs
        , isEmpty: true);
      // Use NoIndent after a "GetText" method.
      Text(createText, NoIndent);
      var retValue = hb.ToString();
      return retValue;
    }

    // Gets common <meta> elements.
    /// <include path='items/GetMetas/*' file='Doc/HTMLBuilder.xml'/>
    public string GetMetas(string author, TextState textState
      , string description = null, string keywords = null
      , string charSet = "utf-8")
    {
      var hb = new HTMLBuilder(textState);
      var attribs = new Attributes()
      {
        { "charset", charSet }
      };
      var createText = hb.GetCreate("meta", null, textState, attribs
        , isEmpty: true);
      // Use NoIndent after a "GetText" method.
      hb.Text(createText, NoIndent);

      if (NetString.HasValue(description))
      {
        hb.Meta("description", description, textState);
      }
      if (NetString.HasValue(keywords))
      {
        hb.Meta("keywords", keywords, textState);
      }
      hb.Meta("author", author, textState);
      var content = "width=device-width initial-scale=1";
      hb.Meta("viewport", content, textState);
      var retValue = hb.ToString();
      return retValue;
    }

    // Gets the <script> element.
    /// <include path='items/GetScript/*' file='Doc/HTMLBuilder.xml'/>
    public string GetScript(string fileName, TextState textState)
    {
      var hb = new HTMLBuilder(textState);
      var attribs = new Attributes()
      {
        { "src", fileName },
      };
      var createText = hb.GetCreate("script", null, textState, attribs);
      // Use NoIndent after a "GetText" method.
      hb.Text(createText, NoIndent);
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
      var retValue = GetHTMLBegin(textState, copyright, fileName);
      Text(retValue);

      // Append Method
      UpdateState(textState);
      return retValue;
    }
    #endregion

    #region Get HTML Methods (3)

    // Gets the HTML beginning up to <head>.
    /// <include path='items/GetHTMLBegin/*' file='Doc/HTMLBuilder.xml'/>
    public string GetHTMLBegin(TextState textState, string[] copyright = null
      , string fileName = null)
    {
      var hb = new HTMLBuilder(textState);
      hb.Text("<!DOCTYPE html>");
      if (NetCommon.HasElements(copyright))
      {
        foreach (string line in copyright)
        {
          hb.Text($"<!-- {line} -->");
        }
      }
      if (NetString.HasValue(fileName))
      {
        hb.Text($"<!-- {fileName} -->");
      }
      var startAttribs = hb.StartAttribs();
      var createText = hb.GetBegin("html", textState, startAttribs
        , NoIndent);
      // Use NoIndent after a "GetText" method.
      hb.Text(createText, NoIndent);
      createText = hb.GetBegin("head", textState, null, NoIndent);
      // Use NoIndent after a "GetText" method.
      hb.Text(createText, NoIndent);
      // Only use AddChildIndent() if additional text is added in this method.
      var retValue = hb.ToString();
      return retValue;
    }

    // Gets the HTML end <body> and <html>.
    /// <include path='items/GetHTMLEnd/*' file='Doc/HTMLBuilder.xml'/>
    public string GetHTMLEnd(TextState textState)
    {
      var hb = new HTMLBuilder(textState);
      var text = hb.GetEnd("body", textState, NoIndent);
      hb.Text(text);

      text = hb.GetEnd("html", textState, NoIndent);
      hb.Text(text);
      AddSyncIndent(hb, null, textState);
      var retValue = hb.ToString();
      return retValue;
    }

    // Gets the main HTML Head elements.
    /// <include path='items/GetHTMLHead/*' file='Doc/HTMLBuilder.xml'/>
    public string GetHTMLHead(TextState textState, string title = null
      , string author = null, string description = null)
    {
      var hb = new HTMLBuilder(textState);
      hb.Create("title", title, textState, childIndent: false);
      hb.Metas(author, textState, description);
      var retValue = hb.ToString();
      return retValue;
    }
    #endregion

    #region Get Attribs Methods (3)

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

    #region Private Methods (7)

    // Adds indent to builders and sync object.
    private void AddSyncIndent(HTMLBuilder hb, TextBuilder tb
      , TextState syncState, int value = 1)
    {
      hb?.AddIndent(value);
      tb?.AddIndent(value);
      syncState.IndentCount += value;
    }

    // Creates the content text.
    private string Content(string text, TextState textState, bool isEmpty
      , out bool isWrapped)
    {
      string retValue = "";

      var hb = new HTMLBuilder(textState);
      isWrapped = false;
      // Add text content.
      if (!isEmpty
        && NetString.HasValue(text))
      {
        if (text.Length > 80 - IndentLength)
        {
          isWrapped = true;
          retValue += "\r\n";
          AddSyncIndent(hb, null, textState);
          var textValue = GetText(text);
          retValue += textValue;
          AddSyncIndent(hb, null, textState, -1);
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

    // Gets the text to add to the existing line.
    private string GetAddText(string text, int addLength)
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

    // Updates the text state values.
    private void UpdateState(TextState textState)
    {
      IndentCount = textState.IndentCount;
      NewIndentCount = textState.ChildIndentCount;
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

        // *** Different than TextBuilder ***
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
      // *** Different than TextBuilder ***
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
        // *** Different than TextBuilder ***
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
        if (Builder.Length > 0)
        {
          retValue = true;
        }
        return retValue;
      }
    }

    /// <summary>Gets or sets the indent character count.</summary>
    public int IndentCharCount { get; set; }

    /// <summary>Gets the indent count.</summary>
    public int IndentCount
    {
      get { return mIndentCount; }
      set
      {
        if (value >= 0)
        {
          mIndentCount = value;
        }
      }
    }
    private int mIndentCount;

    /// <summary>Gets the current indent length.</summary>
    public int IndentLength
    {
      get
      {
        return IndentCount * IndentCharCount;
      }
    }

    /// <summary>Gets the current length.</summary>
    public int LineLength { get; private set; }

    /// <summary>Gets or sets the character limit.</summary>
    public int LineLimit { get; set; }

    /// <summary>Gets or sets the new indent count.</summary>
    public int NewIndentCount { get; set; }

    /// <summary>Indicates if line wrapping is enabled.</summary>
    public bool WrapEnabled { get; set; }

    // Gets or sets the XML text.
    private StringBuilder Builder { get; set; }
    #endregion

    #region Class Data

    private const bool NoIndent = false;
    #endregion
  }
}
