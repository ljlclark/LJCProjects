// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DataColumnGridCode.cs
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

namespace LJCDataUtility
{
  // Provides DataColumnGrid methods for the DataUtilityList window.
  internal class DataColumnGridCode
  {
    #region Constructors

    // Initializes an object instance.
    internal DataColumnGridCode(DataUtilityList parentList)
    {
      // Initialize property values.
      Parent = parentList;
      Parent.Cursor = Cursors.WaitCursor;

      // Set Grid vars.
      TableGrid = Parent.TableGrid;
      ColumnGrid = Parent.ColumnGrid;
      ColumnMenu = Parent.ColumnMenu;
      Managers = Parent.Managers;
      ColumnManager = Managers.DataColumnManager;

      // Set Fonts
      var fontFamily = Parent.Font.FontFamily;
      var style = Parent.Font.Style;
      ColumnGrid.Font = new Font(fontFamily, 11, style);
      ColumnMenu.Font = new Font(fontFamily, 11, style);

      // Font change objects.
      GridFont = new GridFont(Parent, ColumnGrid);
      GridFont.FontChange += GridFont_FontChange;
      MenuFont = new MenuFont(ColumnMenu);
      MenuFont.FontChange += MenuFont_FontChange;

      // Menu item events.
      var list = Parent;
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
      Parent.Cursor = Cursors.Default;
    }
    #endregion

    #region Data Methods

    // Retrieves the list rows.
    internal void DataRetrieve()
    {
      Parent.Cursor = Cursors.WaitCursor;
      ColumnGrid.LJCRowsClear();

      if (TableGrid.CurrentRow is LJCGridRow)
      {
        int parentID = Parent.DataTableID();
        var keyColumns = ColumnManager.ParentKey(parentID);
        var orderByNames = new List<string>()
        {
          DataUtilColumn.ColumnSequence
        };
        ColumnManager.Manager.OrderByNames = orderByNames;
        var items = ColumnManager.Load(keyColumns);
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
      Parent.DoChange(Change.Column);
    }

    // Configures the DataColumn Grid.
    internal void SetupGrid()
    {
      // Setup default grid columns if no columns are defined.
      if (0 == ColumnGrid.Columns.Count)
      {
        List<string> propertyNames = new List<string>()
        {
          DataUtilColumn.ColumnDataTableID,
          DataUtilColumn.ColumnName,
          DataUtilColumn.ColumnDescription,
          DataUtilColumn.ColumnSequence,
          DataUtilColumn.ColumnTypeName,
          DataUtilColumn.ColumnMaxLength,
          DataUtilColumn.ColumnAllowNull
        };

        // Get the grid columns from the manager Data Definition.
        var manager = ColumnManager;
        var gridColumns = manager.Columns(propertyNames);

        // Setup the grid columns.
        ColumnGrid.LJCAddColumns(gridColumns);
      }
    }

    // Adds a grid row and updates it with the record values.
    private LJCGridRow RowAdd(DataUtilColumn dataRecord)
    {
      var retValue = ColumnGrid.LJCRowAdd();
      SetStoredValues(retValue, dataRecord);
      retValue.LJCSetValues(ColumnGrid, dataRecord);
      return retValue;
    }

    // Selects a row based on the key record values.
    private bool RowSelect(int id)
    {
      bool retValue = false;

      if (id > 0)
      {
        Parent.Cursor = Cursors.WaitCursor;
        foreach (LJCGridRow row in ColumnGrid.Rows)
        {
          var rowID = Parent.DataColumnID(row);
          if (rowID == id)
          {
            // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
            ColumnGrid.LJCSetCurrentRow(row, true);
            retValue = true;
            break;
          }
        }
        Parent.Cursor = Cursors.Default;
      }
      return retValue;
    }

    // Updates the current row with the record values.
    private void RowUpdate(DataUtilColumn dataRecord)
    {
      if (ColumnGrid.CurrentRow is LJCGridRow row)
      {
        SetStoredValues(row, dataRecord);
        row.LJCSetValues(ColumnGrid, dataRecord);
      }
    }

    // Sets the control states based on the current control values.
    private void SetControlState()
    {
      bool enableNew = TableGrid.CurrentRow != null;
      bool enableEdit = ColumnGrid.CurrentRow != null;
      FormCommon.SetMenuState(ColumnMenu, enableNew, enableEdit);
      Parent.ColumnHeading.Enabled = true;
    }

    // Sets the row stored values.
    private void SetStoredValues(LJCGridRow row, DataUtilColumn dataRecord)
    {
      row.LJCSetInt32(DataUtilColumn.ColumnID
        , dataRecord.ID);
      row.LJCSetString(DataUtilColumn.ColumnName
        , dataRecord.Name);
    }
    #endregion

    #region Action Methods

    // Deletes the selected row.
    internal void Delete()
    {
      bool success = false;
      var row = ColumnGrid.CurrentRow as LJCGridRow;
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
        var id = Parent.DataColumnID();
        var keyColumns = new DbColumns()
        {
          { DataUtilColumn.ColumnID, id }
        };
        ColumnManager.Delete(keyColumns);
        if (0 == ColumnManager.AffectedCount)
        {
          success = false;
          var message = FormCommon.DeleteError;
          MessageBox.Show(message, "Delete Error", MessageBoxButtons.OK
            , MessageBoxIcon.Exclamation);
        }
      }

      if (success)
      {
        ColumnGrid.Rows.Remove(row);
        SetControlState();
        Parent.TimedChange(Change.Column);
      }
    }

    // Displays a detail dialog to edit a record.
    internal void Edit()
    {
      if (TableGrid.CurrentRow is LJCGridRow
        && ColumnGrid.CurrentRow is LJCGridRow)
      {
        int id = Parent.DataColumnID();
        int parentID = Parent.DataTableID();
        string parentName = Parent.DataTableName();
        var location = FormPoint.DialogScreenPoint(ColumnGrid);
        var detail = new DataColumnDetail()
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
        var location = FormPoint.DialogScreenPoint(ColumnGrid);
        var detail = new DataColumnDetail
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
      if (ColumnGrid.CurrentRow is LJCGridRow)
      {
        // Save the original row.
        id = Parent.DataColumnID();
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
      var detail = sender as DataColumnDetail;
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
          ColumnGrid.LJCSetCurrentRow(row, true);
          SetControlState();
          Parent.TimedChange(Change.Column);
        }
      }
    }
    #endregion

    #region Action Event Handlers

    // Handles the New menu item event.
    private void ColumnNew_Click(object sender, EventArgs e)
    {
      New();
    }

    // Handles the Edit menu item event.
    private void ColumnEdit_Click(object sender, EventArgs e)
    {
      Edit();
    }

    // Handles the Delete menu item event.
    private void ColumnDelete_Click(object sender, EventArgs e)
    {
      Delete();
    }

    // Handles the Refresh menu item event.
    private void ColumnRefresh_Click(object sender, EventArgs e)
    {
      Refresh();
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
    private void ColumnGrid_KeyDown(object sender, KeyEventArgs e)
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
            var menu = Parent.ColumnMenu;
            menu.Show(position);
            menu.Select();
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
    private void ColumnGrid_MouseDoubleClick(object sender, MouseEventArgs e)
    {
      if (ColumnGrid.LJCGetMouseRow(e) != null)
      {
        Edit();
      }
    }

    // Handles the MouseDown event.
    private void ColumnGrid_MouseDown(object sender, MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Right)
      {
        // LJCIsDifferentRow() Sets the LJCLastRowIndex for new row.
        ColumnGrid.Select();
        if (ColumnGrid.LJCIsDifferentRow(e))
        {
          // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
          ColumnGrid.LJCSetCurrentRow(e);
          Parent.TimedChange(Change.Column);
        }
      }
    }

    // Handles the SelectionChanged event.
    private void ColumnGrid_SelectionChanged(object sender, EventArgs e)
    {
      if (ColumnGrid.LJCAllowSelectionChange)
      {
        Parent.TimedChange(Change.Column);
      }
      ColumnGrid.LJCAllowSelectionChange = true;
    }
    #endregion

    #region Properties

    // Gets or sets the parent List reference.
    private DataUtilityList Parent { get; set; }

    // Gets or sets the parent Grid reference.
    private LJCDataGrid TableGrid { get; set; }

    // Gets or sets the Grid reference.
    private LJCDataGrid ColumnGrid { get; set; }

    // Gets or sets the Menu reference.
    private ContextMenuStrip ColumnMenu { get; set; }

    // Gets or sets the Managers reference.
    private ManagersDataUtility Managers { get; set; }

    // Gets or sets the Manager reference.
    private DataColumnManager ColumnManager { get; set; }

    // Provides the Grid font event handlers.
    private GridFont GridFont { get; set; }

    // Provides the Menu font event handlers.
    private MenuFont MenuFont { get; set; }
    #endregion
  }
}
