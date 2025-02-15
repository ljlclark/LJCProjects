﻿// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// DataKey.cs
using LJCNetCommon;
using LJCDBClientLib;
using System;
using System.Collections.Generic;
using LJCDataUtilityDAL;

namespace LJCDataUtilityDAL
{
  /// <summary>The DataKey table Data Object.</summary>
  public class DataKey : IComparable<DataKey>
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../LJCGenDoc/Common/Data.xml'/>
    public DataKey()
    {
      ChangedNames = new ChangedNames();
    }

    // The Copy constructor.
    /// <include path='items/CopyConstructor/*' file='../../LJCGenDoc/Common/Data.xml'/>
    public DataKey(DataKey item)
    {
      ChangedNames = new ChangedNames();
      ID = item.ID;
      DataSiteID = item.DataSiteID;
      DataTableID = item.DataTableID;
      DataTableSiteID = item.DataTableSiteID;
      Name = item.Name;
      KeyType = item.KeyType;
      SourceColumnName = item.SourceColumnName;
      TargetTableName = item.TargetTableName;
      TargetColumnName = item.TargetColumnName;
      IsClustered = item.IsClustered;
      IsAscending = item.IsAscending;
    }
    #endregion

    #region Data Class Methods

    // Adds changed propertynames.
    /// <summary>
    /// Adds changed propertynames.
    /// </summary>
    /// <param name="propertyNames">The property name list.</param>
    public void AddChangedNames(List<string> propertyNames)
    {
      foreach (string propertyName in propertyNames)
      {
        var name = ChangedNames.FindName(propertyName);
        if (null == name)
        {
          ChangedNames.Add(propertyName);
        }
      }
    }

    // Creates and returns a clone of this object.
    /// <include path='items/Clone/*' file='../../LJCGenDoc/Common/Data.xml'/>
    public DataKey Clone()
    {
      var retValue = MemberwiseClone() as DataKey;
      return retValue;
    }

    // Provides the default Sort functionality.
    /// <include path='items/CompareTo/*' file='../../LJCGenDoc/Common/Data.xml'/>
    public int CompareTo(DataKey other)
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
        retValue = ID.CompareTo(other.ID);

        // Not case sensitive.
        //retValue = string.Compare(ID, other.ID, true);
      }
      return retValue;
    }

    // The object string identifier.
    /// <include path='items/ToString/*' file='../../LJCGenDoc/Common/Data.xml'/>
    public override string ToString()
    {
      var retValue = $"{mName}:{mID}";
      return retValue;
    }
    #endregion

    #region Data Properties

    // Update ChangedNames.Add() statements to "Property" constant
    // if property was renamed.

    /// <summary>Gets or sets the ID value.</summary>
    //[Required]
    //[Column("ID", TypeName="int")]
    public Int64 ID
    {
      get { return mID; }
      set
      {
        mID = ChangedNames.Add(ColumnID
          , mID, value);
      }
    }
    private Int64 mID;

    /// <summary>Gets or sets the ID value.</summary>
    //[Required]
    //[Column("ID", TypeName="bigint")]
    public Int64 DataSiteID
    {
      get { return mDataSiteID; }
      set
      {
        mDataSiteID = ChangedNames.Add(ColumnDataSiteID, mDataSiteID, value);
      }
    }
    private Int64 mDataSiteID;

    /// <summary>Gets or sets the DataTableID value.</summary>
    //[Required]
    //[Column("DataTableID", TypeName="int")]
    public Int64 DataTableID
    {
      get { return mDataTableID; }
      set
      {
        mDataTableID = ChangedNames.Add(ColumnDataTableID
          , mDataTableID, value);
      }
    }
    private Int64 mDataTableID;

    /// <summary>Gets or sets the DataTableID value.</summary>
    //[Required]
    //[Column("DataTableSiteID", TypeName="bigint")]
    public Int64 DataTableSiteID
    {
      get { return mDataTableSiteID; }
      set
      {
        mDataTableSiteID = ChangedNames.Add(ColumnDataTableSiteID
          , mDataTableSiteID, value);
      }
    }
    private Int64 mDataTableSiteID;

    /// <summary>Gets or sets the Name value.</summary>
    //[Required]
    //[Column("Name", TypeName="nvarchar(60")]
    public String Name
    {
      get { return mName; }
      set
      {
        value = NetString.InitString(value);
        mName = ChangedNames.Add(ColumnName
          , mName, value);
      }
    }
    private String mName;

    /// <summary>Gets or sets the KeyType value.</summary>
    //[Required]
    //[Column("KeyType", TypeName="smallint")]
    public Int16 KeyType
    {
      get { return mKeyType; }
      set
      {
        mKeyType = ChangedNames.Add(ColumnKeyType
          , mKeyType, value);
      }
    }
    private Int16 mKeyType;

    /// <summary>Gets or sets the SourceColumnName value.</summary>
    //[Column("SourceColumnName", TypeName="nvarchar(60")]
    public String SourceColumnName
    {
      get { return mSourceColumnName; }
      set
      {
        //value = NetString.InitString(value);
        value = NetString.ScrubDelimitedValues(value);
        mSourceColumnName = ChangedNames.Add(ColumnSourceColumnName
          , mSourceColumnName, value);
      }
    }
    private String mSourceColumnName;

    /// <summary>Gets or sets the TargetTableName value.</summary>
    //[Column("TargetTableName", TypeName="nvarchar(60")]
    public String TargetTableName
    {
      get { return mTargetTableName; }
      set
      {
        value = NetString.InitString(value);
        mTargetTableName = ChangedNames.Add(ColumnTargetTableName
          , mTargetTableName, value);
      }
    }
    private String mTargetTableName;

    /// <summary>Gets or sets the TargetColumnName value.</summary>
    //[Column("TargetColumnName", TypeName="nvarchar(60")]
    public String TargetColumnName
    {
      get { return mTargetColumnName; }
      set
      {
        value = NetString.InitString(value);
        mTargetColumnName = ChangedNames.Add(ColumnTargetColumnName
          , mTargetColumnName, value);
      }
    }
    private String mTargetColumnName;

    /// <summary>Gets or sets the IsClustered value.</summary>
    //[Column("IsClustered", TypeName="bit")]
    public Boolean IsClustered
    {
      get { return mIsClustered; }
      set
      {
        mIsClustered = ChangedNames.Add(ColumnIsClustered
          , mIsClustered, value);
      }
    }
    private Boolean mIsClustered;

    /// <summary>Gets or sets the IsAscending value.</summary>
    //[Column("IsAscending", TypeName="bit")]
    public Boolean IsAscending
    {
      get { return mIsAscending; }
      set
      {
        mIsAscending = ChangedNames.Add(ColumnIsAscending
          , mIsAscending, value);
      }
    }
    private Boolean mIsAscending;
    #endregion

    #region Calculated and Join Data Properties

    /// <summary>Gets or sets the Join TableName value.</summary>
    public string DataTableName { get; set; }
    #endregion

    #region Class Properties

    /// <summary>Gets a reference to the ChangedNames list.</summary>
    public ChangedNames ChangedNames { get; private set; }
    #endregion

    #region Calculated and Join Class Data

    /// <summary>The Join ModuleName column name.</summary>
    public static string ColumnDataTableName = "DataTableName";
    #endregion

    #region Class Data

    /// <summary>The table name.</summary>
    public static string TableName = "DataKey";

    /// <summary>The ID column name.</summary>
    public static string ColumnID = "ID";

    /// <summary>The DataSiteID column name.</summary>
    public static string ColumnDataSiteID = "DataSiteID";

    /// <summary>The DataTableID column name.</summary>
    public static string ColumnDataTableID = "DataTableID";

    /// <summary>The DataTableSiteID column name.</summary>
    public static string ColumnDataTableSiteID = "DataTableSiteID";

    /// <summary>The Name column name.</summary>
    public static string ColumnName = "Name";

    /// <summary>The KeyType column name.</summary>
    public static string ColumnKeyType = "KeyType";

    /// <summary>The SourceColumnName column name.</summary>
    public static string ColumnSourceColumnName = "SourceColumnName";

    /// <summary>The SourceColumnNames column name.</summary>
    public static string PropertySourceColumnNames = "SourceColumnNames";

    /// <summary>The TargetTableName column name.</summary>
    public static string ColumnTargetTableName = "TargetTableName";

    /// <summary>The TargetColumnName column name.</summary>
    public static string ColumnTargetColumnName = "TargetColumnName";

    /// <summary>The TargetColumnName column name.</summary>
    public static string PropertyTargetColumnNames = "TargetColumnNames";

    /// <summary>The IsClustered column name.</summary>
    public static string ColumnIsClustered = "IsClustered";

    /// <summary>The IsAscending column name.</summary>
    public static string ColumnIsAscending = "IsAscending";

    /// <summary>The Name maximum length.</summary>
    public static int LengthName = 60;

    /// <summary>The SourceColumnName maximum length.</summary>
    public static int LengthSourceColumnName = 60;

    /// <summary>The TargetTableName maximum length.</summary>
    public static int LengthTargetTableName = 60;

    /// <summary>The TargetColumnName maximum length.</summary>
    public static int LengthTargetColumnName = 60;
    #endregion
  }

  #region Comparers

  /// <summary>Sort and search on Name value.</summary>
  public class DataKeyUniqueComparer : IComparer<DataKey>
  {
    // Compares two objects.
    /// <include path='items/Compare/*' file='../../LJCGenDoc/Common/Data.xml'/>
    public int Compare(DataKey x, DataKey y)
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
        retValue = NetCommon.CompareNull(x.Name, y.Name);
        if (retValue != -2)
        {
          isContinue = false;
        }
      }
      if (isContinue)
      {
        retValue = x.DataTableID.CompareTo(y.DataTableID);
        if (retValue != 0)
        {
          isContinue = false;
        }
      }
      if (isContinue)
      {
        retValue = x.Name.CompareTo(y.Name);
      }
      return retValue;
    }
  }
  #endregion
}
