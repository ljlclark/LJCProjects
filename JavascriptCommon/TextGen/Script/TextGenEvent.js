// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// TextGenEvents.js
// <script src="../LJCCommon.js"></script>
// <script src="Script/RepeatItems.js"></script>
// <script src="Script/Replacements.js"></script>
// <script src="Script/Sections.js"></script>
// <script src="Script/SelectTable.js"></script>
// <script src="Script/StringBuilder.js"></script>
// <script src="Script/TextData.js"></script>
// <script src="Script/TextGen.js"></script>
// <script src="Script/TextGenCode.js"></script>

// Generate output text event handlers.
class TextGenEvent
{
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
    tableDataMenu.style.visibility = "hidden";
    templateMenu.style.visibility = "hidden";
    let itemClick = true;
    let menu = null;
    let url = null;
    switch (eItem.id)
    {
      case "cColl":
        itemClick = false;
        url = `${base}${cBase}${c}/CollectionTemplate.cs`;
        window.open(url);
        break;

      case "cDO":
        itemClick = false;
        url = `${base}${cBase}${c}/DataTemplate.cs`;
        window.open(url);
        break;

      case "jsColl":
        itemClick = false;
        url = `${base}${cBase}${js}/ItemsTemplate.js`;
        window.open(url);
        break;

      case "jsDO":
        itemClick = false;
        url = `${base}${cBase}${js}/ItemTemplate.js`;
        window.open(url);
        break;

      case "createData":
        itemClick = false;
        textData.value = TextGenCode.CreateData(template.value);
        LJC.SetTextRows(textData);
        TextGenEvent.PageTextCols();
        break;

      case "dataTab":
        itemClick = false;
        TextGenCode.ShowTabItems(dataLayout);
        break;

      case "generateTab":
        itemClick = false;
        TextGenCode.ShowTabItems(genLayout);
        gSections = TextGenCode.CreateSections();
        TextGenCode.CreateSectionTables();
        break;

      case "genOutput":
        itemClick = false;
        TextGenCode.Process();
        break;

      case "templateOptions":
        itemClick = false;
        menu = LJC.Element("templateMenu");
        TextGenEvent.ShowMenu(event, menu);
        break;

      case "generateOptions":
        itemClick = false;
        menu = LJC.Element("tableDataMenu");
        TextGenEvent.ShowMenu(event, menu);
        break;
    }

    if (itemClick)
    {
      // Sets SelectedRow if gSectionTable.
      let success = false;
      if (gSectionTable.IsTableData(eItem))
      {
        success = true;
        gSectionTable.SelectRow(gSectionTable.SelectedRow)
      }

      if (!success)
      {
        // Sets SelectedRow if gItemTable.
        if (gItemTable.IsTableData(eItem))
        {
          gItemTable.SelectRow(gItemTable.SelectedRow)
        }
      }
    }
  }

  // 
  static ShowMenu(event, menu)
  {
    menu.style.top = `${event.pageY}px`;
    menu.style.left = `${event.pageX}px`;
    menu.style.visibility = "visible";
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

  // 
  static MouseOut(event)
  {
    const layoutColor = "lightblue";
    const menuColor = "lightsteelblue";

    let eItem = event.target;
    switch (eItem.id)
    {
      case "dataTab":
        eItem.style.backgroundColor = layoutColor;
        if ("none" == gDataDisplay)
        {
          eItem.style.backgroundColor = menuColor;
        }
        break;

      case "generateTab":
        eItem.style.backgroundColor = layoutColor;
        if ("none" == gGenDisplay)
        {
          eItem.style.backgroundColor = menuColor;
        }
        break;

      case "templateOptions":
        eItem.style.backgroundColor = layoutColor;
        break;

      case "generateOptions":
        eItem.style.backgroundColor = layoutColor;
        break;
    }
  }

  // 
  static MouseOver(event)
  {
    const overColor = "aliceblue";

    let eItem = event.target;
    switch (eItem.id)
    {
      case "dataTabs":
        if ("none" == gDataDisplay)
        {
          eItem.style.backgroundColor = overColor;
        }
        break;

      case "generateTab":
        if ("none" == gGenDisplay)
        {
          eItem.style.backgroundColor = overColor;
        }
        break;

      case "templateOptions":
        eItem.style.backgroundColor = "aliceblue";
        break;

      case "generateOptions":
        eItem.style.backgroundColor = "aliceblue";
        break;
    }
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
    TextGenEvent.SetTextCols(event, width);
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
    templateOptions.style.marginLeft = (cellWidth - 130) + "px";
    generateOptions.style.marginLeft = (cellWidth - 130) + "px";

    textData.cols = cols;
    tableData.style.width = cellWidth + "px";
    output.cols = cols;
  }
}