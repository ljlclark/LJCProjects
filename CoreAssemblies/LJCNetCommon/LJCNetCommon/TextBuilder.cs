// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// TextBuilder.cs
using System;
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
      var retText = GetDelimit(text);
      Text(retText);
      return retText;
    }

    // Adds a delimiter if not the first list item
    /// <include path='items/Format/*' file='Doc/TextBuilder.xml'/>
    public string Format(string text)
    {
      var retText = GetDelimit(text);
      Wrap(retText);
      return retText;
    }

    /// <summary>Adds a line.</summary>
    /// <param name="text">The next append value.</param>
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

    /// <summary>Adds text.</summary>
    /// <param name="text">The next append value.</param>
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
    /// <include path='items/Break/*' file='Doc/TextBuilder.xml'/>
    public string Wrap(string text)
    {
      var retText = GetWrap(text, out bool isNewLine);
      Text(retText);
      if (isNewLine)
      {
        LineLength = retText.Length - 2;
      }
      return retText;
    }
    #endregion

    #region Get Modified String Methods

    // Adds a delimiter if not the first list item.
    /// <include path='items/GetDelimit/*' file='Doc/TextBuilder.xml'/>
    public string GetDelimit(string text)
    {
      string retText = text;

      if (!IsFirst)
      {
        retText = $"{Delimiter}{retText}";
      }
      return retText;
    }

    /// <summary>Gets a new line with prefixed indent.</summary>
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
    /// <include path='items/GetBreak/*' file='Doc/TextBuilder.xml'/>
    public string GetWrap(string text, out bool isNewLine)
    {
      string retText = text;

      isNewLine = false;
      var resultLength = LineLength + text.Length;

      // Don't wrap a new line.
      if (LineLength > GetIndentString().Length
        && resultLength > LineLimit)
      {
        var indent = GetIndentString();
        retText = $"\r\n{indent}{WrapPrefix}{text}";
        isNewLine = true;
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

    /// <summary>Gets or sets the new line prefix.</summary>
    public string WrapPrefix { get; set; }
    #endregion
  }
}
