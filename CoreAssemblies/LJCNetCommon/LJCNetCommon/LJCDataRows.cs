// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCDataRows.cs
using System.Collections.Generic;
using System.Xml.Serialization;

namespace LJCNetCommon
{
  // Represents a collection of LJCDataColumns objects.
  /// <include file='Doc/LJCDataRows.xml'
  ///  path='members/LJCDataRows/*'/>
  [XmlRoot("LJCDataRows")]
  public class LJCDataRows : List<LJCDataColumns>
  {
    #region Custom Data Methods

    // Dynamic binary search with key columns.
    /// <include file='Doc/LJCDataRows.xml'
    ///  path='members/LJCBinarySearch/*'/>
    public int LJCBinarySearch()
    {
      int retIndex = -1;

      LJCSort();

      while (true)
      {
        if (!NetCommon.HasItems(_KeyColumns))
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
          for (short index = 0; index < _KeyColumns.Count; index++)
          {
            var keyColumn = _KeyColumns[index];
            var propertyName = keyColumn.PropertyName;
            var columnValue = dataColumns.LJCGetString(propertyName);
            compareValue = LJCCompareColumn(columnValue, keyColumn);
            if (index < _KeyColumns.Count - 1)
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

    // Returns the row that matches the key columns.
    /// <include file='Doc/LJCDataRows.xml'
    ///  path='members/LJCSearchValue/*'/>
    public LJCDataColumns LJCGetRow()
    {
      LJCDataColumns retColumns = null;

      var index = LJCBinarySearch();
      if (index != -1)
      {
        retColumns = this[index];
      }
      return retColumns;
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
        var sortNames = PropertyNames(_KeyColumns);
        var uniqueComparer = new DataRowKeyComparer
        {
          LJCPropertyNames = sortNames
        };
        Sort(uniqueComparer);
      }
      _IsPendingSort = false;
    }

    // Gets property names list from data columns.
    private List<string> PropertyNames(LJCDataColumns dataColumns)
    {
      List<string> retList = null;

      if (NetCommon.HasItems(dataColumns))
      {
        retList = new List<string>();
        foreach (var dataColumn in dataColumns)
        {
          retList.Add(dataColumn.PropertyName);
        }
      }
      return retList;
    }

    // Checks if the key columns value has changed.
    private bool IsKeyColumnsChanged(LJCDataColumns newKeyColumns, LJCDataColumns currentKeyColumns)
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

    #region Properties

    // Gets or sets the key columns.
    /// <include file='Doc/LJCDataRows.xml'
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
    ///  path='members/ColumnNames/*'/>
    public List<string> LJCPropertyNames { get; set; }

    // Compares two objects.
    /// <include file='Doc/LJCDataColumns.xml'
    ///  path='items/Compare/*'/>
    public int Compare(LJCDataColumns x, LJCDataColumns y)
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

        // Check for null values.
        foreach (string propertyName in LJCPropertyNames)
        {
          var xValue = x.LJCGetString(propertyName);
          var yValue = y.LJCGetString(propertyName);
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
