// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// TestTableKeys5.cs
using LJCDataUtilityDAL5;
using LJCNetCommon5;

namespace TestDataUtilityDAL5
{
  // Tests the DataModules collection.
  internal class TestTableKeys
  {
    #region Properties

    // Gets or sets the TestCommon object.
    private LJCTestCommon TestCommon { get; set; }
    #endregion

    #region Constructor Methods

    // Initializes an object instance.
    public TestTableKeys()
    {
      TestCommon = new LJCTestCommon("TestTableKeys");
      Run();
    }

    // Run the tests.
    private void Run()
    {
      // Collection Methods
      Clone();
      LJCHasItems();

      // Collection Data Methods
      LJCGetUnique();

      // Sort Methods
      LJCSortUnique();

      // Data Properties
      UniqueIndexer();
    }
    #endregion

    #region Data Object Methods

    // Creates and returns a clone of this object.
    private void Clone()
    {
      var methodName = "Clone()";

      var tableKeys = new TableKeys();

      var tableKey = new TableKey()
      {
        TableSchema = "TableSchema",
        DBName = "DBName",
        ConstraintName = "ConstraintName",
        KeyType = "KeyType",
        TableName = "TableName",
        ColumnName = "ColumnName",
        TargetTable = "TargetTable",
        TargetColumn = "TargetColumn",
        OrdinalPosition = 1,
        UniqueConstraintName = "UniqueConstraintName",
      };
      tableKeys.Add(tableKey);

      // Test Method
      var newTableKeys = tableKeys.Clone();

      var newTableKey = newTableKeys[0];

      var result = newTableKey.ColumnName;
      result += $", {newTableKey.ConstraintName}";
      var compare = "ColumnName, ConstraintName";
      TestCommon.Write($"{methodName}1", result, compare);

      result = newTableKey.DBName;
      result += $", {newTableKey.KeyType}";
      compare = "DBName, KeyType";
      TestCommon.Write($"{methodName}2", result, compare);

      result = $"{newTableKey.OrdinalPosition}";
      result += $", {newTableKey.TableName}";
      compare = "1, TableName";
      TestCommon.Write($"{methodName}3", result, compare);

      result = newTableKey.TableSchema;
      result += $", {newTableKey.TargetColumn}";
      compare = "TableSchema, TargetColumn";
      TestCommon.Write($"{methodName}4", result, compare);

      result = newTableKey.TargetTable;
      result += $", {newTableKey.UniqueConstraintName}";
      compare = "TargetTable, UniqueConstraintName";
      TestCommon.Write($"{methodName}5", result, compare);
    }

    // Checks if the collection has items.
    private void LJCHasItems()
    {
      var methodName = "LJCHasItems()";

      var tableKeys = new TableKeys();

      // Test Method
      var result = tableKeys.LJCHasItems().ToString();
      var compare = "False";
      TestCommon.Write($"{methodName}1", result, compare);

      var tableKey = new TableKey()
      {
        TableSchema = "TableSchema",
        DBName = "DBName",
        ConstraintName = "ConstraintName",
        KeyType = "KeyType",
        TableName = "TableName",
        ColumnName = "ColumnName",
        TargetTable = "TargetTable",
        TargetColumn = "TargetColumns",
        OrdinalPosition = 1,
        UniqueConstraintName = "UniqueConstraintName",
      };
      tableKeys.Add(tableKey);

      result = tableKeys.LJCHasItems().ToString();
      compare = "True";
      TestCommon.Write($"{methodName}2", result, compare);
    }
    #endregion

    #region Collection Data Methods

    // Retrieve the collection item with unique values.
    private void LJCGetUnique()
    {
      var methodName = "LJCGetWithUnique()";

      var tableKeys = new TableKeys();

      var tableKey = new TableKey()
      {
        TableSchema = "TableSchema",
        DBName = "DBName",
        ConstraintName = "ConstraintName",
        KeyType = "KeyType",
        TableName = "TableName",
        ColumnName = "ColumnName",
        TargetTable = "TargetTable",
        TargetColumn = "TargetColumns",
        OrdinalPosition = 1,
        UniqueConstraintName = "UniqueConstraintName",
      };
      tableKeys.Add(tableKey);

      // Test Method
      var item = tableKeys.LJCGetUnique("ConstraintName", 1);

      var result = "";
      if (item != null)
      {
        result = item.ConstraintName;
      }
      var compare = "ConstraintName";
      TestCommon.Write($"{methodName}", result, compare);
    }

    #endregion

    #region Sort Methods

    // Sort on Unique values.
    private void LJCSortUnique()
    {
      var methodName = "LJCSortUnique()";

      var tableKeys = new TableKeys();

      var tableKey = new TableKey()
      {
        TableSchema = "TableSchema",
        DBName = "DBName",
        ConstraintName = "ConstraintName",
        KeyType = "KeyType",
        TableName = "TableName",
        ColumnName = "ColumnName2",
        TargetColumn = "TargetColumns",
        TargetTable = "TargetTable",
        OrdinalPosition = 2,
        UniqueConstraintName = "UniqueConstraintName",
      };
      tableKeys.Add(tableKey);

      tableKey = new TableKey()
      {
        TableSchema = "TableSchema",
        DBName = "DBName",
        ConstraintName = "ConstraintName",
        KeyType = "KeyType",
        TableName = "TableName",
        ColumnName = "ColumnName",
        TargetTable = "TargetTable",
        TargetColumn = "TargetColumns",
        OrdinalPosition = 1,
        UniqueConstraintName = "UniqueConstraintName",
      };
      tableKeys.Add(tableKey);

      // Test Method
      tableKeys.LJCSortUnique();

      tableKey = tableKeys[0];
      var result = tableKey.ConstraintName;
      var compare = "ConstraintName";
      TestCommon.Write($"{methodName}", result, compare);
    }
    #endregion

    #region Data Properties

    // Creates and returns a clone of this object.
    private void UniqueIndexer()
    {
      var methodName = "UniqueIndexer()";

      var tableKeys = new TableKeys();

      var tableKey = new TableKey()
      {
        TableSchema = "TableSchema",
        DBName = "DBName",
        ConstraintName = "ConstraintName",
        KeyType = "KeyType",
        TableName = "TableName",
        ColumnName = "ColumnName",
        TargetTable = "TargetTable",
        TargetColumn = "TargetColumns",
        OrdinalPosition = 1,
        UniqueConstraintName = "UniqueConstraintName",
      };
      tableKeys.Add(tableKey);

      tableKey = new TableKey()
      {
        TableSchema = "TableSchema",
        DBName = "DBName",
        ConstraintName = "ConstraintName",
        KeyType = "KeyType",
        TableName = "TableName",
        ColumnName = "ColumnName2",
        TargetTable = "TargetTable",
        TargetColumn = "TargetColumns",
        OrdinalPosition = 2,
        UniqueConstraintName = "UniqueConstraintName",
      };
      tableKeys.Add(tableKey);

      tableKey = tableKeys["ConstraintName", 2];
      var result = "";
      if (tableKey != null)
      {
        result = tableKey.ColumnName;
      }
      var compare = "ColumnName2";
      TestCommon.Write($"{methodName}", result, compare);
    }
    #endregion
  }
}
