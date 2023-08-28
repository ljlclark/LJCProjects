// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// ViewOrderBy.cs
using LJCNetCommon;
using LJCDBClientLib;
using System;
using System.Collections.Generic;

namespace LJCAppName
{
  /// <summary>The ViewOrderBy table Data Object.</summary>
  public class ViewOrderBy : IComparable<ViewOrderBy>
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public ViewOrderBy()
    {
      ChangedNames = new ChangedNames();
    }

    // The Copy constructor.
    /// <include path='items/CopyConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public ViewOrderBy(ViewOrderBy item)
    {
      ChangedNames = new ChangedNames();
      ID = item.ID;
    }
    #endregion

    #region Data Methods

    // Creates and returns a clone of this object.
    /// <include path='items/Clone/*' file='../../LJCDocLib/Common/Data.xml'/>
    public ViewOrderBy Clone()
    {
      var retValue = MemberwiseClone() as ViewOrderBy;
      return retValue;
    }

    // Provides the default Sort functionality.
    /// <include path='items/CompareTo/*' file='../../LJCDocLib/Common/Data.xml'/>
    public int CompareTo(ViewOrderBy other)
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
        //retValue = Name.CompareTo(other.Name);

        // Not case sensitive.
        retValue = string.Compare(Name, other.Name, true);
      }
      return retValue;
    }

    // The object string identifier.
    /// <include path='items/ToString/*' file='../../LJCDocLib/Common/Data.xml'/>
    public override string ToString()
    {
      // $"{mSequence}){mName}:{mID}-{mValue}";
      return mDescription;
    }
    #endregion

    #region Data Properties

    // Update ChangedNames.Add() statements to "Property" constant
    // if property was renamed.

    /// <summary>Gets or sets the ID value.</summary>
    //[Column("ID", TypeName="_DBType_")]
    public Int32 ID
    {
      get { return mID; }
      set
      {
        mID = ChangedNames.Add(ColumnID, mID, value);
      }
    }
    private Int32 mID;

    /// <summary>Gets or sets the ViewDataID value.</summary>
    //[Column("ViewDataID", TypeName="_DBType_")]
    public Int32 ViewDataID
    {
      get { return mViewDataID; }
      set
      {
        mViewDataID = ChangedNames.Add(ColumnViewDataID, mViewDataID, value);
      }
    }
    private Int32 mViewDataID;

    /// <summary>Gets or sets the ColumnName value.</summary>
    //[Column("ColumnName", TypeName="_DBType_(60")]
    public String ColumnName
    {
      get { return mColumnName; }
      set
      {
        value = NetString.InitString(value);
        mColumnName = ChangedNames.Add(ColumnColumnName, mColumnName, value);
      }
    }
    private String mColumnName;
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
    public static string TableName = "ViewOrderBy";

    /// <summary>The ID column name.</summary>
    public static string ColumnID = "ID";

    /// <summary>The ViewDataID column name.</summary>
    public static string ColumnViewDataID = "ViewDataID";

    /// <summary>The ColumnName column name.</summary>
    public static string ColumnColumnName = "ColumnName";

    /// <summary>The ColumnName maximum length.</summary>
    public static int LengthColumnName = 60;
    #endregion

    #region Calculated and Join Class Data

    ///// <summary>The Join TypeName column name.</summary>
    //public static string ColumnTypeName = "TypeName";
    #endregion
  }

  #region Comparers

  /// <summary>Sort and search on Name value.</summary>
  public class ViewOrderByUniqueComparer : IComparer<ViewOrderBy>
  {
    // Compares two objects.
    /// <include path='items/Compare/*' file='../../LJCDocLib/Common/Data.xml'/>
    public int Compare(ViewOrderBy x, ViewOrderBy y)
    {
      int retValue;

      retValue = NetCommon.CompareNull(x, y);
      if (-2 == retValue)
      {
        retValue = NetCommon.CompareNull(x._ComparerName_, y._ComparerName_);
        if (-2 == retValue)
        {
          // Case sensitive.
          //retValue = x._ComparerName_.CompareTo(y._ComparerName_);

          // Not case sensitive.
          retValue = string.Compare(x._ComparerName_, y._ComparerName_, true);
        }
      }
      return retValue;
    }
  }
  #endregion
}
