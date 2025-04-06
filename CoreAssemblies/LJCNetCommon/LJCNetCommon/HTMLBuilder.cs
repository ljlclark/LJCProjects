// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// HTMLBuilder.cs
using System.Text;

namespace LJCNetCommon
{
  // Move to LJCNetCommon
  /// <summary>
  /// Provides methods for creating HTML text.
  /// </summary>
  public class HTMLBuilder
  {
    #region Constructors

    /// <summary>Initializes an object instance.</summary>
    public HTMLBuilder()
    {
      IndentCharCount = 2;
      IndentCount = 0;
      LineLength = 0;
      LineLimit = 80;
      WrapEnabled = false;
      HTML = "";
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
    /// <include path='items/Indent/*' file='Doc/HTMLBuilder.xml'/>
    public int Indent(int increment = 1)
    {
      IndentCount += increment;
      if (IndentCount < 0)
      {
        IndentCount = 0;
      }
      return IndentCount;
    }
    #endregion

    #region Append Methods

    // Creates the element begin tag.
    /// <include path='items/Begin/*' file='Doc/HTMLBuilder.xml'/>
    public string Begin(string name, string text = null
      , Attributes htmlAttributes = null, bool applyIndent = true)
    {
      return Create(name, text, htmlAttributes, applyIndent, false
        , false);
    }

    // Creates an element. No new line.
    /// <include path='items/Create/*' file='Doc/HTMLBuilder.xml'/>
    public string Create(string name, string text = null
      , Attributes htmlAttributes = null, bool applyIndent = true
      , bool isEmpty = false, bool close = true)
    {
      var builder = new StringBuilder(128);

      // Any previous text does not have cr/lf.
      if (HTML.Length > 0)
      {
        builder.AppendLine();
      }

      // Add the element name and attributes.
      if (applyIndent)
      {
        Indent();
        builder.Append($"{GetIndentString()}");
      }
      builder.Append($"<{name}");
      builder.Append(GetAttribs(htmlAttributes));
      if (isEmpty)
      {
        if (applyIndent)
        {
          Indent(-1);
        }
        builder.Append(" /");
      }
      builder.Append(">");
      var content = Content(text, isEmpty
        , out bool isWrapped);
      builder.Append(content);

      // Close the element.
      if (!isEmpty
        && close)
      {
        if (isWrapped)
        {
          builder.AppendLine();
          builder.Append(GetIndentString());
        }
        builder.Append($"</{name}>");
        if (applyIndent)
        {
          Indent(-1);
        }
      }

      // Get value and update the HTML.
      string retElement = builder.ToString();
      HTML += retElement;
      return retElement;
    }

    // Creates the element end tag.
    /// <include path='items/End/*' file='Doc/HTMLBuilder.xml'/>
    public string End(string name, bool applyIndent = true)
    {
      string retEnd;

      var builder = new StringBuilder(128);

      // Any previous text does not have cr/lf.
      if (HTML.Length > 0)
      {
        builder.AppendLine();
      }

      if (applyIndent)
      {
        builder.Append($"{GetIndentString()}");
      }
      builder.Append($"</{name}>");
      if (applyIndent)
      {
        if (IndentCount > 0)
        {
          IndentCount--;
        }
      }
      retEnd = builder.ToString();
      HTML += retEnd;
      return retEnd;
    }

    // Adds a modified text line to the builder.
    /// <include path='items/Line/*' file='Doc/HTMLBuilder.xml'/>
    public string Line(string text = "")
    {
      var retText = GetText(text);
      HTML += $"{retText}\r\n";
      return retText;
    }

    // Adds modified text to the builder.
    /// <include path='items/Text/*' file='Doc/HTMLBuilder.xml'/>
    public string Text(string text)
    {
      var retText = GetText(text);
      HTML += retText;
      return retText;
    }
    #endregion

    #region Create Methods

    // Creates the HTML beginning up to and including <head>.
    /// <include path='items/CreateHTMLBegin/*' file='Doc/HTMLBuilder.xml'/>
    public string CreateHTMLBegin(string[] copyright = null
      , string fileName = null)
    {
      var retValue = GetHTMLBegin(copyright, fileName);
      Text(retValue);
      return retValue;
    }

    // Creates a <link> element for a style sheet.
    /// <include path='items/CreateLink/*' file='Doc/HTMLBuilder.xml'/>
    public string CreateLink(string fileName)
    {
      var retValue = GetLink(fileName);
      Line();
      Text(retValue);
      return retValue;
    }

    // Creates a <meta> element.
    /// <include path='items/CreateMeta/*' file='Doc/HTMLBuilder.xml'/>
    public string CreateMeta(string name, string content)
    {
      var retValue = GetMeta(name, content);
      Line();
      Text(retValue);
      return retValue;
    }

    // Creates common <meta> elements.
    /// <include path='items/CreateMetas/*' file='Doc/HTMLBuilder.xml'/>
    public string CreateMetas(string author, string description = null
      , string keywords = null, string charSet = "utf-8")
    {
      var retValue = GetMetas(author, description, keywords, charSet);
      Line();
      Text(retValue);
      return retValue;
    }

    // Creates a <link> element for a style sheet.
    /// <include path='items/CreateScript/*' file='Doc/HTMLBuilder.xml'/>
    public string CreateScript(string fileName)
    {
      var retValue = GetScript(fileName);
      Line();
      Text(retValue);
      return retValue;
    }
    #endregion

    #region Create Get Methods

    // Gets the HTML beginning up to <head>.
    /// <include path='items/GetHTMLBegin/*' file='Doc/HTMLBuilder.xml'/>
    public string GetHTMLBegin(string[] copyright = null
      , string fileName = null)
    {
      var hb = new HTMLBuilder();
      hb.Text("<!DOCTYPE html>");
      if (NetCommon.HasElements(copyright))
      {
        hb.Line();
        var isFirst = true;
        foreach (string text in copyright)
        {
          if (!isFirst)
          {
            hb.Text("\r\n");
          }
          isFirst = false;
          hb.Text($"<!-- {text} -->");
        }
      }
      if (NetString.HasValue(fileName))
      {
        hb.Line();
        hb.Text($"<!-- {fileName} -->");
      }
      var startAttribs = hb.StartAttribs();
      hb.Begin("html", null, startAttribs, NoIndent);
      hb.Begin("head", null, null, NoIndent);
      var retValue = hb.ToString();
      return retValue;
    }

    /// <summary>Gets the HTML end &lt;body&gt; and &lt;html&gt;.</summary>
    public string GetHTMLEnd()
    {
      var hb = new HTMLBuilder();
      hb.End("body", NoIndent);
      hb.End("html", NoIndent);
      var retValue = hb.ToString();
      return retValue;
    }

    // Gets the <link> element for a style sheet.
    /// <include path='items/GetLink/*' file='Doc/HTMLBuilder.xml'/>
    public string GetLink(string fileName)
    {
      var hb = new HTMLBuilder();
      hb.Indent(IndentCount);
      var attribs = new Attributes()
      {
        { "rel", "stylesheet" },
        { "type", "text/css" },
        { "href", fileName },
      };
      hb.Create("link", null, attribs, isEmpty: true);
      var retValue = hb.ToString();
      return retValue;
    }

    // Gets a <meta> element.
    /// <include path='items/GetMeta/*' file='Doc/HTMLBuilder.xml'/>
    public string GetMeta(string name, string content)
    {
      var hb = new HTMLBuilder();
      hb.Indent(IndentCount);
      var attribs = new Attributes()
      {
        { "name", name },
        { "content", content },
      };
      hb.Create("meta", null, attribs, isEmpty: true);
      var retValue = hb.ToString();
      return retValue;
    }

    // Gets common <meta> elements.
    /// <include path='items/GetMetas/*' file='Doc/HTMLBuilder.xml'/>
    public string GetMetas(string author, string description = null
      , string keywords = null, string charSet = "utf-8")
    {
      var hb = new HTMLBuilder();
      hb.Indent(IndentCount);
      var attribs = new Attributes()
      {
        { "charset", charSet }
      };
      hb.Create("meta", null, attribs, isEmpty: true);
      if (NetString.HasValue(description))
      {
        hb.CreateMeta("description", description);
      }
      if (NetString.HasValue(keywords))
      {
        hb.CreateMeta("keywords", keywords);
      }
      hb.CreateMeta("author", author);
      var content = "width=device-width initial-scale=1";
      hb.CreateMeta("viewport", content);
      var retValue = hb.ToString();
      return retValue;
    }

    // Gets the <script> element.
    /// <include path='items/GetScript/*' file='Doc/HTMLBuilder.xml'/>
    public string GetScript(string fileName)
    {
      var hb = new HTMLBuilder();
      hb.Indent(IndentCount);
      var attribs = new Attributes()
      {
        { "src", fileName },
      };
      hb.Create("script", null, attribs);
      var retValue = hb.ToString();
      return retValue;
    }
    #endregion

    #region Get Modified Text Methods

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
    /// <include path='items/GetLine/*' file='Doc/HTMLBuilder.xml'/>
    public string GetLine(string text)
    {
      var retLine = GetText(text);
      retLine += "\r\n";
      return retLine;
    }

    // Gets modified text.
    /// <include path='items/GetText/*' file='Doc/HTMLBuilder.xml'/>
    public string GetText(string text)
    {
      var retText = GetIndented(text);

      bool isReturn = false;
      if (!WrapEnabled)
      {
        // Just add text.
        isReturn = true;
      }

      if (!isReturn)
      {
        retText = GetWrapped(retText);
      }
      return retText;
    }

    // Adds added text and new wrapped line if combined line > LineLimit.
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

    #region Attribs Methods

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
    private string Content(string text, bool isEmpty
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
          Indent();
          var textValue = GetText(text);
          retValue += textValue;
          Indent(-1);
          retValue += "\r\n";
        }
        else
        {
          retValue += text;
        }
        LineLength = 0;
      }
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

    /// <summary>Gets the current indent count.</summary>
    public int CurrentIndentCount
    {
      get { return IndentCount; }
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

    // Gets or sets the current indent value.
    private int IndentCount { get; set; }

    // Gets or sets the XML text.
    private string HTML { get; set; }
    #endregion

    #region Class Values

    const bool NoIndent = false;
    #endregion
  }
}
