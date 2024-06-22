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
  // Returns directive if line is a directive.
  static GetDirective(line, commentChars = "//")
  {
    let retValue = null;

    if (line != null
      && (line.trim().startsWith(commentChars)
      || line.toLowerCase().includes("#commentchars")))
    {
      let tokens = line.trim().split(/\s+/g);
      if (tokens.length > 2
        && tokens[1].startsWith("#"))
      {
        let name = tokens[1];
        let value = tokens[2];
        retValue = { Name: name, Value: value };
      }
    }
    return retValue;
  }

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

  // Checks if there are replacements.
  static HasReplacements(replacements)
  {
    let retValue = false;

    if (replacements != null
      && replacements.Count() > 0)
    {
      retValue = true;
    }
    return retValue;
  }

  // Checks if there are sections.
  static HasSections(sections)
  {
    let retValue = false;

    if (sections != null
      && sections.Count() > 0)
    {
      retValue = true;
    }
    return retValue;
  }

  // The Constructor function.
  constructor()
  {
    this.CommentChars = "//";
    this.PlaceholderBegin = "_";
    this.PlaceholderEnd = "_";

    this.Lines = [];
    this.Output = "";
    this.Sections = [];
  }

  // Main Processing Methods

  // Generate the output text.
  TextGen(sections, templateLines)
  {
    let retValue = "";

    this.Sections = sections;
    this.Lines = templateLines;

    let lineIndex = { Value: 0 };
    for (lineIndex.Value = 0; lineIndex.Value < templateLines.length
      ; lineIndex.Value++)
    {
      let line = this.Lines[lineIndex.Value];
      if (this.IsConfig(line))
      {
        continue;
      }

      let sectionItem = { Value: null };
      if (this.IsSectionBegin(line, sectionItem))
      {
        lineIndex.Value++;
        sectionItem.Value.BeginLineIndex = lineIndex.Value;
        this.ProcessItems(sectionItem.Value, lineIndex);
        continue;
      }
      this.AddOutput(line);
    }
    retValue = this.Output;
    return retValue;
  }

  // Process the RepeatItems.
  ProcessItems(section, lineIndex)
  {
    let items = section.RepeatItems;
    for (let itemIndex = 0; itemIndex < items.Count(); itemIndex++)
    {
      let item = items.Items(itemIndex);
      let index = 0;
      for (index = lineIndex.Value; index < this.Lines.length; index++)
      {
        let line = this.Lines[index];
        lineIndex.Value = index;

        let sectionItem = { Value: null }
        if (this.IsSectionBegin(line, sectionItem))
        {
          sectionItem.Value.BeginLineIndex = index + 1;
          lineIndex.Value++;
          this.ProcessItems(sectionItem.Value, lineIndex);
          index = lineIndex.Value;
        }

        if (this.IsSectionEnd(line))
        {
          if (itemIndex < items.Count() - 1)
          {
            // Do section again for following items.
            lineIndex.Value = section.BeginLineIndex;
            break;
          }
          lineIndex.Value++;
        }

        if (!this.IsDirective(line))
        {
          let lineItem = { Value: line };
          if (line.trim().length > 0)
          {
            this.DoReplacements(item, lineItem);
          }
          this.AddOutput(lineItem.Value);
        }
      }
    }
  }

  // Perform the line replacements.
  DoReplacements(item, lineItem)
  {
    //if (lineItem.Value.includes("_"))
    if (lineItem.Value.includes(this.PlaceholderBegin))
    {
      let replacements = item.Replacements;
      replacements.Sort();
      let line = lineItem.Value;

      //const matches = line.match(/(?<=_).+?(?=_)/g);
      let match = `${this.PlaceholderBegin}.+?${this.PlaceholderEnd}`;
      let matchRegex = new RegExp(match, "g");
      const matches = line.match(matchRegex);
      for (let index = 0; index < matches.length; index++)
      {
        let match = matches[index];
        let replacement = replacements.Retrieve(match);
        if (replacement != null)
        {
          lineItem.Value = lineItem.Value.replaceAll(match
            , replacement.Value);
        }
      }
    }
  }

  // Other Methods

  // Add the line to the output.
  AddOutput(line)
  {
    if (this.Output.length > 0)
    {
      this.Output += "\r\n";
    }
    this.Output += line;
  }

  // Checks if the line has a directive.
  IsDirective(line)
  {
    let retValue = false;

    let directive = TextGenLib.GetDirective(line, this.CommentChars);
    if (directive != null)
    {
      let lowerName = directive.Name.toLowerCase();
      if (lowerName == "#commentchars"
        || lowerName == "#placeholderbegin"
        || lowerName == "#placeholderend"
        || lowerName == "#sectionbegin"
        || lowerName == "#sectionend"
        || lowerName == "#value")
      {
        retValue = true;
      }
    }
    return retValue;
  }

  // Checks if the line is a SectionBegin.
  IsSectionBegin(line, sectionItem)
  {
    let retValue = false;

    // Directive Layout = "// #SectionBegin Name"
    let directive = TextGenLib.GetDirective(line, this.CommentChars);
    if (directive != null
      && "#sectionbegin" == directive.Name.toLowerCase())
    {
      sectionItem.Value = this.Sections.Retrieve(directive.Value);
      retValue = true;
    }
    return retValue;
  }

  // Checks if the line is a SectionEnd.  // 
  IsSectionEnd(line)
  {
    let retValue = false;

    // Directive Layout = "// #SectionEnd Name"
    let directive = TextGenLib.GetDirective(line, this.CommentChars);
    if (directive != null
      && "#sectionend" == directive.Name.toLowerCase())
    {
      retValue = true;
    }
    return retValue;
  }

  // Sets the configuration values.
  IsConfig(line)
  {
    let retValue = false;

    // Sets the configuration.
    if (this.IsDirective(line))
    {
      let directive = TextGenLib.GetDirective(line, this.CommentChars);
      switch (directive.Name.toLowerCase())
      {
        case "#commentchars":
          retValue = true;
          this.CommentChars = directive.Value;
          break;

        case "#placeholderbegin":
          retValue = true;
          this.PlaceholderBegin = directive.Value;
          break;

        case "#placeholderend":
          retValue = true;
          this.PlaceholderEnd = directive.Value;
          break;
      }
    }
    return retValue;
  }
}