// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// DataUtilColumn.cs
using LJCNetCommon;
using LJCDBClientLib;
using System;
using System.Collections.Generic;
using LJCDataUtilityDAL;

namespace LJCDataUtilityDAL
{
  /// <summary>The DataColumn Data Object.</summary>
  public class DataUtilColumn : IComparable<DataUtilColumn>
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public DataUtilColumn()
    {
      ChangedNames = new ChangedNames();
    }

    // Initialize with main values.
    public DataUtilColumn(string name, string typeName
      , bool allowNull = true, short maxLength = 0
      , string defaultValue = null, short identityIncrement = 0)
    {
      ChangedNames = new ChangedNames();
      Name = name;
      TypeName = typeName;
      AllowNull = allowNull;
      MaxLength = maxLength;
      DefaultValue = defaultValue;
      IdentityStart = 0;
      IdentityIncrement = identityIncrement;
      if (IdentityIncrement > 0)
      {
        IdentityStart = 1;
      }
    }

    // The Copy constructor.
    /// <include path='items/CopyConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public DataUtilColumn(DataUtilColumn item)
    {
      ChangedNames = new ChangedNames();
      ID = item.ID;
      DataSiteID = item.DataSiteID;
      DataTableID = item.DataTableID;
      DataTableSiteID = item.DataTableSiteID;
      Name = item.Name;
      Description = item.Description;
      Sequence = item.Sequence;
      TypeName = item.TypeName;
      MaxLength = item.MaxLength;
      AllowNull = item.AllowNull;
      DefaultValue = item.DefaultValue;
      IdentityStart = item.IdentityStart;
      IdentityIncrement = item.IdentityIncrement;
      NewName = item.NewName;
      NewMaxLength = item.NewMaxLength;
    }
    #endregion

    #region Data Class Methods

    // Adds changed propertynames.
    public void AddChangedNames(List<string> propertyNames)
    {
      foreach (string propertyName in propertyNames)
      {
        var name = ChangedNames.FindName(propertyName);
        if (null == name)
        {
          ChangedNames.Add(propertyName);
        }
      }
    }

    // Creates and returns a clone of this object.
    /// <include path='items/Clone/*' file='../../LJCDocLib/Common/Data.xml'/>
    public DataUtilColumn Clone()
    {
      var retValue = MemberwiseClone() as DataUtilColumn;
      return retValue;
    }

    // Provides the default Sort functionality.
    /// <include path='items/CompareTo/*' file='../../LJCDocLib/Common/Data.xml'/>
    public int CompareTo(DataUtilColumn other)
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
        if (retValue != 0)
        {
          isContinue = false;
        }
      }
      if (isContinue)
      {
        retValue = DataSiteID.CompareTo(other.DataSiteID);
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
    //[Column("ID", TypeName="bigint")]
    public Int64 ID
    {
      get { return mID; }
      set
      {
        mID = ChangedNames.Add(ColumnID, mID, value);
      }
    }
    private Int64 mID;

    /// <summary>Gets or sets the ID value.</summary>
    //[Required]
    //[Column("ID", TypeName="bigint")]
    public Int64 DataSiteID
    {
      get { return mDataSiteID; }
      set
      {
        mDataSiteID = ChangedNames.Add(ColumnDataSiteID, mDataSiteID, value);
      }
    }
    private Int64 mDataSiteID;

    /// <summary>Gets or sets the DataTableID value.</summary>
    //[Required]
    //[Column("DataTableID", TypeName="bigint")]
    public Int64 DataTableID
    {
      get { return mDataTableID; }
      set
      {
        mDataTableID = ChangedNames.Add(ColumnDataTableID
          , mDataTableID, value);
      }
    }
    private Int64 mDataTableID;

    /// <summary>Gets or sets the DataTableID value.</summary>
    //[Required]
    //[Column("DataTableSiteID", TypeName="bigint")]
    public Int64 DataTableSiteID
    {
      get { return mDataTableSiteID; }
      set
      {
        mDataTableSiteID = ChangedNames.Add(ColumnDataTableSiteID
          , mDataTableSiteID, value);
      }
    }
    private Int64 mDataTableSiteID;

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
    //[Required]
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

    /// <summary>Gets or sets the Default value.</summary>
    //[Column("DefaultValue", TypeName="nvarchar(30)")]
    public string DefaultValue
    {
      get { return mDefaultValue; }
      set
      {
        mDefaultValue = ChangedNames.Add(ColumnDefaultValue
          , mDefaultValue, value);
      }
    }
    private string mDefaultValue;

    /// <summary>Gets or sets the IdentityStart value.</summary>
    //[Required]
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
    //[Required]
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

    /// <summary>Gets or sets the NewName value.</summary>
    //[Column("NewName", TypeName="nvarchar(60")]
    public String NewName
    {
      get { return mNewName; }
      set
      {
        value = NetString.InitString(value);
        mNewName = ChangedNames.Add(ColumnNewName
          , mNewName, value);
      }
    }
    private String mNewName;

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

    /// <summary>The ID column name.</summary>
    public static string ColumnDataSiteID = "DataSiteID";

    /// <summary>The DataTableID column name.</summary>
    public static string ColumnDataTableID = "DataTableID";

    /// <summary>The DataTableSiteID column name.</summary>
    public static string ColumnDataTableSiteID = "DataTableSiteID";

    /// <summary>The Name column name.</summary>
    public static string ColumnName = "Name";

    /// <summary>The Description column name.</summary>
    public static string ColumnDescription = "Description";

    /// <summary>The Sequence column name.</summary>
    public static string ColumnSequence = "Sequence";

    /// <summary>The TypeName column name.</summary>
    public static string ColumnTypeName = "TypeName";

    /// <summary>The IdentityStart column name.</summary>
    public static string ColumnIdentityStart = "IdentityStart";

    /// <summary>The IdentityIncrement column name.</summary>
    public static string ColumnIdentityIncrement = "IdentityIncrement";

    /// <summary>The MaxLength column name.</summary>
    public static string ColumnMaxLength = "MaxLength";

    /// <summary>The AllowNull column name.</summary>
    public static string ColumnAllowNull = "AllowNull";

    /// <summary>The DefaultValue column name.</summary>
    public static string ColumnDefaultValue = "DefaultValue";

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

    /// <summary>The Sequence maximum length.</summary>
    public static int LengthSequence = 3;

    /// <summary>The IdentityStart maximum length.</summary>
    public static int LengthIdentityStart = 3;

    /// <summary>The IdentityIncrement maximum length.</summary>
    public static int LengthIdentityIncrement = 3;

    /// <summary>The MaxLength maximum length.</summary>
    public static int LengthMaxLength = 5;

    /// <summary>The MaxLength maximum length.</summary>
    public static int LengthDefaultValue = 30;
    #endregion
  }

  #region Comparers

  /// <summary>Sort and search on Name value.</summary>
  public class DataColumnUniqueComparer : IComparer<DataUtilColumn>
  {
    // Compares two objects.
    /// <include path='items/Compare/*' file='../../LJCDocLib/Common/Data.xml'/>
    public int Compare(DataUtilColumn x, DataUtilColumn y)
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
        retValue = x.DataTableID.CompareTo(y.DataTableID);
        if (retValue != 0)
        {
          isContinue = false;
        }
      }
      if (isContinue)
      {
        retValue = x.DataTableSiteID.CompareTo(y.DataTableSiteID);
      }
      return retValue;
    }
  }
  #endregion
}
