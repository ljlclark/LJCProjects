// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// ControlRow.cs
using LJCNetCommon;
using LJCDBClientLib;
using System;
using System.Collections.Generic;

namespace LJCAppName
{
  /// <summary>The ControlRow table Data Object.</summary>
  public class ControlRow : IComparable<ControlRow>
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public ControlRow()
    {
      ChangedNames = new ChangedNames();
    }

    // The Copy constructor.
    /// <include path='items/CopyConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public ControlRow(ControlRow item)
    {
      ChangedNames = new ChangedNames();
      ID = item.ID;
    }
    #endregion

    #region Data Methods

    // Creates and returns a clone of this object.
    /// <include path='items/Clone/*' file='../../LJCDocLib/Common/Data.xml'/>
    public ControlRow Clone()
    {
      var retValue = MemberwiseClone() as ControlRow;
      return retValue;
    }

    // Provides the default Sort functionality.
    /// <include path='items/CompareTo/*' file='../../LJCDocLib/Common/Data.xml'/>
    public int CompareTo(ControlRow other)
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
    public Int64 ID
    {
      get { return mID; }
      set
      {
        mID = ChangedNames.Add(ColumnID, mID, value);
      }
    }
    private Int64 mID;

    /// <summary>Gets or sets the ControlColumnID value.</summary>
    //[Column("ControlColumnID", TypeName="_DBType_")]
    public Int64 ControlColumnID
    {
      get { return mControlColumnID; }
      set
      {
        mControlColumnID = ChangedNames.Add(ColumnControlColumnID, mControlColumnID, value);
      }
    }
    private Int64 mControlColumnID;

    /// <summary>Gets or sets the DataValueName value.</summary>
    //[Column("DataValueName", TypeName="_DBType_(60")]
    public String DataValueName
    {
      get { return mDataValueName; }
      set
      {
        value = NetString.InitString(value);
        mDataValueName = ChangedNames.Add(ColumnDataValueName, mDataValueName, value);
      }
    }
    private String mDataValueName;

    /// <summary>Gets or sets the RowIndex value.</summary>
    //[Column("RowIndex", TypeName="_DBType_")]
    public Int32 RowIndex
    {
      get { return mRowIndex; }
      set
      {
        mRowIndex = ChangedNames.Add(ColumnRowIndex, mRowIndex, value);
      }
    }
    private Int32 mRowIndex;

    /// <summary>Gets or sets the TabbingIndex value.</summary>
    //[Column("TabbingIndex", TypeName="_DBType_")]
    public Int32 TabbingIndex
    {
      get { return mTabbingIndex; }
      set
      {
        mTabbingIndex = ChangedNames.Add(ColumnTabbingIndex, mTabbingIndex, value);
      }
    }
    private Int32 mTabbingIndex;

    /// <summary>Gets or sets the AllowDisplay value.</summary>
    //[Column("AllowDisplay", TypeName="_DBType_")]
    public Boolean AllowDisplay
    {
      get { return mAllowDisplay; }
      set
      {
        mAllowDisplay = ChangedNames.Add(ColumnAllowDisplay, mAllowDisplay, value);
      }
    }
    private Boolean mAllowDisplay;
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
    public static string TableName = "ControlRow";

    /// <summary>The ID column name.</summary>
    public static string ColumnID = "ID";

    /// <summary>The ControlColumnID column name.</summary>
    public static string ColumnControlColumnID = "ControlColumnID";

    /// <summary>The DataValueName column name.</summary>
    public static string ColumnDataValueName = "DataValueName";

    /// <summary>The RowIndex column name.</summary>
    public static string ColumnRowIndex = "RowIndex";

    /// <summary>The TabbingIndex column name.</summary>
    public static string ColumnTabbingIndex = "TabbingIndex";

    /// <summary>The AllowDisplay column name.</summary>
    public static string ColumnAllowDisplay = "AllowDisplay";

    /// <summary>The DataValueName maximum length.</summary>
    public static int LengthDataValueName = 60;
    #endregion

    #region Calculated and Join Class Data

    ///// <summary>The Join TypeName column name.</summary>
    //public static string ColumnTypeName = "TypeName";
    #endregion
  }

  #region Comparers

  /// <summary>Sort and search on Name value.</summary>
  public class ControlRowUniqueComparer : IComparer<ControlRow>
  {
    // Compares two objects.
    /// <include path='items/Compare/*' file='../../LJCDocLib/Common/Data.xml'/>
    public int Compare(ControlRow x, ControlRow y)
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
