// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// ManagerData.cs
using LJCDBClientLib;
using LJCDBMessage;
using LJCWinFormControls;

namespace LJCGridDataTests
{
  // Contains the LJCDataGrid DataManager methods.
  internal class ManagerData
  {
    // Initializes the object instance.
    public ManagerData(LJCDataGrid ljcGrid, DataManager dataManager)
    {
      mLJCGrid = ljcGrid;
      mDataManager = dataManager;
    }

    // Loads the grid rows from the result Rows.
    public void LoadRows()
    {
      var dbResult = mDataManager.Load();
      if (DbResult.HasData(dbResult))
      {
        // Load the grid rows.
        foreach (DbRow dbRow in dbResult.Rows)
        {
          var gridRow = mLJCGrid.LJCRowAdd();
          gridRow.LJCSetValues(mLJCGrid, dbRow.Values);
        }
      }
    }

    private readonly DataManager mDataManager;
    private readonly LJCDataGrid mLJCGrid;
  }
}
