// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// NetString.xml
using System;

namespace LJCNetCommon
{
  // Contains parsing related methods.
  /// <include path='members/LJCParser/*' file='Doc/LJCParser.xml'/>
  public class LJCParser
  {
    #region Constructor Methods

    // Initializes an object instance.
    /// <include path='members/Constructor/*' file='Doc/LJCParser.xml'/>
    public LJCParser()
    {
      BeginIndex = -1;
      EndIndex = -1;
      StartIndex = 0;
    }
    #endregion

    #region Methods

    // Gets the delimited string.
    /// <include path='members/DelimitedString/*' file='Doc/LJCParser.xml'/>
    public string DelimitedString(string text, string beginDelimiter = null
      , string endDelimiter = null)
    {
      string retValue = null;

      BeginIndex = -1;
      EndIndex = -1;

      if (StartIndex > -1
        && NetString.HasValue(text)
        && StartIndex < text.Length - 1
        && (null == beginDelimiter
        || text.Contains(beginDelimiter)))
      {
        var beginLength = 0;
        BeginIndex = 0;
        if (NetString.HasValue(beginDelimiter))
        {
          var index = text.IndexOf(beginDelimiter, StartIndex);
          if (index >= 0)
          {
            beginLength = beginDelimiter.Length;
            BeginIndex = index + beginLength;
          }
        }

        int endLength = 0;
        EndIndex = text.Length;
        if (NetString.HasValue(endDelimiter))
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
          && EndIndex < text.Length - (beginLength + endLength))
        {
          // There is enough text to potentially contain another begin
          // and end delimiter.
          StartIndex = EndIndex + 1;
        }
      }
      return retValue;
    }

    // Finds a tag in a text value.
    /// <include path='members/FindTag/*' file='Doc/LJCParser.xml'/>
    public string FindTag(string text, ref string tagName)
    {
      string retValue;

      var findValue = "<";
      if (!NetString.HasValue(tagName))
      {
        findValue += tagName;
      }
      StartIndex = 0;
      retValue = DelimitedString(text, findValue, ">");
      var name = retValue;

      // Eliminate attributes.
      if (name != null
        && !findValue.Contains("/"))
      {
        StartIndex = 0;
        name = StringWithDelimiters(retValue, retValue[0].ToString(), " ");
        if (NetString.HasValue(name))
        {
          name = name.Substring(0, name.Length - 1);
        }
      }

      if (tagName == "/")
      {
        name = $"/{name}";
      }
      tagName = name;
      return retValue;
    }

    // Removes a section from a text value.
    /// <include path='members/RemoveSection/*' file='Doc/LJCParser.xml'/>
    public string RemoveSection(string text, int beginIndex, int endIndex)
    {
      string retValue = text;

      if (beginIndex >= 0
        && endIndex >= beginIndex)
      {
        BeginIndex = -1;
        EndIndex = -1;
        var value = retValue.Substring(0, beginIndex);
        value += retValue.Substring(endIndex + 1);
        retValue = value;
      }
      return retValue;
    }

    // Removes tags from a text value.
    /// <include path='members/RemoveTags/*' file='Doc/LJCParser.xml'/>
    public string RemoveTags(string text)
    {
      string retValue = text;

      string tag;
      do
      {
        string tagName = null;
        StartIndex = 0;
        tag = FindTag(retValue, ref tagName);
        retValue = RemoveSection(retValue, BeginIndex, EndIndex);
      } while (tag != null);
      return retValue;
    }

    // Gets the string including the supplied delimiters.
    /// <include path='members/StringWithDelimiters/*' file='Doc/LJCParser.xml'/>
    public string StringWithDelimiters(string text, string beginDelimiter = null
      , string endDelimiter = null)
    {
      string retValue = null;

      if (DelimitedString(text, beginDelimiter, endDelimiter) != null)
      {
        // Include Delimiters
        int endLength = endDelimiter.Length;
        if (!NetString.HasValue(endDelimiter))
        {
          endLength = 0;
        }
        int textLength = EndIndex - BeginIndex + endLength + 1;
        retValue = text.Substring(BeginIndex - 1, textLength);
      }
      return retValue;
    }
    #endregion

    #region Properties

    /// <summary>The delimited string begin index.</summary>
    public int BeginIndex { get; private set; }


    /// <summary>The delimited string end index.</summary>
    public int EndIndex { get; private set; }

    /// <summary>The parsing start index.</summary>
    public int StartIndex { get; set; }
    #endregion
  }
}
