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
  static GetDirective(line)
  {
    let retValue = null;

    if (line != null
      && line.trim().startsWith("//"))
    {
      let tokens = line.trim().split(" ");
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
  constructor(commentChars = "//")
  {
    this.CommentChars = commentChars;
    this.Lines = [];
    this.Output = "";
    this.Sections = [];
  }

  // Add the line to the output.
  AddOutput(line)
  {
    if (this.Output.length > 0)
    {
      this.Output += "\r\n";
    }
    this.Output += line;
  }

  // Perform the line replacements.
  DoReplacements(item, lineItem)
  {
    if (lineItem.Value.includes("_"))
    {
      let replacements = item.Replacements;
      replacements.Sort();
      let begin = { Index: 0 };
      do
      {
        let name = LJC.DelimitedString(lineItem.Value, "_", "_", begin);
        if (name != null)
        {
          name = `_${name}_`;
          let replacement = replacements.Retrieve(name);
          if (replacement != null)
          {
            lineItem.Value = lineItem.Value.replaceAll(name
              , replacement.Value);
          }
          begin.Index += name.length;
        }
      }
      while (begin.Index >= 0
        && begin.Index < lineItem.Value.length);
    }
  }

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
      //let section = new Section("Empty");
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

  // Checks if the line has a directive.
  IsDirective(line)
  {
    let retValue = false;

    let directive = TextGenLib.GetDirective(line);
    if (directive != null)
    {
      let lowerName = directive.Name.toLowerCase();
      if (lowerName == "#sectionbegin"
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
    let directive = TextGenLib.GetDirective(line);
    if (directive != null
      && "#sectionbegin" == directive.Name.toLowerCase())
    {
      sectionItem.Value = this.Sections.Retrieve(directive.Value);
      retValue = true;
    }
    return retValue;
  }

  // Checks if the line is a SectionEnd.
  IsSectionEnd(line)
  {
    let retValue = false;

    // Directive Layout = "// #SectionBegin Name"
    let directive = TextGenLib.GetDirective(line);
    if (directive != null
      && "#sectionend" == directive.Name.toLowerCase())
    {
      retValue = true;
    }
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

        let sectionItem = { Value: null }
        if (this.IsSectionBegin(line, sectionItem))
        {
          index++;
          sectionItem.Value.BeginLineIndex = index;
          lineIndex.Value = index;
          this.ProcessItems(sectionItem.Value, lineIndex);
          index = lineIndex.Value;
        }

        if (this.IsSectionEnd(line))
        {
          if (itemIndex < items.Count() - 1)
          {
            // Do section again for following items.
            lineIndex.Value == section.BeginLineIndex;
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
      lineIndex.Value = index;
    }
  }
}