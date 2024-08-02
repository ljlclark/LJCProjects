// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// TextAreaCode.js
// <script src="Script/LJCCommon.js"></script>

// TextArea functions.
class TextAreaCode
{
  // Constructors and initialization.
  // ---------------

  // Initializes an object instance.
  constructor(eItem)
  {
    this.Item = eItem;
    this.Lines = [];
    this.LineIndexes = [];
    this.ResetLines();
  }

  // Parses Lines from Item.
  ResetLines()
  {
    if (LJC.HasValue(this.Item.value))
    {
      this.Lines = this.Item.value.split("\n");
      this.CreateLineIndexes();
    }
  }

  // Creates the line begin indexes.
  CreateLineIndexes()
  {
    this.LineIndexes = [];
    if (this.Lines.length > 0)
    {
      let beginIndex = 0;
      this.LineIndexes.push(beginIndex);
      for (let index = 0; index < this.Lines.length - 1; index++)
      {
        let line = this.Lines[index];
        beginIndex += line.length + 1;
        this.LineIndexes.push(beginIndex);
      }
    }
  }

  // Indentation
  // ---------------

  // Applies the tab or indents.
  DoIndent(remove = false)
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
    let first = { Value: true };
    for (let index = beginIndex; index <= endIndex; index++)
    {
      let line = this.Lines[index];
      let selection = this.GetLineSelection(index);
      this.SetSelection(selection);
      if (remove)
      {
        this.#Unindent(line, saveSelection, first);
      }
      else
      {
        this.#Indent(line, saveSelection, first);
      }
      this.ResetLines();
    }
    this.SetSelection(saveSelection);
  }

  // Indent the selected text.
  #Indent(line, saveSelection, first)
  {
    this.ReplaceSelection(`  ${line}`);
    if (first.Value)
    {
      saveSelection.BeginIndex += 2;
    }
    first.Value = false;
    saveSelection.EndIndex += 2
  }

  // Unindent the selected text.
  #Unindent(line, saveSelection, first)
  {
    if (line.startsWith("  "))
    {
      line = line.substring(2);
      this.ReplaceSelection(line);
      if (first.Value)
      {
        saveSelection.BeginIndex -= 2;
      }
      first.Value = false;
      saveSelection.EndIndex -= 2
    }
  }

  // Selection Methods
  // ---------------

  // Get the selection indexes for the provided line index.
  GetLineSelection(lineIndex)
  {
    let retValue = new Selection();

    this.CreateLineIndexes();
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

  // Gets the selection indexes.
  GetSelection()
  {
    let retValue = new Selection();
    retValue.BeginIndex = this.Item.selectionStart;
    retValue.EndIndex = this.Item.selectionEnd;
    return retValue;
  }

  // Replaces a line by line index.
  ReplaceLine(lineIndex, text)
  {
    let selection = this.GetLineSelection(lineIndex);
    this.SetSelection(selection);
    this.ReplaceSelection(text);
  }

  //// Replaces the already selected text with the provided text.
  //ReplaceSelection(text)
  //{
  //  let beginIndex = this.Item.selectionStart;
  //  let endIndex = this.Item.selectionEnd;
  //  let textValue = this.Item.value;
  //  let frontText = textValue.slice(0, beginIndex);
  //  let backText = textValue.slice(endIndex);
  //  this.Item.value = frontText + text + backText;
  //}

  // Replace the selected text.
  ReplaceSelection(text)
  {
    calcPad.setRangeText(text, this.Item.selectionStart
      , this.Item.selectionEnd, 'end')
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
