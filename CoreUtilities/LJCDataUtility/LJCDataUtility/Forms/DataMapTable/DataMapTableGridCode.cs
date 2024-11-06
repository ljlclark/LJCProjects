using LJCDataUtilityDAL;
using LJCWinFormControls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace LJCDataUtility
{
  internal class DataMapTableGridCode
  {
    internal DataMapTableGridCode(DataUtilityList parentList)
    {
      // Initialize property values.
      UtilityList = parentList;
      UtilityList.Cursor = Cursors.WaitCursor;
      MapTableGrid = UtilityList.MapTableGrid;
      Managers = UtilityList.Managers;
      MapTableManager = Managers.DataMapTableManager;
      var fontFamily = "Microsoft Sans Serif";
      var style = FontStyle.Bold;
      MapTableGrid.Font = new Font(fontFamily, 12, style);
      UtilityList.Cursor = Cursors.Default;
    }

    #region Data Methods

    // Retrieves the list rows.
    internal void DataRetrieve()
    {
      UtilityList.Cursor = Cursors.WaitCursor;
      var items = MapTableManager.Load();
      if (items != null
        && items.Count > 0)
      {
        foreach (var item in items)
        {
          var row = MapTableGrid.LJCRowAdd();
          row.LJCSetValues(MapTableGrid, item);
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
      if (0 == MapTableGrid.Columns.Count)
      {
        List<string> propertyNames = new List<string>()
        {
          DataMapTable.ColumnDataTableID,
          DataMapTable.ColumnNewTableName
        };

        // Get the grid columns from the manager Data Definition.
        var gridColumns = MapTableManager.GetColumns(propertyNames);

        // Setup the grid columns.
        MapTableGrid.LJCAddColumns(gridColumns);
      }
    }
    #endregion

    #region Properties

    // Gets or sets the Managers reference.
    private ManagersDataUtility Managers { get; set; }

    // Gets or sets the Grid reference.
    private LJCDataGrid MapTableGrid { get; set; }

    // Gets or sets the Manager reference.
    private DataMapTableManager MapTableManager { get; set; }

    // Gets or sets the Parent List reference.
    private DataUtilityList UtilityList { get; set; }
    #endregion
  }
}
