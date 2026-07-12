// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// TestTableKeys.cs
using LJCDataUtilityDAL;
using LJCNetCommon;

namespace TestDataUtilityDAL
{
  // Tests the DataModules collection.
  internal class TestTableKeys
  {
    #region Constructor Methods

    // Initializes an object instance.
    public TestTableKeys()
    {
      TestCommon = new TestCommon("TestTableKeys");
      Run();
    }

    // Run the tests.
    public void Run()
    {
      // Collection Methods
      Clone();
      LJCHasItems();

      // Collection Data Methods
      LJCGetWithUnique();

      // Sort Methods
      LJCSortUnique();

      // Data Properties
      UniqueIndexer();
    }
    #endregion

    #region Data Object Methods

    // Creates and returns a clone of this object.
    public void Clone()
    {
      var methodName = "Clone()";

      var tableKeys = new TableKeys();

      var tableKey = new TableKey()
      {
        DBName = "DBName",
        TableSchema = "TableSchema",
        TableName = "TableName",
        KeyType = "KeyType",
        ColumnName = "ColumnName",
        ConstraintName = "ConstraintName",
        UpdateRule = "UpdateRule",
        DeleteRule = "DeleteRule",
        TargetTable = "TargetTable",
        TargetColumns = "TargetColumns",
        OrdinalPosition = 1,
        UniqueConstraintName = "UniqueConstraintName",
      };
      tableKeys.Add(tableKey);

      // Test Method
      var newTableKeys = tableKeys.Clone();

      var newTableKey = newTableKeys[0];

      var result = newTableKey.DBName;
      result += $", {newTableKey.TableSchema}";
      var compare = "DBName, TableSchema";
      TestCommon.Write($"{methodName}1", result, compare);

      result = newTableKey.TableName;
      result += $", {newTableKey.KeyType}";
      compare = "TableName, KeyType";
      TestCommon.Write($"{methodName}2", result, compare);

      result = newTableKey.ColumnName;
      result += $", {newTableKey.ConstraintName}";
      compare = "ColumnName, ConstraintName";
      TestCommon.Write($"{methodName}3", result, compare);

      result = newTableKey.UpdateRule;
      result += $", {newTableKey.DeleteRule}";
      compare = "UpdateRule, DeleteRule";
      TestCommon.Write($"{methodName}4", result, compare);

      result = newTableKey.TargetTable;
      result += $", {newTableKey.TargetColumns}";
      compare = "TargetTable, TargetColumns";
      TestCommon.Write($"{methodName}5", result, compare);

      result = $"{newTableKey.OrdinalPosition}";
      result += $", {newTableKey.UniqueConstraintName}";
      compare = "1, UniqueConstraintName";
      TestCommon.Write($"{methodName}6", result, compare);
    }

    // Checks if the collection has items.
    public void LJCHasItems()
    {
      var methodName = "LJCHasItems()";

      var tableKeys = new TableKeys();

      // Test Method
      var result = tableKeys.LJCHasItems().ToString();
      var compare = "False";
      TestCommon.Write($"{methodName}1", result, compare);

      var tableKey = new TableKey()
      {
        DBName = "DBName",
        TableSchema = "TableSchema",
        TableName = "TableName",
        KeyType = "KeyType",
        ColumnName = "ColumnName",
        ConstraintName = "ConstraintName",
        UpdateRule = "UpdateRule",
        DeleteRule = "DeleteRule",
        TargetTable = "TargetTable",
        TargetColumns = "TargetColumns",
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
    public void LJCGetWithUnique()
    {
      var methodName = "LJCGetWithUnique()";

      var tableKeys = new TableKeys();

      var tableKey = new TableKey()
      {
        DBName = "DBName",
        TableSchema = "TableSchema",
        TableName = "TableName",
        KeyType = "KeyType",
        ColumnName = "ColumnName",
        ConstraintName = "ConstraintName",
        UpdateRule = "UpdateRule",
        DeleteRule = "DeleteRule",
        TargetTable = "TargetTable",
        TargetColumns = "TargetColumns",
        OrdinalPosition = 1,
        UniqueConstraintName = "UniqueConstraintName",
      };
      tableKeys.Add(tableKey);

      // Test Method
      var item = tableKeys.LJCGetWithUnique("ConstraintName", 1);

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
    public void LJCSortUnique()
    {
      var methodName = "LJCSortUnique()";

      var tableKeys = new TableKeys();

      var tableKey = new TableKey()
      {
        DBName = "DBName",
        TableSchema = "TableSchema",
        TableName = "TableName",
        KeyType = "KeyType",
        ColumnName = "ColumnName2",
        ConstraintName = "ConstraintName",
        UpdateRule = "UpdateRule",
        DeleteRule = "DeleteRule",
        TargetTable = "TargetTable",
        TargetColumns = "TargetColumns",
        OrdinalPosition = 2,
        UniqueConstraintName = "UniqueConstraintName",
      };
      tableKeys.Add(tableKey);

      tableKey = new TableKey()
      {
        DBName = "DBName",
        TableSchema = "TableSchema",
        TableName = "TableName",
        KeyType = "KeyType",
        ColumnName = "ColumnName",
        ConstraintName = "ConstraintName",
        UpdateRule = "UpdateRule",
        DeleteRule = "DeleteRule",
        TargetTable = "TargetTable",
        TargetColumns = "TargetColumns",
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
    public void UniqueIndexer()
    {
      var methodName = "UniqueIndexer()";

      var tableKeys = new TableKeys();

      var tableKey = new TableKey()
      {
        DBName = "DBName",
        TableSchema = "TableSchema",
        TableName = "TableName",
        KeyType = "KeyType",
        ColumnName = "ColumnName",
        ConstraintName = "ConstraintName",
        UpdateRule = "UpdateRule",
        DeleteRule = "DeleteRule",
        TargetTable = "TargetTable",
        TargetColumns = "TargetColumns",
        OrdinalPosition = 1,
        UniqueConstraintName = "UniqueConstraintName",
      };
      tableKeys.Add(tableKey);

      tableKey = new TableKey()
      {
        DBName = "DBName",
        TableSchema = "TableSchema",
        TableName = "TableName",
        KeyType = "KeyType",
        ColumnName = "ColumnName2",
        ConstraintName = "ConstraintName",
        UpdateRule = "UpdateRule",
        DeleteRule = "DeleteRule",
        TargetTable = "TargetTable",
        TargetColumns = "TargetColumns",
        OrdinalPosition = 2,
        UniqueConstraintName = "UniqueConstraintName",
      };
      tableKeys.Add(tableKey);

      tableKey = tableKeys["ConstraintName", 2];
      var result = tableKey.ColumnName;
      var compare = "ColumnName2";
      TestCommon.Write($"{methodName}", result, compare);
    }
    #endregion

    #region Properties

    // Gets or sets the TestCommon object.
    private TestCommon TestCommon { get; set; }
    #endregion
  }
}
