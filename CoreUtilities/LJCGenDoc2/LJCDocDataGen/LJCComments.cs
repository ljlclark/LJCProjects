// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCComments.cs
using LJCDocDataDAL;
using LJCNetCommon;
using System.Data.SqlClient;
using System.Diagnostics;

namespace LJCDocDataGenLib
{
  // Provides methods to parse XML comment values.
  /// <include file='Doc/LJCComments.xml'
  ///  path='members/LJCComments/*'/>
  public class LJCComments
  {
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
      IsNewComment = false;
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
          CurrentTagName = GetBeginTagName(trimLine);
          if (NetString.HasValue(CurrentTagName))
          {
            ClearSingleTagComment();
            var comment = GetComment(BothLines);
            SaveComment(comment);
            IsNewComment = HasEndTag(trimLine);
          }
        }
        else
        {
          var comment = GetComment(BothLines);
          SaveComment(comment);
          IsNewComment = !HasEndTag(trimLine);
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
      var lowerCaseLine = line.ToLower();

      // Get using CurrentTagName.
      var beginTag = BeginTag();
      var endTag = EndTag();

      var beginIndex = -1;
      if (beginTag != null)
      {
        beginIndex = lowerCaseLine.IndexOf(beginTag);
      }
      if (beginIndex < 0)
      {
        // No BeginTag so set parse for start of comment.
        beginTag = "///";
      }

      var isSimpleComment = true;
      if ("<include" == beginTag)
      {
        isSimpleComment = false;
        Include.SetComments(bothLines, CodeFileSpec);
        AddIncludeComments();

        // Indicate that Include comment processing is done.
        CurrentTagName = null;
      }

      // Save group item.
      if ("<group" == beginTag)
      {
        isSimpleComment = false;
        var groupComment = new LJCDocDataParam();
        var group = groupComment.GetParam(line);
        Groups.Add(group);
      }

      // Save param item.
      if ("<param" == beginTag)
      {
        isSimpleComment = false;
        var paramComment = new LJCDocDataParam();
        var param = paramComment.GetParam(line);
        Params.Add(param);
      }

      if (isSimpleComment)
      {
        var parser = new LJCParser();
        retComment = parser.DelimitedString(lowerCaseLine, beginTag, endTag);
        if (retComment != null)
        {
          retComment = retComment.TrimStart();
          if (HasEndTag(line))
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
      if (NetString.HasValue(comment))
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

          case "parentgroup":
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

    // Get the starting begin tag for the tag name.
    private string BeginTag(string tagName = null)
    {
      string retTag = null;

      if (null == tagName)
      {
        tagName = CurrentTagName;
      }

      if (NetString.HasValue(tagName))
      {
        switch (tagName.Trim().ToLower())
        {
          case "code":
            retTag = "<code>";
            break;
          case "group":
            retTag = "<group";
            break;
          case "include":
            retTag = "<include";
            break;
          case "parentgroup":
            retTag = "<parentgroup>";
            break;
          case "param":
            retTag = "<param";
            break;
          case "remarks":
            retTag = "<remarks>";
            break;
          case "returns":
            retTag = "<returns>";
            break;
          case "summary":
            retTag = "<summary>";
            break;
        }
      }
      return retTag;
    }

    // Gets the tag name for the starting begin tag.
    private string BeginTagName(string beginTag)
    {
      string retName = null;

      switch (beginTag.Trim().ToLower())
      {
        case "<code>":
          retName = "code";
          break;
        case "<group":
          retName = "group";
          break;
        case "<include":
          retName = "include";
          break;
        case "<parentgroup>":
          retName = "parentgroup";
          break;
        case "<param":
          retName = "param";
          break;
        case "<remarks>":
          retName = "remarks";
          break;
        case "<returns>":
          retName = "returns";
          break;
        case "<summary>":
          retName = "summary";
          break;
      }
      return retName;
    }

    // Gets the end tag for the tag name.
    private string EndTag(string tagName = null)
    {
      string retTag = null;

      if (null == tagName)
      {
        tagName = CurrentTagName;
      }

      if (tagName != null)
      {
        switch (tagName.Trim().ToLower())
        {
          case "code":
            retTag = "</code>";
            break;
          case "group":
            retTag = "</group>";
            break;
          case "parentgroup":
            retTag = "</parentgroup>";
            break;
          case "param":
            retTag = "</param>";
            break;
          case "remarks":
            retTag = "</remarks>";
            break;
          case "returns":
            retTag = "</returns>";
            break;
          case "summary":
            retTag = "</summary>";
            break;
        }
      }
      return retTag;
    }

    // Gets the tag name for the end tag.
    private string EndTagName(string endTag)
    {
      string retName = null;

      switch (endTag.Trim().ToLower())
      {
        case "</code>":
          retName = "code";
          break;
        case "</group>":
          retName = "group";
          break;
        case "</parentgroup>":
          retName = "parentgroup";
          break;
        case "</param>":
          retName = "param";
          break;
        case "</remarks>":
          retName = "remarks";
          break;
        case "</returns>":
          retName = "returns";
          break;
        case "</summary>":
          retName = "summary";
          break;
      }
      return retName;
    }

    // Indicates if the lines has a current EndTag.
    private bool HasEndTag(string line)
    {
      var retValue = false;

      var endTag = EndTag();
      if (endTag != null
        && line.ToLower().LastIndexOf(endTag) >= 0)
      {
        retValue = true;
      }
      return retValue;
    }

    /// Gets the tag name if the line contains a begin tag.
    private string GetBeginTagName(string line)
    {
      string retName = null;

      var lowerCaseLine = line.ToLower();
      foreach (string beginTag in BeginTags)
      {
        if (lowerCaseLine.IndexOf(beginTag) >= 0)
        {
          retName = BeginTagName(beginTag);
          break;
        }
      }
      return retName;
    }

    // Gets the tag name if the line ontains an end tag.
    private string LineEndTagName(string line)
    {
      string retName = null;

      var lowerCaseLine = line.ToLower();
      foreach (string endTag in EndTags)
      {
        if (lowerCaseLine.IndexOf(endTag) >= 0)
        {
          retName = EndTagName(endTag);
          break;
        }
      }
      return retName;
    }

    // Sets the comment tag values.
    private void SetCommentTags()
    {
      BeginTags = new string[]
      {
        "<code>",
        "<group",
        "<include",
        "<parentgroup>",
        "<param",
        "<remarks>",
        "<returns>",
        "<summary>",
      };

      EndTags = new string[]
      {
        "</code>",
        "</group>",
        "</parentgroup>",
        "</param>",
        "</remarks>",
        "</returns>",
        "</summary>",
      };
    }
    #endregion

    #region XML Comment Properties

    /// <summary>The example Code.</summary>
    public string Code { get; set; }

    /// <summary>The groups.</summary>
    public LJCDocDataParams Groups { get; set; }

    /// <summary>The Param comments.</summary>
    public LJCDocDataParams Params { get; set; }

    /// <summary>The Parent group name.</summary>
    public string ParentGroup { get; set; }

    /// <summary>The Remark comment.</summary>
    public string Remarks { get; set; }

    /// <summary>The Returns comment.</summary>
    public string Returns { get; set; }

    /// <summary>The Summary comment.</summary>
    public string Summary { get; set; }
    #endregion

    #region Public Properties

    /// <summary>The current tag name.</summary>
    public string CurrentTagName { get; set; }

    /// <summary>The Code File base (Library) name.</summary>
    public string LibName { get; set; }

    // The Line and ContinueLine values.
    private string[] BothLines { get; set; }
    #endregion

    #region Private Properties

    // The Begin comment tags.
    private string[] BeginTags { get; set; }

    // The Code File specification.
    private string CodeFileSpec { get; set; }

    // The End comment tags.
    private string[] EndTags { get; set; }

    // The IncludeFile object.
    private LJCInclude Include { get; set; }

    // The IsContinue flag.
    private bool IsNewComment { get; set; }
    #endregion
  }
}