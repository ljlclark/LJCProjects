// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// GridDataTest.cs
using LJCWinFormControls;

namespace LJCGridDataTests
{
  // Contains the Grid tests.
  internal class GridDataTests
  {
    // Initializes the object instance.
    /// <include path='items/GridDataTestC/*' file='Doc/GridDataTest.xml'/>
    internal GridDataTests(LJCDataGrid ljcGrid)
    {
      mLJCGrid = ljcGrid;
    }

    internal void Run()
    {
      // *** Test Setting ***
      var testCase = TestCase.SQL;
      switch (testCase)
      {
        case TestCase.DataManager:
          var managerTest = new ManagerTests(mLJCGrid);
          managerTest.Run();
          break;

        case TestCase.SQL:
          var sqlTests = new SQLTests(mLJCGrid);
          //sqlTests.Run();
          sqlTests.DataRetrieve(mLJCGrid);

          var province = sqlTests.SelectWithSql();
          sqlTests.UpdateWithKeys(province);
          sqlTests.UpdateWithFilters(province);
          break;
      }
    }

    #region Class Data

    private readonly LJCDataGrid mLJCGrid;

    private enum TestCase
    {
      DataManager,
      SQL
    }
    #endregion
  }
}
