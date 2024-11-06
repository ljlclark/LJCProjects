using LJCDataUtilityDAL;
using LJCWinFormControls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace LJCDataUtility
{
  internal class DataModuleGridCode
  {
    #region Constructors

    internal DataModuleGridCode(DataUtilityList parentList)
    {
      // Initialize property values.
      UtilityList = parentList;
      UtilityList.Cursor = Cursors.WaitCursor;
      ModuleGrid = UtilityList.MapTableGrid;
      Managers = UtilityList.Managers;
      ModuleManager = Managers.DataModuleManager;
      var fontFamily = "Microsoft Sans Serif";
      var style = FontStyle.Bold;
      ModuleGrid.Font = new Font(fontFamily, 12, style);
      UtilityList.Cursor = Cursors.Default;
    }
    #endregion

    #region Data Methods

    // Retrieves the list rows.
    internal void DataRetrieve()
    {
      UtilityList.Cursor = Cursors.WaitCursor;
      var items = ModuleManager.Load();
      if (items != null
        && items.Count > 0)
      {
        foreach (var item in items)
        {
          var row = ModuleGrid.LJCRowAdd();
          row.LJCSetValues(ModuleGrid, item);
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
      if (0 == ModuleGrid.Columns.Count)
      {
        List<string> propertyNames = new List<string>()
        {
          DataModule.ColumnName,
          DataModule.ColumnDescription
        };

        // Get the grid columns from the manager Data Definition.
        var gridColumns = ModuleManager.GetColumns(propertyNames);

        // Setup the grid columns.
        ModuleGrid.LJCAddColumns(gridColumns);
      }
    }
    #endregion

    #region Properties

    // Gets or sets the Managers reference.
    private ManagersDataUtility Managers { get; set; }

    // Gets or sets the Grid reference.
    private LJCDataGrid ModuleGrid { get; set; }

    // Gets or sets the Manager reference.
    private DataModuleManager ModuleManager { get; set; }

    // Gets or sets the Parent List reference.
    private DataUtilityList UtilityList { get; set; }
    #endregion
  }
}
