// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// LayoutColumn.cs
using LJCNetCommon;
using LJCDBClientLib;
using System;
using System.Collections.Generic;

namespace LJCAppName
{
  /// <summary>The LayoutColumn table Data Object.</summary>
  public class LayoutColumn : IComparable<LayoutColumn>
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public LayoutColumn()
    {
      ChangedNames = new ChangedNames();
    }

    // The Copy constructor.
    /// <include path='items/CopyConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public LayoutColumn(LayoutColumn item)
    {
      ChangedNames = new ChangedNames();
      ID = item.ID;
    }
    #endregion

    #region Data Methods

    // Creates and returns a clone of this object.
    /// <include path='items/Clone/*' file='../../LJCDocLib/Common/Data.xml'/>
    public LayoutColumn Clone()
    {
      var retValue = MemberwiseClone() as LayoutColumn;
      return retValue;
    }

    // Provides the default Sort functionality.
    /// <include path='items/CompareTo/*' file='../../LJCDocLib/Common/Data.xml'/>
    public int CompareTo(LayoutColumn other)
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

    /// <summary>Gets or sets the LayoutColumnID value.</summary>
    //[Column("LayoutColumnID", TypeName="_DBType_")]
    public Int16 LayoutColumnID
    {
      get { return mLayoutColumnID; }
      set
      {
        mLayoutColumnID = ChangedNames.Add(ColumnLayoutColumnID, mLayoutColumnID, value);
      }
    }
    private Int16 mLayoutColumnID;

    /// <summary>Gets or sets the Name value.</summary>
    //[Column("Name", TypeName="_DBType_(60")]
    public String Name
    {
      get { return mName; }
      set
      {
        value = NetString.InitString(value);
        mName = ChangedNames.Add(ColumnName, mName, value);
      }
    }
    private String mName;

    /// <summary>Gets or sets the Description value.</summary>
    //[Column("Description", TypeName="_DBType_(100")]
    public String Description
    {
      get { return mDescription; }
      set
      {
        value = NetString.InitString(value);
        mDescription = ChangedNames.Add(ColumnDescription, mDescription, value);
      }
    }
    private String mDescription;

    /// <summary>Gets or sets the SourceLayoutID value.</summary>
    //[Column("SourceLayoutID", TypeName="_DBType_")]
    public Int32 SourceLayoutID
    {
      get { return mSourceLayoutID; }
      set
      {
        mSourceLayoutID = ChangedNames.Add(ColumnSourceLayoutID, mSourceLayoutID, value);
      }
    }
    private Int32 mSourceLayoutID;

    /// <summary>Gets or sets the Sequence value.</summary>
    //[Column("Sequence", TypeName="_DBType_")]
    public Int32 Sequence
    {
      get { return mSequence; }
      set
      {
        mSequence = ChangedNames.Add(ColumnSequence, mSequence, value);
      }
    }
    private Int32 mSequence;

    /// <summary>Gets or sets the DataTypeID value.</summary>
    //[Column("DataTypeID", TypeName="_DBType_")]
    public Int16 DataTypeID
    {
      get { return mDataTypeID; }
      set
      {
        mDataTypeID = ChangedNames.Add(ColumnDataTypeID, mDataTypeID, value);
      }
    }
    private Int16 mDataTypeID;

    /// <summary>Gets or sets the Length value.</summary>
    //[Column("Length", TypeName="_DBType_")]
    public Int32 Length
    {
      get { return mLength; }
      set
      {
        mLength = ChangedNames.Add(ColumnLength, mLength, value);
      }
    }
    private Int32 mLength;

    /// <summary>Gets or sets the IdentityKey value.</summary>
    //[Column("IdentityKey", TypeName="_DBType_")]
    public Boolean IdentityKey
    {
      get { return mIdentityKey; }
      set
      {
        mIdentityKey = ChangedNames.Add(ColumnIdentityKey, mIdentityKey, value);
      }
    }
    private Boolean mIdentityKey;

    /// <summary>Gets or sets the PrimaryKey value.</summary>
    //[Column("PrimaryKey", TypeName="_DBType_")]
    public Boolean PrimaryKey
    {
      get { return mPrimaryKey; }
      set
      {
        mPrimaryKey = ChangedNames.Add(ColumnPrimaryKey, mPrimaryKey, value);
      }
    }
    private Boolean mPrimaryKey;

    /// <summary>Gets or sets the AllowNull value.</summary>
    //[Column("AllowNull", TypeName="_DBType_")]
    public Boolean AllowNull
    {
      get { return mAllowNull; }
      set
      {
        mAllowNull = ChangedNames.Add(ColumnAllowNull, mAllowNull, value);
      }
    }
    private Boolean mAllowNull;
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
    public static string TableName = "LayoutColumn";

    /// <summary>The LayoutColumnID column name.</summary>
    public static string ColumnLayoutColumnID = "LayoutColumnID";

    /// <summary>The Name column name.</summary>
    public static string ColumnName = "Name";

    /// <summary>The Description column name.</summary>
    public static string ColumnDescription = "Description";

    /// <summary>The SourceLayoutID column name.</summary>
    public static string ColumnSourceLayoutID = "SourceLayoutID";

    /// <summary>The Sequence column name.</summary>
    public static string ColumnSequence = "Sequence";

    /// <summary>The DataTypeID column name.</summary>
    public static string ColumnDataTypeID = "DataTypeID";

    /// <summary>The Length column name.</summary>
    public static string ColumnLength = "Length";

    /// <summary>The IdentityKey column name.</summary>
    public static string ColumnIdentityKey = "IdentityKey";

    /// <summary>The PrimaryKey column name.</summary>
    public static string ColumnPrimaryKey = "PrimaryKey";

    /// <summary>The AllowNull column name.</summary>
    public static string ColumnAllowNull = "AllowNull";

    /// <summary>The Name maximum length.</summary>
    public static int LengthName = 60;

    /// <summary>The Description maximum length.</summary>
    public static int LengthDescription = 100;
    #endregion

    #region Calculated and Join Class Data

    ///// <summary>The Join TypeName column name.</summary>
    //public static string ColumnTypeName = "TypeName";
    #endregion
  }

  #region Comparers

  /// <summary>Sort and search on Name value.</summary>
  public class LayoutColumnUniqueComparer : IComparer<LayoutColumn>
  {
    // Compares two objects.
    /// <include path='items/Compare/*' file='../../LJCDocLib/Common/Data.xml'/>
    public int Compare(LayoutColumn x, LayoutColumn y)
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
