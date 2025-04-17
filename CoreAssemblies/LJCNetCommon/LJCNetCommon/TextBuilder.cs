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
        WrapEnabled = false;
        WrapPrefix = "  ";
      }
      Builder = builder;
      DebugText = "";
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

    // Changes the IndentCount by the supplied value.
    /// <include path='items/AddIndent/*' file='Doc/TextBuilder.xml'/>
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

    // Adds text without processing.
    /// <include path='items/Add/*' file='Doc/TextBuilder.xml'/>
    public void Add(string text)
    {
      if (TextLength(text) > 0)
      {
        Builder.Append(text);
        DebugText += text;
      }
    }

    // Adds a delimiter if not the first list item
    /// <include path='items/Item/*' file='Doc/TextBuilder.xml'/>
    public string Item(string text)
    {
      // If not IsFirst, start with the delimiter.
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
      DebugText += "\r\n";
      LineLength = 0;
      return retText;
    }

    // Adds a newline if line length is greater than LineLimit.
    /// <include path='items/Text/*' file='Doc/TextBuilder.xml'/>
    public string Text(string text, bool allowStartIndent = true
      , bool allowNewLine = true)
    {
      var retText = "";

      if (TextLength(text) > 0)
      {
        retText = text;
        if (allowStartIndent)
        {
          retText = GetIndented(text);
        }

        if (allowNewLine
          && HasText)
        {
          retText = "\r\n";
          retText += GetIndented(text);
        }

        bool isReturn = false;
        if (!WrapEnabled)
        {
          // Just add text.
          isReturn = true;
          Builder.Append(retText);
          DebugText += retText;
          IsFirst = false;
        }

        if (!isReturn)
        {
          retText = GetWrapped(retText);
          Builder.Append(retText);
          DebugText += retText;
          IsFirst = false;
        }
      }
      return retText;
    }
    #endregion

    #region Get Text Methods

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

    /// <summary>Returns the current indent string.</summary>
    public string GetIndentString()
    {
      var retValue = new string(' ', IndentCount * IndentCharCount);
      return retValue;
    }

    // Gets the sync TextState object.
    /// <include path='items/GetSyncIndent/*' file='Doc/TextBuilder.xml'/>
    public TextState GetSyncIndent(TextState textState)
    {
      var retState = new TextState(textState.IndentCount);
      AddIndent(retState.IndentCount);
      return retState;
    }

    // Sync calling function text state.
    /// <include path='items/SyncState/*' file='Doc/TextBuilder.xml'/>
    public void SyncState(TextState toTextState, TextState fromTextState)
    {
      toTextState.IndentCount = fromTextState.IndentCount;
      toTextState.ChildIndentCount = fromTextState.ChildIndentCount;
    }

    // Adds added text and new wrapped line if combined line > LineLimit.
    /// <include path='items/GetWrapped/*' file='Doc/TextBuilder.xml'/>
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
          var lineText = $"{WrapPrepend()}{wrapText}";
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
        nextLength = LineLimit - TextLength(WrapPrepend());
        nextLength = tempText.LastIndexOf(" ", nextLength);
        retText = text.Substring(startIndex, nextLength);
      }
      return retText;
    }
    #endregion

    #region Properties

    /// <summary>The internal StringBuilder class.</summary>
    public StringBuilder Builder { get; set; }

    /// <summary>Gets or sets the delimiter.</summary>
    public string Delimiter { get; set; }

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

    /// <summary>Gets or sets the first item indicator.</summary>
    public bool IsFirst { get; set; }

    /// <summary>Gets the current length.</summary>
    public int LineLength { get; private set; }

    /// <summary>Gets or sets the character limit.</summary>
    public int LineLimit { get; private set; }

    /// <summary>
    /// Indicates that a wrap should occur at a leading delimiter.
    /// </summary>
    public bool WrapAtDelimiter { get; set; }

    /// <summary>Indicates if line wrapping is enabled.</summary>
    public bool WrapEnabled { get; set; }

    /// <summary>Gets or sets the new line prefix.</summary>
    public string WrapPrefix { get; set; }
    #endregion

    #region Class Data

    /// <summary></summary>
    public string DebugText;
    #endregion
  }
}
