﻿// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// CodeGroups.cs
using LJCNetCommon;
using System.Collections.Generic;

namespace ProjectFilesDAL
{
  /// <summary>Represents a collection of CodeGroup Data Objects.</summary>
  public class CodeGroups : List<CodeGroup>
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../LJCGenDoc/Common/Data.xml'/>
    public CodeGroups()
    {
      ArgError = new ArgError("ProjectFilesDAL.CodeGroups");
      mPrevCount = -1;
    }
    #endregion

    #region Collection Methods

    // Creates and returns a clone of the object.
    /// <include path='items/Clone/*' file='../../LJCGenDoc/Common/Data.xml'/>
    public CodeGroups Clone()
    {
      var retValue = MemberwiseClone() as CodeGroups;
      return retValue;
    }

    // Gets a custom collection from List<T>.
    /// <include path='items/GetCollection/*' file='../../LJCGenDoc/Common/Collection.xml'/>
    public CodeGroups GetCollection(List<CodeGroup> list)
    {
      CodeGroups retValue = null;

      if (NetCommon.HasItems(list))
      {
        retValue = new CodeGroups();
        foreach (CodeGroup item in list)
        {
          retValue.Add(item);
        }
      }
      return retValue;
    }
    #endregion

    #region Data Methods

    // Creates and adds the object with the provided values.
    /// <include path='items/Add/*' file='Doc/CodeGroups.xml'/>
    public CodeGroup Add(string codeLine, string name, string path = null)
    {
      CodeGroup retValue;

      ArgError.MethodName = "Add(codeLine, name, path)";
      ArgError.Add(codeLine, "codeLine");
      ArgError.Add(name, "name");
      NetString.ThrowArgError(ArgError.ToString());

      // Do not add duplicate of existing item.
      retValue = LJCRetrieve(codeLine, name);
      if (null == retValue)
      {
        retValue = new CodeGroup()
        {
          CodeLine = codeLine,
          Name = name,
          Path = path
        };
        Add(retValue);
      }
      return retValue;
    }

    // Removes an item by unique values.
    /// <include path='items/LJCDelete/*' file='Doc/CodeGroups.xml'/>
    public void LJCDelete(string codeLine, string name)
    {
      ArgError.MethodName = "LJCDelete(codeLine, name)";
      ArgError.Add(codeLine, "codeLine");
      ArgError.Add(name, "name");
      NetString.ThrowArgError(ArgError.ToString());

      var item = LJCRetrieve(codeLine, name);
      if (item != null)
      {
        Remove(item);
      }
    }

    // Retrieves a collection that match the supplied values.
    /// <include path='items/LJCLoad/*' file='Doc/CodeGroups.xml'/>
    public CodeGroups LJCLoad(string codeLine = null)
    {
      CodeGroups retValue = null;

      if (!NetString.HasValue(codeLine))
      {
        retValue = Clone();
      }
      else
      {
        var items = FindAll(x => x.CodeLine == codeLine);
        retValue = GetCollection(items);
      }
      return retValue;
    }

    // Retrieve the collection element with unique values.
    /// <include path='items/LJCRetrieve/*' file='Doc/CodeGroups.xml'/>
    public CodeGroup LJCRetrieve(string codeLine, string name)
    {
      CodeGroup retValue = null;

      ArgError.MethodName = "LJCRetrieve(codeLine, name)";
      ArgError.Add(codeLine, "codeLine");
      ArgError.Add(name, "name");
      NetString.ThrowArgError(ArgError.ToString());

      LJCSortUnique();
      CodeGroup searchItem = new CodeGroup()
      {
        CodeLine = codeLine,
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
    /// <include path='items/LJCRetrieveWithPath/*' file='Doc/CodeGroups.xml'/>
    public CodeGroup LJCRetrieveWithPath(string codeLine, string path)
    {
      CodeGroup retValue = null;

      ArgError.MethodName = "LJCRetrieveWithPath(codeLine, name)";
      ArgError.Add(codeLine, "codeLine");
      ArgError.Add(path, "path");
      NetString.ThrowArgError(ArgError.ToString());

      var comparer = new CodeGroupPathComparer();
      LJCSortPath(comparer);
      CodeGroup searchItem = new CodeGroup()
      {
        CodeLine = codeLine,
        Path = path
      };
      int index = BinarySearch(searchItem,comparer);
      if (index > -1)
      {
        retValue = this[index];
      }
      return retValue;
    }

    // Finds and updates the collection item.
    /// <include path='items/LJCUpdate/*' file='Doc/CodeGroups.xml'/>
    public void LJCUpdate(CodeGroup codeGroup)
    {
      ArgError.MethodName = "LJCUpdate(codeGroup)";
      ArgError.Add(CodeGroup.ItemValues(codeGroup));
      NetString.ThrowArgError(ArgError.ToString());

      if (NetCommon.HasItems(this))
      {
        var item = LJCRetrieve(codeGroup.CodeLine, codeGroup.Name);
        if (item != null)
        {
          item.Path = codeGroup.Path;
        }
      }
    }
    #endregion

    #region Public Methods

    /// <summary>Sorts on Parent and Path values.</summary>
    public void LJCSortPath(CodeGroupPathComparer comparer)
    {
      ArgError.MethodName = "LJCSortPath(comparer)";
      ArgError.Add(comparer, "comparer");
      NetString.ThrowArgError(ArgError.ToString());

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
      if (Count != mPrevCount
        || mSortType.CompareTo(SortType.Unique) != 0)
      {
        mPrevCount = Count;
        Sort();
        mSortType = SortType.Unique;
      }
    }
    #endregion

    #region Properties

    // Represents Argument errors.
    private ArgError ArgError { get; set; }
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
