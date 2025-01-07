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
using System.Text;
using System.Xml.Linq;
using System.Reflection;
using System.Data;
using System.IO;
using LJCDataUtility;
using LJCDBClientLib;
using LJCDBMessage;

namespace LJCDataUtility
{
  // Provides DataTableGrid methods for the DataUtilityList window.
  internal class DataTableGridCode
  {
    #region Constructors

    // Initializes an object instance.
    internal DataTableGridCode(DataUtilityList parentList)
    {
      // Initialize property values.
      Parent = parentList;
      Parent.Cursor = Cursors.WaitCursor;

      // Set Grid vars.
      ModuleCombo = Parent.ModuleCombo;
      TableGrid = Parent.TableGrid;
      TableMenu = Parent.TableMenu;
      Managers = Parent.Managers;
      TableManager = Managers.DataTableManager;

      // Fonts
      var fontFamily = Parent.Font.FontFamily;
      var style = Parent.Font.Style;
      TableGrid.Font = new Font(fontFamily, 11, style);
      TableMenu.Font = new Font(fontFamily, 11, style);

      // Font change objects.
      GridFont = new GridFont(Parent, TableGrid);
      GridFont.FontChange += GridFont_FontChange;
      MenuFont = new MenuFont(TableMenu);
      MenuFont.FontChange += MenuFont_FontChange;

      // Menu item events.
      Parent.TableNew.Click += TableNew_Click;
      Parent.TableEdit.Click += TableEdit_Click;
      Parent.TableDelete.Click += TableDelete_Click;
      Parent.TableRefresh.Click += TableRefresh_Click;
      Parent.TableExit.Click += Parent.Exit_Click;

      // Custom menu item events.
      Parent.TableSetData.Click += TableSetData_Click;
      Parent.TableCreateProc.Click += TableCreateProc_Click;
      Parent.TableAddDataProc.Click += TableAddDataProc_Click;
      Parent.TableCreateDataProc.Click += TableCreateDataProc_Click;
      Parent.TableInsertSelect.Click += TableInsertSelect_Click;
      Parent.TableRename.Click += TableRename_Click;

      // Grid events.
      var grid = TableGrid;
      grid.KeyDown += TableGrid_KeyDown;
      grid.MouseDoubleClick += TableGrid_MouseDoubleClick;
      grid.MouseDown += TableGrid_MouseDown;
      grid.SelectionChanged += TableGrid_SelectionChanged;
      Parent.Cursor = Cursors.Default;
    }

    // Configures the Grid.
    internal void SetupGrid()
    {
      // Setup default grid columns if no columns are defined.
      if (0 == TableGrid.Columns.Count)
      {
        List<string> propertyNames = new List<string>()
        {
          //DataUtilTable.ColumnModuleName,
          DataUtilTable.ColumnName,
          DataUtilTable.ColumnDescription,
          DataUtilTable.ColumnSequence
        };

        // Get the grid columns from the manager Data Definition.
        var gridColumns = TableManager.Columns(propertyNames);

        // Setup the grid columns.
        TableGrid.LJCAddColumns(gridColumns);
      }
    }
    #endregion

    #region Data Methods

    // Retrieves the list rows.
    internal void DataRetrieve()
    {
      Parent.Cursor = Cursors.WaitCursor;
      TableGrid.LJCRowsClear();

      int parentID = ModuleCombo.LJCSelectedItemID();
      if (parentID > 0)
      {
        var keyColumns = TableManager.ParentKey(parentID);
        var orderBy = new List<string>()
        {
          DataUtilTable.ColumnSequence
        };
        TableManager.Manager.OrderByNames = orderBy;
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
      Parent.Cursor = Cursors.Default;
      Parent.DoChange(Change.Table);
    }

    // Selects a row based on the ID value.
    internal bool RowSelect(long id)
    {
      var dataRecord = new DataUtilTable()
      {
        ID = id
      };
      var retValue = RowSelect(dataRecord);
      return retValue;
    }

    // Adds a grid row and updates it with the record values.
    private LJCGridRow RowAdd(DataUtilTable data)
    {
      var retValue = TableGrid.LJCRowAdd();
      SetStoredValues(retValue, data);
      retValue.LJCSetValues(TableGrid, data);
      return retValue;
    }

    // Selects a row based on the key record values.
    private bool RowSelect(DataUtilTable data)
    {
      bool retValue = false;

      if (data != null)
      {
        Parent.Cursor = Cursors.WaitCursor;
        foreach (LJCGridRow row in TableGrid.Rows)
        {
          var rowID = row.LJCGetInt64(DataUtilTable.ColumnID);
          if (rowID == data.ID)
          {
            // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
            TableGrid.LJCSetCurrentRow(row, true);
            retValue = true;
            break;
          }
        }
        Parent.Cursor = Cursors.Default;
      }
      return retValue;
    }

    // Updates the current row with the record values.
    private void RowUpdate(DataUtilTable data)
    {
      if (TableGrid.CurrentRow is LJCGridRow row)
      {
        SetStoredValues(row, data);
        row.LJCSetValues(TableGrid, data);
      }
    }

    // Sets the control states based on the current control values.
    private void SetControlState()
    {
      bool enableNew = ModuleCombo.SelectedItem != null;
      bool enableEdit = TableGrid.CurrentRow != null;
      FormCommon.SetMenuState(TableMenu, enableNew, enableEdit);
      Parent.TableHeading.Enabled = true;
      Parent.TableSetData.Enabled = true;
    }

    // Sets the row stored values.
    private void SetStoredValues(LJCGridRow row, DataUtilTable dataRecord)
    {
      row.LJCSetInt64(DataUtilTable.ColumnID
        , dataRecord.ID);
      row.LJCSetString(DataUtilTable.ColumnName
        , dataRecord.Name);
    }
    #endregion

    #region Common Action Methods

    // Deletes the selected row.
    internal void Delete()
    {
      bool isContinue = true;
      var row = TableGrid.CurrentRow as LJCGridRow;
      if (row != null)
      {
        var title = "Delete Confirmation";
        var message = FormCommon.DeleteConfirm;
        if (DialogResult.No == MessageBox.Show(message, title
          , MessageBoxButtons.YesNo, MessageBoxIcon.Question))
        {
          isContinue = false;
        }
      }

      if (isContinue)
      {
        // Data from items.
        var id = row.LJCGetInt64(DataUtilTable.ColumnID);

        var keyColumns = new DbColumns()
        {
          { DataUtilTable.ColumnID, id }
        };
        TableManager.Delete(keyColumns);
        if (0 == TableManager.AffectedCount)
        {
          isContinue = false;
          var message = FormCommon.DeleteError;
          MessageBox.Show(message, "Delete Error", MessageBoxButtons.OK
            , MessageBoxIcon.Exclamation);
        }
      }

      if (isContinue)
      {
        TableGrid.Rows.Remove(row);
        SetControlState();
        Parent.TimedChange(Change.Table);
      }
    }

    // Displays a detail dialog to edit a record.
    internal void Edit()
    {
      if (ModuleCombo.SelectedIndex >= 0
        && TableGrid.CurrentRow is LJCGridRow row)
      {
        // Data from items.
        long id = row.LJCGetInt64(DataUtilTable.ColumnID);
        int parentID = ModuleCombo.LJCSelectedItemID();
        string parentName = ModuleCombo.Text;

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
        detail.Dispose();
      }
    }

    // Displays a detail dialog for a new record.
    internal void New()
    {
      if (ModuleCombo.SelectedItem != null)
      {
        int sequence = TableGrid.Rows.Count + 1;
        int parentID = ModuleCombo.LJCSelectedItemID();
        string parentName = ModuleCombo.Text;

        var location = FormPoint.DialogScreenPoint(TableGrid);
        var detail = new DataTableDetail
        {
          LJCLocation = location,
          LJCManagers = Managers,
          LJCParentID = parentID,
          LJCParentName = parentName,
          LJCSequence = sequence
        };
        detail.LJCChange += Detail_Change;
        detail.LJCLocation = FormPoint.AdjustedLocation(detail, location);
        detail.ShowDialog();
        detail.Dispose();
      }
    }

    // Refreshes the list.
    internal void Refresh()
    {
      Parent.Cursor = Cursors.WaitCursor;
      long id = 0;
      if (TableGrid.CurrentRow is LJCGridRow row)
      {
        // Save the original row.
        id = row.LJCGetInt64(DataUtilTable.ColumnID);
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
          Parent.TimedChange(Change.Table);
        }
      }
    }
    #endregion

    #region Common Action Event Handlers

    // Handles the New menu item event.
    private void TableNew_Click(object sender, EventArgs e)
    {
      New();
    }

    // Handles the Edit menu item event.
    private void TableEdit_Click(object sender, EventArgs e)
    {
      Edit();
    }

    // Handles the Delete menu item event.
    private void TableDelete_Click(object sender, EventArgs e)
    {
      Delete();
    }

    // Handles the Refresh menu item event.
    private void TableRefresh_Click(object sender, EventArgs e)
    {
      Refresh();
    }
    #endregion

    #region Custom Action Event Handlers

    // Handles the "Set Data from Table" menu event.
    private void TableSetData_Click(object sender, EventArgs e)
    {
      var setData = new SetUtilData(Parent);
      setData.SetData();
    }

    // Handles the "Create Table Procedure" menu item event.
    private void TableCreateProc_Click(object sender, EventArgs e)
    {
      var createTable = new CreateTable(Parent);
      createTable.CreateTableProc();
    }

    // Handles the "Add Data Procedure" menu item event.
    private void TableAddDataProc_Click(object sender, EventArgs e)
    {
      var addData = new AddData(Parent);
      addData.AddDataProc();
    }

    // Handles the "Create Data Procedure" menu item event.
    private void TableCreateDataProc_Click(object sender, EventArgs e)
    {
      var createData = new CreateData(Parent);
      createData.CreateDataProc();
    }

    // Handles the "Insert Select" menu item event.
    private void TableInsertSelect_Click(object sender, EventArgs e)
    {
      var insertSelect = new InsertSelect(Parent);
      insertSelect.InsertSelectProc();
    }

    // Handles the "Rename Table" menu item event.
    private void TableRename_Click(object sender, EventArgs e)
    {
      var renameTable = new RenameTable(Parent);
      renameTable.RenameTableSQL();
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

        case Keys.M:
          if (e.Control)
          {
            var position = FormPoint.MenuScreenPoint(TableGrid
              , Control.MousePosition);
            Parent.TableMenu.Show(position);
            Parent.TableMenu.Select();
            e.Handled = true;
          }
          break;

        case Keys.Tab:
          if (e.Shift)
          {
            Parent.ColumnTabs.Select();
          }
          else
          {
            Parent.ColumnTabs.Select();
          }
          e.Handled = true;
          break;
      }
    }

    // Handles the Grid MouseDoubleClick event.
    private void TableGrid_MouseDoubleClick(object sender, MouseEventArgs e)
    {
      if (TableGrid.LJCGetMouseRow(e) != null)
      {
        Edit();
      }
    }

    // Handles the MouseDown event.
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
          SetControlState();
          Parent.TimedChange(Change.Table);
        }
      }
    }

    // Handles the SelectionChanged event.
    private void TableGrid_SelectionChanged(object sender, EventArgs e)
    {
      if (TableGrid.LJCAllowSelectionChange)
      {
        SetControlState();
        Parent.TimedChange(Change.Table);
      }
      TableGrid.LJCAllowSelectionChange = true;
    }
    #endregion

    #region Properties

    // Gets or sets the Parent List reference.
    private DataUtilityList Parent { get; set; }

    // Gets or sets the parent Combo reference.
    private LJCItemCombo ModuleCombo { get; set; }

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
