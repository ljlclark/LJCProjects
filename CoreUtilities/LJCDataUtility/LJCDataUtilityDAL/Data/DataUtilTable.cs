// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// DataUtilTable.cs
using LJCDBClientLib;
using LJCNetCommon;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LJCDataUtilityDAL
{
  /// <summary>The DataTable table Data Object.</summary>
  public class DataUtilTable : IComparable<DataUtilTable>
  {
    #region Constructor Methods

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../LJCGenDoc/Common/Data.xml'/>
    public DataUtilTable()
    {
      _ID = 0;
      _DataSiteID = 0;
      _DataModuleID = 0;
      _DataModuleSiteID = 0;
      _Name = "";
      _Description = "";
      _Sequence = 0;

      ChangedNames = new ChangedNames();
      _OriginalValues = new OriginalValues();
      SetOriginalValues();
    }

    // Initializes an object instance.
    /// <include path='items/ParamConstructor/*' file='../../LJCGenDoc/Common/Data.xml'/>
    public DataUtilTable(string name, int sequence) : this()
    {
      _Name = name;
      _Sequence = sequence;
    }

    // The Copy constructor.
    /// <include path='items/CopyConstructor/*' file='../../LJCGenDoc/Common/Data.xml'/>
    public DataUtilTable(DataUtilTable item)
    {
      _DataSiteID = item.DataSiteID;
      _ID = item.ID;
      _DataModuleSiteID = item.DataModuleSiteID;
      _DataModuleID = item.DataModuleID;
      _Name = item.Name;
      _Description = item.Description;
      _Sequence = item.Sequence;
      _SchemaName = item.SchemaName;
      _NewName = item.NewName;

      ChangedNames = item.ChangedNames;
      _OriginalValues = new OriginalValues();
      SetOriginalValues();
    }
    #endregion

    #region Data Object Methods

    // Creates and returns a clone of this object.
    /// <include path='items/Clone/*' file='../../LJCGenDoc/Common/Data.xml'/>
    public DataUtilTable Clone()
    {
      var retValue = MemberwiseClone() as DataUtilTable;
      return retValue;
    }

    // Provides the default Sort functionality.
    /// <include path='items/CompareTo/*' file='../../LJCGenDoc/Common/Data.xml'/>
    public int CompareTo(DataUtilTable other)
    {
      int retValue = -2;

      var isContinue = true;
      if (null == other)
      {
        // This value is greater than null.
        retValue = 1;
        isContinue = false;
      }
      if (isContinue)
      {
        retValue = ID.CompareTo(other.ID);
      }
      return retValue;
    }

    /// <summary>Initializes the original values.</summary>
    public void SetOriginalValues()
    {
      _OriginalValues.DataSiteID = _DataModuleSiteID;
      _OriginalValues.ID = _ID;
      _OriginalValues.DataModuleSiteID = _DataModuleSiteID;
      _OriginalValues.DataModuleID = _DataModuleID;
      _OriginalValues.Name = _Name;
      _OriginalValues.Description = _Description;
      _OriginalValues.Sequence = _Sequence;
      _OriginalValues.SchemaName = _SchemaName;
      _OriginalValues.NewName = _NewName;
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

    /// <summary>Gets or sets the database ID.</summary>
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

    /// <summary>Gets or sets the table row ID.</summary>
    [Required]
    [Column("ID", TypeName = "bigint")]
    public long ID
    {
      get => _ID;
      set
      {
        if (_DataModuleID != value)
        {
          _ID = ChangedNames.Add(ColumnID, _OriginalValues.ID
            , value);
        }
      }
    }
    private long _ID;

    /// <summary>Gets or sets the parent database ID.</summary>
    [Required]
    [Column("DataModuleSiteID", TypeName = "bigint")]
    public long DataModuleSiteID
    {
      get => _DataModuleID;
      set
      {
        if (_DataModuleID != value)
        {
          _DataModuleSiteID = ChangedNames.Add(ColumnDataModuleSiteID
            , _OriginalValues.DataModuleSiteID, value);
        }
      }
    }
    private long _DataModuleSiteID;

    /// <summary>Gets or sets the parent table row ID.</summary>
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

    /// <summary>Gets or sets the name.</summary>
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

    /// <summary>Gets or sets the description.</summary>
    [Required]
    [Column("Description", TypeName = "nvarchar(80")]
    public string Description
    {
      get => _Description;
      set
      {
        var newValue = value?.Trim();
        if (_Name != newValue)
        {
          _Description = ChangedNames.Add(ColumnDescription
            , _OriginalValues.Description, newValue);
        }
      }
    }
    private string _Description;

    /// <summary>Gets or sets the sequence.</summary>
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

    /// <summary>Gets or sets the schema name.</summary>
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

    /// <summary>Gets or sets the new name.</summary>
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

    /// <summary>Gets or sets the Join module name.</summary>
    public string ModuleName { get; set; }
    #endregion

    #region Class Properties

    /// <summary>Gets a reference to the ChangedNames list.</summary>
    public ChangedNames ChangedNames { get; private set; }
    #endregion

    #region Class Data

    /// <summary>The table name.</summary>
    public static string TableName = "DataTable";

    /// <summary>The DataSiteID column name.</summary>
    public static string ColumnDataSiteID = "DataSiteID";

    /// <summary>The ID column name.</summary>
    public static string ColumnID = "ID";

    /// <summary>The DataModuleSiteID column name.</summary>
    public static string ColumnDataModuleSiteID = "DataModuleSiteID";

    /// <summary>The DataModuleID column name.</summary>
    public static string ColumnDataModuleID = "DataModuleID";

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
      // Gets or sets the database ID.
      public long DataSiteID { get; set; }

      // Gets or sets the table row ID.
      public long ID { get; set; }

      // Gets or sets the parent database ID.
      public long DataModuleSiteID { get; set; }

      // Gets or sets the parent table row ID.
      public long DataModuleID { get; set; }

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

  /// <summary>Sort and search on Name value.</summary>
  public class DataTableUniqueComparer : IComparer<DataUtilTable>
  {
    // Compares two objects.
    /// <include path='items/Compare/*' file='../../LJCGenDoc/Common/Data.xml'/>
    public int Compare(DataUtilTable x, DataUtilTable y)
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
        retValue = x.DataModuleID.CompareTo(y.DataModuleID);
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
