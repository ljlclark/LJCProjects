// Copyright (c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// DataUtilTable5.cs
using LJCDBMessage5;
using LJCNetCommon5;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;

namespace LJCDataUtilityDAL5
{
  // The DataTable data.
  /// <include file='Doc/DataUtilTable.xml'
  ///  path='members/DataUtilTable/*'/>
  public class DataUtilTable : IComparable<DataUtilTable>
  {
    #region Constructor Methods

    // Initializes an object instance.
    /// <include file='../../LJCGenDoc5/Common/Data.xml'
    ///  path='members/Constructor/*'/>
    public DataUtilTable()
    {
      _DbId = 0;
      _Id = 0;
      _DataModuleDbId = 0;
      _DataModuleId = 0;
      _Name = "";
      _Description = null;
      _Sequence = 0;

      _SchemaName = null;
      _NewName = null;
      ModuleName = null;

      ChangedNames = [];
      _OriginalValues = new OriginalValues();
      LJCSetOriginalValues();
    }

    // Initializes an object instance with the supplied values.
    /// <include file='Doc/DataUtilTable.xml'
    ///  path='members/ParamConstructor/*'/>
    public DataUtilTable(string name, int sequence) : this()
    {
      _Name = name;
      _Sequence = sequence;
    }

    // The Copy constructor.
    /// <include file='../../LJCGenDoc5/Common/Data.xml'
    ///  path='members/CopyConstructor/*'/>
    public DataUtilTable(DataUtilTable item)
    {
      _DbId = item.DbId;
      _Id = item.Id;
      _DataModuleDbId = item.DataModuleDbId;
      _DataModuleId = item.DataModuleId;
      _Name = item.Name;
      _Description = item.Description;
      _Sequence = item.Sequence;

      _SchemaName = item.SchemaName;
      _NewName = item.NewName;
      ModuleName = item.ModuleName;

      ChangedNames = item.ChangedNames;
      _OriginalValues = new OriginalValues();
      LJCSetOriginalValues();
    }
    #endregion

    #region Data Object Methods

    // Creates and returns a clone of this object.
    /// <include file='../../LJCGenDoc5/Common/Data.xml'
    ///  path='members/Clone/*'/>
    public DataUtilTable? Clone()
    {
      var retValue = MemberwiseClone() as DataUtilTable;
      return retValue;
    }

    // Provides the default Sort functionality.
    /// <include file='../../LJCGenDoc5/Common/Data.xml'
    ///  path='members/CompareTo/*'/>
    public int CompareTo(DataUtilTable? other)
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

        retValue = DbId.CompareTo(other.DbId);
        if (retValue != LJCNetString.CompareEqual)
        {
          break;
        }

        retValue = Id.CompareTo(other.Id);
        break;
      }
      return retValue;
    }

    // Initializes the original values.
    /// <include file='../../LJCGenDoc5/Common/Data.xml'
    ///  path='members/LJCSetOriginalValues/*'/>
    public void LJCSetOriginalValues()
    {
      _OriginalValues.DbId = _DbId;
      _OriginalValues.Id = _Id;
      _OriginalValues.DataModuleDbId = _DataModuleDbId;
      _OriginalValues.DataModuleId = _DataModuleId;
      _OriginalValues.Name = _Name;
      _OriginalValues.Description = _Description;
      _OriginalValues.Sequence = _Sequence;

      _OriginalValues.SchemaName = _SchemaName;
      _OriginalValues.NewName = _NewName;
      ChangedNames.Clear();
    }

    // The object string identifier.
    /// <include file='../../LJCGenDoc5/Common/Data.xml'
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
    /// <include file='Doc/DataUtilTable.xml'
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
    /// <include file='Doc/DataUtilTable.xml'
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
    /// <include file='Doc/DataUtilTable.xml'
    ///  path='members/DataModuleDbId/*'/>
    [Required]
    [Column("DataModuleDbId", TypeName = "bigint")]
    public short DataModuleDbId
    {
      get => _DataModuleDbId;
      set
      {
        if (_DataModuleDbId != value)
        {
          _DataModuleDbId = ChangedNames.Add(ColumnDataModuleDbId
            , _OriginalValues.DataModuleDbId, value);
        }
      }
    }
    private short _DataModuleDbId;

    // Gets or sets the parent table row ID.
    /// <include file='Doc/DataUtilTable.xml'
    ///  path='members/DataModuleId/*'/>
    [Required]
    [Column("DataModuleId", TypeName = "bigint")]
    public long DataModuleId
    {
      get => _DataModuleId;
      set
      {
        if (_DataModuleId != value)
        {
          _DataModuleId = ChangedNames.Add(ColumnDataModuleId
            , _OriginalValues.DataModuleId, value);
        }
      }
    }
    private long _DataModuleId;

    // Gets or sets the name value.
    /// <include file='Doc/DataUtilTable.xml'
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
          _Name = ChangedNames.Add(ColumnName, _OriginalValues.Name
            , newValue);
        }
      }
    }
    private string _Name;

    // Gets or sets the description.
    /// <include file='Doc/DataUtilTable.xml'
    ///  path='members/Description/*'/>
    [Required]
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

    // Gets or sets the sequence.
    /// <include file='Doc/DataUtilTable.xml'
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
    /// <include file='Doc/DataUtilTable.xml'
    ///  path='members/SchemaName/*'/>
    [Column("SchemaName", TypeName = "nvarchar(30")]
    public string? SchemaName
    {
      get => _SchemaName;
      set
      {
        var newValue = value?.Trim();
        if (LJC.HasText(newValue)
          && _SchemaName != newValue)
        {
          _SchemaName = ChangedNames.Add(ColumnSchemaName
            , _OriginalValues.SchemaName, newValue);
        }
      }
    }
    private string? _SchemaName;

    // Gets or sets the new name.
    /// <include file='Doc/DataUtilTable.xml'
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
          _NewName = ChangedNames.Add(ColumnNewName, _OriginalValues.NewName
            , newValue);
        }
      }
    }
    private string? _NewName;

    // Gets or sets the Join module name.
    /// <include file='Doc/DataUtilTable.xml'
    ///  path='members/ModuleName/*'/>
    public string? ModuleName { get; set; }
    #endregion

    #region Class Properties

    // Gets a reference to the ChangedNames list.
    /// <include file='../../LJCGenDoc5/Common/Data.xml'
    ///  path='members/ChangedNames/*'/>
    [XmlIgnore]
    public LJCChangedNames ChangedNames { get; private set; }
    #endregion

    #region Class Data

    /// <summary>The table name.</summary>
    public const string TableName = "DataTable";

    /// <summary>The database ID column name.</summary>
    public const string ColumnDbId = "DbId";

    /// <summary>The table row ID column name.</summary>
    public const string ColumnId = "Id";

    /// <summary>The the parent database ID column name.</summary>
    public const string ColumnDataModuleDbId = "DataModuleDbId";

    /// <summary>The the parent table row ID column name.</summary>
    public const string ColumnDataModuleId = "DataModuleId";

    /// <summary>The Name column name.</summary>
    public const string ColumnName = "Name";

    /// <summary>The Description column name.</summary>
    public const string ColumnDescription = "Description";

    /// <summary>The SchemaName column name.</summary>
    public const string ColumnSchemaName = "SchemaName";

    /// <summary>The Sequence column name.</summary>
    public const string ColumnSequence = "Sequence";

    /// <summary>The NewName column name.</summary>
    public const string ColumnNewName = "NewName";

    /// <summary>The Name maximum length.</summary>
    public const int LengthName = 60;

    /// <summary>The Description maximum length.</summary>
    public const int LengthDescription = 80;

    /// <summary>The Description maximum length.</summary>
    public const int LengthSequence = 3;

    /// <summary>The Join ModuleName column name.</summary>
    public const string ColumnModuleName = "ModuleName";

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
        DataModuleDbId = 0;
        DataModuleId = 0;
        Name = "";
        Description = null;
        Sequence = 0;
        SchemaName = null;
        NewName = null;
      }

      // Gets or sets the database ID.
      public short DbId { get; set; }

      // Gets or sets the table row ID.
      public long Id { get; set; }

      // Gets or sets the parent database ID.
      public short DataModuleDbId { get; set; }

      // Gets or sets the parent table row ID.
      public long DataModuleId { get; set; }

      // Gets or sets the unique name.
      public string Name { get; set; }

      // Gets or sets the description.
      public string? Description { get; set; }

      // Gets or sets the sequence.
      public int Sequence { get; set; }

      // Gets or sets the schema name.
      public string? SchemaName { get; set; }

      // Gets or sets the new name.
      public string? NewName { get; set; }
    }
    #endregion
  }

  #region Comparers

  // Sort and search on Name value.
  /// <include file='Doc/DataUtilTable.xml'
  ///  path='members/DataTableUnique/*'/>
  public class DataTableUnique : IComparer<DataUtilTable>
  {
    // Compares two objects.
    /// <include file='../../LJCGenDoc5/Common/Data.xml'
    ///  path='members/Compare/*'/>
    public int Compare(DataUtilTable? x, DataUtilTable? y)
    {
      int retValue;

      while (true)
      {
        retValue = LJC.CompareNull(x, y);
        if (retValue != LJCNetString.CompareNotNullOrEqual)
        {
          break;
        }

        retValue = LJC.CompareNull(x?.Name, y?.Name);
        if (retValue != LJCNetString.CompareNotNullOrEqual)
        {
          break;
        }

        retValue = x!.DataModuleDbId.CompareTo(y!.DataModuleDbId);
        if (retValue != LJCNetString.CompareEqual)
        {
          break;
        }

        retValue = x.DataModuleId.CompareTo(y.DataModuleId);
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
