// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// TextGenCode.js
// <script src="../LJCCommon.js"></script>
// <script src="Script/StringBuilder.js"></script>

// Generate output text utility functions.
class TextGenCode
{
  // Creates the text data from a template.
  static CreateData(templateText)
  {
    //let text = template.value;
    let lines = templateText.split("\n");
    let b = new StringBuilder();
    for (let index = 0; index < lines.length; index++)
    {
      let line = lines[index];
      if (line.trim().startsWith("//"))
      {
        let tokens = line.split(" ");
        if (tokens.length > 2
          && tokens[1].startsWith("#"))
        {
          let token = tokens[1];
          let name = tokens[2];
          switch (token.toLowerCase())
          {
            case "#sectionbegin":
              b.Line(`Section: ${name}`);
              b.Text("Item: Item1");
              break;

            case "#value":
              let begin = { Index: 0 }
              let value = LJC.DelimitedString(name, "_", "_", begin);
              b.Line();
              b.Text(`${name} ${value}`);
              break;
          }
        }
      }
    }
    return b.ToString();
  }

  // Creates a table from the RepeatItems object.
  static CreateItemRows(section)
  {
    let items = section.RepeatItems;
    if (TextGenLib.HasItems(items))
    {
      // Data Rows
      for (let index = 0; index < items.Count(); index++)
      {
        let item = items.Items(index);
        let b = new StringBuilder();
        b.Line("  <tr class='selectTR'>");
        b.Line(`    <td class='selectTD'>${item.Name}</td>`);
        b.Line("  </tr>");
        itemTable.innerHTML += b.ToString();

        // Replacement Heading Row
        b = new StringBuilder();
        b.Line("  <tr>");
        b.Line("    <th colspan='2'>Replacement</th>");
        b.Line("  </tr>");
        replacementTable.innerHTML = b.ToString();
        TextGenCode.CreateReplacementRows(item);
      }
    }
  }

  // Creates a table from the Replacements object.
  static CreateReplacementRows(item)
  {
    let replacements = item.Replacements;
    if (TextGenLib.HasReplacements(replacements))
    {
      // Data Rows
      for (let index = 0; index < replacements.Count(); index++)
      {
        let replacement = replacements.Items(index);
        let b = new StringBuilder();
        b.Line("  <tr class='noSelectTR'>");
        b.Line(`    <td class='selectTD'>${replacement.Name}</td>`);
        b.Line(`    <td class='selectTD'>${replacement.Value}</td>`);
        b.Line("  </tr>");
        replacementTable.innerHTML += b.ToString();
      }
    }
  }

  // Creates a table from the Sections object.
  static CreateSectionRows(sections)
  {
    if (TextGenLib.HasSections(sections))
    {
      // Heading Row
      let b = new StringBuilder();
      b.Line("  <tr>");
      b.Line("    <th colspan='2'>Sections</th>");
      b.Line("  </tr>");
      sectionTable.innerHTML = b.ToString();

      // Data Rows
      for (let index = 0; index < sections.Count(); index++)
      {
        let section = sections.Items(index);
        b = new StringBuilder();
        b.Line("  <tr class='selectTR'>");
        b.Line(`    <td class='selectTD'>${section.Name}</td>`);
        b.Line("  </tr>");
        sectionTable.innerHTML += b.ToString();
        
        // Item Heading Row
        b = new StringBuilder();
        b.Line("  <tr>");
        b.Line("    <th colspan='2'>Item</th>");
        b.Line("  </tr>");
        itemTable.innerHTML = b.ToString();
        TextGenCode.CreateItemRows(section);
      }
    }
  }

  // Create Sections object from Text Data.
  static CreateSections()
  {
    let sections = null;
    let success = true;
    let data = textData.value;
    if (null == data)
    {
      success = false;
    }

    let lines = null;
    if (success)
    {
      lines = data.split("\n");
      if (null == lines)
      {
        success = false;
      }
    }

    if (success)
    {
      sections = new Sections();
      let section = null;
      let item = null;
      let name = null;

      for (let index = 0; index < lines.length; index++)
      {
        let line = lines[index];
        let tokens = line.split(" ");
        if (tokens.length > 1)
        {
          let type = tokens[0];
          switch (type.toLowerCase())
          {
            case "section:":
              name = tokens[1];
              section = sections.Add(name);
              break;

            case "item:":
              name = tokens[1];
              item = section.RepeatItems.Add(name)
              break;

            default:
              let replacement = tokens[0];
              let value = tokens[1];
              item.Replacements.Add(replacement, value);
              break;
          }
        }
      }
      return sections;
    }
  }

  // Event handler to process the template and data.
  static Process()
  {
    let templateText = template.value;
    let lines = templateText.split("\n");
    let textGenLib = new TextGenLib();
    textGenLib.TextGen(gSections, lines);
    output.value = textGenLib.Output;
    LJC.SetTextRows(output);
  }

  // Shows tab items.
  static ShowTabItems(eItem)
  {
    if ("genLayout" == eItem.id)
    {
      dataLayout.style.display = "none";
      gDataDisplay = "none";
      dataTab.style.backgroundColor = "initial";
      generateTab.style.backgroundColor = "lightblue";
      genLayout.style.display = "initial";
      gGenDisplay = "initial";
    }
    else
    {
      dataLayout.style.display = "initial";
      gDataDisplay = "initial";
      dataTab.style.backgroundColor = "lightblue";
      generateTab.style.backgroundColor = "initial";
      genLayout.style.display = "none";
      gGenDisplay = "none";
    }
  }

  // Displays the width values..
  static ShowWidths(eItem)
  {
    let text = `id: ${eItem.id}\r\n`;
    let css = getComputedStyle(eItem, null);
    let marginLeft = parseInt(css.marginLeft, 10);
    let marginRight = parseInt(css.marginRight, 10);
    let borderLeft = parseInt(css.borderLeft, 10);
    let borderRight = parseInt(css.borderRight, 10);
    let totalWidth = eItem.clientWidth + marginLeft + marginRight;
    totalWidth += borderLeft + borderRight;
    text += `TotalWidth: ${totalWidth}\r\n`;
    text += "\r\n";

    text += `clientWidth - style.marginLeft - style.marginRight\r\n`;
    text += `clientWidth - style.borderLeft - style.borderRight\r\n`;
    text += `clientWidth: ${eItem.clientWidth}\r\n`;
    //text += `offsetWidth: ${eItem.offsetWidth}\r\n`;
    //text += `scrollWidth: ${eItem.scrollWidth}\r\n`;

    text += `style.marginLeft: ${css.marginLeft}\r\n`;
    text += `style.marginRight: ${css.marginRight}\r\n`;
    text += "\r\n";

    text += `style.borderLeft: ${css.borderLeft}\r\n`;
    text += `style.borderRight: ${css.borderRight}\r\n`;
    text += "\r\n";

    //text += `style.offset: ${css.offset}\r\n`;
    text += `style.paddingLeft: ${css.paddingLeft}\r\n`;
    text += `style.paddingRight: ${css.paddingRight}\r\n`;
    text += "\r\n";

    text += `clientWidth - style.paddingLeft - style.paddingRight\r\n`
    let width = parseInt(css.width, 10);
    width = Math.floor(width);
    text += `style.width: ${width}\r\n`;
    alert(text);
  }
}