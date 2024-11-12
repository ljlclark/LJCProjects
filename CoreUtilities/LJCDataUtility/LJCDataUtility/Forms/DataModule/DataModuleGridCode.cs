// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DataModuleGridCode.cs
using LJCDataUtilityDAL;
using LJCNetCommon;
using LJCWinFormCommon;
using LJCWinFormControls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace LJCDataUtility
{
  // Provides DataModuleGrid methods for the DataUtilityList window.
  internal class DataModuleGridCode
  {
    #region Constructors

    // Initializes an object instance.
    // ********************
    internal DataModuleGridCode(DataUtilityList parentList)
    {
      // Initialize property values.
      UtilityList = parentList;
      UtilityList.Cursor = Cursors.WaitCursor;

      ModuleGrid = UtilityList.ModuleGrid;
      var moduleMenu = UtilityList.ModuleMenu;
      Managers = UtilityList.Managers;
      ModuleManager = Managers.DataModuleManager;

      var fontFamily = UtilityList.Font.FontFamily;
      var style = UtilityList.Font.Style;
      ModuleGrid.Font = new Font(fontFamily, 11, style);
      moduleMenu.Font = new Font(fontFamily, 11, style);
      _ = new GridFont(UtilityList, ModuleGrid);
      _ = new MenuFont(moduleMenu);

      // Menu item events.
      var list = UtilityList;
      list.ModuleNew.Click += ModuleNew_Click;
      list.ModuleEdit.Click += ModuleEdit_Click;
      list.ModuleDelete.Click += ModuleDelete_Click;
      list.ModuleRefresh.Click += ModuleRefresh_Click;

      // Grid events.
      var grid = ModuleGrid;
      grid.MouseDoubleClick += ModuleGrid_MouseDoubleClick;
      grid.KeyDown += ModuleGrid_KeyDown;
      UtilityList.Cursor = Cursors.Default;
    }
    #endregion

    #region Data Methods

    // Retrieves the list rows.
    // ********************
    internal void DataRetrieve()
    {
      UtilityList.Cursor = Cursors.WaitCursor;
      ModuleGrid.LJCRowsClear();

      var items = ModuleManager.Load();
      if (items != null
        && items.Count > 0)
      {
        foreach (var item in items)
        {
          RowAdd(item);
        }
      }
      UtilityList.Cursor = Cursors.Default;
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

    // Sets the row stored values.
    // ********************
    private void SetStoredValues(LJCGridRow row, DataModule dataRecord)
    {
      row.LJCSetInt32(DataModule.ColumnID, dataRecord.ID);
    }
    #endregion

    #region Action Methods

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
        var id = row.LJCGetInt32(DataTable.ColumnID);

        var keyColumns = new DbColumns()
        {
          { DataTable.ColumnID, id }
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
        //UtilityList.TimedChange(Change._ClassName_);
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

        //var location = FormCommon.GetDialogScreenPoint(_ClassName_Grid);
        var detail = new DataModuleDetail()
        {
          LJCID = id,
          //LJCLocation = location,
          Managers = Managers,
        };
        detail.LJCChange += Detail_Change;
        detail.ShowDialog();
      }
    }

    // Displays a detail dialog for a new record.
    // ********************
    internal void New()
    {
      //var location = FormCommon.GetDialogScreenPoint(ModuleGrid);
      var detail = new DataModuleDetail
      {
        //LJCLocation = location,
        Managers = Managers,
      };
      //detail.LJCChange += Detail_Change;
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
          //_AppName_List.TimedChange(Change._ClassName_);
        }
      }
    }
    #endregion

    #region Setup and Other Methods

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

    #region Control Event Handlers

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

    // Handles the Grid Doubleclick event.
    // ********************
    private void ModuleGrid_MouseDoubleClick(object sender, MouseEventArgs e)
    {
      if (ModuleGrid.LJCGetMouseRow(e) != null)
      {
        Edit();
      }
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

        case Keys.F5:
          Refresh();
          e.Handled = true;
          break;

        case Keys.M:
          if (e.Control)
          {
            var position = FormCommon.GetMenuScreenPoint(ModuleGrid
              , Control.MousePosition);
            UtilityList.ModuleMenu.Show(position);
            UtilityList.ModuleMenu.Select();
            e.Handled = true;
          }
          break;

        case Keys.Tab:
          if (e.Shift)
          {
            UtilityList.ljcTabControl1.Select();
          }
          else
          {
            UtilityList.ljcTabControl1.Select();
          }
          e.Handled = true;
          break;
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
