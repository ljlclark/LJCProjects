
namespace TestDataUtilityDAL
{
  internal class DataUtilityDALProgram
  {
    static void Main()
    {
      var testDataTable = new TestDataUtilTable();
      testDataTable.Run();

      var testDataTableManager = new TestDataTableManager();
      testDataTableManager.Run();

      // Test collection of DbColumns.
      var testDataTable2 = new TestDataTable2();
      testDataTable2.Run();

      var testDataTables2 = new TestDataTables2();
      testDataTables2.Run();
    }
  }
}
