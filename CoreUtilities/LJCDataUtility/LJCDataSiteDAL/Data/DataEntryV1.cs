﻿// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// DataEntry.cs
using LJCNetCommon;
using LJCDBClientLib;
using System;
using System.Collections.Generic;

namespace LJCDataSiteDAL
{
  /// <summary>The DataEntry Data Object.</summary>
  public class DataEntry : IComparable<DataEntry>
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public DataEntry()
    {
      ChangedNames = new ChangedNames();
    }

    // The Copy constructor.
    /// <include path='items/CopyConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public DataEntry(DataEntry item)
    {
      ChangedNames = new ChangedNames();
      ID = item.ID;
      DataSiteID = item.DataSiteID;
      EntryTime = item.EntryTime;
      ModuleID = item.ModuleID;
      TableID = item.TableID;
      EntryType = item.EntryType;
      DataConfigName = item.DataConfigName;
      PublishTime = item.PublishTime;
      EntryData = item.EntryData;
    }
    #endregion

    #region Data Methods

    // Creates and returns a clone of this object.
    /// <include path='items/Clone/*' file='../../LJCDocLib/Common/Data.xml'/>
    public DataEntry Clone()
    {
      var retValue = MemberwiseClone() as DataEntry;
      return retValue;
    }

    // Provides the default Sort functionality.
    /// <include path='items/CompareTo/*' file='../../LJCDocLib/Common/Data.xml'/>
    public int CompareTo(DataEntry other)
    {
      int retValue = -2;

      var isContinue = true;
      if (null == other)
      {
        retValue = 1;
        isContinue = false;
      }
      if (isContinue)
      {
        retValue = ID.CompareTo(other.ID);
      }
      return retValue;
    }

    // The object string identifier.
    /// <include path='items/ToString/*' file='../../LJCDocLib/Common/Data.xml'/>
    public override string ToString()
    {
      var retValue = $":{mID}-{mDataSiteID}";
      return retValue;
    }
    #endregion

    #region Data Properties

    // Update ChangedNames.Add() statements to "Property" constant
    // if property was renamed.

    /// <summary>Gets or sets the ID value.</summary>
    //[Required]
    //[Column("ID", TypeName="bigint")]
    public Int64 ID
    {
      get { return mID; }
      set
      {
        mID = ChangedNames.Add(ColumnID, mID, value);
      }
    }
    private Int64 mID;

    /// <summary>Gets or sets the DataSiteID value.</summary>
    //[Required]
    //[Column("DataSiteID", TypeName="bigint")]
    public Int64 DataSiteID
    {
      get { return mDataSiteID; }
      set
      {
        mDataSiteID = ChangedNames.Add(ColumnDataSiteID, mDataSiteID, value);
      }
    }
    private Int64 mDataSiteID;

    /// <summary>Gets or sets the EntryTime value.</summary>
    //[Required]
    //[Column("EntryTime", TypeName="datetime")]
    public DateTime EntryTime
    {
      get { return mEntryTime; }
      set
      {
        mEntryTime = ChangedNames.Add(ColumnEntryTime, mEntryTime, value);
      }
    }
    private DateTime mEntryTime;

    /// <summary>Gets or sets the ModuleID value.</summary>
    //[Required]
    //[Column("ModuleID", TypeName="bigint")]
    public Int64 ModuleID
    {
      get { return mModuleID; }
      set
      {
        mModuleID = ChangedNames.Add(ColumnModuleID, mModuleID, value);
      }
    }
    private Int64 mModuleID;

    /// <summary>Gets or sets the TableID value.</summary>
    //[Required]
    //[Column("TableID", TypeName="bigint")]
    public Int64 TableID
    {
      get { return mTableID; }
      set
      {
        mTableID = ChangedNames.Add(ColumnTableID, mTableID, value);
      }
    }
    private Int64 mTableID;

    /// <summary>Gets or sets the EntryType value.</summary>
    //[Required]
    //[Column("EntryType", TypeName="varchar(10")]
    public String EntryType
    {
      get { return mEntryType; }
      set
      {
        value = NetString.InitString(value);
        mEntryType = ChangedNames.Add(ColumnEntryType, mEntryType, value);
      }
    }
    private String mEntryType;

    /// <summary>Gets or sets the DataConfigName value.</summary>
    //[Required]
    //[Column("DataConfigName", TypeName="varchar(60")]
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

    /// <summary>Gets or sets the PublishTime value.</summary>
    //[Required]
    //[Column("PublishTime", TypeName="datetime")]
    public DateTime PublishTime
    {
      get { return mPublishTime; }
      set
      {
        mPublishTime = ChangedNames.Add(ColumnPublishTime, mPublishTime, value);
      }
    }
    private DateTime mPublishTime;

    /// <summary>Gets or sets the EntryData value.</summary>
    //[Required]
    //[Column("EntryData", TypeName="varchar(4000")]
    public String EntryData
    {
      get { return mEntryData; }
      set
      {
        value = NetString.InitString(value);
        mEntryData = ChangedNames.Add(ColumnEntryData, mEntryData, value);
      }
    }
    private String mEntryData;
    #endregion

    #region Class Properties

    /// <summary>Gets a reference to the ChangedNames list.</summary>
    public ChangedNames ChangedNames { get; private set; }
    #endregion

    #region Class Data

    /// <summary>The table name.</summary>
    public static string TableName = "DataEntry";

    /// <summary>The ID column name.</summary>
    public static string ColumnID = "ID";

    /// <summary>The DataSiteID column name.</summary>
    public static string ColumnDataSiteID = "DataSiteID";

    /// <summary>The EntryTime column name.</summary>
    public static string ColumnEntryTime = "EntryTime";

    /// <summary>The ModuleID column name.</summary>
    public static string ColumnModuleID = "ModuleID";

    /// <summary>The TableID column name.</summary>
    public static string ColumnTableID = "TableID";

    /// <summary>The EntryType column name.</summary>
    public static string ColumnEntryType = "EntryType";

    /// <summary>The DataConfigName column name.</summary>
    public static string ColumnDataConfigName = "DataConfigName";

    /// <summary>The PublishTime column name.</summary>
    public static string ColumnPublishTime = "PublishTime";

    /// <summary>The EntryData column name.</summary>
    public static string ColumnEntryData = "EntryData";

    /// <summary>The EntryType maximum length.</summary>
    public static int LengthEntryType = 10;

    /// <summary>The DataConfigName maximum length.</summary>
    public static int LengthDataConfigName = 60;

    /// <summary>The EntryData maximum length.</summary>
    public static int LengthEntryData = 4000;
    #endregion
  }

  #region Comparers

  /// <summary>Sort and search on Name value.</summary>
  public class DataEntryUniqueComparer : IComparer<DataEntry>
  {
    // Compares two objects.
    /// <include path='items/Compare/*' file='../../LJCDocLib/Common/Data.xml'/>
    public int Compare(DataEntry x, DataEntry y)
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
        retValue = x.DataSiteID.CompareTo(y.DataSiteID);
        if (retValue != 0)
        {
          isContinue = false;
        }
      }
      if (isContinue)
      {
        retValue = x.EntryTime.CompareTo(y.EntryTime);
        if (retValue != 0)
        {
          isContinue = false;
        }
      }
      if (isContinue)
      {
        retValue = x.ModuleID.CompareTo(y.ModuleID);
        if (retValue != 0)
        {
          isContinue = false;
        }
      }
      if (isContinue)
      {
        retValue = x.TableID.CompareTo(y.TableID);
      }
      return retValue;
    }
  }
  #endregion
}
