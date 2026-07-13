// Copyright (c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// DataUtilTable.cs
using LJCDBClientLib;
using LJCNetCommon;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;

namespace LJCDataUtilityDAL
{
  // The DataTable data.
  /// <include file='Doc/DataTable.xml'
  ///  path='members/DataUtilTable/*'/>
  public class DataUtilTable : IComparable<DataUtilTable>
  {
    #region Constructor Methods

    // Initializes an object instance.
    /// <include file='../../LJCGenDoc/Common/Data.xml'
    ///  path='members/Constructor/*'/>
    public DataUtilTable()
    {
      _ID = 0;
      _DataDbID = 0;
      _DataModuleID = 0;
      _DataModuleDbID = 0;
      _Name = "";
      _Description = "";
      _Sequence = 0;

      _SchemaName = null;
      _NewName = null;

      ChangedNames = new ChangedNames();
      _OriginalValues = new OriginalValues();
      LJCSetOriginalValues();
    }

    // Initializes an object instance with the supplied values.
    /// <include file='Doc/DataTable.xml'
    ///  path='members/ParamConstructor/*'/>
    public DataUtilTable(string name, int sequence) : this()
    {
      _Name = name;
      _Sequence = sequence;
    }

    // The Copy constructor.
    /// <include file='../../LJCGenDoc/Common/Data.xml'
    ///  path='members/CopyConstructor/*'/>
    public DataUtilTable(DataUtilTable item) : this()
    {
      _ID = item.ID;
      _DataDbID = item.DataSiteID;
      _DataModuleID = item.DataModuleID;
      _DataModuleDbID = item.DataModuleSiteID;
      _Name = item.Name;
      _Description = item.Description;
      _Sequence = item.Sequence;

      _SchemaName = item.SchemaName;
      _NewName = item.NewName;

      ChangedNames = item.ChangedNames;
      _OriginalValues = new OriginalValues();
      LJCSetOriginalValues();
    }
    #endregion

    #region Data Object Methods

    // Creates and returns a clone of this object.
    /// <include file='../../LJCGenDoc/Common/Data.xml'
    ///  path='members/Clone/*'/>
    public DataUtilTable Clone()
    {
      var retValue = MemberwiseClone() as DataUtilTable;
      return retValue;
    }

    // Provides the default Sort functionality.
    /// <include file='../../LJCGenDoc/Common/Data.xml'
    ///  path='members/CompareTo/*'/>
    public int CompareTo(DataUtilTable other)
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
      _OriginalValues.DataDbID = _DataModuleDbID;
      _OriginalValues.DataModuleID = _DataModuleID;
      _OriginalValues.DataModuleSiteID = _DataModuleDbID;
      _OriginalValues.Name = _Name;
      _OriginalValues.Description = _Description;
      _OriginalValues.Sequence = _Sequence;

      _OriginalValues.SchemaName = _SchemaName;
      _OriginalValues.NewName = _NewName;
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
    /// <include file='doc/DataTable.xml'
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
    /// <include file='doc/DataTable.xml'
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
            , _OriginalValues.DataDbID, value);
        }
      }
    }
    private short _DataDbID;

    // Gets or sets the parent table row ID.
    /// <include file='doc/DataTable.xml'
    ///  path='members/DataModuleID/*'/>
    [Required]
    [Column("DataModuleID", TypeName = "bigint")]
    public long DataModuleID
    {
      get => _DataModuleID;
      set
      {
        if (_DataModuleID != value)
        {
          _DataModuleID = ChangedNames.Add(ColumnDataModuleID
            , _OriginalValues.DataModuleID, value);
        }
      }
    }
    private long _DataModuleID;

    // Gets or sets the parent database ID.
    /// <include file='doc/DataTable.xml'
    ///  path='members/DataModuleSiteID/*'/>
    [Required]
    [Column("DataModuleSiteID", TypeName = "bigint")]
    public short DataModuleSiteID
    {
      get => _DataModuleDbID;
      set
      {
        if (_DataModuleDbID != value)
        {
          _DataModuleDbID = ChangedNames.Add(ColumnDataModuleDbID
            , _OriginalValues.DataModuleSiteID, value);
        }
      }
    }
    private short _DataModuleDbID;

    // Gets or sets the name value.
    /// <include file='doc/DataTable.xml'
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

    // Gets or sets the description.
    /// <include file='doc/DataTable.xml'
    ///  path='members/Description/*'/>
    [Required]
    [Column("Description", TypeName = "nvarchar(80")]
    public string Description
    {
      get => _Description;
      set
      {
        var newValue = value?.Trim();
        if (_Description != newValue)
        {
          _Description = ChangedNames.Add(ColumnDescription
            , _OriginalValues.Description, newValue);
        }
      }
    }
    private string _Description;

    // Gets or sets the sequence.
    /// <include file='doc/DataTable.xml'
    ///  path='members/Sequence/*'/>
    [Required]
    [Column("Sequence", TypeName = "int")]
    public int Sequence
    {
      get => _Sequence;
      set
      {
        if (_Sequence != value)
        {
          _Sequence = ChangedNames.Add(ColumnSequence, _OriginalValues.Sequence
            , value);
        }
      }
    }
    private int _Sequence;

    // Gets or sets the schema name.
    /// <include file='doc/DataTable.xml'
    ///  path='members/SchemaName/*'/>
    [Column("SchemaName", TypeName = "nvarchar(30")]
    public string SchemaName
    {
      get => _SchemaName;
      set
      {
        var newValue = value?.Trim();
        if (_SchemaName != newValue)
        {
          _SchemaName = ChangedNames.Add(ColumnSchemaName
            , _OriginalValues.SchemaName, newValue);
        }
      }
    }
    private string _SchemaName;

    // Gets or sets the new name.
    /// <include file='doc/DataTable.xml'
    ///  path='members/NewName/*'/>
    [Column("NewName", TypeName = "nvarchar(60")]
    public string NewName
    {
      get => _NewName;
      set
      {
        var newValue = value?.Trim();
        if (_NewName != newValue)
        {
          _NewName = ChangedNames.Add(ColumnNewName, _OriginalValues.NewName
            , newValue);
        }
      }
    }
    private string _NewName;

    // Gets or sets the Join module name.
    /// <include file='doc/DataTable.xml'
    ///  path='members/ModuleName/*'/>
    public string ModuleName { get; set; }
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
    public static string TableName = "DataTable";

    /// <summary>The ID column name.</summary>
    public static string ColumnID = "ID";

    /// <summary>The DataSiteID column name.</summary>
    public static string ColumnDbID = "DataSiteID";

    /// <summary>The DataModuleID column name.</summary>
    public static string ColumnDataModuleID = "DataModuleID";

    /// <summary>The DataModuleSiteID column name.</summary>
    public static string ColumnDataModuleDbID = "DataModuleSiteID";

    /// <summary>The Name column name.</summary>
    public static string ColumnName = "Name";

    /// <summary>The Description column name.</summary>
    public static string ColumnDescription = "Description";

    /// <summary>The SchemaName column name.</summary>
    public static string ColumnSchemaName = "SchemaName";

    /// <summary>The Sequence column name.</summary>
    public static string ColumnSequence = "Sequence";

    /// <summary>The NewName column name.</summary>
    public static string ColumnNewName = "NewName";

    /// <summary>The Name maximum length.</summary>
    public static int LengthName = 60;

    /// <summary>The Description maximum length.</summary>
    public static int LengthDescription = 80;

    /// <summary>The Description maximum length.</summary>
    public static int LengthSequence = 3;

    /// <summary>The Join ModuleName column name.</summary>
    public static string ColumnModuleName = "ModuleName";

    // The object starting values.
    private readonly OriginalValues _OriginalValues;

    // The object starting values.
    private class OriginalValues
    {
      // Gets or sets the table row ID.
      public long ID { get; set; }

      // Gets or sets the database ID.
      public short DataDbID { get; set; }

      // Gets or sets the parent table row ID.
      public long DataModuleID { get; set; }

      // Gets or sets the parent database ID.
      public short DataModuleSiteID { get; set; }

      // Gets or sets the unique name.
      public string Name { get; set; }

      // Gets or sets the description.
      public string Description { get; set; }

      // Gets or sets the sequence.
      public int Sequence { get; set; }

      // Gets or sets the schema name.
      public string SchemaName { get; set; }

      // Gets or sets the new name.
      public string NewName { get; set; }
    }
    #endregion
  }

  #region Comparers

  // Sort and search on Name value.
  /// <include file='Doc/DataTable.xml'
  ///  path='items/Compare/*'/>
  public class DataTableUniqueComparer : IComparer<DataUtilTable>
  {
    // Compares two objects.
    /// <include file='../../LJCGenDoc/Common/Data.xml'
    ///  path='items/Compare/*'/>
    public int Compare(DataUtilTable x, DataUtilTable y)
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

        retValue = x.DataModuleSiteID.CompareTo(y.DataModuleSiteID);
        if (retValue != NetString.CompareEqual)
        {
          break;
        }

        retValue = x.DataModuleID.CompareTo(y.DataModuleID);
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
