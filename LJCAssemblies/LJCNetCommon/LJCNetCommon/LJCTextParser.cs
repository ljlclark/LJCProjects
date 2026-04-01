// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCTextParser.cs

namespace LJCNetCommon
{
  // Contains parsing related methods.
  /// <include path="members/LJCTextParser/*" file="Doc/LJCTextParser.xml"/>
  public class LJCTextParser
  {
    #region Static Methods

    // Gets or sets the delimiter length.
    private static int DelimiterLength(string? delimiter)
    {
      int retLength = 0;
      if (LJC.HasValue(delimiter))
      {
        retLength = delimiter.Length;
      }
      return retLength;
    }
    #endregion

    #region Constructors

    // Initializes an object instance.
    /// <include path="members/Constructor/*" file="Doc/LJCTextParser.xml"/>
    public LJCTextParser()
    {
      BeginIndex = -1;
      EndIndex = -1;
      StartIndex = 0;
    }
    #endregion

    #region Delimited String Methods

    // Retrieves a delimited string.
    /// <include path="members/DelimitedString/*" file="Doc/LJCTextParser.xml"/>
    public string? DelimitedString(string text, string? beginDelimiter = null
      , string? endDelimiter = null)
    {
      string? retValue = null;

      BeginIndex = -1;
      EndIndex = -1;

      // ToDo: What to do if text does not contain beginDelimiter.
      if (StartIndex > -1
        && LJC.HasValue(text)
        && StartIndex < text.Length - 1
        && (null == beginDelimiter
        || text.Contains(beginDelimiter)))
      {
        BeginIndex = 0;
        if (beginDelimiter != null)
        {
          var index = text.IndexOf(beginDelimiter, StartIndex);
          if (index >= 0)
          {
            // Begin after beginDelimiter.
            var beginLength = beginDelimiter.Length;
            BeginIndex = index + beginLength;
          }
        }

        int endLength = 0;
        EndIndex = text.Length;
        if (endDelimiter != null)
        {
          var index = text.IndexOf(endDelimiter, BeginIndex);
          if (index >= 0)
          {
            endLength = endDelimiter.Length;
            EndIndex = index;
          }
        }

        int textLength = EndIndex - BeginIndex;
        retValue = text.Substring(BeginIndex, textLength);

        StartIndex = -1;
        if (EndIndex >= 0
          && EndIndex + endLength < text.Length)
        {
          // There is enough text to potentially contain more delimiters.
          StartIndex = EndIndex + 1;
        }

        // Set to actual text end.
        if (EndIndex > BeginIndex)
        {
          EndIndex--;
        }
      }
      return retValue;
    }

    // Retrieves a string including the supplied delimiters.
    /// <include path="members/StringWithDelimiters/*" file="Doc/LJCTextParser.xml"/>
    public string? StringWithDelimiters(string text, string? beginDelimiter = null
      , string? endDelimiter = null)
    {
      string? retValue = null;

      if (DelimitedString(text, beginDelimiter, endDelimiter) != null)
      {
        // Include Delimiters
        int beginLength = DelimiterLength(beginDelimiter);
        int endLength = DelimiterLength(endDelimiter);
        int textLength = EndIndex - BeginIndex + 1;
        textLength += beginLength + endLength;
        var beginIndex = BeginIndex - beginLength;
        //retValue = text.Substring(BeginIndex - 1, textLength);
        retValue = text.Substring(beginIndex, textLength);
      }
      return retValue;
    }
    #endregion

    #region Find Tag Methods

    // Finds the first tag in a text value.
    /// <include path="members/FindTag/*" file="Doc/LJCTextParser.xml"/>
    public string? FindTag(string text, ref string? tagName)
    {
      string? retTag = null;

      var beginDelimiter = "<";
      if (LJC.HasValue(tagName)
        && tagName.Contains('/'))
      {
        beginDelimiter += "/";
      }
      var endDelimiter = ">";
      StartIndex = 0;
      var foundTagName = DelimitedString(text, beginDelimiter, endDelimiter);

      // Eliminate attributes, if not end tag.
      if (LJC.HasValue(foundTagName)
        && !foundTagName.Contains('/'))
      {
        StartIndex = 0;
        if (foundTagName.Contains(' '))
        {
          beginDelimiter = null;
          endDelimiter = " ";
          foundTagName = DelimitedString(foundTagName, beginDelimiter
            , endDelimiter);
        }
      }

      if (LJC.HasValue(tagName)
        && tagName.Contains('/'))
      {
        foundTagName = $"/{foundTagName}";
      }
      tagName = foundTagName;
      if (LJC.HasValue(foundTagName))
      {
        retTag = $"<{foundTagName}>";
      }
      return retTag;
    }

    // Removes a section from a text value.
    /// <include path="members/RemoveSection/*" file="Doc/LJCTextParser.xml"/>
    public string RemoveSection(string text, int beginIndex, int endIndex)
    {
      string retValue = text;

      if (beginIndex >= 0
        && endIndex >= beginIndex)
      {
        BeginIndex = -1;
        EndIndex = -1;
        // Get value to the beginIndex.
        var value = retValue[..beginIndex];

        // Add the value from the endIndex.
        if (endIndex <= retValue.Length - 1)
        {
          endIndex++;
        }
        value += retValue[endIndex..];
        retValue = value;
      }
      return retValue;
    }

    // Removes tags from a text value.
    /// <include path="members/RemoveTags/*" file="Doc/LJCTextParser.xml"/>
    public string RemoveTags(string text)
    {
      string retValue = text;

      string? tag;
      do
      {
        string? tagName = null;
        StartIndex = 0;
        tag = FindTag(retValue, ref tagName);
        if (tag != null)
        {
          retValue = RemoveSection(retValue, BeginIndex - 1, EndIndex + 1);
        }
      } while (tag != null);
      return retValue;
    }
    #endregion

    #region Properties

    // The delimited string begin index.
    /// <include path="members/BeginIndex/*" file="Doc/LJCTextParser.xml"/>
    public int BeginIndex { get; private set; }

    // The delimited string end index.
    /// <include path="members/EndIndex/*" file="Doc/LJCTextParser.xml"/>
    public int EndIndex { get; private set; }

    // The parsing start index.
    /// <include path="members/StartIndex/*" file="Doc/LJCTextParser.xml"/>
    public int StartIndex { get; set; }
    #endregion
  }
}
