// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCCommon.js

// Common Functions
class LJC
{
  // Gets the HTMLElement.
  static Element(elementID)
  {
    return document.getElementById(elementID);
  }

  // Get a form element.
  static FormElement(formName, elementName)
  {
    let form = document.forms[formName];
    let retValue = form[elementName];
    return retValue;
  }

  // Get a form element value.
  static FormElementValue(formName, elementName)
  {
    let element = this.FormElement(formName, elementName);
    let retValue = element.value;
    return retValue;
  }

  // Gets HTMLElements by Tag.
  static TagElements(parentElement, tag)
  {
    return parentElement.getElementsByTagName(tag);
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
            index = upperIndex - Common.MiddlePosition(nextCount);
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
            index = lowerIndex + Common.MiddlePosition(nextCount);
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

  // Gets the element text.
  static GetText(elementID)
  {
    let retValue = null;

    let element = this.Element(elementID);
    if (element != null)
    {
      retValue = element.innerText;
    }
    return retValue;
  }

  // Gets the element value.
  static GetValue(elementID)
  {
    let retValue = null;

    let element = this.Element(elementID);
    if (element != null)
    {
      retValue = element.value;
    }
    return retValue;
  }

  // Check if an element has a value.
  static HasValue(element)
  {
    let retValue = false;

    if (element
      && element != null)
    {
      retValue = true;
    }
    return retValue;
  }

  // Sets the element text.
  static SetText(elementID, text)
  {
    let element = this.Element(elementID);
    if (element != null)
    {
      element.innerText = text;
    }
  }

  // Sets the element value.
  static SetValue(elementID, value)
  {
    let element = this.Element(elementID);
    if (element != null)
    {
      element.value = value;
    }
  }

  // Show the properties of an object.
  static ShowProperties(location, item)
  {
    if (item)
    {
      let value = `location: ${location}\r\n`;
      for (let property in item)
      {
        value += `Property: ${property} - ${item[property]}\r\n`;
      }
      alert(value);
    }
  }
}