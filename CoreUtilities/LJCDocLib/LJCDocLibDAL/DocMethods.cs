// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DocMethods.cs
using System.Collections.Generic;

namespace LJCDocLibDAL
{
  /// <summary>
  /// 
  /// </summary>
  public class DocMethods : List<DocMethod>
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public DocMethods()
    {
      //mSortType = SortType.None;
    }
    #endregion

    #region Public Methods

    // Creates and adds the object from the provided values.
    /// <include path='items/Add/*' file='Doc/DocGenGroups.xml'/>
    public DocMethod Add(string name)
    {
      var retValue = new DocMethod()
      {
        Name = name
      };
      Add(retValue);
      return retValue;
    }

    // Finds and returns the object that contains the supplied value.
    /// <include path='items/LJCSearchBySequence/*' file='Doc/DocGenGroups.xml'/>
    public DocMethod LJCSearchBySequence(int sequence)
    {
      DocMethodSequenceComparer comparer;
      DocMethod retValue = null;

      comparer = new DocMethodSequenceComparer();
      LJCSortBySequence(comparer);
      if (Count != mPrevCount
        || mSortType.CompareTo(SortType.Sequence) != 0)
      {
        mPrevCount = Count;
        Sort(comparer);
        mSortType = SortType.Sequence;
      }

      var searchItem = new DocMethod()
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
    public DocMethod LJCSearchName(string name)
    {
      DocMethod retValue = null;

      LJCSortByName();
      var searchItem = new DocMethod()
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
    public void LJCSortBySequence(DocMethodSequenceComparer comparer)
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
