// Copyright (c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// DataKey.cs
using LJCDBClientLib;
using LJCNetCommon;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;

namespace LJCDataUtilityDAL
{
  // The DataKey data.
  /// <include file='Doc/DataKey.xml'
  ///  path='members/DataKey/*'/>
  public class DataKey : IComparable<DataKey>
  {
    #region Constructor Methods

    // Initializes an object instance.
    /// <include file='../../LJCGenDoc/Common/Data.xml'
    ///  path='members/Constructor/*'/>
    public DataKey()
    {
      _ID = 0;
      _DataDbID = 0;
      _DataTableID = 0;
      _DataTableDbID = 0;
      _Name = "";
      _KeyType = 0;

      _IsClustered = false;
      _IsAscending = false;
      _SourceColumnName = "";
      _TargetTableName = null;
      _TargetColumnName = null;

      ChangedNames = new ChangedNames();
      _OriginalValues = new OriginalValues();
      LJCSetOriginalValues();
    }

    // Initializes an object instance with the supplied values.
    /// <include file='Doc/DataKey.xml'
    ///  path='members/ParamConstructor/*'/>
    public DataKey(string name) : this()
    {
      _Name = name;
    }

    // The Copy constructor.
    /// <include file='../../LJCGenDoc/Common/Data.xml'
    ///  path='members/CopyConstructor/*'/>
    public DataKey(DataKey item)
    {
      _ID = item.ID;
      _DataDbID = item.DataSiteID;
      _DataTableID = item.DataTableID;
      _DataTableDbID = item.DataTableSiteID;
      _Name = item.Name;
      _KeyType = item.KeyType;

      _IsClustered = item.IsClustered;
      _IsAscending = item.IsAscending;
      _SourceColumnName = item.SourceColumnName;
      _TargetTableName = item.TargetTableName;
      _TargetColumnName = item.TargetColumnName;

      ChangedNames = item.ChangedNames;
      _OriginalValues = new OriginalValues();
      LJCSetOriginalValues();
    }
    #endregion

    #region Data Object Methods

    // Creates and returns a clone of this object.
    /// <include file='../../LJCGenDoc/Common/Data.xml'
    ///  path='members/Clone/*'/>
    public DataKey Clone()
    {
      var retValue = MemberwiseClone() as DataKey;
      return retValue;
    }

    // Provides the default Sort functionality.
    /// <include file='../../LJCGenDoc/Common/Data.xml'
    ///  path='members/CompareTo/*'/>
    public int CompareTo(DataKey other)
    {
      int retValue;

      while (true)
      {
        if (null == other)
        {
          // This object is greater than null.
          retValue = NetString.CompareGreater;
          break;
        }

        retValue = ID.CompareTo(other.ID);
        if (retValue != NetString.CompareEqual)
        {
          break;
        }

        retValue = DataSiteID.CompareTo(other.DataSiteID);
        break;
      }
      return retValue;
    }

    // Initializes the original values.
    /// <include file='../../LJCGenDoc/Common/Data.xml'
    ///  path='members/LJCSetOriginalValues/*'/>
    public void LJCSetOriginalValues()
    {
      _OriginalValues.ID = _ID;
      _OriginalValues.DbID = _DataDbID;
      _OriginalValues.DataTableID = _DataTableID;
      _OriginalValues.DataTableDbID = _DataTableDbID;
      _OriginalValues.Name = _Name;
      _OriginalValues.KeyType = _KeyType;

      _OriginalValues.IsClustered = _IsClustered;
      _OriginalValues.IsAscending = _IsAscending;
      _OriginalValues.SourceColumnName = _SourceColumnName;
      _OriginalValues.TargetTableName = _TargetTableName;
      _OriginalValues.TargetColumnName = _TargetColumnName;
      ChangedNames.Clear();
    }

    // The object string identifier.
    /// <include file='../../LJCGenDoc/Common/Data.xml'
    ///  path='members/ToString/*'/>
    public override string ToString()
    {
      var retValue = $"{_Name}:{_ID}";
      return retValue;
    }
    #endregion

    #region Data Properties

    // Update ChangedNames.Add() statements to "Property" constant
    // if property was renamed.

    // Gets or sets the ID value.
    /// <include file='doc/DataKey.xml'
    ///  path='members/ID/*'/>
    [Required]
    [Column("ID", TypeName = "bigint")]
    public long ID
    {
      get => _ID;
      set
      {
        if (_ID != value)
        {
          _ID = ChangedNames.Add(ColumnID, _OriginalValues.ID, value);
        }
      }
    }
    private long _ID;

    // Gets or sets the database ID.
    /// <include file='doc/DataKey.xml'
    ///  path='members/DataSiteID/*'/>
    [Required]
    [Column("DataSiteID", TypeName = "bigint")]
    public short DataSiteID
    {
      get => _DataDbID;
      set
      {
        if (_DataDbID != value)
        {
          _DataDbID = ChangedNames.Add(ColumnDbID
            , _OriginalValues.DbID, value);
        }
      }
    }
    private short _DataDbID;

    // Gets or sets the parent table row ID.
    /// <include file='doc/DataKey.xml'
    ///  path='members/DataTableID/*'/>
    [Required]
    [Column("DataTableID", TypeName = "bigint")]
    public long DataTableID
    {
      get => _DataTableID;
      set
      {
        if (_DataTableID != value)
        {
          _DataTableID = ChangedNames.Add(ColumnDataTableID
          , _OriginalValues.DataTableID, value);
        }
      }
    }
    private long _DataTableID;

    // Gets or sets the DataTable database ID.
    /// <include file='doc/DataKey.xml'
    ///  path='members/DataTableSiteID/*'/>
    [Required]
    [Column("DataTableSiteID", TypeName = "bigint")]
    public short DataTableSiteID
    {
      get => _DataTableDbID;
      set
      {
        if (_DataTableDbID != value)
        {
          _DataTableDbID = ChangedNames.Add(ColumnDataTableDbID
            , _OriginalValues.DataTableDbID, value);
        }
      }
    }
    private short _DataTableDbID;

    // Gets or sets the Name value.
    /// <include file='doc/DataKey.xml'
    ///  path='members/Name/*'/>
    [Required]
    [Column("Name", TypeName = "nvarchar(60")]
    public string Name
    {
      get => _Name;
      set
      {
        var newValue = value?.Trim();
        if (_Name != newValue)
        {
          _Name = ChangedNames.Add(ColumnName, _OriginalValues.Name, newValue);
        }
      }
    }
    private string _Name;

    // Gets or sets the KeyType value.
    /// <include file='doc/DataKey.xml'
    ///  path='members/KeyType/*'/>
    [Required]
    [Column("KeyType", TypeName = "smallint")]
    public short KeyType
    {
      get => _KeyType;
      set
      {
        if (_KeyType != value)
        {
          _KeyType = ChangedNames.Add(ColumnKeyType
            , _OriginalValues.KeyType, value);
        }
      }
    }
    private short _KeyType;

    // Gets or sets the IsAscending flag.
    /// <include file='doc/DataKey.xml'
    ///  path='members/IsAscending/*'/>
    [Column("IsAscending", TypeName = "bit")]
    public bool IsAscending
    {
      get => _IsAscending;
      set
      {
        if (_IsAscending != value)
        {
          _IsAscending = ChangedNames.Add(ColumnIsAscending
            , _OriginalValues.IsAscending, value);
        }
      }
    }
    private bool _IsAscending;

    // Gets or sets the IsClustered flag.
    /// <include file='doc/DataKey.xml'
    ///  path='members/IsClustered/*'/>
    [Column("IsClustered", TypeName = "bit")]
    public bool IsClustered
    {
      get => _IsClustered;
      set
      {
        if (_IsClustered != value)
        {
          _IsClustered = ChangedNames.Add(ColumnIsClustered
            , _OriginalValues.IsClustered, value);
        }
      }
    }
    private bool _IsClustered;

    // Gets or sets the source column name.
    /// <include file='doc/DataKey.xml'
    ///  path='members/SourceColumnName/*'/>
    [Column("SourceColumnName", TypeName = "nvarchar(60")]
    public string SourceColumnName
    {
      get => _SourceColumnName;
      set
      {
        var newValue = NetString.ScrubDelimitedValues(value);
        if (_SourceColumnName != newValue)
        {
          _SourceColumnName = ChangedNames.Add(ColumnSourceColumnName
           , _OriginalValues.SourceColumnName, newValue);
        }
      }
    }
    private string _SourceColumnName;

    // Gets or sets the target table name.
    /// <include file='doc/DataKey.xml'
    ///  path='members/TargetTableName/*'/>
    [Column("TargetTableName", TypeName = "nvarchar(60")]
    public string TargetTableName
    {
      get => _TargetTableName;
      set
      {
        var newValue = value?.Trim();
        if (_TargetTableName != newValue)
        {
          _TargetTableName = ChangedNames.Add(ColumnTargetTableName
            , _OriginalValues.TargetTableName, newValue);
        }
      }
    }
    private string _TargetTableName;

    // Gets or sets the target column name.
    /// <include file='doc/DataKey.xml'
    ///  path='members/TargetColumnName/*'/>
    [Column("TargetColumnName", TypeName = "nvarchar(60")]
    public string TargetColumnName
    {
      get => _TargetColumnName;
      set
      {
        var newValue = value?.Trim();
        if (_TargetColumnName != newValue)
        {
          _TargetColumnName = ChangedNames.Add(ColumnTargetColumnName
            , _OriginalValues.TargetColumnName, newValue);
        }
      }
    }
    private string _TargetColumnName;
    #endregion

    #region Calculated and Join Data Properties

    // Gets or sets the Join TableName value.
    /// <include file='doc/DataKey.xml'
    ///  path='members/DataTableName/*'/>
    public string DataTableName { get; set; }
    #endregion

    #region Class Properties

    // Gets a reference to the ChangedNames list.
    /// <include file='../../LJCGenDoc/Common/Data.xml'
    ///  path='members/ChangedNames/*'/>
    [XmlIgnore]
    public ChangedNames ChangedNames { get; private set; }
    #endregion

    #region Class Data

    /// <summary>The table name.</summary>
    public static string TableName = "DataKey";

    /// <summary>The ID column name.</summary>
    public static string ColumnID = "ID";

    /// <summary>The DataSiteID column name.</summary>
    public static string ColumnDbID = "DataSiteID";

    /// <summary>The DataTableID column name.</summary>
    public static string ColumnDataTableID = "DataTableID";

    /// <summary>The DataTableSiteID column name.</summary>
    public static string ColumnDataTableDbID = "DataTableSiteID";

    /// <summary>The Name column name.</summary>
    public static string ColumnName = "Name";

    /// <summary>The KeyType column name.</summary>
    public static string ColumnKeyType = "KeyType";

    /// <summary>The IsAscending column name.</summary>
    public static string ColumnIsAscending = "IsAscending";

    /// <summary>The IsClustered column name.</summary>
    public static string ColumnIsClustered = "IsClustered";

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

    /// <summary>The Name maximum length.</summary>
    public static int LengthName = 60;

    /// <summary>The SourceColumnName maximum length.</summary>
    public static int LengthSourceColumnName = 60;

    /// <summary>The TargetTableName maximum length.</summary>
    public static int LengthTargetTableName = 60;

    /// <summary>The TargetColumnName maximum length.</summary>
    public static int LengthTargetColumnName = 60;

    /// <summary>The Join TableName column name.</summary>
    public static string ColumnDataTableName = "DataTableName";

    // The object starting values.
    private readonly OriginalValues _OriginalValues;

    // The object starting values.
    private class OriginalValues
    {
      // Gets or sets the table row ID.
      public long ID { get; set; }

      // Gets or sets the database ID.
      public short DbID { get; set; }

      // Gets or sets the parent table row ID.
      public long DataTableID { get; set; }

      // Gets or sets the parent database ID.
      public short DataTableDbID { get; set; }

      // Gets or sets the unique name.
      public string Name { get; set; }

      // Gets or sets the KeyType value.
      public short KeyType { get; set; }

      // Gets or sets the IsAscending flag.
      public bool IsAscending { get; set; }

      // Gets or sets the IsClustered flag.
      public bool IsClustered { get; set; }

      // Gets or sets the source column name.
      public string SourceColumnName { get; set; }

      // Gets or sets the target table name.
      public string TargetTableName { get; set; }

      // Gets or sets the target column name.
      public string TargetColumnName { get; set; }
    }
    #endregion
  }

  #region Comparers

  // Sort and search on Name value.
  /// <include file='Doc/DataKey.xml'
  ///  path='items/Compare/*'/>
  public class DataKeyUniqueComparer : IComparer<DataKey>
  {
    // Compares two objects.
    /// <include file='../../LJCGenDoc/Common/Data.xml'
    ///  path='items/Compare/*'/>
    public int Compare(DataKey x, DataKey y)
    {
      int retValue;

      while (true)
      {
        retValue = NetCommon.CompareNull(x, y);
        if (retValue != NetString.CompareNotNull)
        {
          break;
        }

        retValue = NetCommon.CompareNull(x.Name, y.Name);
        if (retValue != NetString.CompareNotNull)
        {
          break;
        }

        retValue = x.DataTableSiteID.CompareTo(y.DataTableSiteID);
        if (retValue != NetString.CompareEqual)
        {
          break;
        }

        retValue = x.DataTableID.CompareTo(y.DataTableID);
        if (retValue != NetString.CompareEqual)
        {
          break;
        }

        retValue = string.Compare(x.Name, y.Name, true);
        break;
      }
      return retValue;
    }
  }
  #endregion
}
