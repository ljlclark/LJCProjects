// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DocGenGroups.cs
using System.Collections.Generic;
using System.Xml.Serialization;

namespace LJCGenDocDAL
{
  // Represents a collection of DocGenGroup objects. 
  /// <include path='items/DocGenGroups/*' file='Doc/DocGenGroups.xml'/>
  [XmlRoot("DocGenGroups")]
  public class DocGenGroups : List<DocGenGroup>
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../LJCGenDoc/Common/Data.xml'/>
    public DocGenGroups()
    {
      //mSortType = SortType.None;
    }
    #endregion

    #region Public Methods

    // Creates and adds the object from the provided values.
    /// <include path='items/Add/*' file='Doc/DocGenGroups.xml'/>
    public DocGenGroup Add(string name)
    {
      DocGenGroup retValue = new DocGenGroup()
      {
        Name = name
      };
      Add(retValue);
      return retValue;
    }

    // Finds and returns the object that contains the supplied value.
    /// <include path='items/LJCSearchBySequence/*' file='Doc/DocGenGroups.xml'/>
    public DocGenGroup LJCSearchBySequence(int sequence)
    {
      DocGenGroupSequenceComparer comparer;
      DocGenGroup retValue = null;

      comparer = new DocGenGroupSequenceComparer();
      LJCSortBySequence(comparer);
      if (Count != mPrevCount
        || mSortType.CompareTo(SortType.Sequence) != 0)
      {
        mPrevCount = Count;
        Sort(comparer);
        mSortType = SortType.Sequence;
      }

      DocGenGroup searchItem = new DocGenGroup()
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
    /// <include path='items/LJCSearchName/*' file='../../LJCGenDoc/Common/Collection.xml'/>
    public DocGenGroup LJCSearchName(string name)
    {
      DocGenGroup retValue = null;

      LJCSortByName();
      var searchItem = new DocGenGroup()
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
    public void LJCSortBySequence(DocGenGroupSequenceComparer comparer)
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
