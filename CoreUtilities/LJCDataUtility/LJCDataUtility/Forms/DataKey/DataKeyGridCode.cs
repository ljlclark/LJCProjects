using LJCDataUtilityDAL;
using LJCWinFormControls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace LJCDataUtility
{
  internal class DataKeyGridCode
  {
    #region Constructors

    internal DataKeyGridCode(DataUtilityList parentList)
    {
      // Initialize property values.
      UtilityList = parentList;
      UtilityList.Cursor = Cursors.WaitCursor;
      KeyGrid = UtilityList.KeyGrid;
      Managers = UtilityList.Managers;
      KeyManager = Managers.DataKeyManager;

      var fontFamily = "Microsoft Sans Serif";
      var style = FontStyle.Bold;
      KeyGrid.Font = new Font(fontFamily, 12, style);
      UtilityList.Cursor = Cursors.Default;
    }
    #endregion

    #region Data Methods

    // Retrieves the list rows.
    internal void DataRetrieve()
    {
      UtilityList.Cursor = Cursors.WaitCursor;
      var items = KeyManager.Load();
      if (items != null
        && items.Count > 0)
      {
        foreach (var item in items)
        {
          var row = KeyGrid.LJCRowAdd();
          row.LJCSetValues(KeyGrid, item);
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
      if (0 == KeyGrid.Columns.Count)
      {
        List<string> propertyNames = new List<string>()
        {
          DataKey.ColumnDataTableID,
          DataKey.ColumnName,
          DataKey.ColumnKeyType,
          DataKey.ColumnSourceColumnName,
          DataKey.ColumnTargetTableName,
          DataKey.ColumnTargetColumnName
        };

        // Get the grid columns from the manager Data Definition.
        var gridColumns = KeyManager.GetColumns(propertyNames);

        // Setup the grid columns.
        KeyGrid.LJCAddColumns(gridColumns);
      }
    }
    #endregion

    #region Properties

    // Gets or sets the Manager reference.
    private DataKeyManager KeyManager { get; set; }

    // Gets or sets the Grid reference.
    private LJCDataGrid KeyGrid { get; set; }

    // Gets or sets the Managers reference.
    private ManagersDataUtility Managers { get; set; }

    // Gets or sets the Parent List reference.
    private DataUtilityList UtilityList { get; set; }
    #endregion
  }
}
