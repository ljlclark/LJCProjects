// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCCommon.js

// Common Static Functions
class LJC
{
  // Get Elements

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
    let eItem = this.FormElement(formName, eName);
    let retValue = eItem.value;
    return retValue;
  }

  // Gets HTMLElements by Tag.
  static TagElements(eParent, tag)
  {
    return eParent.getElementsByTagName(tag);
  }

  // Returns the index of a search item in the array.
  static BinarySearch(array, compareToValue, compareFunction)
  {
    const NotFound = -1;
    const Searching = -2;
    let retValue = NotFound;

    if (array
      && Array.isArray(array))
    {
      // Set initial bounds.
      let lowerIndex = 0;
      let upperIndex = array.length - 1;
      let nextCount = upperIndex - lowerIndex + 1;
      let index = this.MiddlePosition(nextCount) - 1;

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
    }
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
        retValue = text.substr(begin.Index, (endIndex - begin.Index) + 1);
        //begin.Index = endIndex + 1;
      }
    }
    return retValue;
  }

  // Helper Methods

  // Gets the element text.
  static GetText(id)
  {
    let retValue = null;

    let eItem = this.Element(id);
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

    let eItem = this.Element(elementID);
    if (eItem != null)
    {
      retValue = eItem.value;
    }
    return retValue;
  }

  // Check if an element has a value.
  static HasValue(eItem)
  {
    let retValue = false;

    if (eItem
      && eItem != null)
    {
      retValue = true;
    }
    return retValue;
  }

  // Sets the element text.
  static SetText(elementID, text)
  {
    let eItem = this.Element(elementID);
    if (eItem != null)
    {
      eItem.innerText = text;
    }
  }

  // Sets the textarea rows for newlines.
  static EventTextRows(event)
  {
    let eItem = event.target;
    if ("textarea" == eItem.localName)
    {
      LJC.SetTextRows(eItem);
    }
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

  // Sets the element value.
  static SetValue(elementID, value)
  {
    let eItem = this.Element(elementID);
    if (eItem != null)
    {
      eItem.value = value;
    }
  }

  // Show Property Methods

  // Show the properties of an object that are not null or "" and
  // do not start with "on".
  static ShowProperties(location, item)
  {
    if (item)
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
          results += this.AddPropertyOutput(item, propertyName);
        }
      }
      if (results != startText)
      {
        alert(`${page} ${results}`);
      }
    }
  }

  // Show selected properties of an object.
  static ShowSelectProperties(item, typeName, startText = null
    , propertyNames = null)
  {
    if (item)
    {
      if (null == propertyNames)
      {
        propertyNames = this.GetPropertyNames(typeName);
      }
      startText = this.GetStartText(typeName, startText);

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
        results += this.AddPropertyOutput(item, propertyName);
      }
      if (results != startText)
      {
        alert(`page: ${page} ${results}`);
      }
    }
  }

  // Property Helper Methods

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