// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DataTableGridCode5.cs
using LJCControls5;
using LJCDataUtilityDAL5;
using LJCNetCommon5;
using LJControls;
using static LJCDataUtility5.DataUtilityList;

namespace LJCDataUtility5
{
  // Provides methods for the DataTable grid.
  internal class DataTableGridCode
  {
    #region Constructor Methods

    // Initializes an object instance.
    internal DataTableGridCode(DataUtilityList parentObject)
    {
      ParentObject = parentObject;
      ParentObject.Cursor = Cursors.WaitCursor;

      // Set Grid vars.
      TableGrid = parentObject.TableGrid;
      TableMenu = ParentObject.TableMenu;

      // Set Data vars.
      Managers = ParentObject.Managers;
      TableManager = Managers.DataTableManager;

      // Menu item events.
      var list = ParentObject;
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

      ParentObject.Cursor = Cursors.Default;
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
    #endregion

    #region Data Value Methods

    // Gets the current row.
    internal LJCGridRow? Row()
    {
      var retRow = TableGrid.CurrentRow as LJCGridRow;
      return retRow;
    }

    // Gets the selected row ID.
    internal long RowId(out short dbId, LJCGridRow? row = null)
    {
      long retTableId = 0;

      dbId = 0;
      row ??= Row();
      if (row != null
        && row.DataGridView != null
        && "TableGrid" == row.DataGridView.Name)
      {
        retTableId = row.LJCGetInt64(DataUtilTable.ColumnId);
        dbId = row.LJCGetInt16(DataUtilTable.ColumnDbId);
      }
      return retTableId;
    }

    // Gets the selected row Name.
    internal string? RowName(LJCGridRow? row = null)
    {
      string? retTableName = null;

      row ??= Row();
      if (row != null
        && row.DataGridView != null
        && "TableGrid" == row.DataGridView.Name)
      {
        retTableName = row.LJCGetString(DataUtilTable.ColumnName);
      }
      return retTableName;
    }

    // Gets the target table ID.
    internal long? TargetDataTableId(string targetTableName, out short tableDbId)
    {
      long? retTableId = 0;

      tableDbId = 0;

      //var moduleId = DataModuleItemId(out dbId);
      // *** Begin *** Testing
      short moduleDbId = 1;
      var moduleId = 1;
      // *** End ***
      var tableManager = Managers.DataTableManager;
      var targetTable = tableManager.RetrieveUnique(moduleDbId, moduleId
        , targetTableName);
      if (targetTable != null)
      {
        retTableId = targetTable.Id;
        tableDbId = targetTable.DbId;
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

      //var ljcItem = ModuleCombo.SelectedItem as LJCItem;
      //var moduleDbID = ljcItem.DbID;
      //var moduleID = ljcItem.ID;
      // Testing
      short moduleDbId = 1;
      var moduleId = 1;

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
        }
        var items = TableManager.Load(keyColumns);
        if (LJC.HasListItems(items))
        {
          foreach (var item in items)
          {
            RowAdd(item);
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
      var retValue = TableGrid.LJCRowAdd();
      if (retValue != null)
      {
        SetStoredValues(retValue, data);
        retValue.LJCSetValues(data);
      }
      return retValue;
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
          var rowDbId = row.LJCGetInt16(DataUtilTable.ColumnDbId);
          var rowID = row.LJCGetInt64(DataUtilTable.ColumnId);
          if (rowDbId == data.DbId
            && rowID == data.Id)
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
      //bool enableNew = ModuleCombo.CurrentRow != null;
      bool enableNew = true;
      bool enableEdit = TableGrid.CurrentRow != null;
      FormCommon.SetMenuState(TableMenu, enableNew, enableEdit);
      //ParentObject.ColumnHeading.Enabled = true;
    }

    // Sets the row stored values.
    private static void SetStoredValues(LJCGridRow row, DataUtilTable data)
    {
      row.LJCSetInt16(DataUtilTable.ColumnDbId, data.DbId);
      row.LJCSetInt64(DataUtilTable.ColumnId, data.Id);
      row.LJCSetString(DataUtilTable.ColumnName, data.Name);
    }
    #endregion

    #region Action Methods

    // Deletes the selected row.
    internal void Delete()
    {
      //bool isContinue = true;
      //var row = TableGrid.CurrentRow as LJCGridRow;
      //if (TableGrid.CurrentRow is LJCGridRow row)
      //{
      //  var title = "Delete Confirmation";
      //  var message = FormCommon.DeleteConfirm;
      //  if (DialogResult.No == MessageBox.Show(message, title
      //    , MessageBoxButtons.YesNo, MessageBoxIcon.Question))
      //  {
      //    isContinue = false;
      //  }
      //}

      //if (isContinue)
      //{
      //  var id = ParentObject.DataColumnRowId(out short dbId);
      //  var keyColumns = new LJCDataColumns()
      //  {
      //    { DataUtilColumn.ColumnId, id },
      //    { DataUtilColumn.ColumnDbId, dbId },
      //  };
      //  ColumnManager.Delete(keyColumns);
      //  if (0 == ColumnManager.AffectedCount)
      //  {
      //    isContinue = false;
      //    var message = FormCommon.DeleteError;
      //    MessageBox.Show(message, "Delete Error", MessageBoxButtons.OK
      //      , MessageBoxIcon.Exclamation);
      //  }
      //}

      //if (isContinue)
      //{
      //  ColumnGrid.Rows.Remove(row);
      //  SetControlState();
      //  ParentObject.TimedChange(Change.Column);
      //}
    }

    // Displays a detail dialog to edit a record.
    internal void Edit()
    {
      //if (ModuleCombo.CurrentRow is LJCGridRow
      if (TableGrid.CurrentRow is LJCGridRow)
      {
        //var id = ParentObject.DataColumnRowId(out short dbId);
        //var parentId = ParentObject.DataTableRowId(out short parentDbId);
        //string parentName = ParentObject.DataTableRowName();
        //var location = FormPoint.DialogScreenPoint(ColumnGrid);
        //var detail = new DataColumnDetail()
        //{
        //  LJCId = id,
        //  LJCDbId = dbId,
        //  LJCLocation = location,
        //  LJCManagers = Managers,
        //  LJCParentId = parentId,
        //  LJCParentDbId = parentDbId,
        //  LJCParentName = parentName,
        //};
        //detail.LJCChange += Detail_Change;
        //detail.LJCLocation = FormPoint.AdjustedLocation(detail, location);
        //detail.ShowDialog();
        //detail.Dispose();
      }
    }

    // Displays a detail dialog for a new record.
    internal void New()
    {
      //if (ModuleCombo.CurrentRow is LJCGridRow)
      //{
      //int sequence = ColumnGrid.Rows.Count + 1;
      //var parentId = ParentObject.DataTableRowId(out short parentDbId);
      //string parentName = ParentObject.DataTableRowName();
      //var location = FormPoint.DialogScreenPoint(ColumnGrid);
      //var detail = new DataColumnDetail
      //{
      //  LJCLocation = location,
      //  LJCManagers = Managers,
      //  LJCParentId = parentId,
      //  LJCParentDbId = parentDbId,
      //  LJCParentName = parentName,
      //  LJCSequence = sequence
      //};
      //detail.LJCChange += Detail_Change;
      //detail.LJCLocation = FormPoint.AdjustedLocation(detail, location);
      //detail.ShowDialog();
      //detail.Dispose();
      //}
    }

    // Refreshes the list.
    internal void Refresh()
    {
      ParentObject.Cursor = Cursors.WaitCursor;
      long id = 0;
      short dbId = 0;
      if (TableGrid.CurrentRow is LJCGridRow)
      {
        // Save the original row.
        id = RowId(out dbId);
      }
      DataRetrieve();

      // Select the original row.
      if (id > 0)
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

    // Adds new row or updates row with control values.
    private void Detail_Change(object? sender, EventArgs e)
    {
      //var detail = sender as DataColumnDetail;
      //var record = detail.LJCRecord;
      //if (record != null)
      //{
      //  if (detail.LJCIsUpdate)
      //  {
      //    RowUpdate(record);
      //    Refresh();
      //  }
      //  else
      //  {
      //    // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
      //    var row = RowAdd(record);
      //    ColumnGrid.LJCSetCurrentRow(row, true);
      //    SetControlState();
      //    ParentObject.TimedChange(Change.Column);
      //  }
      //}
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

          //case Keys.Tab:
          //  if (e.Shift)
          //  {
          //    ParentObject.ColumnTabs.Select();
          //  }
          //  else
          //  {
          //    ParentObject.ColumnTabs.Select();
          //  }
          //  e.Handled = true;
          //  break;
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
    #endregion

    #region Properties

    // Gets or sets the Parent List reference.
    private DataUtilityList ParentObject { get; set; }

    // Gets or sets the Managers reference.
    private ManagersDataUtility Managers { get; set; }

    // Gets or sets the Grid reference.
    private LJCDataGrid TableGrid { get; set; } = null!;

    // Gets or sets the Menu reference.
    private ContextMenuStrip TableMenu { get; set; }

    // Gets or sets the Manager reference.
    private DataTableManager TableManager { get; set; } = null!;
    #endregion
  }
}
