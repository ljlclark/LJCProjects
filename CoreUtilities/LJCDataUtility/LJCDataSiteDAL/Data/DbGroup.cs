// Copyright (c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// DbGroup.cs
using LJCNetCommon;
using LJCDBClientLib;
using System;
using System.Collections.Generic;

namespace LJCDataSiteDAL
{
  /// <summary>The DataSite Data Object.</summary>
  public class DbGroup : IComparable<DbGroup>
  {
    #region Constructor Methods

    // Initializes an object instance.
    /// <include file='../../LJCGenDoc/Common/Data.xml'
    ///  path='items/DefaultConstructor/*'/>
    public DbGroup()
    {
      ChangedNames = new ChangedNames();
      _OriginalValues = new OriginalValues();
      LJCSetOriginalValues();
    }

    // The Copy constructor.
    /// <include file='../../LJCGenDoc/Common/Data.xml'
    ///  path='items/CopyConstructor/*'/>
    public DbGroup(DbGroup item)
    {
      ChangedNames = new ChangedNames();
      ID = item.ID;
      Name = item.Name;
      Description = item.Description;
    }
    #endregion

    #region Data Class Methods

    // Creates and returns a clone of this object.
    /// <include path='items/Clone/*' file='../../LJCGenDoc/Common/Data.xml'/>
    public DbGroup Clone()
    {
      var retValue = MemberwiseClone() as DbGroup;
      return retValue;
    }

    // Provides the default Sort functionality.
    /// <include path='items/CompareTo/*' file='../../LJCGenDoc/Common/Data.xml'/>
    public int CompareTo(DbGroup other)
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
        // Case sensitive and not string.
        retValue = ID.CompareTo(other.ID);
      }
      return retValue;
    }

    // Initializes the original values.
    /// <include file='../../LJCGenDoc/Common/Data.xml'
    ///  path='members/SetOriginalValues/*'/>
    public void LJCSetOriginalValues()
    {
      _OriginalValues.ID = _ID;
      _OriginalValues.DbID = _DbID;
      _OriginalValues.Name = _Name;
      _OriginalValues.Description = _Description;
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
    //[Required]
    //[Column("ID", TypeName="bigint")]
    public short ID
    {
      get { return _ID; }
      set
      {
        if (_ID != value)
        {
          _ID = ChangedNames.Add(ColumnID, _OriginalValues.ID
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
          _DbID = ChangedNames.Add(ColumnID, _OriginalValues.DbID
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
        if (_Name != newValue)
        {
          _Name = ChangedNames.Add(ColumnName, _OriginalValues.Name
            , value);
        }
      }
    }
    private string _Name;

    /// <summary>Gets or sets the Description value.</summary>
    //[Required]
    //[Column("Description", TypeName="varchar(80")]
    public string Description
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
    private string _Description;
    #endregion

    #region Class Properties

    /// <summary>Gets a reference to the ChangedNames list.</summary>
    public ChangedNames ChangedNames { get; private set; }
    #endregion

    #region Class Data

    /// <summary>The table name.</summary>
    public static string TableName = "DbGroup";

    /// <summary>The ID column name.</summary>
    public static string ColumnID = "ID";

    /// <summary>The DbID column name.</summary>
    public static string ColumnDbID = "DbID";

    /// <summary>The Name column name.</summary>
    public static string ColumnName = "Name";

    /// <summary>The Description column name.</summary>
    public static string ColumnDescription = "Description";

    /// <summary>The Name maximum length.</summary>
    public static int LengthName = 60;

    /// <summary>The Description maximum length.</summary>
    public static int LengthDescription = 80;

    // The object starting values.
    private readonly OriginalValues _OriginalValues;

    // The object starting values.
    private class OriginalValues
    {
      // Gets or sets the table row ID.
      public short ID { get; set; }

      // Gets or sets the database ID.
      public short DbID { get; set; }

      // Gets or sets the unique name.
      public string Name { get; set; }

      // Gets or sets the unique description.
      public string Description { get; set; }
    }
    #endregion
  }

  #region Comparers

  /// <summary>Sort and search on Name value.</summary>
  public class DataSiteUnique : IComparer<DbGroup>
  {
    // Compares two objects.
    /// <include path='items/Compare/*' file='../../LJCGenDoc/Common/Data.xml'/>
    public int Compare(DbGroup x, DbGroup y)
    {
      int retValue;

      while (true)
      {
        retValue = NetCommon.CompareNull(x, y);
        if (retValue != NetString.CompareNotNullOrEqual)
        {
          break;
        }

        retValue = NetCommon.CompareNull(x.Name, y.Name);
        if (retValue != NetString.CompareNotNullOrEqual)
        {
          break;
        }

        retValue = x.Name.CompareTo(y.Name);
        break;
      }
      return retValue;
    }
  }
  #endregion
}
