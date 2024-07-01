// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// Directive.cs

using System;

namespace LJCGenTextLib
{
  // Represents a Directive item.
  /// <include path='items/Directive/*' file='Doc/ProjectGenTextLib.xml'/>
  public class Directive
  {
    #region static Functions

    /// <summary>
    /// Checks line for directive and returns the directive object.
    /// </summary>
    public static Directive GetDirective(string line
      , string commentStart = "//")
    {
      string[] values;
      char[] separator = { ' ' };
      Directive retValue = null;

      // Templates directive is in a comment.
      if (line.Trim().StartsWith(commentStart))
      {
        // Template directive starts with "#".
        int index = line.IndexOf('#');
        if (index > -1)
        {
          retValue = new Directive();
          retValue.CommentChars = commentStart;
          values = line.Substring(index).Split(separator
            , StringSplitOptions.RemoveEmptyEntries);
          if (values.Length > 0)
          {
            // The directive identifier.
            retValue.ID = values[0];
          }
          if (values.Length > 1)
          {
            retValue.Name = values[1];
          }
          if (values.Length > 2)
          {
            retValue.Modifier = values[2];
          }
        }
      }
      return retValue;
    }

    /// <summary>Checks if the line has a directive.</summary>
    public static bool IsDirective(string line, string commentStart = "//")
    {
      var retValue = false;

      var directive = GetDirective(line, commentStart);
      if (directive != null)
      {
        var lowerName = directive.ID.ToLower();
        if (lowerName == "#commentchars"
          || lowerName == "#placeholderbegin"
          || lowerName == "#placeholderend"
          || lowerName == SectionBegin
          || lowerName == SectionEnd
          || lowerName == ValueDirective
          || lowerName == IfBegin
          || lowerName == IfEnd)
        {
          retValue = true;
        }
      }
      return retValue;
    }

    // Checks for a valid IfBegin directive.
    internal static bool IsIfBegin(string line)
    {
      bool retValue = false;

      var directive = GetDirective(line);
      if (directive != null
        && IfBegin == directive.ID.ToLower())
      {
        retValue = true;
      }
      return retValue;
    }

    // Checks for a valid IfElse directive.
    internal static bool IsIfElse(string line)
    {
      bool retValue = false;

      var directive = GetDirective(line);
      if (directive != null
        && IfElse == directive.ID.ToLower())
      {
        retValue = true;
      }
      return retValue;
    }

    // Checks for a valid IfEnd directive.
    internal static bool IsIfEnd(string line)
    {
      bool retValue = false;

      var directive = GetDirective(line);
      if (directive != null
        && IfEnd == directive.ID.ToLower())
      {
        retValue = true;
      }
      return retValue;
    }

    // Checks the Name value.
    internal static bool IsName(Directive directive, string name
      , bool caseInsensitive = true)
    {
      bool retValue = false;

      if (directive != null)
      {
        retValue = directive.IsName(name, caseInsensitive);
      }
      return retValue;
    }

    /// <summary>Checks for Begin Section.</summary>
    public static bool IsSectionBegin(string line)
    {
      bool retValue = false;

      var directive = GetDirective(line);
      if (directive != null
        && SectionBegin == directive.ID.ToLower())
      {
        retValue = true;
      }
      return retValue;
    }

    // Checks for a valid SectionEnd directive.
    internal static bool IsSectionEnd(string line)
    {
      bool retValue = false;

      var directive = GetDirective(line);
      if (directive != null
        && SectionEnd == directive.ID.ToLower())
      {
        retValue = true;
      }
      return retValue;
    }

    // Checks for a valid SectionBegin or SectionEnd directive.
    internal static bool IsSectionDirective(string line)
    {
      bool retValue = false;

      if (IsSectionBegin(line)
        || IsSectionEnd(line))
      {
        retValue = true;
      }
      return retValue;
    }

    // Check if a subsection.
    internal static bool IsSubsection(Directive directive)
    {
      bool retValue = false;

      if (directive != null
        && directive.IsName("Subsection"))
      {
        retValue = true;
      }
      return retValue;
    }

    // Checks for a valid Value directive.
    internal static bool IsValue(Directive directive)
    {
      bool retValue = false;

      if (directive != null
        && ValueDirective == directive.ID.ToLower())
      {
        retValue = true;
      }
      return retValue;
    }
    #endregion

    #region Constructors

    //Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../LJCGenDoc/Common/Data.xml'/>
    public Directive()
    {
      CommentChars = "//";
    }

    // Initializes an object instance.
    /// <include path='items/DirectiveC/*' file='Doc/Directive.xml'/>
    public Directive(string id, string name, string modifier = "")
    {
      ID = id;
      Name = name;
      Modifier = modifier;
      CommentChars = "//";
    }
    #endregion

    #region Data Methods

    // Creates and returns a clone of this object.
    /// <include path='items/Clone/*' file='../../LJCGenDoc/Common/Data.xml'/>
    public Directive Clone()
    {
      Directive retValue = MemberwiseClone() as Directive;
      return retValue;
    }

    // The object string identifier.
    /// <include path='items/ToString/*' file='../../LJCGenDoc/Common/Data.xml'/>
    public override string ToString()
    {
      return $"{ID},{Name}";
    }
    #endregion

    #region Other Methods

    /// <summary>
    /// Checks the Name value.
    /// </summary>
    /// <param name="name">The Name value.</param>
    /// <param name="caseInsensitive">Use insensitive compare.</param>
    /// <returns>true if equal; otherwise, false.</returns>
    public bool IsName(string name, bool caseInsensitive = true)
    {
      bool retValue = false;

      if (caseInsensitive)
      {
        if (Name.ToLower() == name.ToLower())
        {
          retValue = true;
        }
      }
      else
      {
        if (Name == name)
        {
          retValue = true;
        }
      }
      return retValue;
    }
    #endregion

    #region Properties

    /// <summary>Gets or sets the directive ID.</summary>
    public string ID { get; set; }

    /// <summary>Gets or sets the line comment start characters.</summary>
    public string CommentChars { get; set; }

    /// <summary>Gets or sets the name value.</summary>
    public string Name { get; set; }

    /// <summary>Gets or sets the modifier value.</summary>
    public string Modifier { get; set; }
    #endregion

    #region Class Data

    internal const string IfBegin = "#ifbegin";
    internal const string IfElse = "#ifelse";
    internal const string IfEnd = "#ifend";
    internal const string SectionBegin = "#sectionbegin";
    internal const string SectionEnd = "#sectionend";
    internal const string ValueDirective = "#value";
    #endregion
  }
}
