// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// DBMetaDataColumn.cs
using LJCNetCommon;
using LJCDBClientLib;
using System;
using System.Collections.Generic;

namespace LJCAppName
{
  /// <summary>The DBMetaDataColumn table Data Object.</summary>
  public class DBMetaDataColumn : IComparable<DBMetaDataColumn>
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public DBMetaDataColumn()
    {
      ChangedNames = new ChangedNames();
    }

    // The Copy constructor.
    /// <include path='items/CopyConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public DBMetaDataColumn(DBMetaDataColumn item)
    {
      ChangedNames = new ChangedNames();
      ID = item.ID;
    }
    #endregion

    #region Data Methods

    // Creates and returns a clone of this object.
    /// <include path='items/Clone/*' file='../../LJCDocLib/Common/Data.xml'/>
    public DBMetaDataColumn Clone()
    {
      var retValue = MemberwiseClone() as DBMetaDataColumn;
      return retValue;
    }

    // Provides the default Sort functionality.
    /// <include path='items/CompareTo/*' file='../../LJCDocLib/Common/Data.xml'/>
    public int CompareTo(DBMetaDataColumn other)
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
        //retValue = Name.CompareTo(other.Name);

        // Not case sensitive.
        retValue = string.Compare(Name, other.Name, true);
      }
      return retValue;
    }

    // The object string identifier.
    /// <include path='items/ToString/*' file='../../LJCDocLib/Common/Data.xml'/>
    public override string ToString()
    {
      // $"{mSequence}){mName}:{mID}-{mValue}";
      return mDescription;
    }
    #endregion

    #region Data Properties

    // Update ChangedNames.Add() statements to "Property" constant
    // if property was renamed.

    /// <summary>Gets or sets the ID value.</summary>
    //[Column("ID", TypeName="_DBType_")]
    public Int32 ID
    {
      get { return mID; }
      set
      {
        mID = ChangedNames.Add(ColumnID, mID, value);
      }
    }
    private Int32 mID;

    /// <summary>Gets or sets the DBMetaDataTableID value.</summary>
    //[Column("DBMetaDataTableID", TypeName="_DBType_")]
    public Int32 DBMetaDataTableID
    {
      get { return mDBMetaDataTableID; }
      set
      {
        mDBMetaDataTableID = ChangedNames.Add(ColumnDBMetaDataTableID, mDBMetaDataTableID, value);
      }
    }
    private Int32 mDBMetaDataTableID;

    /// <summary>Gets or sets the Sequence value.</summary>
    //[Column("Sequence", TypeName="_DBType_")]
    public Int32 Sequence
    {
      get { return mSequence; }
      set
      {
        mSequence = ChangedNames.Add(ColumnSequence, mSequence, value);
      }
    }
    private Int32 mSequence;

    /// <summary>Gets or sets the ColumnName value.</summary>
    //[Column("ColumnName", TypeName="_DBType_(60")]
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

    /// <summary>Gets or sets the PropertyName value.</summary>
    //[Column("PropertyName", TypeName="_DBType_(60")]
    public String PropertyName
    {
      get { return mPropertyName; }
      set
      {
        value = NetString.InitString(value);
        mPropertyName = ChangedNames.Add(ColumnPropertyName, mPropertyName, value);
      }
    }
    private String mPropertyName;

    /// <summary>Gets or sets the Name value.</summary>
    //[Column("Name", TypeName="_DBType_(60")]
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

    /// <summary>Gets or sets the ShortCaption value.</summary>
    //[Column("ShortCaption", TypeName="_DBType_(30")]
    public String ShortCaption
    {
      get { return mShortCaption; }
      set
      {
        value = NetString.InitString(value);
        mShortCaption = ChangedNames.Add(ColumnShortCaption, mShortCaption, value);
      }
    }
    private String mShortCaption;

    /// <summary>Gets or sets the Caption value.</summary>
    //[Column("Caption", TypeName="_DBType_(60")]
    public String Caption
    {
      get { return mCaption; }
      set
      {
        value = NetString.InitString(value);
        mCaption = ChangedNames.Add(ColumnCaption, mCaption, value);
      }
    }
    private String mCaption;

    /// <summary>Gets or sets the Description value.</summary>
    //[Column("Description", TypeName="_DBType_(100")]
    public String Description
    {
      get { return mDescription; }
      set
      {
        value = NetString.InitString(value);
        mDescription = ChangedNames.Add(ColumnDescription, mDescription, value);
      }
    }
    private String mDescription;

    /// <summary>Gets or sets the DataTypeName value.</summary>
    //[Column("DataTypeName", TypeName="_DBType_(20")]
    public String DataTypeName
    {
      get { return mDataTypeName; }
      set
      {
        value = NetString.InitString(value);
        mDataTypeName = ChangedNames.Add(ColumnDataTypeName, mDataTypeName, value);
      }
    }
    private String mDataTypeName;

    /// <summary>Gets or sets the MaxLength value.</summary>
    //[Column("MaxLength", TypeName="_DBType_")]
    public Int32 MaxLength
    {
      get { return mMaxLength; }
      set
      {
        mMaxLength = ChangedNames.Add(ColumnMaxLength, mMaxLength, value);
      }
    }
    private Int32 mMaxLength;

    /// <summary>Gets or sets the AllowDBNull value.</summary>
    //[Column("AllowDBNull", TypeName="_DBType_")]
    public Boolean AllowDBNull
    {
      get { return mAllowDBNull; }
      set
      {
        mAllowDBNull = ChangedNames.Add(ColumnAllowDBNull, mAllowDBNull, value);
      }
    }
    private Boolean mAllowDBNull;

    /// <summary>Gets or sets the AutoIncrement value.</summary>
    //[Column("AutoIncrement", TypeName="_DBType_")]
    public Boolean AutoIncrement
    {
      get { return mAutoIncrement; }
      set
      {
        mAutoIncrement = ChangedNames.Add(ColumnAutoIncrement, mAutoIncrement, value);
      }
    }
    private Boolean mAutoIncrement;

    /// <summary>Gets or sets the DefaultValue value.</summary>
    //[Column("DefaultValue", TypeName="_DBType_(60")]
    public String DefaultValue
    {
      get { return mDefaultValue; }
      set
      {
        value = NetString.InitString(value);
        mDefaultValue = ChangedNames.Add(ColumnDefaultValue, mDefaultValue, value);
      }
    }
    private String mDefaultValue;

    /// <summary>Gets or sets the Unique value.</summary>
    //[Column("Unique", TypeName="_DBType_")]
    public Boolean Unique
    {
      get { return mUnique; }
      set
      {
        mUnique = ChangedNames.Add(ColumnUnique, mUnique, value);
      }
    }
    private Boolean mUnique;
    #endregion

    #region Calculated and Join Data Properties

    ///// <summary>Gets or sets the Join TypeName value.</summary>
    //public string TypeName { get; set; }
    #endregion

    #region Class Properties

    /// <summary>Gets a reference to the ChangedNames list.</summary>
    public ChangedNames ChangedNames { get; private set; }
    #endregion

    #region Class Data

    /// <summary>The table name.</summary>
    public static string TableName = "DBMetaDataColumn";

    /// <summary>The ID column name.</summary>
    public static string ColumnID = "ID";

    /// <summary>The DBMetaDataTableID column name.</summary>
    public static string ColumnDBMetaDataTableID = "DBMetaDataTableID";

    /// <summary>The Sequence column name.</summary>
    public static string ColumnSequence = "Sequence";

    /// <summary>The ColumnName column name.</summary>
    public static string ColumnColumnName = "ColumnName";

    /// <summary>The PropertyName column name.</summary>
    public static string ColumnPropertyName = "PropertyName";

    /// <summary>The Name column name.</summary>
    public static string ColumnName = "Name";

    /// <summary>The ShortCaption column name.</summary>
    public static string ColumnShortCaption = "ShortCaption";

    /// <summary>The Caption column name.</summary>
    public static string ColumnCaption = "Caption";

    /// <summary>The Description column name.</summary>
    public static string ColumnDescription = "Description";

    /// <summary>The DataTypeName column name.</summary>
    public static string ColumnDataTypeName = "DataTypeName";

    /// <summary>The MaxLength column name.</summary>
    public static string ColumnMaxLength = "MaxLength";

    /// <summary>The AllowDBNull column name.</summary>
    public static string ColumnAllowDBNull = "AllowDBNull";

    /// <summary>The AutoIncrement column name.</summary>
    public static string ColumnAutoIncrement = "AutoIncrement";

    /// <summary>The DefaultValue column name.</summary>
    public static string ColumnDefaultValue = "DefaultValue";

    /// <summary>The Unique column name.</summary>
    public static string ColumnUnique = "Unique";

    /// <summary>The ColumnName maximum length.</summary>
    public static int LengthColumnName = 60;

    /// <summary>The PropertyName maximum length.</summary>
    public static int LengthPropertyName = 60;

    /// <summary>The Name maximum length.</summary>
    public static int LengthName = 60;

    /// <summary>The ShortCaption maximum length.</summary>
    public static int LengthShortCaption = 30;

    /// <summary>The Caption maximum length.</summary>
    public static int LengthCaption = 60;

    /// <summary>The Description maximum length.</summary>
    public static int LengthDescription = 100;

    /// <summary>The DataTypeName maximum length.</summary>
    public static int LengthDataTypeName = 20;

    /// <summary>The DefaultValue maximum length.</summary>
    public static int LengthDefaultValue = 60;
    #endregion

    #region Calculated and Join Class Data

    ///// <summary>The Join TypeName column name.</summary>
    //public static string ColumnTypeName = "TypeName";
    #endregion
  }

  #region Comparers

  /// <summary>Sort and search on Name value.</summary>
  public class DBMetaDataColumnUniqueComparer : IComparer<DBMetaDataColumn>
  {
    // Compares two objects.
    /// <include path='items/Compare/*' file='../../LJCDocLib/Common/Data.xml'/>
    public int Compare(DBMetaDataColumn x, DBMetaDataColumn y)
    {
      int retValue;

      retValue = NetCommon.CompareNull(x, y);
      if (-2 == retValue)
      {
        retValue = NetCommon.CompareNull(x._ComparerName_, y._ComparerName_);
        if (-2 == retValue)
        {
          // Case sensitive.
          //retValue = x._ComparerName_.CompareTo(y._ComparerName_);

          // Not case sensitive.
          retValue = string.Compare(x._ComparerName_, y._ComparerName_, true);
        }
      }
      return retValue;
    }
  }
  #endregion
}
