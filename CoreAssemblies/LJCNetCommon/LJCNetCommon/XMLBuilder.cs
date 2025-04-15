// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// XMLBuilder.cs
using System.Reflection;
using System.Text;

namespace LJCNetCommon
{
  /// <summary>
  /// Provides methods for creating XML text.
  /// </summary>
  public class XMLBuilder
  {
    #region Constructors

    /// <summary>Initializes an object instance.</summary>
    public XMLBuilder()
    {
      IndentCharCount = 2;
      IndentCount = 0;
      LineLength = 0;
      LineLimit = 80;
      WrapEnabled = true;
      XML = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>";
    }
    #endregion

    #region Data Class Methods

    /// <summary>Implements the ToString() method.</summary>
    public override string ToString()
    {
      return XML;
    }
    #endregion

    #region Methods

    // Changes the XMLIndentCount by the supplied value.
    /// <include path='items/IndentXML/*' file='Doc/XMLBuilder.xml'/>
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

    #region Append Text Methods

    // Adds a newline if line length is greater than LineLimit.
    /// <include path='items/Text/*' file='Doc/XMLBuilder.xml'/>
    public string Text(string text)
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
    #endregion

    #region Get Text Methods

    /// <summary>Gets a new line with prefixed indent.</summary>
    /// <include path='items/GetIndented/*' file='Doc/XMLBuilder.xml'/>
    public string GetIndented(string text)
    {
      string retText = "";

      // Add indent to a new line with no indent.
      //if (0 == LineLength
      //  && text != null)
      //{
      //  retText = GetIndentString();
      //}

      // Allow add of blank characters.
      if (text != null)
      {
        retText = GetIndentString();
        retText += text;
      }
      return retText;
    }

    /// <summary>Returns the current XML indent string.</summary>
    public string GetIndentString()
    {
      var retValue = new string(' ', IndentCount * IndentCharCount);
      return retValue;
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

    #region Append Element Methods

    // Creates the element begin tag. No new line.
    /// <include path='items/BeginElement/*' file='Doc/XMLBuilder.xml'/>
    public string Begin(string name, string text = null
      , bool isIndented = true, XMLAttributes xmlAttributes = null)
    {
      return Create(name, text, xmlAttributes, isIndented, false);
    }

    // Creates an element. No new line.
    /// <include path='items/CreateElement/*' file='Doc/XMLBuilder.xml'/>
    public string Create(string name
      , string text = null, XMLAttributes xmlAttributes = null
      , bool isIndented = true, bool close = true)
    {
      var builder = new StringBuilder(128);
      if (XML.Length > 0)
      {
        builder.AppendLine();
      }
      if (isIndented)
      {
        builder.Append($"{GetIndentString()}");
      }
      builder.Append($"<{name}");
      if (NetCommon.HasItems(xmlAttributes))
      {
        var isFirst = true;
        foreach (XMLAttribute xmlAttribute in xmlAttributes)
        {
          if (!isFirst)
          {
            builder.Append(",");
            if (NetString.HasValue(xmlAttribute.Value)
              && xmlAttribute.Value.Length > 35)
            {
              builder.Append($"\r\n{GetIndentString()}   ");
            }
          }
          isFirst = false;

          builder.Append($" {xmlAttribute.Name}");
          if (NetString.HasValue(xmlAttribute.Value))
          {
            builder.Append($"=\"{xmlAttribute.Value}\"");
          }
        }
      }
      builder.Append(">");
      if (isIndented)
      {
        AddIndent();
      }
      if (NetString.HasValue(text))
      {
        builder.AppendLine();
        var textValue = Text(text);
        builder.Append(textValue);
        LineLength = 0;
      }

      if (close)
      {
        if (isIndented)
        {
          AddIndent(-1);
        }
        builder.AppendLine();
        builder.Append($"{GetIndentString()}<\\{name}>");
      }
      string retElement = builder.ToString();
      XML += retElement;
      return retElement;
    }

    // Creates the element end tag.
    /// <include path='items/EndElement/*' file='Doc/XMLBuilder.xml'/>
    public string End(string name, bool isIndented = true)
    {
      string retEnd;

      var builder = new StringBuilder(128);
      if (XML.Length > 0)
      {
        builder.AppendLine();
      }

      if (isIndented)
      {
        if (IndentCount > 0)
        {
          IndentCount--;
        }
        builder.Append($"{GetIndentString()}");
      }

      builder.Append($"<\\{name}>");
      retEnd = builder.ToString();
      XML += retEnd;
      return retEnd;
    }
    #endregion

    #region Element Attributes Methods

    // Creates the XML start attributes.
    /// <include path='items/StartAttributes/*' file='Doc/XMLBuilder.xml'/>
    public XMLAttributes StartAttributes()
    {
      var retAttributes = new XMLAttributes
      {
        { "xmlns:xsd", "http://www.w3.org/2001/XMLSchema" },
        { "xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance" }
      };
      return retAttributes;
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

    /// <summary>Gets or sets the indent character count.</summary>
    public int IndentCharCount { get; set; }

    /// <summary>Gets or sets the current indent value.</summary>
    public int IndentCount { get; private set; }

    /// <summary>Gets the current length.</summary>
    public int LineLength { get; private set; }

    /// <summary>Gets or sets the character limit.</summary>
    public int LineLimit { get; set; }

    /// <summary>Indicates if line wrapping is enabled.</summary>
    public bool WrapEnabled { get; set; }

    // Gets or sets the XML text.
    private string XML { get; set; }
    #endregion
  }
}
