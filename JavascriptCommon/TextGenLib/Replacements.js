// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// Replacements.js
// <script src="../LJCCommon.js"></script>

// Represents a collection of items.
class Replacements
{
  // The case insensitive sort method.
  static SortName(compare, compareTo)
  {
    let retValue = 0;

    let compareValue = compare.Name.toLowerCase();
    let compareToValue = compareTo.Name.toLowerCase();
    if (compareValue < compareToValue)
    {
      retValue = -1;
    }
    if (compareValue > compareToValue)
    {
      retValue = 1;
    }
    return retValue;
  }

  // Private Properties
  #ItemArray = [];

  // The Constructor method.
  constructor()
  {
    this.Name = "Replacements";
    this.PreviousCount = -1;
  }

  // Data Methods

  // Adds a new object.
  Add(name, value)
  {
    let item = new Replacement(name, value);
    this.#ItemArray.push(item);
    let lastIndex = this.#ItemArray.length - 1;
    let retValue = this.#ItemArray[lastIndex];
    return retValue;
  }

  // Delete the matching item.
  Delete(compareToValue)
  {
    let retValue = null;

    let index = this.Search(compareToValue);
    if (index >= 0)
    {
      retValue = this.#ItemArray.splice(index, 1);
    }
    return retValue;
  }

  // Retrieve the matching item.
  Retrieve(compareToValue)
  {
    let retValue = null;

    let index = this.Search(compareToValue);
    if (index >= 0)
    {
      retValue = this.#ItemArray[index];
    }
    return retValue;
  }

  // Retrieve the matching item indexs.
  Search(compareToValue)
  {
    this.Sort();
    let retValue = LJC.BinarySearch(this.#ItemArray, compareToValue
      , this.Compare);
    return retValue;
  }

  // Collection Methods

  // The Name compare method.
  Compare(compareItem, compareToValue)
  {
    let compareToItem = new Replacement(compareToValue);
    return Replacements.SortName(compareItem, compareToItem);
  }

  // Gets the element count.
  Count()
  {
    let retValue = 0;

    if (this.#ItemArray
      && Array.isArray(this.#ItemArray))
    {
      retValue = this.#ItemArray.length;
    }
    return retValue;
  }

  // Get an item by index.
  Items(index)
  {
    let retValue = this.#ItemArray[index];
    return retValue;
  }

  // Sort the internal array.
  Sort(sortMethod = null)
  {
    if (this.PreviousCount < this.Count())
    {
      if (null == sortMethod)
      {
        sortMethod = Replacements.SortName;
      }
      this.#ItemArray.sort(sortMethod);
      this.PreviousCount = this.Count();
    }
  }
}

// Represents a data object.
class Replacement
{
  // The Constructor method.
  constructor(name, value = null, dataType = "string")
  {
    this.Name = name;
    this.Value = value;
    this.DataType = dataType;
  }
}
