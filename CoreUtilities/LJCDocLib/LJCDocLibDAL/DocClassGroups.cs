// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DocClassGroups.cs
using System.Collections.Generic;

namespace LJCDocLibDAL
{
  /// <summary>
  /// 
  /// </summary>
  public class DocClassGroups : List<DocClassGroup>
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public DocClassGroups()
    {
      //mSortType = SortType.None;
    }
    #endregion

    #region Public Methods

    // Creates and adds the object from the provided values.
    /// <include path='items/Add/*' file='Doc/DocGenGroups.xml'/>
    public DocClassGroup Add(string name)
    {
      var retValue = new DocClassGroup()
      {
        Name = name
      };
      Add(retValue);
      return retValue;
    }

    // Finds and returns the object that contains the supplied value.
    /// <include path='items/LJCSearchBySequence/*' file='Doc/DocGenGroups.xml'/>
    public DocClassGroup LJCSearchBySequence(int sequence)
    {
      DocClassGroupSequenceComparer comparer;
      DocClassGroup retValue = null;

      comparer = new DocClassGroupSequenceComparer();
      LJCSortBySequence(comparer);
      if (Count != mPrevCount
        || mSortType.CompareTo(SortType.Sequence) != 0)
      {
        mPrevCount = Count;
        Sort(comparer);
        mSortType = SortType.Sequence;
      }

      var searchItem = new DocClassGroup()
      {
        Sequence = sequence
      };
      int index = BinarySearch(searchItem, comparer);
      if (index > -1)
      {
        retValue = this[index];
      }
      return retValue;
    }

    // Retrieve the collection element with name.
    /// <include path='items/LJCSearchName/*' file='../../LJCDocLib/Common/Collection.xml'/>
    public DocClassGroup LJCSearchName(string name)
    {
      DocClassGroup retValue = null;

      LJCSortByName();
      var searchItem = new DocClassGroup()
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

    // Sorts the collection by Name.
    /// <include path='items/LJCSortByName/*' file='Doc/DocGenGroups.xml'/>
    public void LJCSortByName()
    {
      if (Count != mPrevCount
        || mSortType.CompareTo(SortType.Name) != 0)
      {
        mPrevCount = Count;
        Sort();
        mSortType = SortType.Name;
      }
    }

    // Sorts the collection by Sequence.
    /// <include path='items/LJCSortBySequence/*' file='Doc/DocGenGroups.xml'/>
    public void LJCSortBySequence(DocClassGroupSequenceComparer comparer)
    {
      if (Count != mPrevCount
        || mSortType.CompareTo(SortType.Sequence) != 0)
      {
        mPrevCount = Count;
        Sort(comparer);
        mSortType = SortType.Sequence;
      }
    }
    #endregion

    #region Class Data

    private int mPrevCount;
    private SortType mSortType;

    private enum SortType
    {
      None,
      Name,
      Sequence
    }
    #endregion
  }
}
