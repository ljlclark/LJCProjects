// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// Sections.js
// <script src="../LJCCommon.js"></script>

// Represents a collection of items.
class Sections
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

  // The Constructor method.
  constructor()
  {
    this.ItemArray = [];
    this.Name = "Sections";
    this.PreviousCount = 1;
  }

  // Data Methods

  // Adds a new object.
  Add(name)
  {
    let item = new Section(name);
    this.ItemArray.push(item);
    let lastIndex = this.ItemArray.length - 1;
    let retValue = this.ItemArray[lastIndex];
    return retValue;
  }

  // Deletes the matching item.
  Delete(compareToValue)
  {
    let retValue = null;

    let index = this.Search(compareToValue);
    if (index >= 0)
    {
      retValue = this.ItemArray.splice(index, 1);
    }
    return retValue;
  }

  // Retrieves the matching item.
  Retrieve(compareToValue)
  {
    let retValue = null;

    let index = this.Search(compareToValue);
    if (index >= 0)
    {
      retValue = this.ItemArray[index];
    }
    return retValue;
  }

  // Retrieves the matching item index.
  Search(compareToValue)
  {
    this.Sort();
    let retValue = LJC.BinarySearch(this.ItemArray, compareToValue
      , this.Compare);
    return retValue;
  }

  // Collection Methods

  // The Name compare method.
  Compare(compareItem, compareToValue)
  {
    let compareToItem = new Section(compareToValue);
    return Sections.SortName(compareItem, compareToItem);
  }

  // Gets the element count.
  Count()
  {
    let retValue = 0;

    if (this.ItemArray
      && Array.isArray(this.ItemArray))
    {
      retValue = this.ItemArray.length;
    }
    return retValue;
  }

  // Gets an item by index.
  Items(index)
  {
    let retValue = this.ItemArray[index];
    return retValue;
  }

  // Sorts the internal array.
  Sort(sortMethod = null)
  {
    if (this.PreviousCount < this.Count())
    {
      if (null == sortMethod)
      {
        sortMethod = Sections.SortName;
      }
      this.ItemArray.sort(sortMethod);
      this.PreviousCount = this.Count();
    }
  }
}

// Represents a data object.
class Section
{
  // The Constructor method.
  constructor(name = null)
  {
    this.Name = name;
    this.BeginLineIndex = -1;
    this.RepeatItems = new RepeatItems();
  }
}
