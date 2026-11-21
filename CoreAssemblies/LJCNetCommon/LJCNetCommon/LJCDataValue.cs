// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCDataValue.cs
using System.Xml.Serialization;

namespace LJCNetCommon
{
  // Represents a data source value.
  /// <include file='Doc/LJCDataValue.xml'
  ///  path='members/LJCDataValue/*'/>
  public class LJCDataValue
  {
    #region Data Properties

    // Gets or sets the DataTypeName value.
    /// <include file='Doc/LJCDataValue.xml'
    ///  path='members/DataTypeName/*'/>
    public string DataTypeName
    {
      get => _DataTypeName;
      set
      {
        var newValue = value?.Trim();
        if (_DataTypeName != newValue)
        {
          _DataTypeName = newValue;
        }
      }
    }
    private string _DataTypeName;

    // Gets or sets the PropertyName value.
    /// <include file='Doc/LJCDataValue.xml'
    ///  path='members/PropertyName/*'/>
    public string PropertyName
    {
      get => _PropertyName;
      set
      {
        var newValue = value?.Trim();
        if (_PropertyName != newValue
          && LJC.HasText(newValue))
        {
          _PropertyName = newValue;
        }
      }
    }
    private string _PropertyName;

    // Gets or sets the Value object.
    /// <include file='Doc/LJCDataValue.xml'
    ///  path='members/Value/*'/>
    public object Value
    {
      get => _Value;
      set
      {
        if (!LJC.IsEqual(_Value, value))
        {
          _Value = value;
          if (value != null
            && typeof(string) == value.GetType())
          {
            var newValue = (string)value;
            _Value = newValue?.Trim();
          }

          IsChanged = false;
          if (!LJC.IsEqual(OriginalValue, value))
          {
            IsChanged = true;
          }
        }
      }
    }
    private object _Value;
    #endregion

    #region Properties

    // Gets or sets the changed indicator.
    /// <include file='Doc/LJCDataValue.xml'
    ///  path='members/IsChanged/*'/>
    [XmlIgnore()]
    public bool IsChanged { get; set; }

    // Gets or sets the original value.
    /// <include file='Doc/LJCDataValue.xml'
    ///  path='members/OriginalValue/*'/>
    public object OriginalValue
    {
      get => _OriginalValue;
      set
      {
        if (!LJC.IsEqual(_OriginalValue, value))
        {
          _OriginalValue = value;
          if (value != null
            && typeof(string) == value.GetType())
          {
            var newValue = (string)value;
            _OriginalValue = newValue?.Trim();
          }
        }
      }
    }
    private object _OriginalValue;
    #endregion

    #region Class Data

    // The DataTypeName column name.
    /// <include file='Doc/LJCDataValue.xml'
    ///  path='members/ColumnDataTypeName/*'/>
    public const string ColumnDataTypeName = "DataTypeName";

    // The PropertyName column name.
    /// <include file='Doc/LJCDataValue.xml'
    ///  path='members/ColumnPropertyName/*'/>
    public const string ColumnPropertyName = "PropertyName";

    // The Value column name.
    /// <include file='Doc/LJCDataValue.xml'
    ///  path='members/ColumnValue/*'/>
    public const string ColumnValue = "Value";
    #endregion

    #region Constructor Methods

    // Initializes an object instance.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'
    ///  path='members/Constructor/*'/>
    public LJCDataValue()
    {
      // Data Properties
      _DataTypeName = "string";
      _PropertyName = "";
      _Value = null;

      // Properties
      IsChanged = false;
      _OriginalValue = null;
    }

    // Initializes an object instance with the supplied values.
    /// <include file='Doc/LJCDataValue.xml'
    ///  path='members/ParamConstructor/*'/>
    public LJCDataValue(string propertyName, object value = null
      , string dataTypeName = "string") : this()
    {
      PropertyName = propertyName;
      Value = value;
      DataTypeName = dataTypeName;
    }

    // The Copy constructor.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'
    ///  path='members/CopyConstructor/*'/>
    public LJCDataValue(LJCDataValue item)
    {
      // Data Properties
      _DataTypeName = item.DataTypeName;
      _PropertyName = item.PropertyName;
      Value = item.Value;

      // Properties
      IsChanged = item.IsChanged;
      OriginalValue = item.OriginalValue;
    }
    #endregion

    #region Data Class Methods

    // Creates and returns a clone of the object.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'
    ///  path='members/Clone/*'/>
    public LJCDataValue Clone()
    {
      var retValue = MemberwiseClone() as LJCDataValue;
      return retValue;
    }

    // Formats the column value for the SQL string.
    /// <include file='Doc/LJCDataValue.xml'
    ///  path='members/FormatValue/*'/>
    public string FormatValue()
    {
      string retValue = LJCNetString.FormatValue(Value, DataTypeName);
      return retValue;
    }

    // The object string identifier.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'
    ///  path='members/ToString/*'/>
    public override string ToString()
    {
      string retValue = _PropertyName;

      if (_Value != null)
      {
        retValue += $":{_Value}";
      }
      return retValue;
    }

    // Creates a combined LJCDataColumn from a LJCDataValue and LJCDataColumn.
    /// <include file='Doc/LJCDataValue.xml'
    ///  path='members/CreateColumn/*'/>
    public LJCDataColumn CreateColumn(LJCDataColumn dataColumn)
    {
      LJCDataColumn retColumn;

      retColumn = new LJCDataColumn()
      {
        Caption = dataColumn.Caption,
        ColumnName = dataColumn.ColumnName,
        DataTypeName = dataColumn.DataTypeName,
        IsChanged = dataColumn.IsChanged,
        PropertyName = dataColumn.PropertyName,
        Value = Value
      };

      if (Value != null
        && typeof(string) == Value.GetType())
      {
        retColumn.MaxLength = dataColumn.MaxLength;
        if (0 == retColumn.MaxLength)
        {
          retColumn.MaxLength = 10;
        }
        if (retColumn.MaxLength < 5)
        {
          retColumn.MaxLength += 3;
        }
      }
      return retColumn;
    }
    #endregion
  }
}
