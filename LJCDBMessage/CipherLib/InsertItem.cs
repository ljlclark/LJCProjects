// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// InsertItem.cs
using System;
using System.Collections.Generic;
using LJCNetCommon;

namespace CipherLib
{
  /// <summary>Represents a byte[] Insert Item.</summary>
  public class InsertItem : IComparable<InsertItem>
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public InsertItem()
    {
    }

    // The Copy constructor.
    /// <include path='items/CopyConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public InsertItem(InsertItem item)
    {
      Name = item.Name;
      InsertIndex = item.InsertIndex;
      InsertValue = item.InsertValue;
    }
    #endregion

    #region Data Methods

    // Provides the default Sort functionality.
    /// <include path='items/CompareTo/*' file='../../LJCDocLib/Common/Data.xml'/>
    public int CompareTo(InsertItem other)
    {
      int retValue;

      if (null == other)
      {
        // This value is greater than null.
        retValue = 1;
      }
      else
      {
        // Case sensitive.
        retValue = InsertIndex.CompareTo(other.InsertIndex);

        // Not case sensitive.
        //retValue = string.Compare(Name, other.Name, true);
      }
      return retValue;
    }
    #endregion

    /// <summary>Gets or sets the Item Name.</summary>
    public string Name { get; set; }

    /// <summary>Gets or sets the Item Index.</summary>
    public int InsertIndex { get; set; }

    /// <summary>Gets or sets the Item Value.</summary>
    public byte[] InsertValue { get; set; }
  }

  #region Comparers

  /// <summary>Sort and search on Name value.</summary>
  public class InsertItemNameComparer : IComparer<InsertItem>
  {
    // Compares two objects.
    /// <include path='items/Compare/*' file='../../LJCDocLib/Common/Data.xml'/>
    public int Compare(InsertItem x, InsertItem y)
    {
      int retValue;

      retValue = NetCommon.CompareNull(x, y);
      if (-2 == retValue)
      {
        retValue = NetCommon.CompareNull(x.Name, y.Name);
        if (-2 == retValue)
        {
          // Case sensitive.
          //retValue = x._ComparerName_.CompareTo(y._ComparerName_);

          // Not case sensitive.
          retValue = string.Compare(x.Name, y.Name, true);
        }
      }
      return retValue;
    }
  }
  #endregion
}
