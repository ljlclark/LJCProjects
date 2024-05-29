// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// Items.js
// <script src="../ArgErr.js"></script>
// <script src="../LJCCommon.js"></script>

// Represents a collection of items.
class Items
{
  // The case insensitive sort method.
  static SortName(compare, compareTo)
  {
    let retValue = 0;

    let err = new ArgError();
    err.SetContext("Sections.TextGen.TextGen(sections, lines)");
    err.IsValue(compare, "compare");
    err.IsValue(compareTo, "compareTo");
    err.ShowError();

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
    this.Err.SetContext("Sections.Add(name)");
    this.Err.IsValue(name, "name");
    this.Err.ShowError();

    let item = new Item(name);
    this.ItemArray.push(item);
    let lastIndex = this.ItemArray.length - 1;
    let retValue = this.ItemArray[lastIndex];
    return retValue;
  }

  // The Name compare method.
  Compare(compareItem, compareToValue)
  {
    let err = new ArgError();
    err.SetContext("Sections.Compare(compareItem, compareToValue)");
    err.IsValue(compareItem, "compareItem");
    err.IsValue(compareToValue, "compareToValue");
    err.ShowError();

    let compareToItem = new Item(compareToValue);
    return Items.SortName(compareItem, compareToItem);
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

    this.Err.SetContext("Sections.Delete(compareToValue)");
    this.Err.IsValue(compareToValue, "compareToValue");
    this.Err.ShowError();

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
    this.Err.SetContext("RepeatItems.Items(index)");
    this.Err.IsValue(index, "index");
    this.Err.ShowError();

    let retValue = this.ItemArray[index];
    return retValue;
  }

  // Retrieve the matching item.
  Retrieve(compareToValue)
  {
    let retValue = null;

    this.Err.SetContext("Sections.Retrieve(compareToValue)");
    this.Err.IsValue(compareToValue, "compareToValue");
    this.Err.ShowError();

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
    this.Err.SetContext("Sections.Search(compareToValue)");
    this.Err.IsValue(compareToValue, "compareToValue");
    this.Err.ShowError();

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
  // #SectionEnd Main
}

