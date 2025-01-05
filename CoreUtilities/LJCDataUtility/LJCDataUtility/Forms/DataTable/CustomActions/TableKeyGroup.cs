// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// TableKeyGroup.cs
using LJCDataUtilityDAL;
using LJCNetCommon;
using System;

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
      CurrentTableKey = null;
      if (NetCommon.HasItems(TableKeys))
      {
        CurrentTableKey = TableKeys[0];
        mPrevConstraintName = CurrentTableKey.ConstraintName;
      }
    }
    #endregion

    #region Methods

    // Gets the next column names value.
    internal string NextGroupNames()
    {
      string retNames = null;

      if (NetCommon.HasItems(TableKeys))
      {
        // Find next current value.
        foreach (TableKey sourceKey in TableKeys)
        {
          TableKey searchUsedKey = null;
          searchUsedKey = SearchUsedKeys(sourceKey);

          if (null == searchUsedKey)
          {
            if (mPrevConstraintName == sourceKey.ConstraintName)
            {
              CurrentTableKey = sourceKey;
              NetString.AddDelimitedValue(ref retNames, sourceKey.ColumnName);
              UsedKeys.Add(sourceKey);
            }
            else
            {
              mPrevConstraintName = sourceKey.ConstraintName;
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

    // Gets or sets the Current TableKey value.
    internal TableKey CurrentTableKey { get; set; }

    // Gets or sets the TableKeys value.
    private TableKeys TableKeys { get; set; }

    // Gets or sets the UsedKeys value.
    private TableKeys UsedKeys { get; set; }
    #endregion
  }
}
