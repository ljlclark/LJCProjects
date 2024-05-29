// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// TextGen.js
// <script src="ArgErr.js"></script>

// Generate output text from a template and data.
class TextGenHtml
{
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
}