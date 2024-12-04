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

namespace LJCDataUtility
{
  // Provides DataModuleGrid methods for the DataUtilityList window.
  internal class DataModuleGridCode
  {
    #region Constructors

    // Initializes an object instance.
    internal DataModuleGridCode(DataUtilityList parentList)
    {
      // Initialize property values.
      Parent = parentList;
      Parent.Cursor = Cursors.WaitCursor;

      // Set Grid vars.
      ModuleGrid = Parent.ModuleGrid;
      ModuleMenu = Parent.ModuleMenu;
      Managers = Parent.Managers;
      ModuleManager = Managers.DataModuleManager;

      // Fonts
      var fontFamily = Parent.Font.FontFamily;
      var style = Parent.Font.Style;
      ModuleGrid.Font = new Font(fontFamily, 11, style);
      ModuleMenu.Font = new Font(fontFamily, 11, style);

      // Font change objects.
      GridFont = new GridFont(Parent, ModuleGrid);
      GridFont.FontChange += GridFont_FontChange;
      MenuFont = new MenuFont(ModuleMenu);
      MenuFont.FontChange += MenuFont_FontChange;

      // Menu item events.
      var list = Parent;
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
      Parent.Cursor = Cursors.Default;
    }
    #endregion

    #region Data Methods

    // Retrieves the list rows.
    internal void DataRetrieve()
    {
      Parent.Cursor = Cursors.WaitCursor;
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
      Parent.Cursor = Cursors.Default;
      Parent.DoChange(Change.Module);
    }

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

    // Adds a grid row and updates it with the record values.
    private LJCGridRow RowAdd(DataModule dataRecord)
    {
      var retValue = ModuleGrid.LJCRowAdd();
      SetStoredValues(retValue, dataRecord);
      retValue.LJCSetValues(ModuleGrid, dataRecord);
      return retValue;
    }

    // Selects a row based on the key record values.
    private bool RowSelect(int id)
    {
      bool retValue = false;

      if (id > 0)
      {
        Parent.Cursor = Cursors.WaitCursor;
        foreach (LJCGridRow row in ModuleGrid.Rows)
        {
          var rowID = Parent.DataModuleID(row);
          if (rowID == id)
          {
            // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
            ModuleGrid.LJCSetCurrentRow(row, true);
            retValue = true;
            break;
          }
        }
        Parent.Cursor = Cursors.Default;
      }
      return retValue;
    }

    // Updates the current row with the record values.
    private void RowUpdate(DataModule dataRecord)
    {
      if (ModuleGrid.CurrentRow is LJCGridRow row)
      {
        SetStoredValues(row, dataRecord);
        row.LJCSetValues(ModuleGrid, dataRecord);
      }
    }

    // Sets the control states based on the current control values.
    private void SetControlState()
    {
      bool enableNew = true;
      bool enableEdit = ModuleGrid.CurrentRow != null;
      FormCommon.SetMenuState(ModuleMenu, enableNew, enableEdit);
      Parent.ModuleHeading.Enabled = true;
    }

    // Sets the row stored values.
    private void SetStoredValues(LJCGridRow row, DataModule dataRecord)
    {
      row.LJCSetInt32(DataModule.ColumnID
        , dataRecord.ID);
      row.LJCSetString(DataModule.ColumnName
        , dataRecord.Name);
    }
    #endregion

    #region Action Methods

    // Deletes the selected row.
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
        var id = Parent.DataModuleID();
        var keyColumns = new DbColumns()
        {
          { DataModule.ColumnID, id }
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
        Parent.TimedChange(Change.Module);
      }
    }

    // Displays a detail dialog to edit a record.
    internal void Edit()
    {
      if (ModuleGrid.CurrentRow is LJCGridRow row)
      {
        int id = Parent.DataModuleID();
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
    internal void Refresh()
    {
      Parent.Cursor = Cursors.WaitCursor;
      int id = 0;
      if (ModuleGrid.CurrentRow is LJCGridRow)
      {
        // Save the original row.
        id = Parent.DataModuleID();
      }
      DataRetrieve();

      // Select the original row.
      if (id > 0)
      {
        RowSelect(id);
      }
      Parent.Cursor = Cursors.Default;
    }

    // Shows the help page
    internal void ShowHelp()
    {
      //Help.ShowHelp(DocList, "_AppName_.chm", HelpNavigator.Topic
      //  , "_ClassName_List.html");
    }

    // Adds new row or updates row with control values.
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
          Parent.TimedChange(Change.Module);
        }
      }
    }
    #endregion

    #region Action Event Handlers

    // Handles the New menu item event.
    private void ModuleNew_Click(object sender, EventArgs e)
    {
      New();
    }

    // Handles the Edit menu item event.
    private void ModuleEdit_Click(object sender, EventArgs e)
    {
      Edit();
    }

    // Handles the Delete menu item event.
    private void ModuleDelete_Click(object sender, EventArgs e)
    {
      Delete();
    }

    // Handles the Refresh menu item event.
    private void ModuleRefresh_Click(object sender, EventArgs e)
    {
      Refresh();
    }
    #endregion

    #region Control Event Handlers

    // Handles the Grid FontChange event.
    private void GridFont_FontChange(object sender, EventArgs e)
    {
      var text = Parent.Text;
      var index = text.IndexOf("[");
      if (index > 0)
      {
        text = Parent.Text.Substring(0, index - 1);
      }
      var fontSize = GridFont.FontSize;
      Parent.Text = $"{text} [{fontSize}]";
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
            Parent.ModuleMenu.Show(position);
            Parent.ModuleMenu.Select();
            e.Handled = true;
          }
          break;

        case Keys.Tab:
          if (e.Shift)
          {
            Parent.MainTabs.Select();
          }
          else
          {
            Parent.MainTabs.Select();
          }
          e.Handled = true;
          break;
      }
    }

    // Handles the MouseDoubleClick event.
    private void ModuleGrid_MouseDoubleClick(object sender, MouseEventArgs e)
    {
      if (ModuleGrid.LJCGetMouseRow(e) != null)
      {
        Edit();
      }
    }

    // Handles the MouseDown event.
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
          Parent.TimedChange(Change.Module);
        }
      }
    }

    // Handles the SelectionChanged event.
    private void ModuleGrid_SelectionChanged(object sender, EventArgs e)
    {
      if (ModuleGrid.LJCAllowSelectionChange)
      {
        Parent.TimedChange(Change.Module);
      }
      ModuleGrid.LJCAllowSelectionChange = true;
    }
    #endregion

    #region Properties

    // Gets or sets the Parent List reference.
    private DataUtilityList Parent { get; set; }

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
