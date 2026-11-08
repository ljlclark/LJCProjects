// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCParser.cs
using System;

namespace LJCNetCommon
{
  // Contains parsing related methods.
  /// <include file='Doc/LJCParser.xml'
  ///  path='members/LJCParser/*'/>
  public class LJCParser
  {
    #region Properties

    /// <summary>The delimited string begin index.</summary>
    public int BeginIndex { get; private set; }

    /// <summary>The delimited string end index.</summary>
    public int EndIndex { get; private set; }

    /// <summary>The parsing start index.</summary>
    public int StartIndex { get; set; }

    // Gets or sets the valid tag names.
    /// <include file='Doc/LJCParser.xml'
    ///  path='members/ValidTagNames/*'/>
    public string[] ValidTagNames
    {
      get { return _ValidTagNames; }
      set
      {
        if (NetCommon.HasListItems(value))
        {
          _ValidTagNames = value;
          Array.Sort(ValidTagNames);
        }
      }
    }
    private string[] _ValidTagNames;
    #endregion

    #region Constructor Methods

    // Initializes an object instance.
    /// <include file='Doc/LJCParser.xml'
    ///  path='members/Constructor/*'/>
    public LJCParser()
    {
      BeginIndex = -1;
      EndIndex = -1;
      StartIndex = 0;
    }
    #endregion

    #region Methods

    // Changes quotes between double and single quotes.
    /// <include file='Doc/LJCParser.xml'
    ///  path='members/ChangeQuote/*'/>
    public bool ChangeQuote(string text, out string changeValue)
    {
      bool retValue = false;

      changeValue = text;

      // ToDo?: Handle contained quotes.
      // If double quote, change to single quote.
      if (text.Contains("\""))
      {
        retValue = true;
        changeValue = text.Replace("\"", "'");
      }

      // If quote was not changed.
      if (!retValue)
      {
        // If single quote, change to double quote.
        if (text.Contains("'"))
        {
          retValue = true;
          changeValue = text.Replace("'", "\"");
        }
      }
      return retValue;
    }

    // Gets a delimited string.
    /// <include file='Doc/LJCParser.xml'
    ///  path='members/DelimitedString/*'/>
    public string DelimitedString(string text, string beginDelimiter = null
      , string endDelimiter = null, bool required = false, bool reset = true)
    {
      string retValue = null;

      if (reset)
      {
        StartIndex = 0;
      }

      BeginIndex = -1;
      EndIndex = -1;

      while (true)
      {
        // No text to parse.
        if (!NetString.HasValue(text))
        {
          break;
        }

        // Start parsing index is invalid.
        if (-1 == StartIndex
          || StartIndex > text.Length - 1)
        {
          break;
        }

        // Text does not contain the begin delimiter.
        if (beginDelimiter != null
          && !text.Contains(beginDelimiter))
        {
          break;
        }

        var beginLength = 0;
        BeginIndex = 0;
        //if (NetString.HasValue(beginDelimiter))
        if (beginDelimiter != null)
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
        //if (NetString.HasValue(endDelimiter))
        if (endDelimiter != null)
        {
          var index = text.IndexOf(endDelimiter, BeginIndex);
          if (index >= 0)
          {
            endLength = endDelimiter.Length;
            EndIndex = index;
          }
          if (required
            && index < 0)
          {
            EndIndex = BeginIndex;
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
        break;
      }
      return retValue;
    }

    // Finds a tag in a text value.
    /// <include file='Doc/LJCParser.xml'
    ///  path='members/FindTag/*'/>
    public string FindTag(string text, ref string tagName)
    {
      string retValue;

      var findValue = "<";
      if (!NetString.HasValue(tagName))
      {
        findValue += tagName;
      }
      retValue = DelimitedString(text, findValue, ">");
      var name = retValue;

      // Eliminate attributes.
      if (name != null
        && !findValue.Contains("/"))
      {
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
    /// <include file='Doc/LJCParser.xml'
    ///  path='members/RemoveSection/*'/>
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
    /// <include file='Doc/LJCParser.xml'
    ///  path='members/RemoveTags/*'/>
    public string RemoveTags(string text)
    {
      string retValue = text;

      string tag;
      do
      {
        string tagName = null;
        tag = FindTag(retValue, ref tagName);
        retValue = RemoveSection(retValue, BeginIndex, EndIndex);
      } while (tag != null);
      return retValue;
    }

    // Retrieve the selection string.
    /// <include file='Doc/LJCParser.xml'
    ///  path='members/Selection/*'/>
    public string Selection(string text)
    {
      string retValue = null;

      var length = SelectionLength();
      if (length > 0)
      {
        retValue = text.Substring(BeginIndex, length);
      }
      return retValue;
    }

    // Retrieve the selection length.
    /// <include file='Doc/LJCParser.xml'
    ///  path='members/SelectionLength/*'/>
    public int SelectionLength()
    {
      var retValue = 0;

      if (BeginIndex >= 0
        && EndIndex >= 0
        && BeginIndex < EndIndex)
      {
        retValue = EndIndex - BeginIndex;
      }
      return retValue;
    }

    // Gets the string including the supplied delimiters.
    /// <include file='Doc/LJCParser.xml'
    ///  path='members/StringWithDelimiters/*'/>
    public string StringWithDelimiters(string text, string beginDelimiter = null
      , string endDelimiter = null, bool required = false, bool reset = true)
    {
      string retValue = null;

      if (DelimitedString(text, beginDelimiter, endDelimiter, required, reset)
        != null)
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

    // Parses with different combinations of double or single quotes.
    /// <include file='Doc/LJCParser.xml'
    ///  path='members/ParseWithQuotes/*'/>
    public string ParseWithQuotes(string text, string beginDelimiter = null,
      string endDelimiter = null)
    {
      string retValue;

      var required = true;
      while (true)
      {
        // If the end delimiter is supplied it must exist in the text.
        retValue = DelimitedString(text, beginDelimiter, endDelimiter
          , required);
        if (NetString.HasValue(retValue))
        {
          break;
        }

        ChangeQuote(beginDelimiter, out string changeStart);
        ChangeQuote(endDelimiter, out string changeEnd);
        // If the end delimiter is supplied it must exist in the text.
        retValue = DelimitedString(text, changeStart, changeEnd
          , required);
        break;
      }
      return retValue;
    }
    #endregion

    #region Begin Tag Methods

    // Gets the begin tag.
    /// <include file='Doc/LJCParser.xml'
    ///  path='members/BeginTag/*'/>
    public string BeginTag(string line)
    {
      string retTag = null;

      var tagContents = BeginTagContents(line);
      if (NetString.HasValue(tagContents))
      {
        retTag = $"<{tagContents}";
      }
      return retTag;
    }

    // Gets the begin tag with attributes.
    /// <include file='Doc/LJCParser.xml'
    ///  path='members/BeginTagContents/*'/>
    public string BeginTagContents(string line)
    {
      string retContents = null;

      var tagContents = DelimitedString(line, "<", ">");
      if (NetString.HasValue(tagContents)
        && !tagContents.StartsWith("/"))
      {
        retContents = tagContents;
      }
      return retContents;
    }

    // Gets the begin tag name.
    /// <include file='Doc/LJCParser.xml'
    ///  path='members/BeginTagName/*'/>
    public string BeginTagName(string line)
    {
      string retName = null;

      string beginName = null;
      var beginContents = BeginTagContents(line);
      if (NetString.HasValue(beginContents))
      {
        beginName = beginContents;
        if (beginContents.Contains(" "))
        {
          beginName = DelimitedString(beginContents, null, " ");
        }
      }

      if (NetString.HasValue(beginName))
      {
        retName = beginName;
      }
      return retName;
    }

    // Indicates if a line starts with a specific begin tag name.
    /// <include file='Doc/LJCParser.xml'
    ///  path='members/IsBeginTagName/*'/>
    public bool IsBeginTagName(string line, string beginTagName)
    {
      var retValue = false;

      var tagName = BeginTagName(line);
      if (NetString.HasValue(tagName)
        && tagName == beginTagName)
      {
        retValue = true;
      }
      return retValue;
    }
    #endregion

    #region End Tag Methods

    // Gets the end tag.
    /// <include file='Doc/LJCParser.xml'
    ///  path='members/EndTag/*'/>
    public string EndTag(string line)
    {
      string retTag = null;

      var tagName = EndTagName(line);
      if (NetString.HasValue(tagName))
      {
        retTag = $"</{tagName}";
      }
      return retTag;
    }

    // Gets the end tag name.
    /// <include file='Doc/LJCParser.xml'
    ///  path='members/EndTagName/*'/>
    public string EndTagName(string line)
    {
      string retName = null;

      if (line.Trim().EndsWith("/>"))
      {
        // Set for empty tag.
        retName = ">";
      }
      else
      {
        var tagName = DelimitedString(line, "</", ">");
        if (NetString.HasValue(tagName))
        {
          retName = tagName;
        }
      }
      return retName;
    }

    // Indicates if a line ends with a specific end tag name.
    /// <include file='Doc/LJCParser.xml'
    ///  path='members/IsEndTagName/*'/>
    public bool IsEndTagName(string line, string endTagName)
    {
      var retValue = false;

      var tagName = EndTagName(line);
      if (NetString.HasValue(tagName)
        && tagName == endTagName)
      {
        retValue = true;
      }
      return retValue;
    }
    #endregion

    #region Valid Begin Tag Methods

    // Checks for a valid begin tag.
    /// <include file='Doc/LJCParser.xml'
    ///  path='members/HasValidBeginTag/*'/>
    public bool HasValidBeginTag(string line)
    {
      bool retValue = false;

      var beginTag = ValidBeginTag(line);
      if (NetString.HasValue(beginTag))
      {
        retValue = true;
      }
      return retValue;
    }

    // Checks for a begin tag and sets out value to it if found.
    /// <include file='Doc/LJCParser.xml'
    ///  path='members/HasValidBeginTagName/*'/>
    public bool HasValidBeginTagName(string line, out string beginName)
    {
      bool retValue = false;

      beginName = null;

      var tagName = ValidBeginTagName(line);
      if (NetString.HasValue(tagName))
      {
        beginName = tagName;
        retValue = true;
      }
      return retValue;
    }

    // Gets the valid begin tag if begin tag is valid.
    /// <include file='Doc/LJCParser.xml'
    ///  path='members/ValidBeginTag/*'/>
    public string ValidBeginTag(string line)
    {
      string retTag = null;

      var tagContents = BeginTagContents(line);
      var tagName = BeginTagName(line);

      // Has tag name.
      if (NetString.HasValue(tagContents)
        && NetString.HasValue(tagName)
        && IsValidTagName(tagName))
      {
        retTag = $"<{tagContents}>";
      }
      return retTag;
    }

    // Gets the valid tag name if the line contains a begin tag.
    /// <include file='Doc/LJCParser.xml'
    ///  path='members/ValidBeginTagName/*'/>
    public string ValidBeginTagName(string line)
    {
      string retName = null;

      // Get the first tag.
      var beginName = BeginTagName(line);

      // Has tag and is not an end tag.
      if (NetString.HasValue(beginName)
        && IsValidTagName(beginName))
      {
        retName = beginName;
      }
      return retName;
    }
    #endregion

    #region Valid End Tag Methods

    // Checks for a valid end tag.
    /// <include file='Doc/LJCParser.xml'
    ///  path='members/HasValidEndTag/*'/>
    public bool HasValidEndTag(string line)
    {
      bool retValue = false;

      var endTag = ValidEndTag(line);
      if (NetString.HasValue(endTag))
      {
        retValue = true;
      }
      return retValue;
    }

    // Checks for a begin tag and sets out value to it if found.
    /// <include file='Doc/LJCParser.xml'
    ///  path='members/HasValidEndTagName/*'/>
    public bool HasValidEndTagName(string line, out string endName)
    {
      var retValue = false;

      endName = null;

      var tagName = ValidEndTagName(line);
      if (NetString.HasValue(tagName))
      {
        endName = tagName;
        retValue = true;
      }
      return retValue;
    }

    // Gets the valid begin tag if begin tag is valid.
    /// <include file='Doc/LJCParser.xml'
    ///  path='members/ValidEndTag/*'/>
    public string ValidEndTag(string line)
    {
      string retTag = null;

      var endName = EndTagName(line);

      // Has tag name.
      if (NetString.HasValue(endName)
        && IsValidTagName(endName))
      {
        retTag = $"</{endName}>";
      }
      return retTag;
    }

    // Gets the valid tag name if the line contains an end tag.
    /// <include file='Doc/LJCParser.xml'
    ///  path='members/ValidEndTagName/*'/>
    public string ValidEndTagName(string line)
    {
      string retName = null;

      var endName = EndTagName(line);

      if (NetString.HasValue(endName)
        && IsValidTagName(endName))
      {
        retName = endName;
      }
      return retName;
    }
    #endregion

    #region Name and Tag Methods

    // Gets the tag name for the end tag.
    /// <include file='Doc/LJCParser.xml'
    ///  path='members/ShortBeginTagToName/*'/>
    public string ShortBeginTagToName(string shortBeginTag)
    {
      string retName = null;

      if (NetString.HasValue(shortBeginTag))
      {
        retName = shortBeginTag.Trim();
        retName = retName.Substring(1, retName.Length - 1);
      }
      return retName;
    }

    // Gets the end tag for the tag name.
    /// <include file='Doc/LJCParser.xml'
    ///  path='members/NameToShortBeginTag/*'/>
    public string NameToShortBeginTag(string tagName)
    {
      string retTag = null;

      if (tagName != null)
      {
        retTag = $"<{tagName}";
      }
      return retTag;
    }

    // Gets the tag name for the end tag.
    /// <include file='Doc/LJCParser.xml'
    ///  path='members/EndTagToName/*'/>
    public string EndTagToName(string endTag)
    {
      string retName = null;

      if (NetString.HasValue(endTag))
      {
        retName = endTag.Trim();
        if ('/' == retName[2])
        {
          retName = endTag.Trim();
          retName = retName.Substring(2, retName.Length - 3);
        }
      }
      return retName;
    }

    // Gets the end tag for the tag name.
    /// <include file='Doc/LJCParser.xml'
    ///  path='members/NameToEndTag/*'/>
    public string NameToEndTag(string tagName)
    {
      string retTag = null;

      if (tagName != null)
      {
        retTag = $"</{tagName}>";
      }
      return retTag;
    }

    // Checks for a valid comment tag name.
    /// <include file='Doc/LJCParser.xml'
    ///  path='members/IsValidTagName/*'/>
    private bool IsValidTagName(string tagName)
    {
      var retValue = false;

      if (tagName != null
        && NetCommon.HasListItems(ValidTagNames))
      {
        int index = Array.BinarySearch(ValidTagNames, tagName);
        if (index >= 0
          && index < ValidTagNames.Length)
        {
          retValue = true;
        }
      }
      return retValue;
    }
    #endregion
  }
}
