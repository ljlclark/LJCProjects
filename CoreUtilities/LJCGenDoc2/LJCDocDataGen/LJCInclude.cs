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
  // Provides methods to retrieve the Include file XML comment values.
  /// <include file='Doc/LJCInclude.xml'
  ///  path='members/LJCInclude/*'/>
  public class LJCInclude
  {
    #region Properties

    // Gets the include XML comments.
    /// <include file='Doc/LJCInclude.xml'
    ///  path='members/CurrentTag/*'/>
    public List<string> Comments { get; private set; }

    // Gets the current tag name.
    /// <include file='Doc/LJCInclude.xml'
    ///  path='members/CurrentTagName/*'/>
    public string CurrentTagName { get; private set; }

    // Gets the current tag.
    /// <include file='Doc/LJCInclude.xml'
    ///  path='members/CurrentTag/*'/>
    public string CurrentTag { get; private set; }

    // Gets the project (Lib) name.
    /// <include file='Doc/LJCInclude.xml'
    ///  path='members/LibName/*'/>
    public string LibName { get; private set; }

    // Gets the include XML file name.
    /// <include file='Doc/LJCInclude.xml'
    ///  path='members/IncludeFile/*'/>
    public string IncludeFile { get; private set; }

    // Gets or sets the parser object.
    private LJCParser Parser { get; set; }
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
      Parser = new LJCParser();
    }

    // Sets the parser valid tag names.
    /// <include file='Doc/LJCInclude.xml'
    ///  path='members/SetValidTagNames/*'/>
    public void SetValidTagNames(string[] validTagNames)
    {
      Parser.ValidTagNames = validTagNames;
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
        var lines = File.ReadAllLines(IncludeFile);

        // Start processing at first child of the member tag.
        var startIndex = MemberTagsIndex(lines, memberTag);

        var isNewComment = true;
        for (int index = startIndex; index < lines.Length; index++)
        {
          var line = lines[index];
          var trimLine = line.Trim();

          if (trimLine == $"</{memberTag}>")
          {
            // End of member comments.
            break;
          }

          // Get potentially included end tag.
          var endTag = Parser.ValidEndTag(trimLine);

          // Comment can span multiple lines.
          if (isNewComment)
          {
            CurrentTag = Parser.ValidBeginTag(trimLine);
            if (NetString.HasValue(CurrentTag))
            {
              if ("<example>" == CurrentTag)
              {
                var currentTag = CurrentTag;
                trimLine = AdvanceToCode(lines, ref currentTag, ref index);
                CurrentTag = currentTag;
              }
              CurrentTagName = Parser.ValidBeginTagName(trimLine);
              SetComment(trimLine);
              isNewComment = NetString.HasValue(endTag);
            }
          }
          else
          {
            SetComment(trimLine);
            isNewComment = NetString.HasValue(endTag);
            if ("</code>" == endTag)
            {
              AdvanceAfterExample(lines, endTag, ref index);
            }
          }
        }
      }
    }

    // Gets the comment for the provided line.
    private string SetComment(string line)
    {
      string retComment;

      var beginTag = Parser.ValidBeginTag(line);
      var endTag = Parser.ValidEndTag(line);

      while (true)
      {
        // If endTag is missing, returns comment to end of text line.
        var commentText = Parser.DelimitedString(line, beginTag, endTag);
        if ("code" == CurrentTagName)
        {
          if (null == commentText)
          {
            commentText = "";
          }
          if (endTag != null)
          {
            commentText = commentText.TrimEnd();
          }
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

    // Advances to after the <example> tag.
    private void AdvanceAfterExample(string[] lines, string endTag
      , ref int index)
    {
      if ("</code>" == endTag)
      {
        string line = lines[index];
        while (!Parser.IsEndTagName(line, "example"))
        {
          index++;
          line = lines[index];
        }
      }
    }

    // Advance to the <code> tag.
    private string AdvanceToCode(string[] lines, ref string currentTag
      , ref int index)
    {
      string retLine = lines[index];

      if ("<example>" == currentTag
        && ToBeginCode(lines, ref index))
      {
        var line = lines[index];
        if (Parser.HasValidBeginTagName(line, out string tempTag))
        {
          retLine = line;
          currentTag = tempTag;
        }
      }
      return retLine;
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

    // Set index to first child of the member tag.
    private int MemberTagsIndex(string[] lines, string memberTag)
    {
      var retIndex = 0;

      var process = NetString.HasValue(memberTag);
      var tag = $"<{memberTag}>";
      //if (!Array.Exists(lines, element => element.Contains(tag)))
      if (!Array.Exists(lines, element => element.Trim() == tag))
      {
        process = false;
      }

      while (process)
      {
        if (retIndex >= lines.Length)
        {
          // End of lines.
          break;
        }

        var line = lines[retIndex];
        if (line.IndexOf($"<{memberTag}>") < 0)
        {
          // Tag was not found so continue.
          retIndex++;
          continue;
        }

        // Move to first XML comments.
        if (retIndex < lines.Length)
        {
          retIndex++;
        }
        break;
      }
      return retIndex;
    }

    // Gets the next trimmed line.
    private string NextTrimLine(string[] lines, ref int index)
    {
      string retLine = null;

      index++;
      if (index <= lines.Length)
      {
        retLine = lines[index];
        retLine = retLine.Trim();
      }
      return retLine;
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
        var xmlPath = Parser.ParseWithQuotes(line, "path=\"", "\"");
        if (!NetString.HasValue(xmlPath))
        {
          retValue = false;
        }
        else
        {
          memberTag = Parser.DelimitedString(line, "members/", "/*");
          break;
        }
      }

      if (retValue)
      {
        // Look for "file" with double or single quotes in both lines.
        foreach (string line in bothLines)
        {
          retValue = true;
          IncludeFile = Parser.ParseWithQuotes(line, "file=\"", "\"");
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

    // Finds the beginning of the <code> comment.
    private bool ToBeginCode(string[] lines, ref int index)
    {
      bool retValue = false;

      var tempIndex = index;
      var line = lines[tempIndex];
      if (Parser.IsBeginTagName(line, "example"))
      {
        while (true)
        {
          string trimLine = NextTrimLine(lines, ref tempIndex);

          // Keep searching until begin "code", end "code", end "example" or
          // end of file.
          if (!Parser.IsBeginTagName(trimLine, "code")
            && !Parser.IsEndTagName(trimLine, "code")
            && !Parser.IsEndTagName(trimLine, "example"))
          {
            if (null == trimLine)
            {
              break;
            }
            continue;
          }

          Parser.HasValidBeginTagName(trimLine, out string tagName);
          if ("code" == tagName)
          {
            retValue = true;
            index = tempIndex;
            break;
          }

          if (Parser.HasValidEndTagName(trimLine, out tagName))
          {
            if ("code" == tagName
              || "example" == tagName)
            {
              index = tempIndex;
              break;
            }
          }
        }
      }
      return retValue;
    }
    #endregion
  }
}
