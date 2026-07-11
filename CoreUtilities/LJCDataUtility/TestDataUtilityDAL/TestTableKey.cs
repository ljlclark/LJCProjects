// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// TestTableKey.cs
using LJCDataUtilityDAL;
using LJCNetCommon;

namespace TestDataUtilityDAL
{
  // Tests the DataModule object.
  internal class TestTableKey
  {
    #region Constructor Methods

    // Initializes an object instance.
    public TestTableKey()
    {
      TestCommon = new TestCommon("TestTableKey");
    }

    // Run the tests.
    public void Run()
    {
      // Data Object Methods
      Clone();
      CompareTo();

      // Data Properties
      DBName();
      TableSchema();
      TableName();
      KeyType();
      ColumnName();
      ConstraintName();
      TargetTable();
      UpdateRule();
      DeleteRule();
      OrdinalPosition();
    }
    #endregion

    #region Data Object Methods

    // Creates and returns a clone of this object.
    public void Clone()
    {
      var methodName = "Clone()";

      var tableKey = new TableKey
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

      // Test Method
      var newTableKey = tableKey.Clone();

      var result = newTableKey.DBName;
      var compare = "DBName";
      TestCommon.Write($"{methodName}1", result, compare);

      result = newTableKey.TableSchema;
      compare = "TableSchema";
      TestCommon.Write($"{methodName}2", result, compare);

      result = newTableKey.TableName;
      compare = "TableName";
      TestCommon.Write($"{methodName}3", result, compare);

      result = newTableKey.KeyType;
      compare = "KeyType";
      TestCommon.Write($"{methodName}4", result, compare);

      result = newTableKey.ColumnName;
      compare = "ColumnName";
      TestCommon.Write($"{methodName}5", result, compare);

      result = newTableKey.ConstraintName;
      compare = "ConstraintName";
      TestCommon.Write($"{methodName}6", result, compare);

      result = newTableKey.UpdateRule;
      compare = "UpdateRule";
      TestCommon.Write($"{methodName}7", result, compare);

      result = newTableKey.DeleteRule;
      compare = "DeleteRule";
      TestCommon.Write($"{methodName}8", result, compare);

      result = newTableKey.TargetTable;
      compare = "TargetTable";
      TestCommon.Write($"{methodName}9", result, compare);

      result = newTableKey.TargetColumns;
      compare = "TargetColumns";
      TestCommon.Write($"{methodName}10", result, compare);

      result = $"{newTableKey.OrdinalPosition}";
      compare = "1";
      TestCommon.Write($"{methodName}11", result, compare);

      result = newTableKey.UniqueConstraintName;
      compare = "UniqueConstraintName";
      TestCommon.Write($"{methodName}12", result, compare);
    }

    // Provides the default Sort functionality.
    public void CompareTo()
    {
      var methodName = "CompareTo()";

      var tableKey = new TableKey
      {
        ConstraintName = "Alpha",
        OrdinalPosition = 1,
      };
      var toTableKey = new TableKey
      {
        ConstraintName = "Alpha",
        OrdinalPosition = 2,
      };

      var result = tableKey.CompareTo(toTableKey).ToString();
      var compare = "-1";
      TestCommon.Write($"{methodName}1", result, compare);

      result = toTableKey.CompareTo(tableKey).ToString();
      compare = "1";
      TestCommon.Write($"{methodName}2", result, compare);

      toTableKey.OrdinalPosition = 1;
      result = tableKey.CompareTo(toTableKey).ToString();
      compare = "0";
      TestCommon.Write($"{methodName}3", result, compare);
    }
    #endregion

    #region Data Properties

    // Change DBName
    public void DBName()
    {
      var methodName = "DBName()";

      // Test Method
      var tableKey = new TableKey
      {
        // Original Value
        DBName = "",
      };

      var result = tableKey.DBName;
      var compare = "No Result";
      TestCommon.Write($"{methodName}1", result, compare);

      tableKey.DBName = "DBName";
      result = tableKey.DBName;
      compare = "DBName";
      TestCommon.Write($"{methodName}2", result, compare);
    }

    // Change TableSchema
    public void TableSchema()
    {
      var methodName = "TableSchema()";

      // Test Method
      var tableKey = new TableKey
      {
        // Original Value
        TableSchema = null,
      };

      var result = tableKey.TableSchema;
      var compare = "No Result";
      TestCommon.Write($"{methodName}1", result, compare);

      tableKey.TableSchema = "TableSchema";
      result = tableKey.TableSchema;
      compare = "TableSchema";
      TestCommon.Write($"{methodName}2", result, compare);
    }

    // Change TableSchema
    public void TableName()
    {
      var methodName = "TableName()";

      // Test Method
      var tableKey = new TableKey
      {
        // Original Value
        TableName = null,
      };

      var result = tableKey.TableName;
      var compare = "No Result";
      TestCommon.Write($"{methodName}1", result, compare);

      tableKey.TableName = "TableName";
      result = tableKey.TableName;
      compare = "TableName";
      TestCommon.Write($"{methodName}2", result, compare);
    }

    // Change KeyType
    public void KeyType()
    {
      var methodName = "KeyType()";

      // Test Method
      var tableKey = new TableKey
      {
        // Original Value
        KeyType = null,
      };

      var result = tableKey.KeyType;
      var compare = "No Result";
      TestCommon.Write($"{methodName}1", result, compare);

      tableKey.KeyType = "KeyType";
      result = tableKey.KeyType;
      compare = "KeyType";
      TestCommon.Write($"{methodName}2", result, compare);
    }

    // Change ColumnName
    public void ColumnName()
    {
      var methodName = "ColumnName()";

      // Test Method
      var tableKey = new TableKey
      {
        // Original Value
        ColumnName = null,
      };

      var result = tableKey.ColumnName;
      var compare = "No Result";
      TestCommon.Write($"{methodName}1", result, compare);

      tableKey.ColumnName = "ColumnName";
      result = tableKey.ColumnName;
      compare = "ColumnName";
      TestCommon.Write($"{methodName}2", result, compare);
    }

    // Change ConstraintName
    public void ConstraintName()
    {
      var methodName = "ConstraintName()";

      // Test Method
      var tableKey = new TableKey
      {
        // Original Value
        ConstraintName = null,
      };

      var result = tableKey.ConstraintName;
      var compare = "No Result";
      TestCommon.Write($"{methodName}1", result, compare);

      tableKey.ConstraintName = "ConstraintName";
      result = tableKey.ConstraintName;
      compare = "ConstraintName";
      TestCommon.Write($"{methodName}2", result, compare);
    }

    // Change TargetTable
    public void TargetTable()
    {
      var methodName = "TargetTable()";

      // Test Method
      var tableKey = new TableKey
      {
        // Original Value
        TargetTable = null,
      };

      var result = tableKey.TargetTable;
      var compare = "No Result";
      TestCommon.Write($"{methodName}1", result, compare);

      tableKey.TargetTable = "TargetTable";
      result = tableKey.TargetTable;
      compare = "TargetTable";
      TestCommon.Write($"{methodName}2", result, compare);
    }

    // Change TargetColumns
    public void TargetColumns()
    {
      var methodName = "TargetColumns()";

      // Test Method
      var tableKey = new TableKey
      {
        // Original Value
        TargetColumns = null,
      };

      var result = tableKey.TargetColumns;
      var compare = "No Result";
      TestCommon.Write($"{methodName}1", result, compare);

      tableKey.TargetColumns = "TargetColumns";
      result = tableKey.TargetColumns;
      compare = "TargetColumns";
      TestCommon.Write($"{methodName}2", result, compare);
    }

    // Change UpdateRule
    public void UpdateRule()
    {
      var methodName = "UpdateRule()";

      // Test Method
      var tableKey = new TableKey
      {
        // Original Value
        UpdateRule = null,
      };

      var result = tableKey.UpdateRule;
      var compare = "No Result";
      TestCommon.Write($"{methodName}1", result, compare);

      tableKey.UpdateRule = "UpdateRule";
      result = tableKey.UpdateRule;
      compare = "UpdateRule";
      TestCommon.Write($"{methodName}2", result, compare);
    }

    // Change DeleteRule
    public void DeleteRule()
    {
      var methodName = "DeleteRule()";

      // Test Method
      var tableKey = new TableKey
      {
        // Original Value
        DeleteRule = null,
      };

      var result = tableKey.DeleteRule;
      var compare = "No Result";
      TestCommon.Write($"{methodName}1", result, compare);

      tableKey.DeleteRule = "DeleteRule";
      result = tableKey.DeleteRule;
      compare = "DeleteRule";
      TestCommon.Write($"{methodName}2", result, compare);
    }

    // Change OrdinalPosition
    public void OrdinalPosition()
    {
      var methodName = "OrdinalPosition()";

      // Test Method
      var tableKey = new TableKey
      {
        // Original Value
        OrdinalPosition = 0,
      };

      var result = $"{tableKey.OrdinalPosition}";
      var compare = "0";
      TestCommon.Write($"{methodName}1", result, compare);

      tableKey.OrdinalPosition = 1;
      result = $"{tableKey.OrdinalPosition}";
      compare = "1";
      TestCommon.Write($"{methodName}2", result, compare);
    }
    #endregion

    #region Properties

    // Gets or sets the TestCommon object.
    private TestCommon TestCommon { get; set; }
    #endregion
  }
}
