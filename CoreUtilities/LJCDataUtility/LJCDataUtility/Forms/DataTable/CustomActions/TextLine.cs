using LJCNetCommon;
using System;
using System.Threading;

namespace LJCDataUtility
{
  /// <summary>Represents a text line.</summary>
  public class TextLine
  {
    #region Constructors

    /// <summary>
    /// Initializes a new object instance with the supplied values.
    /// </summary>
    /// <param name="newLinePrefix">The new line prefix.</param>
    /// <param name="delimiter">The delimiter.</param>
    /// <param name="charLimit">The character limit.</param>
    public TextLine(string newLinePrefix = "  ", string delimiter = ", "
      , int charLimit = 80
      )
    {
      NewLinePrefix = newLinePrefix;
      Delimiter = delimiter;
      CharLimit = charLimit;

      Reset();
    }

    /// <summary>
    /// Resets class values.
    /// </summary>
    public void Reset()
    {
      IsFirst = true;
      Length = 0;
    }

    #endregion

    #region Methods

    /// <summary>
    /// Adds a delimiter if not the first list item.
    /// </summary>
    /// <param name="text">The next append value.</param>
    /// <returns>The new text value.</returns>
    public string AddDelimiter(string text)
    {
      string retText = text;

      if (!IsFirst)
      {
        retText = $"{Delimiter}{retText}";
      }
      Length += retText.Length;
      IsFirst = false;
      return retText;
    }

    /// <summary>
    /// Adds a delimiter if not the first list item
    /// and adds a newline if line length is greater than CharLength.
    /// </summary>
    /// <param name="text">Next append value.</param>
    /// <returns>The new text value.</returns>
    public string AddExpanded(string text)
    {
      var retText = AddDelimiter(text);
      retText = AddBreak(retText);
      return retText;
    }

    /// <summary>
    /// Adds a newline if line length is greater than CharLength.
    /// </summary>
    /// <param name="text">The next append value.</param>
    /// <returns>The new text value.</returns>
    public string AddBreak(string text)
    {
      string retText = text;

      if (Length > CharLimit)
      {
        retText += "\r\n" + NewLinePrefix;
        Length = NewLinePrefix.Length;
      }
      return retText;
    }

    /// <summary>
    /// Sets the length for the next append value.
    /// </summary>
    /// <param name="text">The next append value.</param>
    public void NextValue(string text)
    {
      if (NetString.HasValue(text))
      {
        Length += text.Length;
      }
    }
    #endregion

    #region Properties

    /// <summary>Gets or sets the character limit.</summary>
    public int CharLimit { get; set; }

    /// <summary>Gets or sets the delimiter.</summary>
    public string Delimiter { get; set; }

    /// <summary>Gets or sets the first item indicator.</summary>
    public bool IsFirst { get; set; }

    /// <summary>Gets the current length.</summary>
    public int Length { get; private set; }

    /// <summary>Gets or sets the new line prefix.</summary>
    public string NewLinePrefix { get; set; }
    #endregion
  }
}
