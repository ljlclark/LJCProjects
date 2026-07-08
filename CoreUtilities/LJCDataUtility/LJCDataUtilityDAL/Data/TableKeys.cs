// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// TableKeys.cs
using LJCNetCommon;
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
    public TableKeys(TableKeys items) : this()
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
    /// <include file='../../LJCGenDoc/Common/Data.xml'
    ///  path='members/Clone/*'/>
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
    /// <include file='../../LJCGenDoc/Common/Collection.xml'
    ///  path='members/HasItems/*'/>
    public bool LJCHasItems()
    {
      bool retValue = false;

      if (Count > 0)
      {
        retValue = true;
      }
      return retValue;
    }
    #endregion

    #region Collection Data Methods

    // Retrieve the collection element.
    /// <include file='Doc/TableKeys.xml'
    ///  path='members/LJCGetWithName/*'/>
    public TableKey LJCGetWithName(string name)
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
    #endregion

    #region Sort Methods

    /// <summary>Sort on Code.</summary>
    /// <include file='Doc/TableKeys.xml'
    ///  path='members/LJCSortName/*'/>
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

    // The item for the supplied name.
    /// <include file='Doc/TableKeys.xml'
    ///  path='members/UniqueIndexer/*'/>
    public TableKey this[string name]
    {
      get => LJCGetWithName(name);
    }
    #endregion

    #region Class Data

    private int mPrevCount;
    #endregion
  }
}
