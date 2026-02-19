// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCInclude.cs
using LJCNetCommon;
using System.Collections.Generic;
using System.IO;

namespace LJCDocDataGenLib
{
  /// <summary>
  /// Provides methods to retrieve the Include file XML comment values.
  /// </summary>
  public class LJCInclude
  {
    #region Constructor Methods

    /// <summary>
    /// Initializes an object instance.
    /// </summary>
    public LJCInclude()
    {
      Comments = null;
      CurrentTag = null;
      LibName = null;
      XMLFile = null;
    }
    #endregion

    #region Public Methods

    /// <summary>
    /// Sets the XML comments from the specified include file.
    /// </summary>
    /// <param name="includeLine"></param>
    /// <param name="codeFileSpec"></param>
    public void SetComments(string includeLine, string codeFileSpec)
    {
      // Sets LibName, XMLFile and memberTag.
      var memberTag = "";
      if (SetIncludeValues(includeLine, codeFileSpec, ref memberTag))
      {
        var isContinue = false;
        var lines = File.ReadAllLines(XMLFile);

        var isStart = true;
        for (int index = 0; index < lines.Length; index++)
        {
          var line = lines[index];

          // Do not start processing until the memberTag is found.
          if (isStart)
          {
            if (line.IndexOf($"<{memberTag}>") < 0)
            {
              continue;
            }

            // Move to XML comments.
            index++;
            line = lines[index];
          }
          isStart = false;

          var trimLine = line.Trim();

          bool isValid;
          var endTag = LineEndTag(trimLine, out isValid);
          if (!isContinue)
          {
            // New comment.
            CurrentTag = LineBeginTag(line, out isValid);
            var comment = GetComment(line);
            var findEndTag = LineEndTag(trimLine, out isValid);
            if (!isValid)
            {
              // Not supported XML comment.
              // Quit processing lines.
              break;
            }
            if (null == findEndTag)
            {
              // No end tag so start Continue comment.
              isContinue = true;
            }
          }
          else
          {
            // Continue comment.
            if (NetString.HasValue(endTag))
            {
              // Has end tag so end Continue comment.
              isContinue = false;
            }
            var comment = GetComment(line);
            endTag = LineEndTag(comment, out isValid);
            if (!isValid)
            {
              // Not supported XML comment.
              // Quit processing lines.
              break;
            }
          }
        }
      }
    }
    #endregion

    #region Private Methods

    // Gets the comment for the provided line.
    private string GetComment(string line)
    {
      string retComment = null;

      bool isValid;
      var beginTag = LineBeginTag(line, out isValid);
      var endTag = LineEndTag(line, out isValid);

      if (null == beginTag)
      {
        // No BeginTag so set tag for start of comment.
        line = $"/{line}";
        beginTag = "/";
      }

      var startIndex = 0;
      var endDelimiter = endTag;
      var comment = NetString.GetDelimitedString(line, beginTag, ref startIndex
        , endDelimiter);
      if ("<code>" == CurrentTag
        && endTag != null)
      {
        comment = comment.TrimEnd();
      }

      var success = true;
      var findEndTag = LineEndTag(comment, out isValid);
      if (!isValid)
      {
        success = false;
      }

      if (success)
      {
        if ("/" == beginTag)
        {
          beginTag = null;
        }

        // Build an XML comment.
        retComment = "/// ";
        if (beginTag != null)
        {
          retComment += beginTag;
        }
        retComment += comment;
        if (endTag != null)
        {
          retComment += endTag;
        }

        if (retComment != null)
        {
          // Left Trim and Save potentially partial comment.
          retComment = LTrimXMLComment(retComment);
          findEndTag = LineEndTag(retComment, out isValid);
          if (isValid)
          {
            Comments.Add(retComment);
          }
        }
      }
      return retComment;
    }

    // Checks for a valid comment tag name.
    private bool IsCommentTagName(string tagName)
    {
      var retValue = false;

      if (tagName != null)
      {
        switch (tagName)
        {
          case "code":
          case "group":
          case "param":
          case "parentgroup":
          case "remarks":
          case "returns":
          case "summary":
            retValue = true;
            break;
        }
      }
      return retValue;
    }

    // Gets the begin tag.
    private string LineBeginTag(string line, out bool isValid)
    {
      isValid = false;
      string retBeginTag = null;

      var startIndex = 0;
      var beginTagName = NetString.GetDelimitedString(line, "<", ref startIndex
        , ">");

      if (IsCommentTagName(beginTagName))
      {
        isValid = true;
        retBeginTag = $"<{beginTagName}>";
      }
      if (!NetString.HasValue(beginTagName))
      {
        isValid = true;
      }
      return retBeginTag;
    }

    // Gets the end tag.
    private string LineEndTag(string line, out bool isValid)
    {
      isValid = false;
      string retEndTag = null;

      var startIndex = 0;
      var endTagName = NetString.GetDelimitedString(line, "</", ref startIndex
        , ">");

      if (IsCommentTagName(endTagName))
      {
        isValid = true;
        retEndTag = $"</{endTagName}>";
      }
      if (!NetString.HasValue(endTagName))
      {
        isValid = true;
      }
      return retEndTag;
    }

    // Replaces tabs with spaces and removes extra leading spaces
    private string LTrimXMLComment(string comment)
    {
      string retComment = comment.Replace("\t", "  ");

      var startIndex = "///".Length;
      var count = 6;
      var check = retComment.Substring(startIndex, count);

      // If at least count spaces, left trim the count spaces.
      if (check == new string(' ', count))
      {
        retComment = "///" + retComment.Substring(startIndex + count);
      }
      return retComment;
    }

    // Sets the Class include file values: LibName, XMLFile.
    private bool SetIncludeValues(string includeLine, string codeFileSpec
      , ref string memberTag)
    {
      var retValue = true;

      LibName = Path.GetFileNameWithoutExtension(codeFileSpec);
      Comments = new List<string>();
      var startIndex = 0;
      var xmlPath = NetString.GetDelimitedString(includeLine, "path=\""
        , ref startIndex, "\"");
      if (null == xmlPath)
      {
        xmlPath = NetString.GetDelimitedString(includeLine, "path='"
          , ref startIndex, "'");
        if (null == xmlPath)
        {
          retValue = false;
        }
      }

      if (retValue)
      {
        startIndex = 0;
        memberTag = NetString.GetDelimitedString(xmlPath, "members/"
          , ref startIndex, "/*");
        startIndex = 0;
        XMLFile = NetString.GetDelimitedString(includeLine, "file=\""
          , ref startIndex, "\"");
        if (null == XMLFile)
        {
          XMLFile = NetString.GetDelimitedString(includeLine, "file='"
            , ref startIndex, "'");
          if (null == XMLFile)
          {
            retValue = false;
          }
        }
        XMLFile = XMLFile.Replace("/", "\\");

        // Add code file path to doc file path to create XML file spec.
        var fileSpecPath = Path.GetDirectoryName(codeFileSpec);
        if (fileSpecPath != "")
        {
          XMLFile = $"{fileSpecPath}\\{XMLFile}";
        }
      }
      return retValue;
    }
    #endregion

    #region Properties

    /// <summary>The Incude comments.</summary>
    public List<string> Comments { get; private set; }

    /// <summary>The Current tag.</summary>
    public string CurrentTag { get; private set; }

    /// <summary>The code file name.</summary>
    public string LibName { get; private set; }

    /// <summary>The Include file spec.</summary>
    public string XMLFile { get; private set; }
    #endregion
  }
}
