// Copyright (c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// DbGroup5.cs
using LJCNetCommon5;
using System;
using System.Collections.Generic;
using LJCDBMessage5;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LJCDataSiteDAL5
{
  // The DataSite Data Object.
  /// <include file='Doc/DataGroup.xml'
  ///  path='members/DataGroup/*'/>
  public class DbGroup : IComparable<DbGroup>
  {
    #region Constructor Methods

    // Initializes an object instance.
    /// <include file='../../LJCGenDoc/Common/Data.xml'
    ///  path='members/Constructor/*'/>
    public DbGroup()
    {
      _Name = "";
      _Description = null;

      ChangedNames = [];
      _OriginalValues = new OriginalValues();
      LJCSetOriginalValues();
    }

    // The Copy constructor.
    /// <include file='../../LJCGenDoc/Common/Data.xml'
    ///  path='members/CopyConstructor/*'/>
    public DbGroup(DbGroup item)
    {
      _ID = item.ID;
      _Name = item.Name;
      _Description = item.Description;

      ChangedNames = [];
      _OriginalValues = new OriginalValues();
    }
    #endregion

    #region Data Class Methods

    // Creates and returns a clone of this object.
    /// <include file='../../LJCGenDoc/Common/Data.xml'
    ///  path='members/Clone/*'/>
    public DbGroup? Clone()
    {
      var retValue = MemberwiseClone() as DbGroup;
      return retValue;
    }

    // Provides the default Sort functionality.
    /// <include file='../../LJCGenDoc/Common/Data.xml'
    ///  path='members/CompareTo/*'/>
    public int CompareTo(DbGroup? other)
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

        // Case sensitive and not string.
        retValue = ID.CompareTo(other.ID);
        break;
      }
      return retValue;
    }

    // Initializes the original values.
    /// <include file='../../LJCGenDoc/Common/Data.xml'
    ///  path='members/SetOriginalValues/*'/>
    public void LJCSetOriginalValues()
    {
      _OriginalValues.Id = _ID;
      _OriginalValues.DbId = _DbID;
      _OriginalValues.Name = _Name;
      _OriginalValues.Description = _Description;
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

    /// <summary>Gets or sets the ID value.</summary>
    [Required]
    [Column("ID", TypeName = "bigint")]
    public short ID
    {
      get { return _ID; }
      set
      {
        if (_ID != value)
        {
          _ID = ChangedNames.Add(ColumnID, _OriginalValues.Id
          , value);
        }
      }
    }
    private short _ID;

    /// <summary>Gets or sets the database ID value.</summary>
    //[Required]
    //[Column("DbID", TypeName="smallint")]
    public short DbID
    {
      get { return _DbID; }
      set
      {
        if (_DbID != value)
        {
          _DbID = ChangedNames.Add(ColumnID, _OriginalValues.DbId
          , value);
        }
      }
    }
    private short _DbID;

    /// <summary>Gets or sets the Name value.</summary>
    //[Required]
    //[Column("Name", TypeName="varchar(60")]
    public string Name
    {
      get { return _Name; }
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

    /// <summary>Gets or sets the Description value.</summary>
    //[Required]
    //[Column("Description", TypeName="varchar(80")]
    public string? Description
    {
      get => _Description;
      set
      {
        var newValue = value?.Trim();
        if (_Description != newValue)
        {
          _Description = ChangedNames.Add(ColumnDescription
            , _OriginalValues.Description, value);
        }
      }
    }
    private string? _Description;
    #endregion

    #region Class Properties

    /// <summary>Gets a reference to the ChangedNames list.</summary>
    public LJCChangedNames ChangedNames { get; private set; }
    #endregion

    #region Class Data

    /// <summary>The table name.</summary>
    public const string TableName = "DbGroup";

    /// <summary>The ID column name.</summary>
    public const string ColumnID = "ID";

    /// <summary>The DbID column name.</summary>
    public const string ColumnDbID = "DbID";

    /// <summary>The Name column name.</summary>
    public const string ColumnName = "Name";

    /// <summary>The Description column name.</summary>
    public const string ColumnDescription = "Description";

    /// <summary>The Name maximum length.</summary>
    public const int LengthName = 60;

    /// <summary>The Description maximum length.</summary>
    public const int LengthDescription = 80;

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
        Name = "";
        Description = null;
      }

      // Gets or sets the database ID.
      public short DbId { get; set; }

      // Gets or sets the table row ID.
      public short Id { get; set; }

      // Gets or sets the unique name.
      public string Name { get; set; }

      // Gets or sets the unique description.
      public string? Description { get; set; }
    }
    #endregion
  }

  #region Comparers

  /// <summary>Sort and search on Name value.</summary>
  public class DataSiteUnique : IComparer<DbGroup>
  {
    // Compares two objects.
    /// <include path='members/Compare/*' file='../../LJCGenDoc/Common/Data.xml'/>
    public int Compare(DbGroup? x, DbGroup? y)
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

        retValue = x!.Name.CompareTo(y!.Name);
        break;
      }
      return retValue;
    }
  }
  #endregion
}
