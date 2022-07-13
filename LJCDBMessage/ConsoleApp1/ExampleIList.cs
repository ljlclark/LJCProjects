// Copyright (c) Lester J. Clark 2021,2022 - All Rights Reserved
using System;
using System.Collections;
using System.Collections.Generic;

namespace ConsoleApp1
{
  // Represents a collection of objects that can be individually accessed
  // by index.
  /// <include path='items/ExampleIList/*' file='Doc/ExampleIList.xml'/>
  public class ExampleIList : IList<string>
  {
    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public ExampleIList()
    {
      Items = new List<string>();
    }

    // Gets or sets the element at the specified index.
    /// <include path='items/this/*' file='Doc/ExampleIList.xml'/>
    public string this[int index]
    {
      get
      {
        string retValue = null;

        if (index >= 0 && index < Count)
        {
          retValue = Items[index];
        }
        return retValue;
      }
      set
      {
        if (index >= 0 && index < Count)
        {
          Items[index] = value;
        }
      }
    }

    #region Public Methods

    // Gets the number of elements contained in the ICollection<T>.
    /// <include path='items/Count/*' file='Doc/ExampleIList.xml'/>
    public int Count
    {
      get { return Items.Count; }
    }

    // Gets a value indicating whether the ICollection<T> is read-only.
    /// <include path='items/IsReadOnly/*' file='Doc/ExampleIList.xml'/>
    public bool IsReadOnly { get; set; }

    // Adds an item to the ICollection<T>.
    /// <include path='items/Add/*' file='Doc/ExampleIList.xml'/>
    public void Add(string item)
    {
      Items.Add(item);
    }

    // Removes all items from the ICollection<T>.
    /// <include path='items/Clear/*' file='Doc/ExampleIList.xml'/>
    public void Clear()
    {
      Items.Clear();
    }

    // Determines whether the ICollection<T> contains a specific value.
    /// <include path='items/Contains/*' file='Doc/ExampleIList.xml'/>
    public bool Contains(string item)
    {
      bool retValue = false;

      if (Items.Contains(item))
      {
        retValue = true;
      }
      return retValue;
    }

    // Copies the elements of the ICollection<T> to an Array, starting at a
    // particular Array index.
    /// <include path='items/CopyTo/*' file='Doc/ExampleIList.xml'/>
    public void CopyTo(string[] array, int arrayIndex = -1)
    {
      if (-1 == arrayIndex)
      {
        arrayIndex = 0;
      }
      Items.CopyTo(array, arrayIndex);
    }

    // Returns an enumerator that iterates through a collection.
    /// <include path='items/GetEnumerator1/*' file='Doc/ExampleIList.xml'/>
    public IEnumerator<string> GetEnumerator()
    {
      //return ((IEnumerable<string>)Items).GetEnumerator();
      return Items.GetEnumerator();
    }

    // Determines the index of a specific item in the IList<T>.
    /// <include path='items/IndexOf/*' file='Doc/ExampleIList.xml'/>
    public int IndexOf(string item)
    {
      return Items.IndexOf(item);
    }

    // Inserts an item to the IList<T> at the specified index.
    /// <include path='items/Insert/*' file='Doc/ExampleIList.xml'/>
    public void Insert(int index, string item)
    {
      Items.Insert(index, item);
    }

    // Removes the first occurrence of a specific object from the ICollection<T>.
    /// <include path='items/Remove/*' file='Doc/ExampleIList.xml'/>
    public bool Remove(string item)
    {
      bool retValue;

      retValue = Items.Remove(item);
      return retValue;
    }

    // Removes the IList<T> item at the specified index.
    /// <include path='items/RemoveAt/*' file='Doc/ExampleIList.xml'/>
    public void RemoveAt(int index)
    {
      Items.RemoveAt(index);
    }
    #endregion

    // 
    IEnumerator IEnumerable.GetEnumerator()
    {
      //return ((IEnumerable<string>)Items).GetEnumerator();
      return Items.GetEnumerator();
    }

    #region Class Data

    private List<string> Items { get; set; }
    #endregion
  }
}
