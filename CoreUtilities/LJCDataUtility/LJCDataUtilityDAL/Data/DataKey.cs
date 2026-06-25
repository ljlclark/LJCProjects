// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// DataKey.cs
using LJCDBClientLib;
using LJCNetCommon;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LJCDataUtilityDAL
{
  /// <summary>The DataKey table Data Object.</summary>
  public class DataKey : IComparable<DataKey>
  {
    #region Constructor Methods

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../LJCGenDoc/Common/Data.xml'/>
    public DataKey()
    {
      _ID = 0;
      _DataSiteID = 0;
      _DataTableID = 0;
      _DataTableSiteID = 0;
      _Name = "";
      _KeyType = 0;
      _SourceColumnName = null;
      _TargetTableName = null;
      _TargetColumnName = null;
      _IsClustered = false;
      _IsAscending = false;

      ChangedNames = new ChangedNames();
      _OriginalValues = new OriginalValues();
      SetOriginalValues();
    }

    // Initializes an object instance.
    /// <include path='items/ParamConstructor/*' file='../../LJCGenDoc/Common/Data.xml'/>
    public DataKey(string name) : this()
    {
      _Name = name;
    }

    // The Copy constructor.
    /// <include path='items/CopyConstructor/*' file='../../LJCGenDoc/Common/Data.xml'/>
    public DataKey(DataKey item)
    {
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

    #region Data Object Methods

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

    /// <summary>Initializes the original values.</summary>
    public void SetOriginalValues()
    {
      _OriginalValues.ID = _ID;
      _OriginalValues.DataSiteID = _DataSiteID;
      _OriginalValues.DataTableID = _DataTableID;
      _OriginalValues.DataTableSiteID = _DataTableSiteID;
      _OriginalValues.Name = _Name;
      _OriginalValues.KeyType = _KeyType;
      _OriginalValues.SourceColumnName = _SourceColumnName;
      _OriginalValues.TargetTableName = _TargetTableName;
      _OriginalValues.TargetColumnName = _TargetColumnName;
      _OriginalValues.IsClustered = _IsClustered;
      _OriginalValues.IsAscending = _IsAscending;
      ChangedNames.Clear();
    }

    // The object string identifier.
    /// <include path='items/ToString/*' file='../../LJCGenDoc/Common/Data.xml'/>
    public override string ToString()
    {
      var retValue = $"{_Name}:{_ID}";
      return retValue;
    }
    #endregion

    #region Data Properties

    // Update ChangedNames.Add() statements to "Property" constant
    // if property was renamed.

    /// <summary>Gets or sets the ID value.</summary>
    [Required]
    [Column("ID", TypeName = "int")]
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

    /// <summary>Gets or sets the ID value.</summary>
    [Required]
    [Column("DataSiteID", TypeName = "bigint")]
    public long DataSiteID
    {
      get => _DataSiteID;
      set
      {
        if (_DataSiteID != value)
        {
          _DataSiteID = ChangedNames.Add(ColumnDataSiteID
            , _OriginalValues.DataSiteID, value);
        }
      }
    }
    private long _DataSiteID;

    /// <summary>Gets or sets the DataTableID value.</summary>
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

    /// <summary>Gets or sets the DataTableID value.</summary>
    [Required]
    [Column("DataTableSiteID", TypeName = "bigint")]
    public long DataTableSiteID
    {
      get => _DataTableSiteID;
      set
      {
        if (_DataTableSiteID != value)
        {
          _DataTableSiteID = ChangedNames.Add(ColumnDataTableSiteID
            , _OriginalValues.DataTableSiteID, value);
        }
      }
    }
    private long _DataTableSiteID;

    /// <summary>Gets or sets the Name value.</summary>
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
          _Name = ChangedNames.Add(ColumnName
            , _OriginalValues.Name, newValue);
        }
      }
    }
    private String _Name;

    /// <summary>Gets or sets the KeyType value.</summary>
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

    /// <summary>Gets or sets the source column name.</summary>
    [Column("SourceColumnName", TypeName = "nvarchar(60")]
    public string SourceColumnName
    {
      get => _SourceColumnName;
      set
      {
        //value = NetString.InitString(value);
        var newValue = NetString.ScrubDelimitedValues(value);
        if (_SourceColumnName != newValue)
        {
          _SourceColumnName = ChangedNames.Add(ColumnSourceColumnName
           , _OriginalValues.SourceColumnName, newValue);
        }
      }
    }
    private string _SourceColumnName;

    /// <summary>Gets or sets the target table name.</summary>
    [Column("TargetTableName", TypeName = "nvarchar(60")]
    public string TargetTableName
    {
      get => _TargetTableName;
      set
      {
        value = value?.Trim();
        if (_TargetTableName != value)
        {
          _TargetTableName = ChangedNames.Add(ColumnTargetTableName
            , _OriginalValues.TargetTableName, value);
        }
      }
    }
    private string _TargetTableName;

    /// <summary>Gets or sets the target column name.</summary>
    [Column("TargetColumnName", TypeName = "nvarchar(60")]
    public string TargetColumnName
    {
      get => _TargetColumnName;
      set
      {
        value = value?.Trim();
        if (_TargetColumnName != value)
        {
          _TargetColumnName = ChangedNames.Add(ColumnTargetColumnName
            , _OriginalValues.TargetColumnName, value);
        }
      }
    }
    private string _TargetColumnName;

    /// <summary>Gets or sets the IsClustered flag.</summary>
    [Column("IsClustered", TypeName="bit")]
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

    /// <summary>Gets or sets the IsAscending flag.</summary>
    [Column("IsAscending", TypeName="bit")]
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

    // The object starting values.
    private readonly OriginalValues _OriginalValues;

    // The object starting values.
    private class OriginalValues
    {
      // Gets or sets the table row ID.
      public long ID { get; set; }

      // Gets or sets the database ID.
      public long DataSiteID { get; set; }

      // Gets or sets the parent table row ID.
      public long DataTableID { get; set; }

      // Gets or sets the parent database ID.
      public long DataTableSiteID { get; set; }

      // Gets or sets the unique name.
      public string Name { get; set; }

      // Gets or sets the KeyType value.
      public short KeyType { get; set; }

      // Gets or sets the source column name.
      public string SourceColumnName { get; set; }

      // Gets or sets the target table name.
      public string TargetTableName { get; set; }

      // Gets or sets the target column name.
      public string TargetColumnName { get; set; }

      // Gets or sets the IsClustered flag.
      public bool IsClustered { get; set; }

      // Gets or sets the IsAscending flag.
      public bool IsAscending { get; set; }
    }
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
