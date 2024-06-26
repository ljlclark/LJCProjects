// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// TextGenEvents.js
// <script src="../LJCCommon.js"></script>
// <script src="../StringBuilder.js"></script>
// <script src="../TextGenLib/RepeatItems.js"></script>
// <script src="../TextGenLib/Replacements.js"></script>
// <script src="../TextGenLib/Sections.js"></script>
// <script src="Script/TextGen.js"></script>
// <script src="Script/TextGenCode.js"></script>

// Generate text event handlers.
class TextGenEvent
{
  // Sets the Event handlers.
  static SetEvents()
  {
    // window Event Handlers.
    window.addEventListener("resize", TextGenEvent.PageTextCols);

    // document event handlers.
    addEventListener("click", TextGenEvent.DocumentClick);
    addEventListener("mouseover", TextGenEvent.DocumentMouseOver)
    addEventListener("mouseout", TextGenEvent.DocumentMouseOut)

    // textarea elements
    template.addEventListener("keyup", LJC.EventTextRows);
    textData.addEventListener("keyup", LJC.EventTextRows);
    output.addEventListener("keyup", LJC.EventTextRows);
    template.addEventListener("change", TextGenEvent.PageTextCols);
  }

  // Actions

  // Handles the Document "click" event.
  static DocumentClick(event)
  {
    const base = "https://github.com/ljlclark"
    const cBase = "/LJCProjects/blob/main";
    const c = "/CoreUtilities/LJCGenText/LJCGenTableCode/bin/Debug/Templates";
    const js = "/JavascriptCommon/TextGenLib/Templates";

    let eItem = event.target;
    tableDataMenu.style.visibility = "hidden";
    templateMenu.style.visibility = "hidden";

    // Tabs
    let success = false;;
    switch (eItem.id)
    {
      case "dataTab":
        success = true;
        TextGenCode.ShowTabItems(dataLayout);
        break;

      case "generateTab":
        success = true;
        TextGenCode.ShowTabItems(genLayout);
        gSections = TextGenCode.CreateSections();
        TextGenCode.CreateSectionRows(gSections);
        break;
    }

    // Show Menus
    if (!success)
    {
      //let menu = null;
      switch (eItem.id)
      {
        case "templateOptions":
          success = true;
          TextGenEvent.ShowMenu(event, templateMenu);
          break;

        case "generateOptions":
          success = true;
          TextGenEvent.ShowMenu(event, tableDataMenu);
          break;
      }
    }

    // Menu Items
    if (!success)
    {
      let url = null;
      switch (eItem.id)
      {
        case "cColl":
          url = `${base}${cBase}${c}/CollectionTemplate.cs`;
          window.open(url);
          break;

        case "cDO":
          url = `${base}${cBase}${c}/DataTemplate.cs`;
          window.open(url);
          break;

        case "jsColl":
          url = `${base}${cBase}${js}/ItemsTemplate.js`;
          window.open(url);
          break;

        case "jsDO":
          url = `${base}${cBase}${js}/ItemTemplate.js`;
          window.open(url);
          break;

        case "createData":
          textData.value = TextGenCode.CreateData(template.value);
          LJC.SetTextRows(textData);
          TextGenEvent.PageTextCols();
          break;

        case "genOutput":
          TextGenCode.Process();
          break;
      }
    }
  }

  // Other Events

  // Handles the Document "mouseout" event.
  static DocumentMouseOut(event)
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

  // Handles the Document "mouseover" event.
  static DocumentMouseOver(event)
  {
    const overColor = "aliceblue";

    let eItem = event.target;
    switch (eItem.id)
    {
      case "dataTab":
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

  // Sets the Form textarea coluns.
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

  // Sets the Page textarea coluns.
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

  // Sets the textarea coluns.
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

  // Shows the popup menu. 
  static ShowMenu(event, menu)
  {
    menu.style.top = `${event.pageY}px`;
    menu.style.left = `${event.pageX}px`;
    menu.style.visibility = "visible";
  }
}