// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCInclude.cs
using LJCNetCommon;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace LJCDocDataGenLib
{
  /// <summary>
  /// Provides methods to retrieve the Include file XML comment values.
  /// </summary>
  public class LJCInclude
  {
    #region Properties

    // Gets the include XML comments.
    /// <include file='Doc/LJCInclude.xml'
    ///  path='members/CurrentTag/*'/>
    public List<string> Comments { get; private set; }

    // Gets the current tag value.
    /// <include file='Doc/LJCInclude.xml'
    ///  path='members/CurrentTag/*'/>
    public string CurrentTag { get; private set; }

    // Gets the project (Lib) name.
    /// <include file='Doc/LJCInclude.xml'
    ///  path='members/LibName/*'/>
    public string LibName { get; private set; }

    // Gets the include XML file name.
    /// <include file='Doc/LJCInclude.xml'
    ///  path='members/XMLFile/*'/>
    public string IncludeFile { get; private set; }
    #endregion

    #region Constructor Methods

    // Initializes an object instance.
    /// <include file='Doc/LJCInclude.xml'
    ///  path='members/Constructor/*'/>
    public LJCInclude()
    {
      Comments = null;
      CurrentTag = null;
      LibName = null;
      IncludeFile = null;
    }
    #endregion

    #region Methods

    // Sets the XML comments from the specified include file.
    /// <include file='Doc/LJCInclude.xml'
    ///  path='members/SetComments/*'/>
    public void SetComments(string[] bothLines, string codeFileSpec)
    {
      // Sets LibName, IncludeFile and memberTag.
      var memberTag = "";
      if (SetIncludeValues(bothLines, codeFileSpec, ref memberTag))
      {
        var isNewComment = true;
        var lines = File.ReadAllLines(IncludeFile);

        var isStart = true;
        for (int index = 0; index < lines.Length; index++)
        {
          // Start processing at first child of the member tag.
          var line = SetToMemberTags(lines, memberTag, ref index, ref isStart);

          // Comment can span multiple lines.
          var trimLine = line.Trim();
          var endTag = LineEndTag(trimLine, out _);
          //DebugBreak(endTag, "</example>");
          //DebugBreak(endTag, "</code>");
          if (isNewComment)
          {
            CurrentTag = LineBeginTag(line, out bool isValid);
            //DebugBreak(CurrentTag, "<example>");
            //DebugBreak(CurrentTag, "<code");
            if (!isValid)
            {
              break;
            }
            SetComment(trimLine);
            isNewComment = NetString.HasValue(endTag);
          }
          else
          {
            isNewComment = NetString.HasValue(endTag);
            SetComment(trimLine);
          }
        }
      }
    }

    // Gets the comment for the provided line.
    private string SetComment(string line)
    {
      string retComment;

      var beginTag = LineBeginTag(line, out _);
      var endTag = LineEndTag(line, out _);

      while (true)
      {
        var parser = new LJCParser();

        // If endTag is empty, returns comment to end of text line.
        var commentText = parser.DelimitedString(line, beginTag, endTag);
        if ("<code>" == CurrentTag
          && endTag != null)
        {
          commentText = commentText.TrimEnd();
        }

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
        retComment += commentText;
        if (endTag != null)
        {
          retComment += endTag;
        }

        if (retComment != null)
        {
          // Left Trim and Save potentially partial comment.
          retComment = LTrimXMLComment(retComment);
          Comments.Add(retComment);
        }
        break;
      }
      return retComment;
    }
    #endregion

    #region Private Methods

    // Changes quotes between double and single quotes.
    private bool ChangeQuote(string text, out string changeValue)
    {
      bool retValue = false;

      changeValue = text;

      // ToDo: Handle contained quotes.
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

    // Performs a debug break if a value contains a compare to value.
    private void DebugBreak(string compare, string compareTo)
    {
      if (compare != null
        && compareTo != null)
      {
        compare = compare.ToLower();
        compareTo = compareTo.ToLower();
        if (compare.Contains(compareTo))
        {
          Debugger.Break();
        }
      }
    }

    // Checks for a valid comment tag name.
    private bool IsCommentTagName(string tagName)
    {
      var retValue = false;

      if (tagName != null)
      {
        // Eliminate attributes.
        if (tagName != null
          && tagName.Contains(" "))
        {
          var parser = new LJCParser();
          tagName = parser.DelimitedString(tagName, null, " ");
        }

        switch (tagName.ToLower())
        {
          case "code":
          case "example":
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

      var parser = new LJCParser();
      var beginTagName = parser.DelimitedString(line, "<", ">");

      // Skip end tag.
      if (beginTagName != null
        && beginTagName.StartsWith("/"))
      {
        beginTagName = null;
      }

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
      string retEndTag = null;

      isValid = false;

      var parser = new LJCParser();
      var endTagName = parser.DelimitedString(line, "</", ">");

      if (IsCommentTagName(endTagName))
      {
        isValid = true;
        retEndTag = $"</{endTagName}>";
      }
      return retEndTag;
    }

    // Replaces tabs with spaces and removes extra leading spaces
    private string LTrimXMLComment(string comment)
    {
      string retComment = comment.Replace("\t", "  ");

      var startIndex = "///".Length;
      var count = 6;
      if (comment.Length > startIndex + count)
      {
        var check = retComment.Substring(startIndex, count);

        // If at least count spaces, left trim the count spaces.
        if (check == new string(' ', count))
        {
          retComment = "///" + retComment.Substring(startIndex + count);
        }
      }
      return retComment;
    }

    // Parses with different combinations of double or single quotes.
    private string ParseWithQuotes(string text, string beginDelimiter = null,
      string endDelimiter = null)
    {
      string retValue;

      var parser = new LJCParser();
      var required = true;
      while (true)
      {
        parser.StartIndex = 0;
        // If the end delimiter is supplied it must exist in the text.
        retValue = parser.DelimitedString(text, beginDelimiter, endDelimiter
          , required);
        if (NetString.HasValue(retValue))
        {
          break;
        }

        ChangeQuote(beginDelimiter, out string changeStart);
        ChangeQuote(endDelimiter, out string changeEnd);
        parser.StartIndex = 0;
        // If the end delimiter is supplied it must exist in the text.
        retValue = parser.DelimitedString(text, changeStart, changeEnd
          , required);
        break;
      }
      return retValue;
    }

    // Sets the Class include file values: LibName, IncludeFile.
    private bool SetIncludeValues(string[] bothLines, string codeFileSpec
      , ref string memberTag)
    {
      var retValue = true;

      memberTag = null;

      LibName = Path.GetFileNameWithoutExtension(codeFileSpec);
      Comments = new List<string>();

      // Look for "path" with double or single quotes in both lines.
      foreach (string line in bothLines)
      {
        retValue = true;
        var xmlPath = ParseWithQuotes(line, "path=\"", "\"");
        if (!NetString.HasValue(xmlPath))
        {
          retValue = false;
        }
        else
        {
          var parser = new LJCParser();
          memberTag = parser.DelimitedString(line, "members/", "/*");
          break;
        }
      }

      if (retValue)
      {
        // Look for "file" with double or single quotes in both lines.
        foreach (string line in bothLines)
        {
          retValue = true;
          IncludeFile = ParseWithQuotes(line, "file=\"", "\"");
          if (!NetString.HasValue(IncludeFile))
          {
            retValue = false;
          }
          else
          {
            IncludeFile = IncludeFile.Replace("/", "\\");
            break;
          }
        }
      }

      // Add code file path to doc file path to create XML file spec.
      var fileSpecPath = Path.GetDirectoryName(codeFileSpec);
      if (NetString.HasValue(fileSpecPath)
        && NetString.HasValue(IncludeFile))
      {
        IncludeFile = $"{fileSpecPath}\\{IncludeFile}";
      }
      return retValue;
    }

    // Set index to first child of the member tag.
    private string SetToMemberTags(string[] lines, string memberTag, ref int index
      , ref bool isStart)
    {
      var retLine = lines[index];

      while (true)
      {
        if (!NetString.HasValue(memberTag)
          || !isStart)
        {
          break;
        }

        var tag = $"<{memberTag}>";
        //if (!Array.Exists(lines, element => element.Contains(tag)))
        if (!Array.Exists(lines, element => element.Trim() == tag))
        {
          break;
        }

        var line = lines[index];
        if (line.IndexOf($"<{memberTag}>") < 0)
        {
          index++;
          continue;
        }

        // Move to first XML comments.
        index++;
        retLine = lines[index];
        break;
      }
      isStart = false;
      return retLine;
    }
    #endregion
  }
}
