// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// Solutions.cs
using LJCNetCommon;
using System.Collections.Generic;

namespace ProjectFilesDAL
{
  /// <summary>Represents a collection of Solution Data Objects.</summary>
  public class Solutions : List<Solution>
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../LJCGenDoc/Common/Data.xml'/>
    public Solutions()
    {
      mPrevCount = -1;
    }
    #endregion

    #region Collection Methods

    // Creates and returns a clone of the object.
    /// <include path='items/Clone/*' file='../../LJCGenDoc/Common/Data.xml'/>
    public Solutions Clone()
    {
      var retValue = MemberwiseClone() as Solutions;
      return retValue;
    }

    // Gets a custom collection from List<T>.
    /// <include path='items/GetCollection/*' file='../../LJCGenDoc/Common/Collection.xml'/>
    public Solutions GetCollection(List<Solution> list)
    {
      Solutions retValue = null;

      if (NetCommon.HasItems(list))
      {
        retValue = new Solutions();
        foreach (Solution item in list)
        {
          retValue.Add(item);
        }
      }
      return retValue;
    }
    #endregion

    #region Data Methods

    // Creates and adds the object from the provided values.
    /// <summary>
    /// 
    /// </summary>
    /// <param name="parentKey"></param>
    /// <param name="name"></param>
    /// <param name="sequence"></param>
    /// <param name="path"></param>
    /// <returns></returns>
    public Solution Add(SolutionParentKey parentKey, string name, int sequence
      , string path = null)
    {
      var message = NetString.ArgError(null, parentKey, name);
      Solution.ParentKeyValues(ref message, parentKey);
      NetString.ThrowArgError(message);

      var retValue = LJCRetrieve(parentKey, name);
      if (null == retValue)
      {
        retValue = new Solution()
        {
          CodeLine = parentKey.CodeLine,
          CodeGroup = parentKey.CodeGroup,
          Name = name,
          Sequence = sequence,
          Path = path
        };
        Add(retValue);
      }
      return retValue;
    }

    // Removes an item by unique values.
    /// <summary>
    /// Removes an item by unique values.
    /// </summary>
    /// <param name="parentKey">The ParentKey value.</param>
    /// <param name="name">The Name value.</param>
    public void LJCDelete(SolutionParentKey parentKey, string name)
    {
      var item = LJCRetrieve(parentKey, name);
      if (item != null)
      {
        Remove(item);
      }
    }

    // Retrieves a collection that match the supplied values.
    /// <summary>
    /// Retrieves a collection that match the supplied values.
    /// </summary>
    /// <param name="parentKey">The ParentKey value.</param>
    /// <returns>The collection object.</returns>
    public Solutions LJCLoad(SolutionParentKey parentKey)
    {
      Solutions retValue = null;

      if (parentKey != null)
      {
        var items = FindAll(x =>
          x.CodeLine == parentKey.CodeLine
          && x.CodeGroup == parentKey.CodeGroup);
        retValue = GetCollection(items);
      }
      return retValue;
    }

    // Retrieves the collection element with unique values.
    /// <summary>
    /// Retrieves the collection element with unique values.
    /// </summary>
    /// <param name="parentKey">The ParentKey value.</param>
    /// <param name="name">The item name.</param>
    /// <returns>A reference to the matching item.</returns>
    public Solution LJCRetrieve(SolutionParentKey parentKey, string name)
    {
      Solution retValue = null;

      LJCSortUnique();
      var searchItem = new Solution()
      {
        CodeLine = parentKey.CodeLine,
        CodeGroup = parentKey.CodeGroup,
        Name = name
      };
      int index = BinarySearch(searchItem);
      if (index > -1)
      {
        retValue = this[index];
      }
      return retValue;
    }

    // Retrieves the collection element with unique values.
    /// <summary>
    /// Retrieves the collection element with unique values.
    /// </summary>
    /// <param name="parentKey">The ParentKey value.</param>
    /// <param name="path">The Path value.</param>
    /// <returns>A reference to the matching item.</returns>
    public Solution LJCRetrieveWithPath(SolutionParentKey parentKey
      , string path)
    {
      Solution retValue = null;

      var comparer = new SolutionPath();
      LJCSortPath(comparer);
      var searchItem = new Solution()
      {
        CodeLine = parentKey.CodeLine,
        CodeGroup = parentKey.CodeGroup,
        Path = path
      };
      int index = BinarySearch(searchItem, comparer);
      if (index > -1)
      {
        retValue = this[index];
      }
      return retValue;
    }

    // Finds and updates the collection item.
    /// <summary>
    /// Finds and updates the collection item.
    /// </summary>
    /// <param name="solution">The Solution object.</param>
    public void LJCUpdate(Solution solution)
    {
      if (NetCommon.HasItems(this))
      {
        var parentKey = LJCGetParentKey(solution);
        var item = LJCRetrieve(parentKey, solution.Name);
        if (item != null)
        {
          item.Path = solution.Path;
        }
      }
    }
    #endregion

    #region Public Methods

    // Retrieves the ParentKey from the object.
    /// <summary>Retrieves the ParentKey from the object.</summary>
    /// <param name="solution">The Solution object.</param>
    /// <returns>The ParentKey object.</returns>
    public SolutionParentKey LJCGetParentKey(Solution solution)
    {
      var retValue = new SolutionParentKey()
      {
        CodeLine = solution.CodeLine,
        CodeGroup = solution.CodeGroup
      };
      return retValue;
    }

    /// <summary>Sorts on Path.</summary>
    /// <param name="comparer">The Comparer object.</param>
    public void LJCSortPath(SolutionPath comparer)
    {
      if (Count != mPrevCount
        || mSortType.CompareTo(SortType.Path) != 0)
      {
        mPrevCount = Count;
        Sort(comparer);
        mSortType = SortType.Path;
      }
    }

    /// <summary>Sorts on Sequence.</summary>
    /// <param name="comparer">The Comparer object.</param>
    public void LJCSortSequence(SolutionSequence comparer)
    {
      if (Count != mPrevCount
        || mSortType.CompareTo(SortType.Sequence) != 0)
      {
        mPrevCount = Count;
        Sort(comparer);
        mSortType = SortType.Sequence;
      }
    }

    /// <summary>Sorts on Unique values.</summary>
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

    private int mPrevCount;
    private SortType mSortType;

    private enum SortType
    {
      Unique,
      Path,
      Sequence
    }
    #endregion
  }
}
