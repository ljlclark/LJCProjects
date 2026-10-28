// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCDataValues.cs
using System.Xml.Serialization;

namespace LJCNetCommon5
{
  // Represents a collection of LJCDataValue objects.
  /// <include file='Doc/DataValues.xml'
  ///  path='members/LJCDataValues/*'/>
  /// <group name="static">Static Methods</group>
  /// <group name="constructors">Constructors</group>
  /// <group name="collection">Collection Methods</group>
  /// <group name="item">Item Methods</group>
  /// <group name="search">Search and Sort Methods</group>
  /// <group name="value">Value Methods</group>
  [XmlRoot("LJCDataValues")]
  public class LJCDataValues : List<LJCDataValue>
  {
    #region Static Methods

    // Deserializes from the specified XML file.
    /// <include file='../../../LJCUtilities/LJCGenDoc5/Common/Collection.xml'
    ///  path='members/LJCDeserialize/*'/>
    public static LJCDataValues? LJCDeserialize(string? fileSpec = null)
    {
      LJCDataValues retValue;

      if (!LJC.HasText(fileSpec))
      {
        fileSpec = LJCDefaultFileName;
      }
      retValue = LJC.XmlDeserialize(typeof(LJCDataValues), fileSpec)
        as LJCDataValues;
      return retValue;
    }

    // Checks if the key columns value has changed.
    private static bool IsKeyColumnsChanged(LJCDataValues? newKeys
      , LJCDataValues? currentKeys)
    {
      bool retValue = false;

      while (true)
      {
        var hasNewColumns = LJC.HasListItems(newKeys);
        var hasSortColumns = LJC.HasListItems(currentKeys);

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
          if (newKeys!.Count != currentKeys!.Count)
          {
            retValue = true;
            break;
          }

          for (short index = 0; index < newKeys.Count; index++)
          {
            var newColumn = newKeys[index];
            var currentColumn = currentKeys[index];

            var propertyName = newColumn.PropertyName;
            var propertyValue = newColumn.Value;
            var sortPropertyName = currentColumn.PropertyName;
            var sortPropertyValue = currentColumn.Value;
            // Property name or value changed.
            if (!LJC.IsEqual(propertyName, sortPropertyName)
              || !LJC.IsEqual(propertyValue, sortPropertyValue))
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

    #region Properties

    // Gets the Default File Name.
    /// <include file='Doc/DataValues.xml'
    ///  path='members/LJCDefaultFileName/*'/>
    public static string LJCDefaultFileName
    {
      get => "LJCDataValues.xml";
    }

    // Gets or sets the key columns.
    /// <include file='Doc/DataValues.xml'
    ///  path='members/LJCKeyColumns/*'/>
    public LJCDataValues? LJCKeys
    {
      get => _Keys;
      set
      {
        if (IsKeyColumnsChanged(value, _Keys))
        {
          _IsPendingSort = true;
        }
        // Must be done after check for changes.
        _Keys = value;

        // New sort if count has changed.
        if (Count != _PrevCount)
        {
          _IsPendingSort = true;
          _PrevCount = Count;
        }
      }
    }
    private LJCDataValues? _Keys;

    // Gets the item with the supplied property name.
    /// <include file='Doc/DataValues.xml'
    ///  path='members/Item/*'/>
    public LJCDataValue? this[string propertyName]
    {
      get
      {
        LJCDataValue retValue = null;

        if (LJC.HasListItems(this)
          && LJC.HasText(propertyName))
        {
          // Get where LJCDataColumn property = "PropertyName"
          //   , value = propertyName.
          var keys = LJC.Keys(LJCDataColumn.ColumnPropertyName
          , propertyName);
          retValue = LJCGetUnique(keys);
        }
        return retValue;
      }
    }

    private bool _IsPendingSort;
    private int _PrevCount;
    #endregion

    #region Constructor Methods

    // Initializes an object instance.
    /// <include file='../../../LJCUtilities/LJCGenDoc5/Common/Data.xml'
    ///  path='members/Constructor/*'/>
    /// <parentGroup>constructors</parentGroup>
    public LJCDataValues()
    {
      _IsPendingSort = false;
      _PrevCount = -1;
    }

    // The Copy constructor.
    /// <include file='../../../LJCUtilities/LJCGenDoc5/Common/Collection.xml'
    ///  path='members/CopyConstructor/*'/>
    /// <parentGroup>constructors</parentGroup>
    public LJCDataValues(LJCDataValues items)
    {
      if (LJC.HasListItems(items))
      {
        foreach (var item in items)
        {
          Add(new LJCDataValue(item));
        }
      }
    }
    #endregion

    #region Collection Methods

    // Creates and returns a clone of the object.
    /// <include file='../../../LJCUtilities/LJCGenDoc5/Common/Data.xml'
    ///  path='members/Clone/*'/>
    /// <parentGroup>collection</parentGroup>
    public LJCDataValues Clone()
    {
      var retValue = new LJCDataValues();
      foreach (LJCDataValue dataValue in this)
      {
        var newDataValue = dataValue.Clone();
        if (newDataValue != null)
        {
          retValue.Add(newDataValue);
        }
      }
      return retValue;
    }

    // Checks if the collection has items.
    /// <include file='../../../LJCUtilities/LJCGenDoc5/Common/Collection.xml'
    ///  path='members/HasItems/*'/>
    /// <parentGroup>collection</parentGroup>
    public bool HasItems()
    {
      bool retValue = false;

      if (Count > 0)
      {
        retValue = true;
      }
      return retValue;
    }

    // Gets a collection of changed columns.
    /// <include file='Doc/DataValues.xml'
    ///  path='members/LJCChanged/*'/>
    /// <parentGroup>item</parentGroup>
    public LJCDataValues LJCChanged()
    {
      List<LJCDataValue> dataValues;
      var retValue = new LJCDataValues();

      dataValues = FindAll(x => x.IsChanged);
      foreach (LJCDataValue dataValue in dataValues)
      {
        var newDataValue = dataValue.Clone();
        if (newDataValue != null)
        {
          retValue.Add(newDataValue);
        }
      }
      return retValue;
    }

    // Sets the IsChanged value to false for all elements in the collection.
    /// <include file='Doc/DataValues.xml'
    ///  path='members/LJCClearChanged/*'/>
    /// <parentGroup>item</parentGroup>
    public void LJCClearChanged()
    {
      foreach (LJCDataValue dataValue in this)
      {
        dataValue.IsChanged = false;
      }
    }

    // Creates combined LJCDataColumns from LJCDataColumns and LJCDataValues.
    /// <include file='Doc/DataValues.xml'
    ///  path='members/LJCCreateColumns/*'/>
    /// <parentGroup>item</parentGroup>
    public LJCDataColumns? LJCCreateColumns(LJCDataColumns? dataColumns)
    {
      LJCDataColumns retValue = null;

      if (dataColumns != null)
      {
        retValue = [];
        foreach (var dataValue in this)
        {
          if (dataValue.PropertyName != null)
          {
            var dataColumn = dataColumns[dataValue.PropertyName];
            var newDataValue = dataValue.CreateColumn(dataColumn!);
            if (newDataValue != null)
            {
              retValue.Add(newDataValue);
            }
          }
        }
      }
      return retValue;
    }

    // Gets a list of property names from the collection items.
    /// <include file='Doc/DataValues.xml'
    ///  path='members/LJCKeyPropertyNames/*'/>
    public List<string>? LJCKeyPropertyNames(LJCDataValues? keys = null)
    {
      List<string>? retList = null;

      if (!LJC.HasListItems(keys))
      {
        keys = _Keys;
      }
      if (LJC.HasListItems(keys))
      {
        retList = [];
        foreach (var dataColumn in keys)
        {
          retList.Add(dataColumn.PropertyName);
        }
      }
      return retList;
    }

    // Gets a list of property names from the collection items.
    /// <include file='Doc/DataValues.xml'
    ///  path='members/LJCPropertyNames/*'/>
    public List<string>? LJCPropertyNames(LJCDataValues? dataValues = null)
    {
      List<string>? retList = null;

      if (!LJC.HasListItems(dataValues))
      {
        dataValues = this;
      }
      if (LJC.HasListItems(dataValues))
      {
        retList = [];
        foreach (var dataValue in dataValues)
        {
          retList.Add(dataValue.PropertyName);
        }
      }
      return retList;
    }

    // Serializes the collection
    /// <include file='../../../LJCUtilities/LJCGenDoc5/Common/Collection.xml'
    ///  path='members/LJCSerialize/*'/>
    /// <parentGroup>collection</parentGroup>
    public void LJCSerialize(string? fileSpec = null)
    {
      if (!LJC.HasText(fileSpec))
      {
        fileSpec = LJCDefaultFileName;
      }
      LJC.XmlSerialize(GetType(), this, null, fileSpec);
    }
    #endregion

    #region Collection Data Methods

    // Creates item with Value and adds it to the collection.
    /// <include file='Doc/DataValues.xml'
    ///  path='members/Add/*'/>
    /// <parentGroup>collection</parentGroup>
    public LJCDataValue Add(string propertyName, object? value = null
      , string dataTypeName = "String")
    {
      var retValue = new LJCDataValue()
      {
        PropertyName = propertyName,
        Value = value,
        DataTypeName = dataTypeName,
      };
      Add(retValue);
      return retValue;
    }

    // Returns the column that matches the key columns.
    // The column is identified by its property names and values.
    /// <include file='Doc/DataValues.xml'
    ///  path='members/LJCGetUnique/*'/>
    public LJCDataValue? LJCGetUnique(LJCDataColumns? keys = null)
    {
      LJCDataValue retValue = null;

      if (keys != null)
      {
        LJCKeys = keys;
      }

      if (LJC.HasListItems(LJCKeys))
      {
        LJCSort();

        // Create search item.
        var dataValue = new LJCDataValue();
        var reflect = new LJCReflect(dataValue);
        foreach (var key in LJCKeys)
        {
          reflect.SetValue(key.PropertyName, key.Value);
        }

        // Create comparer.
        DataValueKey comparer = null;
        var propertyNames = LJCPropertyNames(LJCKeys);
        if (propertyNames != null)
        {
          comparer = new DataValueKey()
          {
            LJCPropertyNames = propertyNames,
          };
        }

        int index = BinarySearch(dataValue, comparer);
        if (index > -1)
        {
          retValue = this[index];
        }
      }
      return retValue;
    }

    // Removes the item with the supplied property name.
    /// <include file='Doc/DataValues.xml'
    ///  path='members/LJCRemove/*'/>
    public void LJCRemove(string propertyName)
    {
      var dataValue = this[propertyName];
      if (dataValue != null)
      {
        Remove(dataValue);
      }
    }

    // Sorts on the current key columns.
    /// <include file='Doc/DataValues.xml'
    ///  path='members/LJCSort/*'/>
    public void LJCSort(LJCDataColumns? keys = null)
    {
      if (LJC.HasListItems(keys))
      {
        LJCKeys = keys;
      }

      if (_IsPendingSort)
      {
        var sortNames = LJCKeyPropertyNames(_Keys);
        if (sortNames != null)
        {
          var uniqueComparer = new DataValueKey
          {
            LJCPropertyNames = sortNames
          };
          Sort(uniqueComparer);
        }
      }
      _IsPendingSort = false;
    }
    #endregion

    #region Value Methods

    // Gets the column object value as a bool.
    /// <include file='Doc/DataValues.xml'
    ///  path='members/LJCBoolean/*'/>
    /// <parentGroup>value</parentGroup>
    public bool LJCBoolean(string propertyName)
    {
      var value = LJCValue(propertyName);
      var retValue = LJC.GetBoolean(value);
      return retValue;
    }

    // Gets the column object value as a byte.
    /// <include file='Doc/DataValues.xml'
    ///  path='members/LJCByte/*'/>
    /// <parentGroup>value</parentGroup>
    public byte LJCByte(string propertyName)
    {
      var value = LJCValue(propertyName);
      var retValue = LJC.GetByte(value);
      return retValue;
    }

    // Gets the column object value as a byte array.
    /// <include file="Doc/DataValues.xml"
    /// path="members/LJCBytes/*"/>
    public byte[]? LJCBytes(string propertyName)
    {
      var value = LJCValue(propertyName);
      var retValue = LJC.GetBytes(value);
      return retValue;
    }

    // Gets the column object value as a char.
    /// <include file='Doc/DataValues.xml'
    ///  path='members/LJCChar/*'/>
    /// <parentGroup>value</parentGroup>
    public char LJCChar(string propertyName)
    {
      var value = LJCValue(propertyName);
      var retValue = LJC.GetChar(value);
      return retValue;
    }

    // Gets the column object value as a char array.
    /// <include file='Doc/DataValues.xml'
    ///  path='members/LJCChars/*'/>
    /// <parentGroup>value</parentGroup>
    public char[] LJCChars(string propertyName)
    {
      var value = LJCValue(propertyName);
      var retValue = LJC.GetChars(value);
      return retValue;
    }

    /// <include file='Doc/DataValues.xml'
    ///  path='members/LJCDateTime/*'/>
    public DateTime LJCDateTime(string propertyName)
    {
      var value = LJCValue(propertyName);
      var retValue = LJC.GetDateTime(value);
      return retValue;
    }

    // Gets the column object value as a decimal.
    /// <include file='Doc/DataValues.xml'
    ///  path='members/LJCDecimal/*'/>
    /// <parentGroup>value</parentGroup>
    public decimal LJCDecimal(string propertyName)
    {
      var value = LJCValue(propertyName);
      var retValue = LJC.GetDecimal(value);
      return retValue;
    }

    // Gets the column object value as a double.
    /// <include file='Doc/DataValues.xml'
    ///  path='members/LJCDouble/*'/>
    /// <parentGroup>value</parentGroup>
    public double LJCDouble(string propertyName)
    {
      var value = LJCValue(propertyName);
      var retValue = LJC.GetDouble(value);
      return retValue;
    }

    // Gets the column object value as a single.
    /// <include file='Doc/DataValues.xml'
    ///  path='members/LJCFloat/*'/>
    /// <parentGroup>value</parentGroup>
    public float LJCFloat(string propertyName)
    {
      var value = LJCValue(propertyName);
      var retValue = LJC.GetFloat(value);
      return retValue;
    }

    // Returns the Guid value of the data field with the specified name. 
    /// <include file='Doc/DataValues.xml'
    ///  path='items/LJCGuid/*'/>
    public Guid LJCGuid(string propertyName)
    {
      var value = LJCValue(propertyName);
      var retValue = LJC.GetGuid(value);
      return retValue;
    }

    // Gets the column object value as a short int.
    /// <include file='Doc/DataValues.xml'
    ///  path='members/LJCInt16/*'/>
    /// <parentGroup>value</parentGroup>
    public short LJCInt16(string propertyName)
    {
      var value = LJCValue(propertyName);
      var retValue = LJC.GetInt16(value);
      return retValue;
    }

    // Gets the column object value as an int.
    /// <include file='Doc/DataValues.xml'
    ///  path='members/LJCInt32/*'/>
    /// <parentGroup>value</parentGroup>
    public int LJCInt32(string propertyName)
    {
      var value = LJCValue(propertyName);
      var retValue = LJC.GetInt32(value);
      return retValue;
    }

    // Gets the column object value as a long int.
    /// <include file='Doc/DataValues.xml'
    ///  path='members/LJCInt64/*'/>
    /// <parentGroup>value</parentGroup>
    public long LJCInt64(string propertyName)
    {
      var value = LJCValue(propertyName);
      var retValue = LJC.GetInt64(value);
      return retValue;
    }

    // Gets the string value for the column with the specified name.
    /// <include file='Doc/DataValues.xml'
    ///  path='members/LJCString/*'/>
    /// <parentGroup>value</parentGroup>
    public string? LJCString(string propertyName)
    {
      var value = LJCValue(propertyName);
      var retValue = LJC.GetString(value);
      return retValue;
    }

    // Gets the column object value as an object.
    /// <include file='Doc/DataValues.xml'
    ///  path='members/LJCValue/*'/>
    /// <parentGroup>value</parentGroup>
    public object? LJCValue(string propertyName)
    {
      object retValue = default;

      var dataValue = this[propertyName];
      if (dataValue != null
          && dataValue.Value != null)
      {
        retValue = dataValue.Value;
      }
      return retValue;
    }
    #endregion

    #region Additional Value Methods

    // Gets a database DateTime string.
    /// <include file='Doc/DataValues.xml'
    ///  path='members/LJCDbDateString/*'/>
    public string? LJCDbDateString(string propertyName)
    {
      var value = LJCValue(propertyName);
      var retValue = LJC.GetDbDateString(value);
      return retValue;
    }

    // Gets the column object value as a DateTime.
    /// <include file='Doc/DataValues.xml'
    ///  path='members/LJCDbDateTime/*'/>
    public DateTime LJCDbDateTime(string propertyName)
    {
      var value = LJCValue(propertyName);
      var retValue = LJC.GetDbDateTime(value);
      return retValue;
    }

    // Sets the object value for the column with the specified name.
    /// <include file='Doc/DataValues.xml'
    ///  path='members/LJCSetValue/*'/>
    /// <parentGroup>value</parentGroup>
    public void LJCSetValue(string propertyName, object value)
    {
      var dataValue = this[propertyName];
      if (dataValue != null)
      {
        dataValue.Value = value;
      }
    }
    #endregion
  }

  // Sort and search on key values.
  /// <include file='Doc/DataValues.xml'
  ///  path='members/DataValueKeyComparer/*'/>
  public class DataValueKey : IComparer<LJCDataValue>
  {
    /// <include file='Doc/DataValues.xml'
    ///  path='members/ColumnNames/*'/>
    public List<string>? LJCPropertyNames { get; set; }

    // Compares two objects.
    /// <include file='Doc/DataValues.xml'
    ///  path='members/Compare/*'/>
    public int Compare(LJCDataValue? x, LJCDataValue? y)
    {
      int retValue;

      // Check for null objects.
      retValue = LJC.CompareNull(x, y);

      while (true)
      {
        // End if one or both objects are null.
        if (null == LJCPropertyNames
          || retValue != LJCNetString.CompareNotNullOrEqual)
        {
          break;
        }

        var xReflect = new LJCReflect(x!);
        var yReflect = new LJCReflect(y!);

        // Check for null values.
        foreach (string propertyName in LJCPropertyNames)
        {
          var xValue = xReflect.GetString(propertyName);
          var yValue = yReflect.GetString(propertyName);
          retValue = LJC.CompareNull(xValue, yValue);

          // Break if one or both values are null.
          if (retValue != LJCNetString.CompareNotNullOrEqual)
          {
            break;
          }
        }

        // End if one or both values are null.
        if (retValue != LJCNetString.CompareNotNullOrEqual)
        {
          break;
        }

        for (int index = 0; index < LJCPropertyNames.Count; index++)
        {
          var propertyName = LJCPropertyNames[index];
          var xValue = xReflect.GetString(propertyName);
          var yValue = yReflect.GetString(propertyName);

          if (xValue != null)
          {
            if (index < LJCPropertyNames.Count - 1)
            {
              // Compare parent keys.
              retValue = xValue.CompareTo(yValue);
              if (retValue != LJCNetString.CompareEqual)
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
        }
        break;
      }
      return retValue;
    }
  }
}
