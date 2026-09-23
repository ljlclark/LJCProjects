// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// XTable.cs
using LJCNetCommon;
using LJCDBClientLib;
using System;
using System.Collections.Generic;

namespace XLJCAppNamespace
{
  /// <summary>The XTable Data Object.</summary>
  public class XTable : IComparable<XTable>
  {
    #region Constructor Methods

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../LJCGenDoc/Common/Data.xml'/>
    public XTable()
    {
      ChangedNames = new ChangedNames();
    }

    // The Copy constructor.
    /// <include path='items/CopyConstructor/*' file='../../LJCGenDoc/Common/Data.xml'/>
    public XTable(XTable item)
    {
      ChangedNames = new ChangedNames();
      ID = item.ID;
      Name = item.Name;
      Description = item.Description;
    }
    #endregion

    #region Data Methods

    // Creates and returns a clone of this object.
    /// <include path='items/Clone/*' file='../../LJCGenDoc/Common/Data.xml'/>
    public XTable Clone()
    {
      var retValue = MemberwiseClone() as XTable;
      return retValue;
    }

    // Provides the default Sort functionality.
    /// <include path='items/CompareTo/*' file='../../LJCGenDoc/Common/Data.xml'/>
    public int CompareTo(XTable other)
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
        retValue = XCompareToName.CompareTo(other.XCompareToName);

        // Not case sensitive.
        //retValue = string.Compare(XCompareToName, other.XCompareToName, true);
      }
      return retValue;
    }

    // The object string identifier.
    /// <include path='items/ToString/*' file='../../LJCGenDoc/Common/Data.xml'/>
    public override string ToString()
    {
      var retValue = $"{mSequence} {mXToStringName}:{mID}-{mValue}";
      return retValue;
    }
    #endregion

    #region Data Properties

    // Update ChangedNames.Add() statements to "Property" constant
    // if property was renamed.

    /// <summary>Gets or sets the XProperty value.</summary>
    //[Column("", TypeName="_DBType_")]
    public  XProperty
    {
      get { return mXProperty; }
      set
      {
        mXProperty = ChangedNames.Add(ColumnXProperty, mXProperty, value);
      }
    }
    private  mXProperty;
    #endregion

    #region Calculated and Join Data Properties

    ///// <summary>Gets or sets the Join TypeName value.</summary>
    //public string TypeName { get; set; }
    #endregion

    #region Class Properties

    /// <summary>Gets a reference to the ChangedNames list.</summary>
    public ChangedNames ChangedNames { get; private set; }
    #endregion

    #region Class Data

    /// <summary>The table name.</summary>
    public static string TableName = "XTable";

    /// <summary>The  column name.</summary>
    public static string Column = "";
    #endregion

    #region Calculated and Join Class Data

    ///// <summary>The Join TypeName column name.</summary>
    //public static string ColumnTypeName = "TypeName";
    #endregion
  }

  #region Comparers

  /// <summary>Sort and search on Name value.</summary>
  public class XTableUnique : IComparer<XTable>
  {
    // Compares two objects.
    /// <include path='items/Compare/*' file='../../LJCGenDoc/Common/Data.xml'/>
    public int Compare(XTable x, XTable y)
    {
      int retValue;

      var isContinue = true;
      retValue = NetCommon.CompareNull(x, y);
      if (retValue != -2)
      {
        isContinue = false;
      }

      if (isContinue)
      {
        retValue = NetCommon.CompareNull(x.XComparerName, y.XComparerName);
        if (retValue != -2)
        {
          isContinue = false;
        }
      }

      if (isContinue)
      {
        // Case sensitive and not string.
        retValue = x.XComparerName.CompareTo(y.XComparerName);

        // Not case sensitive.
        //retValue = string.Compare(x.XComparerName, y.XComparerName, true);
      }
      return retValue;
    }
  }
  #endregion
}