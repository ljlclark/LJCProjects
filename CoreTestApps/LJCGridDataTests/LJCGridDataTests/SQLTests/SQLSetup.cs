// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// SQLSetup.cs
using LJCDataAccess;
using LJCGridDataLib;
using LJCNetCommon;
using LJCWinFormControls;
using System.Data;

namespace LJCGridDataTests
{
  // Represents the SQL LJCDataGrid setup.
  internal class SQLSetup
  {
    public SQLSetup(LJCDataGrid ljcGrid, DataAccess dataAccess)
    {
      mLJCGrid = ljcGrid;
      mDataAccess = dataAccess;

      var sql = "select * from Province";

      // This also gets the MaxLength.
      DataTable = mDataAccess.GetSchemaOnly(sql);
      mDataAccess.FillDataTable(sql, DataTable);

      TableGridData = new TableGridData(mLJCGrid);
    }

    // Sets the Display Columns from the DataColumns object.
    public DbColumns ColumnsFromTable()
    {
      DbColumns retValue = null;

      if (NetCommon.HasColumns(DataTable))
      {
        TableGridData.SetDisplayColumns(DataTable.Columns);
        retValue = TableGridData.DisplayColumns;
      }
      return retValue;
    }

    // Sets the Display Columns from the DataObject properties.
    public DbColumns ColumnsFromDataObject()
    {
      var province = new Province();
      var dataDefinition = TableGridData.GetDbColumns(DataTable.Columns);
      TableGridData.SetDisplayColumns(province, dataDefinition);
      var retValue = TableGridData.DisplayColumns;
      return retValue;
    }

    #region Properties

    public DataTable DataTable { get; set; }

    public TableGridData TableGridData { get; set; }
    #endregion

    private readonly DataAccess mDataAccess;
    private readonly LJCDataGrid mLJCGrid;
  }
}
