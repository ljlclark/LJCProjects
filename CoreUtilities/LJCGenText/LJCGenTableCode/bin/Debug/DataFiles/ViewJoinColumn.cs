// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// ViewJoinColumn.cs
using LJCNetCommon;
using LJCDBClientLib;
using System;
using System.Collections.Generic;

namespace LJCAppName
{
  /// <summary>The ViewJoinColumn table Data Object.</summary>
  public class ViewJoinColumn : IComparable<ViewJoinColumn>
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public ViewJoinColumn()
    {
      ChangedNames = new ChangedNames();
    }

    // The Copy constructor.
    /// <include path='items/CopyConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public ViewJoinColumn(ViewJoinColumn item)
    {
      ChangedNames = new ChangedNames();
      ID = item.ID;
    }
    #endregion

    #region Data Methods

    // Creates and returns a clone of this object.
    /// <include path='items/Clone/*' file='../../LJCDocLib/Common/Data.xml'/>
    public ViewJoinColumn Clone()
    {
      var retValue = MemberwiseClone() as ViewJoinColumn;
      return retValue;
    }

    // Provides the default Sort functionality.
    /// <include path='items/CompareTo/*' file='../../LJCDocLib/Common/Data.xml'/>
    public int CompareTo(ViewJoinColumn other)
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

    /// <summary>Gets or sets the PropertyName value.</summary>
    //[Column("PropertyName", TypeName="_DBType_(60")]
    public String PropertyName
    {
      get { return mPropertyName; }
      set
      {
        value = NetString.InitString(value);
        mPropertyName = ChangedNames.Add(ColumnPropertyName, mPropertyName, value);
      }
    }
    private String mPropertyName;

    /// <summary>Gets or sets the RenameAs value.</summary>
    //[Column("RenameAs", TypeName="_DBType_(60")]
    public String RenameAs
    {
      get { return mRenameAs; }
      set
      {
        value = NetString.InitString(value);
        mRenameAs = ChangedNames.Add(ColumnRenameAs, mRenameAs, value);
      }
    }
    private String mRenameAs;

    /// <summary>Gets or sets the Caption value.</summary>
    //[Column("Caption", TypeName="_DBType_(60")]
    public String Caption
    {
      get { return mCaption; }
      set
      {
        value = NetString.InitString(value);
        mCaption = ChangedNames.Add(ColumnCaption, mCaption, value);
      }
    }
    private String mCaption;

    /// <summary>Gets or sets the DataTypeName value.</summary>
    //[Column("DataTypeName", TypeName="_DBType_(60")]
    public String DataTypeName
    {
      get { return mDataTypeName; }
      set
      {
        value = NetString.InitString(value);
        mDataTypeName = ChangedNames.Add(ColumnDataTypeName, mDataTypeName, value);
      }
    }
    private String mDataTypeName;

    /// <summary>Gets or sets the Value value.</summary>
    //[Column("Value", TypeName="_DBType_(60")]
    public String Value
    {
      get { return mValue; }
      set
      {
        value = NetString.InitString(value);
        mValue = ChangedNames.Add(ColumnValue, mValue, value);
      }
    }
    private String mValue;
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
    public static string TableName = "ViewJoinColumn";

    /// <summary>The ID column name.</summary>
    public static string ColumnID = "ID";

    /// <summary>The ViewJoinID column name.</summary>
    public static string ColumnViewJoinID = "ViewJoinID";

    /// <summary>The ColumnName column name.</summary>
    public static string ColumnColumnName = "ColumnName";

    /// <summary>The PropertyName column name.</summary>
    public static string ColumnPropertyName = "PropertyName";

    /// <summary>The RenameAs column name.</summary>
    public static string ColumnRenameAs = "RenameAs";

    /// <summary>The Caption column name.</summary>
    public static string ColumnCaption = "Caption";

    /// <summary>The DataTypeName column name.</summary>
    public static string ColumnDataTypeName = "DataTypeName";

    /// <summary>The Value column name.</summary>
    public static string ColumnValue = "Value";

    /// <summary>The ColumnName maximum length.</summary>
    public static int LengthColumnName = 60;

    /// <summary>The PropertyName maximum length.</summary>
    public static int LengthPropertyName = 60;

    /// <summary>The RenameAs maximum length.</summary>
    public static int LengthRenameAs = 60;

    /// <summary>The Caption maximum length.</summary>
    public static int LengthCaption = 60;

    /// <summary>The DataTypeName maximum length.</summary>
    public static int LengthDataTypeName = 60;

    /// <summary>The Value maximum length.</summary>
    public static int LengthValue = 60;
    #endregion

    #region Calculated and Join Class Data

    ///// <summary>The Join TypeName column name.</summary>
    //public static string ColumnTypeName = "TypeName";
    #endregion
  }

  #region Comparers

  /// <summary>Sort and search on Name value.</summary>
  public class ViewJoinColumnUniqueComparer : IComparer<ViewJoinColumn>
  {
    // Compares two objects.
    /// <include path='items/Compare/*' file='../../LJCDocLib/Common/Data.xml'/>
    public int Compare(ViewJoinColumn x, ViewJoinColumn y)
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
