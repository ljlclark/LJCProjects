﻿// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// #CommentChars //
// #PlaceholderBegin _
// #PlaceholderEnd _
// #SectionBegin Main
// #Value _CollectionName_
// #Value _ClassName_
// _CollectionName_.js
// <script src="LJCCommon.js"></script>

// Represents a collection of items.
class _CollectionName_
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
    let item = new _ClassName_(name);
    this.ItemArray.push(item);
    let lastIndex = this.ItemArray.length - 1;
    let retValue = this.ItemArray[lastIndex];
    return retValue;
  }

  // The Name compare method.
  Compare(compareItem, compareToValue)
  {
    let compareToItem = new _ClassName_(compareToValue);
    return _CollectionName_.SortName(compareItem, compareToItem);
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

  // Get an item by index.
  Items(index)
  {
    let retValue = this.ItemArray[index];
    return retValue;
  }

  // Retrieve the matching item.
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

  // Retrieve the matching item indexs.
  Search(compareToValue)
  {
    let retValue = LJC.BinarySearch(this.ItemArray, compareToValue
      , this.Compare);
    return retValue;
  }

  // Sort the internal array.
  Sort(sortMethod = null)
  {
    if (null == sortMethod)
    {
      sortMethod = _CollectionName_.SortName;
    }
    this.ItemArray.sort(sortMethod);
  }
  // #SectionEnd Main
}

