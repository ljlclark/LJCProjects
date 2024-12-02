// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DataModuleGridCode.cs
using static LJCDataUtility.DataUtilityList;
using LJCDataUtilityDAL;
using LJCNetCommon;
using LJCWinFormCommon;
using LJCWinFormControls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using LJCDataUtility;

// Data Methods
//   internal void DataRetrieve()
//   private LJCGridRow RowAdd(DataModule dataRecord)
//   private void RowUpdate(DataModule dataRecord)
//   private void SetControlState()
//   private void SetStoredValues(LJCGridRow row, DataModule dataRecord)
//   private bool RowSelect(DataModule dataRecord)
// Action Methods
//   internal void Delete()
//   internal void Edit()
//   internal void New()
//   internal void Refresh()
//   internal void ShowHelp()
//   private void Detail_Change(object sender, EventArgs e)
// Setup and Other Methods
//   internal void SetupGrid()
// Action Event Handlers
//   private void ModuleNew_Click(object sender, EventArgs e)
//   private void ModuleEdit_Click(object sender, EventArgs e)
//   private void ModuleDelete_Click(object sender, EventArgs e)
//   private void ModuleRefresh_Click(object sender, EventArgs e)
// Control Event Handlers
//   private void GridFont_FontChange(object sender, EventArgs e)
//   private void MenuFont_FontChange(object sender, EventArgs e)
//   private void ModuleGrid_KeyDown(object sender, KeyEventArgs e)
//   private void ModuleGrid_MouseDoubleClick(object sender, MouseEventArgs e)
//   private void ModuleGrid_MouseDown(object sender, MouseEventArgs e)
//   private void ModuleGrid_SelectionChanged(object sender, EventArgs e)

namespace LJCDataUtility
{
  // Provides DataModuleGrid methods for the DataUtilityList window.
  internal class DataModuleGridCode
  {
    // ******************************
    #region Constructors
    // ******************************

    // Initializes an object instance.
    // ********************
    internal DataModuleGridCode(DataUtilityList parentList)
    {
      // Initialize property values.
      UtilityList = parentList;
      UtilityList.Cursor = Cursors.WaitCursor;

      // Set Grid vars.
      ModuleGrid = UtilityList.ModuleGrid;
      ModuleMenu = UtilityList.ModuleMenu;
      Managers = UtilityList.Managers;
      ModuleManager = Managers.DataModuleManager;

      // Fonts
      var fontFamily = UtilityList.Font.FontFamily;
      var style = UtilityList.Font.Style;
      ModuleGrid.Font = new Font(fontFamily, 11, style);
      ModuleMenu.Font = new Font(fontFamily, 11, style);

      // Font change objects.
      GridFont = new GridFont(UtilityList, ModuleGrid);
      GridFont.FontChange += GridFont_FontChange;
      MenuFont = new MenuFont(ModuleMenu);
      MenuFont.FontChange += MenuFont_FontChange;

      // Menu item events.
      var list = UtilityList;
      list.ModuleNew.Click += ModuleNew_Click;
      list.ModuleEdit.Click += ModuleEdit_Click;
      list.ModuleDelete.Click += ModuleDelete_Click;
      list.ModuleRefresh.Click += ModuleRefresh_Click;
      list.ModuleExit.Click += list.Exit_Click;

      // Grid events.
      var grid = ModuleGrid;
      grid.KeyDown += ModuleGrid_KeyDown;
      grid.MouseDoubleClick += ModuleGrid_MouseDoubleClick;
      grid.MouseDown += ModuleGrid_MouseDown;
      grid.SelectionChanged += ModuleGrid_SelectionChanged;
      UtilityList.Cursor = Cursors.Default;
    }
    #endregion

    // ******************************
    #region Data Methods
    // ******************************

    // Retrieves the list rows.
    // ********************
    internal void DataRetrieve()
    {
      UtilityList.Cursor = Cursors.WaitCursor;
      ModuleGrid.LJCRowsClear();

      var items = ModuleManager.Load();
      if (NetCommon.HasItems(items))
      {
        foreach (var item in items)
        {
          RowAdd(item);
        }
      }
      SetControlState();
      UtilityList.Cursor = Cursors.Default;
      UtilityList.DoChange(Change.Module);
    }

    // Adds a grid row and updates it with the record values.
    // ********************
    private LJCGridRow RowAdd(DataModule dataRecord)
    {
      var retValue = ModuleGrid.LJCRowAdd();
      SetStoredValues(retValue, dataRecord);
      retValue.LJCSetValues(ModuleGrid, dataRecord);
      return retValue;
    }

    // Selects a row based on the key record values.
    // ********************
    private bool RowSelect(DataModule dataRecord)
    {
      bool retValue = false;

      if (dataRecord != null)
      {
        UtilityList.Cursor = Cursors.WaitCursor;
        foreach (LJCGridRow row in ModuleGrid.Rows)
        {
          var rowID = row.LJCGetInt32(DataModule.ColumnID);
          if (rowID == dataRecord.ID)
          {
            // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
            ModuleGrid.LJCSetCurrentRow(row, true);
            retValue = true;
            break;
          }
        }
        UtilityList.Cursor = Cursors.Default;
      }
      return retValue;
    }

    // Updates the current row with the record values.
    // ********************
    private void RowUpdate(DataModule dataRecord)
    {
      if (ModuleGrid.CurrentRow is LJCGridRow row)
      {
        SetStoredValues(row, dataRecord);
        row.LJCSetValues(ModuleGrid, dataRecord);
      }
    }

    // Sets the control states based on the current control values.
    // ********************
    private void SetControlState()
    {
      bool enableNew = true;
      bool enableEdit = ModuleGrid.CurrentRow != null;
      FormCommon.SetMenuState(ModuleMenu, enableNew, enableEdit);
      UtilityList.ModuleHeading.Enabled = true;
    }

    // Sets the row stored values.
    // ********************
    private void SetStoredValues(LJCGridRow row, DataModule dataRecord)
    {
      row.LJCSetInt32(DataModule.ColumnID
        , dataRecord.ID);
      row.LJCSetString(DataModule.ColumnName
        , dataRecord.Name);
    }
    #endregion

    // ******************************
    #region Action Methods
    // ******************************

    // Deletes the selected row.
    // ********************
    internal void Delete()
    {
      bool success = false;
      var row = ModuleGrid.CurrentRow as LJCGridRow;
      if (row != null)
      {
        var title = "Delete Confirmation";
        var message = FormCommon.DeleteConfirm;
        if (MessageBox.Show(message, title, MessageBoxButtons.YesNo
          , MessageBoxIcon.Question) == DialogResult.Yes)
        {
          success = true;
        }
      }

      if (success)
      {
        // Data from items.
        var id = row.LJCGetInt32(DataUtilTable.ColumnID);

        var keyColumns = new DbColumns()
        {
          { DataUtilTable.ColumnID, id }
        };
        ModuleManager.Delete(keyColumns);
        if (0 == ModuleManager.AffectedCount)
        {
          success = false;
          var message = FormCommon.DeleteError;
          MessageBox.Show(message, "Delete Error", MessageBoxButtons.OK
            , MessageBoxIcon.Exclamation);
        }
      }

      if (success)
      {
        ModuleGrid.Rows.Remove(row);
        SetControlState();
        UtilityList.TimedChange(Change.Module);
      }
    }

    // Displays a detail dialog to edit a record.
    // ********************
    internal void Edit()
    {
      if (ModuleGrid.CurrentRow is LJCGridRow row)
      {
        // Data from items.
        int id = row.LJCGetInt32(DataModule.ColumnID);

        var location = FormPoint.DialogScreenPoint(ModuleGrid);
        var detail = new DataModuleDetail()
        {
          LJCID = id,
          LJCLocation = location,
          LJCManagers = Managers,
        };
        detail.LJCChange += Detail_Change;
        detail.LJCLocation = FormPoint.AdjustedLocation(detail, location);
        detail.ShowDialog();
      }
    }

    // Displays a detail dialog for a new record.
    // ********************
    internal void New()
    {
      var location = FormPoint.DialogScreenPoint(ModuleGrid);
      var detail = new DataModuleDetail
      {
        LJCLocation = location,
        LJCManagers = Managers,
      };
      detail.LJCChange += Detail_Change;
      detail.LJCLocation = FormPoint.AdjustedLocation(detail, location);
      detail.ShowDialog();
    }

    // Refreshes the list.
    // ********************
    internal void Refresh()
    {
      UtilityList.Cursor = Cursors.WaitCursor;
      int id = 0;
      if (ModuleGrid.CurrentRow is LJCGridRow row)
      {
        // Save the original row.
        id = row.LJCGetInt32(DataModule.ColumnID);
      }
      DataRetrieve();

      // Select the original row.
      if (id > 0)
      {
        var record = new DataModule()
        {
          ID = id
        };
        RowSelect(record);
      }
      UtilityList.Cursor = Cursors.Default;
    }

    // Shows the help page
    // ********************
    internal void ShowHelp()
    {
      //Help.ShowHelp(DocList, "_AppName_.chm", HelpNavigator.Topic
      //  , "_ClassName_List.html");
    }

    // Adds new row or updates row with control values.
    // ********************
    private void Detail_Change(object sender, EventArgs e)
    {
      var detail = sender as DataModuleDetail;
      var record = detail.LJCRecord;
      if (record != null)
      {
        if (detail.LJCIsUpdate)
        {
          RowUpdate(record);
        }
        else
        {
          // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
          var row = RowAdd(record);
          ModuleGrid.LJCSetCurrentRow(row, true);
          SetControlState();
          UtilityList.TimedChange(Change.Module);
        }
      }
    }
    #endregion

    // ******************************
    #region Setup and Other Methods
    // ******************************

    // Configures the Grid.
    // ********************
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

    // ******************************
    #region Action Event Handlers
    // ******************************

    // Handles the New menu item event.
    // ********************
    private void ModuleNew_Click(object sender, EventArgs e)
    {
      New();
    }

    // Handles the Edit menu item event.
    // ********************
    private void ModuleEdit_Click(object sender, EventArgs e)
    {
      Edit();
    }

    // Handles the Delete menu item event.
    // ********************
    private void ModuleDelete_Click(object sender, EventArgs e)
    {
      Delete();
    }

    // Handles the Refresh menu item event.
    // ********************
    private void ModuleRefresh_Click(object sender, EventArgs e)
    {
      Refresh();
    }
    #endregion

    // ******************************
    #region Control Event Handlers
    // ******************************

    // Handles the Grid FontChange event.
    private void GridFont_FontChange(object sender, EventArgs e)
    {
      var text = UtilityList.Text;
      var index = text.IndexOf("[");
      if (index > 0)
      {
        text = UtilityList.Text.Substring(0, index - 1);
      }
      var fontSize = GridFont.FontSize;
      UtilityList.Text = $"{text} [{fontSize}]";
    }

    // Handles the Menu FontChange event.
    private void MenuFont_FontChange(object sender, EventArgs e)
    {
      var menu = sender as ToolStripDropDownMenu;
      var text = menu.Items[0].Text;
      var index = text.IndexOf("[");
      if (index > 0)
      {
        text = text.Substring(0, index - 1);
      }
      var fontSize = MenuFont.FontSize;
      menu.Items[0].Text = $"{text} [{fontSize}]";
    }

    // Handles the Grid KeyDown event.
    // ********************
    private void ModuleGrid_KeyDown(object sender, KeyEventArgs e)
    {
      switch (e.KeyCode)
      {
        case Keys.Enter:
          Edit();
          e.Handled = true;
          break;

        case Keys.F1:
          ShowHelp();
          e.Handled = true;
          break;

        case Keys.M:
          if (e.Control)
          {
            var position = FormPoint.MenuScreenPoint(ModuleGrid
              , Control.MousePosition);
            UtilityList.ModuleMenu.Show(position);
            UtilityList.ModuleMenu.Select();
            e.Handled = true;
          }
          break;

        case Keys.Tab:
          if (e.Shift)
          {
            UtilityList.MainTabs.Select();
          }
          else
          {
            UtilityList.MainTabs.Select();
          }
          e.Handled = true;
          break;
      }
    }

    // Handles the MouseDoubleClick event.
    // ********************
    private void ModuleGrid_MouseDoubleClick(object sender, MouseEventArgs e)
    {
      if (ModuleGrid.LJCGetMouseRow(e) != null)
      {
        Edit();
      }
    }

    // Handles the MouseDown event.
    // ********************
    private void ModuleGrid_MouseDown(object sender, MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Right)
      {
        // LJCIsDifferentRow() Sets the LJCLastRowIndex for new row.
        ModuleGrid.Select();
        if (ModuleGrid.LJCIsDifferentRow(e))
        {
          // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
          ModuleGrid.LJCSetCurrentRow(e);
          UtilityList.TimedChange(Change.Module);
        }
      }
    }

    // Handles the SelectionChanged event.
    // ********************
    private void ModuleGrid_SelectionChanged(object sender, EventArgs e)
    {
      if (ModuleGrid.LJCAllowSelectionChange)
      {
        UtilityList.TimedChange(Change.Module);
      }
      ModuleGrid.LJCAllowSelectionChange = true;
    }
    #endregion

    #region Properties

    // Gets or sets the Parent List reference.
    private DataUtilityList UtilityList { get; set; }

    // Gets or sets the Grid reference.
    private LJCDataGrid ModuleGrid { get; set; }

    // Gets or sets the Menu reference.
    private ContextMenuStrip ModuleMenu { get; set; }

    // Gets or sets the Managers reference.
    private ManagersDataUtility Managers { get; set; }

    // Gets or sets the Manager reference.
    private DataModuleManager ModuleManager { get; set; }

    // Provides the Grid font event handlers.
    private GridFont GridFont { get; set; }

    // Provides the menu font event handlers.
    private MenuFont MenuFont { get; set; }
    #endregion
  }
}
