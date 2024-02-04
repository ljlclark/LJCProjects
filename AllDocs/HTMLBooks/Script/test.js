"use strict";
// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// test.js

// Handles the onload event.
window.onload = function ()
{
  //let lineCount = LJC.LineCount("columns") - 2;
  //let element = LJC.ElementByID("columnsHeight");
  //element.innerHTML = lineCount;
}

// Common static functions.
class LJC
{
  // Gets the element reference by ID.
  static ElementByID(id)
  {
    let retValue = document.getElementById(id);
    if (null == retValue)
    {
      alert(`Element '${id}' was not found.`);
    }
    return retValue;
  }

  // Gets the element font size.
  static FontSize(element)
  {
    let retValue = parseFloat(getComputedStyle(element).fontSize);
    return retValue;
  }

  // Gets the element line count.
  static LineCount(id)
  {
    let element = this.ElementByID(id);
    let fontSize = this.FontSize(element);
    let retValue = Math.round(element.scrollHeight / fontSize);
    return retValue;
  }
}