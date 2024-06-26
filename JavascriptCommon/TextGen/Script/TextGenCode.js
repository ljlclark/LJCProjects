// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// TextGenCode.js
// <script src="../LJCCommon.js"></script>
// <script src="../StringBuilder.js"></script>
// <script src="../TextGenLib/RepeatItems.js"></script>
// <script src="../TextGenLib/Replacements.js"></script>
// <script src="../TextGenLib/Sections.js"></script>
// <script src="../TextGenLib/TextGenLib.js"></script>
// <script src="Script/SelectTable.js"></script>

// Generate output text utility functions.
class TextGenCode
{
  // Main static Process functions.

  // Process the template and data.
  static Process()
  {
    let templateText = template.value;
    let lines = templateText.split("\n");
    let textGenLib = new TextGenLib();
    output.value = textGenLib.TextGen(gSections, lines);
    LJC.SetTextRows(output);
  }

  // Other static functions.

  // Creates the text data from a template.
  static CreateData(templateText)
  {
    //let text = template.value;
    let commentChars = "//";
    let lines = templateText.split("\n");
    let b = new StringBuilder();
    for (let index = 0; index < lines.length; index++)
    {
      let line = lines[index];
      let directive = TextGenLib.GetDirective(line, commentChars);
      if (directive != null)
      {
        let directiveName = directive.Name.toLowerCase();
        if ("#commentchars" == directiveName)
        {
          commentChars = directive.Value;
          continue;
        }

        let name = null;
        switch (directiveName)
        {
          case "#sectionbegin":
            if (b.ToString().length > 0)
            {
              b.Line();
            }
            name = directive.Value;
            b.Line(`Section: ${directive.Value}`);
            b.Text("Item: Item1");
            break;

          case "#value":
            let begin = { Index: 0 }
            name = directive.Value;
            let value = LJC.DelimitedString(directive.Value, "_", "_", begin);
            let dataType = directive.DataType;
            b.Line();
            b.Text(`${name} ${value} ${dataType}`);
            break;
        }
      }
    }
    return b.ToString();
  }

  // Creates a table from the RepeatItems object.
  static CreateItemRows(section)
  {
    // Item Heading Row
    let b = new StringBuilder();
    b.Line("  <tr>");
    b.Line("    <th colspan='2'>Item</th>");
    b.Line("  </tr>");
    itemTable.innerHTML = b.ToString();

    let items = section.RepeatItems;
    if (TextGenLib.HasItems(items))
    {
      // Data Rows
      for (let index = 0; index < items.Count(); index++)
      {
        let item = items.Items(index);
        b = new StringBuilder();
        b.Line("  <tr class='selectTR'>");
        b.Line(`    <td class='selectTD'>${item.Name}</td>`);
        b.Line("  </tr>");
        itemTable.innerHTML += b.ToString();
      }

      // Select first data row first data.
      let tData = gItemTable.GetTData(1);
      gItemTable.SelectRow(tData);
    }
  }

  // Creates a table from the Replacements object.
  static CreateReplacementRows(item)
  {
    // Replacement Heading Row
    let b = new StringBuilder();
    b.Line("  <tr>");
    b.Line("    <th colspan='2'>Replacement</th>");
    b.Line("  </tr>");
    replacementTable.innerHTML = b.ToString();

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
      }

      // Select first data row first data.
      let tData = gSectionTable.GetTData(1);
      gSectionTable.SelectRow(tData);
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
        let data = this.GetData(line);
        if (data != null)
        {
          switch (data.Type.toLowerCase())
          {
            case "section:":
              name = data.Name;
              section = sections.Add(name);
              break;

            case "item:":
              name = data.Name;
              item = section.RepeatItems.Add(name)
              break;

            default:
              let replacement = data.Type;
              let value = data.Name;
              item.Replacements.Add(replacement, value);
              break;
          }
        }
      }
      return sections;
    }
  }

  // Gets a Data object from a data text line.
  static GetData(line)
  {
    let retValue = null;

    let tokens = line.split(/\s+/g);
    if (tokens.length > 1)
    {
      let type = tokens[0];
      let name = tokens[1];
      retValue = { Type: type, Name: name }
    }
    return retValue;
  }

  // Callback from selectTable.SelectRow();
  static TableAction(selectTable, action, data)
  {
    switch (action.toLowerCase())
    {
      case "select":
        if (SelectTable.IsTRow(data))
        {
          // First data row first data.
          let name = selectTable.GetRowTDataText(data);

          let section = null;
          switch (selectTable.Table.id)
          {
            case "sectionTable":
              // Get selected section and create item rows.
              section = gSections.Retrieve(name);
              TextGenCode.CreateItemRows(section);
              break;

            case "itemTable":
              // Get selected section.
              let sectionRow = gSectionTable.SelectedRow;
              let sectionName = gSectionTable.GetRowTDataText(sectionRow);
              section = gSections.Retrieve(sectionName);

              // Get selected item and create replacement rows.
              let items = section.RepeatItems;
              let item = items.Retrieve(name);
              TextGenCode.CreateReplacementRows(item);
              break;
          }

        }
        break;
    }
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