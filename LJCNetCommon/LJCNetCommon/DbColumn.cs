// Copyright (c) Lester J. Clark 2021,2022 - All Rights Reserved
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace LJCNetCommon
{
  // Represents a database column. (D)
  /// <include path='items/DbColumn/*' file='Doc/DbColumn.xml'/>
  public class DbColumn : IComparable<DbColumn>
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public DbColumn()
    {
      DataTypeName = "String";
      IsChanged = false;
    }

    // The Copy constructor.
    /// <include path='items/CopyConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public DbColumn(DbColumn item)
    {
      AllowDBNull = item.AllowDBNull;
      AutoIncrement = item.AutoIncrement;
      Caption = item.Caption;
      ColumnName = item.ColumnName;
      DataTypeName = item.DataTypeName;
      DefaultValue = item.DefaultValue;
      IsChanged = item.IsChanged;
      IsPrimaryKey = item.IsPrimaryKey;
      MaxLength = item.MaxLength;
      Position = item.Position;
      PropertyName = item.PropertyName;
      RenameAs = item.RenameAs;
      SQLTypeName = item.SQLTypeName;
      Unique = item.Unique;
      Value = item.Value;
    }

    // Initializes an object instance with the supplied values.
    /// <include path='items/DbColumnC/*' file='Doc/DbColumn.xml'/>
    public DbColumn(string columnName, string value = null, string dataTypeName = "String"
      , string propertyName = null, bool assignedKey = false, string renameValue = null)
    {
      ColumnName = columnName;
      Value = value;
      DataTypeName = dataTypeName;
      if (NetString.HasValue(propertyName))
      {
        PropertyName = propertyName;
      }
      AutoIncrement = assignedKey;
      RenameAs = renameValue;
      IsChanged = false;
    }
    #endregion

    #region Data Methods

    // Creates and returns a clone of the object.
    /// <include path='items/Clone/*' file='../../LJCDocLib/Common/Data.xml'/>
    public DbColumn Clone()
    {
      DbColumn retValue = MemberwiseClone() as DbColumn;
      return retValue;
    }

    // Provides the default Sort functionality.
    /// <include path='items/CompareTo/*' file='../../LJCDocLib/Common/Data.xml'/>
    public int CompareTo(DbColumn other)
    {
      int retValue;

      if (null == other)
      {
        // This object is larger than the "other" object.
        retValue = 1;
      }
      else
      {
        // Case sensitive.
        retValue = AddOrderIndex.CompareTo(other.AddOrderIndex);
      }
      return retValue;
    }

    // Formats the column value for the SQL string. (D)
    /// <include path='items/FormatValue/*' file='Doc/DbColumn.xml'/>
    public string FormatValue()
    {
      string retValue;

      if (null == Value)
      {
        retValue = "null";
      }
      else
      {
        retValue = Value.ToString();
      }

      switch (DataTypeName)
      {
        case "Boolean":
          retValue = retValue == "True" ? "1" : "0";
          break;

        case "DateTime":
          DateTime dateTime = Convert.ToDateTime(retValue);
          if (IsDbMinDate(dateTime))
          {
            retValue = "null";
          }
          else
          {
            retValue = $"'{dateTime:yyyy/MM/dd HH:mm:ss}'";
          }
          break;

        case "String":
          if (retValue != "null")
          {
            retValue = retValue.Replace("'", "''");
            retValue = $"'{retValue}'";
          }
          break;
      }
      return retValue;
    }

    // The object string identifier.
    /// <include path='items/ToString/*' file='../../LJCDocLib/Common/Data.xml'/>
    public override string ToString()
    {
      string retValue = mColumnName;

      if (mPropertyName != mColumnName)
      {
        retValue += $"-{mPropertyName}";
      }
      if (IsPrimaryKey)
      {
        retValue += "-P";
      }
      if (mValue != null)
      {
        retValue += $":{mValue}";
      }
      return retValue;
    }
    #endregion

    #region Conversions

    // Creates a DbValue object from a DbColumn object. (E)
    /// <include path='items/DbValue/*' file='Doc/DbColumn.xml'/>
    public static implicit operator DbValue(DbColumn dbColumn)
    {
      DbValue retValue = null;

      if (dbColumn != null)
      {
        retValue = new DbValue()
        {
          DataTypeName = dbColumn.DataTypeName,
          IsChanged = dbColumn.IsChanged,
          PropertyName = dbColumn.PropertyName,
          Value = dbColumn.Value
        };
      }
      return retValue;
    }
    #endregion

    #region Private Methods

    // Check for DB Minimum date or less.
    private bool IsDbMinDate(DateTime dateTime)
    {
      bool retValue = false;
      if (dateTime.Year < 1753)
      {
        retValue = true;
      }
      if (1753 == dateTime.Year
        && 1 == dateTime.Month
        && 1 == dateTime.Day)
      {
        retValue = true;
      }
      return retValue;
    }
    #endregion

    #region Data Properties

    /// <summary>Gets or sets the AllowDBNull value.</summary>
    public bool AllowDBNull { get; set; }

    /// <summary>Gets or sets the AutoIncrement value.</summary>
    public bool AutoIncrement { get; set; }

    // Gets or sets the Caption value.
    /// <include path='items/Caption/*' file='Doc/DbColumn.xml'/>
    public string Caption
    {
      get { return mCaption; }
      set { mCaption = NetString.InitString(value); }
    }
    private string mCaption;

    // Gets or sets the ColumnName value.
    /// <include path='items/ColumnName/*' file='Doc/DbColumn.xml'/>
    public string ColumnName
    {
      get { return mColumnName; }
      set
      {
        mColumnName = NetString.InitString(value);

        // Set empty property name the same as the column name.
        if (NetString.HasValue(mColumnName)
          && false == NetString.HasValue(mPropertyName))
        {
          PropertyName = ColumnName;
        }
      }
    }
    private string mColumnName;

    /// <summary>Gets or sets the DataTypeName value.</summary>
    public string DataTypeName
    {
      get { return mDataTypeName; }
      set { mDataTypeName = NetString.InitString(value); }
    }
    private string mDataTypeName;

    /// <summary>Gets or sets the DefaultValue value.</summary>
    public string DefaultValue
    {
      get { return mDefaultValue; }
      set { mDefaultValue = NetString.InitString(value); }
    }
    private string mDefaultValue;

    /// <summary>Indicates that the value has changed.</summary>
    [XmlIgnore()]
    public bool IsChanged { get; set; }

    /// <summary>Gets or sets the IsPrimaryKey value.</summary>
    public bool IsPrimaryKey { get; set; }

    /// <summary>Gets or sets the KeyType value.</summary>
    // "Natural", "Natural*", "Foreign"
    public string KeyType { get; set; }

    /// <summary>Gets or sets the MaxLength value.</summary>
    public int MaxLength { get; set; }

    // Gets or sets the Position value. (R)
    /// <include path='items/Position/*' file='Doc/DbColumn.xml'/>
    public int Position { get; set; }

    // Gets or sets the PropertyName value.
    /// <include path='items/PropertyName/*' file='Doc/DbColumn.xml'/>
    public string PropertyName
    {
      get { return mPropertyName; }
      set
      {
        // Cannot change property name to null or white space.
        if (NetString.HasValue(value))
        {
          mPropertyName = NetString.InitString(value);
        }
      }
    }
    private string mPropertyName;

    // Gets or sets the RenameAs value.
    /// <include path='items/RenameAs/*' file='Doc/DbColumn.xml'/>
    public string RenameAs
    {
      get { return mRenameAs; }
      set { mRenameAs = NetString.InitString(value); }
    }
    private string mRenameAs;

    /// <summary>Gets or sets the SQLTypeName value.</summary>
    public string SQLTypeName
    {
      get { return mSQLTypeName; }
      set { mSQLTypeName = NetString.InitString(value); }
    }
    private string mSQLTypeName;

    /// <summary>Gets or sets the Unique value.</summary>
    public bool Unique { get; set; }

    /// <summary>Gets or sets the Value object.</summary>
    public object Value
    {
      get { return mValue; }
      set
      {
        if (false == NetCommon.IsEqual(mValue, value))
        {
          IsChanged = true;
          mValue = value;
        }
      }
    }
    private object mValue;
    #endregion

    #region Calculated and Join Data Properties

    // Gets or sets the ID value. (R)
    /// <include path='items/ID/*' file='Doc/DbColumn.xml'/>
    [XmlIgnore()]
    public int ID { get; set; }

    // Gets or sets the Sequence value. (R)
    /// <include path='items/Sequence/*' file='Doc/DbColumn.xml'/>
    [XmlIgnore()]
    public int Sequence { get; set; }

    // Gets or sets the ViewData ID value. (R)
    /// <include path='items/ViewDataID/*' file='Doc/DbColumn.xml'/>
    [XmlIgnore()]
    public int ViewDataID { get; set; }

    // Gets or sets the ViewJoin ID value. (R)
    /// <include path='items/ViewJoinID/*' file='Doc/DbColumn.xml'/>
    [XmlIgnore()]
    public int ViewJoinID { get; set; }

    // Gets or sets the Width value. (R)
    /// <include path='items/Width/*' file='Doc/DbColumn.xml'/>
    [XmlIgnore()]
    public int Width { get; set; }
    #endregion

    #region Class Properties

    /// <summary></summary> 
    public int AddOrderIndex { get; set; }
    #endregion
  }

  /// <summary>Sort and search on column name.</summary>
  public class DbColumnNameComparer : IComparer<DbColumn>
  {
    // Compares two objects.
    /// <include path='items/Compare/*' file='../../LJCDocLib/Common/Data.xml'/>
    public int Compare(DbColumn x, DbColumn y)
    {
      int retValue;

      retValue = NetCommon.CompareNull(x, y);
      if (-2 == retValue)
      {
        retValue = NetCommon.CompareNull(x.ColumnName, y.ColumnName);
        if (-2 == retValue)
        {
          // Case sensitive.
          retValue = x.ColumnName.CompareTo(y.ColumnName);
        }
      }
      return retValue;
    }
  }

  // Sort and search on property name.
  /// <include path='items/DbColumnPropertyComparer/*' file='Doc/DbColumn.xml'/>
  public class DbColumnPropertyComparer : IComparer<DbColumn>
  {
    // Compares two objects.
    /// <include path='items/Compare/*' file='../../LJCDocLib/Common/Data.xml'/>
    public int Compare(DbColumn x, DbColumn y)
    {
      int retValue;

      retValue = NetCommon.CompareNull(x, y);
      if (-2 == retValue)
      {
        retValue = NetCommon.CompareNull(x.PropertyName, y.PropertyName);
        if (-2 == retValue)
        {
          // Case sensitive.
          retValue = x.PropertyName.CompareTo(y.PropertyName);
        }
      }
      return retValue;
    }
  }

  // Sort and search on RenameAs value.
  /// <include path='items/DbColumnRenameAsComparer/*' file='Doc/DbColumn.xml'/>
  public class DbColumnRenameAsComparer : IComparer<DbColumn>
  {
    // Compares two objects.
    /// <include path='items/Compare/*' file='../../LJCDocLib/Common/Data.xml'/>
    public int Compare(DbColumn x, DbColumn y)
    {
      int retValue;

      retValue = NetCommon.CompareNull(x, y);
      if (-2 == retValue)
      {
        retValue = NetCommon.CompareNull(x.RenameAs, y.RenameAs);
        if (-2 == retValue)
        {
          // Case sensitive.
          retValue = x.RenameAs.CompareTo(y.RenameAs);
        }
      }
      return retValue;
    }
  }
}
