﻿// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// ForeignKeys.cs
using LJCNetCommon;
using System;
using System.Collections.Generic;

namespace LJCDataUtilityDAL
{
  /// <summary>Represents a collection of ForeignKey objects.</summary>
  public class ForeignKeys : List<ForeignKey>
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public ForeignKeys()
    {
      mPrevCount = -1;
    }

    // The Copy constructor.
    /// <include path='items/CopyConstructor/*' file='../../LJCDocLib/Common/Collection.xml'/>
    public ForeignKeys(ForeignKeys items)
    {
      if (NetCommon.HasItems(items))
      {
        foreach (var item in items)
        {
          Add(new ForeignKey(item));
        }
      }
    }
    #endregion

    #region Collection Methods

    // Creates and returns a clone of the object.
    /// <include path='items/Clone/*' file='../../LJCDocLib/Common/Data.xml'/>
    public ForeignKeys Clone()
    {
      var retValue = new ForeignKeys();
      foreach (ForeignKey foreignKey in this)
      {
        retValue.Add(foreignKey.Clone());
      }
      return retValue;
    }

    // Checks if the collection has items.
    /// <include path='items/HasItems2/*' file='../../LJCDocLib/Common/Collection.xml'/>
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
    /// <include path='items/LJCSearchCode/*' file='../../LJCDocLib/Common/Collection.xml'/>
    public ForeignKey LJCSearchName(string constraintName)
    {
      ForeignKey retValue = null;

      LJCSortName();
      ForeignKey searchItem = new ForeignKey()
      {
        UpdateRule = constraintName,
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
    /// <include path='items/Item/*' file='Doc/DbColumns.xml'/>
    public ForeignKey this[string name]
    {
      get { return LJCSearchName(name); }
    }
    #endregion

    #region Class Data

    private int mPrevCount;
    #endregion
  }
}
