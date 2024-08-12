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
  constructor(eTextArea)
  {
    this.Area = eTextArea;
    this.Lines = [];
    this.LineIndexes = [];
    this.ResetLines();
  }

  // Parses Lines from Item.
  ResetLines()
  {
    if (LJC.HasValue(this.Area.value))
    {
      this.Lines = this.Area.value.split("\n");
      this.CreateLineIndexes();
    }
  }

  // Creates the line begin indexes.
  CreateLineIndexes(startLineIndex = 0)
  {
    let indexes = this.LineIndexes;
    let lines = this.Lines;

    // Force start at end of array.
    if (startLineIndex > indexes.length)
    {
      startLineIndex = indexes.length - 1;
    }

    // Get lineBeginIndex from previous index;\.
    let lineBeginIndex = 0;
    if (startLineIndex > 0)
    {
      let prevIndex = startLineIndex - 1;
      lineBeginIndex = indexes[prevIndex];
      lineBeginIndex += lines[prevIndex].length + 1;
    }

    for (let index = startLineIndex; index < this.Lines.length; index++)
    {
      let line = this.Lines[index];
      indexes[index] = lineBeginIndex;
      lineBeginIndex += line.length + 1;
    }
  }

  // Indentation
  // ---------------

  // Applies the tab or indents.
  DoIndent(unindent = false)
  {
    let area = this.Area;

    // Get the begin and end selected line index.
    let beginIndex = this.FindLineIndex(area.selectionStart);
    let endIndex = this.FindLineIndex(area.selectionEnd - 1);

    let saveSelection = this.GetSelection();
    let first = { Value: true };
    for (let index = beginIndex; index <= endIndex; index++)
    {
      let line = this.Lines[index];
      let selection = this.GetLineSelection(index);
      this.SetSelection(selection);
      if (unindent)
      {
        this.#Unindent(line, saveSelection, index, first);
      }
      else
      {
        this.#Indent(line, saveSelection, index, first);
      }
      this.ResetLines();
    }
    this.SetSelection(saveSelection);
  }

  // Indent the selected text.
  #Indent(line, saveSelection, lineIndex, first)
  {
    this.ReplaceSelection(`  ${line}`);
    if (first.Value)
    {
      // *** Begin *** Add - 8/7/24
      let lineBeginIndex = this.LineIndexes[lineIndex];
      if (lineBeginIndex != saveSelection.BeginIndex)
      {
        // *** End *** Add - 8/7/24
        saveSelection.BeginIndex += 2;
      }
    }
    first.Value = false;
    saveSelection.EndIndex += 2
  }

  // Unindent the selected text.
  #Unindent(line, saveSelection, lineIndex, first)
  {
    if (line.startsWith("  "))
    {
      line = line.substring(2);
      this.ReplaceSelection(line);
      if (first.Value)
      {
        //if (saveSelection.BeginIndex > 0)
        // *** Begin *** Add - 8/7/24
        let lineBeginIndex = this.LineIndexes[lineIndex];
        if (lineBeginIndex != saveSelection.BeginIndex)
        // *** End *** Add - 8/7/24
        {
          saveSelection.BeginIndex -= 2;
        }
      }
      first.Value = false;
      saveSelection.EndIndex -= 2
    }
  }

  // Selection Methods
  // ---------------

  // 
  FindLineIndex(beginIndex)
  {
    let indexes = this.LineIndexes;
    let retValue = 0;

    let item = indexes.find(x => x > beginIndex);
    // *** Begin *** Add - 8/10/24
    if (null == item)
    {
      retValue = indexes.length - 1;
    }
    else
    // *** End *** Add - 8/10/24
    {
      retValue = indexes.indexOf(item);
      retValue--;
    }
    return retValue;
  }

  // Get the selection indexes for the provided line index.
  GetLineSelection(lineIndex)
  {
    let indexes = this.LineIndexes;
    let lines = this.Lines;
    let retValue = new Selection();

    retValue.BeginIndex = indexes[lineIndex];
    var line = lines[lineIndex];
    retValue.EndIndex = retValue.BeginIndex + line.length;
    return retValue;
  }

  // Get the selection values without Indexes array.
  GetLineSelection2(lineIndex)
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

  // Gets the selection indexes.
  GetSelection()
  {
    let retValue = new Selection();
    retValue.BeginIndex = this.Area.selectionStart;
    retValue.EndIndex = this.Area.selectionEnd;
    return retValue;
  }

  // Replaces a line by line index.
  ReplaceLine(lineIndex, text)
  {
    let selection = this.GetLineSelection(lineIndex);
    this.SetSelection(selection);
    this.ReplaceSelection(text);
  }

  // Replace the selected text.
  ReplaceSelection(text)
  {
    let lineIndex = this.FindLineIndex(this.Area.selectionStart);
    calcPad.setRangeText(text, this.Area.selectionStart
      , this.Area.selectionEnd, 'end');
    // *** Next Statement *** - Add 8/6/24
    this.ResetLines();
  }

  // Replaces the already selected text with the provided text.
  ReplaceSelection2(text)
  {
    let beginIndex = this.Area.selectionStart;
    let endIndex = this.Area.selectionEnd;
    let textValue = this.Area.value;
    let frontText = textValue.slice(0, beginIndex);
    let backText = textValue.slice(endIndex);
    this.Area.value = frontText + text + backText;
  }

  // Sets the selection indexes.
  SetSelection(selection)
  {
    this.Area.focus();
    this.Area.selectionStart = selection.BeginIndex;
    this.Area.selectionEnd = selection.EndIndex;
  }
}

// represents a Selection region.
class Selection
{
  // Initializes an object instance.
  constructor(beginIndex = 0, endIndex = 0)
  {
    this.BeginIndex = beginIndex;
    this.EndIndex = endIndex;
  }
}
