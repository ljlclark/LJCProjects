// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// DataColumn.cs
using LJCNetCommon;
using LJCDBClientLib;
using System;
using System.Collections.Generic;

namespace LJCDataUtilityDAL
{
  /// <summary>The DataColumn Data Object.</summary>
  public class DataUtilityColumn : IComparable<DataUtilityColumn>
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public DataUtilityColumn()
    {
      ChangedNames = new ChangedNames();
    }

    // The Copy constructor.
    /// <include path='items/CopyConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public DataUtilityColumn(DataUtilityColumn item)
    {
      ChangedNames = new ChangedNames();
      ID = item.ID;
      DataTableID = item.DataTableID;
      Name = item.Name;
      Description = item.Description;
      Sequence = item.Sequence;
      TypeName = item.TypeName;
      IsIdentity = item.IsIdentity;
      IdentityStart = item.IdentityStart;
      IdentityIncrement = item.IdentityIncrement;
      MaxLength = item.MaxLength;
      AllowNull = item.AllowNull;
      NewName = item.NewName;
      NewSequence = item.NewSequence;
      NewMaxLength = item.NewMaxLength;
    }
    #endregion

    #region Data Methods

    // Creates and returns a clone of this object.
    /// <include path='items/Clone/*' file='../../LJCDocLib/Common/Data.xml'/>
    public DataUtilityColumn Clone()
    {
      var retValue = MemberwiseClone() as DataUtilityColumn;
      return retValue;
    }

    // Provides the default Sort functionality.
    /// <include path='items/CompareTo/*' file='../../LJCDocLib/Common/Data.xml'/>
    public int CompareTo(DataUtilityColumn other)
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
        retValue = ID.CompareTo(other.ID);
      }
      return retValue;
    }

    // The object string identifier.
    /// <include path='items/ToString/*' file='../../LJCDocLib/Common/Data.xml'/>
    public override string ToString()
    {
      var retValue = $"{mName}:{mID}";
      return retValue;
    }
    #endregion

    #region Data Properties

    // Update ChangedNames.Add() statements to "Property" constant
    // if property was renamed.

    /// <summary>Gets or sets the ID value.</summary>
    //[Required]
    //[Column("ID", TypeName="int")]
    public Int32 ID
    {
      get { return mID; }
      set
      {
        mID = ChangedNames.Add(ColumnID, mID, value);
      }
    }
    private Int32 mID;

    /// <summary>Gets or sets the DataTableID value.</summary>
    //[Required]
    //[Column("DataTableID", TypeName="int")]
    public Int32 DataTableID
    {
      get { return mDataTableID; }
      set
      {
        mDataTableID = ChangedNames.Add(ColumnDataTableID
          , mDataTableID, value);
      }
    }
    private Int32 mDataTableID;

    /// <summary>Gets or sets the Name value.</summary>
    //[Required]
    //[Column("Name", TypeName="nvarchar(60")]
    public String Name
    {
      get { return mName; }
      set
      {
        value = NetString.InitString(value);
        mName = ChangedNames.Add(ColumnName, mName, value);
      }
    }
    private String mName;

    /// <summary>Gets or sets the Description value.</summary>
    //[Required]
    //[Column("Description", TypeName="nvarchar(80")]
    public String Description
    {
      get { return mDescription; }
      set
      {
        value = NetString.InitString(value);
        mDescription = ChangedNames.Add(ColumnDescription
          , mDescription, value);
      }
    }
    private String mDescription;

    /// <summary>Gets or sets the Sequence value.</summary>
    //[Required]
    //[Column("Sequence", TypeName="int")]
    public Int32 Sequence
    {
      get { return mSequence; }
      set
      {
        mSequence = ChangedNames.Add(ColumnSequence
          , mSequence, value);
      }
    }
    private Int32 mSequence;

    /// <summary>Gets or sets the TypeName value.</summary>
    //[Required]
    //[Column("TypeName", TypeName="nvarchar(20")]
    public String TypeName
    {
      get { return mTypeName; }
      set
      {
        value = NetString.InitString(value);
        mTypeName = ChangedNames.Add(ColumnTypeName
          , mTypeName, value);
      }
    }
    private String mTypeName;

    /// <summary>Gets or sets the IsIdentity value.</summary>
    //[Column("IsIdentity", TypeName="bit")]
    public Boolean IsIdentity
    {
      get { return mIsIdentity; }
      set
      {
        mIsIdentity = ChangedNames.Add(ColumnIsIdentity
          , mIsIdentity, value);
      }
    }
    private Boolean mIsIdentity;

    /// <summary>Gets or sets the IdentityStart value.</summary>
    //[Column("IdentityStart", TypeName="smallint")]
    public Int16 IdentityStart
    {
      get { return mIdentityStart; }
      set
      {
        mIdentityStart = ChangedNames.Add(ColumnIdentityStart
          , mIdentityStart, value);
      }
    }
    private Int16 mIdentityStart;

    /// <summary>Gets or sets the IdentityIncrement value.</summary>
    //[Column("IdentityIncrement", TypeName="smallint")]
    public Int16 IdentityIncrement
    {
      get { return mIdentityIncrement; }
      set
      {
        mIdentityIncrement = ChangedNames.Add(ColumnIdentityIncrement
          , mIdentityIncrement, value);
      }
    }
    private Int16 mIdentityIncrement;

    /// <summary>Gets or sets the MaxLength value.</summary>
    //[Required]
    //[Column("MaxLength", TypeName="smallint")]
    public Int16 MaxLength
    {
      get { return mMaxLength; }
      set
      {
        mMaxLength = ChangedNames.Add(ColumnMaxLength
          , mMaxLength, value);
      }
    }
    private Int16 mMaxLength;

    /// <summary>Gets or sets the AllowNull value.</summary>
    //[Column("AllowNull", TypeName="bit")]
    public Boolean AllowNull
    {
      get { return mAllowNull; }
      set
      {
        mAllowNull = ChangedNames.Add(ColumnAllowNull
          , mAllowNull, value);
      }
    }
    private Boolean mAllowNull;

    /// <summary>Gets or sets the NewName value.</summary>
    //[Column("NewName", TypeName="nvarchar(60")]
    public String NewName
    {
      get { return mNewName; }
      set
      {
        value = NetString.InitString(value);
        mNewName = ChangedNames.Add(ColumnNewName
          , mName, value);
      }
    }
    private String mNewName;

    /// <summary>Gets or sets the NewSequence value.</summary>
    //[Required]
    //[Column("NewSequence", TypeName="int")]
    public Int32 NewSequence
    {
      get { return mNewSequence; }
      set
      {
        mNewSequence = ChangedNames.Add(ColumnNewSequence
          , mNewSequence, value);
      }
    }
    private Int32 mNewSequence;

    /// <summary>Gets or sets the MaxLength value.</summary>
    //[Required]
    //[Column("NewMaxLength", TypeName="smallint")]
    public Int16 NewMaxLength
    {
      get { return mNewMaxLength; }
      set
      {
        mNewMaxLength = ChangedNames.Add(ColumnNewMaxLength
          , mNewMaxLength, value);
      }
    }
    private Int16 mNewMaxLength;
    #endregion

    #region Class Properties

    /// <summary>Gets a reference to the ChangedNames list.</summary>
    public ChangedNames ChangedNames { get; private set; }
    #endregion

    #region Class Data

    /// <summary>The table name.</summary>
    public static string TableName = "DataColumn";

    /// <summary>The ID column name.</summary>
    public static string ColumnID = "ID";

    /// <summary>The DataTableID column name.</summary>
    public static string ColumnDataTableID = "DataTableID";

    /// <summary>The Name column name.</summary>
    public static string ColumnName = "Name";

    /// <summary>The Description column name.</summary>
    public static string ColumnDescription = "Description";

    /// <summary>The Sequence column name.</summary>
    public static string ColumnSequence = "Sequence";

    /// <summary>The TypeName column name.</summary>
    public static string ColumnTypeName = "TypeName";

    /// <summary>The IsIdentity column name.</summary>
    public static string ColumnIsIdentity = "IsIdentity";

    /// <summary>The IdentityStart column name.</summary>
    public static string ColumnIdentityStart = "IdentityStart";

    /// <summary>The IdentityIncrement column name.</summary>
    public static string ColumnIdentityIncrement = "IdentityIncrement";

    /// <summary>The MaxLength column name.</summary>
    public static string ColumnMaxLength = "MaxLength";

    /// <summary>The AllowNull column name.</summary>
    public static string ColumnAllowNull = "AllowNull";

    /// <summary>The Name column name.</summary>
    public static string ColumnNewName = "NewName";

    /// <summary>The Sequence column name.</summary>
    public static string ColumnNewSequence = "NewSequence";

    /// <summary>The MaxLength column name.</summary>
    public static string ColumnNewMaxLength = "NewMaxLength";

    /// <summary>The Name maximum length.</summary>
    public static int LengthName = 60;

    /// <summary>The Description maximum length.</summary>
    public static int LengthDescription = 80;

    /// <summary>The TypeName maximum length.</summary>
    public static int LengthTypeName = 20;

    /// <summary>The Name maximum length.</summary>
    public static int LengthNewName = 60;
    #endregion
  }

  #region Comparers

  /// <summary>Sort and search on Name value.</summary>
  public class DataColumnUniqueComparer : IComparer<DataUtilityColumn>
  {
    // Compares two objects.
    /// <include path='items/Compare/*' file='../../LJCDocLib/Common/Data.xml'/>
    public int Compare(DataUtilityColumn x, DataUtilityColumn y)
    {
      int retValue;

      retValue = NetCommon.CompareNull(x, y);
      if (-2 == retValue)
      {
        retValue = NetCommon.CompareNull(x.Name, y.Name);
        if (-2 == retValue)
        {
          // Case sensitive.
          retValue = x.DataTableID.CompareTo(y.DataTableID);
          if (0 == retValue)
          {
            retValue = x.Name.CompareTo(y.Name);
          }

          // Not case sensitive.
          //retValue = string.Compare(x.Name, y.Name, true);
        }
      }
      return retValue;
    }
  }
  #endregion
}
