// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// TableKeyGroup.cs
using LJCDataUtilityDAL;
using LJCNetCommon;
using System;
using System.Collections.Generic;

namespace LJCDataUtility
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
      UsedKeys = new TableKeys();
    }
    #endregion

    #region Methods

    // Gets the next column names value.
    internal string NextGroupNames()
    {
      string retNames = null;

      CurrentTableKey = null;
      if (NetCommon.HasItems(TableKeys))
      {
        CurrentTableKey = TableKeys[0];
        mPrevConstraintName = CurrentTableKey.ConstraintName;

        // Find next current value.
        foreach (TableKey sourceKey in TableKeys)
        {
          TableKey searchUsedKey = null;
          searchUsedKey = SearchUsedKeys(sourceKey);

          if (null == searchUsedKey)
          {
            if (mPrevConstraintName == sourceKey.ConstraintName)
            {
              NetString.AddDelimitedValue(ref retNames, sourceKey.ColumnName);
              UsedKeys.Add(sourceKey);
            }
            else
            {
              break;
            }
            mPrevConstraintName = sourceKey.ConstraintName;
          }
        }
      }
      return retNames;
    }
    private string mPrevConstraintName;

    // Searches the UsedKeys collections.
    private TableKey SearchUsedKeys(TableKey searchKey)
    {
      TableKey retKey = null;

      if (NetCommon.HasItems(UsedKeys))
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

    internal TableKey CurrentTableKey { get; set; }

    private TableKeys TableKeys { get; set; }

    private TableKeys UsedKeys { get; set; }
    #endregion
  }
}
