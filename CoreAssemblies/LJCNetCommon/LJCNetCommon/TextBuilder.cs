// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// TextBuilder.cs
using System.Linq;
using System.Text;

namespace LJCNetCommon
{
  /// <summary>A StringBuilder helper class.</summary>
  public class TextBuilder
  {
    #region Constructors

    /// <summary>Initializes an object instance.</summary>
    public TextBuilder(int capacity = 32, StringBuilder builder = null)
    {
      if (null == builder)
      {
        builder = new StringBuilder(capacity);
        Delimiter = ", ";
        IndentCharCount = 2;
        IndentCount = 0;
        IsFirst = true;
        LineLength = 0;
        LineLimit = 80;
        WrapAtDelimiter = true;
        WrapEnabled = true;
        WrapPrefix = "  ";
      }
      Builder = builder;
    }
    #endregion

    #region Data Class Methods

    /// <summary>Implements the ToString() method.</summary>
    public override string ToString()
    {
      return Builder.ToString();
    }
    #endregion

    #region Append Methods

    // Adds text without processing.
    /// <include path='items/Add/*' file='Doc/TextBuilder.xml'/>
    public void Add(string text)
    {
      Builder.Append(text);
    }

    // Adds a delimiter if not the first list item
    /// <include path='items/Item/*' file='Doc/TextBuilder.xml'/>
    public string Item(string text)
    {
      var retText = GetDelimited(text);
      Text(retText);
      return retText;
    }

    // Adds a line.
    /// <include path='items/Line/*' file='Doc/TextBuilder.xml'/>
    public string Line(string text = null)
    {
      var retText = Text(text);
      retText += "\r\n";
      Builder.AppendLine();
      LineLength = 0;
      return retText;
    }

    // Adds a newline if line length is greater than LineLimit.
    /// <include path='items/Text/*' file='Doc/TextBuilder.xml'/>
    public string Text(string text)
    {
      var retText = GetIndented(text);

      bool isContinue = false;
      if (!WrapEnabled)
      {
        isContinue = true;
        SaveLine(retText);
      }

      if (!isContinue)
      {
        retText = GetWrapped(retText);
        SaveLine(retText);
      }
      return retText;
    }
    #endregion

    #region Get Modified String Methods

    // Adds a delimiter if not the first list item.
    /// <include path='items/GetDelimited/*' file='Doc/TextBuilder.xml'/>
    public string GetDelimited(string text)
    {
      string retText = text;

      if (!IsFirst)
      {
        retText = $"{Delimiter}{retText}";
      }
      return retText;
    }

    /// <summary>Gets a new line with prefixed indent.</summary>
    /// <include path='items/GetIndented/*' file='Doc/TextBuilder.xml'/>
    public string GetIndented(string text)
    {
      string retText = "";

      // Add indent to a new line with no indent.
      if (0 == LineLength
        && text != null)
      {
        retText = GetIndentString();
      }

      // Allow add of blank characters.
      if (text != null)
      {
        retText += text;
      }
      return retText;
    }

    // Adds a newline if line length is greater than LineLimit.
    /// <include path='items/GetWrapped/*' file='Doc/TextBuilder.xml'/>
    public string GetWrapped(string text)
    {
      string retText = text;

      string buildText = "";
      var workText = text;
      // Total of current LineLength + (not null)workText.Length;
      var totalLength = TotalLength(workText);
      while (totalLength > LineLimit)
      {
        // Index where text can be added to current line
        // and the remainder is wrapped.
        var wrapIndex = WrapIndex(workText);
        if (wrapIndex > -1)
        {
          var addLength = wrapIndex;
          var addText = AddText(retText, addLength);
          buildText += $"{addText}\r\n";

          // Get next text up to LineLimit.
          string wrapText = WrapText(workText, ref wrapIndex);
          var indent = GetIndentString();
          var saveText = $"{indent}{WrapPrefix}{wrapText}";
          LineLength = saveText.Length;
          buildText += saveText;

          // End loop unless there is more text.
          totalLength = 0;
          var nextIndex = addLength + wrapText.Length + 1;
          if (workText.Length > ToLength(nextIndex)
            && ' ' == workText[nextIndex])
          {
            nextIndex++;
          }
          if (nextIndex < ToIndex(workText.Length))
          {
            var tempText = workText.Substring(nextIndex);
            workText = tempText;
            // Total of current LineLength + (not null)workText.Length;
            totalLength = TotalLength(workText);
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

    // Writes the line to the builder.
    private void SaveLine(string text)
    {
      Builder.Append(text);
      if (text != null
        && text != string.Empty)
      {
        LineLength += text.Length;
      }
      IsFirst = false;
    }

    // Convert length to index.
    private int ToIndex(int length)
    {
      var retIndex = 0;

      if (length > 0)
      {
        retIndex = length - 1;
      }
      return retIndex;
    }

    // Convert index to length.
    private int ToLength(int index)
    {
      int retLength = index + 1;
      return retLength;
    }

    // Gets the combined current line and text length.
    private int TotalLength(string text)
    {
      var retLength = LineLength;

      if (text != null)
      {
        retLength += text.Length;
      }
      return retLength;
    }

    // Calculates the index at which to wrap the text.
    private int WrapIndex(string text)
    {
      int retIndex = -1;

      var totalLength = TotalLength(text);
      if (totalLength > LineLimit)
      {
        // Length of additional characters that fit in LineLimit.
        // Only get up to next LineLimit length;
        var addLength = LineLength;
        if (addLength > LineLimit)
        {
          addLength = LineLimit;
        }
        var wrapLength = LineLimit - addLength;

        // Get wrap point in allowed length.
        var onSpace = true;
        if (WrapAtDelimiter)
        {
          onSpace = false;
          // Wrap on the delimiter.
          // Don't include the delimiter in the added value.
          retIndex = text.LastIndexOf(Delimiter, wrapLength);
          if (-1 == retIndex)
          {
            onSpace = true;
          }
        }

        if (onSpace)
        {
          // Wrap on a space.
          retIndex = text.LastIndexOf(" ", wrapLength);
          if (-1 == retIndex)
          {
            // Start wrap at the new text.
            retIndex = 0;
          }
        }
      }
      return retIndex;
    }

    // Gets the text to wrap to a new line.
    private string WrapText(string text, ref int wrapIndex)
    {
      string retText = null;

      // Default
      var nextLength = LineLimit;

      // Get text at the wrap index.
      if (text.Length - wrapIndex < LineLimit)
      {
        nextLength = text.Length - wrapIndex;
        retText = text.Substring(wrapIndex, nextLength);
        if (retText.StartsWith(" "))
        {
          retText = retText.Substring(1);
        }
      }

      // Default to get text from next section.
      if (nextLength == LineLimit)
      {
        var tempText = text.Substring(wrapIndex);
        if (tempText.StartsWith(" "))
        {
          tempText = tempText.Substring(1);
          wrapIndex++;
        }
        nextLength = tempText.LastIndexOf(" ", nextLength);
        retText = text.Substring(wrapIndex, nextLength);
      }
      return retText;
    }
    #endregion

    #region Other Methods

    /// <summary>Returns the current indent string.</summary>
    public string GetIndentString()
    {
      var retValue = new string(' ', IndentCount * IndentCharCount);
      return retValue;
    }

    // Changes the IndentCount by the supplied value.
    /// <include path='items/Indent/*' file='Doc/TextBuilder.xml'/>
    public int Indent(int count = 1)
    {
      IndentCount += count;
      if (IndentCount < 0)
      {
        IndentCount = 0;
      }
      return IndentCount;
    }

    // Check for wrap at the delimiter.
    private bool IsWrapAtDelimiter(char wrapChar)
    {
      bool retValue = false;

      if (WrapAtDelimiter
        && Delimiter[0] == wrapChar)
      {
        retValue = true;
      }
      return retValue;
    }
    #endregion

    #region Properties

    /// <summary>The internal StringBuilder class.</summary>
    public StringBuilder Builder { get; set; }

    /// <summary>Gets or sets the delimiter.</summary>
    public string Delimiter { get; set; }

    /// <summary>Gets or sets the indent character count.</summary>
    public int IndentCharCount { get; set; }

    /// <summary>Gets or sets the indent count.</summary>
    public int IndentCount { get; set; }

    /// <summary>Gets or sets the first item indicator.</summary>
    public bool IsFirst { get; set; }

    /// <summary>Gets the current length.</summary>
    public int LineLength { get; private set; }

    /// <summary>Gets or sets the character limit.</summary>
    public int LineLimit { get; set; }

    /// <summary>
    /// Indicates that a wrap should occur at a leading delimiter.
    /// </summary>
    public bool WrapAtDelimiter { get; set; }

    /// <summary>Indicates if line wrapping is enabled.</summary>
    public bool WrapEnabled { get; set; }

    /// <summary>Gets or sets the new line prefix.</summary>
    public string WrapPrefix { get; set; }
    #endregion
  }
}
