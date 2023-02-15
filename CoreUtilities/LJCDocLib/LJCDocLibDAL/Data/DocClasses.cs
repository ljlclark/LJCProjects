// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DocClasses.cs
using System.Collections.Generic;

namespace LJCDocLibDAL
{
  /// <summary>
  /// 
  /// </summary>
  public class DocClasses : List<DocClass>
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public DocClasses()
    {
      //mSortType = SortType.None;
    }
    #endregion

    #region Public Methods

    // Creates and adds the object from the provided values.
    /// <include path='items/Add/*' file='Doc/DocGenGroups.xml'/>
    public DocClass Add(string name)
    {
      var retValue = new DocClass()
      {
        Name = name
      };
      Add(retValue);
      return retValue;
    }

    // Finds and returns the object that contains the supplied value.
    /// <include path='items/LJCSearchBySequence/*' file='Doc/DocGenGroups.xml'/>
    public DocClass LJCSearchBySequence(int sequence)
    {
      DocClassSequenceComparer comparer;
      DocClass retValue = null;

      comparer = new DocClassSequenceComparer();
      LJCSortBySequence(comparer);
      if (Count != mPrevCount
        || mSortType.CompareTo(SortType.Sequence) != 0)
      {
        mPrevCount = Count;
        Sort(comparer);
        mSortType = SortType.Sequence;
      }

      var searchItem = new DocClass()
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
    public DocClass LJCSearchName(string name)
    {
      DocClass retValue = null;

      LJCSortByName();
      var searchItem = new DocClass()
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
    public void LJCSortBySequence(DocClassSequenceComparer comparer)
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
