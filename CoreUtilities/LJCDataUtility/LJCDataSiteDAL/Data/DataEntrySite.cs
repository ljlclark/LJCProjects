// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// DataEntrySite.cs
using LJCNetCommon;
using LJCDBClientLib;
using System;
using System.Collections.Generic;

namespace LJCDataSiteDAL
{
  /// <summary>The DataEntrySite Data Object.</summary>
  public class DataEntrySite : IComparable<DataEntrySite>
  {
    #region Constructor Methods

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../LJCGenDoc/Common/Data.xml'/>
    public DataEntrySite()
    {
      ChangedNames = new ChangedNames();
    }

    // The Copy constructor.
    /// <include path='items/CopyConstructor/*' file='../../LJCGenDoc/Common/Data.xml'/>
    public DataEntrySite(DataEntrySite item)
    {
      ChangedNames = new ChangedNames();
      DataEntryID = item.DataEntryID;
      DataEntrySiteID = item.DataEntrySiteID;
      DataSiteID = item.DataSiteID;
    }
    #endregion

    #region Data Methods

    // Creates and returns a clone of this object.
    /// <include path='items/Clone/*' file='../../LJCGenDoc/Common/Data.xml'/>
    public DataEntrySite Clone()
    {
      var retValue = MemberwiseClone() as DataEntrySite;
      return retValue;
    }

    // Provides the default Sort functionality.
    /// <include path='items/CompareTo/*' file='../../LJCGenDoc/Common/Data.xml'/>
    public int CompareTo(DataEntrySite other)
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
        retValue = DataEntryID.CompareTo(other.DataEntryID);
        if (retValue != 0)
        {
          isContinue = false;
        }
      }
      if (isContinue)
      {
        retValue = DataEntrySiteID.CompareTo(other.DataEntrySiteID);
        if (retValue != 0)
        {
          isContinue = false;
        }
      }
      if (isContinue)
      {
        retValue = DataSiteID.CompareTo(other.DataSiteID);
      }
      return retValue;
    }

    // The object string identifier.
    /// <include path='items/ToString/*' file='../../LJCGenDoc/Common/Data.xml'/>
    public override string ToString()
    {
      var retValue = $":{mDataEntryID}-{mDataSiteID}";
      return retValue;
    }
    #endregion

    #region Data Properties

    // Update ChangedNames.Add() statements to "Property" constant
    // if property was renamed.

    /// <summary>Gets or sets the DataEntryID value.</summary>
    //[Required]
    //[Column("DataEntryID", TypeName="bigint")]
    public Int64 DataEntryID
    {
      get { return mDataEntryID; }
      set
      {
        mDataEntryID = ChangedNames.Add(ColumnDataEntryID, mDataEntryID, value);
      }
    }
    private Int64 mDataEntryID;

    /// <summary>Gets or sets the DataEntrySiteID value.</summary>
    //[Required]
    //[Column("DataEntrySiteID", TypeName="bigint")]
    public Int64 DataEntrySiteID
    {
      get { return mDataEntrySiteID; }
      set
      {
        mDataEntrySiteID = ChangedNames.Add(ColumnDataEntrySiteID
          , mDataEntrySiteID, value);
      }
    }
    private Int64 mDataEntrySiteID;

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
    #endregion

    #region Class Properties

    /// <summary>Gets a reference to the ChangedNames list.</summary>
    public ChangedNames ChangedNames { get; private set; }
    #endregion

    #region Class Data

    /// <summary>The table name.</summary>
    public static string TableName = "DataEntrySite";

    /// <summary>The DataEntryID column name.</summary>
    public static string ColumnDataEntryID = "DataEntryID";

    /// <summary>The DataEntrySiteID column name.</summary>
    public static string ColumnDataEntrySiteID = "DataEntrySiteID";

    /// <summary>The DataSiteID column name.</summary>
    public static string ColumnDataSiteID = "DataSiteID";
    #endregion
  }
}
