// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// DataMapTable.cs
using LJCNetCommon;
using LJCDBClientLib;
using System;
using System.Collections.Generic;

namespace LJCDataUtilityDAL
{
  /// <summary>The DataMapTable table Data Object.</summary>
  public class DataMapTable : IComparable<DataMapTable>
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public DataMapTable()
    {
      ChangedNames = new ChangedNames();
    }

    // The Copy constructor.
    /// <include path='items/CopyConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public DataMapTable(DataMapTable item)
    {
      ChangedNames = new ChangedNames();
      DataTableID = item.DataTableID;
      NewTableName = item.NewTableName;
    }
    #endregion

    #region Data Methods

    // Creates and returns a clone of this object.
    /// <include path='items/Clone/*' file='../../LJCDocLib/Common/Data.xml'/>
    public DataMapTable Clone()
    {
      var retValue = MemberwiseClone() as DataMapTable;
      return retValue;
    }

    // Provides the default Sort functionality.
    /// <include path='items/CompareTo/*' file='../../LJCDocLib/Common/Data.xml'/>
    public int CompareTo(DataMapTable other)
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
        retValue = DataTableID.CompareTo(other.DataTableID);
      }
      return retValue;
    }

    // The object string identifier.
    /// <include path='items/ToString/*' file='../../LJCDocLib/Common/Data.xml'/>
    public override string ToString()
    {
      var retValue = $"{mDataTableID}";
      return retValue;
    }
    #endregion

    #region Data Properties

    // Update ChangedNames.Add() statements to "Property" constant
    // if property was renamed.

    /// <summary>Gets or sets the DataTableID value.</summary>
    //[Required]
    //[Column("DataTableID", TypeName="int")]
    public Int32 DataTableID
    {
      get { return mDataTableID; }
      set
      {
        mDataTableID = ChangedNames.Add(ColumnDataTableID, mDataTableID, value);
      }
    }
    private Int32 mDataTableID;

    /// <summary>Gets or sets the NewTableName value.</summary>
    //[Column("NewTableName", TypeName="nvarchar(60")]
    public String NewTableName
    {
      get { return mNewTableName; }
      set
      {
        value = NetString.InitString(value);
        mNewTableName = ChangedNames.Add(ColumnNewTableName, mNewTableName, value);
      }
    }
    private String mNewTableName;
    #endregion

    #region Class Properties

    /// <summary>Gets a reference to the ChangedNames list.</summary>
    public ChangedNames ChangedNames { get; private set; }
    #endregion

    #region Class Data

    /// <summary>The table name.</summary>
    public static string TableName = "DataMapTable";

    /// <summary>The DataTableID column name.</summary>
    public static string ColumnDataTableID = "DataTableID";

    /// <summary>The NewTableName column name.</summary>
    public static string ColumnNewTableName = "NewTableName";

    /// <summary>The NewTableName maximum length.</summary>
    public static int LengthNewTableName = 60;
    #endregion
  }
}
