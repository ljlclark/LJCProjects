// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// CalcPadEvent.js
// <script src="../LJCCommon.js"></script>

// Generate event handlers.
class CalcPadEvent
{
  // Sets the Event handlers.
  static SetEvents()
  {
    // window Event Handlers.
    window.addEventListener("resize", CalcPadEvent.TextCols);
  }

  // Event Handlers.
  // --------------

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

  // Functions
  // --------------

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
}