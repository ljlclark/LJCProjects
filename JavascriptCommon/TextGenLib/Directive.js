// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// Directive.js

// Represents a Directive object.
class Directive
{
  // Checks line for directive and returns the directive object.
  // Returns directive if line is a directive.
  static GetDirective(line, commentChars = "//")
  {
    let retValue = null;

    // Directive Layout = "// #Directive Name [DataType]"
    // Checks for #CommentChars as property may not be set.
    if (line != null
      && (line.trim().startsWith(commentChars)
        || line.toLowerCase().includes("#commentchars")))
    {
      let tokens = line.trim().split(/\s+/g);
      if (tokens.length > 2
        && tokens[1].startsWith("#"))
      {
        let id = tokens[1];
        let name = tokens[2];
        let value = null;
        if (tokens.length > 3)
        {
          value = tokens[3];
        }
        retValue = new Directive(id, name, value);
      }
    }
    return retValue;
  }

  // Checks if the line has a directive.
  static IsDirective(line, commentChars = "//")
  {
    let retValue = false;

    let directive = Directive.GetDirective(line, commentChars);
    if (directive != null)
    {
      let lowerName = directive.ID.toLowerCase();
      if (lowerName == "#commentchars"
        || lowerName == "#placeholderbegin"
        || lowerName == "#placeholderend"
        || lowerName == "#sectionbegin"
        || lowerName == "#sectionend"
        || lowerName == "#value"
        || lowerName == "#ifbegin"
        || lowerName == "#ifend")
      {
        retValue = true;
      }
    }
    return retValue;
  }

  // Checks if the line is a SectionEnd.  // 
  static IsIfBegin(line)
  {
    let retValue = false;

    // Directive Layout = "// #SectionEnd Name"
    let directive = Directive.GetDirective(line, this.CommentChars);
    if (directive != null
      && "#ifbegin" == directive.ID.toLowerCase())
    {
      retValue = true;
    }
    return retValue;
  }

  // Checks if the line is a SectionEnd.  // 
  static IsIfEnd(line)
  {
    let retValue = false;

    // Directive Layout = "// #SectionEnd Name"
    let directive = Directive.GetDirective(line, this.CommentChars);
    if (directive != null
      && "#ifend" == directive.ID.toLowerCase())
    {
      retValue = true;
    }
    return retValue;
  }

  // Checks if the line is a SectionBegin.
  static IsSectionBegin(line)
  {
    let retValue = false;

    // Directive Layout = "// #SectionBegin Name"
    let directive = Directive.GetDirective(line, this.CommentChars);
    if (directive != null
      && "#sectionbegin" == directive.ID.toLowerCase())
    {
      retValue = true;
    }
    return retValue;
  }

  // Checks if the line is a SectionEnd.  // 
  static IsSectionEnd(line)
  {
    let retValue = false;

    // Directive Layout = "// #SectionEnd Name"
    let directive = Directive.GetDirective(line, this.CommentChars);
    if (directive != null
      && "#sectionend" == directive.ID.toLowerCase())
    {
      retValue = true;
    }
    return retValue;
  }

  // The Constructor method.
  constructor(id, name, value = null)
  {
    this.ID = id;
    this.Name = name;
    this.Value = value;
  }
}
