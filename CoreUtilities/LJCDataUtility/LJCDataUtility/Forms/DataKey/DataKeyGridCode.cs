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
using System.Data.Common;
using LJCDataAccessConfig;

namespace LJCDataUtility
{
  // Provides DataKeyGrid methods for the DataUtilityList window.
  internal class DataKeyGridCode
  {
    #region Constructors

    // Initializes an object instance.
    internal DataKeyGridCode(DataUtilityList parentObject)
    {
      // Initialize property values.
      ParentObject = parentObject;
      ParentObject.Cursor = Cursors.WaitCursor;

      // Set Grid vars.
      TableGrid = ParentObject.TableGrid;
      KeyGrid = ParentObject.KeyGrid;
      KeyMenu = ParentObject.KeyMenu;
      Managers = ParentObject.Managers;
      KeyManager = Managers.DataKeyManager;

      // Fonts
      var fontFamily = ParentObject.Font.FontFamily;
      var style = ParentObject.Font.Style;
      KeyGrid.Font = new Font(fontFamily, 11, style);
      KeyMenu.Font = new Font(fontFamily, 11, style);

      // Font change objects.
      GridFont = new GridFont(ParentObject, KeyGrid);
      GridFont.FontChange += GridFont_FontChange;
      MenuFont = new MenuFont(KeyMenu);
      MenuFont.FontChange += MenuFont_FontChange;

      // Menu item events.
      var list = ParentObject;
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
      ParentObject.Cursor = Cursors.Default;
    }

    // Configures the Grid.
    internal void SetupGrid()
    {
      // Setup default grid columns if no columns are defined.
      if (0 == KeyGrid.Columns.Count)
      {
        List<string> propertyNames = new List<string>()
        {
          DataKey.ColumnName,
          DataKey.ColumnKeyType,
          DataKey.ColumnSourceColumnName,
          DataKey.ColumnTargetTableName,
          DataKey.ColumnTargetColumnName
        };

        // Get the grid columns from the manager Data Definition.
        var gridColumns = KeyManager.Columns(propertyNames);

        // Setup the grid columns.
        KeyGrid.LJCAddColumns(gridColumns);
      }
    }
    #endregion

    #region Data Methods

    // Retrieves the list rows.
    internal void DataRetrieve()
    {
      ParentObject.Cursor = Cursors.WaitCursor;
      KeyGrid.LJCRowsClear();

      if (TableGrid.CurrentRow is LJCGridRow)
      {
        var parentID = ParentObject.DataTableID();
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
      ParentObject.Cursor = Cursors.Default;
      ParentObject.DoChange(Change.Key);
    }

    // Adds a grid row and updates it with the record values.
    private LJCGridRow RowAdd(DataKey data)
    {
      var retValue = KeyGrid.LJCRowAdd();
      SetStoredValues(retValue, data);
      retValue.LJCSetValues(KeyGrid, data);
      SetKeyTypeName(retValue, data.KeyType);
      return retValue;
    }

    // Selects a row based on the key record values.
    private bool RowSelect(long id)
    {
      bool retValue = false;

      if (id > 0)
      {
        ParentObject.Cursor = Cursors.WaitCursor;
        foreach (LJCGridRow row in KeyGrid.Rows)
        {
          var rowID = ParentObject.DataTableID();
          if (rowID == id)
          {
            // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
            KeyGrid.LJCSetCurrentRow(row, true);
            retValue = true;
            break;
          }
        }
        ParentObject.Cursor = Cursors.Default;
      }
      return retValue;
    }

    // Updates the current row with the record values.
    private void RowUpdate(DataKey data)
    {
      if (KeyGrid.CurrentRow is LJCGridRow row)
      {
        SetStoredValues(row, data);
        row.LJCSetValues(KeyGrid, data);
        SetKeyTypeName(row, data.KeyType);
      }
    }

    // Sets the control states based on the current control values.
    internal void SetControlState()
    {
      bool enableNew = TableGrid.CurrentRow != null;
      bool enableEdit = KeyGrid.CurrentRow != null;
      FormCommon.SetMenuState(KeyMenu, enableNew, enableEdit);
      ParentObject.KeyHeading.Enabled = true;

      // Custom Menu Items
      ParentObject.KeyForeignKeyProc.Enabled = false;
      ParentObject.KeyForeignKeyDropProc.Enabled = false;
      var row = ParentObject.DataKeyRow();
      var id = ParentObject.DataKeyID(row);
      if (id > 1)
      {
        var dataKey = Managers.GetDataKey(id);
        if ((int)KeyType.Foreign == dataKey.KeyType)
        {
          ParentObject.KeyForeignKeyProc.Enabled = true;
          ParentObject.KeyForeignKeyDropProc.Enabled = true;
        }
      }

    }

    // Sets the row stored values.
    private void SetStoredValues(LJCGridRow row, DataKey data)
    {
      row.LJCSetInt64(DataKey.ColumnID, data.ID);
      row.LJCSetInt64(DataKey.ColumnDataSiteID, data.DataSiteID);
    }

    // Sets the KeyType column value.
    private string SetKeyTypeName(LJCGridRow row, short keyType)
    {
      var retName = Enum.GetName(typeof(KeyType), keyType);
      row.Cells["KeyType"].Value = retName;
      return retName;
    }
    #endregion

    #region Common Action Methods

    // Deletes the selected row.
    internal void Delete()
    {
      bool isContinue = true;
      var row = KeyGrid.CurrentRow as LJCGridRow;
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
        var id = ParentObject.DataKeyID();
        var keyColumns = new DbColumns()
        {
          { DataKey.ColumnID, id }
        };
        KeyManager.Delete(keyColumns);
        if (0 == KeyManager.AffectedCount)
        {
          isContinue = false;
          var message = FormCommon.DeleteError;
          MessageBox.Show(message, "Delete Error", MessageBoxButtons.OK
            , MessageBoxIcon.Exclamation);
        }
      }

      if (isContinue)
      {
        KeyGrid.Rows.Remove(row);
        SetControlState();
        ParentObject.TimedChange(Change.Key);
      }
    }

    // Displays a detail dialog to edit a record.
    internal void Edit()
    {
      if (TableGrid.CurrentRow is LJCGridRow
        && KeyGrid.CurrentRow is LJCGridRow)
      {
        var id = ParentObject.DataKeyID();
        var parentID = ParentObject.DataTableID();
        string parentName = ParentObject.DataTableName();
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
        detail.Dispose();
      }
    }

    // Displays a detail dialog for a new record.
    internal void New()
    {
      if (TableGrid.CurrentRow is LJCGridRow)
      {
        var parentID = ParentObject.DataTableID();
        var parentSiteID = ParentObject.DataKeySiteID();
        string parentName = ParentObject.DataTableName();
        var location = FormPoint.DialogScreenPoint(KeyGrid);
        var detail = new DataKeyDetail
        {
          LJCLocation = location,
          LJCManagers = Managers,
          LJCParentID = parentID,
          LJCParentSiteID = parentSiteID,
          LJCParentName = parentName
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
      ParentObject.Cursor = Cursors.WaitCursor;
      long id = 0;
      if (KeyGrid.CurrentRow is LJCGridRow)
      {
        // Save the original row.
        id = ParentObject.DataKeyID();
      }
      DataRetrieve();

      // Select the original row.
      if (id > 0)
      {
        RowSelect(id);
      }
      ParentObject.Cursor = Cursors.Default;
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
          ParentObject.TimedChange(Change.Key);
        }
      }
    }
    #endregion

    #region Custom Action Methods

    // Generates the create Foreign Key procedure.
    internal void ForeignKeyProc()
    {
      var row = ParentObject.DataKeyRow();
      var id = ParentObject.DataKeyID(row);
      var dataKey = Managers.GetDataKey(id);
      if (dataKey != null
        && dataKey.KeyType == (int)KeyType.Foreign)
      {
        var fkName = dataKey.Name;
        var tableRow = ParentObject.DataTableRow();
        var sourceTableName = ParentObject.DataTableName(tableRow);
        var sourceNames = NetString.DelimitValues(dataKey.SourceColumnName
          , "[", "]");
        var targetTableName = dataKey.TargetTableName;
        var targetNames = NetString.DelimitValues(dataKey.TargetColumnName
          , "[", "]");

        // Get DataConfig
        var configCombo = ParentObject.DataConfigCombo;
        var dataConfig = configCombo.SelectedItem as DataConfig;
        var dbName = dataConfig.Database;

        var proc = new ProcBuilder(ParentObject, dbName, sourceTableName);
        proc.Begin(proc.ForeignKeyProcName);
        proc.Line("AS");
        proc.Line("BEGIN");

        proc.Line($"IF OBJECT_ID('{fkName}', N'f')");
        proc.Line(" IS NULL");
        proc.Line("BEGIN");
        proc.Line($"  ALTER TABLE[dbo].[{sourceTableName}]");
        proc.Line($"  ADD CONSTRAINT[{fkName}]");
        proc.Line($"  FOREIGN KEY([{sourceNames}])");
        proc.Line($"  REFERENCES[dbo].[{targetTableName}]([{targetNames}])");
        proc.Line("   ON DELETE NO ACTION ON UPDATE NO ACTION;");
        proc.Line("END");
        proc.Line("END");
        var value = proc.ToString();

        var infoValue = ParentObject.InfoValue;
        var controlValue = DataUtilityCommon.ShowInfo(value
          , "Foreign Key Procedure", infoValue);
        ParentObject.InfoValue = controlValue;
      }
    }

    // Generates the drop Foreign Key procedure.
    internal void ForeignKeyDropProc()
    {
      var row = ParentObject.DataKeyRow();
      var id = ParentObject.DataKeyID(row);
      var dataKey = Managers.GetDataKey(id);
      if (dataKey != null
        && dataKey.KeyType == (int)KeyType.Foreign)
      {
        var fkName = dataKey.Name;
        var tableRow = ParentObject.DataTableRow();
        var tableName = ParentObject.DataTableName(tableRow);

        // Get DataConfig
        var configCombo = ParentObject.DataConfigCombo;
        var dataConfig = configCombo.SelectedItem as DataConfig;
        var dbName = dataConfig.Database;

        var proc = new ProcBuilder(ParentObject, dbName, tableName);
        proc.Begin(proc.ForeignKeyDropProcName);

        proc.Line("AS");
        proc.Line("BEGIN");

        proc.Line($"IF OBJECT_ID('{fkName}', N'f')");
        proc.Line(" IS NOT NULL");
        proc.Line($"ALTER TABLE[dbo].[{tableName}]");
        proc.Line($"  DROP CONSTRAINT[{fkName}]");
        proc.Line("END");
        var value = proc.ToString();

        var infoValue = ParentObject.InfoValue;
        var controlValue = DataUtilityCommon.ShowInfo(value
          , "Foreign Key Drop Procedure", infoValue);
        ParentObject.InfoValue = controlValue;
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
      var text = ParentObject.Text;
      var index = text.IndexOf("[");
      if (index > 0)
      {
        text = ParentObject.Text.Substring(0, index - 1);
      }
      var fontSize = GridFont.FontSize;
      ParentObject.Text = $"{text} [{fontSize}]";
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
            var menu = ParentObject.KeyMenu;
            menu.Show(position);
            menu.Select();
            e.Handled = true;
          }
          break;

        case Keys.Tab:
          if (e.Shift)
          {
            ParentObject.ColumnTabs.Select();
          }
          else
          {
            ParentObject.ColumnTabs.Select();
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
        if (KeyGrid.LJCIsDifferentRow(e))
        {
          // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
          KeyGrid.LJCSetCurrentRow(e);
          SetControlState();
          ParentObject.TimedChange(Change.Key);
        }
      }
    }

    // Handles the SelectionChanged event.
    private void KeyGrid_SelectionChanged(object sender, EventArgs e)
    {
      if (KeyGrid.LJCAllowSelectionChange)
      {
        SetControlState();
        ParentObject.TimedChange(Change.Key);
      }
      KeyGrid.LJCAllowSelectionChange = true;
    }
    #endregion

    #region Properties

    // Gets or sets the Parent List reference.
    private DataUtilityList ParentObject { get; set; }

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
