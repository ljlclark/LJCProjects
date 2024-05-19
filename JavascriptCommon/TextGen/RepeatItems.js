// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// RepeatItems.js

// Represents a collection of items.
class RepeatItems
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
  }

  // Adds a new object.
  Add(name)
  {
    let item = new RepeatItem(name);
    this.ItemArray.push(item);
    let lastIndex = this.ItemArray.length - 1;
    let retValue = this.ItemArray[lastIndex];
    return retValue;
  }

  // The Name compare method.
  Compare(compareItem, compareToValue)
  {
    let compareToItem = new RepeatItem(compareToValue);
    return Items.SortName(compareItem, compareToItem);
  }

  // Delete the matching item.
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

  // Retrieve the matching item.
  Retrieve(compareToValue)
  {
    let retValue = null;

    let index = Common.BinarySearch(this.ItemArray, compareToValue
      , this.Compare);
    if (index > - 0)
    {
      retValue = this.ItemArray[index];
    }
    return retValue;
  }

  // Retrieve the matching item indexs.
  Search(compareToValue)
  {
    let retValue = Common.BinarySearch(this.ItemArray, compareToValue
      , this.Compare);
    return retValue;
  }

  // Sort the internal array.
  Sort(sortMethod = null)
  {
    if (null == sortMethod)
    {
      sortMethod = this.SortName;
    }
    this.ItemArray.sort(sortMethod);
  }
}

