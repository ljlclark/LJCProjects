using LJCDataUtilityDAL;
using LJCWinFormControls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace LJCDataUtility
{
  internal class DataTableGridCode
  {
    #region Constructors

    internal DataTableGridCode(DataUtilityList parentList)
    {
      // Initialize property values.
      UtilityList = parentList;
      UtilityList.Cursor = Cursors.WaitCursor;
      TableGrid = UtilityList.MapTableGrid;
      Managers = UtilityList.Managers;
      TableManager = Managers.DataModuleManager;
      var fontFamily = "Microsoft Sans Serif";
      var style = FontStyle.Bold;
      TableGrid.Font = new Font(fontFamily, 12, style);
      UtilityList.Cursor = Cursors.Default;
    }
    #endregion

    #region Data Methods

    // Retrieves the list rows.
    internal void DataRetrieve()
    {
      UtilityList.Cursor = Cursors.WaitCursor;
      var items = TableManager.Load();
      if (items != null
        && items.Count > 0)
      {
        foreach (var item in items)
        {
          var row = TableGrid.LJCRowAdd();
          row.LJCSetValues(TableGrid, item);
        }
      }
      UtilityList.Cursor = Cursors.Default;
    }
    #endregion

    #region Setup and Other Methods

    // Configures the Grid.
    internal void SetupGrid()
    {
      // Setup default grid columns if no columns are defined.
      if (0 == TableGrid.Columns.Count)
      {
        List<string> propertyNames = new List<string>()
        {
          DataTable.ColumnDataModuleID,
          DataTable.ColumnName,
          DataTable.ColumnDescription
        };

        // Get the grid columns from the manager Data Definition.
        var gridColumns = TableManager.GetColumns(propertyNames);

        // Setup the grid columns.
        TableGrid.LJCAddColumns(gridColumns);
      }
    }
    #endregion

    #region Properties

    // Gets or sets the Managers reference.
    private ManagersDataUtility Managers { get; set; }

    // Gets or sets the Grid reference.
    private LJCDataGrid TableGrid { get; set; }

    // Gets or sets the Manager reference.
    private DataModuleManager TableManager { get; set; }

    // Gets or sets the Parent List reference.
    private DataUtilityList UtilityList { get; set; }
    #endregion
  }
}
