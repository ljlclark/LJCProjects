// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCHTMLBuilder.cs
using System.Text;

namespace LJCNetCommon5
{
  // Provides methods for creating HTML text.
  /// <include path="members/HTMLBuilder/*" file="Doc/LJCHTMLBuilder.xml"/>
  public class LJCHTMLBuilder
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path="members/HTMLBuilder/*" file="Doc/LJCHTMLBuilder.xml"/>
    public LJCHTMLBuilder(LJCTextState? textState = null)
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

    // Retrieves the object text.
    /// <summary>Implements the ToString() method.</summary>
    public override string ToString()
    {
      return Builder.ToString();
    }
    #endregion

    #region Methods

    // Adds the new (child) indents.
    // Use AddChildIndent after beginning an element.
    /// <include path="members/AddChildIndent/*" file="Doc/LJCHTMLBuilder.xml"/>
    public void AddChildIndent(string createText, LJCTextState textState)
    {
      var childIndentCount = textState.ChildIndentCount;

      //if (NetString.HasValue(createText)
      if (TextLength(createText) > 0
        && childIndentCount > 0)
      {
        AddIndent(childIndentCount);
        textState.IndentCount += childIndentCount;
        textState.ChildIndentCount = 0;
      }
    }

    // Changes the IndentCount by the provided value.
    /// <include path="members/AddIndent/*" file="Doc/LJCHTMLBuilder.xml"/>
    public int AddIndent(int increment = 1)
    {
      IndentCount += increment;
      //if (IndentCount < 0)
      //{
      //  IndentCount = 0;
      //}
      return IndentCount;
    }

    // Indicates if the builder text ends with a newline.
    /// <include path="members/EndsWithNewLine/*" file="Doc/LJCHTMLBuilder.xml"/>
    public bool EndsWithNewLine()
    {
      var retValue = false;

      // *** Add ***
      var length = Builder.Length;
      if (length > 0)
      {
        if ('\n' == Builder[length - 1])
        {
          retValue = true;
        }
      }
      return retValue;
    }

    /// <summary>Indicates if the builder has text.</summary>
    /// <returns>true if builder has text; otherwise false.</returns>
    public bool HasText()
    {
      bool retValue = false;

      if (Builder.Length > 0)
      {
        retValue = true;
      }
      return retValue;
    }

    /// <summary>Gets the current indent length.</summary>
    /// <include path="members/HTMLBuilder/*" file="Doc/LJCHTMLBuilder.xml"/>
    public int IndentLength()
    {
      return IndentCount * IndentCharCount;
    }

    // Checks if text can start with a newline.
    /// <include path="members/StartWithNewLine/*" file="Doc/LJCHTMLBuilder.xml"/>
    public bool StartWithNewLine(bool allowNewLine)
    {
      bool retValue = false;

      if (allowNewLine
        && HasText()
        && !EndsWithNewLine())
      {
        retValue = true;
      }
      return retValue;
    }
    #endregion

    #region Text Methods

    // Adds a text line without modification.
    /// <include path="members/AddLine/*" file="Doc/LJCHTMLBuilder.xml"/>
    public string AddLine(string? text = null)
    {
      var retText = $"{text}\r\n";
      Builder.Append(retText);
      return retText;
    }

    // Adds text without modification.
    /// <include path="members/AddText/*" file="Doc/LJCHTMLBuilder.xml"/>
    public void AddText(string text)
    {
      //if (NetString.HasValue(text))
      if (TextLength(text) > 0)
      {
        Builder.Append(text);
      }
    }

    // Adds a potentially modified text line to the builder.
    /// <include path="members/Line/*" file="Doc/LJCHTMLBuilder.xml"/>
    public string Line(string? text = null, bool addIndent = true
      , bool allowNewLine = true)
    {
      var retText = GetLine(text, addIndent, allowNewLine);
      Builder.Append(retText);
      return retText;
    }

    // Adds potentially modified text to the builder.
    /// <include path="members/Text/*" file="Doc/LJCHTMLBuilder.xml"/>
    public string Text(string text, bool addIndent = true
      , bool allowNewLine = true)
    {
      var retText = GetText(text, addIndent, allowNewLine);
      //if (NetString.HasValue(retText))
      if (TextLength(retText) > 0)
      {
        Builder.Append(retText);
      }
      return retText;
    }
    #endregion

    #region Get Text Methods

    // Gets the attributes text.
    /// <include path="members/GetAttribs/*" file="Doc/LJCHTMLBuilder.xml"/>
    public string GetAttribs(LJCAttributes? attribs, LJCTextState textState)
    {
      string retText = "";

      if (LJC.HasItems(attribs))
      {
        var hb = new LJCHTMLBuilder(textState);
        var isFirst = true;
        foreach (LJCAttribute attrib in attribs)
        {
          var name = attrib.Name;
          var value = attrib.Value;

          if (!isFirst)
          {
            // Wrap line for large attribute value.
            if (LJC.HasValue(value)
              && value.Length > 35)
            {
              hb.AddText($"\r\n{GetIndentString()}");
            }
          }
          isFirst = false;

          hb.AddText($" {name}");
          if (LJC.HasValue(value))
          {
            hb.AddText($"=\"{value}\"");
          }
        }
        retText = hb.ToString();
      }
      return retText;
    }

    // Gets a new potentially indented line.
    /// <include path="members/GetIndented/*" file="Doc/LJCHTMLBuilder.xml"/>
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
    /// <include path="members/GetIndentString/*" file="Doc/LJCHTMLBuilder.xml"/>
    public string GetIndentString()
    {
      var retValue = new string(' ', IndentLength());
      return retValue;
    }

    // Gets a modified text line.
    /// <include path="members/GetLine/*" file="Doc/LJCHTMLBuilder.xml"/>
    public string GetLine(string? text = null, bool addIndent = true
      , bool allowNewLine = true)
    {
      var retLine = GetText(text, addIndent, allowNewLine);
      retLine += "\r\n";
      return retLine;
    }

    // Gets potentially indented and wrapped text.
    /// <include path="members/GetText/*" file="Doc/LJCHTMLBuilder.xml"/>
    public string GetText(string? text, bool addIndent = true
      , bool allowNewLine = true)
    {
      var retText = "";

      // Start with newline if text exists.
      if (StartWithNewLine(allowNewLine))
      {
        retText = "\r\n";
      }

      if (LJC.HasValue(text))
      {
        retText += text;

        if (addIndent)
        {
          // Recreate string.
          retText = GetIndented(text);
        }

        if (StartWithNewLine(allowNewLine))
        {
          // Recreate string.
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
    /// <include path="members/GetWrapped/*" file="Doc/LJCHTMLBuilder.xml"/>
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

    #region Element Methods

    // Appends the element begin tag.
    /// <include path="members/Begin/*" file="Doc/LJCHTMLBuilder.xml"/>
    public string Begin(string name, LJCTextState textState
      , LJCAttributes? attribs = null, bool addIndent = true
      , bool childIndent = true)
    {
      var createText = GetBegin(name, textState, attribs, addIndent
        , childIndent);
      var indent = false;
      Text(createText, indent);

      // Use AddChildIndent after beginning an element.
      AddChildIndent(createText, textState);

      // Append Method
      UpdateState(textState);
      return createText;
    }

    // Appends an element.
    /// <include path="members/Create/*" file="Doc/LJCHTMLBuilder.xml"/>
    public string Create(string name, string? text, LJCTextState textState
      , LJCAttributes? attribs = null, bool addIndent = true
      , bool childIndent = true, bool isEmpty = false, bool close = true)
    {
      // Adds the indent string.
      var createText = GetCreate(name, text, textState, attribs
        , addIndent, childIndent, isEmpty, close);
      var indent = false;
      Text(createText, indent);
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
    /// <include path="members/End/*" file="Doc/LJCHTMLBuilder.xml"/>
    public string End(string name, LJCTextState textState, bool addIndent = true)
    {
      var createText = GetEnd(name, textState, addIndent);
      var indent = false;
      Text(createText, indent);

      // Append Method
      UpdateState(textState);
      return createText;
    }
    #endregion

    #region Get Element Methods

    // Gets the element begin tag.
    /// <include path="members/GetBegin/*" file="Doc/LJCHTMLBuilder.xml"/>
    public string GetBegin(string name, LJCTextState textState
      , LJCAttributes? attribs = null, bool addIndent = true
      , bool childIndent = true)
    {
      var hb = new LJCHTMLBuilder(textState);

      var createText = GetCreate(name, null, textState, attribs
        , addIndent, childIndent, close: false);
      var indent = false;
      hb.Text(createText, indent);

      // Only use AddChildIndent() if additional text is added in this method.
      var retValue = hb.ToString();
      return retValue;
    }

    // Gets beginning of style selector.
    /// <include path="members/GetBeginSelector/*" file="Doc/LJCHTMLBuilder.xml"/>
    public string GetBeginSelector(string selectorName,  LJCTextState textState)
    {
      var hb = new LJCHTMLBuilder(textState);

      hb.Text(selectorName);
      hb.AddText(" {");

      var retValue = hb.ToString();
      return retValue;
    }

    // Gets the element text.
    /// <include path="members/GetCreate/*" file="Doc/LJCHTMLBuilder.xml"/>
    public string GetCreate(string name, string? text, LJCTextState textState
      , LJCAttributes? attribs = null, bool addIndent = true
      , bool childIndent = true, bool isEmpty = false, bool close = true)
    {
      textState.ChildIndentCount = 0; // ?
      var hb = new LJCHTMLBuilder(textState);

      // Start text with the opening tag.
      hb.Text($"<{name}", addIndent);
      var getText = GetAttribs(attribs, textState);
      hb.AddText(getText);
      if (isEmpty)
      {
        hb.AddText(" /");
        close = false;
      }
      hb.AddText(">");

      // Content is added if not an empty element.
      var isWrapped = false;
      if (!isEmpty
        && LJC.HasValue(text))
      {
        var content = Content(text, textState, isEmpty, out isWrapped);
        hb.AddText(content);
      }

      // Close the element.
      if (close)
      {
        if (isWrapped)
        {
          hb.Line();
          hb.AddText(GetIndentString());
        }
        hb.AddText($"</{name}>");
      }

      // Increment ChildIndentCount if not empty and not closed.
      if (!isEmpty
        && !close
        && childIndent)
      {
        textState.ChildIndentCount++;
      }

      var retElement = hb.ToString();
      return retElement;
    }

    // Gets the element end tag.
    /// <include path="members/GetEnd/*" file="Doc/LJCHTMLBuilder.xml"/>
    public string GetEnd(string name, LJCTextState textState
      , bool addIndent = true)
    {
      var hb = new LJCHTMLBuilder(textState);

      AddSyncIndent(hb, textState, -1);
      hb.Text($"</{name}>", addIndent);

      var retElement = hb.ToString();
      return retElement;
    }
    #endregion

    #region Create Element Methods

    // Appends a <link> element for a style sheet.
    /// <include path="members/Link/*" file="Doc/LJCHTMLBuilder.xml"/>
    public string Link(string fileName, LJCTextState textState)
    {
      var createText = GetLink(fileName, textState);
      Text(createText, false);

      // Append Method
      UpdateState(textState);
      return createText;
    }

    // Appends a <meta> element.
    /// <include path="members/Meta/*" file="Doc/LJCHTMLBuilder.xml"/>
    public string Meta(string name, string? content, LJCTextState textState)
    {
      var createText = GetMeta(name, content, textState);
      Text(createText, false);

      // Append Method
      UpdateState(textState);
      return createText;
    }

    // Appends common <meta> elements.
    /// <include path="members/Metas/*" file="Doc/LJCHTMLBuilder.xml"/>
    public string Metas(string? author, LJCTextState textState
      , string? description = null, string? keywords = null
      , string charSet = "utf-8")
    {
      var createText = GetMetas(author, textState, description, keywords
        , charSet);
      Text(createText, false);

      // Append Method
      UpdateState(textState);
      return createText;
    }

    // Appends a <script> element for a script file.
    /// <include path="members/Script/*" file="Doc/LJCHTMLBuilder.xml"/>
    public string Script(string fileName, LJCTextState textState)
    {
      var createText = GetScript(fileName, textState);
      Text(createText, false);

      // Append Method
      UpdateState(textState);
      return createText;
    }

    // Gets the <link> element for a style sheet.
    /// <include path="members/GetLink/*" file="Doc/LJCHTMLBuilder.xml"/>
    public string GetLink(string fileName, LJCTextState textState)
    {
      var hb = new LJCHTMLBuilder(textState);

      var attribs = new LJCAttributes()
      {
        { "rel", "stylesheet" },
        { "type", "text/css" },
        { "href", fileName },
      };
      var createText = hb.GetCreate("link", null, textState, attribs
        , isEmpty: true);
      hb.Text(createText, false);

      var retValue = hb.ToString();
      return retValue;
    }

    // Gets a <meta> element.
    /// <include path="members/GetMeta/*" file="Doc/LJCHTMLBuilder.xml"/>
    public string GetMeta(string name, string? content, LJCTextState textState)
    {
      var hb = new LJCHTMLBuilder(textState);

      var attribs = new LJCAttributes()
      {
        { "name", name },
      };
      if (content != null)
      {
        attribs.Add("content", content);
      }
      var createText = hb.GetCreate("meta", null, textState, attribs
        , isEmpty: true);
      hb.Text(createText, false);

      var retValue = hb.ToString();
      return retValue;
    }

    // Gets common <meta> elements.
    /// <include path="members/GetMetas/*" file="Doc/LJCHTMLBuilder.xml"/>
    public string GetMetas(string? author, LJCTextState textState
      , string? description = null, string? keywords = null
      , string charSet = "utf-8")
    {
      var hb = new LJCHTMLBuilder(textState);

      var attribs = new LJCAttributes()
      {
        { "charset", charSet }
      };
      var createText = hb.GetCreate("meta", null, textState, attribs
        , isEmpty: true);
      hb.Text(createText, false);

      if (LJC.HasValue(description))
      {
        hb.Meta("description", description, textState);
      }
      if (LJC.HasValue(keywords))
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
    /// <include path="members/GetScript/*" file="Doc/LJCHTMLBuilder.xml"/>
    public string GetScript(string fileName, LJCTextState textState)
    {
      var hb = new LJCHTMLBuilder(textState);

      var attribs = new LJCAttributes()
      {
        { "src", fileName },
      };
      var createText = hb.GetCreate("script", null, textState, attribs);
      hb.Text(createText, false);

      var retValue = hb.ToString();
      return retValue;
    }
    #endregion

    #region HTML Methods

    // Creates the HTML beginning up to and including <head>.
    /// <include path="members/HTMLBegin/*" file="Doc/LJCHTMLBuilder.xml"/>
    public string HTMLBegin(LJCTextState textState, string[]? copyright = null
      , string? fileName = null)
    {
      var retValue = GetHTMLBegin(textState, copyright, fileName);
      var indent = false;
      Text(retValue, indent);

      // Append Method
      UpdateState(textState);
      return retValue;
    }

    // Gets the HTML beginning up to <head>.
    /// <include path="members/GetHTMLBegin/*" file="Doc/LJCHTMLBuilder.xml"/>
    public string GetHTMLBegin(LJCTextState textState, string[]? copyright = null
      , string? fileName = null)
    {
      var hb = new LJCHTMLBuilder(textState);

      hb.Text("<!DOCTYPE html>");
      if (LJC.HasElements(copyright))
      {
        foreach (string line in copyright)
        {
          hb.Text($"<!-- {line} -->");
        }
      }
      if (LJC.HasValue(fileName))
      {
        hb.Text($"<!-- {fileName} -->");
      }

      var startAttribs = hb.StartAttribs();
      var createText = hb.GetBegin("html", textState, startAttribs
        , false);
      hb.Text(createText, false);

      createText = hb.GetBegin("head", textState, null, false);
      hb.Text(createText, false);

      // Only use AddChildIndent() if additional text is added in this method.
      var retValue = hb.ToString();
      return retValue;
    }

    // Gets the HTML end <body> and <html>.
    /// <include path="members/GetHTMLEnd/*" file="Doc/LJCHTMLBuilder.xml"/>
    public string GetHTMLEnd(LJCTextState textState)
    {
      var hb = new LJCHTMLBuilder(textState);

      var text = hb.GetEnd("body", textState, false);
      hb.Text(text, false);

      text = hb.GetEnd("html", textState, false);
      hb.Text(text, false);
      AddSyncIndent(hb, textState);

      var retValue = hb.ToString();
      return retValue;
    }

    // Gets the main HTML Head elements.
    /// <include path="members/GetHTMLHead/*" file="Doc/LJCHTMLBuilder.xml"/>
    public string GetHTMLHead(LJCTextState textState, string? title = null
      , string? author = null, string? description = null)
    {
      var hb = new LJCHTMLBuilder(textState);

      hb.Create("title", title, textState, childIndent: false);
      hb.Metas(author, textState, description);

      var retValue = hb.ToString();
      return retValue;
    }
    #endregion

    #region Get Attribs Methods

    // Gets common element attributes.
    /// <include path="members/Attribs/*" file="Doc/LJCHTMLBuilder.xml"/>
    public LJCAttributes Attribs(string? className = null, string? id = null)
    {
      var retAttribs = new LJCAttributes();
      if (LJC.HasValue(id))
      {
        retAttribs.Add("id", id);
      }
      if (LJC.HasValue(className))
      {
        retAttribs.Add("class", className);
      }
      return retAttribs;
    }

    // Creates the HTML element attributes.
    /// <include path="members/StartAttribs/*" file="Doc/LJCHTMLBuilder.xml"/>
    public LJCAttributes StartAttribs()
    {
      var retAttribs = new LJCAttributes()
      {
        { "lang", "en" },
        //{ "xmlns", "http://www.w3.org/1999/xhtml" },
      };
      return retAttribs;
    }

    // Creates the XML start attributes.
    /// <include path="members/StartAttributes/*" file="Doc/LJCHTMLBuilder.xml"/>
    public LJCAttributes StartXMLAttribs()
    {
      var retAttribs = new LJCAttributes
      {
        { "xmlns:xsd", "http://www.w3.org/2001/XMLSchema" },
        { "xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance" }
      };
      return retAttribs;
    }

    // Gets common table attributes.
    /// <include path="members/TableAttribs/*" file="Doc/LJCHTMLBuilder.xml"/>
    public LJCAttributes TableAttribs(int border = 1, int cellSpacing = 0
      , int cellPadding = 2, string? className = null, string? id = null)
    {
      var retAttribs = Attribs(className, id);
      retAttribs.Add("border", border.ToString());
      retAttribs.Add("cellspacing", cellSpacing.ToString());
      retAttribs.Add("cellpadding", cellPadding.ToString());
      return retAttribs;
    }
    #endregion

    #region Private Methods

    // Adds indent to builders and state object.
    private void AddSyncIndent(LJCHTMLBuilder hb, LJCTextState state
      , int value = 1)
    {
      this.AddIndent(value);
      hb?.AddIndent(value);
      state.IndentCount += value;
    }

    // Creates the content text.
    private string Content(string text, LJCTextState textState, bool isEmpty
      , out bool isWrapped)
    {
      string retValue = "";

      // Add text content.
      isWrapped = false;
      if (!isEmpty
        && LJC.HasValue(text))
      {
        if (text.Length > 80 - IndentLength())
        {
          isWrapped = true;
          retValue += "\r\n";
          AddSyncIndent(this, textState);
          var textValue = GetText(text);
          retValue += textValue;
          AddSyncIndent(this, textState, -1);
          retValue += "\r\n";
          LineLength = 0;
        }
        else
        {
          retValue += text;
        }
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
    private void UpdateState(LJCTextState textState)
    {
      if (textState != null)
      {
        IndentCount = textState.IndentCount;
      }
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
      if (nextLength <= LineLimit - IndentLength())
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
        nextLength = LineLimit - IndentLength();
        nextLength = tempText.LastIndexOf(" ", nextLength);
        retText = text.Substring(startIndex, nextLength);
      }
      return retText;
    }
    #endregion

    #region Properties

    // Gets or sets the indent character count.
    /// <include path="members/IndentCharCount/*" file="Doc/LJCHTMLBuilder.xml"/>
    public int IndentCharCount { get; set; }

    // Gets the current length.
    /// <include path="members/LineLength/*" file="Doc/LJCHTMLBuilder.xml"/>
    public int LineLength { get; private set; }

    // Gets or sets the character limit.
    /// <include path="members/LineLimit/*" file="Doc/LJCHTMLBuilder.xml"/>
    public int LineLimit { get; set; }

    // Indicates if line wrapping is enabled.
    /// <include path="members/WrapEnabled/*" file="Doc/LJCHTMLBuilder.xml"/>
    public bool WrapEnabled { get; set; }

    // The internal StringBuilder class.
    private StringBuilder Builder { get; set; }

    // Gets the indent count.
    private int IndentCount
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
    #endregion
  }
}
