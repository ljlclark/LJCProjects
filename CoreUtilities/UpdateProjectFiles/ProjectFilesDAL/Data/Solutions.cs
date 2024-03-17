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
    /// <include path='items/DefaultConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public Solutions()
    {
      mPrevCount = -1;
    }
    #endregion

    #region Collection Methods

    // Creates and returns a clone of the object.
    /// <include path='items/Clone/*' file='../../LJCDocLib/Common/Data.xml'/>
    public Solutions Clone()
    {
      var retValue = MemberwiseClone() as Solutions;
      return retValue;
    }

    // Gets a custom collection from List<T>.
    /// <include path='items/GetCollection/*' file='../../LJCDocLib/Common/Collection.xml'/>
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
    /// <include path='items/Add/*' file='../../LJCDocLib/Common/Collection.xml'/>
    public Solution Add(SolutionParentKey parentKey, string name
      , string path = null)
    {
      Solution retValue;

      string message = "";
      NetString.AddMissingArgument(message, parentKey);
      AddMissingValues(message, parentKey);
      NetString.AddMissingArgument(message, name);
      NetString.ThrowInvalidArgument(message);

      retValue = LJCRetrieve(parentKey, name);
      if (null == retValue)
      {
        retValue = new Solution()
        {
          CodeLine = parentKey.CodeLine,
          CodeGroup = parentKey.CodeGroup,
          Name = name,
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
      var items = FindAll(x =>
        x.CodeLine == parentKey.CodeLine
        && x.CodeGroup == parentKey.CodeGroup);
      var retValue = GetCollection(items);
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

    // Adds the missing ParentKey values messages.
    /// <summary>
    /// Adds the missing ParentKey values messages.
    /// </summary>
    /// <param name="message">The message value.</param>
    /// <param name="parentKey">The ParentKey object.</param>
    public void AddMissingValues(string message, SolutionParentKey parentKey)
    {
      if (parentKey != null)
      {
        if (!NetString.HasValue(parentKey.CodeLine))
        {
          message += $"{parentKey.CodeLine} is missing.";
        }
        if (!NetString.HasValue(parentKey.CodeGroup))
        {
          message += $"{parentKey.CodeGroup} is missing.";
        }
      }
    }

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

    /// <summary>Sorts on Unique values.</summary>
    public void LJCSortUnique()
    {
      if (Count != mPrevCount)
      {
        mPrevCount = Count;
        Sort();
      }
    }
    #endregion

    #region Class Data

    private int mPrevCount;
    #endregion
  }
}
