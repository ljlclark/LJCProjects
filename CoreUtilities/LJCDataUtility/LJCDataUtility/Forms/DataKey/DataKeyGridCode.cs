// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DataKeyGridCode.cs
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
using System.Data;

namespace LJCDataUtility
{
  // Provides DataKeyGrid methods for the DataUtilityList window.
  internal class DataKeyGridCode
  {
    #region Constructors

    // Initializes an object instance.
    internal DataKeyGridCode(DataUtilityList parentList)
    {
      // Initialize property values.
      Parent = parentList;
      Parent.Cursor = Cursors.WaitCursor;

      // Set Grid vars.
      TableGrid = Parent.TableGrid;
      KeyGrid = Parent.KeyGrid;
      KeyMenu = Parent.KeyMenu;
      Managers = Parent.Managers;
      KeyManager = Managers.DataKeyManager;

      // Fonts
      var fontFamily = Parent.Font.FontFamily;
      var style = Parent.Font.Style;
      KeyGrid.Font = new Font(fontFamily, 11, style);
      KeyMenu.Font = new Font(fontFamily, 11, style);

      // Font change objects.
      GridFont = new GridFont(Parent, KeyGrid);
      GridFont.FontChange += GridFont_FontChange;
      MenuFont = new MenuFont(KeyMenu);
      MenuFont.FontChange += MenuFont_FontChange;

      // Menu item events.
      var list = Parent;
      list.KeyNew.Click += KeyNew_Click;
      list.KeyEdit.Click += KeyEdit_Click;
      list.KeyDelete.Click += KeyDelete_Click;
      list.KeyRefresh.Click += KeyRefresh_Click;
      list.KeyForeignKeyProc.Click += KeyForeignKeyProc_Click;
      list.KeyForeignKeyDropProc.Click += KeyForeignKeyDropProc_Click;
      list.KeyExit.Click += list.Exit_Click;

      // Grid events.
      var grid = KeyGrid;
      grid.KeyDown += KeyGrid_KeyDown;
      grid.MouseDoubleClick += KeyGrid_MouseDoubleClick;
      grid.MouseDown += KeyGrid_MouseDown;
      grid.SelectionChanged += KeyGrid_SelectionChanged;
      Parent.Cursor = Cursors.Default;
    }
    #endregion

    #region Data Methods

    // Retrieves the list rows.
    internal void DataRetrieve()
    {
      Parent.Cursor = Cursors.WaitCursor;
      KeyGrid.LJCRowsClear();

      if (TableGrid.CurrentRow is LJCGridRow)
      {
        int parentID = Parent.DataTableID();
        var keyColumns = KeyManager.ParentKey(parentID);
        var items = KeyManager.Load(keyColumns);
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
      Parent.DoChange(Change.Key);
    }

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

    // Adds a grid row and updates it with the record values.
    private LJCGridRow RowAdd(DataKey dataRecord)
    {
      var retValue = KeyGrid.LJCRowAdd();
      SetStoredValues(retValue, dataRecord);
      retValue.LJCSetValues(KeyGrid, dataRecord);
      return retValue;
    }

    // Selects a row based on the key record values.
    private bool RowSelect(int id)
    {
      bool retValue = false;

      if (id > 0)
      {
        Parent.Cursor = Cursors.WaitCursor;
        foreach (LJCGridRow row in KeyGrid.Rows)
        {
          var rowID = Parent.DataTableID();
          if (rowID == id)
          {
            // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
            KeyGrid.LJCSetCurrentRow(row, true);
            retValue = true;
            break;
          }
        }
        Parent.Cursor = Cursors.Default;
      }
      return retValue;
    }

    // Updates the current row with the record values.
    private void RowUpdate(DataKey dataRecord)
    {
      if (KeyGrid.CurrentRow is LJCGridRow row)
      {
        SetStoredValues(row, dataRecord);
        row.LJCSetValues(KeyGrid, dataRecord);
      }
    }

    // Sets the control states based on the current control values.
    private void SetControlState()
    {
      bool enableNew = TableGrid.CurrentRow != null;
      bool enableEdit = KeyGrid.CurrentRow != null;
      FormCommon.SetMenuState(KeyMenu, enableNew, enableEdit);
      Parent.KeyHeading.Enabled = true;
    }

    // Sets the row stored values.
    private void SetStoredValues(LJCGridRow row, DataKey dataRecord)
    {
      row.LJCSetInt32(DataKey.ColumnID, dataRecord.ID);
    }
    #endregion

    #region Common Action Methods

    // Deletes the selected row.
    internal void Delete()
    {
      bool success = false;
      var row = KeyGrid.CurrentRow as LJCGridRow;
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
        var id = Parent.DataTableID();
        var keyColumns = new DbColumns()
        {
          { DataUtilTable.ColumnID, id }
        };
        KeyManager.Delete(keyColumns);
        if (0 == KeyManager.AffectedCount)
        {
          success = false;
          var message = FormCommon.DeleteError;
          MessageBox.Show(message, "Delete Error", MessageBoxButtons.OK
            , MessageBoxIcon.Exclamation);
        }
      }

      if (success)
      {
        KeyGrid.Rows.Remove(row);
        SetControlState();
        Parent.TimedChange(Change.Key);
      }
    }

    // Displays a detail dialog to edit a record.
    internal void Edit()
    {
      if (TableGrid.CurrentRow is LJCGridRow
        && KeyGrid.CurrentRow is LJCGridRow)
      {
        int id = Parent.DataKeyID();
        int parentID = Parent.DataTableID();
        string parentName = Parent.DataTableName();
        var location = FormPoint.DialogScreenPoint(KeyGrid);
        var detail = new DataKeyDetail()
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
    internal void New()
    {
      if (TableGrid.CurrentRow is LJCGridRow)
      {
        int parentID = Parent.DataTableID();
        string parentName = Parent.DataTableName();
        var location = FormPoint.DialogScreenPoint(KeyGrid);
        var detail = new DataKeyDetail
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
    internal void Refresh()
    {
      Parent.Cursor = Cursors.WaitCursor;
      int id = 0;
      if (KeyGrid.CurrentRow is LJCGridRow)
      {
        // Save the original row.
        id = Parent.DataKeyID();
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
      var detail = sender as DataKeyDetail;
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
          KeyGrid.LJCSetCurrentRow(row, true);
          SetControlState();
          Parent.TimedChange(Change.Key);
        }
      }
    }
    #endregion

    #region Custom Action Methods

    // The key types.
    public enum KeyType
    {
      Primary = 1,
      Unique,
      Foreign
    }

    // Generates the create Foreign Key procedure.
    internal void ForeignKeyProc()
    {
      var row = Parent.DataKeyCurrent();
      var id = Parent.DataKeyID(row);
      var dataKey = Managers.GetDataKey(id);
      if (dataKey != null
        && dataKey.KeyType == (int)KeyType.Foreign)
      {
        var dbName = "DataDataUtility";
        var fkName = dataKey.Name;
        var tableRow = Parent.DataTableCurrent();
        var sourceTableName = Parent.DataTableName(tableRow);
        var sourceColumnName = dataKey.SourceColumnName;
        var targetTableName = dataKey.TargetTableName;
        var targetColumnName = dataKey.TargetColumnName;

        var proc = new ProcBuilder(dbName, sourceTableName);
        proc.Begin(proc.ForeignKeyProcName);

        proc.Line("AS");
        proc.Line("BEGIN");

        proc.Line($"IF OBJECT_ID('{fkName}', N'f')");
        proc.Line(" IS NULL");
        proc.Line("BEGIN");
        proc.Line($"  ALTER TABLE[dbo].[{sourceTableName}]");
        proc.Line($"  ADD CONSTRAINT[{fkName}]");
        proc.Line($"  FOREIGN KEY([{sourceColumnName}])");
        proc.Line($"  REFERENCES[dbo].[{targetTableName}]([{targetColumnName}])");
        proc.Line("   ON DELETE NO ACTION ON UPDATE NO ACTION;");
        proc.Line("END");
        proc.Line("END");
        var value = proc.ToString();

        var infoValue = Parent.InfoValue;
        var controlValue = DataUtilityCommon.ShowInfo(value, infoValue.ControlName
          , infoValue);
        Parent.InfoValue = controlValue;
      }
    }

    // Generates the drop Foreign Key procedure.
    internal void ForeignKeyDropProc()
    {
      var row = Parent.DataKeyCurrent();
      var id = Parent.DataKeyID(row);
      var dataKey = Managers.GetDataKey(id);
      if (dataKey != null
        && dataKey.KeyType == (int)KeyType.Foreign)
      {
        var dbName = "DataDataUtility";
        var fkName = dataKey.Name;
        var tableRow = Parent.DataTableCurrent();
        var tableName = Parent.DataTableName(tableRow);

        var proc = new ProcBuilder(dbName, tableName);
        proc.Begin(proc.ForeignKeyDropProcName);

        proc.Line("AS");
        proc.Line("BEGIN");

        proc.Line($"IF OBJECT_ID('{fkName}', N'f')");
        proc.Line(" IS NOT NULL");
        proc.Line($"ALTER TABLE[dbo].[{tableName}]");
        proc.Line($"  DROP CONSTRAINT[{fkName}]");
        proc.Line("END");
        var value = proc.ToString();

        var infoValue = Parent.InfoValue;
        var controlValue = DataUtilityCommon.ShowInfo(value, infoValue.ControlName
          , infoValue);
        Parent.InfoValue = controlValue;
      }
    }
    #endregion

    #region Common Action Event Handlers

    // Handles the New menu item event.
    private void KeyNew_Click(object sender, EventArgs e)
    {
      New();
    }

    // Handles the Edit menu item event.
    private void KeyEdit_Click(object sender, EventArgs e)
    {
      Edit();
    }

    // Handles the Delete menu item event.
    private void KeyDelete_Click(object sender, EventArgs e)
    {
      Delete();
    }

    // Handles the Refresh menu item event.
    private void KeyRefresh_Click(object sender, EventArgs e)
    {
      Refresh();
    }
    #endregion

    #region Custom Action Event Handlers

    // Handles the Gen Foreign Key procedure event.
    private void KeyForeignKeyProc_Click(object sender, EventArgs e)
    {
      ForeignKeyProc();
    }

    // Handles the Gen Foreign Key Drop procedure event.
    private void KeyForeignKeyDropProc_Click(object sender, EventArgs e)
    {
      ForeignKeyDropProc();
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
    private void KeyGrid_KeyDown(object sender, KeyEventArgs e)
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
            var position = FormPoint.MenuScreenPoint(KeyGrid
              , Control.MousePosition);
            var menu = Parent.KeyMenu;
            menu.Show(position);
            menu.Select();
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

    // Handles the Grid MouseDoubleClick event.
    private void KeyGrid_MouseDoubleClick(object sender, MouseEventArgs e)
    {
      if (KeyGrid.LJCGetMouseRow(e) != null)
      {
        Edit();
      }
    }

    // Handles the MouseDown event.
    private void KeyGrid_MouseDown(object sender, MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Right)
      {
        // LJCIsDifferentRow() Sets the LJCLastRowIndex for new row.
        KeyGrid.Select();
        if (KeyGrid.LJCIsDifferentRow(e))
        {
          // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
          KeyGrid.LJCSetCurrentRow(e);
          Parent.TimedChange(Change.Key);
        }
      }
    }

    // Handles the SelectionChanged event.
    private void KeyGrid_SelectionChanged(object sender, EventArgs e)
    {
      if (KeyGrid.LJCAllowSelectionChange)
      {
        Parent.TimedChange(Change.Key);
      }
      KeyGrid.LJCAllowSelectionChange = true;
    }
    #endregion

    #region Properties

    // Gets or sets the Parent List reference.
    private DataUtilityList Parent { get; set; }

    // Gets or sets the parent Grid reference.
    private LJCDataGrid TableGrid { get; set; }

    // Gets or sets the Grid reference.
    private LJCDataGrid KeyGrid { get; set; }

    // Gets or sets the Menu reference.
    private ContextMenuStrip KeyMenu { get; set; }

    // Gets or sets the Managers reference.
    private ManagersDataUtility Managers { get; set; }

    // Gets or sets the Manager reference.
    private DataKeyManager KeyManager { get; set; }

    // Provides the Grid font event handlers.
    private GridFont GridFont { get; set; }

    // Provides the menu font event handlers.
    private MenuFont MenuFont { get; set; }
    #endregion
  }
}
