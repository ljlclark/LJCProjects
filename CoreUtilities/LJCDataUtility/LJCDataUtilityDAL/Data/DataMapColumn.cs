// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// DataMapColumn.css
using LJCNetCommon;
using LJCDBClientLib;
using System;
using System.Collections.Generic;

namespace LJCDataUtilityDAL
{
  /// <summary>The DataMapColumn table Data Object.</summary>
  public class DataMapColumn : IComparable<DataMapColumn>
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public DataMapColumn()
    {
      ChangedNames = new ChangedNames();
    }

    // The Copy constructor.
    /// <include path='items/CopyConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public DataMapColumn(DataMapColumn item)
    {
      ChangedNames = new ChangedNames();
      //DataTableMapID = item.DataTableMapID;
      DataColumnID = item.DataColumnID;
      ColumnName = item.ColumnName;
      Sequence = item.Sequence;
      MaxLength = item.MaxLength;
      IsDelete = item.IsDelete;
    }
    #endregion

    #region Data Methods

    // Creates and returns a clone of this object.
    /// <include path='items/Clone/*' file='../../LJCDocLib/Common/Data.xml'/>
    public DataMapColumn Clone()
    {
      var retValue = MemberwiseClone() as DataMapColumn;
      return retValue;
    }

    // Provides the default Sort functionality.
    /// <include path='items/CompareTo/*' file='../../LJCDocLib/Common/Data.xml'/>
    public int CompareTo(DataMapColumn other)
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
        //retValue = DataTableMapID.CompareTo(other.DataTableMapID);
        //if (0 == retValue)
        //{
          retValue = DataColumnID.CompareTo(other.DataColumnID);
        //}

        // Not case sensitive.
        //retValue = string.Compare(ID, other.ID, true);
      }
      return retValue;
    }

    // The object string identifier.
    /// <include path='items/ToString/*' file='../../LJCDocLib/Common/Data.xml'/>
    public override string ToString()
    {
      //var retValue = $"{mDataTableMapID}: {mColumnName}";
      var retValue = mColumnName;
      return retValue;
    }
    #endregion

    #region Data Properties

    // Update ChangedNames.Add() statements to "Property" constant
    // if property was renamed.

    ///// <summary>Gets or sets the DataTableMapID value.</summary>
    ////[Required]
    ////[Column("DataTableMapID", TypeName="int")]
    //public Int32 DataTableMapID
    //{
    //  get { return mDataTableMapID; }
    //  set
    //  {
    //    mDataTableMapID = ChangedNames.Add(ColumnDataTableMapID, mDataTableMapID, value);
    //  }
    //}
    //private Int32 mDataTableMapID;

    /// <summary>Gets or sets the DataColumnID value.</summary>
    //[Required]
    //[Column("DataColumnID", TypeName="int")]
    public Int32 DataColumnID
    {
      get { return mDataColumnID; }
      set
      {
        mDataColumnID = ChangedNames.Add(ColumnDataColumnID, mDataColumnID, value);
      }
    }
    private Int32 mDataColumnID;

    /// <summary>Gets or sets the ColumnName value.</summary>
    //[Column("ColumnName", TypeName="nvarchar(60")]
    public String ColumnName
    {
      get { return mColumnName; }
      set
      {
        value = NetString.InitString(value);
        mColumnName = ChangedNames.Add(ColumnColumnName, mColumnName, value);
      }
    }
    private String mColumnName;

    /// <summary>Gets or sets the Sequence value.</summary>
    //[Required]
    //[Column("Sequence", TypeName="smallint")]
    public Int16 Sequence
    {
      get { return mSequence; }
      set
      {
        mSequence = ChangedNames.Add(ColumnSequence, mSequence, value);
      }
    }
    private Int16 mSequence;

    /// <summary>Gets or sets the MaxLength value.</summary>
    //[Required]
    //[Column("MaxLength", TypeName="smallint")]
    public Int16 MaxLength
    {
      get { return mMaxLength; }
      set
      {
        mMaxLength = ChangedNames.Add(ColumnMaxLength, mMaxLength, value);
      }
    }
    private Int16 mMaxLength;

    /// <summary>Gets or sets the IsDelete value.</summary>
    //[Required]
    //[Column("IsDelete", TypeName="bit")]
    public Boolean IsDelete
    {
      get { return mIsDelete; }
      set
      {
        mIsDelete = ChangedNames.Add(ColumnIsDelete, mIsDelete, value);
      }
    }
    private Boolean mIsDelete;
    #endregion

    #region Class Properties

    /// <summary>Gets a reference to the ChangedNames list.</summary>
    public ChangedNames ChangedNames { get; private set; }
    #endregion

    #region Class Data

    /// <summary>The table name.</summary>
    public static string TableName = "DataMapColumn";

    /// <summary>The DataTableMapID column name.</summary>
    public static string ColumnDataTableMapID = "DataTableMapID";

    /// <summary>The DataColumnID column name.</summary>
    public static string ColumnDataColumnID = "DataColumnID";

    /// <summary>The ColumnName column name.</summary>
    public static string ColumnColumnName = "ColumnName";

    /// <summary>The Sequence column name.</summary>
    public static string ColumnSequence = "Sequence";

    /// <summary>The MaxLength column name.</summary>
    public static string ColumnMaxLength = "MaxLength";

    /// <summary>The IsDelete column name.</summary>
    public static string ColumnIsDelete = "IsDelete";

    /// <summary>The ColumnName maximum length.</summary>
    public static int LengthColumnName = 60;
    #endregion
  }
}
