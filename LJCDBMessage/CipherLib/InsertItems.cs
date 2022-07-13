﻿// Copyright (c) Lester J. Clark 2021,2022 - All Rights Reserved
using System;
using System.Collections.Generic;

namespace CipherLib
{
  /// <summary>Represents a collection of InsertItem objects.</summary>
  public class InsertItems : List<InsertItem>
  {
    #region Static Functions

    // Checks if the collection has items.
    /// <include path='items/HasItems1/*' file='../../LJCDocLib/Common/Collection.xml'/>
    public static bool HasItems(InsertItems collectionObject)
    {
      bool retValue = false;

      if (collectionObject != null && collectionObject.Count > 0)
      {
        retValue = true;
      }
      return retValue;
    }
    #endregion

    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public InsertItems()
    {
      mPrevCount = -1;
    }

    // The Copy constructor.
    /// <include path='items/CopyConstructor/*' file='../../LJCDocLib/Common/Collection.xml'/>
    public InsertItems(InsertItems items)
    {
      if (HasItems(items))
      {
        foreach (var item in items)
        {
          Add(new InsertItem(item));
        }
      }
    }
    #endregion

    #region Collection Methods

    // Creates and adds the element from the supplied values.
    /// <include path='items/Add/*' file='Doc/InsertItems.xml'/>
    public InsertItem Add(string name, int insertIndex, byte[] insertValue)
    {
      var retValue = new InsertItem()
      {
        Name = name,
        InsertIndex = insertIndex,
        InsertValue = insertValue
      };
      Add(retValue);
      return retValue;
    }
    #endregion

    #region Sort and Search Methods

    // Retrieve the collection element with name.
    /// <include path='items/LJCSearchName/*' file='../../LJCDocLib/Common/Collection.xml'/>
    public InsertItem LJCSearchName(string name)
    {
      InsertItemNameComparer comparer;
      InsertItem retValue = null;

      comparer = new InsertItemNameComparer();
      LJCSortName(comparer);
      InsertItem searchItem = new InsertItem()
      {
        Name = name
      };
      int index = BinarySearch(searchItem, comparer);
      if (index > -1)
      {
        retValue = this[index];
      }
      return retValue;
    }

    /// <summary>Sort on InsertIndex.</summary>
    public void LJCSortIndex()
    {
      if (Count != mPrevCount
        || mSortType.CompareTo(SortType.Sequence) != 0)
      {
        mPrevCount = Count;
        Sort();
        mSortType = SortType.Sequence;
      }
    }

    /// <summary>Sort on Name.</summary>
    public void LJCSortName(InsertItemNameComparer comparer)
    {
      if (Count != mPrevCount
        || mSortType.CompareTo(SortType.Name) != 0)
      {
        mPrevCount = Count;
        Sort(comparer);
        mSortType = SortType.Name;
      }
    }
    #endregion

    #region Class Data

    private int mPrevCount;
    private SortType mSortType;

    private enum SortType
    {
      Sequence,
      Name
    }
    #endregion
  }
}