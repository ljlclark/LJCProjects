// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// TextGen.js

// Generate output text from a template and data.
class TextGen
{
  // The Constructor function.
  constructor()
  {
    this.Err = new ArgError();
    this.Lines = [];
    this.Output = "";
    this.Sections = [];
  }

  // Generate the output text.
  TextGen(sections, lines)
  {
    let retValue = "";

    this.Err.SetContext("TextGen(sections, lines");
    this.Err.IsCollection(sections, "sections");
    this.Err.IsArray(lines, "lines");

    this.Sections = sections;
    this.Lines = lines;

    let section = new Section("Empty");
    let lineIndex = { Value: 0 };
    for (lineIndex.Value = 0; lineIndex.Value < lines.length
      ; lineIndex.Value++)
    {
      let line = this.Lines[lineIndex.Value];
      let sectionItem = { Value: section };
      if (this.IsSectionBegin(line, sectionItem))
      {
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
    if (items != null)
    {
      for (let itemIndex = 0; itemIndex < items.Count(); itemIndex++)
      {
        let item = items.Items(itemIndex);
        let index = 0;
        for (index = lineIndex.Value; index < this.Lines.length; index++)
        {
          let line = this.Lines[index];
          if (!line.includes("#"))
          {
            let lineItem = { Value: line };
            this.DoReplacements(item, lineItem);
            this.AddOutput(lineItem.Value);
          }
        }
        lineIndex.Value = index;
      }
    }
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

  // Add the line to the output.
  AddOutput(line)
  {
    if (this.Output.length > 0)
    {
      this.Output += "\n";
    }
    this.Output += line;
  }

  // Checks if the line is a SectionBegin.
  IsSectionBegin(line, sectionItem)
  {
    let retValue = false;
    if (line.includes("#SectionBegin"))
    {
      let tokens = line.split(" ");
      if (tokens
        && tokens.length > 2)
      {
        let sectionName = tokens[2];
        sectionItem.Value = this.Sections.Retrieve(sectionName);
        retValue = true;
      }
    }
    return retValue;
  }
}