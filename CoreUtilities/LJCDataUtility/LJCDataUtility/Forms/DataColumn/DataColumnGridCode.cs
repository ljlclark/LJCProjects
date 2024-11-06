using LJCDataUtilityDAL;
using LJCWinFormControls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace LJCDataUtility
{
  internal class DataColumnGridCode
  {
    #region Constructors

    internal DataColumnGridCode(DataUtilityList parentList)
    {
      // Initialize property values.
      UtilityList = parentList;
      UtilityList.Cursor = Cursors.WaitCursor;
      ColumnGrid = UtilityList.ColumnGrid;
      Managers = UtilityList.Managers;
      ColumnManager = Managers.DataColumnManager;

      var fontFamily = "Microsoft Sans Serif";
      var style = FontStyle.Bold;
      ColumnGrid.Font = new Font(fontFamily, 12, style);
      UtilityList.Cursor = Cursors.Default;
    }
    #endregion

    #region Data Methods

    // Retrieves the list rows.
    internal void DataRetrieve()
    {
      UtilityList.Cursor = Cursors.WaitCursor;
      var items = ColumnManager.Load();
      if (items != null
        && items.Count > 0)
      {
        foreach (var item in items)
        {
          var row = ColumnGrid.LJCRowAdd();
          row.LJCSetValues(ColumnGrid, item);
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
      if (0 == ColumnGrid.Columns.Count)
      {
        List<string> propertyNames = new List<string>()
        {
          DataColumn.ColumnDataTableID,
          DataColumn.ColumnName,
          DataColumn.ColumnDescription,
          DataColumn.ColumnSequence,
          DataColumn.ColumnTypeName,
          DataColumn.ColumnMaxLength
        };

        // Get the grid columns from the manager Data Definition.
        var gridColumns = ColumnManager.GetColumns(propertyNames);

        // Setup the grid columns.
        ColumnGrid.LJCAddColumns(gridColumns);
      }
    }
    #endregion

    #region Properties

    // Gets or sets the Grid reference.
    private LJCDataGrid ColumnGrid { get; set; }

    // Gets or sets the Manager reference.
    private DataColumnManager ColumnManager { get; set; }

    // Gets or sets the Managers reference.
    private ManagersDataUtility Managers { get; set; }

    // Gets or sets the Parent List reference.
    private DataUtilityList UtilityList { get; set; }
    #endregion
  }
}
