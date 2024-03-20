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
    /// <include path='items/DefaultConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public CodeGroups()
    {
      mPrevCount = -1;
    }
    #endregion

    #region Collection Methods

    // Creates and returns a clone of the object.
    /// <include path='items/Clone/*' file='../../LJCDocLib/Common/Data.xml'/>
    public CodeGroups Clone()
    {
      var retValue = MemberwiseClone() as CodeGroups;
      return retValue;
    }

    // Gets a custom collection from List<T>.
    /// <include path='items/GetCollection/*' file='../../LJCDocLib/Common/Collection.xml'/>
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

    // Creates and adds the object from the provided values.
    /// <include path='items/Add/*' file='../../LJCDocLib/Common/Collection.xml'/>
    public CodeGroup Add(string codeLine, string name, string path = null)
    {
      CodeGroup retValue;

      string message = "";
      NetString.AddMissingArgument(message, codeLine);
      NetString.AddMissingArgument(message, name);
      NetString.ThrowInvalidArgument(message);

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
    /// <summary>
    /// Removes an item by unique values.
    /// </summary>
    /// <param name="codeLine">The CodeLine value.</param>
    /// <param name="name">The Name value.</param>
    public void LJCDelete(string codeLine, string name)
    {
      var item = LJCRetrieve(codeLine, name);
      if (item != null)
      {
        Remove(item);
      }
    }

    // Retrieves a collection that match the supplied values.
    /// <summary>
    /// Retrieves items that match the supplied values.
    /// </summary>
    /// <param name="codeLine">The CodeLine name.</param>
    /// <returns>The collection object.</returns>
    public CodeGroups LJCLoad(string codeLine)
    {
      CodeGroups retValue = null;

      if (NetString.HasValue(codeLine))
      {
        var items = FindAll(x => x.CodeLine == codeLine);
        retValue = GetCollection(items);
      }
      return retValue;
    }

    // Retrieve the collection element with unique values.
    /// <summary>
    /// Retrieve the collection element with unique values.
    /// </summary>
    /// <param name="codeLine">The CodeLine name.</param>
    /// <param name="name">The item name.</param>
    /// <returns>A reference to the matching item.</returns>
    public CodeGroup LJCRetrieve(string codeLine, string name)
    {
      CodeGroup retValue = null;

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

    // Finds and updates the collection item.
    /// <summary>
    /// Finds and updates the collection item.
    /// </summary>
    /// <param name="codeGroup">The CodeGroup object.</param>
    public void LJCUpdate(CodeGroup codeGroup)
    {
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
