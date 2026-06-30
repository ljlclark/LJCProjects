// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DbColumn.cs
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace LJCNetCommon
{
  // Represents a database column. (D)
  /// <include file='Doc/DbColumn.xml'
  ///  path='items/DbColumn/*'/>
  public class DbColumn : IComparable<DbColumn>
  {
    #region Constructors

    // Initializes an object instance.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'
    ///  path='items/DefaultConstructor/*'/>
    public DbColumn()
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
    /// <include file='Doc/DbColumn.xml'
    ///  path='items/DbColumnC/*'/>
    public DbColumn(string columnName, string value = null
      , string dataTypeName = "String", string propertyName = null
      , bool assignedKey = false, string renameValue = null) : this()
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
    }

    // The Copy constructor.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'
    ///  path='items/CopyConstructor/*'/>
    public DbColumn(DbColumn item)
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
    ///  path='items/Clone/*'/>
    public DbColumn Clone()
    {
      DbColumn retValue = MemberwiseClone() as DbColumn;
      return retValue;
    }

    // Provides the default Sort functionality.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'
    ///  path='items/CompareTo/*'/>
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
    /// <include file='Doc/DbColumn.xml'
    ///  path='items/FormatValue/*'/>
    public string FormatValue()
    {
      string retValue = NetString.FormatValue(Value, DataTypeName);
      return retValue;
    }

    // The object string identifier.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'
    ///  path='items/ToString/*'/>
    public override string ToString()
    {
      string retValue = _ColumnName;

      if (_PropertyName != _ColumnName)
      {
        retValue += $"-{_PropertyName}";
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
    #endregion

    #region Conversions

    // Creates a DbValue object from a DbColumn object. (E)
    /// <include file='Doc/DbColumn.xml'
    ///  path='items/DbValue/*'/>
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
          // *** Next Statement *** Add 10/15/23
          //RenameAs = dbColumn.RenameAs,
          Value = dbColumn.Value
        };
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
    /// <include file='Doc/DbColumn.xml'
    ///  path='items/Caption/*'/>
    public string Caption
    {
      get => _Caption;
      set
      {
        if (_Caption != value)
        {
          _Caption = value;
          if (value != null)
          {
            _Caption = value.ToString().Trim();
          }
        }
      }
    }
    private string _Caption;

    // Gets or sets the ColumnName value.
    /// <include file='Doc/DbColumn.xml'
    ///  path='items/ColumnName/*'/>
    public string ColumnName
    {
      get => _ColumnName;
      set
      {
        // Cannot change column name to null or white space.
        if (_ColumnName != value
          && NetString.HasValue(value))
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

    /// <summary>Gets or sets the DataTypeName value.</summary>
    public string DataTypeName
    {
      get => _DataTypeName;
      set
      {
        if (_DataTypeName != value)
        {
          _DataTypeName = value;
          if (value != null)
          {
            _DataTypeName = value.ToString().Trim();
          }
        }
      }
    }
    private string _DataTypeName;

    /// <summary>Gets or sets the MaxLength value.</summary>
    public int MaxLength { get; set; }

    // Gets or sets the Fixed Length Field Position value. (R)
    /// <include file='Doc/DbColumn.xml'
    ///  path='items/Position/*'/>
    public int Position { get; set; }

    // Gets or sets the PropertyName value.
    /// <include file='Doc/DbColumn.xml'
    ///  path='items/PropertyName/*'/>
    public string PropertyName
    {
      get => _PropertyName;
      set
      {
        // Cannot change property name to null or white space.
        if (_PropertyName != value
          && NetString.HasValue(value))
        {
          _PropertyName = value.ToString().Trim();

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
    /// <include file='Doc/DbColumn.xml'
    ///  path='items/RenameAs/*'/>
    public string RenameAs
    {
      get => _RenameAs;
      set
      {
        if (_RenameAs != value)
        {
          _RenameAs = value;
          if (value != null)
          {
            _RenameAs = value.ToString().Trim();
          }
        }
      }
    }
    private string _RenameAs;

    /// <summary>Gets or sets the SQLTypeName value.</summary>
    public string SQLTypeName
    {
      get => _SQLTypeName;
      set
      {
        if (_SQLTypeName != value)
        {
          _SQLTypeName = value;
          if (value != null)
          {
            _SQLTypeName = value.ToString().Trim();
          }
        }
      }
    }
    private string _SQLTypeName;

    /// <summary>Gets or sets the Value object.</summary>
    public object Value
    {
      get => _Value;
      set
      {
        if (_Value != value)
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

    /// <summary>Gets or sets the add order index.</summary> 
    public int AddOrderIndex { get; set; }

    /// <summary>Gets or sets the default value.</summary>
    public string DefaultValue
    {
      get => _DefaultValue;
      set
      {
        if (_DefaultValue != value)
        {
          _DefaultValue = value;
          if (value != null)
          {
            _DefaultValue = value.ToString().Trim();
          }
        }
      }
    }
    private string _DefaultValue;

    /// <summary>Gets or sets the changed indicator.</summary>
    [XmlIgnore()]
    public bool IsChanged { get; set; }

    /// <summary>Gets or sets the primary key indicator.</summary>
    public bool IsPrimaryKey { get; set; }

    /// <summary>Gets or sets the KeyType value.</summary>
    // "Natural", "Natural*", "Foreign"
    public string KeyType
    {
      get => _KeyType;
      set
      {
        if (_KeyType != value)
        {
          _KeyType = value;
          if (value != null)
          {
            _KeyType = value.ToString().Trim();
          }
        }
      }
    }
    private string _KeyType;

    /// <summary>Gets or sets the original value.</summary>
    public object OriginalValue
    {
      get => _OriginalValue;
      set
      {
        if (_OriginalValue != value)
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

    /// <summary>Gets or sets the unique key indicator.</summary>
    public bool Unique { get; set; }
    #endregion

    #region View Join Data Properties

    // Gets or sets the ID value. (R)
    /// <include file='Doc/DbColumn.xml'
    ///  path='items/ID/*'/>
    [XmlIgnore()]
    public int ID { get; set; }

    // Gets or sets the Sequence value. (R)
    /// <include file='Doc/DbColumn.xml'
    ///  path='items/Sequence/*'/>
    [XmlIgnore()]
    public int Sequence { get; set; }

    // Gets or sets the ViewData ID value. (R)
    /// <include file='Doc/DbColumn.xml'
    ///  path='items/ViewDataID/*'/>
    [XmlIgnore()]
    public int ViewDataID { get; set; }

    // Gets or sets the ViewJoin ID value. (R)
    /// <include file='Doc/DbColumn.xml'
    ///  path='items/ViewJoinID/*'/>
    [XmlIgnore()]
    public int ViewJoinID { get; set; }

    // Gets or sets the Width value. (R)
    /// <include file='Doc/DbColumn.xml'
    ///  path='items/Width/*'/>
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
  public class DbColumnNameComparer : IComparer<DbColumn>
  {
    // Compares two objects.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'
    ///  path='items/Compare/*'/>
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
  /// <include file='Doc/DbColumn.xml'
  ///  path='items/DbColumnPropertyComparer/*'/>
  public class DbColumnPropertyComparer : IComparer<DbColumn>
  {
    // Compares two objects.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'
    ///  path='items/Compare/*'/>
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
  /// <include file='Doc/DbColumn.xml'
  ///  path='items/DbColumnRenameAsComparer/*'/>
  public class DbColumnRenameAsComparer : IComparer<DbColumn>
  {
    // Compares two objects.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'
    ///  path='items/Compare/*'/>
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
  #endregion
}
