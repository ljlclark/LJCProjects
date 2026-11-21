// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCDataValues.cs
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace LJCNetCommon
{
  // Represents a collection of LJCDataValue objects.
  /// <include file='Doc/LJCDataValues.xml'
  ///  path='members/LJCDataValues/*'/>
  [XmlRoot("LJCDataValues")]
  public class LJCDataValues : List<LJCDataValue>
  {
    #region Static Methods

    // Deserializes from the specified XML file.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Collection.xml'
    ///  path='members/LJCDeserialize/*'/>
    public static LJCDataValues LJCDeserialize(string fileSpec = null)
    {
      LJCDataValues retValues;

      if (!LJC.HasText(fileSpec))
      {
        fileSpec = LJCDefaultFileName;
      }
      retValues = LJC.XmlDeserialize(typeof(LJCDataValues), fileSpec)
        as LJCDataValues;
      return retValues;
    }

    // Operator to create LJCDataColumns from LJCDataValues.
    // var dataColumns = dataValues;
    /// <include file='Doc/LJCDataValues.xml'
    ///  path='members/ToDataColumns/*'/>
    public static implicit operator LJCDataColumns(LJCDataValues dataValues)
    {
      var retValues = new LJCDataColumns();

      if (LJC.HasListItems(dataValues))
      {
        foreach (LJCDataValue dataValue in dataValues)
        {
          var dataColumn = new LJCDataColumn
          {
            PropertyName = dataValue.PropertyName,
            DataTypeName = dataValue.DataTypeName,
            Value = dataValue.Value
          };
          retValues.Add(dataColumn);
        }
      }
      return retValues;
    }

    // Checks if the key columns value has changed.
    private static bool IsKeyColumnsChanged(LJCDataValues newKeys
      , LJCDataValues currentKeys)
    {
      bool retIsChanged = false;

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
          retIsChanged = true;
          break;
        }

        if (hasNewColumns)
        {
          if (newKeys.Count != currentKeys.Count)
          {
            retIsChanged = true;
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
              retIsChanged = true;
              break;
            }
          }
        }
        break;
      }
      return retIsChanged;
    }
    #endregion

    #region Properties

    // Gets the Default File Name.
    /// <include file='Doc/LJCDataValues.xml'
    ///  path='members/LJCDefaultFileName/*'/>
    public static string LJCDefaultFileName
    {
      get => "LJCDataValues.xml";
    }

    // Gets or sets the key columns.
    /// <include file='Doc/LJCDataValues.xml'
    ///  path='members/LJCKeyColumns/*'/>
    public LJCDataValues LJCKeys
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
    private LJCDataValues _Keys;

    // Gets the item with the supplied property name.
    /// <include file='Doc/LJCDataValues.xml'
    ///  path='members/Item/*'/>
    public LJCDataValue this[string propertyName]
    {
      get
      {
        LJCDataValue retValue = null;

        if (LJC.HasListItems(this)
          && LJC.HasText(propertyName))
        {
          // Get where LJCDataColumn property = "PropertyName"
          //   , value = propertyName.
          var keys = LJC.Keys(LJCDataValue.ColumnPropertyName
          , propertyName);
          retValue = LJCGetUnique(keys);
        }
        return retValue;
      }
    }
    #endregion

    #region Class Data

    private bool _IsPendingSort;
    private int _PrevCount;
    #endregion

    #region Constructor Methods

    // Initializes an object instance.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'
    ///  path='members/Constructor/*'/>
    public LJCDataValues()
    {
      _IsPendingSort = false;
      _PrevCount = -1;
    }

    // The Copy constructor.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Collection.xml'
    ///  path='members/CopyConstructor/*'/>
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
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'
    ///  path='members/Clone/*'/>
    public LJCDataValues Clone()
    {
      var retValues = new LJCDataValues();
      foreach (var dataValue in this)
      {
        var newDataValue = dataValue.Clone();
        if (newDataValue != null)
        {
          retValues.Add(newDataValue);
        }
      }
      return retValues;
    }

    // Checks if the collection has items.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Collection.xml'
    ///  path='members/HasItems/*'/>
    public bool HasItems()
    {
      bool retHasItems = false;

      if (Count > 0)
      {
        retHasItems = true;
      }
      return retHasItems;
    }

    // Gets a collection of changed columns.
    /// <include file='Doc/LJCDataValues.xml'
    ///  path='members/LJCChanged/*'/>
    public LJCDataValues LJCChanged()
    {
      List<LJCDataValue> dataValues;
      var retValues = new LJCDataValues();

      dataValues = FindAll(x => x.IsChanged);
      foreach (var dataValue in dataValues)
      {
        var newDataValue = dataValue.Clone();
        if (newDataValue != null)
        {
          retValues.Add(newDataValue);
        }
      }
      return retValues;
    }

    // Sets the IsChanged value to false for all elements in the collection.
    /// <include file='Doc/LJCDataValues.xml'
    ///  path='members/LJCClearChanged/*'/>
    public void LJCClearChanged()
    {
      foreach (var dataValue in this)
      {
        dataValue.IsChanged = false;
      }
    }

    // Creates combined LJCDataColumns from LJCDataColumns and LJCDataValues.
    /// <include file='Doc/LJCDataValues.xml'
    ///  path='members/LJCCreateColumns/*'/>
    public LJCDataColumns LJCCreateColumns(LJCDataColumns dataColumns)
    {
      LJCDataColumns retColumns = null;

      if (dataColumns != null)
      {
        retColumns = new LJCDataColumns();
        foreach (var dataValue in this)
        {
          if (dataValue.PropertyName != null)
          {
            var dataColumn = dataColumns[dataValue.PropertyName];
            var newDataValue = dataValue.CreateColumn(dataColumn);
            if (newDataValue != null)
            {
              retColumns.Add(newDataValue);
            }
          }
        }
      }
      return retColumns;
    }

    // Gets a list of property names from the collection items.
    /// <include file='Doc/LJCDataValues.xml'
    ///  path='members/LJCKeyPropertyNames/*'/>
    public List<string> LJCKeyPropertyNames(LJCDataValues keys = null)
    {
      List<string> retNames = null;

      if (!LJC.HasListItems(keys))
      {
        keys = _Keys;
      }
      if (LJC.HasListItems(keys))
      {
        retNames = new List<string>();
        foreach (var dataValue in keys)
        {
          retNames.Add(dataValue.PropertyName);
        }
      }
      return retNames;
    }

    // Gets a list of property names from the collection items.
    /// <include file='Doc/LJCDataValues.xml'
    ///  path='members/LJCPropertyNames/*'/>
    public List<string> LJCPropertyNames(LJCDataValues dataValues = null)
    {
      List<string> retNames = null;

      if (!LJC.HasListItems(dataValues))
      {
        dataValues = this;
      }
      if (LJC.HasListItems(dataValues))
      {
        retNames = new List<string>();
        foreach (var dataValue in dataValues)
        {
          retNames.Add(dataValue.PropertyName);
        }
      }
      return retNames;
    }

    // Serializes the collection
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Collection.xml'
    ///  path='members/LJCSerialize/*'/>
    public void LJCSerialize(string fileSpec = null)
    {
      if (!LJC.HasText(fileSpec))
      {
        fileSpec = LJCDefaultFileName;
      }
      LJC.XmlSerialize(GetType(), this, null, fileSpec);
    }
    #endregion

    #region Collection Data Methods

    // Creates item with supplied values and adds it to the collection.
    /// <include file='Doc/LJCDataValues.xml'
    ///  path='members/Add/*'/>
    public LJCDataValue Add(string propertyName, object value
      , string dataTypeName = "string")
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

    // Returns the value that matches the key columns.
    // The value is identified by its property names and values.
    /// <include file='Doc/LJCDataValues.xml'
    ///  path='members/LJCGetUnique/*'/>
    public LJCDataValue LJCGetUnique(LJCDataValues keys = null)
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
    /// <include file='Doc/LJCDataValues.xml'
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
    /// <include file='Doc/LJCDataValues.xml'
    ///  path='members/LJCSort/*'/>
    public void LJCSort(LJCDataValues keys = null)
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
    /// <include file='Doc/LJCDataValues.xml'
    ///  path='members/LJCBoolean/*'/>
    public bool LJCBoolean(string propertyName)
    {
      var value = LJCValue(propertyName);
      var retValue = LJC.GetBoolean(value);
      return retValue;
    }

    // Gets the column object value as a byte.
    /// <include file='Doc/LJCDataValues.xml'
    ///  path='members/LJCByte/*'/>
    public byte LJCByte(string propertyName)
    {
      var value = LJCValue(propertyName);
      var retValue = LJC.GetByte(value);
      return retValue;
    }

    // Gets the column object value as a byte array.
    /// <include file="Doc/LJCDataValues.xml"
    /// path="members/LJCBytes/*"/>
    public byte[] LJCBytes(string propertyName)
    {
      var value = LJCValue(propertyName);
      var retValue = LJC.GetBytes(value);
      return retValue;
    }

    // Gets the column object value as a char.
    /// <include file='Doc/LJCDataValues.xml'
    ///  path='members/LJCChar/*'/>
    public char LJCChar(string propertyName)
    {
      var value = LJCValue(propertyName);
      var retValue = LJC.GetChar(value);
      return retValue;
    }

    // Gets the column object value as a char array.
    /// <include file='Doc/LJCDataValues.xml'
    ///  path='members/LJCChars/*'/>
    public char[] LJCChars(string propertyName)
    {
      var value = LJCValue(propertyName);
      var retValue = LJC.GetChars(value);
      return retValue;
    }

    /// <include file='Doc/LJCDataValues.xml'
    ///  path='members/LJCDateTime/*'/>
    public DateTime LJCDateTime(string propertyName)
    {
      var value = LJCValue(propertyName);
      var retValue = LJC.GetDateTime(value);
      return retValue;
    }

    // Gets the column object value as a decimal.
    /// <include file='Doc/LJCDataValues.xml'
    ///  path='members/LJCDecimal/*'/>
    public decimal LJCDecimal(string propertyName)
    {
      var value = LJCValue(propertyName);
      var retValue = LJC.GetDecimal(value);
      return retValue;
    }

    // Gets the column object value as a double.
    /// <include file='Doc/LJCDataValues.xml'
    ///  path='members/LJCDouble/*'/>
    public double LJCDouble(string propertyName)
    {
      var value = LJCValue(propertyName);
      var retValue = LJC.GetDouble(value);
      return retValue;
    }

    // Gets the column object value as a single.
    /// <include file='Doc/LJCDataValues.xml'
    ///  path='members/LJCFloat/*'/>
    public float LJCFloat(string propertyName)
    {
      var value = LJCValue(propertyName);
      var retValue = LJC.GetFloat(value);
      return retValue;
    }

    // Returns the Guid value of the data field with the specified name. 
    /// <include file='Doc/LJCDataValues.xml'
    ///  path='members/LJCGuid/*'/>
    public Guid LJCGuid(string propertyName)
    {
      var value = LJCValue(propertyName);
      var retValue = LJC.GetGuid(value);
      return retValue;
    }

    // Gets the column object value as a short int.
    /// <include file='Doc/LJCDataValues.xml'
    ///  path='members/LJCInt16/*'/>
    public short LJCInt16(string propertyName)
    {
      var value = LJCValue(propertyName);
      var retValue = LJC.GetInt16(value);
      return retValue;
    }

    // Gets the column object value as an int.
    /// <include file='Doc/LJCDataValues.xml'
    ///  path='members/LJCInt32/*'/>
    public int LJCInt32(string propertyName)
    {
      var value = LJCValue(propertyName);
      var retValue = LJC.GetInt32(value);
      return retValue;
    }

    // Gets the column object value as a long int.
    /// <include file='Doc/LJCDataValues.xml'
    ///  path='members/LJCInt64/*'/>
    public long LJCInt64(string propertyName)
    {
      var value = LJCValue(propertyName);
      var retValue = LJC.GetInt64(value);
      return retValue;
    }

    // Gets the string value for the column with the specified name.
    /// <include file='Doc/LJCDataValues.xml'
    ///  path='members/LJCString/*'/>
    public string LJCString(string propertyName)
    {
      var value = LJCValue(propertyName);
      var retValue = LJC.GetString(value);
      return retValue;
    }

    // Gets the column object value as an object.
    /// <include file='Doc/LJCDataValues.xml'
    ///  path='members/LJCValue/*'/>
    public object LJCValue(string propertyName)
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

    #region Other Value Methods

    // Gets a database DateTime string.
    /// <include file='Doc/LJCDataValues.xml'
    ///  path='members/LJCDbDateString/*'/>
    public string LJCDbDateString(string propertyName)
    {
      var value = LJCValue(propertyName);
      var retValue = LJC.GetDbDateString(value);
      return retValue;
    }

    // Gets the column object value as a DateTime.
    /// <include file='Doc/LJCDataValues.xml'
    ///  path='members/LJCDbDateTime/*'/>
    public DateTime LJCDbDateTime(string propertyName)
    {
      var value = LJCValue(propertyName);
      var retValue = LJC.GetDbDateTime(value);
      return retValue;
    }

    // Sets the object value for the column with the specified name.
    /// <include file='Doc/LJCDataValues.xml'
    ///  path='members/LJCSetValue/*'/>
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
  /// <include file='Doc/LJCDataValues.xml'
  ///  path='members/DataValueKeyComparer/*'/>
  public class DataValueKey : IComparer<LJCDataValue>
  {
    /// <include file='Doc/LJCDataValues.xml'
    ///  path='members/LJCPropertyNames/*'/>
    public List<string> LJCPropertyNames { get; set; }

    // Compares two objects.
    /// <include file='Doc/LJCDataValues.xml'
    ///  path='members/Compare/*'/>
    public int Compare(LJCDataValue x, LJCDataValue y)
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

        var xReflect = new LJCReflect(x);
        var yReflect = new LJCReflect(y);

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
