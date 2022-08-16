// DocGenAssemblies.cs
using System.Collections.Generic;
using System.Xml.Serialization;

namespace LJCDocLibDAL
{
  // Represents a collection of DocGenAssembly objects. 
  /// <include path='items/DocGenAssemblies/*' file='Doc/ProjectDocLibDAL.xml'/>
  [XmlRoot("DocGenAssemblies")]
  public class DocGenAssemblies : List<DocGenAssembly>
  {
    #region Public Methods

    // Creates and adds the object from the provided values.
    /// <include path='items/Add/*' file='Doc/DocGenAssemblies.xml'/>
    public DocGenAssembly Add(string name)
    {
      DocGenAssembly retValue = new DocGenAssembly()
      {
        Name = name
      };
      Add(retValue);
      return retValue;
    }

    // Finds and returns the object that contains the supplied value.
    /// <include path='items/LJCSearchBySequence/*' file='Doc/DocGenAssemblies.xml'/>
    public DocGenAssembly LJCSearchBySequence(int sequence)
    {
      DocAssemblySequenceComparer comparer;
      DocGenAssembly retValue = null;

      comparer = new DocAssemblySequenceComparer();
      LJCSortBySequence(comparer);
      if (Count != mPrevCount
        || mSortType.CompareTo(SortType.Sequence) != 0)
      {
        mPrevCount = Count;
        Sort(comparer);
        mSortType = SortType.Sequence;
      }

      DocGenAssembly searchItem = new DocGenAssembly()
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
    public DocGenAssembly LJCSearchName(string name)
    {
      DocGenAssembly retValue = null;

      LJCSortByName();
      DocGenAssembly searchItem = new DocGenAssembly()
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
    /// <include path='items/LJCSortByName/*' file='Doc/DocGenAssemblies.xml'/>
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
    /// <include path='items/LJCSortBySequence/*' file='Doc/DocGenAssemblies.xml'/>
    public void LJCSortBySequence(DocAssemblySequenceComparer comparer)
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
