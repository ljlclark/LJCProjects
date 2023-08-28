// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// ControlDetail.cs
using LJCNetCommon;
using LJCDBClientLib;
using System;
using System.Collections.Generic;

namespace LJCAppName
{
  /// <summary>The ControlDetail table Data Object.</summary>
  public class ControlDetail : IComparable<ControlDetail>
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public ControlDetail()
    {
      ChangedNames = new ChangedNames();
    }

    // The Copy constructor.
    /// <include path='items/CopyConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public ControlDetail(ControlDetail item)
    {
      ChangedNames = new ChangedNames();
      ID = item.ID;
    }
    #endregion

    #region Data Methods

    // Creates and returns a clone of this object.
    /// <include path='items/Clone/*' file='../../LJCDocLib/Common/Data.xml'/>
    public ControlDetail Clone()
    {
      var retValue = MemberwiseClone() as ControlDetail;
      return retValue;
    }

    // Provides the default Sort functionality.
    /// <include path='items/CompareTo/*' file='../../LJCDocLib/Common/Data.xml'/>
    public int CompareTo(ControlDetail other)
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
    //[Column("Description", TypeName="_DBType_(60")]
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

    /// <summary>Gets or sets the DataConfigName value.</summary>
    //[Column("DataConfigName", TypeName="_DBType_(60")]
    public String DataConfigName
    {
      get { return mDataConfigName; }
      set
      {
        value = NetString.InitString(value);
        mDataConfigName = ChangedNames.Add(ColumnDataConfigName, mDataConfigName, value);
      }
    }
    private String mDataConfigName;

    /// <summary>Gets or sets the TableName value.</summary>
    //[Column("TableName", TypeName="_DBType_(60")]
    public String TableName
    {
      get { return mTableName; }
      set
      {
        value = NetString.InitString(value);
        mTableName = ChangedNames.Add(ColumnTableName, mTableName, value);
      }
    }
    private String mTableName;

    /// <summary>Gets or sets the UserID value.</summary>
    //[Column("UserID", TypeName="_DBType_(60")]
    public String UserID
    {
      get { return mUserID; }
      set
      {
        value = NetString.InitString(value);
        mUserID = ChangedNames.Add(ColumnUserID, mUserID, value);
      }
    }
    private String mUserID;

    /// <summary>Gets or sets the DataValueCount value.</summary>
    //[Column("DataValueCount", TypeName="_DBType_")]
    public Int32 DataValueCount
    {
      get { return mDataValueCount; }
      set
      {
        mDataValueCount = ChangedNames.Add(ColumnDataValueCount, mDataValueCount, value);
      }
    }
    private Int32 mDataValueCount;

    /// <summary>Gets or sets the ColumnRowsLimit value.</summary>
    //[Column("ColumnRowsLimit", TypeName="_DBType_")]
    public Int32 ColumnRowsLimit
    {
      get { return mColumnRowsLimit; }
      set
      {
        mColumnRowsLimit = ChangedNames.Add(ColumnColumnRowsLimit, mColumnRowsLimit, value);
      }
    }
    private Int32 mColumnRowsLimit;

    /// <summary>Gets or sets the PageColumnsLimit value.</summary>
    //[Column("PageColumnsLimit", TypeName="_DBType_")]
    public Int32 PageColumnsLimit
    {
      get { return mPageColumnsLimit; }
      set
      {
        mPageColumnsLimit = ChangedNames.Add(ColumnPageColumnsLimit, mPageColumnsLimit, value);
      }
    }
    private Int32 mPageColumnsLimit;

    /// <summary>Gets or sets the CharacterPixels value.</summary>
    //[Column("CharacterPixels", TypeName="_DBType_")]
    public Int32 CharacterPixels
    {
      get { return mCharacterPixels; }
      set
      {
        mCharacterPixels = ChangedNames.Add(ColumnCharacterPixels, mCharacterPixels, value);
      }
    }
    private Int32 mCharacterPixels;

    /// <summary>Gets or sets the MaxControlCharacters value.</summary>
    //[Column("MaxControlCharacters", TypeName="_DBType_")]
    public Int32 MaxControlCharacters
    {
      get { return mMaxControlCharacters; }
      set
      {
        mMaxControlCharacters = ChangedNames.Add(ColumnMaxControlCharacters, mMaxControlCharacters, value);
      }
    }
    private Int32 mMaxControlCharacters;

    /// <summary>Gets or sets the BorderHorizontal value.</summary>
    //[Column("BorderHorizontal", TypeName="_DBType_")]
    public Int32 BorderHorizontal
    {
      get { return mBorderHorizontal; }
      set
      {
        mBorderHorizontal = ChangedNames.Add(ColumnBorderHorizontal, mBorderHorizontal, value);
      }
    }
    private Int32 mBorderHorizontal;

    /// <summary>Gets or sets the BorderVertical value.</summary>
    //[Column("BorderVertical", TypeName="_DBType_")]
    public Int32 BorderVertical
    {
      get { return mBorderVertical; }
      set
      {
        mBorderVertical = ChangedNames.Add(ColumnBorderVertical, mBorderVertical, value);
      }
    }
    private Int32 mBorderVertical;

    /// <summary>Gets or sets the ControlRowSpacing value.</summary>
    //[Column("ControlRowSpacing", TypeName="_DBType_")]
    public Int32 ControlRowSpacing
    {
      get { return mControlRowSpacing; }
      set
      {
        mControlRowSpacing = ChangedNames.Add(ColumnControlRowSpacing, mControlRowSpacing, value);
      }
    }
    private Int32 mControlRowSpacing;

    /// <summary>Gets or sets the ControlRowHeight value.</summary>
    //[Column("ControlRowHeight", TypeName="_DBType_")]
    public Int32 ControlRowHeight
    {
      get { return mControlRowHeight; }
      set
      {
        mControlRowHeight = ChangedNames.Add(ColumnControlRowHeight, mControlRowHeight, value);
      }
    }
    private Int32 mControlRowHeight;

    /// <summary>Gets or sets the ControlsHeight value.</summary>
    //[Column("ControlsHeight", TypeName="_DBType_")]
    public Int32 ControlsHeight
    {
      get { return mControlsHeight; }
      set
      {
        mControlsHeight = ChangedNames.Add(ColumnControlsHeight, mControlsHeight, value);
      }
    }
    private Int32 mControlsHeight;

    /// <summary>Gets or sets the ColumnRowCount value.</summary>
    //[Column("ColumnRowCount", TypeName="_DBType_")]
    public Int32 ColumnRowCount
    {
      get { return mColumnRowCount; }
      set
      {
        mColumnRowCount = ChangedNames.Add(ColumnColumnRowCount, mColumnRowCount, value);
      }
    }
    private Int32 mColumnRowCount;
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
    public static string TableName = "ControlDetail";

    /// <summary>The ID column name.</summary>
    public static string ColumnID = "ID";

    /// <summary>The Name column name.</summary>
    public static string ColumnName = "Name";

    /// <summary>The Description column name.</summary>
    public static string ColumnDescription = "Description";

    /// <summary>The DataConfigName column name.</summary>
    public static string ColumnDataConfigName = "DataConfigName";

    /// <summary>The TableName column name.</summary>
    public static string ColumnTableName = "TableName";

    /// <summary>The UserID column name.</summary>
    public static string ColumnUserID = "UserID";

    /// <summary>The DataValueCount column name.</summary>
    public static string ColumnDataValueCount = "DataValueCount";

    /// <summary>The ColumnRowsLimit column name.</summary>
    public static string ColumnColumnRowsLimit = "ColumnRowsLimit";

    /// <summary>The PageColumnsLimit column name.</summary>
    public static string ColumnPageColumnsLimit = "PageColumnsLimit";

    /// <summary>The CharacterPixels column name.</summary>
    public static string ColumnCharacterPixels = "CharacterPixels";

    /// <summary>The MaxControlCharacters column name.</summary>
    public static string ColumnMaxControlCharacters = "MaxControlCharacters";

    /// <summary>The BorderHorizontal column name.</summary>
    public static string ColumnBorderHorizontal = "BorderHorizontal";

    /// <summary>The BorderVertical column name.</summary>
    public static string ColumnBorderVertical = "BorderVertical";

    /// <summary>The ControlRowSpacing column name.</summary>
    public static string ColumnControlRowSpacing = "ControlRowSpacing";

    /// <summary>The ControlRowHeight column name.</summary>
    public static string ColumnControlRowHeight = "ControlRowHeight";

    /// <summary>The ControlsHeight column name.</summary>
    public static string ColumnControlsHeight = "ControlsHeight";

    /// <summary>The ColumnRowCount column name.</summary>
    public static string ColumnColumnRowCount = "ColumnRowCount";

    /// <summary>The Name maximum length.</summary>
    public static int LengthName = 60;

    /// <summary>The Description maximum length.</summary>
    public static int LengthDescription = 60;

    /// <summary>The DataConfigName maximum length.</summary>
    public static int LengthDataConfigName = 60;

    /// <summary>The TableName maximum length.</summary>
    public static int LengthTableName = 60;

    /// <summary>The UserID maximum length.</summary>
    public static int LengthUserID = 60;
    #endregion

    #region Calculated and Join Class Data

    ///// <summary>The Join TypeName column name.</summary>
    //public static string ColumnTypeName = "TypeName";
    #endregion
  }

  #region Comparers

  /// <summary>Sort and search on Name value.</summary>
  public class ControlDetailUniqueComparer : IComparer<ControlDetail>
  {
    // Compares two objects.
    /// <include path='items/Compare/*' file='../../LJCDocLib/Common/Data.xml'/>
    public int Compare(ControlDetail x, ControlDetail y)
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
