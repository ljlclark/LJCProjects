// Copyright (c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// DataTablesNew.cs
using LJCNetCommon;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace LJCDataUtilityDAL
{
  // Represents a collection of DataTable objects.
  /// <include file='Doc/DataTablesNew.xml'
  ///  path='members/DataTablesNew/*'/>
  [XmlRoot("DataTables")]
  public class DataTablesNew : List<DataUtilTableNew>
  {
    #region Constructors

    // Initializes an object instance.
    /// <include file='../../LJCGenDoc/Common/Data.xml'
    ///  path='members/Constructor/*'/>
    public DataTablesNew()
    {
      mArgError = new ArgError("LJCDataUtilityDAL.DataTables");
      //mPrevCount = -1;
    }

    // The Copy constructor.
    /// <include file='../../LJCGenDoc/Common/Collection.xml'
    ///  path='members/CopyConstructor/*'/>
    public DataTablesNew(DataTablesNew items)
    {
      if (NetCommon.HasItems(items))
      {
        foreach (var item in items)
        {
          Add(new DataUtilTableNew(item));
        }
      }
    }
    #endregion

    #region Collection Methods

    // Creates and returns a clone of the object.
    /// <include file='../../LJCGenDoc/Common/Data.xml'
    ///  path='members/Clone/*'/>
    public DataTablesNew Clone()
    {
      var retValue = MemberwiseClone() as DataTablesNew;
      return retValue;
    }

    // Get custom collection from List<T>.
    /// <include file='../../LJCGenDoc/Common/Collection.xml'
    ///  path='members/LJCGetCollection/*'/>
    public DataTablesNew LJCGetCollection(List<DataUtilTableNew> list)
    {
      DataTablesNew retValue = null;

      if (NetCommon.HasItems(list))
      {
        retValue = new DataTablesNew();
        foreach (DataUtilTableNew item in list)
        {
          retValue.Add(item);
        }
      }
      return retValue;
    }

    // Retrieves the collection element with unique values.
    /// <include file='Doc/DataTablesNew.xml'
    ///  path='members/LJCGetWithName/*'/>
    public DataUtilTableNew LJCGetWithName(string name)
    {
      DataUtilTableNew retDataTable = null;

      var comparer = new DataTableUniqueComparerNew();
      LJCSortUnique(comparer);

      var foundIndex = BinarySearchColumns(this, "Name", name);
      if (foundIndex != -1)
      {
        retDataTable = this[foundIndex];
      }
      return retDataTable;
    }
    #endregion

    #region Search and Sort Methods

    // Custom binary search for Name value.
    /// <include file='Doc/DataTablesNew.xml'
    ///  path='members/BinarySearchColumns/*'/>
    public int BinarySearchColumns(DataTablesNew items, string propertyName
      , string searchValue)
    {
      int retIndex = -1;

      int leftIndex = 0;
      int rightIndex = items.Count - 1;
      while (leftIndex <= rightIndex)
      {
        // Get the midpoint.
        int middleIndex = leftIndex + (rightIndex - leftIndex) / 2;

        // Get the object compare value.
        var middleItem = items[middleIndex];
        var itemName = middleItem.LJCGetString(propertyName);

        // Case sensitive.
        //var compareValue = itemName.CompareTo(searchName);
        // Not case sensitive.
        var compareValue = string.Compare(itemName, searchValue, true);
        if (NetString.CompareEqual == compareValue)
        {
          retIndex = middleIndex;
          break;
        }

        if (NetString.CompareLess == compareValue)
        {
          // Eliminate left half
          leftIndex = middleIndex + 1;
        }
        else
        {
          // Eliminate right half
          rightIndex = middleIndex - 1;
        }
      }
      return retIndex;
    }

    /// <summary>Sort on Unique values.</summary>
    /// <param name="comparer">The Comparer object.</param>
    /// <include file='Doc/DataTablesNew.xml'
    ///  path='members/LJCSortUnique/*'/>
    public void LJCSortUnique(DataTableUniqueComparerNew comparer)
    {
      if (Count != mPrevCount
        || mSortType.CompareTo(SortType.Unique) != 0)
      {
        mPrevCount = Count;
        Sort(comparer);
        mSortType = SortType.Unique;
      }
    }
    #endregion

    #region Properties

    // The item for the supplied name.
    /// <include file='Doc/DataTablesNew.xml'
    ///  path='members/NameIndexer/*'/>
    public DataUtilTableNew this[int dataTableID, string name]
    {
      get
      {
        return null;
        //return LJCSearchUnique(dataTableID, name);
      }
    }
    #endregion

    #region Class Data

    private readonly ArgError mArgError;

    private int mPrevCount;
    private SortType mSortType;

    private enum SortType
    {
      ID,
      Unique
    }
    #endregion
  }
}

