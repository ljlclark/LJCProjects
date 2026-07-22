// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCDataColumns.cs
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace LJCNetCommon
{
  // Represents a collection of LJCDataColumn objects.
  /// <include file='Doc/LJCDataColumns.xml'
  ///  path='members/LJCDataColumns/*'/>
  [XmlRoot("LJCDataColumns")]
  public class LJCDataColumns : List<LJCDataColumn>
  {
    #region Static Methods

    // Deserializes from the specified XML file.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Collection.xml'
    ///  path='members/LJCDeserialize/*'/>
    public static LJCDataColumns LJCDeserialize(string fileSpec = null)
    {
      LJCDataColumns retValue;

      if (!NetString.HasValue(fileSpec))
      {
        fileSpec = LJCDefaultFileName;
      }
      retValue = NetCommon.XmlDeserialize(typeof(LJCDataColumns), fileSpec)
        as LJCDataColumns;
      return retValue;
    }

    // Gets a collection of items from a data object.
    /// <include file='Doc/LJCDataColumns.xml'
    ///  path='members/LJCGetObjectColumns/*'/>
    public static LJCDataColumns LJCGetObjectColumns(object dataObject
      , LJCDataColumns dataDefinition = null)
    {
      LJCDataColumn definitionColumn = null;
      LJCDataColumns retValue = null;

      LJCReflect reflect = new LJCReflect(dataObject);
      List<string> propertyNames = reflect.GetPropertyNames();

      if (propertyNames != null)
      {
        retValue = new LJCDataColumns();
        foreach (string propertyName in propertyNames)
        {
          if ("ChangedNames" == propertyName)
          {
            continue;
          }

          if (dataDefinition != null)
          {
            // *** Begin *** Change
            var keyColumns = LJCPropertyNameKeys(propertyName);
            definitionColumn = dataDefinition.LJCGetWith(keyColumns);
            // *** End ***
          }

          LJCDataColumn dataColumn = new LJCDataColumn()
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

    // Gets a list of property names from a data object.
    /// <include file='Doc/LJCDataColumns.xml'
    ///  path='members/LJCGetPropertyNames/*'/>
    public static List<string> LJCGetObjectPropertyNames(object dataObject)
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

    /// <include file='Doc/LJCDataColumns.xml'
    ///  path='items/GetPropertyNameKey/*'/>
    public static LJCDataColumns LJCPropertyNameKeys(string value)
    {
      var retKeys = new LJCDataColumns()
      {
        // KeyColumnName, ColumnValue
        { "PropertyName", (object)value },
      };
      return retKeys;
    }

    // Operator to create LJCDataValues from LJCDataColumns.
    /// <include file='Doc/LJCDataColumns.xml'
    ///  path='members/LJCDataValues/*'/>
    public static implicit operator LJCDataValues(LJCDataColumns dataColumns)
    {
      LJCDataValues retValue = null;

      if (NetCommon.HasItems(dataColumns))
      {
        retValue = new LJCDataValues();
        foreach (LJCDataColumn dataColumn in dataColumns)
        {
          var dataValue = dataColumn;
          retValue.Add(dataValue);
        }
      }
      return retValue;
    }
    #endregion

    #region Constructor Methods

    // Initializes an object instance.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'
    ///  path='members/DefaultConstructor/*'/>
    public LJCDataColumns()
    {
      _PrevCount = -1;
    }

    // Initializes an object from the supplied items.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Collection.xml'
    ///  path='members/CopyConstructor/*'/>
    public LJCDataColumns(LJCDataColumns items)
    {
      if (NetCommon.HasItems(items))
      {
        foreach (var item in items)
        {
          Add(new LJCDataColumn(item));
        }
      }
    }
    #endregion

    #region Collection Methods

    // Creates and returns a clone of the object.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'
    ///  path='members/Clone/*'/>
    public LJCDataColumns Clone()
    {
      var retValue = new LJCDataColumns();
      foreach (var dataColumn in this)
      {
        retValue.Add(dataColumn.Clone());
      }
      return retValue;
    }

    // Checks if the collection has items.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Collection.xml'
    ///  path='members/HasItems2/*'/>
    public bool HasItems()
    {
      bool retValue = false;

      if (Count > 0)
      {
        retValue = true;
      }
      return retValue;
    }

    // Sets the IsChanged value to false for all items.
    /// <include file='Doc/LJCDataColumns.xml'
    ///  path='items/LJCClearChanged/*'/>
    public void LJCClearChanged()
    {
      foreach (LJCDataColumn dataColumn in this)
      {
        dataColumn.IsChanged = false;
      }
    }

    // Gets a collection of changed columns.
    /// <include file='Doc/DbColumns.xml'
    ///  path='items/LJCGetChanged/*'/>
    public LJCDataColumns LJCGetChanged()
    {
      List<LJCDataColumn> dataColumns;
      var retValue = new LJCDataColumns();

      dataColumns = FindAll(x => x.IsChanged);
      foreach (LJCDataColumn dataColumn in dataColumns)
      {
        retValue.Add(dataColumn.Clone());
      }
      return retValue;
    }

    // Returns a collection of items that match a list of property names.
    /// <include file='Doc/LJCDataColumns.xml'
    ///  path='members/LJCGetColumns1/*'/>
    public LJCDataColumns LJCGetColumns(List<string> propertyNames)
    {
      LJCDataColumn searchColumn;
      LJCDataColumns retValue = null;

      if (NetCommon.HasItems(propertyNames))
      {
        retValue = new LJCDataColumns();
        foreach (string propertyName in propertyNames)
        {
          var searchName = NetString.GetSearchName(propertyName);
          // *** Begin *** Change
          //searchColumn = LJCGetWith(searchName);
          var keyColumns = LJCPropertyNameKeys(searchName);
          searchColumn = LJCGetWith(keyColumns);
          // *** End ***
          if (searchColumn != null)
          {
            retValue.Add(new LJCDataColumn(searchColumn));
          }
        }
      }
      return retValue;
    }

    // Returns a collection of items from the data object properties.
    /// <include file='Doc/LJCDataColumns.xml'
    ///  path='members/LJCGetColumns2/*'/>
    public static LJCDataColumns LJCGetColumns(object dataObject
      , List<string> propertyNames = null)
    {
      var retValue = LJCGetObjectColumns(dataObject);
      if (propertyNames != null)
      {
        retValue = retValue.LJCGetColumns(propertyNames);
      }
      return retValue;
    }

    // Gets a list of property names from the collection items.
    /// <include file='Doc/LJCDataColumns.xml'
    ///  path='members/LJCGetPropertyNames/*'/>
    public List<string> LJCPropertyNames(LJCDataColumns dataColumns = null)
    {
      List<string> retList = null;

      if (!NetCommon.HasItems(dataColumns))
      {
        dataColumns = this;
      }
      if (NetCommon.HasItems(dataColumns))
      {
        retList = new List<string>();
        foreach (LJCDataColumn dataColumn in dataColumns)
        {
          retList.Add(dataColumn.PropertyName);
        }
      }
      return retList;
    }

    // Serializes the collection
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Collection.xml'
    ///  path='members/LJCSerialize/*'/>
    public void LJCSerialize(string fileSpec = null)
    {
      if (!NetString.HasValue(fileSpec))
      {
        fileSpec = LJCDefaultFileName;
      }
      NetCommon.XmlSerialize(GetType(), this, null, fileSpec);
    }
    #endregion

    #region Collection Data Methods

    // Adds the supplied item to the collection
    /// <include file='Doc/LJCDataColumns.xml'
    ///  path='members/Add1/*'/>
    public new void Add(LJCDataColumn dataColumn)
    {
      base.Add(dataColumn);
      int newIndex = Count - 1;
      dataColumn.AddOrderIndex = newIndex;
    }

    // Creates item with the supplied values and adds it to the collection.
    /// <include file='Doc/LJCDataColumns.xml'
    ///  path='members/Add2/*'/>
    public LJCDataColumn Add(string propertyName, string columnName = null
      , string renameAs = null, string dataTypeName = "String"
      , string caption = null, int maxLength = 5)
    {
      var retValue = new LJCDataColumn()
      {
        PropertyName = propertyName,
        ColumnName = columnName,
        RenameAs = renameAs,
        DataTypeName = dataTypeName,
        Caption = caption,
        MaxLength = maxLength,

        AutoIncrement = false,
      };
      Add(retValue);
      return retValue;
    }

    // Creates item with Position and MaxLength and adds it to the collection.
    /// <include file='Doc/LJCDataColumns.xml'
    ///  path='members/Add3/*'/>
    public LJCDataColumn Add(string propertyName, int position, int maxLength)
    {
      LJCDataColumn retValue = new LJCDataColumn()
      {
        PropertyName = propertyName,
        Position = position,
        MaxLength = maxLength,

        AutoIncrement = false,
      };
      Add(retValue);
      return retValue;
    }

    // Creates item with Value and adds it to the collection.
    // Use (object) cast with a string value to use this overload.
    /// <include file='Doc/LJCDataColumns.xml'
    ///  path='members/Add4/*'/>
    public LJCDataColumn Add(string propertyName, object value
      , string dataTypeName = "String", int maxLength = 5)
    {
      var retValue = new LJCDataColumn()
      {
        PropertyName = propertyName,
        Value = value,
        DataTypeName = dataTypeName,
        MaxLength = maxLength,

        AutoIncrement = false,
      };
      Add(retValue);
      return retValue;
    }

    // Creates item with Caption and RenameAs and adds to the collection.
    /// <include file='Doc/LJCDataColumns.xml'
    ///  path='members/LJCAddPropertyAs/*'/>
    public LJCDataColumn LJCAddPropertyAs(string propertyName, string caption = null
      , string renameAs = null, string dataTypeName = "String")
    {
      var retValue = new LJCDataColumn()
      {
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

    /// <include file='Doc/LJCDataColumns.xml'
    ///  path='members/LJCAddValue/*'/>
    public LJCDataColumn LJCAddValue(string propertyName, object value
      , string dataTypeName = "String", int maxLength = 5)
    {
      var retValue = Add(propertyName, value, dataTypeName, maxLength);
      return retValue;
    }

    // Removes the item with the supplied property name.
    /// <include file='Doc/LJCDataColumns.xml'
    ///  path='members/LJCRemove/*'/>
    public void LJCRemove(string propertyName)
    {
      LJCDataColumn column = Find(x => x.PropertyName == propertyName);
      if (column != null)
      {
        Remove(column);
      }
    }

    // Add or Update.
    /// <include file='Doc/LJCDataColumns.xml'
    ///  path='members/LJCSetData/*'/>
    public void LJCSetData(LJCDataColumn dataColumn)
    {
      if (NetCommon.HasItems(this))
      {
        // *** Begin *** Change
        //var updateColumn = LJCGetWith(dataColumn.PropertyName);
        var keyColumns = LJCPropertyNameKeys(dataColumn.PropertyName);
        var updateColumn = LJCGetWith(keyColumns);
        // *** End ***
        if (updateColumn != null)
        {
          updateColumn.AllowDBNull = dataColumn.AllowDBNull;
          updateColumn.AutoIncrement = dataColumn.AutoIncrement;
          updateColumn.Caption = dataColumn.Caption;
          updateColumn.ColumnName = dataColumn.ColumnName;
          updateColumn.DataTypeName = dataColumn.DataTypeName;
          updateColumn.MaxLength = dataColumn.MaxLength;
          updateColumn.Position = dataColumn.Position;
          updateColumn.PropertyName = dataColumn.PropertyName;
          updateColumn.RenameAs = dataColumn.RenameAs;
          updateColumn.SQLTypeName = dataColumn.SQLTypeName;
          updateColumn.Value = dataColumn.Value;
        }
        else
        {
          Add(dataColumn);
        }
      }
    }
    #endregion

    #region Custom Data Methods

    // Returns the item with the supplied property name.
    /// <include file='Doc/LJCDataColumns.xml'
    ///  path='items/LJCGetWithPropertyName/*'/>
    //public LJCDataColumn LJCGetWith(string propertyName, object value = null)
    public LJCDataColumn LJCGetWith(LJCDataColumns keyColumns)
    {
      LJCDataColumn retValue = null;

      LJCKeyColumns = keyColumns;
      LJCSort();

      // Create search item.
      var dataColumn = new LJCDataColumn();
      var reflect = new LJCReflect(dataColumn);
      foreach (var keyColumn in keyColumns)
      {
        reflect.SetValue(keyColumn.ColumnName, keyColumn.Value);
      }

      // Create comparer.
      var propertyNames = LJCPropertyNames(keyColumns);
      var comparer = new DataColumnKeyComparer()
      {
        LJCPropertyNames = propertyNames,
      };

      int index = BinarySearch(dataColumn, comparer);
      if (index > -1)
      {
        retValue = this[index];
      }
      return retValue;
    }
    #endregion

    #region Sort Methods

    // Sorts on the supplied property names.
    /// <include file='Doc/LJCDataRows.xml'
    ///  path='members/LJCSort/*'/>
    public void LJCSort()
    {
      if (_IsPendingSort)
      {
        var sortNames = LJCPropertyNames(_KeyColumns);
        var uniqueComparer = new DataColumnKeyComparer
        {
          LJCPropertyNames = sortNames
        };
        Sort(uniqueComparer);
      }
      _IsPendingSort = false;
    }

    // Checks if the key columns value has changed.
    private bool IsKeyColumnsChanged(LJCDataColumns newKeyColumns
      , LJCDataColumns currentKeyColumns)
    {
      bool retValue = false;

      while (true)
      {
        var hasNewColumns = NetCommon.HasItems(newKeyColumns);
        var hasSortColumns = NetCommon.HasItems(currentKeyColumns);

        // One value has no columns.
        if ((!hasNewColumns
          && hasSortColumns)
          || hasNewColumns
          && !hasSortColumns)
        {
          retValue = true;
          break;
        }

        if (hasNewColumns)
        {
          if (newKeyColumns.Count != currentKeyColumns.Count)
          {
            retValue = true;
            break;
          }

          for (short index = 0; index < newKeyColumns.Count; index++)
          {
            var newColumn = newKeyColumns[index];
            var currentColumn = currentKeyColumns[index];

            var propertyName = newColumn.PropertyName;
            var propertyValue = newColumn.Value;
            var sortPropertyName = currentColumn.PropertyName;
            var sortPropertyValue = currentColumn.Value;
            if (propertyName.CompareTo(sortPropertyName) != 0
              || !EqualityComparer<object>.Default.Equals(propertyValue
              , sortPropertyValue))
            {
              retValue = true;
              break;
            }
          }
        }
        break;
      }
      return retValue;
    }
    #endregion

    #region Other Public Methods

    // Get the minimum date value.
    /// <include file='Doc/LJCDataColumns.xml'
    ///  path='items/LJCGetMinSqlDate/*'/>
    public static string LJCGetMinSqlDate()
    {
      return "1753/01/01 00:00:00";
    }

    // Sets the caption properties.
    /// <include file='Doc/LJCDataColumns.xml'
    ///  path='items/LJCSetColumnCaptions/*'/>
    public void LJCSetColumnCaptions(LJCDataColumns dataColumns)
    {
      LJCDataColumn searchColumn;

      if (NetCommon.HasItems(dataColumns))
      {
        foreach (var dataColumn in dataColumns)
        {
          // *** Begin *** Change
          //searchColumn = LJCGetWith(dataColumn.PropertyName);
          var keyColumns = LJCPropertyNameKeys(dataColumn.PropertyName);
          searchColumn = LJCGetWith(keyColumns);
          // *** End ***
          if (searchColumn != null)
          {
            dataColumn.Caption = searchColumn.Caption;
          }
        }
      }
    }

    // Maps the column property and rename values.
    /// <include file='Doc/LJCDataColumns.xml'
    ///  path='items/LJCMapNames/*'/>
    public void LJCMapNames(string columnName, string propertyName = null
      , string renameAs = null, string caption = null)
    {
      // *** Begin *** Change
      //var dataColumn = LJCGetWith(columnName);
      var keyColumns = LJCPropertyNameKeys(columnName);
      var dataColumn = LJCGetWith(keyColumns);
      // *** End ***
      SetMapValues(dataColumn, propertyName, renameAs, caption);
    }

    // Sets the Map values.
    private void SetMapValues(LJCDataColumn dataColumn
      , string propertyName = null, string renameAs = null
      , string caption = null)
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

    #region Value Methods

    // Gets the column object value as a bool.
    /// <include file='Doc/LJCDataColumns.xml'
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
    /// <include file='Doc/LJCDataColumns.xml'
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
    /// <include file='Doc/LJCDataColumns.xml'
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
    /// <include file='Doc/LJCDataColumns.xml'
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
    /// <include file='Doc/LJCDataColumns.xml'
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
    /// <include file='Doc/LJCDataColumns.xml'
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
    /// <include file='Doc/LJCDataColumns.xml'
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
    /// <include file='Doc/LJCDataColumns.xml'
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
    /// <include file='Doc/LJCDataColumns.xml'
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

    // Gets the column object value as a single.
    /// <include file='Doc/LJCDataColumns.xml'
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

    // Gets the column object value as a string.
    /// <include file='Doc/LJCDataColumns.xml'
    ///  path='items/LJCGetString/*'/>
    public string LJCGetString(string propertyName)
    {
      string retValue = default;

      if (NetString.HasValue(propertyName))
      {
        // Get with unique compare values.
        var keyColumns = LJCPropertyNameKeys(propertyName);
        var dataColumn = LJCGetWith(keyColumns);

        if (dataColumn != null
          && dataColumn.Value != null
          && NetString.HasValue(dataColumn.Value.ToString()))
        {
          retValue = dataColumn.Value.ToString();
        }
      }
      return retValue;
    }

    // Gets the column object value.
    /// <include file='Doc/LJCDataColumns.xml'
    ///  path='items/LJCGetValue/*'/>
    public object LJCGetValue(string propertyName)
    {
      object retValue = default;

      if (NetCommon.HasItems(this)
        && NetString.HasValue(propertyName))
      {
        // *** Begin *** Change
        //var dataColumn = LJCGetWith(propertyName);
        var keyColumns = LJCPropertyNameKeys(propertyName);
        var dataColumn = LJCGetWith(keyColumns);
        // *** End ***
        if (dataColumn != null
          && dataColumn.Value != null)
        {
          retValue = dataColumn.Value;
        }
      }
      return retValue;
    }

    // Sets the column object value.
    /// <include file='Doc/LJCDataColumns.xml'
    ///  path='items/LJCSetValue/*'/>
    public void LJCSetValue(string propertyName, object value)
    {
      if (NetCommon.HasItems(this)
        && NetString.HasValue(propertyName))
      {
        // *** Begin *** Change
        //var dataColumn = LJCGetWith(propertyName);
        var keyColumns = LJCPropertyNameKeys(propertyName);
        var dataColumn = LJCGetWith(keyColumns);
        // *** End ***
        if (dataColumn != null)
        {
          dataColumn.Value = value;
        }
      }
    }
    #endregion

    #region Properties

    // Gets the Default File Name.
    /// <include file='Doc/LJCDataColumns.xml'
    ///  path='items/LJCDefaultFileName/*'/>
    public static string LJCDefaultFileName
    {
      get { return "DbColumns.xml"; }
    }

    // Gets or sets the key columns.
    /// <include file='Doc/LJCDataColumns.xml'
    ///  path='members/LJCKeyColumns/*'/>
    public LJCDataColumns LJCKeyColumns
    {
      get => _KeyColumns;
      set
      {
        if (IsKeyColumnsChanged(value, _KeyColumns))
        {
          _IsPendingSort = true;
        }
        // Must be done after check for changes.
        _KeyColumns = value;

        // New sort if count has changed.
        if (Count != _PrevCount)
        {
          _IsPendingSort = true;
          _PrevCount = Count;
        }
      }
    }
    private LJCDataColumns _KeyColumns;

    // Returns the item with the supplied property name.
    /// <include file='Doc/LJCDataColumns.xml'
    ///  path='items/Item/*'/>
    public LJCDataColumn this[string propertyName]
    {
      get
      {
        // *** Begin *** Change
        //return LJCGetWith(propertyName);
        var keyColumns = LJCPropertyNameKeys(propertyName);
        return LJCGetWith(keyColumns);
        // *** End ***
      }
    }
    #endregion

    #region Class Data

    private bool _IsPendingSort;
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

  // Sort and search on key values.
  /// <include file='Doc/LJCDataColumns.xml'
  ///  path='members/DataColumnKeyComparer/*'/>
  public class DataColumnKeyComparer : IComparer<LJCDataColumn>
  {
    /// <include file='Doc/LJCDataColumns.xml'
    ///  path='members/ColumnNames/*'/>
    public List<string> LJCPropertyNames { get; set; }

    // Compares two objects.
    /// <include file='Doc/LJCDataColumns.xml'
    ///  path='items/Compare/*'/>
    public int Compare(LJCDataColumn x, LJCDataColumn y)
    {
      int retValue;

      // Check for null objects.
      retValue = NetCommon.CompareNull(x, y);

      while (true)
      {
        // End if one of the objects is null.
        if (null == LJCPropertyNames
          || retValue != NetString.CompareNotNullOrEqual)
        {
          break;
        }

        LJCReflect xReflect = new LJCReflect(x);
        LJCReflect yReflect = new LJCReflect(y);

        // Check for null values.
        foreach (string propertyName in LJCPropertyNames)
        {
          var xValue = xReflect.GetString(propertyName);
          var yValue = yReflect.GetString(propertyName);
          retValue = NetCommon.CompareNull(xValue, yValue);

          // Break if one of the values is null.
          if (retValue != NetString.CompareNotNullOrEqual)
          {
            break;
          }
        }

        // End if one of the values is null.
        if (retValue != NetString.CompareNotNullOrEqual)
        {
          break;
        }

        for (int index = 0; index < LJCPropertyNames.Count; index++)
        {
          var propertyName = LJCPropertyNames[index];
          var xValue = xReflect.GetString(propertyName);
          var yValue = yReflect.GetString(propertyName);

          if (index < LJCPropertyNames.Count - 1)
          {
            // Compare parent keys.
            retValue = xValue.CompareTo(yValue);
            if (retValue != NetString.CompareEqual)
            {
              break;
            }
          }
          else
          {
            // Compare value if parent keys are equal.
            retValue = xValue.CompareTo(yValue);
          }
        }
        break;
      }
      return retValue;
    }
  }
}
