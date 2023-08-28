// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// ViewJoinOn.cs
using LJCNetCommon;
using LJCDBClientLib;
using System;
using System.Collections.Generic;

namespace LJCAppName
{
  /// <summary>The ViewJoinOn table Data Object.</summary>
  public class ViewJoinOn : IComparable<ViewJoinOn>
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public ViewJoinOn()
    {
      ChangedNames = new ChangedNames();
    }

    // The Copy constructor.
    /// <include path='items/CopyConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public ViewJoinOn(ViewJoinOn item)
    {
      ChangedNames = new ChangedNames();
      ID = item.ID;
    }
    #endregion

    #region Data Methods

    // Creates and returns a clone of this object.
    /// <include path='items/Clone/*' file='../../LJCDocLib/Common/Data.xml'/>
    public ViewJoinOn Clone()
    {
      var retValue = MemberwiseClone() as ViewJoinOn;
      return retValue;
    }

    // Provides the default Sort functionality.
    /// <include path='items/CompareTo/*' file='../../LJCDocLib/Common/Data.xml'/>
    public int CompareTo(ViewJoinOn other)
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

    /// <summary>Gets or sets the ViewJoinID value.</summary>
    //[Column("ViewJoinID", TypeName="_DBType_")]
    public Int32 ViewJoinID
    {
      get { return mViewJoinID; }
      set
      {
        mViewJoinID = ChangedNames.Add(ColumnViewJoinID, mViewJoinID, value);
      }
    }
    private Int32 mViewJoinID;

    /// <summary>Gets or sets the FromColumnName value.</summary>
    //[Column("FromColumnName", TypeName="_DBType_(60")]
    public String FromColumnName
    {
      get { return mFromColumnName; }
      set
      {
        value = NetString.InitString(value);
        mFromColumnName = ChangedNames.Add(ColumnFromColumnName, mFromColumnName, value);
      }
    }
    private String mFromColumnName;

    /// <summary>Gets or sets the ToColumnName value.</summary>
    //[Column("ToColumnName", TypeName="_DBType_(60")]
    public String ToColumnName
    {
      get { return mToColumnName; }
      set
      {
        value = NetString.InitString(value);
        mToColumnName = ChangedNames.Add(ColumnToColumnName, mToColumnName, value);
      }
    }
    private String mToColumnName;

    /// <summary>Gets or sets the JoinOnOperator value.</summary>
    //[Column("JoinOnOperator", TypeName="_DBType_(5")]
    public String JoinOnOperator
    {
      get { return mJoinOnOperator; }
      set
      {
        value = NetString.InitString(value);
        mJoinOnOperator = ChangedNames.Add(ColumnJoinOnOperator, mJoinOnOperator, value);
      }
    }
    private String mJoinOnOperator;
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
    public static string TableName = "ViewJoinOn";

    /// <summary>The ID column name.</summary>
    public static string ColumnID = "ID";

    /// <summary>The ViewJoinID column name.</summary>
    public static string ColumnViewJoinID = "ViewJoinID";

    /// <summary>The FromColumnName column name.</summary>
    public static string ColumnFromColumnName = "FromColumnName";

    /// <summary>The ToColumnName column name.</summary>
    public static string ColumnToColumnName = "ToColumnName";

    /// <summary>The JoinOnOperator column name.</summary>
    public static string ColumnJoinOnOperator = "JoinOnOperator";

    /// <summary>The FromColumnName maximum length.</summary>
    public static int LengthFromColumnName = 60;

    /// <summary>The ToColumnName maximum length.</summary>
    public static int LengthToColumnName = 60;

    /// <summary>The JoinOnOperator maximum length.</summary>
    public static int LengthJoinOnOperator = 5;
    #endregion

    #region Calculated and Join Class Data

    ///// <summary>The Join TypeName column name.</summary>
    //public static string ColumnTypeName = "TypeName";
    #endregion
  }

  #region Comparers

  /// <summary>Sort and search on Name value.</summary>
  public class ViewJoinOnUniqueComparer : IComparer<ViewJoinOn>
  {
    // Compares two objects.
    /// <include path='items/Compare/*' file='../../LJCDocLib/Common/Data.xml'/>
    public int Compare(ViewJoinOn x, ViewJoinOn y)
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
