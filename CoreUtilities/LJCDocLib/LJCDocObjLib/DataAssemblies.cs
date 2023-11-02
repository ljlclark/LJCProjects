// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DataAssemblies.cs
using LJCGenDocDAL;
using System.Collections.Generic;

namespace LJCDocObjLib
{
  // Represents a collection of DataAssembly objects.
  /// <include path='items/DataAssemblies/*' file='Doc/ProjectDocObjLib.xml'/>
  public class DataAssemblies : List<DataAssembly>
  {
    #region Search and Sort Methods

    // Returns the element with the name matching the supplied value.
    /// <include path='items/LJCSearchDescription/*' file='Doc/DataAssemblies.xml'/>
    public DataAssembly LJCSearchDescription(string description)
    {
      DataAssembly retValue = null;

      var comparer = new DataAssemblyComparer();
      LJCSortDescription(comparer);
      DataAssembly dataAssembly = new DataAssembly(null, null, null)
      {
        Description = description
      };
      int index = BinarySearch(dataAssembly, comparer);
      if (index > -1)
      {
        retValue = this[index];
      }
      return retValue;
    }

    // Retrieve the collection element with unique values.
    /// <summary>
    /// Retrieve the collection element with unique values.
    /// </summary>
    /// <param name="name">The item name.</param>
    /// <returns>A reference to the matching item.</returns>
    public DataAssembly LJCSearchUnique(string name)
    {
      DataAssembly retValue = null;

      LJCSortUnique();
      DataAssembly searchItem = new DataAssembly()
      {
        Name = name
      };
      int index = BinarySearch(searchItem);
      if (index > -1)
      {
        retValue = this[index];
      }
      return retValue;
    }

    /// <summary>Sort on Unique values.</summary>
    public void LJCSortDescription(DataAssemblyComparer comparer)
    {
      if (Count != mPrevCount
        || mSortType.CompareTo(SortType.Description) != 0)
      {
        mPrevCount = Count;
        Sort(comparer);
        mSortType = SortType.Description;
      }
    }

    /// <summary>Sort on Unique values.</summary>
    public void LJCSortUnique()
    {
      if (Count != mPrevCount
        || mSortType.CompareTo(SortType.Unique) != 0)
      {
        mPrevCount = Count;
        Sort();
        mSortType = SortType.Unique;
      }
    }
    #endregion

    #region Class Data

    // <summary>The previous count value.</summary>
    private int mPrevCount;
    private SortType mSortType;

    private enum SortType
    {
      Unique,
      Description
    }
    #endregion
  }
}
