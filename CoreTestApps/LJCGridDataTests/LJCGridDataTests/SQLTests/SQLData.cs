// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// SQLData.cs
using LJCGridDataLib;
using LJCNetCommon;
using LJCWinFormControls;
using System.Data;

namespace LJCGridDataTests
{
  // Contains the LJCDataGrid SQL data methods.
  internal class SQLData
  {
    // Initializes the object instance.
    public SQLData(LJCDataGrid ljcGrid, DataTable dataTable)
    {
      mLJCGrid = ljcGrid;
      mDataTable = dataTable;
    }

    // Loads the grid rows from the DataRows collection.
    public void LoadRows(TableGridData tableGridData)
    {
      if (NetCommon.HasData(mDataTable))
      {
        // Load the grid rows.
        tableGridData.LoadRows(mDataTable);
      }
    }

    // Adds a grid row and updates it with the DbValues.
    public void RowAdd(TableGridData tableGridData)
    {
      if (NetCommon.HasData(mDataTable))
      {
        // Load the grid rows individually.
        foreach (DataRow dataRow in mDataTable.Rows)
        {
          var ljcGridRow = mLJCGrid.LJCRowAdd();
          tableGridData.RowSetValues(ljcGridRow, dataRow);
        }
      }
    }

    // Updates a grid row with the DbValues.
    public void RowSetValues(TableGridData tableGridData)
    {
      if (NetCommon.HasData(mDataTable))
      {
        // Create and load the grid rows individually.
        foreach (DataRow row in mDataTable.Rows)
        {
          var gridRow = mLJCGrid.LJCRowAdd();
          tableGridData.RowSetValues(gridRow, row);
        }
      }
    }

    private readonly DataTable mDataTable;
    private readonly LJCDataGrid mLJCGrid;
  }
}
