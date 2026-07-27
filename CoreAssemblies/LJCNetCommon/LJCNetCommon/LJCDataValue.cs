// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCDataValue.cs
using System.Collections.Generic;
using System.Xml.Serialization;

namespace LJCNetCommon
{
  /// <summary>Represents a data source value.</summary>
  public class LJCDataValue
  {
    #region Constructors

    // Initializes an object instance.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'
    ///  path='members/Constructor/*'/>
    public LJCDataValue()
    {
      DataTypeName = "string";
      PropertyName = "";
      Value = null;

      IsChanged = false;
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
      DataTypeName = item.DataTypeName;
      IsChanged = item.IsChanged;
      PropertyName = item.PropertyName;
      Value = item.Value;
    }
    #endregion

    #region Data Methods

    // Creates and returns a clone of the object.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'
    ///  path='members/Clone/*'/>
    public LJCDataValue Clone()
    {
      var retValue = MemberwiseClone() as LJCDataValue;
      return retValue;
    }

    // Formats the column value for the SQL string.
    /// <include file='Doc/DbColumn.xml'
    ///  path='members/FormatValue/*'/>
    public string FormatValue()
    {
      string retValue = NetString.FormatValue(Value, DataTypeName);
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
    public LJCDataColumn CreateColumn(LJCDataColumn definitionColumn)
    {
      LJCDataColumn retValue;

      retValue = new LJCDataColumn()
      {
        Caption = definitionColumn.Caption,
        ColumnName = definitionColumn.ColumnName,
        DataTypeName = definitionColumn.DataTypeName,
        IsChanged = definitionColumn.IsChanged,
        PropertyName = definitionColumn.PropertyName,
        Value = Value
      };

      if (typeof(string) == Value.GetType())
      {
        retValue.MaxLength = definitionColumn.MaxLength;
        if (0 == retValue.MaxLength)
        {
          retValue.MaxLength = 10;
        }
        if (retValue.MaxLength < 5)
        {
          retValue.MaxLength += 3;
        }
      }
      return retValue;
    }
    #endregion

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
        if (_PropertyName != newValue)
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
        if (!EqualityComparer<object>.Default.Equals(_Value, value))
        {
          IsChanged = true;
          _Value = value;
          if (value != null
            && typeof(string) == value.GetType())
          {
            _Value = value.ToString().Trim();
          }
        }
      }
    }
    private object _Value;
    #endregion

    #region Additional Properties

    // Gets or sets the changed indicator.
    /// <include file='Doc/LJCDataColumn.xml'
    ///  path='members/IsChanged/*'/>
    [XmlIgnore()]
    public bool IsChanged { get; set; }

    // Gets or sets the original value.
    /// <include file='Doc/LJCDataColumn.xml'
    ///  path='members/OriginalValue/*'/>
    public object OriginalValue
    {
      get => _OriginalValue;
      set
      {
        if (!EqualityComparer<object>.Default.Equals(_OriginalValue, value))
        {
          _OriginalValue = value;
          if (value != null
            && typeof(string) == value.GetType())
          {
            _OriginalValue = value.ToString().Trim();
          }
        }
      }
    }
    private object _OriginalValue;
    #endregion
  }
}
