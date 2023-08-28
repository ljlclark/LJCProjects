// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// GridDataTest.cs
using LJCDataAccess;
using LJCWinFormControls;
using System.Data.Common;

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

          // DataAccess Methods
          var dataAccess = GetDataAccess(out string connectionString
            , out string providerName);
          sqlTests.DataAccessRetrieve(dataAccess, mLJCGrid);
          var ljcGridRow = mLJCGrid.CurrentRow as LJCGridRow;
          sqlTests.GetRowDataObject(dataAccess, ljcGridRow);

          // SQLManager Methods
          var province = sqlTests.Retrieve(connectionString, providerName);
          sqlTests.RetrieveWithRowValues(mLJCGrid, connectionString
            , providerName);
          sqlTests.UpdateWithKeys(province, connectionString, providerName);
          sqlTests.UpdateWithFilters(province, connectionString, providerName);
          sqlTests.Add(province, connectionString, providerName);

          // ProvinceSQLManager
          sqlTests.AddProvince(connectionString, providerName);
          sqlTests.LoadProvince(mLJCGrid, connectionString
            , providerName);
          province = sqlTests.RetrieveProvince(connectionString , providerName);
          sqlTests.UpdateProvince(province, connectionString, providerName);
          break;
      }
    }

    internal DataAccess GetDataAccess(out string connectionString
      , out string providerName)
    {
      DbConnectionStringBuilder connectionBuilder;
      connectionBuilder = new DbConnectionStringBuilder()
          {
            { "Data Source", "DESKTOP-PDPBE34" },
            { "Initial Catalog", "LJCData" },
            { "Integrated Security", "True" }
          };
      connectionString = connectionBuilder.ConnectionString;
      providerName = "System.Data.SqlClient";
      var retValue = new DataAccess(connectionString, providerName);
      return retValue;
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
