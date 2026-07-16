// Copyright (c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// DataUtilColumn.cs
using LJCDBClientLib;
using LJCNetCommon;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;

namespace LJCDataUtilityDAL
{
  /// <summary>The DataColumn data.</summary>
  public class DataUtilColumn : IComparable<DataUtilColumn>
  {
    #region Constructor Methods

    // Initializes an object instance.
    /// <include file='../../LJCGenDoc/Common/Data.xml'
    ///  path='members/Constructor/*'/>
    public DataUtilColumn()
    {
      _ID = 0;
      _DataDbID = 0;
      _DataTableID = 0;
      _DataTableDbID = 0;
      _Name = "";
      _Description = null;
      _Sequence = 0;

      _AllowNull = false;
      _DefaultValue = null;
      _IdentityStart = 0;
      _IdentityIncrement = 1;
      _MaxLength = 0;
      _NewName = null;
      _NewMaxLength = 0;
      _TypeName = "";

      ChangedNames = new ChangedNames();
      _OriginalValues = new OriginalValues();
      LJCSetOriginalValues();
    }

    // Initializes an object instance with the supplied values.
    /// <include file='Doc/DataColumn.xml'
    ///  path='members/ParamConstructor/*'/>
    public DataUtilColumn(string name, string typeName
      , bool allowNull = true, short maxLength = 0
      , string defaultValue = null, short identityIncrement = 0) : this()
    {
      Name = name;
      TypeName = typeName;
      AllowNull = allowNull;
      MaxLength = maxLength;
      DefaultValue = defaultValue;
      IdentityIncrement = identityIncrement;
      if (IdentityIncrement > 0)
      {
        IdentityStart = 1;
      }
    }

    // The Copy constructor.
    /// <include file='../../LJCGenDoc/Common/Data.xml'
    ///  path='members/Constructor/*'/>
    public DataUtilColumn(DataUtilColumn item)
    {
      ID = item.ID;
      DataSiteID = item.DataSiteID;
      DataTableID = item.DataTableID;
      DataTableSiteID = item.DataTableSiteID;
      Name = item.Name;
      Description = item.Description;
      Sequence = item.Sequence;

      AllowNull = item.AllowNull;
      DefaultValue = item.DefaultValue;
      IdentityStart = item.IdentityStart;
      IdentityIncrement = item.IdentityIncrement;
      MaxLength = item.MaxLength;
      NewName = item.NewName;
      NewMaxLength = item.NewMaxLength;
      TypeName = item.TypeName;

      ChangedNames = item.ChangedNames;
      _OriginalValues = new OriginalValues();
      LJCSetOriginalValues();
    }
    #endregion

    #region Data Object Methods

    // Creates and returns a clone of this object.
    /// <include file='../../LJCGenDoc/Common/Data.xml'
    ///  path='members/Clone/*'/>
    public DataUtilColumn Clone()
    {
      var retValue = MemberwiseClone() as DataUtilColumn;
      return retValue;
    }

    // Provides the default Sort functionality.
    /// <include file='../../LJCGenDoc/Common/Data.xml'
    ///  path='members/CompareTo/*'/>
    public int CompareTo(DataUtilColumn other)
    {
      int retValue;

      while (true)
      {
        if (null == other)
        {
          // This value is greater than null.
          retValue = NetString.CompareGreater;
          break;
        }

        retValue = ID.CompareTo(other.ID);
        if (NetString.CompareEqual == retValue)
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
    ///  path='members/SetOriginalValues/*'/>
    public void LJCSetOriginalValues()
    {
      _OriginalValues.ID = _ID;
      _OriginalValues.Name = _Name;
      _OriginalValues.Description = _Description;
      _OriginalValues.Sequence = _Sequence;

      _OriginalValues.AllowNull = _AllowNull;
      _OriginalValues.DefaultValue = _DefaultValue;
      _OriginalValues.IdentityStart = _IdentityStart;
      _OriginalValues.IdentityIncrement = _IdentityIncrement;
      _OriginalValues.MaxLength = _MaxLength;
      _OriginalValues.NewName = _NewName;
      _OriginalValues.NewMaxLength = _NewMaxLength;
      _OriginalValues.TypeName = _TypeName;
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
    /// <include file='Doc/DataColumn.xml'
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

    // Gets or sets the ID value.
    /// <include file='Doc/DataColumn.xml'
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

    // Gets or sets the DataTableID value.
    /// <include file='Doc/DataColumn.xml'
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

    // Gets or sets the DataTableID value.
    /// <include file='Doc/DataColumn.xml'
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
    /// <include file='Doc/DataColumn.xml'
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

    // Gets or sets the Description value.
    /// <include file='Doc/DataColumn.xml'
    ///  path='members/Description/*'/>
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

    // Gets or sets the Sequence value.
    /// <include file='Doc/DataColumn.xml'
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
          _Sequence = ChangedNames.Add(ColumnSequence
            , _OriginalValues.Sequence, value);
        }
      }
    }
    private int _Sequence;

    // Gets or sets the AllowNull value.
    /// <include file='Doc/DataColumn.xml'
    ///  path='members/AllowNull/*'/>
    [Required]
    [Column("AllowNull", TypeName = "bit")]
    public bool AllowNull
    {
      get => _AllowNull;
      set
      {
        if (_AllowNull != value)
        {
          _AllowNull = ChangedNames.Add(ColumnAllowNull
            , _OriginalValues.AllowNull, value);
        }
      }
    }
    private bool _AllowNull;

    // Gets or sets the Default value.
    /// <include file='Doc/DataColumn.xml'
    ///  path='members/DefaultValue/*'/>
    [Column("DefaultValue", TypeName = "nvarchar(30)")]
    public string DefaultValue
    {
      get => _DefaultValue;
      set
      {
        var newValue = value?.Trim();
        if (_DefaultValue != newValue)
        {
          _DefaultValue = ChangedNames.Add(ColumnDefaultValue
            , _OriginalValues.DefaultValue, value);
        }
      }
    }
    private string _DefaultValue;

    // Gets or sets the IdentityStart value.
    /// <include file='Doc/DataColumn.xml'
    ///  path='members/IdentityStart/*'/>
    [Required]
    [Column("IdentityStart", TypeName = "smallint")]
    public short IdentityStart
    {
      get => _IdentityStart;
      set
      {
        if (_IdentityStart != value)
        {
          _IdentityStart = ChangedNames.Add(ColumnIdentityStart
            , _OriginalValues.IdentityStart, value);
        }
      }
    }
    private short _IdentityStart;

    // Gets or sets the IdentityIncrement value.
    /// <include file='Doc/DataColumn.xml'
    ///  path='members/IdentityIncrement/*'/>
    [Required]
    [Column("IdentityIncrement", TypeName = "smallint")]
    public short IdentityIncrement
    {
      get => _IdentityIncrement;
      set
      {
        if (_IdentityIncrement != value)
        {
          _IdentityIncrement = ChangedNames.Add(ColumnIdentityIncrement
            , _OriginalValues.IdentityIncrement, value);
        }
      }
    }
    private short _IdentityIncrement;

    // Gets or sets the MaxLength value.
    /// <include file='Doc/DataColumn.xml'
    ///  path='members/MaxLength/*'/>
    [Required]
    [Column("MaxLength", TypeName = "smallint")]
    public short MaxLength
    {
      get => _MaxLength;
      set
      {
        if (_MaxLength != value)
        {
          _MaxLength = ChangedNames.Add(ColumnMaxLength
            , _OriginalValues.MaxLength, value);
        }
      }
    }
    private short _MaxLength;

    // Gets or sets the NewName value.
    /// <include file='Doc/DataColumn.xml'
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
          _NewName = ChangedNames.Add(ColumnNewName
            , _OriginalValues.NewName, value);
        }
      }
    }
    private string _NewName;

    // Gets or sets the MaxLength value.
    /// <include file='Doc/DataColumn.xml'
    ///  path='members/NewMaxLength/*'/>
    [Required]
    [Column("NewMaxLength", TypeName = "smallint")]
    public short NewMaxLength
    {
      get => _NewMaxLength;
      set
      {
        if (_NewMaxLength != value)
        {
          _NewMaxLength = ChangedNames.Add(ColumnNewMaxLength
            , _OriginalValues.NewMaxLength, value);
        }
      }
    }
    private short _NewMaxLength;

    // Gets or sets the TypeName value.
    /// <include file='Doc/DataColumn.xml'
    ///  path='members/TypeName/*'/>
    [Required]
    [Column("TypeName", TypeName = "nvarchar(20")]
    public string TypeName
    {
      get => _TypeName;
      set
      {
        var newValue = value?.Trim();
        if (_TypeName != newValue)
        {
          _TypeName = ChangedNames.Add(ColumnTypeName
            , _OriginalValues.TypeName, newValue);
        }
      }
    }
    private string _TypeName;
    #endregion

    #region Class Properties

    /// <summary>Gets a reference to the ChangedNames list.</summary>
    [XmlIgnore]
    public ChangedNames ChangedNames { get; private set; }
    #endregion

    #region Class Data

    /// <summary>The table name.</summary>
    public static string TableName = "DataColumn";

    /// <summary>The ID column name.</summary>
    public static string ColumnID = "ID";

    /// <summary>The ID column name.</summary>
    public static string ColumnDbID = "DataSiteID";

    /// <summary>The DataTableID column name.</summary>
    public static string ColumnDataTableID = "DataTableID";

    /// <summary>The DataTableSiteID column name.</summary>
    public static string ColumnDataTableDbID = "DataTableSiteID";

    /// <summary>The Name column name.</summary>
    public static string ColumnName = "Name";

    /// <summary>The Description column name.</summary>
    public static string ColumnDescription = "Description";

    /// <summary>The Sequence column name.</summary>
    public static string ColumnSequence = "Sequence";

    /// <summary>The AllowNull column name.</summary>
    public static string ColumnAllowNull = "AllowNull";

    /// <summary>The DefaultValue column name.</summary>
    public static string ColumnDefaultValue = "DefaultValue";

    /// <summary>The IdentityStart column name.</summary>
    public static string ColumnIdentityStart = "IdentityStart";

    /// <summary>The IdentityIncrement column name.</summary>
    public static string ColumnIdentityIncrement = "IdentityIncrement";

    /// <summary>The MaxLength column name.</summary>
    public static string ColumnMaxLength = "MaxLength";

    /// <summary>The Name column name.</summary>
    public static string ColumnNewName = "NewName";

    /// <summary>The MaxLength column name.</summary>
    public static string ColumnNewMaxLength = "NewMaxLength";

    /// <summary>The TypeName column name.</summary>
    public static string ColumnTypeName = "TypeName";

    /// <summary>The Name maximum length.</summary>
    public static int LengthName = 60;

    /// <summary>The Description maximum length.</summary>
    public static int LengthDescription = 80;

    /// <summary>The Sequence maximum length.</summary>
    public static int LengthSequence = 3;

    /// <summary>The MaxLength maximum length.</summary>
    public static int LengthDefaultValue = 30;

    /// <summary>The IdentityStart maximum length.</summary>
    public static int LengthIdentityStart = 3;

    /// <summary>The IdentityIncrement maximum length.</summary>
    public static int LengthIdentityIncrement = 3;

    /// <summary>The MaxLength maximum length.</summary>
    public static int LengthMaxLength = 5;

    // The object starting values.
    private readonly OriginalValues _OriginalValues;

    // The object starting values.
    private class OriginalValues
    {
      // Gets or sets the table row ID.
      public long ID { get; set; }

      // Gets or sets the database ID.
      public short DbID { get; set; }

      // Gets or sets the parent row ID.
      public long DataTableID { get; set; }

      // Gets or sets the parent database ID.
      public short DataTableDbID { get; set; }

      // Gets or sets the unique name.
      public string Name { get; set; }

      // Gets or sets the description.
      public string Description { get; set; }

      // Gets or sets the Sequence value.
      public int Sequence { get; set; }

      // Gets or sets the AllowNull value.
      public bool AllowNull { get; set; }

      // Gets or sets the Default value.
      public string DefaultValue { get; set; }

      // Gets or sets the IdentityStart value.
      public short IdentityStart { get; set; }

      // Gets or sets the IdentityIncrement value.
      public short IdentityIncrement { get; set; }

      // Gets or sets the MaxLength value.
      public short MaxLength { get; set; }

      // Gets or sets the NewName value.
      public string NewName { get; set; }

      // Gets or sets the MaxLength value.
      public short NewMaxLength { get; set; }

      // Gets or sets the TypeName value.
      public string TypeName { get; set; }
    }
    #endregion
  }

  #region Comparers

  /// <summary>Sort and search on Name value.</summary>
  public class DataColumnUnique : IComparer<DataUtilColumn>
  {
    // Compares two objects.
    /// <include path='items/Compare/*' file='../../LJCGenDoc/Common/Data.xml'/>
    public int Compare(DataUtilColumn x, DataUtilColumn y)
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
        retValue = x.DataTableSiteID.CompareTo(y.DataTableSiteID);
      }
      if (isContinue)
      {
        retValue = string.Compare(x.Name, y.Name, true);
      }
      return retValue;
    }
  }
  #endregion
}
