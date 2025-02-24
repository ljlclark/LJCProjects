﻿// Copyright(c) Lester J. Clark and Contributors.
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
        LineLimit = 80;
        IndentCharCount = 2;
        IndentCount = 0;
        IsFirst = true;
        LineLength = 0;
        WrapPrefix = "  ";
        WrapAtDelimiter = true;
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

    // Adds a delimiter if not the first list item.
    /// <include path='items/Delimit/*' file='Doc/TextBuilder.xml'/>
    public string Delimit(string text)
    {
      var retText = GetDelimited(text);
      Text(retText);
      return retText;
    }

    // Adds a delimiter if not the first list item
    // and adds a newline if line length is greater than LineLimit.
    /// <include path='items/Format/*' file='Doc/TextBuilder.xml'/>
    public string Format(string text)
    {
      var retText = GetDelimited(text);
      Wrap(retText);
      return retText;
    }

    // Adds a line.
    /// <include path='items/Line/*' file='Doc/TextBuilder.xml'/>
    public string Line(string text = null)
    {
      var retText = GetIndented(text);

      // Adds a new line even if text is null.
      Builder.AppendLine(retText);
      LineLength = retText.Length;

      // Allow add of blank characters.
      if (text != null
        && text != string.Empty)
      {
        IsFirst = false;
        LineLength += retText.Length;
      }
      return retText;
    }

    // Adds text.
    /// <include path='items/Text/*' file='Doc/TextBuilder.xml'/>
    public string Text(string text)
    {
      var retText = GetIndented(text);
      if (retText != null
        && retText != string.Empty)
      {
        Builder.Append(retText);
        IsFirst = false;
        LineLength += retText.Length;
      }
      return retText;
    }

    // Adds a newline if line length is greater than LineLimit.
    /// <include path='items/Wrap/*' file='Doc/TextBuilder.xml'/>
    public string Wrap(string text)
    {
      var retText = GetWrapped(text, out bool isNewLine
        , out string wrapText);
      Text(retText);
      if (isNewLine)
      {
        if (wrapText != null)
        {
          LineLength = wrapText.Length;
          LineLength += GetIndentString().Length;
          if (WrapPrefix != null)
          {
            LineLength += WrapPrefix.Length;
          }
        }
        else
        {
          LineLength = retText.Length - 2;
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

      var resultLength = LineLength + text.Length;
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
    /// Indicates that a wrap should occur at the a leading delimiter.
    /// </summary>
    public bool WrapAtDelimiter { get; set; }

    /// <summary>Gets or sets the new line prefix.</summary>
    public string WrapPrefix { get; set; }
    #endregion
  }
}
