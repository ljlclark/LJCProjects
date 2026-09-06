// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// #SectionBegin
// #Value _FileName_ DataColumnGridCode5
// #Value _Namespace_ LJCDataUtility5
// #Value _ParentObject_ DataUtilityList
// #Value _ClassName_ DataColumnGridCode
// #Value _ParentDataObjectName_ DataUtilTable
// #Value _ParentDataObjectShortName_ DataTable
// #Value _ParentGridName_ Table
// #Value _DataObjectName_ DataUtilColumn
// #Value _DataObjectShortName_ DataColumn
// #Value _GridName_ Column

// _FileName_.cs
using LJCControls5;
using LJCDataUtilityDAL5;
using LJCNetCommon5;
//using static _Namespace_._ParentObject_;

namespace LJCDataUtility5
{
  // Provides methods for the _GridName_ grid.
  internal class _ClassName_
  {
    #region Constructor Methods

    // Initializes an object instance.
    internal _ClassName_(_ParentObject_ parentObject, short dbGroupId)
    {
      // Initialize property values.
      ParentObject = parentObject;
      ParentObject.Cursor = Cursors.WaitCursor;
      DbGroupId = dbGroupId;

      // Set control code vars.
      var parentGridCode = ParentObject._ParentGridName_GridCode;
      ParentGrid = ParentObject._ParentGridName_Grid;
      Grid = ParentObject._GridName_Grid;
      GridMenu = ParentObject._GridName_Menu;

      // Set Data vars.
      Managers = ParentObject.Managers;
      Reset();

      // Menu item events.
      var list = ParentObject;
      list._GridName_New.Click += New_Click;
      list._GridName_Edit.Click += Edit_Click;
      list._GridName_Delete.Click += Delete_Click;
      list._GridName_Refresh.Click += Refresh_Click;
      list._GridName_Exit.Click += list.Exit_Click;

      // Grid events.
      //var grid = _GridName_Grid;
      Grid.KeyDown += Grid_KeyDown;
      Grid.MouseDoubleClick += Grid_MouseDoubleClick;
      Grid.MouseDown += Grid_MouseDown;
      Grid.SelectionChanged += Grid_SelectionChanged;
      Grid.MouseEnter += Grid_MouseEnter;

      ParentObject.Cursor = Cursors.Default;
    }

    // Resets the data manager.
    internal void Reset()
    {
      if (!LJC.Equals(CurrentDataConfigName, Managers.DataConfigName))
      {
        GridManager = Managers._DataObjectShortName_Manager;
        CurrentDataConfigName = Managers.DataConfigName;
        var error = Managers.Error;
        if (LJC.HasText(error))
        {
          MessageBox.Show(error, "_GridName_ Manager Error");
        }
      }
    }

    // Configures the Grid.
    internal void SetupGrid()
    {
      // Setup default grid columns if no columns are defined.
      if (0 == Grid.Columns.Count)
      {
        var propertyNames = new List<string>()
        {
          _DataObjectName_.ColumnName,
          _DataObjectName_.ColumnDescription,
        };

        if (GridManager != null)
        {
          // Get the grid columns from the manager Data Definition.
          var gridColumns = GridManager.Columns(propertyNames);

          // Setup the grid columns.
          if (gridColumns != null)
          {
            Grid.LJCAddColumns(gridColumns);
          }
        }
      }
    }
    #endregion

    #region Item Value Methods

    // Gets the selected grid row.
    internal LJCGridRow? Row()
    {
      var retRow = Grid.CurrentRow as LJCGridRow;
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
        && "_GridName_Grid" == row.DataGridView.Name)
      {
        dbId = row.LJCGetInt16(_DataObjectName_.ColumnDbId);
        retId = row.LJCGetInt64(_DataObjectName_.ColumnId);
      }
      return retId;
    }

    // Gets the selected row Name.
    internal string? RowName(LJCGridRow? row = null)
    {
      string? retName = null;

      string gridName = "_GridName_Grid";
      string columnName = _DataObjectName_.ColumnName;

      row ??= Row();
      if (row != null
        && row.DataGridView != null
        && gridName == row.DataGridView.Name)
      {
        retName = row.LJCGetString(columnName);
      }
      return retName;
    }
    #endregion

    #region Data Methods

    // Retrieves the list rows.
    internal void DataRetrieve()
    {
      var parentGrid = _ParentGridName_Grid;
      var parentManager = _ParentDataObjectShortName_Manager;
      //var grid = _GridName_Grid;
      //var manager = _GridName_Manager;

      ParentObject.Cursor = Cursors.WaitCursor;
      Grid.LJCRowsClear();

      // Parent grid has a selection.
      if (parentGrid.CurrentRow is LJCGridRow)
      {
        var parentGridCode = ParentObject._ParentGridName_GridCode;
        var parentId = parentGridCode.RowId(out short parentDbId);
        var keyColumns = parentManager.ParentKey(parentDbId, parentId);

        if (manager != null
          && manager.Manager != null)
        {
          var result = manager.LoadResult(keyColumns);
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
      }
      SetControlState();
      ParentObject.Cursor = Cursors.Default;
      ParentObject.DoChange(Change.Table);
    }

    // Adds a grid row and updates it with the record values.
    private LJCGridRow? RowAdd(DataUtilTable data)
    {
      var retRow = _GridName_Grid.LJCRowAdd();
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
      var retRow = _GridName_Grid.LJCRowAdd();
      if (retRow != null)
      {
        SetStoredValues(retRow, dataValues);
        retRow.LJCSetValues(dataValues);
      }
      return retRow;
    }

    // Selects a row based on the ID value.
    internal bool RowSelect(short dbId, long id)
    {
      bool retValue = false;

      if (dbId > 0
        && id > 0)
      {
        var data = new _DataObjectName_()
        {
          DbId = dbId,
          Id = id,
        };
        retValue = RowSelect(data);
      }
      return retValue;
    }

    // Selects a row based on the data values.
    private bool RowSelect(_DataObjectName_ data)
    {
      bool retValue = false;

      if (data != null)
      {
        ParentObject.Cursor = Cursors.WaitCursor;
        var grid = _GridName_Grid;
        foreach (LJCGridRow row in grid.Rows)
        {
          var rowId = RowId(out short rowDbId, row);
          if (rowDbId == data.DbId
            && rowId == data.Id)
          {
            // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
            grid.LJCSetCurrentRow(row, true);
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
      bool enableNew = _ParentGridName_Grid.CurrentRow != null;
      bool enableEdit = _GridName_Grid.CurrentRow != null;
      FormCommon.SetMenuState(_GridName_Menu, enableNew, enableEdit);
      //ParentObject._GridName_Heading.Enabled = true;
    }

    // Sets the row stored values from the data object.
    private void SetStoredValues(LJCGridRow row, DataUtilTable data)
    {
      row.LJCSetInt16(_DataObjectName_.ColumnDbId, data.DbId);
      row.LJCSetInt64(_DataObjectName_.ColumnId, data.Id);
      row.LJCSetString(_DataObjectName_.ColumnName, data.Name);
    }

    // Sets the row stored values from the data values.
    private void SetStoredValues(LJCGridRow row, LJCDataValues dataValues)
    {
      var columnName = _DataObjectName_.ColumnDbId;
      var dbId = dataValues.LJCInt16(columnName);
      row.LJCSetInt16(columnName, dbId);

      columnName = _DataObjectName_.ColumnId;
      var id = dataValues.LJCInt64(columnName);
      row.LJCSetInt64(columnName, id);

      columnName = _DataObjectName_.ColumnName;
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
        var row = _GridName_Grid.CurrentRow;
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

        var manager = _GridName_Manager;
        var keyColumns = new LJCDataColumns()
        {
          { _DataObjectName_.ColumnDbId, dbId },
          { _DataObjectName_.ColumnId, id },
        };

        if (manager != null)
        {
          manager.Delete(keyColumns);
          if (0 == manager.AffectedCount)
          {
            var message = FormCommon.DeleteError;
            MessageBox.Show(message, "Delete Error", MessageBoxButtons.OK
              , MessageBoxIcon.Exclamation);
            break;
          }

          _GridName_Grid.Rows.Remove(row);
          SetControlState();
        }
        ParentObject.TimedChange(Change.Table);
        break;
      }
    }

    // Displays a detail dialog to edit a record.
    internal void Edit()
    {
      // Parent combo and current grid have selections.
      if (_ParentGridName_Grid.CurrentRow is LJCGridRow
        && _GridName_Grid.CurrentRow is LJCGridRow)
      {
        // Data from parent item.
        var parentGridCode = ParentObject._ParentGridName_GridCode;
        var parentId = parentGridCode.RowId(out short parentDbId);
        string? parentName = parentGridCode.RowName();

        // Data from current item.
        var id = RowId(out short dbId);

        var location = FormPoint.DialogScreenPoint(TableGrid);
        var detail = new DataTableDetail()
        {
          LJCDbId = dbId,
          LJCId = id,
          LJCModuleDbId = parentDbId,
          LJCModuleId = parentId,
          LJCModuleName = parentName,
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

    #region Action Event Handlers

    // Handles the New menu item event.
    private void New_Click(object? sender, EventArgs e)
    {
      New();
    }

    // Handles the Edit menu item event.
    private void Edit_Click(object? sender, EventArgs e)
    {
      Edit();
    }

    // Handles the Delete menu item event.
    private void Delete_Click(object? sender, EventArgs e)
    {
      Delete();
    }

    // Handles the Refresh menu item event.
    private void Refresh_Click(object? sender, EventArgs e)
    {
      Refresh();
    }
    #endregion

    #region Control Event Handlers

    // Handles the Grid KeyDown event.
    private void Grid_KeyDown(object? sender, KeyEventArgs e)
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
    private void Grid_MouseDoubleClick(object? sender, MouseEventArgs e)
    {
      if (TableGrid.LJCGetMouseRow(e) != null)
      {
        Edit();
      }
    }

    // Handles the MouseDown event.
    private void Grid_MouseDown(object? sender, MouseEventArgs e)
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
    private void Grid_SelectionChanged(object? sender, EventArgs e)
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
    private string? CurrentDataConfigName { get; set; }

    // Gets or sets the database id.
    internal short DbGroupId { get; set; }

    // Gets or sets the grid reference.
    private LJCDataGrid Grid { get; set; } = null!;

    // Gets or sets the Menu reference.
    private ContextMenuStrip GridMenu { get; set; }

    // Gets or sets the Manager reference.
    private DataTableManager? GridManager { get; set; } = null!;

    // Gets or sets the Managers reference.
    private ManagersDataUtility Managers { get; set; }

    // Gets or sets the parent Combo reference.
    private LJCItemCombo ModuleCombo { get; set; }

    // Gets or sets the parent grid reference.
    private LJCDataGrid ParentGrid { get; set; } = null!;

    // Gets or sets the parent grid reference.
    private _ParentDataObjectShortName_GridCode ParentGridCode { get; set; } = null!;

    // Gets or sets the Parent List reference.
    private DataUtilityList ParentObject { get; set; }
    #endregion
  }
  // #SectionEnd
}
