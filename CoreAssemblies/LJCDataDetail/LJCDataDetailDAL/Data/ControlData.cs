// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ControlData.cs
using LJCNetCommon;
using System;

namespace LJCDataDetailDAL
{
  /// <summary>The ControlData table Data Object.</summary>
  public class ControlData : IComparable<ControlData>
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
    public ControlData()
    {
    }

    // The Copy constructor.
    /// <include path='items/CopyConstructor/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
    public ControlData(ControlData item)
    {
      // Key Properties
      ID = item.ID;
      ControlDetailID = item.ControlDetailID;

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
    }
    #endregion

    #region Data Methods

    // Creates and returns a clone of this object.
    /// <include path='items/Clone/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
    public ControlData Clone()
    {
      var retValue = MemberwiseClone() as ControlData;
      return retValue;
    }

    // Provides the default Sort functionality.
    /// <include path='items/CompareTo/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
    public int CompareTo(ControlData other)
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
        retValue = ID.CompareTo(other.ID);
      }
      return retValue;
    }

    // Gets the DbColumn values.
    /// <include path='items/GetDbColumnValues/*' file='../Doc/ControlData.xml'/>
    public DbColumn GetDbColumnValues()
    {
      var retValue = new DbColumn()
      {
        AllowDBNull = AllowDBNull,
        AutoIncrement = AutoIncrement,
        Caption = Caption,
        ColumnName = ColumnName,
        DataTypeName = DataTypeName,
        MaxLength = MaxLength,
        Position = Position,
        PropertyName = PropertyName,
        RenameAs = RenameAs,
        SQLTypeName = SQLTypeName,
        Value = Value
      };
      return retValue;
    }

    // Sets the DbColumn values.
    /// <include path='items/SetDbColumnValues/*' file='../Doc/ControlData.xml'/>
    public void SetDbColumnValues(DbColumn dbColumn)
    {
      if (dbColumn != null)
      {
        AllowDBNull = dbColumn.AllowDBNull;
        AutoIncrement = dbColumn.AutoIncrement;
        Caption = dbColumn.Caption;
        ColumnName = dbColumn.ColumnName;
        DataTypeName = dbColumn.DataTypeName;
        MaxLength = dbColumn.MaxLength;
        Position = dbColumn.Position;
        PropertyName = dbColumn.PropertyName;
        RenameAs = dbColumn.RenameAs;
        SQLTypeName = dbColumn.SQLTypeName;
        Value = dbColumn.Value;
      }
    }

    // The object string identifier.
    /// <include path='items/ToString/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
    public override string ToString()
    {
      return $"ID:{ID}-ControlDetailID:{ControlDetailID}";
    }
    #endregion

    #region Data Properties

    /// <summary>Gets or sets the ID value.</summary>
    //[Required]
    //[Column("ID", TypeName="bigint")]
    public Int64 ID { get; set; }

    /// <summary>Gets or sets the ControlTab ID.</summary>
    //[Required]
    //[Column("ControlDetailID", TypeName="bigint")]
    public Int64 ControlDetailID { get; set; }
    #endregion

    #region DbColumn Properties

    /// <summary>Gets or sets the AllowDBNull value.</summary>
    //[Required]
    //[Column("AllowDBNull", TypeName="bigint")]
    public bool AllowDBNull { get; set; }

    /// <summary>Gets or sets the AutoIncrement value.</summary>
    //[Required]
    //[Column("AutoIncrement", TypeName="bit")]
    public bool AutoIncrement { get; set; }

    /// <summary>Gets or sets the Caption value.</summary>
    //[Column("Caption", TypeName="varchar(60)")]
    public string Caption
    {
      get { return mCaption; }
      set { mCaption = NetString.InitString(value); }
    }
    private string mCaption;

    /// <summary>Gets or sets the ColumnName value.</summary>
    //[Required]
    //[Column("ColumnName", TypeName="varchar(60)")]
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
    //[Required]
    //[Column("DataTypeName", TypeName="varchar(60)")]
    public string DataTypeName
    {
      get { return mDataTypeName; }
      set { mDataTypeName = NetString.InitString(value); }
    }
    private string mDataTypeName;

    /// <summary>Gets or sets the MaxLength value.</summary>
    //[Column("MaxLength", TypeName="int")]
    public int MaxLength { get; set; }

    /// <summary>
    /// Gets or sets the Fixed Length Field Position value.
    /// </summary>
    //[Column("Position", TypeName="int")]
    public int Position { get; set; }

    /// <summary>Gets or sets the PropertyName value.</summary>
    //[Required]
    //[Column("PropertyName", TypeName="varchar(60)")]
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

    /// <summary>Gets or sets the RenameAs value.</summary>
    //[Column("RenameAs", TypeName="varchar(60)")]
    public string RenameAs
    {
      get { return mRenameAs; }
      set { mRenameAs = NetString.InitString(value); }
    }
    private string mRenameAs;

    /// <summary>Gets or sets the SQLTypeName value.</summary>
    //[Required]
    //[Column("SQLTypeName", TypeName="varchar(60)")]
    public string SQLTypeName
    {
      get { return mSQLTypeName; }
      set { mSQLTypeName = NetString.InitString(value); }
    }
    private string mSQLTypeName;

    /// <summary>Gets or sets the Value object.</summary>
    //[Column("Value", TypeName="varchar(60)")]
    public object Value
    {
      get { return mValue; }
      set
      {
        // Update if value is changed.
        if (false == NetCommon.IsEqual(mValue, value))
        {
          mValue = value;
        }
      }
    }
    private object mValue;
    #endregion

    #region Class Data

    /// <summary>The ID column name.</summary>
    public static string ColumnID = "ID";

    /// <summary>The DetailDialogID column name.</summary>
    public static string ColumnControlDetailID = "ControlDetailID";
    #endregion
  }
}
