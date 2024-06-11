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
              b.Line("Item: Item1");
              break;

            case "#value":
              let begin = { Index: 0 }
              let value = LJC.DelimitedString(name, "_", "_", begin);
              b.Line(`${name}, ${value}`);
              break;
          }
        }
      }
    }
    return b.ToString();
  }

  // Creates a table from the RepeatItems object.
  static CreateItemRows()
  {
    let b = new StringBuilder();
    b.Line("  <tr>");
    b.Line("    <th colspan='2'>Item</th>");
    b.Line("  </tr>");
    b.Line("  <tr>");
    b.Line("    <td>Item1</td>");
    b.Line("  </tr>");
    return b.ToString();
  }

  // Creates a table from the Replacements object.
  static CreateReplacementRows()
  {
    let b = new StringBuilder();
    b.Line("  <tr>");
    b.Line("    <th colspan='2'>Replacement</th>");
    b.Line("  </tr>");
    b.Line("  <tr>");
    b.Line("    <td>_ClassName_</td>");
    b.Line("    <td>GenItem</td>");
    b.Line("  </tr>");
    b.Line("  <tr>");
    b.Line("    <td>_CollectionName_</td>");
    b.Line("    <td>GenItems</td>");
    b.Line("  </tr>");
    return b.ToString();
  }

  // Creates a table from the Sections object.
  static CreateSectionRows()
  {
    let b = new StringBuilder();
    b.Line("  <tr>");
    b.Line("    <th colspan='2'>Section</th>");
    b.Line("  </tr>");
    b.Line("  <tr>");
    b.Line("    <td>Main</td>");
    b.Line("  </tr>");
    return b.ToString();
  }

  // Event handler to process the template and data.
  static Process()
  {
    let templateText = template.value;
    let sections = TextData.SectionsData();
    let lines = templateText.split("\n");
    let textGen = new TextGen();
    textGen.TextGen(sections, lines);
    output.value = textGen.Output;
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