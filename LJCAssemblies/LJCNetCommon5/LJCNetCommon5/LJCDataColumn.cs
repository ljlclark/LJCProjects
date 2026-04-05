// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCDataColumn.cs
using System.Xml.Serialization;

namespace LJCNetCommon5
{
  // Represents a data source column.
  /// <include path="members/LJCDataColumn/*" file="Doc/LJCDataColumn.xml"/>
  /// <group name="constructor">Constructor</group>
  /// <group name="dataClass">Data Class Methods</group>
  /// <group name="conversion">Conversions</group>
  /// <group name="dataProperties">Data Properties</group>
  /// <group name="joinProperties">Calculated and Join Data Properties</group>
  /// <group name="classProperties">Class Properties</group>
  /// <group name="classData">Class Data</group>
  public class LJCDataColumn : IComparable<LJCDataColumn>
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path="members/Constructor/*" file="../../../CoreUtilities/LJCGenDoc/Common/Data.xml"/>
    /// <parentGroup>constructor</parentGroup>
    public LJCDataColumn()
    {
      AllowDBNull = false;
      AutoIncrement = false;
      DataTypeName = "String";
      IsChanged = false;
      IsPrimaryKey = false;
      MaxLength = 0;
      Position = -1;
      Unique = false;
    }

    // The Copy constructor.
    /// <include path="members/CopyConstructor/*" file="../../../CoreUtilities/LJCGenDoc/Common/Data.xml"/>
    /// <parentGroup>constructor</parentGroup>
    public LJCDataColumn(LJCDataColumn item)
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
    /// <include path="members/LJCDataColumnC/*" file="Doc/LJCDataColumn.xml"/>
    /// <parentGroup>constructor</parentGroup>
    public LJCDataColumn(string columnName, string? value = null, string dataTypeName = "String"
      , string? propertyName = null, bool assignedKey = false, string? renameValue = null)
    {
      AutoIncrement = assignedKey;
      ColumnName = columnName;
      DataTypeName = dataTypeName;
      if (LJC.HasValue(propertyName))
      {
        PropertyName = propertyName;
      }
      IsChanged = false;
      RenameAs = renameValue;
      Value = value;
    }
    #endregion

    #region Data Class Methods

    // Creates and returns a clone of the object.
    /// <include path="members/Clone/*" file="../../../CoreUtilities/LJCGenDoc/Common/Data.xml"/>
    /// <parentGroup>dataClass</parentGroup>
    public LJCDataColumn? Clone()
    {
      LJCDataColumn retValue = MemberwiseClone() as LJCDataColumn;
      return retValue;
    }

    // Provides the default Sort functionality.
    /// <include path="members/CompareTo/*" file="../../../CoreUtilities/LJCGenDoc/Common/Data.xml"/>
    /// <parentGroup>dataClass</parentGroup>
    public int CompareTo(LJCDataColumn? other)
    {
      int retValue;

      if (null == other)
      {
        // This object is greater than the "other" object.
        retValue = LJCNetString.CompareGreater;
      }
      else
      {
        // Case sensitive.
        retValue = AddOrderIndex.CompareTo(other.AddOrderIndex);
      }
      return retValue;
    }

    // Formats the column value for the SQL string. (D)
    /// <include path="members/FormatValue/*" file="Doc/LJCDataColumn.xml"/>
    /// <parentGroup>dataClass</parentGroup>
    public string? FormatValue()
    {
      string retValue = LJCNetString.FormatValue(Value, DataTypeName);
      return retValue;
    }

    // The object string identifier.
    /// <include path="members/ToString/*" file="../../../CoreUtilities/LJCGenDoc/Common/Data.xml"/>
    /// <parentGroup>dataClass</parentGroup>
    public override string? ToString()
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

    // Creates a DbValue object from a LJCDataColumn object. (E)
    /// <include path="members/DbValue/*" file="Doc/LJCDataColumn.xml"/>
    /// <parentGroup>conversion</parentGroup>
    public static implicit operator LJCDataValue(LJCDataColumn dbColumn)
    {
      LJCDataValue retValue = null;

      if (dbColumn != null)
      {
        retValue = new LJCDataValue()
        {
          DataTypeName = dbColumn!.DataTypeName!,
          IsChanged = dbColumn.IsChanged,
          PropertyName = dbColumn!.PropertyName!,
          Value = dbColumn!.Value!
        };
      }
      return retValue!;
    }
    #endregion

    #region Data Properties

    // Gets or sets the AllowDBNull value.
    /// <include path="members/AllowDBNull/*" file="Doc/LJCDataColumn.xml"/>
    /// <parentGroup>dataProperties</parentGroup>
    public bool AllowDBNull { get; set; }

    // Gets or sets the AutoIncrement value.
    /// <include path="members/AutoIncrement/*" file="Doc/LJCDataColumn.xml"/>
    /// <parentGroup>dataProperties</parentGroup>
    public bool AutoIncrement { get; set; }

    // Gets or sets the Caption value.
    /// <include path="members/Caption/*" file="Doc/LJCDataColumn.xml"/>
    /// <parentGroup>dataProperties</parentGroup>
    public string? Caption
    {
      get { return mCaption; }
      set { mCaption = LJCNetString.InitString(value); }
    }
    private string? mCaption;

    // Gets or sets the ColumnName value.
    /// <include path="members/ColumnName/*" file="Doc/LJCDataColumn.xml"/>
    /// <parentGroup>dataProperties</parentGroup>
    public string? ColumnName
    {
      get { return mColumnName; }
      set
      {
        mColumnName = LJCNetString.InitString(value);

        // Set empty property name the same as the column name.
        if (LJC.HasValue(mColumnName)
          && !LJC.HasValue(mPropertyName))
        {
          PropertyName = ColumnName;
        }
      }
    }
    private string? mColumnName;

    // Gets or sets the DataTypeName value.
    /// <include path="members/DataTypeName/*" file="Doc/LJCDataColumn.xml"/>
    /// <parentGroup>dataProperties</parentGroup>
    public string? DataTypeName
    {
      get { return mDataTypeName; }
      set { mDataTypeName = LJCNetString.InitString(value); }
    }
    private string? mDataTypeName;

    // Gets or sets the MaxLength value.
    /// <include path="members/MaxLength/*" file="Doc/LJCDataColumn.xml"/>
    /// <parentGroup>dataProperties</parentGroup>
    public int MaxLength { get; set; }

    // Gets or sets the Fixed Length Field Position value. (R)
    /// <include path="members/Position/*" file="Doc/LJCDataColumn.xml"/>
    /// <parentGroup>dataProperties</parentGroup>
    public int Position { get; set; }

    // Gets or sets the PropertyName value.
    /// <include path="members/PropertyName/*" file="Doc/LJCDataColumn.xml"/>
    /// <parentGroup>dataProperties</parentGroup>
    public string? PropertyName
    {
      get { return mPropertyName; }
      set
      {
        // Cannot change property name to null or white space.
        if (LJC.HasValue(value))
        {
          mPropertyName = LJCNetString.InitString(value);
        }
      }
    }
    private string? mPropertyName;

    // Gets or sets the RenameAs value.
    /// <include path="members/RenameAs/*" file="Doc/LJCDataColumn.xml"/>
    /// <parentGroup>dataProperties</parentGroup>
    public string? RenameAs
    {
      get { return mRenameAs; }
      set { mRenameAs = LJCNetString.InitString(value); }
    }
    private string? mRenameAs;

    // Gets or sets the SQLTypeName value.
    /// <include path="members/SQLTypeName/*" file="Doc/LJCDataColumn.xml"/>
    /// <parentGroup>dataProperties</parentGroup>
    public string? SQLTypeName
    {
      get { return mSQLTypeName; }
      set { mSQLTypeName = LJCNetString.InitString(value); }
    }
    private string? mSQLTypeName;

    // Gets or sets the Value object.
    /// <include path="members/Value/*" file="Doc/LJCDataColumn.xml"/>
    /// <parentGroup>dataProperties</parentGroup>
    public object? Value
    {
      get { return mValue; }
      set
      {
        // Update if value is changed.
        if (!LJC.IsEqual(mValue, value))
        {
          IsChanged = true;
          mValue = value;
        }
      }
    }
    private object? mValue;
    #endregion

    #region Calculated and Join Data Properties

    // Gets or sets the ID value.
    /// <include path="members/ID/*" file="Doc/LJCDataColumn.xml"/>
    /// <parentGroup>joinProperties</parentGroup>
    [XmlIgnore()]
    public int ID { get; set; }

    // Gets or sets the Sequence value.
    /// <include path="members/Sequence/*" file="Doc/LJCDataColumn.xml"/>
    /// <parentGroup>joinProperties</parentGroup>
    [XmlIgnore()]
    public int Sequence { get; set; }

    // Gets or sets the ViewData ID value.
    /// <include path="members/ViewDataID/*" file="Doc/LJCDataColumn.xml"/>
    /// <parentGroup>joinProperties</parentGroup>
    [XmlIgnore()]
    public int ViewDataID { get; set; }

    // Gets or sets the ViewJoin ID value.
    /// <include path="members/ViewJoinID/*" file="Doc/LJCDataColumn.xml"/>
    /// <parentGroup>joinProperties</parentGroup>
    [XmlIgnore()]
    public int ViewJoinID { get; set; }

    // Gets or sets the Width value.
    /// <include path="members/Width/*" file="Doc/LJCDataColumn.xml"/>
    /// <parentGroup>joinProperties</parentGroup>
    [XmlIgnore()]
    public int Width { get; set; }
    #endregion

    #region Class Properties

    // 
    /// <include path="members/AddOrderIndex/*" file="Doc/LJCDataColumn.xml"/>
    /// <parentGroup>classProperties</parentGroup>
    public int AddOrderIndex { get; set; }

    // Gets or sets the DefaultValue value.
    /// <include path="members/DefaultValue/*" file="Doc/LJCDataColumn.xml"/>
    /// <parentGroup>classProperties</parentGroup>
    public string? DefaultValue
    {
      get { return mDefaultValue; }
      set { mDefaultValue = LJCNetString.InitString(value); }
    }
    private string? mDefaultValue;

    // Indicates if the value has changed.
    /// <include path="members/IsChanged/*" file="Doc/LJCDataColumn.xml"/>
    /// <parentGroup>classProperties</parentGroup>
    [XmlIgnore()]
    public bool IsChanged { get; set; }

    // Gets or sets the IsPrimaryKey value.
    /// <include path="members/IsPrimaryKey/*" file="Doc/LJCDataColumn.xml"/>
    /// <parentGroup>classProperties</parentGroup>
    public bool IsPrimaryKey { get; set; }

    // Gets or sets the KeyType value.
    /// <include path="members/KeyType/*" file="Doc/LJCDataColumn.xml"/>
    /// <parentGroup>classProperties</parentGroup>
    // "Natural", "Natural*", "Foreign"
    public string? KeyType { get; set; }

    // Gets or sets the Unique value.
    /// <include path="members/Unique/*" file="Doc/LJCDataColumn.xml"/>
    /// <parentGroup>classProperties</parentGroup>
    public bool Unique { get; set; }
    #endregion

    #region Class Data

    // The AllowDBNull column name.
    /// <include path="members/ColumnAllowDBNull/*" file="Doc/LJCDataColumn.xml"/>
    /// <parentGroup>classData</parentGroup>
    public const string ColumnAllowDBNull = "AllowDBNull";

    // The AutoIncrement column name.
    /// <include path="members/ColumnAutoIncrement/*" file="Doc/LJCDataColumn.xml"/>
    /// <parentGroup>classData</parentGroup>
    public const string ColumnAutoIncrement = "AutoIncrement";

    // The Caption column name.
    /// <include path="members/ColumnCaption/*" file="Doc/LJCDataColumn.xml"/>
    /// <parentGroup>classData</parentGroup>
    public const string ColumnCaption = "Caption";

    // The ColumnName column name.
    /// <include path="members/ColumnColumnName/*" file="Doc/LJCDataColumn.xml"/>
    /// <parentGroup>classData</parentGroup>
    public const string ColumnColumnName = "ColumnName";

    // The DataTypeName column name.
    /// <include path="members/ColumnDataTypeName/*" file="Doc/LJCDataColumn.xml"/>
    /// <parentGroup>classData</parentGroup>
    public const string ColumnDataTypeName = "DataTypeName";

    // The MaxLength column name.
    /// <include path="members/ColumnMaxLength/*" file="Doc/LJCDataColumn.xml"/>
    /// <parentGroup>classData</parentGroup>
    public const string ColumnMaxLength = "MaxLength";

    // The Position column name.
    /// <include path="members/ColumnPosition/*" file="Doc/LJCDataColumn.xml"/>
    /// <parentGroup>classData</parentGroup>
    public const string ColumnPosition = "Position";

    // The PropertyName column name.
    /// <include path="members/ColumnPropertyName/*" file="Doc/LJCDataColumn.xml"/>
    /// <parentGroup>classData</parentGroup>
    public const string ColumnPropertyName = "PropertyName";

    // The RenameAs column name.
    /// <include path="members/ColumnRenameAs/*" file="Doc/LJCDataColumn.xml"/>
    /// <parentGroup>classData</parentGroup>
    public const string ColumnRenameAs = "RenameAs";

    // The SQLTypeName column name.
    /// <include path="members/ColumnSQLTypeName/*" file="Doc/LJCDataColumn.xml"/>
    /// <parentGroup>classData</parentGroup>
    public const string ColumnSQLTypeName = "SQLTypeName";

    // The Value column name.
    /// <include path="members/ColumnValue/*" file="Doc/LJCDataColumn.xml"/>
    /// <parentGroup>classData</parentGroup>
    public const string ColumnValue = "Value";
    #endregion
  }

  // Sort and search on column name.
  /// <include path="members/DbColumnNameComparer/*" file="Doc/LJCDataColumn.xml"/>
  public class DbColumnNameComparer : IComparer<LJCDataColumn>
  {
    // Compares two objects.
    /// <include path="members/Compare/*" file="../../../CoreUtilities/LJCGenDoc/Common/Data.xml"/>
    public int Compare(LJCDataColumn? x, LJCDataColumn? y)
    {
      int retValue;

      retValue = LJC.CompareNull(x, y);
      if (LJCNetString.CompareNotNullOrEqual == retValue)
      {
        retValue = LJC.CompareNull(x?.ColumnName, y?.ColumnName);
        if (LJCNetString.CompareNotNullOrEqual == retValue)
        {
          // Case sensitive.
          retValue = x!.ColumnName!.CompareTo(y?.ColumnName);
        }
      }
      return retValue;
    }
  }

  // Sort and search on property name.
  /// <include path="members/DbColumnPropertyComparer/*" file="Doc/LJCDataColumn.xml"/>
  public class DbColumnPropertyComparer : IComparer<LJCDataColumn>
  {
    // Compares two objects.
    /// <include path="members/Compare/*" file="../../../CoreUtilities/LJCGenDoc/Common/Data.xml"/>
    public int Compare(LJCDataColumn? x, LJCDataColumn? y)
    {
      int retValue;

      retValue = LJC.CompareNull(x, y);
      if (LJCNetString.CompareNotNullOrEqual == retValue)
      {
        retValue = LJC.CompareNull(x?.PropertyName, y?.PropertyName);
        if (LJCNetString.CompareNotNullOrEqual == retValue)
        {
          // Case sensitive.
          retValue = x!.PropertyName!.CompareTo(y?.PropertyName);
        }
      }
      return retValue;
    }
  }

  // Sort and search on RenameAs value.
  /// <include path="members/DbColumnRenameAsComparer/*" file="Doc/LJCDataColumn.xml"/>
  public class LJCDataColumnRenameAsComparer : IComparer<LJCDataColumn>
  {
    // Compares two objects.
    /// <include path="members/Compare/*" file="../../../CoreUtilities/LJCGenDoc/Common/Data.xml"/>
    public int Compare(LJCDataColumn? x, LJCDataColumn? y)
    {
      int retValue;

      retValue = LJC.CompareNull(x, y);
      if (LJCNetString.CompareNotNullOrEqual == retValue)
      {
        retValue = LJC.CompareNull(x?.RenameAs, y?.RenameAs);
        if (LJCNetString.CompareNotNullOrEqual == retValue)
        {
          // Case sensitive.
          retValue = x!.RenameAs!.CompareTo(y?.RenameAs);
        }
      }
      return retValue;
    }
  }
}
