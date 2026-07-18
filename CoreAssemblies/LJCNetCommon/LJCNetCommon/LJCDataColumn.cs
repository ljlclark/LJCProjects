// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCDataColumn.cs
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace LJCNetCommon
{
  // Represents a data source column.
  /// <include file='Doc/LJCDataColumn.xml'
  ///  path='members/LJCDataColumn/*'/>
  public class LJCDataColumn : IComparable<LJCDataColumn>
  {
    #region Constructor Methods

    // Initializes an object instance.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'
    ///  path='members/Constructor/*'/>
    public LJCDataColumn()
    {
      // Data Properties
      AllowDBNull = false;
      AutoIncrement = false;
      _Caption = "";
      _ColumnName = "";
      _DataTypeName = "string";
      MaxLength = 0;
      Position = -1;
      _PropertyName = "";
      _RenameAs = null;
      _SQLTypeName = null;
      _Value = null;

      // Additional Properties
      AddOrderIndex = -1;
      _DefaultValue = null;
      IsChanged = false;
      IsPrimaryKey = false;
      _KeyType = null;
      _OriginalValue = null;
      Unique = false;

      // View Join Data Properties
      ID = 0;
      Sequence = 0;
      ViewDataID = 0;
      ViewJoinID = 0;
      Width = 0;
    }

    // Initializes an object instance with the supplied values.
    /// <include file='Doc/LJCDataColumn.xml'
    ///  path='members/ParamConstructor/*'/>
    public LJCDataColumn(string propertyName, string value = null
      , string dataTypeName = "string", string columnName = null
      , bool assignedKey = false, string renameValue = null) : this()
    {
      PropertyName = propertyName;
      Value = value;
      DataTypeName = dataTypeName;
      if (NetString.HasValue(columnName))
      {
        ColumnName = columnName;
      }
      AutoIncrement = assignedKey;
      RenameAs = renameValue;
    }

    // Initializes an object instance from the supplied object.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'
    ///  path='members/CopyConstructor/*'/>
    public LJCDataColumn(LJCDataColumn item)
    {
      // Data Properties
      AllowDBNull = item.AllowDBNull;
      AutoIncrement = item.AutoIncrement;
      Caption = item.Caption;
      ColumnName = item.ColumnName;
      DataTypeName = item.DataTypeName;
      MaxLength = item.MaxLength;
      Position = item.Position;
      PropertyName = item.PropertyName;
      RenameAs = item.RenameAs;
      SQLTypeName = item.SQLTypeName;
      Value = item.Value;

      // Additional Properties
      AddOrderIndex = item.AddOrderIndex;
      DefaultValue = item.DefaultValue;
      IsChanged = item.IsChanged;
      IsPrimaryKey = item.IsPrimaryKey;
      KeyType = item.KeyType;
      OriginalValue = item.OriginalValue;
      Unique = item.Unique;

      // View Join Data Properties
      ID = item.ID;
      Sequence = item.Sequence;
      ViewDataID = item.ViewDataID;
      ViewJoinID = item.ViewJoinID;
      Width = item.Width;
    }
    #endregion

    #region Data Methods

    // Creates and returns a clone of the object.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'
    ///  path='members/Clone/*'/>
    public LJCDataColumn Clone()
    {
      var retValue = MemberwiseClone() as LJCDataColumn;
      return retValue;
    }

    // Provides the default Sort functionality.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'
    ///  path='members/CompareTo/*'/>
    public int CompareTo(LJCDataColumn other)
    {
      int retValue;

      while (true)
      {
        if (null == other)
        {
          // This object is larger than the "other" object.
          retValue = 1;
          break;
        }

        retValue = AddOrderIndex.CompareTo(other.AddOrderIndex);
        break;
      }
      return retValue;
    }

    // Formats the column value for an SQL string.
    /// <include file='Doc/LJCDataColumn.xml'
    ///  path='members/FormatValue/*'/>
    public string FormatValue()
    {
      string retValue = NetString.FormatValue(Value, DataTypeName);
      return retValue;
    }

    // Returns the object string identifier.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'
    ///  path='members/ToString/*'/>
    public override string ToString()
    {
      string retValue = _PropertyName;

      if (_ColumnName != _PropertyName)
      {
        retValue += $"-{_ColumnName}";
      }
      if (IsPrimaryKey)
      {
        retValue += "-P";
      }
      if (_Value != null)
      {
        retValue += $":{_Value}";
      }
      return retValue;
    }

    // Creates a LJCDataValue object from an LJCDataColumn object.
    /// <include file='Doc/LJCDataColumn.xml'
    ///  path='members/LJCDataValue/*'/>
    public static implicit operator LJCDataValue(LJCDataColumn dataColumn)
    {
      LJCDataValue retValue = null;

      if (dataColumn != null)
      {
        retValue = new LJCDataValue()
        {
          DataTypeName = dataColumn.DataTypeName,
          IsChanged = dataColumn.IsChanged,
          PropertyName = dataColumn.PropertyName,
          Value = dataColumn.Value
        };
      }
      return retValue;
    }
    #endregion

    #region Data Properties

    // Gets or sets the AllowDBNull flag.
    /// <include file='Doc/LJCDataColumn.xml'
    ///  path='members/AllowDBNull/*'/>
    public bool AllowDBNull { get; set; }

    // Gets or sets the AutoIncrement flag.
    /// <include file='Doc/LJCDataColumn.xml'
    ///  path='members/AutoIncrement/*'/>
    public bool AutoIncrement { get; set; }

    // Gets or sets the Caption value.
    /// <include file='Doc/LJCDataColumn.xml'
    ///  path='members/Caption/*'/>
    public string Caption
    {
      get => _Caption;
      set
      {
        var newValue = value?.Trim();
        if (_Caption != newValue)
        {
          _Caption = newValue;
        }
      }
    }
    private string _Caption;

    // Gets or sets the ColumnName value.
    /// <include file='Doc/LJCDataColumn.xml'
    ///  path='members/ColumnName/*'/>
    public string ColumnName
    {
      get => _ColumnName;
      set
      {
        var newValue = value?.Trim();

        // Cannot change column name to null or white space.
        if (_ColumnName != newValue
          && NetString.HasValue(newValue))
        {
          _ColumnName = value.ToString().Trim();

          // Set empty property name the same as the column name.
          if (!NetString.HasValue(_PropertyName))
          {
            PropertyName = ColumnName;
          }
        }
      }
    }
    private string _ColumnName;

    // Gets or sets the DataTypeName value.
    /// <include file='Doc/LJCDataColumn.xml'
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

    // Gets or sets the MaxLength value.
    /// <include file='Doc/LJCDataColumn.xml'
    ///  path='members/MaxLength/*'/>
    public int MaxLength { get; set; }

    // Gets or sets the Fixed Length Field Position value.
    /// <include file='Doc/LJCDataColumn.xml'
    ///  path='members/Position/*'/>
    public int Position { get; set; }

    // Gets or sets the PropertyName value.
    /// <include file='Doc/LJCDataColumn.xml'
    ///  path='members/PropertyName/*'/>
    public string PropertyName
    {
      get => _PropertyName;
      set
      {
        var newValue = value?.Trim();

        // Cannot change property name to null or white space.
        if (_PropertyName != newValue
          && NetString.HasValue(newValue))
        {
          _PropertyName = newValue;

          // Set empty column name the same as the property name.
          if (!NetString.HasValue(_ColumnName))
          {
            ColumnName = PropertyName;
          }
        }
      }
    }
    private string _PropertyName;

    // Gets or sets the RenameAs value.
    /// <include file='Doc/LJCDataColumn.xml'
    ///  path='members/RenameAs/*'/>
    public string RenameAs
    {
      get => _RenameAs;
      set
      {
        var newValue = value?.Trim();
        if (_RenameAs != newValue)
        {
          _RenameAs = newValue;
        }
      }
    }
    private string _RenameAs;

    // Gets or sets the SQLTypeName value.
    /// <include file='Doc/LJCDataColumn.xml'
    ///  path='members/SQLTypeName/*'/>
    public string SQLTypeName
    {
      get => _SQLTypeName;
      set
      {
        var newValue = value?.Trim();
        if (_SQLTypeName != newValue)
        {
          _SQLTypeName = newValue;
        }
      }
    }
    private string _SQLTypeName;

    // Gets or sets the Value object.
    /// <include file='Doc/LJCDataColumn.xml'
    ///  path='members/Value/*'/>
    public object Value
    {
      get => _Value;
      set
      {
        //if (_Value != value)
        if (!EqualityComparer<object>.Default.Equals(_Value, value))
        {
          _Value = value;
          if (typeof(string) == value.GetType()
            && value != null)
          {
            _Value = value.ToString().Trim();
          }

          IsChanged = false;
          if (!EqualityComparer<object>.Default.Equals(OriginalValue, _Value))
          {
            IsChanged = true;
          }
        }
      }
    }
    private object _Value;
    #endregion

    #region Additional Properties

    // Gets or sets the add order index.
    /// <include file='Doc/LJCDataColumn.xml'
    ///  path='members/AddOrderIndex/*'/>
    public int AddOrderIndex { get; set; }

    // Gets or sets the default value.
    /// <include file='Doc/LJCDataColumn.xml'
    ///  path='members/DefaultValue/*'/>
    public string DefaultValue
    {
      get => _DefaultValue;
      set
      {
        var newValue = value?.Trim();
        if (_DefaultValue != newValue)
        {
          _DefaultValue = value;
        }
      }
    }
    private string _DefaultValue;

    // Gets or sets the changed indicator.
    /// <include file='Doc/LJCDataColumn.xml'
    ///  path='members/IsChanged/*'/>
    [XmlIgnore()]
    public bool IsChanged { get; set; }

    // Gets or sets the primary key indicator.
    /// <include file='Doc/LJCDataColumn.xml'
    ///  path='members/IsPrimaryKey/*'/>
    public bool IsPrimaryKey { get; set; }

    // Gets or sets the KeyType value.
    // "Natural", "Natural*", "Foreign"
    /// <include file='Doc/LJCDataColumn.xml'
    ///  path='members/KeyType/*'/>
    public string KeyType
    {
      get => _KeyType;
      set
      {
        var newValue = value?.Trim();
        if (_KeyType != newValue)
        {
          _KeyType = newValue;
        }
      }
    }
    private string _KeyType;

    // Gets or sets the original value.
    /// <include file='Doc/LJCDataColumn.xml'
    ///  path='members/OriginalValue/*'/>
    public object OriginalValue
    {
      get => _OriginalValue;
      set
      {
        //if (_OriginalValue != value)
        if (!EqualityComparer<object>.Default.Equals(_OriginalValue, value))
        {
          _OriginalValue = value;
          if (typeof(string) == value.GetType()
            && value != null)
          {
            _OriginalValue = value.ToString().Trim();
          }
        }
      }
    }
    private object _OriginalValue;

    // Gets or sets the unique key indicator.
    /// <include file='Doc/LJCDataColumn.xml'
    ///  path='members/Unique/*'/>
    public bool Unique { get; set; }
    #endregion

    #region View Join Data Properties

    // Gets or sets the ID value.
    /// <include file='Doc/LJCDataColumn.xml'
    ///  path='members/ID/*'/>
    [XmlIgnore()]
    public int ID { get; set; }

    // Gets or sets the Sequence value.
    /// <include file='Doc/LJCDataColumn.xml'
    ///  path='members/Sequence/*'/>
    [XmlIgnore()]
    public int Sequence { get; set; }

    // Gets or sets the ViewData ID value.
    /// <include file='Doc/LJCDataColumn.xml'
    ///  path='members/ViewDataID/*'/>
    [XmlIgnore()]
    public int ViewDataID { get; set; }

    // Gets or sets the ViewJoin ID value.
    /// <include file='Doc/LJCDataColumn.xml'
    ///  path='members/ViewJoinID/*'/>
    [XmlIgnore()]
    public int ViewJoinID { get; set; }

    // Gets or sets the Width value.
    /// <include file='Doc/LJCDataColumn.xml'
    ///  path='members/Width/*'/>
    [XmlIgnore()]
    public int Width { get; set; }
    #endregion

    #region Class Data

    /// <summary>The AllowDBNull column name.</summary>
    public static string ColumnAllowDBNull = "AllowDBNull";

    /// <summary>The AutoIncrement column name.</summary>
    public static string ColumnAutoIncrement = "AutoIncrement";

    /// <summary>The Caption column name.</summary>
    public static string ColumnCaption = "Caption";

    /// <summary>The ColumnName column name.</summary>
    public static string ColumnColumnName = "ColumnName";

    /// <summary>The DataTypeName column name.</summary>
    public static string ColumnDataTypeName = "DataTypeName";

    /// <summary>The MaxLength column name.</summary>
    public static string ColumnMaxLength = "MaxLength";

    /// <summary>The Position column name.</summary>
    public static string ColumnPosition = "Position";

    /// <summary>The PropertyName column name.</summary>
    public static string ColumnPropertyName = "PropertyName";

    /// <summary>The RenameAs column name.</summary>
    public static string ColumnRenameAs = "RenameAs";

    /// <summary>The SQLTypeName column name.</summary>
    public static string ColumnSQLTypeName = "SQLTypeName";

    /// <summary>The Value column name.</summary>
    public static string ColumnValue = "Value";
    #endregion
  }

  #region Comparers

  /// <summary>Sort and search on column name.</summary>
  public class DataColumnNameComparer : IComparer<LJCDataColumn>
  {
    // Compares two objects.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'
    ///  path='members/Compare/*'/>
    public int Compare(LJCDataColumn x, LJCDataColumn y)
    {
      int retValue;

      while (true)
      {
        retValue = NetCommon.CompareNull(x, y);
        if (retValue != NetString.CompareNotNull)
        {
          break;
        }

        retValue = NetCommon.CompareNull(x.ColumnName, y.ColumnName);
        if (retValue != NetString.CompareNotNull)
        {
          break;
        }

        retValue = x.ColumnName.CompareTo(y.ColumnName);
        break;
      }
      return retValue;
    }
  }

  // Sort and search on property name.
  /// <include file='Doc/LJCDataColumn.xml'
  ///  path='members/DataColumnPropertyComparer/*'/>
  public class DataColumnPropertyComparer : IComparer<LJCDataColumn>
  {
    // Compares two objects.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'
    ///  path='members/Compare/*'/>
    public int Compare(LJCDataColumn x, LJCDataColumn y)
    {
      int retValue;

      while (true)
      {
        retValue = NetCommon.CompareNull(x, y);
        if (retValue != NetString.CompareNotNull)
        {
          break;
        }

        retValue = NetCommon.CompareNull(x.PropertyName, y.PropertyName);
        if (retValue != NetString.CompareNotNull)
        {
          break;
        }

        retValue = x.PropertyName.CompareTo(y.PropertyName);
        break;
      }
      return retValue;
    }
  }

  // Sort and search on RenameAs value.
  /// <include file='Doc/LJCDataColumn.xml'
  ///  path='members/DataColumnRenameAsComparer/*'/>
  public class DataColumnRenameAsComparer : IComparer<LJCDataColumn>
  {
    // Compares two objects.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'
    ///  path='members/Compare/*'/>
    public int Compare(LJCDataColumn x, LJCDataColumn y)
    {
      int retValue;

      while (true)
      {
        retValue = NetCommon.CompareNull(x, y);
        if (retValue != NetString.CompareNotNull)
        {
          break;
        }

        retValue = NetCommon.CompareNull(x.RenameAs, y.RenameAs);
        if (retValue != NetString.CompareNotNull)
        {
          break;
        }

        retValue = x.RenameAs.CompareTo(y.RenameAs);
        break;
      }
      return retValue;
    }
  }
  #endregion
}
