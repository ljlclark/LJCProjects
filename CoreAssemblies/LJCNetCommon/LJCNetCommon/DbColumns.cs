// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DbColumns.cs
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Xml.Serialization;

namespace LJCNetCommon
{
  // Represents a collection of DbColumn objects.
  /// <include file='Doc/DbColumns.xml'
  ///  path='items/DbColumns/*'/>
  [XmlRoot("DbColumns")]
  public class DbColumns : List<DbColumn>
  {
    #region Static Functions

    /// <summary>Compare column value to key column value.</summary>
    /// <include file='Doc/DbColumns.xml'
    ///  path='members/LJCCompareColumn/*'/>
    public static int LJCCompareColumn(string columnValue, DbColumn keyColumn
      , bool ignoreCase = true)
    {
      string searchValue = null;
      if (keyColumn.Value != null)
      {
        searchValue = keyColumn.Value.ToString();
      }
      int retIndex = string.Compare(columnValue, searchValue, ignoreCase);
      return retIndex;
    }

    // Creates DbColumns from a Data Object.
    /// <include file='Doc/DbColumns.xml'
    ///  path='items/LJCCreateObjectColumns/*'/>
    public static DbColumns LJCCreateObjectColumns(object dataObject
      , DbColumns dataDefinition = null)
    {
      DbColumn definitionColumn = null;
      DbColumns retValue = null;

      LJCReflect reflect = new LJCReflect(dataObject);
      List<string> propertyNames = reflect.GetPropertyNames();

      if (propertyNames != null)
      {
        retValue = new DbColumns();
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

          DbColumn dbColumn = new DbColumn()
          {
            Caption = propertyName,
            ColumnName = propertyName,
            Value = reflect.GetValue(propertyName)
          };

          Type type = reflect.GetPropertyType(propertyName);
          if (type != null)
          {
            dbColumn.DataTypeName = type.Name;
            if (definitionColumn != null
              && "String" == type.Name)
            {
              dbColumn.MaxLength = definitionColumn.MaxLength;
            }
          }
          retValue.Add(dbColumn);
        }
      }
      return retValue;
    }

    // Deserializes from the specified XML file.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Collection.xml'
    ///  path='items/LJCDeserialize/*'/>
    public static DbColumns LJCDeserialize(string fileSpec = null)
    {
      DbColumns retValue;

      if (!NetString.HasValue(fileSpec))
      {
        fileSpec = LJCDefaultFileName;
      }
      retValue = NetCommon.XmlDeserialize(typeof(DbColumns), fileSpec)
        as DbColumns;
      return retValue;
    }

    // Creates a ColumnNames list from a DataObject.
    /// <include file='Doc/DbColumns.xml'
    ///  path='items/LJCGetPropertyNames/*'/>
    public static List<string> LJCGetPropertyNames(object dataObject)
    {
      List<string> retValue = null;

      LJCReflect reflect = new LJCReflect(dataObject);
      List<string> propertyNames = reflect.GetPropertyNames();

      if (propertyNames != null)
      {
        retValue = new List<string>();
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

    // Custom binary search for Name value.
    /// <include file='Doc/DbColumns.xml'
    ///  path='members/LJCSearchColumns/*'/>
    public static int LJCSearchColumns<T>(List<T> list, DbColumns keyColumns)
    {
      int retIndex = -1;

      int leftIndex = 0;
      int rightIndex = list.Count - 1;
      while (leftIndex <= rightIndex)
      {
        // Get the midpoint.
        int middleIndex = leftIndex + (rightIndex - leftIndex) / 2;

        // Get the object compare value.
        var dbColumns = list[middleIndex] as DbColumns;

        int compareValue = NetString.CompareGreater;
        for (short index = 0; index < keyColumns.Count; index++)
        {
          var keyColumn = keyColumns[index];
          var propertyName = keyColumn.PropertyName;
          var columnValue = dbColumns.LJCGetString(propertyName);
          compareValue = LJCCompareColumn(columnValue, keyColumn);
          if (index < keyColumns.Count - 1)
          {
            // Parent key value is not equal.
            if (compareValue != NetString.CompareEqual)
            {
              break;
            }
          }
          else
          {
            // Item key value is equal.
            if (NetString.CompareEqual == compareValue)
            {
              retIndex = middleIndex;
            }
          }
        }

        // DbColumns item was found.
        if (NetString.CompareEqual == compareValue)
        {
          break;
        }

        if (NetString.CompareLess == compareValue)
        {
          // Eliminate left half
          leftIndex = middleIndex + 1;
        }
        else
        {
          // Eliminate right half
          rightIndex = middleIndex - 1;
        }
      }
      return retIndex;
    }

    // Creates a DbValues object from a DbColumns object.
    /// <include file='Doc/DbColumns.xml'
    ///  path='items/DbValues/*'/>
    public static implicit operator DbValues(DbColumns dbColumns)
    {
      DbValues retValue = null;

      if (NetCommon.HasItems(dbColumns))
      {
        retValue = new DbValues();
        foreach (DbColumn dbColumn in dbColumns)
        {
          var dbValue = dbColumn;
          retValue.Add(dbValue);
        }
      }
      return retValue;
    }
    #endregion

    #region Constructors

    // Initializes an object instance.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'
    ///  path='items/DefaultConstructor/*'/>
    public DbColumns()
    {
      _PrevCount = -1;
    }

    // The Copy constructor.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Collection.xml'
    ///  path='items/CopyConstructor/*'/>
    public DbColumns(DbColumns items)
    {
      if (NetCommon.HasItems(items))
      {
        foreach (var item in items)
        {
          Add(new DbColumn(item));
        }
      }
    }
    #endregion

    #region Collection Methods

    // Adds the object element to the collection
    /// <include file='Doc/DbColumns.xml'
    ///  path='items/Add/*'/>
    public new void Add(DbColumn dbColumn)
    {
      LJCSortAddOrderIndex();
      base.Add(dbColumn);
      int newIndex = Count - 1;
      dbColumn.AddOrderIndex = newIndex;
    }

    // Creates the Object from the arguments and adds it to the collection. (R)
    /// <include file='Doc/DbColumns.xml'
    ///  path='items/Add1/*'/>
    public DbColumn Add(string columnName, int position, int maxLength)
    {
      DbColumn retValue = new DbColumn()
      {
        ColumnName = columnName,
        Position = position,
        MaxLength = maxLength,

        AutoIncrement = false,
      };
      Add(retValue);
      return retValue;
    }

    // Creates the Object from the arguments and adds it to the collection.
    /// <include file='Doc/DbColumns.xml'
    ///  path='items/Add2/*'/>
    public DbColumn Add(string columnName, string propertyName = null
      , string renameAs = null, string dataTypeName = "String"
      , string caption = null, int maxLength = 5)
    {
      DbColumn retValue = new DbColumn()
      {
        ColumnName = columnName,
        PropertyName = propertyName,
        RenameAs = renameAs,
        DataTypeName = dataTypeName,
        Caption = caption,
        MaxLength = maxLength,

        AutoIncrement = false,
      };
      Add(retValue);
      return retValue;
    }

    // Creates the Object from the arguments and adds it to the collection.
    // Use (object) cast with a string value to use this overload.
    /// <include file='Doc/DbColumns.xml'
    ///  path='items/Add3/*'/>
    public DbColumn Add(string columnName, object value
      , string dataTypeName = "String", int maxLength = 5)
    {
      DbColumn retValue = new DbColumn()
      {
        ColumnName = columnName,
        Value = value,
        DataTypeName = dataTypeName,
        MaxLength = maxLength,

        AutoIncrement = false,
      };
      Add(retValue);
      return retValue;
    }

    // Creates the DbColumn from the supplied values and adds to the collection.
    /// <include file='Doc/DbColumns.xml'
    ///  path='items/LJCAddPropertyAs/*'/>
    public DbColumn LJCAddPropertyAs(string propertyName, string caption = null
      , string renameAs = null, string dataTypeName = "String")
    {
      DbColumn retValue = new DbColumn()
      {
        ColumnName = propertyName,
        PropertyName = propertyName,
        Caption = caption,
        RenameAs = renameAs,
        DataTypeName = dataTypeName,

        AutoIncrement = false,
        Value = null,
      };

      if (!NetString.HasValue(renameAs))
      {
        retValue.RenameAs = retValue.PropertyName;
      }
      Add(retValue);
      return retValue;
    }

    // Creates and returns a clone of the object.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'
    ///  path='items/Clone/*'/>
    public DbColumns Clone()
    {
      var retValue = new DbColumns();
      foreach (DbColumn dbColumn in this)
      {
        retValue.Add(dbColumn.Clone());
      }
      return retValue;
    }

    // Checks if the collection has items.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Collection.xml'
    ///  path='items/HasItems2/*'/>
    public bool HasItems()
    {
      bool retValue = false;

      if (Count > 0)
      {
        retValue = true;
      }
      return retValue;
    }

    // Returns a column by property name.
    /// <include file='Doc/DbColumns.xml'
    ///  path='items/LJCGetColumn/*'/>
    public DbColumn LJCGetColumn(string propertyName)
    {
      DbColumn retValue = null;

      if (Count != _PrevCount
        || _SortType.CompareTo(SortType.PropertyName) != 0)
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
    /// <include file='Doc/DbColumns.xml'
    ///  path='items/LJCGetColumns/*'/>
    public DbColumns LJCGetColumns(List<string> propertyNames)
    {
      DbColumn searchColumn;
      DbColumns retValue = null;

      if (NetCommon.HasItems(propertyNames))
      {
        retValue = new DbColumns();
        foreach (string propertyName in propertyNames)
        {
          // *** Begin *** Change - 10/16/23
          var searchName = NetString.GetSearchName(propertyName);
          searchColumn = LJCSearchPropertyName(searchName);
          // *** End   *** Change - 10/16/23
          if (searchColumn != null)
          {
            retValue.Add(new DbColumn(searchColumn));
          }
        }
      }
      return retValue;
    }

    // Configure the Grid Columns from the Data object properties.
    /// <include file='Doc/DbColumns.xml'
    ///  path='items/LJCGetColumns2/*'/>
    public static DbColumns LJCGetColumns(object dataObject
      , List<string> propertyNames = null)
    {
      var retValue = LJCCreateObjectColumns(dataObject);
      if (propertyNames != null)
      {
        retValue = retValue.LJCGetColumns(propertyNames);
      }
      return retValue;
    }

    /// <summary>Get the list of property names.</summary>
    public List<string> LJCGetPropertyNames()
    {
      List<string> retValue = null;

      if (NetCommon.HasItems(this))
      {
        retValue = new List<string>();
        foreach (DbColumn dbColumn in this)
        {
          retValue.Add(dbColumn.PropertyName);
        }
      }
      return retValue;
    }

    // Removes a DbColumn item.
    /// <include file='Doc/DbColumns.xml'
    ///  path='items/LJCRemoveColumn/*'/>
    public void LJCRemoveColumn(string columnName)
    {
      DbColumn column = Find(x => x.ColumnName == columnName);
      if (column != null)
      {
        Remove(column);
      }
    }

    // Serializes the collection
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Collection.xml'
    ///  path='items/LJCSerialize/*'/>
    public void LJCSerialize(string fileSpec = null)
    {
      if (!NetString.HasValue(fileSpec))
      {
        fileSpec = LJCDefaultFileName;
      }
      NetCommon.XmlSerialize(GetType(), this, null, fileSpec);
    }

    /// <summary>Add or Update.</summary>
    /// <param name="dbColumn">The DbColumn object.</param>
    public void LJCSetData(DbColumn dbColumn)
    {
      if (NetCommon.HasItems(this))
      {
        var dataColumn = LJCGetColumn(dbColumn.PropertyName);
        if (dataColumn != null)
        {
          dataColumn.AllowDBNull = dbColumn.AllowDBNull;
          dataColumn.AutoIncrement = dbColumn.AutoIncrement;
          dataColumn.Caption = dbColumn.Caption;
          dataColumn.ColumnName = dbColumn.ColumnName;
          dataColumn.DataTypeName = dbColumn.DataTypeName;
          dataColumn.MaxLength = dbColumn.MaxLength;
          dataColumn.Position = dbColumn.Position;
          dataColumn.PropertyName = dbColumn.PropertyName;
          dataColumn.RenameAs = dbColumn.RenameAs;
          dataColumn.SQLTypeName = dbColumn.SQLTypeName;
          dataColumn.Value = dbColumn.Value;
        }
        else
        {
          Add(dbColumn);
        }
      }
    }
    #endregion

    #region Data Methods

    // Sets the IsChanged value to false for all elements in the collection.
    /// <include file='Doc/DbColumns.xml'
    ///  path='items/LJCClearChanged/*'/>
    public void LJCClearChanged()
    {
      foreach (DbColumn dbColumn in this)
      {
        dbColumn.IsChanged = false;
      }
    }

    // Gets a collection of changed columns.
    /// <include file='Doc/DbColumns.xml'
    ///  path='items/LJCGetChanged/*'/>
    public DbColumns LJCGetChanged()
    {
      List<DbColumn> columns;
      DbColumns retValue = new DbColumns();

      columns = FindAll(x => x.IsChanged);
      foreach (DbColumn dbColumn in columns)
      {
        retValue.Add(dbColumn.Clone());
      }
      return retValue;
    }
    #endregion

    #region Search and Sort Methods

    // Finds and returns the object that matches the supplied values.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Collection.xml'
    ///  path='items/LJCSearchName/*'/>
    public DbColumn LJCSearchColumnName(string name)
    {
      DbColumnNameComparer comparer;
      DbColumn retValue = null;

      comparer = new DbColumnNameComparer();
      LJCSortName(comparer);

      DbColumn searchDbColumn = new DbColumn()
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
    /// <include file='Doc/DbColumns.xml'
    ///  path='items/LJCSearchPropertyName/*'/>
    public DbColumn LJCSearchPropertyName(string propertyName)
    {
      DbColumn retValue = null;

      var comparer = new DbColumnPropertyComparer();
      LJCSortProperty(comparer);
      DbColumn searchDbColumn = new DbColumn()
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
    /// <include file='Doc/DbColumns.xml'
    ///  path='items/LJCSearchRenameAs/*'/>
    public DbColumn LJCSearchRenameAs(string renameAs)
    {
      DbColumnRenameAsComparer comparer;
      DbColumn retValue = null;

      comparer = new DbColumnRenameAsComparer();
      LJCSortRenameAs(comparer);

      DbColumn searchDbColumn = new DbColumn()
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

    /// <summary>Sort on AddOrderIndex.</summary>
    public void LJCSortAddOrderIndex()
    {
      if (Count != _PrevCount
        || _SortType.CompareTo(SortType.AddOrderIndex) != 0)
      {
        _PrevCount = Count;
        Sort();
        _SortType = SortType.AddOrderIndex;
      }
    }

    /// <summary>Sort on ColumnName.</summary>
    /// <param name="comparer">The comparer function.</param>
    public void LJCSortName(DbColumnNameComparer comparer)
    {
      if (Count != _PrevCount
        || _SortType.CompareTo(SortType.ColumnName) != 0)
      {
        _PrevCount = Count;
        Sort(comparer);
        _SortType = SortType.ColumnName;
      }
    }

    /// <summary>Sort on RenameAs.</summary>
    /// <param name="comparer">The comparer function.</param>
    public void LJCSortProperty(DbColumnPropertyComparer comparer)
    {
      if (Count != _PrevCount
        || _SortType.CompareTo(SortType.PropertyName) != 0)
      {
        _PrevCount = Count;
        Sort(comparer);
        _SortType = SortType.PropertyName;
      }
    }

    /// <summary>Sort on RenameAs.</summary>
    /// <param name="comparer">The comparer function.</param>
    public void LJCSortRenameAs(DbColumnRenameAsComparer comparer)
    {
      if (Count != _PrevCount
        || _SortType.CompareTo(SortType.RenameAs) != 0)
      {
        _PrevCount = Count;
        Sort(comparer);
        _SortType = SortType.RenameAs;
      }
    }
    #endregion

    #region Other Public Methods

    // Get the minimum date value.
    /// <include file='Doc/DbColumns.xml'
    ///  path='items/LJCGetMinSqlDate/*'/>
    public static string LJCGetMinSqlDate()
    {
      return "1753/01/01 00:00:00";
    }

    // Sets the caption properties.
    /// <include file='Doc/DbColumns.xml'
    ///  path='items/LJCSetColumnCaptions/*'/>
    public void LJCSetColumnCaptions(DbColumns dbColumns)
    {
      DbColumn searchColumn;

      if (NetCommon.HasItems(dbColumns))
      {
        foreach (DbColumn dbColumn in dbColumns)
        {
          searchColumn = LJCSearchPropertyName(dbColumn.PropertyName);
          if (searchColumn != null)
          {
            dbColumn.Caption = searchColumn.Caption;
          }
        }
      }
    }

    // Maps the column property and rename values.
    /// <include file='Doc/DbColumns.xml'
    ///  path='items/LJCMapNames/*'/>
    public void LJCMapNames(string columnName, string propertyName = null
      , string renameAs = null, string caption = null)
    {
      DbColumn dbColumn = LJCSearchColumnName(columnName);
      SetMapValues(dbColumn, propertyName, renameAs, caption);
    }

    // Sets the Map values.
    private void SetMapValues(DbColumn dbColumn, string propertyName = null
      , string renameAs = null, string caption = null)
    {
      if (dbColumn != null)
      {
        if (propertyName != null)
        {
          dbColumn.PropertyName = propertyName;
        }
        if (renameAs != null)
        {
          dbColumn.RenameAs = renameAs;
        }
        if (caption != null)
        {
          dbColumn.Caption = caption;
        }
      }
    }
    #endregion

    #region Value Methods

    // Gets the column object value as a bool.
    /// <include file='Doc/DbColumns.xml'
    ///  path='items/LJCGetBoolean/*'/>
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
    /// <include file='Doc/DbColumns.xml'
    ///  path='items/LJCGetByte/*'/>
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
    /// <include file='Doc/DbColumns.xml'
    ///  path='items/LJCGetChar/*'/>
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
    /// <include file='Doc/DbColumns.xml'
    ///  path='items/LJCGetDbDateTime/*'/>
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
    /// <include file='Doc/DbColumns.xml'
    ///  path='items/LJCGetDecimal/*'/>
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
    /// <include file='Doc/DbColumns.xml'
    ///  path='items/LJCGetDouble/*'/>
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
    /// <include file='Doc/DbColumns.xml'
    ///  path='items/LJCGetInt16/*'/>
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
    /// <include file='Doc/DbColumns.xml'
    ///  path='items/LJCGetInt32/*'/>
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
    /// <include file='Doc/DbColumns.xml'
    ///  path='items/LJCGetInt64/*'/>
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
    /// <include file='Doc/DbColumns.xml'
    ///  path='items/LJCGetObject/*'/>
    public object LJCGetObject(string propertyName)
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
    /// <include file='Doc/DbColumns.xml'
    ///  path='items/LJCGetSingle/*'/>
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
    /// <include file='Doc/DbColumns.xml'
    ///  path='items/LJCGetString/*'/>
    public string LJCGetString(string propertyName)
    {
      string retValue = default;

      if (NetString.HasValue(propertyName))
      {
        var dbColumn = LJCSearchPropertyName(propertyName);
        if (dbColumn != null
          && dbColumn.Value != null
          && NetString.HasValue(dbColumn.Value.ToString()))
        {
          retValue = dbColumn.Value.ToString();
        }
      }
      return retValue;
    }

    // Get the column value.
    /// <include file='Doc/DbColumns.xml'
    ///  path='items/LJCGetValue/*'/>
    public object LJCGetValue(string propertyName)
    {
      object retValue = default;

      if (NetCommon.HasItems(this)
        && NetString.HasValue(propertyName))
      {
        var dataColumn = LJCGetColumn(propertyName);
        if (dataColumn != null)
        {
          retValue = dataColumn.Value;
        }
      }
      return retValue;
    }

    // Update column value.
    /// <include file='Doc/DbColumns.xml'
    ///  path='items/LJCSetValue/*'/>
    public void LJCSetValue(string propertyName, object value)
    {
      if (NetCommon.HasItems(this)
        && NetString.HasValue(propertyName))
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
    /// <include file='Doc/DbColumns.xml'
    ///  path='items/Item/*'/>
    public DbColumn this[string propertyName]
    {
      get { return LJCSearchPropertyName(propertyName); }
    }
    #endregion

    #region Class Data

    private int _PrevCount;
    private SortType _SortType;

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
