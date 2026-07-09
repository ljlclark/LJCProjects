
namespace TestDataUtilityDAL
{
  internal class DataUtilityDALProgram
  {
    static void Main()
    {
      var testDataModule = new TestDataModule();
      testDataModule.Run();
      var testDataModules = new TestDataModules();
      testDataModules.Run();

      var testDataTable = new TestDataUtilTable();
      testDataTable.Run();
      var testDataTables = new TestDataTables();
      testDataTables.Run();
      var testDataTableManager = new TestDataTableManager();
      testDataTableManager.Run();

      var testDataColumn = new TestDataColumn();
      testDataColumn.Run();
      var testDataColumns = new TestDataColumns();
      testDataColumns.Run();

      var testDataKey = new TestDataKey();
      testDataKey.Run();

      // Test collection of DbColumns.
      var testDataTable2 = new TestDataTable2();
      testDataTable2.Run();
      var testDataTables2 = new TestDataTables2();
      testDataTables2.Run();
    }
  }
}
