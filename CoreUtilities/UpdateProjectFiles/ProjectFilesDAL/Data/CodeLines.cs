﻿// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// CodeLines.cs
using LJCNetCommon;
using System.Collections.Generic;

namespace ProjectFilesDAL
{
  /// <summary>Represents a collection of CodeLine Data Objects.</summary>
  public class CodeLines : List<CodeLine>
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../LJCGenDoc/Common/Data.xml'/>
    public CodeLines()
    {
      mPrevCount = -1;
    }
    #endregion

    #region Collection Methods

    // Creates and returns a clone of the object.
    /// <include path='items/Clone/*' file='../../LJCGenDoc/Common/Data.xml'/>
    public CodeLines Clone()
    {
      var retValue = MemberwiseClone() as CodeLines;
      return retValue;
    }

    // Gets a custom collection from List<T>.
    /// <include path='items/GetCollection/*' file='../../LJCGenDoc/Common/Collection.xml'/>
    public CodeLines GetCollection(List<CodeLine> list)
    {
      CodeLines retValue = null;

      if (NetCommon.HasItems(list))
      {
        retValue = new CodeLines();
        foreach (CodeLine item in list)
        {
          retValue.Add(item);
        }
      }
      return retValue;
    }
    #endregion

    #region Data Methods

    // Creates and adds the object with the provided values.
    /// <include path='items/Add/*' file='Doc/CodeLines.xml'/>
    public CodeLine Add(string name, string path = null)
    {
      CodeLine retValue;

      // Do not add duplicate of existing item.
      retValue = LJCRetrieve(name);
      if (null == retValue)
      {
        retValue = new CodeLine()
        {
          Name = name,
          Path = path
        };
        Add(retValue);
      }
      return retValue;
    }

    // Removes an item by unique values.
    /// <include path='items/LJCDelete/*' file='Doc/CodeLines.xml'/>
    public void LJCDelete(string name)
    {
      CodeLine item = LJCRetrieve(name);
      if (item != null)
      {
        Remove(item);
      }
    }

    // Retrieves the collection element with unique values.
    /// <include path='items/LJCRetrieve/*' file='Doc/CodeLines.xml'/>
    public CodeLine LJCRetrieve(string name)
    {
      CodeLine retValue = null;

      var message = NetString.ArgError(null, name);
      NetString.ThrowArgError(message);

      LJCSortUnique();
      CodeLine searchItem = new CodeLine()
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

    // Retrieve the collection element with Path.
    /// <include path='items/LJCRetrieveWithPath/*' file='Doc/CodeLines.xml'/>
    public CodeLine LJCRetrieveWithPath(string path)
    {
      CodeLine retValue = null;

      var message = NetString.ArgError(null, path);
      NetString.ThrowArgError(message);

      var comparer = new CodeLinePathComparer();
      LJCSortPath(comparer);
      CodeLine searchItem = new CodeLine()
      {
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
    /// <include path='items/LJCUpdate/*' file='Doc/CodeLines.xml'/>
    public void LJCUpdate(CodeLine codeLine)
    {
      if (NetCommon.HasItems(this))
      {
        var message = NetString.ArgError(null, codeLine);
        NetString.ThrowArgError(message);

        var item = LJCRetrieve(codeLine.Name);
        if (item != null)
        {
          item.Path = codeLine.Path;
        }
      }
    }
    #endregion

    #region Public Methods

    /// <summary>Sorts on Path.</summary>
    public void LJCSortPath(CodeLinePathComparer comparer)
    {
      var message = NetString.ArgError(null, comparer);
      NetString.ThrowArgError(message);

      if (Count != mPrevCount
        || mSortType.CompareTo(SortType.Path) != 0)
      {
        mPrevCount = Count;
        Sort(comparer);
        mSortType = SortType.Path;
      }
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
    private SortType mSortType;

    private enum SortType
    {
      Unique,
      Path
    }
    #endregion
  }
}
