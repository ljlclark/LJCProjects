// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// TextAreaCode.js
// <script src="Script/LJCCommon.js"></script>

// TextArea functions.
class TextAreaCode
{
  // Initializes an object instance.
  constructor(eItem)
  {
    this.Item = eItem;
    this.Lines = [];
    this.ResetLines();
  }

  // Parses Lines from Item.
  ResetLines()
  {
    if (LJC.HasValue(this.Item.value))
    {
      this.Lines = this.Item.value.split("\n");
    }
  }

  // Get the selection indexes for the provided line index.
  GetLineSelection(lineIndex)
  {
    let retValue = new Selection();

    for (let index = 0; index <= lineIndex; index++)
    {
      let line = this.Lines[index];
      if (index < lineIndex)
      {
        retValue.BeginIndex += line.length + 1;
      }
      else
      {
        retValue.EndIndex = retValue.BeginIndex + line.length;
      }
    }
    return retValue;
  }

  // 
  Indent(remove = false)
  {
    let area = this.Item;
    let value = area.value;

    // Get the begin selected line index.
    let text = value.substring(0, area.selectionStart);
    let beginIndex = text.split("\n").length - 1;

    // Get the end selected line index.
    text = value.substring(0, area.selectionEnd);
    let endIndex = text.split("\n").length - 1;

    let saveSelection = this.GetSelection();
    let first = true;
    for (let index = beginIndex; index <= endIndex; index++)
    {
      let line = this.Lines[index];
      let selection = this.GetLineSelection(index);
      this.SetSelection(selection);
      if (remove)
      {
        if (line.startsWith("  "))
        {
          line = line.substring(2);
          this.ReplaceText(line);
          if (first)
          {
            saveSelection.BeginIndex -= 2;
          }
          first = false;
          saveSelection.EndIndex -= 2
        }
      }
      else
      {
        this.ReplaceText(`  ${line}`);
        if (first)
        {
          saveSelection.BeginIndex += 2;
        }
        first = false;
        saveSelection.EndIndex += 2
      }
      this.ResetLines();
    }
    this.SetSelection(saveSelection);
  }

  // Replace the selected text.
  ReplaceText(text)
  {
    calcPad.setRangeText(text, this.Item.selectionStart
      , this.Item.selectionEnd, 'end')
  }

  // Replaces a line by line index.
  ReplaceLine(lineIndex, text)
  {
    let selection = this.GetLineSelection(lineIndex);
    this.SetSelection(selection);
    this.ReplaceSelection(text);
  }

  // Replaces the already selected text with the provided text.
  ReplaceSelection(text)
  {
    let beginIndex = this.Item.selectionStart;
    let endIndex = this.Item.selectionEnd;
    let textValue = this.Item.value;
    let frontText = textValue.slice(0, beginIndex);
    let backText = textValue.slice(endIndex);
    this.Item.value = frontText + text + backText;
  }

  // Gets the selection indexes.
  GetSelection()
  {
    let retValue = new Selection();
    retValue.BeginIndex = this.Item.selectionStart;
    retValue.EndIndex = this.Item.selectionEnd;
    return retValue;
  }

  // Sets the selection indexes.
  SetSelection(selection)
  {
    this.Item.focus();
    this.Item.selectionStart = selection.BeginIndex;
    this.Item.selectionEnd = selection.EndIndex;
  }
}

// 
class Selection
{
  // Initializes an object instance.
  constructor(beginIndex = 0, endIndex = 0)
  {
    this.BeginIndex = beginIndex;
    this.EndIndex = endIndex;
  }
}
