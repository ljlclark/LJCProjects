// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// TableKeys.cs
using LJCNetCommon;
using System;
using System.Collections.Generic;

namespace LJCDataUtilityDAL
{
  /// <summary>Represents a collection of ForeignKey objects.</summary>
  public class TableKeys : List<TableKey>
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../LJCGenDoc/Common/Data.xml'/>
    public TableKeys()
    {
      mPrevCount = -1;
    }

    // The Copy constructor.
    /// <include path='items/CopyConstructor/*' file='../../LJCGenDoc/Common/Collection.xml'/>
    public TableKeys(TableKeys items)
    {
      if (NetCommon.HasItems(items))
      {
        foreach (var item in items)
        {
          Add(new TableKey(item));
        }
      }
    }
    #endregion

    #region Collection Methods

    // Creates and returns a clone of the object.
    /// <include path='items/Clone/*' file='../../LJCGenDoc/Common/Data.xml'/>
    public TableKeys Clone()
    {
      var retValue = new TableKeys();
      foreach (TableKey foreignKey in this)
      {
        retValue.Add(foreignKey.Clone());
      }
      return retValue;
    }

    // Checks if the collection has items.
    /// <include path='items/HasItems2/*' file='../../LJCGenDoc/Common/Collection.xml'/>
    public bool HasItems()
    {
      bool retValue = false;

      if (Count > 0)
      {
        retValue = true;
      }
      return retValue;
    }
    #endregion

    #region Search and Sort Methods

    // Retrieve the collection element.
    /// <include path='items/LJCSearchUnique/*' file='../../LJCGenDoc/Common/Collection.xml'/>
    public TableKey LJCSearchName(string name)
    {
      TableKey retValue = null;

      LJCSortName();
      TableKey searchItem = new TableKey()
      {
        UpdateRule = name,
      };
      int index = BinarySearch(searchItem);
      if (index > -1)
      {
        retValue = this[index];
      }
      return retValue;
    }

    /// <summary>Sort on Code.</summary>
    public void LJCSortName()
    {
      if (Count != mPrevCount)
      {
        mPrevCount = Count;
        Sort();
      }
    }
    #endregion

    #region Properties

    // The item for the specified name.
    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public TableKey this[string name]
    {
      get { return LJCSearchName(name); }
    }
    #endregion

    #region Class Data

    private int mPrevCount;
    #endregion
  }
}
