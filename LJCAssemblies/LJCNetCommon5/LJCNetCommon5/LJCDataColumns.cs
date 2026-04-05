// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCDataColumns

namespace LJCNetCommon5
{
  // Represents a collection of DbColumn objects.
  /// <include path="members/LJCDataColumns/*" file="Doc/LJCDataColumns.xml"/>
  //[XmlRoot("LJCDataColumns")]
  public class LJCDataColumns : List<LJCDataColumn>
  {
    #region Static Functions

    // Creates DbColumns from a Data Object.
    /// <include path="members/LJCCreateObjectColumns/*" file="Doc/LJCDataColumns.xml"/>
    public static LJCDataColumns? LJCCreateObjectColumns(object dataObject
      , LJCDataColumns? dataDefinition = null)
    {
      LJCDataColumn definitionColumn = null;
      LJCDataColumns retValue = null;

      var reflect = new LJCReflect(dataObject);
      List<string> propertyNames = reflect.GetPropertyNames();

      if (propertyNames != null)
      {
        retValue = [];
        foreach (string propertyName in propertyNames)
        {
          if ("ChangedNames" == propertyName)
          {
            continue;
          }

          if (dataDefinition != null)
          {
            definitionColumn = dataDefinition.LJCSearchPropertyName(propertyName);
          }

          var dataColumn = new LJCDataColumn()
          {
            Caption = propertyName,
            ColumnName = propertyName,
            Value = reflect.GetValue(propertyName)
          };

          Type type = reflect.GetPropertyType(propertyName);
          if (type != null)
          {
            dataColumn.DataTypeName = type.Name;
            if (definitionColumn != null
              && "String" == type.Name)
            {
              dataColumn.MaxLength = definitionColumn.MaxLength;
            }
          }
          retValue.Add(dataColumn);
        }
      }
      return retValue;
    }

    // Deserializes from the specified XML file.
    /// <include path="members/LJCDeserialize/*" file="../../../CoreUtilities/LJCGenDoc/Common/Collection.xml"/>
    public static LJCDataColumns? LJCDeserialize(string? fileSpec = null)
    {
      LJCDataColumns retValue;

      if (!LJC.HasValue(fileSpec))
      {
        fileSpec = LJCDefaultFileName;
      }
      retValue = LJC.XmlDeserialize(typeof(LJCDataColumns), fileSpec)
        as LJCDataColumns;
      return retValue;
    }

    // Creates a ColumnNames list from a DataObject.
    /// <include path="members/LJCGetPropertyNames/*" file="Doc/LJCDataColumns.xml"/>
    public static List<string>? LJCGetPropertyNames(object dataObject)
    {
      List<string> retValue = null;

      var reflect = new LJCReflect(dataObject);
      List<string> propertyNames = reflect.GetPropertyNames();

      if (propertyNames != null)
      {
        retValue = [];
        foreach (string propertyName in propertyNames)
        {
          if ("ChangedNames" == propertyName)
          {
            continue;
          }
          retValue.Add(propertyName);
        }
      }
      return retValue;
    }

    // Creates a DbValues object from a DbColumns object.
    /// <include path="members/DbValues/*" file="Doc/LJCDataColumns.xml"/>
    public static implicit operator LJCDataValues(LJCDataColumns dataColumns)
    {
      LJCDataValues retValue = null;

      if (LJC.HasItems(dataColumns))
      {
        retValue = [];
        foreach (LJCDataColumn dataColumn in dataColumns)
        {
          var dataValue = dataColumn;
          retValue.Add(dataValue);
        }
      }
      return retValue!;
    }

    // Sets the Map values.
    private static void SetMapValues(LJCDataColumn? dataColumn, string? propertyName = null
      , string? renameAs = null, string? caption = null)
    {
      if (dataColumn != null)
      {
        if (propertyName != null)
        {
          dataColumn.PropertyName = propertyName;
        }
        if (renameAs != null)
        {
          dataColumn.RenameAs = renameAs;
        }
        if (caption != null)
        {
          dataColumn.Caption = caption;
        }
      }
    }
    #endregion

    #region Constructors

    // Initializes an object instance.
    /// <include path="members/DefaultConstructor/*" file="../../../CoreUtilities/LJCGenDoc/Common/Data.xml"/>
    public LJCDataColumns()
    {
      mPrevCount = -1;
    }

    // The Copy constructor.
    /// <include path="members/CopyConstructor/*" file="../../../CoreUtilities/LJCGenDoc/Common/Collection.xml"/>
    public LJCDataColumns(LJCDataColumns items)
    {
      if (LJC.HasItems(items))
      {
        foreach (var item in items)
        {
          Add(new LJCDataColumn(item));
        }
      }
    }
    #endregion

    #region Collection Methods

    // Adds the object element to the collection
    /// <include path="members/Add/*" file="Doc/LJCDataColumns.xml"/>
    public new void Add(LJCDataColumn dataColumn)
    {
      LJCSortAddOrderIndex();
      base.Add(dataColumn);
      int newIndex = Count - 1;
      dataColumn.AddOrderIndex = newIndex;
    }

    // Creates the Object from the arguments and adds it to the collection. (R)
    /// <include path="members/Add1/*" file="Doc/LJCDataColumns.xml"/>
    public LJCDataColumn Add(string columnName, int position, int maxLength)
    {
      var retValue = new LJCDataColumn()
      {
        AutoIncrement = false,
        ColumnName = columnName,
        MaxLength = maxLength,
        Position = position
      };
      Add(retValue);
      return retValue;
    }

    // Creates the Object from the arguments and adds it to the collection.
    /// <include path="members/Add2/*" file="Doc/LJCDataColumns.xml"/>
    public LJCDataColumn Add(string columnName, string? propertyName = null
      , string? renameAs = null, string dataTypeName = "String"
      , string? caption = null, int maxLength = 5)
    {
      var retValue = new LJCDataColumn()
      {
        AutoIncrement = false,
        Caption = caption,
        ColumnName = columnName,
        DataTypeName = dataTypeName,
        MaxLength = maxLength,
        PropertyName = propertyName,
        RenameAs = renameAs
      };
      Add(retValue);
      return retValue;
    }

    // Creates the Object from the arguments and adds it to the collection.
    /// <include path="members/Add3/*" file="Doc/LJCDataColumns.xml"/>
    public LJCDataColumn Add(string columnName, object value
      , string dataTypeName = "String", int maxLength = 5)
    {
      var retValue = new LJCDataColumn()
      {
        AutoIncrement = false,
        ColumnName = columnName,
        DataTypeName = dataTypeName,
        MaxLength = maxLength,
        Value = value
      };
      Add(retValue);
      return retValue;
    }

    // Creates and returns a clone of the object.
    /// <include path="members/Clone/*" file="../../../CoreUtilities/LJCGenDoc/Common/Data.xml"/>
    public LJCDataColumns Clone()
    {
      var retValue = new LJCDataColumns();
      foreach (LJCDataColumn dataColumn in this)
      {
        retValue.Add(dataColumn.Clone()!);
      }
      return retValue;
    }

    // Checks if the collection has items.
    /// <include path="members/HasItems2/*" file="../../../CoreUtilities/LJCGenDoc/Common/Collection.xml"/>
    public bool HasItems()
    {
      bool retValue = false;

      if (Count > 0)
      {
        retValue = true;
      }
      return retValue;
    }

    // Creates the DbColumn from the supplied values and adds to the collection.
    /// <include path="members/LJCAddPropertyAs/*" file="Doc/LJCDataColumns.xml"/>
    public LJCDataColumn LJCAddPropertyAs(string propertyName, string? caption = null
      , string? renameAs = null, string dataTypeName = "String")
    {
      var retValue = new LJCDataColumn()
      {
        ColumnName = propertyName,
        PropertyName = propertyName,
        Caption = caption,
        DataTypeName = dataTypeName,
        AutoIncrement = false,
        Value = null,
        RenameAs = renameAs
      };

      if (!LJC.HasValue(renameAs))
      {
        retValue.RenameAs = retValue.PropertyName;
      }
      Add(retValue);
      return retValue;
    }

    // Returns a column by property name.
    /// <include path="members/LJCGetColumn/*" file="Doc/LJCDataColumns.xml"/>
    public LJCDataColumn? LJCGetColumn(string propertyName)
    {
      LJCDataColumn retValue = null;

      if (Count != mPrevCount
        || mSortType.CompareTo(SortType.PropertyName) != 0)
      {
        retValue = Find(x => x.PropertyName == propertyName);
      }
      else
      {
        retValue = LJCSearchPropertyName(propertyName);
      }
      return retValue;
    }

    // Returns a set of columns that match the supplied list.
    /// <include path="members/LJCGetColumns/*" file="Doc/LJCDataColumns.xml"/>
    public LJCDataColumns? LJCGetColumns(List<string> propertyNames)
    {
      LJCDataColumn searchColumn;
      LJCDataColumns retValue = null;

      if (LJC.HasItems(propertyNames))
      {
        retValue = [];
        foreach (string propertyName in propertyNames)
        {
          var searchName = LJCNetString.GetSearchName(propertyName);
          searchColumn = LJCSearchPropertyName(searchName);
          if (searchColumn != null)
          {
            retValue.Add(new LJCDataColumn(searchColumn));
          }
        }
      }
      return retValue;
    }

    // Configure the Grid Columns from the Data object properties.
    /// <include path="members/LJCGetColumns2/*" file="Doc/LJCDataColumns.xml"/>
    public static LJCDataColumns? LJCGetColumns(object dataObject
      , List<string>? propertyNames = null)
    {
      var retValue = LJCCreateObjectColumns(dataObject);
      if (propertyNames != null)
      {
        retValue = retValue?.LJCGetColumns(propertyNames);
      }
      return retValue;
    }

    /// <summary>Get the list of property names.</summary>
    public List<string>? LJCGetPropertyNames()
    {
      List<string> retValue = null;

      if (LJC.HasItems(this))
      {
        retValue = [];
        foreach (LJCDataColumn dbColumn in this)
        {
          retValue.Add(dbColumn.PropertyName!);
        }
      }
      return retValue;
    }

    // Removes a DbColumn item.
    /// <include path="members/LJCRemoveColumn/*" file="Doc/LJCDataColumns.xml"/>
    public void LJCRemoveColumn(string columnName)
    {
      var column = Find(x => x.ColumnName == columnName);
      if (column != null)
      {
        Remove(column);
      }
    }

    // Serializes the collection
    /// <include path="members/LJCSerialize/*" file="../../../CoreUtilities/LJCGenDoc/Common/Collection.xml"/>
    public void LJCSerialize(string? fileSpec = null)
    {
      if (!LJC.HasValue(fileSpec))
      {
        fileSpec = LJCDefaultFileName;
      }
      LJC.XmlSerialize(GetType(), this, null, fileSpec);
    }

    // Add or Update.
    /// <include path="members/LJCSetData/*" file="../../../CoreUtilities/LJCGenDoc/Common/Collection.xml"/>
    /// <param name="dataColumn">The LJCDataColumn object.</param>
    public void LJCSetData(LJCDataColumn dataColumn)
    {
      if (LJC.HasItems(this))
      {
        var addDataColumn = LJCGetColumn(dataColumn.PropertyName!);
        if (addDataColumn != null)
        {
          addDataColumn.AllowDBNull = dataColumn.AllowDBNull;
          addDataColumn.AutoIncrement = dataColumn.AutoIncrement;
          addDataColumn.Caption = dataColumn.Caption;
          addDataColumn.ColumnName = dataColumn.ColumnName;
          addDataColumn.DataTypeName = dataColumn.DataTypeName;
          addDataColumn.MaxLength = dataColumn.MaxLength;
          addDataColumn.Position = dataColumn.Position;
          addDataColumn.PropertyName = dataColumn.PropertyName;
          addDataColumn.RenameAs = dataColumn.RenameAs;
          addDataColumn.SQLTypeName = dataColumn.SQLTypeName;
          addDataColumn.Value = dataColumn.Value;
        }
        else
        {
          Add(dataColumn);
        }
      }
    }
    #endregion

    #region Data Methods

    // Sets the IsChanged value to false for all elements in the collection.
    /// <include path="members/LJCClearChanged/*" file="Doc/LJCDataColumns.xml"/>
    public void LJCClearChanged()
    {
      foreach (LJCDataColumn dataColumn in this)
      {
        dataColumn.IsChanged = false;
      }
    }

    // Gets a collection of changed columns.
    /// <include path="members/LJCGetChanged/*" file="Doc/LJCDataColumns.xml"/>
    public LJCDataColumns LJCGetChanged()
    {
      List<LJCDataColumn> columns;
      var retValue = new LJCDataColumns();

      columns = FindAll(x => x.IsChanged);
      foreach (LJCDataColumn dataColumn in columns)
      {
        retValue.Add(dataColumn.Clone()!);
      }
      return retValue;
    }
    #endregion

    #region Search and Sort Methods

    // Finds and returns the object that matches the supplied values.
    /// <include path="members/LJCSearchName/*" file="../../../CoreUtilities/LJCGenDoc/Common/Collection.xml"/>
    public LJCDataColumn? LJCSearchColumnName(string name)
    {
      DbColumnNameComparer comparer;
      LJCDataColumn retValue = null;

      comparer = new DbColumnNameComparer();
      LJCSortName(comparer);

      var searchDbColumn = new LJCDataColumn()
      {
        ColumnName = name
      };
      int index = BinarySearch(searchDbColumn, comparer);
      if (index > -1)
      {
        retValue = this[index];
      }
      return retValue;
    }

    // Finds and returns the column that contains the supplied property name.
    /// <include path="members/LJCSearchPropertyName/*" file="Doc/LJCDataColumns.xml"/>
    public LJCDataColumn? LJCSearchPropertyName(string propertyName)
    {
      LJCDataColumn retValue = null;

      var comparer = new DbColumnPropertyComparer();
      LJCSortProperty(comparer);
      var searchDbColumn = new LJCDataColumn()
      {
        PropertyName = propertyName
      };
      int index = BinarySearch(searchDbColumn, comparer);
      if (index > -1)
      {
        retValue = this[index];
      }
      return retValue;
    }

    // Finds and returns the column that contains the supplied property name.
    /// <include path="members/LJCSearchRenameAs/*" file="Doc/LJCDataColumns.xml"/>
    public LJCDataColumn? LJCSearchRenameAs(string renameAs)
    {
      LJCDataColumnRenameAsComparer comparer;
      LJCDataColumn retValue = null;

      comparer = new LJCDataColumnRenameAsComparer();
      LJCSortRenameAs(comparer);

      var searchDbColumn = new LJCDataColumn()
      {
        RenameAs = renameAs
      };
      int index = BinarySearch(searchDbColumn, comparer);
      if (index > -1)
      {
        retValue = this[index];
      }
      return retValue;
    }

    // Sort on AddOrderIndex.
    /// <include path="members/LJCSortAddOrderIndex/*" file="Doc/LJCDataColumns.xml"/>
    public void LJCSortAddOrderIndex()
    {
      if (Count != mPrevCount
        || mSortType.CompareTo(SortType.AddOrderIndex) != 0)
      {
        mPrevCount = Count;
        Sort();
        mSortType = SortType.AddOrderIndex;
      }
    }

    /// <summary>Sort on ColumnName.</summary>
    /// <include path="members/LJCSortName/*" file="Doc/LJCDataColumns.xml"/>
    /// <param name="comparer">The comparer function.</param>
    public void LJCSortName(DbColumnNameComparer comparer)
    {
      if (Count != mPrevCount
        || mSortType.CompareTo(SortType.ColumnName) != 0)
      {
        mPrevCount = Count;
        Sort(comparer);
        mSortType = SortType.ColumnName;
      }
    }

    // Sort on PropertyName.
    /// <include path="members/LJCSortProperty/*" file="Doc/LJCDataColumns.xml"/>
    /// <param name="comparer">The comparer function.</param>
    public void LJCSortProperty(DbColumnPropertyComparer comparer)
    {
      if (Count != mPrevCount
        || mSortType.CompareTo(SortType.PropertyName) != 0)
      {
        mPrevCount = Count;
        Sort(comparer);
        mSortType = SortType.PropertyName;
      }
    }

    // Sort on RenameAs.
    /// <include path="members/LJCSortRenameAs/*" file="Doc/LJCDataColumns.xml"/>
    /// <param name="comparer">The comparer function.</param>
    public void LJCSortRenameAs(LJCDataColumnRenameAsComparer comparer)
    {
      if (Count != mPrevCount
        || mSortType.CompareTo(SortType.RenameAs) != 0)
      {
        mPrevCount = Count;
        Sort(comparer);
        mSortType = SortType.RenameAs;
      }
    }
    #endregion

    #region Other Public Methods

    // Get the minimum date value.
    /// <include path="members/LJCGetMinSqlDate/*" file="Doc/LJCDataColumns.xml"/>
    public static string LJCGetMinSqlDate()
    {
      return "1753/01/01 00:00:00";
    }

    // Sets the caption properties.
    /// <include path="members/LJCSetColumnCaptions/*" file="Doc/LJCDataColumns.xml"/>
    public void LJCSetColumnCaptions(LJCDataColumns dataColumns)
    {
      LJCDataColumn searchColumn;

      if (LJC.HasItems(dataColumns))
      {
        foreach (LJCDataColumn dataColumn in dataColumns)
        {
          searchColumn = LJCSearchPropertyName(dataColumn.PropertyName!);
          if (searchColumn != null)
          {
            dataColumn.Caption = searchColumn.Caption;
          }
        }
      }
    }

    // Maps the column property and rename values.
    /// <include path="members/LJCMapNames/*" file="Doc/LJCDataColumns.xml"/>
    public void LJCMapNames(string columnName, string? propertyName = null
      , string? renameAs = null, string? caption = null)
    {
      var dataColumn = LJCSearchColumnName(columnName);
      SetMapValues(dataColumn, propertyName, renameAs, caption);
    }
    #endregion

    #region Value Methods

    // Gets the column object value as a bool.
    /// <include path="members/LJCGetBoolean/*" file="Doc/LJCDataColumns.xml"/>
    public bool LJCGetBoolean(string propertyName)
    {
      bool retValue = default;

      var value = LJCGetString(propertyName);
      if (value != null)
      {
        try
        {
          retValue = Convert.ToBoolean(value);
        }
        catch
        {
          retValue = false;
        }
      }
      return retValue;
    }

    // Gets the column object value as a byte.
    /// <include path="members/LJCGetByte/*" file="Doc/LJCDataColumns.xml"/>
    public byte LJCGetByte(string propertyName)
    {
      byte retValue = default;

      var value = LJCGetString(propertyName);
      if (value != null)
      {
        retValue = Convert.ToByte(value);
      }
      return retValue;
    }

    // Gets the column object value as a char.
    /// <include path="members/LJCGetChar/*" file="Doc/LJCDataColumns.xml"/>
    public char LJCGetChar(string propertyName)
    {
      char retValue = default;

      var value = LJCGetString(propertyName);
      if (value != null)
      {
        retValue = Convert.ToChar(value);
      }
      return retValue;
    }

    // Gets the column object value as a DateTime.
    /// <include path="members/LJCGetDbDateTime/*" file="Doc/LJCDataColumns.xml"/>
    public DateTime LJCGetDbDateTime(string propertyName)
    {
      DateTime retValue = DateTime.Parse(LJCGetMinSqlDate());

      var value = LJCGetString(propertyName);
      if (value != null)
      {
        retValue = DateTime.Parse(value);
      }
      return retValue;
    }

    // Gets the column object value as a decimal.
    /// <include path="members/LJCGetDecimal/*" file="Doc/LJCDataColumns.xml"/>
    public decimal LJCGetDecimal(string propertyName)
    {
      decimal retValue = default;

      var value = LJCGetString(propertyName);
      if (value != null)
      {
        retValue = Convert.ToDecimal(value);
      }
      return retValue;
    }

    // Gets the column object value as a double.
    /// <include path="members/LJCGetDouble/*" file="Doc/LJCDataColumns.xml"/>
    public double LJCGetDouble(string propertyName)
    {
      double retValue = default;

      var value = LJCGetString(propertyName);
      if (value != null)
      {
        retValue = Convert.ToDouble(value);
      }
      return retValue;
    }

    // Gets the column object value as a short int.
    /// <include path="members/LJCGetInt16/*" file="Doc/LJCDataColumns.xml"/>
    public short LJCGetInt16(string propertyName)
    {
      short retValue = default;

      var value = LJCGetString(propertyName);
      if (value != null)
      {
        retValue = Convert.ToInt16(value);
      }
      return retValue;
    }

    // Gets the column object value as an int.
    /// <include path="members/LJCGetInt32/*" file="Doc/LJCDataColumns.xml"/>
    public int LJCGetInt32(string propertyName)
    {
      int retValue = default;

      var value = LJCGetString(propertyName);
      if (value != null)
      {
        retValue = Convert.ToInt32(value);
      }
      return retValue;
    }

    // Gets the column object value as a long int.
    /// <include path="members/LJCGetInt64/*" file="Doc/LJCDataColumns.xml"/>
    public long LJCGetInt64(string propertyName)
    {
      long retValue = default;

      var value = LJCGetString(propertyName);
      if (value != null)
      {
        retValue = Convert.ToInt64(value);
      }
      return retValue;
    }

    // Gets the column object value as an object.
    /// <include path="members/LJCGetObject/*" file="Doc/LJCDataColumns.xml"/>
    public object? LJCGetObject(string propertyName)
    {
      object retValue = default;

      var dbColumn = LJCSearchPropertyName(propertyName);
      if (dbColumn != null
        && dbColumn.Value != null)
      {
        retValue = dbColumn.Value;
      }
      return retValue;
    }

    // Gets the column object value as a single.
    /// <include path="members/LJCGetSingle/*" file="Doc/LJCDataColumns.xml"/>
    public float LJCGetSingle(string propertyName)
    {
      float retValue = default;

      var value = LJCGetString(propertyName);
      if (value != null)
      {
        retValue = Convert.ToSingle(value);
      }
      return retValue;
    }

    // Gets the string value for the column with the specified name.
    /// <include path="members/LJCGetString/*" file="Doc/LJCDataColumns.xml"/>
    public string? LJCGetString(string propertyName)
    {
      string retValue = default;

      if (LJC.HasValue(propertyName))
      {
        var dataColumn = LJCSearchPropertyName(propertyName);
        if (dataColumn != null
          && dataColumn.Value != null
          && LJC.HasValue(dataColumn.Value.ToString()))
        {
          retValue = dataColumn.Value.ToString();
        }
      }
      return retValue;
    }

    // Update column value.
    /// <include path="members/LJCSetValue/*" file="Doc/LJCDataColumns.xml"/>
    public void LJCSetValue(string propertyName, object value)
    {
      if (LJC.HasItems(this)
        && LJC.HasValue(propertyName))
      {
        var dataColumn = LJCGetColumn(propertyName);
        if (dataColumn != null)
        {
          dataColumn.Value = value;
        }
      }
    }
    #endregion

    #region Properties

    /// <summary>Gets the Default File Name.</summary>
    public static string LJCDefaultFileName
    {
      get { return "DbColumns.xml"; }
    }

    // The column for the specified name.
    /// <include path="members/Item/*" file="Doc/LJCDataColumns.xml"/>
    public LJCDataColumn? this[string propertyName]
    {
      get { return LJCSearchPropertyName(propertyName); }
    }
    #endregion

    #region Class Data

    private int mPrevCount;
    private SortType mSortType;

    private enum SortType
    {
      AddOrderIndex,
      ColumnName,
      PropertyName,
      RenameAs
    }
    #endregion
  }
}
