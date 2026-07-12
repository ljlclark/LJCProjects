
namespace TestDataUtilityDAL
{
  internal class DataUtilityDALProgram
  {
    static void Main()
    {
      _ = new TestDataModule();
      _ = new TestDataModules();
      _ = new TestDataModuleManager();

      _ = new TestDataUtilTable();
      _ = new TestDataTables();
      _ = new TestDataTableManager();

      _ = new TestDataUtilColumn();
      _ = new TestDataColumns();

      _ = new TestDataKey();
      _ = new TestDataKeys();

      _ = new TestTableKey();
      _ = new TestTableKeys();

      // Test collection of DbColumns.
      var testDataTable2 = new TestDataTable2();
      testDataTable2.Run();
      var testDataTables2 = new TestDataTables2();
      testDataTables2.Run();
    }
  }
}
