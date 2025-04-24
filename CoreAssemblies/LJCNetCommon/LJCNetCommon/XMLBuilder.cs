// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// XMLBuilder.cs
using System.Text;

namespace LJCNetCommon
{
  /// <summary>Provides methods for creating XML text.</summary>
  public class XMLBuilder
  {
    #region Constructors

    /// <summary>Initializes an object instance.</summary>
    public XMLBuilder(TextState textState = null)
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
      WrapEnabled = true;
      DebugText = "";
      XML = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>";
    }
    #endregion

    #region Data Class Methods

    // Implements the ToString() method.
    /// <include path='items/ToString/*' file='Doc/XMLBuilder.xml'/>
    public override string ToString()
    {
      return Builder.ToString();
    }
    #endregion

    #region Methods

    // Changes the IndentCount by the provided value.
    /// <include path='items/AddIndent/*' file='Doc/XMLBuilder.xml'/>
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

    // Adds a text line without modification.
    /// <include path='items/AddLine/*' file='Doc/XMLBuilder.xml'/>
    public string AddLine(string text = null)
    {
      Builder.AppendLine(text);
      var retText = $"{text}\r\n";
      DebugText += retText;
      return retText;
    }

    // Adds text without modification.
    /// <include path='items/AddText/*' file='Doc/XMLBuilder.xml'/>
    public void AddText(string text)
    {
      if (TextLength(text) > 0)
      {
        Builder.Append(text);
        DebugText += text;
      }
    }

    // Adds a modified text line to the builder.
    /// <include path='items/Line/*' file='Doc/XMLBuilder.xml'/>
    public string Line(string text = null, bool addIndent = true
      , bool allowNewLine = true)
    {
      var retText = GetText(text, addIndent, allowNewLine);
      Builder.AppendLine(retText);
      retText = $"{retText}\r\n";
      DebugText += retText;
      return retText;
    }

    // Adds a newline if line length is greater than LineLimit.
    /// <include path='items/Text/*' file='Doc/XMLBuilder.xml'/>
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

    #region Get Text Methods (5)

    /// <summary>Indicates if the builder text ends with a newline.</summary>
    public bool EndsWithNewLine()
    {
      var retValue = false;

      if (Builder.Length > 0)
      {
        if ('\n' == Builder[Builder.Length - 1])
        {
          retValue = true;
        }
      }
      return retValue;
    }

    /// <summary>Allow text to start with a newline.</summary>
    public bool StartWithNewLine(bool allowNewLine)
    {
      bool retValue = false;

      if (allowNewLine
        && HasText
        && !EndsWithNewLine())
      {
        retValue = true;
      }
      return retValue;
    }

    // Gets the attributes text.
    /// <include path='items/GetAttribs/*' file='Doc/XMLBuilder.xml'/>
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
    /// <include path='items/GetIndented/*' file='Doc/XMLBuilder.xml'/>
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
    /// <include path='items/GetIndentString/*' file='Doc/XMLBuilder.xml'/>
    public string GetIndentString()
    {
      var retValue = new string(' ', IndentLength);
      return retValue;
    }

    // Gets a modified text line.
    /// <include path='items/GetLine/*' file='Doc/XMLBuilder.xml'/>
    public string GetLine(string text = null, bool addIndent = true
      , bool allowNewLine = true)
    {
      var retLine = GetText(text, addIndent, allowNewLine);
      retLine += "\r\n";
      return retLine;
    }

    // Gets potentially indented and wrapped text.
    /// <include path='items/GetText/*' file='Doc/XMLBuilder.xml'/>
    public string GetText(string text, bool addIndent = true
      , bool allowNewLine = true)
    {
      var retText = "";

      // Start with newline if text exists.
      if (StartWithNewLine(allowNewLine))
      {
        retText = "\r\n";
      }

      if (TextLength(text) > 0)
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

    // Adds added text and new wrapped line if combined line > LineLimit.
    /// <include path='items/GetWrapped/*' file='Doc/XMLBuilder.xml'/>
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

    #region Append Element Methods (3)

    // Creates the element begin tag. No new line.
    /// <include path='items/Begin/*' file='Doc/XMLBuilder.xml'/>
    public string Begin(string name, TextState textState
      , Attributes attribs = null, bool addIndent = true
      , bool childIndent = true)
    {
      var createText = GetBegin(name, textState, attribs, addIndent
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
    /// <include path='items/Create/*' file='Doc/XMLBuilder.xml'/>
    public string Create(string name, TextState textState, string text = null
      , Attributes attribs = null, bool addIndent = true
      , bool childIndent = true, bool isEmpty = false, bool close = true)
    {
      // Adds the indent string.
      var createText = GetCreate(name, textState, text, attribs, addIndent
        , childIndent, isEmpty, close);
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
    /// <include path='items/EndElement/*' file='Doc/XMLBuilder.xml'/>
    public string End(string name, TextState textState, bool addIndent = true)
    {
      var createText = GetEnd(name, textState, addIndent);
      // Use NoIndent after a GetEnd().
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
    /// <include path='items/GetBegin/*' file='Doc/XMLBuilder.xml'/>
    public string GetBegin(string name, TextState textState
      , Attributes htmlAttribs = null, bool addIndent = true
      , bool childIndent = true)
    {
      var hb = new HTMLBuilder(textState);
      var createText = GetCreate(name, textState, null, htmlAttribs
        , addIndent, childIndent, close: false);
      // Use NoIndent after a "GetText" method.
      hb.Text(createText, NoIndent);
      // Only use AddChildIndent() if additional text is added in this method.
      var retValue = hb.ToString();
      return retValue;
    }

    // Gets the element text.
    /// <include path='items/GetCreate/*' file='Doc/XMLBuilder.xml'/>
    public string GetCreate(string name, TextState textState, string text = null
      , Attributes attribs = null, bool addIndent = true
      , bool childIndent = true, bool isEmpty = false, bool close = true)
    {
      textState.ChildIndentCount = 0; // ?
      var tb = new TextBuilder(textState);

      // Start text with the opening tag.
      tb.Text($"<{name}", addIndent);
      var getText = GetAttribs(attribs, textState);
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
    /// <include path='items/GetEnd/*' file='Doc/XMLBuilder.xml'/>
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
    #endregion

    #region Get Attributes Methods

    // Creates the XML start attributes.
    /// <include path='items/StartAttributes/*' file='Doc/XMLBuilder.xml'/>
    public Attributes StartAttribs()
    {
      var retAttributes = new Attributes
      {
        { "xmlns:xsd", "http://www.w3.org/2001/XMLSchema" },
        { "xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance" }
      };
      return retAttributes;
    }
    #endregion

    #region Private Methods

    // Adds indent to builders and sync object.
    private void AddSyncIndent(XMLBuilder hb, TextBuilder tb
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

      var xb = new XMLBuilder(textState);
      isWrapped = false;
      // Add text content.
      if (!isEmpty
        && NetString.HasValue(text))
      {
        if (text.Length > 80 - IndentLength)
        {
          isWrapped = true;
          retValue += "\r\n";
          AddSyncIndent(xb, null, textState);
          var textValue = GetText(text);
          retValue += textValue;
          AddSyncIndent(xb, null, textState, -1);
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

    /// <summary>The internal StringBuilder class.</summary>
    public StringBuilder Builder { get; set; }

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

    /// <summary>Gets or sets the current indent value.</summary>
    public int IndentCount { get; private set; }

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

    /// <summary>Indicates if line wrapping is enabled.</summary>
    public bool WrapEnabled { get; set; }

    // Gets or sets the XML text.
    private string XML { get; set; }
    #endregion

    #region Class Data

    /// <summary></summary>
    public string DebugText;

    private const bool NoIndent = false;
    #endregion
  }
}
