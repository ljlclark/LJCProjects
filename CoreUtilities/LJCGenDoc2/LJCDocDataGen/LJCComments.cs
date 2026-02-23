// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCComments.cs
using LJCDocDataDAL;
using LJCNetCommon;

namespace LJCDocDataGenLib
{
  // Provides methods to parse XML comment values.
  /// <include path="members/LJCComments/*" file="Doc/LJCComments.xml"/>
  public class LJCComments
  {
    #region Constructor Methods

    // Initializes an object instance.
    /// <include path="members/Constructor/*" file="Doc/LJCComments.xml"/>
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
      IsContinue = false;
      SetCommentTags();
    }
    #endregion

    #region Public Methods

    // Clears the XML comments.
    /// <include path="members/ClearComments/*" file="Doc/LJCComments.xml"/>
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
    /// <include path="members/SetComment/*" file="Doc/LJCComments.xml"/>
    public void SetComment(string line, string codeFileSpec = null)
    {
      if (codeFileSpec != null)
      {
        CodeFileSpec = codeFileSpec;
      }

      if (!IsContinue)
      {
        // New comment.
        CurrentTagName = LineBeginTagName(line);

        // Do not clear if possible multiple tags.
        if ("param" != CurrentTagName
          && "group" != CurrentTagName)
        {
          ClearComment();
        }

        var comment = GetComment(line);
        if (CurrentTagName != null)
        {
          SaveComment(comment);
          if (!HasCurrentEndTag(line))
          {
            // No end tag so start Continue comment.
            IsContinue = true;
          }
        }
      }
      else
      {
        // Continue comment.
        if (HasCurrentEndTag(line))
        {
          // Has end tag so end Continue comment.
          IsContinue = false;
        }
        var comment = GetComment(line);
        SaveComment(comment);
      }
    }
    #endregion

    #region Private Comment Methods

    // Clear comment for supplied tag name.
    private void ClearComment(string tagName = null)
    {
      if (null == tagName)
      {
        tagName = CurrentTagName;
      }

      if (tagName != null)
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

    // Gets the comment from a line.
    /// <include path="members/GetComment/*" file="Doc/LJCComments.xml"/>
    private string GetComment(string line)
    {
      string retComment = null;

      // Get using CurrentTagName.
      var beginTag = BeginTag();
      var endTag = EndTag();

      var beginIndex = -1;
      if (beginTag != null)
      {
        beginIndex = line.IndexOf(beginTag);
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
        Include.SetComments(line, CodeFileSpec);

        // Process the include comment lines through SetComment().
        foreach (var comment in Include.Comments)
        {
          SetComment(comment);
        }

        // Remove extra line from code.
        if (Code != null)
        {
          Code = Code.TrimEnd();
        }

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
        var trimEnd = true;
        if (!HasCurrentEndTag(line))
        {
          // No EndTag so do not remove newline.
          trimEnd = false;
        }
        var parser = new LJCParser();
        retComment = parser.DelimitedString(line, beginTag, endTag);
        if (trimEnd)
        {
          retComment = retComment.TrimEnd();
        }
      }
      return retComment;
    }

    // Saves the comment for the current comment tag.
    private void SaveComment(string comment)
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

      if (tagName != null)
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
            retTag = "</group?";
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
    private bool HasCurrentEndTag(string line)
    {
      var retValue = false;

      var endTag = EndTag();
      if (endTag != null
        && line.LastIndexOf(endTag) >= 0)
      {
        retValue = true;
      }
      return retValue;
    }

    /// Gets the tag name if the line contains a begin tag.
    private string LineBeginTagName(string line)
    {
      string retName = null;

      foreach (string beginTag in BeginTags)
      {
        if (line.ToLower().IndexOf(beginTag) >= 0)
        {
          retName = BeginTagName(beginTag);
        }
      }
      return retName;
    }

    // Gets the tag name if the line ontains an end tag.
    private string LineEndTagName(string line)
    {
      string retName = null;

      foreach (string endTag in EndTags)
      {
        if (line.ToLower().IndexOf(endTag) >= 0)
        {
          retName = EndTagName(endTag);
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
    private bool IsContinue { get; set; }
    #endregion
  }
}