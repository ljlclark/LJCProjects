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

    /// <summary>Checks the line for a directive and returns the directive name and value.</summary>
    public static Directive GetDirective(string line)
    {
      string[] values;
      char[] separator = { ' ' };
      Directive retValue = null;

      // Templates directive is in a comment.
      if (line.Trim().StartsWith("//")
        || line.Trim().StartsWith("<!--"))
      {
        // Template directive starts with "#".
        int index = line.IndexOf('#');
        if (index > -1)
        {
          retValue = new Directive();
          values = line.Substring(index).Split(separator, StringSplitOptions.RemoveEmptyEntries);
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

    /// <summary>Checks for Begin Section.</summary>
    public static bool IsBeginSection(Directive directive)
    {
      bool retValue = false;

      if (directive != null
        && BeginSection == directive.ID.ToLower())
      {
        retValue = true;
      }
      return retValue;
    }

    // Checks for a valid SectionEnd directive.
    internal static bool IsEndSection(Directive directive)
    {
      bool retValue = false;

      if (directive != null
        && EndSection == directive.ID.ToLower())
      {
        retValue = true;
      }
      return retValue;
    }

    // Checks for a valid SectionBegin or SectionEnd directive.
    internal static bool IsSectionDirective(Directive directive)
    {
      bool retValue = false;

      if (directive != null
        && (IsBeginSection(directive)
        || IsEndSection(directive)))
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

    // Checks for a valid IfBegin directive.
    internal static bool IsIfBegin(Directive directive)
    {
      bool retValue = false;

      if (directive != null
        && BeginIf == directive.ID.ToLower())
      {
        retValue = true;
      }
      return retValue;
    }

    // Checks for a valid IfElse directive.
    internal static bool IsIfElse(Directive directive)
    {
      bool retValue = false;

      if (directive != null
        && ElseIf == directive.ID.ToLower())
      {
        retValue = true;
      }
      return retValue;
    }

    // Checks for a valid IfEnd directive.
    internal static bool IsIfEnd(Directive directive)
    {
      bool retValue = false;

      if (directive != null
        && EndIf == directive.ID.ToLower())
      {
        retValue = true;
      }
      return retValue;
    }
    #endregion

    #region Constructors

    //Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public Directive()
    {
    }

    // Initializes an object instance.
    /// <include path='items/DirectiveC/*' file='Doc/Directive.xml'/>
    public Directive(string id, string name, string modifier = "")
    {
      ID = id;
      Name = name;
      Modifier = modifier;
    }
    #endregion

    #region Public Methods

    // Creates and returns a clone of this object.
    /// <include path='items/Clone/*' file='../../LJCDocLib/Common/Data.xml'/>
    public Directive Clone()
    {
      Directive retValue = MemberwiseClone() as Directive;
      return retValue;
    }

    // The object string identifier.
    /// <include path='items/ToString/*' file='../../LJCDocLib/Common/Data.xml'/>
    public override string ToString()
    {
      return $"{ID},{Name}";
    }
    #endregion

    #region Properties

    /// <summary>Gets or sets the directive ID.</summary>
    public string ID { get; set; }

    /// <summary>Gets or sets the name value.</summary>
    public string Name { get; set; }

    /// <summary>Gets or sets the modifier value.</summary>
    public string Modifier { get; set; }
    #endregion

    #region Class Data

    internal const string BeginSection = "#sectionbegin";
    internal const string EndSection = "#sectionend";
    internal const string ValueDirective = "#value";
    internal const string BeginIf = "#ifbegin";
    internal const string ElseIf = "#ifelse";
    internal const string EndIf = "#ifend";
    #endregion
  }
}
