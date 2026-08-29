// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DataKeyGridCode5.cs
using LJCControls5;
using LJCDataUtilityDAL5;
using LJCNetCommon5;
using static LJCDataUtility5.DataUtilityList;

namespace LJCDataUtility5
{
  // Provides methods for the DataKey grid.
  internal class DataKeyGridCode
  {
    #region Constructor Methods

    // Initializes an object instance.
    internal DataKeyGridCode(DataUtilityList parentObject, short dbGroupId)
    {
      // Initialize property values.
      ParentObject = parentObject;
      ParentObject.Cursor = Cursors.WaitCursor;
      DbGroupId = dbGroupId;

      // Set control code vars.
      //var configCombo = ParentObject.ConfigCombo;
      TableGrid = ParentObject.TableGrid;
      KeyGrid = ParentObject.KeyGrid;
      KeyMenu = ParentObject.KeyMenu;

      // Set Data vars.
      Managers = ParentObject.Managers;
      KeyManager = Managers.DataKeyManager;

      // Menu item events.
      var list = ParentObject;
      list.KeyNew.Click += KeyNew_Click;
      list.KeyEdit.Click += KeyEdit_Click;
      list.KeyDelete.Click += KeyDelete_Click;
      list.KeyRefresh.Click += KeyRefresh_Click;
      list.KeyExit.Click += list.Exit_Click;

      // Grid events.
      var grid = KeyGrid;
      grid.KeyDown += KeyGrid_KeyDown;
      grid.MouseDoubleClick += KeyGrid_MouseDoubleClick;
      grid.MouseDown += KeyGrid_MouseDown;
      grid.SelectionChanged += KeyGrid_SelectionChanged;
      grid.MouseEnter += Grid_MouseEnter;

      ParentObject.Cursor = Cursors.Default;
    }

    // Resets the data manager.
    internal void Reset()
    {
      if (!LJC.Equals(CurrentDataConfigName, Managers.DataConfigName))
      {
        KeyManager = Managers.DataKeyManager;
        CurrentDataConfigName = Managers.DataConfigName;
        var error = Managers.Error;
        if (LJC.HasText(error))
        {
          MessageBox.Show(error);
        }
      }
    }

    // Configures the Grid.
    internal void SetupGrid()
    {
      // Setup default grid columns if no columns are defined.
      if (0 == KeyGrid.Columns.Count)
      {
        var propertyNames = new List<string>()
        {
          DataKey.ColumnName,
          DataKey.ColumnKeyType,
          DataKey.ColumnSourceColumnName,
          DataKey.ColumnTargetTableName,
          DataKey.ColumnTargetColumnName
        };

        if (KeyManager != null)
        {
          // Get the grid columns from the manager Data Definition.
          var gridColumns = KeyManager.Columns(propertyNames);

          // Setup the grid columns.
          if (gridColumns != null)
          {
            KeyGrid.LJCAddColumns(gridColumns);
          }
        }
      }
    }
    #endregion

    #region Item Value Methods

    // Gets the current DataKey grid row.
    internal LJCGridRow? Row()
    {
      var retRow = KeyGrid.CurrentRow as LJCGridRow;
      return retRow;
    }

    // Gets the selected row ID.
    internal long RowId(out short dbId, LJCGridRow? row = null)
    {
      long retKeyId = 0;

      dbId = 0;
      row ??= Row();
      if (row != null
        && row.DataGridView != null
        && "KeyGrid" == row.DataGridView.Name)
      {
        dbId = row.LJCGetInt16(DataKey.ColumnDbId);
        retKeyId = row.LJCGetInt64(DataKey.ColumnId);
      }
      return retKeyId;
    }

    // Gets the selected row Name.
    internal string? RowName(LJCGridRow? row = null)
    {
      string? retKeyName = null;

      row ??= Row();
      if (row != null
        && row.DataGridView != null
        && "KeyGrid" == row.DataGridView.Name)
      {
        retKeyName = row.LJCGetString(DataKey.ColumnName);
      }
      return retKeyName;
    }
    #endregion

    #region Data Methods

    // Retrieves the list rows.
    internal void DataRetrieve()
    {
      ParentObject.Cursor = Cursors.WaitCursor;
      KeyGrid.LJCRowsClear();

      // Parent grid has a selection.
      if (TableGrid.CurrentRow is LJCGridRow)
      {
        var tableGridCode = ParentObject.TableGridCode;
        var tableId = tableGridCode.RowId(out short tableDbId);
        var keyColumns = DataKeyManager.ParentKey(tableDbId, tableId);

        if (KeyManager != null
          && KeyManager.Manager != null)
        {
          var items = KeyManager.Load(keyColumns);
          if (LJC.HasListItems(items))
          {
            foreach (var item in items)
            {
              RowAdd(item);
            }
          }
        }
      }
      SetControlState();
      ParentObject.Cursor = Cursors.Default;
      ParentObject.DoChange(Change.Key);
    }

    // Adds a grid row and updates it with the record values.
    private LJCGridRow? RowAdd(DataKey data)
    {
      var retValue = KeyGrid.LJCRowAdd();
      if (retValue != null)
      {
        SetStoredValues(retValue, data);
        retValue.LJCSetValues(data);
        SetKeyTypeName(retValue, data.KeyType);
      }
      return retValue;
    }

    // Selects a row based on the key record values.
    private bool RowSelect(short dbId, long id)
    {
      bool retValue = false;

      if (dbId > 0
        && id > 0)
      {
        var data = new DataKey()
        {
          DbId = dbId,
          Id = id,
        };
        retValue = RowSelect(data);
      }
      return retValue;
    }

    // Selects a row based on the data values.
    private bool RowSelect(DataKey data)
    {
      bool retValue = false;

      if (data != null)
      {
        ParentObject.Cursor = Cursors.WaitCursor;
        foreach (LJCGridRow row in KeyGrid.Rows)
        {
          var rowId = RowId(out short rowDbId, row);
          if (rowDbId == data.DbId
            && rowId == data.Id)
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
        row.LJCSetValues(data);
        SetKeyTypeName(row, data.KeyType);
      }
    }

    // Sets the control states based on the current control values.
    internal void SetControlState()
    {
      bool enableNew = TableGrid.CurrentRow != null;
      bool enableEdit = KeyGrid.CurrentRow != null;
      FormCommon.SetMenuState(KeyMenu, enableNew, enableEdit);
      //ParentObject.KeyHeading.Enabled = true;
    }

    // Sets the row stored values.
    private static void SetStoredValues(LJCGridRow row, DataKey data)
    {
      row.LJCSetInt16(DataKey.ColumnDbId, data.DbId);
      row.LJCSetInt64(DataKey.ColumnId, data.Id);
    }

    // Sets the KeyType column value.
    private static string? SetKeyTypeName(LJCGridRow row, short keyType)
    {
      var retName = Enum.GetName(typeof(KeyType), keyType);
      row.Cells["KeyType"].Value = retName;
      return retName;
    }
    #endregion

    #region Action Methods

    // Deletes the selected row.
    internal void Delete()
    {
      while (true)
      {
        var row = KeyGrid.CurrentRow as LJCGridRow;
        if (row != null)
        {
          var title = "Delete Confirmation";
          var message = FormCommon.DeleteConfirm;
          if (DialogResult.No == MessageBox.Show(message, title
            , MessageBoxButtons.YesNo, MessageBoxIcon.Question))
          {
            break;
          }
        }

        // Data from items.
        var id = RowId(out short dbId);

        var keyColumns = new LJCDataColumns()
        {
          { DataKey.ColumnId, id },
          { DataKey.ColumnDbId, dbId }
        };

        if (KeyManager != null)
        {
          KeyManager.Delete(keyColumns);
          if (0 == KeyManager.AffectedCount)
          {
            var message = FormCommon.DeleteError;
            MessageBox.Show(message, "Delete Error", MessageBoxButtons.OK
              , MessageBoxIcon.Exclamation);
            break;
          }

          KeyGrid.Rows.Remove(row);
          SetControlState();
        }
        ParentObject.TimedChange(Change.Key);
        break;
      }
    }

    // Displays a detail dialog to edit a record.
    internal void Edit()
    {
      // Parent grid and current grid have selections.
      if (TableGrid.CurrentRow is LJCGridRow
        && KeyGrid.CurrentRow is LJCGridRow)
      {
        // Data from items.
        var id = RowId(out short dbId);
        var tableGridCode = ParentObject.TableGridCode;
        var tableId = tableGridCode.RowId(out short tableDbId);
        string? tableName = tableGridCode.RowName();

        var location = FormPoint.DialogScreenPoint(KeyGrid);
        var detail = new DataKeyDetail()
        {
          LJCDbId = dbId,
          LJCId = id,
          LJCTableDbId = tableDbId,
          LJCTableId = tableId,
          LJCTableName = tableName,
          LJCLocation = location,
          LJCManagers = Managers,
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
      // Parent grid has a selection.
      if (TableGrid.CurrentRow is LJCGridRow)
      {
        var tableGridCode = ParentObject.TableGridCode;
        var tableID = tableGridCode.RowId(out short parentDbID);
        string? tableName = tableGridCode.RowName();

        var location = FormPoint.DialogScreenPoint(KeyGrid);
        var detail = new DataKeyDetail
        {
          LJCTableDbId = parentDbID,
          LJCTableId = tableID,
          LJCTableName = tableName,
          LJCLocation = location,
          LJCManagers = Managers,
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
      short dbId = 0;
      long id = 0;
      if (KeyGrid.CurrentRow is LJCGridRow)
      {
        // Save the original row.
        id = RowId(out dbId);
      }
      DataRetrieve();

      // Select the original row.
      if (dbId > 0
        && id > 0)
      {
        RowSelect(dbId, id);
      }
      ParentObject.Cursor = Cursors.Default;
    }

    // Shows the help page
    internal void ShowHelp()
    {
      //Help.ShowHelp(DocList, "_AppName_.chm", HelpNavigator.Topic
      //  , "_ClassName_List.html");
    }

    // Adds or updates row with detail record values.
    private void Detail_Change(object? sender, EventArgs e)
    {
      //var detail = sender as DataKeyDetail;
      if (sender is DataKeyDetail detail)
      {
        var record = detail.LJCRecord;
        if (record != null)
        {
          if (detail.LJCIsUpdate)
          {
            RowUpdate(record);
            Refresh();
          }
          else
          {
            // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
            var row = RowAdd(record);
            if (row != null)
            {
              KeyGrid.LJCSetCurrentRow(row, true);
              SetControlState();
              ParentObject.TimedChange(Change.Key);
            }
          }
        }
      }
    }
    #endregion

    #region Action Event Handlers

    // Handles the New menu item event.
    private void KeyNew_Click(object? sender, EventArgs e)
    {
      New();
    }

    // Handles the Edit menu item event.
    private void KeyEdit_Click(object? sender, EventArgs e)
    {
      Edit();
    }

    // Handles the Delete menu item event.
    private void KeyDelete_Click(object? sender, EventArgs e)
    {
      Delete();
    }

    // Handles the Refresh menu item event.
    private void KeyRefresh_Click(object? sender, EventArgs e)
    {
      Refresh();
    }
    #endregion

    #region Control Event Handlers

    // Handles the Grid KeyDown event.
    private void KeyGrid_KeyDown(object? sender, KeyEventArgs e)
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
            ParentObject.ModuleCombo.Select();
          }
          e.Handled = true;
          break;
      }
    }

    // Handles the Grid MouseDoubleClick event.
    private void KeyGrid_MouseDoubleClick(object? sender, MouseEventArgs e)
    {
      if (KeyGrid.LJCGetMouseRow(e) != null)
      {
        Edit();
      }
    }

    // Handles the MouseDown event.
    private void KeyGrid_MouseDown(object? sender, MouseEventArgs e)
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
    private void KeyGrid_SelectionChanged(object? sender, EventArgs e)
    {
      if (KeyGrid.LJCAllowSelectionChange)
      {
        SetControlState();
        ParentObject.TimedChange(Change.Key);
      }
      KeyGrid.LJCAllowSelectionChange = true;
    }

    // Handles the MouseEnter event.
    private void Grid_MouseEnter(object? sender, EventArgs e)
    {
      KeyGrid.Focus();
    }
    #endregion

    #region Properties

    // Gets or sets the current data config name.
    private string? CurrentDataConfigName { get; set; }

    // Gets or sets the database id.
    internal short DbGroupId { get; set; }

    // Gets or sets the Grid reference.
    private LJCDataGrid KeyGrid { get; set; }

    // Gets or sets the Manager reference.
    private DataKeyManager? KeyManager { get; set; }

    // Gets or sets the Menu reference.
    private ContextMenuStrip KeyMenu { get; set; }

    // Gets or sets the Managers reference.
    private ManagersDataUtility Managers { get; set; }

    // Gets or sets the Parent List reference.
    private DataUtilityList ParentObject { get; set; }

    // Gets or sets the parent Grid reference.
    private LJCDataGrid TableGrid { get; set; }
    #endregion
  }
}
