// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// TextGenPage.js
// <script src="ArgErr.js"></script>

// Generate output text utility functions.
class TextGenPage
{
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
    TextGenPage.SetTextCols(null, width);
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
    TextGenPage.SetTextCols(null, width);
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

  // Sete Event handlers.
  static SetEvents()
  {
    addEventListener("resize", TextGenPage.PageTextCols);
    data.addEventListener("mouseover", TextGenPage.MouseOver)
    data.addEventListener("mouseout", TextGenPage.MouseOut)
    generate.addEventListener("mouseover", TextGenPage.MouseOver)
    generate.addEventListener("mouseout", TextGenPage.MouseOut)
    template.addEventListener("keyup", LJC.EventTextRows);
    ok.addEventListener("click", TextGenPage.Process);
  }

  // 
  static MouseOver(event)
  {
    let eItem = event.target;
    if ("data" == eItem.id)
    {
      if ("none" == gDataDisplay)
      {
        eItem.style.backgroundColor = "aliceblue";
      }
    }

    if ("generate" == eItem.id)
    {
      if ("none" == gGenDisplay)
      {
        eItem.style.backgroundColor = "aliceblue";
      }
    }
  }

  // 
  static MouseOut(event)
  {
    let eItem = event.target;
    if ("data" == eItem.id)
    {
      eItem.style.backgroundColor = "lightblue";
      if ("none" == gDataDisplay)
      {
        eItem.style.backgroundColor = "lightsteelblue";
      }
    }

    if ("generate" == eItem.id)
    {
      eItem.style.backgroundColor = "lightblue";
      if ("none" == gGenDisplay)
      {
        eItem.style.backgroundColor = "lightsteelblue";
      }
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
    template.cols = cols;
    output.cols = cols;
  }

  // Shows tab items.
  static ShowItems(eItem)
  {
    if ("genTable" == eItem.id)
    {
      dataTable.style.display = "none";
      gDataDisplay = "none";
      data.style.backgroundColor = "initial";
      generate.style.backgroundColor = "lightblue";
      genTable.style.display = "initial";
      gGenDisplay = "initial";
      ok.style.display = "initial";
    }
    else
    {
      dataTable.style.display = "initial";
      gDataDisplay = "initial";
      data.style.backgroundColor = "lightblue";
      generate.style.backgroundColor = "initial";
      genTable.style.display = "none";
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
}