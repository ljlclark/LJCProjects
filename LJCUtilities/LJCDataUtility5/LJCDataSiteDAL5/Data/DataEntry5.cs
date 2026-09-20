// Copyright (c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// DataEntry.cs
using LJCDBMessage5;
using LJCNetCommon5;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace LJCDataSiteDAL5
{
  // The DataEntry Data Object.
  /// <include file='Doc/DataEntry.xml'
  ///  path='members/DataEntry/*'/>
  public class DataEntry : IComparable<DataEntry>
  {
    #region Constructor Methods

    // Initializes an object instance.
    /// <include file='../../LJCGenDoc/Common/Data.xml'
    ///  path='members/DefaultConstructor/*'/>
    public DataEntry()
    {
      _Id = 0;
      _DataSiteId = 0;

      _EntryTime = DateTime.MinValue;
      _EntryData = "";

      ChangedNames = new LJCChangedNames();
    }

    // The Copy constructor.
    /// <include file='../../LJCGenDoc/Common/Data.xml'
    ///  path='members/CopyConstructor/*'/>
    public DataEntry(DataEntry item)
    {
      _Id = item.ID;
      _DataSiteId = item.DataSiteID;

      _EntryTime = item.EntryTime;
      _EntryData = item.EntryData;
      ChangedNames = new LJCChangedNames();
    }
    #endregion

    #region Data Object Methods

    // Creates and returns a clone of this object.
    /// <include file='../../LJCGenDoc/Common/Data.xml'
    ///  path='members/Clone/*'/>
    public DataEntry? Clone()
    {
      var retValue = MemberwiseClone() as DataEntry;
      return retValue;
    }

    // Provides the default Sort functionality.
    /// <include file='../../LJCGenDoc/Common/Data.xml'
    ///  path='members/CompareTo/*'/>
    public int CompareTo(DataEntry? other)
    {
      int retValue;

      while (true)
      {
        if (null == other)
        {
          // This value is greater than null.
          retValue = 1;
          break;
        }

        retValue = ID.CompareTo(other.ID);
        break;
      }
      return retValue;
    }

    // The object string identifier.
    /// <include file='../../LJCGenDoc/Common/Data.xml'
    ///  path='members/ToString/*'/>
    public override string ToString()
    {
      var retValue = $":{_Id}-{_DataSiteId}";
      return retValue;
    }
    #endregion

    #region Data Properties

    // Update ChangedNames.Add() statements to "Property" constant
    // if property was renamed.

    // Gets or sets the DataSiteID value.
    /// <include file='doc/DataEntry.xml'
    ///  path='members/DataSiteId/*'/>
    [Required]
    [Column("DataSiteID", TypeName = "bigint")]
    public long DataSiteID
    {
      get => _DataSiteId;
      set
      {
        if (_DataSiteId != value)
        {
          _DataSiteId = ChangedNames.Add(ColumnDataSiteID, _DataSiteId, value);
        }
      }
    }
    private long _DataSiteId;

    // Gets or sets the ID value.
    /// <include file='doc/DataEntry.xml'
    ///  path='members/Id/*'/>
    [Required]
    [Column("ID", TypeName = "bigint")]
    public long ID
    {
      get => _Id;
      set
      {
        if (_Id != value)
        {
          _Id = ChangedNames.Add(ColumnID, _Id, value);
        }
      }
    }
    private long _Id;

    // Gets or sets the EntryTime value.
    /// <include file='doc/DataEntry.xml'
    ///  path='members/EntryTime/*'/>
    [Required]
    [Column("EntryTime", TypeName = "datetime")]
    public DateTime EntryTime
    {
      get => _EntryTime;
      set
      {
        if (_EntryTime != value)
        {
          _EntryTime = ChangedNames.Add(ColumnEntryTime, _EntryTime, value);
        }
      }
    }
    private DateTime _EntryTime;

    // Gets or sets the EntryData value.
    /// <include file='doc/DataEntry.xml'
    ///  path='members/EntryData/*'/>
    [Required]
    [Column("EntryData", TypeName = "varchar(4000")]
    public string EntryData
    {
      get => _EntryData;
      set
      {
        var newValue = value?.Trim();
        if (LJC.HasText(newValue)
          && _EntryData != newValue)
        {
          _EntryData = ChangedNames.Add(ColumnEntryData, _EntryData, newValue);
        }
      }
    }
    private string _EntryData;
    #endregion

    #region Class Properties

    // Gets a reference to the ChangedNames list.
    /// <include file='../../LJCGenDoc/Common/Data.xml'
    ///  path='members/ChangedNames/*'/>
    [XmlIgnore]
    public LJCChangedNames ChangedNames { get; private set; }
    #endregion

    #region Class Data

    /// <summary>The table name.</summary>
    public const string TableName = "DataEntry";

    /// <summary>The ID column name.</summary>
    public const string ColumnID = "ID";

    /// <summary>The DataSiteID column name.</summary>
    public const string ColumnDataSiteID = "DataSiteID";

    /// <summary>The EntryTime column name.</summary>
    public const string ColumnEntryTime = "EntryTime";

    /// <summary>The EntryData column name.</summary>
    public const string ColumnEntryData = "EntryData";

    /// <summary>The EntryData maximum length.</summary>
    public const int LengthEntryData = 4000;
    #endregion
  }

  #region Comparers

  // Sort and search on Name value.
  /// <include file='Doc/DataTable.xml'
  ///  path='members/DataEntryUnique/*'/>
  public class DataEntryUnique : IComparer<DataEntry>
  {
    // Compares two objects.
    /// <include file='../../LJCGenDoc/Common/Data.xml'
    ///  path='members/Compare/*'/>
    public int Compare(DataEntry? x, DataEntry? y)
    {
      int retValue;

      while (true)
      {
        retValue = LJC.CompareNull(x, y);
        if (retValue != LJCNetString.CompareNotNullOrEqual)
        {
          break;
        }

        retValue = x!.DataSiteID.CompareTo(y!.DataSiteID);
        if (retValue != LJCNetString.CompareEqual)
        {
          break;
        }

        retValue = x.EntryTime.CompareTo(y.EntryTime);
        break;
      }
      return retValue;
    }
  }
  #endregion
}
