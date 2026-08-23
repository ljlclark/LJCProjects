// Copyright (c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// DataUtilColumn5.cs
using LJCDBMessage5;
using LJCNetCommon5;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;

namespace LJCDataUtilityDAL5
{
  // The DataColumn data.
  /// <include file='Doc/DataColumn.xml'
  ///  path='members/DataUtilColumn/*'/>
  public class DataUtilColumn : IComparable<DataUtilColumn>
  {
    #region Constructor Methods

    // Initializes an object instance.
    /// <include file='../../LJCGenDoc/Common/Data.xml'
    ///  path='members/Constructor/*'/>
    public DataUtilColumn()
    {
      _DbId = 0;
      _Id = 0;
      _DataTableDbId = 0;
      _DataTableId = 0;
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
      DataTableName = null;

      ChangedNames = [];
      _OriginalValues = new OriginalValues();
      LJCSetOriginalValues();
    }

    // Initializes an object instance with the supplied values.
    /// <include file='Doc/DataColumn.xml'
    ///  path='members/ParamConstructor/*'/>
    public DataUtilColumn(string name, string typeName
      , bool allowNull = true, short maxLength = 0
      , string? defaultValue = null, short identityIncrement = 0) : this()
    {
      _Name = name;
      _TypeName = typeName;
      _AllowNull = allowNull;
      _MaxLength = maxLength;
      _DefaultValue = defaultValue;
      _IdentityIncrement = identityIncrement;
      if (_IdentityIncrement > 0)
      {
        _IdentityStart = 1;
      }
    }

    // The Copy constructor.
    /// <include file='../../LJCGenDoc/Common/Data.xml'
    ///  path='members/CopyConstructor/*'/>
    public DataUtilColumn(DataUtilColumn item)
    {
      _DbId = item.DbId;
      _Id = item.Id;
      _DataTableDbId = item.DataTableDbId;
      _DataTableId = item.DataTableId;
      _Name = item.Name;
      _Description = item.Description;
      _Sequence = item.Sequence;

      _AllowNull = item.AllowNull;
      _DefaultValue = item.DefaultValue;
      _IdentityStart = item.IdentityStart;
      _IdentityIncrement = item.IdentityIncrement;
      _MaxLength = item.MaxLength;
      _NewName = item.NewName;
      _NewMaxLength = item.NewMaxLength;
      _TypeName = item.TypeName;
      DataTableName = item.DataTableName;

      ChangedNames = item.ChangedNames;
      _OriginalValues = new OriginalValues();
      LJCSetOriginalValues();
    }
    #endregion

    #region Data Object Methods

    // Creates and returns a clone of this object.
    /// <include file='../../LJCGenDoc/Common/Data.xml'
    ///  path='members/Clone/*'/>
    public DataUtilColumn? Clone()
    {
      var retValue = MemberwiseClone() as DataUtilColumn;
      return retValue;
    }

    // Provides the default Sort functionality.
    /// <include file='../../LJCGenDoc/Common/Data.xml'
    ///  path='members/CompareTo/*'/>
    public int CompareTo(DataUtilColumn? other)
    {
      int retValue;

      while (true)
      {
        if (null == other)
        {
          // This value is greater than null.
          retValue = LJCNetString.CompareGreater;
          break;
        }

        retValue = DbId.CompareTo(other.DbId);
        if (LJCNetString.CompareEqual == retValue)
        {
          break;
        }

        retValue = Id.CompareTo(other.Id);
        break;
      }
      return retValue;
    }

    // Initializes the original values.
    /// <include file='../../LJCGenDoc/Common/Data.xml'
    ///  path='members/LJCSetOriginalValues/*'/>
    public void LJCSetOriginalValues()
    {
      _OriginalValues.DbId = _DbId;
      _OriginalValues.Id = _Id;
      _OriginalValues.DataTableDbId = _DataTableDbId;
      _OriginalValues.DataTableId = _DataTableId;
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
      var retValue = $"{_Name}:{_Id}";
      return retValue;
    }
    #endregion

    #region Data Properties

    // Update ChangedNames.Add() statements to "Property" constant
    // if property was renamed.

    // Gets or sets the database ID.
    /// <include file='Doc/DataColumn.xml'
    ///  path='members/DbId/*'/>
    [Required]
    [Column("DbId", TypeName = "smallint")]
    public short DbId
    {
      get => _DbId;
      set
      {
        if (_DbId != value)
        {
          _DbId = ChangedNames.Add(ColumnDbId
            , _OriginalValues.DbId, value);
        }
      }
    }
    private short _DbId;

    // Gets or sets the table row ID.
    /// <include file='Doc/DataColumn.xml'
    ///  path='members/Id/*'/>
    [Required]
    [Column("Id", TypeName = "bigint")]
    public long Id
    {
      get => _Id;
      set
      {
        if (_Id != value)
        {
          _Id = ChangedNames.Add(ColumnId, _OriginalValues.Id, value);
        }
      }
    }
    private long _Id;

    // Gets or sets the parent database ID.
    /// <include file='Doc/DataColumn.xml'
    ///  path='members/DataTableDbId/*'/>
    [Required]
    [Column("DataTableDbId", TypeName = "smallint")]
    public short DataTableDbId
    {
      get => _DataTableDbId;
      set
      {
        if (_DataTableDbId != value)
        {
          _DataTableDbId = ChangedNames.Add(ColumnDataTableDbId
            , _OriginalValues.DataTableDbId, value);
        }
      }
    }
    private short _DataTableDbId;

    // Gets or sets the parent table row ID.
    /// <include file='Doc/DataColumn.xml'
    ///  path='members/DataTableId/*'/>
    [Required]
    [Column("DataTableId", TypeName = "bigint")]
    public long DataTableId
    {
      get => _DataTableId;
      set
      {
        if (_DataTableId != value)
        {
          _DataTableId = ChangedNames.Add(ColumnDataTableId
            , _OriginalValues.DataTableId, value);
        }
      }
    }
    private long _DataTableId;

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
        if (LJC.HasText(newValue)
          && _Name != newValue)
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
    public string? Description
    {
      get => _Description;
      set
      {
        var newValue = value?.Trim();
        if (LJC.HasText(newValue)
          && _Description != newValue)
        {
          _Description = ChangedNames.Add(ColumnDescription
            , _OriginalValues.Description, newValue);
        }
      }
    }
    private string? _Description;

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
    public string? DefaultValue
    {
      get => _DefaultValue;
      set
      {
        var newValue = value?.Trim();
        if (LJC.HasText(newValue)
          && _DefaultValue != newValue)
        {
          _DefaultValue = ChangedNames.Add(ColumnDefaultValue
            , _OriginalValues.DefaultValue, value);
        }
      }
    }
    private string? _DefaultValue;

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
    public string? NewName
    {
      get => _NewName;
      set
      {
        var newValue = value?.Trim();
        if (LJC.HasText(newValue)
          && _NewName != newValue)
        {
          _NewName = ChangedNames.Add(ColumnNewName
            , _OriginalValues.NewName, newValue);
        }
      }
    }
    private string? _NewName;

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
        if (LJC.HasText(newValue)
          && _TypeName != newValue)
        {
          _TypeName = ChangedNames.Add(ColumnTypeName
            , _OriginalValues.TypeName, newValue);
        }
      }
    }
    private string _TypeName;

    // Gets or sets the Join TableName value.
    /// <include file='doc/DataKey.xml'
    ///  path='members/DataTableName/*'/>
    public string? DataTableName { get; set; }
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
    public const string TableName = "DataColumn";

    /// <summary>The database ID column name.</summary>
    public const string ColumnDbId = "DbId";

    /// <summary>The table row ID column name.</summary>
    public const string ColumnId = "Id";

    /// <summary>The the parent database ID column name.</summary>
    public const string ColumnDataTableDbId = "DataTableDbId";

    /// <summary>The the parent table row ID column name.</summary>
    public const string ColumnDataTableId = "DataTableId";

    /// <summary>The Name column name.</summary>
    public const string ColumnName = "Name";

    /// <summary>The Description column name.</summary>
    public const string ColumnDescription = "Description";

    /// <summary>The Sequence column name.</summary>
    public const string ColumnSequence = "Sequence";

    /// <summary>The AllowNull column name.</summary>
    public const string ColumnAllowNull = "AllowNull";

    /// <summary>The DefaultValue column name.</summary>
    public const string ColumnDefaultValue = "DefaultValue";

    /// <summary>The IdentityStart column name.</summary>
    public const string ColumnIdentityStart = "IdentityStart";

    /// <summary>The IdentityIncrement column name.</summary>
    public const string ColumnIdentityIncrement = "IdentityIncrement";

    /// <summary>The MaxLength column name.</summary>
    public const string ColumnMaxLength = "MaxLength";

    /// <summary>The Name column name.</summary>
    public const string ColumnNewName = "NewName";

    /// <summary>The MaxLength column name.</summary>
    public const string ColumnNewMaxLength = "NewMaxLength";

    /// <summary>The TypeName column name.</summary>
    public const string ColumnTypeName = "TypeName";

    /// <summary>The Name maximum length.</summary>
    public const int LengthName = 60;

    /// <summary>The Description maximum length.</summary>
    public const int LengthDescription = 80;

    /// <summary>The Sequence maximum length.</summary>
    public const int LengthSequence = 3;

    /// <summary>The MaxLength maximum length.</summary>
    public const int LengthDefaultValue = 30;

    /// <summary>The IdentityStart maximum length.</summary>
    public const int LengthIdentityStart = 3;

    /// <summary>The IdentityIncrement maximum length.</summary>
    public const int LengthIdentityIncrement = 3;

    /// <summary>The MaxLength maximum length.</summary>
    public const int LengthMaxLength = 5;

    // The object starting values.
    private readonly OriginalValues _OriginalValues;

    // The object starting values.
    private class OriginalValues
    {
      // Initializes an object instance.
      public OriginalValues()
      {
        DbId = 0;
        Id = 0;
        DataTableDbId = 0;
        DataTableId = 0;
        Name = "";
        Description = null;
        Sequence = 0;
        AllowNull = false;
        DefaultValue = null;
        IdentityStart = 1;
        IdentityIncrement = 1;
        MaxLength = -1;
        NewName = null;
        NewMaxLength = -1;
        TypeName = "";
      }

      // Gets or sets the database ID.
      public short DbId { get; set; }

      // Gets or sets the table row ID.
      public long Id { get; set; }

      // Gets or sets the parent database ID.
      public short DataTableDbId { get; set; }

      // Gets or sets the parent table row ID.
      public long DataTableId { get; set; }

      // Gets or sets the unique name.
      public string Name { get; set; }

      // Gets or sets the description.
      public string? Description { get; set; }

      // Gets or sets the Sequence value.
      public int Sequence { get; set; }

      // Gets or sets the AllowNull value.
      public bool AllowNull { get; set; }

      // Gets or sets the Default value.
      public string? DefaultValue { get; set; }

      // Gets or sets the IdentityStart value.
      public short IdentityStart { get; set; }

      // Gets or sets the IdentityIncrement value.
      public short IdentityIncrement { get; set; }

      // Gets or sets the MaxLength value.
      public short MaxLength { get; set; }

      // Gets or sets the NewName value.
      public string? NewName { get; set; }

      // Gets or sets the MaxLength value.
      public short NewMaxLength { get; set; }

      // Gets or sets the TypeName value.
      public string TypeName { get; set; }
    }
    #endregion
  }

  #region Comparers

  // Sort and search on Name value.
  /// <include file='Doc/DataColumn.xml'
  ///  path='members/DataColumnUnique/*'/>
  public class DataColumnUnique : IComparer<DataUtilColumn>
  {
    // Compares two objects.
    /// <include file='../../LJCGenDoc/Common/Data.xml'
    ///  path='members/Compare/*'/>
    public int Compare(DataUtilColumn? x, DataUtilColumn? y)
    {
      int retValue;

      while (true)
      {
        retValue = LJC.CompareNull(x, y);
        if (retValue != LJCNetString.CompareNotNullOrEqual)
        {
          break;
        }

        retValue = LJC.CompareNull(x!.Name, y!.Name);
        if (retValue != LJCNetString.CompareNotNullOrEqual)
        {
          break;
        }

        retValue = x.DataTableDbId.CompareTo(y.DataTableDbId);
        if (retValue != LJCNetString.CompareEqual)
        {
          break;
        }

        retValue = x.DataTableId.CompareTo(y.DataTableId);
        if (retValue != LJCNetString.CompareEqual)
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
