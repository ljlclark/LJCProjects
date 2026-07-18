// Copyright (c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// DataUtilityDALProgram.cs

namespace TestDataUtilityDAL
{
  internal class DataUtilityDALProgram
  {
    static void Main()
    {
      // Test DAL
      _ = new TestDataModule();
      _ = new TestDataModules();
      _ = new TestDataModuleManager();

      _ = new TestDataUtilTable();
      _ = new TestDataTables();
      _ = new TestDataTableManager();

      _ = new TestDataUtilColumn();
      _ = new TestDataColumns();
      _ = new TestDataColumnManager();

      _ = new TestDataKey();
      _ = new TestDataKeys();
      _ = new TestDataKeyManager();

      _ = new TestTableKey();
      _ = new TestTableKeys();
      _ = new TestTableKeyManager();

      // Test collection of DbColumns.
      _ = new TestDataTable2();
      _ = new TestDataTables2();
    }
  }
}
