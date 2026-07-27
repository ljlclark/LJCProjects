// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCDataRows.cs
using System.Collections.Generic;
using System.Xml.Serialization;
using LJC = LJCNetCommon.NetCommon;

namespace LJCNetCommon
{
  // Represents a collection of LJCDataColumns objects.
  /// <include file='Doc/LJCDataRows.xml'
  ///  path='members/LJCDataRows/*'/>
  [XmlRoot("LJCDataRows")]
  public class LJCDataRows : List<LJCDataColumns>
  {
    #region Constructor Methods

    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'
    ///  path='members/Constructor/*'/>
    public LJCDataRows()
    {
      _IsPendingSort = false;
      _PrevCount = 0;
    }

    // Initializes an object from the supplied items.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Collection.xml'
    ///  path='members/CopyConstructor/*'/>
    public LJCDataRows(LJCDataRows items)
    {
      if (LJC.HasItems(items))
      {
        foreach (var item in items)
        {
          Add(new LJCDataColumns(item));
        }
      }
    }
    #endregion

    #region Collection Methods

    // Gets property names list from data columns.
    private List<string> LJCPropertyNames(LJCDataColumns dataColumns)
    {
      List<string> retList = null;

      if (LJC.HasItems(dataColumns))
      {
        retList = new List<string>();
        foreach (var dataColumn in dataColumns)
        {
          retList.Add(dataColumn.PropertyName);
        }
      }
      return retList;
    }
    #endregion

    #region Collection Data Methods

    // Returns the row that matches the key columns.
    // The row is identified by its column property name values and column
    // values.
    /// <include file='Doc/LJCDataRows.xml'
    ///  path='members/LJCGetUnique/*'/>
    public LJCDataColumns LJCGetUnique(LJCDataColumns keys = null)
    {
      LJCDataColumns retColumns = null;

      if (keys != null)
      {
        LJCKeys = keys;
      }

      if (LJC.HasItems(LJCKeys))
      {
        var index = LJCBinarySearch();
        if (index != -1)
        {
          retColumns = this[index];
        }
      }
      return retColumns;
    }

    // Sorts on the current key columns.
    /// <include file='Doc/LJCDataRows.xml'
    ///  path='members/LJCSort/*'/>
    public void LJCSort(LJCDataColumns keys = null)
    {
      if (LJC.HasItems(keys))
      {
        LJCKeys = keys;
      }

      if (_IsPendingSort)
      {
        var sortNames = LJCPropertyNames(_Keys);
        if (sortNames != null)
        {
          var uniqueComparer = new DataRowKeyComparer
          {
            LJCPropertyNames = sortNames
          };
          Sort(uniqueComparer);
        }
      }
      _IsPendingSort = false;
    }

    // Checks if the key columns value has changed.
    private bool IsKeyColumnsChanged(LJCDataColumns newKeys
      , LJCDataColumns currentKeys)
    {
      bool retValue = false;

      while (true)
      {
        var hasNewColumns = LJC.HasItems(newKeys);
        var hasSortColumns = LJC.HasItems(currentKeys);

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
          if (newKeys.Count != currentKeys.Count)
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

    #region Custom Data Methods

    // Dynamic binary search with key columns.
    /// <include file='Doc/LJCDataRows.xml'
    ///  path='members/LJCBinarySearch/*'/>
    public int LJCBinarySearch(LJCDataColumns keys = null)
    {
      int retIndex = -1;

      if (keys != null)
      {
        LJCKeys = keys;
      }

      LJCSort();

      while (true)
      {
        if (!LJC.HasItems(_Keys))
        {
          break;
        }

        int leftIndex = 0;
        int rightIndex = Count - 1;
        while (leftIndex <= rightIndex)
        {
          // Get the midpoint.
          int middleIndex = leftIndex + (rightIndex - leftIndex) / 2;

          // Get the object compare value.
          var dataColumns = this[middleIndex];

          int compareValue = NetString.CompareGreater;
          for (short index = 0; index < _Keys.Count; index++)
          {
            var keyColumn = _Keys[index];
            var propertyName = keyColumn.PropertyName;
            var columnValue = dataColumns.LJCGetString(propertyName);
            compareValue = LJCCompareColumn(columnValue, keyColumn);
            if (index < _Keys.Count - 1)
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

          // Item was found.
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
        break;
      }
      return retIndex;
    }

    // Compare column value to key column value.
    /// <include file='Doc/LJCDataRows.xml'
    ///  path='members/LJCCompareColumn/*'/>
    public int LJCCompareColumn(string columnValue
      , LJCDataColumn keyColumn, bool ignoreCase = true)
    {
      string searchValue = null;
      if (keyColumn.Value != null)
      {
        searchValue = keyColumn.Value.ToString();
      }
      int retIndex = string.Compare(columnValue, searchValue, ignoreCase);
      return retIndex;
    }
    #endregion

    #region Properties

    // Gets or sets the key columns.
    /// <include file='Doc/LJCDataRows.xml'
    ///  path='members/LJCKeys/*'/>
    public LJCDataColumns LJCKeys
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
    private LJCDataColumns _Keys;
    #endregion

    #region Class Data

    private bool _IsPendingSort;
    private int _PrevCount;
    #endregion
  }

  // Sort and search on key values.
  /// <include file='Doc/LJCDataRows.xml'
  ///  path='members/DataRowKeyComparer/*'/>
  public class DataRowKeyComparer : IComparer<LJCDataColumns>
  {
    /// <include file='Doc/LJCDataColumns.xml'
    ///  path='members/LJCPropertyNames/*'/>
    public List<string> LJCPropertyNames { get; set; }

    // Compares two objects.
    /// <include file='Doc/LJCDataColumns.xml'
    ///  path='items/Compare/*'/>
    public int Compare(LJCDataColumns x, LJCDataColumns y)
    {
      int retValue;

      // Check for null objects.
      retValue = LJC.CompareNull(x, y);

      while (true)
      {
        // End if one of the objects is null.
        if (null == LJCPropertyNames
          || retValue != NetString.CompareNotNullOrEqual)
        {
          break;
        }

        // Check for null values.
        foreach (string propertyName in LJCPropertyNames)
        {
          var xValue = x.LJCGetString(propertyName);
          var yValue = y.LJCGetString(propertyName);
          retValue = LJC.CompareNull(xValue, yValue);

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
          var xValue = x.LJCGetString(propertyName);
          var yValue = y.LJCGetString(propertyName);

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
