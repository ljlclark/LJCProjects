// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// CalcPadEvent.js
// <script src="Script/LJCCommon.js"></script>
// <script src="Script/CalcPadCode.js"></script>

// Generate event handlers.
class CalcPadEvent
{
  // Sets the Event handlers.
  static SetEvents()
  {
    calcPadMenu.style.visibility = "hidden";

    // window Event Handlers.
    window.addEventListener("resize", CalcPadEvent.TextCols);

    // document event handlers
    addEventListener("click", CalcPadEvent.DocumentClick);
    addEventListener("mouseover", CalcPadEvent.DocumentMouseOver)
    addEventListener("mouseout", CalcPadEvent.DocumentMouseOut)

    // textarea event handers
    calcPad.addEventListener("keyup", LJC.EventTextRows);
    calcPad.addEventListener("keydown", CalcPadEvent.IndentKeys);
    calcPad.addEventListener("contextmenu", CalcPadEvent.ContextMenu);
  }

  // Actions
  // ---------------
  // Handles the Document "click" event.
  static DocumentClick(event)
  {
    let eTarget = event.target;
    let success = false;;

    // Hide Menus
    calcPadMenu.style.visibility = "hidden";

    // Show Menus
    if (!success)
    {
      //let menu = null;
      switch (eTarget.id)
      {
        case "options":
          success = true;
          CalcPadEvent.ShowMenu(event, calcPadMenu);
          break;

        case "setLines":
          LJC.SetTextRows(calcPad);
          break;

        case "needs":
          calcPad.value = CalcPadCode.NeedsAssesment();
          LJC.SetTextRows(calcPad);
          break;

        case "help":
          window.location = "HTML/CalcPadHelp.html";
          //window.open("CalcPadHelp.html");
          break;
      }
    }

    // Menu Items
    if (!success)
    {
      switch (eTarget.id)
      {
        case "doCalcs":
          let calcPadCode = new CalcPadCode();
          calcPadCode.DoCalcs(calcPad.value);
          break;
      }
    }
  }

  // Other Event Handlers.
  // ---------------

  //
  static ContextMenu(event)
  {
    event.preventDefault();
    CalcPadEvent.ShowMenu(event, calcPadMenu);
  }

  // Handles the Document "mouseout" event.
  static DocumentMouseOut(event)
  {
    let eTarget = event.target;
    let backColor = null;
    let parent = null;
    switch (eTarget.id)
    {
      case "options":
        backColor = LJC.ElementStyle(page, "background-color");
        eTarget.style.backgroundColor = backColor;
        break;

      case "doCalcs":
      case "setLines":
      case "needs":
      case "help":
        parent = eTarget.parentElement;
        backColor = LJC.ElementStyle(parent, "background-color");
        eTarget.style.backgroundColor = backColor;
        break;
    }
  }

  // Handles the Document "mouseover" event.
  static DocumentMouseOver(event)
  {
    const dropItemColor = "lightblue";
    const hItemColor = "aliceblue";

    let eTarget = event.target;
    switch (eTarget.id)
    {
      case "options":
        eTarget.style.backgroundColor = hItemColor;
        break;

      case "doCalcs":
      case "setLines":
      case "needs":
      case "help":
        eTarget.style.backgroundColor = dropItemColor;
        break;
    }
  }

  // Applies tabs and indentation.
  static IndentKeys(event)
  {
    if ("Tab" == event.key)
    {
      if (calcPad.selectionStart < calcPad.selectionEnd)
      {
        event.preventDefault();
        let calcArea = new TextAreaCode(calcPad);
        if (event.shiftKey)
        {
          let unindent = true;
          calcArea.DoIndent(unindent);
        }
        else
        {
          calcArea.DoIndent();
        }
      }
      else
      {
        // Inserts a Tab.
        if (event.shiftKey)
        {
          event.preventDefault();
          let calcArea = new TextAreaCode(calcPad);
          calcArea.ReplaceSelection("\t");
        }
      }
    }
    //  else
    //  {
    //    CalcPadEvent.ShowKey(event);
    //    if ("i" == event.key)
    //    {
    //      if (event.ctrlKey)
    //      {
    //        event.preventDefault();
    //        alert("Insert Tab");
    //      }
    //    }
    //  }
  }

  // Sets the textarea columns
  static TextCols(event = null, width = null)
  {
    // Calculate available width for textarea elements.
    if (null == width)
    {
      let css = getComputedStyle(page, "width");
      width = parseInt(css.width, 10);
    }
    let cellPadding = 6;
    width -= cellPadding;
    CalcPadEvent.SetTextCols(event, width);
  }

  // Event Functions
  // ---------------

  // Sets the textarea coluns.
  static SetTextCols(event = null, width = null)
  {
    // Calcualte the average character width.
    let text = "abcdefghijklmnopqrstuvwxyz";
    let averageWidth = LJC.AverageCharWidth("textarea", text);

    // Calculate textarea columns.
    let cols = LJC.GetTextCols(width, 1, averageWidth);
    cols -= 4;  // Adjust?

    let cellWidth = width;
    calcPad.cols = cols;
    options.style.marginLeft = (cellWidth - 130) + "px";
  }

  // Shows the popup menu. 
  static ShowMenu(event, menu)
  {
    let top = event.pageY + 10;
    let left = event.pageX - 100;
    menu.style.top = `${top}px`;
    menu.style.left = `${left}px`;
    menu.style.visibility = "visible";
  }

  // 
  static ShowKey(event)
  {
    alert(`key: ${event.key}\r\n`
      + `code: ${event.code}\r\n`
      + `CTRL: ${event.ctrlKey}\r\n`
      + `SHIFT: ${event.shiftKey}\r\n`
      + `ALT: ${event.altKey}`);
  }
}