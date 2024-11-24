// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DataTableGridCode.cs
using static LJCDataUtility.DataUtilityList;
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
  // Provides DataTableGrid methods for the DataUtilityList window.
  internal class DataTableGridCode
  {
    #region Constructors

    // Initializes an object instance.
    // ********************
    internal DataTableGridCode(DataUtilityList parentList)
    {
      // Initialize property values.
      UtilityList = parentList;
      UtilityList.Cursor = Cursors.WaitCursor;

      // Set Grid vars.
      ModuleGrid = UtilityList.ModuleGrid;
      TableGrid = UtilityList.TableGrid;
      TableMenu = UtilityList.TableMenu;
      Managers = UtilityList.Managers;
      TableManager = Managers.DataTableManager;

      // Fonts
      var fontFamily = UtilityList.Font.FontFamily;
      var style = UtilityList.Font.Style;
      TableGrid.Font = new Font(fontFamily, 11, style);
      TableMenu.Font = new Font(fontFamily, 11, style);

      // Font change objects.
      GridFont = new GridFont(UtilityList, TableGrid);
      GridFont.FontChange += GridFont_FontChange;
      MenuFont = new MenuFont(TableMenu);
      MenuFont.FontChange += MenuFont_FontChange;

      // Menu item events.
      var list = UtilityList;
      list.TableNew.Click += TableNew_Click;
      list.TableEdit.Click += TableEdit_Click;
      list.TableDelete.Click += TableDelete_Click;
      list.TableRefresh.Click += TableRefresh_Click;
      list.TableExit.Click += list.Exit_Click;

      // Grid events.
      var grid = TableGrid;
      grid.KeyDown += TableGrid_KeyDown;
      grid.MouseDoubleClick += TableGrid_MouseDoubleClick;
      grid.MouseDown += TableGrid_MouseDown;
      grid.SelectionChanged += TableGrid_SelectionChanged;
      UtilityList.Cursor = Cursors.Default;
    }
    #endregion

    #region Data Methods

    // Retrieves the list rows.
    // ********************
    internal void DataRetrieve()
    {
      UtilityList.Cursor = Cursors.WaitCursor;
      TableGrid.LJCRowsClear();

      if (ModuleGrid.CurrentRow is LJCGridRow parentRow)
      {
        // Data from items.
        int parentID = parentRow.LJCGetInt32(DataModule.ColumnID);

        var keyColumns = TableManager.IDKey(parentID);
        var items = TableManager.Load(keyColumns);
        if (NetCommon.HasItems(items))
        {
          foreach (var item in items)
          {
            RowAdd(item);
          }
        }
      }
      SetControlState();
      UtilityList.Cursor = Cursors.Default;
      UtilityList.DoChange(Change.Table);
    }

    // Adds a grid row and updates it with the record values.
    // ********************
    private LJCGridRow RowAdd(DataUtilTable dataRecord)
    {
      var retValue = TableGrid.LJCRowAdd();
      SetStoredValues(retValue, dataRecord);
      retValue.LJCSetValues(TableGrid, dataRecord);
      return retValue;
    }

    // Selects a row based on the key record values.
    // ********************
    private bool RowSelect(DataUtilTable dataRecord)
    {
      bool retValue = false;

      if (dataRecord != null)
      {
        UtilityList.Cursor = Cursors.WaitCursor;
        foreach (LJCGridRow row in TableGrid.Rows)
        {
          var rowID = row.LJCGetInt32(DataUtilTable.ColumnID);
          if (rowID == dataRecord.ID)
          {
            // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
            TableGrid.LJCSetCurrentRow(row, true);
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
    private void RowUpdate(DataUtilTable dataRecord)
    {
      if (TableGrid.CurrentRow is LJCGridRow row)
      {
        SetStoredValues(row, dataRecord);
        row.LJCSetValues(TableGrid, dataRecord);
      }
    }

    // Sets the control states based on the current control values.
    // ********************
    private void SetControlState()
    {
      bool enableNew = ModuleGrid.CurrentRow != null;
      bool enableEdit = TableGrid.CurrentRow != null;
      FormCommon.SetMenuState(TableMenu, enableNew, enableEdit);
      UtilityList.TableHeading.Enabled = true;
    }

    // Sets the row stored values.
    // ********************
    private void SetStoredValues(LJCGridRow row, DataUtilTable dataRecord)
    {
      row.LJCSetInt32(DataUtilTable.ColumnID
        , dataRecord.ID);
      row.LJCSetString(DataUtilTable.ColumnName
        , dataRecord.Name);
    }
    #endregion

    #region Action Methods

    // Deletes the selected row.
    // ********************
    internal void Delete()
    {
      bool success = false;
      var row = TableGrid.CurrentRow as LJCGridRow;
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
        TableManager.Delete(keyColumns);
        if (0 == TableManager.AffectedCount)
        {
          success = false;
          var message = FormCommon.DeleteError;
          MessageBox.Show(message, "Delete Error", MessageBoxButtons.OK
            , MessageBoxIcon.Exclamation);
        }
      }

      if (success)
      {
        TableGrid.Rows.Remove(row);
        SetControlState();
        UtilityList.TimedChange(Change.Table);
      }
    }

    // Displays a detail dialog to edit a record.
    // ********************
    internal void Edit()
    {
      if (ModuleGrid.CurrentRow is LJCGridRow parentRow
        && TableGrid.CurrentRow is LJCGridRow row)
      {
        // Data from items.
        int id = row.LJCGetInt32(DataUtilTable.ColumnID);
        int parentID = parentRow.LJCGetInt32(DataModule.ColumnID);
        string parentName = parentRow.LJCGetString(DataModule.ColumnName);

        var location = FormPoint.DialogScreenPoint(TableGrid);
        var detail = new DataTableDetail()
        {
          LJCID = id,
          LJCLocation = location,
          LJCManagers = Managers,
          LJCParentID = parentID,
          LJCParentName = parentName,
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
      if (ModuleGrid.CurrentRow is LJCGridRow parentRow)
      {
        // Data from list items.
        int parentID = parentRow.LJCGetInt32(DataModule.ColumnID);
        string parentName = parentRow.LJCGetString(DataModule.ColumnName);

        var location = FormPoint.DialogScreenPoint(TableGrid);
        var detail = new DataTableDetail
        {
          LJCLocation = location,
          LJCManagers = Managers,
          LJCParentID = parentID,
          LJCParentName = parentName
        };
        detail.LJCChange += Detail_Change;
        detail.LJCLocation = FormPoint.AdjustedLocation(detail, location);
        detail.ShowDialog();
      }
    }

    // Refreshes the list.
    // ********************
    internal void Refresh()
    {
      UtilityList.Cursor = Cursors.WaitCursor;
      int id = 0;
      if (TableGrid.CurrentRow is LJCGridRow row)
      {
        // Save the original row.
        id = row.LJCGetInt32(DataUtilTable.ColumnID);
      }
      DataRetrieve();

      // Select the original row.
      if (id > 0)
      {
        var record = new DataUtilTable()
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
      var detail = sender as DataTableDetail;
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
          TableGrid.LJCSetCurrentRow(row, true);
          SetControlState();
          UtilityList.TimedChange(Change.Table);
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
      if (0 == TableGrid.Columns.Count)
      {
        List<string> propertyNames = new List<string>()
        {
          DataUtilTable.ColumnDataModuleID,
          DataUtilTable.ColumnName,
          DataUtilTable.ColumnDescription
        };

        // Get the grid columns from the manager Data Definition.
        var gridColumns = TableManager.GetColumns(propertyNames);

        // Setup the grid columns.
        TableGrid.LJCAddColumns(gridColumns);
      }
    }
    #endregion

    #region Action Event Handlers

    // Handles the New menu item event.
    // ********************
    private void TableNew_Click(object sender, EventArgs e)
    {
      New();
    }

    // Handles the Edit menu item event.
    // ********************
    private void TableEdit_Click(object sender, EventArgs e)
    {
      Edit();
    }

    // Handles the Delete menu item event.
    // ********************
    private void TableDelete_Click(object sender, EventArgs e)
    {
      Delete();
    }

    // Handles the Refresh menu item event.
    // ********************
    private void TableRefresh_Click(object sender, EventArgs e)
    {
      Refresh();
    }
    #endregion

    #region Control Event Handlers

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
    private void TableGrid_KeyDown(object sender, KeyEventArgs e)
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
            var position = FormPoint.MenuScreenPoint(TableGrid
              , Control.MousePosition);
            UtilityList.TableMenu.Show(position);
            UtilityList.TableMenu.Select();
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

    // Handles the Grid MouseDoubleClick event.
    // ********************
    private void TableGrid_MouseDoubleClick(object sender, MouseEventArgs e)
    {
      if (TableGrid.LJCGetMouseRow(e) != null)
      {
        Edit();
      }
    }

    // Handles the MouseDown event.
    // ********************
    private void TableGrid_MouseDown(object sender, MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Right)
      {
        // LJCIsDifferentRow() Sets the LJCLastRowIndex for new row.
        TableGrid.Select();
        if (TableGrid.LJCIsDifferentRow(e))
        {
          // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
          TableGrid.LJCSetCurrentRow(e);
          UtilityList.TimedChange(Change.Table);
        }
      }
    }

    // Handles the SelectionChanged event.
    // ********************
    private void TableGrid_SelectionChanged(object sender, EventArgs e)
    {
      if (TableGrid.LJCAllowSelectionChange)
      {
        UtilityList.TimedChange(Change.Table);
      }
      TableGrid.LJCAllowSelectionChange = true;
    }
    #endregion

    #region Properties

    // Gets or sets the Parent List reference.
    private DataUtilityList UtilityList { get; set; }

    // Gets or sets the parent Grid reference.
    private LJCDataGrid ModuleGrid { get; set; }

    // Gets or sets the Grid reference.
    private LJCDataGrid TableGrid { get; set; }

    // Gets or sets the Menu reference.
    private ContextMenuStrip TableMenu { get; set; }

    // Gets or sets the Managers reference.
    private ManagersDataUtility Managers { get; set; }

    // Gets or sets the Manager reference.
    private DataTableManager TableManager { get; set; }

    // Provides the Grid font event handlers.
    private GridFont GridFont { get; set; }

    // Provides the menu font event handlers.
    private MenuFont MenuFont { get; set; }
    #endregion
  }
}
