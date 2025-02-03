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
    #region static check Line Functions

    /// <summary>
    /// Checks line for directive and returns the directive object.
    /// </summary>
    public static Directive GetDirective(string line
      , string commentChars = "//")
    {
      string[] values;
      char[] separator = { ' ' };
      Directive retValue = null;

      // Templates directive is in a comment.
      if (line.Trim().StartsWith(commentChars))
      {
        // Template directive starts with "#".
        int index = line.IndexOf('#');
        if (index > -1)
        {
          retValue = new Directive
          {
            CommentChars = commentChars
          };
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
            retValue.Value = values[2];
          }
        }
      }
      return retValue;
    }

    /// <summary>Checks if the line has a directive.</summary>
    public static bool IsDirective(string line
      , string commentChars)
    {
      var retValue = false;

      var directive = GetDirective(line, commentChars);
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
          || lowerName == IfElse
          || lowerName == IfEnd)
        {
          retValue = true;
        }
      }
      return retValue;
    }

    // Checks for a valid IfBegin directive.
    internal static bool IsIfBegin(string line
      , string commentChars)
    {
      bool retValue = false;

      var directive = GetDirective(line, commentChars);
      if (directive != null
        && IfBegin == directive.ID.ToLower())
      {
        retValue = true;
      }
      return retValue;
    }

    // Checks for a valid IfElse directive.
    internal static bool IsIfElse(string line
      , string commentChars)
    {
      bool retValue = false;

      var directive = GetDirective(line, commentChars);
      if (directive != null
        && IfElse == directive.ID.ToLower())
      {
        retValue = true;
      }
      return retValue;
    }

    // Checks for a valid IfEnd directive.
    internal static bool IsIfEnd(string line
      , string commentChars)
    {
      bool retValue = false;

      var directive = GetDirective(line, commentChars);
      if (directive != null
        && IfEnd == directive.ID.ToLower())
      {
        retValue = true;
      }
      return retValue;
    }

    /// <summary>Checks for Begin Section.</summary>
    internal static bool IsSectionBegin(string line
      , string commentChars)
    {
      bool retValue = false;

      var directive = GetDirective(line, commentChars);
      if (directive != null
        && SectionBegin == directive.ID.ToLower())
      {
        retValue = true;
      }
      return retValue;
    }

    // Checks for a valid SectionEnd directive.
    internal static bool IsSectionEnd(string line
      , string commentChars)
    {
      bool retValue = false;

      var directive = GetDirective(line, commentChars);
      if (directive != null
        && SectionEnd == directive.ID.ToLower())
      {
        retValue = true;
      }
      return retValue;
    }

    // Checks for a valid SectionBegin or SectionEnd directive.
    internal static bool IsSectionDirective(string line
      , string commentChars)
    {
      bool retValue = false;

      if (IsSectionBegin(line, commentChars)
        || IsSectionEnd(line, commentChars))
      {
        retValue = true;
      }
      return retValue;
    }
    #endregion

    #region Static check Directive functions.

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
      Value = modifier;
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

    ///// <summary>Sets the Directive properties.</summary>
    //public Directive SetValues(string line, string commentChars)
    //{
    //  Directive retValue = null;

    //  var directive = GetDirective(line, commentChars);
    //  //if (IsDirective(line, commentChars))
    //  if (directive != null)
    //  {
    //    CommentChars = commentChars;
    //    ID = directive.ID;
    //    Name = directive.Name;
    //    Value = directive.Value;
    //    retValue = directive;
    //  }
    //  return retValue;
    //}

    /// <summary>Checks if directive ID = IfBegin.</summary>
    public bool IsIfBegin()
    {
      bool retValue = false;

      if (IfBegin == ID.ToLower())
      {
        retValue = true;
      }
      return retValue;
    }

    /// <summary>Checks if directive ID = IfElse.</summary>
    public bool IsIfElse()
    {
      bool retValue = false;

      if (IfElse == ID.ToLower())
      {
        retValue = true;
      }
      return retValue;
    }

    /// <summary>Checks if directive ID = IfEnd.</summary>
    public bool IsIfEnd()
    {
      bool retValue = false;

      if (IfEnd == ID.ToLower())
      {
        retValue = true;
      }
      return retValue;
    }

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

    /// <summary>Checks if directive ID = SectionBegin.</summary>
    public bool IsSectionBegin()
    {
      bool retValue = false;

      if (SectionBegin == ID.ToLower())
      {
        retValue = true;
      }
      return retValue;
    }

    /// <summary>Checks if directive ID = SectionEnd.</summary>
    public bool IsSectionEnd()
    {
      bool retValue = false;

      if (SectionEnd == ID.ToLower())
      {
        retValue = true;
      }
      return retValue;
    }
    #endregion

    #region Properties

    /// <summary>Gets or sets the line comment start characters.</summary>
    public string CommentChars { get; set; }

    /// <summary>Gets or sets the directive ID.</summary>
    public string ID { get; set; }

    /// <summary>Gets or sets the Name value.</summary>
    public string Name { get; set; }

    /// <summary>Gets or sets the Value property.</summary>
    public string Value { get; set; }
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
