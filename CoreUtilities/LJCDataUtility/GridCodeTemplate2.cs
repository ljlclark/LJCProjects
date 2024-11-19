// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// #CommentChars //
// #PlaceholderBegin _
// #PlaceholderEnd _
// #SectionBegin Title
// #Value _ClassName_
// #Value _FullAppName_
// _ClassName_GridCode.cs
using _FullAppName_DAL;
using static _FullAppName_._FullAppName_List;
// #SectionEnd Title
using LJCDBMessage;
using LJCNetCommon;
using LJCWinFormCommon;
using LJCWinFormControls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

// #SectionBegin Class
// #Value _AppName_
// #Value _ClassName_
// #Value _CollectionName_
// #Value _FullAppName_
// #Value _Namespace_
// #Value _ParentName_
namespace _Namespace_
{
  // Provides _ClassName_Grid methods for the _AppName_List window.
  internal class _ClassName_GridCode
  {
    #region Constructors

    // Initializes an object instance.
    // ********************
    internal _ClassName_GridCode(_FullAppName_List parentList)
    {
      // Initialize property values.
      _AppName_List = parentList;
      _AppName_tList.Cursor = Cursors.WaitCursor;

      _ParentName_Grid = _AppName_List._ParentName_Grid;
      _ClassName_Grid = _AppName_List._ClassName_Grid;
      var _ClassName_Menu = _AppName_List._ClassName_Menu;

      ResetData();

      var fontFamily = _AppName_List.Font.FontFamily;
      var style = _AppName_List.Font.Style;
      ModuleGrid.Font = new Font(fontFamily, 11, style);
      moduleMenu.Font = new Font(fontFamily, 11, style);
      _ = new GridFont(_AppName_List, _ClassName_Grid);
      _ = new MenuFont(_ClassName_Menu);

      // Menu item events.
      var list = _AppName_List;
      list._ClassName_New.Click += _ClassName_New_Click;
      list._ClassName_Edit.Click += _ClassName_Edit_Click;
      list._ClassName_Delete.Click += _ClassName_Delete_Click;
      list._ClassName_Refresh.Click += _ClassName_Refresh_Click;
      list._ClassName_Exit.Click += list.Exit_Click;

      // Grid events.
      var grid = _ClassName_Grid;
      grid.KeyDown += _ClassName_Grid_KeyDown;
      grid.MouseDoubleClick += _ClassName_Grid_MouseDoubleClick;
      grid.MouseDown += ColumnGrid_MouseDown;
      grid.SelectionChanged += ColumnGrid_SelectionChanged;
      _AppName_List.Cursor = Cursors.Default;
    }

    // Resets the DataConfig dependent objects.
    // ********************
    internal void ResetData()
    {
      Managers = _AppName_List.Managers;
      _ClassName_Manager = Managers._ClassName_Manager;
    }
    #endregion

    #region Data Methods

    // Retrieves the combo items.
    // ********************
    internal void DataRetrieveCombo()
    {
      //ComboRecords dataRecords;

      //Cursor = Cursors.WaitCursor;
      //Combo.Items.Clear();

      //dataRecords = mComboManager.Load();

      //if (dataRecords != null && records.Count > 0)
      //{
      //	foreach (ComboRecord dataRecord in dataRecords)
      //	{
      //		Combo.Items.Add(dataRecord);
      //	}
      //	if (Combo.Items.Count > 0)
      //	{
      //		Combo.SelectedIndex = 0;
      //	}
      //}
      //Cursor = Cursors.Default;
    }

    // Retrieves the list rows.
    // ********************
    internal void DataRetrieve()
    {
      _AppName_List.Cursor = Cursors.WaitCursor;
      _ClassName_Grid.LJCRowsClear();

      if (_ParentName_Grid.CurrentRow is LJCGridRow parentRow)
      {
        // Data from items.
        int parentID = parentRow.LJCGetInt32(_ParentName_.ColumnID);

        var result = _ClassName_Manager.ResultWithParentID(parentID);
        if (DbResult.HasRows(result))
        {
          foreach (var dbRow in result.Rows)
          {
            RowAddValues(dbRow.Values);
          }
        }
      }
      _AppName_List.Cursor = Cursors.Default;
      _AppName_List.DoChange(Change._ClassName_);
    }

    // Adds a grid row and updates it with the record values.
    // ********************
    private LJCGridRow RowAdd(_ClassName_ dataRecord)
    {
      var retValue = _ClassName_Grid.LJCRowAdd();
      SetStoredValues(retValue, dataRecord);
      retValue.LJCSetValues(_ClassName_Grid, dataRecord);
      return retValue;
    }

    // Adds a grid row and updates it with the result values.
    // ********************
    private LJCGridRow RowAddValues(DbValues dbValues)
    {
      var retValue = _ClassName_Grid.LJCRowAdd();

      var columnName = _ClassName_.ColumnID;
      var id = dbValues.LJCGetInt32(columnName);
      retValue.LJCSetInt32(columnName, id);

      retValue.LJCSetValues(_ClassName_Grid, dbValues);
      return retValue;
    }

    // Selects a row based on the key record values.
    // ********************
    private bool RowSelect(_ClassName_ dataRecord)
    {
      bool retValue = false;

      if (dataRecord != null)
      {
        _AppName_List.Cursor = Cursors.WaitCursor;
        foreach (LJCGridRow row in _ClassName_Grid.Rows)
        {
          var rowID = row.LJCGetInt32(_ClassName_.ColumnID);
          if (rowID == dataRecord.ID)
          {
            // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
            _ClassName_Grid.LJCSetCurrentRow(row, true);
            retValue = true;
            break;
          }
        }
        _AppName_List.Cursor = Cursors.Default;
      }
      return retValue;
    }

    // Updates the current row with the record values.
    // ********************
    private void RowUpdate(_ClassName_ dataRecord)
    {
      if (_ClassName_Grid.CurrentRow is LJCGridRow row)
      {
        SetStoredValues(row, dataRecord);
        row.LJCSetValues(_ClassName_Grid, dataRecord);
      }
    }

    // Sets the row stored values.
    // ********************
    private void SetStoredValues(LJCGridRow row, _ClassName_ dataRecord)
    {
      row.LJCSetInt32(_ClassName_.ColumnID, dataRecord.ID);
      row.LJCSetString(_ClassName_.ColumnNamr, dataRecord.Name);
    }
    #endregion

    #region Action Methods

    // Performs the default list action.
    // ********************
    internal void DoDefault()
    {
      if (LJCIsSelect)
      {
        DoSelect();
      }
      else
      {
        DoEdit();
      }
    }

    // Deletes the selected row.
    // ********************
    internal void Delete()
    {
      bool success = false;
      var row = _ClassName_Grid.CurrentRow as LJCGridRow;
      if (_ParentName_Grid.CurrentRow is LJCGridRow parentRow
        && row != null)
      {
        var title = "Delete Confirmation";
        var message = FormCommon.DeleteConfirm;
        if (MessageBox.Show(message, title, MessageBoxButtons.YesNo
          , MessageBoxIcon.Question) == DialogResult.Yes)
        {
          success = true;
        }
      }

      //int id = 0;
      if (success)
      {
        // Data from items.
        var id = row.LJCGetInt32(_ClassName_.ColumnID);

        var keyColumns = new DbColumns()
        {
          { _ClassName_.ColumnID, id }
        };
        _ClassName_Manager.Delete(keyColumns);
        if (0 == _ClassName_Manager.AffectedCount)
        {
          success = false;
          var message = FormCommon.DeleteError;
          MessageBox.Show(message, "Delete Error", MessageBoxButtons.OK
            , MessageBoxIcon.Exclamation);
        }
      }

      if (success)
      {
        _ClassName_Grid.Rows.Remove(row);
        _AppName_List.TimedChange(Change._ClassName_);
      }
    }

    // Displays a detail dialog to edit a record.
    // ********************
    internal void Edit()
    {
      if (_ParentName_Grid.CurrentRow is LJCGridRow parentRow
        && _ClassName_Grid.CurrentRow is LJCGridRow row)
      {
        // Data from items.
        int id = row.LJCGetInt32(_ClassName_.ColumnID);
        int parentID = parentRow.LJCGetInt32(_ParentName_.ColumnID);
        string parentName = parentRow.LJCGetString(_ParentName_.ColumnName);

        var location = FormCommon.GetDialogScreenPoint(_ClassName_Grid);
        var detail = new _ClassName_Detail()
        {
          LJCID = id,
          LJCLocation = location,
          LJCManagers = Managers,
          LJCParentID = parentID,
          LJCParentName = parentName,
        };
        detail.LJCChange += Detail_Change;
        detail.ShowDialog();
      }
    }

    // Displays a detail dialog for a new record.
    // ********************
    internal void New()
    {
      if (_ParentName_Grid.CurrentRow is LJCGridRow parentRow)
      {
        // Data from list items.
        int parentID = parentRow.LJCGetInt32(_ParentName_.ColumnID);
        string parentName = parentRow.LJCGetString(_ParentName_.ColumnName);

        var location = FormCommon.GetDialogScreenPoint(_ClassName_Grid);
        var detail = new _ClassName_Detail
        {
          LJCLocation = location,
          LJCManagers = Managers,
          LJCParentID = parentID,
          LJCParentName = parentName
        };
        detail.LJCChange += Detail_Change;
        detail.ShowDialog();
      }
    }

    // Refreshes the list.
    // ********************
    internal void Refresh()
    {
      _AppName_List.Cursor = Cursors.WaitCursor;
      int id = 0;
      if (_ClassName_Grid.CurrentRow is LJCGridRow row)
      {
        // Save the original row.
        id = row.LJCGetInt32(_ClassName_.ColumnID);
      }
      DataRetrieve();

      // Select the original row.
      if (id > 0)
      {
        var record = new _ClassName_()
        {
          ID = id
        };
        RowSelect(record);
      }
      _AppName_List.Cursor = Cursors.Default;
    }

    // Sets the selected item and returns to the parent form.
    // ********************
    internal void DoSelect()
    {
      LJCSelectedRecord = null;
      if (_ClassName_Grid.CurrentRow is LJCGridRow row)
      {
        _AppName_List.Cursor = Cursors.WaitCursor;
        var id = row.LJCGetInt32(_ClassName_.ColumnID);

        var manager = Managers._ClassName_Manager;
        var keyRecord = manager.GetIDKey(id);
        var dataRecord = manager.Retrieve(keyRecord);
        if (dataRecord != null)
        {
          LJCSelectedRecord = dataRecord;
        }
        _AppName_List.Cursor = Cursors.Default;
      }
      _AppName_List.DialogResult = DialogResult.OK;
    }

    // Shows the help page
    // ********************
    internal void ShowHelp()
    {
      Help.ShowHelp(DocList, "_AppName_.chm", HelpNavigator.Topic
        , "_ClassName_List.html");
    }

    // Adds new row or updates row with control values.
    // ********************
    private void Detail_Change(object sender, EventArgs e)
    {
      var detail = sender as _ClassName_Detail;
      var record = detail.LJCRecord;
      if (record != null)
      {
        if (detail.LJCIsUpdate)
        {
          RowUpdate(record);
          //CheckPreviousAndNext(detail);
          //DoRefresh();
        }
        else
        {
          // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
          var row = RowAdd(record);
          _ClassName_Grid.LJCSetCurrentRow(row, true);
          //CheckPreviousAndNext(detail);
          //DoRefresh();
          _AppName_List.TimedChange(Change._ClassName_);
        }
      }
    }
    #endregion

    #region Setup and Other Methods

    // Configures the _ClassName_ Grid.
    // ********************
    internal void SetupGrid()
    {
      // Setup default grid columns if no columns are defined.
      if (0 == _ClassName_Grid.Columns.Count)
      {
        List<string> propertyNames = new List<string>()
        {
          _ClassName_.ColumnName,
          _ClassName_.ColumnDescription
        };

        // Get the grid columns from the manager Data Definition.
        var manager = _ClassName_Manager;
        var gridColumns = manager.GetColumns(propertyNames);

        // Setup the grid columns.
        _ClassName_Grid.LJCAddColumns(gridColumns);
        //m_ClassName_Grid.LJCDragDataName = "_ClassName_";
      }
    }
    #endregion

    #region Get Data Methods

    // Retrieves the row item.
    // ********************
    private _ClassName_ Get_ClassName_(LJCGridRow row = null)
    {
      _ClassName_ retValue = null;

      if (_ClassName_Grid.CurrentRow is LJCGridRow row)
      {
        var id = _ClassName_ID(row);
        if (id > 0)
        {
          var manager = Managers._ClassName_Manager;
          retValue = manager.RetrieveWithID(id);
        }
      }
      return retValue;
    }

    // Retrieves the current row item ID.
    // ********************
    private long _ClassName_ID(LJCGridRow row = null)
    {
      long retValue = 0;

      if (null == row)
      {
        row = _ClassName_Grid.CurrentRow as LJCGridRow;
      }
      if (row != null)
      {
        retValue = row.LJCGetInt64(_ClassName_.ColumnID);
      }
      return retValue;
    }
    #endregion

    #region Private Methods

    // Checks for Previous and Next items.
    // ********************
    private void CheckPreviousAndNext(_ClassName_Detail detail)
    {
      PreviousItem(detail);
      NextItem(detail);
    }

    // Checks for Next item.
    // ********************
    private void NextItem(_ClassName_Detail detail)
    {
      if (detail.LJCNext)
      {
        LJCDataGrid grid = _ClassName_Grid;
        int currentIndex = grid.CurrentRow.Index;
        detail.LJCNext = false;
        if (currentIndex < grid.Rows.Count - 1)
        {
          grid.LJCSetCurrentRow(currentIndex + 1, true);
          var id = _ClassName_ID();
          if (id > 0)
          {
            detail.ID = id;
            detail.LJCNext = true;
          }
        }
      }
    }

    // Checks for Previous item.
    // ********************
    private void PreviousItem(_ClassName_Detail detail)
    {
      if (detail.LJCPrevious)
      {
        LJCDataGrid grid = _ClassName_Grid;
        int currentIndex = grid.CurrentRow.Index;
        detail.LJCPrevious = false;
        if (currentIndex > 0)
        {
          grid.LJCSetCurrentRow(currentIndex - 1, true);
          var id = _ClassName_ID();
          if (id > 0)
          {
            detail.ID = id;
            detail.LJCPrevious = true;
          }
        }
      }
    }
    #endregion

    #region Action Event Handlers

    // Handles the New menu item event.
    // ********************
    private void _ClassName_New_Click(object sender, EventArgs e)
    {
      New();
    }

    // Handles the Edit menu item event.
    // ********************
    private void _ClassName_Edit_Click(object sender, EventArgs e)
    {
      Edit();
    }

    // Handles the Delete menu item event.
    // ********************
    private void _ClassName_Delete_Click(object sender, EventArgs e)
    {
      Delete();
    }

    // Handles the Refresh menu item event.
    // ********************
    private void _ClassName_Refresh_Click(object sender, EventArgs e)
    {
      Refresh();
    }
    #endregion

    #region Control Event Handlers

    // Handles the Grid Doubleclick event.
    // ********************
    private void _ClassName_Grid_MouseDoubleClick(object sender, MouseEventArgs e)
    {
      if (_ClassName_Grid.LJCGetMouseRow(e) != null)
      {
        Edit();
      }
    }

    // Handles the Grid KeyDown event.
    // ********************
    private void _ClassName_Grid_KeyDown(object sender, KeyEventArgs e)
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
            var position = FormCommon.GetMenuScreenPoint(ColumnGrid
              , Control.MousePosition);
            var menu = _AppName_List.ColumnMenu;
            menu.Show(position);
            menu.Select();
            e.Handled = true;
          }
          break;

        case Keys.Tab:
          if (e.Shift)
          {
            _AppName_List._AppName_Tabs.Select();
          }
          else
          {
            _AppName_List._AppName_Tabs.Select();
          }
          e.Handled = true;
          break;
      }
    }

    // Handles the MouseDown event.
    private void _ClassName_Grid_MouseDown(object sender, MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Right)
      {
        // LJCIsDifferentRow() Sets the LJCLastRowIndex for new row.
        _ClassName_Grid.Select();
        if (_ClassName_Grid.LJCIsDifferentRow(e))
        {
          // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
          _ClassName_Grid.LJCSetCurrentRow(e);
          _AppName_List.TimedChange(Change._ClassName_);
        }
      }
    }

    // Handles the SelectionChanged event.
    private void _ClassName_Grid_SelectionChanged(object sender, EventArgs e)
    {
      if (_ClassName_Grid.LJCAllowSelectionChange)
      {
        _AppName_List.TimedChange(Change._ClassName_);
      }
      _ClassName_Grid.LJCAllowSelectionChange = true;
    }
    #endregion

    #region Properties

    // Gets or sets the Parent List reference.
    private _FullAppName_List _AppName_List { get; set; }

    // Gets or sets the _ParentName_ Grid reference.
    private LJCDataGrid _ParentName_Grid { get; set; }

    // Gets or sets the _ClassName_ Grid reference.
    private LJCDataGrid _ClassName_Grid { get; set; }

    // Gets or sets the Managers reference.
    private Managers_AppName_ Managers { get; set; }

    // Gets or sets the Manager reference.
    private _ClassName_Manager _ClassName_Manager { get; set; }

    // Gets or sets the Selected Record reference.
    private _ClassName_ LJCSelectedRecord { get; set; }
    #endregion
  }
}
// #SectionEnd Class

