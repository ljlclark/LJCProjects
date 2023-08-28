// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// DocAppField.cs
using LJCNetCommon;
using LJCDBClientLib;
using System;
using System.Collections.Generic;

namespace LJCAppName
{
  /// <summary>The DocAppField table Data Object.</summary>
  public class DocAppField : IComparable<DocAppField>
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public DocAppField()
    {
      ChangedNames = new ChangedNames();
    }

    // The Copy constructor.
    /// <include path='items/CopyConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public DocAppField(DocAppField item)
    {
      ChangedNames = new ChangedNames();
      ID = item.ID;
    }
    #endregion

    #region Data Methods

    // Creates and returns a clone of this object.
    /// <include path='items/Clone/*' file='../../LJCDocLib/Common/Data.xml'/>
    public DocAppField Clone()
    {
      var retValue = MemberwiseClone() as DocAppField;
      return retValue;
    }

    // Provides the default Sort functionality.
    /// <include path='items/CompareTo/*' file='../../LJCDocLib/Common/Data.xml'/>
    public int CompareTo(DocAppField other)
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

    /// <summary>Gets or sets the DocAppID value.</summary>
    //[Column("DocAppID", TypeName="_DBType_")]
    public Int32 DocAppID
    {
      get { return mDocAppID; }
      set
      {
        mDocAppID = ChangedNames.Add(ColumnDocAppID, mDocAppID, value);
      }
    }
    private Int32 mDocAppID;

    /// <summary>Gets or sets the Sequence value.</summary>
    //[Column("Sequence", TypeName="_DBType_")]
    public Int16 Sequence
    {
      get { return mSequence; }
      set
      {
        mSequence = ChangedNames.Add(ColumnSequence, mSequence, value);
      }
    }
    private Int16 mSequence;

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

    /// <summary>Gets or sets the Caption value.</summary>
    //[Column("Caption", TypeName="_DBType_(100")]
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

    /// <summary>Gets or sets the DocAppFieldBehaviorID value.</summary>
    //[Column("DocAppFieldBehaviorID", TypeName="_DBType_")]
    public Int32 DocAppFieldBehaviorID
    {
      get { return mDocAppFieldBehaviorID; }
      set
      {
        mDocAppFieldBehaviorID = ChangedNames.Add(ColumnDocAppFieldBehaviorID, mDocAppFieldBehaviorID, value);
      }
    }
    private Int32 mDocAppFieldBehaviorID;

    /// <summary>Gets or sets the DataTypeName value.</summary>
    //[Column("DataTypeName", TypeName="_DBType_(30")]
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
    public Int16 MaxLength
    {
      get { return mMaxLength; }
      set
      {
        mMaxLength = ChangedNames.Add(ColumnMaxLength, mMaxLength, value);
      }
    }
    private Int16 mMaxLength;

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
    //[Column("DefaultValue", TypeName="_DBType_(30")]
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
    public static string TableName = "DocAppField";

    /// <summary>The ID column name.</summary>
    public static string ColumnID = "ID";

    /// <summary>The DocAppID column name.</summary>
    public static string ColumnDocAppID = "DocAppID";

    /// <summary>The Sequence column name.</summary>
    public static string ColumnSequence = "Sequence";

    /// <summary>The ColumnName column name.</summary>
    public static string ColumnColumnName = "ColumnName";

    /// <summary>The Caption column name.</summary>
    public static string ColumnCaption = "Caption";

    /// <summary>The Description column name.</summary>
    public static string ColumnDescription = "Description";

    /// <summary>The DocAppFieldBehaviorID column name.</summary>
    public static string ColumnDocAppFieldBehaviorID = "DocAppFieldBehaviorID";

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

    /// <summary>The Caption maximum length.</summary>
    public static int LengthCaption = 100;

    /// <summary>The Description maximum length.</summary>
    public static int LengthDescription = 100;

    /// <summary>The DataTypeName maximum length.</summary>
    public static int LengthDataTypeName = 30;

    /// <summary>The DefaultValue maximum length.</summary>
    public static int LengthDefaultValue = 30;
    #endregion

    #region Calculated and Join Class Data

    ///// <summary>The Join TypeName column name.</summary>
    //public static string ColumnTypeName = "TypeName";
    #endregion
  }

  #region Comparers

  /// <summary>Sort and search on Name value.</summary>
  public class DocAppFieldUniqueComparer : IComparer<DocAppField>
  {
    // Compares two objects.
    /// <include path='items/Compare/*' file='../../LJCDocLib/Common/Data.xml'/>
    public int Compare(DocAppField x, DocAppField y)
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
