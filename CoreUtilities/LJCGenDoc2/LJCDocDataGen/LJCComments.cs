// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCComments.cs
using LJCDocDataDAL;
using LJCNetCommon;
using System;
using System.Data.SqlClient;
using System.Diagnostics;

namespace LJCDocDataGenLib
{
  // Provides methods to parse XML comment values.
  /// <include file='Doc/LJCComments.xml'
  ///  path='members/LJCComments/*'/>
  public class LJCComments
  {
    #region XML Comment Properties

    // Gets or sets the example code.
    /// <include file='Doc/LJCComments.xml'
    ///  path='members/Code/*'/>
    public string Code { get; set; }

    // Gets or sets the Groups collection.
    /// <include file='Doc/LJCComments.xml'
    ///  path='members/Groups/*'/>
    public LJCDocDataParams Groups { get; set; }

    // Gets or sets the Namespace.
    /// <include file='Doc/LJCComments.xml'
    ///  path='members/Namespace/*'/>
    public string Namespace { get; set; }

    // Gets or sets the Params collection.
    /// <include file='Doc/LJCComments.xml'
    ///  path='members/Params/*'/>
    public LJCDocDataParams Params { get; set; }

    // Gets or sets the parent group name.
    /// <include file='Doc/LJCComments.xml'
    ///  path='members/ParentGroup/*'/>
    public string ParentGroup { get; set; }

    // Gets or sets the remarks.
    /// <include file='Doc/LJCComments.xml'
    ///  path='members/Remarks/*'/>
    public string Remarks { get; set; }

    // Gets or sets the returns text.
    /// <include file='Doc/LJCComments.xml'
    ///  path='members/Returns/*'/>
    public string Returns { get; set; }

    // Gets or sets the summary text.
    /// <include file='Doc/LJCComments.xml'
    ///  path='members/Summary/*'/>
    public string Summary { get; set; }
    #endregion

    #region Properties

    // Gets or sets the current tag name.
    /// <include file='Doc/LJCComments.xml'
    ///  path='members/CurrentTagName/*'/>
    public string CurrentTagName { get; set; }

    // Gets or sets the Code File base Library (project) name.
    /// <include file='Doc/LJCComments.xml'
    ///  path='members/LibName/*'/>
    public string LibName { get; set; }

    // Gets or sets the line and continue line values.
    /// <include file='Doc/LJCComments.xml'
    ///  path='members/BothLines/*'/>
    private string[] BothLines { get; set; }
    #endregion

    #region Private Properties

    //// The Begin comment tags.
    //private string[] BeginTags { get; set; }

    // The Code File specification.
    private string CodeFileSpec { get; set; }

    // The IncludeFile object.
    private LJCInclude Include { get; set; }

    // The IsContinue flag.
    private bool IsNewComment { get; set; }

    // Gets or sets the parser object.
    private LJCParser Parser { get; set; }
    #endregion

    #region Constructor Methods

    // Initializes an object instance.
    /// <include file='Doc/LJCComments.xml'
    ///  path='members/Constructor/*'/>
    public LJCComments()
    {
      // Initialize XML Comment properties.
      ClearComments();

      // Initialize public properties.
      CurrentTagName = null;
      LibName = null;

      // Initialize private properties.
      CodeFileSpec = null;
      Include = new LJCInclude();
      IsNewComment = true;
      Parser = new LJCParser();
      SetCommentTags();
    }
    #endregion

    #region Methods

    // Clears the XML comments.
    /// <include file='Doc/LJCComments.xml'
    ///  path='members/ClearComments/*'/>
    public void ClearComments()
    {
      Code = null;
      Groups = new LJCDocDataParams();
      Params = new LJCDocDataParams();
      ParentGroup = null;
      Remarks = null;
      Returns = null;
      Summary = null;
    }

    // Sets the XML comment value.
    /// <include file='Doc/LJCComments.xml'
    ///  path='members/SetComment/*'/>
    public void SetComment(string[] bothLines, string codeFileSpec = null)
    {
      BothLines = bothLines;
      var trimLine = BothLines[0].Trim();

      while (true)
      {
        if (trimLine.StartsWith("namespace"))
        {
          var tokens = NetString.Split(trimLine, " ");
          if (2 == tokens.Length)
          {
            Namespace = tokens[1];
          }
          break;
        }

        // Line is not an XML comment.
        if (!trimLine.StartsWith("///"))
        {
          break;
        }

        if (codeFileSpec != null)
        {
          CodeFileSpec = codeFileSpec;
        }

        // Comment can span multiple lines.
        if (IsNewComment)
        {
          CurrentTagName = Parser.ValidBeginTagName(trimLine);
          if (NetString.HasValue(CurrentTagName))
          {
            ClearSingleTagComment();
            var comment = GetComment(BothLines);
            SaveComment(comment);
            IsNewComment = Parser.HasValidEndTag(trimLine);
          }
        }
        else
        {
          var comment = GetComment(BothLines);
          SaveComment(comment);
          IsNewComment = Parser.HasValidEndTag(trimLine);
          if (trimLine.EndsWith("/>"))
          {
            IsNewComment = true;
          }
        }
        break;
      }
    }

    // Gets the comment from a line.
    /// <include file='Doc/LJCComments.xml'
    ///  path='members/GetComment/*'/>
    private string GetComment(string[] bothLines)
    {
      string retComment = null;

      var line = bothLines[0];

      // Get using CurrentTagName.
      var shortBeginTag = Parser.NameToShortBeginTag(CurrentTagName);
      string beginTag = null;
      if (NetString.HasValue(shortBeginTag))
      {
        beginTag = $"{shortBeginTag}>";
      }
      var endTag = Parser.EndTag(line);

      var beginIndex = NetString.IndexOf(line, shortBeginTag);
      if (beginIndex < 0)
      {
        // No BeginTag so set parse for start of comment.
        beginTag = "/// ";
      }

      var isSimpleComment = true;
      if ("<include" == shortBeginTag)
      {
        isSimpleComment = false;
        Include.SetValidTagNames(Parser.ValidTagNames);
        Include.SetComments(bothLines, CodeFileSpec);
        AddIncludeComments();

        // Indicate that Include comment processing is done.
        CurrentTagName = null;
      }

      // Save group item.
      if ("<group" == shortBeginTag)
      {
        isSimpleComment = false;
        var groupComment = new LJCDocDataParam();
        var group = groupComment.GetParam(line);
        Groups.Add(group);
      }

      // Save param item.
      if ("<param" == shortBeginTag)
      {
        isSimpleComment = false;
        var paramComment = new LJCDocDataParam();
        var param = paramComment.GetParam(line);
        Params.Add(param);
      }

      if (isSimpleComment)
      {
        var parser = new LJCParser();
        retComment = parser.DelimitedString(line, beginTag, endTag);
        if (NetString.HasValue(retComment))
        {
          if (CurrentTagName != "code")
          {
            retComment = retComment.TrimStart();
          }
          if (Parser.HasValidEndTag(line))
          {
            retComment = retComment.TrimEnd();
          }
        }
      }
      return retComment;
    }
    #endregion

    #region Private Comment Methods

    // Adds the include comment lines.
    private void AddIncludeComments()
    {
      foreach (var comment in Include.Comments)
      {
        string[] both = new string[]
        {
            comment,
            null,
        };
        SetComment(both);
      }

      if (Code != null)
      {
        Code = Code.TrimEnd();
      }
    }

    // Clear comment for current or supplied tag name.
    private void ClearSingleTagComment(string tagName = null)
    {
      if (null == tagName)
      {
        tagName = CurrentTagName;
      }

      // Do not clear if multiple tags are allowed.
      if (NetString.HasValue(tagName)
        && "param" != tagName
        && "group" != tagName)
      {
        switch (tagName.ToLower())
        {
          case "code":
            Code = "";
            break;

          case "group":
            Groups = new LJCDocDataParams();
            break;

          case "include":
            break;

          case "param":
            Params = new LJCDocDataParams();
            break;

          case "parentgroup":
            ParentGroup = null;
            break;

          case "remarks":
            Remarks = "";
            break;

          case "returns":
            Returns = "";
            break;

          case "summary":
            Summary = "";
            break;
        }
      }
    }

    // Saves the comment for the current comment tag.
    private void SaveComment(string comment)
    {
      if ("code" == CurrentTagName
        || NetString.HasValue(comment))
      {
        switch (CurrentTagName)
        {
          case "code":
            if (Code != "")
            {
              Code += "\r\n";
            }
            Code += NetCommon.XmlEncode(comment);
            break;

          case "parentGroup":
            ParentGroup = NetCommon.XmlEncode(comment);
            break;

          case "remarks":
            Remarks += NetCommon.XmlEncode(comment);
            break;

          case "returns":
            Returns += NetCommon.XmlEncode(comment);
            break;

          case "summary":
            Summary += NetCommon.XmlEncode(comment);
            break;
        }
      }
    }
    #endregion

    #region Private Tag Methods

    // Sets the comment tag values.
    private void SetCommentTags()
    {
      Parser.ValidTagNames = new string[] {
        "code",
        "group",
        "include",
        "param",
        "parentGroup",
        "remarks",
        "returns",
        "summary",
      };
    }
    #endregion
  }
}