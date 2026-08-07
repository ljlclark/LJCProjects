// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCTextBuilder.cs
using System.Text;

namespace LJCNetCommon5
{
  // Provides methods for creating text.
  /// <include file='Doc/LJCTextBuilder.xml'
  ///  path='members/LJCTextBuilder/*'/>
  public class LJCTextBuilder
  {
    #region Static Methods

    // Gets the text length if not null.
    private static int TextLength(string text)
    {
      int retLength = 0;
      if (text != null)
      {
        retLength = text.Length;
      }
      return retLength;
    }
    #endregion

    #region Constructors

    // Initializes an object instance with the provided values.
    /// <include file='Doc/LJCTextBuilder.xml'
    ///  path='members/Constructor/*'/>
    public LJCTextBuilder(LJCTextState? textState = null)
    {
      Builder = new StringBuilder(128);
      Delimiter = ", ";
      IndentCharCount = 2;
      IndentCount = 0;
      if (textState != null)
      {
        // Sync the important values.
        AddIndent(textState.IndentCount);
      }
      IsFirst = true;
      LineLength = 0;
      LineLimit = 80;
      WrapAtDelimiter = true;
      WrapEnabled = false;
      WrapPrefix = "  ";
      DebugText = "";
    }
    #endregion

    #region Data Class Methods

    // Clears the Builder text.
    /// <include file='Doc/LJCTextBuilder.xml'
    ///  path='members/Clear/*'/>
    public void Clear()
    {
      Builder.Clear();
      IsFirst = true;
      LineLength = 0;
    }

    // Retrieves the object text.
    /// <include file='Doc/LJCTextBuilder.xml'
    ///  path='members/ToString/*'/>
    public override string ToString()
    {
      return Builder.ToString();
    }
    #endregion

    #region Methods

    // Changes the IndentCount by the provided value.
    /// <include file='Doc/LJCTextBuilder.xml'
    ///  path='members/AddIndent/*'/>
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

    // Adds a text line without modification.
    /// <include file='Doc/LJCTextBuilder.xml'
    ///  path='members/AddLine/*'/>
    public string AddLine(string? text = null)
    {
      Builder.AppendLine(text);

      var retText = $"{text}\r\n";
      DebugText += retText;
      return retText;
    }

    // Adds text without modification.
    /// <include file='Doc/LJCTextBuilder.xml'
    ///  path='members/AddText/*'/>
    public void AddText(string text)
    {
      if (TextLength(text) > 0)
      {
        Builder.Append(text);

        DebugText += text;
      }
    }

    // Adds a delimiter if not the first list item.
    /// <include file='Doc/LJCTextBuilder.xml'
    ///  path='members/Item/*'/>
    public string Item(string text, bool addIndent = true
      , bool allowNewLine = true)
    {
      var retText = text;

      if (!IsFirst)
      {
        // Start with the delimiter.
        retText = $"{Delimiter}{retText}";
        addIndent = false;
        allowNewLine = false;
      }
      IsFirst = false;

      DebugText += Text(retText, addIndent, allowNewLine);
      return retText;
    }

    // Adds a modified text line to the builder.
    /// <include file='Doc/LJCTextBuilder.xml'
    ///  path='members/Line/*'/>
    public string Line(string? text = null, bool addIndent = true
      , bool allowNewLine = true)
    {
      var retText = GetText(text, addIndent, allowNewLine);
      Builder.AppendLine(retText);
      LineLength = 0;

      retText = $"{retText}\r\n";
      DebugText += retText;
      return retText;
    }

    // Adds modified text to the builder.
    /// <include file='Doc/LJCTextBuilder.xml'
    ///  path='members/Text/*'/>
    public string Text(string text, bool addIndent = true
      , bool allowNewLine = true)
    {
      var retText = GetText(text, addIndent, allowNewLine);
      if (TextLength(retText) > 0)
      {
        Builder.Append(retText);

        DebugText += retText;
      }
      return retText;
    }
    #endregion

    #region Get Text Methods

    // Indicates if the builder text ends with a newline.
    /// <include file='Doc/LJCTextBuilder.xml'
    ///  path='members/EndsWithNewLine/*'/>
    public bool EndsWithNewLine()
    {
      var retValue = false;

      if (Builder.Length > 0)
      {
        //if ('\n' == Builder[Builder.Length - 1])
        if ('\n' == Builder[^1])
        {
          retValue = true;
        }
      }
      return retValue;
    }

    // Allow text to start with a newline.
    /// <include file='Doc/LJCTextBuilder.xml'
    ///  path='members/StartWithNewLine/*'/>
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

    // Adds a delimiter if not the first list item.
    /// <include file='Doc/LJCTextBuilder.xml'
    ///  path='members/GetDelimited/*'/>
    public string GetDelimited(string text)
    {
      string retText = text;

      if (!IsFirst)
      {
        retText = $"{Delimiter}{retText}";
      }
      IsFirst = false;
      return retText;
    }

    // Gets a new potentially indented line.
    /// <include file='Doc/LJCTextBuilder.xml'
    ///  path='members/GetIndented/*'/>
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

    // Returns the current indent string.
    /// <include file='Doc/LJCTextBuilder.xml'
    ///  path='members/GetIndentString/*'/>
    public string GetIndentString()
    {
      var retValue = new string(' ', IndentLength);
      return retValue;
    }

    // Gets a modified text line.
    /// <include file='Doc/LJCTextBuilder.xml'
    ///  path='members/GetLine/*'/>
    public string GetLine(string? text = null, bool addIndent = true
      , bool allowNewLine = true)
    {
      var retLine = GetText(text, addIndent, allowNewLine);
      retLine = $"{retLine}\r\n";
      return retLine;
    }

    // Gets potentially indented and wrapped text.
    /// <include file='Doc/LJCTextBuilder.xml'
    ///  path='members/GetText/*'/>
    public string GetText(string? text, bool addIndent = true
      , bool allowNewLine = true)
    {
      var retText = "";

      // Start with newline if text exists.
      if (StartWithNewLine(allowNewLine))
      {
        retText = "\r\n";
      }

      //if (TextLength(text) > 0)
      if (LJC.HasText(text))
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

    // Gets added text and new wrapped line if combined line > LineLimit.
    /// <include file='Doc/LJCTextBuilder.xml'
    ///  path='members/GetWrapped/*'/>
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
          var lineText = $"{WrapPrepend()}{wrapText}";
          LineLength = lineText.Length;
          buildText += lineText;

          // End loop unless there is more text.
          totalLength = 0;

          // Get index of next section.
          var nextIndex = wrapIndex + wrapText.Length;
          if (!workText.StartsWith(','))
          {
            // Adjust for removed leading space.
            nextIndex++;
          }

          // Get next work text if available.
          if (nextIndex < workText.Length)
          {
            //var tempText = workText.Substring(nextIndex);
            var tempText = workText[nextIndex..];
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

    #region Private Methods

    // Gets the text to add to the existing line.
    private string GetAddText(string text, int addLength)
    {
      //var retText = text.Substring(0, addLength);
      var retText = text[..addLength];
      if (LineLength > 0
        && addLength > 0)
      {
        // Add a leading space.
        retText = $" {retText}";
      }
      return retText;
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
          retIndex = text.LastIndexOf(' ', wrapLength);
          if (-1 == retIndex)
          {
            // Wrap index not found; Wrap at new text.
            retIndex = 0;
          }
        }
      }
      return retIndex;
    }

    // Get the wrap prepend value.
    private string WrapPrepend()
    {
      var indent = GetIndentString();
      var retValue = $"{indent}{WrapPrefix}";
      return retValue;
    }

    // Get the wrap prepend value.
    private int WrapPrependLength()
    {
      var retLength = TextLength(WrapPrepend());
      return retLength;
    }

    // Get next text up to LineLimit without leading space.
    private string WrapText(string text, int wrapIndex)
    {
      string retText;

      var nextLength = text.Length - wrapIndex;

      // Leave room for prepend text.
      if (nextLength <= LineLimit - WrapPrependLength())
      {
        // Get text at the wrap index.
        retText = text.Substring(wrapIndex, nextLength);
        if (retText.StartsWith(' '))
        {
          // Remove leading space.
          //retText = retText.Substring(1);
          retText = retText[1..];
        }
      }
      else
      {
        // Get text from next section.
        var startIndex = wrapIndex;
        //var tempText = text.Substring(startIndex);
        var tempText = text[startIndex..];
        if (tempText.StartsWith(' '))
        {
          //tempText = tempText.Substring(1);
          tempText = tempText[1..];
          startIndex++;
        }
        nextLength = LineLimit - TextLength(WrapPrepend());
        nextLength = tempText.LastIndexOf(' ', nextLength);
        retText = text.Substring(startIndex, nextLength);
      }
      return retText;
    }
    #endregion

    #region Properties

    // The internal StringBuilder class.
    /// <include file='Doc/LJCTextBuilder.xml'
    ///  path='members/Builder/*'/>
    public StringBuilder Builder { get; set; }

    // The debug text.
    /// <include file='Doc/LJCTextBuilder.xml'
    ///  path='members/DebugText/*'/>
    public string DebugText { get; set; }

    // Gets or sets the delimiter.
    /// <include file='Doc/LJCTextBuilder.xml'
    ///  path='members/Delimiter/*'/>
    public string Delimiter { get; set; }

    // Gets a value indicating if the builder has text.
    /// <include file='Doc/LJCTextBuilder.xml'
    ///  path='members/HasText/*'/>
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

    // Gets or sets the indent character count.
    /// <include file='Doc/LJCTextBuilder.xml'
    ///  path='members/IndentCharCount/*'/>
    public int IndentCharCount { get; set; }

    // Gets or sets the indent count.
    /// <include file='Doc/LJCTextBuilder.xml'
    ///  path='members/IndentCount/*'/>
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

    // Gets the current indent length.
    /// <include file='Doc/LJCTextBuilder.xml'
    ///  path='members/IndentLength/*'/>
    public int IndentLength
    {
      get
      {
        return IndentCount * IndentCharCount;
      }
    }

    // Gets or sets the first item indicator.
    /// <include file='Doc/LJCTextBuilder.xml'
    ///  path='members/IsFirst/*'/>
    public bool IsFirst { get; set; }

    // Gets the current length.
    /// <include file='Doc/LJCTextBuilder.xml'
    ///  path='members/LineLength/*'/>
    public int LineLength { get; private set; }

    // Gets the character limit.
    /// <include file='Doc/LJCTextBuilder.xml'
    ///  path='members/LineLimit/*'/>
    public int LineLimit { get; private set; }

    // Gets or sets a value that indicates if a wrap should occur at a leading
    // delimiter.
    /// <include file='Doc/LJCTextBuilder.xml'
    ///  path='members/WrapAtDelimiter/*'/>
    public bool WrapAtDelimiter { get; set; }

    // Gets or sets a value that indicates if line wrapping is enabled.
    /// <include file='Doc/LJCTextBuilder.xml'
    ///  path='members/WrapEnabled/*'/>
    public bool WrapEnabled { get; set; }

    // Gets or sets the new line prefix.
    /// <include file='Doc/LJCTextBuilder.xml'
    ///  path='members/WrapPrefix/*'/>
    public string WrapPrefix { get; set; }
    #endregion
  }
}
