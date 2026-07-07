// Copyright (c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// DataModule.cs
using LJCDBClientLib;
using LJCNetCommon;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LJCDataUtilityDAL
{
  // The DataModule data.
  /// <include file='Doc/DataModule.xml'
  ///  path='members/DataModule/*'/>
  public class DataModule : IComparable<DataModule>
  {
    #region Constructor Methods

    // Initializes an object instance.
    /// <include file='../../LJCGenDoc/Common/Data.xml'
    ///  path='members/Constructor/*'/>
    public DataModule()
    {
      _ID = 0;
      _DataSiteID = 0;
      _Name = "";
      _Description = "";

      ChangedNames = new ChangedNames();
      _OriginalValues = new OriginalValues();
      LJCSetOriginalValues();
    }

    // Initializes an object instance with the supplied values.
    /// <include file='Doc/DataModule.xml'
    ///  path='members/ParamConstructor/*'/>
    public DataModule(string name) : this()
    {
      _Name = name;
    }

    // The Copy constructor.
    /// <include file='../../LJCGenDoc/Common/Data.xml'
    ///  path='members/CopyConstructor/*'/>
    public DataModule(DataModule item)
    {
      _ID = item.ID;
      _DataSiteID = item.DataSiteID;
      _Name = item.Name;
      _Description = item.Description;

      ChangedNames = item.ChangedNames;
      _OriginalValues = new OriginalValues();
      LJCSetOriginalValues();
    }
    #endregion

    #region Data Object Methods

    // Creates and returns a clone of this object.
    /// <include file='../../LJCGenDoc/Common/Data.xml'
    ///  path='members/Clone/*'/>
    public DataModule Clone()
    {
      var retValue = MemberwiseClone() as DataModule;
      return retValue;
    }

    // Provides the default Sort functionality.
    /// <include file='../../LJCGenDoc/Common/Data.xml'
    ///  path='members/CompareTo/*'/>
    public int CompareTo(DataModule other)
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
    ///  path='members/SetOriginalValues/*'/>
    public void LJCSetOriginalValues()
    {
      _OriginalValues.ID = _ID;
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

    // Gets or sets the ID value.
    /// <include file='Doc/DataModule.xml'
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

    // Gets or sets the DataModuleID value.
    /// <include file='Doc/DataModule.xml'
    ///  path='members/DataSiteID/*'/>
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

    // Gets or sets the Name value.
    /// <include file='Doc/DataModule.xml'
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
    /// <include file='Doc/DataModule.xml'
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
    #endregion

    #region Class Properties

    // Gets a reference to the ChangedNames list.
    /// <include file='../../LJCGenDoc/Common/Data.xml'
    ///  path='members/ToString/*'/>
    public ChangedNames ChangedNames { get; private set; }
    #endregion

    #region Class Data

    /// <summary>The table name.</summary>
    public static string TableName = "DataModule";

    /// <summary>The ID column name.</summary>
    public static string ColumnID = "ID";

    /// <summary>The DataSiteID column name.</summary>
    public static string ColumnDataSiteID = "DataSiteID";

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
      public long ID { get; set; }

      // Gets or sets the database ID.
      public long DataSiteID { get; set; }

      // Gets or sets the unique name.
      public string Name { get; set; }

      // Gets or sets the description.
      public string Description { get; set; }
    }
    #endregion
  }

  #region Comparers

  // Sort and search on Name value.
  /// <include file='Doc/DataModule.xml'
  ///  path='items/Compare/*'/>
  public class DataModuleUniqueComparer : IComparer<DataModule>
  {
    // Compares two objects.
    /// <include file='../../LJCGenDoc/Common/Data.xml'
    ///  path='items/Compare/*'/>
    public int Compare(DataModule x, DataModule y)
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

        retValue = string.Compare(x.Name, y.Name, true);
        break;
      }
      return retValue;
    }
  }
  #endregion
}
