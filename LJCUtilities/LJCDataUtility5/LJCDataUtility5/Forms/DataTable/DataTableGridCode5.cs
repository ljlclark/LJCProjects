// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DataTableGridCode5.cs
using LJCControls5;
using LJCDataUtilityDAL5;
using LJCDBMessage5;
using LJCNetCommon5;
using static LJCDataUtility5.DataUtilityList;

namespace LJCDataUtility5
{
  // Provides methods for the Table grid.
  internal class DataTableGridCode
  {
    #region Constructor Methods

    // Initializes an object instance.
    internal DataTableGridCode(DataUtilityList parentObject, short dbGroupId)
    {
      // Initialize property values.
      ParentObject = parentObject;
      ParentObject.Cursor = Cursors.WaitCursor;
      DbGroupId = dbGroupId;

      // Set control values.
      ModuleCombo = ParentObject.ModuleCombo;
      TableGrid = parentObject.TableGrid;
      TableMenu = ParentObject.TableMenu;

      // Set data values.
      Managers = ParentObject.Managers;
      TableManager = Managers.DataTableManager;
      ShowManagersErrors();
      DataConfigName = Managers.DataConfigName;
      Reset();

      // Menu item events.
      var list = ParentObject;
      list.TableNew.Click += TableNew_Click;
      list.TableEdit.Click += TableEdit_Click;
      list.TableDelete.Click += TableDelete_Click;
      list.TableRefresh.Click += TableRefresh_Click;
      list.TableExit.Click += list.Exit_Click;

      list.TableUpdate.Click += TableUpdate_Click;
      list.TableCreate.Click += TableCreate_Click;
      list.TableConvert.Click += TableConvert_Click;
      list.TableAddProc.Click += TableAddProc_Click;
      list.TableCreateData.Click += TableCreateData_Click;

      // Grid events.
      var grid = TableGrid;
      grid.KeyDown += TableGrid_KeyDown;
      grid.MouseDoubleClick += TableGrid_MouseDoubleClick;
      grid.MouseDown += TableGrid_MouseDown;
      grid.SelectionChanged += TableGrid_SelectionChanged;
      grid.MouseEnter += Grid_MouseEnter;

      ParentObject.Cursor = Cursors.Default;
    }

    // Resets the data manager.
    internal void Reset()
    {
      if (!LJC.IsEqual(DataConfigName, Managers.DataConfigName))
      {
        // "Reset" property values.
        DataConfigName = Managers.DataConfigName;
        TableManager = Managers.DataTableManager;
        ShowManagersErrors();
      }
    }

    // Configures the Grid.
    internal void SetupGrid()
    {
      // Setup default grid columns if no columns are defined.
      if (0 == TableGrid.Columns.Count)
      {
        var propertyNames = new List<string>()
        {
          DataUtilTable.ColumnName,
          DataUtilTable.ColumnDescription,
          DataUtilTable.ColumnSequence
        };

        // Get the grid columns from the manager Data Definition.
        var gridColumns = TableManager.Columns(propertyNames);

        // Setup the grid columns.
        if (gridColumns != null)
        {
          TableGrid.LJCAddColumns(gridColumns);
        }
      }
    }

    // Show the managers errors.
    private void ShowManagersErrors()
    {
      var error = Managers.Error;
      if (LJC.HasText(error))
      {
        MessageBox.Show(error, "Managers Error");
      }
    }
    #endregion

    #region Item Value Methods

    // Gets the selected grid row.
    internal LJCGridRow? Row()
    {
      var retRow = TableGrid.CurrentRow as LJCGridRow;
      return retRow;
    }

    // Gets the selected row ID.
    internal long RowId(out short dbId, LJCGridRow? row = null)
    {
      long retId = 0;

      dbId = 0;
      row ??= Row();
      if (row != null
        && row.DataGridView != null
        && "TableGrid" == row.DataGridView.Name)
      {
        dbId = row.LJCGetInt16(DataUtilTable.ColumnDbId);
        retId = row.LJCGetInt64(DataUtilTable.ColumnId);
      }
      return retId;
    }

    // Gets the selected row Name.
    internal string? RowName(LJCGridRow? row = null)
    {
      string? retName = null;

      row ??= Row();
      if (row != null
        && row.DataGridView != null
        && "TableGrid" == row.DataGridView.Name)
      {
        retName = row.LJCGetString(DataUtilTable.ColumnName);
      }
      return retName;
    }

    // Gets the target table ID.
    internal long? TargetDataTableId(string targetTableName, out short tableDbId)
    {
      long? retTableId = 0;

      tableDbId = 0;
      var comboCode = ParentObject.ModuleComboCode;
      var moduleId = comboCode.ItemId(out short moduleDbId);
      var targetTable = TableManager.RetrieveUnique(moduleDbId, moduleId
        , targetTableName);
      if (targetTable != null)
      {
        tableDbId = targetTable.DbId;
        retTableId = targetTable.Id;
      }
      return retTableId;
    }
    #endregion

    #region Data Methods

    // Retrieves the list rows.
    internal void DataRetrieve()
    {
      ParentObject.Cursor = Cursors.WaitCursor;
      TableGrid.LJCRowsClear();

      // Parent combo has a selection.
      var moduleId = ModuleCombo.LJCSelectedItemID(out short moduleDbId);
      if (moduleDbId > 0
        && moduleId > 0)
      {
        var keyColumns = DataTableManager.ParentKey(moduleDbId, moduleId);
        var orderBy = new List<string>()
        {
          DataUtilTable.ColumnSequence
        };

        if (TableManager.Manager != null)
        {
          TableManager.Manager.OrderByNames = orderBy;
          var result = TableManager.LoadResult(keyColumns);
          RowsAdd(result);
        }
      }
      SetControlState();
      ParentObject.Cursor = Cursors.Default;
      ParentObject.DoChange(Change.Table);
    }

    // Adds a grid row and updates it with the record values.
    private LJCGridRow? RowAdd(DataUtilTable data)
    {
      var retRow = TableGrid.LJCRowAdd();
      if (retRow != null)
      {
        SetStoredValues(retRow, data);
        retRow.LJCSetValues(data);
      }
      return retRow;
    }

    // Adds a grid row and updates it with the result values.
    private LJCGridRow? RowAddValues(LJCDataValues dataValues)
    {
      var retRow = TableGrid.LJCRowAdd();
      if (retRow != null)
      {
        SetStoredValues(retRow, dataValues);
        retRow.LJCSetValues(dataValues);
      }
      return retRow;
    }

    // Creates the grid rows from the collection object.
    private void RowsAdd(DataTables items)
    {
      if (LJC.HasListItems(items))
      {
        foreach (var item in items)
        {
          RowAdd(item);
        }
      }
    }

    // Creates the grid rows from the result rows.
    private void RowsAdd(LJCDBResult? result)
    {
      if (result != null
        && LJC.HasListItems(result.Rows))
      {
        foreach (var row in result.Rows)
        {
          if (LJC.HasListItems(row.Values))
          {
            RowAddValues(row.Values);
          }
        }
      }
    }

    // Selects a row based on the ID value.
    internal bool RowSelect(short dbId, long id)
    {
      bool retValue = false;

      if (dbId > 0
        && id > 0)
      {
        var data = new DataUtilTable()
        {
          DbId = dbId,
          Id = id,
        };
        retValue = RowSelect(data);
      }
      return retValue;
    }

    // Selects a row based on the data values.
    private bool RowSelect(DataUtilTable data)
    {
      bool retValue = false;

      if (data != null)
      {
        ParentObject.Cursor = Cursors.WaitCursor;
        foreach (LJCGridRow row in TableGrid.Rows)
        {
          var rowId = RowId(out short rowDbId, row);
          if (rowDbId == data.DbId
            && rowId == data.Id)
          {
            // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
            TableGrid.LJCSetCurrentRow(row, true);
            retValue = true;
            break;
          }
        }
        ParentObject.Cursor = Cursors.Default;
      }
      return retValue;
    }

    // Updates the current row with the record values.
    private void RowUpdate(DataUtilTable data)
    {
      if (TableGrid.CurrentRow is LJCGridRow row)
      {
        SetStoredValues(row, data);
        row.LJCSetValues(data);
      }
    }

    // Sets the control states based on the current control values.
    private void SetControlState()
    {
      bool enableNew = ModuleCombo.SelectedItem != null;
      bool enableEdit = TableGrid.CurrentRow != null;
      FormCommon.SetMenuState(TableMenu, enableNew, enableEdit);
      //ParentObject.TableHeading.Enabled = true;
    }

    // Sets the row stored values from the data object.
    private void SetStoredValues(LJCGridRow row, DataUtilTable data)
    {
      row.LJCSetInt16(DataUtilTable.ColumnDbId, data.DbId);
      row.LJCSetInt64(DataUtilTable.ColumnId, data.Id);
      row.LJCSetString(DataUtilTable.ColumnName, data.Name);
    }

    // Sets the row stored values from the data values.
    private void SetStoredValues(LJCGridRow row, LJCDataValues dataValues)
    {
      var columnName = DataUtilTable.ColumnDbId;
      var dbId = dataValues.LJCInt16(columnName);
      row.LJCSetInt16(columnName, dbId);

      columnName = DataUtilTable.ColumnId;
      var id = dataValues.LJCInt64(columnName);
      row.LJCSetInt64(columnName, id);

      columnName = DataUtilTable.ColumnName;
      var name = dataValues.LJCString(columnName);
      if (name != null)
      {
        row.LJCSetString(columnName, name);
      }
    }
    #endregion

    #region Action Methods

    // Deletes the selected row.
    internal void Delete()
    {
      while (true)
      {
        var row = TableGrid.CurrentRow;
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

        // Data from current item.
        var id = RowId(out short dbId);

        var keyColumns = new LJCDataColumns()
        {
          { DataUtilTable.ColumnDbId, dbId },
          { DataUtilTable.ColumnId, id },
        };

        TableManager.Delete(keyColumns);
        if (0 == TableManager.AffectedCount)
        {
          var message = FormCommon.DeleteError;
          MessageBox.Show(message, "Delete Error", MessageBoxButtons.OK
            , MessageBoxIcon.Exclamation);
          break;
        }

        TableGrid.Rows.Remove(row);
        SetControlState();
        ParentObject.TimedChange(Change.Table);
        break;
      }
    }

    // Displays a detail dialog to edit a record.
    internal void Edit()
    {
      // Parent combo and current grid have selections.
      if (ModuleCombo.SelectedItem is LJCItem
        && TableGrid.CurrentRow is LJCGridRow)
      {
        // Data from parent item.
        short moduleDbId = 0;
        long moduleId = 0;
        string moduleName = "";
        if (ModuleCombo.SelectedItem is LJCItem item)
        {
          moduleDbId = item.DbID;
          moduleId = item.ID;
          moduleName = ModuleCombo.Text;
        }

        // Data from current item.
        var id = RowId(out short dbId);

        var location = FormPoint.DialogScreenPoint(TableGrid);
        var detail = new DataTableDetail()
        {
          LJCDbId = dbId,
          LJCId = id,
          LJCModuleDbId = moduleDbId,
          LJCModuleId = moduleId,
          LJCModuleName = moduleName,
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
      // Parent combo and current grid have selections.
      if (ModuleCombo.SelectedItem is LJCItem
        && TableGrid.CurrentRow is LJCGridRow)
      {
        // Data from parent item.
        short moduleDbId = 0;
        long moduleId = 0;
        string moduleName = "";
        if (ModuleCombo.SelectedItem is LJCItem item)
        {
          moduleDbId = item.DbID;
          moduleId = item.ID;
          moduleName = ModuleCombo.Text;
        }

        // Data from current item.
        int sequence = TableGrid.Rows.Count + 1;

        var location = FormPoint.DialogScreenPoint(TableGrid);
        var detail = new DataTableDetail
        {
          LJCDbId = DbGroupId,
          LJCModuleDbId = moduleDbId,
          LJCModuleId = moduleId,
          LJCModuleName = moduleName,
          LJCSequence = sequence,
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
      if (TableGrid.CurrentRow is LJCGridRow)
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
      if (sender is DataTableDetail detail)
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
              TableGrid.LJCSetCurrentRow(row, true);
              SetControlState();
              ParentObject.TimedChange(Change.Table);
            }
          }
        }
      }
    }
    #endregion

    #region Custom Action Methods

    // Handles the "Update From Table" menu event.
    internal void UpdateData()
    {
      UpdateData updateData = new(ParentObject);
      updateData.SetData();
    }

    // Handles the "Create Table" menu event.
    internal void CreateTable()
    {
      CreateTable createTable = new(ParentObject);
      createTable.CreateTableProc();
    }

    // Handles the "Create Table" menu event.
    internal void ConvertTable()
    {
      ConvertTable convertTable = new(ParentObject);
      convertTable.ConvertTableProc();
    }

    // Handles the "Add Data Procedure" menu event.
    internal void AddDataProc()
    {
      AddDataProc addDataProc = new(ParentObject);
      addDataProc.CreateAddDataProc();
    }

    // Handles the "Create Data Procedure" menu event.
    internal void CreateDataProc()
    {
      CreateData createData = new(ParentObject);
      createData.CreateDataProc();
    }
    #endregion

    #region Action Event Handlers

    // Handles the New menu item event.
    private void TableNew_Click(object? sender, EventArgs e)
    {
      New();
    }

    // Handles the Edit menu item event.
    private void TableEdit_Click(object? sender, EventArgs e)
    {
      Edit();
    }

    // Handles the Delete menu item event.
    private void TableDelete_Click(object? sender, EventArgs e)
    {
      Delete();
    }

    // Handles the Refresh menu item event.
    private void TableRefresh_Click(object? sender, EventArgs e)
    {
      Refresh();
    }
    #endregion

    #region Custom Action Event Handlers

    // Handles the Update Data menu item event.
    private void TableUpdate_Click(object? sender, EventArgs e)
    {
      UpdateData();
    }

    // Handles the Create Table menu item event.
    private void TableCreate_Click(object? sender, EventArgs e)
    {
      CreateTable();
    }

    // Handles the Create Table menu item event.
    private void TableConvert_Click(object? sender, EventArgs e)
    {
      ConvertTable();
    }

    // Handles the Add Data Procedure menu item event.
    private void TableAddProc_Click(object? sender, EventArgs e)
    {
      AddDataProc();
    }

    // Handles the Create Data menu item event.
    private void TableCreateData_Click(object? sender, EventArgs e)
    {
      CreateDataProc();
    }
    #endregion

    #region Control Event Handlers

    // Handles the Grid KeyDown event.
    private void TableGrid_KeyDown(object? sender, KeyEventArgs e)
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
            var menu = ParentObject.TableMenu;
            menu.Show(position);
            menu.Select();
            e.Handled = true;
          }
          break;

        case Keys.Tab:
          if (e.Shift)
          {
            ParentObject.ConfigCombo.Select();
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
    private void TableGrid_MouseDoubleClick(object? sender, MouseEventArgs e)
    {
      if (TableGrid.LJCGetMouseRow(e) != null)
      {
        Edit();
      }
    }

    // Handles the MouseDown event.
    private void TableGrid_MouseDown(object? sender, MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Right)
      {
        TableGrid.Focus();

        // LJCIsDifferentRow() Sets the LJCLastRowIndex for new row.
        if (TableGrid.LJCIsDifferentRow(e))
        {
          // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
          TableGrid.LJCSetCurrentRow(e);
          SetControlState();
          ParentObject.TimedChange(Change.Table);
        }
      }
    }

    // Handles the SelectionChanged event.
    private void TableGrid_SelectionChanged(object? sender, EventArgs e)
    {
      if (TableGrid.LJCAllowSelectionChange)
      {
        SetControlState();
        ParentObject.TimedChange(Change.Table);
      }
      TableGrid.LJCAllowSelectionChange = true;
    }

    // Handles the MouseEnter event.
    private void Grid_MouseEnter(object? sender, EventArgs e)
    {
      TableGrid.Focus();
    }
    #endregion

    #region Properties

    // Gets or sets the current data config name.
    private string DataConfigName { get; set; }

    // Gets or sets the database id.
    internal short DbGroupId { get; set; }

    // Gets or sets the managers reference.
    private ManagersDataUtility Managers { get; set; }

    // Gets or sets the parent control reference.
    private LJCItemCombo ModuleCombo { get; set; }

    // Gets or sets the parent reference.
    private DataUtilityList ParentObject { get; set; }

    // Gets or sets the main control reference.
    private LJCDataGrid TableGrid { get; set; }

    // Gets or sets the manager reference.
    private DataTableManager TableManager { get; set; }

    // Gets or sets the menu reference.
    private ContextMenuStrip TableMenu { get; set; }
    #endregion
  }
}
