using System.Collections.Generic;

namespace LJCNetCommon
{
  /// <include file='Doc/LJCDataRows.xml'
  ///  path='members/LJCDataRows/*'/>
  public class LJCDataRows : List<LJCDataColumns>
  {
    #region Methods

    // Dynamic binary search with key columns.
    /// <include file='Doc/LJCDataRows.xml'
    ///  path='members/LJCBinarySearch/*'/>
    public int LJCBinarySearch(LJCDataColumns keyColumns)
    {
      int retIndex = -1;

      int leftIndex = 0;
      int rightIndex = Count - 1;
      while (leftIndex <= rightIndex)
      {
        // Get the midpoint.
        int middleIndex = leftIndex + (rightIndex - leftIndex) / 2;

        // Get the object compare value.
        var dataColumns = this[middleIndex];

        int compareValue = NetString.CompareGreater;
        for (short index = 0; index < keyColumns.Count; index++)
        {
          var keyColumn = keyColumns[index];
          var propertyName = keyColumn.PropertyName;
          var columnValue = dataColumns.LJCGetString(propertyName);
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
  }

  // Sort and search on Unique value.
  /// <include file='Doc/LJCDataRows.xml'
  ///  path='members/DataRowsUniqueComparer/*'/>
  public class DataRowsUniqueComparer : IComparer<LJCDataColumns>
  {
    /// <include file='Doc/LJCDataColumns.xml'
    ///  path='members/ColumnNames/*'/>
    public List<string> LJCColumnNames { get; set; }

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
        if (null == LJCColumnNames
          || retValue != NetString.CompareNotNullOrEqual)
        {
          break;
        }

        // Check for null values.
        foreach (string columnName in LJCColumnNames)
        {
          var xValue = x.LJCGetString(columnName);
          var yValue = y.LJCGetString(columnName);
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

        for (int index = 0; index < LJCColumnNames.Count; index++)
        {
          var columnName = LJCColumnNames[index];
          var xValue = x.LJCGetString(columnName);
          var yValue = y.LJCGetString(columnName);

          if (index < LJCColumnNames.Count - 1)
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
