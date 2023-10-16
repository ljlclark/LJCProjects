// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DbValue.cs
using System;
using System.Data.SqlTypes;

namespace LJCNetCommon
{
  /// <summary>Represents a data source value.</summary>
  public class DbValue : IComparable<DbValue>
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCDocLib/Common/Data.xml'/>
    public DbValue()
    {
      IsChanged = false;
    }

    // The Copy constructor.
    /// <include path='items/CopyConstructor/*' file='../../../CoreUtilities/LJCDocLib/Common/Data.xml'/>
    public DbValue(DbValue item)
    {
      DataTypeName = item.DataTypeName;
      IsChanged = item.IsChanged;
      PropertyName = item.PropertyName;
      // *** Next Statement *** Add 10/15/23
      //RenameAs = item.RenameAs;
      Value = item.Value;
    }

    // Initializes an object instance with the supplied values.
    /// <include path='items/DbValueC/*' file='Doc/DbValue.xml'/>
    public DbValue(string propertyName, object value = null
      , string dataTypeName = "String")
    {
      DataTypeName = dataTypeName;
      IsChanged = false;
      PropertyName = propertyName;
      // *** Next Statement *** Add 10/15/23
      //RenameAs = renameAs;
      Value = value;
    }
    #endregion

    #region Data Methods

    // Creates and returns a clone of the object.
    /// <include path='items/Clone/*' file='../../../CoreUtilities/LJCDocLib/Common/Data.xml'/>
    public DbValue Clone()
    {
      DbValue retValue = MemberwiseClone() as DbValue;
      return retValue;
    }

    // Provides the default Sort functionality.
    /// <include path='items/CompareTo/*' file='../../../CoreUtilities/LJCDocLib/Common/Data.xml'/>
    public int CompareTo(DbValue other)
    {
      int retValue;

      if (null == other)
      {
        // This object is larger than the "other" object.
        retValue = 1;
      }
      else
      {
        // Not case sensitive.
        retValue = string.Compare(PropertyName, other.PropertyName, true);
      }
      return retValue;
    }

    // Formats the column value for the SQL string. (D)
    /// <include path='items/FormatValue/*' file='Doc/DbColumn.xml'/>
    public string FormatValue()
    {
      string retValue = NetString.FormatValue(Value, DataTypeName);
      return retValue;
    }

    // The object string identifier.
    /// <include path='items/ToString/*' file='../../../CoreUtilities/LJCDocLib/Common/Data.xml'/>
    public override string ToString()
    {
      string retValue = mPropertyName;

      if (mValue != null)
      {
        retValue += $":{mValue}";
      }
      return retValue;
    }
    #endregion

    #region Conversions

    // Creates a combined DbColumn from a DbValue and DbColumn.
    /// <include path='items/CreateColumn/*' file='Doc/DbValue.xml'/>
    public DbColumn CreateColumn(DbColumn definitionColumn)
    {
      DbColumn retValue;

      retValue = new DbColumn()
      {
        Caption = definitionColumn.Caption,
        ColumnName = definitionColumn.ColumnName,
        DataTypeName = definitionColumn.DataTypeName,
        IsChanged = definitionColumn.IsChanged,
        MaxLength = definitionColumn.MaxLength,
        PropertyName = definitionColumn.PropertyName,
        // *** Next Statement *** Add 10/15/23
        //RenameAs = definitionColumn.RenameAs,
        Value = Value
      };
      if (0 == retValue.MaxLength)
      {
        retValue.MaxLength = 10;
      }
      if (retValue.MaxLength < 5)
      {
        retValue.MaxLength += 3;
      }
      return retValue;
    }
    #endregion

    #region Data Properties

    /// <summary>Gets or sets the DataTypeName value.</summary>
    public string DataTypeName
    {
      get { return mDataTypeName; }
      set { mDataTypeName = NetString.InitString(value); }
    }
    private string mDataTypeName;

    /// <summary>Indicates that the value has changed.</summary>
    public bool IsChanged { get; set; }

    /// <summary>Gets or sets the PropertyName value.</summary>
    public string PropertyName
    {
      get { return mPropertyName; }
      set { mPropertyName = NetString.InitString(value); }
    }
    private string mPropertyName;

    //// Gets or sets the RenameAs value.
    ///// <include path='items/RenameAs/*' file='Doc/DbColumn.xml'/>
    //// *** New Property *** Add 10/15/23
    //public string RenameAs
    //{
    //  get { return mRenameAs; }
    //  set { mRenameAs = NetString.InitString(value); }
    //}
    //private string mRenameAs;

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
  }
}
