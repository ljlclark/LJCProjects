// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// ForeignKey.cs
using LJCDBClientLib;
using LJCNetCommon;
using System;

namespace LJCDataUtilityDAL
{
  /// <summary>The DataColumn Data Object.</summary>
  public class ForeignKey : IComparable<ForeignKey>
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public ForeignKey()
    {
    }

    // The Copy constructor.
    /// <include path='items/CopyConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public ForeignKey(ForeignKey item)
    {
      DBName = item.DBName;
      TableSchema = item.TableSchema;
      TableName = item.TableName;
      ColumnName = item.ColumnName;
      ConstraintDBName = item.ConstraintDBName;
      ConstraintSchema = item.ConstraintSchema;
      ConstraintName = item.ConstraintName;
      UniqueConstraintName = item.UniqueConstraintName;
      UpdateRule = item.UpdateRule;
      DeleteRule = item.DeleteRule;
      TargetTable = item.TargetTable;
      TargetColumn = item.TargetColumn;
      OrdinalPosition = item.OrdinalPosition;
    }
    #endregion

    #region Data Class Methods

    // Creates and returns a clone of this object.
    /// <include path='items/Clone/*' file='../../LJCDocLib/Common/Data.xml'/>
    public ForeignKey Clone()
    {
      var retValue = MemberwiseClone() as ForeignKey;
      return retValue;
    }

    // Provides the default Sort functionality.
    /// <include path='items/CompareTo/*' file='../../LJCDocLib/Common/Data.xml'/>
    public int CompareTo(ForeignKey other)
    {
      int retValue;

      if (null == other)
      {
        // This value is greater than null.
        retValue = 1;
      }
      else
      {
        // Case sensitive.
        retValue = ConstraintName.CompareTo(other.ConstraintName);
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

    /// <summary>Gets or sets the ConstraintDBName value.</summary>
    public String ConstraintDBName
    {
      get { return mConstraintDBName; }
      set
      {
        mConstraintDBName = NetString.InitString(value);
      }
    }
    private String mConstraintDBName;

    /// <summary>Gets or sets the ConstraintSchema value.</summary>
    public String ConstraintSchema
    {
      get { return mConstraintSchema; }
      set
      {
        mConstraintSchema = NetString.InitString(value);
      }
    }
    private String mConstraintSchema;

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
    public String TargetColumn
    {
      get { return mTargetColumn; }
      set
      {
        mTargetColumn = NetString.InitString(value);
      }
    }
    private String mTargetColumn;

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
    public static string ColumnUniqueConstraintName = "UniqueConstraintName";
    public static string ColumnUpdateRule = "UpdateRule";
    public static string ColumnDeleteRule = "DeleteRule";
    public static string ColumnTargetTable = "TargetTable";
    public static string ColumnTargetColumn = "TargetColumn";
    public static string ColumnOrdinalPosition = "OrdinalPosition";
    #endregion
  }
}
