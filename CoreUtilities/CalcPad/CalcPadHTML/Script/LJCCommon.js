// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCCommon.js

// Common Static Functions
class LJC
{
  // Element Helper Functions
  // ----------------------

  // Gets the HTMLElement.
  static Element(id)
  {
    return document.getElementById(id);
  }

  // Get a form element.
  static FormElement(formName, eName)
  {
    let form = document.forms[formName];
    let retValue = form[eName];
    return retValue;
  }

  // Get a form element value.
  static FormElementValue(formName, eName)
  {
    let eItem = LJC.FormElement(formName, eName);
    let retValue = eItem.value;
    return retValue;
  }

  // Gets HTMLElements by Tag.
  static TagElements(eParent, tag)
  {
    return eParent.getElementsByTagName(tag);
  }

  // Gets the element text.
  static GetText(id)
  {
    let retValue = null;

    let eItem = LJC.Element(id);
    if (eItem != null)
    {
      retValue = eItem.innerText;
    }
    return retValue;
  }

  // Gets the element value.
  static GetValue(id)
  {
    let retValue = null;

    let eItem = LJC.Element(elementID);
    if (eItem != null)
    {
      retValue = eItem.value;
    }
    return retValue;
  }

  // Sets the element text.
  static SetText(id, text)
  {
    let eItem = LJC.Element(id);
    if (eItem != null)
    {
      eItem.innerText = text;
    }
  }

  // Sets the element value.
  static SetValue(id, value)
  {
    let eItem = LJC.Element(id);
    if (eItem != null)
    {
      eItem.value = value;
    }
  }

  // Check Functions

  // Check if an text has a value.
  static HasText(text)
  {
    let retValue = false;

    if (text
      && text != null
      && text.trim().length > 0)
    {
      retValue = true;
    }
    return retValue;
  }

  // Check if an item has a value.
  static HasValue(item)
  {
    let retValue = false;

    if (item
      && item != null)
    {
      retValue = true;
    }
    return retValue;
  }

  // Checks if the text is a number.
  static IsNumber(text)
  {
    let retValue = true;

    for (let index = 0; index < text.length; index++)
    {
      let ch = text.charAt(index);
      //let result = /^\d+$/.test(ch);
      let result = /^\d+/.test(ch);
      if (!result)
      {
        if (!"+-.".includes(ch))
        {
          retValue = false;
          break;
        }
      }
    }
    return retValue;
  }

  // Common Functions
  // --------------

  // Get the average character width.
  static AverageCharWidth(selector, text)
  {
    let font = LJC.SelectorStyle(selector, "font");
    let textWidth = LJC.TextWidth(font, text);
    let averageWidth = textWidth / text.length;
    let retValue = LJC.Round(averageWidth, 100);
    return retValue;
  }

  // Returns the index of a search item in the array.
  static BinarySearch(array, compareToValue, compareFunction)
  {
    const NotFound = -1;
    const Searching = -2;
    let retValue = NotFound;

    // Set initial bounds.
    let lowerIndex = 0;
    let upperIndex = array.length - 1;
    let nextCount = upperIndex - lowerIndex + 1;
    let index = LJC.MiddlePosition(nextCount) - 1;

    retValue = Searching;
    while (Searching == retValue)
    {
      let result = compareFunction(array[index], compareToValue);
      switch (result)
      {
        // Item was found.
        case 0:
          retValue = index;
          break;

        // Set previous index.
        case 1:
          if (1 == nextCount)
          {
            // There are no items left.
            retValue = NotFound;
            break;
          }

          // Get middle index of previous items.
          upperIndex = index;
          nextCount = upperIndex - lowerIndex;
          if (0 == nextCount)
          {
            retValue = NotFound;
          }
          index = upperIndex - LJC.MiddlePosition(nextCount);
          break;

        // Set next index.
        case -1:
          if (1 == nextCount)
          {
            // There are no items left.
            retValue = NotFound;
            break;
          }

          // Get middle index of next items.
          lowerIndex = index;
          nextCount = upperIndex - lowerIndex;
          index = lowerIndex + LJC.MiddlePosition(nextCount);
          break;
      }
    }
    return retValue;
  }

  // Gets a delimited substring.
  static DelimitedString(text, beginDelimiter, endDelimiter, begin)
  {
    let retValue = null;

    begin.Index = text.indexOf(beginDelimiter, begin.Index);
    if (begin.Index >= 0)
    {
      let endIndex = text.indexOf(endDelimiter, begin.Index + 1);
      if (endIndex > 0)
      {
        begin.Index++;
        retValue = text.substr(begin.Index, (endIndex - begin.Index));
        //begin.Index = endIndex + 1;
      }
    }
    return retValue;
  }

  // Gets the element ComputerStyle property.
  static ElementStyle(eItem, property)
  {
    let css = window.getComputedStyle(eItem, null);
    let retValue = css.getPropertyValue(property);
    return retValue;
  }

  // Gets the first matching selector ComputerStyle property.
  static SelectorStyle(selector, property)
  {
    let eItem = document.querySelector(selector);
    let retValue = LJC.ElementStyle(eItem, property);
    return retValue;
  }

  // Returns the middle position of the count value.
  static MiddlePosition(count)
  {
    let retValue = 0;

    if (0 == count % 2)
    {
      // Even length.
      retValue = count / 2;
    }
    else
    {
      // Odd length.
      let remainder = count % 2;
      retValue = (count - remainder) / 2 + 1;
    }
    return retValue;
  }

  // Rounds to provided place value.
  static Round(value, placeValue)
  {
    let retValue = value * placeValue;
    retValue = Math.round(retValue);
    retValue = retValue / placeValue;
    return retValue;
  }

  // Gets the text width.
  static TextWidth(font, text)
  {
    let canvas = document.createElement("canvas");
    let context = canvas.getContext("2d");
    context.font = font;
    let metric = context.measureText(text);
    let retValue = metric.width;
    return retValue;
  }

  // textarea Functions
  // ----------------

  // Sets the textarea rows for newlines.
  static EventTextRows(event)
  {
    let eItem = event.target;
    if ("textarea" == eItem.localName)
    {
      LJC.SetTextRows(eItem);
    }
  }

  // Gets the textarea columns.
  static GetTextCols(width, widthDivisor, fontDivisor)
  {
    let retValue = Math.ceil((width / widthDivisor) / fontDivisor);
    return retValue;
  }

  // Sets the textarea rows for newlines.
  static SetTextRows(eItem)
  {
    let count = eItem.rows;
    let matches = eItem.value.match(/\n/g);
    if (Array.isArray(matches))
    {
      count = matches.length + 1;
    }
    else
    {
      count = 1;
    }
    eItem.rows = count;
  }

  // Show Property Functions
  // ---------------------

  // Show the properties of an object that are not null or "" and
  // do not start with "on".
  static ShowProperties(location, item)
  {
    let startText = `${location}: `;

    let results = `${startText}\r\n`;
    let page = 1;
    let count = 1;
    for (let propertyName in item)
    {
      if (false == propertyName.startsWith("on")
        && item[propertyName] != null
        && item[propertyName] != "")
      {
        if (count % 12 == 0)
        {
          alert(`Page: ${page} ${results}`);
          results = `${startText}\r\n`;
          page++;
        }
        count++;
        results += LJC.AddPropertyOutput(item, propertyName);
      }
    }
    if (results != startText)
    {
      alert(`${page} ${results}`);
    }
  }

  // Show selected properties of an object.
  static ShowSelectProperties(item, typeName, startText = null
    , propertyNames = null)
  {
    if (null == propertyNames)
    {
      propertyNames = LJC.GetPropertyNames(typeName);
    }
    startText = LJC.GetStartText(typeName, startText);

    let results = `${startText}\r\n`;
    let page = 1;
    let count = 1;
    let length = propertyNames.length;
    for (let index = 0; index < length; index++)
    {
      let propertyName = propertyNames[index];
      if (count % 12 == 0)
      {
        alert(`page: ${page} ${results}`);
        results = `${startText}\r\n`;
        page++;
      }
      count++;
      results += LJC.AddPropertyOutput(item, propertyName);
    }
    if (results != startText)
    {
      alert(`page: ${page} ${results}`);
    }
  }

  // Property Helper Functions
  // -----------------------

  // Add property output to results.
  static AddPropertyOutput(item, propertyName)
  {
    let retValue = "";

    if (propertyName in item)
    {
      retValue = `${propertyName}=${item[propertyName]}\r\n`;
    }
    return retValue;
  }

  // Gets the default property names.
  static GetPropertyNames(typeName)
  {
    let retValue = null;

    switch (typeName.toLowerCase().trim())
    {
      case "window":
        retValue = [
          "parent", "document", "location", "frames", "length",
          "addEventListener", "removeEventListener"
        ];
        break;

      case "document":
        retValue = [
          "documentElement", "location", "baseURI", "body", "head",
          "id", "name",
          "nodeType", "nodeName", "hasChildNodes", "childNodes", "firstChild",
          "getElementByID", "getElementsByName",
          "getElementsByClassName", "getElementsByTagName",
          "addEventListener", "removeEventListener"
        ];
        break;

      case "element":
        retValue = [
          "id", "name",
          "localName", "tagName",
          "innerHTML", "outerHTML",
          "nodeType", "nodeName", "firstChild"
        ];
        break;

      default:
        retValue = [
          "addEventListener", "removeEventListener"
        ];
        break;
    }
    return retValue;
  }

  // Get the property list start text.
  static GetStartText(typeName, startText = null)
  {
    let retValue = null;

    if (null == startText)
    {
      retValue = `${typeName.toLowerCase().trim()}: `;
    }
    else
    {
      retValue = `${starText.trim()}: `;
    }
    return retValue;
  }
}