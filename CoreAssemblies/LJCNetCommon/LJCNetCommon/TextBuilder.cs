// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// TextBuilder.cs
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

    // Adds a line.
    /// <include path='items/Line/*' file='Doc/TextBuilder.xml'/>
    public string Line(string text = null)
    {
      var retText = Text(text);
      if (null == text)
      {
        LineLength = 0;
      }
      retText += "\r\n";
      Builder.AppendLine();
      return retText;
    }

    // Adds a delimiter if not the first list item
    /// <include path='items/Item/*' file='Doc/TextBuilder.xml'/>
    public string Item(string text)
    {
      var retText = GetDelimited(text);
      Text(retText);
      return retText;
    }

    // Adds a newline if line length is greater than LineLimit.
    /// <include path='items/Text/*' file='Doc/TextBuilder.xml'/>
    public string Text(string text)
    {
      string wrapText = null;

      var retText = GetIndented(text);

      var isNewLine = false;
      if (WrapEnabled)
      {
        retText = GetWrapped(retText, out isNewLine, out wrapText);
      }

      Builder.Append(retText);
      if (retText != null
        && retText != string.Empty)
      {
        IsFirst = false;
        LineLength += retText.Length;
      }

      if (WrapEnabled
        && isNewLine)
      {
        LineLength = retText.Length - 2;
        if (wrapText != null)
        {
          LineLength = wrapText.Length;
          LineLength += GetIndentString().Length;
          if (WrapPrefix != null)
          {
            LineLength += WrapPrefix.Length;
          }
        }
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
    public string GetWrapped(string text, out bool isNewLine
      , out string wrapText)
    {
      string retText = text;

      isNewLine = false;
      wrapText = null;

      // Wrap if not a new line.
      if (LineLength > GetIndentString().Length)
      {
        var wrapIndex = WrapIndex(text);
        if (wrapIndex > -1)
        {
          // Include the following space.
          var addLength = wrapIndex + 1;
          if (IsWrapAtDelimiter(retText[wrapIndex]))
          {
            // Don't include the delimiter.
            addLength = 0;
          }
          var addText = retText.Substring(0, addLength);

          // Don't include the space.
          var startIndex = wrapIndex + 1;

          // Include the delimiter.
          if (IsWrapAtDelimiter(retText[wrapIndex]))
          {
            // Start at the delimiter.
            startIndex = wrapIndex;
          }
          wrapText = retText.Substring(startIndex);
          var indent = GetIndentString();
          retText = $" {addText}\r\n{indent}{WrapPrefix}{wrapText}";
          isNewLine = true;
        }
      }
      return retText;
    }

    // Calculates the index at which to wrap the text.
    private int WrapIndex(string text)
    {
      int retIndex = -1;

      var resultLength = LineLength;
      if (text != null)
      {
        resultLength += text.Length;
      }
      if (resultLength > LineLimit)
      {
        // Index of additional characters that will fit in LineLimit.
        retIndex = text.Length - (resultLength - LineLimit);
        if (text[retIndex] != ' ')
        {
          // Wrap on a space.
          retIndex = text.LastIndexOf(' ', retIndex);
          if (IsWrapAtDelimiter(text[retIndex - 1]))
          {
            retIndex--;
          }
        }
      }
      return retIndex;
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
