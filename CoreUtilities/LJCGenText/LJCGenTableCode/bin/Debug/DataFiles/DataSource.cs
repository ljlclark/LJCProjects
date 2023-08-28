// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// DataSource.cs
using LJCNetCommon;
using LJCDBClientLib;
using System;
using System.Collections.Generic;

namespace LJCAppName
{
  /// <summary>The DataSource table Data Object.</summary>
  public class DataSource : IComparable<DataSource>
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public DataSource()
    {
      ChangedNames = new ChangedNames();
    }

    // The Copy constructor.
    /// <include path='items/CopyConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public DataSource(DataSource item)
    {
      ChangedNames = new ChangedNames();
      ID = item.ID;
    }
    #endregion

    #region Data Methods

    // Creates and returns a clone of this object.
    /// <include path='items/Clone/*' file='../../LJCDocLib/Common/Data.xml'/>
    public DataSource Clone()
    {
      var retValue = MemberwiseClone() as DataSource;
      return retValue;
    }

    // Provides the default Sort functionality.
    /// <include path='items/CompareTo/*' file='../../LJCDocLib/Common/Data.xml'/>
    public int CompareTo(DataSource other)
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

    /// <summary>Gets or sets the DataSourceID value.</summary>
    //[Column("DataSourceID", TypeName="_DBType_")]
    public Int32 DataSourceID
    {
      get { return mDataSourceID; }
      set
      {
        mDataSourceID = ChangedNames.Add(ColumnDataSourceID, mDataSourceID, value);
      }
    }
    private Int32 mDataSourceID;

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

    /// <summary>Gets or sets the SourceTypeID value.</summary>
    //[Column("SourceTypeID", TypeName="_DBType_")]
    public Int16 SourceTypeID
    {
      get { return mSourceTypeID; }
      set
      {
        mSourceTypeID = ChangedNames.Add(ColumnSourceTypeID, mSourceTypeID, value);
      }
    }
    private Int16 mSourceTypeID;

    /// <summary>Gets or sets the DataConfigName value.</summary>
    //[Column("DataConfigName", TypeName="_DBType_(100")]
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

    /// <summary>Gets or sets the SourceItemName value.</summary>
    //[Column("SourceItemName", TypeName="_DBType_(100")]
    public String SourceItemName
    {
      get { return mSourceItemName; }
      set
      {
        value = NetString.InitString(value);
        mSourceItemName = ChangedNames.Add(ColumnSourceItemName, mSourceItemName, value);
      }
    }
    private String mSourceItemName;

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

    /// <summary>Gets or sets the SourceStatusID value.</summary>
    //[Column("SourceStatusID", TypeName="_DBType_")]
    public Int16 SourceStatusID
    {
      get { return mSourceStatusID; }
      set
      {
        mSourceStatusID = ChangedNames.Add(ColumnSourceStatusID, mSourceStatusID, value);
      }
    }
    private Int16 mSourceStatusID;
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
    public static string TableName = "DataSource";

    /// <summary>The DataSourceID column name.</summary>
    public static string ColumnDataSourceID = "DataSourceID";

    /// <summary>The Name column name.</summary>
    public static string ColumnName = "Name";

    /// <summary>The Description column name.</summary>
    public static string ColumnDescription = "Description";

    /// <summary>The SourceTypeID column name.</summary>
    public static string ColumnSourceTypeID = "SourceTypeID";

    /// <summary>The DataConfigName column name.</summary>
    public static string ColumnDataConfigName = "DataConfigName";

    /// <summary>The SourceItemName column name.</summary>
    public static string ColumnSourceItemName = "SourceItemName";

    /// <summary>The SourceLayoutID column name.</summary>
    public static string ColumnSourceLayoutID = "SourceLayoutID";

    /// <summary>The SourceStatusID column name.</summary>
    public static string ColumnSourceStatusID = "SourceStatusID";

    /// <summary>The Name maximum length.</summary>
    public static int LengthName = 60;

    /// <summary>The Description maximum length.</summary>
    public static int LengthDescription = 100;

    /// <summary>The DataConfigName maximum length.</summary>
    public static int LengthDataConfigName = 100;

    /// <summary>The SourceItemName maximum length.</summary>
    public static int LengthSourceItemName = 100;
    #endregion

    #region Calculated and Join Class Data

    ///// <summary>The Join TypeName column name.</summary>
    //public static string ColumnTypeName = "TypeName";
    #endregion
  }

  #region Comparers

  /// <summary>Sort and search on Name value.</summary>
  public class DataSourceUniqueComparer : IComparer<DataSource>
  {
    // Compares two objects.
    /// <include path='items/Compare/*' file='../../LJCDocLib/Common/Data.xml'/>
    public int Compare(DataSource x, DataSource y)
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
