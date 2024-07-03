// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// TextGenLib.js
// <script src="../LJCCommon.js"></script>
// <script src="Script/RepeatItems.js"></script>
// <script src="Script/Replacements.js"></script>
// <script src="Script/Sections.js"></script>

// Generate output text from a template and data.
class TextGenLib
{
  // Checks if there are items.
  static HasItems(items)
  {
    let retValue = false;

    if (items != null
      && items.Count() > 0)
    {
      retValue = true;
    }
    return retValue;
  }

  // Private Properties
  #ActiveReplacements = [];
  #Lines = [];
  #Output = "";
  #Sections = [];

  // The Constructor function.
  constructor()
  {
    this.CommentChars = "//";
    this.PlaceholderBegin = "_";
    this.PlaceholderEnd = "_";
  }

  // Main Processing Methods

  // Generate the #Output text.
  TextGen(sections, templateLines)
  {
    this.#Sections = sections;
    this.#Lines = templateLines;

    let lineIndex = { Value: 0 };
    for (lineIndex.Value = 0; lineIndex.Value < templateLines.length
      ; lineIndex.Value++)
    {
      let line = this.#Lines[lineIndex.Value];
      if (this.#IsConfig(line))
      {
        continue;
      }

      if (Directive.IsSectionBegin(line, this.CommentChars))
      {
        // *** Begin *** - Add
        let directive = Directive.GetDirective(line, this.CommentChars);
        let sectionItem = this.#Sections.Retrieve(directive.Name);
        // *** End   *** - Add
        if (null == sectionItem)
        {
          // No Section data.
          this.#SkipSection(lineIndex.Value);
        }
        else
        {
          lineIndex.Value++;
          sectionItem.BeginLineIndex = lineIndex.Value;
          this.#ProcessItems(sectionItem, lineIndex);
        }
        continue;
      }
      this.#AddOutput(line);
    }
    return this.#Output;
  }

  // Process the RepeatItems.
  #ProcessItems(section, lineIndex)
  {
    let success = true;
    let items = section.RepeatItems;

    // No Section data.
    if (!TextGenLib.HasItems(items))
    {
      success = false;
      lineIndex.Value = this.#SkipSection(lineIndex.Value);
      lineIndex++;
    }

    if (success)
    {
      for (let itemIndex = 0; itemIndex < items.Count(); itemIndex++)
      {
        let item = items.Items(itemIndex);

        // No Replacement data.
        if (!TextGenLib.HasItems(item.Replacements))
        {
          lineIndex.Value = this.#SkipSection(lineIndex.Value);

          // If not last item.
          if (itemIndex < items.Count() - 1)
          {
            // Do section again for following items.
            lineIndex.Value = section.BeginLineIndex;
          }
          continue;
        }

        for (let index = lineIndex.Value; index < this.#Lines.length; index++)
        {
          let line = this.#Lines[index];
          lineIndex.Value = index;

          if (Directive.IsSectionBegin(line, this.CommentChars))
          {
            // *** Begin *** - Add
            let directive = Directive.GetDirective(line, this.CommentChars);
            let sectionItem = this.#Sections.Retrieve(directive.Name);
            // *** End   *** - Add
            if (null == sectionItem)
            {
              // No Section data.
              index = this.#SkipSection(lineIndex.Value);
              continue;
            }

            // RepeatItem processing starts with first line.
            lineIndex.Value++;
            sectionItem.BeginLineIndex = lineIndex.Value;

            let hasActiveItem = false;
            if (TextGenLib.HasItems(item.Replacements))
            {
              // Add current replacements to Active array.
              hasActiveItem = true;
              this.#ActiveReplacements.push(item.Replacements);
            }
            this.#ProcessItems(sectionItem, lineIndex);
            if (hasActiveItem)
            {
              // Remove Replacements that are no longer active.
              this.#ActiveReplacements.pop();
            }

            // Continue with returned line index after the processed section.
            index = lineIndex.Value;
          }

          if (Directive.IsSectionEnd(line, this.CommentChars))
          {
            // If not last item.
            if (itemIndex < items.Count() - 1)
            {
              // Do section again for following items.
              lineIndex.Value = section.BeginLineIndex;
              break;
            }
          }

          let process = this.#DoIf(item.Replacements, line, lineIndex);
          index = lineIndex.Value;

          if (process)
          {
            // Does not output directives.
            this.#DoOutput(item.Replacements, line);
          }
        }
      }
    }
  }

  // Perform the line replacements.
  #DoReplacements(replacements, lineItem)
  {
    if (lineItem.Value.includes(this.PlaceholderBegin))
    {
      replacements.Sort();
      let line = lineItem.Value;

      //const matches = line.match(/(?<=_).+?(?=_)/g);
      let pattern = `${this.PlaceholderBegin}.+?${this.PlaceholderEnd}`;
      let matchRegex = new RegExp(pattern, "g");
      const matches = line.match(matchRegex);
      for (let index = 0; index < matches.length; index++)
      {
        let match = matches[index];
        let replacement = replacements.Retrieve(match);
        if (replacement != null)
        {
          lineItem.Value = lineItem.Value.replaceAll(match, replacement.Value);
        }
        else
        {
          // Replacement not found in current collection.
          // Search active replacements.
          let active = this.#ActiveReplacements;
          // *** Next Statement *** - Change
          for (let activeIndex = active.length - 1; activeIndex >= 0; activeIndex--)
          {
            replacement = active[activeIndex].Retrieve(match);
            if (replacement != null)
            {
              lineItem.Value = lineItem.Value.replaceAll(match
                , replacement.Value);
              break;
            }
          }
        }
      }
    }
  }

  // Process the #IfBegin directive.
  #DoIf(replacements, line, lineIndex)
  {
    let retValue = true;

    // Check for #IfBegin.
    let success = true;
    if (!Directive.IsIfBegin(line, this.CommentChars))
    {
      success = false;
    }

    // Check replacement value against directive value.
    let directive = null;
    let isIf = false;
    let isMatch = false;
    if (success)
    {
      // Is #IfBegin so do not output.
      retValue = false;

      directive = Directive.GetDirective(line, this.CommentChars);
      // *** Begin *** Add
      if (null == directive
        || !LJC.HasText(directive.Value))
      {
        success = false;
      }
      // *** End   *** Add
    }

    if (success)
    {
      let replacement = replacements.Retrieve(directive.Name);
      if ("hasvalue" == directive.Value.toLowerCase())
      {
        isIf = true;
        if (replacement != null
          && LJC.HasText(replacement.Value))
        {
          isMatch = true;
        }
      }
      else
      {
        isIf = true;
        if (replacement != null
          && replacement.Value == directive.Value)
        {
          isMatch = true;
        }
      }
    }

    if (success)
    {
      // Process to IfEnd.
      lineIndex.Value++;
      for (let index = lineIndex.Value; index < this.#Lines.length; index++)
      {
        let line = this.#Lines[index];
        if (isMatch
          && !Directive.IsIfElse(line, this.CommentChars))
        {
          this.#DoOutput(replacements, line);
        }
        if (isIf
          && Directive.IsIfElse(line, this.CommentChars))
        {
          isMatch = !isMatch;
        }
        if (Directive.IsIfEnd(line, this.CommentChars))
        {
          lineIndex.Value = index;
          break;
        }
      }
    }
    return retValue;
  }

  // If not directive, process replacements and add to output.
  #DoOutput(replacements, line)
  {
    if (!Directive.IsDirective(line, this.CommentChars))
    {
      let lineItem = { Value: line };
      if (line != null
        && line.trim().length > 0)
      {
        this.#DoReplacements(replacements, lineItem);
      }
      this.#AddOutput(lineItem.Value);
    }
  }

  // Skips to the end of the current section.
  #SkipSection(lineIndexValue)
  {
    let retValue = lineIndexValue;

    // Skip to end of section.
    for (let index = lineIndexValue; index < this.#Lines.length; index++)
    {
      let line = this.#Lines[index];
      if (Directive.IsSectionEnd(line, this.CommentChars))
      {
        retValue = index++;
        break;
      }
    }
    return retValue;
  }

  // Other Methods

  // Add the line to the output.
  #AddOutput(line)
  {
    if (this.#Output.length > 0)
    {
      this.#Output += "\r\n";
    }
    this.#Output += line;
  }

  // Sets the configuration values.
  #IsConfig(line)
  {
    let retValue = false;

    // Sets the configuration.
    if (Directive.IsDirective(line, this.CommentChars))
    {
      let directive = Directive.GetDirective(line, this.CommentChars);
      switch (directive.ID.toLowerCase())
      {
        case "#commentchars":
          retValue = true;
          this.CommentChars = directive.Name;
          break;

        case "#placeholderbegin":
          retValue = true;
          this.PlaceholderBegin = directive.Name;
          break;

        case "#placeholderend":
          retValue = true;
          this.PlaceholderEnd = directive.Name;
          break;
      }
    }
    return retValue;
  }
}

// Represents a Directive object.
class Directive
{
  // static check Line Functions

  // Checks line for directive and returns the directive object.
  // Returns directive if line is a directive.
  static GetDirective(line, commentChars)
  {
    let retValue = null;

    // Directive Layout = "// #Directive Name [DataType]"
    // Checks for #CommentChars as property may not be set.
    if (line != null
      && (line.trim().startsWith(commentChars)
        || line.toLowerCase().includes("#commentchars")))
    {
      let tokens = line.trim().split(/\s+/g);
      if (tokens.length > 1
        && tokens[1].startsWith("#"))
      {
        let id = tokens[1];
        let name = null;
        if (tokens.length > 2)
        {
          name = tokens[2];
        }
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
  static IsDirective(line, commentChars)
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
  static IsIfBegin(line, commentChars)
  {
    let retValue = false;

    // Directive Layout = "// #SectionEnd Name"
    let directive = Directive.GetDirective(line, commentChars);
    if (directive != null
      && "#ifbegin" == directive.ID.toLowerCase())
    {
      retValue = true;
    }
    return retValue;
  }

  // Checks if the line is a SectionEnd.  // 
  static IsIfElse(line, commentChars)
  {
    let retValue = false;

    // Directive Layout = "// #SectionEnd Name"
    let directive = Directive.GetDirective(line, commentChars);
    if (directive != null
      && "#ifelse" == directive.ID.toLowerCase())
    {
      retValue = true;
    }
    return retValue;
  }

  // Checks if the line is a SectionEnd.  // 
  static IsIfEnd(line, commentChars)
  {
    let retValue = false;

    // Directive Layout = "// #SectionEnd Name"
    let directive = Directive.GetDirective(line, commentChars);
    if (directive != null
      && "#ifend" == directive.ID.toLowerCase())
    {
      retValue = true;
    }
    return retValue;
  }

  // Checks if the line is a SectionBegin.
  static IsSectionBegin(line, commentChars)
  {
    let retValue = false;

    // Directive Layout = "// #SectionBegin Name"
    let directive = Directive.GetDirective(line, commentChars);
    if (directive != null
      && "#sectionbegin" == directive.ID.toLowerCase())
    {
      retValue = true;
    }
    return retValue;
  }

  // Checks if the line is a SectionEnd.  // 
  static IsSectionEnd(line, commentChars)
  {
    let retValue = false;

    // Directive Layout = "// #SectionEnd Name"
    let directive = Directive.GetDirective(line, commentChars);
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
