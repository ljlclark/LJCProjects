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
    public TextBuilder(int capacity, StringBuilder builder = null)
    {
      if (null == builder)
      {
        builder = new StringBuilder(capacity);
        NewLinePrefix = "  ";
        Delimiter = ", ";
        CharLimit = 80;
        IsNewLine = false;
        IsFirst = true;
        Length = 0;
      }
      Builder = builder;
    }
    #endregion

    #region Methods

    /// <summary>Implements the ToString() method.</summary>
    public override string ToString()
    {
      return Builder.ToString();
    }

    // Adds a delimiter if not the first list item
    /// <include path='items/AddExpanded/*' file='Doc/TextBuilder.xml'/>
    public string AddDelimitBreak(string text)
    {
      var retText = GetWithDelimiter(text);
      AddWithBreak(retText);
      return retText;
    }

    // Adds a newline if line length is greater than CharLength.
    /// <include path='items/AddWithBreak/*' file='Doc/TextBuilder.xml'/>
    public string AddWithBreak(string text)
    {
      var retText = GetWithBreak(text);
      Text(retText);
      if (IsNewLine)
      {
        Length = NewLinePrefix.Length;
      }
      return retText;
    }

    // Adds a delimiter if not the first list item.
    /// <include path='items/AddWithDelimiter/*' file='Doc/TextBuilder.xml'/>
    public string AddWithDelimiter(string text)
    {
      var retText = GetWithDelimiter(text);
      Text(retText);
      return retText;
    }

    // Adds a newline if line length is greater than CharLength.
    /// <include path='items/GetWithBreak/*' file='Doc/TextBuilder.xml'/>
    public string GetWithBreak(string text)
    {
      string retText = text;

      IsNewLine = false;
      if (Length > CharLimit)
      {
        retText += "\r\n" + NewLinePrefix;
        IsNewLine = true;
      }
      return retText;
    }

    // Adds a delimiter if not the first list item.
    /// <include path='items/GetWithDelimiter/*' file='Doc/TextBuilder.xml'/>
    public string GetWithDelimiter(string text)
    {
      string retText = text;

      if (!IsFirst)
      {
        retText = $"{Delimiter}{retText}";
      }
      return retText;
    }

    /// <summary>Adds a line.</summary>
    /// <param name="text">The next append value.</param>
    public void Line(string text = null)
    {
      Builder.AppendLine(text);
      if (text != null)
      {
        IsFirst = false;
        Length += text.Length;
      }
    }

    /// <summary>Adds text.</summary>
    /// <param name="text">The next append value.</param>
    public void Text(string text)
    {
      if (text != null)
      {
        Builder.Append(text);
        IsFirst = false;
        Length += text.Length;
      }
    }
    #endregion

    #region Properties

    /// <summary>The internal StringBuilder class.</summary>
    public StringBuilder Builder { get; set; }

    /// <summary>Gets or sets the character limit.</summary>
    public int CharLimit { get; set; }

    /// <summary>Gets or sets the delimiter.</summary>
    public string Delimiter { get; set; }

    /// <summary>Gets or sets the first item indicator.</summary>
    public bool IsFirst { get; set; }

    /// <summary>Indicates if the text starts on a new line.</summary>
    private bool IsNewLine { get; set; }

    /// <summary>Gets the current length.</summary>
    public int Length { get; private set; }

    /// <summary>Gets or sets the new line prefix.</summary>
    public string NewLinePrefix { get; set; }
    #endregion
  }
}
