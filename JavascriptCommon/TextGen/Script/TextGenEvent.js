// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// TextGenEvents.js
// <script src="../LJCCommon.js"></script>
// <script src="Script/RepeatItems.js"></script>
// <script src="Script/Replacements.js"></script>
// <script src="Script/Sections.js"></script>
// <script src="Script/StringBuilder.js"></script>
// <script src="Script/TextData.js"></script>
// <script src="Script/TextGen.js"></script>

// Generate output text utility functions.
class TextGenEvent
{
  // Functions

  // Creates the text data from a template.
  static CreateData(text)
  {
    //let text = template.value;
    let lines = text.split("\n");
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

  // Creates a table from the sections object.
  static CreateTable()
  {
    let b = new StringBuilder();
    b.Line("<table border='1' cellspacing='0'");
    b.Line("  style='background - color: aliceblue; width: 100%'>");
    b.Line("  <tr>");
    b.Line("    <th colspan='2'>Section</th>");
    b.Line("  </tr>");
    b.Line("  <tr>");
    b.Line("    <td>Main</td>");
    b.Line("  </tr>");
    b.Line("  <tr>");
    b.Line("    <th colspan='2'>Item</th>");
    b.Line("  </tr>");
    b.Line("  <tr>");
    b.Line("    <td>Item1</td>");
    b.Line("  </tr>");
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
    b.Line("</table>");
    return b.ToString();
  }

  // Shows tab items.
  static ShowTabItems(eItem)
  {
    if ("genLayout" == eItem.id)
    {
      dataLayout.style.display = "none";
      gDataDisplay = "none";
      data.style.backgroundColor = "initial";
      generate.style.backgroundColor = "lightblue";
      genLayout.style.display = "initial";
      gGenDisplay = "initial";
      ok.style.display = "initial";
    }
    else
    {
      dataLayout.style.display = "initial";
      gDataDisplay = "initial";
      data.style.backgroundColor = "lightblue";
      generate.style.backgroundColor = "initial";
      genLayout.style.display = "none";
      gGenDisplay = "none";
      ok.style.display = "none";
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

  // Sets Event handlers.
  static SetEvents()
  {
    // window Event Handlers.
    window.addEventListener("resize", TextGenEvent.PageTextCols);

    // document event handlers.
    //addEventListener("contextmenu", TextGenEvent.ContextMenu);
    document.addEventListener("click", TextGenEvent.DocumentClick);
    addEventListener("mouseover", TextGenEvent.MouseOver)
    addEventListener("mouseout", TextGenEvent.MouseOut)

    // textarea elemnts
    template.addEventListener("keyup", LJC.EventTextRows);
    textData.addEventListener("keyup", LJC.EventTextRows);
    output.addEventListener("keyup", LJC.EventTextRows);
    template.addEventListener("change", TextGenEvent.PageTextCols);

    ok.addEventListener("click", TextGenEvent.Process);
  }

  // Actions

  //
  static ContextMenu(event)
  {
    let eItem = event.target;

    let menu = null;
    switch (eItem.id)
    {
      case "templateMenuShow":
        menu = LJC.Element("templateMenu");
        break;
    }

    if (menu != null)
    {
      event.preventDefault();
      menu.style.top = `${event.pageY}px`;
      menu.style.left = `${event.pageX}px`;
      templateMenu.style.visibility = "visible";
    }
  }

  // 
  static DocumentClick(event)
  {
    const base = "https://github.com/ljlclark"
    const cBase = "/LJCProjects/blob/main";
    const c = "/CoreUtilities/LJCGenText/LJCGenTableCode/bin/Debug/Templates";
    const js = "/JavascriptCommon/TextGen/Templates";

    let eItem = event.target;
    templateMenu.style.visibility = "hidden";
    let menu = null;
    let url = null;
    switch (eItem.id)
    {
      case "cColl":
        url = `${base}${cBase}${c}/CollectionTemplate.cs`;
        break;

      case "cDO":
        url = `${base}${cBase}${c}/DataTemplate.cs`;
        break;

      case "jsColl":
        url = `${base}${cBase}${js}/ItemsTemplate.js`;
        break;

      case "jsDO":
        url = `${base}${cBase}${js}/ItemTemplate.js`;
        break;

      case "createData":
        textData.value = TextGenEvent.CreateData(template.value);
        LJC.SetTextRows(textData);
        TextGenEvent.PageTextCols();
        break;

      case "data":
        TextGenEvent.ShowTabItems(dataLayout);
        break;

      case "generate":
        TextGenEvent.ShowTabItems(genLayout);
        break;

      case "templateMenuShow":
        menu = LJC.Element("templateMenu");
        break;
    }
    if (menu != null)
    {
      menu.style.top = `${event.pageY}px`;
      menu.style.left = `${event.pageX}px`;
      templateMenu.style.visibility = "visible";
    }
    if (url != null)
    {
      window.open(url);
    }
  }

  // Other Events

  // Set the Form textarea coluns.
  static FormTextCols(event = null, width = null)
  {
    // Calculate available width for textarea elements.
    if (null == width)
    {
      let css = getComputedStyle(form, "width");
      width = parseInt(css.width, 10);
    }
    let cellPadding = 6;
    width -= cellPadding;
    TextGenEvent.SetTextCols(null, width);
  }

  // Set the Page textarea coluns.
  static PageTextCols(event = null, width = null)
  {
    // Calculate available width for textarea elements.
    if (null == width)
    {
      let css = getComputedStyle(page, "width");
      width = parseInt(css.width, 10);
    }
    let cellPadding = 6;
    width -= cellPadding;
    TextGenEvent.SetTextCols(null, width);
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

  // 
  static MouseOver(event)
  {
    const overColor = "aliceblue";

    let eItem = event.target;
    switch (eItem.id)
    {
      case "data":
        if ("none" == gDataDisplay)
        {
          eItem.style.backgroundColor = overColor;
        }
        break;

      case "generate":
        if ("none" == gGenDisplay)
        {
          eItem.style.backgroundColor = overColor;
        }
        break;

      case "templateMenuShow":
        eItem.style.backgroundColor = "aliceblue";
        break;
    }
  }

  // 
  static MouseOut(event)
  {
    const layoutColor = "lightblue";
    const menuColor = "lightsteelblue";

    let eItem = event.target;
    switch (eItem.id)
    {
      case "data":
        eItem.style.backgroundColor = layoutColor;
        if ("none" == gDataDisplay)
        {
          eItem.style.backgroundColor = menuColor;
        }
        break;

      case "generate":
        eItem.style.backgroundColor = layoutColor;
        if ("none" == gGenDisplay)
        {
          eItem.style.backgroundColor = menuColor;
        }
        break;

      case "templateMenuShow":
        eItem.style.backgroundColor = layoutColor;
        break;
    }
  }

  // Set the textarea coluns.
  static SetTextCols(event = null, width = null)
  {
    // Calcualte the average character width.
    let text = "abcdefghijklmnopqrstuvwxyz";
    let averageWidth = LJC.AverageCharWidth("textarea", text);

    // Calculate textarea columns.
    let cols = LJC.GetTextCols(width, 2, averageWidth);
    cols -= 4;  // Adjust?

    let cellWidth = (width / 2);
    template.cols = cols;
    templateMenuShow.style.marginLeft = (cellWidth - 130) + "px";

    textData.cols = cols;
    tableData.style.width = cellWidth + "px";
    output.cols = cols;
  }
}