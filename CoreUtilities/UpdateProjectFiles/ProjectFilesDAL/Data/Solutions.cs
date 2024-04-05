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

    // Creates and adds the object with the provided values.
    /// <include path='items/Add/*' file='../Doc/Solutions.xml'/>
    public Solution Add(SolutionParentKey parentKey, string name, int sequence
      , string path = null)
    {
      // Do not add duplicate of existing item.
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
    /// <include path='items/LJCDelete/*' file='../Doc/Solutions.xml'/>
    public void LJCDelete(SolutionParentKey parentKey, string name)
    {
      var item = LJCRetrieve(parentKey, name);
      if (item != null)
      {
        Remove(item);
      }
    }

    // Retrieves a collection that match the supplied values.
    /// <include path='items/LJCLoad/*' file='../Doc/Solutions.xml'/>
    public Solutions LJCLoad(SolutionParentKey parentKey = null)
    {
      Solutions retValue = null;

      if (null == parentKey)
      {
        retValue = Clone();
      }
      else
      {
        string message = "";
        Solution.ParentKeyValues(ref message, parentKey);
        NetString.ThrowArgError(message);

        var items = FindAll(x =>
          x.CodeLine == parentKey.CodeLine
          && x.CodeGroup == parentKey.CodeGroup);
        retValue = GetCollection(items);
      }
      return retValue;
    }

    // Retrieves the collection element with unique values.
    /// <include path='items/LJCRetrieve/*' file='../Doc/Solutions.xml'/>
    public Solution LJCRetrieve(SolutionParentKey parentKey, string name)
    {
      Solution retValue = null;

      string message = "";
      string context = ClassContext + "LJCRetrieve()";
      NetString.ArgError(ref message, parentKey, context);
      NetString.ArgError(ref message, name);
      Solution.ParentKeyValues(ref message, parentKey);
      NetString.ThrowArgError(message);

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
    /// <include path='items/LJCRetrieveWithPath/*' file='../Doc/Solutions.xml'/>
    public Solution LJCRetrieveWithPath(SolutionParentKey parentKey
      , string path)
    {
      Solution retValue = null;

      string message = "";
      string context = ClassContext + "LJCRetrieveWithPath()";
      NetString.ArgError(ref message, parentKey, context);
      NetString.ArgError(ref message, path);
      Solution.ParentKeyValues(ref message, parentKey);
      NetString.ThrowArgError(message);

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
    /// <include path='items/LJCUpdate/*' file='../Doc/Solutions.xml'/>
    public void LJCUpdate(Solution solution)
    {
      if (NetCommon.HasItems(this))
      {
        string message = "";
        string context = ClassContext + "LJCUpdate()";
        NetString.ArgError(ref message, solution, context);
        Solution.ItemValues(ref message, solution);
        NetString.ThrowArgError(message);

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
    /// <include path='items/LJCGetParentKey/*' file='../Doc/Solutions.xml'/>
    public SolutionParentKey LJCGetParentKey(Solution solution)
    {
      string message = "";
      string context = ClassContext + "LJCGetParentKey()";
      NetString.ArgError(ref message, solution, context);
      Solution.ItemParentValues(ref message, solution);
      NetString.ThrowArgError(message);

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
      string message = "";
      string context = ClassContext + "LJCSortPath()";
      NetString.ArgError(ref message, comparer, context);
      NetString.ThrowArgError(message);

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
      string message = "";
      string context = ClassContext + "LJCSortSequence()";
      NetString.ArgError(ref message, comparer, context);
      NetString.ThrowArgError(message);

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
    private const string ClassContext = "ProjectFilesDAL.Solutions.";
    #endregion
  }
}
