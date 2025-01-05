// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// TableKey.cs
using LJCDBClientLib;
using LJCNetCommon;
using System;

namespace LJCDataUtilityDAL
{
  /// <summary>The DataColumn Data Object.</summary>
  public class TableKey : IComparable<TableKey>
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public TableKey()
    {
    }

    // The Copy constructor.
    /// <include path='items/CopyConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public TableKey(TableKey item)
    {
      DBName = item.DBName;
      TableSchema = item.TableSchema;
      TableName = item.TableName;
      KeyType = item.KeyType;
      ColumnName = item.ColumnName;
      ConstraintName = item.ConstraintName;
      TargetTable = item.TargetTable;
      TargetColumns = item.TargetColumns;
      UpdateRule = item.UpdateRule;
      DeleteRule = item.DeleteRule;
      OrdinalPosition = item.OrdinalPosition;
    }
    #endregion

    #region Data Class Methods

    // Creates and returns a clone of this object.
    /// <include path='items/Clone/*' file='../../LJCDocLib/Common/Data.xml'/>
    public TableKey Clone()
    {
      var retValue = MemberwiseClone() as TableKey;
      return retValue;
    }

    // Provides the default Sort functionality.
    /// <include path='items/CompareTo/*' file='../../LJCDocLib/Common/Data.xml'/>
    public int CompareTo(TableKey other)
    {
      int retValue;

      if (null == other)
      {
        // This value is greater than null.
        retValue = 1;
      }
      else
      {
        // Not case sensitive.
        retValue = string.Compare(ConstraintName, other.ConstraintName, true);
        if (0 == retValue)
        {
          // Case sensitive.
          retValue = OrdinalPosition.CompareTo(other.OrdinalPosition);
        }
      }
      return retValue;
    }
    #endregion

    #region Data Properties

    // Update ChangedNames.Add() statements to "Property" constant
    // if property was renamed.

    /// <summary>Gets or sets the DBName value.</summary>
    public String DBName
    {
      get { return mDBName; }
      set
      {
        mDBName = NetString.InitString(value);
      }
    }
    private String mDBName;

    /// <summary>Gets or sets the TableSchema value.</summary>
    public String TableSchema
    {
      get { return mTableSchema; }
      set
      {
        mTableSchema = NetString.InitString(value);
      }
    }
    private String mTableSchema;

    /// <summary>Gets or sets the TableName value.</summary>
    public String TableName
    {
      get { return mTableName; }
      set
      {
        mTableName = NetString.InitString(value);
      }
    }
    private String mTableName;

    /// <summary>Gets or sets the KeyType value.</summary>
    public String KeyType
    {
      get { return mKeyType; }
      set
      {
        mKeyType = NetString.InitString(value);
      }
    }
    private String mKeyType;

    /// <summary>Gets or sets the ColumnName value.</summary>
    public String ColumnName
    {
      get { return mColumnName; }
      set
      {
        mColumnName = NetString.InitString(value);
      }
    }
    private String mColumnName;

    /// <summary>Gets or sets the ConstraintName value.</summary>
    public String ConstraintName
    {
      get { return mConstraintName; }
      set
      {
        mConstraintName = NetString.InitString(value);
      }
    }
    private String mConstraintName;

    /// <summary>Gets or sets the UpdateRule value.</summary>
    public String UpdateRule
    {
      get { return mUpdateRule; }
      set
      {
        mUpdateRule = NetString.InitString(value);
      }
    }
    private String mUpdateRule;

    /// <summary>Gets or sets the DeleteRule value.</summary>
    public String DeleteRule
    {
      get { return mDeleteRule; }
      set
      {
        mDeleteRule = NetString.InitString(value);
      }
    }
    private String mDeleteRule;

    /// <summary>Gets or sets the TargetTable value.</summary>
    public String TargetTable
    {
      get { return mTargetTable; }
      set
      {
        mTargetTable = NetString.InitString(value);
      }
    }
    private String mTargetTable;

    /// <summary>Gets or sets the TargetColumn value.</summary>
    public String TargetColumns
    {
      get { return mTargetColumns; }
      set
      {
        mTargetColumns = NetString.InitString(value);
      }
    }
    private String mTargetColumns;

    /// <summary>Gets or sets the OrdinalPosition value.</summary>
    public Int32 OrdinalPosition
    {
      get { return mOrdinalPosition; }
      set
      {
        mOrdinalPosition = value;
      }
    }
    private Int32 mOrdinalPosition;

    /// <summary>Gets or sets the UniqueConstraintName value.</summary>
    public String UniqueConstraintName
    {
      get { return mUniqueConstraintName; }
      set
      {
        mUniqueConstraintName = NetString.InitString(value);
      }
    }
    private String mUniqueConstraintName;
    #endregion

    #region Class Data

    /// <summary>The ID column name.</summary>
    public static string ColumnDBName = "TableCatalog";
    public static string ColumnTableSchema = "TableSchema";
    public static string ColumnTableName = "TableName";
    public static string ColumnColumnName = "ColumnName";
    public static string ColumnConstraintDBName = "ConstraintDBName";
    public static string ColumnConstraintSchema = "ConstraintSchema";
    public static string ColumnConstraintName = "ConstraintName";
    public static string ColumnUpdateRule = "UpdateRule";
    public static string ColumnDeleteRule = "DeleteRule";
    public static string ColumnTargetTable = "TargetTable";
    public static string ColumnTargetColumn = "TargetColumn";
    public static string ColumnOrdinalPosition = "OrdinalPosition";
    #endregion
  }
}
