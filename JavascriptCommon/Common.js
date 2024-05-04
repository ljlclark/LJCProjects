// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// Common.js

// Common Static Functions
class Common
{
  // Gets the HTMLElement.
  static Element(elementID)
  {
    return document.getElementById(elementID);
  }

  // Check if an item has a value.
  static HasValue(element)
  {
    var retValue = false;

    if (element
      && element != null)
    {
      retValue = true;
    }
    return retValue;
  }

  // Returns the index of a search item in the array.
  static BinarySearch(array, sortFunction, compareFunction, showAlerts = false)
  {
    var retValue = -1;

    if (array
      && Array.isArray(array))
    {
      array.sort(sortFunction);

      // Set initial bounds
      let lowerIndex = 0;
      let upperIndex = array.length - 1;
      let nextCount = upperIndex - lowerIndex + 1;
      let index = this.MiddlePosition(nextCount) - 1;

      retValue = -2;
      while (-2 == retValue)
      {
        if (showAlerts)
        {
          alert(`Index: ${index}`);
        }

        let result = compareFunction(array[index]);
        switch (result)
        {
          // Item was found.
          case 0:
            retValue = index;
            if (showAlerts)
            {
              alert(`Found: index: ${index}`);
            }
            break;

          // Set previous index.
          case 1:
            if (1 == nextCount)
            {
              // There are no items left.
              retValue = -1;
              break;
            }

            // Get middle index of previous items.
            upperIndex = index;
            nextCount = upperIndex - lowerIndex;
            if (0 == nextCount)
            {
              retValue = NotFound;
            }
            index = upperIndex - this.MiddlePosition(nextCount);
            break;

          // Set next index.
          case -1:
            if (1 == nextCount)
            {
              // There are no items left.
              retValue = -1;
              break;
            }

            // Get middle index of next items.
            lowerIndex = index;
            nextCount = upperIndex - lowerIndex;
            index = lowerIndex + this.MiddlePosition(nextCount);
            break;
        }
      }
    }
    return retValue;
  }

  // Returns the middle position of the count value.
  static MiddlePosition(count)
  {
    var retValue = 0;
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