// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// TextGen.js
// <script src="../ArgErr.js"></script>
// <script src="../LJCCommon.js"></script>
// <script src="Script/RepeatItems.js"></script>
// <script src="Script/Replacements.js"></script>
// <script src="Script/Sections.js"></script>

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
  TextGen(sections, templateLines)
  {
    let retValue = "";

    this.Err.SetContext("TextGen.TextGen(sections, lines)");
    this.Err.IsCollection(sections, "sections");
    this.Err.IsArray(templateLines, "templateLines");
    this.Err.ShowError();

    this.Sections = sections;
    this.Lines = templateLines;

    let section = new Section("Empty");
    let lineIndex = { Value: 0 };
    for (lineIndex.Value = 0; lineIndex.Value < templateLines.length
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
    this.Err.SetContext("TextGen.ProcessItems(section, lineIndex)");
    this.Err.IsValue(section, "section");
    this.Err.IsCollection(section.RepeatItems, "section.RepeatItems");
    this.Err.IsValue(lineIndex.Value, "lineIndex.Value");
    this.Err.ShowError();

    let items = section.RepeatItems;
    //if (items != null)
    //{
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
    //}
  }

  // Perform the line replacements.
  DoReplacements(item, lineItem)
  {
    this.Err.SetContext("TextGen.DoReplacements(item, lineItem)");
    this.Err.IsValue(item, "item");
    this.Err.IsCollection(item.Replacements, "item.Replacements");
    this.Err.IsValue(lineItem.Value, "lineItem.Value");
    this.Err.ShowError();

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

  // Add the line to the output.
  AddOutput(line)
  {
    this.Err.SetContext("TextGen.AddOutput(line)");
    this.Err.IsValue(line, "line");
    this.Err.ShowError();

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

    this.Err.SetContext("TextGen.IsSectionBegin(line, sectionItem)");
    this.Err.IsValue(line, "line");
    this.Err.IsValue(sectionItem.Value, "sectionItem.Value");
    this.Err.ShowError();

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