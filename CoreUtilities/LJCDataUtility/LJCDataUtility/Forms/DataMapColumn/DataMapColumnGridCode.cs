using LJCDataUtilityDAL;
using LJCWinFormControls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace LJCDataUtility
{
  internal class DataMapColumnGridCode
  {
    #region Constructors

    internal DataMapColumnGridCode(DataUtilityList parentList)
    {
      // Initialize property values.
      UtilityList = parentList;
      UtilityList.Cursor = Cursors.WaitCursor;
      MapColumnGrid = UtilityList.KeyGrid;
      Managers = UtilityList.Managers;
      MapColumnManager = Managers.DataMapColumnManager;
      var fontFamily = "Microsoft Sans Serif";
      var style = FontStyle.Bold;
      MapColumnGrid.Font = new Font(fontFamily, 12, style);
      UtilityList.Cursor = Cursors.Default;
    }
    #endregion

    #region Data Methods

    // Retrieves the list rows.
    internal void DataRetrieve()
    {
      UtilityList.Cursor = Cursors.WaitCursor;
      var items = MapColumnManager.Load();
      if (items != null
        && items.Count > 0)
      {
        foreach (var item in items)
        {
          var row = MapColumnGrid.LJCRowAdd();
          row.LJCSetValues(MapColumnGrid, item);
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
      if (0 == MapColumnGrid.Columns.Count)
      {
        List<string> propertyNames = new List<string>()
        {
          DataMapColumn.ColumnDataTableMapID,
          DataMapColumn.ColumnDataColumnID,
          DataMapColumn.ColumnColumnName,
          DataMapColumn.ColumnSequence,
          DataMapColumn.ColumnIsDelete,
          DataMapColumn.ColumnMaxLength
        };

        // Get the grid columns from the manager Data Definition.
        var gridColumns = MapColumnManager.GetColumns(propertyNames);

        // Setup the grid columns.
        MapColumnGrid.LJCAddColumns(gridColumns);
      }
    }
    #endregion

    #region Properties

    // Gets or sets the Managers reference.
    private ManagersDataUtility Managers { get; set; }

    // Gets or sets the Grid reference.
    private LJCDataGrid MapColumnGrid { get; set; }

    // Gets or sets the Manager reference.
    private DataMapColumnManager MapColumnManager { get; set; }

    // Gets or sets the Parent List reference.
    private DataUtilityList UtilityList { get; set; }
    #endregion
  }
}
