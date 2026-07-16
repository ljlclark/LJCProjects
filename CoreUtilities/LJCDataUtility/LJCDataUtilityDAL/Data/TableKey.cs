// Copyright (c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// TableKey.cs
using LJCNetCommon;
using System;

namespace LJCDataUtilityDAL
{
  // The DataColumn Data Object.
  /// <include file='Doc/TableKey.xml'
  ///  path='members/TableKey/*'/>
  public class TableKey : IComparable<TableKey>
  {
    #region Constructors

    // Initializes an object instance.
    /// <include file='../../LJCGenDoc/Common/Data.xml'
    ///  path='members/Constructor/*'/>
    public TableKey()
    {
      _ColumnName = null;
      _ConstraintName = null;
      _DBName = null;
      _KeyType = null;
      _OrdinalPosition = 0;
      _TableName = null;
      _TableSchema = null;
      _TargetColumn = null;
      _TargetTable = null;
      _UniqueConstraintName = null;
    }

    // The Copy constructor.
    /// <include file='../../LJCGenDoc/Common/Data.xml'
    ///  path='members/CopyConstructor/*'/>
    public TableKey(TableKey item)
    {
      _ColumnName = item.ColumnName;
      _ConstraintName = item.ConstraintName;
      _DBName = item.DBName;
      _KeyType = item.KeyType;
      _OrdinalPosition = item.OrdinalPosition;
      _TableName = item.TableName;
      _TableSchema = item.TableSchema;
      _TargetColumn = item.TargetColumn;
      _TargetTable = item.TargetTable;
      _UniqueConstraintName = item.UniqueConstraintName;
    }
    #endregion

    #region Data Object Methods

    // Creates and returns a clone of this object.
    /// <include file='../../LJCGenDoc/Common/Data.xml'
    ///  path='members/Clone/*'/>
    public TableKey Clone()
    {
      var retValue = MemberwiseClone() as TableKey;
      return retValue;
    }

    // Provides the default Sort functionality.
    /// <include file='../../LJCGenDoc/Common/Data.xml'
    ///  path='members/CompareTo/*'/>
    public int CompareTo(TableKey other)
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

        retValue = string.Compare(ConstraintName, other.ConstraintName, true);
        if (retValue != NetString.CompareEqual)
        {
          break;
        }

        retValue = OrdinalPosition.CompareTo(other.OrdinalPosition);
        break;
      }
      return retValue;
    }
    #endregion

    #region Data Properties

    // Gets or sets the ColumnName value.
    /// <include file='doc/TableKey.xml'
    ///  path='members/ColumnName/*'/>
    public string ColumnName
    {
      get => _ColumnName;
      set
      {
        _ColumnName = value?.Trim();
      }
    }
    private string _ColumnName;

    // Gets or sets the ConstraintName value.
    /// <include file='doc/TableKey.xml'
    ///  path='members/ConstraintName/*'/>
    public string ConstraintName
    {
      get => _ConstraintName;
      set
      {
        _ConstraintName = value?.Trim();
      }
    }
    private string _ConstraintName;

    // Update ChangedNames.Add() statements to "Property" constant
    // if property was renamed.

    // Gets or sets the DBName value.
    /// <include file='doc/TableKey.xml'
    ///  path='members/DBName/*'/>
    public string DBName
    {
      get => _DBName;
      set
      {
        _DBName = value?.Trim();
      }
    }
    private string _DBName;

    // Gets or sets the KeyType value.
    /// <include file='doc/TableKey.xml'
    ///  path='members/KeyType/*'/>
    public string KeyType
    {
      get => _KeyType;
      set
      {
        _KeyType = value?.Trim();
      }
    }
    private string _KeyType;

    // Gets or sets the OrdinalPosition value.
    /// <include file='doc/TableKey.xml'
    ///  path='members/OrdinalPosition/*'/>
    public int OrdinalPosition
    {
      get => _OrdinalPosition;
      set
      {
        _OrdinalPosition = value;
      }
    }
    private int _OrdinalPosition;

    // Gets or sets the TableName value.
    /// <include file='doc/TableKey.xml'
    ///  path='members/TableName/*'/>
    public string TableName
    {
      get => _TableName;
      set
      {
        _TableName = value?.Trim();
      }
    }
    private string _TableName;

    // Gets or sets the TableSchema value.
    /// <include file='doc/TableKey.xml'
    ///  path='members/TableSchema/*'/>
    public string TableSchema
    {
      get => _TableSchema;
      set
      {
        _TableSchema = value?.Trim();
      }
    }
    private string _TableSchema;

    // Gets or sets the TargetColumn value.
    /// <include file='doc/TableKey.xml'
    ///  path='members/TargetColumns/*'/>
    public string TargetColumn
    {
      get => _TargetColumn;
      set
      {
        _TargetColumn = value?.Trim();
      }
    }
    private string _TargetColumn;

    // Gets or sets the TargetTable value.
    /// <include file='doc/TableKey.xml'
    ///  path='members/TargetTable/*'/>
    public string TargetTable
    {
      get => _TargetTable;
      set
      {
        _TargetTable = value?.Trim();
      }
    }
    private string _TargetTable;

    // Gets or sets the UniqueConstraintName value.
    /// <include file='doc/TableKey.xml'
    ///  path='members/UniqueConstraintName/*'/>
    public string UniqueConstraintName
    {
      get => _UniqueConstraintName;
      set
      {
        _UniqueConstraintName = value?.Trim();
      }
    }
    private string _UniqueConstraintName;
    #endregion
  }
}
