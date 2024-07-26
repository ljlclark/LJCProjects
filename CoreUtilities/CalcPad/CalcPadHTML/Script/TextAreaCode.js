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

  //
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
