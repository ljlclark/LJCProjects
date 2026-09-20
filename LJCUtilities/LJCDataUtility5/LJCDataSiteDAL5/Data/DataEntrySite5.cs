// Copyright (c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// DataEntrySite5.cs
using LJCDBMessage5;
using LJCNetCommon5;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LJCDataSiteDAL5
{
  /// <summary>The DataEntrySite Data Object.</summary>
  public class DataEntrySite : IComparable<DataEntrySite>
  {
    #region Constructor Methods

    // Initializes an object instance.
    /// <include file='../../LJCGenDoc/Common/Data.xml'
    ///  path='members/DefaultConstructor/*'/>
    public DataEntrySite()
    {
      ChangedNames = [];
    }

    // The Copy constructor.
    /// <include file='../../LJCGenDoc/Common/Data.xml'
    ///  path='members/CopyConstructor/*'/>
    public DataEntrySite(DataEntrySite item)
    {
      ChangedNames = [];
      DataEntryID = item.DataEntryID;
      DataEntrySiteID = item.DataEntrySiteID;
      DataSiteID = item.DataSiteID;
    }
    #endregion

    #region Data Methods

    // Creates and returns a clone of this object.
    /// <include file='../../LJCGenDoc/Common/Data.xml'
    ///  path='members/Clone/*'/>
    public DataEntrySite? Clone()
    {
      var retValue = MemberwiseClone() as DataEntrySite;
      return retValue;
    }

    // Provides the default Sort functionality.
    /// <include file='../../LJCGenDoc/Common/Data.xml'
    ///  path='members/CompareTo/*'/>
    public int CompareTo(DataEntrySite? other)
    {
      int retValue;

      while (true)
      {
        if (null == other)
        {
          // This object is greater than null.
          retValue = LJCNetString.CompareGreater;
          break;
        }

        retValue = DataEntryID.CompareTo(other.DataEntryID);
        if (retValue != LJCNetString.CompareEqual)
        {
          break;
        }

        retValue = DataEntrySiteID.CompareTo(other.DataEntrySiteID);
        if (retValue != LJCNetString.CompareEqual)
        {
          break;
        }

        retValue = DataSiteID.CompareTo(other.DataSiteID);
        break;
      }
      return retValue;
    }

    // The object string identifier.
    /// <include file='../../LJCGenDoc/Common/Data.xml'
    ///  path='members/ToString/*'/>
    public override string ToString()
    {
      var retValue = $":{_DataEntryID}-{_DataSiteID}";
      return retValue;
    }
    #endregion

    #region Data Properties

    // Update ChangedNames.Add() statements to "Property" constant
    // if property was renamed.

    // Gets or sets the DataEntrySiteID value.
    /// <include file='Doc/DataEntrySite.xml'
    ///  path='members/DataEntrySiteID/*'/>
    [Required]
    [Column("DataEntrySiteID", TypeName="bigint")]
    public long DataEntrySiteID
    {
      get => _DataEntrySiteID;
      set
      {
        if (_DataEntrySiteID != value)
        {
          _DataEntrySiteID = ChangedNames.Add(ColumnDataEntrySiteID
          , _DataEntrySiteID, value);
        }
      }
    }
    private long _DataEntrySiteID;

    // Gets or sets the DataEntryID value.
    /// <include file='Doc/DataEntrySite.xml'
    ///  path='members/DataEntryID/*'/>
    //[Required]
    //[Column("DataEntryID", TypeName="bigint")]
    public long DataEntryID
    {
      get => _DataEntryID;
      set
      {
        if (_DataEntryID != value)
        {
          _DataEntryID = ChangedNames.Add(ColumnDataEntryID, _DataEntryID, value);
        }
      }
    }
    private long _DataEntryID;

    // Gets or sets the DataSiteID value.
    /// <include file='Doc/DataEntrySite.xml'
    ///  path='members/DataSiteID/*'/>
    //[Required]
    //[Column("DataSiteID", TypeName="bigint")]
    public long DataSiteID
    {
      get => _DataSiteID;
      set
      {
        if (_DataSiteID != value)
        {
          _DataSiteID = ChangedNames.Add(ColumnDataSiteID, _DataSiteID, value);
        }
      }
    }
    private long _DataSiteID;
    #endregion

    #region Class Properties

    // Gets a reference to the ChangedNames list.
    /// <include file='Doc/DataEntrySite.xml'
    ///  path='members/ChangedNames/*'/>
    public LJCChangedNames ChangedNames { get; private set; }
    #endregion

    #region Class Data

    /// <summary>The table name.</summary>
    public const string TableName = "DataEntrySite";

    /// <summary>The DataEntryID column name.</summary>
    public const string ColumnDataEntryID = "DataEntryID";

    /// <summary>The DataEntrySiteID column name.</summary>
    public const string ColumnDataEntrySiteID = "DataEntrySiteID";

    /// <summary>The DataSiteID column name.</summary>
    public const string ColumnDataSiteID = "DataSiteID";
    #endregion
  }
}
