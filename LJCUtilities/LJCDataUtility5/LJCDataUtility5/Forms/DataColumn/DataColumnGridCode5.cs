// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DataColumnGridCode5.cs
using LJCControls5;
using LJCDataUtilityDAL5;
using LJCNetCommon5;
using static LJCDataUtility5.DataUtilityList;

namespace LJCDataUtility5
{
  // Provides methods for the DataColumn grid.
  internal class DataColumnGridCode
  {
    #region Constructor Methods

    // Initializes an object instance.
    internal DataColumnGridCode(DataUtilityList parentObject, short dbGroupId)
    {
      // Initialize property values.
      ParentObject = parentObject;
      ParentObject.Cursor = Cursors.WaitCursor;
      DbGroupId = dbGroupId;

      // Set control code vars.
      TableGrid = ParentObject.TableGrid;
      ColumnGrid = ParentObject.ColumnGrid;
      ColumnMenu = ParentObject.ColumnMenu;

      // Set Data vars.
      Managers = ParentObject.Managers;
      Reset();

      // Menu item events.
      var list = ParentObject;
      list.ColumnNew.Click += ColumnNew_Click;
      list.ColumnEdit.Click += ColumnEdit_Click;
      list.ColumnDelete.Click += ColumnDelete_Click;
      list.ColumnRefresh.Click += ColumnRefresh_Click;
      list.ColumnExit.Click += list.Exit_Click;

      // Grid events.
      var grid = ColumnGrid;
      grid.KeyDown += ColumnGrid_KeyDown;
      grid.MouseDoubleClick += ColumnGrid_MouseDoubleClick;
      grid.MouseDown += ColumnGrid_MouseDown;
      grid.SelectionChanged += ColumnGrid_SelectionChanged;
      grid.MouseEnter += Grid_MouseEnter;

      ParentObject.Cursor = Cursors.Default;
    }

    // Resets the data manager.
    internal void Reset()
    {
      if (!LJC.Equals(CurrentDataConfigName, Managers.DataConfigName))
      {
        ColumnManager = Managers.DataColumnManager;
        CurrentDataConfigName = Managers.DataConfigName;
        var error = Managers.Error;
        if (LJC.HasText(error))
        {
          MessageBox.Show(error);
        }
      }
    }

    // Configures the DataColumn Grid.
    internal void SetupGrid()
    {
      // Setup default grid columns if no columns are defined.
      if (0 == ColumnGrid.Columns.Count)
      {
        var propertyNames = new List<string>()
        {
          DataUtilColumn.ColumnName,
          DataUtilColumn.ColumnDescription,
          DataUtilColumn.ColumnSequence,
          DataUtilColumn.ColumnTypeName,
          DataUtilColumn.ColumnMaxLength,
          DataUtilColumn.ColumnAllowNull,
          DataUtilColumn.ColumnDefaultValue
        };

        if (ColumnManager != null)
        {
          // Get the grid columns from the manager Data Definition.
          var gridColumns = ColumnManager.Columns(propertyNames);

          // Setup the grid columns.
          if (gridColumns != null)
          {
            ColumnGrid.LJCAddColumns(gridColumns);
          }
        }
      }
    }
    #endregion

    #region Data Value Methods

    // Gets the current Column Grid row.
    internal LJCGridRow? Row()
    {
      var retRow = ColumnGrid.CurrentRow as LJCGridRow;
      return retRow;
    }

    // Gets the selected row ID.
    internal long RowId(out short dbId, LJCGridRow? row = null)
    {
      long retColumnId = 0;

      dbId = 0;
      row ??= Row();
      if (row != null
        && row.DataGridView != null
        && "ColumnGrid" == row.DataGridView.Name)
      {
        dbId = row.LJCGetInt16(DataUtilColumn.ColumnDbId);
        retColumnId = row.LJCGetInt64(DataUtilColumn.ColumnId);
      }
      return retColumnId;
    }

    // Gets the selected row Name.
    internal string? RowName(LJCGridRow? row = null)
    {
      string? retColumnName = null;

      row ??= Row();
      if (row != null
        && row.DataGridView != null
        && "ColumnGrid" == row.DataGridView.Name)
      {
        retColumnName = row.LJCGetString(DataUtilColumn.ColumnName);
      }
      return retColumnName;
    }
    #endregion

    #region Data Methods

    // Retrieves the list rows.
    internal void DataRetrieve()
    {
      ParentObject.Cursor = Cursors.WaitCursor;
      ColumnGrid.LJCRowsClear();

      // Parent grid has a selection.
      if (TableGrid.CurrentRow is LJCGridRow)
      {
        var tableGridCode = ParentObject.TableGridCode;
        var tableId = tableGridCode.RowId(out short tableDbId);
        var keyColumns = DataColumnManager.ParentKey(tableDbId, tableId);
        var orderByNames = new List<string>()
        {
          DataUtilColumn.ColumnSequence
        };

        if (ColumnManager != null
          && ColumnManager.Manager != null)
        {
          ColumnManager.Manager.OrderByNames = orderByNames;
          var items = ColumnManager.Load(keyColumns);
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
      ParentObject.DoChange(Change.Column);
    }

    // Adds a grid row and updates it with the record values.
    private LJCGridRow? RowAdd(DataUtilColumn data)
    {
      var retValue = ColumnGrid.LJCRowAdd();
      if (retValue != null)
      {
        SetStoredValues(retValue, data);
        retValue.LJCSetValues(data);
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
        var data = new DataUtilColumn()
        {
          DbId = dbId,
          Id = id,
        };
        retValue = RowSelect(data);
      }
      return retValue;
    }

    // Selects a row based on the data values.
    private bool RowSelect(DataUtilColumn data)
    {
      bool retValue = false;

      if (data != null)
      {
        ParentObject.Cursor = Cursors.WaitCursor;
        foreach (LJCGridRow row in ColumnGrid.Rows)
        {
          var rowId = RowId(out short rowDbId, row);
          if (rowDbId == data.DbId
            && rowId == data.Id)
          {
            // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
            ColumnGrid.LJCSetCurrentRow(row, true);
            retValue = true;
            break;
          }
        }
        ParentObject.Cursor = Cursors.Default;
      }
      return retValue;
    }

    // Updates the current row with the record values.
    private void RowUpdate(DataUtilColumn data)
    {
      if (ColumnGrid.CurrentRow is LJCGridRow row)
      {
        SetStoredValues(row, data);
        row.LJCSetValues(data);
      }
    }

    // Sets the control states based on the current control values.
    private void SetControlState()
    {
      bool enableNew = TableGrid.CurrentRow != null;
      bool enableEdit = ColumnGrid.CurrentRow != null;
      FormCommon.SetMenuState(ColumnMenu, enableNew, enableEdit);
      //ParentObject.ColumnHeading.Enabled = true;
    }

    // Sets the row stored values.
    private static void SetStoredValues(LJCGridRow row, DataUtilColumn dataRecord)
    {
      row.LJCSetInt16(DataUtilColumn.ColumnDbId, dataRecord.DbId);
      row.LJCSetInt64(DataUtilColumn.ColumnId, dataRecord.Id);
      row.LJCSetString(DataUtilColumn.ColumnName, dataRecord.Name);
    }
    #endregion

    #region Action Methods

    // Test generating Table HTML Document.
    internal void ColumnTableHTML()
    {
      ParentObject.Cursor = Cursors.WaitCursor;
      if (TableGrid.CurrentRow is LJCGridRow)
      {
        //var fileName = "DataUtilColumn.html";
        //var genHTML = new GenHTMLTable(fileName);
        //var columnHTML = new ColumnHTMLTable(ParentObject);

        //var textState = new TextState();
        //DataTable dataTable;
        //var heading = "Data Columns";

        //// DataObject collection.
        //var dataColumns = columnHTML.GetDataColumns();
        //List<object> dataObjects = dataColumns.ToList<object>();
        //var propertyNames = columnHTML.GetColumnPropertyNames();
        //var dataHTML = genHTML.DataHTML(dataObjects, heading, propertyNames
        //  , textState);

        //// DataTable
        //dataTable = columnHTML.GetColumnDataTable();
        //var tableHTML = genHTML.DataTableHTML(dataTable, heading, textState);

        //// DbResult
        //var dbResult = columnHTML.GetColumnResult();
        //var resultHTML = genHTML.ResultHTML(dbResult, heading, textState);

        //// DataGridView
        //var gridHTML = genHTML.DataGridHTML(ColumnGrid, heading, textState);

        //File.WriteAllText(fileName, gridHTML);
        //ParentObject.Cursor = Cursors.Default;
        //NetFile.ShellProgram(null, fileName);
      }
      ParentObject.Cursor = Cursors.Default;
    }

    // Deletes the selected row.
    internal void Delete()
    {
      while (true)
      {
        var row = ColumnGrid.CurrentRow;
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
          { DataUtilColumn.ColumnDbId, dbId },
          { DataUtilColumn.ColumnId, id },
        };

        if (ColumnManager != null)
        {
          ColumnManager.Delete(keyColumns);
          if (0 == ColumnManager.AffectedCount)
          {
            var message = FormCommon.DeleteError;
            MessageBox.Show(message, "Delete Error", MessageBoxButtons.OK
              , MessageBoxIcon.Exclamation);
            break;
          }

          ColumnGrid.Rows.Remove(row);
          SetControlState();
        }
        ParentObject.TimedChange(Change.Column);
        break;
      }
    }

    // Displays a detail dialog to edit a record.
    internal void Edit()
    {
      // Parent grid and current grid have selections.
      if (TableGrid.CurrentRow is LJCGridRow
        && ColumnGrid.CurrentRow is LJCGridRow)
      {
        // Data from items.
        var id = RowId(out short dbId);
        var tableGridCode = ParentObject.TableGridCode;
        var tableId = tableGridCode.RowId(out short tableDbId);
        string? tableName = tableGridCode.RowName();

        var location = FormPoint.DialogScreenPoint(ColumnGrid);
        var detail = new DataColumnDetail()
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
        // Data from items.
        var tableGridCode = ParentObject.TableGridCode;
        int sequence = ColumnGrid.Rows.Count + 1;
        var tableId = tableGridCode.RowId(out short tableDbId);
        string? tableName = tableGridCode.RowName();

        var location = FormPoint.DialogScreenPoint(ColumnGrid);
        var detail = new DataColumnDetail
        {
          LJCTableDbId = tableDbId,
          LJCTableId = tableId,
          LJCTableName = tableName,
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
      if (ColumnGrid.CurrentRow is LJCGridRow)
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

    // Adds new row or updates row with control values.
    private void Detail_Change(object? sender, EventArgs e)
    {
      if (sender is DataColumnDetail detail)
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
              ColumnGrid.LJCSetCurrentRow(row, true);
              SetControlState();
              ParentObject.TimedChange(Change.Column);
            }
          }
        }
      }
    }
    #endregion

    #region Action Event Handlers

    // Handles the New menu item event.
    private void ColumnNew_Click(object? sender, EventArgs e)
    {
      New();
    }

    // Handles the Edit menu item event.
    private void ColumnEdit_Click(object? sender, EventArgs e)
    {
      Edit();
    }

    // Handles the Delete menu item event.
    private void ColumnDelete_Click(object? sender, EventArgs e)
    {
      Delete();
    }

    // Handles the Refresh menu item event.
    private void ColumnRefresh_Click(object? sender, EventArgs e)
    {
      Refresh();
    }

    // Handles the Column HTML menu item event.
    private void ColumnHTML_Click(object sender, EventArgs e)
    {
      ColumnTableHTML();
    }
    #endregion

    #region Control Event Handlers

    // Handles the Grid KeyDown event.
    private void ColumnGrid_KeyDown(object? sender, KeyEventArgs e)
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
            var position = FormPoint.MenuScreenPoint(ColumnGrid
              , Control.MousePosition);
            var menu = ParentObject.ColumnMenu;
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
    private void ColumnGrid_MouseDoubleClick(object? sender, MouseEventArgs e)
    {
      if (ColumnGrid.LJCGetMouseRow(e) != null)
      {
        Edit();
      }
    }

    // Handles the MouseDown event.
    private void ColumnGrid_MouseDown(object? sender, MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Right)
      {
        // LJCIsDifferentRow() Sets the LJCLastRowIndex for new row.
        if (ColumnGrid.LJCIsDifferentRow(e))
        {
          // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
          ColumnGrid.LJCSetCurrentRow(e);
          SetControlState();
          ParentObject.TimedChange(Change.Column);
        }
      }
    }

    // Handles the SelectionChanged event.
    private void ColumnGrid_SelectionChanged(object? sender, EventArgs e)
    {
      if (ColumnGrid.LJCAllowSelectionChange)
      {
        SetControlState();
        ParentObject.TimedChange(Change.Column);
      }
      ColumnGrid.LJCAllowSelectionChange = true;
    }

    // Handles the MouseEnter event.
    private void Grid_MouseEnter(object? sender, EventArgs e)
    {
      ColumnGrid.Focus();
    }
    #endregion

    #region Properties

    // Gets or sets the current data config name.
    private string? CurrentDataConfigName { get; set; }

    // Gets or sets the Grid reference.
    private LJCDataGrid ColumnGrid { get; set; }

    // Gets or sets the Manager reference.
    private DataColumnManager? ColumnManager { get; set; }

    // Gets or sets the Menu reference.
    private ContextMenuStrip ColumnMenu { get; set; }

    // Gets or sets the database id.
    internal short DbGroupId { get; set; }

    // Gets or sets the Managers reference.
    private ManagersDataUtility Managers { get; set; }

    // Gets or sets the parent List reference.
    private DataUtilityList ParentObject { get; set; }

    // Gets or sets the parent Grid reference.
    private LJCDataGrid TableGrid { get; set; }
    #endregion
  }
}
