// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// ViewConditionSet.cs
using LJCNetCommon;
using LJCDBClientLib;
using System;
using System.Collections.Generic;

namespace LJCAppName
{
  /// <summary>The ViewConditionSet table Data Object.</summary>
  public class ViewConditionSet : IComparable<ViewConditionSet>
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public ViewConditionSet()
    {
      ChangedNames = new ChangedNames();
    }

    // The Copy constructor.
    /// <include path='items/CopyConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public ViewConditionSet(ViewConditionSet item)
    {
      ChangedNames = new ChangedNames();
      ID = item.ID;
    }
    #endregion

    #region Data Methods

    // Creates and returns a clone of this object.
    /// <include path='items/Clone/*' file='../../LJCDocLib/Common/Data.xml'/>
    public ViewConditionSet Clone()
    {
      var retValue = MemberwiseClone() as ViewConditionSet;
      return retValue;
    }

    // Provides the default Sort functionality.
    /// <include path='items/CompareTo/*' file='../../LJCDocLib/Common/Data.xml'/>
    public int CompareTo(ViewConditionSet other)
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

    /// <summary>Gets or sets the ViewFilterID value.</summary>
    //[Column("ViewFilterID", TypeName="_DBType_")]
    public Int32 ViewFilterID
    {
      get { return mViewFilterID; }
      set
      {
        mViewFilterID = ChangedNames.Add(ColumnViewFilterID, mViewFilterID, value);
      }
    }
    private Int32 mViewFilterID;

    /// <summary>Gets or sets the BooleanOperator value.</summary>
    //[Column("BooleanOperator", TypeName="_DBType_(3")]
    public String BooleanOperator
    {
      get { return mBooleanOperator; }
      set
      {
        value = NetString.InitString(value);
        mBooleanOperator = ChangedNames.Add(ColumnBooleanOperator, mBooleanOperator, value);
      }
    }
    private String mBooleanOperator;
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
    public static string TableName = "ViewConditionSet";

    /// <summary>The ID column name.</summary>
    public static string ColumnID = "ID";

    /// <summary>The ViewFilterID column name.</summary>
    public static string ColumnViewFilterID = "ViewFilterID";

    /// <summary>The BooleanOperator column name.</summary>
    public static string ColumnBooleanOperator = "BooleanOperator";

    /// <summary>The BooleanOperator maximum length.</summary>
    public static int LengthBooleanOperator = 3;
    #endregion

    #region Calculated and Join Class Data

    ///// <summary>The Join TypeName column name.</summary>
    //public static string ColumnTypeName = "TypeName";
    #endregion
  }

  #region Comparers

  /// <summary>Sort and search on Name value.</summary>
  public class ViewConditionSetUniqueComparer : IComparer<ViewConditionSet>
  {
    // Compares two objects.
    /// <include path='items/Compare/*' file='../../LJCDocLib/Common/Data.xml'/>
    public int Compare(ViewConditionSet x, ViewConditionSet y)
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
