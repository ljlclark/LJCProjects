// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// TableKeyGroup.cs
using LJCDataUtilityDAL5;
using LJCNetCommon5;

namespace LJCDataUtility5
{
  // Provides methods to group key columns.
  internal class TableKeyGroup
  {
    #region Constructors

    // Initializes an object instance.
    internal TableKeyGroup(TableKeys tableKeys)
    {
      TableKeys = tableKeys;
      TableKeys.Sort();
      UsedKeys = [];
      _PrevConstraintName = "";
      if (LJC.HasListItems(TableKeys))
      {
        CurrentTableKey = TableKeys[0];
        if (CurrentTableKey.ConstraintName != null)
        {
          _PrevConstraintName = CurrentTableKey.ConstraintName;
        }
      }
    }
    #endregion

    #region Methods

    // Gets the next column names value.
    internal string? NextGroupNames()
    {
      string? retNames = null;

      if (LJC.HasListItems(TableKeys))
      {
        // Find next current value.
        foreach (TableKey sourceKey in TableKeys)
        {
          TableKey? searchUsedKey = null;
          searchUsedKey = SearchUsedKeys(sourceKey);

          if (null == searchUsedKey)
          {
            if (_PrevConstraintName == sourceKey.ConstraintName)
            {
              CurrentTableKey = sourceKey;
              if (sourceKey.ColumnName != null)
              {
                LJCNetString.AddDelimitedValue(ref retNames
                  , sourceKey.ColumnName);
              }
              UsedKeys.Add(sourceKey);
            }
            else
            {
              if (sourceKey.ConstraintName != null)
              {
                _PrevConstraintName = sourceKey.ConstraintName;
              }
              break;
            }
            _PrevConstraintName = sourceKey.ConstraintName;
          }
        }
      }
      return retNames;
    }

    // Searches the UsedKeys collections.
    private TableKey? SearchUsedKeys(TableKey searchKey)
    {
      TableKey? retKey = null;

      if (LJC.HasListItems(UsedKeys))
      {
        var tableKey = new TableKey
        {
          ConstraintName = searchKey.ConstraintName,
          OrdinalPosition = searchKey.OrdinalPosition
        };
        var index = UsedKeys.BinarySearch(tableKey);
        if (index >= 0)
        {
          retKey = UsedKeys[index];
        }
      }
      return retKey;
    }
    #endregion

    #region Properties

    // Gets or sets the Current TableKey value.
    internal TableKey CurrentTableKey { get; set; } = null!;

    // Gets or sets the TableKeys value.
    private TableKeys TableKeys { get; set; }

    // Gets or sets the UsedKeys value.
    private TableKeys UsedKeys { get; set; }
    #endregion

    #region Class Data

    private string _PrevConstraintName;
    #endregion
  }
}
