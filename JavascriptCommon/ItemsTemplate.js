// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ItemsTemplate.js

// Represents a collection of items.
class Items
{
  // The case insensitive sort method.
  static SortName(a, b)
  {
    let retValue = 0;
    let compare = a.Name.toLowerCase();
    let compareTo = b.Name.toLowerCase();
    if (compare < compareTo)
    {
      retValue = -1;
    }
    if (compare > compareTo)
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
    let nextIndex = this.ItemArray.length;
    let item = new Item(name);
    this.ItemArray[nextIndex] = item;
  }

  // The Name compare method.
  Compare(item, compareTo)
  {
    let compareToItem = new Item(compareTo);
    return Items.SortName(item, compareToItem);
  }

  // Delete the matching item.
  Delete(compareTo)
  {
    let retValue = null;

    let index = this.Search(compareTo);
    if (index >= 0)
    {
      retValue = this.ItemArray.splice(index, 1);
    }
    return retValue;
  }

  // Retrieve the matching item.
  Retrieve(compareTo)
  {
    let retValue = null;

    let index = Common.BinarySearch(this.ItemArray, compareTo
      , this.Compare);
    if (index >- 0)
    {
      retValue = this.ItemArray[index];
    }
    return retValue;
  }

  // Retrieve the matching item indexs.
  Search(compareTo)
  {
    let retValue = Common.BinarySearch(this.ItemArray, compareTo
      , this.Compare);
    return retValue;
  }

  // Sort the internal array.
  Sort(sortMethod = null)
  {
    if (null == sortMethod)
    {
      sortMethod = Items.SortName;
    }
    this.ItemArray.sort(sortMethod);
  }
}

