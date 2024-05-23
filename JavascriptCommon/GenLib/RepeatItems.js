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

    let Err = new ArgError();
    Err.SetContext("RepeatItems.TextGen.TextGen(sections, lines)");
    Err.IsValue(compare, "compare");
    Err.IsValue(compareTo, "compareTo");

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
    this.Err = new ArgError();
    this.ItemArray = [];
  }

  // Adds a new object.
  Add(name)
  {
    this.Err.SetContext("RepeatItems.Add(name)");
    this.Err.IsValue(name, "name");

    let item = new RepeatItem(name);
    this.ItemArray.push(item);
    let lastIndex = this.ItemArray.length - 1;
    let retValue = this.ItemArray[lastIndex];
    return retValue;
  }

  // The Name compare method.
  Compare(compareItem, compareToValue)
  {
    let Err = new ArgError();
    Err.SetContext("RepeatItems.Compare(compareItem, compareToValue)");
    Err.IsValue(compareItem, "compareItem");
    Err.IsValue(compareToValue, "compareToValue");

    let compareToItem = new RepeatItem(compareToValue);
    return RepeatItems.SortName(compareItem, compareToItem);
  }

  // 
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

    this.Err.SetContext("RepeatItems.Delete(compareToValue)");
    this.Err.IsValue(compareToValue, "compareToValue");

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

    this.Err.SetContext("RepeatItems.Items(index)");
    this.Err.IsValue(index, "index");

    return retValue;
  }

  // Retrieve the matching item.
  Retrieve(compareToValue)
  {
    let retValue = null;

    this.Err.SetContext("RepeatItems.Retrieve(compareToValue)");
    this.Err.IsValue(compareToValue, "compareToValue");

    let index = LJC.BinarySearch(this.ItemArray, compareToValue
      , this.Compare);
    if (index >= 0)
    {
      retValue = this.ItemArray[index];
    }
    return retValue;
  }

  // Retrieve the matching item indexs.
  Search(compareToValue)
  {
    this.Err.SetContext("RepeatItems.Search(compareToValue)");
    this.Err.IsValue(compareToValue, "compareToValue");

    let retValue = LJC.BinarySearch(this.ItemArray, compareToValue
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

// Represents a data object.
class RepeatItem
{
  // The Constructor method.
  constructor(name = null)
  {
    this.Name = name;
    this.Replacements = new Replacements();
  }
}
