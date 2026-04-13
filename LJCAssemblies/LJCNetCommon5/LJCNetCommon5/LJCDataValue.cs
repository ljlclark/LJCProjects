// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCDataValue.cs

namespace LJCNetCommon5
{
  // Represents a data source value.
  /// <include path="members/LJCDataValue/*" file="../../../CoreUtilities/LJCGenDoc/Common/Data.xml"/>
  /// <group name="constructors">Constructors</group>
  /// <group name="data">Data Methods</group>
  /// <group name="conversions">Conversions</group>
  /// <group name="dataProperties">Data Properties</group>
  public class LJCDataValue : IComparable<LJCDataValue>
  {
    #region Constructor Methods

    // Initializes an object instance.
    /// <include path="members/Constructor/*" file="../../../CoreUtilities/LJCGenDoc/Common/Data.xml"/>
    /// <parentGroup>constructors</parentGroup>
    public LJCDataValue()
    {
      IsChanged = false;
    }

    // The Copy constructor.
    /// <include path="members/CopyConstructor/*" file="../../../CoreUtilities/LJCGenDoc/Common/Data.xml"/>
    /// <parentGroup>constructors</parentGroup>
    public LJCDataValue(LJCDataValue item)
    {
      DataTypeName = item.DataTypeName;
      IsChanged = item.IsChanged;
      PropertyName = item.PropertyName;
      Value = item.Value;
    }

    // Initializes an object instance with the supplied values.
    /// <include path="members/ParamConstructor/*" file="Doc/LJCDataValue.xml"/>
    /// <parentGroup>constructors</parentGroup>
    public LJCDataValue(string propertyName, object? value = null
      , string dataTypeName = "String")
    {
      DataTypeName = dataTypeName;
      //IsChanged = false;
      PropertyName = propertyName;
      Value = value;
      IsChanged = false;
    }
    #endregion

    #region Data Methods

    // Creates and returns a clone of the object.
    /// <include path="members/Clone/*" file="../../../CoreUtilities/LJCGenDoc/Common/Data.xml"/>
    /// <parentGroup>data</parentGroup>
    public LJCDataValue? Clone()
    {
      var retValue = MemberwiseClone() as LJCDataValue;
      return retValue;
    }

    // Provides the default Sort functionality.
    /// <include path="members/CompareTo/*" file="../../../CoreUtilities/LJCGenDoc/Common/Data.xml"/>
    /// <parentGroup>data</parentGroup>
    public int CompareTo(LJCDataValue? other)
    {
      int retValue;

      if (null == other)
      {
        // This object is greater than the "other" object.
        retValue = LJCNetString.CompareGreater;
      }
      else
      {
        // Not case sensitive.
        retValue = string.Compare(PropertyName, other.PropertyName, true);
      }
      return retValue;
    }

    // Formats the column value for the SQL string.
    /// <include path="members/FormatValue/*" file="Doc/LJCDataColumn.xml"/>
    /// <parentGroup>data</parentGroup>
    public string? FormatValue()
    {
      string retValue = LJCNetString.FormatValue(Value, DataTypeName);
      return retValue;
    }

    // The object string identifier.
    /// <include path="members/ToString/*" file="../../../CoreUtilities/LJCGenDoc/Common/Data.xml"/>
    /// <parentGroup>data</parentGroup>
    public override string? ToString()
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

    // Creates a combined LJCDataColumn from an LJCDataValue and LJCDataColumn.
    /// <include path="members/CreateColumn/*" file="Doc/LJCDataValue.xml"/>
    /// <parentGroup>conversions</parentGroup>
    public LJCDataColumn? CreateColumn(LJCDataColumn dataColumn)
    {
      LJCDataColumn retValue = null;

      if (dataColumn.PropertyName == PropertyName)
      {
        retValue = new LJCDataColumn()
        {
          Caption = dataColumn.Caption,
          ColumnName = dataColumn.ColumnName,
          DataTypeName = dataColumn.DataTypeName,
          IsChanged = dataColumn.IsChanged,
          MaxLength = dataColumn.MaxLength,
          Value = Value
        };
        if (retValue.DataTypeName == LJC.TypeString)
        {
          if (0 == retValue.MaxLength)
          {
            retValue.MaxLength = 10;
          }
          if (retValue.MaxLength < 5)
          {
            retValue.MaxLength += 3;
          }
        }
      }
      return retValue;
    }
    #endregion

    #region Data Properties

    // Gets or sets the DataTypeName value.
    /// <include path="members/DataTypeName/*" file="Doc/LJCDataValue.xml"/>
    /// <parentGroup>dataProperties</parentGroup>
    public string? DataTypeName
    {
      get { return mDataTypeName; }
      set { mDataTypeName = LJCNetString.InitString(value); }
    }
    private string? mDataTypeName;

    // Indicates that the value has changed.
    /// <include path="members/IsChanged/*" file="Doc/LJCDataValue.xml"/>
    /// <parentGroup>dataProperties</parentGroup>
    public bool IsChanged { get; set; }

    // Gets or sets the PropertyName value.
    /// <include path="members/PropertyName/*" file="Doc/LJCDataValue.xml"/>
    /// <parentGroup>dataProperties</parentGroup>
    public string? PropertyName
    {
      get { return mPropertyName; }
      set { mPropertyName = LJCNetString.InitString(value); }
    }
    private string? mPropertyName;

    // Gets or sets the Value object.
    /// <include path="members/Value/*" file="Doc/LJCDataValue.xml"/>
    /// <parentGroup>dataProperties</parentGroup>
    public object? Value
    {
      get { return mValue; }
      set
      {
        if (!LJC.IsEqual(mValue, value))
        {
          IsChanged = true;
          mValue = value;
        }
      }
    }
    private object? mValue;
    #endregion
  }
}
